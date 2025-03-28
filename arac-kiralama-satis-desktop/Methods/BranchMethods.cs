using System;
using System.Collections.Generic;
using System.Data;
using arac_kiralama_satis_desktop.Models;
using arac_kiralama_satis_desktop.Utils;
using MySql.Data.MySqlClient;

namespace arac_kiralama_satis_desktop.Methods
{
    public class BranchMethods
    {
        public static DataTable GetBranchList()
        {
            try
            {
                string query = @"SELECT s.SubeID, s.SubeAdi, s.Adres, s.SehirPlaka, 
                                CONCAT(s.UlkeKodu, s.TelefonNo) as Telefon, 
                                s.Email, s.AktifMi, s.OlusturmaTarihi 
                                FROM Subeler s
                                ORDER BY s.SubeAdi";

                DataTable result = DatabaseConnection.ExecuteQuery(query);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Şube listesi alınırken bir hata oluştu: " + ex.Message);
            }
        }

        public static DataRow GetBranchById(int branchId)
        {
            try
            {
                string query = @"SELECT s.SubeID, s.SubeAdi, s.Adres, s.UlkeKodu, s.TelefonNo, 
                                s.Email, s.SehirPlaka, s.AktifMi, s.OlusturmaTarihi 
                                FROM Subeler s 
                                WHERE s.SubeID = @subeId";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@subeId", branchId)
                };

                DataTable result = DatabaseConnection.ExecuteQuery(query, parameters);

                if (result.Rows.Count > 0)
                {
                    return result.Rows[0];
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
                    new MySqlParameter("@email", branch.Email != string.Empty ? (object)branch.Email : DBNull.Value),
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
                    new MySqlParameter("@email", branch.Email != string.Empty ? (object)branch.Email : DBNull.Value),
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

        public static void DeleteBranch(int branchId)
        {
            try
            {
                // Önce şubeye ait araçları ve kullanıcıları kontrol et
                string checkQuery = @"SELECT COUNT(*) FROM Araclar WHERE SubeID = @subeId;
                                    SELECT COUNT(*) FROM Kullanicilar WHERE SubeID = @subeId;";

                MySqlParameter[] checkParams = new MySqlParameter[]
                {
                    new MySqlParameter("@subeId", branchId)
                };

                int vehicleCount = Convert.ToInt32(DatabaseConnection.ExecuteScalar(checkQuery, checkParams));
                if (vehicleCount > 0)
                {
                    throw new Exception("Bu şubeye ait araçlar bulunmaktadır. Önce araçları başka bir şubeye transfer edin.");
                }

                // Direkt silmek yerine AktifMi = false olarak işaretle
                string query = "UPDATE Subeler SET AktifMi = FALSE WHERE SubeID = @subeId";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@subeId", branchId)
                };

                DatabaseConnection.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Şube silinirken bir hata oluştu: " + ex.Message);
            }
        }
    }
}