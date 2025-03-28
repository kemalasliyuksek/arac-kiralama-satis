using System;

namespace arac_kiralama_satis_desktop.Models
{
    public class Sale
    {
        public int SaleID { get; set; }
        public int CustomerID { get; set; }
        public string CustomerFullName { get; set; }
        public int VehicleID { get; set; }
        public string VehiclePlate { get; set; }
        public string VehicleBrand { get; set; }
        public string VehicleModel { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal SaleAmount { get; set; }
        public string PaymentType { get; set; }
        public int InstallmentCount { get; set; }
        public DateTime? NotaryDate { get; set; }
        public int? ContractID { get; set; }
        public int UserID { get; set; }
        public string UserFullName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public Sale()
        {
            CustomerFullName = string.Empty;
            VehiclePlate = string.Empty;
            VehicleBrand = string.Empty;
            VehicleModel = string.Empty;
            PaymentType = string.Empty;
            UserFullName = string.Empty;
        }

        public bool IsCashPayment => InstallmentCount == 0;
        public bool IsInstallment => InstallmentCount > 0;
        public decimal InstallmentAmount => IsInstallment ? SaleAmount / InstallmentCount : 0;
    }
}