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
    public partial class VehiclesControl : UserControl
    {
        private DataTable vehiclesTable;

        public event EventHandler VehicleAdded;

        public VehiclesControl()
        {
            try
            {
                ErrorManager.Instance.LogInfo("Araç kontrol bileşeni başlatılıyor", "VehiclesControl.Constructor");
                InitializeComponent();
                SetupDataGridView();
                ErrorManager.Instance.LogInfo("Araç kontrol bileşeni başarıyla başlatıldı", "VehiclesControl.Constructor");
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.HandleException(
                    ex,
                    "Araç kontrol bileşeni başlatılırken hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.UI);
            }
        }

        private void SetupDataGridView()
        {
            try
            {
                ErrorManager.Instance.LogInfo("Araç veri tablosu ayarlanıyor", "VehiclesControl.SetupDataGridView");
                UIUtils.SetupDataGridView(dgvVehicles);
                ErrorManager.Instance.LogInfo("Araç veri tablosu başarıyla ayarlandı", "VehiclesControl.SetupDataGridView");
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.HandleException(
                    ex,
                    "Araç veri tablosu ayarlanırken hata oluştu",
                    ErrorSeverity.Warning,
                    ErrorSource.UI);

                // Temel ayarlamaları yapmayı dene
                try
                {
                    dgvVehicles.AutoGenerateColumns = false;
                    dgvVehicles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgvVehicles.ReadOnly = true;
                }
                catch { /* Suppress further errors */ }
            }
        }

        public void LoadData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                ErrorManager.Instance.LogInfo("Araç verileri yükleniyor", "VehiclesControl.LoadData");
                vehiclesTable = VehicleMethods.GetVehiclesAsDataTable();

                dgvVehicles.DataSource = vehiclesTable;

                if (dgvVehicles.Columns.Count > 0)
                {
                    ErrorManager.Instance.LogInfo("Araç tablosu sütunları yapılandırılıyor", "VehiclesControl.LoadData");

                    dgvVehicles.Columns["AracID"].Visible = false;
                    dgvVehicles.Columns["Plaka"].Width = 80;
                    dgvVehicles.Columns["Marka"].Width = 100;
                    dgvVehicles.Columns["Model"].Width = 120;
                    dgvVehicles.Columns["Yıl"].Width = 60;
                    dgvVehicles.Columns["Renk"].Width = 80;
                    dgvVehicles.Columns["Kilometre"].Width = 100;
                    dgvVehicles.Columns["YakitTipi"].Width = 80;
                    dgvVehicles.Columns["VitesTipi"].Width = 80;
                    dgvVehicles.Columns["Durum"].Width = 100;
                    dgvVehicles.Columns["Şube"].Width = 120;
                    dgvVehicles.Columns["Sınıf"].Width = 100;
                }

                lblVehiclesTitle.Text = $"Araç Listesi ({vehiclesTable.Rows.Count})";
                ErrorManager.Instance.LogInfo($"Araç verileri başarıyla yüklendi. Toplam {vehiclesTable.Rows.Count} araç.", "VehiclesControl.LoadData");
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Araç verileri yüklenirken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.UI,
                    true); // Kullanıcıya göster
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void SearchVehicles(string searchText)
        {
            try
            {
                if (vehiclesTable == null) return;

                if (string.IsNullOrWhiteSpace(searchText))
                {
                    dgvVehicles.DataSource = vehiclesTable;
                    lblVehiclesTitle.Text = $"Araç Listesi ({vehiclesTable.Rows.Count})";
                    return;
                }

                ErrorManager.Instance.LogInfo($"Araç araması yapılıyor. Arama metni: '{searchText}'", "VehiclesControl.SearchVehicles");

                string filter = "";
                string searchLower = searchText.ToLower();

                filter = $"Plaka LIKE '%{searchText}%' OR " +
                         $"Marka LIKE '%{searchText}%' OR " +
                         $"Model LIKE '%{searchText}%' OR " +
                         $"Durum LIKE '%{searchText}%' OR " +
                         $"Şube LIKE '%{searchText}%' OR " +
                         $"CONVERT(Yıl, System.String) LIKE '%{searchText}%'";

                DataView dv = vehiclesTable.DefaultView;
                dv.RowFilter = filter;

                lblVehiclesTitle.Text = $"Araç Listesi ({dv.Count})";
                ErrorManager.Instance.LogInfo($"Araç araması tamamlandı. '{searchText}' için {dv.Count} sonuç bulundu", "VehiclesControl.SearchVehicles");
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.LogWarning(
                    $"Araç arama sırasında hata oluştu. Arama metni: '{searchText}', Hata: {ex.Message}",
                    "VehiclesControl.SearchVehicles");

                // Arama başarısız olursa tüm listeye geri dön
                try
                {
                    if (vehiclesTable != null)
                    {
                        dgvVehicles.DataSource = vehiclesTable;
                        lblVehiclesTitle.Text = $"Araç Listesi ({vehiclesTable.Rows.Count})";
                    }
                }
                catch (Exception innerEx)
                {
                    ErrorManager.Instance.LogWarning(
                        $"Arama hatası sonrası veri tablosuna dönülürken ikinci bir hata oluştu: {innerEx.Message}",
                        "VehiclesControl.SearchVehicles");
                }
            }
        }

        private void BtnAddVehicle_Click(object sender, EventArgs e)
        {
            try
            {
                ErrorManager.Instance.LogInfo("Araç ekleme formu açılıyor", "VehiclesControl.BtnAddVehicle_Click");

                VehicleAddForm vehicleForm = new VehicleAddForm();
                vehicleForm.VehicleAdded += (s, args) => {
                    try
                    {
                        ErrorManager.Instance.LogInfo("Yeni araç eklendi, liste yenileniyor", "VehiclesControl.VehicleAdded");
                        LoadData();
                        VehicleAdded?.Invoke(this, EventArgs.Empty);
                        ErrorManager.Instance.LogInfo("Araç listesi başarıyla güncellendi", "VehiclesControl.VehicleAdded");
                    }
                    catch (Exception ex)
                    {
                        ErrorManager.Instance.HandleException(
                            ex,
                            "Araç eklendikten sonra liste güncellenirken hata oluştu",
                            ErrorSeverity.Error,
                            ErrorSource.UI,
                            true);
                    }
                };

                vehicleForm.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.HandleException(
                    ex,
                    "Araç ekleme formu açılırken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.UI,
                    true);
            }
        }

        private void BtnRefreshVehicles_Click(object sender, EventArgs e)
        {
            try
            {
                ErrorManager.Instance.LogInfo("Araç listesi yenileniyor", "VehiclesControl.BtnRefreshVehicles_Click");
                LoadData();
                txtSearchVehicles.Clear();
                ErrorManager.Instance.LogInfo("Araç listesi başarıyla yenilendi", "VehiclesControl.BtnRefreshVehicles_Click");
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.HandleException(
                    ex,
                    "Araç listesi yenilenirken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.UI,
                    true);
            }
        }

        private void TxtSearchVehicles_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SearchVehicles(txtSearchVehicles.Text);
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.HandleException(
                    ex,
                    "Araç arama işlemi sırasında beklenmeyen bir hata oluştu",
                    ErrorSeverity.Warning,
                    ErrorSource.UI,
                    false); // Küçük bir hata, kullanıcıya göstermeye gerek yok
            }
        }
    }
}