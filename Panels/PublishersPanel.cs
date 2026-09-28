using LibraryManagementSystem.Dialogs;
using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Panels
{
    /// <summary>
    /// Publisher management panel for publishing houses and contact information (Admin only).
    /// 100% compatible with the Visual Studio WinForms Designer.
    /// </summary>
    public partial class PublishersPanel : UserControl
    {
        private List<Publisher> _items = new();

        public PublishersPanel()
        {
            InitializeComponent();

            UIHelper.ApplyPaddingToAllTextBoxes(this, 8);
            this.Load += PublishersPanel_Load;
        }

        private void PublishersPanel_Load(object? sender, EventArgs e)
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
                _items = new PublisherService(ctx).GetAll();
                dgv.Rows.Clear();
                foreach (var p in _items)
                {
                    dgv.Rows.Add(p.PublisherId, p.Name, p.Address, p.Phone);
                }
                UIHelper.UpdateGridState(dgv, _items.Count, false, "Publisher");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to load publishers from database.\n\nDetails: {ex.Message}",
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
                MessageBox.Show("Administrator privileges are required to add publishers.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var dlg = new SimpleEditDialog("Add Publisher", new (string, string, bool, int, bool)[]
            {
                ("Publisher Name", "", false, 150, true),
                ("Address", "", false, 500, false),
                ("Phone", "", false, 50, false)
            });

            if (dlg.ShowDialog() != DialogResult.OK) return;

            try
            {
                using var ctx = Program.CreateDbContext();
                var (ok, msg) = new PublisherService(ctx).Add(new Publisher
                {
                    Name = dlg.Values[0],
                    Address = dlg.Values[1],
                    Phone = dlg.Values[2]
                });

                MessageBox.Show(msg, ok ? "Success" : "Error", MessageBoxButtons.OK,
                    ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                if (ok) LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not save publisher.\n\nDetails: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEdit_Click(object? sender, EventArgs e)
        {
            if (!SessionManager.IsAdmin)
            {
                MessageBox.Show("Administrator privileges are required to edit publishers.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = SelectedId();
            if (id == -1)
            {
                MessageBox.Show("Please select a publisher to edit.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var pub = _items.FirstOrDefault(p => p.PublisherId == id);
            if (pub == null)
            {
                MessageBox.Show("Selected publisher was not found. The list will refresh.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadData();
                return;
            }

            using var dlg = new SimpleEditDialog("Edit Publisher", new (string, string, bool, int, bool)[]
            {
                ("Publisher Name", pub.Name, false, 150, true),
                ("Address", pub.Address, false, 500, false),
                ("Phone", pub.Phone, false, 50, false)
            });

            if (dlg.ShowDialog() != DialogResult.OK) return;

            try
            {
                using var ctx = Program.CreateDbContext();
                var (ok, msg) = new PublisherService(ctx).Update(new Publisher
                {
                    PublisherId = id,
                    Name = dlg.Values[0],
                    Address = dlg.Values[1],
                    Phone = dlg.Values[2]
                });

                MessageBox.Show(msg, ok ? "Success" : "Error", MessageBoxButtons.OK,
                    ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                if (ok) LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not update publisher.\n\nDetails: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (!SessionManager.IsAdmin)
            {
                MessageBox.Show("Administrator privileges are required to delete publishers.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = SelectedId();
            if (id == -1)
            {
                MessageBox.Show("Please select a publisher to delete.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var pub = _items.FirstOrDefault(p => p.PublisherId == id);
            string pubName = pub?.Name ?? $"ID {id}";

            if (MessageBox.Show(
                $"Are you sure you want to delete publisher '{pubName}'?\n\nNote: If books are currently assigned to this publisher, deletion will be prevented.",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes) return;

            try
            {
                using var ctx = Program.CreateDbContext();
                var (ok, msg) = new PublisherService(ctx).Delete(id);
                MessageBox.Show(msg, ok ? "Success" : "Delete Failed", MessageBoxButtons.OK,
                    ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                if (ok) LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not delete publisher.\n\nDetails: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureGridColumns()
        {
            dgv.AllowUserToResizeColumns = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgv.ColumnHeadersHeight = 36;

            colId.HeaderText = "Publisher ID";
            colId.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colId.Width = 150;
            colId.MinimumWidth = 140;
            colId.Resizable = DataGridViewTriState.True;

            colName.HeaderText = "Publisher Name";
            colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colName.FillWeight = 30F;
            colName.MinimumWidth = 150;
            colName.Resizable = DataGridViewTriState.True;

            colAddress.HeaderText = "Address";
            colAddress.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colAddress.FillWeight = 70F;
            colAddress.MinimumWidth = 180;
            colAddress.Resizable = DataGridViewTriState.True;

            colPhone.HeaderText = "Contact Phone";
            colPhone.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colPhone.Width = 180;
            colPhone.MinimumWidth = 170;
            colPhone.Resizable = DataGridViewTriState.True;
        }
    }
}
