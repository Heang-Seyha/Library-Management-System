namespace LibraryManagementSystem.Panels
{
    partial class AuthorsPanel
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlHeaderContainer;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;

        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.FlowLayoutPanel flpActions;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox txtSearch;

        private System.Windows.Forms.Panel pnlGridContainer;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGender;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDob;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBio;

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
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnRefresh = new Button();
            txtSearch = new TextBox();
            pnlGridContainer = new Panel();
            dgv = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colGender = new DataGridViewTextBoxColumn();
            colDob = new DataGridViewTextBoxColumn();
            colBio = new DataGridViewTextBoxColumn();
            pnlHeaderContainer.SuspendLayout();
            pnlHeader.SuspendLayout();
            pnlToolbar.SuspendLayout();
            flpActions.SuspendLayout();
            pnlGridContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
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
            lblTitle.AutoSize = false;
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(1002, 54);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Authors Directory";
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
            flpActions.Controls.Add(btnAdd);
            flpActions.Controls.Add(btnEdit);
            flpActions.Controls.Add(btnDelete);
            flpActions.Controls.Add(btnRefresh);
            flpActions.Dock = DockStyle.Right;
            flpActions.Location = new Point(16, 7);
            flpActions.Name = "flpActions";
            flpActions.Size = new Size(435, 32);
            flpActions.TabIndex = 0;
            flpActions.WrapContents = false;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.SteelBlue;
            btnAdd.Cursor = Cursors.Hand;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnAdd.ForeColor = Color.Transparent;
            btnAdd.Location = new Point(0, 0);
            btnAdd.Margin = new Padding(0, 0, 8, 0);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(126, 32);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "Add Author";
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
            btnEdit.Location = new Point(134, 0);
            btnEdit.Margin = new Padding(0, 0, 8, 0);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(95, 32);
            btnEdit.TabIndex = 1;
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
            btnDelete.Location = new Point(237, 0);
            btnDelete.Margin = new Padding(0, 0, 8, 0);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(95, 32);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.SteelBlue;
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(208, 225, 253);
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.Transparent;
            btnRefresh.Location = new Point(340, 0);
            btnRefresh.Margin = new Padding(0);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(95, 32);
            btnRefresh.TabIndex = 3;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            // 
            // txtSearch
            // 
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Dock = DockStyle.Left;
            txtSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSearch.ForeColor = Color.FromArgb(13, 59, 102);
            txtSearch.Location = new Point(16, 7);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "  Search by author name or biography...";
            txtSearch.Size = new Size(380, 29);
            txtSearch.TabIndex = 0;
            // 
            // pnlGridContainer
            // 
            pnlGridContainer.BackColor = Color.FromArgb(235, 243, 250);
            pnlGridContainer.Controls.Add(dgv);
            pnlGridContainer.Dock = DockStyle.Fill;
            pnlGridContainer.Location = new Point(0, 114);
            pnlGridContainer.Name = "pnlGridContainer";
            pnlGridContainer.Padding = new Padding(16, 4, 16, 12);
            pnlGridContainer.Size = new Size(1034, 607);
            pnlGridContainer.TabIndex = 2;
            // 
            // dgv
            // 
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeColumns = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.Fixed3D;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.ColumnHeadersHeight = 36;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgv.Columns.AddRange(new DataGridViewColumn[] { colId, colName, colGender, colDob, colBio });
            dgv.Dock = DockStyle.Fill;
            dgv.EnableHeadersVisualStyles = false;
            dgv.GridColor = SystemColors.MenuHighlight;
            dgv.Location = new Point(16, 4);
            dgv.MultiSelect = false;
            dgv.Name = "dgv";
            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false;
            dgv.RowHeadersWidth = 51;
            dgv.RowTemplate.Height = 32;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.Size = new Size(1002, 591);
            dgv.TabIndex = 0;
            // 
            // colId
            // 
            colId.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colId.HeaderText = "Author ID";
            colId.MinimumWidth = 135;
            colId.Name = "colId";
            colId.ReadOnly = true;
            colId.Resizable = DataGridViewTriState.True;
            colId.Width = 145;
            // 
            // colName
            // 
            colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colName.FillWeight = 40F;
            colName.HeaderText = "Author Name";
            colName.MinimumWidth = 160;
            colName.Name = "colName";
            colName.ReadOnly = true;
            colName.Resizable = DataGridViewTriState.True;
            // 
            // colGender
            // 
            colGender.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colGender.HeaderText = "Gender";
            colGender.MinimumWidth = 100;
            colGender.Name = "colGender";
            colGender.ReadOnly = true;
            colGender.Resizable = DataGridViewTriState.True;
            colGender.Width = 110;
            // 
            // colDob
            // 
            colDob.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colDob.HeaderText = "Date of Birth";
            colDob.MinimumWidth = 135;
            colDob.Name = "colDob";
            colDob.ReadOnly = true;
            colDob.Resizable = DataGridViewTriState.True;
            colDob.Width = 145;
            // 
            // colBio
            // 
            colBio.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colBio.FillWeight = 60F;
            colBio.HeaderText = "Biography";
            colBio.MinimumWidth = 200;
            colBio.Name = "colBio";
            colBio.ReadOnly = true;
            colBio.Resizable = DataGridViewTriState.True;
            // 
            // AuthorsPanel
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.SteelBlue;
            Controls.Add(pnlGridContainer);
            Controls.Add(pnlToolbar);
            Controls.Add(pnlHeaderContainer);
            Font = new Font("Segoe UI", 9.5F);
            ForeColor = Color.Transparent;
            Name = "AuthorsPanel";
            Size = new Size(1034, 721);
            pnlHeaderContainer.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlToolbar.ResumeLayout(false);
            pnlToolbar.PerformLayout();
            flpActions.ResumeLayout(false);
            pnlGridContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            ResumeLayout(false);

        }
    }
}
