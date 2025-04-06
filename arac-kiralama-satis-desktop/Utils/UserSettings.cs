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
                    Directory.CreateDirectory(directory);
                }

                XmlSerializer serializer = new XmlSerializer(typeof(UserSettingsData));
                using (FileStream stream = new FileStream(SettingsFilePath, FileMode.Create))
                {
                    serializer.Serialize(stream, _instance);
                }

                // Başarılı kayıt bilgisini logla
                ErrorManager.Instance.LogInfo("Kullanıcı ayarları başarıyla kaydedildi", "UserSettings");
            }
            catch (IOException ex)
            {
                // Dosya I/O hatası
                ErrorManager.Instance.HandleException(
                    ex,
                    $"Kullanıcı ayarları dosyası '{SettingsFilePath}' kaydedilirken I/O hatası oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.FileSystem);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Dosya erişim izni hatası
                ErrorManager.Instance.HandleException(
                    ex,
                    $"Kullanıcı ayarları dosyası '{SettingsFilePath}' için erişim izni yok",
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
        /// Kullanıcı ayarlarını XML dosyasından yükler, yoksa yeni oluşturur
        /// </summary>
        private static UserSettingsData Load()
        {
            try
            {
                if (File.Exists(SettingsFilePath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(UserSettingsData));
                    using (FileStream stream = new FileStream(SettingsFilePath, FileMode.Open))
                    {
                        return (UserSettingsData)serializer.Deserialize(stream);
                    }
                }

                // Dosya bulunamadı, yeni ayarlar oluşturuluyor
                ErrorManager.Instance.LogInfo(
                    $"Kullanıcı ayarları dosyası '{SettingsFilePath}' bulunamadı, yeni ayarlar oluşturuluyor",
                    "UserSettings");
            }
            catch (InvalidOperationException ex)
            {
                // XML deserialleştirme hatası
                ErrorManager.Instance.HandleException(
                    ex,
                    "Kullanıcı ayarları dosyası geçersiz format içeriyor",
                    ErrorSeverity.Warning,
                    ErrorSource.FileSystem);
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

            // Hata durumunda veya dosya yoksa yeni ayarlar oluştur
            return new UserSettingsData();
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