using System;
using System.Data;
using System.Net;
using arac_kiralama_satis_desktop.Utils;
using MySql.Data.MySqlClient;

namespace arac_kiralama_satis_desktop.Methods
{
    public class LoginMethods
    {
        // Verify user credentials
        public static bool VerifyLogin(string username, string password)
        {
            try
            {
                // Önce kullanıcının varlığını kontrol et - daha güvenli bir yaklaşım
                string userQuery = "SELECT KullaniciID, Sifre, Durum FROM Kullanicilar WHERE KullaniciAdi = @username";

                MySqlParameter[] userParams = new MySqlParameter[]
                {
                    new MySqlParameter("@username", username)
                };

                DataTable userResult = DatabaseConnection.ExecuteQuery(userQuery, userParams);

                if (userResult.Rows.Count == 0)
                {
                    // Kullanıcı bulunamadı
                    return false;
                }

                // Kullanıcı var ama durum kontrolü
                bool isActive = Convert.ToBoolean(userResult.Rows[0]["Durum"]);
                if (!isActive)
                {
                    // Kullanıcı pasif durumda
                    return false;
                }

                // Şifre kontrolü
                string storedPassword = userResult.Rows[0]["Sifre"].ToString();

                // Direkt karşılaştırma - prodüksiyonda hash kullanılmalı
                return password == storedPassword;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Giriş kontrolü sırasında hata: " + ex.Message,
                                "Doğrulama Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Get user information after successful login
        public static DataRow GetUserInfo(string username)
        {
            string query = @"SELECT k.KullaniciID, k.Ad, k.Soyad, k.Email, k.RolID, r.RolAdi, k.SubeID, s.SubeAdi 
                           FROM Kullanicilar k 
                           LEFT JOIN Roller r ON k.RolID = r.RolID 
                           LEFT JOIN Subeler s ON k.SubeID = s.SubeID 
                           WHERE k.KullaniciAdi = @username AND k.Durum = 1";

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@username", username)
            };

            DataTable dt = DatabaseConnection.ExecuteQuery(query, parameters);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0];
            }
            return null;
        }

        // Update last login date
        public static void UpdateLastLogin(int userID)
        {
            string query = "UPDATE Kullanicilar SET SonGirisTarihi = CURRENT_TIMESTAMP WHERE KullaniciID = @userID";

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@userID", userID)
            };

            DatabaseConnection.ExecuteNonQuery(query, parameters);
        }

        // Log login attempt
        public static void LogLoginAttempt(int? userID, bool success, string ipAddress)
        {
            string islemTipi = success ? "Başarılı Giriş" : "Başarısız Giriş";
            string islemDetayi = success
                ? $"Kullanıcı başarıyla giriş yaptı. Kullanıcı ID: {userID}"
                : "Geçersiz kullanıcı adı veya şifre.";

            string query = @"INSERT INTO Loglar (KullaniciID, IslemTipi, IslemDetayi, IPAdresi) 
                           VALUES (@userID, @islemTipi, @islemDetayi, @ipAddress)";

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@userID", userID.HasValue ? (object)userID.Value : DBNull.Value),
                new MySqlParameter("@islemTipi", islemTipi),
                new MySqlParameter("@islemDetayi", islemDetayi),
                new MySqlParameter("@ipAddress", ipAddress)
            };

            DatabaseConnection.ExecuteNonQuery(query, parameters);
        }

        // Get client IP address 
        public static string GetIPAddress()
        {
            string hostName = Dns.GetHostName();
            IPAddress[] addresses = Dns.GetHostAddresses(hostName);

            foreach (IPAddress address in addresses)
            {
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return address.ToString();
                }
            }

            return "127.0.0.1";
        }

        // Save user credentials for "Remember Me" functionality
        public static void SaveUserCredentials(string username)
        {
            // Save the username using the UserSettings class
            UserSettings.Current.SavedUsername = username;
            UserSettings.Current.RememberMe = true;
            UserSettings.Save();
        }

        // Load saved username if "Remember Me" was checked
        public static string LoadSavedUsername()
        {
            if (UserSettings.Current.RememberMe)
            {
                return UserSettings.Current.SavedUsername;
            }
            return string.Empty;
        }

        // Clear saved credentials
        public static void ClearSavedCredentials()
        {
            UserSettings.Current.SavedUsername = "";
            UserSettings.Current.RememberMe = false;
            UserSettings.Save();
        }
    }
}