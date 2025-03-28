using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using arac_kiralama_satis_desktop.Methods;
using FontAwesome.Sharp;

namespace arac_kiralama_satis_desktop.Interfaces
{
    public partial class MainPage
    {
        private void LoadDashboardData()
        {
            try
            {
                // Get data from database
                var dashboardData = MainMethods.GetDashboardData();

                // Update dashboard cards with data
                lblCarCount.Text = dashboardData.TotalCarCount.ToString();
                lblLocationCount.Text = dashboardData.LocationCount.ToString();
                lblCustomerCount.Text = dashboardData.CustomerCount.ToString();
                lblTotalRevenue.Text = $"₺ {dashboardData.TotalRevenue:N2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Dashboard verileri yüklenirken bir hata oluştu: {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Dictionary<string, int> GetBrandDistribution()
        {
            try
            {
                return MainMethods.GetBrandDistribution();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Marka dağılımı verileri alınırken bir hata oluştu: {ex.Message}",
                    "Veri Alma Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new Dictionary<string, int>();
            }
        }

        private Dictionary<int, int> GetYearlyRentals()
        {
            try
            {
                return MainMethods.GetYearlyRentals();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Yıllık kiralama verileri alınırken bir hata oluştu: {ex.Message}",
                    "Veri Alma Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new Dictionary<int, int>();
            }
        }

        private Dictionary<string, int> GetLocationData()
        {
            try
            {
                return MainMethods.GetLocationData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lokasyon verileri alınırken bir hata oluştu: {ex.Message}",
                    "Veri Alma Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new Dictionary<string, int>();
            }
        }

        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowPanel(pnlDashboard);
            LoadDashboardData();
            lblPageTitle.Text = "Dashboard";
        }
    }
}