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
                DataTable dt = DatabaseHelper.ExecuteQuery(query);
                return MapDataTableToBranches(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Şubeler listelenirken bir hata oluştu: " + ex.Message, ex);
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
                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
                if (dt.Rows.Count > 0)
                    return MapDataRowToBranch(dt.Rows[0]);
                else
                    throw new Exception("Şube bulunamadı.");
            }
            catch (Exception ex)
            {
                throw new Exception("Şube bilgisi alınırken bir hata oluştu: " + ex.Message, ex);
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

                object result = DatabaseHelper.ExecuteScalar(query, parameters);
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Şube eklenirken bir hata oluştu: " + ex.Message, ex);
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

                DatabaseHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Şube güncellenirken bir hata oluştu: " + ex.Message, ex);
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

                DatabaseHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Şube silinirken bir hata oluştu: " + ex.Message, ex);
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

                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
                return MapDataTableToBranches(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Şube arama sırasında bir hata oluştu: " + ex.Message, ex);
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
    }
}
