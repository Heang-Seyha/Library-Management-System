using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LibraryManagementSystem.Dialogs;
using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Panels
{
    /// 
    /// Member management panel for viewing, searching, adding, editing, and deleting library members.
    /// 100% compatible with the Visual Studio WinForms Designer.
    /// 
    public partial class MembersPanel : UserControl
    {
        private Member[] _members = Array.Empty<Member>();

        public MembersPanel()
        {
            InitializeComponent();

            UIHelper.ApplyPaddingToAllTextBoxes(this, 8);
            this.Load += MembersPanel_Load;
        }

        private void MembersPanel_Load(object? sender, EventArgs e)
        {
            if (DesignMode) return;

            // 1. Style DataGridView ជាមុនសិន
            UIHelper.StyleDataGridView(dgv);

            // 2. កំណត់ទំហំ Column និង Header បន្ទាប់ពី Style ដើម្បីកុំឱ្យ StyleDataGridView ទៅ override វា
            ApplyTableLayout();

            // Wire events
            txtSearch.TextChanged += (s, ev) => FilterMembers();
            btnRefresh.Click += (s, ev) => { txtSearch.Clear(); LoadData(); };
            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;

            LoadData();
        }

        private void ApplyTableLayout()
        {
            dgv.AllowUserToResizeColumns = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgv.ColumnHeadersHeight = 36;

            colId.HeaderText = "Member ID";
            colId.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colId.Width = 135;
            colId.MinimumWidth = 125;
            colId.Resizable = DataGridViewTriState.True;

            colName.HeaderText = "Full Name";
            colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colName.FillWeight = 34F;
            colName.MinimumWidth = 160;
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

            colPhone.HeaderText = "Phone";
            colPhone.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colPhone.Width = 130;
            colPhone.MinimumWidth = 120;
            colPhone.Resizable = DataGridViewTriState.True;

            colEmail.HeaderText = "Email Address";
            colEmail.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colEmail.FillWeight = 33F;
            colEmail.MinimumWidth = 160;
            colEmail.Resizable = DataGridViewTriState.True;

            colAddress.HeaderText = "Address";
            colAddress.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colAddress.FillWeight = 33F;
            colAddress.MinimumWidth = 160;
            colAddress.Resizable = DataGridViewTriState.True;

            colJoinDate.HeaderText = "Join Date";
            colJoinDate.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colJoinDate.Width = 130;
            colJoinDate.MinimumWidth = 120;
            colJoinDate.Resizable = DataGridViewTriState.True;
        }

        public void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                using var ctx = Program.CreateDbContext();
                _members = new MemberService(ctx).GetAll().ToArray();
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
                (m.Gender != null && m.Gender.ToLower().Contains(q)) ||
                (m.DateOfBirth.HasValue && (m.DateOfBirth.Value.ToString("MM/dd/yyyy").Contains(q) || m.DateOfBirth.Value.ToString("yyyy-MM-dd").Contains(q))) ||
                m.Phone.ToLower().Contains(q) ||
                (m.Email != null && m.Email.ToLower().Contains(q)) ||
                (m.Address != null && m.Address.ToLower().Contains(q)) ||
                m.JoinDate.ToString("MM/dd/yyyy").Contains(q) ||
                m.JoinDate.ToString("yyyy-MM-dd").Contains(q) ||
                m.MemberId.ToString() == q).ToArray();

            BindGrid(filtered, true, q);
        }

        private void BindGrid(Member[] members, bool isSearchActive, string? query = null)
        {
            dgv.Rows.Clear();
            foreach (var m in members)
            {
                dgv.Rows.Add(m.MemberId, m.Name, m.Gender, m.DateOfBirth?.ToString("MM/dd/yyyy") ?? "", m.Phone, m.Email, m.Address, m.JoinDate.ToString("MM/dd/yyyy"));
            }

            UIHelper.UpdateGridState(dgv, members.Length, isSearchActive, "Member", query, () =>
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