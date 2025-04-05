using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using arac_kiralama_satis_desktop.Methods;
using arac_kiralama_satis_desktop.Utils;
using FontAwesome.Sharp;

namespace arac_kiralama_satis_desktop.Controls
{
    public partial class CustomersControl : UserControl
    {
        // DataTable for search/filter
        private DataTable customersTable;

        // Events for main form to handle
        public event EventHandler CustomerAdded;

        public CustomersControl()
        {
            InitializeComponent();

            // Setup data grid view properties
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            UIUtils.SetupDataGridView(dgvCustomers);
        }

        public void LoadData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                // Müşteri verilerini DataTable olarak al
                customersTable = CustomerMethods.GetCustomersAsDataTable();

                // DataSource olarak ata
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
                    dgvCustomers.Columns["MusteriTipi"].Width = 100;
                    dgvCustomers.Columns["KayitTarihi"].Width = 120;
                    dgvCustomers.Columns["KayitTarihi"].DefaultCellStyle.Format = "dd.MM.yyyy";
                    // Diğer formatlama ayarları...
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

        // Event handlers
        private void BtnAddCustomer_Click(object sender, EventArgs e)
        {
            // Aslında burada gerçek bir form açacaktık, ancak şu an için sadece event'i tetikleyelim
            CustomerAdded?.Invoke(this, EventArgs.Empty);

            // Şu an için bir örnek mesaj gösterelim
            MessageBox.Show("Yeni müşteri ekleme formu burada açılacak.",
                "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Form işlemi sonrası veriyi yeniliyoruz
            LoadData();
        }

        private void BtnRefreshCustomers_Click(object sender, EventArgs e)
        {
            LoadData();
            txtSearchCustomers.Clear();
        }

        private void TxtSearchCustomers_TextChanged(object sender, EventArgs e)
        {
            SearchCustomers(txtSearchCustomers.Text);
        }
    }
}