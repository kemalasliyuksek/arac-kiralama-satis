using System;
using System.Collections.Generic;
using System.Data;
using arac_kiralama_satis_desktop._Backups;
using MySql.Data.MySqlClient;

namespace arac_kiralama_satis_desktop.Methods
{
    public class StaffMethods
    {
        /// <summary>
        /// Tüm rolleri getirir
        /// </summary>
        public static DataTable GetRoles()
        {
            try
            {
                string query = "SELECT RolID, RolAdi, Aciklama FROM Roller ORDER BY RolID";
                return DatabaseConnection.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Roller alınırken bir hata oluştu: " + ex.Message);
            }
        }

        /// <summary>
        /// Kullanıcı ID'sine göre kullanıcı bilgilerini getirir
        /// </summary>
        public static DataRow GetUserById(int userId)
        {
            try
            {
                string query = @"SELECT k.KullaniciID, k.Ad, k.Soyad, k.KullaniciAdi, 
                               k.Email, CONCAT(k.UlkeKodu, k.TelefonNo) as Telefon,
                               k.RolID, r.RolAdi, k.SubeID, s.SubeAdi, k.Durum
                               FROM Kullanicilar k
                               LEFT JOIN Roller r ON k.RolID = r.RolID
                               LEFT JOIN Subeler s ON k.SubeID = s.SubeID
                               WHERE k.KullaniciID = @kullaniciId";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@kullaniciId", userId)
                };

                DataTable result = DatabaseConnection.ExecuteQuery(query, parameters);

                if (result.Rows.Count > 0)
                {
                    return result.Rows[0];
                }
                else
                {
                    throw new Exception("Kullanıcı bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcı bilgisi alınırken bir hata oluştu: " + ex.Message);
            }
        }

        /// <summary>
        /// Kullanıcı adının kullanılabilir olup olmadığını kontrol eder
        /// </summary>
        public static bool IsUsernameAvailable(string username, int? excludeUserId = null)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM Kullanicilar WHERE KullaniciAdi = @kullaniciAdi";

                if (excludeUserId.HasValue)
                {
                    query += " AND KullaniciID != @kullaniciId";
                }

                List<MySqlParameter> parameters = new List<MySqlParameter>
                {
                    new MySqlParameter("@kullaniciAdi", username)
                };

                if (excludeUserId.HasValue)
                {
                    parameters.Add(new MySqlParameter("@kullaniciId", excludeUserId.Value));
                }

                object result = DatabaseConnection.ExecuteScalar(query, parameters.ToArray());
                int count = Convert.ToInt32(result);

                return count == 0; // Eğer count 0 ise kullanıcı adı kullanılabilir
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcı adı kontrolü yapılırken bir hata oluştu: " + ex.Message);
            }
        }

        /// <summary>
        /// Yeni kullanıcı ekler
        /// </summary>
        public static bool AddUser(Dictionary<string, object> parameters)
        {
            try
            {
                if (!parameters.ContainsKey("KullaniciAdi") || !parameters.ContainsKey("Sifre"))
                {
                    throw new Exception("Kullanıcı adı ve şifre zorunludur.");
                }

                // Kullanıcı adı kontrolü
                string username = parameters["KullaniciAdi"].ToString();
                if (!IsUsernameAvailable(username))
                {
                    throw new Exception("Bu kullanıcı adı zaten kullanımda.");
                }

                // SQL sorgusu ve parametreleri hazırla
                string query = @"INSERT INTO Kullanicilar 
                               (Ad, Soyad, KullaniciAdi, Sifre, Email,
                                UlkeKodu, TelefonNo, RolID, SubeID, Durum)
                               VALUES 
                               (@ad, @soyad, @kullaniciAdi, @sifre, @email,
                                @ulkeKodu, @telefonNo, @rolID, @subeID, @durum)";

                List<MySqlParameter> sqlParams = new List<MySqlParameter>
                {
                    new MySqlParameter("@ad", parameters["Ad"]),
                    new MySqlParameter("@soyad", parameters["Soyad"]),
                    new MySqlParameter("@kullaniciAdi", parameters["KullaniciAdi"]),
                    new MySqlParameter("@sifre", parameters["Sifre"]),
                    new MySqlParameter("@email", parameters["Email"]),
                    new MySqlParameter("@ulkeKodu", parameters["UlkeKodu"]),
                    new MySqlParameter("@telefonNo", parameters["TelefonNo"]),
                    new MySqlParameter("@rolID", parameters["RolID"]),
                    new MySqlParameter("@durum", parameters["Durum"])
                };

                // SubeID null olabilir
                if (parameters.ContainsKey("SubeID") && parameters["SubeID"] != null)
                {
                    sqlParams.Add(new MySqlParameter("@subeID", parameters["SubeID"]));
                }
                else
                {
                    sqlParams.Add(new MySqlParameter("@subeID", DBNull.Value));
                }

                int result = DatabaseConnection.ExecuteNonQuery(query, sqlParams.ToArray());
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcı eklenirken bir hata oluştu: " + ex.Message);
            }
        }

        /// <summary>
        /// Kullanıcı bilgilerini günceller
        /// </summary>
        public static bool UpdateUser(Dictionary<string, object> parameters)
        {
            try
            {
                if (!parameters.ContainsKey("KullaniciID") || !parameters.ContainsKey("KullaniciAdi"))
                {
                    throw new Exception("Kullanıcı ID ve Kullanıcı adı zorunludur.");
                }

                int userId = Convert.ToInt32(parameters["KullaniciID"]);
                string username = parameters["KullaniciAdi"].ToString();

                // Kullanıcı adı kullanılabilirlik kontrolü (kendisi hariç)
                if (!IsUsernameAvailable(username, userId))
                {
                    throw new Exception("Bu kullanıcı adı zaten başka bir kullanıcı tarafından kullanılıyor.");
                }

                // SQL sorgusu oluştur
                string query = @"UPDATE Kullanicilar SET 
                               Ad = @ad, 
                               Soyad = @soyad, 
                               KullaniciAdi = @kullaniciAdi, 
                               Email = @email,
                               UlkeKodu = @ulkeKodu, 
                               TelefonNo = @telefonNo, 
                               RolID = @rolID, 
                               SubeID = @subeID, 
                               Durum = @durum";

                // Şifre güncellenecekse sorguya ekle
                if (parameters.ContainsKey("Sifre"))
                {
                    query += ", Sifre = @sifre";
                }

                query += " WHERE KullaniciID = @kullaniciID";

                // SQL parametrelerini hazırla
                List<MySqlParameter> sqlParams = new List<MySqlParameter>
                {
                    new MySqlParameter("@kullaniciID", userId),
                    new MySqlParameter("@ad", parameters["Ad"]),
                    new MySqlParameter("@soyad", parameters["Soyad"]),
                    new MySqlParameter("@kullaniciAdi", parameters["KullaniciAdi"]),
                    new MySqlParameter("@email", parameters["Email"]),
                    new MySqlParameter("@ulkeKodu", parameters["UlkeKodu"]),
                    new MySqlParameter("@telefonNo", parameters["TelefonNo"]),
                    new MySqlParameter("@rolID", parameters["RolID"]),
                    new MySqlParameter("@durum", parameters["Durum"])
                };

                // Şifre parametresi ekle
                if (parameters.ContainsKey("Sifre"))
                {
                    sqlParams.Add(new MySqlParameter("@sifre", parameters["Sifre"]));
                }

                // SubeID null olabilir
                if (parameters.ContainsKey("SubeID") && parameters["SubeID"] != null)
                {
                    sqlParams.Add(new MySqlParameter("@subeID", parameters["SubeID"]));
                }
                else
                {
                    sqlParams.Add(new MySqlParameter("@subeID", DBNull.Value));
                }

                int result = DatabaseConnection.ExecuteNonQuery(query, sqlParams.ToArray());
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcı güncellenirken bir hata oluştu: " + ex.Message);
            }
        }

        /// <summary>
        /// Kullanıcı durumunu değiştirir (aktif/pasif)
        /// </summary>
        public static bool ChangeUserStatus(int userId, bool isActive)
        {
            try
            {
                string query = "UPDATE Kullanicilar SET Durum = @durum WHERE KullaniciID = @kullaniciId";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@kullaniciId", userId),
                    new MySqlParameter("@durum", isActive)
                };

                int result = DatabaseConnection.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcı durumu değiştirilirken bir hata oluştu: " + ex.Message);
            }
        }
    }
}