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

            if (existing != null)
            {
                this.Text = "Library Management System — Edit Librarian";
                lblHeader.Text = "Edit Librarian Information";
                lblPasswordLabel.Text = "Password (leave blank to keep current)";
                btnSave.Text = "Save Changes";
            }
            else
            {
                this.Text = "Library Management System — Add Librarian";
                lblHeader.Text = "Add New Librarian";
                lblPasswordLabel.Text = "Account Password *";
                btnSave.Text = "Save Librarian";
                cmbRole.SelectedIndex = 0; // Default to Librarian
            }

            this.Load += LibrarianEditDialog_Load;
        }

        private void LibrarianEditDialog_Load(object? sender, EventArgs e)
        {
            if (DesignMode) return;

            if (_existing != null)
            {
                PopulateFields(_existing);
            }
        }

        private void PopulateFields(Librarian lib)
        {
            txtName.Text = lib.Name;
            txtUsername.Text = lib.Username;
            txtPhone.Text = lib.Phone;
            txtPosition.Text = string.IsNullOrWhiteSpace(lib.Position) ? "Librarian" : lib.Position;
            cmbRole.SelectedItem = (lib.Role == "Admin") ? "Admin" : "Librarian";
        }

        private void txtName_TextChanged(object? sender, EventArgs e) => ValidationHelper.ClearFieldError(txtName, lblNameErr);
        private void txtUsername_TextChanged(object? sender, EventArgs e) => ValidationHelper.ClearFieldError(txtUsername, lblUsernameErr);
        private void txtPhone_TextChanged(object? sender, EventArgs e) => ValidationHelper.ClearFieldError(txtPhone, lblPhoneErr);
        private void txtPosition_TextChanged(object? sender, EventArgs e) => ValidationHelper.ClearFieldError(txtPosition, lblPositionErr);
        private void cmbRole_SelectedIndexChanged(object? sender, EventArgs e) => ValidationHelper.ClearFieldError(cmbRole, lblRoleErr);
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

            // Validate Username
            if (!ValidationHelper.IsValidUsername(txtUsername.Text, 3, 50))
            {
                ValidationHelper.SetFieldError(txtUsername, lblUsernameErr, "Username must be 3-50 alphanumeric characters or underscores.");
                hasErrors = true;
            }

            // Validate Role
            if (cmbRole.SelectedItem == null)
            {
                ValidationHelper.SetFieldError(cmbRole, lblRoleErr, "Please select an account role.");
                hasErrors = true;
            }

            // Validate Phone (optional, but if present must be valid)
            if (!string.IsNullOrWhiteSpace(txtPhone.Text) && !ValidationHelper.IsValidPhone(txtPhone.Text))
            {
                ValidationHelper.SetFieldError(txtPhone, lblPhoneErr, "Invalid phone number (must start with 0, 9-10 digits).");
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

            string selectedRole = cmbRole.SelectedItem?.ToString() == "Admin" ? "Admin" : "Librarian";

            var librarian = new Librarian
            {
                LibrarianId = _existing?.LibrarianId ?? 0,
                Name = txtName.Text.Trim(),
                Username = txtUsername.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Position = string.IsNullOrWhiteSpace(txtPosition.Text) ? "Librarian" : txtPosition.Text.Trim(),
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
                MessageBox.Show($"Could not save librarian account.\n\nDetails: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
