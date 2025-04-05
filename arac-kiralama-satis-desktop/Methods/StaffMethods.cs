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

        public static DataRow GetUserById(int userId)
        {
            try
            {
                // Repository’den Staff nesnesini aldıktan sonra örnek DataTable oluşturularak DataRow elde ediliyor.
                Staff staff = _repository.GetById(userId);
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

                DataRow row = dt.NewRow();
                row["KullaniciID"] = staff.StaffID;
                row["Ad"] = staff.FirstName;
                row["Soyad"] = staff.LastName;
                row["KullaniciAdi"] = staff.Username;
                row["Email"] = staff.Email;
                row["UlkeKodu"] = staff.CountryCode;
                row["TelefonNo"] = staff.PhoneNumber;
                row["RolID"] = staff.RoleID;
                row["RolAdi"] = ""; // Rol adı repository’de dönülmediği için
                row["SubeID"] = staff.BranchID.HasValue ? staff.BranchID.Value : 0;
                row["SubeAdi"] = "";
                row["Durum"] = staff.IsActive;
                row["SonGirisTarihi"] = staff.LastLoginDate.HasValue ? staff.LastLoginDate.Value : DateTime.MinValue;
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
                    BranchID = parameters.ContainsKey("SubeID") && parameters["SubeID"] != null ? Convert.ToInt32(parameters["SubeID"]) : (int?)null,
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
                    BranchID = parameters.ContainsKey("SubeID") && parameters["SubeID"] != null ? Convert.ToInt32(parameters["SubeID"]) : (int?)null,
                    IsActive = Convert.ToBoolean(parameters["Durum"]),
                    Password = parameters.ContainsKey("Sifre") ? parameters["Sifre"].ToString() : string.Empty
                };
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
    }
}
