using LibraryManagementSystem.Dialogs;
using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Panels
{
    /// <summary>
    /// Librarian management panel — Admin only.
    /// Allows Admin to add, edit, and delete librarian staff accounts.
    /// 100% compatible with the Visual Studio WinForms Designer.
    /// </summary>
    public partial class LibrariansPanel : UserControl
    {
        private List<Librarian> _librarians = new();

        public LibrariansPanel()
        {
            InitializeComponent();

            UIHelper.ApplyPaddingToAllTextBoxes(this, 8);
            if (DesignMode) return;

            UIHelper.StyleDataGridView(dgv);
            ConfigureGridColumns();

            txtSearch.TextChanged += (s, ev) => SearchLibrarians();
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
                _librarians = new LibrarianService(ctx).GetAll();
                BindGrid(_librarians, false);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[LibrariansPanel.LoadLibrarians] {ex}");
                MessageBox.Show(
                    "Unable to load librarians from database. Please check database connectivity and try again.",
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void SearchLibrarians()
        {
            var q = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(q))
            {
                BindGrid(_librarians, false);
                return;
            }

            var filtered = _librarians.Where(l =>
                l.Name.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                (l.Gender != null && l.Gender.Contains(q, StringComparison.OrdinalIgnoreCase)) ||
                (l.DateOfBirth.HasValue && (l.DateOfBirth.Value.ToString("MM/dd/yyyy").Contains(q) || l.DateOfBirth.Value.ToString("yyyy-MM-dd").Contains(q))) ||
                l.Username.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                (l.Role != null && l.Role.Contains(q, StringComparison.OrdinalIgnoreCase)) ||
                (l.Email != null && l.Email.Contains(q, StringComparison.OrdinalIgnoreCase)) ||
                (l.Phone != null && l.Phone.Contains(q, StringComparison.OrdinalIgnoreCase)) ||
                l.LibrarianId.ToString() == q).ToList();

            BindGrid(filtered, true, q);
        }

        private void BindGrid(List<Librarian> librarians, bool isSearchActive, string? query = null)
        {
            dgv.Rows.Clear();
            foreach (var l in librarians)
            {
                string displayRole = l.Role;
                int row = dgv.Rows.Add(l.LibrarianId, l.Name, l.Gender, l.DateOfBirth?.ToString("MM/dd/yyyy") ?? "", l.Username, displayRole, l.Email, l.Phone);
                if (string.Equals(l.Role, "Admin", StringComparison.OrdinalIgnoreCase))
                {
                    dgv.Rows[row].DefaultCellStyle.ForeColor = UIHelper.BrandNavy;
                    dgv.Rows[row].DefaultCellStyle.Font = new("Segoe UI", 9.5f, FontStyle.Bold);
                }
            }
            UIHelper.UpdateGridState(dgv, librarians.Count, isSearchActive, "Librarian", query, () =>
            {
                txtSearch.Clear();
                LoadData();
            });
        }

        private int SelectedId() => dgv.SelectedRows.Count == 0 ? -1 : (int)dgv.SelectedRows[0].Cells["colId"].Value;

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            if (!AuthorizationHelper.CanManageLibrarians())
            {
                MessageBox.Show("Administrator privileges are required to add librarians.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var form = new LibrarianEditDialog(null);
            if (form.ShowDialog() == DialogResult.OK) LoadData();
        }

        private void BtnEdit_Click(object? sender, EventArgs e)
        {
            if (!AuthorizationHelper.CanManageLibrarians())
            {
                MessageBox.Show("Administrator privileges are required to edit librarians.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = SelectedId();
            if (id == -1)
            {
                MessageBox.Show("Please select a librarian to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var librarian = _librarians.FirstOrDefault(l => l.LibrarianId == id);
            if (librarian == null)
            {
                MessageBox.Show("Selected librarian was not found. The list will refresh.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadData();
                return;
            }

            using var form = new LibrarianEditDialog(librarian);
            if (form.ShowDialog() == DialogResult.OK) LoadData();
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (!AuthorizationHelper.CanDeleteLibrarians())
            {
                MessageBox.Show("Administrator privileges are required to delete a librarian account.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = SelectedId();
            if (id == -1)
            {
                MessageBox.Show("Please select a librarian to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var librarian = _librarians.FirstOrDefault(l => l.LibrarianId == id);
            string librarianName = librarian?.Name ?? $"ID {id}";

            if (MessageBox.Show(
                $"Are you sure you want to delete librarian \"{librarianName}\"?\n\nNote: Deletion will be rejected if this librarian has borrowing history or is the Administrator account.",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes) return;

            try
            {
                using var ctx = Program.CreateDbContext();
                var (ok, msg) = new LibrarianService(ctx).Delete(id);
                MessageBox.Show(msg, ok ? "Success" : "Delete Blocked", MessageBoxButtons.OK,
                    ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                if (ok) LoadData();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[LibrariansPanel.BtnDelete_Click] {ex}");
                MessageBox.Show("Could not delete librarian. Please try again.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

private void ConfigureGridColumns()
        {
            dgv.AllowUserToResizeColumns = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgv.ColumnHeadersHeight = 36;

            colId.HeaderText = "Librarian ID";
            colId.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colId.Width = 150;
            colId.MinimumWidth = 140;
            colId.Resizable = DataGridViewTriState.True;

            colName.HeaderText = "Full Name";
            colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colName.FillWeight = 26F;
            colName.MinimumWidth = 140;
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

            colUsername.HeaderText = "Username";
            colUsername.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colUsername.FillWeight = 34F;
            colUsername.MinimumWidth = 120;
            colUsername.Resizable = DataGridViewTriState.True;

            colRole.HeaderText = "Role";
            colRole.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colRole.Width = 105;
            colRole.MinimumWidth = 95;
            colRole.Resizable = DataGridViewTriState.True;

            colEmail.HeaderText = "Email Address";
            colEmail.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colEmail.FillWeight = 40F;
            colEmail.MinimumWidth = 130;
            colEmail.Resizable = DataGridViewTriState.True;

            colPhone.HeaderText = "Phone";
            colPhone.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colPhone.Width = 160;
            colPhone.MinimumWidth = 150;
            colPhone.Resizable = DataGridViewTriState.True;
        }
    }
}
