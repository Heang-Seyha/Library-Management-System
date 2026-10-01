using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Panels
{
    /// <summary>
    /// Reports panel providing 7 different analytics and transaction audit views.
    /// Supports multi-dimensional query filters (Date range, Status, Member, Book)
    /// and export capabilities (Excel, PDF/HTML, Print) respecting all active filters.
    /// Fully responsive and styled with the Professional Blue design system.
    /// </summary>
    public partial class ReportsPanel : UserControl
    {
        // Programmatic Filter Bar Controls
        private Panel _pnlFilterBar = null!;
        private FlowLayoutPanel _flpFilters = null!;
        private TextBox _txtSearch = null!;
        private CheckBox _chkUseDate = null!;
        private DateTimePicker _dtpFrom = null!;
        private Label _lblTo = null!;
        private DateTimePicker _dtpTo = null!;
        private Button _btnExcel = null!;
        private Button _btnPdf = null!;
        private Button _btnPrint = null!;

        // Cached report datasets for in-memory live search
        private List<BorrowReportRow> _allActive = new();
        private List<BorrowReportRow> _allOverdue = new();
        private List<BorrowReportRow> _allReturned = new();
        private List<FineReportRow> _allFines = new();
        private List<PopularBookRow> _allPopular = new();
        private List<ActiveMemberRow> _allMembers = new();
        private List<InventoryRow> _allInventory = new();

        public TextBox SearchBox => _txtSearch;

        public ReportsPanel()
        {
            InitializeComponent();

            btnRefresh.Click += (s, e) =>
            {
                if (_txtSearch != null) _txtSearch.Text = string.Empty;
                LoadAllReports();
            };

            if (DesignMode) return;

            UIHelper.StyleDataGridView(dgvActive);
            UIHelper.StyleDataGridView(dgvOverdue);
            UIHelper.StyleDataGridView(dgvReturned);
            UIHelper.StyleDataGridView(dgvFines);
            UIHelper.StyleDataGridView(dgvPopular);
            UIHelper.StyleDataGridView(dgvMembers);
            UIHelper.StyleDataGridView(dgvInventory);

            ConfigureGrid(dgvActive);
            ConfigureGrid(dgvOverdue);
            ConfigureGrid(dgvReturned);
            ConfigureGrid(dgvFines);
            ConfigureGrid(dgvPopular);
            ConfigureGrid(dgvMembers);
            ConfigureGrid(dgvInventory);

            InitializeFilterBar();
            WireFilterEvents();
            LoadAllReports();
        }

        private void InitializeFilterBar()
        {
            _pnlFilterBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 52,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(12, 8, 12, 8)
            };

            // Font & Colors
            var fontRegular = new Font("Segoe UI", 9F, FontStyle.Regular);
            var fontBold = new Font("Segoe UI", 9F, FontStyle.Bold);
            Color labelColor = Color.FromArgb(13, 59, 102);

            // Right-aligned Export Action Group (Export: Excel, PDF, Print)
            var flpExport = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = Color.Transparent,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };

            var lblExport = new Label
            {
                Text = "Export:",
                Font = fontBold,
                ForeColor = labelColor,
                AutoSize = true,
                Margin = new Padding(8, 7, 4, 3)
            };

            _btnExcel = new Button
            {
                Text = "Excel",
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = fontBold,
                Width = 60,
                Height = 28,
                Cursor = Cursors.Hand,
                Margin = new Padding(3, 3, 3, 3)
            };
            _btnExcel.FlatAppearance.BorderColor = Color.FromArgb(208, 225, 253);
            _btnExcel.FlatAppearance.BorderSize = 1;
            _btnExcel.Click += (s, e) => ExportActiveReportToExcel();

            _btnPdf = new Button
            {
                Text = "PDF",
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = fontBold,
                Width = 55,
                Height = 28,
                Cursor = Cursors.Hand,
                Margin = new Padding(3, 3, 3, 3)
            };
            _btnPdf.FlatAppearance.BorderColor = Color.FromArgb(208, 225, 253);
            _btnPdf.FlatAppearance.BorderSize = 1;
            _btnPdf.Click += (s, e) => ExportActiveReportToPdf();

            _btnPrint = new Button
            {
                Text = "Print",
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = fontBold,
                Width = 55,
                Height = 28,
                Cursor = Cursors.Hand,
                Margin = new Padding(3, 3, 0, 3)
            };
            _btnPrint.FlatAppearance.BorderColor = Color.FromArgb(208, 225, 253);
            _btnPrint.FlatAppearance.BorderSize = 1;
            _btnPrint.Click += (s, e) => PrintActiveReport();

            flpExport.Controls.AddRange(new Control[] { lblExport, _btnExcel, _btnPdf, _btnPrint });

            // Left-aligned Filters Group (AutoScroll=false to eliminate unwanted scrollbars)
            _flpFilters = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                AutoScroll = false,
                WrapContents = false,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };

            // Search Box
            _txtSearch = new TextBox
            {
                Name = "txtSearch",
                Font = fontRegular,
                PlaceholderText = "  Search report records...",
                Width = 230,
                Margin = new Padding(0, 4, 10, 3)
            };

            // Date Range
            _chkUseDate = new CheckBox
            {
                Text = "Date Range:",
                Font = fontBold,
                ForeColor = labelColor,
                AutoSize = true,
                Margin = new Padding(0, 6, 2, 4),
                Checked = false
            };
            _dtpFrom = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Today.AddDays(-30),
                Width = 130,
                Font = fontRegular,
                Enabled = false,
                Margin = new Padding(2, 3, 2, 3)
            };
            _lblTo = new Label
            {
                Text = "to",
                Font = fontRegular,
                ForeColor = labelColor,
                AutoSize = true,
                Margin = new Padding(2, 6, 2, 4)
            };
            _dtpTo = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Today,
                Width = 130,
                Font = fontRegular,
                Enabled = false,
                Margin = new Padding(2, 3, 8, 3)
            };

            _flpFilters.Controls.AddRange(new Control[]
            {
                _txtSearch, _chkUseDate, _dtpFrom, _lblTo, _dtpTo
            });

            _pnlFilterBar.Controls.Add(flpExport);
            _pnlFilterBar.Controls.Add(_flpFilters);

            // Add filter bar into workspace between Summary and Tabs
            pnlCenteredWorkspace.Controls.Add(_pnlFilterBar);
            pnlCenteredWorkspace.Controls.SetChildIndex(_pnlFilterBar, 1);
        }

        private void WireFilterEvents()
        {
            _txtSearch.TextChanged += (s, e) => ApplySearchFilter();

            _chkUseDate.CheckedChanged += (s, e) =>
            {
                _dtpFrom.Enabled = _chkUseDate.Checked;
                _dtpTo.Enabled = _chkUseDate.Checked;
                LoadAllReports();
            };
            _dtpFrom.ValueChanged += (s, e) =>
            {
                if (_chkUseDate.Checked) LoadAllReports();
            };
            _dtpTo.ValueChanged += (s, e) =>
            {
                if (_chkUseDate.Checked) LoadAllReports();
            };
        }

        public void ResetFilters()
        {
            if (_txtSearch != null) _txtSearch.Text = string.Empty;
            _chkUseDate.Checked = false;
            _dtpFrom.Value = DateTime.Today.AddDays(-30);
            _dtpTo.Value = DateTime.Today;
            LoadAllReports();
        }

        private ReportFilterCriteria GetActiveCriteria()
        {
            var criteria = new ReportFilterCriteria
            {
                FromDate = _chkUseDate != null && _chkUseDate.Checked ? _dtpFrom.Value.Date : null,
                ToDate = _chkUseDate != null && _chkUseDate.Checked ? _dtpTo.Value.Date : null,
                Status = "All",
                MemberId = null,
                BookId = null
            };
            return criteria;
        }

        private string GetFilterSummaryString()
        {
            var parts = new List<string>();
            if (_chkUseDate != null && _chkUseDate.Checked)
            {
                parts.Add($"Date Range: {_dtpFrom.Value:dd/MM/yyyy} – {_dtpTo.Value:dd/MM/yyyy}");
            }
            else
            {
                parts.Add("Date Range: All");
            }

            if (_txtSearch != null && !string.IsNullOrWhiteSpace(_txtSearch.Text))
            {
                parts.Add($"Search: \"{_txtSearch.Text.Trim()}\"");
            }

            return string.Join(" | ", parts);
        }

        public void LoadAllReports()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                using var ctx = Program.CreateDbContext();
                var svc = new ReportService(ctx);
                var criteria = GetActiveCriteria();

                // Update summary strip with global overview figures
                lblTotalBooks.Text = $"Total Books: {svc.GetTotalBooksCount():N0}";
                lblTotalMember.Text = $"Total Member: {svc.GetTotalMembersCount():N0}";
                lblActiveBorrows.Text = $"Active Borrows: {svc.GetActiveBorrowCount():N0}";
                lblOverdueBorrows.Text = $"Overdue Borrows: {svc.GetOverdueCount():N0}";

                // Cache active query sets respecting criteria
                var activeAll = svc.GetFilteredBorrows(criteria);
                _allActive = (criteria.Status == "All")
                    ? activeAll.Where(b => b.Status == BorrowStatus.Active || b.Status == BorrowStatus.Overdue).ToList()
                    : activeAll.Where(b => b.Status == criteria.Status).ToList();

                _allOverdue = activeAll.Where(r => r.DaysOverdue > 0).ToList();
                _allReturned = activeAll.Where(b => b.Status == BorrowStatus.Returned).ToList();
                _allFines = svc.GetFineReport(criteria);
                _allPopular = svc.GetMostBorrowedBooks();
                _allMembers = svc.GetMostActiveMembers();
                _allInventory = svc.GetInventory(criteria);

                ApplySearchFilter();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ReportsPanel.GenerateReport] {ex}");
                MessageBox.Show("Unable to generate analytics reports. Please check database connectivity and try again.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        public void ApplySearchFilter()
        {
            string query = _txtSearch?.Text.Trim() ?? string.Empty;
            bool isSearchActive = !string.IsNullOrWhiteSpace(query);

            // 1. Active Borrows
            var filteredActive = isSearchActive
                ? _allActive.Where(b =>
                    b.BorrowId.ToString().Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    $"BRW-{b.BorrowId:D4}".Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    (b.MemberName != null && b.MemberName.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                    (b.Books != null && b.Books.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                    (b.Status != null && b.Status.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                    (b.LibrarianName != null && b.LibrarianName.Contains(query, StringComparison.OrdinalIgnoreCase))
                ).ToList()
                : _allActive;

            BindGrid(dgvActive, filteredActive, new (string header, string prop, int width, int minWidth, bool isElastic, float fillWeight)[]
            {
                ("Borrow ID", "BorrowId", 135, 125, false, 0),
                ("Member Name", "MemberName", 0, 165, true, 30F),
                ("Books", "Books", 0, 200, true, 70F),
                ("Borrow Date", "BorrowDate", 160, 150, false, 0),
                ("Due Date", "DueDate", 125, 115, false, 0),
                ("Status", "Status", 105, 95, false, 0),
                ("Days Overdue", "DaysOverdue", 160, 150, false, 0),
                ("Est. Fine (៛)", "EstimatedFine", 135, 125, false, 0)
            }, (dgv) =>
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.Cells["Status"].Value?.ToString() == "Overdue")
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 241, 242);
                        row.DefaultCellStyle.ForeColor = UIHelper.DangerRed;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = (row.Index % 2 == 1) ? UIHelper.GridAltRow : Color.White;
                        row.DefaultCellStyle.ForeColor = UIHelper.TextDark;
                    }
                }
            }, isSearchActive, query);

            // 2. Overdue Borrows
            var filteredOverdue = isSearchActive
                ? _allOverdue.Where(b =>
                    b.BorrowId.ToString().Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    $"BRW-{b.BorrowId:D4}".Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    (b.MemberName != null && b.MemberName.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                    (b.Books != null && b.Books.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                    (b.LibrarianName != null && b.LibrarianName.Contains(query, StringComparison.OrdinalIgnoreCase))
                ).ToList()
                : _allOverdue;

            BindGrid(dgvOverdue, filteredOverdue, new (string header, string prop, int width, int minWidth, bool isElastic, float fillWeight)[]
            {
                ("Borrow ID", "BorrowId", 135, 125, false, 0),
                ("Member Name", "MemberName", 0, 165, true, 30F),
                ("Books", "Books", 0, 200, true, 70F),
                ("Due Date", "DueDate", 125, 115, false, 0),
                ("Days Overdue", "DaysOverdue", 160, 150, false, 0),
                ("Est. Fine (៛)", "EstimatedFine", 135, 125, false, 0)
            }, (dgv) =>
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 241, 242);
                    row.DefaultCellStyle.ForeColor = UIHelper.DangerRed;
                }
            }, isSearchActive, query);

            // 3. Returned Borrows
            var filteredReturned = isSearchActive
                ? _allReturned.Where(b =>
                    b.BorrowId.ToString().Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    $"BRW-{b.BorrowId:D4}".Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    (b.MemberName != null && b.MemberName.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                    (b.Books != null && b.Books.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                    (b.Status != null && b.Status.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                    (b.LibrarianName != null && b.LibrarianName.Contains(query, StringComparison.OrdinalIgnoreCase))
                ).ToList()
                : _allReturned;

            BindGrid(dgvReturned, filteredReturned, new (string header, string prop, int width, int minWidth, bool isElastic, float fillWeight)[]
            {
                ("Borrow ID", "BorrowId", 135, 125, false, 0),
                ("Member Name", "MemberName", 0, 165, true, 30F),
                ("Books", "Books", 0, 200, true, 70F),
                ("Borrow Date", "BorrowDate", 160, 150, false, 0),
                ("Due Date", "DueDate", 125, 115, false, 0),
                ("Return Date", "ReturnDate", 160, 150, false, 0),
                ("Status", "Status", 105, 95, false, 0),
                ("Fine (៛)", "Fine", 120, 110, false, 0)
            }, null, isSearchActive, query);

            // 4. Fines Analysis
            var filteredFines = isSearchActive
                ? _allFines.Where(f =>
                    f.BorrowId.ToString().Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    $"BRW-{f.BorrowId:D4}".Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    (f.MemberName != null && f.MemberName.Contains(query, StringComparison.OrdinalIgnoreCase))
                ).ToList()
                : _allFines;

            BindGrid(dgvFines, filteredFines, new (string header, string prop, int width, int minWidth, bool isElastic, float fillWeight)[]
            {
                ("Borrow ID", "BorrowId", 135, 125, false, 0),
                ("Member Name", "MemberName", 0, 165, true, 100F),
                ("Due Date", "DueDate", 125, 115, false, 0),
                ("Return Date", "ReturnDate", 160, 150, false, 0),
                ("Days Overdue", "DaysOverdue", 160, 150, false, 0),
                ("Fine Amount (៛)", "FineAmount", 155, 145, false, 0)
            }, null, isSearchActive, query);

            // 5. Popular Books
            var filteredPopular = isSearchActive
                ? _allPopular.Where(p =>
                    (p.Title != null && p.Title.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                    (p.Author != null && p.Author.Contains(query, StringComparison.OrdinalIgnoreCase))
                ).ToList()
                : _allPopular;

            BindGrid(dgvPopular, filteredPopular, new (string header, string prop, int width, int minWidth, bool isElastic, float fillWeight)[]
            {
                ("Book Title", "Title", 0, 140, true, 60F),
                ("Author", "Author", 0, 110, true, 40F),
                ("Times Borrowed", "TotalBorrowed", 160, 140, false, 0)
            }, null, isSearchActive, query);

            // 6. Active Members
            var filteredMembers = isSearchActive
                ? _allMembers.Where(m =>
                    m.MemberName != null && m.MemberName.Contains(query, StringComparison.OrdinalIgnoreCase)
                ).ToList()
                : _allMembers;

            BindGrid(dgvMembers, filteredMembers, new (string header, string prop, int width, int minWidth, bool isElastic, float fillWeight)[]
            {
                ("Member Name", "MemberName", 0, 170, true, 100F),
                ("Total Borrows", "TotalBorrows", 155, 145, false, 0),
                ("Total Fines (៛)", "TotalFines", 155, 145, false, 0)
            }, null, isSearchActive, query);

            // 7. Inventory
            var filteredInventory = isSearchActive
                ? _allInventory.Where(i =>
                    (i.Title != null && i.Title.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                    (i.ISBN != null && i.ISBN.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                    (i.Author != null && i.Author.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                    (i.Category != null && i.Category.Contains(query, StringComparison.OrdinalIgnoreCase))
                ).ToList()
                : _allInventory;

            BindGrid(dgvInventory, filteredInventory, new (string header, string prop, int width, int minWidth, bool isElastic, float fillWeight)[]
            {
                ("Book Title", "Title", 0, 120, true, 32F),
                ("ISBN", "ISBN", 190, 180, false, 0),
                ("Author", "Author", 0, 95, true, 22F),
                ("Category", "Category", 0, 95, true, 20F),
                ("Total Copies", "TotalCopies", 125, 115, false, 0),
                ("Available", "AvailableCopies", 125, 115, false, 0),
                ("Borrowed", "BorrowedCopies", 125, 115, false, 0)
            }, (dgv) =>
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    var cell = dgv.Columns.Contains("AvailableCopies") ? row.Cells["AvailableCopies"] :
                               dgv.Columns.Contains("Available") ? row.Cells["Available"] : null;
                    var avail = cell?.Value as int?;
                    if (avail == 0)
                    {
                        row.DefaultCellStyle.ForeColor = UIHelper.DangerRed;
                        row.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                    }
                    else if (avail <= 2)
                    {
                        row.DefaultCellStyle.ForeColor = UIHelper.WarningAmber;
                        row.DefaultCellStyle.Font = dgv.DefaultCellStyle.Font;
                    }
                    else
                    {
                        row.DefaultCellStyle.ForeColor = UIHelper.TextDark;
                        row.DefaultCellStyle.Font = dgv.DefaultCellStyle.Font;
                    }
                }
            }, isSearchActive, query);
        }

        private (DataGridView Grid, string Title) GetActiveGridAndTitle()
        {
            if (tabReports.SelectedTab == tabInventory) return (dgvInventory, "Stock Status Report");
            if (tabReports.SelectedTab == tabMembers) return (dgvMembers, "Member Activity Report");
            if (tabReports.SelectedTab == tabPopular) return (dgvPopular, "Popular Books Report");
            if (tabReports.SelectedTab == tabActive) return (dgvActive, "Active Borrows Report");
            if (tabReports.SelectedTab == tabOverdue) return (dgvOverdue, "Overdue Borrows Report");
            if (tabReports.SelectedTab == tabReturned) return (dgvReturned, "Return History Report");
            if (tabReports.SelectedTab == tabFines) return (dgvFines, "Fines Analysis Report");

            return tabReports.SelectedIndex switch
            {
                0 => (dgvInventory, "Stock Status Report"),
                1 => (dgvMembers, "Member Activity Report"),
                2 => (dgvPopular, "Popular Books Report"),
                3 => (dgvActive, "Active Borrows Report"),
                4 => (dgvOverdue, "Overdue Borrows Report"),
                5 => (dgvReturned, "Return History Report"),
                6 => (dgvFines, "Fines Analysis Report"),
                _ => (dgvInventory, "Circulation Report")
            };
        }

        private void ExportActiveReportToExcel()
        {
            var (grid, title) = GetActiveGridAndTitle();
            string filterSummary = GetFilterSummaryString();
            ExportHelper.ExportToExcel(grid, title, filterSummary);
        }

        private void ExportActiveReportToPdf()
        {
            var (grid, title) = GetActiveGridAndTitle();
            string filterSummary = GetFilterSummaryString();
            ExportHelper.ExportToPdf(grid, title, filterSummary);
        }

        private void PrintActiveReport()
        {
            var (grid, title) = GetActiveGridAndTitle();
            string filterSummary = GetFilterSummaryString();
            ExportHelper.PrintReport(grid, title, filterSummary);
        }

        private static void ConfigureGrid(DataGridView dgv)
        {
            dgv.AllowUserToResizeColumns = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgv.ColumnHeadersHeight = 36;
            dgv.CellFormatting += Grid_ReportCellFormatting;
            dgv.Sorted += (s, e) => (dgv.Tag as Action<DataGridView>)?.Invoke(dgv);
        }

        private static void Grid_ReportCellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || sender is not DataGridView dgv || e.CellStyle == null || e.Value == null) return;

            var col = dgv.Columns[e.ColumnIndex];
            if (e.Value is DateTime dt)
            {
                e.Value = dt.ToString("dd/MM/yyyy");
                e.FormattingApplied = true;
            }
            else if (e.Value is decimal dec)
            {
                if (col.Name.Contains("Fine") || col.HeaderText.Contains("៛"))
                {
                    e.Value = $"{dec:N0} ៛";
                    e.FormattingApplied = true;
                }
                else
                {
                    e.Value = dec.ToString("N0");
                    e.FormattingApplied = true;
                }
            }
        }

        private void BindGrid<T>(
            DataGridView dgv,
            List<T> data,
            (string header, string prop, int width, int minWidth, bool isElastic, float fillWeight)[] columns,
            Action<DataGridView>? postStyle = null,
            bool isSearchActive = false,
            string? query = null)
        {
            dgv.AutoGenerateColumns = false;
            dgv.AllowUserToResizeColumns = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            if (dgv.Columns.Count == 0)
            {
                foreach (var (header, prop, width, minWidth, isElastic, fillWeight) in columns)
                {
                    var col = new DataGridViewTextBoxColumn
                    {
                        Name = prop,
                        HeaderText = header,
                        DataPropertyName = prop,
                        MinimumWidth = minWidth,
                        Resizable = DataGridViewTriState.True,
                        SortMode = DataGridViewColumnSortMode.Automatic
                    };
                    if (isElastic)
                    {
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        col.FillWeight = fillWeight;
                    }
                    else
                    {
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        col.Width = width;
                    }

                    // Left aligned by default (DefaultCellStyle handles this)

                    dgv.Columns.Add(col);
                }
            }

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            dgv.Tag = postStyle;
            dgv.DataSource = null;
            dgv.DataSource = new SortableBindingList<T>(data);
            postStyle?.Invoke(dgv);

            UIHelper.UpdateGridState(dgv, data.Count, isSearchActive, "report record", query, () =>
            {
                if (_txtSearch != null) _txtSearch.Text = string.Empty;
            });
        }
    }
}
