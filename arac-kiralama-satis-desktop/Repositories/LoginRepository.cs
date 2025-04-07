using System;
using System.Data;
using System.Net;
using MySql.Data.MySqlClient;
using arac_kiralama_satis_desktop.Utils;

namespace arac_kiralama_satis_desktop.Repositories
{
    public class LoginRepository
    {
        public bool VerifyLogin(string username, string password)
        {
            try
            {
                string query = @"SELECT KullaniciID, Sifre, Durum 
                                 FROM Kullanicilar
                                 WHERE KullaniciAdi = @username";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@username", username)
                };

                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
                if (dt.Rows.Count == 0) return false;

                bool isActive = Convert.ToBoolean(dt.Rows[0]["Durum"]);
                if (!isActive) return false;

                string storedPassword = dt.Rows[0]["Sifre"].ToString();
                return (password == storedPassword);
            }
            catch (Exception ex)
            {
                throw new Exception("Giriş doğrulaması sırasında hata: " + ex.Message, ex);
            }
        }

        public DataRow GetUserInfo(string username)
        {
            string query = @"SELECT k.KullaniciID, k.Ad, k.Soyad, k.Email, k.RolID, r.RolAdi, 
                                    k.SubeID, s.SubeAdi
                             FROM Kullanicilar k
                             LEFT JOIN Roller r ON k.RolID = r.RolID
                             LEFT JOIN Subeler s ON k.SubeID = s.SubeID
                             WHERE k.KullaniciAdi = @username
                               AND k.Durum = 1";

            MySqlParameter[] parameters = {
                DatabaseHelper.CreateParameter("@username", username)
            };

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        public void UpdateLastLogin(int userId)
        {
            string query = @"UPDATE Kullanicilar
                             SET SonGirisTarihi = CURRENT_TIMESTAMP
                             WHERE KullaniciID = @id";

            MySqlParameter[] parameters = {
                DatabaseHelper.CreateParameter("@id", userId)
            };

            DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // LogLoginAttempt metodunu kaldırdık - artık ErrorManager kullanılacak

        public string GetIPAddress()
        {
            string hostName = Dns.GetHostName();
            var addresses = Dns.GetHostAddresses(hostName);
            foreach (var addr in addresses)
            {
                if (addr.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    return addr.ToString();
            }
            return "127.0.0.1";
        }
    }
}