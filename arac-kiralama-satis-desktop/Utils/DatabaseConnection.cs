using System;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace arac_kiralama_satis_desktop.Utils
{
    public static class DatabaseConnection
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["AracDB"]?.ConnectionString
                                     ?? "Server=92.205.171.9;Database=arac_kiralama_satis;Uid=admin;Pwd=Ke3@1.3ySQ1;Port=3306;SslMode=None;CharSet=utf8mb4;ConnectionTimeout=60;DefaultCommandTimeout=60;Pooling=true;";

        public static MySqlConnection GetConnection()
        {
            try
            {
                Console.WriteLine("Bağlantı dizesi: " + connectionString);

                MySqlConnection connection = new MySqlConnection(connectionString);

                // Test connection before returning
                connection.Open();
                connection.Close();

                return connection;
            }
            catch (MySqlException ex)
            {
                string detailMessage = "Veritabanı bağlantısı oluşturulamadı: " + ex.Message;

                // Handle specific MySQL errors
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
                        detailMessage = "Bu IP adresi veritabanı sunucusuna erişim için yetkilendirilmemiş. Veritabanı yöneticinizle iletişime geçin.";
                        break;
                }

                if (ex.InnerException != null)
                {
                    detailMessage += " | İç hata: " + ex.InnerException.Message;
                }

                MessageBox.Show(detailMessage, "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);

                throw new Exception(detailMessage);
            }
            catch (Exception ex)
            {
                string detailMessage = "Veritabanı bağlantısında beklenmeyen hata: " + ex.Message;

                if (ex.InnerException != null)
                {
                    detailMessage += " | İç hata: " + ex.InnerException.Message;
                }

                MessageBox.Show(detailMessage, "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);

                throw new Exception(detailMessage);
            }
        }

        public static DataTable ExecuteQuery(string query, params MySqlParameter[] parameters)
        {
            DataTable dataTable = new DataTable();

            using (MySqlConnection connection = GetConnection())
            {
                try
                {
                    Console.WriteLine("SQL Sorgusu: " + query);
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            Console.WriteLine($"Parametre: {param.ParameterName} = {param.Value}");
                        }
                    }

                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.CommandTimeout = 60; // 60 saniye zaman aşımı

                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    string detailMessage = "Sorgu çalıştırma hatası: " + ex.Message;

                    // Handle specific MySQL errors
                    switch (ex.Number)
                    {
                        case 1064: // SQL syntax error
                            detailMessage = "SQL sözdizimi hatası. Sorguyu kontrol edin.";
                            break;
                        case 1146: // Table doesn't exist
                            detailMessage = "Belirtilen tablo bulunamadı.";
                            break;
                        case 1054: // Unknown column
                            detailMessage = "Belirtilen sütun bulunamadı.";
                            break;
                    }

                    if (ex.InnerException != null)
                    {
                        detailMessage += " | İç hata: " + ex.InnerException.Message;
                    }

                    detailMessage += "\nSorgu: " + query;
                    if (parameters != null)
                    {
                        detailMessage += "\nParametreler: ";
                        foreach (var param in parameters)
                        {
                            detailMessage += $"{param.ParameterName}={param.Value}, ";
                        }
                    }

                    MessageBox.Show(detailMessage, "Sorgu Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    throw new Exception(detailMessage);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }

            return dataTable;
        }

        public static int ExecuteNonQuery(string query, params MySqlParameter[] parameters)
        {
            int affectedRows = 0;

            using (MySqlConnection connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.CommandTimeout = 60; // 60 saniye zaman aşımı

                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        affectedRows = command.ExecuteNonQuery();
                    }
                }
                catch (MySqlException ex)
                {
                    string detailMessage = "Sorgu çalıştırma hatası: " + ex.Message;

                    if (ex.InnerException != null)
                    {
                        detailMessage += " | İç hata: " + ex.InnerException.Message;
                    }

                    MessageBox.Show(detailMessage, "Sorgu Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    throw new Exception(detailMessage);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }

            return affectedRows;
        }

        public static object ExecuteScalar(string query, params MySqlParameter[] parameters)
        {
            object result = null;

            using (MySqlConnection connection = GetConnection())
            {
                try
                {
                    Console.WriteLine("Scalar SQL Sorgusu: " + query);
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            Console.WriteLine($"Parametre: {param.ParameterName} = {param.Value}");
                        }
                    }

                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.CommandTimeout = 60; // 60 saniye zaman aşımı

                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        result = command.ExecuteScalar();
                    }
                }
                catch (MySqlException ex)
                {
                    string detailMessage = "Scalar sorgu çalıştırma hatası: " + ex.Message;

                    if (ex.InnerException != null)
                    {
                        detailMessage += " | İç hata: " + ex.InnerException.Message;
                    }

                    detailMessage += "\nSorgu: " + query;
                    if (parameters != null)
                    {
                        detailMessage += "\nParametreler: ";
                        foreach (var param in parameters)
                        {
                            detailMessage += $"{param.ParameterName}={param.Value}, ";
                        }
                    }

                    MessageBox.Show(detailMessage, "Scalar Sorgu Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    throw new Exception(detailMessage);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }

            return result;
        }

        // Test veritabanı bağlantısı için metod
        public static bool TestConnection(out string errorMessage)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    connection.Close();
                }

                errorMessage = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += " | " + ex.InnerException.Message;
                }
                return false;
            }
        }
    }
}