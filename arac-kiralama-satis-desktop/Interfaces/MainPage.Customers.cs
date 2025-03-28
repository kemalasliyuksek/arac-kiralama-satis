using System;
using System.Data;
using System.Windows.Forms;
using arac_kiralama_satis_desktop.Methods;
using FontAwesome.Sharp;

namespace arac_kiralama_satis_desktop.Interfaces
{
    public partial class MainPage
    {
        private void LoadCustomersData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                // Get customer data from database
                DataTable result = MainMethods.GetCustomerList();
                customersTable = result.Copy();

                // Rename columns for display
                customersTable.Columns["MusteriID"].ColumnName = "MusteriID";
                customersTable.Columns["Ad"].ColumnName = "Ad";
                customersTable.Columns["Soyad"].ColumnName = "Soyad";
                customersTable.Columns["TC"].ColumnName = "TC";
                customersTable.Columns["Telefon"].ColumnName = "Telefon";
                customersTable.Columns["Email"].ColumnName = "Email";
                customersTable.Columns["MusteriTipi"].ColumnName = "Müşteri Tipi";
                customersTable.Columns["KayitTarihi"].ColumnName = "Kayıt Tarihi";

                // Set DataSource
                dgvCustomers.DataSource = customersTable;

                // Format columns
                if (dgvCustomers.Columns.Count > 0)
                {
                    dgvCustomers.Columns["MusteriID"].Visible = false;
                    dgvCustomers.Columns["Ad"].Width = 100;
                    dgvCustomers.Columns["Soyad"].Width = 100;
                    dgvCustomers.Columns["TC"].Width = 120;
                    dgvCustomers.Columns["Telefon"].Width = 120;
                    dgvCustomers.Columns["Email"].Width = 180;
                    dgvCustomers.Columns["Müşteri Tipi"].Width = 100;
                    dgvCustomers.Columns["Kayıt Tarihi"].Width = 120;
                    dgvCustomers.Columns["Kayıt Tarihi"].DefaultCellStyle.Format = "dd.MM.yyyy";
                }

                // Update count information
                lblCustomersTitle.Text = $"Müşteri Listesi ({customersTable.Rows.Count})";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Müşteri verileri yüklenirken bir hata oluştu: {ex.Message}",
                    "Veri Yükleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void SearchCustomers(string searchText)
        {
            try
            {
                if (customersTable == null) return;

                if (string.IsNullOrWhiteSpace(searchText))
                {
                    // Show all data
                    dgvCustomers.DataSource = customersTable;
                    lblCustomersTitle.Text = $"Müşteri Listesi ({customersTable.Rows.Count})";
                    return;
                }

                // Create filter
                string filter = "";
                string searchLower = searchText.ToLower();

                // Search in most relevant columns
                filter = $"Ad LIKE '%{searchText}%' OR " +
                         $"Soyad LIKE '%{searchText}%' OR " +
                         $"TC LIKE '%{searchText}%' OR " +
                         $"Telefon LIKE '%{searchText}%' OR " +
                         $"Email LIKE '%{searchText}%'";

                DataView dv = customersTable.DefaultView;
                dv.RowFilter = filter;

                // Update label with count
                lblCustomersTitle.Text = $"Müşteri Listesi ({dv.Count})";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Müşteri aramada hata: {ex.Message}");
                // Failed filter - just reset
                if (customersTable != null)
                {
                    dgvCustomers.DataSource = customersTable;
                    lblCustomersTitle.Text = $"Müşteri Listesi ({customersTable.Rows.Count})";
                }
            }
        }

        private void BtnCustomers_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowPanel(pnlCustomers);
            LoadCustomersData();
            lblPageTitle.Text = "Müşteriler";
        }

        private void TxtSearchCustomers_TextChanged(object sender, EventArgs e)
        {
            SearchCustomers(txtSearchCustomers.Text);
        }

        private void BtnRefreshCustomers_Click(object sender, EventArgs e)
        {
            LoadCustomersData();
            txtSearchCustomers.Clear();
        }
    }
}