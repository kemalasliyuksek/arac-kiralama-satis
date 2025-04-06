using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using arac_kiralama_satis_desktop.Methods;
using arac_kiralama_satis_desktop.Utils;

namespace arac_kiralama_satis_desktop.Controls
{
    public partial class DashboardControl : UserControl
    {
        public DashboardControl()
        {
            InitializeComponent();

            // Form resize event handler
            this.Resize += DashboardControl_Resize;
        }

        public void LoadData()
        {
            InitializeDashboard();
            LoadDashboardData();
        }

        private void InitializeDashboard()
        {
            // Apply shadow effects to all metric panels
            UIUtils.ApplyShadowEffect(metricPanel1);
            UIUtils.ApplyShadowEffect(metricPanel2);
            UIUtils.ApplyShadowEffect(metricPanel3);
            UIUtils.ApplyShadowEffect(metricPanel4);
            UIUtils.ApplyShadowEffect(metricPanel5);
            UIUtils.ApplyShadowEffect(metricPanel6);
            UIUtils.ApplyShadowEffect(metricPanel7);
            UIUtils.ApplyShadowEffect(metricPanel8);

            // Ayarla TableLayoutPanel'in hücre boşluklarını
            tableLayoutMetrics.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
            tableLayoutMetrics.Padding = new Padding(15);
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
                metricValue1.Text = dashboardData.TotalCarCount.ToString("N0");
                metricValue2.Text = dashboardData.LocationCount.ToString("N0");
                metricValue3.Text = dashboardData.CustomerCount.ToString("N0");
                metricValue4.Text = dashboardData.TotalRevenue.ToString("N0");

                // Get additional metrics from database
                int activeRentals = MainMethods.GetActiveRentalsCount();
                int monthlySales = MainMethods.GetMonthlySalesCount();
                int pendingService = MainMethods.GetPendingServiceCount();
                int teamMembers = MainMethods.GetTeamMembersCount();

                // Update additional metrics
                metricValue5.Text = activeRentals.ToString("N0");
                metricValue6.Text = monthlySales.ToString("N0");
                metricValue7.Text = pendingService.ToString("N0");
                metricValue8.Text = teamMembers.ToString("N0");
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

        private void DashboardControl_Resize(object sender, EventArgs e)
        {
            // TableLayoutPanel kullanıldığı için otomatik olarak paneller yeniden boyutlandırılacak
            // ve ortalanacak, ek bir işlem gerektirmiyor.
        }
    }
}