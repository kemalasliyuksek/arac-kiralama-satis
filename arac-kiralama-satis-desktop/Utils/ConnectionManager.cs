using System;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace arac_kiralama_satis_desktop.Utils
{
    /// <summary>
    /// Veritabanı bağlantılarını yöneten ve güvenlik sağlayan sınıf
    /// </summary>
    public static class ConnectionManager
    {
        // Şifreleme için kullanılacak anahtar ve IV
        private static readonly byte[] EncryptionKey = Encoding.UTF8.GetBytes("Arac12345678910"); // 16 bytes key
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("1234567890123456"); // 16 bytes IV

        /// <summary>
        /// Şifrelenmiş bağlantı cümlesini çözer ve döndürür
        /// </summary>
        private static string DecryptConnectionString(string encryptedString)
        {
            try
            {
                byte[] cipherText = Convert.FromBase64String(encryptedString);
                string decryptedConnectionString;

                using (Aes aes = Aes.Create())
                {
                    aes.Key = EncryptionKey;
                    aes.IV = IV;

                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream ms = new MemoryStream(cipherText))
                    {
                        using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader reader = new StreamReader(cs))
                            {
                                decryptedConnectionString = reader.ReadToEnd();
                            }
                        }
                    }
                }

                return decryptedConnectionString;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Decryption error: " + ex.Message);
                // Şifre çözme başarısız olursa, varsayılan yerel bağlantı cümlesini döndür
                return ConfigurationManager.ConnectionStrings["AracDB_Local"].ConnectionString;
            }
        }

        /// <summary>
        /// Bağlantı cümlesini şifreler
        /// </summary>
        public static string EncryptConnectionString(string connectionString)
        {
            try
            {
                byte[] encrypted;

                using (Aes aes = Aes.Create())
                {
                    aes.Key = EncryptionKey;
                    aes.IV = IV;

                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter writer = new StreamWriter(cs))
                            {
                                writer.Write(connectionString);
                            }
                            encrypted = ms.ToArray();
                        }
                    }
                }

                return Convert.ToBase64String(encrypted);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Encryption error: " + ex.Message);
                return connectionString; // Şifreleme başarısız olursa, plain text olarak döndür
            }
        }

        /// <summary>
        /// Aktif veritabanı bağlantısı tipini belirler (Local/Remote)
        /// </summary>
        public static bool UseRemoteDatabase { get; set; } = false;

        /// <summary>
        /// Veritabanı bağlantı bilgilerini içeren sınıf
        /// </summary>
        public static string ActiveEnvironment => UseRemoteDatabase ? "Remote" : "Local";

        /// <summary>
        /// Bağlantı havuzu oluşturur ve döndürür
        /// </summary>
        private static readonly Lazy<MySqlConnection> ConnectionPool = new Lazy<MySqlConnection>(() =>
        {
            MySqlConnection connection = new MySqlConnection(GetConnectionString());
            return connection;
        });

        /// <summary>
        /// Aktif bağlantı cümlesini döndürür
        /// </summary>
        public static string GetConnectionString()
        {
            string connectionName = UseRemoteDatabase ? "AracDB_Remote" : "AracDB_Local";

            try
            {
                // Şifreli bağlantı cümlesini al
                string encryptedConnectionString = ConfigurationManager.AppSettings[$"Encrypted_{connectionName}"];

                // Eğer şifreli bağlantı cümlesi varsa çöz
                if (!string.IsNullOrEmpty(encryptedConnectionString))
                {
                    return DecryptConnectionString(encryptedConnectionString);
                }

                // Şifreli bağlantı cümlesi yoksa, normal bağlantı cümlesini al
                string connectionString = ConfigurationManager.ConnectionStrings[connectionName]?.ConnectionString;

                // Config'de bağlantı dizesi bulunamazsa varsayılan değerlere dön
                if (string.IsNullOrEmpty(connectionString))
                {
                    if (UseRemoteDatabase)
                    {
                        // Bu değerler gerçek uygulamada burada olmamalı
                        // Sadece örnek olarak gösterilmiştir
                        return "Server=database_server;Database=arac_kiralama_satis;Uid=secure_user;Pwd=********;Port=3306;SslMode=Required;CharSet=utf8mb4;";
                    }
                    else
                    {
                        return "Server=localhost;Database=arac_kiralama_satis;Uid=secure_user;Pwd=********;Port=3306;SslMode=Required;CharSet=utf8mb4;";
                    }
                }

                return connectionString;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bağlantı dizesi alınırken hata: {ex.Message}");

                // Hata durumunda varsayılan değer döndür
                if (UseRemoteDatabase)
                {
                    return "Server=database_server;Database=arac_kiralama_satis;Uid=secure_user;Pwd=********;Port=3306;SslMode=Required;CharSet=utf8mb4;";
                }
                else
                {
                    return "Server=localhost;Database=arac_kiralama_satis;Uid=secure_user;Pwd=********;Port=3306;SslMode=Required;CharSet=utf8mb4;";
                }
            }
        }

        /// <summary>
        /// Bağlantı havuzundan bağlantı alır
        /// </summary>
        public static MySqlConnection GetConnection()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(GetConnectionString());
                return connection;
            }
            catch (MySqlException ex)
            {
                HandleDatabaseException(ex, "Veritabanı bağlantısı kurulamadı");
                throw;
            }
        }

        /// <summary>
        /// Veritabanı hatalarını ele alır
        /// </summary>
        private static void HandleDatabaseException(MySqlException ex, string contextMessage)
        {
            string detailMessage = $"{contextMessage}: {ex.Message}";

            // Specific MySQL error handling
            switch (ex.Number)
            {
                case 1042: // Unable to connect to server
                    detailMessage = "Veritabanı sunucusuna bağlanılamıyor. Sunucu çalışıyor ve erişilebilir durumda mı kontrol edin.";
                    break;
                case 1045: // Invalid username/password
                    detailMessage = "Veritabanı kullanıcı adı veya şifresi geçersiz.";
                    break;
                case 1049: // Unknown database
                    detailMessage = "Belirtilen veritabanı bulunamadı.";
                    break;
                case 1130: // Host not allowed
                    detailMessage = "Bu IP adresi veritabanı sunucusuna erişim için yetkilendirilmemiş.";
                    break;
                case 1064: // SQL Syntax error
                    detailMessage = "SQL sözdizimi hatası: " + ex.Message;
                    break;
            }

            if (ex.InnerException != null)
            {
                detailMessage += $" | İç hata: {ex.InnerException.Message}";
            }

            // Log the error
            LogError(detailMessage, ex);

            // Show a user-friendly message
            MessageBox.Show(detailMessage, "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Hata loglaması yapar
        /// </summary>
        private static void LogError(string message, Exception ex)
        {
            try
            {
                string logPath = Path.Combine(Application.StartupPath, "Logs");
                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }

                string logFile = Path.Combine(logPath, $"db_error_{DateTime.Now:yyyyMMdd}.log");
                string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}\r\n";
                if (ex != null)
                {
                    logEntry += $"Exception: {ex.GetType().Name}\r\nMessage: {ex.Message}\r\nStack Trace: {ex.StackTrace}\r\n";
                    if (ex.InnerException != null)
                    {
                        logEntry += $"Inner Exception: {ex.InnerException.Message}\r\n";
                    }
                }
                logEntry += new string('-', 80) + "\r\n";

                File.AppendAllText(logFile, logEntry);
            }
            catch
            {
                // Logging should not throw exceptions
            }
        }
    }
}