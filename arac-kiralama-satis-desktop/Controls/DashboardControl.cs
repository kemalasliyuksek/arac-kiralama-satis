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
            InitializeDashboard();
            LoadDashboardData();
        }

        private void InitializeDashboard()
        {
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
        }

        private void LoadDashboardData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                DashboardData dashboardData = DashboardMethods.GetDashboardData();

                metricValue1.Text = dashboardData.TotalCarCount.ToString("N0");
                metricValue2.Text = dashboardData.LocationCount.ToString("N0");
                metricValue3.Text = dashboardData.CustomerCount.ToString("N0");
                metricValue4.Text = dashboardData.TotalRevenue.ToString("N0");

                metricValue5.Text = dashboardData.ActiveRentalsCount.ToString("N0");
                metricValue6.Text = dashboardData.MonthlySalesCount.ToString("N0");
                metricValue7.Text = dashboardData.PendingServiceCount.ToString("N0");
                metricValue8.Text = dashboardData.TeamMembersCount.ToString("N0");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Dashboard verisi yüklenirken hata oluştu: {ex.Message}",
                    "Veri Yükleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
    }
}