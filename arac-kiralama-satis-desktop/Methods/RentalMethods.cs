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
                ErrorManager.Instance.HandleException(
                    ex,
                    "Kiralamalar listesini alma",
                    ErrorSeverity.Error,
                    ErrorSource.Business
                );
                return new List<Rental>();
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
                ErrorManager.Instance.HandleException(
                    ex,
                    $"Kiralama bilgisi alma (ID: {rentalId})",
                    ErrorSeverity.Error,
                    ErrorSource.Business
                );
                return null;
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
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Kiralama ekleme",
                    ErrorSeverity.Error,
                    ErrorSource.Business,
                    true
                );
                throw new InvalidOperationException($"Kiralama eklenemedi. Hata ID: {errorId}");
            }
        }

        public static void UpdateRental(Rental rental)
        {
            try
            {
                _repository.Update(rental);
                ErrorManager.Instance.LogInfo(
                    $"Kiralama güncellendi (ID: {rental.RentalID})",
                    "RentalMethods.UpdateRental"
                );
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Kiralama güncelleme (ID: {rental.RentalID})",
                    ErrorSeverity.Error,
                    ErrorSource.Business,
                    true
                );
                throw new InvalidOperationException($"Kiralama güncellenemedi. Hata ID: {errorId}");
            }
        }

        public static void DeleteRental(int rentalId)
        {
            try
            {
                _repository.Delete(rentalId);
                ErrorManager.Instance.LogInfo(
                    $"Kiralama silindi (ID: {rentalId})",
                    "RentalMethods.DeleteRental"
                );
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Kiralama silme (ID: {rentalId})",
                    ErrorSeverity.Error,
                    ErrorSource.Business,
                    true
                );
                throw new InvalidOperationException($"Kiralama silinemedi. Hata ID: {errorId}");
            }
        }

        public static void CompleteRental(int rentalId, int endKm, DateTime returnDate)
        {
            try
            {
                Rental rental = GetRentalById(rentalId);
                if (rental == null)
                {
                    throw new InvalidOperationException($"Kiralama bulunamadı (ID: {rentalId})");
                }

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

                VehicleMethods.UpdateVehicleStatus(rental.VehicleID, 1); 

                string updateVehicleQuery = "UPDATE Araclar SET Kilometre = @kilometre WHERE AracID = @aracId";
                var updateParams = new[]
                {
                    DatabaseHelper.CreateParameter("@aracId", rental.VehicleID),
                    DatabaseHelper.CreateParameter("@kilometre", endKm)
                };
                DatabaseHelper.ExecuteNonQuery(updateVehicleQuery, updateParams);

                ErrorManager.Instance.LogInfo(
                    $"Kiralama tamamlandı (ID: {rentalId}, Araç: {rental.VehiclePlate})",
                    "RentalMethods.CompleteRental"
                );
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Kiralama teslim alma (ID: {rentalId})",
                    ErrorSeverity.Error,
                    ErrorSource.Business,
                    true
                );
                throw new InvalidOperationException($"Kiralama teslim alınamadı. Hata ID: {errorId}");
            }
        }

        public static int GetVehicleKilometers(int vehicleId)
        {
            try
            {
                Vehicle vehicle = VehicleMethods.GetVehicleById(vehicleId);

                if (vehicle != null)
                {
                    ErrorManager.Instance.LogInfo($"Araç kilometre bilgisi alındı. Araç ID: {vehicleId}, Kilometre: {vehicle.Kilometers}",
                        "RentalMethods.GetVehicleKilometers");

                    return vehicle.Kilometers;
                }
                else
                {
                    ErrorManager.Instance.LogWarning($"Araç bulunamadı. Araç ID: {vehicleId}",
                        "RentalMethods.GetVehicleKilometers");
                    return 0;
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.HandleException(
                    ex,
                    $"Araç kilometresi alınırken hata oluştu. Araç ID: {vehicleId}",
                    ErrorSeverity.Warning,
                    ErrorSource.Business,
                    false);

                return 0;
            }
        }

        public static DataTable GetRentalsAsDataTable()
        {
            try
            {
                List<Rental> rentals = _repository.GetAll();
                DataTable dt = new DataTable();

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
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Kiralamalar DataTable'a dönüştürme",
                    ErrorSeverity.Error,
                    ErrorSource.Business
                );
                throw new InvalidOperationException($"Kiralamalar DataTable'a dönüştürülemedi. Hata ID: {errorId}");
            }
        }
    }
}