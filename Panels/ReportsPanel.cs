using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Panels
{
    /// <summary>
    /// Reports panel providing 7 different analytics and transaction audit views.
    /// Fully responsive and styled with the Professional Blue design system.
    /// </summary>
    public partial class ReportsPanel : UserControl
    {
        public ReportsPanel()
        {
            InitializeComponent();

            btnRefresh.Click += (s, e) => LoadAllReports();
            this.Load += ReportsPanel_Load;
        }

        private void ReportsPanel_Load(object? sender, EventArgs e)
        {
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

            LoadAllReports();
        }

        public void LoadAllReports()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                using var ctx = Program.CreateDbContext();
                var svc = new ReportService(ctx);

                // Update summary strip
                lblTotalFine.Text = $"Total Fines Collected: {svc.GetTotalFinesCollected():N0} ៛";
                lblOverdueCount.Text = $"Overdue Borrows: {svc.GetOverdueCount()}";
                lblBorrowTotal.Text = $"Total Borrows: {svc.GetTotalBorrowCount()}";

                // Active borrows
                var active = svc.GetActiveBorrows();
                BindGrid(dgvActive, active, new (string header, string prop, int width, int minWidth, bool isElastic, float fillWeight)[]
                {
                    ("Borrow ID", "BorrowId", 125, 115, false, 0),
                    ("Member Name", "MemberName", 0, 165, true, 30F),
                    ("Books", "Books", 0, 200, true, 70F),
                    ("Borrow Date", "BorrowDate", 160, 150, false, 0),
                    ("Due Date", "DueDate", 120, 110, false, 0),
                    ("Status", "Status", 95, 90, false, 0),
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
                    }
                });

                // Overdue
                var overdue = svc.GetOverdueBorrows();
                BindGrid(dgvOverdue, overdue, new (string header, string prop, int width, int minWidth, bool isElastic, float fillWeight)[]
                {
                    ("Borrow ID", "BorrowId", 125, 115, false, 0),
                    ("Member Name", "MemberName", 0, 165, true, 30F),
                    ("Books", "Books", 0, 200, true, 70F),
                    ("Due Date", "DueDate", 120, 110, false, 0),
                    ("Days Overdue", "DaysOverdue", 160, 150, false, 0),
                    ("Est. Fine (៛)", "EstimatedFine", 135, 125, false, 0)
                }, (dgv) =>
                {
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 241, 242);
                        row.DefaultCellStyle.ForeColor = UIHelper.DangerRed;
                    }
                });

                // Returned
                var returned = svc.GetReturnedBorrows();
                BindGrid(dgvReturned, returned, new (string header, string prop, int width, int minWidth, bool isElastic, float fillWeight)[]
                {
                    ("Borrow ID", "BorrowId", 125, 115, false, 0),
                    ("Member Name", "MemberName", 0, 165, true, 30F),
                    ("Books", "Books", 0, 200, true, 70F),
                    ("Borrow Date", "BorrowDate", 160, 150, false, 0),
                    ("Due Date", "DueDate", 120, 110, false, 0),
                    ("Return Date", "ReturnDate", 160, 150, false, 0),
                    ("Status", "Status", 95, 90, false, 0),
                    ("Fine (៛)", "Fine", 115, 105, false, 0)
                });

                // Fines
                var fines = svc.GetFineReport();
                BindGrid(dgvFines, fines, new (string header, string prop, int width, int minWidth, bool isElastic, float fillWeight)[]
                {
                    ("Borrow ID", "BorrowId", 125, 115, false, 0),
                    ("Member Name", "MemberName", 0, 165, true, 100F),
                    ("Due Date", "DueDate", 120, 110, false, 0),
                    ("Return Date", "ReturnDate", 160, 150, false, 0),
                    ("Days Overdue", "DaysOverdue", 160, 150, false, 0),
                    ("Fine Amount (៛)", "FineAmount", 155, 145, false, 0)
                });

                // Popular books
                var popular = svc.GetMostBorrowedBooks();
                BindGrid(dgvPopular, popular, new (string header, string prop, int width, int minWidth, bool isElastic, float fillWeight)[]
                {
                    ("Book Title", "Title", 0, 190, true, 65F),
                    ("Author", "Author", 0, 140, true, 35F),
                    ("Times Borrowed", "TotalBorrowed", 160, 150, false, 0)
                });

                // Active members
                var activeMembers = svc.GetMostActiveMembers();
                BindGrid(dgvMembers, activeMembers, new (string header, string prop, int width, int minWidth, bool isElastic, float fillWeight)[]
                {
                    ("Member Name", "MemberName", 0, 170, true, 100F),
                    ("Total Borrows", "TotalBorrows", 155, 145, false, 0),
                    ("Total Fines (៛)", "TotalFines", 155, 145, false, 0)
                });

                // Inventory
                var inventory = svc.GetInventory();
                BindGrid(dgvInventory, inventory, new (string header, string prop, int width, int minWidth, bool isElastic, float fillWeight)[]
                {
                    ("Book Title", "Title", 0, 180, true, 62F),
                    ("ISBN", "ISBN", 165, 155, false, 0),
                    ("Author", "Author", 0, 120, true, 20F),
                    ("Category", "Category", 0, 110, true, 18F),
                    ("Total Copies", "TotalCopies", 135, 125, false, 0),
                    ("Available", "AvailableCopies", 120, 110, false, 0),
                    ("Borrowed", "BorrowedCopies", 115, 105, false, 0)
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
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to generate analytics reports.\n\nDetails: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }private static void ConfigureGrid(DataGridView dgv)
        {
            dgv.AllowUserToResizeColumns = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgv.ColumnHeadersHeight = 36;
        }

        private static void BindGrid<T>(
            DataGridView dgv,
            List<T> data,
            (string header, string prop, int width, int minWidth, bool isElastic, float fillWeight)[] columns,
            Action<DataGridView>? postStyle = null)
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
                        Resizable = DataGridViewTriState.True
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
                    dgv.Columns.Add(col);
                }
            }

            dgv.DataSource = null;
            dgv.DataSource = data;
            postStyle?.Invoke(dgv);

            UIHelper.UpdateGridState(dgv, data.Count, false, "report record");
        }
    }
}
