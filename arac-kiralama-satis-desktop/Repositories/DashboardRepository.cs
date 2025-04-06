using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using arac_kiralama_satis_desktop.Utils;

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
                return DatabaseHelper.ExecuteScalar(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Araç sayısı alınırken bir hata oluştu: " + ex.Message, ex);
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
                return DatabaseHelper.ExecuteScalar(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Şube sayısı alınırken bir hata oluştu: " + ex.Message, ex);
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
                return DatabaseHelper.ExecuteScalar(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Müşteri sayısı alınırken bir hata oluştu: " + ex.Message, ex);
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
                return DatabaseHelper.ExecuteScalar(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Toplam gelir alınırken bir hata oluştu: " + ex.Message, ex);
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
                return DatabaseHelper.ExecuteScalar(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Aktif kiralamalar sayısı alınırken bir hata oluştu: " + ex.Message, ex);
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
                return DatabaseHelper.ExecuteScalar(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Aylık satış sayısı alınırken bir hata oluştu: " + ex.Message, ex);
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
                return DatabaseHelper.ExecuteScalar(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Bekleyen servis sayısı alınırken bir hata oluştu: " + ex.Message, ex);
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
                return DatabaseHelper.ExecuteScalar(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Ekip üyesi sayısı alınırken bir hata oluştu: " + ex.Message, ex);
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
                return DatabaseHelper.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Marka dağılımı alınırken bir hata oluştu: " + ex.Message, ex);
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
                return DatabaseHelper.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Yıllık kiralamalar alınırken bir hata oluştu: " + ex.Message, ex);
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
                return DatabaseHelper.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Konum verisi alınırken bir hata oluştu: " + ex.Message, ex);
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
                return DatabaseHelper.ExecuteScalar(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Marka sayısı alınırken bir hata oluştu: " + ex.Message, ex);
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
                return DatabaseHelper.ExecuteScalar(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Ortalama kiralama fiyatı alınırken bir hata oluştu: " + ex.Message, ex);
            }
        }
    }
}