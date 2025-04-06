using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Text;

namespace arac_kiralama_satis_desktop.Utils
{
    /// <summary>
    /// Jenerik veritabanı işlemleri için temel sınıf
    /// </summary>
    public static class DatabaseHelper
    {
        /// <summary>
        /// SELECT sorgusu çalıştırıp DataTable olarak sonuç döndürür
        /// </summary>
        public static DataTable ExecuteQuery(string query, params MySqlParameter[] parameters)
        {
            DataTable dataTable = new DataTable();

            using (MySqlConnection connection = ConnectionManager.GetConnection())
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

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    // ErrorManager ile hata yönetimi
                    DatabaseException dbEx = new DatabaseException("Sorgu çalıştırma hatası", ex, query, parameters);
                    ErrorManager.Instance.HandleException(
                        dbEx,
                        "SELECT sorgusu çalıştırılırken hata oluştu",
                        ErrorSeverity.Error,
                        ErrorSource.Database);
                    throw dbEx;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }

            return dataTable;
        }

        /// <summary>
        /// INSERT, UPDATE, DELETE gibi geri dönüş değeri olmayan sorguları çalıştırır
        /// </summary>
        public static int ExecuteNonQuery(string query, params MySqlParameter[] parameters)
        {
            int affectedRows = 0;

            using (MySqlConnection connection = ConnectionManager.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.CommandTimeout = 60;

                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        affectedRows = command.ExecuteNonQuery();
                    }
                }
                catch (MySqlException ex)
                {
                    // ErrorManager ile hata yönetimi
                    DatabaseException dbEx = new DatabaseException("Komut çalıştırma hatası", ex, query, parameters);
                    ErrorManager.Instance.HandleException(
                        dbEx,
                        "Veri değiştirme sorgusu çalıştırılırken hata oluştu",
                        ErrorSeverity.Error,
                        ErrorSource.Database);
                    throw dbEx;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }

            return affectedRows;
        }

        /// <summary>
        /// Tek bir değer döndüren sorgular için (COUNT, SUM, vb.)
        /// </summary>
        public static object ExecuteScalar(string query, params MySqlParameter[] parameters)
        {
            object result = null;

            using (MySqlConnection connection = ConnectionManager.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.CommandTimeout = 60;

                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        result = command.ExecuteScalar();
                    }
                }
                catch (MySqlException ex)
                {
                    // ErrorManager ile hata yönetimi
                    DatabaseException dbEx = new DatabaseException("Scalar sorgu hatası", ex, query, parameters);
                    ErrorManager.Instance.HandleException(
                        dbEx,
                        "Scalar sorgusu çalıştırılırken hata oluştu",
                        ErrorSeverity.Error,
                        ErrorSource.Database);
                    throw dbEx;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }

            return result;
        }

        /// <summary>
        /// Paramatreler için null kontrolü yapıp DBNull.Value ile değiştirir
        /// </summary>
        public static MySqlParameter CreateParameter(string name, object value)
        {
            return new MySqlParameter(name, value ?? DBNull.Value);
        }

        /// <summary>
        /// DataRow'dan değer okuma işlemi için extension method
        /// </summary>
        public static T GetValue<T>(this DataRow row, string columnName)
        {
            if (row == null || !row.Table.Columns.Contains(columnName) || row[columnName] == DBNull.Value)
            {
                return default(T);
            }

            object value = row[columnName];
            Type targetType = typeof(T);

            // Try to convert the value to the target type
            try
            {
                if (targetType == typeof(string))
                {
                    return (T)(object)value.ToString();
                }
                else if (targetType == typeof(int) || targetType == typeof(int?))
                {
                    return (T)(object)Convert.ToInt32(value);
                }
                else if (targetType == typeof(decimal) || targetType == typeof(decimal?))
                {
                    return (T)(object)Convert.ToDecimal(value);
                }
                else if (targetType == typeof(DateTime) || targetType == typeof(DateTime?))
                {
                    return (T)(object)Convert.ToDateTime(value);
                }
                else if (targetType == typeof(bool) || targetType == typeof(bool?))
                {
                    if (value is int)
                    {
                        return (T)(object)(Convert.ToInt32(value) != 0);
                    }
                    return (T)(object)Convert.ToBoolean(value);
                }
                else
                {
                    return (T)Convert.ChangeType(value, targetType);
                }
            }
            catch (Exception ex)
            {
                // ErrorManager ile dönüşüm hatasını logla
                ErrorManager.Instance.LogWarning(
                    $"'{columnName}' sütunundaki '{value}' değeri '{targetType.Name}' tipine dönüştürülemedi: {ex.Message}",
                    "DatabaseHelper.GetValue");

                return default(T);
            }
        }
    }

    /// <summary>
    /// Veritabanı işlemlerinde oluşan hataları yönetmek için özel exception sınıfı
    /// </summary>
    public class DatabaseException : Exception
    {
        public string Query { get; }
        public MySqlParameter[] Parameters { get; }

        public DatabaseException(string message, Exception innerException, string query, MySqlParameter[] parameters)
            : base(message, innerException)
        {
            Query = query;
            Parameters = parameters;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Message: {Message}");
            sb.AppendLine($"Query: {Query}");

            if (Parameters != null && Parameters.Length > 0)
            {
                sb.AppendLine("Parameters:");
                foreach (var param in Parameters)
                {
                    sb.AppendLine($"  {param.ParameterName} = {param.Value}");
                }
            }

            if (InnerException != null)
            {
                sb.AppendLine($"Inner Exception: {InnerException.Message}");
            }

            return sb.ToString();
        }
    }
}