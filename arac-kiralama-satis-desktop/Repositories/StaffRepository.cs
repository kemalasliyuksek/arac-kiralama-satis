using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using arac_kiralama_satis_desktop.Models;
using arac_kiralama_satis_desktop.Utils;

namespace arac_kiralama_satis_desktop.Repositories
{
    public class StaffRepository : IRepository<Staff, int>
    {
        private const string TABLE_NAME = "Kullanicilar";

        public List<Staff> GetAll()
        {
            try
            {
                string query = $@"SELECT KullaniciID, Ad, Soyad, KullaniciAdi, Sifre, Email, 
                                         UlkeKodu, TelefonNo, RolID, SubeID, Durum, 
                                         SonGirisTarihi, OlusturmaTarihi
                                  FROM {TABLE_NAME}
                                  ORDER BY KullaniciID DESC";
                DataTable dt = DatabaseHelper.ExecuteQuery(query);
                return MapDataTableToStaff(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcılar listelenirken bir hata oluştu: " + ex.Message, ex);
            }
        }

        public Staff GetById(int id)
        {
            try
            {
                string query = $@"SELECT KullaniciID, Ad, Soyad, KullaniciAdi, Sifre, Email, 
                                         UlkeKodu, TelefonNo, RolID, SubeID, Durum, 
                                         SonGirisTarihi, OlusturmaTarihi
                                  FROM {TABLE_NAME}
                                  WHERE KullaniciID = @id";
                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@id", id)
                };
                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
                if (dt.Rows.Count > 0)
                    return MapDataRowToStaff(dt.Rows[0]);
                else
                    throw new Exception("Kullanıcı bulunamadı.");
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcı bilgisi alınırken bir hata oluştu: " + ex.Message, ex);
            }
        }

        public int Add(Staff entity)
        {
            try
            {
                string query = $@"INSERT INTO {TABLE_NAME}
                                  (Ad, Soyad, KullaniciAdi, Sifre, Email, UlkeKodu, TelefonNo, 
                                   RolID, SubeID, Durum)
                                  VALUES
                                  (@ad, @soyad, @username, @sifre, @mail, @ulkeKodu, @telefonNo, 
                                   @rolID, @subeID, @durum);
                                  SELECT LAST_INSERT_ID();";
                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@ad", entity.FirstName),
                    DatabaseHelper.CreateParameter("@soyad", entity.LastName),
                    DatabaseHelper.CreateParameter("@username", entity.Username),
                    DatabaseHelper.CreateParameter("@sifre", entity.Password),
                    DatabaseHelper.CreateParameter("@mail", entity.Email),
                    DatabaseHelper.CreateParameter("@ulkeKodu", entity.CountryCode),
                    DatabaseHelper.CreateParameter("@telefonNo", entity.PhoneNumber),
                    DatabaseHelper.CreateParameter("@rolID", entity.RoleID),
                    DatabaseHelper.CreateParameter("@subeID", entity.BranchID ?? (object)DBNull.Value),
                    DatabaseHelper.CreateParameter("@durum", entity.IsActive)
                };

                object result = DatabaseHelper.ExecuteScalar(query, parameters);
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcı eklenirken bir hata oluştu: " + ex.Message, ex);
            }
        }

        public void Update(Staff entity)
        {
            try
            {
                // Şifre güncellemesi isteğe bağlı olabilir. Eğer boş ise değiştirmeyebiliriz.
                string query = $@"UPDATE {TABLE_NAME} SET
                                  Ad = @ad,
                                  Soyad = @soyad,
                                  KullaniciAdi = @username,
                                  Email = @mail,
                                  UlkeKodu = @ulkeKodu,
                                  TelefonNo = @telefonNo,
                                  RolID = @rolID,
                                  SubeID = @subeID,
                                  Durum = @durum" +
                                  (string.IsNullOrEmpty(entity.Password) ? "" : ", Sifre = @sifre") +
                                  " WHERE KullaniciID = @id";

                List<MySqlParameter> parameters = new List<MySqlParameter>
                {
                    DatabaseHelper.CreateParameter("@id", entity.StaffID),
                    DatabaseHelper.CreateParameter("@ad", entity.FirstName),
                    DatabaseHelper.CreateParameter("@soyad", entity.LastName),
                    DatabaseHelper.CreateParameter("@username", entity.Username),
                    DatabaseHelper.CreateParameter("@mail", entity.Email),
                    DatabaseHelper.CreateParameter("@ulkeKodu", entity.CountryCode),
                    DatabaseHelper.CreateParameter("@telefonNo", entity.PhoneNumber),
                    DatabaseHelper.CreateParameter("@rolID", entity.RoleID),
                    DatabaseHelper.CreateParameter("@subeID", entity.BranchID ?? (object)DBNull.Value),
                    DatabaseHelper.CreateParameter("@durum", entity.IsActive)
                };

                if (!string.IsNullOrEmpty(entity.Password))
                {
                    parameters.Add(DatabaseHelper.CreateParameter("@sifre", entity.Password));
                }

                DatabaseHelper.ExecuteNonQuery(query, parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcı güncellenirken bir hata oluştu: " + ex.Message, ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                // Soft-delete uygulanabilir: Örneğin Durum = false olarak güncellenebilir.
                // Burada direkt silme örneği verilmiştir.
                string query = $@"DELETE FROM {TABLE_NAME}
                                  WHERE KullaniciID = @id";
                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@id", id)
                };
                DatabaseHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcı silinirken bir hata oluştu: " + ex.Message, ex);
            }
        }

        public List<Staff> Search(string searchText)
        {
            try
            {
                string query = $@"SELECT KullaniciID, Ad, Soyad, KullaniciAdi, Sifre, Email, 
                                         UlkeKodu, TelefonNo, RolID, SubeID, Durum, 
                                         SonGirisTarihi, OlusturmaTarihi
                                  FROM {TABLE_NAME}
                                  WHERE Ad LIKE @search OR Soyad LIKE @search OR KullaniciAdi LIKE @search OR Email LIKE @search
                                  ORDER BY KullaniciID DESC";
                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@search", "%" + searchText + "%")
                };
                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
                return MapDataTableToStaff(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcı arama sırasında bir hata oluştu: " + ex.Message, ex);
            }
        }

        private List<Staff> MapDataTableToStaff(DataTable dt)
        {
            var list = new List<Staff>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(MapDataRowToStaff(row));
            }
            return list;
        }

        private Staff MapDataRowToStaff(DataRow row)
        {
            return new Staff
            {
                StaffID = row.GetValue<int>("KullaniciID"),
                FirstName = row.GetValue<string>("Ad"),
                LastName = row.GetValue<string>("Soyad"),
                Username = row.GetValue<string>("KullaniciAdi"),
                Password = row.GetValue<string>("Sifre"),
                Email = row.GetValue<string>("Email"),
                CountryCode = row.GetValue<string>("UlkeKodu"),
                PhoneNumber = row.GetValue<string>("TelefonNo"),
                RoleID = row.GetValue<int>("RolID"),
                BranchID = row.GetValue<int?>("SubeID"),
                IsActive = row.GetValue<bool>("Durum"),
                LastLoginDate = row.GetValue<DateTime?>("SonGirisTarihi"),
                CreatedDate = row.GetValue<DateTime>("OlusturmaTarihi")
            };
        }
    }
}
