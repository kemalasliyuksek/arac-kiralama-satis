using System;
using System.IO;
using System.Xml.Serialization;

namespace arac_kiralama_satis_desktop.Utils
{
    public class UserSettings
    {
        private static readonly string SettingsFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "AracKiralamaSatis",
            "user_settings.xml");

        // Singleton instance
        private static UserSettingsData _instance;

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

        // Save settings
        public static void Save()
        {
            try
            {
                // Create directory if it doesn't exist
                string directory = Path.GetDirectoryName(SettingsFilePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Serialize settings to XML
                XmlSerializer serializer = new XmlSerializer(typeof(UserSettingsData));
                using (FileStream stream = new FileStream(SettingsFilePath, FileMode.Create))
                {
                    serializer.Serialize(stream, _instance);
                }
            }
            catch (Exception ex)
            {
                // Log error or handle it as appropriate
                Console.WriteLine("Error saving settings: " + ex.Message);
            }
        }

        // Load settings
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
            }
            catch (Exception ex)
            {
                // Log error or handle it as appropriate
                Console.WriteLine("Error loading settings: " + ex.Message);
            }

            // Return default settings if file doesn't exist or there's an error
            return new UserSettingsData();
        }
    }

    [Serializable]
    public class UserSettingsData
    {
        public string SavedUsername { get; set; } = string.Empty;
        public bool RememberMe { get; set; } = false;
    }
}