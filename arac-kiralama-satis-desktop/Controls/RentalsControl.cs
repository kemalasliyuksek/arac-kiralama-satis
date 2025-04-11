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
    public partial class RentalsControl : UserControl
    {
        private DataTable rentalsTable;

        public event EventHandler RentalAdded;

        public RentalsControl()
        {
            InitializeComponent();

            try
            {
                ErrorManager.Instance.LogInfo("Kiralama kontrol bileşeni başlatılıyor", "RentalsControl.Constructor");
                SetupDataGridView();
                ErrorManager.Instance.LogInfo("Kiralama kontrol bileşeni başarıyla başlatıldı", "RentalsControl.Constructor");
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.HandleException(
                    ex,
                    "Kiralama kontrol bileşeni başlatılırken hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.UI);
            }
        }

        private void SetupDataGridView()
        {
            try
            {
                ErrorManager.Instance.LogInfo("Kiralama veri tablosu ayarlanıyor", "RentalsControl.SetupDataGridView");
                UIUtils.SetupDataGridView(dgvRentals);
                ErrorManager.Instance.LogInfo("Kiralama veri tablosu başarıyla ayarlandı", "RentalsControl.SetupDataGridView");
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.HandleException(
                    ex,
                    "Kiralama veri tablosu ayarlanırken hata oluştu",
                    ErrorSeverity.Warning,
                    ErrorSource.UI);

                // Fail gracefully - try basic setup if UIUtils fails
                try
                {
                    dgvRentals.AutoGenerateColumns = false;
                    dgvRentals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgvRentals.ReadOnly = true;
                }
                catch { /* Suppress further errors */ }
            }
        }

        public void LoadData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                ErrorManager.Instance.LogInfo("Kiralama verileri yükleniyor", "RentalsControl.LoadData");
                rentalsTable = RentalMethods.GetRentalsAsDataTable();

                dgvRentals.DataSource = rentalsTable;

                if (dgvRentals.Columns.Count > 0)
                {
                    ErrorManager.Instance.LogInfo("Kiralama tablosu sütunları yapılandırılıyor", "RentalsControl.LoadData");

                    dgvRentals.Columns["KiralamaID"].Visible = false;
                    dgvRentals.Columns["MusteriAdSoyad"].Width = 150;
                    dgvRentals.Columns["Plaka"].Width = 80;
                    dgvRentals.Columns["Marka"].Width = 100;
                    dgvRentals.Columns["Model"].Width = 100;
                    dgvRentals.Columns["BaslangicTarihi"].Width = 120;
                    dgvRentals.Columns["BitisTarihi"].Width = 120;
                    dgvRentals.Columns["TeslimTarihi"].Width = 120;
                    dgvRentals.Columns["KiralamaTutari"].Width = 120;
                    dgvRentals.Columns["OdemeTipi"].Width = 100;

                    dgvRentals.Columns["BaslangicTarihi"].DefaultCellStyle.Format = "dd.MM.yyyy";
                    dgvRentals.Columns["BitisTarihi"].DefaultCellStyle.Format = "dd.MM.yyyy";
                    dgvRentals.Columns["TeslimTarihi"].DefaultCellStyle.Format = "dd.MM.yyyy";
                    dgvRentals.Columns["KiralamaTutari"].DefaultCellStyle.Format = "N2";
                    dgvRentals.Columns["KiralamaTutari"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    dgvRentals.CellFormatting += (s, e) => {
                        try
                        {
                            if (e.RowIndex >= 0)
                            {
                                DateTime baslangicTarihi = Convert.ToDateTime(dgvRentals.Rows[e.RowIndex].Cells["BaslangicTarihi"].Value);
                                DateTime bitisTarihi = Convert.ToDateTime(dgvRentals.Rows[e.RowIndex].Cells["BitisTarihi"].Value);
                                object teslimTarihiValue = dgvRentals.Rows[e.RowIndex].Cells["TeslimTarihi"].Value;

                                bool teslimEdildi = teslimTarihiValue != DBNull.Value && teslimTarihiValue != null;
                                bool aktifKiralama = DateTime.Now >= baslangicTarihi && DateTime.Now <= bitisTarihi && !teslimEdildi;
                                bool gecikmisKiralama = DateTime.Now > bitisTarihi && !teslimEdildi;

                                if (gecikmisKiralama)
                                {
                                    dgvRentals.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 235);
                                }
                                else if (aktifKiralama)
                                {
                                    dgvRentals.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(235, 255, 235);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // Just log formatting errors, but don't disrupt the UI
                            ErrorManager.Instance.LogWarning(
                                $"Kiralama satırı formatlanırken hata: Satır={e.RowIndex}, Sütun={e.ColumnIndex}, Hata={ex.Message}",
                                "RentalsControl.CellFormatting");
                        }
                    };
                }

                lblRentalsTitle.Text = $"Kiralama Listesi ({rentalsTable.Rows.Count})";
                ErrorManager.Instance.LogInfo($"Kiralama verileri başarıyla yüklendi. Toplam {rentalsTable.Rows.Count} kiralama.", "RentalsControl.LoadData");
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Kiralama verileri yüklenirken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.UI,
                    true); // Kullanıcıya göster
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void SearchRentals(string searchText)
        {
            try
            {
                if (rentalsTable == null) return;

                if (string.IsNullOrWhiteSpace(searchText))
                {
                    dgvRentals.DataSource = rentalsTable;
                    lblRentalsTitle.Text = $"Kiralama Listesi ({rentalsTable.Rows.Count})";
                    return;
                }

                ErrorManager.Instance.LogInfo($"Kiralama araması yapılıyor. Arama metni: '{searchText}'", "RentalsControl.SearchRentals");

                string filter = "";

                filter = $"Müşteri LIKE '%{searchText}%' OR " +
                         $"Plaka LIKE '%{searchText}%' OR " +
                         $"Marka LIKE '%{searchText}%' OR " +
                         $"Model LIKE '%{searchText}%' OR " +
                         $"[Ödeme Tipi] LIKE '%{searchText}%'";

                DataView dv = rentalsTable.DefaultView;
                dv.RowFilter = filter;

                lblRentalsTitle.Text = $"Kiralama Listesi ({dv.Count})";
                ErrorManager.Instance.LogInfo($"Kiralama araması tamamlandı. '{searchText}' için {dv.Count} sonuç bulundu", "RentalsControl.SearchRentals");
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.LogWarning(
                    $"Kiralama arama sırasında hata oluştu. Arama metni: '{searchText}', Hata: {ex.Message}",
                    "RentalsControl.SearchRentals");

                // Arama başarısız olursa tüm listeyi göster
                if (rentalsTable != null)
                {
                    dgvRentals.DataSource = rentalsTable;
                    lblRentalsTitle.Text = $"Kiralama Listesi ({rentalsTable.Rows.Count})";
                }
            }
        }

        private void BtnAddRental_Click(object sender, EventArgs e)
        {
            try
            {
                ErrorManager.Instance.LogInfo("Yeni kiralama ekleme formu açılıyor", "RentalsControl.BtnAddRental_Click");

                RentalAddForm rentalForm = new RentalAddForm();
                rentalForm.RentalAdded += (s, args) => {
                    try
                    {
                        ErrorManager.Instance.LogInfo("Yeni kiralama eklendi, liste yenileniyor", "RentalsControl.RentalAdded");
                        LoadData();
                        RentalAdded?.Invoke(this, EventArgs.Empty);
                        ErrorManager.Instance.LogInfo("Kiralama listesi başarıyla güncellendi", "RentalsControl.RentalAdded");
                    }
                    catch (Exception ex)
                    {
                        ErrorManager.Instance.HandleException(
                            ex,
                            "Kiralama eklendikten sonra liste güncellenirken hata oluştu",
                            ErrorSeverity.Error,
                            ErrorSource.UI,
                            true);
                    }
                };

                rentalForm.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.HandleException(
                    ex,
                    "Kiralama ekleme formu açılırken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.UI,
                    true);
            }
        }

        private void BtnRefreshRentals_Click(object sender, EventArgs e)
        {
            try
            {
                ErrorManager.Instance.LogInfo("Kiralama listesi yenileniyor", "RentalsControl.BtnRefreshRentals_Click");
                LoadData();
                txtSearchRentals.Clear();
                ErrorManager.Instance.LogInfo("Kiralama listesi başarıyla yenilendi", "RentalsControl.BtnRefreshRentals_Click");
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.HandleException(
                    ex,
                    "Kiralama listesi yenilenirken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.UI,
                    true);
            }
        }

        private void TxtSearchRentals_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SearchRentals(txtSearchRentals.Text);
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.HandleException(
                    ex,
                    "Kiralama arama işlemi sırasında beklenmeyen bir hata oluştu",
                    ErrorSeverity.Warning,
                    ErrorSource.UI,
                    false); // Küçük bir hata, kullanıcıya göstermeye gerek yok
            }
        }
    }
}