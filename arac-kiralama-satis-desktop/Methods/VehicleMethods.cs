using System;
using System.Collections.Generic;
using System.Data;
using arac_kiralama_satis_desktop.Models;
using arac_kiralama_satis_desktop.Utils;
using MySql.Data.MySqlClient;

namespace arac_kiralama_satis_desktop.Methods
{
    public class VehicleMethods
    {
        public static List<Vehicle> GetVehicles()
        {
            List<Vehicle> vehicles = new List<Vehicle>();

            try
            {
                string query = @"SELECT a.AracID, a.Plaka, a.Marka, a.Model, a.YapimYili, a.MotorNo, a.SasiNo, 
                               a.Renk, a.Kilometre, a.YakitTipi, a.VitesTipi, a.DurumID, d.DurumAdi,
                               a.AlinmaTarihi, a.AlisFiyati, a.SatisFiyati, a.SubeID, s.SubeAdi, 
                               a.AracSinifID, c.SinifAdi, a.OlusturmaTarihi, a.GuncellenmeTarihi
                               FROM Araclar a
                               LEFT JOIN AracDurumlari d ON a.DurumID = d.DurumID
                               LEFT JOIN Subeler s ON a.SubeID = s.SubeID
                               LEFT JOIN AracSiniflari c ON a.AracSinifID = c.AracSinifID
                               ORDER BY a.AracID DESC";

                DataTable result = DatabaseConnection.ExecuteQuery(query);

                foreach (DataRow row in result.Rows)
                {
                    Vehicle vehicle = new Vehicle
                    {
                        VehicleID = Convert.ToInt32(row["AracID"]),
                        Plate = row["Plaka"].ToString(),
                        Brand = row["Marka"].ToString(),
                        Model = row["Model"].ToString(),
                        Year = Convert.ToInt32(row["YapimYili"]),
                        EngineNo = row["MotorNo"] != DBNull.Value ? row["MotorNo"].ToString() : string.Empty,
                        ChassisNo = row["SasiNo"].ToString(),
                        Color = row["Renk"] != DBNull.Value ? row["Renk"].ToString() : string.Empty,
                        Kilometers = Convert.ToInt32(row["Kilometre"]),
                        FuelType = row["YakitTipi"].ToString(),
                        TransmissionType = row["VitesTipi"].ToString(),
                        StatusID = Convert.ToInt32(row["DurumID"]),
                        StatusName = row["DurumAdi"].ToString(),
                        PurchaseDate = row["AlinmaTarihi"] != DBNull.Value ? Convert.ToDateTime(row["AlinmaTarihi"]) : DateTime.MinValue,
                        PurchasePrice = row["AlisFiyati"] != DBNull.Value ? Convert.ToDecimal(row["AlisFiyati"]) : 0,
                        SalePrice = row["SatisFiyati"] != DBNull.Value ? Convert.ToDecimal(row["SatisFiyati"]) : null,
                        BranchID = row["SubeID"] != DBNull.Value ? Convert.ToInt32(row["SubeID"]) : null,
                        BranchName = row["SubeAdi"] != DBNull.Value ? row["SubeAdi"].ToString() : string.Empty,
                        VehicleClassID = row["AracSinifID"] != DBNull.Value ? Convert.ToInt32(row["AracSinifID"]) : null,
                        VehicleClassName = row["SinifAdi"] != DBNull.Value ? row["SinifAdi"].ToString() : string.Empty,
                        CreatedDate = Convert.ToDateTime(row["OlusturmaTarihi"]),
                        UpdatedDate = row["GuncellenmeTarihi"] != DBNull.Value ? Convert.ToDateTime(row["GuncellenmeTarihi"]) : null
                    };

                    vehicles.Add(vehicle);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Araçlar listelenirken bir hata oluştu: " + ex.Message);
            }

            return vehicles;
        }

        public static Vehicle GetVehicleById(int vehicleId)
        {
            try
            {
                string query = @"SELECT a.AracID, a.Plaka, a.Marka, a.Model, a.YapimYili, a.MotorNo, a.SasiNo, 
                               a.Renk, a.Kilometre, a.YakitTipi, a.VitesTipi, a.DurumID, d.DurumAdi,
                               a.AlinmaTarihi, a.AlisFiyati, a.SatisFiyati, a.SubeID, s.SubeAdi, 
                               a.AracSinifID, c.SinifAdi, a.OlusturmaTarihi, a.GuncellenmeTarihi
                               FROM Araclar a
                               LEFT JOIN AracDurumlari d ON a.DurumID = d.DurumID
                               LEFT JOIN Subeler s ON a.SubeID = s.SubeID
                               LEFT JOIN AracSiniflari c ON a.AracSinifID = c.AracSinifID
                               WHERE a.AracID = @aracId";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@aracId", vehicleId)
                };

                DataTable result = DatabaseConnection.ExecuteQuery(query, parameters);

                if (result.Rows.Count > 0)
                {
                    DataRow row = result.Rows[0];
                    Vehicle vehicle = new Vehicle
                    {
                        VehicleID = Convert.ToInt32(row["AracID"]),
                        Plate = row["Plaka"].ToString(),
                        Brand = row["Marka"].ToString(),
                        Model = row["Model"].ToString(),
                        Year = Convert.ToInt32(row["YapimYili"]),
                        EngineNo = row["MotorNo"] != DBNull.Value ? row["MotorNo"].ToString() : string.Empty,
                        ChassisNo = row["SasiNo"].ToString(),
                        Color = row["Renk"] != DBNull.Value ? row["Renk"].ToString() : string.Empty,
                        Kilometers = Convert.ToInt32(row["Kilometre"]),
                        FuelType = row["YakitTipi"].ToString(),
                        TransmissionType = row["VitesTipi"].ToString(),
                        StatusID = Convert.ToInt32(row["DurumID"]),
                        StatusName = row["DurumAdi"].ToString(),
                        PurchaseDate = row["AlinmaTarihi"] != DBNull.Value ? Convert.ToDateTime(row["AlinmaTarihi"]) : DateTime.MinValue,
                        PurchasePrice = row["AlisFiyati"] != DBNull.Value ? Convert.ToDecimal(row["AlisFiyati"]) : 0,
                        SalePrice = row["SatisFiyati"] != DBNull.Value ? Convert.ToDecimal(row["SatisFiyati"]) : null,
                        BranchID = row["SubeID"] != DBNull.Value ? Convert.ToInt32(row["SubeID"]) : null,
                        BranchName = row["SubeAdi"] != DBNull.Value ? row["SubeAdi"].ToString() : string.Empty,
                        VehicleClassID = row["AracSinifID"] != DBNull.Value ? Convert.ToInt32(row["AracSinifID"]) : null,
                        VehicleClassName = row["SinifAdi"] != DBNull.Value ? row["SinifAdi"].ToString() : string.Empty,
                        CreatedDate = Convert.ToDateTime(row["OlusturmaTarihi"]),
                        UpdatedDate = row["GuncellenmeTarihi"] != DBNull.Value ? Convert.ToDateTime(row["GuncellenmeTarihi"]) : null
                    };

                    return vehicle;
                }
                else
                {
                    throw new Exception("Araç bulunamadı.");
                }
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
                string query = @"INSERT INTO Araclar (Plaka, Marka, Model, YapimYili, MotorNo, SasiNo,
                               Renk, Kilometre, YakitTipi, VitesTipi, DurumID, AlinmaTarihi, 
                               AlisFiyati, SatisFiyati, SubeID, AracSinifID)
                               VALUES (@plaka, @marka, @model, @yapimYili, @motorNo, @sasiNo,
                               @renk, @kilometre, @yakitTipi, @vitesTipi, @durumId, @alinmaTarihi,
                               @alisFiyati, @satisFiyati, @subeId, @aracSinifId);
                               SELECT LAST_INSERT_ID();";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@plaka", vehicle.Plate),
                    new MySqlParameter("@marka", vehicle.Brand),
                    new MySqlParameter("@model", vehicle.Model),
                    new MySqlParameter("@yapimYili", vehicle.Year),
                    new MySqlParameter("@motorNo", vehicle.EngineNo != string.Empty ? (object)vehicle.EngineNo : DBNull.Value),
                    new MySqlParameter("@sasiNo", vehicle.ChassisNo),
                    new MySqlParameter("@renk", vehicle.Color != string.Empty ? (object)vehicle.Color : DBNull.Value),
                    new MySqlParameter("@kilometre", vehicle.Kilometers),
                    new MySqlParameter("@yakitTipi", vehicle.FuelType),
                    new MySqlParameter("@vitesTipi", vehicle.TransmissionType),
                    new MySqlParameter("@durumId", vehicle.StatusID),
                    new MySqlParameter("@alinmaTarihi", vehicle.PurchaseDate),
                    new MySqlParameter("@alisFiyati", vehicle.PurchasePrice),
                    new MySqlParameter("@satisFiyati", vehicle.SalePrice.HasValue ? (object)vehicle.SalePrice : DBNull.Value),
                    new MySqlParameter("@subeId", vehicle.BranchID.HasValue ? (object)vehicle.BranchID : DBNull.Value),
                    new MySqlParameter("@aracSinifId", vehicle.VehicleClassID.HasValue ? (object)vehicle.VehicleClassID : DBNull.Value)
                };

                object result = DatabaseConnection.ExecuteScalar(query, parameters);
                return Convert.ToInt32(result);
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
                string query = @"UPDATE Araclar SET 
                               Plaka = @plaka, 
                               Marka = @marka, 
                               Model = @model, 
                               YapimYili = @yapimYili, 
                               MotorNo = @motorNo, 
                               SasiNo = @sasiNo,
                               Renk = @renk, 
                               Kilometre = @kilometre, 
                               YakitTipi = @yakitTipi, 
                               VitesTipi = @vitesTipi, 
                               DurumID = @durumId, 
                               AlinmaTarihi = @alinmaTarihi,
                               AlisFiyati = @alisFiyati, 
                               SatisFiyati = @satisFiyati, 
                               SubeID = @subeId, 
                               AracSinifID = @aracSinifId
                               WHERE AracID = @aracId";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@aracId", vehicle.VehicleID),
                    new MySqlParameter("@plaka", vehicle.Plate),
                    new MySqlParameter("@marka", vehicle.Brand),
                    new MySqlParameter("@model", vehicle.Model),
                    new MySqlParameter("@yapimYili", vehicle.Year),
                    new MySqlParameter("@motorNo", vehicle.EngineNo != string.Empty ? (object)vehicle.EngineNo : DBNull.Value),
                    new MySqlParameter("@sasiNo", vehicle.ChassisNo),
                    new MySqlParameter("@renk", vehicle.Color != string.Empty ? (object)vehicle.Color : DBNull.Value),
                    new MySqlParameter("@kilometre", vehicle.Kilometers),
                    new MySqlParameter("@yakitTipi", vehicle.FuelType),
                    new MySqlParameter("@vitesTipi", vehicle.TransmissionType),
                    new MySqlParameter("@durumId", vehicle.StatusID),
                    new MySqlParameter("@alinmaTarihi", vehicle.PurchaseDate),
                    new MySqlParameter("@alisFiyati", vehicle.PurchasePrice),
                    new MySqlParameter("@satisFiyati", vehicle.SalePrice.HasValue ? (object)vehicle.SalePrice : DBNull.Value),
                    new MySqlParameter("@subeId", vehicle.BranchID.HasValue ? (object)vehicle.BranchID : DBNull.Value),
                    new MySqlParameter("@aracSinifId", vehicle.VehicleClassID.HasValue ? (object)vehicle.VehicleClassID : DBNull.Value)
                };

                DatabaseConnection.ExecuteNonQuery(query, parameters);
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

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@aracId", vehicleId),
                    new MySqlParameter("@durumId", statusId)
                };

                DatabaseConnection.ExecuteNonQuery(query, parameters);
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
                string query = "SELECT DurumID, DurumAdi, Aciklama FROM AracDurumlari ORDER BY DurumID";
                return DatabaseConnection.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Araç durumları listelenirken bir hata oluştu: " + ex.Message);
            }
        }

        public static DataTable GetVehicleClasses()
        {
            try
            {
                string query = "SELECT AracSinifID, SinifAdi, Aciklama, OrtalamaKiraFiyati FROM AracSiniflari ORDER BY AracSinifID";
                return DatabaseConnection.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Araç sınıfları listelenirken bir hata oluştu: " + ex.Message);
            }
        }

        public static List<Vehicle> GetAvailableVehiclesForRental()
        {
            List<Vehicle> vehicles = new List<Vehicle>();

            try
            {
                string query = @"SELECT a.AracID, a.Plaka, a.Marka, a.Model, a.YapimYili, a.Renk, a.Kilometre, 
                               a.YakitTipi, a.VitesTipi, a.SubeID, s.SubeAdi, a.AracSinifID, c.SinifAdi
                               FROM Araclar a
                               LEFT JOIN Subeler s ON a.SubeID = s.SubeID
                               LEFT JOIN AracSiniflari c ON a.AracSinifID = c.AracSinifID
                               WHERE a.DurumID = 1 OR a.DurumID = 4 
                               ORDER BY a.AracID DESC";

                DataTable result = DatabaseConnection.ExecuteQuery(query);

                foreach (DataRow row in result.Rows)
                {
                    Vehicle vehicle = new Vehicle
                    {
                        VehicleID = Convert.ToInt32(row["AracID"]),
                        Plate = row["Plaka"].ToString(),
                        Brand = row["Marka"].ToString(),
                        Model = row["Model"].ToString(),
                        Year = Convert.ToInt32(row["YapimYili"]),
                        Color = row["Renk"] != DBNull.Value ? row["Renk"].ToString() : string.Empty,
                        Kilometers = Convert.ToInt32(row["Kilometre"]),
                        FuelType = row["YakitTipi"].ToString(),
                        TransmissionType = row["VitesTipi"].ToString(),
                        BranchID = row["SubeID"] != DBNull.Value ? Convert.ToInt32(row["SubeID"]) : null,
                        BranchName = row["SubeAdi"] != DBNull.Value ? row["SubeAdi"].ToString() : string.Empty,
                        VehicleClassID = row["AracSinifID"] != DBNull.Value ? Convert.ToInt32(row["AracSinifID"]) : null,
                        VehicleClassName = row["SinifAdi"] != DBNull.Value ? row["SinifAdi"].ToString() : string.Empty
                    };

                    vehicles.Add(vehicle);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Müsait araçlar listelenirken bir hata oluştu: " + ex.Message);
            }

            return vehicles;
        }

        public static List<Vehicle> GetAvailableVehiclesForSale()
        {
            List<Vehicle> vehicles = new List<Vehicle>();

            try
            {
                string query = @"SELECT a.AracID, a.Plaka, a.Marka, a.Model, a.YapimYili, a.Renk, a.Kilometre, 
                               a.YakitTipi, a.VitesTipi, a.AlisFiyati, a.SatisFiyati, a.SubeID, s.SubeAdi
                               FROM Araclar a
                               LEFT JOIN Subeler s ON a.SubeID = s.SubeID
                               WHERE a.DurumID = 2 
                               ORDER BY a.AracID DESC";

                DataTable result = DatabaseConnection.ExecuteQuery(query);

                foreach (DataRow row in result.Rows)
                {
                    Vehicle vehicle = new Vehicle
                    {
                        VehicleID = Convert.ToInt32(row["AracID"]),
                        Plate = row["Plaka"].ToString(),
                        Brand = row["Marka"].ToString(),
                        Model = row["Model"].ToString(),
                        Year = Convert.ToInt32(row["YapimYili"]),
                        Color = row["Renk"] != DBNull.Value ? row["Renk"].ToString() : string.Empty,
                        Kilometers = Convert.ToInt32(row["Kilometre"]),
                        FuelType = row["YakitTipi"].ToString(),
                        TransmissionType = row["VitesTipi"].ToString(),
                        PurchasePrice = row["AlisFiyati"] != DBNull.Value ? Convert.ToDecimal(row["AlisFiyati"]) : 0,
                        SalePrice = row["SatisFiyati"] != DBNull.Value ? Convert.ToDecimal(row["SatisFiyati"]) : null,
                        BranchID = row["SubeID"] != DBNull.Value ? Convert.ToInt32(row["SubeID"]) : null,
                        BranchName = row["SubeAdi"] != DBNull.Value ? row["SubeAdi"].ToString() : string.Empty
                    };

                    vehicles.Add(vehicle);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Satılık araçlar listelenirken bir hata oluştu: " + ex.Message);
            }

            return vehicles;
        }
    }
}