using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Reflection;
using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Panels
{
    /// <summary>
    /// Dashboard panel: KPI cards, quick actions, borrows-per-month chart,
    /// overdue list and recent borrows. Responsive via TableLayoutPanel percent columns.
    /// Designer-compatible (all controls live in DashboardPanel.Designer.cs).
    /// </summary>
    public partial class DashboardPanel : UserControl
    {
        private const int ChartMonths = 6;
        private const int OverdueRows = 6;
        private const int RecentRows = 6;

        private static readonly Font ChartLabelFont = new("Segoe UI", 8.5f);
        private static readonly Font ChartValueFont = new("Segoe UI Semibold", 9f);
        private static readonly Font BoldCellFont = new("Segoe UI", 9.5f, FontStyle.Bold);

        private readonly Action<string>? _navigate;
        private List<(string Label, int Count)> _monthlyBorrows = new();

        /// <summary>Parameterless constructor for WinForms Designer.</summary>
        public DashboardPanel() : this(null)
        {
        }

        public DashboardPanel(Action<string>? navigate = null)
        {
            _navigate = navigate;
            InitializeComponent();

            ApplyIcons();
            ConfigureGrids();
            EnableSmoothPainting(pnlChart, resizeRedraw: true);
            EnableSmoothPainting(cardChart, resizeRedraw: true);
            EnableSmoothPainting(cardOverdueList, resizeRedraw: true);
            EnableSmoothPainting(cardRecent, resizeRedraw: true);
            EnableSmoothPainting(dgvOverdue);
            EnableSmoothPainting(dgvRecent);

            pnlChart.Resize += (s, e) => pnlChart.Invalidate();

            btnRefresh.Click += (s, e) => LoadStatistics();
            btnQuickBorrow.Click += (s, e) => _navigate?.Invoke("Borrow");
            btnQuickReturn.Click += (s, e) => _navigate?.Invoke("Return");
            btnQuickBooks.Click += (s, e) => _navigate?.Invoke("Books");
            btnQuickMembers.Click += (s, e) => _navigate?.Invoke("Members");
            btnQuickReports.Click += (s, e) => _navigate?.Invoke("Reports");

            Load += DashboardPanel_Load;
        }

        private void DashboardPanel_Load(object? sender, EventArgs e)
        {
            if (DesignMode) return;
            LoadStatistics();
        }

        // ─────────────────────────────────────────────────────────────────
        //  Data
        // ─────────────────────────────────────────────────────────────────

        public void LoadStatistics()
        {
            if (DesignMode) return;

            try
            {
                Cursor = Cursors.WaitCursor;
                UpdateHeader();

                using var context = Program.CreateDbContext();
                var today = DateTime.Today;

                // "Overdue" = flagged Overdue, or still Active but past due date.
                var overdueQuery = context.Borrows.Where(b =>
                    b.Status == BorrowStatus.Overdue ||
                    (b.Status == BorrowStatus.Active && b.DueDate < today));

                lblTotalBooks.Text = context.Books.Count().ToString("N0");
                lblTotalMembers.Text = context.Members.Count().ToString("N0");
                lblBorrowedBooks.Text = context.Borrows
                    .Count(b => b.Status == BorrowStatus.Active || b.Status == BorrowStatus.Overdue)
                    .ToString("N0");
                lblOverdueBooks.Text = overdueQuery.Count().ToString("N0");

                LoadMonthlyChart(context, today);
                LoadOverdueList(overdueQuery, today);
                LoadRecentBorrows(context, today);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[DashboardPanel.LoadDashboardData] {ex}");
                MessageBox.Show("Failed to load statistics. Please check database connectivity and try again.", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void UpdateHeader()
        {
            var librarian = SessionManager.CurrentLibrarian;
            lblWelcome.Text = $"Welcome, {librarian?.Name ?? "Librarian"}";
            string roleDisplay = librarian?.Role ?? "Librarian";
            lblSubtitle.Text = $"Role: {roleDisplay}  •  {DateTime.Now:dddd, MMMM dd, yyyy}";
        }

        private void LoadMonthlyChart(Data.LibraryDbContext context, DateTime today)
        {
            var first = new DateTime(today.Year, today.Month, 1).AddMonths(-(ChartMonths - 1));
            var dates = context.Borrows
                .Where(b => b.BorrowDate >= first)
                .Select(b => b.BorrowDate)
                .ToList();

            var months = new List<(string Label, int Count)>();
            for (int i = 0; i < ChartMonths; i++)
            {
                var m = first.AddMonths(i);
                int count = dates.Count(d => d.Year == m.Year && d.Month == m.Month);
                months.Add((m.ToString("MMM"), count));
            }

            _monthlyBorrows = months;
            pnlChart.Invalidate();
        }

        private void LoadOverdueList(IQueryable<Borrow> overdueQuery, DateTime today)
        {
            var rows = overdueQuery
                .Include(b => b.Member)
                .OrderBy(b => b.DueDate)
                .Take(OverdueRows)
                .ToList();

            dgvOverdue.Rows.Clear();
            foreach (var b in rows)
            {
                int daysLate = Math.Max(0, (today - b.DueDate.Date).Days);
                decimal fine = FinePolicy.CalculateFine(b.DueDate, today);
                dgvOverdue.Rows.Add(b.Member?.Name ?? "—", daysLate, fine);
            }

            bool hasRows = rows.Count > 0;
            dgvOverdue.Visible = hasRows;
            lblOverdueEmpty.Visible = !hasRows;
            dgvOverdue.ClearSelection();
        }

        private void LoadRecentBorrows(Data.LibraryDbContext context, DateTime today)
        {
            var rows = context.Borrows
                .Include(b => b.Member)
                .Include(b => b.BorrowDetails).ThenInclude(d => d.Book)
                .OrderByDescending(b => b.BorrowDate)
                .ThenByDescending(b => b.BorrowId)
                .Take(RecentRows)
                .ToList();

            dgvRecent.Rows.Clear();
            foreach (var b in rows)
            {
                string books = string.Join(", ", b.BorrowDetails.Select(d => $"{d.Book?.Title} ×{d.Quantity}"));
                string status = b.Status == BorrowStatus.Active && b.DueDate < today
                    ? BorrowStatus.Overdue
                    : b.Status;

                dgvRecent.Rows.Add(
                    b.BorrowId,
                    b.Member?.Name ?? "—",
                    books,
                    b.BorrowDate,
                    b.DueDate,
                    status);
            }

            bool hasRows = rows.Count > 0;
            dgvRecent.Visible = hasRows;
            lblRecentEmpty.Visible = !hasRows;
            dgvRecent.ClearSelection();
        }

        // ─────────────────────────────────────────────────────────────────
        //  UI setup
        // ─────────────────────────────────────────────────────────────────

        private void ApplyIcons()
        {
            ApplyBadgeIcon(lblBooksIcon, UIHelper.Icons.Books);
            ApplyBadgeIcon(lblMembersIcon, UIHelper.Icons.Members);
            ApplyBadgeIcon(lblBorrowIcon, UIHelper.Icons.Borrow);
            ApplyBadgeIcon(lblOverdueIcon, UIHelper.Icons.Warning);

            SetButtonIcon(btnRefresh, UIHelper.Icons.Refresh, 16, ImageAlign.Left);
            SetButtonIcon(btnQuickBorrow, UIHelper.Icons.Borrow, 24, ImageAlign.Center);
            SetButtonIcon(btnQuickReturn, UIHelper.Icons.ReturnBook, 24, ImageAlign.Center);
            SetButtonIcon(btnQuickBooks, UIHelper.Icons.Books, 24, ImageAlign.Center);
            SetButtonIcon(btnQuickMembers, UIHelper.Icons.Members, 24, ImageAlign.Center);
            SetButtonIcon(btnQuickReports, UIHelper.Icons.Reports, 24, ImageAlign.Center);
        }

        private static void ApplyBadgeIcon(Label badge, string glyph)
        {
            badge.Text = string.Empty;
            badge.TextAlign = ContentAlignment.MiddleCenter;
            badge.ImageAlign = ContentAlignment.MiddleCenter;

            void Render()
            {
                int w = badge.Width > 0 ? badge.Width : 40;
                int h = badge.Height > 0 ? badge.Height : 40;
                var old = badge.Image;
                badge.Image = UIHelper.CreateCenteredIconBitmap(glyph, w, h, Color.White, h * 0.52f);
                old?.Dispose();
            }

            Render();
            badge.Resize += (s, e) => Render();
        }

        private enum ImageAlign { Left, Center }

        private static void SetButtonIcon(Button btn, string glyph, int size, ImageAlign align)
        {
            btn.Image = UIHelper.CreateIconBitmap(glyph, size, Color.White);
            if (align == ImageAlign.Center)
            {
                btn.TextImageRelation = TextImageRelation.ImageAboveText;
                btn.ImageAlign = ContentAlignment.MiddleCenter;
                btn.TextAlign = ContentAlignment.MiddleCenter;
                btn.Padding = new Padding(2);
            }
            else
            {
                btn.TextImageRelation = TextImageRelation.ImageBeforeText;
                btn.ImageAlign = ContentAlignment.MiddleCenter;
                btn.TextAlign = ContentAlignment.MiddleCenter;
                btn.Padding = new Padding(8, 0, 8, 0);
            }
        }

        private void ConfigureGrids()
        {
            // Overdue list
            ConfigureGrid(dgvOverdue);
            dgvOverdue.Columns.Add(TextCol("colOverdueMember", "Member", 45, 90));
            dgvOverdue.Columns.Add(TextCol("colDaysLate", "Days Late", 25, 70));
            dgvOverdue.Columns.Add(TextCol("colFine", "Est. Fine", 30, 80));
            dgvOverdue.CellFormatting += Grid_CellFormatting;

            // Recent borrows
            ConfigureGrid(dgvRecent);
            dgvRecent.Columns.Add(TextCol("colBorrowId", "#", 7, 50));
            dgvRecent.Columns.Add(TextCol("colMember", "Member", 22, 110));
            dgvRecent.Columns.Add(TextCol("colBooks", "Books", 34, 140));
            dgvRecent.Columns.Add(TextCol("colBorrowed", "Borrowed", 14, 95));
            dgvRecent.Columns.Add(TextCol("colDue", "Due", 14, 95));
            dgvRecent.Columns.Add(TextCol("colStatus", "Status", 12, 85));
            dgvRecent.CellFormatting += Grid_CellFormatting;
        }

        private static void ConfigureGrid(DataGridView dgv)
        {
            UIHelper.StyleDataGridView(dgv);

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AllowUserToResizeColumns = true;
            dgv.AllowUserToResizeRows = false;
            dgv.AllowUserToOrderColumns = false;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgv.ColumnHeadersHeight = UIHelper.DefaultGridHdrHeight;
            dgv.RowTemplate.Height = 34;
            dgv.TabStop = false;

            // Read-only overview: no selection highlight.
            dgv.DefaultCellStyle.SelectionBackColor = dgv.DefaultCellStyle.BackColor;
            dgv.DefaultCellStyle.SelectionForeColor = dgv.DefaultCellStyle.ForeColor;
            dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = dgv.AlternatingRowsDefaultCellStyle.BackColor;
            dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = dgv.AlternatingRowsDefaultCellStyle.ForeColor;
        }

        private static DataGridViewTextBoxColumn TextCol(string name, string header, float weight, int minWidth, bool right = false)
        {
            var col = new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = header,
                FillWeight = weight,
                MinimumWidth = minWidth,
                SortMode = DataGridViewColumnSortMode.Automatic
            };

            if (right)
            {
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            return col;
        }

        private void Grid_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || sender is not DataGridView dgv || e.CellStyle == null) return;

            switch (dgv.Columns[e.ColumnIndex].Name)
            {
                case "colStatus":
                    e.CellStyle.Font = BoldCellFont;
                    e.CellStyle.ForeColor = (e.Value as string) switch
                    {
                        BorrowStatus.Overdue => UIHelper.DangerRed,
                        BorrowStatus.Returned => UIHelper.SuccessGreen,
                        _ => Color.SteelBlue
                    };
                    break;

                case "colDaysLate":
                    e.CellStyle.Font = BoldCellFont;
                    e.CellStyle.ForeColor = UIHelper.DangerRed;
                    break;

                case "colFine":
                    e.CellStyle.Font = BoldCellFont;
                    e.CellStyle.ForeColor = UIHelper.DangerRed;
                    if (e.Value is decimal fineVal)
                    {
                        e.Value = $"{fineVal:N0} ៛";
                        e.FormattingApplied = true;
                    }
                    break;

                case "colBorrowed":
                case "colDue":
                    if (e.Value is DateTime dt)
                    {
                        e.Value = dt.ToString("dd MMM yyyy");
                        e.FormattingApplied = true;
                    }
                    break;
            }
        }

        // ─────────────────────────────────────────────────────────────────
        //  Custom painting
        // ─────────────────────────────────────────────────────────────────

        /// <summary>1px border around white section cards.</summary>
        private void Card_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Control c) return;
            using var pen = new Pen(UIHelper.BorderColor);
            e.Graphics.DrawRectangle(pen, 0, 0, c.Width - 1, c.Height - 1);
        }

        /// <summary>Bar chart: borrows per month. Pure GDI+, no extra NuGet package.</summary>
        private void PnlChart_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            var area = pnlChart.ClientRectangle;
            if (area.Width < 160 || area.Height < 120) return;

            int max = _monthlyBorrows.Count == 0 ? 0 : _monthlyBorrows.Max(m => m.Count);
            if (max == 0)
            {
                TextRenderer.DrawText(g, "No borrow data yet", ChartLabelFont, area, UIHelper.TextMedium,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                return;
            }

            const int ticks = 4;
            int top = (int)Math.Ceiling(max / (double)ticks) * ticks;
            var plot = new Rectangle(area.Left + 44, area.Top + 28, area.Width - 44 - 20, area.Height - 28 - 34);

            // Grid lines + Y labels
            using (var gridPen = new Pen(Color.FromArgb(226, 232, 240)))
            {
                for (int i = 0; i <= ticks; i++)
                {
                    int y = plot.Bottom - (int)Math.Round(plot.Height * (i / (double)ticks));
                    g.DrawLine(gridPen, plot.Left, y, plot.Right, y);

                    var labelRect = new Rectangle(area.Left, y - 9, plot.Left - 8 - area.Left, 18);
                    TextRenderer.DrawText(g, (top * i / ticks).ToString(), ChartLabelFont, labelRect,
                        UIHelper.TextMedium, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
                }
            }

            // Bars
            int n = _monthlyBorrows.Count;
            float slot = plot.Width / (float)n;
            int barW = (int)Math.Min(56f, slot * 0.5f);

            for (int i = 0; i < n; i++)
            {
                var (label, count) = _monthlyBorrows[i];
                int cx = plot.Left + (int)(slot * i + slot / 2f);
                int h = (int)Math.Round(plot.Height * (count / (double)top));
                var bar = new Rectangle(cx - barW / 2, plot.Bottom - h, barW, h);

                if (h > 0)
                {
                    bool isCurrentMonth = i == n - 1;
                    using var path = RoundedTop(bar, 5);
                    using var brush = new SolidBrush(isCurrentMonth ? UIHelper.BrandNavy : Color.SteelBlue);
                    g.FillPath(brush, path);

                    TextRenderer.DrawText(g, count.ToString(), ChartValueFont,
                        new Rectangle(cx - 30, bar.Top - 20, 60, 18), UIHelper.TextDark,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                }

                TextRenderer.DrawText(g, label, ChartLabelFont,
                    new Rectangle(cx - 30, plot.Bottom + 6, 60, 20), UIHelper.TextMedium,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }

        private static GraphicsPath RoundedTop(Rectangle r, int radius)
        {
            var path = new GraphicsPath();
            int d = Math.Min(radius * 2, Math.Min(r.Width, r.Height));
            if (d < 2)
            {
                path.AddRectangle(r);
                return path;
            }

            path.AddArc(r.Left, r.Top, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Top, d, d, 270, 90);
            path.AddLine(r.Right, r.Bottom, r.Left, r.Bottom);
            path.CloseFigure();
            return path;
        }

        /// <summary>Turns on double buffering (and optionally ResizeRedraw) to avoid flicker.</summary>
        private static void EnableSmoothPainting(Control control, bool resizeRedraw = false)
        {
            typeof(Control)
                .GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.SetValue(control, true);

            if (resizeRedraw)
            {
                typeof(Control)
                    .GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.Invoke(control, new object[] { ControlStyles.ResizeRedraw, true });
            }
        }
    }
}