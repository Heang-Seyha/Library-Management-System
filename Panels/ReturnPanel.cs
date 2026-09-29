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
    /// Return panel for processing book returns with a responsive SplitContainer layout
    /// (left: DataGridView of active/overdue borrows, right: detailed return summary & fine calculation).
    /// </summary>
    public partial class ReturnPanel : UserControl
    {
        private List<Borrow> _activeBorrows = new();
        private Borrow? _selectedBorrow;

        public ReturnPanel()
        {
            InitializeComponent();

            UIHelper.ApplyPaddingToAllTextBoxes(this, 8);

            txtSearch.TextChanged += (s, e) => FilterAndBind(txtSearch.Text);
            btnRefresh.Click += (s, e) => { txtSearch.Clear(); LoadActiveBorrows(); };
            dgvActive.SelectionChanged += DgvActive_SelectionChanged;
            btnReturn.Click += BtnReturn_Click;
            pnlDetailsCard.Resize += (s, e) => LayoutDetailsCard();
            split.SizeChanged += (s, e) => EnsureSplitterDistance();

            this.Load += ReturnPanel_Load;
        }

        private void ReturnPanel_Load(object? sender, EventArgs e)
        {
            if (DesignMode) return;

            UIHelper.StyleDataGridView(dgvActive);
            ConfigureGridColumns();

            EnsureSplitterDistance();
            LayoutDetailsCard();

            LoadActiveBorrows();
        }

        private void EnsureSplitterDistance()
        {
            const int targetPanel2Width = 310;
            if (split.Width > targetPanel2Width + 200)
            {
                int desired = split.Width - targetPanel2Width - split.SplitterWidth;
                if (desired >= 200 && desired != split.SplitterDistance)
                {
                    split.SplitterDistance = desired;
                }
            }
        }

        private void LayoutDetailsCard()
        {
            int margin = 12;
            int availableWidth = Math.Max(100, pnlDetailsCard.ClientSize.Width - (margin * 2));
            int bottom = pnlDetailsCard.ClientSize.Height;

            int btnHeight = 36;
            int btnTop = bottom - btnHeight - margin;
            btnReturn.SetBounds(margin, btnTop, availableWidth, btnHeight);

            // Expand overdue fine banner height upwards to 74px to cleanly show all 3 text lines
            int fineHeight = 74;
            int fineTop = btnTop - 8 - fineHeight;
            lblDetailFine.SetBounds(margin, fineTop, availableWidth, fineHeight);

            int contentWidth = Math.Max(100, pnlDetailsCard.ClientSize.Width - 32);
            lblDetailTitle.Width = contentWidth;
            lblDetailMember.Width = contentWidth;
            lblDetailBorrow.Width = contentWidth;
            lblDetailDue.Width = contentWidth;
            lblDetailStatus.Width = contentWidth;
            lblDetailBooks.SetBounds(16, 244, contentWidth, Math.Max(40, fineTop - 244 - 8));
        }

        public void LoadActiveBorrows()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                using var ctx = Program.CreateDbContext();
                new BorrowService(ctx).UpdateOverdueStatuses();
                _activeBorrows = new BorrowService(ctx).GetActiveBorrows();
                FilterAndBind(txtSearch.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to load active borrows.\n\nDetails: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void FilterAndBind(string search)
        {
            var q = search.Trim().ToLower();
            var filtered = string.IsNullOrEmpty(q) ? _activeBorrows :
                _activeBorrows.Where(b =>
                    (b.Member?.Name.ToLower().Contains(q) ?? false) ||
                    b.BorrowId.ToString() == q).ToList();

            dgvActive.Rows.Clear();
            foreach (var b in filtered)
            {
                var books = string.Join(", ", b.BorrowDetails.Select(bd => $"{bd.Book?.Title} (x{bd.Quantity})"));
                int row = dgvActive.Rows.Add(b.BorrowId, b.Member?.Name, b.BorrowDate.ToShortDateString(),
                    b.DueDate.ToShortDateString(), b.Status, books);

                if (b.Status == BorrowStatus.Overdue)
                {
                    dgvActive.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(255, 241, 242);
                    dgvActive.Rows[row].DefaultCellStyle.ForeColor = UIHelper.DangerRed;
                }
            }

            UIHelper.UpdateGridState(dgvActive, filtered.Count, !string.IsNullOrEmpty(q), "active borrow record", q, () =>
            {
                txtSearch.Clear();
                LoadActiveBorrows();
            });
        }

        private void DgvActive_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvActive.SelectedRows.Count == 0)
            {
                ClearDetails();
                return;
            }

            var id = (int)dgvActive.SelectedRows[0].Cells["Id"].Value;
            _selectedBorrow = _activeBorrows.FirstOrDefault(b => b.BorrowId == id);
            if (_selectedBorrow == null)
            {
                ClearDetails();
                return;
            }

            lblDetailMember.Text = _selectedBorrow.Member?.Name ?? "—";
            lblDetailBorrow.Text = _selectedBorrow.BorrowDate.ToLongDateString();
            lblDetailDue.Text = _selectedBorrow.DueDate.ToLongDateString();
            lblDetailStatus.Text = _selectedBorrow.Status;
            lblDetailBooks.Text = string.Join("\n", _selectedBorrow.BorrowDetails.Select(bd => $"• {bd.Book?.Title} ×{bd.Quantity}"));

            var fine = FinePolicy.CalculateFine(_selectedBorrow.DueDate, DateTime.Today);
            if (fine > 0)
            {
                int overdueDays = (DateTime.Today - _selectedBorrow.DueDate.Date).Days;
                lblDetailFine.Text = $"⚠ OVERDUE!\n{overdueDays} day(s) × {FinePolicy.FinePerDay:N0} ៛\nFine: {fine:N0} ៛";
                lblDetailFine.ForeColor = UIHelper.DangerRed;
            }
            else
            {
                lblDetailFine.Text = "✓ On Time — No fine required";
                lblDetailFine.ForeColor = UIHelper.SuccessGreen;
            }

            btnReturn.Enabled = true;
        }

        private void ClearDetails()
        {
            _selectedBorrow = null;
            lblDetailMember.Text = "—";
            lblDetailBorrow.Text = "—";
            lblDetailDue.Text = "—";
            lblDetailStatus.Text = "—";
            lblDetailBooks.Text = "—";
            lblDetailFine.Text = "Select a record to view details";
            lblDetailFine.ForeColor = UIHelper.TextMedium;
            btnReturn.Enabled = false;
        }

        private void BtnReturn_Click(object? sender, EventArgs e)
        {
            if (_selectedBorrow == null) return;

            var fine = FinePolicy.CalculateFine(_selectedBorrow.DueDate, DateTime.Today);
            string fineMessage = fine > 0
                ? $"\n\nAn overdue fine of {fine:N0} ៛ applies and must be collected."
                : "\n\nNo fine applies (returned on or before due date).";

            var confirm = MessageBox.Show(
                $"Process return for Borrow #{_selectedBorrow.BorrowId} ({_selectedBorrow.Member?.Name})?{fineMessage}",
                "Confirm Return", MessageBoxButtons.YesNo,
                fine > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                using var ctx = Program.CreateDbContext();
                var (ok, msg, fineAmount) = new BorrowService(ctx).ProcessReturn(_selectedBorrow.BorrowId);

                if (ok)
                {
                    string successMsg = fineAmount > 0
                        ? $"Books returned successfully!\nFine recorded: {fineAmount:N0} ៛"
                        : "Books returned successfully. Inventory updated.";
                    MessageBox.Show(successMsg, "Return Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearDetails();
                    LoadActiveBorrows();
                }
                else
                {
                    MessageBox.Show(msg, "Return Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not process book return.\n\nDetails: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureGridColumns()
        {
            dgvActive.AllowUserToResizeColumns = true;
            dgvActive.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvActive.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvActive.ColumnHeadersHeight = 36;

            colId.Name = "Id";
            colId.HeaderText = "Borrow ID";
            colId.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colId.Width = 135;
            colId.MinimumWidth = 125;
            colId.Resizable = DataGridViewTriState.True;

            colMember.HeaderText = "Member Name";
            colMember.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colMember.FillWeight = 28F;
            colMember.MinimumWidth = 135;
            colMember.Resizable = DataGridViewTriState.True;

            colBorrow.HeaderText = "Borrow Date";
            colBorrow.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colBorrow.Width = 160;
            colBorrow.MinimumWidth = 150;
            colBorrow.Resizable = DataGridViewTriState.True;

            colDue.HeaderText = "Due Date";
            colDue.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colDue.Width = 125;
            colDue.MinimumWidth = 115;
            colDue.Resizable = DataGridViewTriState.True;

            colStatus.HeaderText = "Status";
            colStatus.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colStatus.Width = 105;
            colStatus.MinimumWidth = 95;
            colStatus.Resizable = DataGridViewTriState.True;

            colBooks.HeaderText = "Books";
            colBooks.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colBooks.FillWeight = 72F;
            colBooks.MinimumWidth = 220;
            colBooks.Resizable = DataGridViewTriState.True;
        }
    }
}
