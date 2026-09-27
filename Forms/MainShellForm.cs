using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Panels;
using System.Drawing.Imaging;

namespace LibraryManagementSystem.Forms
{
    /// <summary>
    /// The primary Single-Window SPA Shell for the Library Management System.
    /// Hosts a responsive collapsible sidebar navigation and dynamically caches/docks active UserControl panels.
    /// 100% compatible with the Visual Studio WinForms Designer.
    /// </summary>
    public partial class MainShellForm : Form
    {
        public static MainShellForm? Instance { get; private set; }

        private Button? _activeNavButton;
        private readonly Dictionary<string, (Button Button, Func<UserControl> Creator, bool AdminOnly)> _navRegistry = new(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, UserControl> _panelCache = new(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<Button, string> _buttonTitles = new();
        private readonly ToolTip _navToolTip = new();
        private bool _isSidebarCollapsed = false;

        public MainShellForm()
        {
            Instance = this;
            InitializeComponent();

            // Strict dock layout and Z-order enforcement:
            pnlTopDivider.SendToBack();
            pnlSidebar.BringToFront();
            pnlContentHost.BringToFront();

            pnlContentHost.BackColor = UIHelper.MainBackground;

            this.Load += MainShellForm_Load;
            this.Resize += MainShellForm_Resize;
            lblLogoIcon.Click += (s, e) => SetSidebarCollapsed(!_isSidebarCollapsed);
            lblBrandTitle.Click += (s, e) => SetSidebarCollapsed(!_isSidebarCollapsed);
        }

        private void MainShellForm_Load(object? sender, EventArgs e)
        {
            if (DesignMode) return;

            InitNavRegistry();
            ApplySessionState();

            // Initial check for small screens
            if (this.ClientSize.Width < 1120)
            {
                SetSidebarCollapsed(true);
            }

            NavigateTo("Dashboard");
        }

        private void MainShellForm_Resize(object? sender, EventArgs e)
        {
            if (this.ClientSize.Width < 1120 && !_isSidebarCollapsed)
            {
                SetSidebarCollapsed(true);
            }
            else if (this.ClientSize.Width >= 1120 && _isSidebarCollapsed)
            {
                SetSidebarCollapsed(false);
            }
        }

        private void SetSidebarCollapsed(bool collapsed)
        {
            _isSidebarCollapsed = collapsed;
            pnlSidebar.Width = collapsed ? 64 : 220;
            lblBrandTitle.Visible = !collapsed;

            foreach (var kvp in _buttonTitles)
            {
                var btn = kvp.Key;
                var title = kvp.Value;

                if (collapsed)
                {
                    btn.Text = string.Empty;
                    btn.ImageAlign = ContentAlignment.MiddleCenter;
                    btn.Padding = Padding.Empty;
                }
                else
                {
                    btn.Text = "   " + title;
                    btn.ImageAlign = ContentAlignment.MiddleLeft;
                    btn.TextAlign = ContentAlignment.MiddleLeft;
                    btn.TextImageRelation = TextImageRelation.ImageBeforeText;
                    btn.Padding = new Padding(16, 0, 0, 0);
                }

                if (btn != btnLogout)
                {
                    btn.FlatAppearance.BorderSize = 1;
                    btn.FlatAppearance.BorderColor = Color.FromArgb(55, 105, 145);
                    btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 155, 213);
                    btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(55, 105, 150);
                }
            }
        }

        private static Image? LoadWhiteTintedIcon(string relativePath, int width = 18, int height = 18)
        {
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
            if (!File.Exists(fullPath))
            {
                fullPath = Path.Combine(Directory.GetCurrentDirectory(), relativePath);
            }
            if (!File.Exists(fullPath))
            {
                fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", relativePath);
            }
            if (!File.Exists(fullPath)) return null;

            using var orig = new Bitmap(fullPath);
            var resized = new Bitmap(orig, new Size(width, height));
            var tinted = new Bitmap(width, height);

            using var g = Graphics.FromImage(tinted);
            var colorMatrix = new ColorMatrix(new float[][]
            {
                new float[] {0, 0, 0, 0, 0},
                new float[] {0, 0, 0, 0, 0},
                new float[] {0, 0, 0, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {1, 1, 1, 0, 1}
            });
            using var attributes = new ImageAttributes();
            attributes.SetColorMatrix(colorMatrix);
            g.DrawImage(resized, new Rectangle(0, 0, width, height), 0, 0, width, height, GraphicsUnit.Pixel, attributes);
            return tinted;
        }

        private void InitNavRegistry()
        {
            _navRegistry["Dashboard"] = (btnNavDashboard, () => new DashboardPanel(NavigateTo), false);
            _navRegistry["Books"] = (btnNavBooks, () => new BooksPanel(), false);
            _navRegistry["Members"] = (btnNavMembers, () => new MembersPanel(), false);
            _navRegistry["Borrow"] = (btnNavBorrow, () => new BorrowPanel(), false);
            _navRegistry["Return"] = (btnNavReturn, () => new ReturnPanel(), false);
            _navRegistry["Reports"] = (btnNavReports, () => new ReportsPanel(), false);
            _navRegistry["Categories"] = (btnNavCategories, () => new CategoriesPanel(), true);
            _navRegistry["Authors"] = (btnNavAuthors, () => new AuthorsPanel(), true);
            _navRegistry["Publishers"] = (btnNavPublishers, () => new PublishersPanel(), true);
            _navRegistry["Librarians"] = (btnNavLibrarians, () => new LibrariansPanel(), true);

            // Register Titles for tooltip and collapsed sidebar support
            _buttonTitles[btnNavDashboard] = "Dashboard";
            _buttonTitles[btnNavBooks] = "Books";
            _buttonTitles[btnNavMembers] = "Members";
            _buttonTitles[btnNavBorrow] = "Borrow Books";
            _buttonTitles[btnNavReturn] = "Return Books";
            _buttonTitles[btnNavReports] = "Reports";
            _buttonTitles[btnNavCategories] = "Categories";
            _buttonTitles[btnNavAuthors] = "Authors";
            _buttonTitles[btnNavPublishers] = "Publishers";
            _buttonTitles[btnNavLibrarians] = "Librarians";
            _buttonTitles[btnLogout] = "Sign Out";

            // Bind PNG icons from 'icons/' tinted to white
            btnNavDashboard.Image = LoadWhiteTintedIcon("icons/dashborad.png");
            btnNavBooks.Image = LoadWhiteTintedIcon("icons/book.png");
            btnNavMembers.Image = LoadWhiteTintedIcon("icons/member.png");
            btnNavBorrow.Image = LoadWhiteTintedIcon("icons/borrow.png");
            btnNavReturn.Image = LoadWhiteTintedIcon("icons/return.png");
            btnNavReports.Image = LoadWhiteTintedIcon("icons/report.png");
            btnNavCategories.Image = LoadWhiteTintedIcon("icons/category.png");
            btnNavAuthors.Image = LoadWhiteTintedIcon("icons/author.png");
            btnNavPublishers.Image = LoadWhiteTintedIcon("icons/publisher.png");
            btnNavLibrarians.Image = LoadWhiteTintedIcon("icons/librarian.png");
            btnLogout.Image = LoadWhiteTintedIcon("icons/logout.png") ?? UIHelper.CreateIconBitmap(UIHelper.Icons.Logout, 18, Color.White);

            // Configure navigation buttons styling
            Color defaultNavBg = Color.SteelBlue;
            Color navBorderColor = Color.FromArgb(55, 105, 145);
            Color navHoverColor = Color.FromArgb(91, 155, 213);
            Color navDownColor = Color.FromArgb(55, 105, 150);

            foreach (var item in _navRegistry.Values)
            {
                var btn = item.Button;
                btn.FlatStyle = FlatStyle.Flat;
                btn.BackColor = defaultNavBg;
                btn.UseVisualStyleBackColor = false;
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = navBorderColor;
                btn.FlatAppearance.MouseOverBackColor = navHoverColor;
                btn.FlatAppearance.MouseDownBackColor = navDownColor;
            }

            foreach (var kvp in _buttonTitles)
            {
                var btn = kvp.Key;
                var title = kvp.Value;
                _navToolTip.SetToolTip(btn, title);
            }

            foreach (var kvp in _navRegistry)
            {
                string targetName = kvp.Key;
                kvp.Value.Button.Click += (s, e) => NavigateTo(targetName);
            }
        }

        private void ApplySessionState()
        {
            if (SessionManager.CurrentLibrarian != null)
            {
                lblUserName.Text = SessionManager.CurrentLibrarian.Name;
                string roleDisplay = SessionManager.CurrentLibrarian.Role == "Employee"
                    ? "Librarian"
                    : SessionManager.CurrentLibrarian.Role;
                lblUserRole.Text = roleDisplay;
            }
            else
            {
                lblUserName.Text = "Guest User";
                lblUserRole.Text = "Librarian";
            }

            bool isAdmin = SessionManager.IsAdmin;
            btnNavCategories.Visible = isAdmin;
            btnNavAuthors.Visible = isAdmin;
            btnNavPublishers.Visible = isAdmin;
            btnNavLibrarians.Visible = isAdmin;
        }

        public void InvalidateCache(string? panelName = null)
        {
            if (string.IsNullOrEmpty(panelName))
            {
                foreach (var p in _panelCache.Values) p.Dispose();
                _panelCache.Clear();
            }
            else if (_panelCache.TryGetValue(panelName, out var p))
            {
                p.Dispose();
                _panelCache.Remove(panelName);
            }
        }

        public void NavigateTo(string name)
        {
            if (!_navRegistry.TryGetValue(name, out var item)) return;

            if (item.AdminOnly && !SessionManager.IsAdmin)
            {
                MessageBox.Show(
                    "You do not have permission to access this section.\nAdministrator privileges are required.",
                    "Unauthorized Access",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            this.Text = $"Library Management System — {name}";
            SetActiveButton(item.Button);

            if (!_panelCache.TryGetValue(name, out var panel) || panel.IsDisposed)
            {
                panel = item.Creator();
                _panelCache[name] = panel;
            }

            if (panel is DashboardPanel dbPanel)
            {
                dbPanel.LoadStatistics();
            }

            NavigateTo(panel);
        }

        public void NavigateTo(UserControl panel)
        {
            pnlContentHost.SuspendLayout();
            pnlContentHost.Controls.Clear();
            panel.Dock = DockStyle.Fill;
            pnlContentHost.Controls.Add(panel);
            panel.BringToFront();
            pnlContentHost.ResumeLayout(true);
        }

        private void SetActiveButton(Button btn)
        {
            Color defaultBg = Color.SteelBlue;
            Color activeBg = Color.FromArgb(55, 105, 150); // slightly darker SteelBlue
            Color borderColor = Color.FromArgb(55, 105, 145);

            // Reset previous active button
            if (_activeNavButton != null && _activeNavButton != btn)
            {
                _activeNavButton.BackColor = defaultBg;
                _activeNavButton.FlatAppearance.BorderSize = 1;
                _activeNavButton.FlatAppearance.BorderColor = borderColor;
            }

            // Apply selected style to current button
            _activeNavButton = btn;
            _activeNavButton.BackColor = activeBg;
            _activeNavButton.FlatAppearance.BorderSize = 1;
            _activeNavButton.FlatAppearance.BorderColor = borderColor;
        }

        private void BtnLogout_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to log out of your session?", "Confirm Sign Out",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SessionManager.Clear();
                InvalidateCache();
                this.Hide();
                var loginForm = new LoginForm();
                loginForm.FormClosed += (s, args) => this.Close();
                loginForm.Show();
            }
        }
    }
}