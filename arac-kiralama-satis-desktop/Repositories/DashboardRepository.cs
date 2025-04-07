using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using arac_kiralama_satis_desktop.Utils;
using arac_kiralama_satis_desktop.Models;

namespace arac_kiralama_satis_desktop.Repositories
{
    public class DashboardRepository
    {
        /// <summary>
        /// Araç sayısını getirir
        /// </summary>
        public object GetVehicleCount()
        {
            try
            {
                string query = "SELECT COUNT(*) FROM Araclar";

                ErrorManager.Instance.LogInfo("Toplam araç sayısı getiriliyor", "DashboardRepository.GetVehicleCount");
                object result = DatabaseHelper.ExecuteScalar(query);

                int count = Convert.ToInt32(result);
                ErrorManager.Instance.LogInfo($"Toplam araç sayısı: {count}", "DashboardRepository.GetVehicleCount");

                return result;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Araç sayısı alınırken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Araç sayısı alınırken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// Aktif şube sayısını getirir
        /// </summary>
        public object GetBranchCount()
        {
            try
            {
                string query = "SELECT COUNT(*) FROM Subeler WHERE AktifMi = 1";

                ErrorManager.Instance.LogInfo("Aktif şube sayısı getiriliyor", "DashboardRepository.GetBranchCount");
                object result = DatabaseHelper.ExecuteScalar(query);

                int count = Convert.ToInt32(result);
                ErrorManager.Instance.LogInfo($"Aktif şube sayısı: {count}", "DashboardRepository.GetBranchCount");

                return result;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Şube sayısı alınırken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Şube sayısı alınırken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// Müşteri sayısını getirir
        /// </summary>
        public object GetCustomerCount()
        {
            try
            {
                string query = "SELECT COUNT(*) FROM Musteriler";

                ErrorManager.Instance.LogInfo("Toplam müşteri sayısı getiriliyor", "DashboardRepository.GetCustomerCount");
                object result = DatabaseHelper.ExecuteScalar(query);

                int count = Convert.ToInt32(result);
                ErrorManager.Instance.LogInfo($"Toplam müşteri sayısı: {count}", "DashboardRepository.GetCustomerCount");

                return result;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Müşteri sayısı alınırken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Müşteri sayısı alınırken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// Toplam geliri hesaplar (Kiralama + Satış)
        /// </summary>
        public object GetTotalRevenue()
        {
            try
            {
                string query = @"
                    SELECT 
                        IFNULL((SELECT SUM(KiralamaTutari) FROM Kiralamalar), 0) + 
                        IFNULL((SELECT SUM(SatisTutari) FROM Satislar), 0) AS TotalRevenue";

                ErrorManager.Instance.LogInfo("Toplam gelir hesaplanıyor", "DashboardRepository.GetTotalRevenue");
                object result = DatabaseHelper.ExecuteScalar(query);

                decimal revenue = Convert.ToDecimal(result);
                ErrorManager.Instance.LogInfo($"Toplam gelir: {revenue:C2}", "DashboardRepository.GetTotalRevenue");

                return result;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Toplam gelir alınırken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Toplam gelir alınırken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// Aktif kiralama sayısını getirir
        /// </summary>
        public object GetActiveRentalsCount()
        {
            try
            {
                string query = @"
                    SELECT COUNT(*) 
                    FROM Kiralamalar 
                    WHERE BitisTarihi >= CURRENT_DATE() 
                    AND (TeslimTarihi IS NULL)";

                ErrorManager.Instance.LogInfo("Aktif kiralama sayısı getiriliyor", "DashboardRepository.GetActiveRentalsCount");
                object result = DatabaseHelper.ExecuteScalar(query);

                int count = Convert.ToInt32(result);
                ErrorManager.Instance.LogInfo($"Aktif kiralama sayısı: {count}", "DashboardRepository.GetActiveRentalsCount");

                return result;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Aktif kiralamalar sayısı alınırken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Aktif kiralamalar sayısı alınırken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// İçinde bulunulan aydaki satış sayısını getirir
        /// </summary>
        public object GetMonthlySalesCount()
        {
            try
            {
                string query = @"
                    SELECT COUNT(*) 
                    FROM Satislar 
                    WHERE MONTH(SatisTarihi) = MONTH(CURRENT_DATE()) 
                    AND YEAR(SatisTarihi) = YEAR(CURRENT_DATE())";

                string currentMonth = DateTime.Now.ToString("MMMM yyyy");
                ErrorManager.Instance.LogInfo($"{currentMonth} için aylık satış sayısı getiriliyor", "DashboardRepository.GetMonthlySalesCount");

                object result = DatabaseHelper.ExecuteScalar(query);

                int count = Convert.ToInt32(result);
                ErrorManager.Instance.LogInfo($"{currentMonth} için aylık satış sayısı: {count}", "DashboardRepository.GetMonthlySalesCount");

                return result;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Aylık satış sayısı alınırken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Aylık satış sayısı alınırken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// Servis bekleyen araç sayısını getirir
        /// </summary>
        public object GetPendingServiceCount()
        {
            try
            {
                string query = @"
                    SELECT COUNT(*) 
                    FROM Bakimlar 
                    WHERE BitisTarihi IS NULL";

                ErrorManager.Instance.LogInfo("Servis bekleyen araç sayısı getiriliyor", "DashboardRepository.GetPendingServiceCount");
                object result = DatabaseHelper.ExecuteScalar(query);

                int count = Convert.ToInt32(result);
                ErrorManager.Instance.LogInfo($"Servis bekleyen araç sayısı: {count}", "DashboardRepository.GetPendingServiceCount");

                return result;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Bekleyen servis sayısı alınırken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Bekleyen servis sayısı alınırken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// Aktif personel sayısını getirir
        /// </summary>
        public object GetTeamMembersCount()
        {
            try
            {
                string query = @"
                    SELECT COUNT(*) 
                    FROM Kullanicilar 
                    WHERE Durum = 1";

                ErrorManager.Instance.LogInfo("Aktif personel sayısı getiriliyor", "DashboardRepository.GetTeamMembersCount");
                object result = DatabaseHelper.ExecuteScalar(query);

                int count = Convert.ToInt32(result);
                ErrorManager.Instance.LogInfo($"Aktif personel sayısı: {count}", "DashboardRepository.GetTeamMembersCount");

                return result;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Ekip üyesi sayısı alınırken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Ekip üyesi sayısı alınırken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// Araç markalarının dağılımını getirir
        /// </summary>
        public DataTable GetBrandDistribution()
        {
            try
            {
                string query = "SELECT Marka, COUNT(*) as Adet FROM Araclar GROUP BY Marka ORDER BY Adet DESC";

                ErrorManager.Instance.LogInfo("Araç markalarının dağılımı getiriliyor", "DashboardRepository.GetBrandDistribution");
                DataTable result = DatabaseHelper.ExecuteQuery(query);

                ErrorManager.Instance.LogInfo($"Araç markaları dağılımı: {result.Rows.Count} farklı marka bulundu", "DashboardRepository.GetBrandDistribution");

                return result;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Marka dağılımı alınırken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Marka dağılımı alınırken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// Yıllara göre kiralama sayılarını getirir
        /// </summary>
        public DataTable GetYearlyRentals()
        {
            try
            {
                string query = @"
                    SELECT YEAR(BaslangicTarihi) as Year, COUNT(*) as Count 
                    FROM Kiralamalar 
                    GROUP BY YEAR(BaslangicTarihi)
                    ORDER BY Year";

                ErrorManager.Instance.LogInfo("Yıllık kiralama istatistikleri getiriliyor", "DashboardRepository.GetYearlyRentals");
                DataTable result = DatabaseHelper.ExecuteQuery(query);

                ErrorManager.Instance.LogInfo($"Yıllık kiralama istatistikleri: {result.Rows.Count} yıl için veri bulundu", "DashboardRepository.GetYearlyRentals");

                return result;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Yıllık kiralamalar alınırken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Yıllık kiralamalar alınırken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// Şubelerdeki araç sayılarını getirir
        /// </summary>
        public DataTable GetLocationData()
        {
            try
            {
                string query = @"
                    SELECT s.SubeAdi, COUNT(a.AracID) as AracSayisi 
                    FROM Subeler s
                    LEFT JOIN Araclar a ON s.SubeID = a.SubeID
                    WHERE s.AktifMi = 1
                    GROUP BY s.SubeAdi
                    ORDER BY AracSayisi DESC";

                ErrorManager.Instance.LogInfo("Şubelerdeki araç dağılımı getiriliyor", "DashboardRepository.GetLocationData");
                DataTable result = DatabaseHelper.ExecuteQuery(query);

                ErrorManager.Instance.LogInfo($"Şubelerdeki araç dağılımı: {result.Rows.Count} şube için veri bulundu", "DashboardRepository.GetLocationData");

                return result;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Konum verisi alınırken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Konum verisi alınırken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// Farklı marka sayısını getirir
        /// </summary>
        public object GetBrandCount()
        {
            try
            {
                string query = "SELECT COUNT(DISTINCT Marka) FROM Araclar";

                ErrorManager.Instance.LogInfo("Farklı marka sayısı getiriliyor", "DashboardRepository.GetBrandCount");
                object result = DatabaseHelper.ExecuteScalar(query);

                int count = Convert.ToInt32(result);
                ErrorManager.Instance.LogInfo($"Farklı marka sayısı: {count}", "DashboardRepository.GetBrandCount");

                return result;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Marka sayısı alınırken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Marka sayısı alınırken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// Ortalama günlük kiralama fiyatını getirir
        /// </summary>
        public object GetAverageRentalPrice()
        {
            try
            {
                string query = "SELECT AVG(Fiyat) FROM KiraFiyatlari WHERE KiralamaTipi = 'Günlük'";

                ErrorManager.Instance.LogInfo("Ortalama günlük kiralama fiyatı getiriliyor", "DashboardRepository.GetAverageRentalPrice");
                object result = DatabaseHelper.ExecuteScalar(query);

                if (result != DBNull.Value && result != null)
                {
                    decimal avgPrice = Convert.ToDecimal(result);
                    ErrorManager.Instance.LogInfo($"Ortalama günlük kiralama fiyatı: {avgPrice:C2}", "DashboardRepository.GetAverageRentalPrice");
                }
                else
                {
                    ErrorManager.Instance.LogWarning("Ortalama günlük kiralama fiyatı hesaplanamadı, veri bulunamadı", "DashboardRepository.GetAverageRentalPrice");
                    result = 0;
                }

                return result;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Ortalama kiralama fiyatı alınırken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Ortalama kiralama fiyatı alınırken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// Dashboard için tüm istatistikleri tek seferde getirir
        /// </summary>
        public DashboardData GetAllDashboardData()
        {
            try
            {
                ErrorManager.Instance.LogInfo("Dashboard için tüm veriler getiriliyor", "DashboardRepository.GetAllDashboardData");

                DashboardData dashboardData = new DashboardData();

                // Tüm verileri tek bir işlemde toparlama
                dashboardData.TotalCarCount = Convert.ToInt32(GetVehicleCount());
                dashboardData.LocationCount = Convert.ToInt32(GetBranchCount());
                dashboardData.CustomerCount = Convert.ToInt32(GetCustomerCount());
                dashboardData.TotalRevenue = Convert.ToDecimal(GetTotalRevenue());
                dashboardData.BrandCount = Convert.ToInt32(GetBrandCount());

                object avgPrice = GetAverageRentalPrice();
                dashboardData.AverageRentalPrice = avgPrice != DBNull.Value ? Convert.ToDouble(avgPrice) : 0;

                dashboardData.ActiveRentalsCount = Convert.ToInt32(GetActiveRentalsCount());
                dashboardData.MonthlySalesCount = Convert.ToInt32(GetMonthlySalesCount());
                dashboardData.PendingServiceCount = Convert.ToInt32(GetPendingServiceCount());
                dashboardData.TeamMembersCount = Convert.ToInt32(GetTeamMembersCount());

                ErrorManager.Instance.LogInfo("Dashboard verileri başarıyla toplandı", "DashboardRepository.GetAllDashboardData");

                return dashboardData;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Dashboard verileri alınırken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Dashboard verileri alınırken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }
    }
}