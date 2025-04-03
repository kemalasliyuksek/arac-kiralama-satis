using System;
using System.Collections.Generic;
using System.Data;
using arac_kiralama_satis_desktop.Models;
using arac_kiralama_satis_desktop.Utils;
using MySql.Data.MySqlClient;

namespace arac_kiralama_satis_desktop.Methods
{
    public class RentalMethods
    {
        /// <summary>
        /// Tüm kiralamaları getirir
        /// </summary>
        public static List<Rental> GetRentals()
        {
            List<Rental> rentals = new List<Rental>();

            try
            {
                string query = @"SELECT k.KiralamaID, k.MusteriID, CONCAT(m.Ad, ' ', m.Soyad) as MusteriAdSoyad,
                               k.AracID, a.Plaka, a.Marka, a.Model,
                               k.BaslangicTarihi, k.BitisTarihi, k.TeslimTarihi,
                               k.BaslangicKm, k.BitisKm, k.KiralamaTutari, k.DepozitTutari, k.OdemeTipi,
                               k.KiralamaNotuID, k.SozlesmeID, k.KullaniciID, 
                               CONCAT(ku.Ad, ' ', ku.Soyad) as KullaniciAdSoyad,
                               k.OlusturmaTarihi, k.GuncellenmeTarihi
                               FROM Kiralamalar k
                               JOIN Musteriler m ON k.MusteriID = m.MusteriID
                               JOIN Araclar a ON k.AracID = a.AracID
                               JOIN Kullanicilar ku ON k.KullaniciID = ku.KullaniciID
                               ORDER BY k.KiralamaID DESC";

                DataTable result = DatabaseConnection.ExecuteQuery(query);

                foreach (DataRow row in result.Rows)
                {
                    Rental rental = new Rental
                    {
                        RentalID = Convert.ToInt32(row["KiralamaID"]),
                        CustomerID = Convert.ToInt32(row["MusteriID"]),
                        CustomerFullName = row["MusteriAdSoyad"].ToString(),
                        VehicleID = Convert.ToInt32(row["AracID"]),
                        VehiclePlate = row["Plaka"].ToString(),
                        VehicleBrand = row["Marka"].ToString(),
                        VehicleModel = row["Model"].ToString(),
                        StartDate = Convert.ToDateTime(row["BaslangicTarihi"]),
                        EndDate = Convert.ToDateTime(row["BitisTarihi"]),
                        ReturnDate = row["TeslimTarihi"] != DBNull.Value ? Convert.ToDateTime(row["TeslimTarihi"]) : (DateTime?)null,
                        StartKm = Convert.ToInt32(row["BaslangicKm"]),
                        EndKm = row["BitisKm"] != DBNull.Value ? Convert.ToInt32(row["BitisKm"]) : (int?)null,
                        RentalAmount = Convert.ToDecimal(row["KiralamaTutari"]),
                        DepositAmount = row["DepozitTutari"] != DBNull.Value ? Convert.ToDecimal(row["DepozitTutari"]) : (decimal?)null,
                        PaymentType = row["OdemeTipi"].ToString(),
                        NoteID = row["KiralamaNotuID"] != DBNull.Value ? Convert.ToInt32(row["KiralamaNotuID"]) : (int?)null,
                        ContractID = row["SozlesmeID"] != DBNull.Value ? Convert.ToInt32(row["SozlesmeID"]) : (int?)null,
                        UserID = Convert.ToInt32(row["KullaniciID"]),
                        UserFullName = row["KullaniciAdSoyad"].ToString(),
                        CreatedDate = Convert.ToDateTime(row["OlusturmaTarihi"]),
                        UpdatedDate = row["GuncellenmeTarihi"] != DBNull.Value ? Convert.ToDateTime(row["GuncellenmeTarihi"]) : (DateTime?)null
                    };

                    rentals.Add(rental);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Kiralamalar listelenirken bir hata oluştu: " + ex.Message);
            }

            return rentals;
        }

        /// <summary>
        /// Kiralama ID'sine göre kiralama bilgisini getirir
        /// </summary>
        public static Rental GetRentalById(int rentalId)
        {
            try
            {
                string query = @"SELECT k.KiralamaID, k.MusteriID, CONCAT(m.Ad, ' ', m.Soyad) as MusteriAdSoyad,
                               k.AracID, a.Plaka, a.Marka, a.Model,
                               k.BaslangicTarihi, k.BitisTarihi, k.TeslimTarihi,
                               k.BaslangicKm, k.BitisKm, k.KiralamaTutari, k.DepozitTutari, k.OdemeTipi,
                               k.KiralamaNotuID, k.SozlesmeID, k.KullaniciID, 
                               CONCAT(ku.Ad, ' ', ku.Soyad) as KullaniciAdSoyad,
                               k.OlusturmaTarihi, k.GuncellenmeTarihi
                               FROM Kiralamalar k
                               JOIN Musteriler m ON k.MusteriID = m.MusteriID
                               JOIN Araclar a ON k.AracID = a.AracID
                               JOIN Kullanicilar ku ON k.KullaniciID = ku.KullaniciID
                               WHERE k.KiralamaID = @kiralamaId";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@kiralamaId", rentalId)
                };

                DataTable result = DatabaseConnection.ExecuteQuery(query, parameters);

                if (result.Rows.Count > 0)
                {
                    DataRow row = result.Rows[0];
                    Rental rental = new Rental
                    {
                        RentalID = Convert.ToInt32(row["KiralamaID"]),
                        CustomerID = Convert.ToInt32(row["MusteriID"]),
                        CustomerFullName = row["MusteriAdSoyad"].ToString(),
                        VehicleID = Convert.ToInt32(row["AracID"]),
                        VehiclePlate = row["Plaka"].ToString(),
                        VehicleBrand = row["Marka"].ToString(),
                        VehicleModel = row["Model"].ToString(),
                        StartDate = Convert.ToDateTime(row["BaslangicTarihi"]),
                        EndDate = Convert.ToDateTime(row["BitisTarihi"]),
                        ReturnDate = row["TeslimTarihi"] != DBNull.Value ? Convert.ToDateTime(row["TeslimTarihi"]) : (DateTime?)null,
                        StartKm = Convert.ToInt32(row["BaslangicKm"]),
                        EndKm = row["BitisKm"] != DBNull.Value ? Convert.ToInt32(row["BitisKm"]) : (int?)null,
                        RentalAmount = Convert.ToDecimal(row["KiralamaTutari"]),
                        DepositAmount = row["DepozitTutari"] != DBNull.Value ? Convert.ToDecimal(row["DepozitTutari"]) : (decimal?)null,
                        PaymentType = row["OdemeTipi"].ToString(),
                        NoteID = row["KiralamaNotuID"] != DBNull.Value ? Convert.ToInt32(row["KiralamaNotuID"]) : (int?)null,
                        ContractID = row["SozlesmeID"] != DBNull.Value ? Convert.ToInt32(row["SozlesmeID"]) : (int?)null,
                        UserID = Convert.ToInt32(row["KullaniciID"]),
                        UserFullName = row["KullaniciAdSoyad"].ToString(),
                        CreatedDate = Convert.ToDateTime(row["OlusturmaTarihi"]),
                        UpdatedDate = row["GuncellenmeTarihi"] != DBNull.Value ? Convert.ToDateTime(row["GuncellenmeTarihi"]) : (DateTime?)null
                    };

                    return rental;
                }
                else
                {
                    throw new Exception("Kiralama bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Kiralama bilgisi alınırken bir hata oluştu: " + ex.Message);
            }
        }

        /// <summary>
        /// Yeni kiralama ekler
        /// </summary>
        public static int AddRental(Rental rental)
        {
            try
            {
                string query = @"INSERT INTO Kiralamalar (MusteriID, AracID, BaslangicTarihi, BitisTarihi, TeslimTarihi,
                               BaslangicKm, BitisKm, KiralamaTutari, DepozitTutari, OdemeTipi,
                               KiralamaNotuID, SozlesmeID, KullaniciID)
                               VALUES (@musteriId, @aracId, @baslangicTarihi, @bitisTarihi, @teslimTarihi,
                               @baslangicKm, @bitisKm, @kiralamaTutari, @depozitTutari, @odemeTipi,
                               @kiralamaNotuId, @sozlesmeId, @kullaniciId);
                               SELECT LAST_INSERT_ID();";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@musteriId", rental.CustomerID),
                    new MySqlParameter("@aracId", rental.VehicleID),
                    new MySqlParameter("@baslangicTarihi", rental.StartDate),
                    new MySqlParameter("@bitisTarihi", rental.EndDate),
                    new MySqlParameter("@teslimTarihi", rental.ReturnDate.HasValue ? (object)rental.ReturnDate.Value : DBNull.Value),
                    new MySqlParameter("@baslangicKm", rental.StartKm),
                    new MySqlParameter("@bitisKm", rental.EndKm.HasValue ? (object)rental.EndKm.Value : DBNull.Value),
                    new MySqlParameter("@kiralamaTutari", rental.RentalAmount),
                    new MySqlParameter("@depozitTutari", rental.DepositAmount.HasValue ? (object)rental.DepositAmount.Value : DBNull.Value),
                    new MySqlParameter("@odemeTipi", rental.PaymentType),
                    new MySqlParameter("@kiralamaNotuId", rental.NoteID.HasValue ? (object)rental.NoteID.Value : DBNull.Value),
                    new MySqlParameter("@sozlesmeId", rental.ContractID.HasValue ? (object)rental.ContractID.Value : DBNull.Value),
                    new MySqlParameter("@kullaniciId", rental.UserID)
                };

                object result = DatabaseConnection.ExecuteScalar(query, parameters);
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Kiralama eklenirken bir hata oluştu: " + ex.Message);
            }
        }

        /// <summary>
        /// Kiralama bilgilerini günceller
        /// </summary>
        public static void UpdateRental(Rental rental)
        {
            try
            {
                string query = @"UPDATE Kiralamalar SET 
                               MusteriID = @musteriId, 
                               AracID = @aracId, 
                               BaslangicTarihi = @baslangicTarihi, 
                               BitisTarihi = @bitisTarihi, 
                               TeslimTarihi = @teslimTarihi,
                               BaslangicKm = @baslangicKm, 
                               BitisKm = @bitisKm, 
                               KiralamaTutari = @kiralamaTutari, 
                               DepozitTutari = @depozitTutari, 
                               OdemeTipi = @odemeTipi,
                               KiralamaNotuID = @kiralamaNotuId, 
                               SozlesmeID = @sozlesmeId, 
                               KullaniciID = @kullaniciId
                               WHERE KiralamaID = @kiralamaId";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@kiralamaId", rental.RentalID),
                    new MySqlParameter("@musteriId", rental.CustomerID),
                    new MySqlParameter("@aracId", rental.VehicleID),
                    new MySqlParameter("@baslangicTarihi", rental.StartDate),
                    new MySqlParameter("@bitisTarihi", rental.EndDate),
                    new MySqlParameter("@teslimTarihi", rental.ReturnDate.HasValue ? (object)rental.ReturnDate.Value : DBNull.Value),
                    new MySqlParameter("@baslangicKm", rental.StartKm),
                    new MySqlParameter("@bitisKm", rental.EndKm.HasValue ? (object)rental.EndKm.Value : DBNull.Value),
                    new MySqlParameter("@kiralamaTutari", rental.RentalAmount),
                    new MySqlParameter("@depozitTutari", rental.DepositAmount.HasValue ? (object)rental.DepositAmount.Value : DBNull.Value),
                    new MySqlParameter("@odemeTipi", rental.PaymentType),
                    new MySqlParameter("@kiralamaNotuId", rental.NoteID.HasValue ? (object)rental.NoteID.Value : DBNull.Value),
                    new MySqlParameter("@sozlesmeId", rental.ContractID.HasValue ? (object)rental.ContractID.Value : DBNull.Value),
                    new MySqlParameter("@kullaniciId", rental.UserID)
                };

                DatabaseConnection.ExecuteNonQuery(query, parameters);

                // Eğer teslim edildiyse araç durumunu güncelle
                if (rental.ReturnDate.HasValue)
                {
                    // Araç durumunu müsait olarak güncelle (1: Müsait)
                    VehicleMethods.UpdateVehicleStatus(rental.VehicleID, 1);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Kiralama güncellenirken bir hata oluştu: " + ex.Message);
            }
        }

        /// <summary>
        /// Kiralama silme işlemi (genellikle kullanılmaz, yerine iptal veya güncelleme tercih edilir)
        /// </summary>
        public static void DeleteRental(int rentalId)
        {
            try
            {
                // Kiralama silmeden önce araç ID'sini al
                string getVehicleIdQuery = "SELECT AracID FROM Kiralamalar WHERE KiralamaID = @kiralamaId";
                MySqlParameter[] getVehicleIdParams = new MySqlParameter[]
                {
                    new MySqlParameter("@kiralamaId", rentalId)
                };

                object vehicleIdResult = DatabaseConnection.ExecuteScalar(getVehicleIdQuery, getVehicleIdParams);
                int vehicleId = Convert.ToInt32(vehicleIdResult);

                // Kiralama silme
                string query = "DELETE FROM Kiralamalar WHERE KiralamaID = @kiralamaId";
                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@kiralamaId", rentalId)
                };

                DatabaseConnection.ExecuteNonQuery(query, parameters);

                // Araç durumunu müsait olarak güncelle (1: Müsait)
                VehicleMethods.UpdateVehicleStatus(vehicleId, 1);
            }
            catch (Exception ex)
            {
                throw new Exception("Kiralama silinirken bir hata oluştu: " + ex.Message);
            }
        }

        /// <summary>
        /// Kiralama teslim alma işlemi
        /// </summary>
        public static void CompleteRental(int rentalId, int endKm, DateTime returnDate)
        {
            try
            {
                // Kiralama bilgilerini al
                Rental rental = GetRentalById(rentalId);

                // Teslim bilgilerini güncelle
                string query = @"UPDATE Kiralamalar SET 
                               TeslimTarihi = @teslimTarihi,
                               BitisKm = @bitisKm
                               WHERE KiralamaID = @kiralamaId";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@kiralamaId", rentalId),
                    new MySqlParameter("@teslimTarihi", returnDate),
                    new MySqlParameter("@bitisKm", endKm)
                };

                DatabaseConnection.ExecuteNonQuery(query, parameters);

                // Araç durumunu müsait olarak güncelle (1: Müsait)
                VehicleMethods.UpdateVehicleStatus(rental.VehicleID, 1);

                // Araç kilometresini güncelle
                string updateVehicleQuery = "UPDATE Araclar SET Kilometre = @kilometre WHERE AracID = @aracId";
                MySqlParameter[] updateVehicleParams = new MySqlParameter[]
                {
                    new MySqlParameter("@aracId", rental.VehicleID),
                    new MySqlParameter("@kilometre", endKm)
                };

                DatabaseConnection.ExecuteNonQuery(updateVehicleQuery, updateVehicleParams);
            }
            catch (Exception ex)
            {
                throw new Exception("Kiralama teslim alınırken bir hata oluştu: " + ex.Message);
            }
        }

        /// <summary>
        /// Aktif kiralamaları listeler
        /// </summary>
        public static List<Rental> GetActiveRentals()
        {
            List<Rental> activeRentals = new List<Rental>();

            try
            {
                string query = @"SELECT k.KiralamaID, k.MusteriID, CONCAT(m.Ad, ' ', m.Soyad) as MusteriAdSoyad,
                               k.AracID, a.Plaka, a.Marka, a.Model,
                               k.BaslangicTarihi, k.BitisTarihi, k.TeslimTarihi,
                               k.BaslangicKm, k.BitisKm, k.KiralamaTutari, k.DepozitTutari, k.OdemeTipi,
                               k.KiralamaNotuID, k.SozlesmeID, k.KullaniciID, 
                               CONCAT(ku.Ad, ' ', ku.Soyad) as KullaniciAdSoyad,
                               k.OlusturmaTarihi, k.GuncellenmeTarihi
                               FROM Kiralamalar k
                               JOIN Musteriler m ON k.MusteriID = m.MusteriID
                               JOIN Araclar a ON k.AracID = a.AracID
                               JOIN Kullanicilar ku ON k.KullaniciID = ku.KullaniciID
                               WHERE k.BitisTarihi >= CURRENT_DATE() AND k.TeslimTarihi IS NULL
                               ORDER BY k.BitisTarihi ASC";

                DataTable result = DatabaseConnection.ExecuteQuery(query);

                foreach (DataRow row in result.Rows)
                {
                    Rental rental = new Rental
                    {
                        RentalID = Convert.ToInt32(row["KiralamaID"]),
                        CustomerID = Convert.ToInt32(row["MusteriID"]),
                        CustomerFullName = row["MusteriAdSoyad"].ToString(),
                        VehicleID = Convert.ToInt32(row["AracID"]),
                        VehiclePlate = row["Plaka"].ToString(),
                        VehicleBrand = row["Marka"].ToString(),
                        VehicleModel = row["Model"].ToString(),
                        StartDate = Convert.ToDateTime(row["BaslangicTarihi"]),
                        EndDate = Convert.ToDateTime(row["BitisTarihi"]),
                        ReturnDate = row["TeslimTarihi"] != DBNull.Value ? Convert.ToDateTime(row["TeslimTarihi"]) : (DateTime?)null,
                        StartKm = Convert.ToInt32(row["BaslangicKm"]),
                        EndKm = row["BitisKm"] != DBNull.Value ? Convert.ToInt32(row["BitisKm"]) : (int?)null,
                        RentalAmount = Convert.ToDecimal(row["KiralamaTutari"]),
                        DepositAmount = row["DepozitTutari"] != DBNull.Value ? Convert.ToDecimal(row["DepozitTutari"]) : (decimal?)null,
                        PaymentType = row["OdemeTipi"].ToString(),
                        NoteID = row["KiralamaNotuID"] != DBNull.Value ? Convert.ToInt32(row["KiralamaNotuID"]) : (int?)null,
                        ContractID = row["SozlesmeID"] != DBNull.Value ? Convert.ToInt32(row["SozlesmeID"]) : (int?)null,
                        UserID = Convert.ToInt32(row["KullaniciID"]),
                        UserFullName = row["KullaniciAdSoyad"].ToString(),
                        CreatedDate = Convert.ToDateTime(row["OlusturmaTarihi"]),
                        UpdatedDate = row["GuncellenmeTarihi"] != DBNull.Value ? Convert.ToDateTime(row["GuncellenmeTarihi"]) : (DateTime?)null
                    };

                    activeRentals.Add(rental);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Aktif kiralamalar listelenirken bir hata oluştu: " + ex.Message);
            }

            return activeRentals;
        }

        /// <summary>
        /// Gecikmeli kiralamaları listeler
        /// </summary>
        public static List<Rental> GetOverdueRentals()
        {
            List<Rental> overdueRentals = new List<Rental>();

            try
            {
                string query = @"SELECT k.KiralamaID, k.MusteriID, CONCAT(m.Ad, ' ', m.Soyad) as MusteriAdSoyad,
                               k.AracID, a.Plaka, a.Marka, a.Model,
                               k.BaslangicTarihi, k.BitisTarihi, k.TeslimTarihi,
                               k.BaslangicKm, k.BitisKm, k.KiralamaTutari, k.DepozitTutari, k.OdemeTipi,
                               k.KiralamaNotuID, k.SozlesmeID, k.KullaniciID, 
                               CONCAT(ku.Ad, ' ', ku.Soyad) as KullaniciAdSoyad,
                               k.OlusturmaTarihi, k.GuncellenmeTarihi
                               FROM Kiralamalar k
                               JOIN Musteriler m ON k.MusteriID = m.MusteriID
                               JOIN Araclar a ON k.AracID = a.AracID
                               JOIN Kullanicilar ku ON k.KullaniciID = ku.KullaniciID
                               WHERE k.BitisTarihi < CURRENT_DATE() AND k.TeslimTarihi IS NULL
                               ORDER BY k.BitisTarihi ASC";

                DataTable result = DatabaseConnection.ExecuteQuery(query);

                foreach (DataRow row in result.Rows)
                {
                    Rental rental = new Rental
                    {
                        RentalID = Convert.ToInt32(row["KiralamaID"]),
                        CustomerID = Convert.ToInt32(row["MusteriID"]),
                        CustomerFullName = row["MusteriAdSoyad"].ToString(),
                        VehicleID = Convert.ToInt32(row["AracID"]),
                        VehiclePlate = row["Plaka"].ToString(),
                        VehicleBrand = row["Marka"].ToString(),
                        VehicleModel = row["Model"].ToString(),
                        StartDate = Convert.ToDateTime(row["BaslangicTarihi"]),
                        EndDate = Convert.ToDateTime(row["BitisTarihi"]),
                        ReturnDate = row["TeslimTarihi"] != DBNull.Value ? Convert.ToDateTime(row["TeslimTarihi"]) : (DateTime?)null,
                        StartKm = Convert.ToInt32(row["BaslangicKm"]),
                        EndKm = row["BitisKm"] != DBNull.Value ? Convert.ToInt32(row["BitisKm"]) : (int?)null,
                        RentalAmount = Convert.ToDecimal(row["KiralamaTutari"]),
                        DepositAmount = row["DepozitTutari"] != DBNull.Value ? Convert.ToDecimal(row["DepozitTutari"]) : (decimal?)null,
                        PaymentType = row["OdemeTipi"].ToString(),
                        NoteID = row["KiralamaNotuID"] != DBNull.Value ? Convert.ToInt32(row["KiralamaNotuID"]) : (int?)null,
                        ContractID = row["SozlesmeID"] != DBNull.Value ? Convert.ToInt32(row["SozlesmeID"]) : (int?)null,
                        UserID = Convert.ToInt32(row["KullaniciID"]),
                        UserFullName = row["KullaniciAdSoyad"].ToString(),
                        CreatedDate = Convert.ToDateTime(row["OlusturmaTarihi"]),
                        UpdatedDate = row["GuncellenmeTarihi"] != DBNull.Value ? Convert.ToDateTime(row["GuncellenmeTarihi"]) : (DateTime?)null
                    };

                    overdueRentals.Add(rental);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Gecikmeli kiralamalar listelenirken bir hata oluştu: " + ex.Message);
            }

            return overdueRentals;
        }
    }
}