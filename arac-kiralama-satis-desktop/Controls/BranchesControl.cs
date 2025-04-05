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
        // DataTable for search/filter
        private DataTable branchesTable;

        // Events for main form to handle
        public event EventHandler BranchAdded;

        public BranchesControl()
        {
            InitializeComponent();

            // Setup data grid view properties
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

                // Şube verilerini DataTable olarak al
                branchesTable = BranchMethods.GetBranchesAsDataTable();

                // DataSource olarak ata
                dgvBranches.DataSource = branchesTable;

                // Format columns
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
                    // Diğer formatlama ayarları...
                }

                // Update count information
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
                    // Show all data
                    dgvBranches.DataSource = branchesTable;
                    lblBranchesTitle.Text = $"Şube Listesi ({branchesTable.Rows.Count})";
                    return;
                }

                // Create filter
                string filter = "";

                // Search in most relevant columns
                filter = $"[Şube Adı] LIKE '%{searchText}%' OR " +
                         $"Adres LIKE '%{searchText}%' OR " +
                         $"[Şehir Plaka] LIKE '%{searchText}%' OR " +
                         $"Telefon LIKE '%{searchText}%' OR " +
                         $"Email LIKE '%{searchText}%'";

                DataView dv = branchesTable.DefaultView;
                dv.RowFilter = filter;

                // Update label with count
                lblBranchesTitle.Text = $"Şube Listesi ({dv.Count})";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Şube aramada hata: {ex.Message}");
                // Failed filter - just reset
                if (branchesTable != null)
                {
                    dgvBranches.DataSource = branchesTable;
                    lblBranchesTitle.Text = $"Şube Listesi ({branchesTable.Rows.Count})";
                }
            }
        }

        // Event handlers
        private void BtnAddBranch_Click(object sender, EventArgs e)
        {
            BranchAddForm branchForm = new BranchAddForm();
            if (branchForm.ShowDialog() == DialogResult.OK)
            {
                // Şube ekleme başarılı olduğunda listeyi yenile
                LoadData();

                // Event'i tetikle
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