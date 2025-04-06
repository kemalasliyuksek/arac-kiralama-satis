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
            return CustomerMethods.GetCustomersAsDataTable();
        }

        public static DataTable GetVehicleList()
        {
            return VehicleMethods.GetVehiclesAsDataTable();
        }

        public static DataTable GetRentalList()
        {
            return RentalMethods.GetRentalsAsDataTable();
        }

        public static DataTable GetStaffList()
        {
            return StaffMethods.GetStaffAsDataTable();
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

                return DatabaseHelper.ExecuteQuery(query);
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

                return DatabaseHelper.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Bakım listesi alınırken bir hata oluştu: " + ex.Message);
            }
        }
    }
}