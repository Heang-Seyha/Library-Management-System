using LibraryManagementSystem.Dialogs;
using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Panels
{
    /// <summary>
    /// Category management panel for book classification and genres (Admin only).
    /// 100% compatible with the Visual Studio WinForms Designer.
    /// </summary>
    public partial class CategoriesPanel : UserControl
    {
        private List<Category> _items = new();

        public CategoriesPanel()
        {
            InitializeComponent();

            UIHelper.ApplyPaddingToAllTextBoxes(this, 8);
            if (DesignMode) return;

            UIHelper.StyleDataGridView(dgv);
            ConfigureGridColumns();

            txtSearch.TextChanged += (s, ev) => SearchCategories();
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
                _items = new CategoryService(ctx).GetAll();
                BindGrid(_items, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to load categories from database.\n\nDetails: {ex.Message}",
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void SearchCategories()
        {
            var q = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(q))
            {
                BindGrid(_items, false);
                return;
            }

            var filtered = _items.Where(c =>
                c.Name.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                (c.Description != null && c.Description.Contains(q, StringComparison.OrdinalIgnoreCase)) ||
                c.CategoryId.ToString() == q).ToList();

            BindGrid(filtered, true, q);
        }

        private void BindGrid(List<Category> categories, bool isSearchActive, string? query = null)
        {
            dgv.Rows.Clear();
            foreach (var c in categories)
            {
                dgv.Rows.Add(c.CategoryId, c.Name, c.Description);
            }
            UIHelper.UpdateGridState(dgv, categories.Count, isSearchActive, "Category", query, () =>
            {
                txtSearch.Clear();
                LoadData();
            });
        }

        private int SelectedId() => dgv.SelectedRows.Count == 0 ? -1 : (int)dgv.SelectedRows[0].Cells["colId"].Value;

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            using var dlg = new SimpleEditDialog("Add Category", new (string, string, bool, int, bool)[]
            {
                ("Category Name", "", false, 100, true),
                ("Description", "", false, 500, false)
            });

            if (dlg.ShowDialog() != DialogResult.OK) return;

            try
            {
                using var ctx = Program.CreateDbContext();
                var (ok, msg) = new CategoryService(ctx).Add(new Category
                {
                    Name = dlg.Values[0],
                    Description = dlg.Values[1]
                });

                MessageBox.Show(msg, ok ? "Success" : "Error", MessageBoxButtons.OK,
                    ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                if (ok) LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not add category.\n\nDetails: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEdit_Click(object? sender, EventArgs e)
        {
            int id = SelectedId();
            if (id == -1)
            {
                MessageBox.Show("Please select a category to edit.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var cat = _items.FirstOrDefault(c => c.CategoryId == id);
            if (cat == null)
            {
                MessageBox.Show("Selected category was not found. The list will refresh.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadData();
                return;
            }

            using var dlg = new SimpleEditDialog("Edit Category", new (string, string, bool, int, bool)[]
            {
                ("Category Name", cat.Name, false, 100, true),
                ("Description", cat.Description ?? "", false, 500, false)
            });

            if (dlg.ShowDialog() != DialogResult.OK) return;

            try
            {
                using var ctx = Program.CreateDbContext();
                var (ok, msg) = new CategoryService(ctx).Update(new Category
                {
                    CategoryId = id,
                    Name = dlg.Values[0],
                    Description = dlg.Values[1]
                });

                MessageBox.Show(msg, ok ? "Success" : "Error", MessageBoxButtons.OK,
                    ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                if (ok) LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not update category.\n\nDetails: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            int id = SelectedId();
            if (id == -1)
            {
                MessageBox.Show("Please select a category to delete.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var cat = _items.FirstOrDefault(c => c.CategoryId == id);
            string catName = cat?.Name ?? $"ID {id}";

            if (MessageBox.Show(
                $"Are you sure you want to delete category '{catName}'?\n\nNote: If books are currently assigned to this category, deletion will be prevented.",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes) return;

            try
            {
                using var ctx = Program.CreateDbContext();
                var (ok, msg) = new CategoryService(ctx).Delete(id);
                MessageBox.Show(msg, ok ? "Success" : "Delete Failed", MessageBoxButtons.OK,
                    ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                if (ok) LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not delete category.\n\nDetails: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureGridColumns()
        {
            dgv.AllowUserToResizeColumns = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgv.ColumnHeadersHeight = 36;

            colId.HeaderText = "Category ID";
            colId.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colId.Width = 160;
            colId.MinimumWidth = 150;
            colId.Resizable = DataGridViewTriState.True;

            colName.HeaderText = "Category Name";
            colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colName.FillWeight = 40F;
            colName.MinimumWidth = 160;
            colName.Resizable = DataGridViewTriState.True;

            colDesc.HeaderText = "Description";
            colDesc.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colDesc.FillWeight = 60F;
            colDesc.MinimumWidth = 200;
            colDesc.Resizable = DataGridViewTriState.True;
        }
    }
}
