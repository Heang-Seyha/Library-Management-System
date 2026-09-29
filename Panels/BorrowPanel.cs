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
    /// Borrow panel for creating book borrow transactions using a responsive 3-column TableLayoutPanel.
    /// </summary>
    public partial class BorrowPanel : UserControl
    {
        private Member? _selectedMember;
        private readonly Dictionary<int, (Book book, int quantity)> _borrowItems = new();

        private List<Member> _allMembers = new();
        private List<Book> _allBooks = new();

        public BorrowPanel()
        {
            InitializeComponent();

            UIHelper.ApplyPaddingToAllTextBoxes(this, 8);

            txtMemberSearch.TextChanged += (s, e) => RefreshMemberList(txtMemberSearch.Text);
            lstMembers.SelectedIndexChanged += LstMembers_SelectedIndexChanged;
            txtBookSearch.TextChanged += (s, e) => RefreshBookList(txtBookSearch.Text);
            btnAddBook.Click += BtnAddBook_Click;
            btnRemoveItem.Click += BtnRemoveItem_Click;
            btnConfirm.Click += BtnConfirm_Click;

            this.Load += BorrowPanel_Load;
        }

        private void BorrowPanel_Load(object? sender, EventArgs e)
        {
            if (DesignMode) return;

            UIHelper.StyleDataGridView(dgvBorrowItems);
            ConfigureGridColumns();

            try
            {
                dtpDueDate.MinDate = DateTime.Today.AddDays(1);
                dtpDueDate.Value = DateTime.Today.AddDays(14);
            }
            catch
            {
                // Fallback for edge cases
            }

            LoadData();
        }

        public void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                using var ctx = Program.CreateDbContext();
                new BorrowService(ctx).UpdateOverdueStatuses();
                _allMembers = new MemberService(ctx).GetAll();
                _allBooks = new BookService(ctx).GetAll();
                RefreshMemberList(txtMemberSearch.Text);
                RefreshBookList(txtBookSearch.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to load circulation data.\n\nDetails: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void LstMembers_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (lstMembers.SelectedItem is Member m)
            {
                _selectedMember = m;
                lblSelectedMember.Text = $"✓ {m.Name} (ID: {m.MemberId})";
                lblSelectedMember.ForeColor = UIHelper.SuccessGreen;
            }
        }

        private void RefreshMemberList(string search)
        {
            lstMembers.Items.Clear();
            var q = search.Trim().ToLower();
            var list = string.IsNullOrEmpty(q) ? _allMembers :
                _allMembers.Where(m => m.Name.ToLower().Contains(q) || m.Phone.ToLower().Contains(q)).ToList();
            foreach (var m in list) lstMembers.Items.Add(m);
            lstMembers.DisplayMember = "Name";
        }

        private void RefreshBookList(string search)
        {
            lstBooks.Items.Clear();
            var q = search.Trim().ToLower();
            var list = string.IsNullOrEmpty(q)
                ? _allBooks.Where(b => b.AvailableCopies > 0).ToList()
                : _allBooks.Where(b => b.AvailableCopies > 0 &&
                    (b.Title.ToLower().Contains(q) || b.ISBN.ToLower().Contains(q))).ToList();
            foreach (var b in list) lstBooks.Items.Add(b);
            lstBooks.DisplayMember = "Title";
        }

        private void BtnAddBook_Click(object? sender, EventArgs e)
        {
            if (lstBooks.SelectedItem is not Book book)
            {
                MessageBox.Show("Please select a book from the list first.", "No Book Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int qty = (int)nudQuantity.Value;
            if (qty > book.AvailableCopies)
            {
                MessageBox.Show($"Only {book.AvailableCopies} copies available for '{book.Title}'.",
                    "Insufficient Inventory", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_borrowItems.ContainsKey(book.BookId))
            {
                var existing = _borrowItems[book.BookId];
                int newQty = existing.quantity + qty;
                if (newQty > book.AvailableCopies)
                {
                    MessageBox.Show($"Total quantity ({newQty}) exceeds available copies ({book.AvailableCopies}).",
                        "Insufficient Inventory", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                _borrowItems[book.BookId] = (book, newQty);
            }
            else
            {
                _borrowItems[book.BookId] = (book, qty);
            }

            RefreshGrid();
        }

        private void BtnRemoveItem_Click(object? sender, EventArgs e)
        {
            if (dgvBorrowItems.SelectedRows.Count == 0) return;
            var id = (int)dgvBorrowItems.SelectedRows[0].Cells["BookId"].Value;
            _borrowItems.Remove(id);
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            dgvBorrowItems.Rows.Clear();
            foreach (var item in _borrowItems.Values)
            {
                dgvBorrowItems.Rows.Add(item.book.BookId, item.book.Title, item.book.ISBN,
                    item.book.AvailableCopies, item.quantity);
            }
            int total = _borrowItems.Values.Sum(i => i.quantity);
            lblStatus.Text = _borrowItems.Count == 0
                ? "No books added yet."
                : $"Selected {_borrowItems.Count} book title(s), {total} total item(s).";

            UIHelper.UpdateGridState(dgvBorrowItems, _borrowItems.Count, false, "borrowed item");
        }

        private void BtnConfirm_Click(object? sender, EventArgs e)
        {
            if (_selectedMember == null)
            {
                MessageBox.Show("Please select a library member first.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_borrowItems.Count == 0)
            {
                MessageBox.Show("Please add at least one book to the borrow list.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpDueDate.Value.Date <= DateTime.Today)
            {
                MessageBox.Show("Due date must be at least one day after borrow date.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int totalCopies = _borrowItems.Values.Sum(i => i.quantity);
            if (MessageBox.Show(
                $"Confirm borrow transaction for member \"{_selectedMember.Name}\"?\n\nTotal Books: {totalCopies} copy(ies)\nDue Date: {dtpDueDate.Value:dddd, MMMM dd, yyyy}",
                "Confirm Borrow Transaction",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes) return;

            if (SessionManager.CurrentLibrarian == null)
            {
                MessageBox.Show("You must be logged in to record a borrow transaction.", "Authentication Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var librarianId = SessionManager.CurrentLibrarian.LibrarianId;
            var items = _borrowItems.Values.Select(i => (i.book.BookId, i.quantity)).ToList();

            try
            {
                using var ctx = Program.CreateDbContext();
                var (ok, msg) = new BorrowService(ctx).CreateBorrow(
                    _selectedMember.MemberId, librarianId, dtpDueDate.Value.Date, items);

                if (ok)
                {
                    MessageBox.Show(
                        $"{msg}\nDue Date: {dtpDueDate.Value:dd/MM/yyyy}",
                        "Borrow Successful",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    _borrowItems.Clear();
                    _selectedMember = null;
                    lblSelectedMember.Text = "No member selected";
                    lblSelectedMember.ForeColor = UIHelper.DangerRed;
                    RefreshGrid();
                    LoadData();
                }
                else
                {
                    MessageBox.Show(msg, "Borrow Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not record borrow transaction.\n\nDetails: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblMTitle_Click(object sender, EventArgs e)
        {

        }

        private void lblSTitle_Click(object sender, EventArgs e)
        {

        }

        private void lblBTitle_Click(object sender, EventArgs e)
        {

        }

        private void ConfigureGridColumns()
        {
            dgvBorrowItems.AllowUserToResizeColumns = true;
            dgvBorrowItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvBorrowItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvBorrowItems.ColumnHeadersHeight = 36;

            colBookId.Name = "BookId";
            colBookId.HeaderText = "Book ID";
            colBookId.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colBookId.Width = 95;
            colBookId.MinimumWidth = 90;
            colBookId.Resizable = DataGridViewTriState.True;

            colTitle.HeaderText = "Book Title";
            colTitle.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTitle.FillWeight = 100F;
            colTitle.MinimumWidth = 180;
            colTitle.Resizable = DataGridViewTriState.True;

            colISBN.HeaderText = "ISBN";
            colISBN.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colISBN.Width = 165;
            colISBN.MinimumWidth = 155;
            colISBN.Resizable = DataGridViewTriState.True;

            colAvailable.HeaderText = "Available";
            colAvailable.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colAvailable.Width = 115;
            colAvailable.MinimumWidth = 110;
            colAvailable.Resizable = DataGridViewTriState.True;

            colQty.HeaderText = "Qty to Borrow";
            colQty.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colQty.Width = 160;
            colQty.MinimumWidth = 150;
            colQty.Resizable = DataGridViewTriState.True;
        }
    }
}
