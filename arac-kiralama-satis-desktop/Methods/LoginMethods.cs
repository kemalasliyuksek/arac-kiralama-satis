using System;
using System.Data;
using System.Net;
using arac_kiralama_satis_desktop.Utils;
using arac_kiralama_satis_desktop.Repositories;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace arac_kiralama_satis_desktop.Methods
{
    public class LoginMethods
    {
        private static readonly LoginRepository _repository = new LoginRepository();

        public static bool VerifyLogin(string username, string password)
        {
            try
            {
                return _repository.VerifyLogin(username, password);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Giriş kontrolü sırasında hata: " + ex.Message,
                                "Doğrulama Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static DataRow GetUserInfo(string username)
        {
            try
            {
                return _repository.GetUserInfo(username);
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcı bilgisi alınırken bir hata oluştu: " + ex.Message);
            }
        }

        public static void UpdateLastLogin(int userID)
        {
            _repository.UpdateLastLogin(userID);
        }

        public static void LogLoginAttempt(int? userID, bool success, string ipAddress)
        {
            _repository.LogLoginAttempt(userID, success, ipAddress);
        }

        public static string GetIPAddress()
        {
            return _repository.GetIPAddress();
        }

        public static void SaveUserCredentials(string username)
        {
            UserSettings.Current.SavedUsername = username;
            UserSettings.Current.RememberMe = true;
            UserSettings.Save();
        }

        public static string LoadSavedUsername()
        {
            if (UserSettings.Current.RememberMe)
            {
                return UserSettings.Current.SavedUsername;
            }
            return string.Empty;
        }

        public static void ClearSavedCredentials()
        {
            UserSettings.Current.SavedUsername = "";
            UserSettings.Current.RememberMe = false;
            UserSettings.Save();
        }
    }
}