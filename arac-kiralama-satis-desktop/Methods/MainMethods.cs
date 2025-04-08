using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using arac_kiralama_satis_desktop.Utils;

namespace arac_kiralama_satis_desktop.Methods
{
    public class MainMethods
    {
        // Farklı listeler için DataTable metodları
        public static DataTable GetCustomerList()
        {
            try
            {
                ErrorManager.Instance.LogInfo("Müşteri listesi alınıyor", "MainMethods.GetCustomerList");
                return CustomerMethods.GetCustomersAsDataTable();
            }
            catch (Exception ex)
            {
                // Exception zaten CustomerMethods tarafından işlenmiştir, bu nedenle sadece loglayıp yeniden fırlatıyoruz
                ErrorManager.Instance.LogWarning(
                    $"GetCustomerList metodu hata ile karşılaştı: {ex.Message}",
                    "MainMethods.GetCustomerList");
                throw; // Orijinal exception'ı koru ve yeniden fırlat
            }
        }

        public static DataTable GetVehicleList()
        {
            try
            {
                ErrorManager.Instance.LogInfo("Araç listesi alınıyor", "MainMethods.GetVehicleList");
                return VehicleMethods.GetVehiclesAsDataTable();
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.LogWarning(
                    $"GetVehicleList metodu hata ile karşılaştı: {ex.Message}",
                    "MainMethods.GetVehicleList");
                throw; // Orijinal exception'ı koru ve yeniden fırlat
            }
        }

        public static DataTable GetRentalList()
        {
            try
            {
                ErrorManager.Instance.LogInfo("Kiralama listesi alınıyor", "MainMethods.GetRentalList");
                return RentalMethods.GetRentalsAsDataTable();
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.LogWarning(
                    $"GetRentalList metodu hata ile karşılaştı: {ex.Message}",
                    "MainMethods.GetRentalList");
                throw; // Orijinal exception'ı koru ve yeniden fırlat
            }
        }

        public static DataTable GetStaffList()
        {
            try
            {
                ErrorManager.Instance.LogInfo("Personel listesi alınıyor", "MainMethods.GetStaffList");
                return StaffMethods.GetStaffAsDataTable();
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.LogWarning(
                    $"GetStaffList metodu hata ile karşılaştı: {ex.Message}",
                    "MainMethods.GetStaffList");
                throw; // Orijinal exception'ı koru ve yeniden fırlat
            }
        }

        public static DataTable GetSalesList()
        {
            try
            {
                ErrorManager.Instance.LogInfo("Satış listesi alınıyor", "MainMethods.GetSalesList");

                string query = @"SELECT s.SatisID, 
                               CONCAT(m.Ad, ' ', m.Soyad) as MusteriAdSoyad,
                               a.Plaka, a.Marka, a.Model,
                               s.SatisTarihi, s.SatisTutari, s.OdemeTipi,
                               s.TaksitSayisi, s.NoterTarihi
                               FROM Satislar s
                               JOIN Musteriler m ON s.MusteriID = m.MusteriID
                               JOIN Araclar a ON s.AracID = a.AracID
                               ORDER BY s.SatisTarihi DESC";

                return DatabaseHelper.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Satış listesi alınırken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Satış listesi alınırken bir hata oluştu. (Hata ID: {errorId})");
            }
        }

        public static DataTable GetMaintenanceList()
        {
            try
            {
                ErrorManager.Instance.LogInfo("Bakım listesi alınıyor", "MainMethods.GetMaintenanceList");

                string query = @"SELECT b.BakimID, 
                               a.Plaka, a.Marka, a.Model,
                               b.BaslamaTarihi, b.BitisTarihi, 
                               b.BakimTipi, b.Maliyet,
                               s.ServisAdi
                               FROM Bakimlar b
                               JOIN Araclar a ON b.AracID = a.AracID
                               LEFT JOIN Servisler s ON b.ServisID = s.ServisID
                               ORDER BY b.BaslamaTarihi DESC";

                return DatabaseHelper.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Bakım listesi alınırken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Bakım listesi alınırken bir hata oluştu. (Hata ID: {errorId})");
            }
        }
    }
}