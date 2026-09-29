namespace LibraryManagementSystem.Dialogs
{
    partial class MemberEditDialog
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Label lblHeader;

        private System.Windows.Forms.Label lblNameLabel;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblNameErr;

        private System.Windows.Forms.Label lblPhoneLabel;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblPhoneErr;

        private System.Windows.Forms.Label lblGenderLabel;
        private System.Windows.Forms.ComboBox cmbGender;
        private System.Windows.Forms.Label lblGenderErr;

        private System.Windows.Forms.Label lblEmailLabel;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmailErr;

        private System.Windows.Forms.Label lblAddressLabel;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblAddressErr;

        private System.Windows.Forms.Label lblDobLabel;
        private System.Windows.Forms.DateTimePicker dtpDob;
        private System.Windows.Forms.Label lblDobErr;

        private System.Windows.Forms.Label lblJoinLabel;
        private System.Windows.Forms.DateTimePicker dtpJoin;
        private System.Windows.Forms.Label lblJoinErr;

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
            lblDobErr = new Label();
            dtpDob = new DateTimePicker();
            lblDobLabel = new Label();
            lblJoinErr = new Label();
            dtpJoin = new DateTimePicker();
            lblJoinLabel = new Label();
            lblAddressErr = new Label();
            txtAddress = new TextBox();
            lblAddressLabel = new Label();
            lblEmailErr = new Label();
            txtEmail = new TextBox();
            lblEmailLabel = new Label();
            lblPhoneErr = new Label();
            txtPhone = new TextBox();
            lblPhoneLabel = new Label();
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
            pnlContainer.AutoScroll = true;
            pnlContainer.BackColor = Color.FromArgb(235, 243, 250);
            pnlContainer.Controls.Add(btnCancel);
            pnlContainer.Controls.Add(btnSave);
            pnlContainer.Controls.Add(lblJoinErr);
            pnlContainer.Controls.Add(dtpJoin);
            pnlContainer.Controls.Add(lblJoinLabel);
            pnlContainer.Controls.Add(lblDobErr);
            pnlContainer.Controls.Add(dtpDob);
            pnlContainer.Controls.Add(lblDobLabel);
            pnlContainer.Controls.Add(lblAddressErr);
            pnlContainer.Controls.Add(txtAddress);
            pnlContainer.Controls.Add(lblAddressLabel);
            pnlContainer.Controls.Add(lblEmailErr);
            pnlContainer.Controls.Add(txtEmail);
            pnlContainer.Controls.Add(lblEmailLabel);
            pnlContainer.Controls.Add(lblGenderErr);
            pnlContainer.Controls.Add(cmbGender);
            pnlContainer.Controls.Add(lblGenderLabel);
            pnlContainer.Controls.Add(lblPhoneErr);
            pnlContainer.Controls.Add(txtPhone);
            pnlContainer.Controls.Add(lblPhoneLabel);
            pnlContainer.Controls.Add(lblNameErr);
            pnlContainer.Controls.Add(txtName);
            pnlContainer.Controls.Add(lblNameLabel);
            pnlContainer.Controls.Add(lblHeader);
            pnlContainer.Dock = DockStyle.Fill;
            lblNameErr.BringToFront();
            lblPhoneErr.BringToFront();
            lblGenderErr.BringToFront();
            lblEmailErr.BringToFront();
            lblAddressErr.BringToFront();
            lblDobErr.BringToFront();
            lblJoinErr.BringToFront();

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
            btnSave.Location = new Point(390, 472);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(110, 32);
            btnSave.TabIndex = 17;
            btnSave.Text = "Save Member";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // lblDobErr
            // 
            lblDobErr.Font = new Font("Segoe UI", 8F);
            lblDobErr.ForeColor = Color.FromArgb(220, 38, 38);
            lblDobErr.Location = new Point(20, 431);
            lblDobErr.Name = "lblDobErr";
            lblDobErr.Size = new Size(232, 24);
            lblDobErr.TabIndex = 14;
            lblDobErr.Visible = false;
            // 
            // dtpDob
            // 
            dtpDob.CustomFormat = " ";
            dtpDob.Font = new Font("Segoe UI", 9.5F);
            dtpDob.Format = DateTimePickerFormat.Custom;
            dtpDob.Location = new Point(20, 400);
            dtpDob.Name = "dtpDob";
            dtpDob.Size = new Size(232, 29);
            dtpDob.TabIndex = 13;
            // 
            // lblDobLabel
            // 
            lblDobLabel.AutoSize = true;
            lblDobLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDobLabel.ForeColor = Color.FromArgb(13, 59, 102);
            lblDobLabel.Location = new Point(20, 378);
            lblDobLabel.Name = "lblDobLabel";
            lblDobLabel.Size = new Size(110, 20);
            lblDobLabel.TabIndex = 12;
            lblDobLabel.Text = "Date of Birth *";
            // 
            // lblJoinErr
            // 
            lblJoinErr.Font = new Font("Segoe UI", 8F);
            lblJoinErr.ForeColor = Color.FromArgb(220, 38, 38);
            lblJoinErr.Location = new Point(268, 431);
            lblJoinErr.Name = "lblJoinErr";
            lblJoinErr.Size = new Size(232, 24);
            lblJoinErr.TabIndex = 17;
            lblJoinErr.Visible = false;
            // 
            // dtpJoin
            // 
            dtpJoin.CustomFormat = "MM/dd/yyyy";
            dtpJoin.Font = new Font("Segoe UI", 9.5F);
            dtpJoin.Format = DateTimePickerFormat.Custom;
            dtpJoin.Location = new Point(268, 400);
            dtpJoin.Name = "dtpJoin";
            dtpJoin.Size = new Size(232, 29);
            dtpJoin.TabIndex = 16;
            // 
            // lblJoinLabel
            // 
            lblJoinLabel.AutoSize = true;
            lblJoinLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblJoinLabel.ForeColor = Color.FromArgb(13, 59, 102);
            lblJoinLabel.Location = new Point(268, 378);
            lblJoinLabel.Name = "lblJoinLabel";
            lblJoinLabel.Size = new Size(86, 20);
            lblJoinLabel.TabIndex = 15;
            lblJoinLabel.Text = "Join Date *";
            // 
            // lblAddressErr
            // 
            lblAddressErr.Font = new Font("Segoe UI", 8F);
            lblAddressErr.ForeColor = Color.FromArgb(220, 38, 38);
            lblAddressErr.Location = new Point(20, 349);
            lblAddressErr.Name = "lblAddressErr";
            lblAddressErr.Size = new Size(480, 24);
            lblAddressErr.TabIndex = 12;
            lblAddressErr.Visible = false;
            // 
            // txtAddress
            // 
            txtAddress.BorderStyle = BorderStyle.FixedSingle;
            txtAddress.Font = new Font("Segoe UI", 9.5F);
            txtAddress.ForeColor = Color.FromArgb(13, 59, 102);
            txtAddress.Location = new Point(20, 318);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(480, 29);
            txtAddress.TabIndex = 11;
            txtAddress.TextChanged += txtAddress_TextChanged;
            // 
            // lblAddressLabel
            // 
            lblAddressLabel.AutoSize = true;
            lblAddressLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAddressLabel.ForeColor = Color.FromArgb(13, 59, 102);
            lblAddressLabel.Location = new Point(20, 296);
            lblAddressLabel.Name = "lblAddressLabel";
            lblAddressLabel.Size = new Size(77, 20);
            lblAddressLabel.TabIndex = 10;
            lblAddressLabel.Text = "Address *";
            // 
            // lblEmailErr
            // 
            lblEmailErr.Font = new Font("Segoe UI", 8F);
            lblEmailErr.ForeColor = Color.FromArgb(220, 38, 38);
            lblEmailErr.Location = new Point(20, 267);
            lblEmailErr.Name = "lblEmailErr";
            lblEmailErr.Size = new Size(480, 24);
            lblEmailErr.TabIndex = 9;
            lblEmailErr.Visible = false;
            // 
            // txtEmail
            // 
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.Font = new Font("Segoe UI", 9.5F);
            txtEmail.ForeColor = Color.FromArgb(13, 59, 102);
            txtEmail.Location = new Point(20, 236);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(480, 29);
            txtEmail.TabIndex = 8;
            txtEmail.TextChanged += txtEmail_TextChanged;
            // 
            // lblEmailLabel
            // 
            lblEmailLabel.AutoSize = true;
            lblEmailLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEmailLabel.ForeColor = Color.FromArgb(13, 59, 102);
            lblEmailLabel.Location = new Point(20, 214);
            lblEmailLabel.Name = "lblEmailLabel";
            lblEmailLabel.Size = new Size(119, 20);
            lblEmailLabel.TabIndex = 7;
            lblEmailLabel.Text = "Email Address *";
            // 
            // lblPhoneErr
            // 
            lblPhoneErr.Font = new Font("Segoe UI", 8F);
            lblPhoneErr.ForeColor = Color.FromArgb(220, 38, 38);
            lblPhoneErr.Location = new Point(20, 185);
            lblPhoneErr.Name = "lblPhoneErr";
            lblPhoneErr.Size = new Size(232, 24);
            lblPhoneErr.TabIndex = 6;
            lblPhoneErr.Visible = false;
            // 
            // txtPhone
            // 
            txtPhone.BorderStyle = BorderStyle.FixedSingle;
            txtPhone.Font = new Font("Segoe UI", 9.5F);
            txtPhone.ForeColor = Color.FromArgb(13, 59, 102);
            txtPhone.Location = new Point(20, 154);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(232, 29);
            txtPhone.TabIndex = 4;
            txtPhone.TextChanged += txtPhone_TextChanged;
            // 
            // lblPhoneLabel
            // 
            lblPhoneLabel.AutoSize = true;
            lblPhoneLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPhoneLabel.ForeColor = Color.FromArgb(13, 59, 102);
            lblPhoneLabel.Location = new Point(20, 132);
            lblPhoneLabel.Name = "lblPhoneLabel";
            lblPhoneLabel.Size = new Size(126, 20);
            lblPhoneLabel.TabIndex = 3;
            lblPhoneLabel.Text = "Phone Number *";
            // 
            // lblGenderErr
            // 
            lblGenderErr.Font = new Font("Segoe UI", 8F);
            lblGenderErr.ForeColor = Color.FromArgb(220, 38, 38);
            lblGenderErr.Location = new Point(268, 185);
            lblGenderErr.Name = "lblGenderErr";
            lblGenderErr.Size = new Size(232, 24);
            lblGenderErr.TabIndex = 9;
            lblGenderErr.Visible = false;
            // 
            // cmbGender
            // 
            cmbGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGender.Font = new Font("Segoe UI", 9.5F);
            cmbGender.ForeColor = Color.FromArgb(13, 59, 102);
            cmbGender.FormattingEnabled = true;
            cmbGender.Items.AddRange(new object[] { "Male", "Female" });
            cmbGender.Location = new Point(268, 154);
            cmbGender.Name = "cmbGender";
            cmbGender.Size = new Size(232, 29);
            cmbGender.TabIndex = 5;
            cmbGender.SelectedIndexChanged += cmbGender_SelectedIndexChanged;
            // 
            // lblGenderLabel
            // 
            lblGenderLabel.AutoSize = true;
            lblGenderLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblGenderLabel.ForeColor = Color.FromArgb(13, 59, 102);
            lblGenderLabel.Location = new Point(268, 132);
            lblGenderLabel.Name = "lblGenderLabel";
            lblGenderLabel.Size = new Size(70, 20);
            lblGenderLabel.TabIndex = 7;
            lblGenderLabel.Text = "Gender *";
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
            lblHeader.Location = new Point(20, 8);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(480, 40);
            lblHeader.TabIndex = 0;
            lblHeader.Text = "Add New Member";
            lblHeader.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MemberEditDialog
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
            Name = "MemberEditDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Library Management System — Add Member";
            pnlContainer.ResumeLayout(false);
            pnlContainer.PerformLayout();
            ResumeLayout(false);

        }
    }
}
