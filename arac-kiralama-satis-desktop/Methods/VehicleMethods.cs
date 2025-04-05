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
                throw new Exception("Araçlar listelenirken bir hata oluştu: " + ex.Message);
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
                throw new Exception("Araç bilgisi alınırken bir hata oluştu: " + ex.Message);
            }
        }

        public static int AddVehicle(Vehicle vehicle)
        {
            try
            {
                return _repository.Add(vehicle);
            }
            catch (Exception ex)
            {
                throw new Exception("Araç eklenirken bir hata oluştu: " + ex.Message);
            }
        }

        public static void UpdateVehicle(Vehicle vehicle)
        {
            try
            {
                _repository.Update(vehicle);
            }
            catch (Exception ex)
            {
                throw new Exception("Araç güncellenirken bir hata oluştu: " + ex.Message);
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
            }
            catch (Exception ex)
            {
                throw new Exception("Araç durumu güncellenirken bir hata oluştu: " + ex.Message);
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
                throw new Exception("Araç durumları alınırken bir hata oluştu: " + ex.Message);
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
                throw new Exception("Araç sınıfları alınırken bir hata oluştu: " + ex.Message);
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
                throw new Exception("Kiralanabilir araçlar listelenirken bir hata oluştu: " + ex.Message);
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
                throw new Exception("Araçlar DataTable'a dönüştürülürken bir hata oluştu: " + ex.Message);
            }
        }
    }
}