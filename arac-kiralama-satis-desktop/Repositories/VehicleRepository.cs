using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using arac_kiralama_satis_desktop.Models;
using arac_kiralama_satis_desktop.Utils;

namespace arac_kiralama_satis_desktop.Repositories
{
    public class VehicleRepository : IRepository<Vehicle, int>
    {
        private const string TABLE_NAME = "Araclar";

        public List<Vehicle> GetAll()
        {
            try
            {
                string query = $@"SELECT a.AracID, a.Plaka, a.Marka, a.Model, a.YapimYili, a.MotorNo, a.SasiNo, 
                                         a.Renk, a.Kilometre, a.YakitTipi, a.VitesTipi, a.DurumID, d.DurumAdi,
                                         a.AlinmaTarihi, a.AlisFiyati, a.SatisFiyati, a.SubeID, s.SubeAdi, 
                                         a.AracSinifID, c.SinifAdi, a.OlusturmaTarihi, a.GuncellenmeTarihi
                                  FROM {TABLE_NAME} a
                                  LEFT JOIN AracDurumlari d ON a.DurumID = d.DurumID
                                  LEFT JOIN Subeler s ON a.SubeID = s.SubeID
                                  LEFT JOIN AracSiniflari c ON a.AracSinifID = c.AracSinifID
                                  ORDER BY a.AracID DESC";

                DataTable dt = DatabaseHelper.ExecuteQuery(query);
                return MapDataTableToVehicles(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Araçlar listelenirken bir hata oluştu: " + ex.Message, ex);
            }
        }

        public Vehicle GetById(int id)
        {
            try
            {
                string query = $@"SELECT a.AracID, a.Plaka, a.Marka, a.Model, a.YapimYili, a.MotorNo, a.SasiNo, 
                                         a.Renk, a.Kilometre, a.YakitTipi, a.VitesTipi, a.DurumID, d.DurumAdi,
                                         a.AlinmaTarihi, a.AlisFiyati, a.SatisFiyati, a.SubeID, s.SubeAdi, 
                                         a.AracSinifID, c.SinifAdi, a.OlusturmaTarihi, a.GuncellenmeTarihi
                                  FROM {TABLE_NAME} a
                                  LEFT JOIN AracDurumlari d ON a.DurumID = d.DurumID
                                  LEFT JOIN Subeler s ON a.SubeID = s.SubeID
                                  LEFT JOIN AracSiniflari c ON a.AracSinifID = c.AracSinifID
                                  WHERE a.AracID = @aracId";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@aracId", id)
                };

                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
                if (dt.Rows.Count > 0)
                    return MapDataRowToVehicle(dt.Rows[0]);
                else
                    throw new Exception("Araç bulunamadı.");
            }
            catch (Exception ex)
            {
                throw new Exception("Araç bilgisi alınırken bir hata oluştu: " + ex.Message, ex);
            }
        }

        public int Add(Vehicle entity)
        {
            try
            {
                string query = $@"INSERT INTO {TABLE_NAME}
                                  (Plaka, Marka, Model, YapimYili, MotorNo, SasiNo,
                                   Renk, Kilometre, YakitTipi, VitesTipi, DurumID, AlinmaTarihi,
                                   AlisFiyati, SatisFiyati, SubeID, AracSinifID)
                                  VALUES
                                  (@plaka, @marka, @model, @yapimYili, @motorNo, @sasiNo,
                                   @renk, @km, @yakit, @vites, @durum, @alinma,
                                   @alisFiyat, @satisFiyat, @subeId, @sinifId);
                                  SELECT LAST_INSERT_ID();";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@plaka", entity.Plate),
                    DatabaseHelper.CreateParameter("@marka", entity.Brand),
                    DatabaseHelper.CreateParameter("@model", entity.Model),
                    DatabaseHelper.CreateParameter("@yapimYili", entity.Year),
                    DatabaseHelper.CreateParameter("@motorNo", string.IsNullOrEmpty(entity.EngineNo) ? DBNull.Value : (object)entity.EngineNo),
                    DatabaseHelper.CreateParameter("@sasiNo", entity.ChassisNo),
                    DatabaseHelper.CreateParameter("@renk", string.IsNullOrEmpty(entity.Color) ? DBNull.Value : (object)entity.Color),
                    DatabaseHelper.CreateParameter("@km", entity.Kilometers),
                    DatabaseHelper.CreateParameter("@yakit", entity.FuelType),
                    DatabaseHelper.CreateParameter("@vites", entity.TransmissionType),
                    DatabaseHelper.CreateParameter("@durum", entity.StatusID),
                    DatabaseHelper.CreateParameter("@alinma", entity.PurchaseDate),
                    DatabaseHelper.CreateParameter("@alisFiyat", entity.PurchasePrice),
                    DatabaseHelper.CreateParameter("@satisFiyat", entity.SalePrice ?? (object)DBNull.Value),
                    DatabaseHelper.CreateParameter("@subeId", entity.BranchID ?? (object)DBNull.Value),
                    DatabaseHelper.CreateParameter("@sinifId", entity.VehicleClassID ?? (object)DBNull.Value)
                };

                object result = DatabaseHelper.ExecuteScalar(query, parameters);
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Araç eklenirken bir hata oluştu: " + ex.Message, ex);
            }
        }

        public void Update(Vehicle entity)
        {
            try
            {
                string query = $@"UPDATE {TABLE_NAME} SET
                                  Plaka = @plaka,
                                  Marka = @marka,
                                  Model = @model,
                                  YapimYili = @yapimYili,
                                  MotorNo = @motorNo,
                                  SasiNo = @sasiNo,
                                  Renk = @renk,
                                  Kilometre = @km,
                                  YakitTipi = @yakit,
                                  VitesTipi = @vites,
                                  DurumID = @durum,
                                  AlinmaTarihi = @alinma,
                                  AlisFiyati = @alisFiyat,
                                  SatisFiyati = @satisFiyat,
                                  SubeID = @subeId,
                                  AracSinifID = @sinifId
                                  WHERE AracID = @aracId";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@aracId", entity.VehicleID),
                    DatabaseHelper.CreateParameter("@plaka", entity.Plate),
                    DatabaseHelper.CreateParameter("@marka", entity.Brand),
                    DatabaseHelper.CreateParameter("@model", entity.Model),
                    DatabaseHelper.CreateParameter("@yapimYili", entity.Year),
                    DatabaseHelper.CreateParameter("@motorNo", string.IsNullOrEmpty(entity.EngineNo) ? DBNull.Value : (object)entity.EngineNo),
                    DatabaseHelper.CreateParameter("@sasiNo", entity.ChassisNo),
                    DatabaseHelper.CreateParameter("@renk", string.IsNullOrEmpty(entity.Color) ? DBNull.Value : (object)entity.Color),
                    DatabaseHelper.CreateParameter("@km", entity.Kilometers),
                    DatabaseHelper.CreateParameter("@yakit", entity.FuelType),
                    DatabaseHelper.CreateParameter("@vites", entity.TransmissionType),
                    DatabaseHelper.CreateParameter("@durum", entity.StatusID),
                    DatabaseHelper.CreateParameter("@alinma", entity.PurchaseDate),
                    DatabaseHelper.CreateParameter("@alisFiyat", entity.PurchasePrice),
                    DatabaseHelper.CreateParameter("@satisFiyat", entity.SalePrice ?? (object)DBNull.Value),
                    DatabaseHelper.CreateParameter("@subeId", entity.BranchID ?? (object)DBNull.Value),
                    DatabaseHelper.CreateParameter("@sinifId", entity.VehicleClassID ?? (object)DBNull.Value)
                };

                DatabaseHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Araç güncellenirken bir hata oluştu: " + ex.Message, ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                // İsterseniz soft-delete yapabilirsiniz.
                string query = $@"DELETE FROM {TABLE_NAME}
                                  WHERE AracID = @aracId";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@aracId", id)
                };

                DatabaseHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Araç silinirken bir hata oluştu: " + ex.Message, ex);
            }
        }

        public List<Vehicle> Search(string searchText)
        {
            try
            {
                string query = $@"SELECT a.AracID, a.Plaka, a.Marka, a.Model, a.YapimYili, a.MotorNo, a.SasiNo, 
                                         a.Renk, a.Kilometre, a.YakitTipi, a.VitesTipi, a.DurumID, d.DurumAdi,
                                         a.AlinmaTarihi, a.AlisFiyati, a.SatisFiyati, a.SubeID, s.SubeAdi, 
                                         a.AracSinifID, c.SinifAdi, a.OlusturmaTarihi, a.GuncellenmeTarihi
                                  FROM {TABLE_NAME} a
                                  LEFT JOIN AracDurumlari d ON a.DurumID = d.DurumID
                                  LEFT JOIN Subeler s ON a.SubeID = s.SubeID
                                  LEFT JOIN AracSiniflari c ON a.AracSinifID = c.AracSinifID
                                  WHERE a.Plaka LIKE @search OR a.Marka LIKE @search OR a.Model LIKE @search
                                  ORDER BY a.AracID DESC";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@search", "%" + searchText + "%")
                };

                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
                return MapDataTableToVehicles(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Araç arama sırasında bir hata oluştu: " + ex.Message, ex);
            }
        }

        private List<Vehicle> MapDataTableToVehicles(DataTable dt)
        {
            var vehicles = new List<Vehicle>();
            foreach (DataRow row in dt.Rows)
            {
                vehicles.Add(MapDataRowToVehicle(row));
            }
            return vehicles;
        }

        private Vehicle MapDataRowToVehicle(DataRow row)
        {
            return new Vehicle
            {
                VehicleID = row.GetValue<int>("AracID"),
                Plate = row.GetValue<string>("Plaka"),
                Brand = row.GetValue<string>("Marka"),
                Model = row.GetValue<string>("Model"),
                Year = row.GetValue<int>("YapimYili"),
                EngineNo = row.GetValue<string>("MotorNo"),
                ChassisNo = row.GetValue<string>("SasiNo"),
                Color = row.GetValue<string>("Renk"),
                Kilometers = row.GetValue<int>("Kilometre"),
                FuelType = row.GetValue<string>("YakitTipi"),
                TransmissionType = row.GetValue<string>("VitesTipi"),
                StatusID = row.GetValue<int>("DurumID"),
                StatusName = row.GetValue<string>("DurumAdi"),
                PurchaseDate = row.GetValue<DateTime>("AlinmaTarihi"),
                PurchasePrice = row.GetValue<decimal>("AlisFiyati"),
                SalePrice = row.GetValue<decimal?>("SatisFiyati"),
                BranchID = row.GetValue<int?>("SubeID"),
                BranchName = row.GetValue<string>("SubeAdi"),
                VehicleClassID = row.GetValue<int?>("AracSinifID"),
                VehicleClassName = row.GetValue<string>("SinifAdi"),
                CreatedDate = row.GetValue<DateTime>("OlusturmaTarihi"),
                UpdatedDate = row.GetValue<DateTime?>("GuncellenmeTarihi")
            };
        }
    }
}
