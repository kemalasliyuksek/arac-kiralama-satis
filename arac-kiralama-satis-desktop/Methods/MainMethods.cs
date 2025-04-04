﻿using System;
using System.Collections.Generic;
using System.Data;
using arac_kiralama_satis_desktop._Backups;
using MySql.Data.MySqlClient;

namespace arac_kiralama_satis_desktop.Methods
{
    public class MainMethods
    {
        public static DashboardData GetDashboardData()
        {
            DashboardData dashboardData = new DashboardData();

            try
            {
                // Get total car count
                try
                {
                    string carCountQuery = "SELECT COUNT(*) FROM Araclar";
                    object carCountResult = DatabaseConnection.ExecuteScalar(carCountQuery);
                    dashboardData.TotalCarCount = Convert.ToInt32(carCountResult);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error getting car count: {ex.Message}");
                    dashboardData.TotalCarCount = 0;
                }

                // Get total branch count
                try
                {
                    string locationCountQuery = "SELECT COUNT(*) FROM Subeler WHERE AktifMi = 1";
                    object locationCountResult = DatabaseConnection.ExecuteScalar(locationCountQuery);
                    dashboardData.LocationCount = Convert.ToInt32(locationCountResult);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error getting branch count: {ex.Message}");
                    dashboardData.LocationCount = 0;
                }

                // Get total customer count
                try
                {
                    string customerCountQuery = "SELECT COUNT(*) FROM Musteriler";
                    object customerCountResult = DatabaseConnection.ExecuteScalar(customerCountQuery);
                    dashboardData.CustomerCount = Convert.ToInt32(customerCountResult);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error getting customer count: {ex.Message}");
                    dashboardData.CustomerCount = 0;
                }

                // Get total revenue (from both rentals and sales)
                try
                {
                    string revenueQuery = @"
                        SELECT 
                            IFNULL((SELECT SUM(KiralamaTutari) FROM Kiralamalar), 0) + 
                            IFNULL((SELECT SUM(SatisTutari) FROM Satislar), 0) AS TotalRevenue";
                    object revenueResult = DatabaseConnection.ExecuteScalar(revenueQuery);
                    dashboardData.TotalRevenue = Convert.ToDecimal(revenueResult);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error getting total revenue: {ex.Message}");
                    dashboardData.TotalRevenue = 0;
                }

                // Keep brand count and average rental price for compatibility
                try
                {
                    string brandCountQuery = "SELECT COUNT(DISTINCT Marka) FROM Araclar";
                    object brandCountResult = DatabaseConnection.ExecuteScalar(brandCountQuery);
                    dashboardData.BrandCount = Convert.ToInt32(brandCountResult);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error getting brand count: {ex.Message}");
                    dashboardData.BrandCount = 0;
                }

                try
                {
                    string avgPriceQuery = "SELECT AVG(Fiyat) FROM KiraFiyatlari WHERE KiralamaTipi = 'Günlük'";
                    object avgPriceResult = DatabaseConnection.ExecuteScalar(avgPriceQuery);
                    dashboardData.AverageRentalPrice = avgPriceResult != DBNull.Value ? Convert.ToDouble(avgPriceResult) : 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error getting average rental price: {ex.Message}");
                    dashboardData.AverageRentalPrice = 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting dashboard data: {ex.Message}");

                // Set all values to 0 on error
                dashboardData.TotalCarCount = 0;
                dashboardData.LocationCount = 0;
                dashboardData.CustomerCount = 0;
                dashboardData.TotalRevenue = 0;
                dashboardData.BrandCount = 0;
                dashboardData.AverageRentalPrice = 0;
            }

            return dashboardData;
        }

        public static int GetActiveRentalsCount()
        {
            try
            {
                // Get count of active rentals (where end date is in the future and not returned yet)
                string query = @"
                    SELECT COUNT(*) 
                    FROM Kiralamalar 
                    WHERE BitisTarihi >= CURRENT_DATE() 
                    AND (TeslimTarihi IS NULL)";

                object result = DatabaseConnection.ExecuteScalar(query);
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting active rentals count: {ex.Message}");
                return 0;
            }
        }

        public static int GetMonthlySalesCount()
        {
            try
            {
                // Get count of sales in the current month
                string query = @"
                    SELECT COUNT(*) 
                    FROM Satislar 
                    WHERE MONTH(SatisTarihi) = MONTH(CURRENT_DATE()) 
                    AND YEAR(SatisTarihi) = YEAR(CURRENT_DATE())";

                object result = DatabaseConnection.ExecuteScalar(query);
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting monthly sales count: {ex.Message}");
                return 0;
            }
        }

        public static int GetPendingServiceCount()
        {
            try
            {
                // Get count of pending service/maintenance records (where end date is null)
                string query = @"
                    SELECT COUNT(*) 
                    FROM Bakimlar 
                    WHERE BitisTarihi IS NULL";

                object result = DatabaseConnection.ExecuteScalar(query);
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting pending service count: {ex.Message}");
                return 0;
            }
        }

        public static int GetTeamMembersCount()
        {
            try
            {
                // Get count of active employees
                string query = @"
                    SELECT COUNT(*) 
                    FROM Kullanicilar 
                    WHERE Durum = 1";

                object result = DatabaseConnection.ExecuteScalar(query);
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting team members count: {ex.Message}");
                return 0;
            }
        }

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
                Console.WriteLine($"Error getting brand distribution: {ex.Message}");
            }

            return brandDistribution;
        }

        public static Dictionary<int, int> GetYearlyRentals()
        {
            Dictionary<int, int> yearlyRentals = new Dictionary<int, int>();

            try
            {
                // Get rental counts grouped by year
                string query = @"
                    SELECT YEAR(BaslangicTarihi) as Year, COUNT(*) as Count 
                    FROM Kiralamalar 
                    GROUP BY YEAR(BaslangicTarihi)
                    ORDER BY Year";

                DataTable result = DatabaseConnection.ExecuteQuery(query);

                foreach (DataRow row in result.Rows)
                {
                    int year = Convert.ToInt32(row["Year"]);
                    int count = Convert.ToInt32(row["Count"]);
                    yearlyRentals.Add(year, count);
                }

                // If no data found, provide sample data for demonstration
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

                // Provide sample data on error
                int currentYear = DateTime.Now.Year;
                for (int i = -3; i <= 0; i++)
                {
                    yearlyRentals.Add(currentYear + i, new Random().Next(100, 500));
                }
            }

            return yearlyRentals;
        }

        public static Dictionary<string, int> GetLocationData()
        {
            Dictionary<string, int> locationData = new Dictionary<string, int>();

            try
            {
                string query = @"
                    SELECT s.SubeAdi, COUNT(a.AracID) as AracSayisi 
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
                Console.WriteLine($"Error getting location data: {ex.Message}");
            }

            return locationData;
        }

        // Existing methods for lists remain unchanged
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

        // Personel listesini getiren metot
        public static DataTable GetStaffList()
        {
            try
            {
                string query = @"SELECT 
            k.KullaniciID, 
            k.Ad, 
            k.Soyad, 
            k.KullaniciAdi, 
            k.Email, 
            CONCAT(k.UlkeKodu, k.TelefonNo) as Telefon,
            r.RolAdi, 
            s.SubeAdi, 
            IF(k.Durum, 'Aktif', 'Pasif') as Durum, 
            k.SonGirisTarihi,
            k.OlusturmaTarihi
            FROM Kullanicilar k
            LEFT JOIN Roller r ON k.RolID = r.RolID
            LEFT JOIN Subeler s ON k.SubeID = s.SubeID
            ORDER BY k.KullaniciID DESC";

                return DatabaseConnection.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Personel listesi alınırken bir hata oluştu: " + ex.Message);
            }
        }

        // Personel arama metotu (ihtiyaç olursa daha sonra eklenebilir)
        public static DataTable SearchStaffList(string searchText)
        {
            try
            {
                string query = @"SELECT k.KullaniciID, k.Ad, k.Soyad, k.KullaniciAdi, k.Email, 
                               CONCAT(k.UlkeKodu, k.TelefonNo) as Telefon,
                               r.RolAdi, s.SubeAdi, k.Durum, k.SonGirisTarihi,
                               k.OlusturmaTarihi
                               FROM Kullanicilar k
                               LEFT JOIN Roller r ON k.RolID = r.RolID
                               LEFT JOIN Subeler s ON k.SubeID = s.SubeID
                               WHERE k.Ad LIKE @searchText 
                               OR k.Soyad LIKE @searchText 
                               OR k.KullaniciAdi LIKE @searchText 
                               OR k.Email LIKE @searchText 
                               OR CONCAT(k.UlkeKodu, k.TelefonNo) LIKE @searchText
                               OR r.RolAdi LIKE @searchText
                               OR s.SubeAdi LIKE @searchText
                               ORDER BY k.KullaniciID DESC";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@searchText", "%" + searchText + "%")
                };

                return DatabaseConnection.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Personel arama sırasında bir hata oluştu: " + ex.Message);
            }
        }

        // Personel durumunu değiştiren metot
        public static void ChangeStaffStatus(int staffId, bool isActive)
        {
            try
            {
                string query = "UPDATE Kullanicilar SET Durum = @durum WHERE KullaniciID = @kullaniciId";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@kullaniciId", staffId),
                    new MySqlParameter("@durum", isActive)
                };

                DatabaseConnection.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Personel durumu güncellenirken bir hata oluştu: " + ex.Message);
            }
        }
    }

    public class DashboardData
    {
        public int TotalCarCount { get; set; }
        public int LocationCount { get; set; }
        public int CustomerCount { get; set; }
        public decimal TotalRevenue { get; set; }
        public int BrandCount { get; set; }
        public double AverageRentalPrice { get; set; }
    }
}