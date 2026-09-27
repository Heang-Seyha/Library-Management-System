using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Dialogs
{
    /// <summary>
    /// Modal dialog for adding or editing a Library Member with professional inline validation.
    /// 100% compatible with the Visual Studio WinForms Designer.
    /// </summary>
    public partial class MemberEditDialog : Form
    {
        private readonly Member? _existing;

        /// <summary>Parameterless constructor for WinForms Designer.</summary>
        public MemberEditDialog() : this(null)
        {
        }

        public MemberEditDialog(Member? existing)
        {
            _existing = existing;
            InitializeComponent();

            if (existing != null)
            {
                this.Text = "Library Management System — Edit Member";
                lblHeader.Text = "Edit Member Information";
                btnSave.Text = "Save Changes";
            }
            else
            {
                this.Text = "Library Management System — Add Member";
                lblHeader.Text = "Add New Member";
                btnSave.Text = "Save Member";
            }

            this.Load += MemberEditDialog_Load;
        }

        private void MemberEditDialog_Load(object? sender, EventArgs e)
        {
            if (DesignMode) return;

            if (_existing != null)
            {
                PopulateFields(_existing);
            }
        }

        private void PopulateFields(Member m)
        {
            txtName.Text = m.Name;
            txtPhone.Text = m.Phone;
            txtEmail.Text = m.Email;
            txtAddress.Text = m.Address;
            if (m.JoinDate <= DateTime.Today)
            {
                dtpJoin.Value = m.JoinDate;
            }
        }

        private void txtName_TextChanged(object? sender, EventArgs e) => ValidationHelper.ClearFieldError(txtName, lblNameErr);
        private void txtPhone_TextChanged(object? sender, EventArgs e) => ValidationHelper.ClearFieldError(txtPhone, lblPhoneErr);
        private void txtEmail_TextChanged(object? sender, EventArgs e) => ValidationHelper.ClearFieldError(txtEmail, lblEmailErr);
        private void txtAddress_TextChanged(object? sender, EventArgs e) => ValidationHelper.ClearFieldError(txtAddress, lblAddressErr);

        private void btnCancel_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSave_Click(object? sender, EventArgs e)
        {
            bool hasErrors = false;

            // Validate Full Name
            if (!ValidationHelper.IsRequired(txtName.Text, 150))
            {
                ValidationHelper.SetFieldError(txtName, lblNameErr, "Full name is required (max 150 characters).");
                hasErrors = true;
            }

            // Validate Phone
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                ValidationHelper.SetFieldError(txtPhone, lblPhoneErr, "Phone number is required.");
                hasErrors = true;
            }
            else if (!ValidationHelper.IsValidPhone(txtPhone.Text))
            {
                ValidationHelper.SetFieldError(txtPhone, lblPhoneErr, "Invalid phone number (must start with 0, 9-10 digits).");
                hasErrors = true;
            }

            // Validate Email
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                ValidationHelper.SetFieldError(txtEmail, lblEmailErr, "Email address is required.");
                hasErrors = true;
            }
            else if (!ValidationHelper.IsValidEmail(txtEmail.Text))
            {
                ValidationHelper.SetFieldError(txtEmail, lblEmailErr, "Please enter a valid email address (e.g. name@domain.com).");
                hasErrors = true;
            }

            // Validate Address
            if (!ValidationHelper.IsRequired(txtAddress.Text, 500))
            {
                ValidationHelper.SetFieldError(txtAddress, lblAddressErr, "Address is required (max 500 characters).");
                hasErrors = true;
            }

            // Validate Join Date (cannot be in the future)
            if (dtpJoin.Value.Date > DateTime.Today)
            {
                lblJoinErr.Text = "Join date cannot be in the future.";
                lblJoinErr.Visible = true;
                hasErrors = true;
            }

            if (hasErrors) return;

            var member = new Member
            {
                MemberId = _existing?.MemberId ?? 0,
                Name = txtName.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                JoinDate = dtpJoin.Value.Date
            };

            try
            {
                using var ctx = Program.CreateDbContext();
                var svc = new MemberService(ctx);
                var (success, message) = _existing == null ? svc.Add(member) : svc.Update(member);

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
                MessageBox.Show($"Could not save member record.\n\nDetails: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
