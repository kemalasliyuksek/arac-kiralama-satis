using System;
using System.Collections.Generic;
using System.Data;
using arac_kiralama_satis_desktop.Models;
using arac_kiralama_satis_desktop.Repositories;
using arac_kiralama_satis_desktop.Utils;

namespace arac_kiralama_satis_desktop.Methods
{
    public class StaffMethods
    {
        private static readonly StaffRepository _repository = new StaffRepository();

        public static DataTable GetRoles()
        {
            try
            {
                string query = "SELECT RolID, RolAdi, Aciklama FROM Roller ORDER BY RolID";
                return DatabaseHelper.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Roller alınırken bir hata oluştu: " + ex.Message);
            }
        }

        public static List<Staff> GetStaffList()
        {
            try
            {
                return _repository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Personel listesi alınırken bir hata oluştu: " + ex.Message);
            }
        }

        public static Staff GetStaffById(int staffId)
        {
            try
            {
                return _repository.GetById(staffId);
            }
            catch (Exception ex)
            {
                throw new Exception("Personel bilgisi alınırken bir hata oluştu: " + ex.Message);
            }
        }

        public static DataRow GetUserById(int userId)
        {
            try
            {
                // Repository'den Staff nesnesini al
                Staff staff = _repository.GetById(userId);

                // DataTable ve DataRow oluştur
                DataTable dt = new DataTable();
                dt.Columns.Add("KullaniciID", typeof(int));
                dt.Columns.Add("Ad", typeof(string));
                dt.Columns.Add("Soyad", typeof(string));
                dt.Columns.Add("KullaniciAdi", typeof(string));
                dt.Columns.Add("Email", typeof(string));
                dt.Columns.Add("UlkeKodu", typeof(string));
                dt.Columns.Add("TelefonNo", typeof(string));
                dt.Columns.Add("RolID", typeof(int));
                dt.Columns.Add("RolAdi", typeof(string));
                dt.Columns.Add("SubeID", typeof(int));
                dt.Columns.Add("SubeAdi", typeof(string));
                dt.Columns.Add("Durum", typeof(bool));
                dt.Columns.Add("SonGirisTarihi", typeof(DateTime));
                dt.Columns.Add("OlusturmaTarihi", typeof(DateTime));

                // Rol ve şube bilgilerini al
                string roleName = string.Empty;
                string branchName = string.Empty;

                if (staff.RoleID > 0)
                {
                    string roleQuery = "SELECT RolAdi FROM Roller WHERE RolID = @roleId";
                    var roleParam = new[] { DatabaseHelper.CreateParameter("@roleId", staff.RoleID) };
                    object roleResult = DatabaseHelper.ExecuteScalar(roleQuery, roleParam);
                    roleName = roleResult != null ? roleResult.ToString() : string.Empty;
                }

                if (staff.BranchID.HasValue && staff.BranchID.Value > 0)
                {
                    string branchQuery = "SELECT SubeAdi FROM Subeler WHERE SubeID = @branchId";
                    var branchParam = new[] { DatabaseHelper.CreateParameter("@branchId", staff.BranchID.Value) };
                    object branchResult = DatabaseHelper.ExecuteScalar(branchQuery, branchParam);
                    branchName = branchResult != null ? branchResult.ToString() : string.Empty;
                }

                // DataRow'u doldur
                DataRow row = dt.NewRow();
                row["KullaniciID"] = staff.StaffID;
                row["Ad"] = staff.FirstName;
                row["Soyad"] = staff.LastName;
                row["KullaniciAdi"] = staff.Username;
                row["Email"] = staff.Email;
                row["UlkeKodu"] = staff.CountryCode;
                row["TelefonNo"] = staff.PhoneNumber;
                row["RolID"] = staff.RoleID;
                row["RolAdi"] = roleName;
                row["SubeID"] = staff.BranchID ?? (object)DBNull.Value;
                row["SubeAdi"] = branchName;
                row["Durum"] = staff.IsActive;
                row["SonGirisTarihi"] = staff.LastLoginDate ?? (object)DBNull.Value;
                row["OlusturmaTarihi"] = staff.CreatedDate;

                dt.Rows.Add(row);
                return dt.Rows[0];
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcı bilgisi alınırken bir hata oluştu: " + ex.Message);
            }
        }

        public static bool AddUser(Dictionary<string, object> parameters)
        {
            try
            {
                Staff staff = new Staff
                {
                    FirstName = parameters["Ad"].ToString(),
                    LastName = parameters["Soyad"].ToString(),
                    Username = parameters["KullaniciAdi"].ToString(),
                    Password = parameters["Sifre"].ToString(),
                    Email = parameters["Email"].ToString(),
                    CountryCode = parameters["UlkeKodu"].ToString(),
                    PhoneNumber = parameters["TelefonNo"].ToString(),
                    RoleID = Convert.ToInt32(parameters["RolID"]),
                    BranchID = parameters.ContainsKey("SubeID") && parameters["SubeID"] != null ?
                        Convert.ToInt32(parameters["SubeID"]) : (int?)null,
                    IsActive = Convert.ToBoolean(parameters["Durum"])
                };

                return _repository.Add(staff) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcı eklenirken bir hata oluştu: " + ex.Message);
            }
        }

        public static bool UpdateUser(Dictionary<string, object> parameters)
        {
            try
            {
                Staff staff = new Staff
                {
                    StaffID = Convert.ToInt32(parameters["KullaniciID"]),
                    FirstName = parameters["Ad"].ToString(),
                    LastName = parameters["Soyad"].ToString(),
                    Username = parameters["KullaniciAdi"].ToString(),
                    Email = parameters["Email"].ToString(),
                    CountryCode = parameters["UlkeKodu"].ToString(),
                    PhoneNumber = parameters["TelefonNo"].ToString(),
                    RoleID = Convert.ToInt32(parameters["RolID"]),
                    BranchID = parameters.ContainsKey("SubeID") && parameters["SubeID"] != null ?
                        Convert.ToInt32(parameters["SubeID"]) : (int?)null,
                    IsActive = Convert.ToBoolean(parameters["Durum"])
                };

                if (parameters.ContainsKey("Sifre") && !string.IsNullOrEmpty(parameters["Sifre"].ToString()))
                {
                    staff.Password = parameters["Sifre"].ToString();
                }

                _repository.Update(staff);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcı güncellenirken bir hata oluştu: " + ex.Message);
            }
        }

        public static bool ChangeUserStatus(int userId, bool isActive)
        {
            try
            {
                Staff staff = _repository.GetById(userId);
                staff.IsActive = isActive;
                _repository.Update(staff);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcı durumu değiştirilirken bir hata oluştu: " + ex.Message);
            }
        }

        // DataTable dönüşümü için yardımcı metot
        public static DataTable GetStaffAsDataTable()
        {
            try
            {
                List<Staff> staffList = _repository.GetAll();
                DataTable dt = new DataTable();

                // DataTable sütunlarını oluştur
                dt.Columns.Add("KullaniciID", typeof(int));
                dt.Columns.Add("Ad", typeof(string));
                dt.Columns.Add("Soyad", typeof(string));
                dt.Columns.Add("KullaniciAdi", typeof(string));
                dt.Columns.Add("Email", typeof(string));
                dt.Columns.Add("Telefon", typeof(string));
                dt.Columns.Add("RolID", typeof(int));
                dt.Columns.Add("RolAdi", typeof(string));
                dt.Columns.Add("SubeID", typeof(int));
                dt.Columns.Add("SubeAdi", typeof(string));
                dt.Columns.Add("Durum", typeof(string));
                dt.Columns.Add("SonGiris", typeof(DateTime));
                dt.Columns.Add("KayitTarihi", typeof(DateTime));

                // Dictionary'ler rol ve şube adlarını almak için
                Dictionary<int, string> roleNames = new Dictionary<int, string>();
                Dictionary<int, string> branchNames = new Dictionary<int, string>();

                // Rol adlarını al
                DataTable roles = GetRoles();
                foreach (DataRow role in roles.Rows)
                {
                    roleNames.Add(Convert.ToInt32(role["RolID"]), role["RolAdi"].ToString());
                }

                // Şube adlarını al
                DataTable branches = BranchMethods.GetBranchList();
                foreach (DataRow branch in branches.Rows)
                {
                    branchNames.Add(Convert.ToInt32(branch["SubeID"]), branch["SubeAdi"].ToString());
                }

                // Personelleri DataTable'a ekle
                foreach (var staff in staffList)
                {
                    string roleName = staff.RoleID > 0 && roleNames.ContainsKey(staff.RoleID) ?
                        roleNames[staff.RoleID] : string.Empty;

                    string branchName = staff.BranchID.HasValue && staff.BranchID.Value > 0 &&
                        branchNames.ContainsKey(staff.BranchID.Value) ?
                        branchNames[staff.BranchID.Value] : string.Empty;

                    dt.Rows.Add(
                        staff.StaffID,
                        staff.FirstName,
                        staff.LastName,
                        staff.Username,
                        staff.Email,
                        staff.CountryCode + staff.PhoneNumber,
                        staff.RoleID,
                        roleName,
                        staff.BranchID ?? (object)DBNull.Value,
                        branchName,
                        staff.IsActive ? "Aktif" : "Pasif",
                        staff.LastLoginDate ?? (object)DBNull.Value,
                        staff.CreatedDate
                    );
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Personeller DataTable'a dönüştürülürken bir hata oluştu: " + ex.Message);
            }
        }
    }
}