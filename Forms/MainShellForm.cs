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
        private bool _isLogoutPressed = false;
        private bool _bypassCloseConfirmation = false;
        private FormWindowState _previousWindowState = FormWindowState.Maximized;
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_RESTORE = 0xF120;

        public MainShellForm()
        {
            Instance = this;
            InitializeComponent();
            this.Icon = UIHelper.AppIcon;

            UIHelper.ApplyPaddingToAllTextBoxes(this, 8);

            // Strict dock layout and Z-order enforcement:
            pnlTopDivider.SendToBack();
            pnlSidebar.BringToFront();
            pnlContentHost.BringToFront();

            pnlContentHost.BackColor = UIHelper.MainBackground;
            pnlNavButtons.AutoScroll = false;

            this.Load += MainShellForm_Load;
            this.Shown += MainShellForm_Shown;
            this.Resize += MainShellForm_Resize;
            pnlSidebar.Resize += (s, e) => AdjustNavButtonHeights();
            lblLogoIcon.Click += (s, e) => SetSidebarCollapsed(!_isSidebarCollapsed);
            lblBrandTitle.Click += (s, e) => SetSidebarCollapsed(!_isSidebarCollapsed);
        }

        private void MainShellForm_Load(object? sender, EventArgs e)
        {
            if (DesignMode) return;

            InitNavRegistry();
            ApplySessionState();

            CenterNormalBounds();

            // Initial check for small screens
            if (this.ClientSize.Width < 1120)
            {
                SetSidebarCollapsed(true);
            }

            AdjustNavButtonHeights();
        }

        private void MainShellForm_Shown(object? sender, EventArgs e)
        {
            if (DesignMode) return;

            if (SessionManager.CurrentLibrarian == null)
            {
                if (!PromptLogin())
                {
                    _bypassCloseConfirmation = true;
                    this.Close();
                    Application.Exit();
                }
            }
        }

        private bool PromptLogin()
        {
            this.Refresh();
            using var loginForm = new LoginForm();
            loginForm.StartPosition = FormStartPosition.CenterParent;
            if (loginForm.ShowDialog(this) == DialogResult.OK)
            {
                ApplySessionState();
                NavigateTo("Dashboard");
                return true;
            }
            return false;
        }

        private void MainShellForm_Resize(object? sender, EventArgs e)
        {
            if (_previousWindowState == FormWindowState.Maximized && this.WindowState == FormWindowState.Normal)
            {
                if ((Control.MouseButtons & MouseButtons.Left) == 0)
                {
                    CenterNormalBounds();
                    this.BeginInvoke(new Action(CenterNormalBounds));
                }
            }
            _previousWindowState = this.WindowState;

            if (this.ClientSize.Width < 1120 && !_isSidebarCollapsed)
            {
                SetSidebarCollapsed(true);
            }
            else if (this.ClientSize.Width >= 1120 && _isSidebarCollapsed)
            {
                SetSidebarCollapsed(false);
            }

            AdjustNavButtonHeights();
        }

        private void SetSidebarCollapsed(bool collapsed)
        {
            _isSidebarCollapsed = collapsed;
            pnlSidebar.Width = collapsed ? 64 : 225;
            lblBrandTitle.Visible = !collapsed;
            lblLogoIcon.Location = collapsed ? new Point(16, 26) : new Point(12, 30);

            lblGroupMain.Visible = !collapsed;
            lblGroupOperations.Visible = !collapsed;
            lblGroupManagement.Visible = !collapsed;
            lblGroupMetadata.Visible = !collapsed;
            lblGroupAdmin.Visible = !collapsed;

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

                btn.FlatStyle = FlatStyle.Popup;
                btn.Cursor = Cursors.Hand;
                btn.Margin = new Padding(4, 2, 4, 2);

                if (btn != btnLogout)
                {
                    btn.FlatAppearance.BorderSize = 1;
                    btn.FlatAppearance.BorderColor = Color.FromArgb(13, 59, 102);
                    btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 155, 213);
                    btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(55, 105, 150);
                }
                else
                {
                    btn.FlatAppearance.BorderSize = 0;
                }
            }

            AdjustNavButtonHeights();
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
            _navRegistry["Categories"] = (btnNavCategories, () => new CategoriesPanel(), false);
            _navRegistry["Authors"] = (btnNavAuthors, () => new AuthorsPanel(), false);
            _navRegistry["Publishers"] = (btnNavPublishers, () => new PublishersPanel(), false);
            _navRegistry["Reports"] = (btnNavReports, () => new ReportsPanel(), false);
            _navRegistry["Librarians"] = (btnNavLibrarians, () => new LibrariansPanel(), true);

            // Register Titles for tooltip and collapsed sidebar support
            _buttonTitles[btnNavDashboard] = "Dashboard";
            _buttonTitles[btnNavBooks] = "Books";
            _buttonTitles[btnNavMembers] = "Members";
            _buttonTitles[btnNavBorrow] = "Borrow Books";
            _buttonTitles[btnNavReturn] = "Return Books";
            _buttonTitles[btnNavCategories] = "Categories";
            _buttonTitles[btnNavAuthors] = "Authors";
            _buttonTitles[btnNavPublishers] = "Publishers";
            _buttonTitles[btnNavReports] = "Reports";
            _buttonTitles[btnNavLibrarians] = "Librarians";
            _buttonTitles[btnLogout] = "Sign Out";

            // Bind PNG icons from 'icons/' tinted to white
            btnNavDashboard.Image = LoadWhiteTintedIcon("icons/dashborad.png");
            btnNavBorrow.Image = LoadWhiteTintedIcon("icons/borrow.png");
            btnNavReturn.Image = LoadWhiteTintedIcon("icons/return.png");
            btnNavBooks.Image = LoadWhiteTintedIcon("icons/book.png");
            btnNavMembers.Image = LoadWhiteTintedIcon("icons/member.png");
            btnNavCategories.Image = LoadWhiteTintedIcon("icons/category.png");
            btnNavAuthors.Image = LoadWhiteTintedIcon("icons/author.png");
            btnNavPublishers.Image = LoadWhiteTintedIcon("icons/publisher.png");
            btnNavReports.Image = LoadWhiteTintedIcon("icons/report.png");
            btnNavLibrarians.Image = LoadWhiteTintedIcon("icons/librarian.png");
            btnLogout.Image = LoadWhiteTintedIcon("icons/logout.png") ?? UIHelper.CreateIconBitmap(UIHelper.Icons.Logout, 18, Color.White);

            // Configure navigation buttons styling
            Color defaultNavBg = Color.SteelBlue;
            Color navBorderColor = Color.FromArgb(13, 59, 102);
            Color navHoverColor = Color.FromArgb(91, 155, 213);
            Color navDownColor = Color.FromArgb(55, 105, 150);

            foreach (var item in _navRegistry.Values)
            {
                var btn = item.Button;
                btn.FlatStyle = FlatStyle.Popup;
                btn.Cursor = Cursors.Hand;
                btn.Margin = new Padding(4, 2, 4, 2);
                btn.BackColor = defaultNavBg;
                btn.UseVisualStyleBackColor = false;
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = navBorderColor;
                btn.FlatAppearance.MouseOverBackColor = navHoverColor;
                btn.FlatAppearance.MouseDownBackColor = navDownColor;
            }

            btnLogout.FlatStyle = FlatStyle.Popup;
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.Margin = new Padding(4, 2, 4, 2);
            btnLogout.BackColor = defaultNavBg;
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.FlatAppearance.BorderSize = 0;

            btnLogout.MouseDown += (s, e) => { _isLogoutPressed = true; btnLogout.Invalidate(); };
            btnLogout.MouseUp += (s, e) => { _isLogoutPressed = false; btnLogout.Invalidate(); };
            btnLogout.MouseLeave += (s, e) => { _isLogoutPressed = false; btnLogout.Invalidate(); };

            Color logoutDarkOuter = Color.FromArgb(13, 59, 102);
            Color logoutLightHighlight = Color.FromArgb(162, 193, 219);
            Color logoutDarkShadow = Color.FromArgb(18, 35, 48);

            pnlBrand.Paint += (s, e) =>
            {
                using var pDivider = new Pen(Color.FromArgb(13, 59, 102), 1);
                e.Graphics.DrawLine(pDivider, 0, pnlBrand.Height - 1, pnlBrand.Width - 1, pnlBrand.Height - 1);
            };
            pnlBrand.Resize += (s, e) => pnlBrand.Invalidate();

            btnLogout.Paint += (s, e) =>
            {
                var g = e.Graphics;
                int w = btnLogout.Width;
                int h = btnLogout.Height;

                // Outer border matching theme
                using (var pDark = new Pen(logoutDarkOuter))
                {
                    g.DrawRectangle(pDark, 0, 0, w - 1, h - 1);
                }

                if (_isLogoutPressed)
                {
                    // Sunken effect when pressed/clicked
                    using var pShadow = new Pen(logoutDarkShadow);
                    g.DrawLine(pShadow, 1, 1, w - 2, 1);
                    g.DrawLine(pShadow, 1, 1, 1, h - 2);

                    using var pLight = new Pen(logoutLightHighlight);
                    g.DrawLine(pLight, 1, h - 2, w - 2, h - 2);
                    g.DrawLine(pLight, w - 2, 1, w - 2, h - 2);
                }
                else
                {
                    // Raised 3D border identical to hover state (no harsh black border when idle)
                    using var pLight = new Pen(logoutLightHighlight);
                    g.DrawLine(pLight, 1, 1, w - 2, 1);
                    g.DrawLine(pLight, 1, 1, 1, h - 2);

                    using var pShadow = new Pen(logoutDarkShadow);
                    g.DrawLine(pShadow, 1, h - 2, w - 2, h - 2);
                    g.DrawLine(pShadow, w - 2, 1, w - 2, h - 2);
                }
            };

            lblGroupMain.BackColor = Color.Transparent;
            lblGroupMain.Cursor = Cursors.Default;
            lblGroupOperations.BackColor = Color.Transparent;
            lblGroupOperations.Cursor = Cursors.Default;
            lblGroupManagement.BackColor = Color.Transparent;
            lblGroupManagement.Cursor = Cursors.Default;
            lblGroupMetadata.BackColor = Color.Transparent;
            lblGroupMetadata.Cursor = Cursors.Default;
            lblGroupAdmin.BackColor = Color.Transparent;
            lblGroupAdmin.Cursor = Cursors.Default;

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
                lblUserRole.Text = SessionManager.CurrentLibrarian.Role;
            }
            else
            {
                lblUserName.Text = string.Empty;
                lblUserRole.Text = string.Empty;
            }

            btnNavCategories.Visible = true;
            btnNavAuthors.Visible = true;
            btnNavPublishers.Visible = true;
            btnNavLibrarians.Visible = SessionManager.IsAdmin;
            lblGroupAdmin.Visible = !_isSidebarCollapsed;
            pnlNavDivider5.Visible = true;

            AdjustNavButtonHeights();
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

            this.Text = $"  Library Management System — {name}";
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
            Color activeBg = Color.FromArgb(91, 155, 213); // matches hover color (navHoverColor)
            Color borderColor = Color.FromArgb(13, 59, 102);

            // Reset previous active button
            if (_activeNavButton != null && _activeNavButton != btn)
            {
                _activeNavButton.BackColor = defaultBg;
                _activeNavButton.FlatStyle = FlatStyle.Popup;
                _activeNavButton.FlatAppearance.BorderSize = 1;
                _activeNavButton.FlatAppearance.BorderColor = borderColor;
            }

            // Apply selected style to current button
            _activeNavButton = btn;
            _activeNavButton.BackColor = activeBg;
            _activeNavButton.FlatStyle = FlatStyle.Popup;
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
                pnlContentHost.Controls.Clear();

                if (_activeNavButton != null)
                {
                    _activeNavButton.BackColor = Color.SteelBlue;
                    _activeNavButton = null;
                }

                this.Text = "Library Management System";
                ApplySessionState();

                if (!PromptLogin())
                {
                    _bypassCloseConfirmation = true;
                    this.Close();
                    Application.Exit();
                }
            }
        }

private void AdjustNavButtonHeights()
        {
            if (pnlSidebar == null || pnlBrand == null || pnlNavButtons == null || btnLogout == null)
                return;

            var navButtons = new Button[]
            {
                btnNavDashboard,
                btnNavBooks,
                btnNavMembers,
                btnNavBorrow,
                btnNavReturn,
                btnNavCategories,
                btnNavAuthors,
                btnNavPublishers,
                btnNavReports,
                btnNavLibrarians
            };

            var visibleButtons = new List<Button>();
            foreach (var b in navButtons)
            {
                if (b.Visible) visibleButtons.Add(b);
            }

            int totalCount = visibleButtons.Count + (btnLogout.Visible ? 1 : 0);
            if (totalCount == 0) return;

            var groupLabels = new Label[]
            {
                lblGroupMain,
                lblGroupManagement,
                lblGroupOperations,
                lblGroupMetadata,
                lblGroupAdmin
            };

            int totalLabelHeight = 0;
            foreach (var lbl in groupLabels)
            {
                if (lbl != null && lbl.Visible)
                {
                    totalLabelHeight += lbl.Height;
                }
            }

            var dividers = new Panel[]
            {
                pnlNavDivider1,
                pnlNavDivider2,
                pnlNavDivider3,
                pnlNavDivider4,
                pnlNavDivider5
            };

            int totalDividersHeight = 0;
            foreach (var d in dividers)
            {
                if (d != null && d.Visible)
                {
                    totalDividersHeight += d.Height;
                }
            }

            int spacerHeight = (pnlBottomSpacer != null && pnlBottomSpacer.Visible) ? pnlBottomSpacer.Height : 0;
            int logoutHeight = (pnlLogout != null && pnlLogout.Visible) ? pnlLogout.Height : 40;

            int availableForButtons = pnlSidebar.ClientSize.Height - pnlBrand.Height - logoutHeight - spacerHeight - totalLabelHeight - totalDividersHeight;
            if (availableForButtons <= 0) return;

            const int minButtonHeight = 32;
            int baseHeight = Math.Max(minButtonHeight, availableForButtons / visibleButtons.Count);
            int remainder = availableForButtons - (baseHeight * visibleButtons.Count);
            if (remainder < 0 || baseHeight == minButtonHeight) remainder = 0;

            pnlSidebar.SuspendLayout();
            pnlNavButtons.SuspendLayout();

            for (int i = 0; i < visibleButtons.Count; i++)
            {
                int h = baseHeight + (i < remainder ? 1 : 0);
                visibleButtons[i].Height = h;
            }

            pnlNavButtons.ResumeLayout(true);
            pnlSidebar.ResumeLayout(true);
            pnlSidebar.PerformLayout();
            pnlNavButtons.PerformLayout();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt32() & 0xFFF0) == SC_RESTORE)
            {
                base.WndProc(ref m);
                if (this.WindowState == FormWindowState.Normal)
                {
                    CenterNormalBounds();
                    this.BeginInvoke(new Action(CenterNormalBounds));
                }
                return;
            }

            base.WndProc(ref m);
        }

        private void CenterNormalBounds()
        {
            try
            {
                var screen = Screen.FromControl(this);
                var workingArea = screen.WorkingArea;
                int w = this.WindowState == FormWindowState.Normal ? this.Width : this.RestoreBounds.Width;
                int h = this.WindowState == FormWindowState.Normal ? this.Height : this.RestoreBounds.Height;
                if (w <= 0) w = 1264;
                if (h <= 0) h = 721;

                int x = workingArea.X + Math.Max(0, (workingArea.Width - w) / 2);
                int y = workingArea.Y + Math.Max(0, (workingArea.Height - h) / 2);

                this.Location = new Point(x, y);
            }
            catch
            {
                this.CenterToScreen();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!_bypassCloseConfirmation && e.CloseReason == CloseReason.UserClosing)
            {
                var result = MessageBox.Show(
                    "Are you sure you want to exit the application?",
                    "Confirm Exit",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }

            base.OnFormClosing(e);
        }
    }
}