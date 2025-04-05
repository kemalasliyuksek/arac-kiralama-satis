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
        // DataTable for search/filter
        private DataTable staffTable;

        // Events for main form to handle
        public event EventHandler StaffAdded;

        public StaffControl()
        {
            InitializeComponent();

            // Setup data grid view properties
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            UIUtils.SetupDataGridView(dgvStaff);
        }

        public void LoadData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                // Personel verilerini DataTable olarak al
                staffTable = StaffMethods.GetStaffAsDataTable();

                // DataSource olarak ata 
                dgvStaff.DataSource = staffTable;

                // Format columns
                if (dgvStaff.Columns.Count > 0)
                {
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

                    // Durum sütunu için renklendirme
                    dgvStaff.CellFormatting += (s, e) => {
                        try
                        {
                            if (e.ColumnIndex == dgvStaff.Columns["Durum"].Index && e.Value != null)
                            {
                                string durumText = e.Value.ToString();
                                if (durumText == "Aktif")
                                    e.CellStyle.ForeColor = Color.FromArgb(40, 167, 69); // Yeşil
                                else if (durumText == "Pasif")
                                    e.CellStyle.ForeColor = Color.FromArgb(220, 53, 69); // Kırmızı

                                e.FormattingApplied = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Hücre formatlanırken hata: {ex.Message}");
                            // Hata durumunda formatlamayı atla
                        }
                    };
                }

                // Update count information
                lblStaffTitle.Text = $"Personel Listesi ({staffTable.Rows.Count})";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Personel verileri yüklenirken bir hata oluştu: {ex.Message}",
                    "Veri Yükleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    // Show all data
                    dgvStaff.DataSource = staffTable;
                    lblStaffTitle.Text = $"Personel Listesi ({staffTable.Rows.Count})";
                    return;
                }

                // Create filter
                string filter = "";

                // Search in most relevant columns
                filter = $"Ad LIKE '%{searchText}%' OR " +
                         $"Soyad LIKE '%{searchText}%' OR " +
                         $"[Kullanıcı Adı] LIKE '%{searchText}%' OR " +
                         $"Email LIKE '%{searchText}%' OR " +
                         $"Telefon LIKE '%{searchText}%' OR " +
                         $"Rol LIKE '%{searchText}%' OR " +
                         $"Şube LIKE '%{searchText}%'";

                DataView dv = staffTable.DefaultView;
                dv.RowFilter = filter;

                // Update label with count
                lblStaffTitle.Text = $"Personel Listesi ({dv.Count})";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Personel aramada hata: {ex.Message}");
                // Failed filter - just reset
                if (staffTable != null)
                {
                    dgvStaff.DataSource = staffTable;
                    lblStaffTitle.Text = $"Personel Listesi ({staffTable.Rows.Count})";
                }
            }
        }

        // Event handlers
        private void BtnAddStaff_Click(object sender, EventArgs e)
        {
            PersonelAddForm staffForm = new PersonelAddForm();
            staffForm.StaffAdded += (s, args) => {
                // Personel eklendiğinde listeyi yenile
                LoadData();

                // Event'i tetikle
                StaffAdded?.Invoke(this, EventArgs.Empty);
            };

            staffForm.ShowDialog();
        }

        private void BtnRefreshStaff_Click(object sender, EventArgs e)
        {
            LoadData();
            txtSearchStaff.Clear();
        }

        private void TxtSearchStaff_TextChanged(object sender, EventArgs e)
        {
            SearchStaff(txtSearchStaff.Text);
        }
    }
}