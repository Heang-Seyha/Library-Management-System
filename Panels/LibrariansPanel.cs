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
            this.Load += LibrariansPanel_Load;
        }

        private void LibrariansPanel_Load(object? sender, EventArgs e)
        {
            if (DesignMode) return;

            UIHelper.StyleDataGridView(dgv);

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
                _librarians = new LibrarianService(ctx).GetAll();
                dgv.Rows.Clear();
                foreach (var l in _librarians)
                {
                    string displayRole = l.Role == "Employee" ? "Librarian" : l.Role;
                    int row = dgv.Rows.Add(l.LibrarianId, l.Name, l.Username, displayRole, l.Position, l.Phone);
                    if (l.Role == "Admin")
                    {
                        dgv.Rows[row].DefaultCellStyle.ForeColor = UIHelper.BrandNavy;
                        dgv.Rows[row].DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                    }
                }
                UIHelper.UpdateGridState(dgv, _librarians.Count, false, "Librarian");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to load librarians from database.\n\nDetails: {ex.Message}",
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
                MessageBox.Show("Administrator privileges are required to add librarians.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var form = new LibrarianEditDialog(null);
            if (form.ShowDialog() == DialogResult.OK) LoadData();
        }

        private void BtnEdit_Click(object? sender, EventArgs e)
        {
            if (!SessionManager.IsAdmin)
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
            if (!SessionManager.IsAdmin)
            {
                MessageBox.Show("Administrator privileges are required to delete librarians.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = SelectedId();
            if (id == -1)
            {
                MessageBox.Show("Please select a librarian to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (id == SessionManager.CurrentLibrarian?.LibrarianId)
            {
                MessageBox.Show("You cannot delete your own logged-in account.", "Action Prohibited",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var librarian = _librarians.FirstOrDefault(l => l.LibrarianId == id);
            string libName = librarian?.Name ?? $"ID {id}";

            if (MessageBox.Show(
                $"Are you sure you want to permanently delete librarian account '{libName}' ({librarian?.Username})?\n\nThis will revoke access immediately.",
                "Confirm Librarian Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes) return;

            try
            {
                using var ctx = Program.CreateDbContext();
                var (ok, msg) = new LibrarianService(ctx).Delete(id);
                MessageBox.Show(msg, ok ? "Success" : "Delete Failed", MessageBoxButtons.OK,
                    ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                if (ok) LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not delete librarian.\n\nDetails: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
