using System;
using System.Collections.Generic;
using System.Data;
using arac_kiralama_satis_desktop.Models;
using arac_kiralama_satis_desktop.Repositories;
using arac_kiralama_satis_desktop.Utils;

namespace arac_kiralama_satis_desktop.Methods
{
    public class RentalMethods
    {
        private static readonly RentalRepository _repository = new RentalRepository();

        public static List<Rental> GetRentals()
        {
            try
            {
                return _repository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Kiralamalar listelenirken bir hata oluştu: " + ex.Message);
            }
        }

        public static Rental GetRentalById(int rentalId)
        {
            try
            {
                return _repository.GetById(rentalId);
            }
            catch (Exception ex)
            {
                throw new Exception("Kiralama bilgisi alınırken bir hata oluştu: " + ex.Message);
            }
        }

        public static int AddRental(Rental rental)
        {
            try
            {
                return _repository.Add(rental);
            }
            catch (Exception ex)
            {
                throw new Exception("Kiralama eklenirken bir hata oluştu: " + ex.Message);
            }
        }

        public static void UpdateRental(Rental rental)
        {
            try
            {
                _repository.Update(rental);
            }
            catch (Exception ex)
            {
                throw new Exception("Kiralama güncellenirken bir hata oluştu: " + ex.Message);
            }
        }

        public static void DeleteRental(int rentalId)
        {
            try
            {
                _repository.Delete(rentalId);
            }
            catch (Exception ex)
            {
                throw new Exception("Kiralama silinirken bir hata oluştu: " + ex.Message);
            }
        }

        public static void CompleteRental(int rentalId, int endKm, DateTime returnDate)
        {
            try
            {
                // Özel teslim alma işlemi için:
                Rental rental = GetRentalById(rentalId);

                string query = @"UPDATE Kiralamalar SET 
                                   TeslimTarihi = @teslimTarihi,
                                   BitisKm = @bitisKm
                                   WHERE KiralamaID = @kiralamaId";
                var parameters = new[]
                {
                    DatabaseHelper.CreateParameter("@kiralamaId", rentalId),
                    DatabaseHelper.CreateParameter("@teslimTarihi", returnDate),
                    DatabaseHelper.CreateParameter("@bitisKm", endKm)
                };
                DatabaseHelper.ExecuteNonQuery(query, parameters);

                // Teslim alındığında araç durumunu güncelle
                VehicleMethods.UpdateVehicleStatus(rental.VehicleID, 1); // 1: Müsait durumu

                // Araç kilometresini güncelle
                string updateVehicleQuery = "UPDATE Araclar SET Kilometre = @kilometre WHERE AracID = @aracId";
                var updateParams = new[]
                {
                    DatabaseHelper.CreateParameter("@aracId", rental.VehicleID),
                    DatabaseHelper.CreateParameter("@kilometre", endKm)
                };
                DatabaseHelper.ExecuteNonQuery(updateVehicleQuery, updateParams);
            }
            catch (Exception ex)
            {
                throw new Exception("Kiralama teslim alınırken bir hata oluştu: " + ex.Message);
            }
        }

        // DataTable dönüşümü için yardımcı metot
        public static DataTable GetRentalsAsDataTable()
        {
            try
            {
                List<Rental> rentals = _repository.GetAll();
                DataTable dt = new DataTable();

                // DataTable sütunlarını oluştur
                dt.Columns.Add("KiralamaID", typeof(int));
                dt.Columns.Add("MusteriAdSoyad", typeof(string));
                dt.Columns.Add("Plaka", typeof(string));
                dt.Columns.Add("Marka", typeof(string));
                dt.Columns.Add("Model", typeof(string));
                dt.Columns.Add("BaslangicTarihi", typeof(DateTime));
                dt.Columns.Add("BitisTarihi", typeof(DateTime));
                dt.Columns.Add("TeslimTarihi", typeof(DateTime));
                dt.Columns.Add("KiralamaTutari", typeof(decimal));
                dt.Columns.Add("OdemeTipi", typeof(string));

                // Kiralamaları DataTable'a ekle
                foreach (var rental in rentals)
                {
                    dt.Rows.Add(
                        rental.RentalID,
                        rental.CustomerFullName,
                        rental.VehiclePlate,
                        rental.VehicleBrand,
                        rental.VehicleModel,
                        rental.StartDate,
                        rental.EndDate,
                        rental.ReturnDate,
                        rental.RentalAmount,
                        rental.PaymentType
                    );
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Kiralamalar DataTable'a dönüştürülürken bir hata oluştu: " + ex.Message);
            }
        }
    }
}