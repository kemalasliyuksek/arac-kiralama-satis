using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
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

        public MainPage()
        {
            try
            {
                InitializeComponent();
                CustomizeDesign();
                InitializeDashboard();
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
            picLogo.Image = IconChar.Car.ToBitmap(Color.White, 32);
            picUserAvatar.Image = IconChar.UserCircle.ToBitmap(Color.White, 32);

            // Kullanıcı bilgilerini göster
            lblUserName.Text = CurrentSession.FullName;
            lblUserRole.Text = CurrentSession.RoleName;
            lblBranchName.Text = CurrentSession.BranchName ?? "Genel Merkez";

            // Aktif menü butonunu işaretle
            ActivateButton(btnDashboard);
        }

        private void InitializeDashboard()
        {
            // Boş dashboard - hiçbir eleman gösterilmeyecek
            pnlDashboard.Controls.Clear();
            pnlDashboard.BackColor = Color.FromArgb(245, 245, 250);
        }

        private void LayoutDashboardPanels()
        {
            // Sabit pencere boyutu için panel konumlarını ayarla
            int padding = 15;

            // Kart bölümü düzeni
            int availableWidth = pnlCards.Width - (padding * 2);
            int cardWidth = (availableWidth - (padding * 3)) / 4;

            // Kart panellerinin boyutlarını ve konumlarını ayarla
            pnlTotalCars.Width = cardWidth;
            pnlLocations.Width = cardWidth;
            pnlCarBrands.Width = cardWidth;
            pnlAvgRentalPrice.Width = cardWidth;

            pnlTotalCars.Location = new Point(padding, padding);
            pnlLocations.Location = new Point(pnlTotalCars.Right + padding, padding);
            pnlCarBrands.Location = new Point(pnlLocations.Right + padding, padding);
            pnlAvgRentalPrice.Location = new Point(pnlCarBrands.Right + padding, padding);

            // Grafik bölümü düzeni
            availableWidth = pnlCharts.Width - (padding * 2);
            int chartWidth = (availableWidth - (padding * 2)) / 3;
            int chartHeight = pnlCharts.Height - (padding * 2);

            // Grafik panellerinin boyutlarını ve konumlarını ayarla
            pnlCarsByBrand.Size = new Size(chartWidth, chartHeight);
            pnlRentalsByYear.Size = new Size(chartWidth, chartHeight);
            pnlCarsByLocation.Size = new Size(chartWidth, chartHeight);

            pnlCarsByBrand.Location = new Point(padding, padding);
            pnlRentalsByYear.Location = new Point(pnlCarsByBrand.Right + padding, padding);
            pnlCarsByLocation.Location = new Point(pnlRentalsByYear.Right + padding, padding);

            // Grafiklerin ve panellerin yenilenmesi
            pnlCards.Refresh();
            pnlCharts.Refresh();
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

        // Olay işleyicileri
        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            // Dashboard içeriğini göster
            pnlDashboard.Visible = true;
            lblPageTitle.Text = "Dashboard";
        }

        private void BtnVehicles_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            // Araçlar içeriğini göster (boş panel)
            pnlDashboard.Visible = false;
            lblPageTitle.Text = "Araçlar";
        }

        private void BtnCustomers_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            // Müşteriler içeriğini göster (boş panel)
            pnlDashboard.Visible = false;
            lblPageTitle.Text = "Müşteriler";
        }

        private void BtnRentals_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            // Kiralamalar içeriğini göster (boş panel)
            pnlDashboard.Visible = false;
            lblPageTitle.Text = "Kiralamalar";
        }

        private void BtnSales_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            // Satışlar içeriğini göster (boş panel)
            pnlDashboard.Visible = false;
            lblPageTitle.Text = "Satışlar";
        }

        private void BtnMaintenance_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            // Bakım ve Servis içeriğini göster (boş panel)
            pnlDashboard.Visible = false;
            lblPageTitle.Text = "Bakım & Servis";
        }

        private void BtnReports_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            // Raporlar içeriğini göster (boş panel)
            pnlDashboard.Visible = false;
            lblPageTitle.Text = "Raporlar";
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            // Ayarlar içeriğini göster (boş panel)
            pnlDashboard.Visible = false;
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

        // Form yüklendiğinde kontrolleri düzenle
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Formu maksimize et
            this.WindowState = FormWindowState.Maximized;

            // Panelleri düzenle
            LayoutDashboardPanels();

            // UI'ın yenilenmesi için
            Application.DoEvents();
        }

        // Form boyutu değiştiğinde kontrolleri yeniden düzenle
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (this.WindowState != FormWindowState.Minimized)
            {
                LayoutDashboardPanels();
            }
        }
    }
}