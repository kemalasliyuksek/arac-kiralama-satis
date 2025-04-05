using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace arac_kiralama_satis_desktop.Utils
{
    /// <summary>
    /// Bağlantı dizelerini şifrelemek için kullanılan form
    /// </summary>
    public partial class ConnectionStringEncryptor : Form
    {
        // Şifreleme için kullanılacak anahtar ve IV - 16 bayt uzunluğunda olmalı
        private static readonly byte[] EncryptionKey = Encoding.UTF8.GetBytes("Arac123456789101"); // 16 bytes key
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("1234567890123456"); // 16 bytes IV

        public ConnectionStringEncryptor()
        {
            InitializeComponent();
            LoadConnectionStrings();
        }

        private void LoadConnectionStrings()
        {
            cmbConnectionNames.Items.Clear();

            // App.config'den tüm bağlantı dizelerini yükle
            foreach (ConnectionStringSettings conn in ConfigurationManager.ConnectionStrings)
            {
                if (conn.Name != "LocalSqlServer") // System connection string'i atla
                {
                    cmbConnectionNames.Items.Add(conn.Name);
                }
            }

            if (cmbConnectionNames.Items.Count > 0)
            {
                cmbConnectionNames.SelectedIndex = 0;
            }
        }

        private void cmbConnectionNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedConnectionName = cmbConnectionNames.SelectedItem.ToString();
            string connectionString = ConfigurationManager.ConnectionStrings[selectedConnectionName]?.ConnectionString;

            txtConnectionString.Text = connectionString;
            txtEncryptedString.Text = string.Empty;
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtConnectionString.Text))
            {
                MessageBox.Show("Lütfen şifrelenecek bağlantı dizesini girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string encryptedString = EncryptConnectionString(txtConnectionString.Text);
                txtEncryptedString.Text = encryptedString;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Şifreleme işlemi sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string EncryptConnectionString(string connectionString)
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

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEncryptedString.Text))
            {
                Clipboard.SetText(txtEncryptedString.Text);
                MessageBox.Show("Şifreli bağlantı dizesi panoya kopyalandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                string decrypted = DecryptConnectionString(txtEncryptedString.Text);
                MessageBox.Show($"Şifre çözme başarılı:\n\n{decrypted}", "Test Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Şifre çözme işlemi sırasında bir hata oluştu: {ex.Message}", "Test Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string DecryptConnectionString(string encryptedString)
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
    }
}