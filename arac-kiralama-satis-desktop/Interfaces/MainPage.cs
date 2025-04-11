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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ana sayfa yüklenirken bir hata oluştu: {ex.Message}\n\nDetay: {ex.StackTrace}",
                    "Kritik Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
            // Tam ekran (fullscreen) için
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = false; // Diğer pencerelerin üzerinde kalmasını sağlar

            // Ekranın tam boyutunu kullan
            this.Bounds = Screen.PrimaryScreen.Bounds;

            this.Text = "Araç Kiralama ve Satış Uygulaması";

            picUserAvatar.Image = IconChar.UserCircle.ToBitmap(Color.White, 32);

            lblUserName.Text = CurrentSession.FullName;
            lblUserRole.Text = CurrentSession.RoleName;
            lblBranchName.Text = CurrentSession.BranchName ?? "Genel Merkez";

            ActivateButton(btnDashboard);

            // ESC tuşuna basıldığında tam ekrandan çıkılabilmesi için
            this.KeyPreview = true;
            this.KeyDown += (s, e) => {
                if (e.KeyCode == Keys.Escape)
                {
                    this.WindowState = FormWindowState.Normal;
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                    this.TopMost = false;
                }
            };
        }

        private void InitializeUserControls()
        {
            dashboardControl = new DashboardControl();
            vehiclesControl = new VehiclesControl();
            customersControl = new CustomersControl();
            branchesControl = new BranchesControl();
            staffControl = new StaffControl();
            rentalsControl = new RentalsControl();

            dashboardControl.Dock = DockStyle.Fill;
            vehiclesControl.Dock = DockStyle.Fill;
            customersControl.Dock = DockStyle.Fill;
            branchesControl.Dock = DockStyle.Fill;
            staffControl.Dock = DockStyle.Fill;
            rentalsControl.Dock = DockStyle.Fill;

            pnlContent.Controls.Add(dashboardControl);
            pnlContent.Controls.Add(vehiclesControl);
            pnlContent.Controls.Add(customersControl);
            pnlContent.Controls.Add(branchesControl);
            pnlContent.Controls.Add(staffControl);
            pnlContent.Controls.Add(rentalsControl);

            vehiclesControl.Visible = false;
            customersControl.Visible = false;
            branchesControl.Visible = false;
            staffControl.Visible = false;
            rentalsControl.Visible = false;

            branchesControl.BranchAdded += (s, e) => RefreshAllData();
            staffControl.StaffAdded += (s, e) => RefreshAllData();
            rentalsControl.RentalAdded += (s, e) => RefreshAllData();
        }

        private void RefreshAllData()
        {
            RefreshCurrentView();
        }

        private void RefreshCurrentView()
        {
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

                currentBtn = button;
                currentBtn.BackColor = Color.FromArgb(83, 107, 168);
                currentBtn.ForeColor = Color.White;
                currentBtn.IconColor = Color.White;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;

                lblPageTitle.Text = currentBtn.Text.Trim();
            }
        }

        private void DisableOtherButtons()
        {
            foreach (Control previousBtn in pnlMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(IconButton))
                {
                    previousBtn.BackColor = Color.FromArgb(49, 76, 143);
                    previousBtn.ForeColor = Color.White;
                    ((IconButton)previousBtn).IconColor = Color.White;
                }
            }
        }

        private void ShowControl(UserControl control)
        {
            dashboardControl.Visible = false;
            vehiclesControl.Visible = false;
            customersControl.Visible = false;
            branchesControl.Visible = false;
            staffControl.Visible = false;
            rentalsControl.Visible = false;

            if (control != null)
            {
                control.Visible = true;
                control.BringToFront();
            }
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            BtnDashboard_Click(btnDashboard, EventArgs.Empty);
        }

        #region Event Handlers

        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowControl(dashboardControl);
            lblPageTitle.Text = "Dashboard";

            dashboardControl.LoadData();
        }

        private void BtnVehicles_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowControl(vehiclesControl);
            lblPageTitle.Text = "Araçlar";

            vehiclesControl.LoadData();
        }

        private void BtnCustomers_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowControl(customersControl);
            lblPageTitle.Text = "Müşteriler";

            customersControl.LoadData();
        }

        private void BtnBranches_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowControl(branchesControl);
            lblPageTitle.Text = "Şubeler";

            branchesControl.LoadData();
        }

        private void BtnStaff_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowControl(staffControl);
            lblPageTitle.Text = "Personeller";

            staffControl.LoadData();
        }

        private void BtnRentals_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowControl(rentalsControl);
            lblPageTitle.Text = "Kiralamalar";

            rentalsControl.LoadData();
        }

        private void BtnSales_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowControl(null);
            lblPageTitle.Text = "Satışlar";

            MessageBox.Show("Satışlar modülü yakında eklenecek.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnMaintenance_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowControl(null);
            lblPageTitle.Text = "Bakım & Servis";

            MessageBox.Show("Bakım ve Servis modülü yakında eklenecek.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnReports_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowControl(null);
            lblPageTitle.Text = "Raporlar";

            MessageBox.Show("Raporlar modülü yakında eklenecek.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowControl(null);
            lblPageTitle.Text = "Ayarlar";

            MessageBox.Show("Ayarlar modülü yakında eklenecek.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Oturumu kapatmak istediğinize emin misiniz?",
                "Oturumu Kapat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                CurrentSession.ClearSession();

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