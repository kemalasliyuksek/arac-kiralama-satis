using System;
using System.Drawing;
using System.Windows.Forms;
using arac_kiralama_satis_desktop.Controls;
using arac_kiralama_satis_desktop.Utils;
using arac_kiralama_satis_desktop.Models;
using FontAwesome.Sharp;

namespace arac_kiralama_satis_desktop.Interfaces
{
    public partial class MainPage : Form
    {
        // Aktif buton referansı
        private IconButton currentBtn;

        // UserControls
        private DashboardControl dashboardControl;
        private VehiclesControl vehiclesControl;
        private CustomersControl customersControl;
        private BranchesControl branchesControl;
        private StaffControl staffControl;
        private RentalsControl rentalsControl;

        public MainPage()
        {
            try
            {
                InitializeComponent();
                CustomizeDesign();
                InitializeUserControls();

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

        private void InitializeUserControls()
        {
            // Create UserControl instances
            dashboardControl = new DashboardControl();
            vehiclesControl = new VehiclesControl();
            customersControl = new CustomersControl();
            branchesControl = new BranchesControl();
            staffControl = new StaffControl();
            rentalsControl = new RentalsControl();

            // Set dock style for all controls
            dashboardControl.Dock = DockStyle.Fill;
            vehiclesControl.Dock = DockStyle.Fill;
            customersControl.Dock = DockStyle.Fill;
            branchesControl.Dock = DockStyle.Fill;
            staffControl.Dock = DockStyle.Fill;
            rentalsControl.Dock = DockStyle.Fill;

            // Add to content panel but don't show them yet
            pnlContent.Controls.Add(dashboardControl);
            pnlContent.Controls.Add(vehiclesControl);
            pnlContent.Controls.Add(customersControl);
            pnlContent.Controls.Add(branchesControl);
            pnlContent.Controls.Add(staffControl);
            pnlContent.Controls.Add(rentalsControl);

            // Initially hide all except dashboard
            vehiclesControl.Visible = false;
            customersControl.Visible = false;
            branchesControl.Visible = false;
            staffControl.Visible = false;
            rentalsControl.Visible = false;

            // Subscribe to events
            branchesControl.BranchAdded += (s, e) => RefreshAllData();
            staffControl.StaffAdded += (s, e) => RefreshAllData();
            rentalsControl.RentalAdded += (s, e) => RefreshAllData();
        }

        private void RefreshAllData()
        {
            // This method refreshes all data in user controls when needed
            // For example, when a new branch is added, staff control needs to refresh its branch list

            // We'll implement this if/when needed in the future
            // For now, we'll just ensure the current view is refreshed
            RefreshCurrentView();
        }

        private void RefreshCurrentView()
        {
            // Determine which control is currently visible and refresh its data
            if (dashboardControl.Visible)
            {
                dashboardControl.LoadData();
            }
            else if (vehiclesControl.Visible)
            {
                vehiclesControl.LoadData();
            }
            else if (customersControl.Visible)
            {
                customersControl.LoadData();
            }
            else if (branchesControl.Visible)
            {
                branchesControl.LoadData();
            }
            else if (staffControl.Visible)
            {
                staffControl.LoadData();
            }
            else if (rentalsControl.Visible)
            {
                rentalsControl.LoadData();
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

        private void ShowControl(UserControl control)
        {
            // Hide all controls
            dashboardControl.Visible = false;
            vehiclesControl.Visible = false;
            customersControl.Visible = false;
            branchesControl.Visible = false;
            staffControl.Visible = false;
            rentalsControl.Visible = false;

            // Show selected control
            if (control != null)
            {
                control.Visible = true;
                control.BringToFront();
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
            // No need to handle resize here - each control will handle its own resize events
        }

        #region Event Handlers

        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowControl(dashboardControl);
            lblPageTitle.Text = "Dashboard";

            // Load dashboard data
            dashboardControl.LoadData();
        }

        private void BtnVehicles_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowControl(vehiclesControl);
            lblPageTitle.Text = "Araçlar";

            // Load vehicles data
            vehiclesControl.LoadData();
        }

        private void BtnCustomers_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowControl(customersControl);
            lblPageTitle.Text = "Müşteriler";

            // Load customers data
            customersControl.LoadData();
        }

        private void BtnBranches_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowControl(branchesControl);
            lblPageTitle.Text = "Şubeler";

            // Load branches data
            branchesControl.LoadData();
        }

        private void BtnStaff_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowControl(staffControl);
            lblPageTitle.Text = "Personeller";

            // Load staff data
            staffControl.LoadData();
        }

        private void BtnRentals_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowControl(rentalsControl);
            lblPageTitle.Text = "Kiralamalar";

            // Load rentals data
            rentalsControl.LoadData();
        }

        private void BtnSales_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowControl(null);
            lblPageTitle.Text = "Satışlar";

            // Future implementation
            MessageBox.Show("Satışlar modülü yakında eklenecek.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnMaintenance_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowControl(null);
            lblPageTitle.Text = "Bakım & Servis";

            // Future implementation
            MessageBox.Show("Bakım ve Servis modülü yakında eklenecek.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnReports_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowControl(null);
            lblPageTitle.Text = "Raporlar";

            // Future implementation
            MessageBox.Show("Raporlar modülü yakında eklenecek.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowControl(null);
            lblPageTitle.Text = "Ayarlar";

            // Future implementation
            MessageBox.Show("Ayarlar modülü yakında eklenecek.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                // Refresh current view
                RefreshCurrentView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veriler yenilenirken bir hata oluştu: {ex.Message}",
                    "Yenileme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        #endregion
    }
}