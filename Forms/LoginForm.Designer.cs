namespace LibraryManagementSystem.Forms
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlRight;

        // Controls នៅលើ pnlLeft (ប្រើ PictureBox ដាក់ Logo)
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblBrandTitle;

        // Controls នៅលើ pnlRight
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblGeneralError;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblUsernameError;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPasswordError;
        private System.Windows.Forms.CheckBox chkShowPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlLeft = new Panel();
            lblBrandTitle = new Label();
            picLogo = new PictureBox();
            pnlRight = new Panel();
            lblGeneralError = new Label();
            lblWelcome = new Label();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblUsernameError = new Label();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblPasswordError = new Label();
            chkShowPassword = new CheckBox();
            btnLogin = new Button();
            btnCancel = new Button();
            pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            pnlRight.SuspendLayout();
            SuspendLayout();
            // 
            // pnlLeft
            // 
            pnlLeft.BackColor = Color.SteelBlue;
            pnlLeft.Controls.Add(lblBrandTitle);
            pnlLeft.Controls.Add(picLogo);
            pnlLeft.Dock = DockStyle.Left;
            pnlLeft.Location = new Point(0, 0);
            pnlLeft.Margin = new Padding(3, 4, 3, 4);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Padding = new Padding(36, 40, 20, 20);
            pnlLeft.Size = new Size(313, 425);
            pnlLeft.TabIndex = 0;
            // 
            // lblBrandTitle
            // 
            lblBrandTitle.Font = new Font("Times New Roman", 25.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBrandTitle.ForeColor = Color.White;
            lblBrandTitle.Location = new Point(23, -6);
            lblBrandTitle.Name = "lblBrandTitle";
            lblBrandTitle.Size = new Size(284, 232);
            lblBrandTitle.TabIndex = 1;
            lblBrandTitle.Text = "Library\r\nManagement\r\nSystem";
            lblBrandTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // picLogo
            // 
            picLogo.BackColor = Color.Transparent;
            picLogo.Location = new Point(0, 184);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(313, 241);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabIndex = 0;
            picLogo.TabStop = false;
            // 
            // pnlRight
            // 
            pnlRight.BackColor = Color.White;
            pnlRight.Controls.Add(lblGeneralError);
            pnlRight.Controls.Add(lblWelcome);
            pnlRight.Controls.Add(lblUsername);
            pnlRight.Controls.Add(txtUsername);
            pnlRight.Controls.Add(lblUsernameError);
            pnlRight.Controls.Add(lblPassword);
            pnlRight.Controls.Add(txtPassword);
            pnlRight.Controls.Add(lblPasswordError);
            pnlRight.Controls.Add(chkShowPassword);
            pnlRight.Controls.Add(btnLogin);
            pnlRight.Controls.Add(btnCancel);
            pnlRight.Dock = DockStyle.Fill;
            pnlRight.Location = new Point(313, 0);
            pnlRight.Margin = new Padding(3, 4, 3, 4);
            pnlRight.Name = "pnlRight";
            pnlRight.Padding = new Padding(41, 37, 41, 37);
            pnlRight.Size = new Size(446, 425);
            pnlRight.TabIndex = 1;
            // 
            // lblGeneralError
            // 
            lblGeneralError.Font = new Font("Segoe UI", 9F);
            lblGeneralError.ForeColor = Color.FromArgb(220, 38, 38);
            lblGeneralError.Location = new Point(44, 276);
            lblGeneralError.Name = "lblGeneralError";
            lblGeneralError.Size = new Size(361, 23);
            lblGeneralError.TabIndex = 2;
            lblGeneralError.Text = "Invalid username or password.";
            lblGeneralError.TextAlign = ContentAlignment.BottomLeft;
            lblGeneralError.Visible = false;
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblWelcome.ForeColor = Color.FromArgb(13, 59, 102);
            lblWelcome.Location = new Point(111, 23);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(226, 41);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "Welcome Back!";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblUsername.ForeColor = Color.FromArgb(13, 59, 102);
            lblUsername.Location = new Point(44, 87);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(83, 21);
            lblUsername.TabIndex = 3;
            lblUsername.Text = "Username";
            // 
            // txtUsername
            // 
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Font = new Font("Segoe UI", 10F);
            txtUsername.ForeColor = Color.FromArgb(13, 59, 102);
            txtUsername.Location = new Point(44, 114);
            txtUsername.Margin = new Padding(3, 4, 3, 4);
            txtUsername.MaxLength = 50;
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "  Enter your username";
            txtUsername.Size = new Size(361, 30);
            txtUsername.TabIndex = 4;
            txtUsername.TextChanged += txtUsername_TextChanged;
            txtUsername.KeyDown += txtUsername_KeyDown;
            // 
            // lblUsernameError
            // 
            lblUsernameError.AutoSize = true;
            lblUsernameError.Font = new Font("Segoe UI", 8F);
            lblUsernameError.ForeColor = Color.FromArgb(220, 38, 38);
            lblUsernameError.Location = new Point(44, 148);
            lblUsernameError.Name = "lblUsernameError";
            lblUsernameError.Size = new Size(142, 19);
            lblUsernameError.TabIndex = 5;
            lblUsernameError.Text = "Username is required.";
            lblUsernameError.Visible = false;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblPassword.ForeColor = Color.FromArgb(13, 59, 102);
            lblPassword.Location = new Point(44, 171);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(79, 21);
            lblPassword.TabIndex = 6;
            lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI", 10F);
            txtPassword.ForeColor = Color.FromArgb(13, 59, 102);
            txtPassword.Location = new Point(44, 196);
            txtPassword.Margin = new Padding(3, 4, 3, 4);
            txtPassword.MaxLength = 100;
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderText = "  Enter your password";
            txtPassword.Size = new Size(361, 30);
            txtPassword.TabIndex = 7;
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.TextChanged += txtPassword_TextChanged;
            txtPassword.KeyDown += txtPassword_KeyDown;
            // 
            // lblPasswordError
            // 
            lblPasswordError.AutoSize = true;
            lblPasswordError.Font = new Font("Segoe UI", 8F);
            lblPasswordError.ForeColor = Color.FromArgb(220, 38, 38);
            lblPasswordError.Location = new Point(46, 230);
            lblPasswordError.Name = "lblPasswordError";
            lblPasswordError.Size = new Size(138, 19);
            lblPasswordError.TabIndex = 8;
            lblPasswordError.Text = "Password is required.";
            lblPasswordError.Visible = false;
            // 
            // chkShowPassword
            // 
            chkShowPassword.AutoSize = true;
            chkShowPassword.Cursor = Cursors.Hand;
            chkShowPassword.Font = new Font("Segoe UI", 9F);
            chkShowPassword.ForeColor = Color.FromArgb(100, 116, 139);
            chkShowPassword.Location = new Point(44, 253);
            chkShowPassword.Margin = new Padding(3, 4, 3, 4);
            chkShowPassword.Name = "chkShowPassword";
            chkShowPassword.Size = new Size(134, 24);
            chkShowPassword.TabIndex = 9;
            chkShowPassword.Text = "Show password";
            chkShowPassword.UseVisualStyleBackColor = true;
            chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.SteelBlue;
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatAppearance.MouseOverBackColor = Color.FromArgb(10, 56, 128);
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(44, 303);
            btnLogin.Margin = new Padding(3, 4, 3, 4);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(361, 50);
            btnLogin.TabIndex = 10;
            btnLogin.Text = "Sign In";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(241, 245, 249);
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(208, 225, 253);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold);
            btnCancel.ForeColor = Color.FromArgb(100, 116, 139);
            btnCancel.Location = new Point(44, 361);
            btnCancel.Margin = new Padding(3, 4, 3, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(361, 43);
            btnCancel.TabIndex = 11;
            btnCancel.Text = "Exit ";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(759, 425);
            Controls.Add(pnlRight);
            Controls.Add(pnlLeft);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Library Management System — Sign In";
            pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            pnlRight.ResumeLayout(false);
            pnlRight.PerformLayout();
            ResumeLayout(false);
        }
    }
}