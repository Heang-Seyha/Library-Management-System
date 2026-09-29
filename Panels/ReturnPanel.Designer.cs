namespace LibraryManagementSystem.Panels
{
    partial class ReturnPanel
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.Panel pnlCenteredWorkspace;
        private System.Windows.Forms.Panel pnlHeaderContainer;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;

        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnRefresh;

        private System.Windows.Forms.Panel pnlHost;
        private System.Windows.Forms.SplitContainer split;

        private System.Windows.Forms.Panel pnlGridCard;
        private System.Windows.Forms.DataGridView dgvActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMember;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBorrow;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBooks;

        private System.Windows.Forms.Panel pnlDetailsCard;
        private System.Windows.Forms.Label lblDetailTitle;
        private System.Windows.Forms.Label lblCapMember;
        private System.Windows.Forms.Label lblDetailMember;
        private System.Windows.Forms.Label lblCapBorrow;
        private System.Windows.Forms.Label lblDetailBorrow;
        private System.Windows.Forms.Label lblCapDue;
        private System.Windows.Forms.Label lblDetailDue;
        private System.Windows.Forms.Label lblCapStatus;
        private System.Windows.Forms.Label lblDetailStatus;
        private System.Windows.Forms.Label lblCapBooks;
        private System.Windows.Forms.Label lblDetailBooks;
        private System.Windows.Forms.Label lblDetailFine;
        private System.Windows.Forms.Button btnReturn;

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
            tlpRoot = new TableLayoutPanel();
            pnlCenteredWorkspace = new Panel();
            pnlHost = new Panel();
            split = new SplitContainer();
            pnlGridCard = new Panel();
            dgvActive = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colMember = new DataGridViewTextBoxColumn();
            colBorrow = new DataGridViewTextBoxColumn();
            colDue = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            colBooks = new DataGridViewTextBoxColumn();
            pnlDetailsCard = new Panel();
            btnReturn = new Button();
            lblDetailFine = new Label();
            lblDetailBooks = new Label();
            lblCapBooks = new Label();
            lblDetailStatus = new Label();
            lblCapStatus = new Label();
            lblDetailDue = new Label();
            lblCapDue = new Label();
            lblDetailBorrow = new Label();
            lblCapBorrow = new Label();
            lblDetailMember = new Label();
            lblCapMember = new Label();
            lblDetailTitle = new Label();
            pnlToolbar = new Panel();
            btnRefresh = new Button();
            txtSearch = new TextBox();
            pnlHeaderContainer = new Panel();
            pnlHeader = new Panel();
            lblTitle = new Label();
            tlpRoot.SuspendLayout();
            pnlCenteredWorkspace.SuspendLayout();
            pnlHost.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)split).BeginInit();
            split.Panel1.SuspendLayout();
            split.Panel2.SuspendLayout();
            split.SuspendLayout();
            pnlGridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvActive).BeginInit();
            pnlDetailsCard.SuspendLayout();
            pnlToolbar.SuspendLayout();
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
            pnlCenteredWorkspace.Controls.Add(pnlToolbar);
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
            pnlHost.Controls.Add(split);
            pnlHost.Dock = DockStyle.Fill;
            pnlHost.Location = new Point(16, 116);
            pnlHost.Name = "pnlHost";
            pnlHost.Padding = new Padding(0, 8, 0, 0);
            pnlHost.Size = new Size(1002, 593);
            pnlHost.TabIndex = 2;
            // 
            // split
            // 
            split.Dock = DockStyle.Fill;
            split.FixedPanel = FixedPanel.Panel2;
            split.Location = new Point(0, 8);
            split.Name = "split";
            // 
            // split.Panel1
            // 
            split.Panel1.Controls.Add(pnlGridCard);
            // 
            // split.Panel2
            // 
            split.Panel2.Controls.Add(pnlDetailsCard);
            split.Size = new Size(1002, 585);
            split.SplitterDistance = 684;
            split.SplitterWidth = 8;
            split.TabIndex = 0;
            // 
            // pnlGridCard
            // 
            pnlGridCard.BackColor = Color.White;
            pnlGridCard.BorderStyle = BorderStyle.FixedSingle;
            pnlGridCard.Controls.Add(dgvActive);
            pnlGridCard.Dock = DockStyle.Fill;
            pnlGridCard.Location = new Point(0, 0);
            pnlGridCard.Name = "pnlGridCard";
            pnlGridCard.Padding = new Padding(10);
            pnlGridCard.Size = new Size(684, 585);
            pnlGridCard.TabIndex = 0;
            // 
            // dgvActive
            // 
            dgvActive.AllowUserToAddRows = false;
            dgvActive.AllowUserToDeleteRows = false;
            dgvActive.AllowUserToResizeColumns = true;
            dgvActive.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvActive.BackgroundColor = Color.White;
            dgvActive.BorderStyle = BorderStyle.Fixed3D;
            dgvActive.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvActive.ColumnHeadersHeight = 36;
            dgvActive.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvActive.Columns.AddRange(new DataGridViewColumn[] { colId, colMember, colBorrow, colDue, colStatus, colBooks });
            dgvActive.Dock = DockStyle.Fill;
            dgvActive.EnableHeadersVisualStyles = false;
            dgvActive.GridColor = SystemColors.MenuHighlight;
            dgvActive.Location = new Point(10, 10);
            dgvActive.MultiSelect = false;
            dgvActive.Name = "dgvActive";
            dgvActive.ReadOnly = true;
            dgvActive.RowHeadersVisible = false;
            dgvActive.RowHeadersWidth = 51;
            dgvActive.RowTemplate.Height = 32;
            dgvActive.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvActive.Size = new Size(502, 563);
            dgvActive.TabIndex = 1;
            // 
            // colId
            // 
            colId.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colId.HeaderText = "Borrow ID";
            colId.MinimumWidth = 125;
            colId.Name = "Id";
            colId.ReadOnly = true;
            colId.Resizable = DataGridViewTriState.True;
            colId.Width = 135;
            // 
            // colMember
            // 
            colMember.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colMember.FillWeight = 28F;
            colMember.HeaderText = "Member Name";
            colMember.MinimumWidth = 135;
            colMember.Name = "colMember";
            colMember.ReadOnly = true;
            colMember.Resizable = DataGridViewTriState.True;
            // 
            // colBorrow
            // 
            colBorrow.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colBorrow.HeaderText = "Borrow Date";
            colBorrow.MinimumWidth = 150;
            colBorrow.Name = "colBorrow";
            colBorrow.ReadOnly = true;
            colBorrow.Resizable = DataGridViewTriState.True;
            colBorrow.Width = 160;
            // 
            // colDue
            // 
            colDue.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colDue.HeaderText = "Due Date";
            colDue.MinimumWidth = 115;
            colDue.Name = "colDue";
            colDue.ReadOnly = true;
            colDue.Resizable = DataGridViewTriState.True;
            colDue.Width = 125;
            // 
            // colStatus
            // 
            colStatus.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colStatus.HeaderText = "Status";
            colStatus.MinimumWidth = 95;
            colStatus.Name = "colStatus";
            colStatus.ReadOnly = true;
            colStatus.Resizable = DataGridViewTriState.True;
            colStatus.Width = 105;
            // 
            // colBooks
            // 
            colBooks.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colBooks.FillWeight = 72F;
            colBooks.HeaderText = "Books";
            colBooks.MinimumWidth = 220;
            colBooks.Name = "colBooks";
            colBooks.ReadOnly = true;
            colBooks.Resizable = DataGridViewTriState.True;
            // 
            // pnlDetailsCard
            // 
            pnlDetailsCard.BackColor = Color.White;
            pnlDetailsCard.BorderStyle = BorderStyle.FixedSingle;
            pnlDetailsCard.Controls.Add(btnReturn);
            pnlDetailsCard.Controls.Add(lblDetailFine);
            pnlDetailsCard.Controls.Add(lblDetailBooks);
            pnlDetailsCard.Controls.Add(lblCapBooks);
            pnlDetailsCard.Controls.Add(lblDetailStatus);
            pnlDetailsCard.Controls.Add(lblCapStatus);
            pnlDetailsCard.Controls.Add(lblDetailDue);
            pnlDetailsCard.Controls.Add(lblCapDue);
            pnlDetailsCard.Controls.Add(lblDetailBorrow);
            pnlDetailsCard.Controls.Add(lblCapBorrow);
            pnlDetailsCard.Controls.Add(lblDetailMember);
            pnlDetailsCard.Controls.Add(lblCapMember);
            pnlDetailsCard.Controls.Add(lblDetailTitle);
            pnlDetailsCard.Dock = DockStyle.Fill;
            pnlDetailsCard.Location = new Point(0, 0);
            pnlDetailsCard.Name = "pnlDetailsCard";
            pnlDetailsCard.Padding = new Padding(12);
            pnlDetailsCard.Size = new Size(310, 585);
            pnlDetailsCard.TabIndex = 0;
            // 
            // btnReturn
            // 
            btnReturn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnReturn.BackColor = Color.SteelBlue;
            btnReturn.Cursor = Cursors.Hand;
            btnReturn.Enabled = false;
            btnReturn.FlatAppearance.BorderSize = 0;
            btnReturn.FlatStyle = FlatStyle.Flat;
            btnReturn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnReturn.ForeColor = Color.Transparent;
            btnReturn.Location = new Point(12, 533);
            btnReturn.Name = "btnReturn";
            btnReturn.Size = new Size(286, 36);
            btnReturn.TabIndex = 12;
            btnReturn.Text = "Process Return";
            btnReturn.UseVisualStyleBackColor = false;
            // 
            // lblDetailFine
            // 
            lblDetailFine.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblDetailFine.BackColor = Color.FromArgb(254, 242, 242);
            lblDetailFine.BorderStyle = BorderStyle.FixedSingle;
            lblDetailFine.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblDetailFine.ForeColor = Color.FromArgb(192, 0, 0);
            lblDetailFine.Location = new Point(12, 451);
            lblDetailFine.Name = "lblDetailFine";
            lblDetailFine.Padding = new Padding(4);
            lblDetailFine.Size = new Size(286, 74);
            lblDetailFine.TabIndex = 11;
            lblDetailFine.Text = "Fine: $0.00";
            lblDetailFine.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblDetailBooks
            // 
            lblDetailBooks.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblDetailBooks.Font = new Font("Segoe UI", 9.5F);
            lblDetailBooks.ForeColor = Color.FromArgb(13, 59, 102);
            lblDetailBooks.Location = new Point(16, 244);
            lblDetailBooks.Name = "lblDetailBooks";
            lblDetailBooks.Size = new Size(278, 225);
            lblDetailBooks.TabIndex = 10;
            lblDetailBooks.Text = "—";
            // 
            // lblCapBooks
            // 
            lblCapBooks.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblCapBooks.ForeColor = Color.FromArgb(100, 116, 139);
            lblCapBooks.Location = new Point(16, 228);
            lblCapBooks.Name = "lblCapBooks";
            lblCapBooks.Size = new Size(290, 16);
            lblCapBooks.TabIndex = 9;
            lblCapBooks.Text = "Books in this Loan:";
            // 
            // lblDetailStatus
            // 
            lblDetailStatus.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblDetailStatus.ForeColor = Color.FromArgb(13, 59, 102);
            lblDetailStatus.Location = new Point(16, 200);
            lblDetailStatus.Name = "lblDetailStatus";
            lblDetailStatus.Size = new Size(290, 22);
            lblDetailStatus.TabIndex = 8;
            lblDetailStatus.Text = "—";
            // 
            // lblCapStatus
            // 
            lblCapStatus.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblCapStatus.ForeColor = Color.FromArgb(100, 116, 139);
            lblCapStatus.Location = new Point(16, 184);
            lblCapStatus.Name = "lblCapStatus";
            lblCapStatus.Size = new Size(290, 16);
            lblCapStatus.TabIndex = 7;
            lblCapStatus.Text = "Status:";
            // 
            // lblDetailDue
            // 
            lblDetailDue.Font = new Font("Segoe UI", 9.5F);
            lblDetailDue.ForeColor = Color.FromArgb(13, 59, 102);
            lblDetailDue.Location = new Point(16, 156);
            lblDetailDue.Name = "lblDetailDue";
            lblDetailDue.Size = new Size(290, 22);
            lblDetailDue.TabIndex = 6;
            lblDetailDue.Text = "—";
            // 
            // lblCapDue
            // 
            lblCapDue.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblCapDue.ForeColor = Color.FromArgb(100, 116, 139);
            lblCapDue.Location = new Point(16, 140);
            lblCapDue.Name = "lblCapDue";
            lblCapDue.Size = new Size(290, 16);
            lblCapDue.TabIndex = 5;
            lblCapDue.Text = "Due Date:";
            // 
            // lblDetailBorrow
            // 
            lblDetailBorrow.Font = new Font("Segoe UI", 9.5F);
            lblDetailBorrow.ForeColor = Color.FromArgb(13, 59, 102);
            lblDetailBorrow.Location = new Point(16, 112);
            lblDetailBorrow.Name = "lblDetailBorrow";
            lblDetailBorrow.Size = new Size(290, 22);
            lblDetailBorrow.TabIndex = 4;
            lblDetailBorrow.Text = "—";
            // 
            // lblCapBorrow
            // 
            lblCapBorrow.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblCapBorrow.ForeColor = Color.FromArgb(100, 116, 139);
            lblCapBorrow.Location = new Point(16, 96);
            lblCapBorrow.Name = "lblCapBorrow";
            lblCapBorrow.Size = new Size(290, 16);
            lblCapBorrow.TabIndex = 3;
            lblCapBorrow.Text = "Borrow Date:";
            // 
            // lblDetailMember
            // 
            lblDetailMember.Font = new Font("Segoe UI", 9.5F);
            lblDetailMember.ForeColor = Color.FromArgb(13, 59, 102);
            lblDetailMember.Location = new Point(16, 68);
            lblDetailMember.Name = "lblDetailMember";
            lblDetailMember.Size = new Size(290, 22);
            lblDetailMember.TabIndex = 2;
            lblDetailMember.Text = "—";
            // 
            // lblCapMember
            // 
            lblCapMember.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblCapMember.ForeColor = Color.FromArgb(100, 116, 139);
            lblCapMember.Location = new Point(16, 52);
            lblCapMember.Name = "lblCapMember";
            lblCapMember.Size = new Size(290, 16);
            lblCapMember.TabIndex = 1;
            lblCapMember.Text = "Borrower:";
            // 
            // lblDetailTitle
            // 
            lblDetailTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblDetailTitle.ForeColor = Color.FromArgb(13, 59, 102);
            lblDetailTitle.Location = new Point(16, 16);
            lblDetailTitle.Name = "lblDetailTitle";
            lblDetailTitle.Size = new Size(290, 28);
            lblDetailTitle.TabIndex = 0;
            lblDetailTitle.Text = "Borrow Information";
            // 
            // pnlToolbar
            // 
            pnlToolbar.BackColor = Color.White;
            pnlToolbar.BorderStyle = BorderStyle.FixedSingle;
            pnlToolbar.Controls.Add(btnRefresh);
            pnlToolbar.Controls.Add(txtSearch);
            pnlToolbar.Dock = DockStyle.Top;
            pnlToolbar.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            pnlToolbar.Location = new Point(16, 70);
            pnlToolbar.Name = "pnlToolbar";
            pnlToolbar.Padding = new Padding(12, 6, 12, 6);
            pnlToolbar.Size = new Size(1002, 46);
            pnlToolbar.TabIndex = 1;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.SteelBlue;
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.Dock = DockStyle.Right;
            btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(208, 225, 253);
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.Transparent;
            btnRefresh.Location = new Point(893, 6);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(95, 32);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            // 
            // txtSearch
            // 
            txtSearch.BackColor = Color.White;
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Dock = DockStyle.Left;
            txtSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSearch.ForeColor = Color.FromArgb(13, 59, 102);
            txtSearch.Location = new Point(12, 6);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "  Search by member name or borrow ID...";
            txtSearch.Size = new Size(400, 29);
            txtSearch.TabIndex = 0;
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
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(1002, 54);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Return Desk";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ReturnPanel
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(235, 243, 250);
            Controls.Add(tlpRoot);
            Font = new Font("Segoe UI", 9.5F);
            Name = "ReturnPanel";
            Size = new Size(1034, 721);
            tlpRoot.ResumeLayout(false);
            pnlCenteredWorkspace.ResumeLayout(false);
            pnlHost.ResumeLayout(false);
            split.Panel1.ResumeLayout(false);
            split.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)split).EndInit();
            split.ResumeLayout(false);
            pnlGridCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvActive).EndInit();
            pnlDetailsCard.ResumeLayout(false);
            pnlToolbar.ResumeLayout(false);
            pnlToolbar.PerformLayout();
            pnlHeaderContainer.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            ResumeLayout(false);

        }
    }
}
