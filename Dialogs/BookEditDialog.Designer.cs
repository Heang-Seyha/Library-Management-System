namespace LibraryManagementSystem.Dialogs
{
    partial class BookEditDialog
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Label lblHeader;

        private System.Windows.Forms.Label lblTitleLabel;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblTitleErr;

        private System.Windows.Forms.Label lblISBNLabel;
        private System.Windows.Forms.TextBox txtISBN;
        private System.Windows.Forms.Label lblISBNErr;

        private System.Windows.Forms.Label lblYearLabel;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.Label lblYearErr;

        private System.Windows.Forms.Label lblCategoryLabel;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblCategoryErr;

        private System.Windows.Forms.Label lblAuthorLabel;
        private System.Windows.Forms.ComboBox cmbAuthor;
        private System.Windows.Forms.Label lblAuthorErr;

        private System.Windows.Forms.Label lblPublisherLabel;
        private System.Windows.Forms.ComboBox cmbPublisher;
        private System.Windows.Forms.Label lblPublisherErr;

        private System.Windows.Forms.Label lblTotalLabel;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label lblTotalErr;

        private System.Windows.Forms.Label lblAvailableLabel;
        private System.Windows.Forms.TextBox txtAvailable;
        private System.Windows.Forms.Label lblAvailableErr;

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
            lblAvailableErr = new Label();
            txtAvailable = new TextBox();
            lblAvailableLabel = new Label();
            lblTotalErr = new Label();
            txtTotal = new TextBox();
            lblTotalLabel = new Label();
            lblPublisherErr = new Label();
            cmbPublisher = new ComboBox();
            lblPublisherLabel = new Label();
            lblAuthorErr = new Label();
            cmbAuthor = new ComboBox();
            lblAuthorLabel = new Label();
            lblCategoryErr = new Label();
            cmbCategory = new ComboBox();
            lblCategoryLabel = new Label();
            lblYearErr = new Label();
            txtYear = new TextBox();
            lblYearLabel = new Label();
            lblISBNErr = new Label();
            txtISBN = new TextBox();
            lblISBNLabel = new Label();
            lblTitleErr = new Label();
            txtTitle = new TextBox();
            lblTitleLabel = new Label();
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
            pnlContainer.Controls.Add(lblAvailableErr);
            pnlContainer.Controls.Add(txtAvailable);
            pnlContainer.Controls.Add(lblAvailableLabel);
            pnlContainer.Controls.Add(lblTotalErr);
            pnlContainer.Controls.Add(txtTotal);
            pnlContainer.Controls.Add(lblTotalLabel);
            pnlContainer.Controls.Add(lblPublisherErr);
            pnlContainer.Controls.Add(cmbPublisher);
            pnlContainer.Controls.Add(lblPublisherLabel);
            pnlContainer.Controls.Add(lblAuthorErr);
            pnlContainer.Controls.Add(cmbAuthor);
            pnlContainer.Controls.Add(lblAuthorLabel);
            pnlContainer.Controls.Add(lblCategoryErr);
            pnlContainer.Controls.Add(cmbCategory);
            pnlContainer.Controls.Add(lblCategoryLabel);
            pnlContainer.Controls.Add(lblYearErr);
            pnlContainer.Controls.Add(txtYear);
            pnlContainer.Controls.Add(lblYearLabel);
            pnlContainer.Controls.Add(lblISBNErr);
            pnlContainer.Controls.Add(txtISBN);
            pnlContainer.Controls.Add(lblISBNLabel);
            pnlContainer.Controls.Add(lblTitleErr);
            pnlContainer.Controls.Add(txtTitle);
            pnlContainer.Controls.Add(lblTitleLabel);
            pnlContainer.Controls.Add(lblHeader);
            pnlContainer.Dock = DockStyle.Fill;
            lblTitleErr.BringToFront();
            lblISBNErr.BringToFront();
            lblYearErr.BringToFront();
            lblCategoryErr.BringToFront();
            lblAuthorErr.BringToFront();
            lblPublisherErr.BringToFront();
            lblTotalErr.BringToFront();
            lblAvailableErr.BringToFront();

            pnlContainer.Location = new Point(0, 0);
            pnlContainer.Name = "pnlContainer";
            pnlContainer.Padding = new Padding(20, 16, 20, 16);
            pnlContainer.Size = new Size(536, 520);
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
            btnCancel.Location = new Point(286, 472);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(110, 32);
            btnCancel.TabIndex = 25;
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
            btnSave.Location = new Point(406, 472);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(110, 32);
            btnSave.TabIndex = 26;
            btnSave.Text = "Save Book";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // lblAvailableErr
            // 
            lblAvailableErr.Font = new Font("Segoe UI", 8F);
            lblAvailableErr.ForeColor = Color.FromArgb(220, 38, 38);
            lblAvailableErr.Location = new Point(276, 431);
            lblAvailableErr.Name = "lblAvailableErr";
            lblAvailableErr.Size = new Size(240, 24);
            lblAvailableErr.TabIndex = 24;
            lblAvailableErr.Visible = false;
            // 
            // txtAvailable
            // 
            txtAvailable.BorderStyle = BorderStyle.FixedSingle;
            txtAvailable.Font = new Font("Segoe UI", 9.5F);
            txtAvailable.ForeColor = Color.FromArgb(13, 59, 102);
            txtAvailable.Location = new Point(276, 400);
            txtAvailable.Name = "txtAvailable";
            txtAvailable.Size = new Size(240, 29);
            txtAvailable.TabIndex = 23;
            txtAvailable.TextChanged += txtAvailable_TextChanged;
            // 
            // lblAvailableLabel
            // 
            lblAvailableLabel.AutoSize = true;
            lblAvailableLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAvailableLabel.ForeColor = Color.FromArgb(13, 59, 102);
            lblAvailableLabel.Location = new Point(276, 378);
            lblAvailableLabel.Name = "lblAvailableLabel";
            lblAvailableLabel.Size = new Size(134, 20);
            lblAvailableLabel.TabIndex = 22;
            lblAvailableLabel.Text = "Available Copies *";
            // 
            // lblTotalErr
            // 
            lblTotalErr.Font = new Font("Segoe UI", 8F);
            lblTotalErr.ForeColor = Color.FromArgb(220, 38, 38);
            lblTotalErr.Location = new Point(20, 431);
            lblTotalErr.Name = "lblTotalErr";
            lblTotalErr.Size = new Size(240, 24);
            lblTotalErr.TabIndex = 21;
            lblTotalErr.Visible = false;
            // 
            // txtTotal
            // 
            txtTotal.BorderStyle = BorderStyle.FixedSingle;
            txtTotal.Font = new Font("Segoe UI", 9.5F);
            txtTotal.ForeColor = Color.FromArgb(13, 59, 102);
            txtTotal.Location = new Point(20, 400);
            txtTotal.Name = "txtTotal";
            txtTotal.Size = new Size(240, 29);
            txtTotal.TabIndex = 20;
            txtTotal.TextChanged += txtTotal_TextChanged;
            // 
            // lblTotalLabel
            // 
            lblTotalLabel.AutoSize = true;
            lblTotalLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotalLabel.ForeColor = Color.FromArgb(13, 59, 102);
            lblTotalLabel.Location = new Point(20, 378);
            lblTotalLabel.Name = "lblTotalLabel";
            lblTotalLabel.Size = new Size(105, 20);
            lblTotalLabel.TabIndex = 19;
            lblTotalLabel.Text = "Total Copies *";
            // 
            // lblPublisherErr
            // 
            lblPublisherErr.Font = new Font("Segoe UI", 8F);
            lblPublisherErr.ForeColor = Color.FromArgb(220, 38, 38);
            lblPublisherErr.Location = new Point(20, 349);
            lblPublisherErr.Name = "lblPublisherErr";
            lblPublisherErr.Size = new Size(496, 24);
            lblPublisherErr.TabIndex = 18;
            lblPublisherErr.Visible = false;
            // 
            // cmbPublisher
            // 
            cmbPublisher.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPublisher.Font = new Font("Segoe UI", 9.5F);
            cmbPublisher.ForeColor = Color.FromArgb(13, 59, 102);
            cmbPublisher.FormattingEnabled = true;
            cmbPublisher.Location = new Point(20, 318);
            cmbPublisher.Name = "cmbPublisher";
            cmbPublisher.Size = new Size(496, 29);
            cmbPublisher.TabIndex = 17;
            cmbPublisher.SelectedIndexChanged += cmbPublisher_SelectedIndexChanged;
            // 
            // lblPublisherLabel
            // 
            lblPublisherLabel.AutoSize = true;
            lblPublisherLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPublisherLabel.ForeColor = Color.FromArgb(13, 59, 102);
            lblPublisherLabel.Location = new Point(20, 296);
            lblPublisherLabel.Name = "lblPublisherLabel";
            lblPublisherLabel.Size = new Size(85, 20);
            lblPublisherLabel.TabIndex = 16;
            lblPublisherLabel.Text = "Publisher *";
            // 
            // lblAuthorErr
            // 
            lblAuthorErr.Font = new Font("Segoe UI", 8F);
            lblAuthorErr.ForeColor = Color.FromArgb(220, 38, 38);
            lblAuthorErr.Location = new Point(276, 267);
            lblAuthorErr.Name = "lblAuthorErr";
            lblAuthorErr.Size = new Size(240, 24);
            lblAuthorErr.TabIndex = 15;
            lblAuthorErr.Visible = false;
            // 
            // cmbAuthor
            // 
            cmbAuthor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAuthor.Font = new Font("Segoe UI", 9.5F);
            cmbAuthor.ForeColor = Color.FromArgb(13, 59, 102);
            cmbAuthor.FormattingEnabled = true;
            cmbAuthor.Location = new Point(276, 236);
            cmbAuthor.Name = "cmbAuthor";
            cmbAuthor.Size = new Size(240, 29);
            cmbAuthor.TabIndex = 14;
            cmbAuthor.SelectedIndexChanged += cmbAuthor_SelectedIndexChanged;
            // 
            // lblAuthorLabel
            // 
            lblAuthorLabel.AutoSize = true;
            lblAuthorLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAuthorLabel.ForeColor = Color.FromArgb(13, 59, 102);
            lblAuthorLabel.Location = new Point(276, 214);
            lblAuthorLabel.Name = "lblAuthorLabel";
            lblAuthorLabel.Size = new Size(70, 20);
            lblAuthorLabel.TabIndex = 13;
            lblAuthorLabel.Text = "Author *";
            // 
            // lblCategoryErr
            // 
            lblCategoryErr.Font = new Font("Segoe UI", 8F);
            lblCategoryErr.ForeColor = Color.FromArgb(220, 38, 38);
            lblCategoryErr.Location = new Point(20, 267);
            lblCategoryErr.Name = "lblCategoryErr";
            lblCategoryErr.Size = new Size(240, 24);
            lblCategoryErr.TabIndex = 12;
            lblCategoryErr.Visible = false;
            // 
            // cmbCategory
            // 
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.Font = new Font("Segoe UI", 9.5F);
            cmbCategory.ForeColor = Color.FromArgb(13, 59, 102);
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Location = new Point(20, 236);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(240, 29);
            cmbCategory.TabIndex = 11;
            cmbCategory.SelectedIndexChanged += cmbCategory_SelectedIndexChanged;
            // 
            // lblCategoryLabel
            // 
            lblCategoryLabel.AutoSize = true;
            lblCategoryLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCategoryLabel.ForeColor = Color.FromArgb(13, 59, 102);
            lblCategoryLabel.Location = new Point(20, 214);
            lblCategoryLabel.Name = "lblCategoryLabel";
            lblCategoryLabel.Size = new Size(84, 20);
            lblCategoryLabel.TabIndex = 10;
            lblCategoryLabel.Text = "Category *";
            // 
            // lblYearErr
            // 
            lblYearErr.Font = new Font("Segoe UI", 8F);
            lblYearErr.ForeColor = Color.FromArgb(220, 38, 38);
            lblYearErr.Location = new Point(346, 185);
            lblYearErr.Name = "lblYearErr";
            lblYearErr.Size = new Size(170, 24);
            lblYearErr.TabIndex = 9;
            lblYearErr.Visible = false;
            // 
            // txtYear
            // 
            txtYear.BorderStyle = BorderStyle.FixedSingle;
            txtYear.Font = new Font("Segoe UI", 9.5F);
            txtYear.ForeColor = Color.FromArgb(13, 59, 102);
            txtYear.Location = new Point(346, 154);
            txtYear.Name = "txtYear";
            txtYear.Size = new Size(170, 29);
            txtYear.TabIndex = 8;
            txtYear.TextChanged += txtYear_TextChanged;
            // 
            // lblYearLabel
            // 
            lblYearLabel.AutoSize = true;
            lblYearLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblYearLabel.ForeColor = Color.FromArgb(13, 59, 102);
            lblYearLabel.Location = new Point(346, 132);
            lblYearLabel.Name = "lblYearLabel";
            lblYearLabel.Size = new Size(132, 20);
            lblYearLabel.TabIndex = 7;
            lblYearLabel.Text = "Publication Year *";
            // 
            // lblISBNErr
            // 
            lblISBNErr.Font = new Font("Segoe UI", 8F);
            lblISBNErr.ForeColor = Color.FromArgb(220, 38, 38);
            lblISBNErr.Location = new Point(20, 185);
            lblISBNErr.Name = "lblISBNErr";
            lblISBNErr.Size = new Size(310, 24);
            lblISBNErr.TabIndex = 6;
            lblISBNErr.Visible = false;
            // 
            // txtISBN
            // 
            txtISBN.BorderStyle = BorderStyle.FixedSingle;
            txtISBN.Font = new Font("Segoe UI", 9.5F);
            txtISBN.ForeColor = Color.FromArgb(13, 59, 102);
            txtISBN.Location = new Point(20, 154);
            txtISBN.Name = "txtISBN";
            txtISBN.Size = new Size(310, 29);
            txtISBN.TabIndex = 5;
            txtISBN.TextChanged += txtISBN_TextChanged;
            // 
            // lblISBNLabel
            // 
            lblISBNLabel.AutoSize = true;
            lblISBNLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblISBNLabel.ForeColor = Color.FromArgb(13, 59, 102);
            lblISBNLabel.Location = new Point(20, 132);
            lblISBNLabel.Name = "lblISBNLabel";
            lblISBNLabel.Size = new Size(223, 20);
            lblISBNLabel.TabIndex = 4;
            lblISBNLabel.Text = "ISBN * (e.g. 978-0132350884)";
            // 
            // lblTitleErr
            // 
            lblTitleErr.Font = new Font("Segoe UI", 8F);
            lblTitleErr.ForeColor = Color.FromArgb(220, 38, 38);
            lblTitleErr.Location = new Point(20, 103);
            lblTitleErr.Name = "lblTitleErr";
            lblTitleErr.Size = new Size(496, 24);
            lblTitleErr.TabIndex = 3;
            lblTitleErr.Visible = false;
            // 
            // txtTitle
            // 
            txtTitle.BorderStyle = BorderStyle.FixedSingle;
            txtTitle.Font = new Font("Segoe UI", 9.5F);
            txtTitle.ForeColor = Color.FromArgb(13, 59, 102);
            txtTitle.Location = new Point(20, 72);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(496, 29);
            txtTitle.TabIndex = 2;
            txtTitle.TextChanged += txtTitle_TextChanged;
            // 
            // lblTitleLabel
            // 
            lblTitleLabel.AutoSize = true;
            lblTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTitleLabel.ForeColor = Color.FromArgb(13, 59, 102);
            lblTitleLabel.Location = new Point(20, 50);
            lblTitleLabel.Name = "lblTitleLabel";
            lblTitleLabel.Size = new Size(91, 20);
            lblTitleLabel.TabIndex = 1;
            lblTitleLabel.Text = "Book Title *";
            // 
            // lblHeader
            // 
            lblHeader.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblHeader.ForeColor = Color.FromArgb(13, 59, 102);
            lblHeader.Location = new Point(20, 14);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(496, 28);
            lblHeader.TabIndex = 0;
            lblHeader.Text = "Add New Book";
            lblHeader.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // BookEditDialog
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(235, 243, 250);
            CancelButton = btnCancel;
            ClientSize = new Size(536, 520);
            Controls.Add(pnlContainer);
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(536, 520);
            Name = "BookEditDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Library Management System — Add Book";
            pnlContainer.ResumeLayout(false);
            pnlContainer.PerformLayout();
            ResumeLayout(false);

        }
    }
}
