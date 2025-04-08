using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using arac_kiralama_satis_desktop.Methods;
using arac_kiralama_satis_desktop.Utils;
using arac_kiralama_satis_desktop.Interfaces;
using FontAwesome.Sharp;

namespace arac_kiralama_satis_desktop.Controls
{
    public partial class BranchesControl : UserControl
    {
        private DataTable branchesTable;

        public event EventHandler BranchAdded;

        public BranchesControl()
        {
            InitializeComponent();

            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            UIUtils.SetupDataGridView(dgvBranches);
        }

        public void LoadData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                branchesTable = BranchMethods.GetBranchesAsDataTable();

                dgvBranches.DataSource = branchesTable;

                if (dgvBranches.Columns.Count > 0)
                {
                    dgvBranches.Columns["SubeID"].Visible = false;
                    dgvBranches.Columns["SubeAdi"].Width = 150;
                    dgvBranches.Columns["Adres"].Width = 250;
                    dgvBranches.Columns["SehirPlaka"].Width = 80;
                    dgvBranches.Columns["Telefon"].Width = 120;
                    dgvBranches.Columns["Email"].Width = 180;
                    dgvBranches.Columns["AktifMi"].Width = 70;
                    dgvBranches.Columns["OlusturmaTarihi"].Width = 120;
                    dgvBranches.Columns["OlusturmaTarihi"].DefaultCellStyle.Format = "dd.MM.yyyy";
                }

                lblBranchesTitle.Text = $"Şube Listesi ({branchesTable.Rows.Count})";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Şube verileri yüklenirken bir hata oluştu: {ex.Message}",
                    "Veri Yükleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void SearchBranches(string searchText)
        {
            try
            {
                if (branchesTable == null) return;

                if (string.IsNullOrWhiteSpace(searchText))
                {
                    dgvBranches.DataSource = branchesTable;
                    lblBranchesTitle.Text = $"Şube Listesi ({branchesTable.Rows.Count})";
                    return;
                }

                string filter = "";

                filter = $"[Şube Adı] LIKE '%{searchText}%' OR " +
                         $"Adres LIKE '%{searchText}%' OR " +
                         $"[Şehir Plaka] LIKE '%{searchText}%' OR " +
                         $"Telefon LIKE '%{searchText}%' OR " +
                         $"Email LIKE '%{searchText}%'";

                DataView dv = branchesTable.DefaultView;
                dv.RowFilter = filter;

                lblBranchesTitle.Text = $"Şube Listesi ({dv.Count})";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Şube aramada hata: {ex.Message}");
                if (branchesTable != null)
                {
                    dgvBranches.DataSource = branchesTable;
                    lblBranchesTitle.Text = $"Şube Listesi ({branchesTable.Rows.Count})";
                }
            }
        }

        private void BtnAddBranch_Click(object sender, EventArgs e)
        {
            BranchAddForm branchForm = new BranchAddForm();
            if (branchForm.ShowDialog() == DialogResult.OK)
            {
                LoadData();

                BranchAdded?.Invoke(this, EventArgs.Empty);
            }
        }

        private void BtnRefreshBranches_Click(object sender, EventArgs e)
        {
            LoadData();
            txtSearchBranches.Clear();
        }

        private void TxtSearchBranches_TextChanged(object sender, EventArgs e)
        {
            SearchBranches(txtSearchBranches.Text);
        }
    }
}