using System;
using System.Data;
using System.Net;
using arac_kiralama_satis_desktop.Utils;
using arac_kiralama_satis_desktop.Repositories;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace arac_kiralama_satis_desktop.Methods
{
    /// <summary>
    /// Kullanıcı girişi ile ilgili işlemleri yöneten sınıf
    /// </summary>
    public class LoginMethods
    {
        private static readonly LoginRepository _repository = new LoginRepository();

        /// <summary>
        /// Kullanıcı adı ve şifre doğrulaması yapar
        /// </summary>
        /// <param name="username">Kullanıcı adı</param>
        /// <param name="password">Şifre</param>
        /// <returns>Doğrulama başarılı ise true, değilse false</returns>
        public static bool VerifyLogin(string username, string password)
        {
            try
            {
                bool result = _repository.VerifyLogin(username, password);

                // Giriş sonucunu bilgi olarak logla
                ErrorManager.Instance.LogInfo(
                    result
                        ? $"Kullanıcı girişi başarılı: {username}"
                        : $"Kullanıcı girişi başarısız: {username}",
                    "LoginAttempt");

                return result;
            }
            catch (MySqlException ex)
            {
                // Veritabanı hatası
                ErrorManager.Instance.HandleException(
                    ex,
                    $"Kullanıcı '{username}' için giriş doğrulanırken veritabanı hatası oluştu",
                    ErrorSeverity.Critical,
                    ErrorSource.Database,
                    true);

                return false;
            }
            catch (Exception ex)
            {
                // Genel hata
                ErrorManager.Instance.HandleException(
                    ex,
                    $"Kullanıcı '{username}' için giriş doğrulanırken beklenmeyen bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Business,
                    true);

                return false;
            }
        }

        /// <summary>
        /// Kullanıcı bilgilerini getirir
        /// </summary>
        /// <param name="username">Kullanıcı adı</param>
        /// <returns>Kullanıcı bilgilerini içeren DataRow</returns>
        public static DataRow GetUserInfo(string username)
        {
            try
            {
                DataRow result = _repository.GetUserInfo(username);
                // Bilgi olarak logla
                ErrorManager.Instance.LogInfo($"Kullanıcı bilgileri alındı: {username}", "UserInfo");
                return result;
            }
            catch (MySqlException ex)
            {
                // Veritabanı hatası
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Kullanıcı '{username}' bilgisi alınırken veritabanı hatası oluştu: {ex.Number}",
                    ErrorSeverity.Error,
                    ErrorSource.Database,
                    false);

                throw new InvalidOperationException($"Kullanıcı bilgisi alınırken veritabanı hatası oluştu (Hata ID: {errorId})", ex);
            }
            catch (Exception ex)
            {
                // Genel hata
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Kullanıcı '{username}' bilgisi alınırken beklenmeyen bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Business,
                    false);

                throw new InvalidOperationException($"Kullanıcı bilgisi alınırken bir hata oluştu (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// Kullanıcının son giriş zamanını günceller
        /// </summary>
        /// <param name="userID">Kullanıcı ID</param>
        public static void UpdateLastLogin(int userID)
        {
            try
            {
                _repository.UpdateLastLogin(userID);
                // Bilgi olarak logla
                ErrorManager.Instance.LogInfo($"Kullanıcı ID:{userID} için son giriş zamanı güncellendi", "LoginUpdate");
            }
            catch (Exception ex)
            {
                // Kritik olmayan ancak loglanması gereken bir hata
                ErrorManager.Instance.HandleException(
                    ex,
                    $"Kullanıcı ID:{userID} için son giriş zamanı güncellenirken hata oluştu",
                    ErrorSeverity.Warning,
                    ErrorSource.Database,
                    false);

                // Uygulamanın çalışmasını etkilememesi için hatayı yukarı fırlatmıyoruz
            }
        }

        /// <summary>
        /// Giriş denemesini loglar - ErrorManager kullanarak
        /// </summary>
        /// <param name="userID">Kullanıcı ID (başarısız girişte null olabilir)</param>
        /// <param name="success">Giriş başarılı mı?</param>
        /// <param name="ipAddress">IP adresi</param>
        public static void LogLoginAttempt(int? userID, bool success, string ipAddress)
        {
            try
            {
                // Veritabanı logu yerine ErrorManager ile bilgi olarak logla
                string logMessage = success
                    ? $"Başarılı giriş: Kullanıcı ID:{userID}, IP:{ipAddress}"
                    : $"Başarısız giriş denemesi: {(userID.HasValue ? $"Kullanıcı ID:{userID}" : "Bilinmeyen kullanıcı")}, IP:{ipAddress}";

                // Tüm giriş bilgilerini (başarılı/başarısız) Info seviyesinde logla
                ErrorManager.Instance.LogInfo(logMessage, "LoginAttempt");
            }
            catch (Exception ex)
            {
                // Loglama sırasında hata oluşursa
                ErrorManager.Instance.HandleException(
                    ex,
                    $"Giriş bilgisi loglanırken hata oluştu",
                    ErrorSeverity.Warning,
                    ErrorSource.Other,
                    false);
            }
        }

        /// <summary>
        /// Kullanıcının IP adresini getirir
        /// </summary>
        /// <returns>IP adresi</returns>
        public static string GetIPAddress()
        {
            try
            {
                string ip = _repository.GetIPAddress();
                ErrorManager.Instance.LogInfo($"IP adresi alındı: {ip}", "NetworkInfo");
                return ip;
            }
            catch (Exception ex)
            {
                // Ağ hatası
                ErrorManager.Instance.HandleException(
                    ex,
                    $"IP adresi alınırken hata oluştu: {ex.Message}",
                    ErrorSeverity.Warning,
                    ErrorSource.Network,
                    false);

                // Hata durumunda localhost döndür
                return "127.0.0.1";
            }
        }

        /// <summary>
        /// Kullanıcı bilgilerini kaydeder
        /// </summary>
        /// <param name="username">Kullanıcı adı</param>
        public static void SaveUserCredentials(string username)
        {
            try
            {
                UserSettings.Current.SavedUsername = username;
                UserSettings.Current.RememberMe = true;
                UserSettings.Save();

                ErrorManager.Instance.LogInfo($"Kullanıcı bilgileri kaydedildi: {username}", "UserSettings");
            }
            catch (Exception ex)
            {
                // Dosya işlemi hatası
                ErrorManager.Instance.HandleException(
                    ex,
                    $"Kullanıcı '{username}' bilgileri kaydedilirken dosya hatası oluştu",
                    ErrorSeverity.Warning,
                    ErrorSource.FileSystem,
                    false);
            }
        }

        /// <summary>
        /// Kaydedilmiş kullanıcı adını getirir
        /// </summary>
        /// <returns>Kaydedilmiş kullanıcı adı veya boş string</returns>
        public static string LoadSavedUsername()
        {
            try
            {
                if (UserSettings.Current.RememberMe)
                {
                    string username = UserSettings.Current.SavedUsername;
                    ErrorManager.Instance.LogInfo($"Kaydedilmiş kullanıcı adı yüklendi: {username}", "UserSettings");
                    return username;
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                // Dosya işlemi hatası
                ErrorManager.Instance.HandleException(
                    ex,
                    "Kaydedilmiş kullanıcı bilgileri okunurken dosya hatası oluştu",
                    ErrorSeverity.Warning,
                    ErrorSource.FileSystem,
                    false);

                return string.Empty;
            }
        }

        /// <summary>
        /// Kaydedilmiş kullanıcı bilgilerini temizler
        /// </summary>
        public static void ClearSavedCredentials()
        {
            try
            {
                UserSettings.Current.SavedUsername = "";
                UserSettings.Current.RememberMe = false;
                UserSettings.Save();

                ErrorManager.Instance.LogInfo("Kaydedilmiş kullanıcı bilgileri temizlendi", "UserSettings");
            }
            catch (Exception ex)
            {
                // Dosya işlemi hatası
                ErrorManager.Instance.HandleException(
                    ex,
                    "Kaydedilmiş kullanıcı bilgileri temizlenirken dosya hatası oluştu",
                    ErrorSeverity.Warning,
                    ErrorSource.FileSystem,
                    false);
            }
        }
    }
}