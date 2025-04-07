using System;

namespace arac_kiralama_satis_desktop.Utils
{
    public static class CurrentSession
    {
        public static int UserID { get; set; }
        public static string FullName { get; set; }
        public static string UserName { get; set; }
        public static int RoleID { get; set; }
        public static string RoleName { get; set; }
        public static int? BranchID { get; set; }
        public static string BranchName { get; set; }

        // Yeni eklenen özellikler
        public static DateTime LoginTime { get; set; } = DateTime.MinValue;
        public static DateTime LastActivityTime { get; set; } = DateTime.MinValue;
        public static bool IsLoggedIn { get; set; } = false;

        /// <summary>
        /// Kullanıcı oturumunu temizler
        /// </summary>
        public static void ClearSession()
        {
            UserID = 0;
            FullName = string.Empty;
            UserName = string.Empty;
            RoleID = 0;
            RoleName = string.Empty;
            BranchID = null;
            BranchName = string.Empty;
            LoginTime = DateTime.MinValue;
            LastActivityTime = DateTime.MinValue;
            IsLoggedIn = false;
        }

        /// <summary>
        /// Kullanıcının giriş yaptığı andan itibaren geçen süreyi hesaplar
        /// </summary>
        /// <returns>Oturum süresi</returns>
        public static TimeSpan GetSessionDuration()
        {
            if (!IsLoggedIn || LoginTime == DateTime.MinValue)
                return TimeSpan.Zero;

            return DateTime.Now - LoginTime;
        }

        /// <summary>
        /// Kullanıcının son aktivite zamanını günceller
        /// </summary>
        public static void UpdateLastActivity()
        {
            LastActivityTime = DateTime.Now;
        }

        /// <summary>
        /// Kullanıcı oturumunun zaman aşımına uğrayıp uğramadığını kontrol eder
        /// </summary>
        /// <param name="timeoutMinutes">Zaman aşımı süresi (dakika)</param>
        /// <returns>Zaman aşımına uğradıysa true, aksi halde false</returns>
        public static bool IsSessionTimedOut(int timeoutMinutes)
        {
            if (!IsLoggedIn || LastActivityTime == DateTime.MinValue)
                return false;

            return (DateTime.Now - LastActivityTime).TotalMinutes > timeoutMinutes;
        }
    }
}