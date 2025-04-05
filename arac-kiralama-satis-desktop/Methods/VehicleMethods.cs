using System;
using System.Collections.Generic;
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

        // Diğer özel metotlar (örneğin, GetAvailableVehiclesForRental) proje gereksinimlerine göre düzenlenebilir.
    }
}
