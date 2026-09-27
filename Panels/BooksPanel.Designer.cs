namespace LibraryManagementSystem.Panels
{
    partial class BooksPanel
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlHeaderContainer;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;

        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.FlowLayoutPanel flpActions;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;

        private System.Windows.Forms.Panel pnlGridContainer;
        private System.Windows.Forms.DataGridView dgvBooks;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colISBN;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAuthor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAvailable;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotal;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlHeaderContainer = new Panel();
            pnlHeader = new Panel();
            lblTitle = new Label();
            pnlToolbar = new Panel();
            flpActions = new FlowLayoutPanel();
            btnRefresh = new Button();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            txtSearch = new TextBox();
            pnlGridContainer = new Panel();
            dgvBooks = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colTitle = new DataGridViewTextBoxColumn();
            colISBN = new DataGridViewTextBoxColumn();
            colAuthor = new DataGridViewTextBoxColumn();
            colCategory = new DataGridViewTextBoxColumn();
            colYear = new DataGridViewTextBoxColumn();
            colAvailable = new DataGridViewTextBoxColumn();
            colTotal = new DataGridViewTextBoxColumn();
            pnlHeaderContainer.SuspendLayout();
            pnlHeader.SuspendLayout();
            pnlToolbar.SuspendLayout();
            flpActions.SuspendLayout();
            pnlGridContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBooks).BeginInit();
            SuspendLayout();
            // 
            // pnlHeaderContainer
            // 
            pnlHeaderContainer.BackColor = Color.FromArgb(235, 243, 250);
            pnlHeaderContainer.Controls.Add(pnlHeader);
            pnlHeaderContainer.Dock = DockStyle.Top;
            pnlHeaderContainer.Location = new Point(0, 0);
            pnlHeaderContainer.Name = "pnlHeaderContainer";
            pnlHeaderContainer.Padding = new Padding(16, 8, 16, 6);
            pnlHeaderContainer.Size = new Size(1034, 68);
            pnlHeaderContainer.TabIndex = 0;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.SteelBlue;
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Fill;
            pnlHeader.Location = new Point(16, 8);
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
            lblTitle.Text = "Books Management";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlToolbar
            // 
            pnlToolbar.BackColor = Color.FromArgb(235, 243, 250);
            pnlToolbar.Controls.Add(flpActions);
            pnlToolbar.Controls.Add(txtSearch);
            pnlToolbar.Dock = DockStyle.Top;
            pnlToolbar.Location = new Point(0, 68);
            pnlToolbar.Name = "pnlToolbar";
            pnlToolbar.Padding = new Padding(16, 7, 16, 7);
            pnlToolbar.Size = new Size(1034, 46);
            pnlToolbar.TabIndex = 1;
            // 
            // flpActions
            // 
            flpActions.AutoSize = true;
            flpActions.BackColor = Color.Transparent;
            flpActions.Controls.Add(btnRefresh);
            flpActions.Controls.Add(btnAdd);
            flpActions.Controls.Add(btnEdit);
            flpActions.Controls.Add(btnDelete);
            flpActions.Dock = DockStyle.Right;
            flpActions.Location = new Point(593, 7);
            flpActions.Name = "flpActions";
            flpActions.Size = new Size(425, 32);
            flpActions.TabIndex = 1;
            flpActions.WrapContents = false;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.SteelBlue;
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(208, 225, 253);
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.Transparent;
            btnRefresh.Location = new Point(0, 0);
            btnRefresh.Margin = new Padding(0, 0, 8, 0);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(95, 32);
            btnRefresh.TabIndex = 0;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.SteelBlue;
            btnAdd.Cursor = Cursors.Hand;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnAdd.ForeColor = Color.Transparent;
            btnAdd.Location = new Point(103, 0);
            btnAdd.Margin = new Padding(0, 0, 8, 0);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(116, 32);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Add Book";
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.SteelBlue;
            btnEdit.Cursor = Cursors.Hand;
            btnEdit.FlatAppearance.BorderColor = Color.FromArgb(208, 225, 253);
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnEdit.ForeColor = Color.Transparent;
            btnEdit.Location = new Point(227, 0);
            btnEdit.Margin = new Padding(0, 0, 8, 0);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(95, 32);
            btnEdit.TabIndex = 2;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.SteelBlue;
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnDelete.ForeColor = Color.Transparent;
            btnDelete.Location = new Point(330, 0);
            btnDelete.Margin = new Padding(0);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(95, 32);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // txtSearch
            // 
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Dock = DockStyle.Left;
            txtSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSearch.ForeColor = Color.FromArgb(13, 59, 102);
            txtSearch.Location = new Point(16, 7);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "  Search title, ISBN, author, category...";
            txtSearch.Size = new Size(380, 29);
            txtSearch.TabIndex = 0;
            // 
            // pnlGridContainer
            // 
            pnlGridContainer.BackColor = Color.FromArgb(235, 243, 250);
            pnlGridContainer.Controls.Add(dgvBooks);
            pnlGridContainer.Dock = DockStyle.Fill;
            pnlGridContainer.Location = new Point(0, 114);
            pnlGridContainer.Name = "pnlGridContainer";
            pnlGridContainer.Padding = new Padding(16, 4, 16, 12);
            pnlGridContainer.Size = new Size(1034, 607);
            pnlGridContainer.TabIndex = 2;
            // 
            // dgvBooks
            // 
            dgvBooks.AllowUserToAddRows = false;
            dgvBooks.AllowUserToDeleteRows = false;
            dgvBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBooks.BackgroundColor = Color.White;
            dgvBooks.BorderStyle = BorderStyle.Fixed3D;
            dgvBooks.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvBooks.ColumnHeadersHeight = 36;
            dgvBooks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvBooks.Columns.AddRange(new DataGridViewColumn[] { colId, colTitle, colISBN, colAuthor, colCategory, colYear, colAvailable, colTotal });
            dgvBooks.Dock = DockStyle.Fill;
            dgvBooks.EnableHeadersVisualStyles = false;
            dgvBooks.GridColor = SystemColors.MenuHighlight;
            dgvBooks.Location = new Point(16, 4);
            dgvBooks.MultiSelect = false;
            dgvBooks.Name = "dgvBooks";
            dgvBooks.ReadOnly = true;
            dgvBooks.RowHeadersVisible = false;
            dgvBooks.RowHeadersWidth = 51;
            dgvBooks.RowTemplate.Height = 32;
            dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBooks.Size = new Size(1002, 591);
            dgvBooks.TabIndex = 0;
            // 
            // colId
            // 
            colId.FillWeight = 6F;
            colId.HeaderText = "ID";
            colId.MinimumWidth = 60;
            colId.Name = "colId";
            colId.ReadOnly = true;
            // 
            // colTitle
            // 
            colTitle.FillWeight = 28F;
            colTitle.HeaderText = "Title";
            colTitle.MinimumWidth = 160;
            colTitle.Name = "colTitle";
            colTitle.ReadOnly = true;
            // 
            // colISBN
            // 
            colISBN.FillWeight = 16F;
            colISBN.HeaderText = "ISBN";
            colISBN.MinimumWidth = 135;
            colISBN.Name = "colISBN";
            colISBN.ReadOnly = true;
            // 
            // colAuthor
            // 
            colAuthor.FillWeight = 18F;
            colAuthor.HeaderText = "Author";
            colAuthor.MinimumWidth = 120;
            colAuthor.Name = "colAuthor";
            colAuthor.ReadOnly = true;
            // 
            // colCategory
            // 
            colCategory.FillWeight = 14F;
            colCategory.HeaderText = "Category";
            colCategory.MinimumWidth = 110;
            colCategory.Name = "colCategory";
            colCategory.ReadOnly = true;
            // 
            // colYear
            // 
            colYear.FillWeight = 8F;
            colYear.HeaderText = "Year";
            colYear.MinimumWidth = 70;
            colYear.Name = "colYear";
            colYear.ReadOnly = true;
            // 
            // colAvailable
            // 
            colAvailable.FillWeight = 10F;
            colAvailable.HeaderText = "Available";
            colAvailable.MinimumWidth = 80;
            colAvailable.Name = "colAvailable";
            colAvailable.ReadOnly = true;
            // 
            // colTotal
            // 
            colTotal.FillWeight = 10F;
            colTotal.HeaderText = "Total";
            colTotal.MinimumWidth = 80;
            colTotal.Name = "colTotal";
            colTotal.ReadOnly = true;
            // 
            // BooksPanel
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(235, 243, 250);
            Controls.Add(pnlGridContainer);
            Controls.Add(pnlToolbar);
            Controls.Add(pnlHeaderContainer);
            Font = new Font("Segoe UI", 9.5F);
            Name = "BooksPanel";
            Size = new Size(1034, 721);
            pnlHeaderContainer.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            pnlToolbar.ResumeLayout(false);
            pnlToolbar.PerformLayout();
            flpActions.ResumeLayout(false);
            pnlGridContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvBooks).EndInit();
            ResumeLayout(false);

        }
    }
}
