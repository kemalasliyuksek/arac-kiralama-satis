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

        public static bool Login(string username, string password)
        {
            try
            {
                bool isValid = _repository.VerifyLogin(username, password);

                if (!isValid)
                {
                    string ipAddress = GetIPAddress();
                    ErrorManager.Instance.LogWarning(
                        $"Başarısız giriş denemesi: {username}, IP: {ipAddress}",
                        "Login.Failed");
                    return false;
                }

                DataRow userInfo = GetUserInfo(username);
                if (userInfo == null)
                {
                    string ipAddress = GetIPAddress();
                    ErrorManager.Instance.LogWarning(
                        $"Kullanıcı girişi doğrulandı fakat bilgiler alınamadı: {username}, IP: {ipAddress}",
                        "Login.UserInfoMissing");
                    return false;
                }

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

                UpdateLastLogin(CurrentSession.UserID);

                string ipAddr = GetIPAddress();
                ErrorManager.Instance.LogInfo(
                    $"Başarılı giriş: {username}, Rol: {CurrentSession.RoleName}, IP: {ipAddr}",
                    "Login.Success");

                return true;
            }
            catch (MySqlException ex)
            {
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
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Kullanıcı '{username}' için giriş yapılırken beklenmeyen bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Business,
                    true);

                return false;
            }
        }

        public static void Logout()
        {
            try
            {
                if (CurrentSession.IsLoggedIn)
                {
                    ErrorManager.Instance.LogInfo(
                        $"Kullanıcı çıkış yaptı: {CurrentSession.UserName}, Oturum süresi: {CurrentSession.GetSessionDuration()}",
                        "Logout.Success");

                    CurrentSession.ClearSession();
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.HandleException(
                    ex,
                    "Oturum kapatılırken beklenmeyen bir hata oluştu",
                    ErrorSeverity.Warning,
                    ErrorSource.Business,
                    true);
            }
        }

        public static bool VerifyLogin(string username, string password)
        {
            try
            {
                bool result = _repository.VerifyLogin(username, password);

                ErrorManager.Instance.LogInfo(
                    result
                        ? $"Kullanıcı girişi doğrulandı: {username}"
                        : $"Kullanıcı girişi doğrulanamadı: {username}",
                    "LoginVerification");

                return result;
            }
            catch (MySqlException ex)
            {
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
                ErrorManager.Instance.HandleException(
                    ex,
                    $"Kullanıcı '{username}' için giriş doğrulanırken beklenmeyen bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Business,
                    true);

                return false;
            }
        }

        public static DataRow GetUserInfo(string username)
        {
            try
            {
                DataRow result = _repository.GetUserInfo(username);

                if (result != null)
                {
                    ErrorManager.Instance.LogInfo($"Kullanıcı bilgileri alındı: {username}", "UserInfo");
                }
                else
                {
                    ErrorManager.Instance.LogWarning($"Kullanıcı bilgileri bulunamadı: {username}", "UserInfo.NotFound");
                }

                return result;
            }
            catch (MySqlException ex)
            {
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
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Kullanıcı '{username}' bilgisi alınırken beklenmeyen bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Business,
                    false);

                throw new InvalidOperationException($"Kullanıcı bilgisi alınırken bir hata oluştu (Hata ID: {errorId})", ex);
            }
        }

        public static void UpdateLastLogin(int userID)
        {
            try
            {
                _repository.UpdateLastLogin(userID);
                ErrorManager.Instance.LogInfo($"Kullanıcı ID:{userID} için son giriş zamanı güncellendi", "LoginUpdate");
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.HandleException(
                    ex,
                    $"Kullanıcı ID:{userID} için son giriş zamanı güncellenirken hata oluştu",
                    ErrorSeverity.Warning,
                    ErrorSource.Database,
                    false);
            }
        }

        public static void LogLoginAttempt(int? userID, bool success, string ipAddress)
        {
            try
            {
                string logMessage = success
                    ? $"Başarılı giriş: Kullanıcı ID:{userID}, IP:{ipAddress}"
                    : $"Başarısız giriş denemesi: {(userID.HasValue ? $"Kullanıcı ID:{userID}" : "Bilinmeyen kullanıcı")}, IP:{ipAddress}";

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
                ErrorManager.Instance.HandleException(
                    ex,
                    $"Giriş bilgisi loglanırken hata oluştu",
                    ErrorSeverity.Warning,
                    ErrorSource.Other,
                    false);
            }
        }

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
                ErrorManager.Instance.HandleException(
                    ex,
                    $"IP adresi alınırken hata oluştu: {ex.Message}",
                    ErrorSeverity.Warning,
                    ErrorSource.Network,
                    false);

                return "127.0.0.1";
            }
        }

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
                ErrorManager.Instance.HandleException(
                    ex,
                    $"Kullanıcı '{username}' bilgileri kaydedilirken dosya hatası oluştu",
                    ErrorSeverity.Warning,
                    ErrorSource.FileSystem,
                    false);
            }
        }

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
                ErrorManager.Instance.HandleException(
                    ex,
                    "Kaydedilmiş kullanıcı bilgileri okunurken dosya hatası oluştu",
                    ErrorSeverity.Warning,
                    ErrorSource.FileSystem,
                    false);

                return string.Empty;
            }
        }

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
                ErrorManager.Instance.HandleException(
                    ex,
                    "Kaydedilmiş kullanıcı bilgileri temizlenirken dosya hatası oluştu",
                    ErrorSeverity.Warning,
                    ErrorSource.FileSystem,
                    false);
            }
        }

        public static bool CheckSessionTimeout(int timeoutMinutes)
        {
            if (!CurrentSession.IsLoggedIn)
                return false;

            if (CurrentSession.IsSessionTimedOut(timeoutMinutes))
            {
                ErrorManager.Instance.LogWarning(
                    $"Oturum zaman aşımı: {CurrentSession.UserName}, Süre: {timeoutMinutes} dakika",
                    "Session.Timeout");
                Logout();
                return true;
            }

            return false;
        }

        public static void UpdateLastActivity()
        {
            if (CurrentSession.IsLoggedIn)
            {
                CurrentSession.UpdateLastActivity();
            }
        }

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