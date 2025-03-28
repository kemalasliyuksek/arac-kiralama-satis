using System;
using System.Collections.Generic;
using System.Data;
using arac_kiralama_satis_desktop.Models;
using arac_kiralama_satis_desktop.Utils;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;
using Branch = arac_kiralama_satis_desktop.Models.Branch;

namespace arac_kiralama_satis_desktop.Methods
{
    public class BranchMethods
    {
        public static DataTable GetBranches()
        {
            try
            {
                string query = @"SELECT SubeID, SubeAdi, Adres, UlkeKodu, TelefonNo, Email, SehirPlaka, 
                               AktifMi, OlusturmaTarihi
                               FROM Subeler
                               WHERE AktifMi = 1
                               ORDER BY SubeID";

                return DatabaseConnection.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Şubeler listelenirken bir hata oluştu: " + ex.Message);
            }
        }

        public static Branch GetBranchById(int branchId)
        {
            try
            {
                string query = @"SELECT SubeID, SubeAdi, Adres, UlkeKodu, TelefonNo, Email, SehirPlaka, 
                               AktifMi, OlusturmaTarihi
                               FROM Subeler 
                               WHERE SubeID = @subeId";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@subeId", branchId)
                };

                DataTable result = DatabaseConnection.ExecuteQuery(query, parameters);

                if (result.Rows.Count > 0)
                {
                    DataRow row = result.Rows[0];
                    Branch branch = new Branch
                    {
                        BranchID = Convert.ToInt32(row["SubeID"]),
                        BranchName = row["SubeAdi"].ToString(),
                        Address = row["Adres"].ToString(),
                        CountryCode = row["UlkeKodu"].ToString(),
                        PhoneNumber = row["TelefonNo"].ToString(),
                        Email = row["Email"] != DBNull.Value ? row["Email"].ToString() : string.Empty,
                        CityCode = row["SehirPlaka"].ToString(),
                        IsActive = Convert.ToBoolean(row["AktifMi"]),
                        CreatedDate = Convert.ToDateTime(row["OlusturmaTarihi"])
                    };

                    return branch;
                }
                else
                {
                    throw new Exception("Şube bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Şube bilgisi alınırken bir hata oluştu: " + ex.Message);
            }
        }

        public static int AddBranch(Branch branch)
        {
            try
            {
                string query = @"INSERT INTO Subeler (SubeAdi, Adres, UlkeKodu, TelefonNo, Email, SehirPlaka, AktifMi)
                               VALUES (@subeAdi, @adres, @ulkeKodu, @telefonNo, @email, @sehirPlaka, @aktifMi);
                               SELECT LAST_INSERT_ID();";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@subeAdi", branch.BranchName),
                    new MySqlParameter("@adres", branch.Address),
                    new MySqlParameter("@ulkeKodu", branch.CountryCode),
                    new MySqlParameter("@telefonNo", branch.PhoneNumber),
                    new MySqlParameter("@email", !string.IsNullOrEmpty(branch.Email) ? (object)branch.Email : DBNull.Value),
                    new MySqlParameter("@sehirPlaka", branch.CityCode),
                    new MySqlParameter("@aktifMi", branch.IsActive)
                };

                object result = DatabaseConnection.ExecuteScalar(query, parameters);
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Şube eklenirken bir hata oluştu: " + ex.Message);
            }
        }

        public static void UpdateBranch(Branch branch)
        {
            try
            {
                string query = @"UPDATE Subeler SET 
                               SubeAdi = @subeAdi, 
                               Adres = @adres, 
                               UlkeKodu = @ulkeKodu, 
                               TelefonNo = @telefonNo, 
                               Email = @email, 
                               SehirPlaka = @sehirPlaka, 
                               AktifMi = @aktifMi
                               WHERE SubeID = @subeId";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@subeId", branch.BranchID),
                    new MySqlParameter("@subeAdi", branch.BranchName),
                    new MySqlParameter("@adres", branch.Address),
                    new MySqlParameter("@ulkeKodu", branch.CountryCode),
                    new MySqlParameter("@telefonNo", branch.PhoneNumber),
                    new MySqlParameter("@email", !string.IsNullOrEmpty(branch.Email) ? (object)branch.Email : DBNull.Value),
                    new MySqlParameter("@sehirPlaka", branch.CityCode),
                    new MySqlParameter("@aktifMi", branch.IsActive)
                };

                DatabaseConnection.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Şube güncellenirken bir hata oluştu: " + ex.Message);
            }
        }

        public static void DeactivateBranch(int branchId)
        {
            try
            {
                string query = "UPDATE Subeler SET AktifMi = 0 WHERE SubeID = @subeId";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@subeId", branchId)
                };

                DatabaseConnection.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Şube pasifleştirilirken bir hata oluştu: " + ex.Message);
            }
        }
    }
}