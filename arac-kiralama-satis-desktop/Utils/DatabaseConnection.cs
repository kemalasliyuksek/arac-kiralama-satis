using System;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace arac_kiralama_satis_desktop.Utils
{
    public static class DatabaseConnection
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["AracDB"]?.ConnectionString
                                     ?? "Server=localhost;Database=arac_kiralama_satis;Uid=root;Pwd=2307;";

        public static MySqlConnection GetConnection()
        {
            try
            {
                Console.WriteLine("Bağlantı dizesi: " + connectionString);

                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                connection.Close();
                return connection;
            }
            catch (Exception ex)
            {
                string detailMessage = "Veritabanı bağlantısı oluşturulamadı: " + ex.Message;
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
                catch (Exception ex)
                {
                    string detailMessage = "Sorgu çalıştırma hatası: " + ex.Message;
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
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        affectedRows = command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Sorgu çalıştırma hatası: " + ex.Message);
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
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        result = command.ExecuteScalar();
                    }
                }
                catch (Exception ex)
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
    }
}