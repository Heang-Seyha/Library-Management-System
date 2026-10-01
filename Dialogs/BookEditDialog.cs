using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Dialogs
{
    /// <summary>
    /// Modal dialog for adding or editing a Book with professional inline validation.
    /// 100% compatible with the Visual Studio WinForms Designer.
    /// </summary>
    public partial class BookEditDialog : Form
    {
        private readonly Book? _existing;

        /// <summary>Parameterless constructor for WinForms Designer.</summary>
        public BookEditDialog() : this(null)
        {
        }

        public BookEditDialog(Book? existing)
        {
            _existing = existing;
            InitializeComponent();
            UIHelper.ApplyPaddingToAllTextBoxes(this, 8);

            if (existing != null)
            {
                this.Text = "  Library Management System — Edit Book";
                lblHeader.Text = "Edit Book Information";
                btnSave.Text = "Save Changes";
            }
            else
            {
                this.Text = "  Library Management System — Add Book";
                lblHeader.Text = "Add New Book";
                btnSave.Text = "Save Book";
            }

            this.Load += BookEditDialog_Load;
        }

        private void BookEditDialog_Load(object? sender, EventArgs e)
        {
            btnCancel.Location = new Point(320, 526);
            btnSave.Location = new Point(440, 526);
            btnCancel.BringToFront();
            btnSave.BringToFront();

            if (DesignMode) return;

            LoadDropdowns();

            if (_existing != null)
            {
                PopulateFields(_existing);
            }
            else
            {
                cmbCategory.SelectedIndex = -1;
                cmbAuthor.SelectedIndex = -1;
                cmbPublisher.SelectedIndex = -1;
            }
        }

        private void LoadDropdowns()
        {
            try
            {
                using var ctx = Program.CreateDbContext();
                cmbCategory.DataSource = new CategoryService(ctx).GetAll();
                cmbCategory.DisplayMember = "Name";
                cmbCategory.ValueMember = "CategoryId";

                cmbAuthor.DataSource = new AuthorService(ctx).GetAll();
                cmbAuthor.DisplayMember = "Name";
                cmbAuthor.ValueMember = "AuthorId";

                cmbPublisher.DataSource = new PublisherService(ctx).GetAll();
                cmbPublisher.DisplayMember = "Name";
                cmbPublisher.ValueMember = "PublisherId";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[BookEditDialog.LoadDropdowns] {ex}");
                MessageBox.Show("Failed to load master dropdown data. Please check database connectivity and try again.", "Data Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateFields(Book b)
        {
            txtTitle.Text = b.Title;
            txtISBN.Text = b.ISBN;
            txtYear.Text = b.Year.ToString();
            txtTotal.Text = b.TotalCopies.ToString();
            txtAvailable.Text = b.AvailableCopies.ToString();

            if (cmbCategory.Items.Count > 0) cmbCategory.SelectedValue = b.CategoryId;
            if (cmbAuthor.Items.Count > 0) cmbAuthor.SelectedValue = b.AuthorId;
            if (cmbPublisher.Items.Count > 0) cmbPublisher.SelectedValue = b.PublisherId;
        }

        private void txtTitle_TextChanged(object? sender, EventArgs e) => ValidationHelper.ClearFieldError(txtTitle, lblTitleErr);
        private void txtISBN_TextChanged(object? sender, EventArgs e) => ValidationHelper.ClearFieldError(txtISBN, lblISBNErr);
        private void txtYear_TextChanged(object? sender, EventArgs e) => ValidationHelper.ClearFieldError(txtYear, lblYearErr);
        private void cmbCategory_SelectedIndexChanged(object? sender, EventArgs e) => ValidationHelper.ClearFieldError(cmbCategory, lblCategoryErr);
        private void cmbAuthor_SelectedIndexChanged(object? sender, EventArgs e) => ValidationHelper.ClearFieldError(cmbAuthor, lblAuthorErr);
        private void cmbPublisher_SelectedIndexChanged(object? sender, EventArgs e) => ValidationHelper.ClearFieldError(cmbPublisher, lblPublisherErr);
        private void txtTotal_TextChanged(object? sender, EventArgs e) => ValidationHelper.ClearFieldError(txtTotal, lblTotalErr);
        private void txtAvailable_TextChanged(object? sender, EventArgs e) => ValidationHelper.ClearFieldError(txtAvailable, lblAvailableErr);

        private void btnCancel_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSave_Click(object? sender, EventArgs e)
        {
            bool hasErrors = false;

            // Validate Title
            if (!ValidationHelper.IsRequired(txtTitle.Text, 300))
            {
                ValidationHelper.SetFieldError(txtTitle, lblTitleErr, "Title is required (max 300 characters).");
                hasErrors = true;
            }

            // Validate ISBN
            if (!ValidationHelper.IsValidISBN(txtISBN.Text))
            {
                ValidationHelper.SetFieldError(txtISBN, lblISBNErr, "Enter a valid ISBN-10 or ISBN-13.");
                hasErrors = true;
            }

            // Validate Year
            if (!int.TryParse(txtYear.Text.Trim(), out int year) || !ValidationHelper.IsValidYear(year))
            {
                ValidationHelper.SetFieldError(txtYear, lblYearErr, $"Year must be between 1000 and {DateTime.Now.Year}.");
                hasErrors = true;
            }

            // Validate Category
            if (cmbCategory.SelectedValue == null)
            {
                ValidationHelper.SetFieldError(cmbCategory, lblCategoryErr, "Please select a category.");
                hasErrors = true;
            }

            // Validate Author
            if (cmbAuthor.SelectedValue == null)
            {
                ValidationHelper.SetFieldError(cmbAuthor, lblAuthorErr, "Please select an author.");
                hasErrors = true;
            }

            // Validate Publisher
            if (cmbPublisher.SelectedValue == null)
            {
                ValidationHelper.SetFieldError(cmbPublisher, lblPublisherErr, "Please select a publisher.");
                hasErrors = true;
            }

            // Validate Total Copies
            bool totalOk = ValidationHelper.IsPositiveInt(txtTotal.Text, out int total);
            if (!totalOk)
            {
                ValidationHelper.SetFieldError(txtTotal, lblTotalErr, "Total copies must be positive (≥ 1).");
                hasErrors = true;
            }

            // Validate Available Copies
            bool availOk = ValidationHelper.IsNonNegativeInt(txtAvailable.Text, out int available);
            if (!availOk)
            {
                ValidationHelper.SetFieldError(txtAvailable, lblAvailableErr, "Available copies cannot be negative.");
                hasErrors = true;
            }
            else if (totalOk && available > total)
            {
                ValidationHelper.SetFieldError(txtAvailable, lblAvailableErr, "Available cannot exceed total copies.");
                hasErrors = true;
            }

            if (hasErrors) return;

            var book = new Book
            {
                BookId = _existing?.BookId ?? 0,
                Title = txtTitle.Text.Trim(),
                ISBN = txtISBN.Text.Trim(),
                Year = year,
                TotalCopies = total,
                AvailableCopies = available,
                CategoryId = (int)cmbCategory.SelectedValue!,
                AuthorId = (int)cmbAuthor.SelectedValue!,
                PublisherId = (int)cmbPublisher.SelectedValue!,
                RowVersion = _existing?.RowVersion ?? Array.Empty<byte>()
            };

            try
            {
                using var ctx = Program.CreateDbContext();
                var svc = new BookService(ctx);
                var (success, message) = _existing == null ? svc.Add(book) : svc.Update(book);

                if (!success)
                {
                    if (message.Contains("ISBN", StringComparison.OrdinalIgnoreCase))
                    {
                        ValidationHelper.SetFieldError(txtISBN, lblISBNErr, message);
                    }
                    else
                    {
                        MessageBox.Show(message, "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    return;
                }

                MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[BookEditDialog.BtnSave_Click] {ex}");
                MessageBox.Show("Could not save book record. Please try again.", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
