using System;

namespace arac_kiralama_satis_desktop.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string LicenseNumber { get; set; }
        public string LicenseClass { get; set; }
        public DateTime? LicenseDate { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsAvailable { get; set; }
        public string CustomerType { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public Customer()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            IdentityNumber = string.Empty;
            LicenseNumber = string.Empty;
            LicenseClass = string.Empty;
            CountryCode = "+90";
            PhoneNumber = string.Empty;
            Email = string.Empty;
            Address = string.Empty;
            CustomerType = "Bireysel";
            IsAvailable = true;
        }

        public string FullName => $"{FirstName} {LastName}";
        public string FullPhoneNumber => $"{CountryCode}{PhoneNumber}";
    }
}