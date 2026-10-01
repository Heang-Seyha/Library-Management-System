using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Dialogs
{
    /// <summary>
    /// Modal dialog for adding or editing a Librarian account with professional inline validation.
    /// 100% compatible with the Visual Studio WinForms Designer.
    /// </summary>
    public partial class LibrarianEditDialog : Form
    {
        private readonly Librarian? _existing;

        /// <summary>Parameterless constructor for WinForms Designer.</summary>
        public LibrarianEditDialog() : this(null)
        {
        }

        public LibrarianEditDialog(Librarian? existing)
        {
            _existing = existing;
            InitializeComponent();
            UIHelper.ApplyPaddingToAllTextBoxes(this, 8);

            if (existing != null)
            {
                this.Text = "  Library Management System — Edit Librarian";
                lblHeader.Text = "Edit Librarian Information";
                lblPasswordLabel.Text = "Password (leave blank to keep current)";
                btnSave.Text = "Save Changes";
            }
            else
            {
                this.Text = "  Library Management System — Add Librarian";
                lblHeader.Text = "Add New Librarian";
                lblPasswordLabel.Text = "Account Password *";
                btnSave.Text = "Save Librarian";
            }

            this.Load += LibrarianEditDialog_Load;

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

        private void LibrarianEditDialog_Load(object? sender, EventArgs e)
        {
            btnCancel.Location = new Point(300, 428);
            btnSave.Location = new Point(420, 428);
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

        private void PopulateFields(Librarian lib)
        {
            txtName.Text = lib.Name;
            cmbGender.SelectedItem = lib.Gender == "Female" ? "Female" : (lib.Gender == "Male" ? "Male" : null);
            if (lib.DateOfBirth.HasValue)
            {
                dtpDob.CustomFormat = "MM/dd/yyyy";
                dtpDob.Value = lib.DateOfBirth.Value;
            }
            else
            {
                dtpDob.CustomFormat = " ";
            }
            txtUsername.Text = lib.Username;
            txtPhone.Text = lib.Phone;
            txtEmail.Text = lib.Email;
        }

        private void txtName_TextChanged(object? sender, EventArgs e) => ValidationHelper.ClearFieldError(txtName, lblNameErr);
        private void cmbGender_SelectedIndexChanged(object? sender, EventArgs e) => ValidationHelper.ClearFieldError(cmbGender, lblGenderErr);
        private void txtUsername_TextChanged(object? sender, EventArgs e) => ValidationHelper.ClearFieldError(txtUsername, lblUsernameErr);
        private void txtPhone_TextChanged(object? sender, EventArgs e) => ValidationHelper.ClearFieldError(txtPhone, lblPhoneErr);
        private void txtEmail_TextChanged(object? sender, EventArgs e) => ValidationHelper.ClearFieldError(txtEmail, lblEmailErr);
        private void txtPassword_TextChanged(object? sender, EventArgs e) => ValidationHelper.ClearFieldError(txtPassword, lblPasswordErr);

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
                ValidationHelper.SetFieldError(txtName, lblNameErr, "Full name is required (max 150 characters).");
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

            // Validate Username
            if (!ValidationHelper.IsValidUsername(txtUsername.Text, 3, 50))
            {
                ValidationHelper.SetFieldError(txtUsername, lblUsernameErr, "Username must be 3-50 alphanumeric characters or _.");
                hasErrors = true;
            }

            // Validate Phone (required & valid format)
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                ValidationHelper.SetFieldError(txtPhone, lblPhoneErr, "Phone number is required.");
                hasErrors = true;
            }
            else if (!ValidationHelper.IsValidPhone(txtPhone.Text))
            {
                ValidationHelper.SetFieldError(txtPhone, lblPhoneErr, "Invalid phone (must start with 0, 9-10 digits).");
                hasErrors = true;
            }

            // Validate Email (required & valid format)
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                ValidationHelper.SetFieldError(txtEmail, lblEmailErr, "Email address is required.");
                hasErrors = true;
            }
            else if (!ValidationHelper.IsValidEmail(txtEmail.Text))
            {
                ValidationHelper.SetFieldError(txtEmail, lblEmailErr, "Please enter a valid email address.");
                hasErrors = true;
            }

            // Validate Password
            if (_existing == null)
            {
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    ValidationHelper.SetFieldError(txtPassword, lblPasswordErr, "Password is required for new accounts.");
                    hasErrors = true;
                }
                else if (!ValidationHelper.IsValidPassword(txtPassword.Text))
                {
                    ValidationHelper.SetFieldError(txtPassword, lblPasswordErr, "Password must be at least 8 characters with 1 letter & 1 digit.");
                    hasErrors = true;
                }
            }
            else if (!string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                if (!ValidationHelper.IsValidPassword(txtPassword.Text))
                {
                    ValidationHelper.SetFieldError(txtPassword, lblPasswordErr, "New password must be at least 8 characters with 1 letter & 1 digit.");
                    hasErrors = true;
                }
            }

            if (hasErrors) return;

            string selectedRole = _existing?.Role ?? AuthorizationHelper.RoleLibrarian;

            var librarian = new Librarian
            {
                LibrarianId = _existing?.LibrarianId ?? 0,
                Name = txtName.Text.Trim(),
                Gender = cmbGender.SelectedItem?.ToString() ?? "",
                DateOfBirth = (dtpDob.CustomFormat != " " && !string.IsNullOrWhiteSpace(dtpDob.CustomFormat)) ? dtpDob.Value.Date : (DateTime?)null,
                Username = txtUsername.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Role = selectedRole
            };

            try
            {
                using var ctx = Program.CreateDbContext();
                var svc = new LibrarianService(ctx);
                (bool success, string message) result = _existing == null
                    ? svc.Add(librarian, txtPassword.Text)
                    : svc.Update(librarian, string.IsNullOrWhiteSpace(txtPassword.Text) ? null : txtPassword.Text);

                if (!result.success)
                {
                    if (result.message.Contains("Username", StringComparison.OrdinalIgnoreCase))
                    {
                        ValidationHelper.SetFieldError(txtUsername, lblUsernameErr, result.message);
                    }
                    else
                    {
                        MessageBox.Show(result.message, "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    return;
                }

                MessageBox.Show(result.message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[LibrarianEditDialog.BtnSave_Click] {ex}");
                MessageBox.Show("Could not save librarian account. Please try again.", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
