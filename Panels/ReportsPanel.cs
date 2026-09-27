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
                BindGrid(dgvActive, active, new (string header, string prop, int minWidth)[]
                {
                    ("Borrow ID", "BorrowId", 70),
                    ("Member", "MemberName", 140),
                    ("Books", "Books", 150),
                    ("Borrow Date", "BorrowDate", 95),
                    ("Due Date", "DueDate", 95),
                    ("Status", "Status", 85),
                    ("Days Overdue", "DaysOverdue", 90),
                    ("Est. Fine (៛)", "EstimatedFine", 100)
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
                BindGrid(dgvOverdue, overdue, new (string header, string prop, int minWidth)[]
                {
                    ("Borrow ID", "BorrowId", 70),
                    ("Member", "MemberName", 140),
                    ("Books", "Books", 150),
                    ("Due Date", "DueDate", 95),
                    ("Days Overdue", "DaysOverdue", 90),
                    ("Est. Fine (៛)", "EstimatedFine", 100)
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
                BindGrid(dgvReturned, returned, new (string header, string prop, int minWidth)[]
                {
                    ("Borrow ID", "BorrowId", 70),
                    ("Member", "MemberName", 140),
                    ("Books", "Books", 150),
                    ("Borrow Date", "BorrowDate", 95),
                    ("Due Date", "DueDate", 95),
                    ("Return Date", "ReturnDate", 95),
                    ("Status", "Status", 85),
                    ("Fine (៛)", "Fine", 90)
                });

                // Fines
                var fines = svc.GetFineReport();
                BindGrid(dgvFines, fines, new (string header, string prop, int minWidth)[]
                {
                    ("Borrow ID", "BorrowId", 70),
                    ("Member", "MemberName", 140),
                    ("Due Date", "DueDate", 95),
                    ("Return Date", "ReturnDate", 95),
                    ("Days Overdue", "DaysOverdue", 90),
                    ("Fine Amount (៛)", "FineAmount", 110)
                });

                // Popular books
                var popular = svc.GetMostBorrowedBooks();
                BindGrid(dgvPopular, popular, new (string header, string prop, int minWidth)[]
                {
                    ("Book Title", "Title", 180),
                    ("Author", "Author", 140),
                    ("Times Borrowed", "TotalBorrowed", 110)
                });

                // Active members
                var activeMembers = svc.GetMostActiveMembers();
                BindGrid(dgvMembers, activeMembers, new (string header, string prop, int minWidth)[]
                {
                    ("Member Name", "MemberName", 160),
                    ("Total Borrows", "TotalBorrows", 110),
                    ("Total Fines (៛)", "TotalFines", 110)
                });

                // Inventory
                var inventory = svc.GetInventory();
                BindGrid(dgvInventory, inventory, new (string header, string prop, int minWidth)[]
                {
                    ("Title", "Title", 180),
                    ("ISBN", "ISBN", 130),
                    ("Author", "Author", 130),
                    ("Category", "Category", 120),
                    ("Total Copies", "TotalCopies", 90),
                    ("Available", "AvailableCopies", 90),
                    ("Borrowed", "BorrowedCopies", 90)
                }, (dgv) =>
                {
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        var avail = row.Cells["Available"].Value as int?;
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
        }

        private static void BindGrid<T>(
            DataGridView dgv,
            List<T> data,
            (string header, string prop, int minWidth)[] columns,
            Action<DataGridView>? postStyle = null)
        {
            dgv.AutoGenerateColumns = false;
            if (dgv.Columns.Count == 0)
            {
                foreach (var (header, prop, minWidth) in columns)
                {
                    dgv.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = prop,
                        HeaderText = header,
                        DataPropertyName = prop,
                        MinimumWidth = minWidth
                    });
                }
            }

            dgv.DataSource = null;
            dgv.DataSource = data;
            postStyle?.Invoke(dgv);

            UIHelper.UpdateGridState(dgv, data.Count, false, "report record");
        }
    }
}
