using System;
using System.Collections.Generic;
using System.Data;
using arac_kiralama_satis_desktop.Models;
using arac_kiralama_satis_desktop.Repositories;
using arac_kiralama_satis_desktop.Utils; // ErrorManager için ekledim

namespace arac_kiralama_satis_desktop.Methods
{
    public class CustomerMethods
    {
        private static readonly CustomerRepository _repository = new CustomerRepository();

        public static List<Customer> GetCustomers()
        {
            try
            {
                // İşlem başlangıcını logla
                ErrorManager.Instance.LogInfo("Müşteriler listeleniyor", "CustomerMethods.GetCustomers");
                return _repository.GetAll();
            }
            catch (Exception ex)
            {
                // ErrorManager ile hata yönetimi
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Müşteriler listelenirken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Müşteriler listelenirken bir hata oluştu. (Hata ID: {errorId})");
            }
        }

        public static Customer GetCustomerById(int customerId)
        {
            try
            {
                ErrorManager.Instance.LogInfo($"Müşteri bilgisi alınıyor. Müşteri ID: {customerId}",
                    "CustomerMethods.GetCustomerById");
                return _repository.GetById(customerId);
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Müşteri bilgisi alınırken bir hata oluştu. Müşteri ID: {customerId}",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Müşteri bilgisi alınırken bir hata oluştu. (Hata ID: {errorId})");
            }
        }

        public static int AddCustomer(Customer customer)
        {
            try
            {
                ErrorManager.Instance.LogInfo($"Yeni müşteri ekleniyor. Müşteri adı: {customer.FullName}",
                    "CustomerMethods.AddCustomer");
                return _repository.Add(customer);
            }
            catch (Exception ex)
            {
                // Müşteri ekleme kritik bir işlem olduğu için kullanıcıya göster
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Müşteri eklenirken bir hata oluştu. Müşteri adı: {customer.FullName}",
                    ErrorSeverity.Error,
                    ErrorSource.Database,
                    true); // Kullanıcıya hata gösterilsin

                throw new Exception($"Müşteri eklenirken bir hata oluştu. (Hata ID: {errorId})");
            }
        }

        public static void UpdateCustomer(Customer customer)
        {
            try
            {
                ErrorManager.Instance.LogInfo($"Müşteri güncelleniyor. Müşteri ID: {customer.CustomerID}, Müşteri adı: {customer.FullName}",
                    "CustomerMethods.UpdateCustomer");
                _repository.Update(customer);
            }
            catch (Exception ex)
            {
                // Güncelleme işlemi için kullanıcıya göster
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Müşteri güncellenirken bir hata oluştu. Müşteri ID: {customer.CustomerID}",
                    ErrorSeverity.Error,
                    ErrorSource.Database,
                    true); // Kullanıcıya hata gösterilsin

                throw new Exception($"Müşteri güncellenirken bir hata oluştu. (Hata ID: {errorId})");
            }
        }

        public static void SetCustomerAvailability(int customerId, bool isAvailable)
        {
            try
            {
                string durumText = isAvailable ? "aktif" : "pasif";
                ErrorManager.Instance.LogInfo($"Müşteri durumu {durumText} olarak güncelleniyor. Müşteri ID: {customerId}",
                    "CustomerMethods.SetCustomerAvailability");

                _repository.SetAvailability(customerId, isAvailable);
            }
            catch (Exception ex)
            {
                // Durum değiştirme işlemi için kullanıcıya göster
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Müşteri durumu güncellenirken bir hata oluştu. Müşteri ID: {customerId}",
                    ErrorSeverity.Error,
                    ErrorSource.Database,
                    true); // Kullanıcıya hata gösterilsin

                throw new Exception($"Müşteri durumu güncellenirken bir hata oluştu. (Hata ID: {errorId})");
            }
        }

        public static List<Customer> SearchCustomers(string searchText)
        {
            try
            {
                ErrorManager.Instance.LogInfo($"Müşteri araması yapılıyor. Arama metni: '{searchText}'",
                    "CustomerMethods.SearchCustomers");
                return _repository.Search(searchText);
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Müşteri arama sırasında bir hata oluştu. Arama metni: '{searchText}'",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Müşteri arama sırasında bir hata oluştu. (Hata ID: {errorId})");
            }
        }

        // DataTable dönüşümü için yardımcı metot
        public static DataTable GetCustomersAsDataTable()
        {
            try
            {
                ErrorManager.Instance.LogInfo("Müşteriler DataTable olarak alınıyor",
                    "CustomerMethods.GetCustomersAsDataTable");

                List<Customer> customers = _repository.GetAll();
                DataTable dt = new DataTable();

                // DataTable sütunlarını oluştur
                dt.Columns.Add("MusteriID", typeof(int));
                dt.Columns.Add("Ad", typeof(string));
                dt.Columns.Add("Soyad", typeof(string));
                dt.Columns.Add("TC", typeof(string));
                dt.Columns.Add("Telefon", typeof(string));
                dt.Columns.Add("Email", typeof(string));
                dt.Columns.Add("MusteriTipi", typeof(string));
                dt.Columns.Add("KayitTarihi", typeof(DateTime));
                dt.Columns.Add("MusaitlikDurumu", typeof(bool));

                // Müşterileri DataTable'a ekle
                foreach (var customer in customers)
                {
                    dt.Rows.Add(
                        customer.CustomerID,
                        customer.FirstName,
                        customer.LastName,
                        customer.IdentityNumber,
                        customer.FullPhoneNumber,
                        customer.Email,
                        customer.CustomerType,
                        customer.RegistrationDate,
                        customer.IsAvailable
                    );
                }

                return dt;
            }
            catch (Exception ex)
            {
                // DataTable dönüşümü bir iş mantığı işlemi olduğu için ErrorSource.Business olarak işaretlendi
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Müşteriler DataTable'a dönüştürülürken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Business);

                throw new Exception($"Müşteriler DataTable'a dönüştürülürken bir hata oluştu. (Hata ID: {errorId})");
            }
        }
    }
}