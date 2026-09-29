namespace LibraryManagementSystem.Dialogs
{
    partial class AuthorEditDialog
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

        private System.Windows.Forms.Label lblDobLabel;
        private System.Windows.Forms.DateTimePicker dtpDob;
        private System.Windows.Forms.Label lblDobErr;

        private System.Windows.Forms.Label lblBioLabel;
        private System.Windows.Forms.TextBox txtBio;
        private System.Windows.Forms.Label lblBioErr;

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
            lblBioErr = new Label();
            txtBio = new TextBox();
            lblBioLabel = new Label();
            lblDobErr = new Label();
            dtpDob = new DateTimePicker();
            lblDobLabel = new Label();
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
            pnlContainer.Controls.Add(lblBioErr);
            pnlContainer.Controls.Add(txtBio);
            pnlContainer.Controls.Add(lblBioLabel);
            pnlContainer.Controls.Add(lblDobErr);
            pnlContainer.Controls.Add(dtpDob);
            pnlContainer.Controls.Add(lblDobLabel);
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
            lblDobErr.BringToFront();
            lblBioErr.BringToFront();

            pnlContainer.Location = new Point(0, 0);
            pnlContainer.Name = "pnlContainer";
            pnlContainer.Padding = new Padding(20, 16, 20, 16);
            pnlContainer.Size = new Size(520, 420);
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
            btnCancel.Location = new Point(270, 370);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(110, 32);
            btnCancel.TabIndex = 6;
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
            btnSave.Location = new Point(390, 370);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(110, 32);
            btnSave.TabIndex = 7;
            btnSave.Text = "Save Author";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // lblBioErr
            // 
            lblBioErr.Font = new Font("Segoe UI", 8F);
            lblBioErr.ForeColor = Color.FromArgb(220, 38, 38);
            lblBioErr.Location = new Point(20, 335);
            lblBioErr.Name = "lblBioErr";
            lblBioErr.Size = new Size(480, 24);
            lblBioErr.TabIndex = 13;
            lblBioErr.Visible = false;
            // 
            // txtBio
            // 
            txtBio.BorderStyle = BorderStyle.FixedSingle;
            txtBio.Font = new Font("Segoe UI", 9.5F);
            txtBio.ForeColor = Color.FromArgb(13, 59, 102);
            txtBio.Location = new Point(20, 236);
            txtBio.Multiline = true;
            txtBio.Name = "txtBio";
            txtBio.ScrollBars = ScrollBars.Vertical;
            txtBio.Size = new Size(480, 95);
            txtBio.TabIndex = 5;
            txtBio.TextChanged += txtBio_TextChanged;
            // 
            // lblBioLabel
            // 
            lblBioLabel.AutoSize = true;
            lblBioLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblBioLabel.ForeColor = Color.FromArgb(13, 59, 102);
            lblBioLabel.Location = new Point(20, 214);
            lblBioLabel.Name = "lblBioLabel";
            lblBioLabel.Size = new Size(81, 20);
            lblBioLabel.TabIndex = 11;
            lblBioLabel.Text = "Biography";
            // 
            // lblDobErr
            // 
            lblDobErr.Font = new Font("Segoe UI", 8F);
            lblDobErr.ForeColor = Color.FromArgb(220, 38, 38);
            lblDobErr.Location = new Point(20, 185);
            lblDobErr.Name = "lblDobErr";
            lblDobErr.Size = new Size(232, 24);
            lblDobErr.TabIndex = 10;
            lblDobErr.Visible = false;
            // 
            // dtpDob
            // 
            dtpDob.CustomFormat = " ";
            dtpDob.Font = new Font("Segoe UI", 9.5F);
            dtpDob.Format = DateTimePickerFormat.Custom;
            dtpDob.Location = new Point(20, 154);
            dtpDob.Name = "dtpDob";
            dtpDob.Size = new Size(232, 29);
            dtpDob.TabIndex = 4;
            // 
            // lblDobLabel
            // 
            lblDobLabel.AutoSize = true;
            lblDobLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDobLabel.ForeColor = Color.FromArgb(13, 59, 102);
            lblDobLabel.Location = new Point(20, 132);
            lblDobLabel.Name = "lblDobLabel";
            lblDobLabel.Size = new Size(110, 20);
            lblDobLabel.TabIndex = 8;
            lblDobLabel.Text = "Date of Birth *";
            // 
            // lblGenderErr
            // 
            lblGenderErr.Font = new Font("Segoe UI", 8F);
            lblGenderErr.ForeColor = Color.FromArgb(220, 38, 38);
            lblGenderErr.Location = new Point(268, 103);
            lblGenderErr.Name = "lblGenderErr";
            lblGenderErr.Size = new Size(232, 24);
            lblGenderErr.TabIndex = 7;
            lblGenderErr.Visible = false;
            // 
            // cmbGender
            // 
            cmbGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGender.Font = new Font("Segoe UI", 9.5F);
            cmbGender.ForeColor = Color.FromArgb(13, 59, 102);
            cmbGender.FormattingEnabled = true;
            cmbGender.Items.AddRange(new object[] { "Male", "Female" });
            cmbGender.Location = new Point(268, 72);
            cmbGender.Name = "cmbGender";
            cmbGender.Size = new Size(232, 29);
            cmbGender.TabIndex = 2;
            cmbGender.SelectedIndexChanged += cmbGender_SelectedIndexChanged;
            // 
            // lblGenderLabel
            // 
            lblGenderLabel.AutoSize = true;
            lblGenderLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblGenderLabel.ForeColor = Color.FromArgb(13, 59, 102);
            lblGenderLabel.Location = new Point(268, 50);
            lblGenderLabel.Name = "lblGenderLabel";
            lblGenderLabel.Size = new Size(70, 20);
            lblGenderLabel.TabIndex = 5;
            lblGenderLabel.Text = "Gender *";
            // 
            // lblNameErr
            // 
            lblNameErr.Font = new Font("Segoe UI", 8F);
            lblNameErr.ForeColor = Color.FromArgb(220, 38, 38);
            lblNameErr.Location = new Point(20, 103);
            lblNameErr.Name = "lblNameErr";
            lblNameErr.Size = new Size(232, 24);
            lblNameErr.TabIndex = 4;
            lblNameErr.Visible = false;
            // 
            // txtName
            // 
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Font = new Font("Segoe UI", 9.5F);
            txtName.ForeColor = Color.FromArgb(13, 59, 102);
            txtName.Location = new Point(20, 72);
            txtName.Name = "txtName";
            txtName.Size = new Size(232, 29);
            txtName.TabIndex = 1;
            txtName.TextChanged += txtName_TextChanged;
            // 
            // lblNameLabel
            // 
            lblNameLabel.AutoSize = true;
            lblNameLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblNameLabel.ForeColor = Color.FromArgb(13, 59, 102);
            lblNameLabel.Location = new Point(20, 50);
            lblNameLabel.Name = "lblNameLabel";
            lblNameLabel.Size = new Size(110, 20);
            lblNameLabel.TabIndex = 0;
            lblNameLabel.Text = "Author Name *";
            // 
            // lblHeader
            // 
            lblHeader.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblHeader.ForeColor = Color.FromArgb(13, 59, 102);
            lblHeader.Location = new Point(20, 8);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(480, 40);
            lblHeader.TabIndex = 14;
            lblHeader.Text = "Add New Author";
            lblHeader.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // AuthorEditDialog
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(520, 420);
            Controls.Add(pnlContainer);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AuthorEditDialog";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Library Management System — Author";
            pnlContainer.ResumeLayout(false);
            pnlContainer.PerformLayout();
            ResumeLayout(false);
        }
    }
}
