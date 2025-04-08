using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using arac_kiralama_satis_desktop.Interfaces;
using arac_kiralama_satis_desktop.Methods;
using arac_kiralama_satis_desktop.Utils;
using FontAwesome.Sharp;

namespace arac_kiralama_satis_desktop.Controls
{
    public partial class CustomersControl : UserControl
    {
        private DataTable customersTable;

        public event EventHandler CustomerAdded;

        public CustomersControl()
        {
            InitializeComponent();

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

                customersTable = CustomerMethods.GetCustomersAsDataTable();

                dgvCustomers.DataSource = customersTable;

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
                }

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
                    dgvCustomers.DataSource = customersTable;
                    lblCustomersTitle.Text = $"Müşteri Listesi ({customersTable.Rows.Count})";
                    return;
                }

                string filter = "";
                string searchLower = searchText.ToLower();

                filter = $"Ad LIKE '%{searchText}%' OR " +
                         $"Soyad LIKE '%{searchText}%' OR " +
                         $"TC LIKE '%{searchText}%' OR " +
                         $"Telefon LIKE '%{searchText}%' OR " +
                         $"Email LIKE '%{searchText}%'";

                DataView dv = customersTable.DefaultView;
                dv.RowFilter = filter;

                lblCustomersTitle.Text = $"Müşteri Listesi ({dv.Count})";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Müşteri aramada hata: {ex.Message}");
                if (customersTable != null)
                {
                    dgvCustomers.DataSource = customersTable;
                    lblCustomersTitle.Text = $"Müşteri Listesi ({customersTable.Rows.Count})";
                }
            }
        }

        private void BtnAddCustomer_Click(object sender, EventArgs e)
        {
            CustomerAddForm customerForm = new CustomerAddForm();
            customerForm.CustomerAdded += (s, args) => {
                LoadData();

                CustomerAdded?.Invoke(this, EventArgs.Empty);
            };

            customerForm.ShowDialog();
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