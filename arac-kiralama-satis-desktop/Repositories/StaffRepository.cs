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

                ErrorManager.Instance.LogInfo("Tüm personel kayıtları getiriliyor", "StaffRepository.GetAll");
                DataTable dt = DatabaseHelper.ExecuteQuery(query);
                List<Staff> staffList = MapDataTableToStaff(dt);
                ErrorManager.Instance.LogInfo($"{staffList.Count} personel kaydı başarıyla listelendi", "StaffRepository.GetAll");

                return staffList;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Kullanıcılar listelenirken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Kullanıcılar listelenirken bir hata oluştu. (Hata ID: {errorId})", ex);
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

                ErrorManager.Instance.LogInfo($"Personel ID: {id} ile aranıyor", "StaffRepository.GetById");
                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    Staff staff = MapDataRowToStaff(dt.Rows[0]);
                    ErrorManager.Instance.LogInfo($"Personel bulundu: ID: {staff.StaffID}, Ad Soyad: {staff.FullName}, Kullanıcı Adı: {staff.Username}", "StaffRepository.GetById");
                    return staff;
                }
                else
                {
                    ErrorManager.Instance.LogWarning($"Personel bulunamadı. ID: {id}", "StaffRepository.GetById");
                    throw new Exception($"ID: {id} ile kullanıcı bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Kullanıcı bilgisi alınırken bir hata oluştu (ID: {id})",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Kullanıcı bilgisi alınırken bir hata oluştu. (Hata ID: {errorId})", ex);
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

                string rolBilgisi = GetRoleDescription(entity.RoleID);
                string subeBilgisi = entity.BranchID.HasValue ? $", Şube ID: {entity.BranchID}" : ", Şube Atanmamış";

                ErrorManager.Instance.LogInfo($"Yeni personel ekleniyor: {entity.FirstName} {entity.LastName}, " +
                    $"Kullanıcı Adı: {entity.Username}, Rol: {rolBilgisi}{subeBilgisi}", "StaffRepository.Add");

                object result = DatabaseHelper.ExecuteScalar(query, parameters);
                int newId = Convert.ToInt32(result);

                ErrorManager.Instance.LogInfo($"Yeni personel başarıyla eklendi. ID: {newId}, Ad Soyad: {entity.FirstName} {entity.LastName}", "StaffRepository.Add");
                return newId;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Kullanıcı eklenirken bir hata oluştu: {entity.FirstName} {entity.LastName}, Kullanıcı Adı: {entity.Username}",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Kullanıcı eklenirken bir hata oluştu. (Hata ID: {errorId})", ex);
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

                string rolBilgisi = GetRoleDescription(entity.RoleID);
                string subeBilgisi = entity.BranchID.HasValue ? $", Şube ID: {entity.BranchID}" : ", Şube Atanmamış";
                string durumBilgisi = entity.IsActive ? "Aktif" : "Pasif";
                string sifreBilgisi = string.IsNullOrEmpty(entity.Password) ? "" : ", Şifre güncelleniyor";

                ErrorManager.Instance.LogInfo($"Personel güncelleniyor. ID: {entity.StaffID}, Ad Soyad: {entity.FirstName} {entity.LastName}, " +
                    $"Kullanıcı Adı: {entity.Username}, Rol: {rolBilgisi}{subeBilgisi}, Durum: {durumBilgisi}{sifreBilgisi}", "StaffRepository.Update");

                int affectedRows = DatabaseHelper.ExecuteNonQuery(query, parameters.ToArray());

                if (affectedRows > 0)
                {
                    ErrorManager.Instance.LogInfo($"Personel başarıyla güncellendi. ID: {entity.StaffID}, Ad Soyad: {entity.FirstName} {entity.LastName}", "StaffRepository.Update");
                }
                else
                {
                    ErrorManager.Instance.LogWarning($"Personel güncellenemedi, kayıt bulunamadı. ID: {entity.StaffID}", "StaffRepository.Update");
                }
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Kullanıcı güncellenirken bir hata oluştu. ID: {entity.StaffID}, Ad Soyad: {entity.FirstName} {entity.LastName}",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Kullanıcı güncellenirken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                // Önce personel bilgilerini al
                Staff staff = null;
                try
                {
                    staff = GetById(id);
                    ErrorManager.Instance.LogInfo($"Personel silme işlemi için veri alındı. ID: {id}, Ad Soyad: {staff.FullName}, Kullanıcı Adı: {staff.Username}", "StaffRepository.Delete");
                }
                catch
                {
                    ErrorManager.Instance.LogWarning($"Silinecek personel kaydı bulunamadı. ID: {id}", "StaffRepository.Delete");
                }

                // Soft-delete uygulanabilir: Örneğin Durum = false olarak güncellenebilir.
                // Burada direkt silme örneği verilmiştir.
                string query = $@"DELETE FROM {TABLE_NAME}
                                  WHERE KullaniciID = @id";
                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@id", id)
                };

                ErrorManager.Instance.LogInfo($"Personel siliniyor. ID: {id}", "StaffRepository.Delete");
                int affectedRows = DatabaseHelper.ExecuteNonQuery(query, parameters);

                if (affectedRows > 0)
                {
                    string staffInfo = staff != null ?
                        $", Ad Soyad: {staff.FullName}, Kullanıcı Adı: {staff.Username}" : "";

                    ErrorManager.Instance.LogInfo($"Personel başarıyla silindi. ID: {id}{staffInfo}", "StaffRepository.Delete");
                }
                else
                {
                    ErrorManager.Instance.LogWarning($"Personel silinemedi, kayıt bulunamadı. ID: {id}", "StaffRepository.Delete");
                }
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Kullanıcı silinirken bir hata oluştu. ID: {id}",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Kullanıcı silinirken bir hata oluştu. (Hata ID: {errorId})", ex);
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

                ErrorManager.Instance.LogInfo($"Personel arama yapılıyor. Arama metni: '{searchText}'", "StaffRepository.Search");
                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
                List<Staff> staffList = MapDataTableToStaff(dt);

                ErrorManager.Instance.LogInfo($"Personel araması tamamlandı. '{searchText}' için {staffList.Count} sonuç bulundu", "StaffRepository.Search");
                return staffList;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Kullanıcı arama sırasında bir hata oluştu. Arama metni: '{searchText}'",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Kullanıcı arama sırasında bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// Kullanıcı adı ve şifre ile giriş doğrulama
        /// </summary>
        public Staff ValidateLogin(string username, string password)
        {
            try
            {
                string query = $@"SELECT KullaniciID, Ad, Soyad, KullaniciAdi, Sifre, Email, 
                                         UlkeKodu, TelefonNo, RolID, SubeID, Durum, 
                                         SonGirisTarihi, OlusturmaTarihi
                                  FROM {TABLE_NAME}
                                  WHERE KullaniciAdi = @username AND Sifre = @password AND Durum = 1";
                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@username", username),
                    DatabaseHelper.CreateParameter("@password", password)
                };

                ErrorManager.Instance.LogInfo($"Kullanıcı girişi doğrulanıyor. Kullanıcı Adı: {username}", "StaffRepository.ValidateLogin");
                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    Staff staff = MapDataRowToStaff(dt.Rows[0]);
                    string rolBilgisi = GetRoleDescription(staff.RoleID);
                    ErrorManager.Instance.LogInfo($"Kullanıcı girişi başarılı. ID: {staff.StaffID}, Ad Soyad: {staff.FullName}, Rol: {rolBilgisi}", "StaffRepository.ValidateLogin");

                    // Son giriş tarihini güncelle
                    UpdateLastLoginDate(staff.StaffID);

                    return staff;
                }
                else
                {
                    ErrorManager.Instance.LogWarning($"Kullanıcı girişi başarısız. Kullanıcı Adı: {username}", "StaffRepository.ValidateLogin");
                    return null;
                }
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Kullanıcı girişi doğrulanırken bir hata oluştu. Kullanıcı Adı: {username}",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Kullanıcı girişi doğrulanırken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// Son giriş tarihini günceller
        /// </summary>
        private void UpdateLastLoginDate(int staffId)
        {
            try
            {
                string query = $@"UPDATE {TABLE_NAME} SET
                                  SonGirisTarihi = NOW()
                                  WHERE KullaniciID = @id";
                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@id", staffId)
                };

                ErrorManager.Instance.LogInfo($"Son giriş tarihi güncelleniyor. Personel ID: {staffId}", "StaffRepository.UpdateLastLoginDate");
                DatabaseHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                // Bu metod kritik değil, hatayı sadece logla
                ErrorManager.Instance.HandleException(
                    ex,
                    $"Son giriş tarihi güncellenirken bir hata oluştu. Personel ID: {staffId}",
                    ErrorSeverity.Warning,
                    ErrorSource.Database,
                    false);
            }
        }

        /// <summary>
        /// Belirli bir role sahip tüm personeli getirir
        /// </summary>
        public List<Staff> GetStaffByRole(int roleId)
        {
            try
            {
                string query = $@"SELECT KullaniciID, Ad, Soyad, KullaniciAdi, Sifre, Email, 
                                         UlkeKodu, TelefonNo, RolID, SubeID, Durum, 
                                         SonGirisTarihi, OlusturmaTarihi
                                  FROM {TABLE_NAME}
                                  WHERE RolID = @roleId AND Durum = 1
                                  ORDER BY Ad, Soyad";
                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@roleId", roleId)
                };

                string rolBilgisi = GetRoleDescription(roleId);
                ErrorManager.Instance.LogInfo($"Rol ID: {roleId} ({rolBilgisi}) ile personel getiriliyor", "StaffRepository.GetStaffByRole");
                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
                List<Staff> staffList = MapDataTableToStaff(dt);

                ErrorManager.Instance.LogInfo($"Rol ID: {roleId} ({rolBilgisi}) ile {staffList.Count} personel bulundu", "StaffRepository.GetStaffByRole");
                return staffList;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Rol ID: {roleId} ile personel listelenirken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Rol ile personel listelenirken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// Belirli bir şubedeki tüm personeli getirir
        /// </summary>
        public List<Staff> GetStaffByBranch(int branchId)
        {
            try
            {
                string query = $@"SELECT KullaniciID, Ad, Soyad, KullaniciAdi, Sifre, Email, 
                                         UlkeKodu, TelefonNo, RolID, SubeID, Durum, 
                                         SonGirisTarihi, OlusturmaTarihi
                                  FROM {TABLE_NAME}
                                  WHERE SubeID = @branchId AND Durum = 1
                                  ORDER BY Ad, Soyad";
                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@branchId", branchId)
                };

                ErrorManager.Instance.LogInfo($"Şube ID: {branchId} ile personel getiriliyor", "StaffRepository.GetStaffByBranch");
                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
                List<Staff> staffList = MapDataTableToStaff(dt);

                ErrorManager.Instance.LogInfo($"Şube ID: {branchId} ile {staffList.Count} personel bulundu", "StaffRepository.GetStaffByBranch");
                return staffList;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Şube ID: {branchId} ile personel listelenirken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Şube ile personel listelenirken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// Kullanıcının şifresini değiştirir
        /// </summary>
        public void ChangePassword(int staffId, string newPassword)
        {
            try
            {
                string query = $@"UPDATE {TABLE_NAME} SET
                                  Sifre = @sifre
                                  WHERE KullaniciID = @id";
                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@id", staffId),
                    DatabaseHelper.CreateParameter("@sifre", newPassword)
                };

                ErrorManager.Instance.LogInfo($"Personel şifresi değiştiriliyor. ID: {staffId}", "StaffRepository.ChangePassword");
                int affectedRows = DatabaseHelper.ExecuteNonQuery(query, parameters);

                if (affectedRows > 0)
                {
                    ErrorManager.Instance.LogInfo($"Personel şifresi başarıyla değiştirildi. ID: {staffId}", "StaffRepository.ChangePassword");
                }
                else
                {
                    ErrorManager.Instance.LogWarning($"Personel şifresi değiştirilemedi, kayıt bulunamadı. ID: {staffId}", "StaffRepository.ChangePassword");
                    throw new Exception($"ID: {staffId} ile kullanıcı bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Kullanıcı şifresi değiştirilirken bir hata oluştu. ID: {staffId}",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Kullanıcı şifresi değiştirilirken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// Personelin durumunu günceller (aktif/pasif)
        /// </summary>
        public void SetStaffStatus(int staffId, bool isActive)
        {
            try
            {
                string query = $@"UPDATE {TABLE_NAME} SET
                                  Durum = @durum
                                  WHERE KullaniciID = @id";
                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@id", staffId),
                    DatabaseHelper.CreateParameter("@durum", isActive)
                };

                string durumText = isActive ? "aktif" : "pasif";
                ErrorManager.Instance.LogInfo($"Personel durumu {durumText} olarak güncelleniyor. ID: {staffId}", "StaffRepository.SetStaffStatus");
                int affectedRows = DatabaseHelper.ExecuteNonQuery(query, parameters);

                if (affectedRows > 0)
                {
                    ErrorManager.Instance.LogInfo($"Personel durumu başarıyla {durumText} yapıldı. ID: {staffId}", "StaffRepository.SetStaffStatus");
                }
                else
                {
                    ErrorManager.Instance.LogWarning($"Personel durumu değiştirilemedi, kayıt bulunamadı. ID: {staffId}", "StaffRepository.SetStaffStatus");
                    throw new Exception($"ID: {staffId} ile kullanıcı bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                string durumText = isActive ? "aktif" : "pasif";
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Kullanıcı durumu {durumText} yapılırken bir hata oluştu. ID: {staffId}",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Kullanıcı durumu değiştirilirken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// Rol ID'sine göre açıklayıcı bilgi döndürür
        /// </summary>
        private string GetRoleDescription(int roleId)
        {
            switch (roleId)
            {
                case 1: return "Yönetici";
                case 2: return "Satış Yetkilisi";
                case 3: return "Kiralama Yetkilisi";
                case 4: return "Müşteri Temsilcisi";
                case 5: return "Servis Yetkilisi";
                default: return $"Bilinmeyen Rol ({roleId})";
            }
        }

        private List<Staff> MapDataTableToStaff(DataTable dt)
        {
            try
            {
                var list = new List<Staff>();
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(MapDataRowToStaff(row));
                }
                return list;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Personel verileri dönüştürülürken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Business);

                throw new Exception($"Personel verileri dönüştürülürken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        private Staff MapDataRowToStaff(DataRow row)
        {
            try
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
            catch (Exception ex)
            {
                // Hangi personel kaydıyla ilgili sorun olduğunu belirlemek için
                int staffId = 0;
                try { staffId = row.GetValue<int>("KullaniciID"); } catch { }

                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Personel verisi dönüştürme sırasında hata oluştu (KullaniciID: {staffId})",
                    ErrorSeverity.Error,
                    ErrorSource.Business);

                throw new Exception($"Personel verisi dönüştürülürken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }
    }
}