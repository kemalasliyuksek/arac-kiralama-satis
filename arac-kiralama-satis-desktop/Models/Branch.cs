using System;

namespace arac_kiralama_satis_desktop.Models
{
    public class Branch
    {
        public int BranchID { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string CityCode { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }

        public Branch()
        {
            BranchName = string.Empty;
            Address = string.Empty;
            CountryCode = "+90";
            PhoneNumber = string.Empty;
            Email = string.Empty;
            CityCode = string.Empty;
            IsActive = true;
        }

        public string FullPhoneNumber => $"{CountryCode}{PhoneNumber}";
    }
}