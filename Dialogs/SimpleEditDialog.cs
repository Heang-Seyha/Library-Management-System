using LibraryManagementSystem.Helpers;

namespace LibraryManagementSystem.Dialogs
{
    /// <summary>
    /// A generic modern dialog with labeled text fields and inline validation.
    /// Used for master data entities (Category, Author, Publisher).
    /// 100% compatible with the Visual Studio WinForms Designer.
    /// </summary>
    public partial class SimpleEditDialog : Form
    {
        public string[] Values { get; private set; }
        private readonly TextBox[] _textBoxes;
        private readonly Label[] _errorLabels;
        private readonly (string label, string value, bool isPassword, int maxLength, bool isRequired)[] _fields;

        /// <summary>Parameterless constructor for WinForms Designer.</summary>
        public SimpleEditDialog() : this("Item", new[]
        {
            ("Name *", "", false, 100, true),
            ("Description", "", false, 255, false)
        })
        {
        }

        public SimpleEditDialog(
            string title,
            (string label, string value, bool isPassword, int maxLength, bool isRequired)[] fields)
        {
            _fields = fields;
            Values = new string[fields.Length];
            _textBoxes = new TextBox[fields.Length];
            _errorLabels = new Label[fields.Length];

            InitializeComponent();
            UIHelper.ApplyPaddingToAllTextBoxes(this, 8);

            this.Text = $"  Library Management System — {title}";
            lblTitle.Text = title;
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Location = new Point(20, 12);
            lblTitle.Size = new Size(500, 36);

            BuildFields();
        }
        private void BuildFields()
        {
            int fieldHeight = 88;
            int contentWidth = 500;
            pnlFields.Controls.Clear();
            pnlFields.Location = new Point(20, 64);
            pnlFields.Width = contentWidth;
            pnlFields.Height = _fields.Length * fieldHeight;

            int totalHeight = Math.Max(288, pnlFields.Bottom + 68);
            this.ClientSize = new Size(540, totalHeight);
            this.MinimumSize = new Size(540, totalHeight);

            // Re-position buttons at bottom of content with comfortable spacing
            btnCancel.Location = new Point(290, pnlFields.Bottom + 16);
            btnSave.Location = new Point(410, pnlFields.Bottom + 16);

            for (int i = 0; i < _fields.Length; i++)
            {
                int index = i;
                var field = _fields[i];

                string displayLabel = field.label.Trim();
                if (field.isRequired && !displayLabel.EndsWith("*"))
                {
                    displayLabel += " *";
                }

                var lbl = new Label
                {
                    Text = displayLabel,
                    Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                    ForeColor = Color.FromArgb(13, 59, 102),
                    Location = new Point(0, i * fieldHeight),
                    Size = new Size(contentWidth, 20),
                    AutoSize = false
                };

                var txt = new TextBox
                {
                    Text = field.value,
                    Font = new Font("Segoe UI", 9.5f),
                    ForeColor = Color.FromArgb(13, 59, 102),
                    Location = new Point(0, i * fieldHeight + 22),
                    Size = new Size(contentWidth, 29),
                    BorderStyle = BorderStyle.FixedSingle,
                    MaxLength = field.maxLength,
                    UseSystemPasswordChar = field.isPassword
                };

                var err = new Label
                {
                    Text = "",
                    Font = new Font("Segoe UI", 8f),
                    ForeColor = Color.FromArgb(220, 38, 38),
                    Location = new Point(0, i * fieldHeight + 54),
                    Size = new Size(contentWidth, 28),
                    UseMnemonic = false,
                    Visible = false
                };

                UIHelper.SetTextBoxLeftPadding(txt, 8);
                txt.TextChanged += (s, e) => ValidationHelper.ClearFieldError(txt, err);

                _textBoxes[index] = txt;
                _errorLabels[index] = err;

                pnlFields.Controls.Add(lbl);
                pnlFields.Controls.Add(txt);
                pnlFields.Controls.Add(err);
                err.BringToFront();
            }
        }

        private void btnCancel_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSave_Click(object? sender, EventArgs e)
        {
            bool hasErrors = false;
            for (int i = 0; i < _fields.Length; i++)
            {
                var field = _fields[i];
                var tb = _textBoxes[i];
                var errLbl = _errorLabels[i];
                string val = tb.Text.Trim();
                string cleanName = field.label.Trim().TrimEnd('*').Trim();

                if (field.isRequired && string.IsNullOrWhiteSpace(val))
                {
                    ValidationHelper.SetFieldError(tb, errLbl, $"{cleanName} is required.");
                    hasErrors = true;
                }
                else if (val.Length > field.maxLength)
                {
                    ValidationHelper.SetFieldError(tb, errLbl, $"{cleanName} cannot exceed {field.maxLength} characters.");
                    hasErrors = true;
                }
                else if (!string.IsNullOrWhiteSpace(val) && cleanName.Contains("Phone", StringComparison.OrdinalIgnoreCase) && !ValidationHelper.IsValidPhone(val))
                {
                    ValidationHelper.SetFieldError(tb, errLbl, "Invalid phone (must start with 0, 9-10 digits).");
                    hasErrors = true;
                }
                else if (!string.IsNullOrWhiteSpace(val) && cleanName.Contains("Email", StringComparison.OrdinalIgnoreCase) && !ValidationHelper.IsValidEmail(val))
                {
                    ValidationHelper.SetFieldError(tb, errLbl, "Invalid email address format.");
                    hasErrors = true;
                }
                else
                {
                    Values[i] = val;
                }
            }

            if (hasErrors) return;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
