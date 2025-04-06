using System;
using System.Collections.Generic;
using System.Data;
using arac_kiralama_satis_desktop.Models;
using arac_kiralama_satis_desktop.Repositories;

namespace arac_kiralama_satis_desktop.Methods
{
    /// <summary>
    /// Dashboard işlemleri için yardımcı metotları içeren sınıf
    /// </summary>
    public class DashboardMethods
    {
        private static readonly DashboardRepository _repository = new DashboardRepository();

        /// <summary>
        /// Dashboard için tüm verileri toplu olarak getirir
        /// </summary>
        public static DashboardData GetDashboardData()
        {
            DashboardData dashboardData = new DashboardData();

            try
            {
                try
                {
                    object carCountResult = _repository.GetVehicleCount();
                    dashboardData.TotalCarCount = Convert.ToInt32(carCountResult);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error getting car count: {ex.Message}");
                    dashboardData.TotalCarCount = 0;
                }

                try
                {
                    object locationCountResult = _repository.GetBranchCount();
                    dashboardData.LocationCount = Convert.ToInt32(locationCountResult);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error getting branch count: {ex.Message}");
                    dashboardData.LocationCount = 0;
                }

                try
                {
                    object customerCountResult = _repository.GetCustomerCount();
                    dashboardData.CustomerCount = Convert.ToInt32(customerCountResult);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error getting customer count: {ex.Message}");
                    dashboardData.CustomerCount = 0;
                }

                try
                {
                    object revenueResult = _repository.GetTotalRevenue();
                    dashboardData.TotalRevenue = Convert.ToDecimal(revenueResult);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error getting total revenue: {ex.Message}");
                    dashboardData.TotalRevenue = 0;
                }

                try
                {
                    object brandCountResult = _repository.GetBrandCount();
                    dashboardData.BrandCount = Convert.ToInt32(brandCountResult);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error getting brand count: {ex.Message}");
                    dashboardData.BrandCount = 0;
                }

                try
                {
                    object avgPriceResult = _repository.GetAverageRentalPrice();
                    dashboardData.AverageRentalPrice = avgPriceResult != DBNull.Value ? Convert.ToDouble(avgPriceResult) : 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error getting average rental price: {ex.Message}");
                    dashboardData.AverageRentalPrice = 0;
                }

                // Diğer metrikler
                dashboardData.ActiveRentalsCount = GetActiveRentalsCount();
                dashboardData.MonthlySalesCount = GetMonthlySalesCount();
                dashboardData.PendingServiceCount = GetPendingServiceCount();
                dashboardData.TeamMembersCount = GetTeamMembersCount();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting dashboard data: {ex.Message}");

                dashboardData.TotalCarCount = 0;
                dashboardData.LocationCount = 0;
                dashboardData.CustomerCount = 0;
                dashboardData.TotalRevenue = 0;
                dashboardData.BrandCount = 0;
                dashboardData.AverageRentalPrice = 0;
                dashboardData.ActiveRentalsCount = 0;
                dashboardData.MonthlySalesCount = 0;
                dashboardData.PendingServiceCount = 0;
                dashboardData.TeamMembersCount = 0;
            }

            return dashboardData;
        }

        /// <summary>
        /// Aktif kiralama sayısını getirir
        /// </summary>
        public static int GetActiveRentalsCount()
        {
            try
            {
                object result = _repository.GetActiveRentalsCount();
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting active rentals count: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Ay içerisinde yapılan satış sayısını getirir
        /// </summary>
        public static int GetMonthlySalesCount()
        {
            try
            {
                object result = _repository.GetMonthlySalesCount();
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting monthly sales count: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Servis bekleyen araç sayısını getirir
        /// </summary>
        public static int GetPendingServiceCount()
        {
            try
            {
                object result = _repository.GetPendingServiceCount();
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting pending service count: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Aktif personel sayısını getirir
        /// </summary>
        public static int GetTeamMembersCount()
        {
            try
            {
                object result = _repository.GetTeamMembersCount();
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting team members count: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Markalara göre araç dağılımını getirir
        /// </summary>
        public static Dictionary<string, int> GetBrandDistribution()
        {
            Dictionary<string, int> brandDistribution = new Dictionary<string, int>();

            try
            {
                DataTable result = _repository.GetBrandDistribution();

                foreach (DataRow row in result.Rows)
                {
                    string brand = row["Marka"].ToString();
                    int count = Convert.ToInt32(row["Adet"]);
                    brandDistribution.Add(brand, count);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting brand distribution: {ex.Message}");
            }

            return brandDistribution;
        }

        /// <summary>
        /// Yıllara göre kiralama istatistiklerini getirir
        /// </summary>
        public static Dictionary<int, int> GetYearlyRentals()
        {
            Dictionary<int, int> yearlyRentals = new Dictionary<int, int>();

            try
            {
                DataTable result = _repository.GetYearlyRentals();

                foreach (DataRow row in result.Rows)
                {
                    int year = Convert.ToInt32(row["Year"]);
                    int count = Convert.ToInt32(row["Count"]);
                    yearlyRentals.Add(year, count);
                }

                if (yearlyRentals.Count == 0)
                {
                    int currentYear = DateTime.Now.Year;
                    for (int i = -3; i <= 0; i++)
                    {
                        yearlyRentals.Add(currentYear + i, new Random().Next(100, 500));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting yearly rentals: {ex.Message}");

                int currentYear = DateTime.Now.Year;
                for (int i = -3; i <= 0; i++)
                {
                    yearlyRentals.Add(currentYear + i, new Random().Next(100, 500));
                }
            }

            return yearlyRentals;
        }

        /// <summary>
        /// Şubelere göre araç dağılımını getirir
        /// </summary>
        public static Dictionary<string, int> GetLocationData()
        {
            Dictionary<string, int> locationData = new Dictionary<string, int>();

            try
            {
                DataTable result = _repository.GetLocationData();

                foreach (DataRow row in result.Rows)
                {
                    string location = row["SubeAdi"].ToString();
                    int count = Convert.ToInt32(row["AracSayisi"]);
                    locationData.Add(location, count);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting location data: {ex.Message}");
            }

            return locationData;
        }
    }
}