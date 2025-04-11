using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using arac_kiralama_satis_desktop.Methods;
using arac_kiralama_satis_desktop.Utils;
using arac_kiralama_satis_desktop.Models;

namespace arac_kiralama_satis_desktop.Controls
{
    public partial class DashboardControl : UserControl
    {
        public DashboardControl()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
            try
            {
                ErrorManager.Instance.LogInfo("Dashboard verileri yükleniyor", "DashboardControl.LoadData");
                InitializeDashboard();
                LoadDashboardData();
                ErrorManager.Instance.LogInfo("Dashboard verileri başarıyla yüklendi", "DashboardControl.LoadData");
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.HandleException(
                    ex,
                    "Dashboard verilerini yüklerken beklenmeyen bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.UI,
                    true); // Kullanıcıya göster
            }
        }

        private void InitializeDashboard()
        {
            try
            {
                ErrorManager.Instance.LogInfo("Dashboard görsel bileşenleri hazırlanıyor", "DashboardControl.InitializeDashboard");

                UIUtils.ApplyShadowEffect(metricPanel1);
                UIUtils.ApplyShadowEffect(metricPanel2);
                UIUtils.ApplyShadowEffect(metricPanel3);
                UIUtils.ApplyShadowEffect(metricPanel4);
                UIUtils.ApplyShadowEffect(metricPanel5);
                UIUtils.ApplyShadowEffect(metricPanel6);
                UIUtils.ApplyShadowEffect(metricPanel7);
                UIUtils.ApplyShadowEffect(metricPanel8);

                tableLayoutMetrics.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
                tableLayoutMetrics.Padding = new Padding(15);

                ErrorManager.Instance.LogInfo("Dashboard görsel bileşenleri başarıyla hazırlandı", "DashboardControl.InitializeDashboard");
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.HandleException(
                    ex,
                    "Dashboard görsel bileşenlerini hazırlarken bir hata oluştu",
                    ErrorSeverity.Warning, // Kritik olmayan UI hatası
                    ErrorSource.UI,
                    false); // Kritik olmadığı için kullanıcıya gösterme

                // Hata oluşsa bile devam et, ölümcül bir hata değil
            }
        }

        private void LoadDashboardData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                ErrorManager.Instance.LogInfo("Dashboard metric verileri alınıyor", "DashboardControl.LoadDashboardData");
                DashboardData dashboardData = DashboardMethods.GetDashboardData();

                // Toplam araç sayısı
                metricValue1.Text = dashboardData.TotalCarCount.ToString("N0");
                ErrorManager.Instance.LogInfo($"Toplam araç sayısı başarıyla yüklendi: {dashboardData.TotalCarCount}",
                    "DashboardControl.LoadDashboardData");

                // Şube sayısı
                metricValue2.Text = dashboardData.LocationCount.ToString("N0");
                ErrorManager.Instance.LogInfo($"Şube sayısı başarıyla yüklendi: {dashboardData.LocationCount}",
                    "DashboardControl.LoadDashboardData");

                // Müşteri sayısı
                metricValue3.Text = dashboardData.CustomerCount.ToString("N0");
                ErrorManager.Instance.LogInfo($"Müşteri sayısı başarıyla yüklendi: {dashboardData.CustomerCount}",
                    "DashboardControl.LoadDashboardData");

                // Toplam gelir
                metricValue4.Text = dashboardData.TotalRevenue.ToString("N0");
                ErrorManager.Instance.LogInfo($"Toplam gelir başarıyla yüklendi: {dashboardData.TotalRevenue}",
                    "DashboardControl.LoadDashboardData");

                // Aktif kiralama sayısı
                metricValue5.Text = dashboardData.ActiveRentalsCount.ToString("N0");
                ErrorManager.Instance.LogInfo($"Aktif kiralama sayısı başarıyla yüklendi: {dashboardData.ActiveRentalsCount}",
                    "DashboardControl.LoadDashboardData");

                // Aylık satış sayısı
                metricValue6.Text = dashboardData.MonthlySalesCount.ToString("N0");
                ErrorManager.Instance.LogInfo($"Aylık satış sayısı başarıyla yüklendi: {dashboardData.MonthlySalesCount}",
                    "DashboardControl.LoadDashboardData");

                // Servis bekleyen araç sayısı
                metricValue7.Text = dashboardData.PendingServiceCount.ToString("N0");
                ErrorManager.Instance.LogInfo($"Servis bekleyen araç sayısı başarıyla yüklendi: {dashboardData.PendingServiceCount}",
                    "DashboardControl.LoadDashboardData");

                // Ekip üyesi sayısı
                metricValue8.Text = dashboardData.TeamMembersCount.ToString("N0");
                ErrorManager.Instance.LogInfo($"Ekip üyesi sayısı başarıyla yüklendi: {dashboardData.TeamMembersCount}",
                    "DashboardControl.LoadDashboardData");

                ErrorManager.Instance.LogInfo("Tüm dashboard verileri başarıyla yüklendi",
                    "DashboardControl.LoadDashboardData");
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Dashboard verileri yüklenirken hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Business,
                    true); // Kullanıcıya göster
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
    }
}