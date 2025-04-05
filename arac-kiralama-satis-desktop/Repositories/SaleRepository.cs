using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using arac_kiralama_satis_desktop.Models;
using arac_kiralama_satis_desktop.Utils;

namespace arac_kiralama_satis_desktop.Repositories
{
    public class SaleRepository : IRepository<Sale, int>
    {
        private const string TABLE_NAME = "Satislar";

        public List<Sale> GetAll()
        {
            try
            {
                string query = $@"SELECT s.SatisID, s.MusteriID, CONCAT(m.Ad, ' ', m.Soyad) AS MusteriAdSoyad,
                                         s.AracID, a.Plaka, a.Marka, a.Model,
                                         s.SatisTarihi, s.SatisTutari, s.OdemeTipi, s.TaksitSayisi, s.NoterTarihi,
                                         s.SozlesmeID, s.KullaniciID, CONCAT(u.Ad, ' ', u.Soyad) AS KullaniciAdSoyad,
                                         s.OlusturmaTarihi, s.GuncellenmeTarihi
                                  FROM {TABLE_NAME} s
                                  JOIN Musteriler m ON s.MusteriID = m.MusteriID
                                  JOIN Araclar a ON s.AracID = a.AracID
                                  JOIN Kullanicilar u ON s.KullaniciID = u.KullaniciID
                                  ORDER BY s.SatisTarihi DESC";
                DataTable dt = DatabaseHelper.ExecuteQuery(query);
                return MapDataTableToSales(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Satışlar listelenirken bir hata oluştu: " + ex.Message, ex);
            }
        }

        public Sale GetById(int id)
        {
            try
            {
                string query = $@"SELECT s.SatisID, s.MusteriID, CONCAT(m.Ad, ' ', m.Soyad) AS MusteriAdSoyad,
                                         s.AracID, a.Plaka, a.Marka, a.Model,
                                         s.SatisTarihi, s.SatisTutari, s.OdemeTipi, s.TaksitSayisi, s.NoterTarihi,
                                         s.SozlesmeID, s.KullaniciID, CONCAT(u.Ad, ' ', u.Soyad) AS KullaniciAdSoyad,
                                         s.OlusturmaTarihi, s.GuncellenmeTarihi
                                  FROM {TABLE_NAME} s
                                  JOIN Musteriler m ON s.MusteriID = m.MusteriID
                                  JOIN Araclar a ON s.AracID = a.AracID
                                  JOIN Kullanicilar u ON s.KullaniciID = u.KullaniciID
                                  WHERE s.SatisID = @id";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@id", id)
                };

                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
                if (dt.Rows.Count > 0)
                    return MapDataRowToSale(dt.Rows[0]);
                else
                    throw new Exception("Satış kaydı bulunamadı.");
            }
            catch (Exception ex)
            {
                throw new Exception("Satış bilgisi alınırken bir hata oluştu: " + ex.Message, ex);
            }
        }

        public int Add(Sale entity)
        {
            try
            {
                string query = $@"INSERT INTO {TABLE_NAME}
                                  (MusteriID, AracID, SatisTarihi, SatisTutari, OdemeTipi, TaksitSayisi, NoterTarihi,
                                   SozlesmeID, KullaniciID)
                                  VALUES
                                  (@musteriId, @aracId, @satisTarihi, @tutar, @odeme, @taksit, @noter,
                                   @sozlesme, @kulId);
                                  SELECT LAST_INSERT_ID();";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@musteriId", entity.CustomerID),
                    DatabaseHelper.CreateParameter("@aracId", entity.VehicleID),
                    DatabaseHelper.CreateParameter("@satisTarihi", entity.SaleDate),
                    DatabaseHelper.CreateParameter("@tutar", entity.SaleAmount),
                    DatabaseHelper.CreateParameter("@odeme", entity.PaymentType),
                    DatabaseHelper.CreateParameter("@taksit", entity.InstallmentCount),
                    DatabaseHelper.CreateParameter("@noter", entity.NotaryDate ?? (object)DBNull.Value),
                    DatabaseHelper.CreateParameter("@sozlesme", entity.ContractID ?? (object)DBNull.Value),
                    DatabaseHelper.CreateParameter("@kulId", entity.UserID)
                };

                object result = DatabaseHelper.ExecuteScalar(query, parameters);
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Satış eklenirken bir hata oluştu: " + ex.Message, ex);
            }
        }

        public void Update(Sale entity)
        {
            try
            {
                string query = $@"UPDATE {TABLE_NAME} SET
                                  MusteriID = @musteriId,
                                  AracID = @aracId,
                                  SatisTarihi = @satisTarihi,
                                  SatisTutari = @tutar,
                                  OdemeTipi = @odeme,
                                  TaksitSayisi = @taksit,
                                  NoterTarihi = @noter,
                                  SozlesmeID = @sozlesme,
                                  KullaniciID = @kulId
                                  WHERE SatisID = @id";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@id", entity.SaleID),
                    DatabaseHelper.CreateParameter("@musteriId", entity.CustomerID),
                    DatabaseHelper.CreateParameter("@aracId", entity.VehicleID),
                    DatabaseHelper.CreateParameter("@satisTarihi", entity.SaleDate),
                    DatabaseHelper.CreateParameter("@tutar", entity.SaleAmount),
                    DatabaseHelper.CreateParameter("@odeme", entity.PaymentType),
                    DatabaseHelper.CreateParameter("@taksit", entity.InstallmentCount),
                    DatabaseHelper.CreateParameter("@noter", entity.NotaryDate ?? (object)DBNull.Value),
                    DatabaseHelper.CreateParameter("@sozlesme", entity.ContractID ?? (object)DBNull.Value),
                    DatabaseHelper.CreateParameter("@kulId", entity.UserID)
                };

                DatabaseHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Satış güncellenirken bir hata oluştu: " + ex.Message, ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                string query = $@"DELETE FROM {TABLE_NAME}
                                  WHERE SatisID = @id";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@id", id)
                };

                DatabaseHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Satış silinirken bir hata oluştu: " + ex.Message, ex);
            }
        }

        public List<Sale> Search(string searchText)
        {
            try
            {
                // Müşteri adı, plaka vb. üzerinden arama örneği
                string query = $@"SELECT s.SatisID, s.MusteriID, CONCAT(m.Ad, ' ', m.Soyad) AS MusteriAdSoyad,
                                         s.AracID, a.Plaka, a.Marka, a.Model,
                                         s.SatisTarihi, s.SatisTutari, s.OdemeTipi, s.TaksitSayisi, s.NoterTarihi,
                                         s.SozlesmeID, s.KullaniciID, CONCAT(u.Ad, ' ', u.Soyad) AS KullaniciAdSoyad,
                                         s.OlusturmaTarihi, s.GuncellenmeTarihi
                                  FROM {TABLE_NAME} s
                                  JOIN Musteriler m ON s.MusteriID = m.MusteriID
                                  JOIN Araclar a ON s.AracID = a.AracID
                                  JOIN Kullanicilar u ON s.KullaniciID = u.KullaniciID
                                  WHERE m.Ad LIKE @search
                                     OR m.Soyad LIKE @search
                                     OR a.Plaka LIKE @search
                                  ORDER BY s.SatisTarihi DESC";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@search", "%" + searchText + "%")
                };

                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
                return MapDataTableToSales(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Satış arama sırasında bir hata oluştu: " + ex.Message, ex);
            }
        }

        private List<Sale> MapDataTableToSales(DataTable dt)
        {
            var sales = new List<Sale>();
            foreach (DataRow row in dt.Rows)
            {
                sales.Add(MapDataRowToSale(row));
            }
            return sales;
        }

        private Sale MapDataRowToSale(DataRow row)
        {
            return new Sale
            {
                SaleID = row.GetValue<int>("SatisID"),
                CustomerID = row.GetValue<int>("MusteriID"),
                CustomerFullName = row.GetValue<string>("MusteriAdSoyad"),
                VehicleID = row.GetValue<int>("AracID"),
                VehiclePlate = row.GetValue<string>("Plaka"),
                VehicleBrand = row.GetValue<string>("Marka"),
                VehicleModel = row.GetValue<string>("Model"),
                SaleDate = row.GetValue<DateTime>("SatisTarihi"),
                SaleAmount = row.GetValue<decimal>("SatisTutari"),
                PaymentType = row.GetValue<string>("OdemeTipi"),
                InstallmentCount = row.GetValue<int>("TaksitSayisi"),
                NotaryDate = row.GetValue<DateTime?>("NoterTarihi"),
                ContractID = row.GetValue<int?>("SozlesmeID"),
                UserID = row.GetValue<int>("KullaniciID"),
                UserFullName = row.GetValue<string>("KullaniciAdSoyad"),
                CreatedDate = row.GetValue<DateTime>("OlusturmaTarihi"),
                UpdatedDate = row.GetValue<DateTime?>("GuncellenmeTarihi")
            };
        }
    }
}
