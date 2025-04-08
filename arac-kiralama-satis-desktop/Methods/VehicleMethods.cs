using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using arac_kiralama_satis_desktop.Models;
using arac_kiralama_satis_desktop.Repositories;
using arac_kiralama_satis_desktop.Utils;

namespace arac_kiralama_satis_desktop.Methods
{
    public class VehicleMethods
    {
        private static readonly VehicleRepository _repository = new VehicleRepository();

        public static List<Vehicle> GetVehicles()
        {
            try
            {
                return _repository.GetAll();
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Araçları listeleme",
                    ErrorSeverity.Error,
                    ErrorSource.Business
                );
                return new List<Vehicle>();
            }
        }

        public static Vehicle GetVehicleById(int vehicleId)
        {
            try
            {
                return _repository.GetById(vehicleId);
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Araç bilgisi alma (ID: {vehicleId})",
                    ErrorSeverity.Error,
                    ErrorSource.Business
                );
                return null;
            }
        }

        public static int AddVehicle(Vehicle vehicle)
        {
            try
            {
                int vehicleId = _repository.Add(vehicle);

                // Log successful vehicle addition
                ErrorManager.Instance.LogInfo(
                    $"Araç eklendi (ID: {vehicleId}, Plaka: {vehicle.Plate})",
                    "VehicleMethods.AddVehicle"
                );

                return vehicleId;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Araç ekleme",
                    ErrorSeverity.Error,
                    ErrorSource.Business,
                    true
                );
                throw new InvalidOperationException($"Araç eklenemedi. Hata ID: {errorId}");
            }
        }

        public static void UpdateVehicle(Vehicle vehicle)
        {
            try
            {
                _repository.Update(vehicle);

                // Log successful vehicle update
                ErrorManager.Instance.LogInfo(
                    $"Araç güncellendi (ID: {vehicle.VehicleID}, Plaka: {vehicle.Plate})",
                    "VehicleMethods.UpdateVehicle"
                );
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Araç güncelleme (ID: {vehicle.VehicleID})",
                    ErrorSeverity.Error,
                    ErrorSource.Business,
                    true
                );
                throw new InvalidOperationException($"Araç güncellenemedi. Hata ID: {errorId}");
            }
        }

        public static void UpdateVehicleStatus(int vehicleId, int statusId)
        {
            try
            {
                string query = "UPDATE Araclar SET DurumID = @durumId WHERE AracID = @aracId";
                var parameters = new[]
                {
                    DatabaseHelper.CreateParameter("@aracId", vehicleId),
                    DatabaseHelper.CreateParameter("@durumId", statusId)
                };
                DatabaseHelper.ExecuteNonQuery(query, parameters);

                // Log status change
                ErrorManager.Instance.LogInfo(
                    $"Araç durumu değiştirildi (Araç ID: {vehicleId}, Durum ID: {statusId})",
                    "VehicleMethods.UpdateVehicleStatus"
                );
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Araç durumu güncelleme (Araç ID: {vehicleId})",
                    ErrorSeverity.Error,
                    ErrorSource.Database,
                    true
                );
                throw new InvalidOperationException($"Araç durumu güncellenemedi. Hata ID: {errorId}");
            }
        }

        public static DataTable GetVehicleStatuses()
        {
            try
            {
                string query = "SELECT DurumID, DurumAdi FROM AracDurumlari ORDER BY DurumID";
                return DatabaseHelper.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Araç durumları alma",
                    ErrorSeverity.Error,
                    ErrorSource.Database
                );
                throw new InvalidOperationException($"Araç durumları alınamadı. Hata ID: {errorId}");
            }
        }

        public static DataTable GetVehicleClasses()
        {
            try
            {
                string query = "SELECT AracSinifID, SinifAdi FROM AracSiniflari ORDER BY SinifAdi";
                return DatabaseHelper.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Araç sınıfları alma",
                    ErrorSeverity.Error,
                    ErrorSource.Database
                );
                throw new InvalidOperationException($"Araç sınıfları alınamadı. Hata ID: {errorId}");
            }
        }

        public static List<Vehicle> GetAvailableVehiclesForRental()
        {
            try
            {
                return _repository.GetAll().Where(v => v.StatusID == 1).ToList(); // DurumID = 1: Müsait
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Kiralanabilir araçları listeleme",
                    ErrorSeverity.Error,
                    ErrorSource.Business
                );
                return new List<Vehicle>();
            }
        }

        // DataTable dönüşümü için yardımcı metot
        public static DataTable GetVehiclesAsDataTable()
        {
            try
            {
                List<Vehicle> vehicles = _repository.GetAll();
                DataTable dt = new DataTable();

                // DataTable sütunlarını oluştur
                dt.Columns.Add("AracID", typeof(int));
                dt.Columns.Add("Plaka", typeof(string));
                dt.Columns.Add("Marka", typeof(string));
                dt.Columns.Add("Model", typeof(string));
                dt.Columns.Add("Yıl", typeof(int));
                dt.Columns.Add("Renk", typeof(string));
                dt.Columns.Add("Kilometre", typeof(int));
                dt.Columns.Add("YakitTipi", typeof(string));
                dt.Columns.Add("VitesTipi", typeof(string));
                dt.Columns.Add("Durum", typeof(string));
                dt.Columns.Add("Şube", typeof(string));
                dt.Columns.Add("Sınıf", typeof(string));

                // Araçları DataTable'a ekle
                foreach (var vehicle in vehicles)
                {
                    dt.Rows.Add(
                        vehicle.VehicleID,
                        vehicle.Plate,
                        vehicle.Brand,
                        vehicle.Model,
                        vehicle.Year,
                        vehicle.Color,
                        vehicle.Kilometers,
                        vehicle.FuelType,
                        vehicle.TransmissionType,
                        vehicle.StatusName,
                        vehicle.BranchName,
                        vehicle.VehicleClassName
                    );
                }

                return dt;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Araçları DataTable'a dönüştürme",
                    ErrorSeverity.Error,
                    ErrorSource.Business
                );
                throw new InvalidOperationException($"Araçlar DataTable'a dönüştürülemedi. Hata ID: {errorId}");
            }
        }
    }
}