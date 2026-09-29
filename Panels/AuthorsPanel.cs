using LibraryManagementSystem.Dialogs;
using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Panels
{
    /// <summary>
    /// Author management panel for author profiles and biographies (Admin only).
    /// 100% compatible with the Visual Studio WinForms Designer.
    /// </summary>
    public partial class AuthorsPanel : UserControl
    {
        private List<Author> _items = new();

        public AuthorsPanel()
        {
            InitializeComponent();

            UIHelper.ApplyPaddingToAllTextBoxes(this, 8);
            if (DesignMode) return;

            UIHelper.StyleDataGridView(dgv);
            ConfigureGridColumns();

            txtSearch.TextChanged += (s, ev) => SearchAuthors();
            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnRefresh.Click += (s, ev) => { txtSearch.Clear(); LoadData(); };

            LoadData();
        }

        public void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                using var ctx = Program.CreateDbContext();
                _items = new AuthorService(ctx).GetAll();
                BindGrid(_items, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to load authors from database.\n\nDetails: {ex.Message}",
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void SearchAuthors()
        {
            var q = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(q))
            {
                BindGrid(_items, false);
                return;
            }

            var filtered = _items.Where(a =>
                a.Name.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                (a.Gender != null && a.Gender.Contains(q, StringComparison.OrdinalIgnoreCase)) ||
                (a.DateOfBirth.HasValue && (a.DateOfBirth.Value.ToString("MM/dd/yyyy").Contains(q) || a.DateOfBirth.Value.ToString("yyyy-MM-dd").Contains(q))) ||
                (a.Bio != null && a.Bio.Contains(q, StringComparison.OrdinalIgnoreCase)) ||
                a.AuthorId.ToString() == q).ToList();

            BindGrid(filtered, true, q);
        }

        private void BindGrid(List<Author> authors, bool isSearchActive, string? query = null)
        {
            dgv.Rows.Clear();
            foreach (var a in authors)
            {
                dgv.Rows.Add(a.AuthorId, a.Name, a.Gender, a.DateOfBirth?.ToString("MM/dd/yyyy") ?? "", a.Bio);
            }
            UIHelper.UpdateGridState(dgv, authors.Count, isSearchActive, "Author", query, () =>
            {
                txtSearch.Clear();
                LoadData();
            });
        }

        private int SelectedId() => dgv.SelectedRows.Count == 0 ? -1 : (int)dgv.SelectedRows[0].Cells["colId"].Value;

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            using var dlg = new AuthorEditDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void BtnEdit_Click(object? sender, EventArgs e)
        {
            int id = SelectedId();
            if (id == -1)
            {
                MessageBox.Show("Please select an author to edit.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var author = _items.FirstOrDefault(a => a.AuthorId == id);
            if (author == null)
            {
                MessageBox.Show("Selected author was not found. The list will refresh.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadData();
                return;
            }

            using var dlg = new AuthorEditDialog(author);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            int id = SelectedId();
            if (id == -1)
            {
                MessageBox.Show("Please select an author to delete.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var author = _items.FirstOrDefault(a => a.AuthorId == id);
            string authorName = author?.Name ?? $"ID {id}";

            if (MessageBox.Show(
                $"Are you sure you want to delete author '{authorName}'?\n\nNote: If books are currently assigned to this author, deletion will be prevented.",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes) return;

            try
            {
                using var ctx = Program.CreateDbContext();
                var (ok, msg) = new AuthorService(ctx).Delete(id);
                MessageBox.Show(msg, ok ? "Success" : "Delete Failed", MessageBoxButtons.OK,
                    ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                if (ok) LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not delete author.\n\nDetails: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureGridColumns()
        {
            dgv.AllowUserToResizeColumns = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgv.ColumnHeadersHeight = 36;

            colId.HeaderText = "Author ID";
            colId.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colId.Width = 135;
            colId.MinimumWidth = 125;
            colId.Resizable = DataGridViewTriState.True;

            colName.HeaderText = "Author Name";
            colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colName.FillWeight = 35F;
            colName.MinimumWidth = 150;
            colName.Resizable = DataGridViewTriState.True;

            colGender.HeaderText = "Gender";
            colGender.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colGender.Width = 110;
            colGender.MinimumWidth = 100;
            colGender.Resizable = DataGridViewTriState.True;

            colDob.HeaderText = "Date of Birth";
            colDob.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colDob.Width = 145;
            colDob.MinimumWidth = 135;
            colDob.Resizable = DataGridViewTriState.True;

            colBio.HeaderText = "Biography";
            colBio.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colBio.FillWeight = 65F;
            colBio.MinimumWidth = 200;
            colBio.Resizable = DataGridViewTriState.True;
        }
    }
}
