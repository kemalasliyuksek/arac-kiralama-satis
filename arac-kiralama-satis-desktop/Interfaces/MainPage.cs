using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing.Drawing2D;
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

        // Dashboard controls
        private Panel[] metricPanels;
        private Label[] metricTitles;
        private Label[] metricValues;

        public MainPage()
        {
            try
            {
                InitializeComponent();
                CustomizeDesign();
                SetupDataGridViews();

                // Form resize event için handler ekle
                this.Resize += MainPage_Resize;
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

        private void InitializeDashboard()
        {
            if (metricPanels != null)
                return; // Dashboard already initialized

            // Clear existing controls
            pnlCharts.Controls.Clear();

            // Create metric panels
            int panelCount = 8; // Increased from 4 to 8 panels
            int panelWidth = 220;
            int panelHeight = 120;
            int panelSpacing = 20;

            // Calculate how many panels per row based on available width
            int panelsPerRow = 4;
            int startX = (pnlCharts.Width - (panelWidth * panelsPerRow + panelSpacing * (panelsPerRow - 1))) / 2;

            metricPanels = new Panel[panelCount];
            metricTitles = new Label[panelCount];
            metricValues = new Label[panelCount];

            // Define titles for all metrics
            string[] titles = new string[] {
                "Toplam Araç",
                "Şube Sayısı",
                "Müşteri Sayısı",
                "Toplam Gelir (₺)",
                "Aktif Kiralamalar",
                "Bu Ay Satışlar",
                "Servis Bekleyen",
                "Ekip Üyeleri"
            };

            // Define colors for each panel
            Color[] colors = new Color[] {
                Color.FromArgb(83, 107, 168),   // Mavi
                Color.FromArgb(40, 167, 69),    // Yeşil
                Color.FromArgb(255, 193, 7),    // Sarı
                Color.FromArgb(23, 162, 184),   // Turkuaz
                Color.FromArgb(220, 53, 69),    // Kırmızı
                Color.FromArgb(111, 66, 193),   // Mor
                Color.FromArgb(253, 126, 20),   // Turuncu
                Color.FromArgb(108, 117, 125)   // Gri
            };

            // Create panels in a grid layout (2 rows x 4 columns)
            for (int i = 0; i < panelCount; i++)
            {
                int row = i / panelsPerRow;
                int col = i % panelsPerRow;

                // Create panel
                metricPanels[i] = new Panel
                {
                    Width = panelWidth,
                    Height = panelHeight,
                    Left = startX + (panelWidth + panelSpacing) * col,
                    Top = 20 + (panelHeight + panelSpacing) * row,
                    BackColor = Color.White
                };

                // Apply shadow and rounded corners
                UIUtils.ApplyShadowEffect(metricPanels[i]);

                // Create title label
                metricTitles[i] = new Label
                {
                    Text = titles[i],
                    Font = new Font("Segoe UI", 10, FontStyle.Regular),
                    ForeColor = Color.FromArgb(100, 100, 100),
                    Left = 15,
                    Top = 15,
                    AutoSize = true
                };

                // Create value label
                metricValues[i] = new Label
                {
                    Text = "0",
                    Font = new Font("Segoe UI", 22, FontStyle.Bold),
                    ForeColor = colors[i],
                    Left = 15,
                    Top = 45,
                    AutoSize = true
                };

                // Create icon label
                Label iconLabel = new Label
                {
                    Text = "•",
                    Font = new Font("Segoe UI", 28, FontStyle.Bold),
                    ForeColor = colors[i],
                    Left = panelWidth - 45,
                    Top = 35,
                    AutoSize = true
                };

                // Add controls to panel
                metricPanels[i].Controls.Add(metricTitles[i]);
                metricPanels[i].Controls.Add(metricValues[i]);
                metricPanels[i].Controls.Add(iconLabel);

                // Add panel to charts panel
                pnlCharts.Controls.Add(metricPanels[i]);
            }

            // Add additional informational panels if needed
            // For example, a recent activity panel or announcements panel
            Panel recentActivityPanel = new Panel
            {
                Width = pnlCharts.Width - 40,
                Height = 300,
                Left = 20,
                Top = 20 + (panelHeight + panelSpacing) * 2 + 20,
                BackColor = Color.White
            };

            UIUtils.ApplyShadowEffect(recentActivityPanel);

            Label activityTitle = new Label
            {
                Text = "Son Aktiviteler",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(49, 76, 143),
                Left = 15,
                Top = 15,
                AutoSize = true
            };

            recentActivityPanel.Controls.Add(activityTitle);
            pnlCharts.Controls.Add(recentActivityPanel);
        }

        private void LoadDashboardData()
        {
            try
            {
                // Show loading indicator
                Cursor = Cursors.WaitCursor;

                // Get dashboard data from database
                var dashboardData = MainMethods.GetDashboardData();

                // Update metric values
                if (metricValues != null && metricValues.Length >= 8)
                {
                    // Update existing metrics
                    metricValues[0].Text = dashboardData.TotalCarCount.ToString("N0");
                    metricValues[1].Text = dashboardData.LocationCount.ToString("N0");
                    metricValues[2].Text = dashboardData.CustomerCount.ToString("N0");
                    metricValues[3].Text = dashboardData.TotalRevenue.ToString("N0");

                    // Get additional metrics from database
                    int activeRentals = MainMethods.GetActiveRentalsCount();
                    int monthlySales = MainMethods.GetMonthlySalesCount();
                    int pendingService = MainMethods.GetPendingServiceCount();
                    int teamMembers = MainMethods.GetTeamMembersCount();

                    // Update additional metrics
                    metricValues[4].Text = activeRentals.ToString("N0");
                    metricValues[5].Text = monthlySales.ToString("N0");
                    metricValues[6].Text = pendingService.ToString("N0");
                    metricValues[7].Text = teamMembers.ToString("N0");
                }

                // Load recent activities data
                LoadRecentActivities();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Dashboard verisi yüklenirken hata oluştu: {ex.Message}",
                    "Veri Yükleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Hide loading indicator
                Cursor = Cursors.Default;
            }
        }

        private void LoadRecentActivities()
        {
            // Son aktiviteler paneli bul 
            Panel recentActivityPanel = null;

            foreach (Control ctrl in pnlCharts.Controls)
            {
                if (ctrl is Panel && ctrl != null && !metricPanels.Contains(ctrl))
                {
                    recentActivityPanel = ctrl as Panel;
                    break;
                }
            }

            if (recentActivityPanel == null)
                return;

            // Mevcut içeriği temizle (başlık dışında)
            Label titleLabel = null;

            foreach (Control ctrl in recentActivityPanel.Controls)
            {
                if (ctrl is Label && ctrl.Text == "Son Aktiviteler")
                {
                    titleLabel = ctrl as Label;
                }
            }

            recentActivityPanel.Controls.Clear();

            if (titleLabel != null)
                recentActivityPanel.Controls.Add(titleLabel);

            // Örnek aktiviteler ekle
            string[] sampleActivities = new string[]
            {
                "Bugün, 10:15 - Yeni araç kaydedildi: BMW 320i, 34AC123",
                "Bugün, 09:30 - Müşteri kaydı: Ahmet Yılmaz",
                "Dün, 16:45 - Kiralama başlatıldı: Mercedes C180, 5 gün",
                "Dün, 14:20 - Servis tamamlandı: Audi A3, 34ZT456",
                "Dün, 11:00 - Satış gerçekleşti: Volkswagen Passat",
                "24.03.2025, 15:30 - Yeni şube açıldı: İzmir Merkez"
            };

            int top = titleLabel != null ? titleLabel.Bottom + 15 : 15;

            foreach (string activity in sampleActivities)
            {
                Label activityLabel = new Label
                {
                    Text = activity,
                    AutoSize = false,
                    Width = recentActivityPanel.Width - 30,
                    Height = 25,
                    Left = 15,
                    Top = top,
                    Font = new Font("Segoe UI", 9)
                };

                recentActivityPanel.Controls.Add(activityLabel);
                top += 30;
            }
        }

        private void ResizeDashboardComponents()
        {
            if (metricPanels == null)
                return;

            int panelCount = metricPanels.Length;
            int panelWidth = 220;
            int panelHeight = 120;
            int panelSpacing = 20;
            int panelsPerRow = 4;
            int startX = (pnlCharts.Width - (panelWidth * panelsPerRow + panelSpacing * (panelsPerRow - 1))) / 2;

            // Resize metric panels
            for (int i = 0; i < panelCount; i++)
            {
                int row = i / panelsPerRow;
                int col = i % panelsPerRow;

                metricPanels[i].Left = startX + (panelWidth + panelSpacing) * col;
                metricPanels[i].Top = 20 + (panelHeight + panelSpacing) * row;
            }

            // Resize recent activity panel (if it exists)
            Panel recentActivityPanel = null;

            foreach (Control ctrl in pnlCharts.Controls)
            {
                if (ctrl is Panel && ctrl != null && !metricPanels.Contains(ctrl))
                {
                    recentActivityPanel = ctrl as Panel;
                    break;
                }
            }

            if (recentActivityPanel != null)
            {
                recentActivityPanel.Width = pnlCharts.Width - 40;
                recentActivityPanel.Left = 20;
            }
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

            // Dashboard'ı varsayılan olarak yükle
            BtnDashboard_Click(btnDashboard, EventArgs.Empty);
        }

        // Form yeniden boyutlandırıldığında çağrılır
        private void MainPage_Resize(object sender, EventArgs e)
        {
            ResizeDashboardComponents();
        }

        #region Event Handlers

        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowPanel(pnlDashboard);
            lblPageTitle.Text = "Dashboard";

            // Dashboard'ı başlat ve verileri yükle
            InitializeDashboard();
            LoadDashboardData();
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