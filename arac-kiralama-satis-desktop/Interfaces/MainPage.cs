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

        private void LoadDashboardData()
        {
            // Dashboard verilerini temizle - sadece başlıklar görünsün
            lblCarCount.Text = "0";
            lblLocationCount.Text = "0";
            lblBrandCount.Text = "0";
            lblAvgPrice.Text = "₺ 0,00";
        }

        private void InitializeCharts()
        {
            // Grafik panellerini temizle - grafik gösterilmeyecek
            chartCarsByBrand.Controls.Clear();
            chartRentalsByYear.Controls.Clear();
            chartCarsByLocation.Controls.Clear();
        }

        private Chart CreatePieChart()
        {
            Chart chart = new Chart();
            chart.Size = new Size(chartCarsByBrand.Width, chartCarsByBrand.Height);

            // ChartArea oluştur
            ChartArea chartArea = new ChartArea("MainArea");
            chartArea.BackColor = Color.White;
            chartArea.Area3DStyle.Enable3D = false;
            chart.ChartAreas.Add(chartArea);

            // Series oluştur
            Series series = new Series("BrandSeries");
            series.ChartArea = "MainArea";
            series.ChartType = SeriesChartType.Pie;
            series.IsValueShownAsLabel = true;
            series.LabelFormat = "{0}";
            series.XValueType = ChartValueType.String;
            series.YValueType = ChartValueType.Int32;
            series.Font = new Font("Segoe UI", 9);
            series.BorderWidth = 1;
            series.BorderColor = Color.White;

            chart.Series.Add(series);

            // Ekstra özellikler
            Legend legend = new Legend("MainLegend");
            legend.BackColor = Color.White;
            legend.Font = new Font("Segoe UI", 9);
            chart.Legends.Add(legend);
            series.Legend = "MainLegend";

            chart.BackColor = Color.White;
            chart.BorderlineWidth = 0;
            chart.BorderSkin.SkinStyle = BorderSkinStyle.None;
            chart.AntiAliasing = AntiAliasingStyles.All;
            // Remove the TextAntiAliasingQuality that's causing the error
            // chart.TextAntiAliasingQuality = AntiAliasingStyles.High;

            return chart;
        }

        private Chart CreateLineChart()
        {
            Chart chart = new Chart();

            // Sabit boyutlu grafik
            chart.Size = new Size(chartRentalsByYear.Width - 40, chartRentalsByYear.Height - 80);
            chart.Dock = DockStyle.Fill;

            // ChartArea oluştur
            ChartArea chartArea = new ChartArea("YearlyArea");
            chartArea.BackColor = Color.White;
            chartArea.AxisX.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisX.LabelStyle.Font = new Font("Segoe UI", 9);
            chartArea.AxisY.LabelStyle.Font = new Font("Segoe UI", 9);
            chartArea.AxisX.Title = "Yıl";
            chartArea.AxisY.Title = "Kiralama Sayısı";
            chartArea.AxisX.TitleFont = new Font("Segoe UI", 10, FontStyle.Bold);
            chartArea.AxisY.TitleFont = new Font("Segoe UI", 10, FontStyle.Bold);
            chartArea.AxisX.Interval = 1;
            chartArea.AxisY.LabelStyle.Format = "{0:0}";  // Tam sayı formatı

            // Izgara çizgilerini düzenle
            chartArea.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            chartArea.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;

            // Y ekseni için minimum değeri 0 olarak ayarla
            chartArea.AxisY.Minimum = 0;

            chart.ChartAreas.Add(chartArea);

            // Series oluştur
            Series series = new Series("RentalSeries");
            series.ChartArea = "YearlyArea";
            series.ChartType = SeriesChartType.Line;
            series.BorderWidth = 4;
            series.MarkerStyle = MarkerStyle.Circle;
            series.MarkerSize = 10;
            series.MarkerColor = Color.FromArgb(26, 115, 232);
            series.Color = Color.FromArgb(26, 115, 232);
            series.XValueType = ChartValueType.Int32;
            series.YValueType = ChartValueType.Int32;
            series.IsValueShownAsLabel = true;
            series.LabelFormat = "{0:0}";  // Tam sayı formatı
            series.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            chart.Series.Add(series);
            chart.BackColor = Color.White;
            chart.AntiAliasing = AntiAliasingStyles.All;

            // Chart başlığı ekle
            Title title = new Title("Yıllara Göre Kiralamalar", Docking.Top, new Font("Segoe UI", 12, FontStyle.Bold), Color.FromArgb(49, 76, 143));
            chart.Titles.Add(title);

            return chart;
        }

        private Chart CreateBarChart()
        {
            Chart chart = new Chart();

            // Sabit boyutlu grafik
            chart.Size = new Size(chartCarsByLocation.Width - 40, chartCarsByLocation.Height - 80);
            chart.Dock = DockStyle.Fill;

            // ChartArea oluştur
            ChartArea chartArea = new ChartArea("LocationArea");
            chartArea.BackColor = Color.White;
            chartArea.AxisX.MajorGrid.LineColor = Color.Transparent;
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisX.LabelStyle.Font = new Font("Segoe UI", 9);
            chartArea.AxisY.LabelStyle.Font = new Font("Segoe UI", 9);
            chartArea.AxisX.Title = "Lokasyon";
            chartArea.AxisY.Title = "Araç Sayısı";
            chartArea.AxisX.TitleFont = new Font("Segoe UI", 10, FontStyle.Bold);
            chartArea.AxisY.TitleFont = new Font("Segoe UI", 10, FontStyle.Bold);
            chartArea.AxisX.Interval = 1;
            // X ekseni etiketlerini açılı göster
            chartArea.AxisX.LabelStyle.Angle = -30;
            chartArea.AxisX.LabelStyle.IsStaggered = true; // Etiketleri daha iyi göstermek için çakışıyorsa farklı seviyelerde göster

            // Izgara çizgilerini düzenle
            chartArea.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;

            // Y ekseni için minimum değeri 0 olarak ayarla
            chartArea.AxisY.Minimum = 0;

            chart.ChartAreas.Add(chartArea);

            // Series oluştur
            Series series = new Series("LocationSeries");
            series.ChartArea = "LocationArea";
            series.ChartType = SeriesChartType.Column;

            // Çoklu renk paleti
            Color[] colorPalette = new Color[]
            {
                Color.FromArgb(156, 136, 255),
                Color.FromArgb(79, 119, 198),
                Color.FromArgb(47, 192, 120)
            };

            series["DrawingStyle"] = "Cylinder"; // Silindir stili
            series.XValueType = ChartValueType.String;
            series.YValueType = ChartValueType.Int32;
            series.IsValueShownAsLabel = true;
            series.LabelFormat = "{0:0}";  // Tam sayı formatı
            series.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            series.BorderWidth = 0;

            chart.Series.Add(series);
            chart.BackColor = Color.White;
            chart.AntiAliasing = AntiAliasingStyles.All;

            // Renk paleti uygula - Çubuklar için farklı renkler
            chart.Palette = ChartColorPalette.BrightPastel;

            // Chart başlığı ekle
            Title title = new Title("Şubelere Göre Araç Sayıları", Docking.Top, new Font("Segoe UI", 12, FontStyle.Bold), Color.FromArgb(49, 76, 143));
            chart.Titles.Add(title);

            return chart;
        }

        private void LoadBrandDistributionData(Chart chart)
        {
            try
            {
                // Marka dağılım verilerini yükle
                Dictionary<string, int> brandDistribution;

                try
                {
                    brandDistribution = MainMethods.GetBrandDistribution();
                }
                catch (Exception)
                {
                    // Eğer gerçek veri alınamazsa, demo verisi kullan
                    brandDistribution = new Dictionary<string, int> {
                        { "Mercedes", 3 },
                        { "BMW", 2 },
                        { "Audi", 1 },
                        { "Ford", 1 },
                        { "Alfa Romeo", 1 },
                        { "Range Rover", 1 },
                        { "Jeep", 1 }
                    };
                }

                // Grafiği temizle
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

                // Verileri grafiğe ekle
                foreach (var brand in brandDistribution)
                {
                    DataPoint point = new DataPoint();
                    point.SetValueXY(brand.Key, brand.Value);
                    point.LegendText = brand.Key;
                    point.Label = brand.Value.ToString();

                    if (brandColors.ContainsKey(brand.Key))
                    {
                        point.Color = brandColors[brand.Key];
                    }

                    chart.Series[0].Points.Add(point);
                }

                // Grafiği güncelle
                chart.Invalidate();
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
                Dictionary<int, int> yearlyRentals;

                try
                {
                    yearlyRentals = MainMethods.GetYearlyRentals();
                }
                catch (Exception)
                {
                    // Eğer gerçek veri alınamazsa, demo verisi kullan
                    yearlyRentals = new Dictionary<int, int> {
                        { 2019, 150 },
                        { 2020, 70 },
                        { 2021, 200 },
                        { 2022, 480 },
                        { 2023, 330 },
                        { 2024, 220 },
                        { 2025, 450 }
                    };
                }

                // Grafiği temizle
                chart.Series[0].Points.Clear();

                // Verileri grafiğe ekle
                foreach (var rental in yearlyRentals)
                {
                    DataPoint point = new DataPoint();
                    point.SetValueXY(rental.Key, rental.Value);
                    point.Label = rental.Value.ToString();
                    chart.Series[0].Points.Add(point);
                }

                // Grafiği güncelle
                chart.Invalidate();
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
                Dictionary<string, int> locationData;

                try
                {
                    locationData = MainMethods.GetLocationData();
                }
                catch (Exception)
                {
                    // Eğer gerçek veri alınamazsa, demo verisi kullan
                    locationData = new Dictionary<string, int> {
                        { "Bursa Merkez Şube", 4 },
                        { "İstanbul Beşiktaş Şube", 4 },
                        { "İzmir Alsancak Şube", 2 }
                    };
                }

                // Grafiği temizle
                chart.Series[0].Points.Clear();

                // Verileri grafiğe ekle
                foreach (var location in locationData)
                {
                    DataPoint point = new DataPoint();
                    point.SetValueXY(location.Key, location.Value);
                    point.Label = location.Value.ToString();
                    chart.Series[0].Points.Add(point);
                }

                // Grafiği güncelle
                chart.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lokasyon grafiği yüklenirken bir hata oluştu: " + ex.Message,
                    "Grafik Veri Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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