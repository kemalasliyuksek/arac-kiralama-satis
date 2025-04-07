using System;
using System.Collections.Generic;
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
        private static readonly byte[] EncryptionKey = Encoding.UTF8.GetBytes("Arac123456789101"); // 16 bytes key
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
                // ErrorManager ile hata yönetimi
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Bağlantı cümlesi şifre çözme hatası",
                    ErrorSeverity.Critical,
                    ErrorSource.Database);

                throw new InvalidOperationException($"Bağlantı cümlesi şifresi çözülemedi. (Hata ID: {errorId})", ex);
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
                // ErrorManager ile hata yönetimi
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Bağlantı cümlesi şifreleme hatası",
                    ErrorSeverity.Critical,
                    ErrorSource.Database);

                throw new InvalidOperationException($"Bağlantı cümlesi şifrelenemiyor. (Hata ID: {errorId})", ex);
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
            try
            {
                string connectionName = UseRemoteDatabase ? "AracDB_Remote" : "AracDB_Local";

                // Şifreli bağlantı cümlesini al
                string encryptedConnectionString = ConfigurationManager.AppSettings[$"Encrypted_{connectionName}"];

                if (string.IsNullOrEmpty(encryptedConnectionString))
                {
                    string errorMessage = $"Şifreli bağlantı cümlesi bulunamadı: {connectionName}.";

                    // ErrorManager ile hata bilgisi
                    string errorId = ErrorManager.Instance.HandleException(
                        new InvalidOperationException(errorMessage),
                        "Bağlantı yapılandırması eksik",
                        ErrorSeverity.Critical,
                        ErrorSource.Database);

                    throw new InvalidOperationException($"{errorMessage} (Hata ID: {errorId})");
                }

                return DecryptConnectionString(encryptedConnectionString);
            }
            catch (Exception ex) when (!(ex is InvalidOperationException))
            {
                // Zaten işlenmiş InvalidOperationException hatalarını tekrar işleme
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Bağlantı cümlesi alınırken beklenmeyen hata",
                    ErrorSeverity.Critical,
                    ErrorSource.Database);

                throw new InvalidOperationException($"Bağlantı cümlesi alınırken beklenmeyen hata. (Hata ID: {errorId})", ex);
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
                // MySQL hata numarasına göre özel bilgiler ekle
                Dictionary<string, object> additionalData = new Dictionary<string, object>
                {
                    { "MySqlErrorNumber", ex.Number },
                    { "ConnectionString", GetSanitizedConnectionString() }
                };

                string context = GetDatabaseErrorContext(ex);

                // ErrorManager ile hata yönetimi
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    context,
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new InvalidOperationException($"Veritabanı bağlantısı alınamadı. (Hata ID: {errorId})", ex);
            }
            catch (Exception ex) when (!(ex is InvalidOperationException))
            {
                // Diğer hatalar için
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Veritabanı bağlantısı oluşturulurken beklenmeyen hata",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new InvalidOperationException($"Veritabanı bağlantısı oluşturulurken beklenmeyen hata. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// Bağlantı cümlesinin hassas bilgilerini gizler
        /// </summary>
        private static string GetSanitizedConnectionString()
        {
            try
            {
                string connectionString = GetConnectionString();
                // Şifreyi gizle
                return connectionString.Replace(
                    System.Text.RegularExpressions.Regex.Match(
                        connectionString,
                        "(?<=Password=).*?(?=;|$)",
                        System.Text.RegularExpressions.RegexOptions.IgnoreCase
                    ).Value,
                    "********"
                );
            }
            catch
            {
                return "Bağlantı cümlesi alınamadı";
            }
        }

        /// <summary>
        /// MySQL hata numarasına göre uygun hata bağlamını döndürür
        /// </summary>
        private static string GetDatabaseErrorContext(MySqlException ex)
        {
            switch (ex.Number)
            {
                case 1042: // Unable to connect to server
                    return "Veritabanı sunucusuna bağlanılamıyor. Sunucu çalışıyor ve erişilebilir durumda mı kontrol edin.";
                case 1045: // Invalid username/password
                    return "Veritabanı kullanıcı adı veya şifresi geçersiz.";
                case 1049: // Unknown database
                    return "Belirtilen veritabanı bulunamadı.";
                case 1130: // Host not allowed
                    return "Bu IP adresi veritabanı sunucusuna erişim için yetkilendirilmemiş.";
                case 1064: // SQL Syntax error
                    return "SQL sözdizimi hatası: " + ex.Message;
                default:
                    return $"Veritabanı bağlantı hatası: {ex.Message}";
            }
        }
    }
}