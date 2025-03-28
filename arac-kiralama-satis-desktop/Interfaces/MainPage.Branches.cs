using System;
using System.Data;
using System.Windows.Forms;
using arac_kiralama_satis_desktop.Methods;
using FontAwesome.Sharp;

namespace arac_kiralama_satis_desktop.Interfaces
{
    public partial class MainPage
    {
        private void LoadBranchesData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                // Get branch data from database
                DataTable result = BranchMethods.GetBranchList();
                branchesTable = result.Copy();

                // Rename columns for display
                branchesTable.Columns["SubeID"].ColumnName = "SubeID";
                branchesTable.Columns["SubeAdi"].ColumnName = "Şube Adı";
                branchesTable.Columns["Adres"].ColumnName = "Adres";
                branchesTable.Columns["SehirPlaka"].ColumnName = "Şehir Plaka";
                branchesTable.Columns["Telefon"].ColumnName = "Telefon";
                branchesTable.Columns["Email"].ColumnName = "Email";
                branchesTable.Columns["AktifMi"].ColumnName = "Aktif Mi";
                branchesTable.Columns["OlusturmaTarihi"].ColumnName = "Oluşturma Tarihi";

                // Set DataSource
                dgvBranches.DataSource = branchesTable;

                // Format columns
                if (dgvBranches.Columns.Count > 0)
                {
                    dgvBranches.Columns["SubeID"].Visible = false;
                    dgvBranches.Columns["Şube Adı"].Width = 150;
                    dgvBranches.Columns["Adres"].Width = 250;
                    dgvBranches.Columns["Şehir Plaka"].Width = 80;
                    dgvBranches.Columns["Telefon"].Width = 120;
                    dgvBranches.Columns["Email"].Width = 180;
                    dgvBranches.Columns["Aktif Mi"].Width = 70;
                    dgvBranches.Columns["Oluşturma Tarihi"].Width = 120;
                    dgvBranches.Columns["Oluşturma Tarihi"].DefaultCellStyle.Format = "dd.MM.yyyy";
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

        private void BtnBranches_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowPanel(pnlBranches);
            LoadBranchesData();
            lblPageTitle.Text = "Şubeler";
        }

        private void TxtSearchBranches_TextChanged(object sender, EventArgs e)
        {
            SearchBranches(txtSearchBranches.Text);
        }

        private void BtnRefreshBranches_Click(object sender, EventArgs e)
        {
            LoadBranchesData();
            txtSearchBranches.Clear();
        }

        private void BtnAddBranch_Click(object sender, EventArgs e)
        {
            BranchAddForm branchForm = new BranchAddForm();
            if (branchForm.ShowDialog() == DialogResult.OK)
            {
                // Şube ekleme başarılı olduğunda listeyi yenile
                LoadBranchesData();
            }
        }
    }
}