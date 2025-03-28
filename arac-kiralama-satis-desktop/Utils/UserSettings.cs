using System;
using System.IO;
using System.Xml.Serialization;

namespace arac_kiralama_satis_desktop.Utils
{
    public static class UserSettings
    {
        private static readonly string SettingsFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "AracKiralamaSatis",
            "user_settings.xml");

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
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving settings: " + ex.Message);
            }
        }

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
                Console.WriteLine("Error loading settings: " + ex.Message);
            }

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