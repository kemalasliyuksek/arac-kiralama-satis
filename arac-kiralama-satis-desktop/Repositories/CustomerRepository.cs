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

                ErrorManager.Instance.LogInfo("Tüm müşteriler getiriliyor", "CustomerRepository.GetAll");
                DataTable result = DatabaseHelper.ExecuteQuery(query);
                List<Customer> customers = MapDataTableToCustomers(result);
                ErrorManager.Instance.LogInfo($"{customers.Count} müşteri başarıyla listelendi", "CustomerRepository.GetAll");

                return customers;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Müşteriler listelenirken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Müşteriler listelenirken bir hata oluştu. (Hata ID: {errorId})", ex);
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

                ErrorManager.Instance.LogInfo($"Müşteri ID: {customerId} ile aranıyor", "CustomerRepository.GetById");
                DataTable result = DatabaseHelper.ExecuteQuery(query, parameters);

                if (result.Rows.Count > 0)
                {
                    Customer customer = MapDataRowToCustomer(result.Rows[0]);
                    ErrorManager.Instance.LogInfo($"Müşteri bulundu: {customer.FirstName} {customer.LastName} (ID: {customer.CustomerID})", "CustomerRepository.GetById");
                    return customer;
                }
                else
                {
                    ErrorManager.Instance.LogWarning($"Müşteri bulunamadı. ID: {customerId}", "CustomerRepository.GetById");
                    throw new Exception($"ID: {customerId} ile müşteri bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Müşteri bilgisi alınırken bir hata oluştu (ID: {customerId})",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Müşteri bilgisi alınırken bir hata oluştu. (Hata ID: {errorId})", ex);
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

                ErrorManager.Instance.LogInfo($"Yeni müşteri ekleniyor: {customer.FirstName} {customer.LastName}", "CustomerRepository.Add");
                object result = DatabaseHelper.ExecuteScalar(query, parameters);
                int newId = Convert.ToInt32(result);

                ErrorManager.Instance.LogInfo($"Yeni müşteri başarıyla eklendi. ID: {newId}, Ad: {customer.FirstName} {customer.LastName}", "CustomerRepository.Add");
                return newId;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Müşteri eklenirken bir hata oluştu: {customer.FirstName} {customer.LastName}",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Müşteri eklenirken bir hata oluştu. (Hata ID: {errorId})", ex);
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
                               MusteriTipi = @musteriTipi,
                               GuncellenmeTarihi = NOW()
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

                ErrorManager.Instance.LogInfo($"Müşteri güncelleniyor. ID: {customer.CustomerID}, Ad: {customer.FirstName} {customer.LastName}", "CustomerRepository.Update");
                int affectedRows = DatabaseHelper.ExecuteNonQuery(query, parameters);

                if (affectedRows > 0)
                {
                    ErrorManager.Instance.LogInfo($"Müşteri başarıyla güncellendi. ID: {customer.CustomerID}, Ad: {customer.FirstName} {customer.LastName}", "CustomerRepository.Update");
                }
                else
                {
                    ErrorManager.Instance.LogWarning($"Müşteri güncellenemedi, kayıt bulunamadı. ID: {customer.CustomerID}", "CustomerRepository.Update");
                }
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Müşteri güncellenirken bir hata oluştu. ID: {customer.CustomerID}, Ad: {customer.FirstName} {customer.LastName}",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Müşteri güncellenirken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// Müşterinin durumunu güncelleyen metot (aktif/pasif)
        /// </summary>
        public void SetAvailability(int customerId, bool isAvailable)
        {
            try
            {
                string query = $@"UPDATE {TABLE_NAME} 
                               SET MusaitlikDurumu = @musaitlikDurumu,
                               GuncellenmeTarihi = NOW()
                               WHERE MusteriID = @musteriId";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    DatabaseHelper.CreateParameter("@musteriId", customerId),
                    DatabaseHelper.CreateParameter("@musaitlikDurumu", isAvailable)
                };

                string statusText = isAvailable ? "aktif" : "pasif";
                ErrorManager.Instance.LogInfo($"Müşteri durumu {statusText} olarak güncelleniyor. ID: {customerId}", "CustomerRepository.SetAvailability");

                int affectedRows = DatabaseHelper.ExecuteNonQuery(query, parameters);

                if (affectedRows > 0)
                {
                    ErrorManager.Instance.LogInfo($"Müşteri durumu başarıyla {statusText} olarak güncellendi. ID: {customerId}", "CustomerRepository.SetAvailability");
                }
                else
                {
                    ErrorManager.Instance.LogWarning($"Müşteri durumu güncellenemedi, kayıt bulunamadı. ID: {customerId}", "CustomerRepository.SetAvailability");
                }
            }
            catch (Exception ex)
            {
                string statusText = isAvailable ? "aktif" : "pasif";
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Müşteri {statusText} durumuna getirilirken bir hata oluştu (ID: {customerId})",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Müşteri durumu güncellenirken bir hata oluştu. (Hata ID: {errorId})", ex);
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

                ErrorManager.Instance.LogInfo($"Müşteri arama yapılıyor. Arama metni: '{searchText}'", "CustomerRepository.Search");
                DataTable result = DatabaseHelper.ExecuteQuery(query, parameters);
                List<Customer> customers = MapDataTableToCustomers(result);

                ErrorManager.Instance.LogInfo($"Müşteri araması tamamlandı. '{searchText}' için {customers.Count} sonuç bulundu", "CustomerRepository.Search");
                return customers;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Müşteri arama sırasında bir hata oluştu. Arama metni: '{searchText}'",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Müşteri arama sırasında bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        #region Helper Methods

        /// <summary>
        /// DataTable'ı Customer listesine dönüştürür
        /// </summary>
        private List<Customer> MapDataTableToCustomers(DataTable dataTable)
        {
            try
            {
                List<Customer> customers = new List<Customer>();

                foreach (DataRow row in dataTable.Rows)
                {
                    customers.Add(MapDataRowToCustomer(row));
                }

                return customers;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Müşteri verileri dönüştürülürken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Business);

                throw new Exception($"Müşteri verileri dönüştürülürken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// DataRow'u Customer nesnesine dönüştürür
        /// </summary>
        private Customer MapDataRowToCustomer(DataRow row)
        {
            try
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
            catch (Exception ex)
            {
                // Hangi satırla ilgili sorun olduğunu belirlemek için
                int customerId = 0;
                try { customerId = row.GetValue<int>("MusteriID"); } catch { }

                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Müşteri verisi dönüştürme sırasında hata oluştu (MusteriID: {customerId})",
                    ErrorSeverity.Error,
                    ErrorSource.Business);

                throw new Exception($"Müşteri verisi dönüştürülürken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        #endregion
    }
}