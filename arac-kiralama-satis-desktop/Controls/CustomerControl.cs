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

                ErrorManager.Instance.LogInfo("Müşteri listesi yükleniyor", "CustomersControl.LoadData");
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
                ErrorManager.Instance.LogInfo($"Müşteri listesi başarıyla yüklendi. Toplam {customersTable.Rows.Count} müşteri.", "CustomersControl.LoadData");
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Müşteri verileri yüklenirken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.UI,
                    true); // Kullanıcıya göster
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
                ErrorManager.Instance.LogInfo($"Müşteri araması yapıldı. Arama metni: '{searchText}', Sonuç: {dv.Count} müşteri", "CustomersControl.SearchCustomers");
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.LogWarning(
                    $"Müşteri arama sırasında hata oluştu. Arama metni: '{searchText}', Hata: {ex.Message}",
                    "CustomersControl.SearchCustomers");

                // Arama başarısız olursa tüm listeye geri dön
                if (customersTable != null)
                {
                    dgvCustomers.DataSource = customersTable;
                    lblCustomersTitle.Text = $"Müşteri Listesi ({customersTable.Rows.Count})";
                }
            }
        }

        private void BtnAddCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                ErrorManager.Instance.LogInfo("Yeni müşteri ekleme formu açılıyor", "CustomersControl.BtnAddCustomer_Click");
                CustomerAddForm customerForm = new CustomerAddForm();
                customerForm.CustomerAdded += (s, args) => {
                    try
                    {
                        LoadData();
                        CustomerAdded?.Invoke(this, EventArgs.Empty);
                        ErrorManager.Instance.LogInfo("Yeni müşteri eklendikten sonra liste güncellendi", "CustomersControl.CustomerAdded");
                    }
                    catch (Exception ex)
                    {
                        ErrorManager.Instance.HandleException(
                            ex,
                            "Müşteri eklendikten sonra liste güncellenirken hata oluştu",
                            ErrorSeverity.Error,
                            ErrorSource.UI,
                            true);
                    }
                };

                customerForm.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.HandleException(
                    ex,
                    "Müşteri ekleme formu açılırken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.UI,
                    true);
            }
        }

        private void BtnRefreshCustomers_Click(object sender, EventArgs e)
        {
            try
            {
                ErrorManager.Instance.LogInfo("Müşteri listesi yenileniyor", "CustomersControl.BtnRefreshCustomers_Click");
                LoadData();
                txtSearchCustomers.Clear();
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.HandleException(
                    ex,
                    "Müşteri listesi yenilenirken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.UI,
                    true);
            }
        }

        private void TxtSearchCustomers_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SearchCustomers(txtSearchCustomers.Text);
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.HandleException(
                    ex,
                    "Müşteri arama işlemi sırasında beklenmeyen bir hata oluştu",
                    ErrorSeverity.Warning,
                    ErrorSource.UI,
                    false); // Bu tür hatalar kullanıcı deneyimini çok etkilemediği için göstermiyoruz
            }
        }
    }
}