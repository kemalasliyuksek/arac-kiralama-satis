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
    public partial class StaffControl : UserControl
    {
        private DataTable staffTable;

        public event EventHandler StaffAdded;

        public StaffControl()
        {
            try
            {
                ErrorManager.Instance.LogInfo("Personel kontrol bileşeni başlatılıyor", "StaffControl.Constructor");
                InitializeComponent();
                SetupDataGridView();
                ErrorManager.Instance.LogInfo("Personel kontrol bileşeni başarıyla başlatıldı", "StaffControl.Constructor");
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.HandleException(
                    ex,
                    "Personel kontrol bileşeni başlatılırken hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.UI);
            }
        }

        private void SetupDataGridView()
        {
            try
            {
                ErrorManager.Instance.LogInfo("Personel veri tablosu ayarlanıyor", "StaffControl.SetupDataGridView");
                UIUtils.SetupDataGridView(dgvStaff);
                ErrorManager.Instance.LogInfo("Personel veri tablosu başarıyla ayarlandı", "StaffControl.SetupDataGridView");
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.HandleException(
                    ex,
                    "Personel veri tablosu ayarlanırken hata oluştu",
                    ErrorSeverity.Warning,
                    ErrorSource.UI);

                // Graceful fallback - try basic setup if UIUtils fails
                try
                {
                    dgvStaff.AutoGenerateColumns = false;
                    dgvStaff.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgvStaff.ReadOnly = true;
                }
                catch { /* Suppress further errors */ }
            }
        }

        public void LoadData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                ErrorManager.Instance.LogInfo("Personel verileri yükleniyor", "StaffControl.LoadData");
                staffTable = StaffMethods.GetStaffAsDataTable();

                dgvStaff.DataSource = staffTable;

                if (dgvStaff.Columns.Count > 0)
                {
                    ErrorManager.Instance.LogInfo("Personel tablosu sütunları yapılandırılıyor", "StaffControl.LoadData");

                    dgvStaff.Columns["KullaniciID"].Visible = false;
                    dgvStaff.Columns["Ad"].Width = 100;
                    dgvStaff.Columns["Soyad"].Width = 100;
                    dgvStaff.Columns["KullaniciAdi"].Width = 120;
                    dgvStaff.Columns["Email"].Width = 180;
                    dgvStaff.Columns["Telefon"].Width = 120;
                    dgvStaff.Columns["RolID"].Visible = false;
                    dgvStaff.Columns["RolAdi"].Width = 120;
                    dgvStaff.Columns["SubeID"].Visible = false;
                    dgvStaff.Columns["SubeAdi"].Width = 150;
                    dgvStaff.Columns["Durum"].Width = 80;
                    dgvStaff.Columns["SonGiris"].Width = 120;
                    dgvStaff.Columns["KayitTarihi"].Width = 120;
                    dgvStaff.Columns["SonGiris"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";
                    dgvStaff.Columns["KayitTarihi"].DefaultCellStyle.Format = "dd.MM.yyyy";

                    dgvStaff.CellFormatting += (s, e) => {
                        try
                        {
                            if (e.ColumnIndex == dgvStaff.Columns["Durum"].Index && e.Value != null)
                            {
                                string durumText = e.Value.ToString();
                                if (durumText == "Aktif")
                                    e.CellStyle.ForeColor = Color.FromArgb(40, 167, 69);
                                else if (durumText == "Pasif")
                                    e.CellStyle.ForeColor = Color.FromArgb(220, 53, 69);

                                e.FormattingApplied = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            // Just log formatting errors, but don't disrupt the UI
                            ErrorManager.Instance.LogWarning(
                                $"Personel hücresi formatlanırken hata: Satır={e.RowIndex}, Sütun={e.ColumnIndex}, Hata={ex.Message}",
                                "StaffControl.CellFormatting");
                        }
                    };
                }

                lblStaffTitle.Text = $"Personel Listesi ({staffTable.Rows.Count})";
                ErrorManager.Instance.LogInfo($"Personel verileri başarıyla yüklendi. Toplam {staffTable.Rows.Count} personel.", "StaffControl.LoadData");
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Personel verileri yüklenirken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.UI,
                    true); // Kullanıcıya göster
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void SearchStaff(string searchText)
        {
            try
            {
                if (staffTable == null) return;

                if (string.IsNullOrWhiteSpace(searchText))
                {
                    dgvStaff.DataSource = staffTable;
                    lblStaffTitle.Text = $"Personel Listesi ({staffTable.Rows.Count})";
                    return;
                }

                ErrorManager.Instance.LogInfo($"Personel araması yapılıyor. Arama metni: '{searchText}'", "StaffControl.SearchStaff");

                string filter = "";

                filter = $"Ad LIKE '%{searchText}%' OR " +
                         $"Soyad LIKE '%{searchText}%' OR " +
                         $"[Kullanıcı Adı] LIKE '%{searchText}%' OR " +
                         $"Email LIKE '%{searchText}%' OR " +
                         $"Telefon LIKE '%{searchText}%' OR " +
                         $"Rol LIKE '%{searchText}%' OR " +
                         $"Şube LIKE '%{searchText}%'";

                DataView dv = staffTable.DefaultView;
                dv.RowFilter = filter;

                lblStaffTitle.Text = $"Personel Listesi ({dv.Count})";
                ErrorManager.Instance.LogInfo($"Personel araması tamamlandı. '{searchText}' için {dv.Count} sonuç bulundu", "StaffControl.SearchStaff");
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.LogWarning(
                    $"Personel arama sırasında hata oluştu. Arama metni: '{searchText}', Hata: {ex.Message}",
                    "StaffControl.SearchStaff");

                // Arama başarısız olursa tüm listeye geri dön
                try
                {
                    if (staffTable != null)
                    {
                        dgvStaff.DataSource = staffTable;
                        lblStaffTitle.Text = $"Personel Listesi ({staffTable.Rows.Count})";
                    }
                }
                catch (Exception innerEx)
                {
                    ErrorManager.Instance.LogWarning(
                        $"Arama hatası sonrası veri tablosuna dönülürken ikinci bir hata oluştu: {innerEx.Message}",
                        "StaffControl.SearchStaff");
                }
            }
        }

        private void BtnAddStaff_Click(object sender, EventArgs e)
        {
            try
            {
                ErrorManager.Instance.LogInfo("Personel ekleme formu açılıyor", "StaffControl.BtnAddStaff_Click");

                PersonelAddForm staffForm = new PersonelAddForm();
                staffForm.StaffAdded += (s, args) => {
                    try
                    {
                        ErrorManager.Instance.LogInfo("Yeni personel eklendi, liste yenileniyor", "StaffControl.StaffAdded");
                        LoadData();
                        StaffAdded?.Invoke(this, EventArgs.Empty);
                        ErrorManager.Instance.LogInfo("Personel listesi başarıyla güncellendi", "StaffControl.StaffAdded");
                    }
                    catch (Exception ex)
                    {
                        ErrorManager.Instance.HandleException(
                            ex,
                            "Personel eklendikten sonra liste güncellenirken hata oluştu",
                            ErrorSeverity.Error,
                            ErrorSource.UI,
                            true);
                    }
                };

                staffForm.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.HandleException(
                    ex,
                    "Personel ekleme formu açılırken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.UI,
                    true);
            }
        }

        private void BtnRefreshStaff_Click(object sender, EventArgs e)
        {
            try
            {
                ErrorManager.Instance.LogInfo("Personel listesi yenileniyor", "StaffControl.BtnRefreshStaff_Click");
                LoadData();
                txtSearchStaff.Clear();
                ErrorManager.Instance.LogInfo("Personel listesi başarıyla yenilendi", "StaffControl.BtnRefreshStaff_Click");
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.HandleException(
                    ex,
                    "Personel listesi yenilenirken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.UI,
                    true);
            }
        }

        private void TxtSearchStaff_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SearchStaff(txtSearchStaff.Text);
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.HandleException(
                    ex,
                    "Personel arama işlemi sırasında beklenmeyen bir hata oluştu",
                    ErrorSeverity.Warning,
                    ErrorSource.UI,
                    false); // Küçük bir hata, kullanıcıya göstermeye gerek yok
            }
        }
    }
}