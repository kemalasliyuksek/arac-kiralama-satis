using System;
using System.Collections.Generic;
using System.Data;
using arac_kiralama_satis_desktop.Models;
using arac_kiralama_satis_desktop.Utils;
using MySql.Data.MySqlClient;

namespace arac_kiralama_satis_desktop.Methods
{
    public class CustomerMethods
    {
        public static List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();

            try
            {
                string query = @"SELECT MusteriID, Ad, Soyad, TC, DogumTarihi, EhliyetNo, EhliyetSinifi, 
                               EhliyetTarihi, UlkeKodu, TelefonNo, Email, Adres, KayitTarihi, 
                               MusaitlikDurumu, MusteriTipi, GuncellenmeTarihi
                               FROM Musteriler
                               ORDER BY MusteriID DESC";

                DataTable result = DatabaseConnection.ExecuteQuery(query);

                foreach (DataRow row in result.Rows)
                {
                    Customer customer = new Customer
                    {
                        CustomerID = Convert.ToInt32(row["MusteriID"]),
                        FirstName = row["Ad"].ToString(),
                        LastName = row["Soyad"].ToString(),
                        IdentityNumber = row["TC"].ToString(),
                        BirthDate = row["DogumTarihi"] != DBNull.Value ? Convert.ToDateTime(row["DogumTarihi"]) : (DateTime?)null,
                        LicenseNumber = row["EhliyetNo"] != DBNull.Value ? row["EhliyetNo"].ToString() : string.Empty,
                        LicenseClass = row["EhliyetSinifi"] != DBNull.Value ? row["EhliyetSinifi"].ToString() : string.Empty,
                        LicenseDate = row["EhliyetTarihi"] != DBNull.Value ? Convert.ToDateTime(row["EhliyetTarihi"]) : (DateTime?)null,
                        CountryCode = row["UlkeKodu"].ToString(),
                        PhoneNumber = row["TelefonNo"].ToString(),
                        Email = row["Email"] != DBNull.Value ? row["Email"].ToString() : string.Empty,
                        Address = row["Adres"] != DBNull.Value ? row["Adres"].ToString() : string.Empty,
                        RegistrationDate = Convert.ToDateTime(row["KayitTarihi"]),
                        IsAvailable = Convert.ToBoolean(row["MusaitlikDurumu"]),
                        CustomerType = row["MusteriTipi"].ToString(),
                        UpdatedDate = row["GuncellenmeTarihi"] != DBNull.Value ? Convert.ToDateTime(row["GuncellenmeTarihi"]) : (DateTime?)null
                    };

                    customers.Add(customer);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Müşteriler listelenirken bir hata oluştu: " + ex.Message);
            }

            return customers;
        }

        public static Customer GetCustomerById(int customerId)
        {
            try
            {
                string query = @"SELECT MusteriID, Ad, Soyad, TC, DogumTarihi, EhliyetNo, EhliyetSinifi, 
                               EhliyetTarihi, UlkeKodu, TelefonNo, Email, Adres, KayitTarihi, 
                               MusaitlikDurumu, MusteriTipi, GuncellenmeTarihi
                               FROM Musteriler
                               WHERE MusteriID = @musteriId";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@musteriId", customerId)
                };

                DataTable result = DatabaseConnection.ExecuteQuery(query, parameters);

                if (result.Rows.Count > 0)
                {
                    DataRow row = result.Rows[0];
                    Customer customer = new Customer
                    {
                        CustomerID = Convert.ToInt32(row["MusteriID"]),
                        FirstName = row["Ad"].ToString(),
                        LastName = row["Soyad"].ToString(),
                        IdentityNumber = row["TC"].ToString(),
                        BirthDate = row["DogumTarihi"] != DBNull.Value ? Convert.ToDateTime(row["DogumTarihi"]) : (DateTime?)null,
                        LicenseNumber = row["EhliyetNo"] != DBNull.Value ? row["EhliyetNo"].ToString() : string.Empty,
                        LicenseClass = row["EhliyetSinifi"] != DBNull.Value ? row["EhliyetSinifi"].ToString() : string.Empty,
                        LicenseDate = row["EhliyetTarihi"] != DBNull.Value ? Convert.ToDateTime(row["EhliyetTarihi"]) : (DateTime?)null,
                        CountryCode = row["UlkeKodu"].ToString(),
                        PhoneNumber = row["TelefonNo"].ToString(),
                        Email = row["Email"] != DBNull.Value ? row["Email"].ToString() : string.Empty,
                        Address = row["Adres"] != DBNull.Value ? row["Adres"].ToString() : string.Empty,
                        RegistrationDate = Convert.ToDateTime(row["KayitTarihi"]),
                        IsAvailable = Convert.ToBoolean(row["MusaitlikDurumu"]),
                        CustomerType = row["MusteriTipi"].ToString(),
                        UpdatedDate = row["GuncellenmeTarihi"] != DBNull.Value ? Convert.ToDateTime(row["GuncellenmeTarihi"]) : (DateTime?)null
                    };

                    return customer;
                }
                else
                {
                    throw new Exception("Müşteri bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Müşteri bilgisi alınırken bir hata oluştu: " + ex.Message);
            }
        }

        public static int AddCustomer(Customer customer)
        {
            try
            {
                string query = @"INSERT INTO Musteriler 
                               (Ad, Soyad, TC, DogumTarihi, EhliyetNo, EhliyetSinifi, EhliyetTarihi,
                                UlkeKodu, TelefonNo, Email, Adres, MusaitlikDurumu, MusteriTipi)
                               VALUES 
                               (@ad, @soyad, @tc, @dogumTarihi, @ehliyetNo, @ehliyetSinifi, @ehliyetTarihi,
                                @ulkeKodu, @telefonNo, @email, @adres, @musaitlikDurumu, @musteriTipi);
                               SELECT LAST_INSERT_ID();";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@ad", customer.FirstName),
                    new MySqlParameter("@soyad", customer.LastName),
                    new MySqlParameter("@tc", !string.IsNullOrEmpty(customer.IdentityNumber) ? (object)customer.IdentityNumber : DBNull.Value),
                    new MySqlParameter("@dogumTarihi", customer.BirthDate.HasValue ? (object)customer.BirthDate.Value : DBNull.Value),
                    new MySqlParameter("@ehliyetNo", !string.IsNullOrEmpty(customer.LicenseNumber) ? (object)customer.LicenseNumber : DBNull.Value),
                    new MySqlParameter("@ehliyetSinifi", !string.IsNullOrEmpty(customer.LicenseClass) ? (object)customer.LicenseClass : DBNull.Value),
                    new MySqlParameter("@ehliyetTarihi", customer.LicenseDate.HasValue ? (object)customer.LicenseDate.Value : DBNull.Value),
                    new MySqlParameter("@ulkeKodu", customer.CountryCode),
                    new MySqlParameter("@telefonNo", customer.PhoneNumber),
                    new MySqlParameter("@email", !string.IsNullOrEmpty(customer.Email) ? (object)customer.Email : DBNull.Value),
                    new MySqlParameter("@adres", !string.IsNullOrEmpty(customer.Address) ? (object)customer.Address : DBNull.Value),
                    new MySqlParameter("@musaitlikDurumu", customer.IsAvailable),
                    new MySqlParameter("@musteriTipi", customer.CustomerType)
                };

                object result = DatabaseConnection.ExecuteScalar(query, parameters);
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Müşteri eklenirken bir hata oluştu: " + ex.Message);
            }
        }

        public static void UpdateCustomer(Customer customer)
        {
            try
            {
                string query = @"UPDATE Musteriler SET 
                               Ad = @ad, 
                               Soyad = @soyad, 
                               TC = @tc, 
                               DogumTarihi = @dogumTarihi, 
                               EhliyetNo = @ehliyetNo, 
                               EhliyetSinifi = @ehliyetSinifi, 
                               EhliyetTarihi = @ehliyetTarihi,
                               UlkeKodu = @ulkeKodu, 
                               TelefonNo = @telefonNo, 
                               Email = @email, 
                               Adres = @adres, 
                               MusaitlikDurumu = @musaitlikDurumu, 
                               MusteriTipi = @musteriTipi
                               WHERE MusteriID = @musteriId";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@musteriId", customer.CustomerID),
                    new MySqlParameter("@ad", customer.FirstName),
                    new MySqlParameter("@soyad", customer.LastName),
                    new MySqlParameter("@tc", !string.IsNullOrEmpty(customer.IdentityNumber) ? (object)customer.IdentityNumber : DBNull.Value),
                    new MySqlParameter("@dogumTarihi", customer.BirthDate.HasValue ? (object)customer.BirthDate.Value : DBNull.Value),
                    new MySqlParameter("@ehliyetNo", !string.IsNullOrEmpty(customer.LicenseNumber) ? (object)customer.LicenseNumber : DBNull.Value),
                    new MySqlParameter("@ehliyetSinifi", !string.IsNullOrEmpty(customer.LicenseClass) ? (object)customer.LicenseClass : DBNull.Value),
                    new MySqlParameter("@ehliyetTarihi", customer.LicenseDate.HasValue ? (object)customer.LicenseDate.Value : DBNull.Value),
                    new MySqlParameter("@ulkeKodu", customer.CountryCode),
                    new MySqlParameter("@telefonNo", customer.PhoneNumber),
                    new MySqlParameter("@email", !string.IsNullOrEmpty(customer.Email) ? (object)customer.Email : DBNull.Value),
                    new MySqlParameter("@adres", !string.IsNullOrEmpty(customer.Address) ? (object)customer.Address : DBNull.Value),
                    new MySqlParameter("@musaitlikDurumu", customer.IsAvailable),
                    new MySqlParameter("@musteriTipi", customer.CustomerType)
                };

                DatabaseConnection.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Müşteri güncellenirken bir hata oluştu: " + ex.Message);
            }
        }

        public static void SetCustomerAvailability(int customerId, bool isAvailable)
        {
            try
            {
                string query = "UPDATE Musteriler SET MusaitlikDurumu = @musaitlikDurumu WHERE MusteriID = @musteriId";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@musteriId", customerId),
                    new MySqlParameter("@musaitlikDurumu", isAvailable)
                };

                DatabaseConnection.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Müşteri durumu güncellenirken bir hata oluştu: " + ex.Message);
            }
        }

        public static List<Customer> SearchCustomers(string searchText)
        {
            List<Customer> customers = new List<Customer>();

            try
            {
                string query = @"SELECT MusteriID, Ad, Soyad, TC, DogumTarihi, EhliyetNo, EhliyetSinifi, 
                               EhliyetTarihi, UlkeKodu, TelefonNo, Email, Adres, KayitTarihi, 
                               MusaitlikDurumu, MusteriTipi, GuncellenmeTarihi
                               FROM Musteriler
                               WHERE Ad LIKE @searchText 
                               OR Soyad LIKE @searchText 
                               OR TC LIKE @searchText 
                               OR TelefonNo LIKE @searchText 
                               OR Email LIKE @searchText
                               ORDER BY Ad, Soyad";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@searchText", "%" + searchText + "%")
                };

                DataTable result = DatabaseConnection.ExecuteQuery(query, parameters);

                foreach (DataRow row in result.Rows)
                {
                    Customer customer = new Customer
                    {
                        CustomerID = Convert.ToInt32(row["MusteriID"]),
                        FirstName = row["Ad"].ToString(),
                        LastName = row["Soyad"].ToString(),
                        IdentityNumber = row["TC"].ToString(),
                        BirthDate = row["DogumTarihi"] != DBNull.Value ? Convert.ToDateTime(row["DogumTarihi"]) : (DateTime?)null,
                        LicenseNumber = row["EhliyetNo"] != DBNull.Value ? row["EhliyetNo"].ToString() : string.Empty,
                        LicenseClass = row["EhliyetSinifi"] != DBNull.Value ? row["EhliyetSinifi"].ToString() : string.Empty,
                        LicenseDate = row["EhliyetTarihi"] != DBNull.Value ? Convert.ToDateTime(row["EhliyetTarihi"]) : (DateTime?)null,
                        CountryCode = row["UlkeKodu"].ToString(),
                        PhoneNumber = row["TelefonNo"].ToString(),
                        Email = row["Email"] != DBNull.Value ? row["Email"].ToString() : string.Empty,
                        Address = row["Adres"] != DBNull.Value ? row["Adres"].ToString() : string.Empty,
                        RegistrationDate = Convert.ToDateTime(row["KayitTarihi"]),
                        IsAvailable = Convert.ToBoolean(row["MusaitlikDurumu"]),
                        CustomerType = row["MusteriTipi"].ToString(),
                        UpdatedDate = row["GuncellenmeTarihi"] != DBNull.Value ? Convert.ToDateTime(row["GuncellenmeTarihi"]) : (DateTime?)null
                    };

                    customers.Add(customer);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Müşteri arama sırasında bir hata oluştu: " + ex.Message);
            }

            return customers;
        }
    }
}