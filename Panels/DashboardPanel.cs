using LibraryManagementSystem.Helpers;

namespace LibraryManagementSystem.Panels
{
    /// <summary>
    /// Dashboard panel displaying real-time statistics and quick action shortcuts.
    /// Fully responsive with auto-reflowing card grids for all window widths down to 1280x720.
    /// 100% compatible with the Visual Studio WinForms Designer.
    /// </summary>
    public partial class DashboardPanel : UserControl
    {
        private readonly Action<string>? _navigate;

        /// <summary>Parameterless constructor for WinForms Designer.</summary>
        public DashboardPanel() : this(null)
        {
        }

        public DashboardPanel(Action<string>? navigate = null)
        {
            _navigate = navigate;
            InitializeComponent();

            this.Load += DashboardPanel_Load;
        }

        private void DashboardPanel_Load(object? sender, EventArgs e)
        {
            if (DesignMode) return;

          

            // Wire actions
            btnRefresh.Click += (s, ev) => LoadStatistics();
            btnQuickBorrow.Click += (s, ev) => _navigate?.Invoke("Borrow");
            btnQuickReturn.Click += (s, ev) => _navigate?.Invoke("Return");
            btnQuickBooks.Click += (s, ev) => _navigate?.Invoke("Books");
            btnQuickMembers.Click += (s, ev) => _navigate?.Invoke("Members");
            btnQuickReports.Click += (s, ev) => _navigate?.Invoke("Reports");

            // Update user info
            lblWelcome.Text = $"Welcome, {SessionManager.CurrentLibrarian?.Name ?? "Librarian"}";
            string roleDisplay = SessionManager.CurrentLibrarian?.Role == "Employee"
                ? "Librarian"
                : (SessionManager.CurrentLibrarian?.Role ?? "Librarian");
            lblSubtitle.Text = $" Role: {roleDisplay}  •  {DateTime.Now:dddd, MMMM dd, yyyy}";

            LoadStatistics();
        }

        public void LoadStatistics()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                using var context = Program.CreateDbContext();
                lblTotalBooks.Text = context.Books.Count().ToString("N0");
                lblTotalMembers.Text = context.Members.Count().ToString("N0");
                lblBorrowedBooks.Text = context.Borrows.Count(b => b.Status == "Active" || b.Status == "Overdue").ToString("N0");
                lblOverdueBooks.Text = context.Borrows.Count(b => b.Status == "Overdue").ToString("N0");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load statistics: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
