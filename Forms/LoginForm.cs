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

            UIHelper.ApplyPaddingToAllTextBoxes(this, 8);
        }

        private void LoadAppLogo()
        {
            try
            {
                string[] candidates = new[]
                {
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "libraryLogo.png"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "icons", "libraryLogo.png"),
                    Path.Combine(Directory.GetCurrentDirectory(), "libraryLogo.png"),
                    Path.Combine(Directory.GetCurrentDirectory(), "icons", "libraryLogo.png"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "libraryLogo.png"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "icons", "libraryLogo.png")
                };

                foreach (var path in candidates)
                {
                    if (File.Exists(path))
                    {
                        using var stream = new MemoryStream(File.ReadAllBytes(path));
                        picLogo.Image = Image.FromStream(stream);
                        break;
                    }
                }
            }
            catch
            {
            }
        }

        private void btnLogin_Click(object? sender, EventArgs e)
        {
            PerformLogin();
        }

        private void btnCancel_Click(object? sender, EventArgs e)
        {
            Application.Exit();
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

                this.Hide();
                var mainShell = new MainShellForm();
                mainShell.FormClosed += (s, args) => this.Close();
                mainShell.Show();
            }
            catch (Exception ex)
            {
                SetGeneralError($"An unexpected error occurred: {ex.Message}");
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