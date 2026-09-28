using LibraryManagementSystem.Dialogs;
using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Panels
{
    /// <summary>
    /// Books management panel for listing, searching, adding, editing, and deleting books.
    /// 100% compatible with the Visual Studio WinForms Designer.
    /// </summary>
    public partial class BooksPanel : UserControl
    {
        private List<Book> _books = new();

        public BooksPanel()
        {
            InitializeComponent();

            UIHelper.ApplyPaddingToAllTextBoxes(this, 8);
            this.Load += BooksPanel_Load;
        }

        private void BooksPanel_Load(object? sender, EventArgs e)
        {
            if (DesignMode) return;

            UIHelper.StyleDataGridView(dgvBooks);
            ConfigureGridColumns();

            // Wire events
            txtSearch.TextChanged += (s, ev) => SearchBooks();
            btnRefresh.Click += (s, ev) => { txtSearch.Clear(); LoadBooks(); };
            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;

            btnDelete.Visible = SessionManager.IsAdmin;

            LoadBooks();
        }

        public void LoadBooks()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                using var ctx = Program.CreateDbContext();
                _books = new BookService(ctx).GetAll();
                BindGrid(_books, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to load books from database.\n\nDetails: {ex.Message}",
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void SearchBooks()
        {
            var q = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(q))
            {
                BindGrid(_books, false);
                return;
            }

            var filtered = _books.Where(b =>
                b.Title.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                b.ISBN.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                (b.Author?.Name.Contains(q, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (b.Category?.Name.Contains(q, StringComparison.OrdinalIgnoreCase) ?? false)).ToList();

            BindGrid(filtered, true, q);
        }

        private void BindGrid(List<Book> books, bool isSearchActive, string? query = null)
        {
            dgvBooks.Rows.Clear();
            foreach (var b in books)
            {
                int row = dgvBooks.Rows.Add(
                    b.BookId, b.Title, b.ISBN, b.Author?.Name, b.Category?.Name, b.Year,
                    b.AvailableCopies, b.TotalCopies);

                if (b.AvailableCopies == 0)
                {
                    dgvBooks.Rows[row].DefaultCellStyle.ForeColor = UIHelper.DangerRed;
                    dgvBooks.Rows[row].DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                }
                else if (b.AvailableCopies <= 2)
                {
                    dgvBooks.Rows[row].DefaultCellStyle.ForeColor = UIHelper.WarningAmber;
                }
            }

            UIHelper.UpdateGridState(dgvBooks, books.Count, isSearchActive, "Book", query, () =>
            {
                txtSearch.Clear();
                LoadBooks();
            });
        }

        private int GetSelectedBookId()
        {
            if (dgvBooks.SelectedRows.Count == 0) return -1;
            return (int)dgvBooks.SelectedRows[0].Cells["colId"].Value;
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            using var dlg = new BookEditDialog(null);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                LoadBooks();
            }
        }

        private void BtnEdit_Click(object? sender, EventArgs e)
        {
            int id = GetSelectedBookId();
            if (id == -1)
            {
                MessageBox.Show("Please select a book from the list to edit.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var ctx = Program.CreateDbContext();
            var book = new BookService(ctx).GetById(id);
            if (book == null)
            {
                MessageBox.Show("Selected book was not found or was removed by another user. The list will refresh.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadBooks();
                return;
            }

            using var dlg = new BookEditDialog(book);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                LoadBooks();
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (!SessionManager.IsAdmin)
            {
                MessageBox.Show("Administrator privileges are required to delete books.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = GetSelectedBookId();
            if (id == -1)
            {
                MessageBox.Show("Please select a book from the list to delete.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedBook = _books.FirstOrDefault(b => b.BookId == id);
            string title = selectedBook?.Title ?? $"ID {id}";

            if (MessageBox.Show(
                $"Are you sure you want to permanently delete the book \"{title}\"?\n\nNote: If this book has associated active borrow records, deletion will be blocked.",
                "Confirm Book Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using var ctx = Program.CreateDbContext();
                    var (success, msg) = new BookService(ctx).Delete(id);
                    if (!success)
                    {
                        MessageBox.Show(msg, "Cannot Delete Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    LoadBooks();
                    MessageBox.Show("Book deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Could not delete book.\n\nDetails: {ex.Message}", "Delete Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ConfigureGridColumns()
        {
            dgvBooks.AllowUserToResizeColumns = true;
            dgvBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvBooks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvBooks.ColumnHeadersHeight = 36;

            colId.HeaderText = "Book ID";
            colId.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colId.Width = 95;
            colId.MinimumWidth = 90;
            colId.Resizable = DataGridViewTriState.True;

            colTitle.HeaderText = "Title";
            colTitle.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTitle.FillWeight = 62F;
            colTitle.MinimumWidth = 180;
            colTitle.Resizable = DataGridViewTriState.True;

            colISBN.HeaderText = "ISBN";
            colISBN.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colISBN.Width = 165;
            colISBN.MinimumWidth = 155;
            colISBN.Resizable = DataGridViewTriState.True;

            colAuthor.HeaderText = "Author";
            colAuthor.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colAuthor.FillWeight = 20F;
            colAuthor.MinimumWidth = 120;
            colAuthor.Resizable = DataGridViewTriState.True;

            colCategory.HeaderText = "Category";
            colCategory.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colCategory.FillWeight = 18F;
            colCategory.MinimumWidth = 110;
            colCategory.Resizable = DataGridViewTriState.True;

            colYear.HeaderText = "Year";
            colYear.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colYear.Width = 80;
            colYear.MinimumWidth = 75;
            colYear.Resizable = DataGridViewTriState.True;

            colAvailable.HeaderText = "Available";
            colAvailable.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colAvailable.Width = 115;
            colAvailable.MinimumWidth = 110;
            colAvailable.Resizable = DataGridViewTriState.True;

            colTotal.HeaderText = "Total";
            colTotal.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colTotal.Width = 85;
            colTotal.MinimumWidth = 80;
            colTotal.Resizable = DataGridViewTriState.True;
        }
    }
}
