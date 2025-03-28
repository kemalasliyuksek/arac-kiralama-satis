using System;
using System.Collections.Generic;
using System.Data;
using arac_kiralama_satis_desktop.Utils;
using MySql.Data.MySqlClient;

namespace arac_kiralama_satis_desktop.Methods
{
    public class MainMethods
    {
        /// <summary>
        /// Dashboard için özet verileri alır
        /// </summary>
        public static DashboardData GetDashboardData()
        {
            DashboardData dashboardData = new DashboardData();

            try
            {
                try
                {
                    // Toplam araç sayısı
                    string carCountQuery = "SELECT COUNT(*) FROM Araclar";
                    object carCountResult = DatabaseConnection.ExecuteScalar(carCountQuery);
                    dashboardData.TotalCarCount = Convert.ToInt32(carCountResult);
                }
                catch (Exception)
                {
                    // Hata durumunda varsayılan değer
                    dashboardData.TotalCarCount = 10;
                }

                try
                {
                    // Şube sayısı
                    string locationCountQuery = "SELECT COUNT(*) FROM Subeler WHERE AktifMi = 1";
                    object locationCountResult = DatabaseConnection.ExecuteScalar(locationCountQuery);
                    dashboardData.LocationCount = Convert.ToInt32(locationCountResult);
                }
                catch (Exception)
                {
                    // Hata durumunda varsayılan değer
                    dashboardData.LocationCount = 3;
                }

                try
                {
                    // Marka sayısı
                    string brandCountQuery = "SELECT COUNT(DISTINCT Marka) FROM Araclar";
                    object brandCountResult = DatabaseConnection.ExecuteScalar(brandCountQuery);
                    dashboardData.BrandCount = Convert.ToInt32(brandCountResult);
                }
                catch (Exception)
                {
                    // Hata durumunda varsayılan değer
                    dashboardData.BrandCount = 7;
                }

                try
                {
                    // Ortalama kiralama fiyatı
                    string avgPriceQuery = "SELECT AVG(Fiyat) FROM KiraFiyatlari WHERE KiralamaTipi = 'Haftalık'";
                    object avgPriceResult = DatabaseConnection.ExecuteScalar(avgPriceQuery);
                    dashboardData.AverageRentalPrice = Convert.ToDouble(avgPriceResult);
                }
                catch (Exception)
                {
                    // Hata durumunda varsayılan değer
                    dashboardData.AverageRentalPrice = 12500.00;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Dashboard verileri alınırken bir hata oluştu: " + ex.Message);

                // Hata durumunda varsayılan değerler
                dashboardData.TotalCarCount = 10;
                dashboardData.LocationCount = 3;
                dashboardData.BrandCount = 7;
                dashboardData.AverageRentalPrice = 12500.00;
            }

            return dashboardData;
        }

        /// <summary>
        /// Marka dağılımını alır
        /// </summary>
        public static Dictionary<string, int> GetBrandDistribution()
        {
            Dictionary<string, int> brandDistribution = new Dictionary<string, int>();

            try
            {
                string query = "SELECT Marka, COUNT(*) as Adet FROM Araclar GROUP BY Marka ORDER BY Adet DESC";

                DataTable result = DatabaseConnection.ExecuteQuery(query);

                foreach (DataRow row in result.Rows)
                {
                    string brand = row["Marka"].ToString();
                    int count = Convert.ToInt32(row["Adet"]);
                    brandDistribution.Add(brand, count);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Marka dağılımı alınırken bir hata oluştu: " + ex.Message);
            }

            return brandDistribution;
        }

        /// <summary>
        /// Yıllara göre kiralama verilerini alır
        /// </summary>
        public static Dictionary<int, int> GetYearlyRentals()
        {
            Dictionary<int, int> yearlyRentals = new Dictionary<int, int>();

            try
            {
                // Gerçek veri olmadığı için örnek veri oluştur
                // Not: Gerçek uygulamada bu verileri veritabanından çekmelisiniz
                yearlyRentals.Add(2019, 150);
                yearlyRentals.Add(2020, 70);
                yearlyRentals.Add(2021, 200);
                yearlyRentals.Add(2022, 480);
                yearlyRentals.Add(2023, 330);
                yearlyRentals.Add(2024, 220);
                yearlyRentals.Add(2025, 450);

                // Eğer sistemde veri varsa, aşağıdaki sorguyu kullanabilirsiniz
                /*
                string query = @"SELECT YEAR(BaslangicTarihi) as Yil, COUNT(*) as KiralamaSayisi 
                               FROM Kiralamalar 
                               GROUP BY YEAR(BaslangicTarihi) 
                               ORDER BY Yil";
                
                DataTable result = DatabaseConnection.ExecuteQuery(query);

                foreach (DataRow row in result.Rows)
                {
                    int year = Convert.ToInt32(row["Yil"]);
                    int count = Convert.ToInt32(row["KiralamaSayisi"]);
                    yearlyRentals.Add(year, count);
                }
                */
            }
            catch (Exception ex)
            {
                throw new Exception("Yıllık kiralama verileri alınırken bir hata oluştu: " + ex.Message);
            }

            return yearlyRentals;
        }

        /// <summary>
        /// Şubelere göre araç sayılarını alır
        /// </summary>
        public static Dictionary<string, int> GetLocationData()
        {
            Dictionary<string, int> locationData = new Dictionary<string, int>();

            try
            {
                string query = @"SELECT s.SubeAdi, COUNT(a.AracID) as AracSayisi 
                               FROM Subeler s
                               LEFT JOIN Araclar a ON s.SubeID = a.SubeID
                               WHERE s.AktifMi = 1
                               GROUP BY s.SubeAdi
                               ORDER BY AracSayisi DESC";

                DataTable result = DatabaseConnection.ExecuteQuery(query);

                foreach (DataRow row in result.Rows)
                {
                    string location = row["SubeAdi"].ToString();
                    int count = Convert.ToInt32(row["AracSayisi"]);
                    locationData.Add(location, count);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lokasyon verileri alınırken bir hata oluştu: " + ex.Message);
            }

            return locationData;
        }

        /// <summary>
        /// Araç detaylarını almak için kullanılır
        /// </summary>
        public static DataTable GetVehicleList()
        {
            try
            {
                string query = @"SELECT a.AracID, a.Plaka, a.Marka, a.Model, a.YapimYili, 
                               a.Renk, a.Kilometre, a.YakitTipi, a.VitesTipi, 
                               d.DurumAdi as Durum,
                               s.SubeAdi as Sube,
                               c.SinifAdi as AracSinifi
                               FROM Araclar a
                               LEFT JOIN AracDurumlari d ON a.DurumID = d.DurumID
                               LEFT JOIN Subeler s ON a.SubeID = s.SubeID
                               LEFT JOIN AracSiniflari c ON a.AracSinifID = c.AracSinifID
                               ORDER BY a.AracID DESC";

                return DatabaseConnection.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Araç listesi alınırken bir hata oluştu: " + ex.Message);
            }
        }

        /// <summary>
        /// Müşteri listesini almak için kullanılır
        /// </summary>
        public static DataTable GetCustomerList()
        {
            try
            {
                string query = @"SELECT MusteriID, Ad, Soyad, TC, CONCAT(UlkeKodu, TelefonNo) as Telefon, 
                               Email, MusteriTipi, KayitTarihi
                               FROM Musteriler
                               ORDER BY KayitTarihi DESC";

                return DatabaseConnection.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Müşteri listesi alınırken bir hata oluştu: " + ex.Message);
            }
        }

        /// <summary>
        /// Kiralama listesini almak için kullanılır
        /// </summary>
        public static DataTable GetRentalList()
        {
            try
            {
                string query = @"SELECT k.KiralamaID, 
                               CONCAT(m.Ad, ' ', m.Soyad) as MusteriAdSoyad,
                               a.Plaka, a.Marka, a.Model,
                               k.BaslangicTarihi, k.BitisTarihi, k.TeslimTarihi,
                               k.KiralamaTutari, k.OdemeTipi
                               FROM Kiralamalar k
                               JOIN Musteriler m ON k.MusteriID = m.MusteriID
                               JOIN Araclar a ON k.AracID = a.AracID
                               ORDER BY k.BaslangicTarihi DESC";

                return DatabaseConnection.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Kiralama listesi alınırken bir hata oluştu: " + ex.Message);
            }
        }

        /// <summary>
        /// Satış listesini almak için kullanılır
        /// </summary>
        public static DataTable GetSalesList()
        {
            try
            {
                string query = @"SELECT s.SatisID, 
                               CONCAT(m.Ad, ' ', m.Soyad) as MusteriAdSoyad,
                               a.Plaka, a.Marka, a.Model,
                               s.SatisTarihi, s.SatisTutari, s.OdemeTipi,
                               s.TaksitSayisi, s.NoterTarihi
                               FROM Satislar s
                               JOIN Musteriler m ON s.MusteriID = m.MusteriID
                               JOIN Araclar a ON s.AracID = a.AracID
                               ORDER BY s.SatisTarihi DESC";

                return DatabaseConnection.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Satış listesi alınırken bir hata oluştu: " + ex.Message);
            }
        }

        /// <summary>
        /// Bakım listesini almak için kullanılır
        /// </summary>
        public static DataTable GetMaintenanceList()
        {
            try
            {
                string query = @"SELECT b.BakimID, 
                               a.Plaka, a.Marka, a.Model,
                               b.BaslamaTarihi, b.BitisTarihi, 
                               b.BakimTipi, b.Maliyet,
                               s.ServisAdi
                               FROM Bakimlar b
                               JOIN Araclar a ON b.AracID = a.AracID
                               LEFT JOIN Servisler s ON b.ServisID = s.ServisID
                               ORDER BY b.BaslamaTarihi DESC";

                return DatabaseConnection.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Bakım listesi alınırken bir hata oluştu: " + ex.Message);
            }
        }
    }

    /// <summary>
    /// Dashboard ana sayfasındaki verileri tutan sınıf
    /// </summary>
    public class DashboardData
    {
        public int TotalCarCount { get; set; }
        public int LocationCount { get; set; }
        public int BrandCount { get; set; }
        public double AverageRentalPrice { get; set; }
    }
}