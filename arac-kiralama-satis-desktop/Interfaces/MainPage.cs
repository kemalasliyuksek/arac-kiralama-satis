using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using arac_kiralama_satis_desktop.Methods;
using arac_kiralama_satis_desktop.Utils;
using arac_kiralama_satis_desktop.Models;
using FontAwesome.Sharp;

namespace arac_kiralama_satis_desktop.Interfaces
{
    public partial class MainPage : Form
    {
        // Aktif buton referansı
        private IconButton currentBtn;
        // DataTable'lar for search/filter
        private DataTable vehiclesTable;
        private DataTable customersTable;
        private DataTable branchesTable;

        public MainPage()
        {
            try
            {
                InitializeComponent();
                CustomizeDesign();
                SetupDataGridViews();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ana sayfa yüklenirken bir hata oluştu: {ex.Message}\n\nDetay: {ex.StackTrace}",
                    "Kritik Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Hata logunu yazdır
                Console.WriteLine($"HATA: {ex.Message}");
                Console.WriteLine($"STACK TRACE: {ex.StackTrace}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"INNER EXCEPTION: {ex.InnerException.Message}");
                    Console.WriteLine($"INNER STACK TRACE: {ex.InnerException.StackTrace}");
                }
            }
        }

        private void CustomizeDesign()
        {
            // Form ayarları
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MinimumSize = new Size(1200, 800);
            this.MaximizeBox = true;
            this.WindowState = FormWindowState.Maximized;
            this.Text = "Araç Kiralama ve Satış Uygulaması";

            // Logo ve kullanıcı avatarı
            picUserAvatar.Image = IconChar.UserCircle.ToBitmap(Color.White, 32);

            // Kullanıcı bilgilerini göster
            lblUserName.Text = CurrentSession.FullName;
            lblUserRole.Text = CurrentSession.RoleName;
            lblBranchName.Text = CurrentSession.BranchName ?? "Genel Merkez";

            // Aktif menü butonunu işaretle
            ActivateButton(btnDashboard);
        }

        private void SetupDataGridViews()
        {
            // Set up vehicles DataGridView
            UIUtils.SetupDataGridView(dgvVehicles);

            // Set up customers DataGridView
            UIUtils.SetupDataGridView(dgvCustomers);

            // Set up branches DataGridView
            UIUtils.SetupDataGridView(dgvBranches);
        }

        private void LoadVehiclesData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                List<Vehicle> vehicles = VehicleMethods.GetVehicles();
                vehiclesTable = new DataTable();

                // Add columns
                vehiclesTable.Columns.Add("AracID", typeof(int));
                vehiclesTable.Columns.Add("Plaka", typeof(string));
                vehiclesTable.Columns.Add("Marka", typeof(string));
                vehiclesTable.Columns.Add("Model", typeof(string));
                vehiclesTable.Columns.Add("Yıl", typeof(int));
                vehiclesTable.Columns.Add("Renk", typeof(string));
                vehiclesTable.Columns.Add("Kilometre", typeof(int));
                vehiclesTable.Columns.Add("Yakıt", typeof(string));
                vehiclesTable.Columns.Add("Vites", typeof(string));
                vehiclesTable.Columns.Add("Durum", typeof(string));
                vehiclesTable.Columns.Add("Şube", typeof(string));
                vehiclesTable.Columns.Add("Sınıf", typeof(string));

                // Add rows from the vehicles list
                foreach (var vehicle in vehicles)
                {
                    vehiclesTable.Rows.Add(
                        vehicle.VehicleID,
                        vehicle.Plate,
                        vehicle.Brand,
                        vehicle.Model,
                        vehicle.Year,
                        vehicle.Color,
                        vehicle.Kilometers,
                        vehicle.FuelType,
                        vehicle.TransmissionType,
                        vehicle.StatusName,
                        vehicle.BranchName,
                        vehicle.VehicleClassName
                    );
                }

                // Set DataSource
                dgvVehicles.DataSource = vehiclesTable;

                // Format columns
                if (dgvVehicles.Columns.Count > 0)
                {
                    dgvVehicles.Columns["AracID"].Visible = false;
                    dgvVehicles.Columns["Plaka"].Width = 80;
                    dgvVehicles.Columns["Marka"].Width = 100;
                    dgvVehicles.Columns["Model"].Width = 120;
                    dgvVehicles.Columns["Yıl"].Width = 60;
                    dgvVehicles.Columns["Renk"].Width = 80;
                    dgvVehicles.Columns["Kilometre"].Width = 100;
                    dgvVehicles.Columns["Yakıt"].Width = 80;
                    dgvVehicles.Columns["Vites"].Width = 80;
                    dgvVehicles.Columns["Durum"].Width = 100;
                    dgvVehicles.Columns["Şube"].Width = 120;
                    dgvVehicles.Columns["Sınıf"].Width = 100;
                }

                // Update count information
                lblVehiclesTitle.Text = $"Araç Listesi ({vehicles.Count})";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Araç verileri yüklenirken bir hata oluştu: {ex.Message}",
                    "Veri Yükleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

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

        private void SearchVehicles(string searchText)
        {
            try
            {
                if (vehiclesTable == null) return;

                if (string.IsNullOrWhiteSpace(searchText))
                {
                    // Show all data
                    dgvVehicles.DataSource = vehiclesTable;
                    lblVehiclesTitle.Text = $"Araç Listesi ({vehiclesTable.Rows.Count})";
                    return;
                }

                // Create a case-insensitive filter
                string filter = "";
                string searchLower = searchText.ToLower();

                // Search in most relevant columns
                filter = $"Plaka LIKE '%{searchText}%' OR " +
                         $"Marka LIKE '%{searchText}%' OR " +
                         $"Model LIKE '%{searchText}%' OR " +
                         $"Durum LIKE '%{searchText}%' OR " +
                         $"Şube LIKE '%{searchText}%' OR " +
                         $"CONVERT(Yıl, System.String) LIKE '%{searchText}%'";

                DataView dv = vehiclesTable.DefaultView;
                dv.RowFilter = filter;

                // Update label with count
                lblVehiclesTitle.Text = $"Araç Listesi ({dv.Count})";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Araç aramada hata: {ex.Message}");
                // Failed filter - just reset
                if (vehiclesTable != null)
                {
                    dgvVehicles.DataSource = vehiclesTable;
                    lblVehiclesTitle.Text = $"Araç Listesi ({vehiclesTable.Rows.Count})";
                }
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

        private void ActivateButton(IconButton button)
        {
            if (button != null)
            {
                DisableOtherButtons();

                // Aktif butonu işaretle
                currentBtn = button;
                currentBtn.BackColor = Color.FromArgb(83, 107, 168);  // Daha açık mavi
                currentBtn.ForeColor = Color.White;
                currentBtn.IconColor = Color.White;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;

                // Başlığı güncelle
                lblPageTitle.Text = currentBtn.Text.Trim();
            }
        }

        private void DisableOtherButtons()
        {
            // Diğer bütün butonları devre dışı bırak
            foreach (Control previousBtn in pnlMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(IconButton))
                {
                    previousBtn.BackColor = Color.FromArgb(49, 76, 143);  // Ana menü rengi
                    previousBtn.ForeColor = Color.White;
                    ((IconButton)previousBtn).IconColor = Color.White;
                }
            }
        }

        private void ShowPanel(Panel panel)
        {
            // Hide all panels
            pnlDashboard.Visible = false;
            pnlVehicles.Visible = false;
            pnlCustomers.Visible = false;
            pnlBranches.Visible = false;

            // Show selected panel
            if (panel != null)
            {
                panel.Visible = true;
                panel.BringToFront();
            }
        }

        // Form yüklendiğinde gerçekleştirilecek işlemler
        private void MainPage_Load(object sender, EventArgs e)
        {
            // Formu maksimize et
            this.WindowState = FormWindowState.Maximized;

        }

        #region Event Handlers

        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowPanel(pnlDashboard);
            lblPageTitle.Text = "Dashboard";
        }

        private void BtnVehicles_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowPanel(pnlVehicles);
            LoadVehiclesData();
            lblPageTitle.Text = "Araçlar";
        }

        private void BtnCustomers_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowPanel(pnlCustomers);
            LoadCustomersData();
            lblPageTitle.Text = "Müşteriler";
        }

        private void BtnBranches_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowPanel(pnlBranches);
            LoadBranchesData();
            lblPageTitle.Text = "Şubeler";
        }

        private void BtnRentals_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowPanel(null);
            lblPageTitle.Text = "Kiralamalar";
        }

        private void BtnSales_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowPanel(null);
            lblPageTitle.Text = "Satışlar";
        }

        private void BtnMaintenance_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowPanel(null);
            lblPageTitle.Text = "Bakım & Servis";
        }

        private void BtnReports_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowPanel(null);
            lblPageTitle.Text = "Raporlar";
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowPanel(null);
            lblPageTitle.Text = "Ayarlar";
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            // Oturumu kapat
            DialogResult result = MessageBox.Show("Oturumu kapatmak istediğinize emin misiniz?",
                "Oturumu Kapat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Oturum bilgilerini temizle
                CurrentSession.ClearSession();

                // Login sayfasına yönlendir
                LoginPage loginPage = new LoginPage();
                loginPage.Show();
                this.Close();
            }
        }

        private void TxtSearchVehicles_TextChanged(object sender, EventArgs e)
        {
            SearchVehicles(txtSearchVehicles.Text);
        }

        private void TxtSearchCustomers_TextChanged(object sender, EventArgs e)
        {
            SearchCustomers(txtSearchCustomers.Text);
        }

        private void TxtSearchBranches_TextChanged(object sender, EventArgs e)
        {
            SearchBranches(txtSearchBranches.Text);
        }

        private void BtnRefreshVehicles_Click(object sender, EventArgs e)
        {
            LoadVehiclesData();
            txtSearchVehicles.Clear();
        }

        private void BtnRefreshCustomers_Click(object sender, EventArgs e)
        {
            LoadCustomersData();
            txtSearchCustomers.Clear();
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

        #endregion
    }
}