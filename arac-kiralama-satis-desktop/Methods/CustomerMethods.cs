using System;
using System.Collections.Generic;
using System.Data;
using arac_kiralama_satis_desktop.Models;
using arac_kiralama_satis_desktop.Repositories;

namespace arac_kiralama_satis_desktop.Methods
{
    public class CustomerMethods
    {
        private static readonly CustomerRepository _repository = new CustomerRepository();

        public static List<Customer> GetCustomers()
        {
            try
            {
                return _repository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Müşteriler listelenirken bir hata oluştu: " + ex.Message);
            }
        }

        public static Customer GetCustomerById(int customerId)
        {
            try
            {
                return _repository.GetById(customerId);
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
                return _repository.Add(customer);
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
                _repository.Update(customer);
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
                _repository.SetAvailability(customerId, isAvailable);
            }
            catch (Exception ex)
            {
                throw new Exception("Müşteri durumu güncellenirken bir hata oluştu: " + ex.Message);
            }
        }

        public static List<Customer> SearchCustomers(string searchText)
        {
            try
            {
                return _repository.Search(searchText);
            }
            catch (Exception ex)
            {
                throw new Exception("Müşteri arama sırasında bir hata oluştu: " + ex.Message);
            }
        }
    }
}