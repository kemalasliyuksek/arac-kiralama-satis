using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using arac_kiralama_satis_desktop.Models;
using arac_kiralama_satis_desktop.Utils;

namespace arac_kiralama_satis_desktop.Repositories
{
    public class RentalRepository : IRepository<Rental, int>
    {
        private const string TABLE_NAME = "Kiralamalar";

        public List<Rental> GetAll()
        {
            try
            {
                string query = $@"SELECT k.KiralamaID, k.MusteriID, 
                                        CONCAT(m.Ad, ' ', m.Soyad) AS MusteriAdSoyad,
                                        k.AracID, a.Plaka, a.Marka, a.Model,
                                        k.BaslangicTarihi, k.BitisTarihi, k.TeslimTarihi,
                                        k.BaslangicKm, k.BitisKm, k.KiralamaTutari, k.DepozitTutari, k.OdemeTipi,
                                        k.KiralamaNotuID, k.SozlesmeID, k.KullaniciID, 
                                        CONCAT(ku.Ad, ' ', ku.Soyad) AS KullaniciAdSoyad,
                                        k.OlusturmaTarihi, k.GuncellenmeTarihi
                                 FROM {TABLE_NAME} k
                                 JOIN Musteriler m ON k.MusteriID = m.MusteriID
                                 JOIN Araclar a ON k.AracID = a.AracID
                                 JOIN Kullanicilar ku ON k.KullaniciID = ku.KullaniciID
                                 ORDER BY k.KiralamaID DESC";
                DataTable dt = DatabaseHelper.ExecuteQuery(query);
                return MapDataTableToRentals(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Kiralamalar listelenirken bir hata oluştu: " + ex.Message, ex);
            }
        }

        public Rental GetById(int id)
        {
            try
            {
                string query = $@"SELECT k.KiralamaID, k.MusteriID, 
                                        CONCAT(m.Ad, ' ', m.Soyad) AS MusteriAdSoyad,
                                        k.AracID, a.Plaka, a.Marka, a.Model,
                                        k.BaslangicTarihi, k.BitisTarihi, k.TeslimTarihi,
                                        k.BaslangicKm, k.BitisKm, k.KiralamaTutari, k.DepozitTutari, k.OdemeTipi,
                                        k.KiralamaNotuID, k.SozlesmeID, k.KullaniciID, 
                                        CONCAT(ku.Ad, ' ', ku.Soyad) AS KullaniciAdSoyad,
                                        k.OlusturmaTarihi, k.GuncellenmeTarihi
                                 FROM {TABLE_NAME} k
                                 JOIN Musteriler m ON k.MusteriID = m.MusteriID
                                 JOIN Araclar a ON k.AracID = a.AracID
                                 JOIN Kullanicilar ku ON k.KullaniciID = ku.KullaniciID
                                 WHERE k.KiralamaID = @id";
                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@id", id)
                };
                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
                if (dt.Rows.Count > 0)
                    return MapDataRowToRental(dt.Rows[0]);
                else
                    throw new Exception("Kiralama bulunamadı.");
            }
            catch (Exception ex)
            {
                throw new Exception("Kiralama bilgisi alınırken bir hata oluştu: " + ex.Message, ex);
            }
        }

        public int Add(Rental entity)
        {
            try
            {
                string query = $@"INSERT INTO {TABLE_NAME} 
                                  (MusteriID, AracID, BaslangicTarihi, BitisTarihi, TeslimTarihi,
                                   BaslangicKm, BitisKm, KiralamaTutari, DepozitTutari, OdemeTipi,
                                   KiralamaNotuID, SozlesmeID, KullaniciID)
                                  VALUES 
                                  (@musteriId, @aracId, @baslangic, @bitis, @teslim,
                                   @basKm, @bitisKm, @tutar, @depozit, @odeme,
                                   @notId, @sozlesmeId, @kulId);
                                  SELECT LAST_INSERT_ID();";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@musteriId", entity.CustomerID),
                    DatabaseHelper.CreateParameter("@aracId", entity.VehicleID),
                    DatabaseHelper.CreateParameter("@baslangic", entity.StartDate),
                    DatabaseHelper.CreateParameter("@bitis", entity.EndDate),
                    DatabaseHelper.CreateParameter("@teslim", entity.ReturnDate ?? (object)DBNull.Value),
                    DatabaseHelper.CreateParameter("@basKm", entity.StartKm),
                    DatabaseHelper.CreateParameter("@bitisKm", entity.EndKm ?? (object)DBNull.Value),
                    DatabaseHelper.CreateParameter("@tutar", entity.RentalAmount),
                    DatabaseHelper.CreateParameter("@depozit", entity.DepositAmount ?? (object)DBNull.Value),
                    DatabaseHelper.CreateParameter("@odeme", entity.PaymentType),
                    DatabaseHelper.CreateParameter("@notId", entity.NoteID ?? (object)DBNull.Value),
                    DatabaseHelper.CreateParameter("@sozlesmeId", entity.ContractID ?? (object)DBNull.Value),
                    DatabaseHelper.CreateParameter("@kulId", entity.UserID)
                };

                object result = DatabaseHelper.ExecuteScalar(query, parameters);
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Kiralama eklenirken bir hata oluştu: " + ex.Message, ex);
            }
        }

        public void Update(Rental entity)
        {
            try
            {
                string query = $@"UPDATE {TABLE_NAME} SET 
                                  MusteriID = @musteriId,
                                  AracID = @aracId,
                                  BaslangicTarihi = @baslangic,
                                  BitisTarihi = @bitis,
                                  TeslimTarihi = @teslim,
                                  BaslangicKm = @basKm,
                                  BitisKm = @bitisKm,
                                  KiralamaTutari = @tutar,
                                  DepozitTutari = @depozit,
                                  OdemeTipi = @odeme,
                                  KiralamaNotuID = @notId,
                                  SozlesmeID = @sozlesmeId,
                                  KullaniciID = @kulId
                                  WHERE KiralamaID = @id";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@id", entity.RentalID),
                    DatabaseHelper.CreateParameter("@musteriId", entity.CustomerID),
                    DatabaseHelper.CreateParameter("@aracId", entity.VehicleID),
                    DatabaseHelper.CreateParameter("@baslangic", entity.StartDate),
                    DatabaseHelper.CreateParameter("@bitis", entity.EndDate),
                    DatabaseHelper.CreateParameter("@teslim", entity.ReturnDate ?? (object)DBNull.Value),
                    DatabaseHelper.CreateParameter("@basKm", entity.StartKm),
                    DatabaseHelper.CreateParameter("@bitisKm", entity.EndKm ?? (object)DBNull.Value),
                    DatabaseHelper.CreateParameter("@tutar", entity.RentalAmount),
                    DatabaseHelper.CreateParameter("@depozit", entity.DepositAmount ?? (object)DBNull.Value),
                    DatabaseHelper.CreateParameter("@odeme", entity.PaymentType),
                    DatabaseHelper.CreateParameter("@notId", entity.NoteID ?? (object)DBNull.Value),
                    DatabaseHelper.CreateParameter("@sozlesmeId", entity.ContractID ?? (object)DBNull.Value),
                    DatabaseHelper.CreateParameter("@kulId", entity.UserID)
                };

                DatabaseHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Kiralama güncellenirken bir hata oluştu: " + ex.Message, ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                // Araç durumunu vs. kontrol etmek isterseniz önce VehicleID alabilirsiniz.
                string query = $@"DELETE FROM {TABLE_NAME} 
                                  WHERE KiralamaID = @id";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@id", id)
                };

                DatabaseHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Kiralama silinirken bir hata oluştu: " + ex.Message, ex);
            }
        }

        public List<Rental> Search(string searchText)
        {
            // Örnek olarak müşteri adı/soyadı veya plaka üzerinden arama yapabilirsiniz.
            try
            {
                string query = $@"SELECT k.KiralamaID, k.MusteriID, 
                                        CONCAT(m.Ad, ' ', m.Soyad) AS MusteriAdSoyad,
                                        k.AracID, a.Plaka, a.Marka, a.Model,
                                        k.BaslangicTarihi, k.BitisTarihi, k.TeslimTarihi,
                                        k.BaslangicKm, k.BitisKm, k.KiralamaTutari, k.DepozitTutari, k.OdemeTipi,
                                        k.KiralamaNotuID, k.SozlesmeID, k.KullaniciID, 
                                        CONCAT(ku.Ad, ' ', ku.Soyad) AS KullaniciAdSoyad,
                                        k.OlusturmaTarihi, k.GuncellenmeTarihi
                                 FROM {TABLE_NAME} k
                                 JOIN Musteriler m ON k.MusteriID = m.MusteriID
                                 JOIN Araclar a ON k.AracID = a.AracID
                                 JOIN Kullanicilar ku ON k.KullaniciID = ku.KullaniciID
                                 WHERE m.Ad LIKE @search OR m.Soyad LIKE @search OR a.Plaka LIKE @search
                                 ORDER BY k.KiralamaID DESC";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@search", "%" + searchText + "%")
                };

                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
                return MapDataTableToRentals(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Kiralama arama sırasında bir hata oluştu: " + ex.Message, ex);
            }
        }

        private List<Rental> MapDataTableToRentals(DataTable dt)
        {
            var rentals = new List<Rental>();
            foreach (DataRow row in dt.Rows)
            {
                rentals.Add(MapDataRowToRental(row));
            }
            return rentals;
        }

        private Rental MapDataRowToRental(DataRow row)
        {
            return new Rental
            {
                RentalID = row.GetValue<int>("KiralamaID"),
                CustomerID = row.GetValue<int>("MusteriID"),
                CustomerFullName = row.GetValue<string>("MusteriAdSoyad"),
                VehicleID = row.GetValue<int>("AracID"),
                VehiclePlate = row.GetValue<string>("Plaka"),
                VehicleBrand = row.GetValue<string>("Marka"),
                VehicleModel = row.GetValue<string>("Model"),
                StartDate = row.GetValue<DateTime>("BaslangicTarihi"),
                EndDate = row.GetValue<DateTime>("BitisTarihi"),
                ReturnDate = row.GetValue<DateTime?>("TeslimTarihi"),
                StartKm = row.GetValue<int>("BaslangicKm"),
                EndKm = row.GetValue<int?>("BitisKm"),
                RentalAmount = row.GetValue<decimal>("KiralamaTutari"),
                DepositAmount = row.GetValue<decimal?>("DepozitTutari"),
                PaymentType = row.GetValue<string>("OdemeTipi"),
                NoteID = row.GetValue<int?>("KiralamaNotuID"),
                ContractID = row.GetValue<int?>("SozlesmeID"),
                UserID = row.GetValue<int>("KullaniciID"),
                UserFullName = row.GetValue<string>("KullaniciAdSoyad"),
                CreatedDate = row.GetValue<DateTime>("OlusturmaTarihi"),
                UpdatedDate = row.GetValue<DateTime?>("GuncellenmeTarihi")
            };
        }
    }
}
