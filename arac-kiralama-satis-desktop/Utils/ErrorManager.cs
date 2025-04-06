using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MySql.Data.MySqlClient;

namespace arac_kiralama_satis_desktop.Utils
{
    /// <summary>
    /// Hata seviyelerini tanımlayan enum
    /// </summary>
    public enum ErrorSeverity
    {
        /// <summary>
        /// Kritik hatalar - Uygulamanın çalışmasını engelleyecek durumlar
        /// </summary>
        Critical = 0,

        /// <summary>
        /// Standart hatalar - İşlemin tamamlanmasını engelleyen durumlar
        /// </summary>
        Error = 1,

        /// <summary>
        /// Uyarılar - İşlem tamamlandı ancak kullanıcının bilmesi gereken durumlar
        /// </summary>
        Warning = 2,

        /// <summary>
        /// Bilgi - Sadece bilgilendirme amaçlı
        /// </summary>
        Info = 3
    }

    /// <summary>
    /// Hata kaynağını belirten enum
    /// </summary>
    public enum ErrorSource
    {
        /// <summary>
        /// Veritabanı ile ilgili hatalar
        /// </summary>
        Database,

        /// <summary>
        /// Kullanıcı arayüzü ile ilgili hatalar
        /// </summary>
        UI,

        /// <summary>
        /// İş mantığı ile ilgili hatalar
        /// </summary>
        Business,

        /// <summary>
        /// Doğrulama hataları
        /// </summary>
        Validation,

        /// <summary>
        /// Ağ ile ilgili hatalar
        /// </summary>
        Network,

        /// <summary>
        /// Dosya işlemleri ile ilgili hatalar
        /// </summary>
        FileSystem,

        /// <summary>
        /// Diğer hatalar
        /// </summary>
        Other
    }

    /// <summary>
    /// Uygulama genelinde hata yönetimini sağlayan merkezi sınıf
    /// </summary>
    public class ErrorManager
    {
        #region Singleton

        private static readonly Lazy<ErrorManager> _instance = new Lazy<ErrorManager>(() => new ErrorManager());

        /// <summary>
        /// ErrorManager singleton instance'ını döndürür
        /// </summary>
        public static ErrorManager Instance => _instance.Value;

        /// <summary>
        /// Private constructor for singleton pattern
        /// </summary>
        private ErrorManager()
        {
            // Log klasörünü oluştur
            EnsureLogDirectoryExists();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Hata mesajlarının kaydedileceği klasör yolu
        /// </summary>
        public string LogDirectory { get; set; } = Path.Combine(Application.StartupPath, "Logs");

        /// <summary>
        /// Hataların veritabanına da kaydedilip kaydedilmeyeceği
        /// </summary>
        public bool LogToDatabase { get; set; } = true;

        /// <summary>
        /// Kritik hatalarda e-posta bildiriminin gönderileceği adres
        /// </summary>
        public string ErrorNotificationEmail { get; set; } = "";

        /// <summary>
        /// Kritik hatalar için e-posta bildirimi gönderilsin mi?
        /// </summary>
        public bool SendEmailNotifications { get; set; } = false;

        /// <summary>
        /// Veritabanı hatalarında son çalıştırılan SQL sorgusu ve parametreleri gösterilsin mi?
        /// </summary>
        public bool ShowSqlInErrorMessage { get; set; } = false;

        /// <summary>
        /// Hata mesajlarında teknik detaylar gösterilsin mi?
        /// </summary>
        public bool ShowTechnicalDetails { get; set; } = true;

        #endregion

        #region Public Methods

        /// <summary>
        /// Exception'ı işleyerek loglama ve bildirim işlemlerini yapar
        /// </summary>
        /// <param name="ex">Yakalanan exception</param>
        /// <param name="context">Hatanın oluştuğu bağlam bilgisi</param>
        /// <param name="severity">Hatanın önem seviyesi</param>
        /// <param name="source">Hatanın kaynağı</param>
        /// <param name="showMessage">Kullanıcıya mesaj gösterilsin mi?</param>
        /// <param name="callerMemberName">Hatanın oluştuğu metod adı (otomatik olarak doldurulur)</param>
        /// <param name="callerFilePath">Hatanın oluştuğu dosya yolu (otomatik olarak doldurulur)</param>
        /// <param name="callerLineNumber">Hatanın oluştuğu satır numarası (otomatik olarak doldurulur)</param>
        /// <returns>Hata ID'si (log kayıt numarası)</returns>
        public string HandleException(
            Exception ex,
            string context = "",
            ErrorSeverity severity = ErrorSeverity.Error,
            ErrorSource source = ErrorSource.Other,
            bool showMessage = true,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            if (ex == null) return string.Empty;

            try
            {
                // Benzersiz bir hata ID'si oluştur
                string errorId = GenerateErrorId();

                // Hata bilgilerini çıkart
                ErrorInfo errorInfo = new ErrorInfo
                {
                    ErrorId = errorId,
                    Exception = ex,
                    Context = context,
                    Severity = severity,
                    Source = source,
                    CallerMemberName = callerMemberName,
                    CallerFilePath = callerFilePath,
                    CallerLineNumber = callerLineNumber,
                    Timestamp = DateTime.Now,
                    AdditionalData = new Dictionary<string, object>()
                };

                // Veritabanı hatası için özel işlem
                if (ex is MySqlException mysqlEx)
                {
                    errorInfo.AdditionalData["MySqlErrorCode"] = mysqlEx.Number;
                }

                // DatabaseException için özel işlem
                if (ex is DatabaseException dbEx)
                {
                    errorInfo.AdditionalData["Query"] = dbEx.Query;
                    errorInfo.AdditionalData["Parameters"] = dbEx.Parameters;
                }

                // Hatayı logla
                LogError(errorInfo);

                // Kullanıcıya göster
                if (showMessage)
                {
                    ShowErrorToUser(errorInfo);
                }

                // Kritik hatalar için ek işlemler
                if (severity == ErrorSeverity.Critical)
                {
                    HandleCriticalError(errorInfo);
                }

                return errorId;
            }
            catch (Exception logEx)
            {
                // Loglama sırasında bir hata oluşursa, son çare olarak konsola yaz
                Console.WriteLine($"Error logging failed: {logEx.Message}");
                Debug.WriteLine($"Error logging failed: {logEx.Message}");

                // Hata mesajını göster
                if (showMessage)
                {
                    MessageBox.Show(
                        $"Uygulama bir hata ile karşılaştı ve bu hata kaydedilemedi.\n\nHata: {ex.Message}\n\nLoglama Hatası: {logEx.Message}",
                        "Kritik Hata",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Doğrulama hatalarını işler (genellikle kullanıcı girdilerinin doğrulanması)
        /// </summary>
        /// <param name="errorMessage">Hata mesajı</param>
        /// <param name="showMessage">Mesaj gösterilsin mi?</param>
        /// <returns>Hata ID'si</returns>
        public string HandleValidationError(string errorMessage, bool showMessage = true)
        {
            if (string.IsNullOrEmpty(errorMessage)) return string.Empty;

            try
            {
                string errorId = GenerateErrorId();

                ErrorInfo errorInfo = new ErrorInfo
                {
                    ErrorId = errorId,
                    Exception = new ValidationException(errorMessage),
                    Context = "Veri doğrulama hatası",
                    Severity = ErrorSeverity.Warning,
                    Source = ErrorSource.Validation,
                    Timestamp = DateTime.Now
                };

                // Log sadece uyarı olarak
                LogError(errorInfo);

                // Kullanıcıya göster
                if (showMessage)
                {
                    MessageBox.Show(
                        errorMessage,
                        "Veri Doğrulama Hatası",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }

                return errorId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Validation error handling failed: {ex.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        /// Belirli bir işlem için doğrulama sonucunu kontrol eder
        /// </summary>
        /// <param name="isValid">Doğrulama sonucu</param>
        /// <param name="errorMessage">Hata durumunda gösterilecek mesaj</param>
        /// <param name="showMessage">Mesaj gösterilsin mi?</param>
        /// <returns>Doğrulama sonucu</returns>
        public bool ValidateOperation(bool isValid, string errorMessage, bool showMessage = true)
        {
            if (!isValid)
            {
                HandleValidationError(errorMessage, showMessage);
            }
            return isValid;
        }

        /// <summary>
        /// Bilgi mesajı (log) kaydeder
        /// </summary>
        /// <param name="message">Mesaj</param>
        /// <param name="source">Kaynağı</param>
        public void LogInfo(string message, string source = "")
        {
            try
            {
                string logFile = Path.Combine(LogDirectory, $"info_{DateTime.Now:yyyyMMdd}.log");
                string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{source}] {message}{Environment.NewLine}";
                File.AppendAllText(logFile, logEntry);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Info logging failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Kullanıcıya bir bilgi mesajı gösterir ve opsiyonel olarak loglar
        /// </summary>
        /// <param name="message">Gösterilecek mesaj</param>
        /// <param name="title">Mesaj başlığı</param>
        /// <param name="logMessage">Mesaj loglansın mı?</param>
        public void ShowInfo(string message, string title = "Bilgi", bool logMessage = true)
        {
            if (logMessage)
            {
                LogInfo(message, title);
            }

            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Kullanıcıya bir uyarı mesajı gösterir ve opsiyonel olarak loglar
        /// </summary>
        /// <param name="message">Gösterilecek mesaj</param>
        /// <param name="title">Mesaj başlığı</param>
        /// <param name="logMessage">Mesaj loglansın mı?</param>
        public void ShowWarning(string message, string title = "Uyarı", bool logMessage = true)
        {
            if (logMessage)
            {
                LogWarning(message, title);
            }

            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Uyarı mesajı (log) kaydeder
        /// </summary>
        /// <param name="message">Mesaj</param>
        /// <param name="source">Kaynağı</param>
        public void LogWarning(string message, string source = "")
        {
            try
            {
                string logFile = Path.Combine(LogDirectory, $"warning_{DateTime.Now:yyyyMMdd}.log");
                string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{source}] {message}{Environment.NewLine}";
                File.AppendAllText(logFile, logEntry);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning logging failed: {ex.Message}");
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Benzersiz bir hata ID'si oluşturur
        /// </summary>
        /// <returns>Hata ID'si</returns>
        private string GenerateErrorId()
        {
            return $"{DateTime.Now:yyyyMMdd}_{DateTime.Now:HHmmss}_{Guid.NewGuid().ToString().Substring(0, 8)}";
        }

        /// <summary>
        /// Hata bilgisini loglar
        /// </summary>
        /// <param name="errorInfo">Hata bilgisi</param>
        private void LogError(ErrorInfo errorInfo)
        {
            // Dosyaya loglama
            LogErrorToFile(errorInfo);

            // Veritabanına loglama (opsiyonel)
            if (LogToDatabase)
            {
                LogErrorToDatabase(errorInfo);
            }

            // Ayıklama modunda ise konsola ve Debug.WriteLine'a da yaz
#if DEBUG
            Console.WriteLine($"ERROR: [{errorInfo.ErrorId}] {errorInfo.Exception.Message}");
            Debug.WriteLine($"ERROR: [{errorInfo.ErrorId}] {errorInfo.Exception.Message}");
#endif
        }

        /// <summary>
        /// Hata bilgisini dosyaya loglar
        /// </summary>
        /// <param name="errorInfo">Hata bilgisi</param>
        private void LogErrorToFile(ErrorInfo errorInfo)
        {
            try
            {
                // Log klasörünün var olduğundan emin ol
                EnsureLogDirectoryExists();

                // Tarih bazlı log dosyası oluştur
                string logFileName = $"error_{DateTime.Now:yyyyMMdd}.log";
                string logFilePath = Path.Combine(LogDirectory, logFileName);

                // Log içeriğini oluştur
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(new string('-', 80));
                sb.AppendLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{errorInfo.Severity}] [{errorInfo.Source}] Error ID: {errorInfo.ErrorId}");

                if (!string.IsNullOrEmpty(errorInfo.Context))
                {
                    sb.AppendLine($"Context: {errorInfo.Context}");
                }

                sb.AppendLine($"Message: {errorInfo.Exception.Message}");

                if (!string.IsNullOrEmpty(errorInfo.CallerMemberName))
                {
                    sb.AppendLine($"Method: {errorInfo.CallerMemberName}");
                }

                if (!string.IsNullOrEmpty(errorInfo.CallerFilePath))
                {
                    sb.AppendLine($"File: {Path.GetFileName(errorInfo.CallerFilePath)}");
                    sb.AppendLine($"Line: {errorInfo.CallerLineNumber}");
                }

                // DatabaseException için özel bilgiler
                if (errorInfo.Exception is DatabaseException dbEx && ShowSqlInErrorMessage)
                {
                    sb.AppendLine($"SQL Query: {dbEx.Query}");

                    if (dbEx.Parameters != null && dbEx.Parameters.Length > 0)
                    {
                        sb.AppendLine("Parameters:");
                        foreach (var param in dbEx.Parameters)
                        {
                            sb.AppendLine($"  {param.ParameterName} = {param.Value}");
                        }
                    }
                }

                // Ek veri varsa ekle
                if (errorInfo.AdditionalData != null && errorInfo.AdditionalData.Count > 0)
                {
                    sb.AppendLine("Additional Data:");
                    foreach (var item in errorInfo.AdditionalData)
                    {
                        sb.AppendLine($"  {item.Key} = {item.Value}");
                    }
                }

                // Stack trace
                if (errorInfo.Exception.StackTrace != null)
                {
                    sb.AppendLine("Stack Trace:");
                    sb.AppendLine(errorInfo.Exception.StackTrace);
                }

                // Inner exception varsa ekle
                if (errorInfo.Exception.InnerException != null)
                {
                    sb.AppendLine("Inner Exception:");
                    sb.AppendLine($"  Message: {errorInfo.Exception.InnerException.Message}");
                    if (errorInfo.Exception.InnerException.StackTrace != null)
                    {
                        sb.AppendLine("  Stack Trace:");
                        sb.AppendLine(errorInfo.Exception.InnerException.StackTrace);
                    }
                }

                sb.AppendLine(new string('-', 80));
                sb.AppendLine();

                // Dosyaya yaz
                File.AppendAllText(logFilePath, sb.ToString());
            }
            catch (Exception ex)
            {
                // Loglama hatası, bu durumda yapılabilecek çok şey yok
                Console.WriteLine($"Failed to log error to file: {ex.Message}");
            }
        }

        /// <summary>
        /// Hata bilgisini veritabanına loglar
        /// </summary>
        /// <param name="errorInfo">Hata bilgisi</param>
        private void LogErrorToDatabase(ErrorInfo errorInfo)
        {
            try
            {
                // Veritabanı hatası ise ve aynı zamanda veritabanına log yapılıyorsa
                // sonsuz döngü riski var, bu durumda sadece dosyaya logla
                if (errorInfo.Exception is MySqlException || errorInfo.Exception is DatabaseException)
                {
                    // Alternatif bir bağlantı kullanmayı dene, eğer bu da olmazsa
                    // sadece dosyaya logla ve çık
                    try
                    {
                        using (MySqlConnection connection = GetAlternativeConnection())
                        {
                            SaveErrorLog(connection, errorInfo);
                        }
                    }
                    catch
                    {
                        // Alternatif bağlantı da başarısız olursa çık
                        return;
                    }
                }
                else
                {
                    // Normal veritabanı bağlantısı ile logla
                    string query = @"INSERT INTO HataLoglar 
                                  (HataID, Tarih, Seviye, Kaynak, Mesaj, Baglam, StackTrace, 
                                   IcHata, CagiranMetod, CagiranDosya, CagiranSatir, 
                                   KullaniciID, KullaniciAdi, IPAdresi, EkBilgiler)
                                  VALUES 
                                  (@hataId, @tarih, @seviye, @kaynak, @mesaj, @baglam, @stackTrace, 
                                   @icHata, @cagiranMetod, @cagiranDosya, @cagiranSatir, 
                                   @kullaniciId, @kullaniciAdi, @ipAdresi, @ekBilgiler)";

                    // Oturum açmış kullanıcı bilgilerini al (eğer varsa)
                    int? userId = null;
                    string username = "";
                    try
                    {
                        if (CurrentSession.UserID > 0)
                        {
                            userId = CurrentSession.UserID;
                            username = CurrentSession.UserName;
                        }
                    }
                    catch { /* Kullanıcı bilgisi alınamazsa dikkate alma */ }

                    // IP adresini al
                    string ipAddress = GetUserIpAddress();

                    // Ek bilgileri JSON formatında serialize et
                    string additionalData = SerializeAdditionalData(errorInfo);

                    // İç hata mesajı
                    string innerExceptionMessage = errorInfo.Exception.InnerException != null
                        ? errorInfo.Exception.InnerException.Message
                        : null;

                    MySqlParameter[] parameters = new MySqlParameter[]
                    {
                        DatabaseHelper.CreateParameter("@hataId", errorInfo.ErrorId),
                        DatabaseHelper.CreateParameter("@tarih", errorInfo.Timestamp),
                        DatabaseHelper.CreateParameter("@seviye", errorInfo.Severity.ToString()),
                        DatabaseHelper.CreateParameter("@kaynak", errorInfo.Source.ToString()),
                        DatabaseHelper.CreateParameter("@mesaj", errorInfo.Exception.Message),
                        DatabaseHelper.CreateParameter("@baglam", errorInfo.Context),
                        DatabaseHelper.CreateParameter("@stackTrace", errorInfo.Exception.StackTrace),
                        DatabaseHelper.CreateParameter("@icHata", innerExceptionMessage),
                        DatabaseHelper.CreateParameter("@cagiranMetod", errorInfo.CallerMemberName),
                        DatabaseHelper.CreateParameter("@cagiranDosya", errorInfo.CallerFilePath),
                        DatabaseHelper.CreateParameter("@cagiranSatir", errorInfo.CallerLineNumber),
                        DatabaseHelper.CreateParameter("@kullaniciId", userId),
                        DatabaseHelper.CreateParameter("@kullaniciAdi", username),
                        DatabaseHelper.CreateParameter("@ipAdresi", ipAddress),
                        DatabaseHelper.CreateParameter("@ekBilgiler", additionalData)
                    };

                    // Veritabanına kaydet
                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                }
            }
            catch (Exception ex)
            {
                // Loglama hatası, bunu sadece konsola ve dosyaya yaz
                Console.WriteLine($"Failed to log error to database: {ex.Message}");
                LogBackupErrorRecord(errorInfo, ex);
            }
        }

        /// <summary>
        /// Alternatif bir veritabanı bağlantısı döndürür (birincil bağlantı başarısız olduğunda)
        /// </summary>
        private MySqlConnection GetAlternativeConnection()
        {
            // Uygulama ayarlarından alternatif bir bağlantı cümlesi alınabilir
            // Veya sabit kodlanmış bir yedek bağlantıya başvurulabilir
            // Bu örnekte basit bir bağlantı oluşturulmuştur
            string connectionString = "Server=localhost;Database=yedek_logdb;Uid=log_user;Pwd=log_password;";
            return new MySqlConnection(connectionString);
        }

        /// <summary>
        /// Alternatif bir veritabanı bağlantısı ile hata kaydını yapar
        /// </summary>
        private void SaveErrorLog(MySqlConnection connection, ErrorInfo errorInfo)
        {
            try
            {
                connection.Open();

                string query = @"INSERT INTO HataLoglar 
                               (HataID, Tarih, Seviye, Kaynak, Mesaj, Baglam)
                               VALUES 
                               (@hataId, @tarih, @seviye, @kaynak, @mesaj, @baglam)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@hataId", errorInfo.ErrorId);
                    command.Parameters.AddWithValue("@tarih", errorInfo.Timestamp);
                    command.Parameters.AddWithValue("@seviye", errorInfo.Severity.ToString());
                    command.Parameters.AddWithValue("@kaynak", errorInfo.Source.ToString());
                    command.Parameters.AddWithValue("@mesaj", errorInfo.Exception.Message);
                    command.Parameters.AddWithValue("@baglam", errorInfo.Context ?? "");

                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        /// <summary>
        /// Hata loglaması başarısız olduğunda yedek bir dosyaya kayıt ekler
        /// </summary>
        private void LogBackupErrorRecord(ErrorInfo errorInfo, Exception loggingException)
        {
            try
            {
                // Log klasörünün var olduğundan emin ol
                EnsureLogDirectoryExists();

                string backupFile = Path.Combine(LogDirectory, "database_logging_failures.log");

                StringBuilder logEntry = new StringBuilder();
                logEntry.AppendLine(new string('-', 80));
                logEntry.AppendLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Veritabanına loglama hatası");
                logEntry.AppendLine($"Orjinal Hata ID: {errorInfo.ErrorId}");
                logEntry.AppendLine($"Loglama Hatası: {loggingException.Message}");
                logEntry.AppendLine($"Orjinal Hata: {errorInfo.Exception.Message}");
                logEntry.AppendLine(new string('-', 80));

                File.AppendAllText(backupFile, logEntry.ToString());
            }
            catch
            {
                // Yedek loglama da başarısız olursa yapılacak bir şey kalmadı
            }
        }

        /// <summary>
        /// Kullanıcının IP adresini döndürür
        /// </summary>
        private string GetUserIpAddress()
        {
            try
            {
                // Burada NetworkHelper veya başka bir yöntemle IP adresi alınabilir
                // Basitlik için localhost döndürüyoruz
                return "127.0.0.1";
            }
            catch
            {
                return "unknown";
            }
        }

        /// <summary>
        /// Ek verileri JSON formatına dönüştürür
        /// </summary>
        private string SerializeAdditionalData(ErrorInfo errorInfo)
        {
            if (errorInfo.AdditionalData == null || errorInfo.AdditionalData.Count == 0)
                return null;

            try
            {
                // Basit bir JSON formatı oluşturalım
                StringBuilder json = new StringBuilder();
                json.Append("{");

                bool isFirst = true;
                foreach (var item in errorInfo.AdditionalData)
                {
                    if (!isFirst) json.Append(",");

                    // Anahtar
                    json.Append("\"");
                    json.Append(item.Key.Replace("\"", "\\\""));
                    json.Append("\":");

                    // Değer
                    if (item.Value == null)
                    {
                        json.Append("null");
                    }
                    else if (item.Value is string)
                    {
                        json.Append("\"");
                        json.Append(item.Value.ToString().Replace("\"", "\\\""));
                        json.Append("\"");
                    }
                    else if (item.Value is bool)
                    {
                        json.Append(item.Value.ToString().ToLower());
                    }
                    else if (item.Value is int || item.Value is double || item.Value is decimal)
                    {
                        json.Append(item.Value);
                    }
                    else
                    {
                        json.Append("\"");
                        json.Append(item.Value.ToString().Replace("\"", "\\\""));
                        json.Append("\"");
                    }

                    isFirst = false;
                }

                json.Append("}");
                return json.ToString();
            }
            catch
            {
                return "{\"error\":\"serialization_failed\"}";
            }
        }

        /// <summary>
        /// Kullanıcıya hata mesajı gösterir
        /// </summary>
        /// <param name="errorInfo">Hata bilgisi</param>
        private void ShowErrorToUser(ErrorInfo errorInfo)
        {
            try
            {
                string title = GetErrorTitle(errorInfo.Severity);
                string message = GetUserFriendlyMessage(errorInfo);
                MessageBoxIcon icon = GetMessageBoxIcon(errorInfo.Severity);

                MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to show error message: {ex.Message}");
                MessageBox.Show(
                    "Bir hata oluştu ve hata mesajı gösterilemedi.",
                    "Hata Bildirimi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Kritik hataları özel olarak işler
        /// </summary>
        /// <param name="errorInfo">Hata bilgisi</param>
        private void HandleCriticalError(ErrorInfo errorInfo)
        {
            try
            {
                // E-posta bildirimi gönder
                if (SendEmailNotifications && !string.IsNullOrEmpty(ErrorNotificationEmail))
                {
                    SendErrorNotificationEmail(errorInfo);
                }

                // Uygulamanın kritik bir hatadan kurtulabilmesi için
                // yapılabilecek işlemler burada yer alabilir.
                // Örneğin, bağlantıları yeniden kurma, cache temizleme vb.
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to handle critical error: {ex.Message}");
            }
        }

        /// <summary>
        /// Kritik hatalar için e-posta bildirimi gönderir
        /// </summary>
        /// <param name="errorInfo">Hata bilgisi</param>
        private void SendErrorNotificationEmail(ErrorInfo errorInfo)
        {
            try
            {
                // E-posta bildirimi için gerekli kodlar burada yer alabilir.
                // .NET'in System.Net.Mail kütüphanesi kullanılabilir.

                // Bu metod şu anda sadece bir işlem yapmadığını göstermek için var,
                // gerçek uygulamada e-posta gönderme işlemleri burada yer alacak.

                // Örnek bir kodlama:
                /*
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("sistem@sirketim.com");
                    mail.To.Add(ErrorNotificationEmail);
                    mail.Subject = $"Kritik Hata: {errorInfo.ErrorId}";
                    
                    StringBuilder body = new StringBuilder();
                    body.AppendLine($"Hata ID: {errorInfo.ErrorId}");
                    body.AppendLine($"Tarih: {errorInfo.Timestamp}");
                    body.AppendLine($"Hata Mesajı: {errorInfo.Exception.Message}");
                    body.AppendLine($"Bağlam: {errorInfo.Context}");
                    
                    mail.Body = body.ToString();
                    
                    using (SmtpClient smtp = new SmtpClient("smtp.sirketim.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential("user", "password");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
                */
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send error notification email: {ex.Message}");
            }
        }

        /// <summary>
        /// Hata seviyesine göre MessageBox ikon belirler
        /// </summary>
        /// <param name="severity">Hata seviyesi</param>
        /// <returns>MessageBox ikonu</returns>
        private MessageBoxIcon GetMessageBoxIcon(ErrorSeverity severity)
        {
            switch (severity)
            {
                case ErrorSeverity.Critical:
                case ErrorSeverity.Error:
                    return MessageBoxIcon.Error;
                case ErrorSeverity.Warning:
                    return MessageBoxIcon.Warning;
                case ErrorSeverity.Info:
                    return MessageBoxIcon.Information;
                default:
                    return MessageBoxIcon.Error;
            }
        }

        /// <summary>
        /// Hata seviyesine göre MessageBox başlığı belirler
        /// </summary>
        /// <param name="severity">Hata seviyesi</param>
        /// <returns>MessageBox başlığı</returns>
        private string GetErrorTitle(ErrorSeverity severity)
        {
            switch (severity)
            {
                case ErrorSeverity.Critical:
                    return "Kritik Hata";
                case ErrorSeverity.Error:
                    return "Hata";
                case ErrorSeverity.Warning:
                    return "Uyarı";
                case ErrorSeverity.Info:
                    return "Bilgi";
                default:
                    return "Hata";
            }
        }

        /// <summary>
        /// Kullanıcı dostu hata mesajı oluşturur
        /// </summary>
        /// <param name="errorInfo">Hata bilgisi</param>
        /// <returns>Kullanıcı dostu hata mesajı</returns>
        private string GetUserFriendlyMessage(ErrorInfo errorInfo)
        {
            StringBuilder message = new StringBuilder();

            // Bağlam bilgisi varsa ekle
            if (!string.IsNullOrEmpty(errorInfo.Context))
            {
                message.AppendLine(errorInfo.Context);
                message.AppendLine();
            }

            // Hata mesajı
            if (errorInfo.Exception is MySqlException mysqlEx)
            {
                message.AppendLine(GetDatabaseErrorMessage(mysqlEx, errorInfo));
            }
            else if (errorInfo.Exception is DatabaseException dbEx)
            {
                message.AppendLine(GetDatabaseErrorMessage(dbEx, errorInfo));
            }
            else
            {
                message.AppendLine(errorInfo.Exception.Message);
            }

            // Teknik detaylar gösterilecekse
            if (ShowTechnicalDetails && errorInfo.Severity == ErrorSeverity.Critical)
            {
                message.AppendLine();
                message.AppendLine($"Hata ID: {errorInfo.ErrorId}");

                if (errorInfo.Exception.InnerException != null)
                {
                    message.AppendLine($"İç Hata: {errorInfo.Exception.InnerException.Message}");
                }
            }

            return message.ToString();
        }

        /// <summary>
        /// Veritabanı hatası için kullanıcı dostu mesaj oluşturur
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <param name="errorInfo">Hata bilgisi</param>
        /// <returns>Kullanıcı dostu mesaj</returns>
        private string GetDatabaseErrorMessage(Exception ex, ErrorInfo errorInfo)
        {
            if (ex is MySqlException mysqlEx)
            {
                // MySQL hata numarasına göre özel mesajlar
                switch (mysqlEx.Number)
                {
                    case 1042: // Cannot connect to server
                        return "Veritabanı sunucusuna bağlanılamıyor. Lütfen internet bağlantınızı kontrol edin.";
                    case 1045: // Invalid username/password
                        return "Veritabanı erişimi reddedildi. Oturum açma bilgilerinizi kontrol edin.";
                    case 1049: // Unknown database
                        return "Veritabanı bulunamadı. Lütfen sistem yöneticinize başvurun.";
                    case 1062: // Duplicate entry
                        return "Bu kayıt zaten mevcut. Lütfen farklı bir değer deneyin.";
                    case 1451: // Foreign key constraint fails
                        return "Bu kaydı silmek için önce ilişkili kayıtları silmeniz gerekiyor.";
                    default:
                        return ShowSqlInErrorMessage ?
                            $"Veritabanı hatası: {mysqlEx.Message}" :
                            "Veritabanı işlemi sırasında bir hata oluştu.";
                }
            }
            else if (ex is DatabaseException dbEx)
            {
                // Query ve parametreleri göster (opsiyonel)
                if (ShowSqlInErrorMessage)
                {
                    return $"Veritabanı hatası: {dbEx.Message}\n\nSorgu: {dbEx.Query}";
                }
                return "Veritabanı işlemi sırasında bir hata oluştu.";
            }

            return "Veritabanı işlemi sırasında bir hata oluştu.";
        }

        /// <summary>
        /// Log klasörünün var olduğundan emin olur
        /// </summary>
        private void EnsureLogDirectoryExists()
        {
            try
            {
                if (!Directory.Exists(LogDirectory))
                {
                    Directory.CreateDirectory(LogDirectory);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to create log directory: {ex.Message}");
            }
        }

        #endregion
    }

    /// <summary>
    /// İç kullanım için hata bilgilerini tutan sınıf
    /// </summary>
    public class ErrorInfo
    {
        /// <summary>
        /// Benzersiz hata ID'si
        /// </summary>
        public string ErrorId { get; set; }

        /// <summary>
        /// Exception nesnesi
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Hatanın oluştuğu bağlam
        /// </summary>
        public string Context { get; set; }

        /// <summary>
        /// Hatanın önem derecesi
        /// </summary>
        public ErrorSeverity Severity { get; set; }

        /// <summary>
        /// Hatanın kaynağı
        /// </summary>
        public ErrorSource Source { get; set; }

        /// <summary>
        /// Hatanın oluştuğu metod
        /// </summary>
        public string CallerMemberName { get; set; }

        /// <summary>
        /// Hatanın oluştuğu dosya
        /// </summary>
        public string CallerFilePath { get; set; }

        /// <summary>
        /// Hatanın oluştuğu satır
        /// </summary>
        public int CallerLineNumber { get; set; }

        /// <summary>
        /// Hata zamanı
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Ek veri sözlüğü
        /// </summary>
        public Dictionary<string, object> AdditionalData { get; set; }
    }

    /// <summary>
    /// Doğrulama hatalarını temsil eden özel exception sınıfı
    /// </summary>
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
        public ValidationException(string message, Exception innerException) : base(message, innerException) { }
    }
}