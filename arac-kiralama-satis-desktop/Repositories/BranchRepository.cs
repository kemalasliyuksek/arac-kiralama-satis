using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using arac_kiralama_satis_desktop.Models;
using arac_kiralama_satis_desktop.Utils;

namespace arac_kiralama_satis_desktop.Repositories
{
    public class BranchRepository : IRepository<Branch, int>
    {
        private const string TABLE_NAME = "Subeler";

        public List<Branch> GetAll()
        {
            try
            {
                string query = $@"SELECT SubeID, SubeAdi, Adres, UlkeKodu, TelefonNo, Email, SehirPlaka, AktifMi, OlusturmaTarihi
                                  FROM {TABLE_NAME}
                                  ORDER BY SubeAdi";

                ErrorManager.Instance.LogInfo($"Tüm şubeler getiriliyor", "BranchRepository.GetAll");
                DataTable dt = DatabaseHelper.ExecuteQuery(query);
                List<Branch> branches = MapDataTableToBranches(dt);
                ErrorManager.Instance.LogInfo($"{branches.Count} şube başarıyla listelendi", "BranchRepository.GetAll");

                return branches;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Şubeler listelenirken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Şubeler listelenirken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        public Branch GetById(int id)
        {
            try
            {
                string query = $@"SELECT SubeID, SubeAdi, Adres, UlkeKodu, TelefonNo, Email, SehirPlaka, AktifMi, OlusturmaTarihi
                                  FROM {TABLE_NAME}
                                  WHERE SubeID = @subeId";
                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@subeId", id)
                };

                ErrorManager.Instance.LogInfo($"Şube ID: {id} ile aranıyor", "BranchRepository.GetById");
                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    Branch branch = MapDataRowToBranch(dt.Rows[0]);
                    ErrorManager.Instance.LogInfo($"Şube bulundu: {branch.BranchName}", "BranchRepository.GetById");
                    return branch;
                }
                else
                {
                    ErrorManager.Instance.LogWarning($"Şube bulunamadı. ID: {id}", "BranchRepository.GetById");
                    throw new Exception($"ID: {id} ile şube bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Şube bilgisi alınırken bir hata oluştu (ID: {id})",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Şube bilgisi alınırken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        public int Add(Branch entity)
        {
            try
            {
                string query = $@"INSERT INTO {TABLE_NAME} 
                                  (SubeAdi, Adres, UlkeKodu, TelefonNo, Email, SehirPlaka, AktifMi)
                                  VALUES 
                                  (@subeAdi, @adres, @ulkeKodu, @telefonNo, @email, @sehirPlaka, @aktifMi);
                                  SELECT LAST_INSERT_ID();";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@subeAdi", entity.BranchName),
                    DatabaseHelper.CreateParameter("@adres", entity.Address),
                    DatabaseHelper.CreateParameter("@ulkeKodu", entity.CountryCode),
                    DatabaseHelper.CreateParameter("@telefonNo", entity.PhoneNumber),
                    DatabaseHelper.CreateParameter("@email", entity.Email),
                    DatabaseHelper.CreateParameter("@sehirPlaka", entity.CityCode),
                    DatabaseHelper.CreateParameter("@aktifMi", entity.IsActive)
                };

                ErrorManager.Instance.LogInfo($"Yeni şube ekleniyor: {entity.BranchName}", "BranchRepository.Add");
                object result = DatabaseHelper.ExecuteScalar(query, parameters);
                int newId = Convert.ToInt32(result);

                ErrorManager.Instance.LogInfo($"Yeni şube başarıyla eklendi. ID: {newId}, Ad: {entity.BranchName}", "BranchRepository.Add");
                return newId;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Şube eklenirken bir hata oluştu: {entity.BranchName}",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Şube eklenirken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        public void Update(Branch entity)
        {
            try
            {
                string query = $@"UPDATE {TABLE_NAME} SET 
                                  SubeAdi = @subeAdi,
                                  Adres = @adres,
                                  UlkeKodu = @ulkeKodu,
                                  TelefonNo = @telefonNo,
                                  Email = @email,
                                  SehirPlaka = @sehirPlaka,
                                  AktifMi = @aktifMi
                                  WHERE SubeID = @subeId";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@subeId", entity.BranchID),
                    DatabaseHelper.CreateParameter("@subeAdi", entity.BranchName),
                    DatabaseHelper.CreateParameter("@adres", entity.Address),
                    DatabaseHelper.CreateParameter("@ulkeKodu", entity.CountryCode),
                    DatabaseHelper.CreateParameter("@telefonNo", entity.PhoneNumber),
                    DatabaseHelper.CreateParameter("@email", entity.Email),
                    DatabaseHelper.CreateParameter("@sehirPlaka", entity.CityCode),
                    DatabaseHelper.CreateParameter("@aktifMi", entity.IsActive)
                };

                ErrorManager.Instance.LogInfo($"Şube güncelleniyor. ID: {entity.BranchID}, Ad: {entity.BranchName}", "BranchRepository.Update");
                int affectedRows = DatabaseHelper.ExecuteNonQuery(query, parameters);

                if (affectedRows > 0)
                {
                    ErrorManager.Instance.LogInfo($"Şube başarıyla güncellendi. ID: {entity.BranchID}, Ad: {entity.BranchName}", "BranchRepository.Update");
                }
                else
                {
                    ErrorManager.Instance.LogWarning($"Şube güncellenemedi, kayıt bulunamadı. ID: {entity.BranchID}", "BranchRepository.Update");
                }
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Şube güncellenirken bir hata oluştu. ID: {entity.BranchID}, Ad: {entity.BranchName}",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Şube güncellenirken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                // Soft delete
                string query = $@"UPDATE {TABLE_NAME} 
                                  SET AktifMi = FALSE
                                  WHERE SubeID = @subeId";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@subeId", id)
                };

                ErrorManager.Instance.LogInfo($"Şube siliniyor (pasif hale getiriliyor). ID: {id}", "BranchRepository.Delete");
                int affectedRows = DatabaseHelper.ExecuteNonQuery(query, parameters);

                if (affectedRows > 0)
                {
                    ErrorManager.Instance.LogInfo($"Şube başarıyla silindi (pasif hale getirildi). ID: {id}", "BranchRepository.Delete");
                }
                else
                {
                    ErrorManager.Instance.LogWarning($"Şube silinemedi, kayıt bulunamadı. ID: {id}", "BranchRepository.Delete");
                }
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Şube silinirken bir hata oluştu. ID: {id}",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Şube silinirken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        public List<Branch> Search(string searchText)
        {
            try
            {
                string query = $@"SELECT SubeID, SubeAdi, Adres, UlkeKodu, TelefonNo, Email, SehirPlaka, AktifMi, OlusturmaTarihi
                                  FROM {TABLE_NAME}
                                  WHERE SubeAdi LIKE @search OR Adres LIKE @search OR Email LIKE @search
                                  ORDER BY SubeAdi";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@search", "%" + searchText + "%")
                };

                ErrorManager.Instance.LogInfo($"Şube arama yapılıyor. Arama metni: '{searchText}'", "BranchRepository.Search");
                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
                List<Branch> branches = MapDataTableToBranches(dt);

                ErrorManager.Instance.LogInfo($"Şube araması tamamlandı. '{searchText}' için {branches.Count} sonuç bulundu", "BranchRepository.Search");
                return branches;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Şube arama sırasında bir hata oluştu. Arama metni: '{searchText}'",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Şube arama sırasında bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        private List<Branch> MapDataTableToBranches(DataTable dt)
        {
            var branches = new List<Branch>();
            foreach (DataRow row in dt.Rows)
            {
                branches.Add(MapDataRowToBranch(row));
            }
            return branches;
        }

        private Branch MapDataRowToBranch(DataRow row)
        {
            try
            {
                return new Branch
                {
                    BranchID = row.GetValue<int>("SubeID"),
                    BranchName = row.GetValue<string>("SubeAdi"),
                    Address = row.GetValue<string>("Adres"),
                    CountryCode = row.GetValue<string>("UlkeKodu"),
                    PhoneNumber = row.GetValue<string>("TelefonNo"),
                    Email = row.GetValue<string>("Email"),
                    CityCode = row.GetValue<string>("SehirPlaka"),
                    IsActive = row.GetValue<bool>("AktifMi"),
                    CreatedDate = row.GetValue<DateTime>("OlusturmaTarihi")
                };
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Veri dönüştürme sırasında hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Business);

                throw new Exception($"Şube verileri dönüştürülürken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }
    }
}