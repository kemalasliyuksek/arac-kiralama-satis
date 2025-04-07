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

                ErrorManager.Instance.LogInfo("Tüm araç kayıtları getiriliyor", "VehicleRepository.GetAll");
                DataTable dt = DatabaseHelper.ExecuteQuery(query);
                List<Vehicle> vehicles = MapDataTableToVehicles(dt);
                ErrorManager.Instance.LogInfo($"{vehicles.Count} araç kaydı başarıyla listelendi", "VehicleRepository.GetAll");

                return vehicles;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Araçlar listelenirken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Araçlar listelenirken bir hata oluştu. (Hata ID: {errorId})", ex);
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

                ErrorManager.Instance.LogInfo($"Araç ID: {id} ile aranıyor", "VehicleRepository.GetById");
                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    Vehicle vehicle = MapDataRowToVehicle(dt.Rows[0]);
                    ErrorManager.Instance.LogInfo($"Araç bulundu: ID: {vehicle.VehicleID}, Plaka: {vehicle.Plate}, Marka/Model: {vehicle.Brand} {vehicle.Model}", "VehicleRepository.GetById");
                    return vehicle;
                }
                else
                {
                    ErrorManager.Instance.LogWarning($"Araç bulunamadı. ID: {id}", "VehicleRepository.GetById");
                    throw new Exception($"ID: {id} ile araç bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Araç bilgisi alınırken bir hata oluştu (ID: {id})",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Araç bilgisi alınırken bir hata oluştu. (Hata ID: {errorId})", ex);
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

                string durumBilgisi = GetStatusDescription(entity.StatusID);
                string subeBilgisi = entity.BranchID.HasValue ? $", Şube: {entity.BranchName}" : "";
                string sinifBilgisi = entity.VehicleClassID.HasValue ? $", Sınıf: {entity.VehicleClassName}" : "";

                ErrorManager.Instance.LogInfo($"Yeni araç ekleniyor: Plaka: {entity.Plate}, Marka/Model: {entity.Brand} {entity.Model}, " +
                    $"Yıl: {entity.Year}, Durum: {durumBilgisi}{subeBilgisi}{sinifBilgisi}, " +
                    $"Alış Fiyatı: {entity.PurchasePrice:C2}", "VehicleRepository.Add");

                object result = DatabaseHelper.ExecuteScalar(query, parameters);
                int newId = Convert.ToInt32(result);

                ErrorManager.Instance.LogInfo($"Yeni araç başarıyla eklendi. ID: {newId}, Plaka: {entity.Plate}, Marka/Model: {entity.Brand} {entity.Model}", "VehicleRepository.Add");
                return newId;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Araç eklenirken bir hata oluştu: Plaka: {entity.Plate}, Marka/Model: {entity.Brand} {entity.Model}",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Araç eklenirken bir hata oluştu. (Hata ID: {errorId})", ex);
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
                                  AracSinifID = @sinifId,
                                  GuncellenmeTarihi = NOW()
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

                string durumBilgisi = GetStatusDescription(entity.StatusID);
                string subeBilgisi = entity.BranchID.HasValue ? $", Şube: {entity.BranchName}" : "";

                ErrorManager.Instance.LogInfo($"Araç güncelleniyor. ID: {entity.VehicleID}, Plaka: {entity.Plate}, " +
                    $"Marka/Model: {entity.Brand} {entity.Model}, Durum: {durumBilgisi}{subeBilgisi}, " +
                    $"Kilometre: {entity.Kilometers}", "VehicleRepository.Update");

                int affectedRows = DatabaseHelper.ExecuteNonQuery(query, parameters);

                if (affectedRows > 0)
                {
                    ErrorManager.Instance.LogInfo($"Araç başarıyla güncellendi. ID: {entity.VehicleID}, Plaka: {entity.Plate}", "VehicleRepository.Update");
                }
                else
                {
                    ErrorManager.Instance.LogWarning($"Araç güncellenemedi, kayıt bulunamadı. ID: {entity.VehicleID}", "VehicleRepository.Update");
                }
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Araç güncellenirken bir hata oluştu. ID: {entity.VehicleID}, Plaka: {entity.Plate}",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Araç güncellenirken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                // Önce araç bilgilerini al
                Vehicle vehicle = null;
                try
                {
                    vehicle = GetById(id);
                    ErrorManager.Instance.LogInfo($"Araç silme işlemi için veri alındı. ID: {id}, Plaka: {vehicle.Plate}, Marka/Model: {vehicle.Brand} {vehicle.Model}", "VehicleRepository.Delete");
                }
                catch
                {
                    ErrorManager.Instance.LogWarning($"Silinecek araç kaydı bulunamadı. ID: {id}", "VehicleRepository.Delete");
                }

                // Aracı sil
                string query = $@"DELETE FROM {TABLE_NAME}
                                  WHERE AracID = @aracId";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@aracId", id)
                };

                ErrorManager.Instance.LogInfo($"Araç siliniyor. ID: {id}", "VehicleRepository.Delete");
                int affectedRows = DatabaseHelper.ExecuteNonQuery(query, parameters);

                if (affectedRows > 0)
                {
                    string vehicleInfo = vehicle != null ?
                        $", Plaka: {vehicle.Plate}, Marka/Model: {vehicle.Brand} {vehicle.Model}" : "";

                    ErrorManager.Instance.LogInfo($"Araç başarıyla silindi. ID: {id}{vehicleInfo}", "VehicleRepository.Delete");
                }
                else
                {
                    ErrorManager.Instance.LogWarning($"Araç silinemedi, kayıt bulunamadı. ID: {id}", "VehicleRepository.Delete");
                }
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Araç silinirken bir hata oluştu. ID: {id}",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Araç silinirken bir hata oluştu. (Hata ID: {errorId})", ex);
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
                                     OR a.SasiNo LIKE @search OR a.Renk LIKE @search
                                  ORDER BY a.AracID DESC";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@search", "%" + searchText + "%")
                };

                ErrorManager.Instance.LogInfo($"Araç arama yapılıyor. Arama metni: '{searchText}'", "VehicleRepository.Search");
                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
                List<Vehicle> vehicles = MapDataTableToVehicles(dt);

                ErrorManager.Instance.LogInfo($"Araç araması tamamlandı. '{searchText}' için {vehicles.Count} sonuç bulundu", "VehicleRepository.Search");
                return vehicles;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Araç arama sırasında bir hata oluştu. Arama metni: '{searchText}'",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Araç arama sırasında bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// Belirli bir duruma sahip araçları getirir
        /// </summary>
        public List<Vehicle> GetVehiclesByStatus(int statusId)
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
                                  WHERE a.DurumID = @durumId
                                  ORDER BY a.AracID DESC";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@durumId", statusId)
                };

                string durumAdi = GetStatusDescription(statusId);
                ErrorManager.Instance.LogInfo($"Durum ID: {statusId} ({durumAdi}) araçları getiriliyor", "VehicleRepository.GetVehiclesByStatus");
                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
                List<Vehicle> vehicles = MapDataTableToVehicles(dt);

                ErrorManager.Instance.LogInfo($"Durum ID: {statusId} ({durumAdi}) için {vehicles.Count} araç bulundu", "VehicleRepository.GetVehiclesByStatus");
                return vehicles;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Durum ID: {statusId} ile araçlar listelenirken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Durum ile araçlar listelenirken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// Müsait araçları getirir (satılık veya kiralanabilir)
        /// </summary>
        public List<Vehicle> GetAvailableVehicles()
        {
            try
            {
                // Müsait durumundaki araçları getirir (1: Müsait, 2: Satılık)
                string query = $@"SELECT a.AracID, a.Plaka, a.Marka, a.Model, a.YapimYili, a.MotorNo, a.SasiNo, 
                                         a.Renk, a.Kilometre, a.YakitTipi, a.VitesTipi, a.DurumID, d.DurumAdi,
                                         a.AlinmaTarihi, a.AlisFiyati, a.SatisFiyati, a.SubeID, s.SubeAdi, 
                                         a.AracSinifID, c.SinifAdi, a.OlusturmaTarihi, a.GuncellenmeTarihi
                                  FROM {TABLE_NAME} a
                                  LEFT JOIN AracDurumlari d ON a.DurumID = d.DurumID
                                  LEFT JOIN Subeler s ON a.SubeID = s.SubeID
                                  LEFT JOIN AracSiniflari c ON a.AracSinifID = c.AracSinifID
                                  WHERE a.DurumID IN (1, 2)
                                  ORDER BY a.AracID DESC";

                ErrorManager.Instance.LogInfo("Müsait araçlar getiriliyor", "VehicleRepository.GetAvailableVehicles");
                DataTable dt = DatabaseHelper.ExecuteQuery(query);
                List<Vehicle> vehicles = MapDataTableToVehicles(dt);

                ErrorManager.Instance.LogInfo($"{vehicles.Count} müsait araç bulundu", "VehicleRepository.GetAvailableVehicles");
                return vehicles;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Müsait araçlar listelenirken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Müsait araçlar listelenirken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// Belirli bir şubedeki araçları getirir
        /// </summary>
        public List<Vehicle> GetVehiclesByBranch(int branchId)
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
                                  WHERE a.SubeID = @subeId
                                  ORDER BY a.AracID DESC";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@subeId", branchId)
                };

                ErrorManager.Instance.LogInfo($"Şube ID: {branchId} araçları getiriliyor", "VehicleRepository.GetVehiclesByBranch");
                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
                List<Vehicle> vehicles = MapDataTableToVehicles(dt);

                ErrorManager.Instance.LogInfo($"Şube ID: {branchId} için {vehicles.Count} araç bulundu", "VehicleRepository.GetVehiclesByBranch");
                return vehicles;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Şube ID: {branchId} ile araçlar listelenirken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Şube ile araçlar listelenirken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// Aracın durumunu günceller
        /// </summary>
        public void UpdateVehicleStatus(int vehicleId, int newStatusId)
        {
            try
            {
                string query = $@"UPDATE {TABLE_NAME} SET
                                  DurumID = @durumId,
                                  GuncellenmeTarihi = NOW()
                                  WHERE AracID = @aracId";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@aracId", vehicleId),
                    DatabaseHelper.CreateParameter("@durumId", newStatusId)
                };

                string durumAdi = GetStatusDescription(newStatusId);
                ErrorManager.Instance.LogInfo($"Araç durumu güncelleniyor. ID: {vehicleId}, Yeni Durum: {durumAdi}", "VehicleRepository.UpdateVehicleStatus");
                int affectedRows = DatabaseHelper.ExecuteNonQuery(query, parameters);

                if (affectedRows > 0)
                {
                    ErrorManager.Instance.LogInfo($"Araç durumu başarıyla güncellendi. ID: {vehicleId}, Yeni Durum: {durumAdi}", "VehicleRepository.UpdateVehicleStatus");
                }
                else
                {
                    ErrorManager.Instance.LogWarning($"Araç durumu güncellenemedi, kayıt bulunamadı. ID: {vehicleId}", "VehicleRepository.UpdateVehicleStatus");
                    throw new Exception($"ID: {vehicleId} ile araç bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Araç durumu güncellenirken bir hata oluştu. ID: {vehicleId}, Yeni Durum ID: {newStatusId}",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Araç durumu güncellenirken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// Aracın kilometre bilgisini günceller
        /// </summary>
        public void UpdateVehicleKilometers(int vehicleId, int newKilometers)
        {
            try
            {
                // Önce mevcut kilometre bilgisini kontrol edelim
                Vehicle vehicle = GetById(vehicleId);

                if (newKilometers < vehicle.Kilometers)
                {
                    string warningMessage = $"Yeni kilometre değeri ({newKilometers}) mevcut değerden ({vehicle.Kilometers}) küçük.";
                    ErrorManager.Instance.LogWarning(warningMessage, "VehicleRepository.UpdateVehicleKilometers");
                    throw new Exception(warningMessage);
                }

                string query = $@"UPDATE {TABLE_NAME} SET
                                  Kilometre = @km,
                                  GuncellenmeTarihi = NOW()
                                  WHERE AracID = @aracId";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@aracId", vehicleId),
                    DatabaseHelper.CreateParameter("@km", newKilometers)
                };

                ErrorManager.Instance.LogInfo($"Araç kilometre bilgisi güncelleniyor. ID: {vehicleId}, Plaka: {vehicle.Plate}, " +
                    $"Eski: {vehicle.Kilometers} km, Yeni: {newKilometers} km", "VehicleRepository.UpdateVehicleKilometers");

                int affectedRows = DatabaseHelper.ExecuteNonQuery(query, parameters);

                if (affectedRows > 0)
                {
                    ErrorManager.Instance.LogInfo($"Araç kilometre bilgisi başarıyla güncellendi. ID: {vehicleId}, " +
                        $"Eski: {vehicle.Kilometers} km, Yeni: {newKilometers} km", "VehicleRepository.UpdateVehicleKilometers");
                }
                else
                {
                    ErrorManager.Instance.LogWarning($"Araç kilometre bilgisi güncellenemedi, kayıt bulunamadı. ID: {vehicleId}", "VehicleRepository.UpdateVehicleKilometers");
                }
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Araç kilometre bilgisi güncellenirken bir hata oluştu. ID: {vehicleId}, Yeni Kilometre: {newKilometers}",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Araç kilometre bilgisi güncellenirken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// Belirli bir fiyat aralığındaki araçları getirir
        /// </summary>
        public List<Vehicle> GetVehiclesByPriceRange(decimal minPrice, decimal maxPrice)
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
                                  WHERE a.SatisFiyati BETWEEN @minFiyat AND @maxFiyat
                                  AND a.SatisFiyati IS NOT NULL
                                  ORDER BY a.SatisFiyati ASC";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@minFiyat", minPrice),
                    DatabaseHelper.CreateParameter("@maxFiyat", maxPrice)
                };

                ErrorManager.Instance.LogInfo($"Fiyat aralığına göre araç getiriliyor. Min: {minPrice:C2}, Max: {maxPrice:C2}", "VehicleRepository.GetVehiclesByPriceRange");
                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
                List<Vehicle> vehicles = MapDataTableToVehicles(dt);

                ErrorManager.Instance.LogInfo($"Fiyat aralığı araması tamamlandı. {minPrice:C2} - {maxPrice:C2} aralığında {vehicles.Count} araç bulundu", "VehicleRepository.GetVehiclesByPriceRange");
                return vehicles;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Fiyat aralığına göre araç getirme sırasında bir hata oluştu. Min: {minPrice:C2}, Max: {maxPrice:C2}",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Fiyat aralığına göre araç getirme sırasında bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// Aracın satış fiyatını günceller
        /// </summary>
        public void UpdateSalePrice(int vehicleId, decimal? newSalePrice)
        {
            try
            {
                string query = $@"UPDATE {TABLE_NAME} SET
                                  SatisFiyati = @satisFiyat,
                                  GuncellenmeTarihi = NOW()
                                  WHERE AracID = @aracId";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@aracId", vehicleId),
                    DatabaseHelper.CreateParameter("@satisFiyat", newSalePrice ?? (object)DBNull.Value)
                };

                string fiyatBilgisi = newSalePrice.HasValue ? $"{newSalePrice:C2}" : "Belirlenmedi";
                ErrorManager.Instance.LogInfo($"Araç satış fiyatı güncelleniyor. ID: {vehicleId}, Yeni Fiyat: {fiyatBilgisi}", "VehicleRepository.UpdateSalePrice");
                int affectedRows = DatabaseHelper.ExecuteNonQuery(query, parameters);

                if (affectedRows > 0)
                {
                    ErrorManager.Instance.LogInfo($"Araç satış fiyatı başarıyla güncellendi. ID: {vehicleId}, Yeni Fiyat: {fiyatBilgisi}", "VehicleRepository.UpdateSalePrice");
                }
                else
                {
                    ErrorManager.Instance.LogWarning($"Araç satış fiyatı güncellenemedi, kayıt bulunamadı. ID: {vehicleId}", "VehicleRepository.UpdateSalePrice");
                    throw new Exception($"ID: {vehicleId} ile araç bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                string fiyatBilgisi = newSalePrice.HasValue ? $"{newSalePrice:C2}" : "Belirlenmedi";
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Araç satış fiyatı güncellenirken bir hata oluştu. ID: {vehicleId}, Yeni Fiyat: {fiyatBilgisi}",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Araç satış fiyatı güncellenirken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// Durum ID'sine göre açıklayıcı bilgi döndürür
        /// </summary>
        private string GetStatusDescription(int statusId)
        {
            switch (statusId)
            {
                case 1: return "Müsait";
                case 2: return "Satılık";
                case 3: return "Satıldı";
                case 4: return "Kirada";
                case 5: return "Rezerve Edildi";
                case 6: return "Serviste";
                case 7: return "Arızalı";
                case 8: return "Bakımda";
                default: return $"Bilinmeyen Durum ({statusId})";
            }
        }

        private List<Vehicle> MapDataTableToVehicles(DataTable dt)
        {
            try
            {
                var vehicles = new List<Vehicle>();
                foreach (DataRow row in dt.Rows)
                {
                    vehicles.Add(MapDataRowToVehicle(row));
                }
                return vehicles;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Araç verileri dönüştürülürken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Business);

                throw new Exception($"Araç verileri dönüştürülürken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        private Vehicle MapDataRowToVehicle(DataRow row)
        {
            try
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
            catch (Exception ex)
            {
                // Hangi araç kaydıyla ilgili sorun olduğunu belirlemek için
                int vehicleId = 0;
                string plate = "Bilinmiyor";
                try
                {
                    vehicleId = row.GetValue<int>("AracID");
                    plate = row.GetValue<string>("Plaka");
                }
                catch { }

                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Araç verisi dönüştürme sırasında hata oluştu (AracID: {vehicleId}, Plaka: {plate})",
                    ErrorSeverity.Error,
                    ErrorSource.Business);

                throw new Exception($"Araç verisi dönüştürülürken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }
    }
}