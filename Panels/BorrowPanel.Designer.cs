namespace LibraryManagementSystem.Panels
{
    partial class BorrowPanel
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.Panel pnlCenteredWorkspace;
        private System.Windows.Forms.Panel pnlHeaderContainer;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;

        private System.Windows.Forms.Panel pnlHost;
        private System.Windows.Forms.TableLayoutPanel tlpTop;

        // Card 1: Member
        private System.Windows.Forms.Panel pnlMemberCard;
        private System.Windows.Forms.Label lblMTitle;
        private System.Windows.Forms.TextBox txtMemberSearch;
        private System.Windows.Forms.ListBox lstMembers;
        private System.Windows.Forms.Label lblSelectedMember;

        // Card 2: Book
        private System.Windows.Forms.Panel pnlBookCard;
        private System.Windows.Forms.Label lblBTitle;
        private System.Windows.Forms.TextBox txtBookSearch;
        private System.Windows.Forms.ListBox lstBooks;
        private System.Windows.Forms.Panel pnlBookControls;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private System.Windows.Forms.Button btnAddBook;

        // Card 3: Summary
        private System.Windows.Forms.Panel pnlSummaryCard;
        private System.Windows.Forms.Label lblSTitle;
        private System.Windows.Forms.Label lblDueLabel;
        private System.Windows.Forms.DateTimePicker dtpDueDate;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnConfirm;

        // Bottom: Selected Books Grid Card
        private System.Windows.Forms.Panel pnlGridCard;
        private System.Windows.Forms.Panel pnlGridHeader;
        private System.Windows.Forms.Label lblCartTitle;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.DataGridView dgvBorrowItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBookId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colISBN;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAvailable;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQty;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            tlpRoot = new TableLayoutPanel();
            pnlCenteredWorkspace = new Panel();
            pnlHost = new Panel();
            pnlGridCard = new Panel();
            dgvBorrowItems = new DataGridView();
            colBookId = new DataGridViewTextBoxColumn();
            colTitle = new DataGridViewTextBoxColumn();
            colISBN = new DataGridViewTextBoxColumn();
            colAvailable = new DataGridViewTextBoxColumn();
            colQty = new DataGridViewTextBoxColumn();
            pnlGridHeader = new Panel();
            btnRemoveItem = new Button();
            lblCartTitle = new Label();
            tlpTop = new TableLayoutPanel();
            pnlSummaryCard = new Panel();
            btnConfirm = new Button();
            lblStatus = new Label();
            dtpDueDate = new DateTimePicker();
            lblDueLabel = new Label();
            lblSTitle = new Label();
            pnlBookCard = new Panel();
            lstBooks = new ListBox();
            pnlBookControls = new Panel();
            btnAddBook = new Button();
            nudQuantity = new NumericUpDown();
            lblQty = new Label();
            txtBookSearch = new TextBox();
            lblBTitle = new Label();
            pnlMemberCard = new Panel();
            lstMembers = new ListBox();
            lblSelectedMember = new Label();
            txtMemberSearch = new TextBox();
            lblMTitle = new Label();
            pnlHeaderContainer = new Panel();
            pnlHeader = new Panel();
            lblTitle = new Label();
            tlpRoot.SuspendLayout();
            pnlCenteredWorkspace.SuspendLayout();
            pnlHost.SuspendLayout();
            pnlGridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBorrowItems).BeginInit();
            pnlGridHeader.SuspendLayout();
            tlpTop.SuspendLayout();
            pnlSummaryCard.SuspendLayout();
            pnlBookCard.SuspendLayout();
            pnlBookControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudQuantity).BeginInit();
            pnlMemberCard.SuspendLayout();
            pnlHeaderContainer.SuspendLayout();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // tlpRoot
            // 
            tlpRoot.BackColor = Color.FromArgb(235, 243, 250);
            tlpRoot.ColumnCount = 1;
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRoot.Controls.Add(pnlCenteredWorkspace, 0, 0);
            tlpRoot.Dock = DockStyle.Fill;
            tlpRoot.Location = new Point(0, 0);
            tlpRoot.Margin = new Padding(0);
            tlpRoot.Name = "tlpRoot";
            tlpRoot.RowCount = 1;
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpRoot.Size = new Size(1034, 721);
            tlpRoot.TabIndex = 0;
            // 
            // pnlCenteredWorkspace
            // 
            pnlCenteredWorkspace.BackColor = Color.Transparent;
            pnlCenteredWorkspace.Controls.Add(pnlHost);
            pnlCenteredWorkspace.Controls.Add(pnlHeaderContainer);
            pnlCenteredWorkspace.Dock = DockStyle.Fill;
            pnlCenteredWorkspace.Location = new Point(0, 0);
            pnlCenteredWorkspace.Margin = new Padding(0);
            pnlCenteredWorkspace.Name = "pnlCenteredWorkspace";
            pnlCenteredWorkspace.Padding = new Padding(16, 8, 16, 12);
            pnlCenteredWorkspace.Size = new Size(1034, 721);
            pnlCenteredWorkspace.TabIndex = 0;
            // 
            // pnlHost
            // 
            pnlHost.BackColor = Color.Transparent;
            pnlHost.Controls.Add(pnlGridCard);
            pnlHost.Controls.Add(tlpTop);
            pnlHost.Dock = DockStyle.Fill;
            pnlHost.Location = new Point(16, 70);
            pnlHost.Name = "pnlHost";
            pnlHost.Padding = new Padding(0, 6, 0, 0);
            pnlHost.Size = new Size(1002, 639);
            pnlHost.TabIndex = 1;
            // 
            // pnlGridCard
            // 
            pnlGridCard.BackColor = Color.White;
            pnlGridCard.BorderStyle = BorderStyle.FixedSingle;
            pnlGridCard.Controls.Add(dgvBorrowItems);
            pnlGridCard.Controls.Add(pnlGridHeader);
            pnlGridCard.Dock = DockStyle.Fill;
            pnlGridCard.Location = new Point(0, 244);
            pnlGridCard.Margin = new Padding(0, 8, 0, 0);
            pnlGridCard.Name = "pnlGridCard";
            pnlGridCard.Padding = new Padding(10);
            pnlGridCard.Size = new Size(1002, 395);
            pnlGridCard.TabIndex = 1;
            // 
            // dgvBorrowItems
            // 
            dgvBorrowItems.AllowUserToAddRows = false;
            dgvBorrowItems.AllowUserToDeleteRows = false;
            dgvBorrowItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBorrowItems.BackgroundColor = Color.White;
            dgvBorrowItems.BorderStyle = BorderStyle.Fixed3D;
            dgvBorrowItems.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvBorrowItems.ColumnHeadersHeight = 36;
            dgvBorrowItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvBorrowItems.Columns.AddRange(new DataGridViewColumn[] { colBookId, colTitle, colISBN, colAvailable, colQty });
            dgvBorrowItems.Dock = DockStyle.Fill;
            dgvBorrowItems.EnableHeadersVisualStyles = false;
            dgvBorrowItems.GridColor = SystemColors.MenuHighlight;
            dgvBorrowItems.Location = new Point(10, 44);
            dgvBorrowItems.MultiSelect = false;
            dgvBorrowItems.Name = "dgvBorrowItems";
            dgvBorrowItems.ReadOnly = true;
            dgvBorrowItems.RowHeadersVisible = false;
            dgvBorrowItems.RowHeadersWidth = 51;
            dgvBorrowItems.RowTemplate.Height = 32;
            dgvBorrowItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBorrowItems.Size = new Size(980, 339);
            dgvBorrowItems.TabIndex = 1;
            // 
            // colBookId
            // 
            colBookId.FillWeight = 8F;
            colBookId.HeaderText = "ID";
            colBookId.MinimumWidth = 60;
            colBookId.Name = "colBookId";
            colBookId.ReadOnly = true;
            // 
            // colTitle
            // 
            colTitle.FillWeight = 50F;
            colTitle.HeaderText = "Book Title";
            colTitle.MinimumWidth = 160;
            colTitle.Name = "colTitle";
            colTitle.ReadOnly = true;
            // 
            // colISBN
            // 
            colISBN.FillWeight = 22F;
            colISBN.HeaderText = "ISBN";
            colISBN.MinimumWidth = 130;
            colISBN.Name = "colISBN";
            colISBN.ReadOnly = true;
            // 
            // colAvailable
            // 
            colAvailable.FillWeight = 10F;
            colAvailable.HeaderText = "Available";
            colAvailable.MinimumWidth = 80;
            colAvailable.Name = "colAvailable";
            colAvailable.ReadOnly = true;
            // 
            // colQty
            // 
            colQty.FillWeight = 10F;
            colQty.HeaderText = "Qty to Borrow";
            colQty.MinimumWidth = 80;
            colQty.Name = "colQty";
            colQty.ReadOnly = true;
            // 
            // pnlGridHeader
            // 
            pnlGridHeader.Controls.Add(btnRemoveItem);
            pnlGridHeader.Controls.Add(lblCartTitle);
            pnlGridHeader.Dock = DockStyle.Top;
            pnlGridHeader.Location = new Point(10, 10);
            pnlGridHeader.Name = "pnlGridHeader";
            pnlGridHeader.Size = new Size(980, 34);
            pnlGridHeader.TabIndex = 0;
            // 
            // btnRemoveItem
            // 
            btnRemoveItem.BackColor = Color.SteelBlue;
            btnRemoveItem.Cursor = Cursors.Hand;
            btnRemoveItem.Dock = DockStyle.Right;
            btnRemoveItem.FlatAppearance.BorderSize = 0;
            btnRemoveItem.FlatStyle = FlatStyle.Flat;
            btnRemoveItem.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRemoveItem.ForeColor = Color.Transparent;
            btnRemoveItem.Location = new Point(840, 0);
            btnRemoveItem.Name = "btnRemoveItem";
            btnRemoveItem.Size = new Size(140, 34);
            btnRemoveItem.TabIndex = 1;
            btnRemoveItem.Text = "Remove Selected";
            btnRemoveItem.UseVisualStyleBackColor = false;
            // 
            // lblCartTitle
            // 
            lblCartTitle.AutoSize = true;
            lblCartTitle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblCartTitle.ForeColor = Color.FromArgb(13, 59, 102);
            lblCartTitle.Location = new Point(3, 0);
            lblCartTitle.Name = "lblCartTitle";
            lblCartTitle.Size = new Size(256, 25);
            lblCartTitle.TabIndex = 0;
            lblCartTitle.Text = "Selected Books for Checkout";
            // 
            // tlpTop
            // 
            tlpTop.ColumnCount = 3;
            tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36.67F));
            tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tlpTop.Controls.Add(pnlSummaryCard, 2, 0);
            tlpTop.Controls.Add(pnlBookCard, 1, 0);
            tlpTop.Controls.Add(pnlMemberCard, 0, 0);
            tlpTop.Dock = DockStyle.Top;
            tlpTop.Location = new Point(0, 6);
            tlpTop.Name = "tlpTop";
            tlpTop.RowCount = 1;
            tlpTop.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpTop.Size = new Size(1002, 238);
            tlpTop.TabIndex = 0;
            // 
            // pnlSummaryCard
            // 
            pnlSummaryCard.BackColor = Color.White;
            pnlSummaryCard.BorderStyle = BorderStyle.FixedSingle;
            pnlSummaryCard.Controls.Add(btnConfirm);
            pnlSummaryCard.Controls.Add(lblStatus);
            pnlSummaryCard.Controls.Add(dtpDueDate);
            pnlSummaryCard.Controls.Add(lblDueLabel);
            pnlSummaryCard.Controls.Add(lblSTitle);
            pnlSummaryCard.Dock = DockStyle.Fill;
            pnlSummaryCard.Location = new Point(706, 3);
            pnlSummaryCard.Margin = new Padding(6, 3, 3, 3);
            pnlSummaryCard.Name = "pnlSummaryCard";
            pnlSummaryCard.Padding = new Padding(10);
            pnlSummaryCard.Size = new Size(293, 232);
            pnlSummaryCard.TabIndex = 2;
            // 
            // btnConfirm
            // 
            btnConfirm.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnConfirm.BackColor = Color.SteelBlue;
            btnConfirm.Cursor = Cursors.Hand;
            btnConfirm.FlatAppearance.BorderSize = 0;
            btnConfirm.FlatStyle = FlatStyle.Flat;
            btnConfirm.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnConfirm.ForeColor = Color.Transparent;
            btnConfirm.Location = new Point(10, 186);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(271, 34);
            btnConfirm.TabIndex = 4;
            btnConfirm.Text = "Confirm Loan Checkout";
            btnConfirm.UseVisualStyleBackColor = false;
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblStatus.ForeColor = Color.FromArgb(100, 116, 139);
            lblStatus.Location = new Point(10, 88);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(271, 92);
            lblStatus.TabIndex = 3;
            lblStatus.Text = "Ready to issue loan...";
            // 
            // dtpDueDate
            // 
            dtpDueDate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dtpDueDate.Font = new Font("Segoe UI", 9.5F);
            dtpDueDate.Format = DateTimePickerFormat.Short;
            dtpDueDate.Location = new Point(10, 56);
            dtpDueDate.Name = "dtpDueDate";
            dtpDueDate.Size = new Size(271, 29);
            dtpDueDate.TabIndex = 2;
            // 
            // lblDueLabel
            // 
            lblDueLabel.AutoSize = true;
            lblDueLabel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDueLabel.ForeColor = Color.FromArgb(13, 59, 102);
            lblDueLabel.Location = new Point(55, 35);
            lblDueLabel.Name = "lblDueLabel";
            lblDueLabel.Size = new Size(195, 20);
            lblDueLabel.TabIndex = 1;
            lblDueLabel.Text = "Due Date (Default 14 days):";
            // 
            // lblSTitle
            // 
            lblSTitle.Dock = DockStyle.Top;
            lblSTitle.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            lblSTitle.ForeColor = Color.FromArgb(13, 59, 102);
            lblSTitle.Location = new Point(10, 10);
            lblSTitle.Name = "lblSTitle";
            lblSTitle.Size = new Size(271, 24);
            lblSTitle.TabIndex = 0;
            lblSTitle.Text = "3. Transaction Summary";
            lblSTitle.TextAlign = ContentAlignment.TopCenter;
            lblSTitle.Click += lblSTitle_Click;
            // 
            // pnlBookCard
            // 
            pnlBookCard.BackColor = Color.White;
            pnlBookCard.BorderStyle = BorderStyle.FixedSingle;
            pnlBookCard.Controls.Add(lstBooks);
            pnlBookCard.Controls.Add(pnlBookControls);
            pnlBookCard.Controls.Add(txtBookSearch);
            pnlBookCard.Controls.Add(lblBTitle);
            pnlBookCard.Dock = DockStyle.Fill;
            pnlBookCard.Location = new Point(339, 3);
            pnlBookCard.Margin = new Padding(6, 3, 6, 3);
            pnlBookCard.Name = "pnlBookCard";
            pnlBookCard.Padding = new Padding(10);
            pnlBookCard.Size = new Size(355, 232);
            pnlBookCard.TabIndex = 1;
            // 
            // lstBooks
            // 
            lstBooks.BorderStyle = BorderStyle.FixedSingle;
            lstBooks.Dock = DockStyle.Fill;
            lstBooks.Font = new Font("Segoe UI", 9F);
            lstBooks.ForeColor = Color.FromArgb(13, 59, 102);
            lstBooks.FormattingEnabled = true;
            lstBooks.Location = new Point(10, 71);
            lstBooks.Name = "lstBooks";
            lstBooks.Size = new Size(333, 115);
            lstBooks.TabIndex = 2;
            // 
            // pnlBookControls
            // 
            pnlBookControls.Controls.Add(btnAddBook);
            pnlBookControls.Controls.Add(nudQuantity);
            pnlBookControls.Controls.Add(lblQty);
            pnlBookControls.Dock = DockStyle.Bottom;
            pnlBookControls.Location = new Point(10, 186);
            pnlBookControls.Name = "pnlBookControls";
            pnlBookControls.Size = new Size(333, 34);
            pnlBookControls.TabIndex = 3;
            // 
            // btnAddBook
            // 
            btnAddBook.BackColor = Color.SteelBlue;
            btnAddBook.Cursor = Cursors.Hand;
            btnAddBook.FlatAppearance.BorderColor = Color.FromArgb(208, 225, 253);
            btnAddBook.FlatStyle = FlatStyle.Flat;
            btnAddBook.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnAddBook.ForeColor = Color.Transparent;
            btnAddBook.Location = new Point(100, 2);
            btnAddBook.Name = "btnAddBook";
            btnAddBook.Size = new Size(95, 30);
            btnAddBook.TabIndex = 2;
            btnAddBook.Text = "Add";
            btnAddBook.UseVisualStyleBackColor = false;
            // 
            // nudQuantity
            // 
            nudQuantity.Font = new Font("Segoe UI", 9.5F);
            nudQuantity.Location = new Point(36, 4);
            nudQuantity.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            nudQuantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudQuantity.Name = "nudQuantity";
            nudQuantity.Size = new Size(56, 29);
            nudQuantity.TabIndex = 1;
            nudQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblQty
            // 
            lblQty.AutoSize = true;
            lblQty.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblQty.ForeColor = Color.FromArgb(13, 59, 102);
            lblQty.Location = new Point(2, 8);
            lblQty.Name = "lblQty";
            lblQty.Size = new Size(38, 20);
            lblQty.TabIndex = 0;
            lblQty.Text = "Qty:";
            // 
            // txtBookSearch
            // 
            txtBookSearch.BorderStyle = BorderStyle.FixedSingle;
            txtBookSearch.Dock = DockStyle.Top;
            txtBookSearch.Font = new Font("Segoe UI", 9.5F);
            txtBookSearch.ForeColor = Color.FromArgb(13, 59, 102);
            txtBookSearch.Location = new Point(10, 42);
            txtBookSearch.Name = "txtBookSearch";
            txtBookSearch.PlaceholderText = "  Search book title or ISBN...";
            txtBookSearch.Size = new Size(333, 29);
            txtBookSearch.TabIndex = 1;
            // 
            // lblBTitle
            // 
            lblBTitle.Dock = DockStyle.Top;
            lblBTitle.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            lblBTitle.ForeColor = Color.FromArgb(13, 59, 102);
            lblBTitle.Location = new Point(10, 10);
            lblBTitle.Name = "lblBTitle";
            lblBTitle.Size = new Size(333, 32);
            lblBTitle.TabIndex = 0;
            lblBTitle.Text = "2. Select Book";
            lblBTitle.TextAlign = ContentAlignment.TopCenter;
            lblBTitle.Click += lblBTitle_Click;
            // 
            // pnlMemberCard
            // 
            pnlMemberCard.BackColor = Color.White;
            pnlMemberCard.BorderStyle = BorderStyle.FixedSingle;
            pnlMemberCard.Controls.Add(lstMembers);
            pnlMemberCard.Controls.Add(lblSelectedMember);
            pnlMemberCard.Controls.Add(txtMemberSearch);
            pnlMemberCard.Controls.Add(lblMTitle);
            pnlMemberCard.Dock = DockStyle.Fill;
            pnlMemberCard.Location = new Point(3, 3);
            pnlMemberCard.Margin = new Padding(3, 3, 6, 3);
            pnlMemberCard.Name = "pnlMemberCard";
            pnlMemberCard.Padding = new Padding(10);
            pnlMemberCard.Size = new Size(324, 232);
            pnlMemberCard.TabIndex = 0;
            // 
            // lstMembers
            // 
            lstMembers.BorderStyle = BorderStyle.FixedSingle;
            lstMembers.Dock = DockStyle.Fill;
            lstMembers.Font = new Font("Segoe UI", 9F);
            lstMembers.ForeColor = Color.FromArgb(13, 59, 102);
            lstMembers.FormattingEnabled = true;
            lstMembers.Location = new Point(10, 71);
            lstMembers.Name = "lstMembers";
            lstMembers.Size = new Size(302, 125);
            lstMembers.TabIndex = 2;
            // 
            // lblSelectedMember
            // 
            lblSelectedMember.AutoEllipsis = true;
            lblSelectedMember.Dock = DockStyle.Bottom;
            lblSelectedMember.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSelectedMember.ForeColor = Color.FromArgb(13, 59, 102);
            lblSelectedMember.Location = new Point(10, 196);
            lblSelectedMember.Name = "lblSelectedMember";
            lblSelectedMember.Size = new Size(302, 24);
            lblSelectedMember.TabIndex = 3;
            lblSelectedMember.Text = "No member selected";
            lblSelectedMember.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtMemberSearch
            // 
            txtMemberSearch.BorderStyle = BorderStyle.FixedSingle;
            txtMemberSearch.Dock = DockStyle.Top;
            txtMemberSearch.Font = new Font("Segoe UI", 9.5F);
            txtMemberSearch.ForeColor = Color.FromArgb(13, 59, 102);
            txtMemberSearch.Location = new Point(10, 42);
            txtMemberSearch.Name = "txtMemberSearch";
            txtMemberSearch.PlaceholderText = "  Search member name or phone...";
            txtMemberSearch.Size = new Size(302, 29);
            txtMemberSearch.TabIndex = 1;
            // 
            // lblMTitle
            // 
            lblMTitle.Dock = DockStyle.Top;
            lblMTitle.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            lblMTitle.ForeColor = Color.FromArgb(13, 59, 102);
            lblMTitle.Location = new Point(10, 10);
            lblMTitle.Name = "lblMTitle";
            lblMTitle.Size = new Size(302, 32);
            lblMTitle.TabIndex = 0;
            lblMTitle.Text = "1. Select Member";
            lblMTitle.TextAlign = ContentAlignment.TopCenter;
            lblMTitle.Click += lblMTitle_Click;
            // 
            // pnlHeaderContainer
            // 
            pnlHeaderContainer.BackColor = Color.Transparent;
            pnlHeaderContainer.Controls.Add(pnlHeader);
            pnlHeaderContainer.Dock = DockStyle.Top;
            pnlHeaderContainer.Location = new Point(16, 8);
            pnlHeaderContainer.Name = "pnlHeaderContainer";
            pnlHeaderContainer.Padding = new Padding(0, 0, 0, 8);
            pnlHeaderContainer.Size = new Size(1002, 62);
            pnlHeaderContainer.TabIndex = 0;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.SteelBlue;
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Fill;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1002, 54);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = false;
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(1002, 54);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Borrow Desk";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // BorrowPanel
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(235, 243, 250);
            Controls.Add(tlpRoot);
            Font = new Font("Segoe UI", 9.5F);
            Name = "BorrowPanel";
            Size = new Size(1034, 721);
            tlpRoot.ResumeLayout(false);
            pnlCenteredWorkspace.ResumeLayout(false);
            pnlHost.ResumeLayout(false);
            pnlGridCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvBorrowItems).EndInit();
            pnlGridHeader.ResumeLayout(false);
            pnlGridHeader.PerformLayout();
            tlpTop.ResumeLayout(false);
            pnlSummaryCard.ResumeLayout(false);
            pnlSummaryCard.PerformLayout();
            pnlBookCard.ResumeLayout(false);
            pnlBookCard.PerformLayout();
            pnlBookControls.ResumeLayout(false);
            pnlBookControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudQuantity).EndInit();
            pnlMemberCard.ResumeLayout(false);
            pnlMemberCard.PerformLayout();
            pnlHeaderContainer.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ResumeLayout(false);

        }
    }
}
