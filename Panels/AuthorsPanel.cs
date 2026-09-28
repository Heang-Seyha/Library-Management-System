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
            this.Load += AuthorsPanel_Load;
        }

        private void AuthorsPanel_Load(object? sender, EventArgs e)
        {
            if (DesignMode) return;

            UIHelper.StyleDataGridView(dgv);
            ConfigureGridColumns();

            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnRefresh.Click += (s, ev) => LoadData();

            LoadData();
        }

        public void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                using var ctx = Program.CreateDbContext();
                _items = new AuthorService(ctx).GetAll();
                dgv.Rows.Clear();
                foreach (var a in _items)
                {
                    dgv.Rows.Add(a.AuthorId, a.Name, a.Bio);
                }
                UIHelper.UpdateGridState(dgv, _items.Count, false, "Author");
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

        private int SelectedId() => dgv.SelectedRows.Count == 0 ? -1 : (int)dgv.SelectedRows[0].Cells["colId"].Value;

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            if (!SessionManager.IsAdmin)
            {
                MessageBox.Show("Administrator privileges are required to add authors.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var dlg = new SimpleEditDialog("Add Author", new (string, string, bool, int, bool)[]
            {
                ("Author Name", "", false, 150, true),
                ("Biography", "", false, 1000, false)
            });

            if (dlg.ShowDialog() != DialogResult.OK) return;

            try
            {
                using var ctx = Program.CreateDbContext();
                var (ok, msg) = new AuthorService(ctx).Add(new Author
                {
                    Name = dlg.Values[0],
                    Bio = dlg.Values[1]
                });

                MessageBox.Show(msg, ok ? "Success" : "Error", MessageBoxButtons.OK,
                    ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                if (ok) LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not save author.\n\nDetails: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEdit_Click(object? sender, EventArgs e)
        {
            if (!SessionManager.IsAdmin)
            {
                MessageBox.Show("Administrator privileges are required to edit authors.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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

            using var dlg = new SimpleEditDialog("Edit Author", new (string, string, bool, int, bool)[]
            {
                ("Author Name", author.Name, false, 150, true),
                ("Biography", author.Bio, false, 1000, false)
            });

            if (dlg.ShowDialog() != DialogResult.OK) return;

            try
            {
                using var ctx = Program.CreateDbContext();
                var (ok, msg) = new AuthorService(ctx).Update(new Author
                {
                    AuthorId = id,
                    Name = dlg.Values[0],
                    Bio = dlg.Values[1]
                });

                MessageBox.Show(msg, ok ? "Success" : "Error", MessageBoxButtons.OK,
                    ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                if (ok) LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not update author.\n\nDetails: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (!SessionManager.IsAdmin)
            {
                MessageBox.Show("Administrator privileges are required to delete authors.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
            colId.Width = 145;
            colId.MinimumWidth = 135;
            colId.Resizable = DataGridViewTriState.True;

            colName.HeaderText = "Author Name";
            colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colName.FillWeight = 40F;
            colName.MinimumWidth = 160;
            colName.Resizable = DataGridViewTriState.True;

            colBio.HeaderText = "Biography";
            colBio.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colBio.FillWeight = 60F;
            colBio.MinimumWidth = 200;
            colBio.Resizable = DataGridViewTriState.True;
        }
    }
}
