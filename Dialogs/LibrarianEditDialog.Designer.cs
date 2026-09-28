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

        private System.Windows.Forms.Label lblUsernameLabel;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblUsernameErr;

        private System.Windows.Forms.Label lblPhoneLabel;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblPhoneErr;

        private System.Windows.Forms.Label lblPositionLabel;
        private System.Windows.Forms.TextBox txtPosition;
        private System.Windows.Forms.Label lblPositionErr;

        private System.Windows.Forms.Label lblRoleLabel;
        private System.Windows.Forms.ComboBox cmbRole;
        private System.Windows.Forms.Label lblRoleErr;

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
            lblRoleErr = new Label();
            cmbRole = new ComboBox();
            lblRoleLabel = new Label();
            lblPositionErr = new Label();
            txtPosition = new TextBox();
            lblPositionLabel = new Label();
            lblPhoneErr = new Label();
            txtPhone = new TextBox();
            lblPhoneLabel = new Label();
            lblUsernameErr = new Label();
            txtUsername = new TextBox();
            lblUsernameLabel = new Label();
            lblNameErr = new Label();
            txtName = new TextBox();
            lblNameLabel = new Label();
            lblHeader = new Label();
            pnlContainer.SuspendLayout();
            SuspendLayout();
            // 
            // pnlContainer
            // 
            pnlContainer.AutoScroll = true;
            pnlContainer.BackColor = Color.FromArgb(235, 243, 250);
            pnlContainer.Controls.Add(btnCancel);
            pnlContainer.Controls.Add(btnSave);
            pnlContainer.Controls.Add(lblPasswordErr);
            pnlContainer.Controls.Add(txtPassword);
            pnlContainer.Controls.Add(lblPasswordLabel);
            pnlContainer.Controls.Add(lblRoleErr);
            pnlContainer.Controls.Add(cmbRole);
            pnlContainer.Controls.Add(lblRoleLabel);
            pnlContainer.Controls.Add(lblPositionErr);
            pnlContainer.Controls.Add(txtPosition);
            pnlContainer.Controls.Add(lblPositionLabel);
            pnlContainer.Controls.Add(lblPhoneErr);
            pnlContainer.Controls.Add(txtPhone);
            pnlContainer.Controls.Add(lblPhoneLabel);
            pnlContainer.Controls.Add(lblUsernameErr);
            pnlContainer.Controls.Add(txtUsername);
            pnlContainer.Controls.Add(lblUsernameLabel);
            pnlContainer.Controls.Add(lblNameErr);
            pnlContainer.Controls.Add(txtName);
            pnlContainer.Controls.Add(lblNameLabel);
            pnlContainer.Controls.Add(lblHeader);
            pnlContainer.Dock = DockStyle.Fill;
            lblNameErr.BringToFront();
            lblUsernameErr.BringToFront();
            lblPhoneErr.BringToFront();
            lblPositionErr.BringToFront();
            lblRoleErr.BringToFront();
            lblPasswordErr.BringToFront();

            pnlContainer.Location = new Point(0, 0);
            pnlContainer.Name = "pnlContainer";
            pnlContainer.Padding = new Padding(20, 16, 20, 16);
            pnlContainer.Size = new Size(520, 520);
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
            btnCancel.Location = new Point(270, 472);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(110, 32);
            btnCancel.TabIndex = 19;
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
            btnSave.Location = new Point(390, 472);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(110, 32);
            btnSave.TabIndex = 20;
            btnSave.Text = "Save Librarian";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // lblPasswordErr
            // 
            lblPasswordErr.Font = new Font("Segoe UI", 8F);
            lblPasswordErr.ForeColor = Color.FromArgb(220, 38, 38);
            lblPasswordErr.Location = new Point(20, 431);
            lblPasswordErr.Name = "lblPasswordErr";
            lblPasswordErr.Size = new Size(480, 24);
            lblPasswordErr.TabIndex = 18;
            lblPasswordErr.Visible = false;
            // 
            // txtPassword
            // 
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI", 9.5F);
            txtPassword.ForeColor = Color.FromArgb(13, 59, 102);
            txtPassword.Location = new Point(20, 400);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(480, 29);
            txtPassword.TabIndex = 17;
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.TextChanged += txtPassword_TextChanged;
            // 
            // lblPasswordLabel
            // 
            lblPasswordLabel.AutoSize = true;
            lblPasswordLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPasswordLabel.ForeColor = Color.FromArgb(13, 59, 102);
            lblPasswordLabel.Location = new Point(20, 378);
            lblPasswordLabel.Name = "lblPasswordLabel";
            lblPasswordLabel.Size = new Size(149, 20);
            lblPasswordLabel.TabIndex = 16;
            lblPasswordLabel.Text = "Account Password *";
            // 
            // lblRoleErr
            // 
            lblRoleErr.Font = new Font("Segoe UI", 8F);
            lblRoleErr.ForeColor = Color.FromArgb(220, 38, 38);
            lblRoleErr.Location = new Point(20, 349);
            lblRoleErr.Name = "lblRoleErr";
            lblRoleErr.Size = new Size(480, 24);
            lblRoleErr.TabIndex = 15;
            lblRoleErr.Visible = false;
            // 
            // cmbRole
            // 
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.Font = new Font("Segoe UI", 9.5F);
            cmbRole.ForeColor = Color.FromArgb(13, 59, 102);
            cmbRole.FormattingEnabled = true;
            cmbRole.Items.AddRange(new object[] { "Librarian", "Admin" });
            cmbRole.Location = new Point(20, 318);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new Size(480, 29);
            cmbRole.TabIndex = 14;
            cmbRole.SelectedIndexChanged += cmbRole_SelectedIndexChanged;
            // 
            // lblRoleLabel
            // 
            lblRoleLabel.AutoSize = true;
            lblRoleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblRoleLabel.ForeColor = Color.FromArgb(13, 59, 102);
            lblRoleLabel.Location = new Point(20, 296);
            lblRoleLabel.Name = "lblRoleLabel";
            lblRoleLabel.Size = new Size(102, 20);
            lblRoleLabel.TabIndex = 13;
            lblRoleLabel.Text = "Access Role *";
            // 
            // lblPositionErr
            // 
            lblPositionErr.Font = new Font("Segoe UI", 8F);
            lblPositionErr.ForeColor = Color.FromArgb(220, 38, 38);
            lblPositionErr.Location = new Point(268, 267);
            lblPositionErr.Name = "lblPositionErr";
            lblPositionErr.Size = new Size(232, 24);
            lblPositionErr.TabIndex = 12;
            lblPositionErr.Visible = false;
            // 
            // txtPosition
            // 
            txtPosition.BorderStyle = BorderStyle.FixedSingle;
            txtPosition.Font = new Font("Segoe UI", 9.5F);
            txtPosition.ForeColor = Color.FromArgb(13, 59, 102);
            txtPosition.Location = new Point(268, 236);
            txtPosition.Name = "txtPosition";
            txtPosition.Size = new Size(232, 29);
            txtPosition.TabIndex = 11;
            txtPosition.TextChanged += txtPosition_TextChanged;
            // 
            // lblPositionLabel
            // 
            lblPositionLabel.AutoSize = true;
            lblPositionLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPositionLabel.ForeColor = Color.FromArgb(13, 59, 102);
            lblPositionLabel.Location = new Point(268, 214);
            lblPositionLabel.Name = "lblPositionLabel";
            lblPositionLabel.Size = new Size(66, 20);
            lblPositionLabel.TabIndex = 10;
            lblPositionLabel.Text = "Position";
            // 
            // lblPhoneErr
            // 
            lblPhoneErr.Font = new Font("Segoe UI", 8F);
            lblPhoneErr.ForeColor = Color.FromArgb(220, 38, 38);
            lblPhoneErr.Location = new Point(20, 267);
            lblPhoneErr.Name = "lblPhoneErr";
            lblPhoneErr.Size = new Size(232, 24);
            lblPhoneErr.TabIndex = 9;
            lblPhoneErr.Visible = false;
            // 
            // txtPhone
            // 
            txtPhone.BorderStyle = BorderStyle.FixedSingle;
            txtPhone.Font = new Font("Segoe UI", 9.5F);
            txtPhone.ForeColor = Color.FromArgb(13, 59, 102);
            txtPhone.Location = new Point(20, 236);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(232, 29);
            txtPhone.TabIndex = 8;
            txtPhone.TextChanged += txtPhone_TextChanged;
            // 
            // lblPhoneLabel
            // 
            lblPhoneLabel.AutoSize = true;
            lblPhoneLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPhoneLabel.ForeColor = Color.FromArgb(13, 59, 102);
            lblPhoneLabel.Location = new Point(20, 214);
            lblPhoneLabel.Name = "lblPhoneLabel";
            lblPhoneLabel.Size = new Size(115, 20);
            lblPhoneLabel.TabIndex = 7;
            lblPhoneLabel.Text = "Phone Number";
            // 
            // lblUsernameErr
            // 
            lblUsernameErr.Font = new Font("Segoe UI", 8F);
            lblUsernameErr.ForeColor = Color.FromArgb(220, 38, 38);
            lblUsernameErr.Location = new Point(20, 185);
            lblUsernameErr.Name = "lblUsernameErr";
            lblUsernameErr.Size = new Size(480, 24);
            lblUsernameErr.TabIndex = 6;
            lblUsernameErr.Visible = false;
            // 
            // txtUsername
            // 
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Font = new Font("Segoe UI", 9.5F);
            txtUsername.ForeColor = Color.FromArgb(13, 59, 102);
            txtUsername.Location = new Point(20, 154);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(480, 29);
            txtUsername.TabIndex = 5;
            txtUsername.TextChanged += txtUsername_TextChanged;
            // 
            // lblUsernameLabel
            // 
            lblUsernameLabel.AutoSize = true;
            lblUsernameLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblUsernameLabel.ForeColor = Color.FromArgb(13, 59, 102);
            lblUsernameLabel.Location = new Point(20, 132);
            lblUsernameLabel.Name = "lblUsernameLabel";
            lblUsernameLabel.Size = new Size(91, 20);
            lblUsernameLabel.TabIndex = 4;
            lblUsernameLabel.Text = "Username *";
            // 
            // lblNameErr
            // 
            lblNameErr.Font = new Font("Segoe UI", 8F);
            lblNameErr.ForeColor = Color.FromArgb(220, 38, 38);
            lblNameErr.Location = new Point(20, 103);
            lblNameErr.Name = "lblNameErr";
            lblNameErr.Size = new Size(480, 24);
            lblNameErr.TabIndex = 3;
            lblNameErr.Visible = false;
            // 
            // txtName
            // 
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Font = new Font("Segoe UI", 9.5F);
            txtName.ForeColor = Color.FromArgb(13, 59, 102);
            txtName.Location = new Point(20, 72);
            txtName.Name = "txtName";
            txtName.Size = new Size(480, 29);
            txtName.TabIndex = 2;
            txtName.TextChanged += txtName_TextChanged;
            // 
            // lblNameLabel
            // 
            lblNameLabel.AutoSize = true;
            lblNameLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblNameLabel.ForeColor = Color.FromArgb(13, 59, 102);
            lblNameLabel.Location = new Point(20, 50);
            lblNameLabel.Name = "lblNameLabel";
            lblNameLabel.Size = new Size(91, 20);
            lblNameLabel.TabIndex = 1;
            lblNameLabel.Text = "Full Name *";
            // 
            // lblHeader
            // 
            lblHeader.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblHeader.ForeColor = Color.FromArgb(13, 59, 102);
            lblHeader.Location = new Point(20, 14);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(480, 28);
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
            ClientSize = new Size(520, 520);
            Controls.Add(pnlContainer);
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(520, 520);
            Name = "LibrarianEditDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Library Management System — Add Librarian";
            pnlContainer.ResumeLayout(false);
            pnlContainer.PerformLayout();
            ResumeLayout(false);

        }
    }
}
