using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using arac_kiralama_satis_desktop.Methods;
using arac_kiralama_satis_desktop.Utils;
using System.Drawing.Drawing2D;
using Timer = System.Windows.Forms.Timer;

namespace arac_kiralama_satis_desktop.Controls
{
    public partial class DashboardControl : UserControl
    {
        // Tasarım için gereken sözlükler
        private Dictionary<string, int> brandDistribution;
        private Dictionary<string, int> locationData;
        private Dictionary<int, int> yearlyRentals;
        private Timer refreshTimer;

        public DashboardControl()
        {
            InitializeComponent();
            ApplyStyling();

            // Otomatik veri yenileme
            refreshTimer = new Timer();
            refreshTimer.Interval = 300000; // 5 dakika
            refreshTimer.Tick += (s, e) => LoadData();

            // Kontrol yüklendiğinde verileri yükle
            this.Load += (s, e) => LoadData();
        }

        private void ApplyStyling()
        {
            // Panellere gölge ve yuvarlak köşe eklemek
            ApplyShadowAndRoundedCorners(pnlTotalVehicles, 10);
            ApplyShadowAndRoundedCorners(pnlActiveRentals, 10);
            ApplyShadowAndRoundedCorners(pnlMonthlySales, 10);
            ApplyShadowAndRoundedCorners(pnlPendingServices, 10);
            ApplyShadowAndRoundedCorners(pnlBrandDistribution, 10);
            ApplyShadowAndRoundedCorners(pnlLocationDistribution, 10);
            ApplyShadowAndRoundedCorners(pnlRevenue, 10);
            ApplyShadowAndRoundedCorners(pnlRecentActivity, 10);
            ApplyShadowAndRoundedCorners(pnlWelcome, 10);

            // Liste görünümünü özelleştirme
            lvwRecentActivity.FullRowSelect = true;
            lvwRecentActivity.View = View.Details;
            lvwRecentActivity.Columns.Add("İşlem", 150);
            lvwRecentActivity.Columns.Add("Tarih", 150);
            lvwRecentActivity.Columns.Add("Detay", 200);
        }

        private void ApplyShadowAndRoundedCorners(Panel panel, int radius)
        {
            // Köşeleri yuvarlatma ve gölge efekti
            panel.BackColor = Color.White;
            panel.Paint += (s, e) =>
            {
                // Yuvarlatılmış köşeler
                Rectangle rect = new Rectangle(0, 0, panel.Width, panel.Height);
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                    path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
                    path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
                    path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
                    path.CloseAllFigures();

                    panel.Region = new Region(path);

                    // Gölge efekti
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                    using (SolidBrush brush = new SolidBrush(panel.BackColor))
                    {
                        e.Graphics.FillPath(brush, path);
                    }

                    using (Pen pen = new Pen(Color.FromArgb(20, 0, 0, 0), 1))
                    {
                        e.Graphics.DrawPath(pen, path);
                    }
                }
            };
        }

        public void LoadData()
        {
            try
            {
                // Kullanıcı bilgilerini göster
                UpdateUserInfo();

                // Ana metrikleri güncelle
                UpdateMainMetrics();

                // Grafik verilerini güncelle
                LoadChartData();

                // Grafikleri çiz
                DrawBrandDistribution();
                DrawLocationDistribution();

                // Son etkinlikleri yükle
                LoadRecentActivities();

                // Zamanlayıcıyı başlat
                if (!refreshTimer.Enabled)
                {
                    refreshTimer.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Dashboard verileri yüklenirken bir hata oluştu: {ex.Message}",
                                "Veri Yükleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateUserInfo()
        {
            // Kullanıcı adını gösterme
            lblWelcomeUser.Text = $"Hoş Geldiniz, {CurrentSession.FullName}";

            // Mevcut tarih ve saati gösterme
            lblDateTime.Text = DateTime.Now.ToString("dd MMMM yyyy, HH:mm");
        }

        private void UpdateMainMetrics()
        {
            // Ana istatistikleri çekme
            var dashboardData = MainMethods.GetDashboardData();
            int activeRentals = MainMethods.GetActiveRentalsCount();
            int monthlySales = MainMethods.GetMonthlySalesCount();
            int pendingServices = MainMethods.GetPendingServiceCount();

            // UI'a yansıtma
            lblTotalVehiclesValue.Text = dashboardData.TotalCarCount.ToString("N0");
            lblActiveRentalsValue.Text = activeRentals.ToString("N0");
            lblMonthlySalesValue.Text = monthlySales.ToString("N0");
            lblPendingServicesValue.Text = pendingServices.ToString("N0");

            // Toplam geliri formatla ve göster (₺ işareti ile)
            lblTotalRevenueValue.Text = $"₺{dashboardData.TotalRevenue:N2}";
        }

        private void LoadChartData()
        {
            // Grafik verilerini çek
            brandDistribution = MainMethods.GetBrandDistribution();
            locationData = MainMethods.GetLocationData();
            yearlyRentals = MainMethods.GetYearlyRentals();
        }

        private void DrawBrandDistribution()
        {
            // Grafik alanını temizle
            pnlBrandChartContent.Controls.Clear();

            if (brandDistribution == null || brandDistribution.Count == 0)
            {
                lblNoBrandData.Visible = true;
                return;
            }

            lblNoBrandData.Visible = false;

            // Markaları ve değerleri al
            List<KeyValuePair<string, int>> brandItems = new List<KeyValuePair<string, int>>(brandDistribution);

            // En fazla 5 markayı göster, geri kalanları "Diğer" kategorisinde topla
            int maxBrandsToShow = Math.Min(5, brandItems.Count);

            // Renkleri tanımla
            Color[] brandColors = new Color[]
            {
                Color.FromArgb(49, 76, 143),    // Primary
                Color.FromArgb(40, 167, 69),    // Success
                Color.FromArgb(0, 123, 255),    // Info
                Color.FromArgb(255, 193, 7),    // Warning
                Color.FromArgb(220, 53, 69),    // Danger
                Color.FromArgb(108, 117, 125)   // Secondary (Diğerleri için)
            };

            // Toplam değeri hesapla
            int totalCount = 0;
            foreach (var item in brandItems)
            {
                totalCount += item.Value;
            }

            // Sıralı olarak göster
            brandItems.Sort((x, y) => y.Value.CompareTo(x.Value));

            // Her marka için bir etiket ve bar oluştur
            int yPos = 10;
            int maxBarWidth = pnlBrandChartContent.Width - 170; // Etiket ve değer için alan bırak

            for (int i = 0; i < maxBrandsToShow; i++)
            {
                // Marka adı etiketi
                Label lblBrand = new Label
                {
                    Text = brandItems[i].Key,
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Width = 80,
                    Height = 20,
                    Location = new Point(0, yPos),
                    Font = new Font("Segoe UI", 9F)
                };
                pnlBrandChartContent.Controls.Add(lblBrand);

                // Bar gösterimi
                int barWidth = (int)((double)brandItems[i].Value / totalCount * maxBarWidth);
                Panel pnlBar = new Panel
                {
                    BackColor = brandColors[i % brandColors.Length],
                    Location = new Point(90, yPos + 5),
                    Size = new Size(barWidth, 10)
                };
                pnlBrandChartContent.Controls.Add(pnlBar);

                // Değer etiketi
                Label lblValue = new Label
                {
                    Text = $"{brandItems[i].Value} ({(double)brandItems[i].Value / totalCount:P0})",
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Width = 80,
                    Height = 20,
                    Location = new Point(95 + barWidth, yPos),
                    Font = new Font("Segoe UI", 8F),
                    ForeColor = Color.FromArgb(108, 117, 125)
                };
                pnlBrandChartContent.Controls.Add(lblValue);

                yPos += 30;
            }
        }

        private void DrawLocationDistribution()
        {
            // Grafik alanını temizle
            pnlLocationChartContent.Controls.Clear();

            if (locationData == null || locationData.Count == 0)
            {
                lblNoLocationData.Visible = true;
                return;
            }

            lblNoLocationData.Visible = false;

            // Şubeleri ve değerleri al
            List<KeyValuePair<string, int>> locationItems = new List<KeyValuePair<string, int>>(locationData);

            // En fazla 5 şubeyi göster, geri kalanları "Diğer" kategorisinde topla
            int maxLocationsToShow = Math.Min(5, locationItems.Count);

            // Renkleri tanımla
            Color[] locationColors = new Color[]
            {
                Color.FromArgb(49, 76, 143),    // Primary
                Color.FromArgb(40, 167, 69),    // Success
                Color.FromArgb(0, 123, 255),    // Info
                Color.FromArgb(255, 193, 7),    // Warning
                Color.FromArgb(220, 53, 69),    // Danger
                Color.FromArgb(108, 117, 125)   // Secondary (Diğerleri için)
            };

            // Toplam değeri hesapla
            int totalCount = 0;
            foreach (var item in locationItems)
            {
                totalCount += item.Value;
            }

            // Sıralı olarak göster
            locationItems.Sort((x, y) => y.Value.CompareTo(x.Value));

            // Her şube için bir etiket ve bar oluştur
            int yPos = 10;
            int maxBarWidth = pnlLocationChartContent.Width - 170; // Etiket ve değer için alan bırak

            for (int i = 0; i < maxLocationsToShow; i++)
            {
                // Şube adı etiketi
                Label lblLocation = new Label
                {
                    Text = locationItems[i].Key,
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Width = 80,
                    Height = 20,
                    Location = new Point(0, yPos),
                    Font = new Font("Segoe UI", 9F)
                };
                pnlLocationChartContent.Controls.Add(lblLocation);

                // Bar gösterimi
                int barWidth = totalCount > 0
                    ? (int)((double)locationItems[i].Value / totalCount * maxBarWidth)
                    : 0;

                Panel pnlBar = new Panel
                {
                    BackColor = locationColors[i % locationColors.Length],
                    Location = new Point(90, yPos + 5),
                    Size = new Size(barWidth > 0 ? barWidth : 1, 10)
                };
                pnlLocationChartContent.Controls.Add(pnlBar);

                // Değer etiketi
                string percentage = totalCount > 0
                    ? $"({(double)locationItems[i].Value / totalCount:P0})"
                    : "(0%)";

                Label lblValue = new Label
                {
                    Text = $"{locationItems[i].Value} {percentage}",
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Width = 80,
                    Height = 20,
                    Location = new Point(95 + barWidth, yPos),
                    Font = new Font("Segoe UI", 8F),
                    ForeColor = Color.FromArgb(108, 117, 125)
                };
                pnlLocationChartContent.Controls.Add(lblValue);

                yPos += 30;
            }
        }

        private void LoadRecentActivities()
        {
            // Son etkinlikleri temizle
            lvwRecentActivity.Items.Clear();

            // Son kiralama ve satışları almak için veritabanı sorgularını ekle
            try
            {
                // En son 5 kiralamayı al
                List<ListViewItem> items = new List<ListViewItem>();

                // Aktif kiralama öğelerini ekle (örnek)
                ListViewItem item1 = new ListViewItem("Kiralama");
                item1.SubItems.Add(DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"));
                item1.SubItems.Add("Plaka: 34ABC123 Müşteri: Ahmet Y.");
                items.Add(item1);

                ListViewItem item2 = new ListViewItem("Kiralama");
                item2.SubItems.Add(DateTime.Now.AddDays(-2).ToString("dd.MM.yyyy"));
                item2.SubItems.Add("Plaka: 06DEF456 Müşteri: Mehmet S.");
                items.Add(item2);

                // Son satış öğelerini ekle (örnek)
                ListViewItem item3 = new ListViewItem("Satış");
                item3.SubItems.Add(DateTime.Now.AddDays(-5).ToString("dd.MM.yyyy"));
                item3.SubItems.Add("Plaka: 35GHI789 Müşteri: Ayşe T.");
                items.Add(item3);

                // Son bakım öğelerini ekle (örnek)
                ListViewItem item4 = new ListViewItem("Bakım");
                item4.SubItems.Add(DateTime.Now.AddDays(-3).ToString("dd.MM.yyyy"));
                item4.SubItems.Add("Plaka: 34JKL012 Durum: Tamamlandı");
                items.Add(item4);

                ListViewItem item5 = new ListViewItem("Kiralama Dönüşü");
                item5.SubItems.Add(DateTime.Now.AddHours(-12).ToString("dd.MM.yyyy"));
                item5.SubItems.Add("Plaka: 34MNO345 Müşteri: Fatma K.");
                items.Add(item5);

                // Tüm öğeleri listeye ekle
                foreach (var item in items)
                {
                    lvwRecentActivity.Items.Add(item);
                }

                // Varsayılan olarak sütun genişliklerini ayarla
                lvwRecentActivity.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Son etkinlikler yüklenirken hata: {ex.Message}");
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            // Yeniden boyutlandırmada grafikleri tekrar çiz
            if (brandDistribution != null && brandDistribution.Count > 0)
            {
                DrawBrandDistribution();
            }

            if (locationData != null && locationData.Count > 0)
            {
                DrawLocationDistribution();
            }
        }
    }
}