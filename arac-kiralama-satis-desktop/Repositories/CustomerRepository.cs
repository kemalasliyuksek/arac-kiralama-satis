using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using arac_kiralama_satis_desktop.Models;
using arac_kiralama_satis_desktop.Utils;

namespace arac_kiralama_satis_desktop.Repositories
{
    /// <summary>
    /// Müşteri işlemleri için repository sınıfı
    /// </summary>
    public class CustomerRepository
    {
        private const string TABLE_NAME = "Musteriler";

        /// <summary>
        /// Tüm müşterileri getirir
        /// </summary>
        public List<Customer> GetAll()
        {
            try
            {
                string query = $@"SELECT MusteriID, Ad, Soyad, TC, DogumTarihi, EhliyetNo, EhliyetSinifi, 
                               EhliyetTarihi, UlkeKodu, TelefonNo, Email, Adres, KayitTarihi, 
                               MusaitlikDurumu, MusteriTipi, GuncellenmeTarihi
                               FROM {TABLE_NAME}
                               ORDER BY MusteriID DESC";

                DataTable result = DatabaseHelper.ExecuteQuery(query);
                return MapDataTableToCustomers(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Müşteriler listelenirken bir hata oluştu: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Müşteri ID'sine göre müşteri bilgisini getirir
        /// </summary>
        public Customer GetById(int customerId)
        {
            try
            {
                string query = $@"SELECT MusteriID, Ad, Soyad, TC, DogumTarihi, EhliyetNo, EhliyetSinifi, 
                               EhliyetTarihi, UlkeKodu, TelefonNo, Email, Adres, KayitTarihi, 
                               MusaitlikDurumu, MusteriTipi, GuncellenmeTarihi
                               FROM {TABLE_NAME}
                               WHERE MusteriID = @musteriId";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    DatabaseHelper.CreateParameter("@musteriId", customerId)
                };

                DataTable result = DatabaseHelper.ExecuteQuery(query, parameters);

                if (result.Rows.Count > 0)
                {
                    return MapDataRowToCustomer(result.Rows[0]);
                }
                else
                {
                    throw new Exception("Müşteri bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Müşteri bilgisi alınırken bir hata oluştu: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Yeni müşteri ekler
        /// </summary>
        public int Add(Customer customer)
        {
            try
            {
                string query = $@"INSERT INTO {TABLE_NAME} 
                               (Ad, Soyad, TC, DogumTarihi, EhliyetNo, EhliyetSinifi, EhliyetTarihi,
                                UlkeKodu, TelefonNo, Email, Adres, MusaitlikDurumu, MusteriTipi)
                               VALUES 
                               (@ad, @soyad, @tc, @dogumTarihi, @ehliyetNo, @ehliyetSinifi, @ehliyetTarihi,
                                @ulkeKodu, @telefonNo, @email, @adres, @musaitlikDurumu, @musteriTipi);
                               SELECT LAST_INSERT_ID();";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    DatabaseHelper.CreateParameter("@ad", customer.FirstName),
                    DatabaseHelper.CreateParameter("@soyad", customer.LastName),
                    DatabaseHelper.CreateParameter("@tc", customer.IdentityNumber),
                    DatabaseHelper.CreateParameter("@dogumTarihi", customer.BirthDate),
                    DatabaseHelper.CreateParameter("@ehliyetNo", customer.LicenseNumber),
                    DatabaseHelper.CreateParameter("@ehliyetSinifi", customer.LicenseClass),
                    DatabaseHelper.CreateParameter("@ehliyetTarihi", customer.LicenseDate),
                    DatabaseHelper.CreateParameter("@ulkeKodu", customer.CountryCode),
                    DatabaseHelper.CreateParameter("@telefonNo", customer.PhoneNumber),
                    DatabaseHelper.CreateParameter("@email", customer.Email),
                    DatabaseHelper.CreateParameter("@adres", customer.Address),
                    DatabaseHelper.CreateParameter("@musaitlikDurumu", customer.IsAvailable),
                    DatabaseHelper.CreateParameter("@musteriTipi", customer.CustomerType)
                };

                object result = DatabaseHelper.ExecuteScalar(query, parameters);
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Müşteri eklenirken bir hata oluştu: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Müşteri bilgilerini günceller
        /// </summary>
        public void Update(Customer customer)
        {
            try
            {
                string query = $@"UPDATE {TABLE_NAME} SET 
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
                    DatabaseHelper.CreateParameter("@musteriId", customer.CustomerID),
                    DatabaseHelper.CreateParameter("@ad", customer.FirstName),
                    DatabaseHelper.CreateParameter("@soyad", customer.LastName),
                    DatabaseHelper.CreateParameter("@tc", customer.IdentityNumber),
                    DatabaseHelper.CreateParameter("@dogumTarihi", customer.BirthDate),
                    DatabaseHelper.CreateParameter("@ehliyetNo", customer.LicenseNumber),
                    DatabaseHelper.CreateParameter("@ehliyetSinifi", customer.LicenseClass),
                    DatabaseHelper.CreateParameter("@ehliyetTarihi", customer.LicenseDate),
                    DatabaseHelper.CreateParameter("@ulkeKodu", customer.CountryCode),
                    DatabaseHelper.CreateParameter("@telefonNo", customer.PhoneNumber),
                    DatabaseHelper.CreateParameter("@email", customer.Email),
                    DatabaseHelper.CreateParameter("@adres", customer.Address),
                    DatabaseHelper.CreateParameter("@musaitlikDurumu", customer.IsAvailable),
                    DatabaseHelper.CreateParameter("@musteriTipi", customer.CustomerType)
                };

                DatabaseHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Müşteri güncellenirken bir hata oluştu: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Müşterinin durumunu güncelleyen metot (aktif/pasif)
        /// </summary>
        public void SetAvailability(int customerId, bool isAvailable)
        {
            try
            {
                string query = $"UPDATE {TABLE_NAME} SET MusaitlikDurumu = @musaitlikDurumu WHERE MusteriID = @musteriId";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    DatabaseHelper.CreateParameter("@musteriId", customerId),
                    DatabaseHelper.CreateParameter("@musaitlikDurumu", isAvailable)
                };

                DatabaseHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Müşteri durumu güncellenirken bir hata oluştu: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Müşteri arama işlemi için metot
        /// </summary>
        public List<Customer> Search(string searchText)
        {
            try
            {
                string query = $@"SELECT MusteriID, Ad, Soyad, TC, DogumTarihi, EhliyetNo, EhliyetSinifi, 
                               EhliyetTarihi, UlkeKodu, TelefonNo, Email, Adres, KayitTarihi, 
                               MusaitlikDurumu, MusteriTipi, GuncellenmeTarihi
                               FROM {TABLE_NAME}
                               WHERE Ad LIKE @searchText 
                               OR Soyad LIKE @searchText 
                               OR TC LIKE @searchText 
                               OR TelefonNo LIKE @searchText 
                               OR Email LIKE @searchText
                               ORDER BY Ad, Soyad";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    DatabaseHelper.CreateParameter("@searchText", "%" + searchText + "%")
                };

                DataTable result = DatabaseHelper.ExecuteQuery(query, parameters);
                return MapDataTableToCustomers(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Müşteri arama sırasında bir hata oluştu: " + ex.Message, ex);
            }
        }

        #region Helper Methods

        /// <summary>
        /// DataTable'ı Customer listesine dönüştürür
        /// </summary>
        private List<Customer> MapDataTableToCustomers(DataTable dataTable)
        {
            List<Customer> customers = new List<Customer>();

            foreach (DataRow row in dataTable.Rows)
            {
                customers.Add(MapDataRowToCustomer(row));
            }

            return customers;
        }

        /// <summary>
        /// DataRow'u Customer nesnesine dönüştürür
        /// </summary>
        private Customer MapDataRowToCustomer(DataRow row)
        {
            return new Customer
            {
                CustomerID = row.GetValue<int>("MusteriID"),
                FirstName = row.GetValue<string>("Ad"),
                LastName = row.GetValue<string>("Soyad"),
                IdentityNumber = row.GetValue<string>("TC"),
                BirthDate = row.GetValue<DateTime?>("DogumTarihi"),
                LicenseNumber = row.GetValue<string>("EhliyetNo"),
                LicenseClass = row.GetValue<string>("EhliyetSinifi"),
                LicenseDate = row.GetValue<DateTime?>("EhliyetTarihi"),
                CountryCode = row.GetValue<string>("UlkeKodu"),
                PhoneNumber = row.GetValue<string>("TelefonNo"),
                Email = row.GetValue<string>("Email"),
                Address = row.GetValue<string>("Adres"),
                RegistrationDate = row.GetValue<DateTime>("KayitTarihi"),
                IsAvailable = row.GetValue<bool>("MusaitlikDurumu"),
                CustomerType = row.GetValue<string>("MusteriTipi"),
                UpdatedDate = row.GetValue<DateTime?>("GuncellenmeTarihi")
            };
        }

        #endregion
    }
}