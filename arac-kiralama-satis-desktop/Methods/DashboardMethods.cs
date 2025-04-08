using System;
using System.Collections.Generic;
using System.Data;
using arac_kiralama_satis_desktop.Models;
using arac_kiralama_satis_desktop.Repositories;
using arac_kiralama_satis_desktop.Utils; // ErrorManager için ekledim

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
            ErrorManager.Instance.LogInfo("Dashboard verileri alınıyor", "DashboardMethods.GetDashboardData");

            DashboardData dashboardData = new DashboardData();

            try
            {
                try
                {
                    ErrorManager.Instance.LogInfo("Toplam araç sayısı alınıyor", "DashboardMethods.GetDashboardData");
                    object carCountResult = _repository.GetVehicleCount();
                    dashboardData.TotalCarCount = Convert.ToInt32(carCountResult);
                }
                catch (Exception ex)
                {
                    // Hata loglanıyor ancak işlem devam ediyor
                    ErrorManager.Instance.HandleException(
                        ex,
                        "Toplam araç sayısı alınırken hata oluştu",
                        ErrorSeverity.Warning,
                        ErrorSource.Database,
                        false);

                    dashboardData.TotalCarCount = 0;
                }

                try
                {
                    ErrorManager.Instance.LogInfo("Şube sayısı alınıyor", "DashboardMethods.GetDashboardData");
                    object locationCountResult = _repository.GetBranchCount();
                    dashboardData.LocationCount = Convert.ToInt32(locationCountResult);
                }
                catch (Exception ex)
                {
                    ErrorManager.Instance.HandleException(
                        ex,
                        "Şube sayısı alınırken hata oluştu",
                        ErrorSeverity.Warning,
                        ErrorSource.Database,
                        false);

                    dashboardData.LocationCount = 0;
                }

                try
                {
                    ErrorManager.Instance.LogInfo("Müşteri sayısı alınıyor", "DashboardMethods.GetDashboardData");
                    object customerCountResult = _repository.GetCustomerCount();
                    dashboardData.CustomerCount = Convert.ToInt32(customerCountResult);
                }
                catch (Exception ex)
                {
                    ErrorManager.Instance.HandleException(
                        ex,
                        "Müşteri sayısı alınırken hata oluştu",
                        ErrorSeverity.Warning,
                        ErrorSource.Database,
                        false);

                    dashboardData.CustomerCount = 0;
                }

                try
                {
                    ErrorManager.Instance.LogInfo("Toplam gelir alınıyor", "DashboardMethods.GetDashboardData");
                    object revenueResult = _repository.GetTotalRevenue();
                    dashboardData.TotalRevenue = Convert.ToDecimal(revenueResult);
                }
                catch (Exception ex)
                {
                    ErrorManager.Instance.HandleException(
                        ex,
                        "Toplam gelir alınırken hata oluştu",
                        ErrorSeverity.Warning,
                        ErrorSource.Database,
                        false);

                    dashboardData.TotalRevenue = 0;
                }

                try
                {
                    ErrorManager.Instance.LogInfo("Marka sayısı alınıyor", "DashboardMethods.GetDashboardData");
                    object brandCountResult = _repository.GetBrandCount();
                    dashboardData.BrandCount = Convert.ToInt32(brandCountResult);
                }
                catch (Exception ex)
                {
                    ErrorManager.Instance.HandleException(
                        ex,
                        "Marka sayısı alınırken hata oluştu",
                        ErrorSeverity.Warning,
                        ErrorSource.Database,
                        false);

                    dashboardData.BrandCount = 0;
                }

                try
                {
                    ErrorManager.Instance.LogInfo("Ortalama kiralama fiyatı alınıyor", "DashboardMethods.GetDashboardData");
                    object avgPriceResult = _repository.GetAverageRentalPrice();
                    dashboardData.AverageRentalPrice = avgPriceResult != DBNull.Value ? Convert.ToDouble(avgPriceResult) : 0;
                }
                catch (Exception ex)
                {
                    ErrorManager.Instance.HandleException(
                        ex,
                        "Ortalama kiralama fiyatı alınırken hata oluştu",
                        ErrorSeverity.Warning,
                        ErrorSource.Database,
                        false);

                    dashboardData.AverageRentalPrice = 0;
                }

                // Diğer metrikler
                ErrorManager.Instance.LogInfo("Ek dashboard metrikleri alınıyor", "DashboardMethods.GetDashboardData");
                dashboardData.ActiveRentalsCount = GetActiveRentalsCount();
                dashboardData.MonthlySalesCount = GetMonthlySalesCount();
                dashboardData.PendingServiceCount = GetPendingServiceCount();
                dashboardData.TeamMembersCount = GetTeamMembersCount();
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.HandleException(
                    ex,
                    "Dashboard verileri alınırken genel bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Business,
                    false);

                // Tüm verileri varsayılan değerlere sıfırla
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
                ErrorManager.Instance.LogInfo("Aktif kiralama sayısı alınıyor", "DashboardMethods.GetActiveRentalsCount");
                object result = _repository.GetActiveRentalsCount();
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.HandleException(
                    ex,
                    "Aktif kiralama sayısı alınırken hata oluştu",
                    ErrorSeverity.Warning,
                    ErrorSource.Database,
                    false);

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
                ErrorManager.Instance.LogInfo("Aylık satış sayısı alınıyor", "DashboardMethods.GetMonthlySalesCount");
                object result = _repository.GetMonthlySalesCount();
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.HandleException(
                    ex,
                    "Aylık satış sayısı alınırken hata oluştu",
                    ErrorSeverity.Warning,
                    ErrorSource.Database,
                    false);

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
                ErrorManager.Instance.LogInfo("Servis bekleyen araç sayısı alınıyor", "DashboardMethods.GetPendingServiceCount");
                object result = _repository.GetPendingServiceCount();
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.HandleException(
                    ex,
                    "Servis bekleyen araç sayısı alınırken hata oluştu",
                    ErrorSeverity.Warning,
                    ErrorSource.Database,
                    false);

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
                ErrorManager.Instance.LogInfo("Aktif personel sayısı alınıyor", "DashboardMethods.GetTeamMembersCount");
                object result = _repository.GetTeamMembersCount();
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.HandleException(
                    ex,
                    "Aktif personel sayısı alınırken hata oluştu",
                    ErrorSeverity.Warning,
                    ErrorSource.Database,
                    false);

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
                ErrorManager.Instance.LogInfo("Markalara göre araç dağılımı alınıyor", "DashboardMethods.GetBrandDistribution");
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
                ErrorManager.Instance.HandleException(
                    ex,
                    "Markalara göre araç dağılımı alınırken hata oluştu",
                    ErrorSeverity.Warning,
                    ErrorSource.Database,
                    false);
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
                ErrorManager.Instance.LogInfo("Yıllık kiralama istatistikleri alınıyor", "DashboardMethods.GetYearlyRentals");
                DataTable result = _repository.GetYearlyRentals();

                foreach (DataRow row in result.Rows)
                {
                    int year = Convert.ToInt32(row["Year"]);
                    int count = Convert.ToInt32(row["Count"]);
                    yearlyRentals.Add(year, count);
                }

                if (yearlyRentals.Count == 0)
                {
                    // Veri yoksa örnek veri oluştur
                    ErrorManager.Instance.LogWarning(
                        "Yıllık kiralama verisi bulunamadı, örnek veri oluşturuluyor",
                        "DashboardMethods.GetYearlyRentals");

                    int currentYear = DateTime.Now.Year;
                    for (int i = -3; i <= 0; i++)
                    {
                        yearlyRentals.Add(currentYear + i, new Random().Next(100, 500));
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.HandleException(
                    ex,
                    "Yıllık kiralama istatistikleri alınırken hata oluştu",
                    ErrorSeverity.Warning,
                    ErrorSource.Database,
                    false);

                // Hata olduğunda örnek veri oluştur
                ErrorManager.Instance.LogWarning(
                    "Hata nedeniyle örnek yıllık kiralama verisi oluşturuluyor",
                    "DashboardMethods.GetYearlyRentals");

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
                ErrorManager.Instance.LogInfo("Şubelere göre araç dağılımı alınıyor", "DashboardMethods.GetLocationData");
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
                ErrorManager.Instance.HandleException(
                    ex,
                    "Şubelere göre araç dağılımı alınırken hata oluştu",
                    ErrorSeverity.Warning,
                    ErrorSource.Database,
                    false);
            }

            return locationData;
        }
    }
}