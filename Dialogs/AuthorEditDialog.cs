using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Dialogs
{
    /// <summary>
    /// Modal dialog for adding or editing an Author with Gender and Date of Birth.
    /// 100% compatible with the Visual Studio WinForms Designer.
    /// </summary>
    public partial class AuthorEditDialog : Form
    {
        private readonly Author? _existing;

        /// <summary>Parameterless constructor for WinForms Designer.</summary>
        public AuthorEditDialog() : this(null)
        {
        }

        public AuthorEditDialog(Author? existing)
        {
            _existing = existing;
            InitializeComponent();
            UIHelper.ApplyPaddingToAllTextBoxes(this, 8);

            if (existing != null)
            {
                this.Text = "  Library Management System — Edit Author";
                lblHeader.Text = "Edit Author Information";
                btnSave.Text = "Save Changes";
            }
            else
            {
                this.Text = "  Library Management System — Add Author";
                lblHeader.Text = "Add New Author";
                btnSave.Text = "Save Author";
            }

            this.Load += AuthorEditDialog_Load;

            dtpDob.ValueChanged += (s, e) =>
            {
                if (dtpDob.CustomFormat == " ")
                {
                    dtpDob.CustomFormat = "MM/dd/yyyy";
                }
                lblDobErr.Visible = false;
            };

            dtpDob.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                {
                    dtpDob.CustomFormat = " ";
                    lblDobErr.Visible = false;
                }
            };
        }

        private void AuthorEditDialog_Load(object? sender, EventArgs e)
        {
            btnCancel.Location = new Point(300, 412);
            btnSave.Location = new Point(420, 412);
            btnCancel.BringToFront();
            btnSave.BringToFront();

            if (DesignMode) return;

            if (_existing != null)
            {
                PopulateFields(_existing);
            }
            else
            {
                cmbGender.SelectedIndex = -1;
                dtpDob.CustomFormat = " ";
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            btnCancel.Location = new Point(300, 412);
            btnSave.Location = new Point(420, 412);
            btnCancel.BringToFront();
            btnSave.BringToFront();
        }

        private void PopulateFields(Author a)
        {
            txtName.Text = a.Name;
            cmbGender.SelectedItem = a.Gender == "Female" ? "Female" : (a.Gender == "Male" ? "Male" : null);
            if (a.DateOfBirth.HasValue)
            {
                dtpDob.CustomFormat = "MM/dd/yyyy";
                dtpDob.Value = a.DateOfBirth.Value;
            }
            else
            {
                dtpDob.CustomFormat = " ";
            }
            txtBio.Text = a.Bio ?? "";
        }

        private void txtName_TextChanged(object? sender, EventArgs e) => ValidationHelper.ClearFieldError(txtName, lblNameErr);
        private void cmbGender_SelectedIndexChanged(object? sender, EventArgs e) => ValidationHelper.ClearFieldError(cmbGender, lblGenderErr);
        private void txtBio_TextChanged(object? sender, EventArgs e) => ValidationHelper.ClearFieldError(txtBio, lblBioErr);

        private void btnCancel_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSave_Click(object? sender, EventArgs e)
        {
            bool hasErrors = false;

            // Validate Name
            if (!ValidationHelper.IsRequired(txtName.Text, 150))
            {
                ValidationHelper.SetFieldError(txtName, lblNameErr, "Author name is required (max 150 characters).");
                hasErrors = true;
            }

            // Validate Gender
            if (cmbGender.SelectedItem == null || string.IsNullOrWhiteSpace(cmbGender.Text))
            {
                ValidationHelper.SetFieldError(cmbGender, lblGenderErr, "Please select a gender.");
                hasErrors = true;
            }

            // Validate Date of Birth (required & cannot be in the future)
            if (string.IsNullOrWhiteSpace(dtpDob.CustomFormat) || dtpDob.CustomFormat == " ")
            {
                ValidationHelper.SetFieldError(dtpDob, lblDobErr, "Date of birth is required.");
                hasErrors = true;
            }
            else if (dtpDob.Value.Date > DateTime.Today)
            {
                ValidationHelper.SetFieldError(dtpDob, lblDobErr, "Date of birth cannot be in the future.");
                hasErrors = true;
            }
            else
            {
                ValidationHelper.ClearFieldError(dtpDob, lblDobErr);
            }

            // Validate Bio
            if (txtBio.Text.Length > 1000)
            {
                ValidationHelper.SetFieldError(txtBio, lblBioErr, "Biography cannot exceed 1000 characters.");
                hasErrors = true;
            }

            if (hasErrors) return;

            var author = new Author
            {
                AuthorId = _existing?.AuthorId ?? 0,
                Name = txtName.Text.Trim(),
                Gender = cmbGender.SelectedItem?.ToString() ?? "",
                DateOfBirth = (dtpDob.CustomFormat != " " && !string.IsNullOrWhiteSpace(dtpDob.CustomFormat)) ? dtpDob.Value.Date : (DateTime?)null,
                Bio = txtBio.Text.Trim()
            };

            try
            {
                using var ctx = Program.CreateDbContext();
                var svc = new AuthorService(ctx);
                var (success, message) = _existing == null ? svc.Add(author) : svc.Update(author);

                if (!success)
                {
                    MessageBox.Show(message, "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AuthorEditDialog.BtnSave_Click] {ex}");
                MessageBox.Show("Could not save author record. Please try again.", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
