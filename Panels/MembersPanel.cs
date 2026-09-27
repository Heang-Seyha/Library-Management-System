using LibraryManagementSystem.Dialogs;
using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Panels
{
    /// <summary>
    /// Member management panel for viewing, searching, adding, editing, and deleting library members.
    /// 100% compatible with the Visual Studio WinForms Designer.
    /// </summary>
    public partial class MembersPanel : UserControl
    {
        private List<Member> _members = new();

        public MembersPanel()
        {
            InitializeComponent();
            this.Load += MembersPanel_Load;
        }

        private void MembersPanel_Load(object? sender, EventArgs e)
        {
            if (DesignMode) return;

            UIHelper.StyleDataGridView(dgv);

            // Wire events
            txtSearch.TextChanged += (s, ev) => FilterMembers();
            btnRefresh.Click += (s, ev) => { txtSearch.Clear(); LoadData(); };
            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;

            btnDelete.Visible = SessionManager.IsAdmin;

            LoadData();
        }

        public void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                using var ctx = Program.CreateDbContext();
                _members = new MemberService(ctx).GetAll();
                BindGrid(_members, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to load members from database.\n\nDetails: {ex.Message}",
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void FilterMembers()
        {
            var q = txtSearch.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(q))
            {
                BindGrid(_members, false);
                return;
            }

            var filtered = _members.Where(m =>
                m.Name.ToLower().Contains(q) ||
                m.Phone.ToLower().Contains(q) ||
                (m.Email != null && m.Email.ToLower().Contains(q))).ToList();

            BindGrid(filtered, true, q);
        }

        private void BindGrid(List<Member> members, bool isSearchActive, string? query = null)
        {
            dgv.Rows.Clear();
            foreach (var m in members)
            {
                dgv.Rows.Add(m.MemberId, m.Name, m.Phone, m.Email, m.Address, m.JoinDate.ToShortDateString());
            }

            UIHelper.UpdateGridState(dgv, members.Count, isSearchActive, "Member", query, () =>
            {
                txtSearch.Clear();
                LoadData();
            });
        }

        private int SelectedId() => dgv.SelectedRows.Count == 0 ? -1 : (int)dgv.SelectedRows[0].Cells["colId"].Value;

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            using var form = new MemberEditDialog(null);
            if (form.ShowDialog() == DialogResult.OK) LoadData();
        }

        private void BtnEdit_Click(object? sender, EventArgs e)
        {
            int id = SelectedId();
            if (id == -1)
            {
                MessageBox.Show("Please select a member to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var member = _members.FirstOrDefault(m => m.MemberId == id);
            if (member == null)
            {
                MessageBox.Show("Selected member was not found or was removed by another user. The list will refresh.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadData();
                return;
            }

            using var form = new MemberEditDialog(member);
            if (form.ShowDialog() == DialogResult.OK) LoadData();
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (!SessionManager.IsAdmin)
            {
                MessageBox.Show("Administrator privileges are required to delete members.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = SelectedId();
            if (id == -1)
            {
                MessageBox.Show("Please select a member to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var member = _members.FirstOrDefault(m => m.MemberId == id);
            string memberName = member?.Name ?? $"ID {id}";

            if (MessageBox.Show(
                $"Are you sure you want to delete member \"{memberName}\"?\n\nNote: If this member has active borrowing records, deletion will be blocked.",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes) return;

            try
            {
                using var ctx = Program.CreateDbContext();
                var (ok, msg) = new MemberService(ctx).Delete(id);
                MessageBox.Show(msg, ok ? "Success" : "Delete Failed", MessageBoxButtons.OK,
                    ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                if (ok) LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not delete member.\n\nDetails: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
