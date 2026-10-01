using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            LoadAppLogo();
            this.Icon = UIHelper.AppIcon;

            UIHelper.ApplyPaddingToAllTextBoxes(this, 8);
        }

        private void LoadAppLogo()
        {
            if (picLogo.Image != null) return;

            try
            {
                picLogo.Image = Properties.Resources.libraryLogo;
            }
            catch
            {
            }
        }

        private void btnLogin_Click(object? sender, EventArgs e)
        {
            PerformLogin();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            txtUsername.Focus();
        }

        private void btnCancel_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtPassword_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                PerformLogin();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter && txtUsername.Focused)
            {
                txtPassword.Focus();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtUsername_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                txtPassword.Focus();
            }
        }

        private void txtUsername_TextChanged(object? sender, EventArgs e)
        {
            ClearUsernameError();
        }

        private void txtPassword_TextChanged(object? sender, EventArgs e)
        {
            ClearPasswordError();
        }

        private void chkShowPassword_CheckedChanged(object? sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private void PerformLogin()
        {
            ClearAllErrors();

            var username = txtUsername.Text.Trim();
            var password = txtPassword.Text;

            bool hasError = false;

            if (string.IsNullOrEmpty(username))
            {
                SetUsernameError("Please enter your username.");
                hasError = true;
            }

            if (string.IsNullOrEmpty(password))
            {
                SetPasswordError("Please enter your password.");
                hasError = true;
            }

            if (hasError)
            {
                if (string.IsNullOrEmpty(username))
                    txtUsername.Focus();
                else
                    txtPassword.Focus();
                return;
            }

            try
            {
                btnLogin.Enabled = false;
                btnLogin.Text = "Signing In...";

                using var context = Program.CreateDbContext();
                var authService = new AuthenticationService(context);
                var librarian = authService.Login(username, password);

                if (librarian == null)
                {
                    // ១. បង្ហាញសារ Error ពណ៌ក្រហម
                    SetGeneralError("​​Invalid username or password. Please try again.");

                    // ២. ដោះ Event TextChanged ចេញជាបណ្តោះអាសន្ន ដើម្បីកុំឱ្យវាបិទសារ Error វិញ
                    txtPassword.TextChanged -= txtPassword_TextChanged;

                    // ៣. សម្អាត Password Textbox
                    txtPassword.Clear();

                    // ៤. ភ្ជាប់ Event TextChanged ត្រឡប់មកវិញ (ពេល user វាយតួអក្សរថ្មី ទើប Error បាត់)
                    txtPassword.TextChanged += txtPassword_TextChanged;

                    txtPassword.Focus();
                    return;
                }

                SessionManager.Login(librarian);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[LoginForm.btnLogin_Click] {ex}");
                SetGeneralError("An unexpected error occurred during sign in. Please try again.");
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "Sign In";
            }
        }

        private void SetUsernameError(string message)
        {
            lblUsernameError.Text = message;
            lblUsernameError.Visible = true;
            lblUsernameError.BringToFront();
        }

        private void SetPasswordError(string message)
        {
            lblPasswordError.Text = message;
            lblPasswordError.Visible = true;
            lblPasswordError.BringToFront();
        }

        private void SetGeneralError(string message)
        {
            lblGeneralError.Text = message;
            lblGeneralError.Visible = true;
            lblGeneralError.BringToFront();
        }

        private void ClearUsernameError()
        {
            lblUsernameError.Visible = false;
            if (lblGeneralError.Visible)
                lblGeneralError.Visible = false;
        }

        private void ClearPasswordError()
        {
            lblPasswordError.Visible = false;
            if (lblGeneralError.Visible)
                lblGeneralError.Visible = false;
        }

        private void ClearAllErrors()
        {
            lblUsernameError.Visible = false;
            lblPasswordError.Visible = false;
            lblGeneralError.Visible = false;
        }
    }
}