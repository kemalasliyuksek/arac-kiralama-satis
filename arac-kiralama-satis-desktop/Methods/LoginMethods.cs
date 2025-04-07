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
        /// Kullanıcı adı ve şifre doğrulaması yaparak oturum açar
        /// </summary>
        /// <param name="username">Kullanıcı adı</param>
        /// <param name="password">Şifre</param>
        /// <returns>Giriş başarılı ise true, değilse false</returns>
        public static bool Login(string username, string password)
        {
            try
            {
                // Giriş bilgilerini doğrula
                bool isValid = _repository.VerifyLogin(username, password);

                if (!isValid)
                {
                    // Başarısız giriş
                    string ipAddress = GetIPAddress();
                    ErrorManager.Instance.LogWarning(
                        $"Başarısız giriş denemesi: {username}, IP: {ipAddress}",
                        "Login.Failed");
                    return false;
                }

                // Kullanıcı bilgilerini getir
                DataRow userInfo = GetUserInfo(username);
                if (userInfo == null)
                {
                    // Kullanıcı bilgisi alınamadı
                    string ipAddress = GetIPAddress();
                    ErrorManager.Instance.LogWarning(
                        $"Kullanıcı girişi doğrulandı fakat bilgiler alınamadı: {username}, IP: {ipAddress}",
                        "Login.UserInfoMissing");
                    return false;
                }

                // Oturum bilgilerini ayarla
                CurrentSession.UserID = Convert.ToInt32(userInfo["KullaniciID"]);
                CurrentSession.UserName = username;
                CurrentSession.FullName = $"{userInfo["Ad"]} {userInfo["Soyad"]}";
                CurrentSession.RoleID = Convert.ToInt32(userInfo["RolID"]);
                CurrentSession.RoleName = userInfo["RolAdi"].ToString();
                CurrentSession.BranchID = userInfo["SubeID"] != DBNull.Value ? Convert.ToInt32(userInfo["SubeID"]) : (int?)null;
                CurrentSession.BranchName = userInfo["SubeAdi"]?.ToString();
                CurrentSession.LoginTime = DateTime.Now;
                CurrentSession.LastActivityTime = DateTime.Now;
                CurrentSession.IsLoggedIn = true;

                // Son giriş zamanını güncelle
                UpdateLastLogin(CurrentSession.UserID);

                // Başarılı girişi logla
                string ipAddr = GetIPAddress();
                ErrorManager.Instance.LogInfo(
                    $"Başarılı giriş: {username}, Rol: {CurrentSession.RoleName}, IP: {ipAddr}",
                    "Login.Success");

                return true;
            }
            catch (MySqlException ex)
            {
                // Veritabanı hatası
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Kullanıcı '{username}' için giriş yapılırken veritabanı hatası oluştu: {ex.Number}",
                    ErrorSeverity.Error,
                    ErrorSource.Database,
                    true);

                return false;
            }
            catch (Exception ex)
            {
                // Genel hata
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Kullanıcı '{username}' için giriş yapılırken beklenmeyen bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Business,
                    true);

                return false;
            }
        }

        /// <summary>
        /// Kullanıcının oturumunu sonlandırır
        /// </summary>
        public static void Logout()
        {
            try
            {
                if (CurrentSession.IsLoggedIn)
                {
                    // Oturum kapatma bilgilerini logla
                    ErrorManager.Instance.LogInfo(
                        $"Kullanıcı çıkış yaptı: {CurrentSession.UserName}, Oturum süresi: {CurrentSession.GetSessionDuration()}",
                        "Logout.Success");

                    // Oturum bilgilerini temizle
                    CurrentSession.ClearSession();
                }
            }
            catch (Exception ex)
            {
                // Genel hata
                ErrorManager.Instance.HandleException(
                    ex,
                    "Oturum kapatılırken beklenmeyen bir hata oluştu",
                    ErrorSeverity.Warning,
                    ErrorSource.Business,
                    true);
            }
        }

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
                        ? $"Kullanıcı girişi doğrulandı: {username}"
                        : $"Kullanıcı girişi doğrulanamadı: {username}",
                    "LoginVerification");

                return result;
            }
            catch (MySqlException ex)
            {
                // Veritabanı hatası
                ErrorManager.Instance.HandleException(
                    ex,
                    $"Kullanıcı '{username}' için giriş doğrulanırken veritabanı hatası oluştu",
                    ErrorSeverity.Error,
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

                if (result != null)
                {
                    // Bilgi olarak logla
                    ErrorManager.Instance.LogInfo($"Kullanıcı bilgileri alındı: {username}", "UserInfo");
                }
                else
                {
                    // Uyarı olarak logla
                    ErrorManager.Instance.LogWarning($"Kullanıcı bilgileri bulunamadı: {username}", "UserInfo.NotFound");
                }

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
        /// Giriş denemesini loglar - sadece ErrorManager kullanarak
        /// </summary>
        /// <param name="userID">Kullanıcı ID (başarısız girişte null olabilir)</param>
        /// <param name="success">Giriş başarılı mı?</param>
        /// <param name="ipAddress">IP adresi</param>
        public static void LogLoginAttempt(int? userID, bool success, string ipAddress)
        {
            try
            {
                // ErrorManager ile bilgi olarak logla
                string logMessage = success
                    ? $"Başarılı giriş: Kullanıcı ID:{userID}, IP:{ipAddress}"
                    : $"Başarısız giriş denemesi: {(userID.HasValue ? $"Kullanıcı ID:{userID}" : "Bilinmeyen kullanıcı")}, IP:{ipAddress}";

                // Giriş bilgilerini başarıya göre Info veya Warning olarak logla
                if (success)
                {
                    ErrorManager.Instance.LogInfo(logMessage, "LoginAttempt.Success");
                }
                else
                {
                    ErrorManager.Instance.LogWarning(logMessage, "LoginAttempt.Failed");
                }
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

        /// <summary>
        /// Oturum zaman aşımını kontrol eder
        /// </summary>
        /// <param name="timeoutMinutes">Zaman aşımı süresi (dakika)</param>
        /// <returns>Oturum zaman aşımına uğradıysa true, değilse false</returns>
        public static bool CheckSessionTimeout(int timeoutMinutes)
        {
            if (!CurrentSession.IsLoggedIn)
                return false;

            if (CurrentSession.IsSessionTimedOut(timeoutMinutes))
            {
                // Oturum zaman aşımına uğradı
                ErrorManager.Instance.LogWarning(
                    $"Oturum zaman aşımı: {CurrentSession.UserName}, Süre: {timeoutMinutes} dakika",
                    "Session.Timeout");
                Logout();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Kullanıcının son aktivite zamanını günceller
        /// </summary>
        public static void UpdateLastActivity()
        {
            if (CurrentSession.IsLoggedIn)
            {
                CurrentSession.UpdateLastActivity();
            }
        }

        /// <summary>
        /// Kullanıcının belirli bir işlem için yetkisi olup olmadığını kontrol eder
        /// </summary>
        /// <param name="requiredPermission">Gerekli yetki kodu</param>
        /// <returns>Yetkisi varsa true, yoksa false</returns>
        public static bool HasPermission(string requiredPermission)
        {
            try
            {
                if (!CurrentSession.IsLoggedIn)
                {
                    ErrorManager.Instance.LogWarning(
                        $"Oturum açılmadan yetki kontrolü yapıldı: {requiredPermission}",
                        "Permission.NotLoggedIn");
                    return false;
                }

                // Burada yetki kontrolü yapılabilir
                // Örnek: veritabanından yetki kontrolü
                // bool hasPermission = _repository.CheckPermission(CurrentSession.UserID, requiredPermission);

                // Şimdilik sadece admin rolünün tüm yetkilere sahip olduğunu varsayalım
                bool hasPermission = CurrentSession.RoleName == "Admin";

                if (!hasPermission)
                {
                    ErrorManager.Instance.LogWarning(
                        $"Yetkisiz erişim girişimi: Kullanıcı: {CurrentSession.UserName}, Rol: {CurrentSession.RoleName}, İstenen yetki: {requiredPermission}",
                        "Permission.Denied");
                }

                return hasPermission;
            }
            catch (Exception ex)
            {
                // Genel hata
                ErrorManager.Instance.HandleException(
                    ex,
                    $"Yetki kontrolü sırasında hata oluştu: {requiredPermission}",
                    ErrorSeverity.Warning,
                    ErrorSource.Business,
                    false);

                return false;
            }
        }
    }
}