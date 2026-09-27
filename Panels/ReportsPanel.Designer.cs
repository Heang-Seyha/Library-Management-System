namespace LibraryManagementSystem.Panels
{
    partial class ReportsPanel
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.Panel pnlCenteredWorkspace;
        private System.Windows.Forms.Panel pnlHeaderContainer;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;

        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.FlowLayoutPanel flpMetrics;
        private System.Windows.Forms.Label lblTotalFine;
        private System.Windows.Forms.Label lblOverdueCount;
        private System.Windows.Forms.Label lblBorrowTotal;
        private System.Windows.Forms.Button btnRefresh;

        private System.Windows.Forms.Panel pnlTabsContainer;
        private System.Windows.Forms.TabControl tabReports;
        private System.Windows.Forms.TabPage tabActive;
        private System.Windows.Forms.DataGridView dgvActive;
        private System.Windows.Forms.TabPage tabOverdue;
        private System.Windows.Forms.DataGridView dgvOverdue;
        private System.Windows.Forms.TabPage tabReturned;
        private System.Windows.Forms.DataGridView dgvReturned;
        private System.Windows.Forms.TabPage tabFines;
        private System.Windows.Forms.DataGridView dgvFines;
        private System.Windows.Forms.TabPage tabPopular;
        private System.Windows.Forms.DataGridView dgvPopular;
        private System.Windows.Forms.TabPage tabMembers;
        private System.Windows.Forms.DataGridView dgvMembers;
        private System.Windows.Forms.TabPage tabInventory;
        private System.Windows.Forms.DataGridView dgvInventory;

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
            pnlTabsContainer = new Panel();
            tabReports = new TabControl();
            tabActive = new TabPage();
            dgvActive = new DataGridView();
            tabOverdue = new TabPage();
            dgvOverdue = new DataGridView();
            tabReturned = new TabPage();
            dgvReturned = new DataGridView();
            tabFines = new TabPage();
            dgvFines = new DataGridView();
            tabPopular = new TabPage();
            dgvPopular = new DataGridView();
            tabMembers = new TabPage();
            dgvMembers = new DataGridView();
            tabInventory = new TabPage();
            dgvInventory = new DataGridView();
            pnlSummary = new Panel();
            flpMetrics = new FlowLayoutPanel();
            lblTotalFine = new Label();
            lblOverdueCount = new Label();
            lblBorrowTotal = new Label();
            btnRefresh = new Button();
            pnlHeaderContainer = new Panel();
            pnlHeader = new Panel();
            lblTitle = new Label();
            tlpRoot.SuspendLayout();
            pnlCenteredWorkspace.SuspendLayout();
            pnlTabsContainer.SuspendLayout();
            tabReports.SuspendLayout();
            tabActive.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvActive).BeginInit();
            tabOverdue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOverdue).BeginInit();
            tabReturned.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReturned).BeginInit();
            tabFines.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFines).BeginInit();
            tabPopular.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPopular).BeginInit();
            tabMembers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMembers).BeginInit();
            tabInventory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvInventory).BeginInit();
            pnlSummary.SuspendLayout();
            flpMetrics.SuspendLayout();
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
            pnlCenteredWorkspace.Controls.Add(pnlTabsContainer);
            pnlCenteredWorkspace.Controls.Add(pnlSummary);
            pnlCenteredWorkspace.Controls.Add(pnlHeaderContainer);
            pnlCenteredWorkspace.Dock = DockStyle.Fill;
            pnlCenteredWorkspace.Location = new Point(0, 0);
            pnlCenteredWorkspace.Margin = new Padding(0);
            pnlCenteredWorkspace.Name = "pnlCenteredWorkspace";
            pnlCenteredWorkspace.Padding = new Padding(16, 8, 16, 12);
            pnlCenteredWorkspace.Size = new Size(1034, 721);
            pnlCenteredWorkspace.TabIndex = 0;
            // 
            // pnlTabsContainer
            // 
            pnlTabsContainer.BackColor = Color.Transparent;
            pnlTabsContainer.Controls.Add(tabReports);
            pnlTabsContainer.Dock = DockStyle.Fill;
            pnlTabsContainer.Location = new Point(16, 114);
            pnlTabsContainer.Name = "pnlTabsContainer";
            pnlTabsContainer.Padding = new Padding(0, 8, 0, 0);
            pnlTabsContainer.Size = new Size(1002, 595);
            pnlTabsContainer.TabIndex = 2;
            // 
            // tabReports
            // 
            tabReports.Controls.Add(tabActive);
            tabReports.Controls.Add(tabOverdue);
            tabReports.Controls.Add(tabReturned);
            tabReports.Controls.Add(tabFines);
            tabReports.Controls.Add(tabPopular);
            tabReports.Controls.Add(tabMembers);
            tabReports.Controls.Add(tabInventory);
            tabReports.Dock = DockStyle.Fill;
            tabReports.Font = new Font("Segoe UI", 9.5F);
            tabReports.Location = new Point(0, 8);
            tabReports.Name = "tabReports";
            tabReports.SelectedIndex = 0;
            tabReports.Size = new Size(1002, 587);
            tabReports.TabIndex = 0;
            // 
            // tabActive
            // 
            tabActive.BackColor = Color.White;
            tabActive.Controls.Add(dgvActive);
            tabActive.Location = new Point(4, 30);
            tabActive.Name = "tabActive";
            tabActive.Padding = new Padding(8);
            tabActive.Size = new Size(994, 553);
            tabActive.TabIndex = 0;
            tabActive.Text = "Active Borrows";
            // 
            // dgvActive
            // 
            dgvActive.AllowUserToAddRows = false;
            dgvActive.AllowUserToDeleteRows = false;
            dgvActive.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvActive.BackgroundColor = Color.White;
            dgvActive.BorderStyle = BorderStyle.Fixed3D;
            dgvActive.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvActive.ColumnHeadersHeight = 36;
            dgvActive.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvActive.Dock = DockStyle.Fill;
            dgvActive.EnableHeadersVisualStyles = false;
            dgvActive.GridColor = SystemColors.MenuHighlight;
            dgvActive.Location = new Point(8, 8);
            dgvActive.MultiSelect = false;
            dgvActive.Name = "dgvActive";
            dgvActive.ReadOnly = true;
            dgvActive.RowHeadersVisible = false;
            dgvActive.RowHeadersWidth = 51;
            dgvActive.RowTemplate.Height = 32;
            dgvActive.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvActive.Size = new Size(978, 537);
            dgvActive.TabIndex = 0;
            // 
            // tabOverdue
            // 
            tabOverdue.BackColor = Color.White;
            tabOverdue.Controls.Add(dgvOverdue);
            tabOverdue.Location = new Point(4, 30);
            tabOverdue.Name = "tabOverdue";
            tabOverdue.Padding = new Padding(8);
            tabOverdue.Size = new Size(994, 553);
            tabOverdue.TabIndex = 1;
            tabOverdue.Text = "Overdue Borrows";
            // 
            // dgvOverdue
            // 
            dgvOverdue.AllowUserToAddRows = false;
            dgvOverdue.AllowUserToDeleteRows = false;
            dgvOverdue.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOverdue.BackgroundColor = Color.White;
            dgvOverdue.BorderStyle = BorderStyle.Fixed3D;
            dgvOverdue.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvOverdue.ColumnHeadersHeight = 36;
            dgvOverdue.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvOverdue.Dock = DockStyle.Fill;
            dgvOverdue.EnableHeadersVisualStyles = false;
            dgvOverdue.GridColor = SystemColors.MenuHighlight;
            dgvOverdue.Location = new Point(8, 8);
            dgvOverdue.MultiSelect = false;
            dgvOverdue.Name = "dgvOverdue";
            dgvOverdue.ReadOnly = true;
            dgvOverdue.RowHeadersVisible = false;
            dgvOverdue.RowHeadersWidth = 51;
            dgvOverdue.RowTemplate.Height = 32;
            dgvOverdue.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOverdue.Size = new Size(978, 537);
            dgvOverdue.TabIndex = 0;
            // 
            // tabReturned
            // 
            tabReturned.BackColor = Color.White;
            tabReturned.Controls.Add(dgvReturned);
            tabReturned.Location = new Point(4, 30);
            tabReturned.Name = "tabReturned";
            tabReturned.Padding = new Padding(8);
            tabReturned.Size = new Size(994, 553);
            tabReturned.TabIndex = 2;
            tabReturned.Text = "Return History";
            // 
            // dgvReturned
            // 
            dgvReturned.AllowUserToAddRows = false;
            dgvReturned.AllowUserToDeleteRows = false;
            dgvReturned.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReturned.BackgroundColor = Color.White;
            dgvReturned.BorderStyle = BorderStyle.Fixed3D;
            dgvReturned.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvReturned.ColumnHeadersHeight = 36;
            dgvReturned.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvReturned.Dock = DockStyle.Fill;
            dgvReturned.EnableHeadersVisualStyles = false;
            dgvReturned.GridColor = SystemColors.MenuHighlight;
            dgvReturned.Location = new Point(8, 8);
            dgvReturned.MultiSelect = false;
            dgvReturned.Name = "dgvReturned";
            dgvReturned.ReadOnly = true;
            dgvReturned.RowHeadersVisible = false;
            dgvReturned.RowHeadersWidth = 51;
            dgvReturned.RowTemplate.Height = 32;
            dgvReturned.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReturned.Size = new Size(978, 537);
            dgvReturned.TabIndex = 0;
            // 
            // tabFines
            // 
            tabFines.BackColor = Color.White;
            tabFines.Controls.Add(dgvFines);
            tabFines.Location = new Point(4, 30);
            tabFines.Name = "tabFines";
            tabFines.Padding = new Padding(8);
            tabFines.Size = new Size(994, 553);
            tabFines.TabIndex = 3;
            tabFines.Text = "Fines Analysis";
            // 
            // dgvFines
            // 
            dgvFines.AllowUserToAddRows = false;
            dgvFines.AllowUserToDeleteRows = false;
            dgvFines.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvFines.BackgroundColor = Color.White;
            dgvFines.BorderStyle = BorderStyle.Fixed3D;
            dgvFines.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvFines.ColumnHeadersHeight = 36;
            dgvFines.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvFines.Dock = DockStyle.Fill;
            dgvFines.EnableHeadersVisualStyles = false;
            dgvFines.GridColor = SystemColors.MenuHighlight;
            dgvFines.Location = new Point(8, 8);
            dgvFines.MultiSelect = false;
            dgvFines.Name = "dgvFines";
            dgvFines.ReadOnly = true;
            dgvFines.RowHeadersVisible = false;
            dgvFines.RowHeadersWidth = 51;
            dgvFines.RowTemplate.Height = 32;
            dgvFines.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFines.Size = new Size(978, 537);
            dgvFines.TabIndex = 0;
            // 
            // tabPopular
            // 
            tabPopular.BackColor = Color.White;
            tabPopular.Controls.Add(dgvPopular);
            tabPopular.Location = new Point(4, 30);
            tabPopular.Name = "tabPopular";
            tabPopular.Padding = new Padding(8);
            tabPopular.Size = new Size(994, 553);
            tabPopular.TabIndex = 4;
            tabPopular.Text = "Popular Books";
            // 
            // dgvPopular
            // 
            dgvPopular.AllowUserToAddRows = false;
            dgvPopular.AllowUserToDeleteRows = false;
            dgvPopular.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPopular.BackgroundColor = Color.White;
            dgvPopular.BorderStyle = BorderStyle.Fixed3D;
            dgvPopular.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvPopular.ColumnHeadersHeight = 36;
            dgvPopular.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvPopular.Dock = DockStyle.Fill;
            dgvPopular.EnableHeadersVisualStyles = false;
            dgvPopular.GridColor = SystemColors.MenuHighlight;
            dgvPopular.Location = new Point(8, 8);
            dgvPopular.MultiSelect = false;
            dgvPopular.Name = "dgvPopular";
            dgvPopular.ReadOnly = true;
            dgvPopular.RowHeadersVisible = false;
            dgvPopular.RowHeadersWidth = 51;
            dgvPopular.RowTemplate.Height = 32;
            dgvPopular.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPopular.Size = new Size(978, 537);
            dgvPopular.TabIndex = 0;
            // 
            // tabMembers
            // 
            tabMembers.BackColor = Color.White;
            tabMembers.Controls.Add(dgvMembers);
            tabMembers.Location = new Point(4, 30);
            tabMembers.Name = "tabMembers";
            tabMembers.Padding = new Padding(8);
            tabMembers.Size = new Size(994, 553);
            tabMembers.TabIndex = 5;
            tabMembers.Text = "Member Activity";
            // 
            // dgvMembers
            // 
            dgvMembers.AllowUserToAddRows = false;
            dgvMembers.AllowUserToDeleteRows = false;
            dgvMembers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMembers.BackgroundColor = Color.White;
            dgvMembers.BorderStyle = BorderStyle.Fixed3D;
            dgvMembers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvMembers.ColumnHeadersHeight = 36;
            dgvMembers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvMembers.Dock = DockStyle.Fill;
            dgvMembers.EnableHeadersVisualStyles = false;
            dgvMembers.GridColor = SystemColors.MenuHighlight;
            dgvMembers.Location = new Point(8, 8);
            dgvMembers.MultiSelect = false;
            dgvMembers.Name = "dgvMembers";
            dgvMembers.ReadOnly = true;
            dgvMembers.RowHeadersVisible = false;
            dgvMembers.RowHeadersWidth = 51;
            dgvMembers.RowTemplate.Height = 32;
            dgvMembers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMembers.Size = new Size(978, 537);
            dgvMembers.TabIndex = 0;
            // 
            // tabInventory
            // 
            tabInventory.BackColor = Color.White;
            tabInventory.Controls.Add(dgvInventory);
            tabInventory.Location = new Point(4, 30);
            tabInventory.Name = "tabInventory";
            tabInventory.Padding = new Padding(8);
            tabInventory.Size = new Size(994, 553);
            tabInventory.TabIndex = 6;
            tabInventory.Text = "Stock Status";
            // 
            // dgvInventory
            // 
            dgvInventory.AllowUserToAddRows = false;
            dgvInventory.AllowUserToDeleteRows = false;
            dgvInventory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvInventory.BackgroundColor = Color.White;
            dgvInventory.BorderStyle = BorderStyle.Fixed3D;
            dgvInventory.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvInventory.ColumnHeadersHeight = 36;
            dgvInventory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvInventory.Dock = DockStyle.Fill;
            dgvInventory.EnableHeadersVisualStyles = false;
            dgvInventory.GridColor = SystemColors.MenuHighlight;
            dgvInventory.Location = new Point(8, 8);
            dgvInventory.MultiSelect = false;
            dgvInventory.Name = "dgvInventory";
            dgvInventory.ReadOnly = true;
            dgvInventory.RowHeadersVisible = false;
            dgvInventory.RowHeadersWidth = 51;
            dgvInventory.RowTemplate.Height = 32;
            dgvInventory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInventory.Size = new Size(978, 537);
            dgvInventory.TabIndex = 0;
            // 
            // pnlSummary
            // 
            pnlSummary.BackColor = Color.White;
            pnlSummary.BorderStyle = BorderStyle.FixedSingle;
            pnlSummary.Controls.Add(flpMetrics);
            pnlSummary.Controls.Add(btnRefresh);
            pnlSummary.Dock = DockStyle.Top;
            pnlSummary.Location = new Point(16, 70);
            pnlSummary.Name = "pnlSummary";
            pnlSummary.Padding = new Padding(12, 6, 12, 6);
            pnlSummary.Size = new Size(1002, 44);
            pnlSummary.TabIndex = 1;
            // 
            // flpMetrics
            // 
            flpMetrics.Controls.Add(lblTotalFine);
            flpMetrics.Controls.Add(lblOverdueCount);
            flpMetrics.Controls.Add(lblBorrowTotal);
            flpMetrics.Dock = DockStyle.Fill;
            flpMetrics.Location = new Point(12, 6);
            flpMetrics.Name = "flpMetrics";
            flpMetrics.Padding = new Padding(0, 4, 0, 0);
            flpMetrics.Size = new Size(861, 30);
            flpMetrics.TabIndex = 0;
            flpMetrics.WrapContents = false;
            // 
            // lblTotalFine
            // 
            lblTotalFine.AutoSize = true;
            lblTotalFine.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotalFine.ForeColor = Color.FromArgb(13, 59, 102);
            lblTotalFine.Location = new Point(0, 4);
            lblTotalFine.Margin = new Padding(0, 0, 20, 0);
            lblTotalFine.Name = "lblTotalFine";
            lblTotalFine.Size = new Size(172, 20);
            lblTotalFine.TabIndex = 0;
            lblTotalFine.Text = "Total Fines Collected: ...";
            // 
            // lblOverdueCount
            // 
            lblOverdueCount.AutoSize = true;
            lblOverdueCount.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblOverdueCount.ForeColor = Color.FromArgb(13, 59, 102);
            lblOverdueCount.Location = new Point(192, 4);
            lblOverdueCount.Margin = new Padding(0, 0, 20, 0);
            lblOverdueCount.Name = "lblOverdueCount";
            lblOverdueCount.Size = new Size(151, 20);
            lblOverdueCount.TabIndex = 1;
            lblOverdueCount.Text = "Overdue Borrows: ...";
            // 
            // lblBorrowTotal
            // 
            lblBorrowTotal.AutoSize = true;
            lblBorrowTotal.BackColor = Color.Transparent;
            lblBorrowTotal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblBorrowTotal.ForeColor = Color.FromArgb(13, 59, 102);
            lblBorrowTotal.Location = new Point(363, 4);
            lblBorrowTotal.Margin = new Padding(0, 0, 20, 0);
            lblBorrowTotal.Name = "lblBorrowTotal";
            lblBorrowTotal.Size = new Size(127, 20);
            lblBorrowTotal.TabIndex = 2;
            lblBorrowTotal.Text = "Total Borrows: ...";
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.SteelBlue;
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.Dock = DockStyle.Right;
            btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(208, 225, 253);
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(873, 6);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(115, 30);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "Refresh All";
            btnRefresh.UseVisualStyleBackColor = false;
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
            lblTitle.Text = "Reports & Analytics";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ReportsPanel
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(235, 243, 250);
            Controls.Add(tlpRoot);
            Font = new Font("Segoe UI", 9.5F);
            Name = "ReportsPanel";
            Size = new Size(1034, 721);
            tlpRoot.ResumeLayout(false);
            pnlCenteredWorkspace.ResumeLayout(false);
            pnlTabsContainer.ResumeLayout(false);
            tabReports.ResumeLayout(false);
            tabActive.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvActive).EndInit();
            tabOverdue.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvOverdue).EndInit();
            tabReturned.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvReturned).EndInit();
            tabFines.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvFines).EndInit();
            tabPopular.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPopular).EndInit();
            tabMembers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMembers).EndInit();
            tabInventory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvInventory).EndInit();
            pnlSummary.ResumeLayout(false);
            flpMetrics.ResumeLayout(false);
            flpMetrics.PerformLayout();
            pnlHeaderContainer.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ResumeLayout(false);

        }
    }
}
