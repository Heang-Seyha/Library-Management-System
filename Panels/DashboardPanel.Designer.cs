namespace LibraryManagementSystem.Panels
{
    partial class DashboardPanel
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Button btnRefresh;


        private System.Windows.Forms.FlowLayoutPanel flpCards;

        // Stat Card 1: Books
        private System.Windows.Forms.Panel cardTotalBooks;
        private System.Windows.Forms.Label lblBooksIcon;
        private System.Windows.Forms.Label lblBooksTitle;
        private System.Windows.Forms.Label lblTotalBooks;

        // Stat Card 2: Members
        private System.Windows.Forms.Panel cardTotalMembers;
        private System.Windows.Forms.Label lblMembersIcon;
        private System.Windows.Forms.Label lblMembersTitle;
        private System.Windows.Forms.Label lblTotalMembers;

        // Stat Card 3: Borrows
        private System.Windows.Forms.Panel cardBorrowedBooks;
        private System.Windows.Forms.Label lblBorrowIcon;
        private System.Windows.Forms.Label lblBorrowTitle;
        private System.Windows.Forms.Label lblBorrowedBooks;

        // Stat Card 4: Overdue
        private System.Windows.Forms.Panel cardOverdueBooks;
        private System.Windows.Forms.Label lblOverdueIcon;
        private System.Windows.Forms.Label lblOverdueTitle;
        private System.Windows.Forms.Label lblOverdueBooks;

        private System.Windows.Forms.Label lblActionsTitle;
        private System.Windows.Forms.FlowLayoutPanel flpActions;
        private System.Windows.Forms.Button btnQuickBorrow;
        private System.Windows.Forms.Button btnQuickReturn;
        private System.Windows.Forms.Button btnQuickBooks;
        private System.Windows.Forms.Button btnQuickMembers;
        private System.Windows.Forms.Button btnQuickReports;
        private System.Windows.Forms.TableLayoutPanel tblLayout;

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
            btnRefresh = new Button();
            lblSubtitle = new Label();
            lblWelcome = new Label();
            flpCards = new FlowLayoutPanel();
            cardTotalBooks = new Panel();
            lblTotalBooks = new Label();
            lblBooksTitle = new Label();
            lblBooksIcon = new Label();
            cardTotalMembers = new Panel();
            lblTotalMembers = new Label();
            lblMembersTitle = new Label();
            lblMembersIcon = new Label();
            cardBorrowedBooks = new Panel();
            lblBorrowedBooks = new Label();
            lblBorrowTitle = new Label();
            lblBorrowIcon = new Label();
            cardOverdueBooks = new Panel();
            lblOverdueBooks = new Label();
            lblOverdueTitle = new Label();
            lblOverdueIcon = new Label();
            lblActionsTitle = new Label();
            flpActions = new FlowLayoutPanel();
            btnQuickBorrow = new Button();
            btnQuickReturn = new Button();
            btnQuickBooks = new Button();
            btnQuickMembers = new Button();
            btnQuickReports = new Button();
            pnlMain.SuspendLayout();
            tblLayout.SuspendLayout();
            pnlHeader.SuspendLayout();
            flpCards.SuspendLayout();
            cardTotalBooks.SuspendLayout();
            cardTotalMembers.SuspendLayout();
            cardBorrowedBooks.SuspendLayout();
            cardOverdueBooks.SuspendLayout();
            flpActions.SuspendLayout();
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
            tblLayout.ColumnCount = 1;
            tblLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblLayout.Controls.Add(pnlHeader, 0, 0);
            tblLayout.Controls.Add(flpCards, 0, 1);
            tblLayout.Controls.Add(lblActionsTitle, 0, 2);
            tblLayout.Controls.Add(flpActions, 0, 3);
            tblLayout.Dock = DockStyle.Top;
            tblLayout.Location = new Point(24, 16);
            tblLayout.Margin = new Padding(0);
            tblLayout.Name = "tblLayout";
            tblLayout.RowCount = 4;
            tblLayout.RowStyles.Add(new RowStyle());
            tblLayout.RowStyles.Add(new RowStyle());
            tblLayout.RowStyles.Add(new RowStyle());
            tblLayout.RowStyles.Add(new RowStyle());
            tblLayout.Size = new Size(986, 356);
            tblLayout.TabIndex = 0;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.SteelBlue;
            pnlHeader.Controls.Add(btnRefresh);
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(lblWelcome);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Margin = new Padding(0, 0, 0, 16);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(16, 8, 16, 8);
            pnlHeader.Size = new Size(986, 95);
            pnlHeader.TabIndex = 0;
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.BackColor = Color.SteelBlue;
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(208, 225, 253);
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.Transparent;
            btnRefresh.Location = new Point(867, 36);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(100, 32);
            btnRefresh.TabIndex = 2;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = false;
            lblSubtitle.Dock = DockStyle.Top;
            lblSubtitle.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSubtitle.ForeColor = Color.FromArgb(227, 242, 253);
            lblSubtitle.Location = new Point(16, 52);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(954, 28);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Overview & Operations";
            lblSubtitle.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = false;
            lblWelcome.Dock = DockStyle.Top;
            lblWelcome.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            lblWelcome.ForeColor = Color.White;
            lblWelcome.Location = new Point(16, 8);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(954, 44);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "Welcome, Librarian";
            lblWelcome.TextAlign = ContentAlignment.BottomCenter;
            // 
            // flpCards
            // 
            flpCards.AutoSize = true;
            flpCards.BackColor = Color.Transparent;
            flpCards.Controls.Add(cardTotalBooks);
            flpCards.Controls.Add(cardTotalMembers);
            flpCards.Controls.Add(cardBorrowedBooks);
            flpCards.Controls.Add(cardOverdueBooks);
            flpCards.Dock = DockStyle.Top;
            flpCards.Location = new Point(0, 111);
            flpCards.Margin = new Padding(0, 0, 0, 16);
            flpCards.Name = "flpCards";
            flpCards.Size = new Size(986, 110);
            flpCards.TabIndex = 1;
            // 
            // cardTotalBooks
            // 
            cardTotalBooks.BackColor = Color.SteelBlue;
            cardTotalBooks.BorderStyle = BorderStyle.Fixed3D;
            cardTotalBooks.Controls.Add(lblTotalBooks);
            cardTotalBooks.Controls.Add(lblBooksTitle);
            cardTotalBooks.Controls.Add(lblBooksIcon);
            cardTotalBooks.Location = new Point(0, 0);
            cardTotalBooks.Margin = new Padding(0, 0, 14, 14);
            cardTotalBooks.Name = "cardTotalBooks";
            cardTotalBooks.Padding = new Padding(12);
            cardTotalBooks.Size = new Size(205, 96);
            cardTotalBooks.TabIndex = 0;
            // 
            // lblTotalBooks
            // 
            lblTotalBooks.AutoEllipsis = true;
            lblTotalBooks.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTotalBooks.ForeColor = Color.White;
            lblTotalBooks.Location = new Point(10, 48);
            lblTotalBooks.Name = "lblTotalBooks";
            lblTotalBooks.Size = new Size(182, 38);
            lblTotalBooks.TabIndex = 2;
            lblTotalBooks.Text = "...";
            lblTotalBooks.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblBooksTitle
            // 
            lblBooksTitle.AutoEllipsis = true;
            lblBooksTitle.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold);
            lblBooksTitle.ForeColor = Color.White;
            lblBooksTitle.Location = new Point(44, 12);
            lblBooksTitle.Name = "lblBooksTitle";
            lblBooksTitle.Size = new Size(148, 20);
            lblBooksTitle.TabIndex = 1;
            lblBooksTitle.Text = "Total Books";
            // 
            // lblBooksIcon
            // 
            lblBooksIcon.Font = new Font("Segoe MDL2 Assets", 15F);
            lblBooksIcon.ForeColor = Color.White;
            lblBooksIcon.Location = new Point(10, 8);
            lblBooksIcon.Name = "lblBooksIcon";
            lblBooksIcon.Size = new Size(28, 26);
            lblBooksIcon.TabIndex = 0;
            lblBooksIcon.Text = "";
            lblBooksIcon.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cardTotalMembers
            // 
            cardTotalMembers.BackColor = Color.SteelBlue;
            cardTotalMembers.BorderStyle = BorderStyle.Fixed3D;
            cardTotalMembers.Controls.Add(lblTotalMembers);
            cardTotalMembers.Controls.Add(lblMembersTitle);
            cardTotalMembers.Controls.Add(lblMembersIcon);
            cardTotalMembers.Location = new Point(219, 0);
            cardTotalMembers.Margin = new Padding(0, 0, 14, 14);
            cardTotalMembers.Name = "cardTotalMembers";
            cardTotalMembers.Padding = new Padding(12);
            cardTotalMembers.Size = new Size(205, 96);
            cardTotalMembers.TabIndex = 1;
            // 
            // lblTotalMembers
            // 
            lblTotalMembers.AutoEllipsis = true;
            lblTotalMembers.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTotalMembers.ForeColor = Color.White;
            lblTotalMembers.Location = new Point(10, 48);
            lblTotalMembers.Name = "lblTotalMembers";
            lblTotalMembers.Size = new Size(182, 38);
            lblTotalMembers.TabIndex = 2;
            lblTotalMembers.Text = "...";
            lblTotalMembers.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblMembersTitle
            // 
            lblMembersTitle.AutoEllipsis = true;
            lblMembersTitle.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold);
            lblMembersTitle.ForeColor = Color.White;
            lblMembersTitle.Location = new Point(44, 12);
            lblMembersTitle.Name = "lblMembersTitle";
            lblMembersTitle.Size = new Size(148, 20);
            lblMembersTitle.TabIndex = 1;
            lblMembersTitle.Text = "Total Members";
            // 
            // lblMembersIcon
            // 
            lblMembersIcon.Font = new Font("Segoe MDL2 Assets", 15F);
            lblMembersIcon.ForeColor = Color.White;
            lblMembersIcon.Location = new Point(10, 8);
            lblMembersIcon.Name = "lblMembersIcon";
            lblMembersIcon.Size = new Size(28, 26);
            lblMembersIcon.TabIndex = 0;
            lblMembersIcon.Text = "";
            lblMembersIcon.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cardBorrowedBooks
            // 
            cardBorrowedBooks.BackColor = Color.SteelBlue;
            cardBorrowedBooks.BorderStyle = BorderStyle.Fixed3D;
            cardBorrowedBooks.Controls.Add(lblBorrowedBooks);
            cardBorrowedBooks.Controls.Add(lblBorrowTitle);
            cardBorrowedBooks.Controls.Add(lblBorrowIcon);
            cardBorrowedBooks.Location = new Point(438, 0);
            cardBorrowedBooks.Margin = new Padding(0, 0, 14, 14);
            cardBorrowedBooks.Name = "cardBorrowedBooks";
            cardBorrowedBooks.Padding = new Padding(12);
            cardBorrowedBooks.Size = new Size(205, 96);
            cardBorrowedBooks.TabIndex = 2;
            // 
            // lblBorrowedBooks
            // 
            lblBorrowedBooks.AutoEllipsis = true;
            lblBorrowedBooks.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblBorrowedBooks.ForeColor = Color.White;
            lblBorrowedBooks.Location = new Point(10, 48);
            lblBorrowedBooks.Name = "lblBorrowedBooks";
            lblBorrowedBooks.Size = new Size(182, 38);
            lblBorrowedBooks.TabIndex = 2;
            lblBorrowedBooks.Text = "...";
            lblBorrowedBooks.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblBorrowTitle
            // 
            lblBorrowTitle.AutoEllipsis = true;
            lblBorrowTitle.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold);
            lblBorrowTitle.ForeColor = Color.White;
            lblBorrowTitle.Location = new Point(44, 12);
            lblBorrowTitle.Name = "lblBorrowTitle";
            lblBorrowTitle.Size = new Size(148, 20);
            lblBorrowTitle.TabIndex = 1;
            lblBorrowTitle.Text = "Active Borrows";
            // 
            // lblBorrowIcon
            // 
            lblBorrowIcon.Font = new Font("Segoe MDL2 Assets", 15F);
            lblBorrowIcon.ForeColor = Color.White;
            lblBorrowIcon.Location = new Point(10, 8);
            lblBorrowIcon.Name = "lblBorrowIcon";
            lblBorrowIcon.Size = new Size(28, 26);
            lblBorrowIcon.TabIndex = 0;
            lblBorrowIcon.Text = "";
            lblBorrowIcon.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cardOverdueBooks
            // 
            cardOverdueBooks.BackColor = Color.SteelBlue;
            cardOverdueBooks.BorderStyle = BorderStyle.Fixed3D;
            cardOverdueBooks.Controls.Add(lblOverdueBooks);
            cardOverdueBooks.Controls.Add(lblOverdueTitle);
            cardOverdueBooks.Controls.Add(lblOverdueIcon);
            cardOverdueBooks.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold);
            cardOverdueBooks.Location = new Point(657, 0);
            cardOverdueBooks.Margin = new Padding(0, 0, 14, 14);
            cardOverdueBooks.Name = "cardOverdueBooks";
            cardOverdueBooks.Padding = new Padding(12);
            cardOverdueBooks.Size = new Size(225, 96);
            cardOverdueBooks.TabIndex = 3;
            // 
            // lblOverdueBooks
            // 
            lblOverdueBooks.AutoEllipsis = true;
            lblOverdueBooks.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblOverdueBooks.ForeColor = Color.White;
            lblOverdueBooks.Location = new Point(10, 48);
            lblOverdueBooks.Name = "lblOverdueBooks";
            lblOverdueBooks.Size = new Size(182, 38);
            lblOverdueBooks.TabIndex = 2;
            lblOverdueBooks.Text = "...";
            lblOverdueBooks.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblOverdueTitle
            // 
            lblOverdueTitle.AutoEllipsis = true;
            lblOverdueTitle.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold);
            lblOverdueTitle.ForeColor = Color.White;
            lblOverdueTitle.Location = new Point(44, 12);
            lblOverdueTitle.Name = "lblOverdueTitle";
            lblOverdueTitle.Size = new Size(162, 20);
            lblOverdueTitle.TabIndex = 1;
            lblOverdueTitle.Text = "Overdue Borrows";
            // 
            // lblOverdueIcon
            // 
            lblOverdueIcon.Font = new Font("Segoe MDL2 Assets", 15F);
            lblOverdueIcon.ForeColor = Color.White;
            lblOverdueIcon.Location = new Point(10, 8);
            lblOverdueIcon.Name = "lblOverdueIcon";
            lblOverdueIcon.Size = new Size(28, 26);
            lblOverdueIcon.TabIndex = 0;
            lblOverdueIcon.Text = "";
            lblOverdueIcon.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblActionsTitle
            // 
            lblActionsTitle.AutoSize = true;
            lblActionsTitle.Dock = DockStyle.Top;
            lblActionsTitle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblActionsTitle.ForeColor = Color.FromArgb(13, 59, 102);
            lblActionsTitle.Location = new Point(0, 237);
            lblActionsTitle.Margin = new Padding(0, 0, 0, 8);
            lblActionsTitle.Name = "lblActionsTitle";
            lblActionsTitle.Size = new Size(986, 25);
            lblActionsTitle.TabIndex = 2;
            lblActionsTitle.Text = "Quick Actions";
            // 
            // flpActions
            // 
            flpActions.AutoSize = true;
            flpActions.BackColor = Color.Transparent;
            flpActions.Controls.Add(btnQuickBorrow);
            flpActions.Controls.Add(btnQuickReturn);
            flpActions.Controls.Add(btnQuickBooks);
            flpActions.Controls.Add(btnQuickMembers);
            flpActions.Controls.Add(btnQuickReports);
            flpActions.Dock = DockStyle.Top;
            flpActions.Location = new Point(0, 270);
            flpActions.Margin = new Padding(0, 0, 0, 16);
            flpActions.Name = "flpActions";
            flpActions.Size = new Size(986, 70);
            flpActions.TabIndex = 3;
            // 
            // btnQuickBorrow
            // 
            btnQuickBorrow.BackColor = Color.SteelBlue;
            btnQuickBorrow.Cursor = Cursors.Hand;
            btnQuickBorrow.FlatAppearance.BorderSize = 0;
            btnQuickBorrow.FlatStyle = FlatStyle.Flat;
            btnQuickBorrow.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnQuickBorrow.ForeColor = Color.Transparent;
            btnQuickBorrow.Location = new Point(0, 0);
            btnQuickBorrow.Margin = new Padding(0, 0, 10, 10);
            btnQuickBorrow.Name = "btnQuickBorrow";
            btnQuickBorrow.Size = new Size(180, 60);
            btnQuickBorrow.TabIndex = 0;
            btnQuickBorrow.Text = "Borrow Books";
            btnQuickBorrow.UseVisualStyleBackColor = false;
            // 
            // btnQuickReturn
            // 
            btnQuickReturn.BackColor = Color.SteelBlue;
            btnQuickReturn.Cursor = Cursors.Hand;
            btnQuickReturn.FlatAppearance.BorderSize = 0;
            btnQuickReturn.FlatStyle = FlatStyle.Flat;
            btnQuickReturn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnQuickReturn.ForeColor = Color.Transparent;
            btnQuickReturn.Location = new Point(190, 0);
            btnQuickReturn.Margin = new Padding(0, 0, 10, 10);
            btnQuickReturn.Name = "btnQuickReturn";
            btnQuickReturn.Size = new Size(169, 60);
            btnQuickReturn.TabIndex = 1;
            btnQuickReturn.Text = "Return Books";
            btnQuickReturn.UseVisualStyleBackColor = false;
            // 
            // btnQuickBooks
            // 
            btnQuickBooks.BackColor = Color.SteelBlue;
            btnQuickBooks.Cursor = Cursors.Hand;
            btnQuickBooks.FlatAppearance.BorderColor = Color.FromArgb(208, 225, 253);
            btnQuickBooks.FlatStyle = FlatStyle.Flat;
            btnQuickBooks.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnQuickBooks.ForeColor = Color.Transparent;
            btnQuickBooks.Location = new Point(369, 0);
            btnQuickBooks.Margin = new Padding(0, 0, 10, 10);
            btnQuickBooks.Name = "btnQuickBooks";
            btnQuickBooks.Size = new Size(187, 60);
            btnQuickBooks.TabIndex = 2;
            btnQuickBooks.Text = "Manage Books";
            btnQuickBooks.UseVisualStyleBackColor = false;
            // 
            // btnQuickMembers
            // 
            btnQuickMembers.BackColor = Color.SteelBlue;
            btnQuickMembers.Cursor = Cursors.Hand;
            btnQuickMembers.FlatAppearance.BorderColor = Color.FromArgb(208, 225, 253);
            btnQuickMembers.FlatStyle = FlatStyle.Flat;
            btnQuickMembers.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnQuickMembers.ForeColor = Color.Transparent;
            btnQuickMembers.Location = new Point(566, 0);
            btnQuickMembers.Margin = new Padding(0, 0, 10, 10);
            btnQuickMembers.Name = "btnQuickMembers";
            btnQuickMembers.Size = new Size(214, 60);
            btnQuickMembers.TabIndex = 3;
            btnQuickMembers.Text = "Manage Members";
            btnQuickMembers.UseVisualStyleBackColor = false;
            // 
            // btnQuickReports
            // 
            btnQuickReports.BackColor = Color.SteelBlue;
            btnQuickReports.Cursor = Cursors.Hand;
            btnQuickReports.FlatAppearance.BorderColor = Color.FromArgb(208, 225, 253);
            btnQuickReports.FlatStyle = FlatStyle.Flat;
            btnQuickReports.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnQuickReports.ForeColor = Color.Transparent;
            btnQuickReports.Location = new Point(790, 0);
            btnQuickReports.Margin = new Padding(0, 0, 10, 10);
            btnQuickReports.Name = "btnQuickReports";
            btnQuickReports.Size = new Size(186, 60);
            btnQuickReports.TabIndex = 4;
            btnQuickReports.Text = "View Reports";
            btnQuickReports.UseVisualStyleBackColor = false;
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
            flpCards.ResumeLayout(false);
            cardTotalBooks.ResumeLayout(false);
            cardTotalMembers.ResumeLayout(false);
            cardBorrowedBooks.ResumeLayout(false);
            cardOverdueBooks.ResumeLayout(false);
            flpActions.ResumeLayout(false);
            ResumeLayout(false);

        }
    }
}
