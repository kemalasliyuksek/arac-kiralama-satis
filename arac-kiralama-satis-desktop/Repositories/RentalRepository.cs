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

                ErrorManager.Instance.LogInfo("Tüm kiralama kayıtları getiriliyor", "RentalRepository.GetAll");
                DataTable dt = DatabaseHelper.ExecuteQuery(query);
                List<Rental> rentals = MapDataTableToRentals(dt);
                ErrorManager.Instance.LogInfo($"{rentals.Count} kiralama kaydı başarıyla listelendi", "RentalRepository.GetAll");

                return rentals;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Kiralamalar listelenirken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Kiralamalar listelenirken bir hata oluştu. (Hata ID: {errorId})", ex);
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

                ErrorManager.Instance.LogInfo($"Kiralama ID: {id} ile aranıyor", "RentalRepository.GetById");
                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    Rental rental = MapDataRowToRental(dt.Rows[0]);
                    ErrorManager.Instance.LogInfo($"Kiralama bulundu: ID: {rental.RentalID}, Müşteri: {rental.CustomerFullName}, Araç: {rental.VehiclePlate}", "RentalRepository.GetById");
                    return rental;
                }
                else
                {
                    ErrorManager.Instance.LogWarning($"Kiralama bulunamadı. ID: {id}", "RentalRepository.GetById");
                    throw new Exception($"ID: {id} ile kiralama bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Kiralama bilgisi alınırken bir hata oluştu (ID: {id})",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Kiralama bilgisi alınırken bir hata oluştu. (Hata ID: {errorId})", ex);
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

                int days = (entity.EndDate - entity.StartDate).Days + 1;
                ErrorManager.Instance.LogInfo($"Yeni kiralama ekleniyor: Müşteri ID: {entity.CustomerID}, Araç ID: {entity.VehicleID}, " +
                    $"Süre: {days} gün ({entity.StartDate:dd.MM.yyyy} - {entity.EndDate:dd.MM.yyyy}), " +
                    $"Tutar: {entity.RentalAmount:C2}", "RentalRepository.Add");

                object result = DatabaseHelper.ExecuteScalar(query, parameters);
                int newId = Convert.ToInt32(result);

                ErrorManager.Instance.LogInfo($"Yeni kiralama başarıyla eklendi. ID: {newId}, Müşteri ID: {entity.CustomerID}, " +
                    $"Araç ID: {entity.VehicleID}, Tutar: {entity.RentalAmount:C2}", "RentalRepository.Add");

                return newId;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Kiralama eklenirken bir hata oluştu: Müşteri ID: {entity.CustomerID}, Araç ID: {entity.VehicleID}",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Kiralama eklenirken bir hata oluştu. (Hata ID: {errorId})", ex);
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
                                  KullaniciID = @kulId,
                                  GuncellenmeTarihi = NOW()
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

                string teslimDurumu = entity.ReturnDate.HasValue ?
                    $"Teslim edildi: {entity.ReturnDate.Value:dd.MM.yyyy}" : "Teslim edilmedi";

                ErrorManager.Instance.LogInfo($"Kiralama güncelleniyor. ID: {entity.RentalID}, Müşteri: {entity.CustomerFullName}, " +
                    $"Araç: {entity.VehiclePlate}, {teslimDurumu}", "RentalRepository.Update");

                int affectedRows = DatabaseHelper.ExecuteNonQuery(query, parameters);

                if (affectedRows > 0)
                {
                    ErrorManager.Instance.LogInfo($"Kiralama başarıyla güncellendi. ID: {entity.RentalID}, Müşteri: {entity.CustomerFullName}", "RentalRepository.Update");
                }
                else
                {
                    ErrorManager.Instance.LogWarning($"Kiralama güncellenemedi, kayıt bulunamadı. ID: {entity.RentalID}", "RentalRepository.Update");
                }
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Kiralama güncellenirken bir hata oluştu. ID: {entity.RentalID}, Müşteri: {entity.CustomerFullName}",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Kiralama güncellenirken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                Rental rental = null;
                try
                {
                    rental = GetById(id);
                    ErrorManager.Instance.LogInfo($"Kiralama silme işlemi için veri alındı. ID: {id}, Müşteri: {rental.CustomerFullName}, Araç: {rental.VehiclePlate}", "RentalRepository.Delete");
                }
                catch
                {
                    ErrorManager.Instance.LogWarning($"Silinecek kiralama kaydı bulunamadı. ID: {id}", "RentalRepository.Delete");
                }

                string query = $@"DELETE FROM {TABLE_NAME} 
                                  WHERE KiralamaID = @id";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@id", id)
                };

                ErrorManager.Instance.LogInfo($"Kiralama siliniyor. ID: {id}", "RentalRepository.Delete");
                int affectedRows = DatabaseHelper.ExecuteNonQuery(query, parameters);

                if (affectedRows > 0)
                {
                    string rentalInfo = rental != null ?
                        $", Müşteri: {rental.CustomerFullName}, Araç: {rental.VehiclePlate}" : "";

                    ErrorManager.Instance.LogInfo($"Kiralama başarıyla silindi. ID: {id}{rentalInfo}", "RentalRepository.Delete");
                }
                else
                {
                    ErrorManager.Instance.LogWarning($"Kiralama silinemedi, kayıt bulunamadı. ID: {id}", "RentalRepository.Delete");
                }
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Kiralama silinirken bir hata oluştu. ID: {id}",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Kiralama silinirken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        public List<Rental> Search(string searchText)
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
                                 WHERE m.Ad LIKE @search OR m.Soyad LIKE @search OR a.Plaka LIKE @search
                                 ORDER BY k.KiralamaID DESC";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@search", "%" + searchText + "%")
                };

                ErrorManager.Instance.LogInfo($"Kiralama arama yapılıyor. Arama metni: '{searchText}'", "RentalRepository.Search");
                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
                List<Rental> rentals = MapDataTableToRentals(dt);

                ErrorManager.Instance.LogInfo($"Kiralama araması tamamlandı. '{searchText}' için {rentals.Count} sonuç bulundu", "RentalRepository.Search");
                return rentals;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Kiralama arama sırasında bir hata oluştu. Arama metni: '{searchText}'",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Kiralama arama sırasında bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        public List<Rental> GetOverdueRentals()
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
                                 WHERE BitisTarihi < CURRENT_DATE() 
                                 AND TeslimTarihi IS NULL
                                 ORDER BY BitisTarihi ASC";

                ErrorManager.Instance.LogInfo("Gecikmiş kiralamalar getiriliyor", "RentalRepository.GetOverdueRentals");
                DataTable dt = DatabaseHelper.ExecuteQuery(query);
                List<Rental> rentals = MapDataTableToRentals(dt);

                ErrorManager.Instance.LogInfo($"{rentals.Count} gecikmiş kiralama kaydı bulundu", "RentalRepository.GetOverdueRentals");
                return rentals;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Gecikmiş kiralamalar listelenirken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Gecikmiş kiralamalar listelenirken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        public List<Rental> GetActiveRentals()
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
                                 WHERE BitisTarihi >= CURRENT_DATE() 
                                 AND BaslangicTarihi <= CURRENT_DATE()
                                 AND TeslimTarihi IS NULL
                                 ORDER BY BitisTarihi ASC";

                ErrorManager.Instance.LogInfo("Aktif kiralamalar getiriliyor", "RentalRepository.GetActiveRentals");
                DataTable dt = DatabaseHelper.ExecuteQuery(query);
                List<Rental> rentals = MapDataTableToRentals(dt);

                ErrorManager.Instance.LogInfo($"{rentals.Count} aktif kiralama kaydı bulundu", "RentalRepository.GetActiveRentals");
                return rentals;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Aktif kiralamalar listelenirken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Aktif kiralamalar listelenirken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        public void ReturnVehicle(int rentalId, int endKm, DateTime returnDate)
        {
            try
            {
                string query = $@"UPDATE {TABLE_NAME} SET 
                                  TeslimTarihi = @teslimTarihi,
                                  BitisKm = @bitisKm,
                                  GuncellenmeTarihi = NOW()
                                  WHERE KiralamaID = @id";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@id", rentalId),
                    DatabaseHelper.CreateParameter("@teslimTarihi", returnDate),
                    DatabaseHelper.CreateParameter("@bitisKm", endKm)
                };

                ErrorManager.Instance.LogInfo($"Araç teslim alınıyor. Kiralama ID: {rentalId}, Teslim Tarihi: {returnDate:dd.MM.yyyy}, Bitiş Km: {endKm}", "RentalRepository.ReturnVehicle");
                int affectedRows = DatabaseHelper.ExecuteNonQuery(query, parameters);

                if (affectedRows > 0)
                {
                    ErrorManager.Instance.LogInfo($"Araç başarıyla teslim alındı. Kiralama ID: {rentalId}, Teslim Tarihi: {returnDate:dd.MM.yyyy}, Bitiş Km: {endKm}", "RentalRepository.ReturnVehicle");
                }
                else
                {
                    ErrorManager.Instance.LogWarning($"Araç teslim işlemi yapılamadı, kiralama kaydı bulunamadı. ID: {rentalId}", "RentalRepository.ReturnVehicle");
                    throw new Exception($"Kiralama kaydı bulunamadı. ID: {rentalId}");
                }
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Araç teslim işlemi sırasında bir hata oluştu. Kiralama ID: {rentalId}",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Araç teslim işlemi sırasında bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        private List<Rental> MapDataTableToRentals(DataTable dt)
        {
            try
            {
                var rentals = new List<Rental>();
                foreach (DataRow row in dt.Rows)
                {
                    rentals.Add(MapDataRowToRental(row));
                }
                return rentals;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Kiralama verileri dönüştürülürken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Business);

                throw new Exception($"Kiralama verileri dönüştürülürken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        private Rental MapDataRowToRental(DataRow row)
        {
            try
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
            catch (Exception ex)
            {
                int rentalId = 0;
                try { rentalId = row.GetValue<int>("KiralamaID"); } catch { }

                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Kiralama verisi dönüştürme sırasında hata oluştu (KiralamaID: {rentalId})",
                    ErrorSeverity.Error,
                    ErrorSource.Business);

                throw new Exception($"Kiralama verisi dönüştürülürken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }
    }
}