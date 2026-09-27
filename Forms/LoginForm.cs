using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Services;
using System.Runtime.InteropServices;

namespace LibraryManagementSystem.Forms
{
    public partial class LoginForm : Form
    {
        // កូដ Win32 API សម្រាប់រុញគម្លាតខាងឆ្វេងនៃ TextBox (Inner Left Padding)
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
        private const int EM_SETMARGINS = 0xd3;
        private const int EC_LEFTMARGIN = 0x1;

        public LoginForm()
        {
            InitializeComponent();
            LoadAppLogo();

            // កំណត់គម្លាតឆ្វេង 8px លើ TextBox ទាំងពីរឱ្យអក្សរមិនកៀកគែមឆ្វេង
            SetTextBoxLeftPadding(txtUsername, 8);
            SetTextBoxLeftPadding(txtPassword, 8);
        }

        private static void SetTextBoxLeftPadding(TextBox textBox, int leftPaddingPixels)
        {
            if (textBox.IsHandleCreated)
            {
                SendMessage(textBox.Handle, EM_SETMARGINS, (IntPtr)EC_LEFTMARGIN, (IntPtr)leftPaddingPixels);
            }
            else
            {
                textBox.HandleCreated += (s, e) =>
                {
                    SendMessage(textBox.Handle, EM_SETMARGINS, (IntPtr)EC_LEFTMARGIN, (IntPtr)leftPaddingPixels);
                };
            }
        }

        private void LoadAppLogo()
        {
            try
            {
                string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "libraryLogo.png");
                if (!File.Exists(imagePath))
                {
                    // ស្វែងរកក្នុង Project Directory ប្រសិនបើរូបភាពនៅទីតាំង Root
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), "libraryLogo.png");
                }

                if (File.Exists(imagePath))
                {
                    picLogo.Image = Image.FromFile(imagePath);
                }
            }
            catch
            {
                // បើមានបញ្ហាផ្លូវ path វានឹងមិនបង្កឱ្យ Error Crash នោះទេ
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            PerformLogin();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                PerformLogin();
            }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                txtPassword.Focus();
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            ClearUsernameError();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            ClearPasswordError();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
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
                    SetGeneralError("Invalid username or password. Please try again.");
                    txtPassword.Clear();
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
        }

        private void SetPasswordError(string message)
        {
            lblPasswordError.Text = message;
            lblPasswordError.Visible = true;
        }

        private void SetGeneralError(string message)
        {
            lblGeneralError.Text = message;
            lblGeneralError.Visible = true;
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