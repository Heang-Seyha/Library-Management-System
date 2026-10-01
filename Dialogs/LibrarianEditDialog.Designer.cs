namespace LibraryManagementSystem.Dialogs
{
    partial class LibrarianEditDialog
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Label lblHeader;

        private System.Windows.Forms.Label lblNameLabel;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblNameErr;

        private System.Windows.Forms.Label lblGenderLabel;
        private System.Windows.Forms.ComboBox cmbGender;
        private System.Windows.Forms.Label lblGenderErr;

        private System.Windows.Forms.Label lblUsernameLabel;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblUsernameErr;

        private System.Windows.Forms.Label lblDobLabel;
        private System.Windows.Forms.DateTimePicker dtpDob;
        private System.Windows.Forms.Label lblDobErr;

        private System.Windows.Forms.Label lblPhoneLabel;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblPhoneErr;

        private System.Windows.Forms.Label lblEmailLabel;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmailErr;

        private System.Windows.Forms.Label lblPasswordLabel;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPasswordErr;

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlContainer = new Panel();
            btnCancel = new Button();
            btnSave = new Button();
            lblPasswordErr = new Label();
            txtPassword = new TextBox();
            lblPasswordLabel = new Label();
            lblEmailErr = new Label();
            txtEmail = new TextBox();
            lblEmailLabel = new Label();
            lblPhoneErr = new Label();
            txtPhone = new TextBox();
            lblPhoneLabel = new Label();
            lblDobErr = new Label();
            dtpDob = new DateTimePicker();
            lblDobLabel = new Label();
            lblUsernameErr = new Label();
            txtUsername = new TextBox();
            lblUsernameLabel = new Label();
            lblGenderErr = new Label();
            cmbGender = new ComboBox();
            lblGenderLabel = new Label();
            lblNameErr = new Label();
            txtName = new TextBox();
            lblNameLabel = new Label();
            lblHeader = new Label();
            pnlContainer.SuspendLayout();
            SuspendLayout();
            // 
            // pnlContainer
            // 
            pnlContainer.BackColor = Color.FromArgb(235, 243, 250);
            pnlContainer.Controls.Add(btnCancel);
            pnlContainer.Controls.Add(btnSave);
            pnlContainer.Controls.Add(lblPasswordErr);
            pnlContainer.Controls.Add(txtPassword);
            pnlContainer.Controls.Add(lblPasswordLabel);
            pnlContainer.Controls.Add(lblEmailErr);
            pnlContainer.Controls.Add(txtEmail);
            pnlContainer.Controls.Add(lblEmailLabel);
            pnlContainer.Controls.Add(lblPhoneErr);
            pnlContainer.Controls.Add(txtPhone);
            pnlContainer.Controls.Add(lblPhoneLabel);
            pnlContainer.Controls.Add(lblDobErr);
            pnlContainer.Controls.Add(dtpDob);
            pnlContainer.Controls.Add(lblDobLabel);
            pnlContainer.Controls.Add(lblUsernameErr);
            pnlContainer.Controls.Add(txtUsername);
            pnlContainer.Controls.Add(lblUsernameLabel);
            pnlContainer.Controls.Add(lblGenderErr);
            pnlContainer.Controls.Add(cmbGender);
            pnlContainer.Controls.Add(lblGenderLabel);
            pnlContainer.Controls.Add(lblNameErr);
            pnlContainer.Controls.Add(txtName);
            pnlContainer.Controls.Add(lblNameLabel);
            pnlContainer.Controls.Add(lblHeader);
            pnlContainer.Dock = DockStyle.Fill;
            lblNameErr.BringToFront();
            lblGenderErr.BringToFront();
            lblUsernameErr.BringToFront();
            lblDobErr.BringToFront();
            lblPhoneErr.BringToFront();
            lblEmailErr.BringToFront();
            lblPasswordErr.BringToFront();

            pnlContainer.Location = new Point(0, 0);
            pnlContainer.Name = "pnlContainer";
            pnlContainer.Padding = new Padding(20, 16, 20, 16);
            pnlContainer.Size = new Size(560, 480);
            pnlContainer.TabIndex = 0;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.BackColor = Color.FromArgb(220, 235, 252);
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(208, 225, 253);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCancel.ForeColor = Color.FromArgb(13, 59, 102);
            btnCancel.Location = new Point(300, 428);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(110, 34);
            btnCancel.TabIndex = 16;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.BackColor = Color.SteelBlue;
            btnSave.Cursor = Cursors.Hand;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(21, 101, 192);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(420, 428);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 34);
            btnSave.TabIndex = 17;
            btnSave.Text = "Save Librarian";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // lblPasswordErr
            // 
            lblPasswordErr.Font = new Font("Segoe UI", 8F);
            lblPasswordErr.ForeColor = Color.FromArgb(220, 38, 38);
            lblPasswordErr.Location = new Point(20, 385);
            lblPasswordErr.Name = "lblPasswordErr";
            lblPasswordErr.Size = new Size(520, 34);
            lblPasswordErr.TabIndex = 15;
            lblPasswordErr.UseMnemonic = false;
            lblPasswordErr.Visible = false;
            // 
            // txtPassword
            // 
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI", 9.5F);
            txtPassword.ForeColor = Color.FromArgb(13, 59, 102);
            txtPassword.Location = new Point(20, 354);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(520, 29);
            txtPassword.TabIndex = 14;
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.TextChanged += txtPassword_TextChanged;
            // 
            // lblPasswordLabel
            // 
            lblPasswordLabel.AutoSize = true;
            lblPasswordLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPasswordLabel.ForeColor = Color.FromArgb(13, 59, 102);
            lblPasswordLabel.Location = new Point(20, 332);
            lblPasswordLabel.Name = "lblPasswordLabel";
            lblPasswordLabel.Size = new Size(149, 20);
            lblPasswordLabel.TabIndex = 13;
            lblPasswordLabel.Text = "Account Password *";
            // 
            // lblEmailErr
            // 
            lblEmailErr.Font = new Font("Segoe UI", 8F);
            lblEmailErr.ForeColor = Color.FromArgb(220, 38, 38);
            lblEmailErr.Location = new Point(290, 295);
            lblEmailErr.Name = "lblEmailErr";
            lblEmailErr.Size = new Size(250, 34);
            lblEmailErr.TabIndex = 12;
            lblEmailErr.UseMnemonic = false;
            lblEmailErr.Visible = false;
            // 
            // txtEmail
            // 
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.Font = new Font("Segoe UI", 9.5F);
            txtEmail.ForeColor = Color.FromArgb(13, 59, 102);
            txtEmail.Location = new Point(290, 264);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(250, 29);
            txtEmail.TabIndex = 11;
            txtEmail.TextChanged += txtEmail_TextChanged;
            // 
            // lblEmailLabel
            // 
            lblEmailLabel.AutoSize = true;
            lblEmailLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEmailLabel.ForeColor = Color.FromArgb(13, 59, 102);
            lblEmailLabel.Location = new Point(290, 242);
            lblEmailLabel.Name = "lblEmailLabel";
            lblEmailLabel.Size = new Size(116, 20);
            lblEmailLabel.TabIndex = 10;
            lblEmailLabel.Text = "Email Address *";
            // 
            // lblPhoneErr
            // 
            lblPhoneErr.Font = new Font("Segoe UI", 8F);
            lblPhoneErr.ForeColor = Color.FromArgb(220, 38, 38);
            lblPhoneErr.Location = new Point(20, 295);
            lblPhoneErr.Name = "lblPhoneErr";
            lblPhoneErr.Size = new Size(250, 34);
            lblPhoneErr.TabIndex = 9;
            lblPhoneErr.UseMnemonic = false;
            lblPhoneErr.Visible = false;
            // 
            // txtPhone
            // 
            txtPhone.BorderStyle = BorderStyle.FixedSingle;
            txtPhone.Font = new Font("Segoe UI", 9.5F);
            txtPhone.ForeColor = Color.FromArgb(13, 59, 102);
            txtPhone.Location = new Point(20, 264);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(250, 29);
            txtPhone.TabIndex = 8;
            txtPhone.TextChanged += txtPhone_TextChanged;
            // 
            // lblPhoneLabel
            // 
            lblPhoneLabel.AutoSize = true;
            lblPhoneLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPhoneLabel.ForeColor = Color.FromArgb(13, 59, 102);
            lblPhoneLabel.Location = new Point(20, 242);
            lblPhoneLabel.Name = "lblPhoneLabel";
            lblPhoneLabel.Size = new Size(125, 20);
            lblPhoneLabel.TabIndex = 7;
            lblPhoneLabel.Text = "Phone Number *";
            // 
            // lblUsernameErr
            // 
            lblUsernameErr.Font = new Font("Segoe UI", 8F);
            lblUsernameErr.ForeColor = Color.FromArgb(220, 38, 38);
            lblUsernameErr.Location = new Point(20, 205);
            lblUsernameErr.Name = "lblUsernameErr";
            lblUsernameErr.Size = new Size(250, 34);
            lblUsernameErr.TabIndex = 6;
            lblUsernameErr.UseMnemonic = false;
            lblUsernameErr.Visible = false;
            // 
            // txtUsername
            // 
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Font = new Font("Segoe UI", 9.5F);
            txtUsername.ForeColor = Color.FromArgb(13, 59, 102);
            txtUsername.Location = new Point(20, 174);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(250, 29);
            txtUsername.TabIndex = 4;
            txtUsername.TextChanged += txtUsername_TextChanged;
            // 
            // lblUsernameLabel
            // 
            lblUsernameLabel.AutoSize = true;
            lblUsernameLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblUsernameLabel.ForeColor = Color.FromArgb(13, 59, 102);
            lblUsernameLabel.Location = new Point(20, 152);
            lblUsernameLabel.Name = "lblUsernameLabel";
            lblUsernameLabel.Size = new Size(91, 20);
            lblUsernameLabel.TabIndex = 3;
            lblUsernameLabel.Text = "Username *";
            // 
            // lblDobErr
            // 
            lblDobErr.Font = new Font("Segoe UI", 8F);
            lblDobErr.ForeColor = Color.FromArgb(220, 38, 38);
            lblDobErr.Location = new Point(290, 205);
            lblDobErr.Name = "lblDobErr";
            lblDobErr.Size = new Size(250, 34);
            lblDobErr.TabIndex = 22;
            lblDobErr.UseMnemonic = false;
            lblDobErr.Visible = false;
            // 
            // dtpDob
            // 
            dtpDob.CustomFormat = " ";
            dtpDob.Font = new Font("Segoe UI", 9.5F);
            dtpDob.Format = DateTimePickerFormat.Custom;
            dtpDob.Location = new Point(290, 174);
            dtpDob.Name = "dtpDob";
            dtpDob.Size = new Size(250, 29);
            dtpDob.TabIndex = 5;
            // 
            // lblDobLabel
            // 
            lblDobLabel.AutoSize = true;
            lblDobLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDobLabel.ForeColor = Color.FromArgb(13, 59, 102);
            lblDobLabel.Location = new Point(290, 152);
            lblDobLabel.Name = "lblDobLabel";
            lblDobLabel.Size = new Size(110, 20);
            lblDobLabel.TabIndex = 21;
            lblDobLabel.Text = "Date of Birth *";
            // 
            // lblNameErr
            // 
            lblNameErr.Font = new Font("Segoe UI", 8F);
            lblNameErr.ForeColor = Color.FromArgb(220, 38, 38);
            lblNameErr.Location = new Point(20, 115);
            lblNameErr.Name = "lblNameErr";
            lblNameErr.Size = new Size(250, 34);
            lblNameErr.TabIndex = 20;
            lblNameErr.UseMnemonic = false;
            lblNameErr.Visible = false;
            // 
            // txtName
            // 
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Font = new Font("Segoe UI", 9.5F);
            txtName.ForeColor = Color.FromArgb(13, 59, 102);
            txtName.Location = new Point(20, 84);
            txtName.Name = "txtName";
            txtName.Size = new Size(250, 29);
            txtName.TabIndex = 1;
            txtName.TextChanged += txtName_TextChanged;
            // 
            // lblNameLabel
            // 
            lblNameLabel.AutoSize = true;
            lblNameLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblNameLabel.ForeColor = Color.FromArgb(13, 59, 102);
            lblNameLabel.Location = new Point(20, 62);
            lblNameLabel.Name = "lblNameLabel";
            lblNameLabel.Size = new Size(91, 20);
            lblNameLabel.TabIndex = 0;
            lblNameLabel.Text = "Full Name *";
            // 
            // lblGenderErr
            // 
            lblGenderErr.Font = new Font("Segoe UI", 8F);
            lblGenderErr.ForeColor = Color.FromArgb(220, 38, 38);
            lblGenderErr.Location = new Point(290, 115);
            lblGenderErr.Name = "lblGenderErr";
            lblGenderErr.Size = new Size(250, 34);
            lblGenderErr.TabIndex = 24;
            lblGenderErr.UseMnemonic = false;
            lblGenderErr.Visible = false;
            // 
            // cmbGender
            // 
            cmbGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGender.Font = new Font("Segoe UI", 9.5F);
            cmbGender.ForeColor = Color.FromArgb(13, 59, 102);
            cmbGender.FormattingEnabled = true;
            cmbGender.Items.AddRange(new object[] { "Male", "Female" });
            cmbGender.Location = new Point(290, 84);
            cmbGender.Name = "cmbGender";
            cmbGender.Size = new Size(250, 29);
            cmbGender.TabIndex = 2;
            cmbGender.SelectedIndexChanged += cmbGender_SelectedIndexChanged;
            // 
            // lblGenderLabel
            // 
            lblGenderLabel.AutoSize = true;
            lblGenderLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblGenderLabel.ForeColor = Color.FromArgb(13, 59, 102);
            lblGenderLabel.Location = new Point(290, 62);
            lblGenderLabel.Name = "lblGenderLabel";
            lblGenderLabel.Size = new Size(70, 20);
            lblGenderLabel.TabIndex = 23;
            lblGenderLabel.Text = "Gender *";
            // 
            // lblHeader
            // 
            lblHeader.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblHeader.ForeColor = Color.FromArgb(13, 59, 102);
            lblHeader.Location = new Point(20, 12);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(520, 36);
            lblHeader.TabIndex = 0;
            lblHeader.Text = "Add New Librarian";
            lblHeader.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LibrarianEditDialog
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(235, 243, 250);
            CancelButton = btnCancel;
            ClientSize = new Size(560, 485);
            Controls.Add(pnlContainer);
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(560, 485);
            Name = "LibrarianEditDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "  Library Management System — Add Librarian";
            pnlContainer.ResumeLayout(false);
            pnlContainer.PerformLayout();
            ResumeLayout(false);

        }
    }
}
