using System;
using System.IO;
using System.Xml.Serialization;

namespace arac_kiralama_satis_desktop.Utils
{
    /// <summary>
    /// Kullanıcı ayarlarını yöneten statik sınıf
    /// </summary>
    public static class UserSettings
    {
        private static readonly string SettingsFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "AracKiralamaSatis",
            "user_settings.xml");

        private static UserSettingsData _instance;

        /// <summary>
        /// Mevcut kullanıcı ayarlarını döndürür, yoksa yeni oluşturur
        /// </summary>
        public static UserSettingsData Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Load();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Kullanıcı ayarlarını XML dosyasına kaydeder
        /// </summary>
        public static void Save()
        {
            try
            {
                string directory = Path.GetDirectoryName(SettingsFilePath);
                if (!Directory.Exists(directory))
                {
                    // Dizin yoksa oluştur
                    Directory.CreateDirectory(directory);
                    ErrorManager.Instance.LogInfo($"Ayarlar dizini oluşturuldu: {directory}", "UserSettings.Save");
                }

                XmlSerializer serializer = new XmlSerializer(typeof(UserSettingsData));
                using (FileStream stream = new FileStream(SettingsFilePath, FileMode.Create))
                {
                    serializer.Serialize(stream, _instance);
                }

                // Başarılı kayıt bilgisini logla
                ErrorManager.Instance.LogInfo(
                    $"Kullanıcı ayarları başarıyla kaydedildi. Dosya: {SettingsFilePath}",
                    "UserSettings.Save");
            }
            catch (IOException ex)
            {
                // Dosya I/O hatası
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Kullanıcı ayarları dosyası '{SettingsFilePath}' kaydedilirken I/O hatası oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.FileSystem);

                // Varsayılan işlemler gerçekleştirilemediğinde yapılabilecek alternatif işlemler
                // Örneğin geçici bir konuma kaydetmek gibi
                TryFallbackSave(errorId);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Dosya erişim izni hatası
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Kullanıcı ayarları dosyası '{SettingsFilePath}' için erişim izni yok",
                    ErrorSeverity.Error,
                    ErrorSource.FileSystem);

                // Alternatif yöntem denenebilir
                TryFallbackSave(errorId);
            }
            catch (InvalidOperationException ex)
            {
                // Serileştirme hatası
                ErrorManager.Instance.HandleException(
                    ex,
                    "Kullanıcı ayarları serileştirilirken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.FileSystem);
            }
            catch (Exception ex)
            {
                // Diğer tüm hatalar
                ErrorManager.Instance.HandleException(
                    ex,
                    "Kullanıcı ayarları kaydedilirken beklenmeyen bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.FileSystem);
            }
        }

        /// <summary>
        /// Başarısız kaydetme işlemi sonrası alternatif kaydetme yöntemi
        /// </summary>
        private static void TryFallbackSave(string errorId)
        {
            try
            {
                // Alternatif olarak Geçici klasöre kaydetmeyi dene
                string tempPath = Path.Combine(
                    Path.GetTempPath(),
                    "AracKiralamaSatis",
                    "user_settings_backup.xml");

                // Geçici dizin oluştur
                Directory.CreateDirectory(Path.GetDirectoryName(tempPath));

                XmlSerializer serializer = new XmlSerializer(typeof(UserSettingsData));
                using (FileStream stream = new FileStream(tempPath, FileMode.Create))
                {
                    serializer.Serialize(stream, _instance);
                }

                ErrorManager.Instance.LogWarning(
                    $"Asıl kayıt başarısız oldu (Hata ID: {errorId}), ayarlar geçici konuma kaydedildi: {tempPath}",
                    "UserSettings.TryFallbackSave");
            }
            catch (Exception ex)
            {
                // Alternatif kaydetme de başarısız olursa, sadece logla
                ErrorManager.Instance.HandleException(
                    ex,
                    "Alternatif kaydetme işlemi de başarısız oldu",
                    ErrorSeverity.Warning,
                    ErrorSource.FileSystem,
                    false);
            }
        }

        /// <summary>
        /// Kullanıcı ayarlarını XML dosyasından yükler, yoksa yeni oluşturur
        /// </summary>
        private static UserSettingsData Load()
        {
            try
            {
                if (File.Exists(SettingsFilePath))
                {
                    ErrorManager.Instance.LogInfo(
                        $"Kullanıcı ayarları dosyası bulundu: {SettingsFilePath}",
                        "UserSettings.Load");

                    XmlSerializer serializer = new XmlSerializer(typeof(UserSettingsData));
                    using (FileStream stream = new FileStream(SettingsFilePath, FileMode.Open))
                    {
                        UserSettingsData settings = (UserSettingsData)serializer.Deserialize(stream);
                        return settings;
                    }
                }
                else
                {
                    // Dosya bulunamadı, yeni ayarlar oluşturuluyor
                    ErrorManager.Instance.LogInfo(
                        $"Kullanıcı ayarları dosyası bulunamadı: '{SettingsFilePath}', yeni ayarlar oluşturuluyor",
                        "UserSettings.Load");

                    return new UserSettingsData();
                }
            }
            catch (InvalidOperationException ex)
            {
                // XML deserialleştirme hatası
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Kullanıcı ayarları dosyası geçersiz format içeriyor",
                    ErrorSeverity.Warning,
                    ErrorSource.FileSystem);

                // Bozuk dosyayı yedekle ve yeni oluştur
                TryBackupCorruptFile(errorId);
            }
            catch (IOException ex)
            {
                // Dosya I/O hatası
                ErrorManager.Instance.HandleException(
                    ex,
                    $"Kullanıcı ayarları dosyası '{SettingsFilePath}' okunurken I/O hatası oluştu",
                    ErrorSeverity.Warning,
                    ErrorSource.FileSystem);
            }
            catch (Exception ex)
            {
                // Diğer tüm hatalar
                ErrorManager.Instance.HandleException(
                    ex,
                    "Kullanıcı ayarları yüklenirken beklenmeyen bir hata oluştu",
                    ErrorSeverity.Warning,
                    ErrorSource.FileSystem);
            }

            // Hata durumunda geçici dosyadan okumayı dene
            try
            {
                string tempPath = Path.Combine(
                    Path.GetTempPath(),
                    "AracKiralamaSatis",
                    "user_settings_backup.xml");

                if (File.Exists(tempPath))
                {
                    ErrorManager.Instance.LogWarning(
                        $"Ana ayarlar dosyası okunamadı, yedek dosya deneniyor: {tempPath}",
                        "UserSettings.Load");

                    XmlSerializer serializer = new XmlSerializer(typeof(UserSettingsData));
                    using (FileStream stream = new FileStream(tempPath, FileMode.Open))
                    {
                        return (UserSettingsData)serializer.Deserialize(stream);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.HandleException(
                    ex,
                    "Yedek ayarlar dosyası da okunamadı",
                    ErrorSeverity.Warning,
                    ErrorSource.FileSystem,
                    false);
            }

            // Tüm okuma girişimleri başarısız olduğunda yeni ayarlar oluştur
            ErrorManager.Instance.LogWarning(
                "Ayarlar yüklenemedi, yeni varsayılan ayarlar oluşturuluyor",
                "UserSettings.Load");

            return new UserSettingsData();
        }

        /// <summary>
        /// Bozuk ayarlar dosyasını yedeklemeyi dener
        /// </summary>
        private static void TryBackupCorruptFile(string errorId)
        {
            try
            {
                if (File.Exists(SettingsFilePath))
                {
                    string backupPath = $"{SettingsFilePath}.corrupt.{DateTime.Now:yyyyMMddHHmmss}.bak";
                    File.Copy(SettingsFilePath, backupPath);
                    File.Delete(SettingsFilePath);

                    ErrorManager.Instance.LogWarning(
                        $"Bozuk ayarlar dosyası yedeklendi (Hata ID: {errorId}): {backupPath}",
                        "UserSettings.TryBackupCorruptFile");
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.HandleException(
                    ex,
                    "Bozuk ayarlar dosyasını yedekleme işlemi başarısız oldu",
                    ErrorSeverity.Warning,
                    ErrorSource.FileSystem,
                    false);
            }
        }
    }

    /// <summary>
    /// Kullanıcı ayarlarını içeren serileştirilebilir sınıf
    /// </summary>
    [Serializable]
    public class UserSettingsData
    {
        /// <summary>
        /// Son kullanılan kullanıcı adı
        /// </summary>
        public string SavedUsername { get; set; } = string.Empty;

        /// <summary>
        /// Kullanıcı adını hatırla seçeneği 
        /// </summary>
        public bool RememberMe { get; set; } = false;
    }
}