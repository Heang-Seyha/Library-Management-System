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
        private System.Windows.Forms.Panel pnlLogout;
        private System.Windows.Forms.Panel pnlBottomSpacer;
        private System.Windows.Forms.Panel pnlNavDivider1;
        private System.Windows.Forms.Panel pnlNavDivider2;
        private System.Windows.Forms.Panel pnlNavDivider3;
        private System.Windows.Forms.Panel pnlNavDivider4;
        private System.Windows.Forms.Panel pnlNavDivider5;

        // Brand & User Controls
        private System.Windows.Forms.Label lblLogoIcon;
        private System.Windows.Forms.Label lblBrandTitle;
        private System.Windows.Forms.Label lblUserName = new();
        private System.Windows.Forms.Label lblUserRole = new();

        // Category Section Labels
        private System.Windows.Forms.Label lblGroupMain;
        private System.Windows.Forms.Label lblGroupOperations;
        private System.Windows.Forms.Label lblGroupManagement;
        private System.Windows.Forms.Label lblGroupMetadata;
        private System.Windows.Forms.Label lblGroupAdmin;

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
            pnlNavDivider5 = new Panel();
            btnNavLibrarians = new Button();
            btnNavReports = new Button();
            lblGroupAdmin = new Label();
            pnlNavDivider4 = new Panel();
            btnNavPublishers = new Button();
            btnNavAuthors = new Button();
            btnNavCategories = new Button();
            lblGroupMetadata = new Label();
            pnlNavDivider3 = new Panel();
            btnNavMembers = new Button();
            btnNavBooks = new Button();
            lblGroupManagement = new Label();
            pnlNavDivider2 = new Panel();
            btnNavReturn = new Button();
            btnNavBorrow = new Button();
            lblGroupOperations = new Label();
            pnlNavDivider1 = new Panel();
            btnNavDashboard = new Button();
            lblGroupMain = new Label();
            pnlBottomSpacer = new Panel();
            pnlLogout = new Panel();
            btnLogout = new Button();
            pnlBrand = new Panel();
            lblBrandTitle = new Label();
            lblLogoIcon = new Label();
            pnlContentHost = new Panel();
            pnlTopDivider = new Panel();
            pnlSidebar.SuspendLayout();
            pnlNavButtons.SuspendLayout();
            pnlLogout.SuspendLayout();
            pnlBrand.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.FromArgb(220, 235, 252);
            pnlSidebar.Controls.Add(pnlNavButtons);
            pnlSidebar.Controls.Add(pnlBottomSpacer);
            pnlSidebar.Controls.Add(pnlLogout);
            pnlSidebar.Controls.Add(pnlBrand);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 1);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(220, 720);
            pnlSidebar.TabIndex = 0;
            // 
            // pnlNavButtons
            // 
            pnlNavButtons.BackColor = Color.FromArgb(63, 117, 162);
            pnlNavButtons.Controls.Add(pnlNavDivider5);
            pnlNavButtons.Controls.Add(btnNavLibrarians);
            pnlNavButtons.Controls.Add(btnNavReports);
            pnlNavButtons.Controls.Add(lblGroupAdmin);
            pnlNavButtons.Controls.Add(pnlNavDivider4);
            pnlNavButtons.Controls.Add(btnNavPublishers);
            pnlNavButtons.Controls.Add(btnNavAuthors);
            pnlNavButtons.Controls.Add(btnNavCategories);
            pnlNavButtons.Controls.Add(lblGroupMetadata);
            pnlNavButtons.Controls.Add(pnlNavDivider3);
            pnlNavButtons.Controls.Add(btnNavMembers);
            pnlNavButtons.Controls.Add(btnNavBooks);
            pnlNavButtons.Controls.Add(lblGroupManagement);
            pnlNavButtons.Controls.Add(pnlNavDivider2);
            pnlNavButtons.Controls.Add(btnNavReturn);
            pnlNavButtons.Controls.Add(btnNavBorrow);
            pnlNavButtons.Controls.Add(lblGroupOperations);
            pnlNavButtons.Controls.Add(pnlNavDivider1);
            pnlNavButtons.Controls.Add(btnNavDashboard);
            pnlNavButtons.Controls.Add(lblGroupMain);
            pnlNavButtons.Dock = DockStyle.Fill;
            pnlNavButtons.ForeColor = Color.FromArgb(13, 59, 102);
            pnlNavButtons.Location = new Point(0, 90);
            pnlNavButtons.Name = "pnlNavButtons";
            pnlNavButtons.Size = new Size(220, 566);
            pnlNavButtons.TabIndex = 2;
            // 
            // pnlNavDivider5
            // 
            pnlNavDivider5.BackColor = Color.FromArgb(13, 59, 102);
            pnlNavDivider5.Dock = DockStyle.Top;
            pnlNavDivider5.Location = new Point(0, 518);
            pnlNavDivider5.Name = "pnlNavDivider5";
            pnlNavDivider5.Size = new Size(220, 1);
            pnlNavDivider5.TabIndex = 24;
            // 
            // btnNavLibrarians
            // 
            btnNavLibrarians.BackColor = Color.FromArgb(63, 117, 162);
            btnNavLibrarians.Cursor = Cursors.Hand;
            btnNavLibrarians.Dock = DockStyle.Top;
            btnNavLibrarians.FlatAppearance.BorderColor = Color.FromArgb(13, 59, 102);
            btnNavLibrarians.FlatAppearance.MouseDownBackColor = Color.FromArgb(55, 105, 150);
            btnNavLibrarians.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 155, 213);
            btnNavLibrarians.FlatStyle = FlatStyle.Popup;
            btnNavLibrarians.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnNavLibrarians.ForeColor = Color.White;
            btnNavLibrarians.ImageAlign = ContentAlignment.MiddleLeft;
            btnNavLibrarians.Location = new Point(0, 482);
            btnNavLibrarians.Margin = new Padding(4, 2, 4, 2);
            btnNavLibrarians.Name = "btnNavLibrarians";
            btnNavLibrarians.Padding = new Padding(16, 0, 0, 0);
            btnNavLibrarians.Size = new Size(220, 36);
            btnNavLibrarians.TabIndex = 9;
            btnNavLibrarians.Text = "   Librarians";
            btnNavLibrarians.TextAlign = ContentAlignment.MiddleLeft;
            btnNavLibrarians.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNavLibrarians.UseVisualStyleBackColor = false;
            // 
            // btnNavReports
            // 
            btnNavReports.BackColor = Color.FromArgb(63, 117, 162);
            btnNavReports.Cursor = Cursors.Hand;
            btnNavReports.Dock = DockStyle.Top;
            btnNavReports.FlatAppearance.BorderColor = Color.FromArgb(13, 59, 102);
            btnNavReports.FlatAppearance.MouseDownBackColor = Color.FromArgb(55, 105, 150);
            btnNavReports.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 155, 213);
            btnNavReports.FlatStyle = FlatStyle.Popup;
            btnNavReports.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnNavReports.ForeColor = Color.White;
            btnNavReports.ImageAlign = ContentAlignment.MiddleLeft;
            btnNavReports.Location = new Point(0, 446);
            btnNavReports.Margin = new Padding(4, 2, 4, 2);
            btnNavReports.Name = "btnNavReports";
            btnNavReports.Padding = new Padding(16, 0, 0, 0);
            btnNavReports.Size = new Size(220, 36);
            btnNavReports.TabIndex = 5;
            btnNavReports.Text = "   Reports";
            btnNavReports.TextAlign = ContentAlignment.MiddleLeft;
            btnNavReports.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNavReports.UseVisualStyleBackColor = false;
            // 
            // lblGroupAdmin
            // 
            lblGroupAdmin.BackColor = Color.FromArgb(63, 117, 162);
            lblGroupAdmin.Dock = DockStyle.Top;
            lblGroupAdmin.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblGroupAdmin.ForeColor = Color.FromArgb(208, 225, 253);
            lblGroupAdmin.Location = new Point(0, 414);
            lblGroupAdmin.Name = "lblGroupAdmin";
            lblGroupAdmin.Padding = new Padding(16, 12, 0, 4);
            lblGroupAdmin.Size = new Size(220, 32);
            lblGroupAdmin.TabIndex = 14;
            lblGroupAdmin.Text = "SYSTEM";
            lblGroupAdmin.TextAlign = ContentAlignment.BottomLeft;
            // 
            // pnlNavDivider4
            // 
            pnlNavDivider4.BackColor = Color.FromArgb(13, 59, 102);
            pnlNavDivider4.Dock = DockStyle.Top;
            pnlNavDivider4.Location = new Point(0, 413);
            pnlNavDivider4.Name = "pnlNavDivider4";
            pnlNavDivider4.Size = new Size(220, 1);
            pnlNavDivider4.TabIndex = 23;
            // 
            // btnNavPublishers
            // 
            btnNavPublishers.BackColor = Color.FromArgb(63, 117, 162);
            btnNavPublishers.Cursor = Cursors.Hand;
            btnNavPublishers.Dock = DockStyle.Top;
            btnNavPublishers.FlatAppearance.BorderColor = Color.FromArgb(13, 59, 102);
            btnNavPublishers.FlatAppearance.MouseDownBackColor = Color.FromArgb(55, 105, 150);
            btnNavPublishers.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 155, 213);
            btnNavPublishers.FlatStyle = FlatStyle.Popup;
            btnNavPublishers.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnNavPublishers.ForeColor = Color.White;
            btnNavPublishers.ImageAlign = ContentAlignment.MiddleLeft;
            btnNavPublishers.Location = new Point(0, 377);
            btnNavPublishers.Margin = new Padding(4, 2, 4, 2);
            btnNavPublishers.Name = "btnNavPublishers";
            btnNavPublishers.Padding = new Padding(16, 0, 0, 0);
            btnNavPublishers.Size = new Size(220, 36);
            btnNavPublishers.TabIndex = 8;
            btnNavPublishers.Text = "   Publishers";
            btnNavPublishers.TextAlign = ContentAlignment.MiddleLeft;
            btnNavPublishers.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNavPublishers.UseVisualStyleBackColor = false;
            // 
            // btnNavAuthors
            // 
            btnNavAuthors.BackColor = Color.FromArgb(63, 117, 162);
            btnNavAuthors.Cursor = Cursors.Hand;
            btnNavAuthors.Dock = DockStyle.Top;
            btnNavAuthors.FlatAppearance.BorderColor = Color.FromArgb(13, 59, 102);
            btnNavAuthors.FlatAppearance.MouseDownBackColor = Color.FromArgb(55, 105, 150);
            btnNavAuthors.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 155, 213);
            btnNavAuthors.FlatStyle = FlatStyle.Popup;
            btnNavAuthors.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnNavAuthors.ForeColor = Color.White;
            btnNavAuthors.ImageAlign = ContentAlignment.MiddleLeft;
            btnNavAuthors.Location = new Point(0, 341);
            btnNavAuthors.Margin = new Padding(4, 2, 4, 2);
            btnNavAuthors.Name = "btnNavAuthors";
            btnNavAuthors.Padding = new Padding(16, 0, 0, 0);
            btnNavAuthors.Size = new Size(220, 36);
            btnNavAuthors.TabIndex = 7;
            btnNavAuthors.Text = "   Authors";
            btnNavAuthors.TextAlign = ContentAlignment.MiddleLeft;
            btnNavAuthors.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNavAuthors.UseVisualStyleBackColor = false;
            // 
            // btnNavCategories
            // 
            btnNavCategories.BackColor = Color.FromArgb(63, 117, 162);
            btnNavCategories.Cursor = Cursors.Hand;
            btnNavCategories.Dock = DockStyle.Top;
            btnNavCategories.FlatAppearance.BorderColor = Color.FromArgb(13, 59, 102);
            btnNavCategories.FlatAppearance.MouseDownBackColor = Color.FromArgb(55, 105, 150);
            btnNavCategories.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 155, 213);
            btnNavCategories.FlatStyle = FlatStyle.Popup;
            btnNavCategories.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnNavCategories.ForeColor = Color.White;
            btnNavCategories.ImageAlign = ContentAlignment.MiddleLeft;
            btnNavCategories.Location = new Point(0, 305);
            btnNavCategories.Margin = new Padding(4, 2, 4, 2);
            btnNavCategories.Name = "btnNavCategories";
            btnNavCategories.Padding = new Padding(16, 0, 0, 0);
            btnNavCategories.Size = new Size(220, 36);
            btnNavCategories.TabIndex = 6;
            btnNavCategories.Text = "   Categories";
            btnNavCategories.TextAlign = ContentAlignment.MiddleLeft;
            btnNavCategories.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNavCategories.UseVisualStyleBackColor = false;
            // 
            // lblGroupMetadata
            // 
            lblGroupMetadata.BackColor = Color.FromArgb(63, 117, 162);
            lblGroupMetadata.Dock = DockStyle.Top;
            lblGroupMetadata.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblGroupMetadata.ForeColor = Color.FromArgb(208, 225, 253);
            lblGroupMetadata.Location = new Point(0, 273);
            lblGroupMetadata.Name = "lblGroupMetadata";
            lblGroupMetadata.Padding = new Padding(16, 12, 0, 4);
            lblGroupMetadata.Size = new Size(220, 32);
            lblGroupMetadata.TabIndex = 13;
            lblGroupMetadata.Text = "METADATA";
            lblGroupMetadata.TextAlign = ContentAlignment.BottomLeft;
            // 
            // pnlNavDivider3
            // 
            pnlNavDivider3.BackColor = Color.FromArgb(13, 59, 102);
            pnlNavDivider3.Dock = DockStyle.Top;
            pnlNavDivider3.Location = new Point(0, 272);
            pnlNavDivider3.Name = "pnlNavDivider3";
            pnlNavDivider3.Size = new Size(220, 1);
            pnlNavDivider3.TabIndex = 22;
            // 
            // btnNavMembers
            // 
            btnNavMembers.BackColor = Color.FromArgb(63, 117, 162);
            btnNavMembers.Cursor = Cursors.Hand;
            btnNavMembers.Dock = DockStyle.Top;
            btnNavMembers.FlatAppearance.BorderColor = Color.FromArgb(13, 59, 102);
            btnNavMembers.FlatAppearance.MouseDownBackColor = Color.FromArgb(55, 105, 150);
            btnNavMembers.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 155, 213);
            btnNavMembers.FlatStyle = FlatStyle.Popup;
            btnNavMembers.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnNavMembers.ForeColor = Color.White;
            btnNavMembers.ImageAlign = ContentAlignment.MiddleLeft;
            btnNavMembers.Location = new Point(0, 236);
            btnNavMembers.Margin = new Padding(4, 2, 4, 2);
            btnNavMembers.Name = "btnNavMembers";
            btnNavMembers.Padding = new Padding(16, 0, 0, 0);
            btnNavMembers.Size = new Size(220, 36);
            btnNavMembers.TabIndex = 2;
            btnNavMembers.Text = "   Members";
            btnNavMembers.TextAlign = ContentAlignment.MiddleLeft;
            btnNavMembers.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNavMembers.UseVisualStyleBackColor = false;
            // 
            // btnNavBooks
            // 
            btnNavBooks.BackColor = Color.FromArgb(63, 117, 162);
            btnNavBooks.Cursor = Cursors.Hand;
            btnNavBooks.Dock = DockStyle.Top;
            btnNavBooks.FlatAppearance.BorderColor = Color.FromArgb(13, 59, 102);
            btnNavBooks.FlatAppearance.MouseDownBackColor = Color.FromArgb(55, 105, 150);
            btnNavBooks.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 155, 213);
            btnNavBooks.FlatStyle = FlatStyle.Popup;
            btnNavBooks.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnNavBooks.ForeColor = Color.White;
            btnNavBooks.ImageAlign = ContentAlignment.MiddleLeft;
            btnNavBooks.Location = new Point(0, 200);
            btnNavBooks.Margin = new Padding(4, 2, 4, 2);
            btnNavBooks.Name = "btnNavBooks";
            btnNavBooks.Padding = new Padding(16, 0, 0, 0);
            btnNavBooks.Size = new Size(220, 36);
            btnNavBooks.TabIndex = 1;
            btnNavBooks.Text = "   Books";
            btnNavBooks.TextAlign = ContentAlignment.MiddleLeft;
            btnNavBooks.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNavBooks.UseVisualStyleBackColor = false;
            // 
            // lblGroupManagement
            // 
            lblGroupManagement.BackColor = Color.FromArgb(63, 117, 162);
            lblGroupManagement.Dock = DockStyle.Top;
            lblGroupManagement.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblGroupManagement.ForeColor = Color.FromArgb(208, 225, 253);
            lblGroupManagement.Location = new Point(0, 168);
            lblGroupManagement.Name = "lblGroupManagement";
            lblGroupManagement.Padding = new Padding(16, 12, 0, 4);
            lblGroupManagement.Size = new Size(220, 32);
            lblGroupManagement.TabIndex = 12;
            lblGroupManagement.Text = "MANAGEMENT";
            lblGroupManagement.TextAlign = ContentAlignment.BottomLeft;
            // 
            // pnlNavDivider2
            // 
            pnlNavDivider2.BackColor = Color.FromArgb(13, 59, 102);
            pnlNavDivider2.Dock = DockStyle.Top;
            pnlNavDivider2.Location = new Point(0, 167);
            pnlNavDivider2.Name = "pnlNavDivider2";
            pnlNavDivider2.Size = new Size(220, 1);
            pnlNavDivider2.TabIndex = 21;
            // 
            // btnNavReturn
            // 
            btnNavReturn.BackColor = Color.FromArgb(63, 117, 162);
            btnNavReturn.Cursor = Cursors.Hand;
            btnNavReturn.Dock = DockStyle.Top;
            btnNavReturn.FlatAppearance.BorderColor = Color.FromArgb(13, 59, 102);
            btnNavReturn.FlatAppearance.MouseDownBackColor = Color.FromArgb(55, 105, 150);
            btnNavReturn.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 155, 213);
            btnNavReturn.FlatStyle = FlatStyle.Popup;
            btnNavReturn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnNavReturn.ForeColor = Color.White;
            btnNavReturn.ImageAlign = ContentAlignment.MiddleLeft;
            btnNavReturn.Location = new Point(0, 131);
            btnNavReturn.Margin = new Padding(4, 2, 4, 2);
            btnNavReturn.Name = "btnNavReturn";
            btnNavReturn.Padding = new Padding(16, 0, 0, 0);
            btnNavReturn.Size = new Size(220, 36);
            btnNavReturn.TabIndex = 4;
            btnNavReturn.Text = "   Return Books";
            btnNavReturn.TextAlign = ContentAlignment.MiddleLeft;
            btnNavReturn.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNavReturn.UseVisualStyleBackColor = false;
            // 
            // btnNavBorrow
            // 
            btnNavBorrow.BackColor = Color.FromArgb(63, 117, 162);
            btnNavBorrow.Cursor = Cursors.Hand;
            btnNavBorrow.Dock = DockStyle.Top;
            btnNavBorrow.FlatAppearance.BorderColor = Color.FromArgb(13, 59, 102);
            btnNavBorrow.FlatAppearance.MouseDownBackColor = Color.FromArgb(55, 105, 150);
            btnNavBorrow.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 155, 213);
            btnNavBorrow.FlatStyle = FlatStyle.Popup;
            btnNavBorrow.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnNavBorrow.ForeColor = Color.White;
            btnNavBorrow.ImageAlign = ContentAlignment.MiddleLeft;
            btnNavBorrow.Location = new Point(0, 95);
            btnNavBorrow.Margin = new Padding(4, 2, 4, 2);
            btnNavBorrow.Name = "btnNavBorrow";
            btnNavBorrow.Padding = new Padding(16, 0, 0, 0);
            btnNavBorrow.Size = new Size(220, 36);
            btnNavBorrow.TabIndex = 3;
            btnNavBorrow.Text = "   Borrow Books";
            btnNavBorrow.TextAlign = ContentAlignment.MiddleLeft;
            btnNavBorrow.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNavBorrow.UseVisualStyleBackColor = false;
            // 
            // lblGroupOperations
            // 
            lblGroupOperations.BackColor = Color.FromArgb(63, 117, 162);
            lblGroupOperations.Dock = DockStyle.Top;
            lblGroupOperations.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblGroupOperations.ForeColor = Color.FromArgb(208, 225, 253);
            lblGroupOperations.Location = new Point(0, 63);
            lblGroupOperations.Name = "lblGroupOperations";
            lblGroupOperations.Padding = new Padding(16, 12, 0, 4);
            lblGroupOperations.Size = new Size(220, 32);
            lblGroupOperations.TabIndex = 11;
            lblGroupOperations.Text = "OPERATIONS";
            lblGroupOperations.TextAlign = ContentAlignment.BottomLeft;
            // 
            // pnlNavDivider1
            // 
            pnlNavDivider1.BackColor = Color.FromArgb(13, 59, 102);
            pnlNavDivider1.Dock = DockStyle.Top;
            pnlNavDivider1.Location = new Point(0, 62);
            pnlNavDivider1.Name = "pnlNavDivider1";
            pnlNavDivider1.Size = new Size(220, 1);
            pnlNavDivider1.TabIndex = 20;
            // 
            // btnNavDashboard
            // 
            btnNavDashboard.BackColor = Color.FromArgb(63, 117, 162);
            btnNavDashboard.Cursor = Cursors.Hand;
            btnNavDashboard.Dock = DockStyle.Top;
            btnNavDashboard.FlatAppearance.BorderColor = Color.FromArgb(13, 59, 102);
            btnNavDashboard.FlatAppearance.MouseDownBackColor = Color.FromArgb(55, 105, 150);
            btnNavDashboard.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 155, 213);
            btnNavDashboard.FlatStyle = FlatStyle.Popup;
            btnNavDashboard.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnNavDashboard.ForeColor = Color.White;
            btnNavDashboard.ImageAlign = ContentAlignment.MiddleLeft;
            btnNavDashboard.Location = new Point(0, 26);
            btnNavDashboard.Margin = new Padding(4, 2, 4, 2);
            btnNavDashboard.Name = "btnNavDashboard";
            btnNavDashboard.Padding = new Padding(16, 0, 0, 0);
            btnNavDashboard.Size = new Size(220, 36);
            btnNavDashboard.TabIndex = 0;
            btnNavDashboard.Text = "   Dashboard";
            btnNavDashboard.TextAlign = ContentAlignment.MiddleLeft;
            btnNavDashboard.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNavDashboard.UseVisualStyleBackColor = false;
            // 
            // lblGroupMain
            // 
            lblGroupMain.BackColor = Color.FromArgb(63, 117, 162);
            lblGroupMain.Dock = DockStyle.Top;
            lblGroupMain.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblGroupMain.ForeColor = Color.FromArgb(208, 225, 253);
            lblGroupMain.Location = new Point(0, 0);
            lblGroupMain.Name = "lblGroupMain";
            lblGroupMain.Padding = new Padding(16, 6, 0, 4);
            lblGroupMain.Size = new Size(220, 26);
            lblGroupMain.TabIndex = 10;
            lblGroupMain.Text = "MAIN";
            lblGroupMain.TextAlign = ContentAlignment.BottomLeft;
            // 
            // pnlBottomSpacer
            // 
            pnlBottomSpacer.BackColor = Color.FromArgb(63, 117, 162);
            pnlBottomSpacer.Dock = DockStyle.Bottom;
            pnlBottomSpacer.Location = new Point(0, 656);
            pnlBottomSpacer.Name = "pnlBottomSpacer";
            pnlBottomSpacer.Size = new Size(220, 26);
            pnlBottomSpacer.TabIndex = 3;
            // 
            // pnlLogout
            // 
            pnlLogout.BackColor = Color.FromArgb(63, 117, 162);
            pnlLogout.BorderStyle = BorderStyle.Fixed3D;
            pnlLogout.Controls.Add(btnLogout);
            pnlLogout.Dock = DockStyle.Bottom;
            pnlLogout.Location = new Point(0, 682);
            pnlLogout.Name = "pnlLogout";
            pnlLogout.Size = new Size(220, 38);
            pnlLogout.TabIndex = 1;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.FromArgb(63, 117, 162);
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.Dock = DockStyle.Fill;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatAppearance.MouseOverBackColor = Color.FromArgb(254, 226, 226);
            btnLogout.FlatStyle = FlatStyle.Popup;
            btnLogout.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnLogout.ForeColor = Color.White;
            btnLogout.ImageAlign = ContentAlignment.MiddleLeft;
            btnLogout.Location = new Point(0, 0);
            btnLogout.Margin = new Padding(4, 2, 4, 2);
            btnLogout.Name = "btnLogout";
            btnLogout.Padding = new Padding(16, 0, 0, 0);
            btnLogout.Size = new Size(216, 34);
            btnLogout.TabIndex = 0;
            btnLogout.Text = "   Sign Out";
            btnLogout.TextAlign = ContentAlignment.MiddleLeft;
            btnLogout.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += BtnLogout_Click;
            // 
            // pnlBrand
            // 
            pnlBrand.BackColor = Color.FromArgb(63, 117, 162);
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
            lblBrandTitle.BackColor = Color.FromArgb(63, 117, 162);
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
            pnlLogout.ResumeLayout(false);
            pnlBrand.ResumeLayout(false);
            ResumeLayout(false);

        }
    }
}
