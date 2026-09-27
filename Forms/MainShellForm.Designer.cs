namespace LibraryManagementSystem.Forms
{
    partial class MainShellForm
    {
        private System.ComponentModel.IContainer components = null;

        // Container Panels
        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlBrand;
        private System.Windows.Forms.Panel pnlNavButtons;
        private System.Windows.Forms.Panel pnlContentHost;
        private System.Windows.Forms.Panel pnlTopDivider;

        // Brand & User Controls
        private System.Windows.Forms.Label lblLogoIcon;
        private System.Windows.Forms.Label lblBrandTitle;
        private System.Windows.Forms.Label lblUserName = new();
        private System.Windows.Forms.Label lblUserRole = new();

        // Navigation Buttons
        private System.Windows.Forms.Button btnNavDashboard;
        private System.Windows.Forms.Button btnNavBooks;
        private System.Windows.Forms.Button btnNavMembers;
        private System.Windows.Forms.Button btnNavBorrow;
        private System.Windows.Forms.Button btnNavReturn;
        private System.Windows.Forms.Button btnNavReports;
        private System.Windows.Forms.Button btnNavCategories;
        private System.Windows.Forms.Button btnNavAuthors;
        private System.Windows.Forms.Button btnNavPublishers;
        private System.Windows.Forms.Button btnNavLibrarians;
        private System.Windows.Forms.Button btnLogout;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlSidebar = new Panel();
            pnlNavButtons = new Panel();
            btnNavLibrarians = new Button();
            btnNavPublishers = new Button();
            btnNavAuthors = new Button();
            btnNavCategories = new Button();
            btnNavReports = new Button();
            btnNavReturn = new Button();
            btnNavBorrow = new Button();
            btnNavMembers = new Button();
            btnNavBooks = new Button();
            btnNavDashboard = new Button();
            btnLogout = new Button();
            pnlBrand = new Panel();
            lblBrandTitle = new Label();
            lblLogoIcon = new Label();
            pnlContentHost = new Panel();
            pnlTopDivider = new Panel();
            pnlSidebar.SuspendLayout();
            pnlNavButtons.SuspendLayout();
            pnlBrand.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.FromArgb(220, 235, 252);
            pnlSidebar.Controls.Add(pnlNavButtons);
            pnlSidebar.Controls.Add(btnLogout);
            pnlSidebar.Controls.Add(pnlBrand);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 1);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(220, 720);
            pnlSidebar.TabIndex = 0;
            // 
            // pnlNavButtons
            // 
            pnlNavButtons.AutoScroll = true;
            pnlNavButtons.BackColor = Color.SteelBlue;
            pnlNavButtons.BorderStyle = BorderStyle.Fixed3D;
            pnlNavButtons.Controls.Add(btnNavLibrarians);
            pnlNavButtons.Controls.Add(btnNavPublishers);
            pnlNavButtons.Controls.Add(btnNavAuthors);
            pnlNavButtons.Controls.Add(btnNavCategories);
            pnlNavButtons.Controls.Add(btnNavReports);
            pnlNavButtons.Controls.Add(btnNavReturn);
            pnlNavButtons.Controls.Add(btnNavBorrow);
            pnlNavButtons.Controls.Add(btnNavMembers);
            pnlNavButtons.Controls.Add(btnNavBooks);
            pnlNavButtons.Controls.Add(btnNavDashboard);
            pnlNavButtons.Dock = DockStyle.Fill;
            pnlNavButtons.ForeColor = Color.FromArgb(13, 59, 102);
            pnlNavButtons.Location = new Point(0, 90);
            pnlNavButtons.Name = "pnlNavButtons";
            pnlNavButtons.Padding = new Padding(0, 4, 0, 4);
            pnlNavButtons.Size = new Size(220, 592);
            pnlNavButtons.TabIndex = 2;
            // 
            // btnNavLibrarians
            // 
            btnNavLibrarians.BackColor = Color.SteelBlue;
            btnNavLibrarians.Cursor = Cursors.Hand;
            btnNavLibrarians.Dock = DockStyle.Top;
            btnNavLibrarians.FlatAppearance.BorderColor = Color.FromArgb(55, 105, 145);
            btnNavLibrarians.FlatAppearance.MouseDownBackColor = Color.FromArgb(55, 105, 150);
            btnNavLibrarians.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 155, 213);
            btnNavLibrarians.FlatStyle = FlatStyle.Flat;
            btnNavLibrarians.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnNavLibrarians.ForeColor = Color.White;
            btnNavLibrarians.ImageAlign = ContentAlignment.MiddleLeft;
            btnNavLibrarians.Location = new Point(0, 328);
            btnNavLibrarians.Name = "btnNavLibrarians";
            btnNavLibrarians.Padding = new Padding(16, 0, 0, 0);
            btnNavLibrarians.Size = new Size(216, 36);
            btnNavLibrarians.TabIndex = 9;
            btnNavLibrarians.Text = "   Librarians";
            btnNavLibrarians.TextAlign = ContentAlignment.MiddleLeft;
            btnNavLibrarians.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNavLibrarians.UseVisualStyleBackColor = false;
            // 
            // btnNavPublishers
            // 
            btnNavPublishers.BackColor = Color.SteelBlue;
            btnNavPublishers.Cursor = Cursors.Hand;
            btnNavPublishers.Dock = DockStyle.Top;
            btnNavPublishers.FlatAppearance.BorderColor = Color.FromArgb(55, 105, 145);
            btnNavPublishers.FlatAppearance.MouseDownBackColor = Color.FromArgb(55, 105, 150);
            btnNavPublishers.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 155, 213);
            btnNavPublishers.FlatStyle = FlatStyle.Flat;
            btnNavPublishers.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnNavPublishers.ForeColor = Color.White;
            btnNavPublishers.ImageAlign = ContentAlignment.MiddleLeft;
            btnNavPublishers.Location = new Point(0, 292);
            btnNavPublishers.Name = "btnNavPublishers";
            btnNavPublishers.Padding = new Padding(16, 0, 0, 0);
            btnNavPublishers.Size = new Size(216, 36);
            btnNavPublishers.TabIndex = 8;
            btnNavPublishers.Text = "   Publishers";
            btnNavPublishers.TextAlign = ContentAlignment.MiddleLeft;
            btnNavPublishers.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNavPublishers.UseVisualStyleBackColor = false;
            // 
            // btnNavAuthors
            // 
            btnNavAuthors.BackColor = Color.SteelBlue;
            btnNavAuthors.Cursor = Cursors.Hand;
            btnNavAuthors.Dock = DockStyle.Top;
            btnNavAuthors.FlatAppearance.BorderColor = Color.FromArgb(55, 105, 145);
            btnNavAuthors.FlatAppearance.MouseDownBackColor = Color.FromArgb(55, 105, 150);
            btnNavAuthors.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 155, 213);
            btnNavAuthors.FlatStyle = FlatStyle.Flat;
            btnNavAuthors.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnNavAuthors.ForeColor = Color.White;
            btnNavAuthors.ImageAlign = ContentAlignment.MiddleLeft;
            btnNavAuthors.Location = new Point(0, 256);
            btnNavAuthors.Name = "btnNavAuthors";
            btnNavAuthors.Padding = new Padding(16, 0, 0, 0);
            btnNavAuthors.Size = new Size(216, 36);
            btnNavAuthors.TabIndex = 7;
            btnNavAuthors.Text = "   Authors";
            btnNavAuthors.TextAlign = ContentAlignment.MiddleLeft;
            btnNavAuthors.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNavAuthors.UseVisualStyleBackColor = false;
            // 
            // btnNavCategories
            // 
            btnNavCategories.BackColor = Color.SteelBlue;
            btnNavCategories.Cursor = Cursors.Hand;
            btnNavCategories.Dock = DockStyle.Top;
            btnNavCategories.FlatAppearance.BorderColor = Color.FromArgb(55, 105, 145);
            btnNavCategories.FlatAppearance.MouseDownBackColor = Color.FromArgb(55, 105, 150);
            btnNavCategories.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 155, 213);
            btnNavCategories.FlatStyle = FlatStyle.Flat;
            btnNavCategories.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnNavCategories.ForeColor = Color.White;
            btnNavCategories.ImageAlign = ContentAlignment.MiddleLeft;
            btnNavCategories.Location = new Point(0, 220);
            btnNavCategories.Name = "btnNavCategories";
            btnNavCategories.Padding = new Padding(16, 0, 0, 0);
            btnNavCategories.Size = new Size(216, 36);
            btnNavCategories.TabIndex = 6;
            btnNavCategories.Text = "   Categories";
            btnNavCategories.TextAlign = ContentAlignment.MiddleLeft;
            btnNavCategories.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNavCategories.UseVisualStyleBackColor = false;
            // 
            // btnNavReports
            // 
            btnNavReports.BackColor = Color.SteelBlue;
            btnNavReports.Cursor = Cursors.Hand;
            btnNavReports.Dock = DockStyle.Top;
            btnNavReports.FlatAppearance.BorderColor = Color.FromArgb(55, 105, 145);
            btnNavReports.FlatAppearance.MouseDownBackColor = Color.FromArgb(55, 105, 150);
            btnNavReports.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 155, 213);
            btnNavReports.FlatStyle = FlatStyle.Flat;
            btnNavReports.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnNavReports.ForeColor = Color.White;
            btnNavReports.ImageAlign = ContentAlignment.MiddleLeft;
            btnNavReports.Location = new Point(0, 184);
            btnNavReports.Name = "btnNavReports";
            btnNavReports.Padding = new Padding(16, 0, 0, 0);
            btnNavReports.Size = new Size(216, 36);
            btnNavReports.TabIndex = 5;
            btnNavReports.Text = "   Reports";
            btnNavReports.TextAlign = ContentAlignment.MiddleLeft;
            btnNavReports.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNavReports.UseVisualStyleBackColor = false;
            // 
            // btnNavReturn
            // 
            btnNavReturn.BackColor = Color.SteelBlue;
            btnNavReturn.Cursor = Cursors.Hand;
            btnNavReturn.Dock = DockStyle.Top;
            btnNavReturn.FlatAppearance.BorderColor = Color.FromArgb(55, 105, 145);
            btnNavReturn.FlatAppearance.MouseDownBackColor = Color.FromArgb(55, 105, 150);
            btnNavReturn.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 155, 213);
            btnNavReturn.FlatStyle = FlatStyle.Flat;
            btnNavReturn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnNavReturn.ForeColor = Color.White;
            btnNavReturn.ImageAlign = ContentAlignment.MiddleLeft;
            btnNavReturn.Location = new Point(0, 148);
            btnNavReturn.Name = "btnNavReturn";
            btnNavReturn.Padding = new Padding(16, 0, 0, 0);
            btnNavReturn.Size = new Size(216, 36);
            btnNavReturn.TabIndex = 4;
            btnNavReturn.Text = "   Return Books";
            btnNavReturn.TextAlign = ContentAlignment.MiddleLeft;
            btnNavReturn.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNavReturn.UseVisualStyleBackColor = false;
            // 
            // btnNavBorrow
            // 
            btnNavBorrow.BackColor = Color.SteelBlue;
            btnNavBorrow.Cursor = Cursors.Hand;
            btnNavBorrow.Dock = DockStyle.Top;
            btnNavBorrow.FlatAppearance.BorderColor = Color.FromArgb(55, 105, 145);
            btnNavBorrow.FlatAppearance.MouseDownBackColor = Color.FromArgb(55, 105, 150);
            btnNavBorrow.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 155, 213);
            btnNavBorrow.FlatStyle = FlatStyle.Flat;
            btnNavBorrow.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnNavBorrow.ForeColor = Color.White;
            btnNavBorrow.ImageAlign = ContentAlignment.MiddleLeft;
            btnNavBorrow.Location = new Point(0, 112);
            btnNavBorrow.Name = "btnNavBorrow";
            btnNavBorrow.Padding = new Padding(16, 0, 0, 0);
            btnNavBorrow.Size = new Size(216, 36);
            btnNavBorrow.TabIndex = 3;
            btnNavBorrow.Text = "   Borrow Books";
            btnNavBorrow.TextAlign = ContentAlignment.MiddleLeft;
            btnNavBorrow.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNavBorrow.UseVisualStyleBackColor = false;
            // 
            // btnNavMembers
            // 
            btnNavMembers.BackColor = Color.SteelBlue;
            btnNavMembers.Cursor = Cursors.Hand;
            btnNavMembers.Dock = DockStyle.Top;
            btnNavMembers.FlatAppearance.BorderColor = Color.FromArgb(55, 105, 145);
            btnNavMembers.FlatAppearance.MouseDownBackColor = Color.FromArgb(55, 105, 150);
            btnNavMembers.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 155, 213);
            btnNavMembers.FlatStyle = FlatStyle.Flat;
            btnNavMembers.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnNavMembers.ForeColor = Color.White;
            btnNavMembers.ImageAlign = ContentAlignment.MiddleLeft;
            btnNavMembers.Location = new Point(0, 76);
            btnNavMembers.Name = "btnNavMembers";
            btnNavMembers.Padding = new Padding(16, 0, 0, 0);
            btnNavMembers.Size = new Size(216, 36);
            btnNavMembers.TabIndex = 2;
            btnNavMembers.Text = "   Members";
            btnNavMembers.TextAlign = ContentAlignment.MiddleLeft;
            btnNavMembers.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNavMembers.UseVisualStyleBackColor = false;
            // 
            // btnNavBooks
            // 
            btnNavBooks.BackColor = Color.SteelBlue;
            btnNavBooks.Cursor = Cursors.Hand;
            btnNavBooks.Dock = DockStyle.Top;
            btnNavBooks.FlatAppearance.BorderColor = Color.FromArgb(55, 105, 145);
            btnNavBooks.FlatAppearance.MouseDownBackColor = Color.FromArgb(55, 105, 150);
            btnNavBooks.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 155, 213);
            btnNavBooks.FlatStyle = FlatStyle.Flat;
            btnNavBooks.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnNavBooks.ForeColor = Color.White;
            btnNavBooks.ImageAlign = ContentAlignment.MiddleLeft;
            btnNavBooks.Location = new Point(0, 40);
            btnNavBooks.Name = "btnNavBooks";
            btnNavBooks.Padding = new Padding(16, 0, 0, 0);
            btnNavBooks.Size = new Size(216, 36);
            btnNavBooks.TabIndex = 1;
            btnNavBooks.Text = "   Books";
            btnNavBooks.TextAlign = ContentAlignment.MiddleLeft;
            btnNavBooks.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNavBooks.UseVisualStyleBackColor = false;
            // 
            // btnNavDashboard
            // 
            btnNavDashboard.BackColor = Color.SteelBlue;
            btnNavDashboard.Cursor = Cursors.Hand;
            btnNavDashboard.Dock = DockStyle.Top;
            btnNavDashboard.FlatAppearance.BorderColor = Color.FromArgb(55, 105, 145);
            btnNavDashboard.FlatAppearance.MouseDownBackColor = Color.FromArgb(55, 105, 150);
            btnNavDashboard.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 155, 213);
            btnNavDashboard.FlatStyle = FlatStyle.Flat;
            btnNavDashboard.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnNavDashboard.ForeColor = Color.White;
            btnNavDashboard.ImageAlign = ContentAlignment.MiddleLeft;
            btnNavDashboard.Location = new Point(0, 4);
            btnNavDashboard.Name = "btnNavDashboard";
            btnNavDashboard.Padding = new Padding(16, 0, 0, 0);
            btnNavDashboard.Size = new Size(216, 36);
            btnNavDashboard.TabIndex = 0;
            btnNavDashboard.Text = "   Dashboard";
            btnNavDashboard.TextAlign = ContentAlignment.MiddleLeft;
            btnNavDashboard.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNavDashboard.UseVisualStyleBackColor = false;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.SteelBlue;
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.Dock = DockStyle.Bottom;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatAppearance.MouseOverBackColor = Color.FromArgb(254, 226, 226);
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnLogout.ForeColor = Color.White;
            btnLogout.ImageAlign = ContentAlignment.MiddleLeft;
            btnLogout.Location = new Point(0, 682);
            btnLogout.Name = "btnLogout";
            btnLogout.Padding = new Padding(16, 0, 0, 0);
            btnLogout.Size = new Size(220, 38);
            btnLogout.TabIndex = 1;
            btnLogout.Text = "   Sign Out";
            btnLogout.TextAlign = ContentAlignment.MiddleLeft;
            btnLogout.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += BtnLogout_Click;
            // 
            // pnlBrand
            // 
            pnlBrand.BackColor = Color.SteelBlue;
            pnlBrand.BorderStyle = BorderStyle.Fixed3D;
            pnlBrand.Controls.Add(lblBrandTitle);
            pnlBrand.Controls.Add(lblLogoIcon);
            pnlBrand.Dock = DockStyle.Top;
            pnlBrand.Location = new Point(0, 0);
            pnlBrand.Name = "pnlBrand";
            pnlBrand.Padding = new Padding(16, 12, 16, 12);
            pnlBrand.Size = new Size(220, 90);
            pnlBrand.TabIndex = 0;
            // 
            // lblBrandTitle
            // 
            lblBrandTitle.BackColor = Color.SteelBlue;
            lblBrandTitle.Font = new Font("Segoe UI Semibold", 13.5F, FontStyle.Bold);
            lblBrandTitle.ForeColor = Color.White;
            lblBrandTitle.Location = new Point(44, 26);
            lblBrandTitle.Name = "lblBrandTitle";
            lblBrandTitle.Size = new Size(168, 32);
            lblBrandTitle.TabIndex = 1;
            lblBrandTitle.Text = "Library System";
            // 
            // lblLogoIcon
            // 
            lblLogoIcon.Cursor = Cursors.Hand;
            lblLogoIcon.Font = new Font("Segoe MDL2 Assets", 16F);
            lblLogoIcon.ForeColor = Color.White;
            lblLogoIcon.Location = new Point(10, 26);
            lblLogoIcon.Name = "lblLogoIcon";
            lblLogoIcon.Size = new Size(43, 32);
            lblLogoIcon.TabIndex = 0;
            lblLogoIcon.Text = "";
            lblLogoIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlContentHost
            // 
            pnlContentHost.BackColor = Color.FromArgb(235, 243, 250);
            pnlContentHost.Dock = DockStyle.Fill;
            pnlContentHost.Location = new Point(220, 1);
            pnlContentHost.Name = "pnlContentHost";
            pnlContentHost.Size = new Size(1044, 720);
            pnlContentHost.TabIndex = 1;
            // 
            // pnlTopDivider
            // 
            pnlTopDivider.BackColor = Color.FromArgb(15, 23, 42);
            pnlTopDivider.Dock = DockStyle.Top;
            pnlTopDivider.Location = new Point(0, 0);
            pnlTopDivider.Name = "pnlTopDivider";
            pnlTopDivider.Size = new Size(1264, 1);
            pnlTopDivider.TabIndex = 2;
            // 
            // MainShellForm
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(235, 243, 250);
            ClientSize = new Size(1264, 721);
            Controls.Add(pnlContentHost);
            Controls.Add(pnlSidebar);
            Controls.Add(pnlTopDivider);
            Font = new Font("Segoe UI", 9.5F);
            MinimumSize = new Size(1024, 600);
            Name = "MainShellForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Library Management System — Dashboard";
            pnlSidebar.ResumeLayout(false);
            pnlNavButtons.ResumeLayout(false);
            pnlBrand.ResumeLayout(false);
            ResumeLayout(false);

        }
    }
}
