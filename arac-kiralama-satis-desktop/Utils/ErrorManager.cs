using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Globalization;
using MySql.Data.MySqlClient;

namespace arac_kiralama_satis_desktop.Utils
{
    /// <summary>
    /// Hata türünü belirleyen enum
    /// </summary>
    public enum ErrorSeverity
    {
        /// <summary>
        /// Bilgilendirme mesajı, hata değil
        /// </summary>
        Info,

        /// <summary>
        /// Düşük öncelikli hata, sistem çalışmaya devam edebilir
        /// </summary>
        Warning,

        /// <summary>
        /// Orta öncelikli hata, işlem tamamlanamadı
        /// </summary>
        Error,

        /// <summary>
        /// Yüksek öncelikli hata, sistem kararsız olabilir
        /// </summary>
        Critical
    }

    /// <summary>
    /// Hatanın kaynağını belirleyen enum
    /// </summary>
    public enum ErrorSource
    {
        /// <summary>
        /// Veritabanı ile ilgili hatalar
        /// </summary>
        Database,

        /// <summary>
        /// Dosya sistemi ile ilgili hatalar
        /// </summary>
        FileSystem,

        /// <summary>
        /// İş mantığı ile ilgili hatalar
        /// </summary>
        Business,

        /// <summary>
        /// Ağ ile ilgili hatalar
        /// </summary>
        Network,

        /// <summary>
        /// Kullanıcı arayüzü ile ilgili hatalar
        /// </summary>
        UI,

        /// <summary>
        /// Diğer hatalar
        /// </summary>
        Other
    }

    /// <summary>
    /// Uygulama hata yönetimi sınıfı
    /// </summary>
    public class ErrorManager
    {
        #region Singleton Implementation

        private static readonly Lazy<ErrorManager> _instance = new Lazy<ErrorManager>(() => new ErrorManager());

        /// <summary>
        /// ErrorManager singleton örneğini döndürür
        /// </summary>
        public static ErrorManager Instance => _instance.Value;

        // Private constructor for singleton pattern
        private ErrorManager()
        {
            Initialize();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Log dosyasının bulunduğu klasör
        /// </summary>
        private string LogDirectory { get; set; }

        /// <summary>
        /// Hata log dosyası yolu
        /// </summary>
        private string ErrorLogPath { get; set; }

        /// <summary>
        /// İşlem log dosyası yolu
        /// </summary>
        private string InfoLogPath { get; set; }

        /// <summary>
        /// Uygulama adı
        /// </summary>
        private string ApplicationName { get; set; } = "Araç Kiralama ve Satış";

        /// <summary>
        /// Kullanıcı için hataları göster
        /// </summary>
        public bool ShowErrorsToUser { get; set; } = true;

        #endregion

        #region Initialization

        /// <summary>
        /// Log klasörünü ve dosyalarını oluşturur
        /// </summary>
        private void Initialize()
        {
            try
            {
                // Proje içindeki Logs klasörünün yolunu belirle
                string projeDizini = Application.StartupPath;

                // Projenin Logs klasörü yolunu belirle
                LogDirectory = Path.Combine(projeDizini, "Logs");

                // Eğer Logs klasörü yoksa oluştur
                if (!Directory.Exists(LogDirectory))
                {
                    Directory.CreateDirectory(LogDirectory);
                }

                // Log dosya yollarını belirle
                string currentDate = DateTime.Now.ToString("yyyyMMdd");
                ErrorLogPath = Path.Combine(LogDirectory, $"Error_{currentDate}.log");
                InfoLogPath = Path.Combine(LogDirectory, $"Info_{currentDate}.log");

                // Başlangıç mesajını logla
                LogInfo("ErrorManager başlatıldı", "ErrorManager.Initialize");
            }
            catch (Exception ex)
            {
                // Initialize sırasında hata olursa konsola yaz
                Debug.WriteLine($"ErrorManager başlatılırken hata oluştu: {ex.Message}");
            }
        }

        /* HataKayitlari tablosu için SQL Oluşturma Kodu:
        
        CREATE TABLE IF NOT EXISTS HataKayitlari (
            ID INT AUTO_INCREMENT PRIMARY KEY,
            HataID VARCHAR(50) NOT NULL,
            Tarih DATETIME NOT NULL,
            Onem VARCHAR(20) NOT NULL,
            Kaynak VARCHAR(50) NOT NULL,
            Icerik VARCHAR(255) NOT NULL,
            KullaniciAdi VARCHAR(100),
            IstisnaTuru VARCHAR(255),
            Mesaj TEXT NOT NULL,
            IcIstisna TEXT,
            YiginIzleme TEXT
        );
        
        */

        #endregion

        #region Error Handling Methods

        /// <summary>
        /// Exception'ı işler, loglar ve gerekirse kullanıcıya gösterir
        /// </summary>
        /// <param name="ex">İşlenecek exception</param>
        /// <param name="context">Hatanın gerçekleştiği bağlam</param>
        /// <param name="severity">Hatanın önemi</param>
        /// <param name="source">Hatanın kaynağı</param>
        /// <param name="showToUser">Kullanıcıya gösterilip gösterilmeyeceği</param>
        /// <returns>Hata takibi için benzersiz ID</returns>
        public string HandleException(Exception ex, string context, ErrorSeverity severity, ErrorSource source, bool showToUser = false)
        {
            if (ex == null)
                return string.Empty;

            // Benzersiz hata ID'si oluştur
            string errorId = GenerateErrorId();

            try
            {
                // Exception detaylarını hazırla
                StringBuilder errorDetails = new StringBuilder();
                errorDetails.AppendLine($"[ERROR ID: {errorId}]");
                errorDetails.AppendLine($"[TIMESTAMP: {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}]");
                errorDetails.AppendLine($"[SEVERITY: {severity}]");
                errorDetails.AppendLine($"[SOURCE: {source}]");
                errorDetails.AppendLine($"[CONTEXT: {context}]");
                errorDetails.AppendLine($"[USER: {CurrentSession.UserName ?? "Not logged in"}]");
                errorDetails.AppendLine($"[EXCEPTION TYPE: {ex.GetType().FullName}]");
                errorDetails.AppendLine($"[MESSAGE: {ex.Message}]");

                // Inner exception varsa ekle
                string innerExceptionMessage = null;
                if (ex.InnerException != null)
                {
                    innerExceptionMessage = ex.InnerException.Message;
                    errorDetails.AppendLine($"[INNER EXCEPTION: {innerExceptionMessage}]");
                }

                // Stack trace ekle
                string stackTrace = ex is DatabaseException ? ex.ToString() : ex.StackTrace;
                errorDetails.AppendLine($"[STACK TRACE:]");
                errorDetails.AppendLine(stackTrace);
                errorDetails.AppendLine(new string('-', 80));

                // Log dosyasına yaz
                WriteToLog(ErrorLogPath, errorDetails.ToString());

                // Veritabanına kaydet
                SaveToDatabase(
                    errorId,
                    DateTime.Now,
                    severity.ToString(),
                    source.ToString(),
                    context,
                    CurrentSession.UserName,
                    ex.GetType().FullName,
                    ex.Message,
                    innerExceptionMessage,
                    stackTrace
                );

                // Kullanıcıya göster
                if (showToUser && ShowErrorsToUser)
                {
                    ShowErrorMessage(ex, context, severity, errorId);
                }

                // Critical hata ise uygulamayı kapat
                if (severity == ErrorSeverity.Critical)
                {
                    MessageBox.Show(
                        "Uygulama kritik bir hata nedeniyle kapatılacak.\n\n" +
                        $"Hata ID: {errorId}\n\n" +
                        "Lütfen sistem yöneticinize bu hata ID'sini bildirin.",
                        "Kritik Hata",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Stop);

                    Application.Exit();
                }

                return errorId;
            }
            catch (Exception logEx)
            {
                // Loglama sırasında hata oluşursa konsola yaz
                Debug.WriteLine($"Hata loglanırken beklenmeyen bir hata oluştu: {logEx.Message}");
                return errorId;
            }
        }

        /// <summary>
        /// Exception'ı işler, loglar ve gerekirse kullanıcıya gösterir. Kısa form.
        /// </summary>
        public string HandleException(Exception ex, string context, ErrorSeverity severity, ErrorSource source)
        {
            return HandleException(ex, context, severity, source, ShowErrorsToUser);
        }

        #endregion

        #region Logging Methods

        /// <summary>
        /// Bilgi mesajını loglar
        /// </summary>
        /// <param name="message">Log mesajı</param>
        /// <param name="source">Mesajın kaynağı</param>
        public void LogInfo(string message, string source)
        {
            try
            {
                string logEntry = $"[INFO] [{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] [{source}] {message}";
                WriteToLog(InfoLogPath, logEntry);

                // Bilgi seviyesindeki mesajları da veritabanına kaydedelim
                string errorId = GenerateErrorId();
                SaveToDatabase(
                    errorId,
                    DateTime.Now,
                    ErrorSeverity.Info.ToString(),
                    ErrorSource.Other.ToString(),
                    source,
                    CurrentSession.UserName,
                    "Info",
                    message,
                    null,
                    null
                );
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Bilgi loglanırken hata oluştu: {ex.Message}");
            }
        }

        /// <summary>
        /// Uyarı mesajını loglar
        /// </summary>
        /// <param name="message">Uyarı mesajı</param>
        /// <param name="source">Mesajın kaynağı</param>
        public void LogWarning(string message, string source)
        {
            try
            {
                string logEntry = $"[WARNING] [{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] [{source}] {message}";
                WriteToLog(ErrorLogPath, logEntry);

                // Uyarıları da veritabanına kaydedelim
                string errorId = GenerateErrorId();
                SaveToDatabase(
                    errorId,
                    DateTime.Now,
                    ErrorSeverity.Warning.ToString(),
                    ErrorSource.Other.ToString(),
                    source,
                    CurrentSession.UserName,
                    "Warning",
                    message,
                    null,
                    null
                );
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Uyarı loglanırken hata oluştu: {ex.Message}");
            }
        }

        /// <summary>
        /// Dosyaya log yazar
        /// </summary>
        private void WriteToLog(string logPath, string content)
        {
            try
            {
                // Log dosyasına yaz (thread-safe)
                lock (this)
                {
                    using (StreamWriter writer = new StreamWriter(logPath, true, Encoding.UTF8))
                    {
                        writer.WriteLine(content);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Log dosyasına yazılırken hata oluştu: {ex.Message}");
            }
        }

        /// <summary>
        /// Hata bilgilerini veritabanına kaydeder
        /// </summary>
        private void SaveToDatabase(
            string errorId,
            DateTime timestamp,
            string severity,
            string source,
            string context,
            string username,
            string exceptionType,
            string message,
            string innerExceptionMessage,
            string stackTrace)
        {
            try
            {
                string query = @"
                INSERT INTO HataKayitlari (
                    HataID, 
                    Tarih, 
                    Onem, 
                    Kaynak, 
                    Icerik, 
                    KullaniciAdi, 
                    IstisnaTuru, 
                    Mesaj, 
                    IcIstisna, 
                    YiginIzleme
                ) VALUES (
                    @hataId, 
                    @tarih, 
                    @onem, 
                    @kaynak, 
                    @icerik, 
                    @kullaniciAdi, 
                    @istisnaTuru, 
                    @mesaj, 
                    @icIstisna, 
                    @yiginIzleme
                );";

                // DatabaseHelper kullanarak parametreleri oluştur
                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@hataId", errorId),
                    DatabaseHelper.CreateParameter("@tarih", timestamp),
                    DatabaseHelper.CreateParameter("@onem", severity),
                    DatabaseHelper.CreateParameter("@kaynak", source),
                    DatabaseHelper.CreateParameter("@icerik", context),
                    DatabaseHelper.CreateParameter("@kullaniciAdi", username),
                    DatabaseHelper.CreateParameter("@istisnaTuru", exceptionType),
                    DatabaseHelper.CreateParameter("@mesaj", message),
                    DatabaseHelper.CreateParameter("@icIstisna", innerExceptionMessage),
                    DatabaseHelper.CreateParameter("@yiginIzleme", stackTrace)
                };

                // DatabaseHelper.ExecuteNonQuery metodunu kullan
                DatabaseHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                // Veritabanına yazma başarısız olursa sadece dosyaya log ekle ve devam et
                Debug.WriteLine($"Hata veritabanına kaydedilemedi: {ex.Message}");
                WriteToLog(ErrorLogPath, $"[ERROR] Hata veritabanına kaydedilemedi: {ex.Message}");
            }
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Benzersiz hata ID'si oluşturur
        /// </summary>
        private string GenerateErrorId()
        {
            // Tarih, saat ve GUID'in ilk 8 karakterini kullanarak hata ID'si oluştur
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            string guid = Guid.NewGuid().ToString("N").Substring(0, 8);
            return $"ERR-{timestamp}-{guid}";
        }

        /// <summary>
        /// Kullanıcıya hata mesajı gösterir
        /// </summary>
        private void ShowErrorMessage(Exception ex, string context, ErrorSeverity severity, string errorId)
        {
            try
            {
                MessageBoxIcon icon;
                string title;

                switch (severity)
                {
                    case ErrorSeverity.Warning:
                        icon = MessageBoxIcon.Warning;
                        title = "Uyarı";
                        break;
                    case ErrorSeverity.Error:
                        icon = MessageBoxIcon.Error;
                        title = "Hata";
                        break;
                    case ErrorSeverity.Critical:
                        icon = MessageBoxIcon.Stop;
                        title = "Kritik Hata";
                        break;
                    default:
                        icon = MessageBoxIcon.Information;
                        title = "Bilgi";
                        break;
                }

                string message = $"{context}\n\n{ex.Message}\n\nHata ID: {errorId}";

                MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
            }
            catch
            {
                // MessageBox gösterilirken hata olursa sessizce geç
            }
        }

        #endregion
    }
}