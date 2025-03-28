using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using arac_kiralama_satis_desktop.Methods;
using arac_kiralama_satis_desktop.Utils;
using FontAwesome.Sharp;

namespace arac_kiralama_satis_desktop.Interfaces
{
    public partial class MainPage : Form
    {
        // Aktif buton referansı
        private IconButton currentBtn;

        // Araç durumlarını temsil eden renkler
        private readonly Dictionary<string, Color> statusColors = new Dictionary<string, Color>
        {
            { "Müsait", Color.FromArgb(40, 167, 69) },    // Yeşil
            { "Satılık", Color.FromArgb(0, 123, 255) },   // Mavi
            { "Satıldı", Color.FromArgb(108, 117, 125) }, // Gri
            { "Kirada", Color.FromArgb(255, 193, 7) },    // Sarı
            { "Serviste", Color.FromArgb(220, 53, 69) },  // Kırmızı
            { "Arızalı", Color.FromArgb(220, 53, 69) },   // Kırmızı
            { "Bakımda", Color.FromArgb(255, 193, 7) },   // Sarı
        };

        public MainPage()
        {
            InitializeComponent();
            CustomizeDesign();
            InitializeDashboard();
        }

        private void CustomizeDesign()
        {
            // Form ayarları
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MinimumSize = new Size(1200, 800);
            this.Text = "Araç Kiralama ve Satış Uygulaması";

            // Logo ve kullanıcı avatarı
            picLogo.Image = IconChar.Car.ToBitmap(Color.White, 32);
            picUserAvatar.Image = IconChar.UserCircle.ToBitmap(Color.White, 32);

            // Kullanıcı bilgilerini göster
            lblUserName.Text = CurrentSession.FullName;
            lblUserRole.Text = CurrentSession.RoleName;
            lblBranchName.Text = CurrentSession.BranchName ?? "Genel Merkez";

            // Kartlar için gölge efekti
            ApplyShadowEffect(pnlTotalCars);
            ApplyShadowEffect(pnlLocations);
            ApplyShadowEffect(pnlCarBrands);
            ApplyShadowEffect(pnlAvgRentalPrice);

            // Grafik panelleri için gölge efekti
            ApplyShadowEffect(pnlCarsByBrand);
            ApplyShadowEffect(pnlRentalsByYear);
            ApplyShadowEffect(pnlCarsByLocation);

            // Panellerin köşelerini yuvarlaklaştır
            ApplyRoundedCorners(pnlTotalCars, 10);
            ApplyRoundedCorners(pnlLocations, 10);
            ApplyRoundedCorners(pnlCarBrands, 10);
            ApplyRoundedCorners(pnlAvgRentalPrice, 10);
            ApplyRoundedCorners(pnlCarsByBrand, 10);
            ApplyRoundedCorners(pnlRentalsByYear, 10);
            ApplyRoundedCorners(pnlCarsByLocation, 10);

            // Aktif menü butonunu işaretle
            ActivateButton(btnDashboard);
        }

        private void InitializeDashboard()
        {
            // Dashboard verilerini yükle
            LoadDashboardData();

            // Grafikleri oluştur
            InitializeCharts();

            // Ekrandaki panelleri yerleştir
            LayoutDashboardPanels();
        }

        private void LoadDashboardData()
        {
            try
            {
                // Ana sayfa verilerini yükle
                var dashboardData = MainMethods.GetDashboardData();

                // Kart verilerini güncelle
                lblCarCount.Text = dashboardData.TotalCarCount.ToString();
                lblLocationCount.Text = dashboardData.LocationCount.ToString();
                lblBrandCount.Text = dashboardData.BrandCount.ToString();
                lblAvgPrice.Text = $"₺ {dashboardData.AverageRentalPrice:N2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dashboard verileri yüklenirken bir hata oluştu: " + ex.Message,
                    "Veri Yükleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeCharts()
        {
            try
            {
                // Marka dağılım grafiğini oluştur ve yükle
                Chart brandChart = CreatePieChart();
                brandChart.Dock = DockStyle.Fill;
                chartCarsByBrand.Controls.Add(brandChart);
                LoadBrandDistributionData(brandChart);

                // Kiralama yıl grafiğini oluştur ve yükle
                Chart yearChart = CreateLineChart();
                yearChart.Dock = DockStyle.Fill;
                chartRentalsByYear.Controls.Add(yearChart);
                LoadRentalYearData(yearChart);

                // Lokasyon grafiğini oluştur ve yükle
                Chart locationChart = CreateBarChart();
                locationChart.Dock = DockStyle.Fill;
                chartCarsByLocation.Controls.Add(locationChart);
                LoadLocationData(locationChart);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Grafikler oluşturulurken bir hata oluştu: " + ex.Message,
                    "Grafik Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Chart CreatePieChart()
        {
            Chart chart = new Chart();

            // ChartArea oluştur
            ChartArea chartArea = new ChartArea();
            chartArea.BackColor = Color.White;
            chart.ChartAreas.Add(chartArea);

            // Series oluştur
            Series series = new Series
            {
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = true,
                LabelFormat = "{0}",
                Font = new Font("Segoe UI", 9)
            };

            chart.Series.Add(series);

            // Ekstra özellikler
            chart.Legends.Add(new Legend());
            chart.BackColor = Color.White;
            chart.BorderlineWidth = 0;
            chart.BorderSkin.SkinStyle = BorderSkinStyle.None;

            return chart;
        }

        private Chart CreateLineChart()
        {
            Chart chart = new Chart();

            // ChartArea oluştur
            ChartArea chartArea = new ChartArea();
            chartArea.BackColor = Color.White;
            chartArea.AxisX.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisX.LabelStyle.Font = new Font("Segoe UI", 8);
            chartArea.AxisY.LabelStyle.Font = new Font("Segoe UI", 8);
            chart.ChartAreas.Add(chartArea);

            // Series oluştur
            Series series = new Series
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 3,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 8,
                MarkerColor = Color.FromArgb(26, 115, 232),
                Color = Color.FromArgb(26, 115, 232)
            };

            chart.Series.Add(series);
            chart.BackColor = Color.White;

            return chart;
        }

        private Chart CreateBarChart()
        {
            Chart chart = new Chart();

            // ChartArea oluştur
            ChartArea chartArea = new ChartArea();
            chartArea.BackColor = Color.White;
            chartArea.AxisX.MajorGrid.LineColor = Color.Transparent;
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisX.LabelStyle.Font = new Font("Segoe UI", 8);
            chartArea.AxisY.LabelStyle.Font = new Font("Segoe UI", 8);
            chart.ChartAreas.Add(chartArea);

            // Series oluştur
            Series series = new Series
            {
                ChartType = SeriesChartType.Column,
                Color = Color.FromArgb(156, 136, 255)
            };

            chart.Series.Add(series);
            chart.BackColor = Color.White;

            return chart;
        }

        private void LoadBrandDistributionData(Chart chart)
        {
            try
            {
                // Marka dağılım verilerini yükle
                var brandDistribution = MainMethods.GetBrandDistribution();

                chart.Series[0].Points.Clear();
                Dictionary<string, Color> brandColors = new Dictionary<string, Color>
                {
                    { "Mercedes", Color.FromArgb(34, 139, 34) },  // ForestGreen
                    { "BMW", Color.FromArgb(65, 105, 225) },      // RoyalBlue
                    { "Audi", Color.FromArgb(70, 130, 180) },     // SteelBlue
                    { "Range Rover", Color.FromArgb(148, 0, 211) },// DarkViolet
                    { "Ford", Color.FromArgb(30, 144, 255) },     // DodgerBlue
                    { "Alfa Romeo", Color.FromArgb(47, 79, 79) }, // DarkSlateGray
                    { "McLaren", Color.FromArgb(0, 191, 255) },   // DeepSkyBlue
                    { "Jeep", Color.FromArgb(199, 21, 133) }      // MediumVioletRed
                };

                foreach (var brand in brandDistribution)
                {
                    int pointIndex = chart.Series[0].Points.AddXY(brand.Key, brand.Value);
                    if (brandColors.ContainsKey(brand.Key))
                    {
                        chart.Series[0].Points[pointIndex].Color = brandColors[brand.Key];
                    }
                    chart.Series[0].Points[pointIndex].LegendText = brand.Key;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Marka dağılım grafiği yüklenirken bir hata oluştu: " + ex.Message,
                    "Grafik Veri Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRentalYearData(Chart chart)
        {
            try
            {
                // Yıllara göre kiralama verilerini yükle
                var yearlyRentals = MainMethods.GetYearlyRentals();

                chart.Series[0].Points.Clear();

                foreach (var rental in yearlyRentals)
                {
                    chart.Series[0].Points.AddXY(rental.Key, rental.Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Yıllık kiralama grafiği yüklenirken bir hata oluştu: " + ex.Message,
                    "Grafik Veri Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadLocationData(Chart chart)
        {
            try
            {
                // Lokasyon verilerini yükle
                var locationData = MainMethods.GetLocationData();

                chart.Series[0].Points.Clear();

                foreach (var location in locationData)
                {
                    chart.Series[0].Points.AddXY(location.Key, location.Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lokasyon grafiği yüklenirken bir hata oluştu: " + ex.Message,
                    "Grafik Veri Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LayoutDashboardPanels()
        {
            // Responsive kontrol için panel konumlarını ayarla
            int padding = 10;
            int cardWidth = (pnlCards.Width - (padding * 5)) / 4;
            int chartWidth = (pnlCharts.Width - (padding * 4)) / 3;

            // Kart panellerinin boyutlarını ve konumlarını ayarla
            pnlTotalCars.Width = cardWidth;
            pnlLocations.Width = cardWidth;
            pnlCarBrands.Width = cardWidth;
            pnlAvgRentalPrice.Width = cardWidth;

            pnlTotalCars.Location = new Point(padding, padding);
            pnlLocations.Location = new Point(pnlTotalCars.Right + padding, padding);
            pnlCarBrands.Location = new Point(pnlLocations.Right + padding, padding);
            pnlAvgRentalPrice.Location = new Point(pnlCarBrands.Right + padding, padding);

            // Grafik panellerinin boyutlarını ve konumlarını ayarla
            pnlCarsByBrand.Width = chartWidth;
            pnlRentalsByYear.Width = chartWidth;
            pnlCarsByLocation.Width = chartWidth;

            pnlCarsByBrand.Location = new Point(padding, padding);
            pnlRentalsByYear.Location = new Point(pnlCarsByBrand.Right + padding, padding);
            pnlCarsByLocation.Location = new Point(pnlRentalsByYear.Right + padding, padding);
        }

        private void ApplyShadowEffect(Panel panel)
        {
            panel.Paint += (sender, e) =>
            {
                Graphics g = e.Graphics;
                Rectangle rect = new Rectangle(0, 0, panel.Width, panel.Height);

                using (GraphicsPath path = new GraphicsPath())
                {
                    int radius = 10;
                    path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                    path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
                    path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
                    path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
                    path.CloseAllFigures();

                    using (SolidBrush brush = new SolidBrush(panel.BackColor))
                    {
                        g.FillPath(brush, path);
                    }

                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.DrawPath(new Pen(Color.FromArgb(15, 0, 0, 0), 1), path);
                }
            };
        }

        private void ApplyRoundedCorners(Control control, int radius)
        {
            Rectangle rect = new Rectangle(0, 0, control.Width, control.Height);
            GraphicsPath path = new GraphicsPath();

            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseAllFigures();

            control.Region = new Region(path);
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
            // Araçlar içeriğini göster (henüz uygulanmadı)
            pnlDashboard.Visible = false;
            lblPageTitle.Text = "Araçlar";
            MessageBox.Show("Araçlar modülü henüz uygulanmadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnCustomers_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            // Müşteriler içeriğini göster (henüz uygulanmadı)
            pnlDashboard.Visible = false;
            lblPageTitle.Text = "Müşteriler";
            MessageBox.Show("Müşteriler modülü henüz uygulanmadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnRentals_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            // Kiralamalar içeriğini göster (henüz uygulanmadı)
            pnlDashboard.Visible = false;
            lblPageTitle.Text = "Kiralamalar";
            MessageBox.Show("Kiralamalar modülü henüz uygulanmadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnSales_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            // Satışlar içeriğini göster (henüz uygulanmadı)
            pnlDashboard.Visible = false;
            lblPageTitle.Text = "Satışlar";
            MessageBox.Show("Satışlar modülü henüz uygulanmadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnMaintenance_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            // Bakım ve Servis içeriğini göster (henüz uygulanmadı)
            pnlDashboard.Visible = false;
            lblPageTitle.Text = "Bakım & Servis";
            MessageBox.Show("Bakım & Servis modülü henüz uygulanmadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnReports_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            // Raporlar içeriğini göster (henüz uygulanmadı)
            pnlDashboard.Visible = false;
            lblPageTitle.Text = "Raporlar";
            MessageBox.Show("Raporlar modülü henüz uygulanmadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            // Ayarlar içeriğini göster (henüz uygulanmadı)
            pnlDashboard.Visible = false;
            lblPageTitle.Text = "Ayarlar";
            MessageBox.Show("Ayarlar modülü henüz uygulanmadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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