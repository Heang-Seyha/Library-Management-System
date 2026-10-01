namespace LibraryManagementSystem.Panels
{
    partial class DashboardPanel
    {
        private System.ComponentModel.IContainer components = null;

        // Layout
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TableLayoutPanel tblLayout;

        // Header
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Button btnRefresh;

        // Stat cards
        private System.Windows.Forms.TableLayoutPanel tblCards;
        private System.Windows.Forms.Panel cardTotalBooks;
        private System.Windows.Forms.Label lblBooksIcon;
        private System.Windows.Forms.Label lblBooksTitle;
        private System.Windows.Forms.Label lblTotalBooks;
        private System.Windows.Forms.Panel cardTotalMembers;
        private System.Windows.Forms.Label lblMembersIcon;
        private System.Windows.Forms.Label lblMembersTitle;
        private System.Windows.Forms.Label lblTotalMembers;
        private System.Windows.Forms.Panel cardBorrowedBooks;
        private System.Windows.Forms.Label lblBorrowIcon;
        private System.Windows.Forms.Label lblBorrowTitle;
        private System.Windows.Forms.Label lblBorrowedBooks;
        private System.Windows.Forms.Panel cardOverdueBooks;
        private System.Windows.Forms.Label lblOverdueIcon;
        private System.Windows.Forms.Label lblOverdueTitle;
        private System.Windows.Forms.Label lblOverdueBooks;

        // Quick actions
        private System.Windows.Forms.Label lblActionsTitle;
        private System.Windows.Forms.TableLayoutPanel tblActions;
        private System.Windows.Forms.Button btnQuickBorrow;
        private System.Windows.Forms.Button btnQuickReturn;
        private System.Windows.Forms.Button btnQuickBooks;
        private System.Windows.Forms.Button btnQuickMembers;
        private System.Windows.Forms.Button btnQuickReports;

        // Chart + overdue list
        private System.Windows.Forms.TableLayoutPanel tblMiddle;
        private System.Windows.Forms.Panel cardChart;
        private System.Windows.Forms.Panel pnlChart;
        private System.Windows.Forms.Label lblChartTitle;
        private System.Windows.Forms.Panel cardOverdueList;
        private System.Windows.Forms.DataGridView dgvOverdue;
        private System.Windows.Forms.Label lblOverdueEmpty;
        private System.Windows.Forms.Label lblOverdueListTitle;

        // Recent borrows
        private System.Windows.Forms.Panel cardRecent;
        private System.Windows.Forms.DataGridView dgvRecent;
        private System.Windows.Forms.Label lblRecentEmpty;
        private System.Windows.Forms.Label lblRecentTitle;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlMain = new Panel();
            tblLayout = new TableLayoutPanel();
            pnlHeader = new Panel();
            lblWelcome = new Label();
            lblSubtitle = new Label();
            btnRefresh = new Button();
            tblCards = new TableLayoutPanel();
            cardTotalBooks = new Panel();
            lblBooksIcon = new Label();
            lblBooksTitle = new Label();
            lblTotalBooks = new Label();
            cardTotalMembers = new Panel();
            lblMembersIcon = new Label();
            lblMembersTitle = new Label();
            lblTotalMembers = new Label();
            cardBorrowedBooks = new Panel();
            lblBorrowIcon = new Label();
            lblBorrowTitle = new Label();
            lblBorrowedBooks = new Label();
            cardOverdueBooks = new Panel();
            lblOverdueIcon = new Label();
            lblOverdueTitle = new Label();
            lblOverdueBooks = new Label();
            lblActionsTitle = new Label();
            tblActions = new TableLayoutPanel();
            btnQuickBorrow = new Button();
            btnQuickReturn = new Button();
            btnQuickBooks = new Button();
            btnQuickMembers = new Button();
            btnQuickReports = new Button();
            tblMiddle = new TableLayoutPanel();
            cardChart = new Panel();
            pnlChart = new Panel();
            lblChartTitle = new Label();
            cardOverdueList = new Panel();
            dgvOverdue = new DataGridView();
            lblOverdueEmpty = new Label();
            lblOverdueListTitle = new Label();
            cardRecent = new Panel();
            dgvRecent = new DataGridView();
            lblRecentEmpty = new Label();
            lblRecentTitle = new Label();
            pnlMain.SuspendLayout();
            tblLayout.SuspendLayout();
            pnlHeader.SuspendLayout();
            tblCards.SuspendLayout();
            cardTotalBooks.SuspendLayout();
            cardTotalMembers.SuspendLayout();
            cardBorrowedBooks.SuspendLayout();
            cardOverdueBooks.SuspendLayout();
            tblActions.SuspendLayout();
            tblMiddle.SuspendLayout();
            cardChart.SuspendLayout();
            cardOverdueList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOverdue).BeginInit();
            cardRecent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecent).BeginInit();
            SuspendLayout();
            //
            // pnlMain
            //
            pnlMain.AutoScroll = true;
            pnlMain.BackColor = Color.FromArgb(235, 243, 250);
            pnlMain.Controls.Add(tblLayout);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(24, 16, 24, 16);
            pnlMain.Size = new Size(1034, 721);
            pnlMain.TabIndex = 0;
            //
            // tblLayout
            //
            tblLayout.AutoSize = true;
            tblLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tblLayout.BackColor = Color.Transparent;
            tblLayout.ColumnCount = 1;
            tblLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblLayout.Controls.Add(pnlHeader, 0, 0);
            tblLayout.Controls.Add(tblCards, 0, 1);
            tblLayout.Controls.Add(lblActionsTitle, 0, 2);
            tblLayout.Controls.Add(tblActions, 0, 3);
            tblLayout.Controls.Add(tblMiddle, 0, 4);
            tblLayout.Controls.Add(cardRecent, 0, 5);
            tblLayout.Dock = DockStyle.Top;
            tblLayout.Location = new Point(24, 16);
            tblLayout.Margin = new Padding(0);
            tblLayout.Name = "tblLayout";
            tblLayout.RowCount = 6;
            tblLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 104F));
            tblLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 124F));
            tblLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tblLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 84F));
            tblLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 336F));
            tblLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 300F));
            tblLayout.Size = new Size(986, 1000);
            tblLayout.TabIndex = 0;
            //
            // pnlHeader
            //
            pnlHeader.BackColor = Color.SteelBlue;
            pnlHeader.Controls.Add(btnRefresh);
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(lblWelcome);
            pnlHeader.Dock = DockStyle.Fill;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Margin = new Padding(0, 0, 0, 16);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(986, 88);
            pnlHeader.TabIndex = 0;
            //
            // lblWelcome
            //
            lblWelcome.AutoSize = true;
            lblWelcome.BackColor = Color.Transparent;
            lblWelcome.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
            lblWelcome.ForeColor = Color.White;
            lblWelcome.Location = new Point(24, 14);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(190, 30);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "Welcome, Librarian";
            //
            // lblSubtitle
            //
            lblSubtitle.AutoSize = true;
            lblSubtitle.BackColor = Color.Transparent;
            lblSubtitle.Font = new Font("Segoe UI", 9.5F);
            lblSubtitle.ForeColor = Color.FromArgb(227, 242, 253);
            lblSubtitle.Location = new Point(26, 52);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(150, 17);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Overview & Operations";
            //
            // btnRefresh
            //
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.BackColor = Color.SteelBlue;
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(208, 225, 253);
            btnRefresh.FlatAppearance.MouseDownBackColor = Color.FromArgb(48, 98, 142);
            btnRefresh.FlatAppearance.MouseOverBackColor = Color.FromArgb(58, 112, 158);
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(850, 27);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(112, 34);
            btnRefresh.TabIndex = 2;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            //
            // tblCards
            //
            tblCards.BackColor = Color.Transparent;
            tblCards.ColumnCount = 4;
            tblCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblCards.Controls.Add(cardTotalBooks, 0, 0);
            tblCards.Controls.Add(cardTotalMembers, 1, 0);
            tblCards.Controls.Add(cardBorrowedBooks, 2, 0);
            tblCards.Controls.Add(cardOverdueBooks, 3, 0);
            tblCards.Dock = DockStyle.Fill;
            tblCards.Location = new Point(0, 104);
            tblCards.Margin = new Padding(0, 0, 0, 16);
            tblCards.Name = "tblCards";
            tblCards.RowCount = 1;
            tblCards.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblCards.Size = new Size(986, 108);
            tblCards.TabIndex = 1;
            //
            // cardTotalBooks
            //
            cardTotalBooks.BackColor = Color.SteelBlue;
            cardTotalBooks.Controls.Add(lblBooksIcon);
            cardTotalBooks.Controls.Add(lblBooksTitle);
            cardTotalBooks.Controls.Add(lblTotalBooks);
            cardTotalBooks.Dock = DockStyle.Fill;
            cardTotalBooks.Margin = new Padding(0, 0, 14, 0);
            cardTotalBooks.Name = "cardTotalBooks";
            cardTotalBooks.Size = new Size(236, 108);
            cardTotalBooks.TabIndex = 0;
            //
            // lblBooksIcon
            //
            lblBooksIcon.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblBooksIcon.BackColor = Color.FromArgb(96, 150, 194);
            lblBooksIcon.Font = new Font("Segoe MDL2 Assets", 15F);
            lblBooksIcon.ForeColor = Color.White;
            lblBooksIcon.Location = new Point(180, 16);
            lblBooksIcon.Name = "lblBooksIcon";
            lblBooksIcon.Size = new Size(40, 40);
            lblBooksIcon.TabIndex = 2;
            lblBooksIcon.Text = "\uE736";
            lblBooksIcon.TextAlign = ContentAlignment.MiddleCenter;
            //
            // lblBooksTitle
            //
            lblBooksTitle.AutoSize = true;
            lblBooksTitle.BackColor = Color.Transparent;
            lblBooksTitle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblBooksTitle.ForeColor = Color.FromArgb(227, 242, 253);
            lblBooksTitle.Location = new Point(16, 18);
            lblBooksTitle.Name = "lblBooksTitle";
            lblBooksTitle.Size = new Size(90, 19);
            lblBooksTitle.TabIndex = 1;
            lblBooksTitle.Text = "Total Books";
            //
            // lblTotalBooks
            //
            lblTotalBooks.AutoSize = true;
            lblTotalBooks.BackColor = Color.Transparent;
            lblTotalBooks.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold);
            lblTotalBooks.ForeColor = Color.White;
            lblTotalBooks.Location = new Point(14, 48);
            lblTotalBooks.Name = "lblTotalBooks";
            lblTotalBooks.Size = new Size(50, 45);
            lblTotalBooks.TabIndex = 0;
            lblTotalBooks.Text = "...";
            //
            // cardTotalMembers
            //
            cardTotalMembers.BackColor = Color.SteelBlue;
            cardTotalMembers.Controls.Add(lblMembersIcon);
            cardTotalMembers.Controls.Add(lblMembersTitle);
            cardTotalMembers.Controls.Add(lblTotalMembers);
            cardTotalMembers.Dock = DockStyle.Fill;
            cardTotalMembers.Margin = new Padding(0, 0, 14, 0);
            cardTotalMembers.Name = "cardTotalMembers";
            cardTotalMembers.Size = new Size(236, 108);
            cardTotalMembers.TabIndex = 1;
            //
            // lblMembersIcon
            //
            lblMembersIcon.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblMembersIcon.BackColor = Color.FromArgb(96, 150, 194);
            lblMembersIcon.Font = new Font("Segoe MDL2 Assets", 15F);
            lblMembersIcon.ForeColor = Color.White;
            lblMembersIcon.Location = new Point(180, 16);
            lblMembersIcon.Name = "lblMembersIcon";
            lblMembersIcon.Size = new Size(40, 40);
            lblMembersIcon.TabIndex = 2;
            lblMembersIcon.Text = "\uE716";
            lblMembersIcon.TextAlign = ContentAlignment.MiddleCenter;
            //
            // lblMembersTitle
            //
            lblMembersTitle.AutoSize = true;
            lblMembersTitle.BackColor = Color.Transparent;
            lblMembersTitle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblMembersTitle.ForeColor = Color.FromArgb(227, 242, 253);
            lblMembersTitle.Location = new Point(16, 18);
            lblMembersTitle.Name = "lblMembersTitle";
            lblMembersTitle.Size = new Size(90, 19);
            lblMembersTitle.TabIndex = 1;
            lblMembersTitle.Text = "Total Members";
            //
            // lblTotalMembers
            //
            lblTotalMembers.AutoSize = true;
            lblTotalMembers.BackColor = Color.Transparent;
            lblTotalMembers.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold);
            lblTotalMembers.ForeColor = Color.White;
            lblTotalMembers.Location = new Point(14, 48);
            lblTotalMembers.Name = "lblTotalMembers";
            lblTotalMembers.Size = new Size(50, 45);
            lblTotalMembers.TabIndex = 0;
            lblTotalMembers.Text = "...";
            //
            // cardBorrowedBooks
            //
            cardBorrowedBooks.BackColor = Color.SteelBlue;
            cardBorrowedBooks.Controls.Add(lblBorrowIcon);
            cardBorrowedBooks.Controls.Add(lblBorrowTitle);
            cardBorrowedBooks.Controls.Add(lblBorrowedBooks);
            cardBorrowedBooks.Dock = DockStyle.Fill;
            cardBorrowedBooks.Margin = new Padding(0, 0, 14, 0);
            cardBorrowedBooks.Name = "cardBorrowedBooks";
            cardBorrowedBooks.Size = new Size(236, 108);
            cardBorrowedBooks.TabIndex = 2;
            //
            // lblBorrowIcon
            //
            lblBorrowIcon.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblBorrowIcon.BackColor = Color.FromArgb(96, 150, 194);
            lblBorrowIcon.Font = new Font("Segoe MDL2 Assets", 15F);
            lblBorrowIcon.ForeColor = Color.White;
            lblBorrowIcon.Location = new Point(180, 16);
            lblBorrowIcon.Name = "lblBorrowIcon";
            lblBorrowIcon.Size = new Size(40, 40);
            lblBorrowIcon.TabIndex = 2;
            lblBorrowIcon.Text = "\uE898";
            lblBorrowIcon.TextAlign = ContentAlignment.MiddleCenter;
            //
            // lblBorrowTitle
            //
            lblBorrowTitle.AutoSize = true;
            lblBorrowTitle.BackColor = Color.Transparent;
            lblBorrowTitle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblBorrowTitle.ForeColor = Color.FromArgb(227, 242, 253);
            lblBorrowTitle.Location = new Point(16, 18);
            lblBorrowTitle.Name = "lblBorrowTitle";
            lblBorrowTitle.Size = new Size(90, 19);
            lblBorrowTitle.TabIndex = 1;
            lblBorrowTitle.Text = "Active Borrows";
            //
            // lblBorrowedBooks
            //
            lblBorrowedBooks.AutoSize = true;
            lblBorrowedBooks.BackColor = Color.Transparent;
            lblBorrowedBooks.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold);
            lblBorrowedBooks.ForeColor = Color.White;
            lblBorrowedBooks.Location = new Point(14, 48);
            lblBorrowedBooks.Name = "lblBorrowedBooks";
            lblBorrowedBooks.Size = new Size(50, 45);
            lblBorrowedBooks.TabIndex = 0;
            lblBorrowedBooks.Text = "...";
            //
            // cardOverdueBooks
            //
            cardOverdueBooks.BackColor = Color.SteelBlue;
            cardOverdueBooks.Controls.Add(lblOverdueIcon);
            cardOverdueBooks.Controls.Add(lblOverdueTitle);
            cardOverdueBooks.Controls.Add(lblOverdueBooks);
            cardOverdueBooks.Dock = DockStyle.Fill;
            cardOverdueBooks.Margin = new Padding(0, 0, 0, 0);
            cardOverdueBooks.Name = "cardOverdueBooks";
            cardOverdueBooks.Size = new Size(236, 108);
            cardOverdueBooks.TabIndex = 3;
            //
            // lblOverdueIcon
            //
            lblOverdueIcon.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblOverdueIcon.BackColor = Color.FromArgb(96, 150, 194);
            lblOverdueIcon.Font = new Font("Segoe MDL2 Assets", 15F);
            lblOverdueIcon.ForeColor = Color.White;
            lblOverdueIcon.Location = new Point(180, 16);
            lblOverdueIcon.Name = "lblOverdueIcon";
            lblOverdueIcon.Size = new Size(40, 40);
            lblOverdueIcon.TabIndex = 2;
            lblOverdueIcon.Text = "\uE7BA";
            lblOverdueIcon.TextAlign = ContentAlignment.MiddleCenter;
            //
            // lblOverdueTitle
            //
            lblOverdueTitle.AutoSize = true;
            lblOverdueTitle.BackColor = Color.Transparent;
            lblOverdueTitle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblOverdueTitle.ForeColor = Color.FromArgb(227, 242, 253);
            lblOverdueTitle.Location = new Point(16, 18);
            lblOverdueTitle.Name = "lblOverdueTitle";
            lblOverdueTitle.Size = new Size(90, 19);
            lblOverdueTitle.TabIndex = 1;
            lblOverdueTitle.Text = "Overdue Borrows";
            //
            // lblOverdueBooks
            //
            lblOverdueBooks.AutoSize = true;
            lblOverdueBooks.BackColor = Color.Transparent;
            lblOverdueBooks.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold);
            lblOverdueBooks.ForeColor = Color.White;
            lblOverdueBooks.Location = new Point(14, 48);
            lblOverdueBooks.Name = "lblOverdueBooks";
            lblOverdueBooks.Size = new Size(50, 45);
            lblOverdueBooks.TabIndex = 0;
            lblOverdueBooks.Text = "...";
            //
            // lblActionsTitle
            //
            lblActionsTitle.Anchor = AnchorStyles.Left;
            lblActionsTitle.AutoSize = true;
            lblActionsTitle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblActionsTitle.ForeColor = Color.FromArgb(13, 59, 102);
            lblActionsTitle.Location = new Point(0, 232);
            lblActionsTitle.Margin = new Padding(0, 0, 0, 8);
            lblActionsTitle.Name = "lblActionsTitle";
            lblActionsTitle.Size = new Size(104, 20);
            lblActionsTitle.TabIndex = 2;
            lblActionsTitle.Text = "Quick Actions";
            //
            // tblActions
            //
            tblActions.BackColor = Color.Transparent;
            tblActions.ColumnCount = 5;
            tblActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tblActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tblActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tblActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tblActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tblActions.Controls.Add(btnQuickBorrow, 0, 0);
            tblActions.Controls.Add(btnQuickReturn, 1, 0);
            tblActions.Controls.Add(btnQuickBooks, 2, 0);
            tblActions.Controls.Add(btnQuickMembers, 3, 0);
            tblActions.Controls.Add(btnQuickReports, 4, 0);
            tblActions.Dock = DockStyle.Fill;
            tblActions.Location = new Point(0, 260);
            tblActions.Margin = new Padding(0, 0, 0, 16);
            tblActions.Name = "tblActions";
            tblActions.RowCount = 1;
            tblActions.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblActions.Size = new Size(986, 68);
            tblActions.TabIndex = 3;
            //
            // btnQuickBorrow
            //
            btnQuickBorrow.BackColor = Color.SteelBlue;
            btnQuickBorrow.Cursor = Cursors.Hand;
            btnQuickBorrow.Dock = DockStyle.Fill;
            btnQuickBorrow.FlatAppearance.BorderSize = 0;
            btnQuickBorrow.FlatAppearance.MouseDownBackColor = Color.FromArgb(48, 98, 142);
            btnQuickBorrow.FlatAppearance.MouseOverBackColor = Color.FromArgb(58, 112, 158);
            btnQuickBorrow.FlatStyle = FlatStyle.Flat;
            btnQuickBorrow.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnQuickBorrow.ForeColor = Color.White;
            btnQuickBorrow.Location = new Point(0, 0);
            btnQuickBorrow.Margin = new Padding(0, 0, 10, 0);
            btnQuickBorrow.Name = "btnQuickBorrow";
            btnQuickBorrow.Size = new Size(187, 68);
            btnQuickBorrow.TabIndex = 0;
            btnQuickBorrow.Text = "Borrow Books";
            btnQuickBorrow.UseVisualStyleBackColor = false;
            //
            // btnQuickReturn
            //
            btnQuickReturn.BackColor = Color.SteelBlue;
            btnQuickReturn.Cursor = Cursors.Hand;
            btnQuickReturn.Dock = DockStyle.Fill;
            btnQuickReturn.FlatAppearance.BorderSize = 0;
            btnQuickReturn.FlatAppearance.MouseDownBackColor = Color.FromArgb(48, 98, 142);
            btnQuickReturn.FlatAppearance.MouseOverBackColor = Color.FromArgb(58, 112, 158);
            btnQuickReturn.FlatStyle = FlatStyle.Flat;
            btnQuickReturn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnQuickReturn.ForeColor = Color.White;
            btnQuickReturn.Location = new Point(0, 0);
            btnQuickReturn.Margin = new Padding(0, 0, 10, 0);
            btnQuickReturn.Name = "btnQuickReturn";
            btnQuickReturn.Size = new Size(187, 68);
            btnQuickReturn.TabIndex = 1;
            btnQuickReturn.Text = "Return Books";
            btnQuickReturn.UseVisualStyleBackColor = false;
            //
            // btnQuickBooks
            //
            btnQuickBooks.BackColor = Color.SteelBlue;
            btnQuickBooks.Cursor = Cursors.Hand;
            btnQuickBooks.Dock = DockStyle.Fill;
            btnQuickBooks.FlatAppearance.BorderSize = 0;
            btnQuickBooks.FlatAppearance.MouseDownBackColor = Color.FromArgb(48, 98, 142);
            btnQuickBooks.FlatAppearance.MouseOverBackColor = Color.FromArgb(58, 112, 158);
            btnQuickBooks.FlatStyle = FlatStyle.Flat;
            btnQuickBooks.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnQuickBooks.ForeColor = Color.White;
            btnQuickBooks.Location = new Point(0, 0);
            btnQuickBooks.Margin = new Padding(0, 0, 10, 0);
            btnQuickBooks.Name = "btnQuickBooks";
            btnQuickBooks.Size = new Size(187, 68);
            btnQuickBooks.TabIndex = 2;
            btnQuickBooks.Text = "Manage Books";
            btnQuickBooks.UseVisualStyleBackColor = false;
            //
            // btnQuickMembers
            //
            btnQuickMembers.BackColor = Color.SteelBlue;
            btnQuickMembers.Cursor = Cursors.Hand;
            btnQuickMembers.Dock = DockStyle.Fill;
            btnQuickMembers.FlatAppearance.BorderSize = 0;
            btnQuickMembers.FlatAppearance.MouseDownBackColor = Color.FromArgb(48, 98, 142);
            btnQuickMembers.FlatAppearance.MouseOverBackColor = Color.FromArgb(58, 112, 158);
            btnQuickMembers.FlatStyle = FlatStyle.Flat;
            btnQuickMembers.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnQuickMembers.ForeColor = Color.White;
            btnQuickMembers.Location = new Point(0, 0);
            btnQuickMembers.Margin = new Padding(0, 0, 10, 0);
            btnQuickMembers.Name = "btnQuickMembers";
            btnQuickMembers.Size = new Size(187, 68);
            btnQuickMembers.TabIndex = 3;
            btnQuickMembers.Text = "Manage Members";
            btnQuickMembers.UseVisualStyleBackColor = false;
            //
            // btnQuickReports
            //
            btnQuickReports.BackColor = Color.SteelBlue;
            btnQuickReports.Cursor = Cursors.Hand;
            btnQuickReports.Dock = DockStyle.Fill;
            btnQuickReports.FlatAppearance.BorderSize = 0;
            btnQuickReports.FlatAppearance.MouseDownBackColor = Color.FromArgb(48, 98, 142);
            btnQuickReports.FlatAppearance.MouseOverBackColor = Color.FromArgb(58, 112, 158);
            btnQuickReports.FlatStyle = FlatStyle.Flat;
            btnQuickReports.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnQuickReports.ForeColor = Color.White;
            btnQuickReports.Location = new Point(0, 0);
            btnQuickReports.Margin = new Padding(0, 0, 0, 0);
            btnQuickReports.Name = "btnQuickReports";
            btnQuickReports.Size = new Size(187, 68);
            btnQuickReports.TabIndex = 4;
            btnQuickReports.Text = "View Reports";
            btnQuickReports.UseVisualStyleBackColor = false;
            //
            // tblMiddle
            //
            tblMiddle.BackColor = Color.Transparent;
            tblMiddle.ColumnCount = 2;
            tblMiddle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tblMiddle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tblMiddle.Controls.Add(cardChart, 0, 0);
            tblMiddle.Controls.Add(cardOverdueList, 1, 0);
            tblMiddle.Dock = DockStyle.Fill;
            tblMiddle.Location = new Point(0, 344);
            tblMiddle.Margin = new Padding(0, 0, 0, 16);
            tblMiddle.Name = "tblMiddle";
            tblMiddle.RowCount = 1;
            tblMiddle.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblMiddle.Size = new Size(986, 320);
            tblMiddle.TabIndex = 4;
            //
            // cardChart
            //
            cardChart.BackColor = Color.White;
            cardChart.Controls.Add(pnlChart);
            cardChart.Controls.Add(lblChartTitle);
            cardChart.Dock = DockStyle.Fill;
            cardChart.Margin = new Padding(0, 0, 14, 0);
            cardChart.Name = "cardChart";
            cardChart.Padding = new Padding(1);
            cardChart.Size = new Size(577, 320);
            cardChart.TabIndex = 0;
            cardChart.Paint += Card_Paint;
            //
            // pnlChart
            //
            pnlChart.BackColor = Color.White;
            pnlChart.Dock = DockStyle.Fill;
            pnlChart.Name = "pnlChart";
            pnlChart.Size = new Size(575, 270);
            pnlChart.TabIndex = 1;
            pnlChart.Paint += PnlChart_Paint;
            //
            // lblChartTitle
            //
            lblChartTitle.BackColor = Color.White;
            lblChartTitle.Dock = DockStyle.Top;
            lblChartTitle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblChartTitle.ForeColor = Color.FromArgb(13, 59, 102);
            lblChartTitle.Location = new Point(1, 1);
            lblChartTitle.Name = "lblChartTitle";
            lblChartTitle.Padding = new Padding(16, 0, 0, 0);
            lblChartTitle.Size = new Size(575, 48);
            lblChartTitle.TabIndex = 0;
            lblChartTitle.Text = "Borrows per Month (Last 6 Months)";
            lblChartTitle.TextAlign = ContentAlignment.MiddleLeft;
            //
            // cardOverdueList
            //
            cardOverdueList.BackColor = Color.White;
            cardOverdueList.Controls.Add(dgvOverdue);
            cardOverdueList.Controls.Add(lblOverdueEmpty);
            cardOverdueList.Controls.Add(lblOverdueListTitle);
            cardOverdueList.Dock = DockStyle.Fill;
            cardOverdueList.Margin = new Padding(0);
            cardOverdueList.Name = "cardOverdueList";
            cardOverdueList.Padding = new Padding(1);
            cardOverdueList.Size = new Size(395, 320);
            cardOverdueList.TabIndex = 1;
            cardOverdueList.Paint += Card_Paint;
            //
            // dgvOverdue
            //
            dgvOverdue.Dock = DockStyle.Fill;
            dgvOverdue.Name = "dgvOverdue";
            dgvOverdue.TabIndex = 2;
            //
            // lblOverdueEmpty
            //
            lblOverdueEmpty.BackColor = Color.White;
            lblOverdueEmpty.Dock = DockStyle.Fill;
            lblOverdueEmpty.Font = new Font("Segoe UI", 9.5F);
            lblOverdueEmpty.ForeColor = Color.FromArgb(100, 116, 139);
            lblOverdueEmpty.Name = "lblOverdueEmpty";
            lblOverdueEmpty.TabIndex = 1;
            lblOverdueEmpty.Text = "No overdue borrows. All books are on time.";
            lblOverdueEmpty.TextAlign = ContentAlignment.MiddleCenter;
            lblOverdueEmpty.Visible = false;
            //
            // lblOverdueListTitle
            //
            lblOverdueListTitle.BackColor = Color.White;
            lblOverdueListTitle.Dock = DockStyle.Top;
            lblOverdueListTitle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblOverdueListTitle.ForeColor = Color.FromArgb(13, 59, 102);
            lblOverdueListTitle.Location = new Point(1, 1);
            lblOverdueListTitle.Name = "lblOverdueListTitle";
            lblOverdueListTitle.Padding = new Padding(16, 0, 0, 0);
            lblOverdueListTitle.Size = new Size(393, 48);
            lblOverdueListTitle.TabIndex = 0;
            lblOverdueListTitle.Text = "Overdue Borrows";
            lblOverdueListTitle.TextAlign = ContentAlignment.MiddleLeft;
            //
            // cardRecent
            //
            cardRecent.BackColor = Color.White;
            cardRecent.Controls.Add(dgvRecent);
            cardRecent.Controls.Add(lblRecentEmpty);
            cardRecent.Controls.Add(lblRecentTitle);
            cardRecent.Dock = DockStyle.Fill;
            cardRecent.Margin = new Padding(0);
            cardRecent.Name = "cardRecent";
            cardRecent.Padding = new Padding(1);
            cardRecent.Size = new Size(986, 300);
            cardRecent.TabIndex = 5;
            cardRecent.Paint += Card_Paint;
            //
            // dgvRecent
            //
            dgvRecent.Dock = DockStyle.Fill;
            dgvRecent.Name = "dgvRecent";
            dgvRecent.TabIndex = 2;
            //
            // lblRecentEmpty
            //
            lblRecentEmpty.BackColor = Color.White;
            lblRecentEmpty.Dock = DockStyle.Fill;
            lblRecentEmpty.Font = new Font("Segoe UI", 9.5F);
            lblRecentEmpty.ForeColor = Color.FromArgb(100, 116, 139);
            lblRecentEmpty.Name = "lblRecentEmpty";
            lblRecentEmpty.TabIndex = 1;
            lblRecentEmpty.Text = "No borrow records yet.";
            lblRecentEmpty.TextAlign = ContentAlignment.MiddleCenter;
            lblRecentEmpty.Visible = false;
            //
            // lblRecentTitle
            //
            lblRecentTitle.BackColor = Color.White;
            lblRecentTitle.Dock = DockStyle.Top;
            lblRecentTitle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblRecentTitle.ForeColor = Color.FromArgb(13, 59, 102);
            lblRecentTitle.Location = new Point(1, 1);
            lblRecentTitle.Name = "lblRecentTitle";
            lblRecentTitle.Padding = new Padding(16, 0, 0, 0);
            lblRecentTitle.Size = new Size(984, 48);
            lblRecentTitle.TabIndex = 0;
            lblRecentTitle.Text = "Recent Borrows";
            lblRecentTitle.TextAlign = ContentAlignment.MiddleLeft;
            //
            // DashboardPanel
            //
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(235, 243, 250);
            Controls.Add(pnlMain);
            Font = new Font("Segoe UI", 9.5F);
            Name = "DashboardPanel";
            Size = new Size(1034, 721);
            pnlMain.ResumeLayout(false);
            pnlMain.PerformLayout();
            tblLayout.ResumeLayout(false);
            tblLayout.PerformLayout();
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            tblCards.ResumeLayout(false);
            cardTotalBooks.ResumeLayout(false);
            cardTotalBooks.PerformLayout();
            cardTotalMembers.ResumeLayout(false);
            cardTotalMembers.PerformLayout();
            cardBorrowedBooks.ResumeLayout(false);
            cardBorrowedBooks.PerformLayout();
            cardOverdueBooks.ResumeLayout(false);
            cardOverdueBooks.PerformLayout();
            tblActions.ResumeLayout(false);
            tblMiddle.ResumeLayout(false);
            cardChart.ResumeLayout(false);
            cardOverdueList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvOverdue).EndInit();
            cardRecent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvRecent).EndInit();
            ResumeLayout(false);
        }
    }
}
