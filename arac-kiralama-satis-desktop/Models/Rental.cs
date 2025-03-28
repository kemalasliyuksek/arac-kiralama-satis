using System;

namespace arac_kiralama_satis_desktop.Models
{
    public class Rental
    {
        public int RentalID { get; set; }
        public int CustomerID { get; set; }
        public string CustomerFullName { get; set; }
        public int VehicleID { get; set; }
        public string VehiclePlate { get; set; }
        public string VehicleBrand { get; set; }
        public string VehicleModel { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int StartKm { get; set; }
        public int? EndKm { get; set; }
        public decimal RentalAmount { get; set; }
        public decimal? DepositAmount { get; set; }
        public string PaymentType { get; set; }
        public int? NoteID { get; set; }
        public int? ContractID { get; set; }
        public int UserID { get; set; }
        public string UserFullName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public Rental()
        {
            CustomerFullName = string.Empty;
            VehiclePlate = string.Empty;
            VehicleBrand = string.Empty;
            VehicleModel = string.Empty;
            PaymentType = string.Empty;
            UserFullName = string.Empty;
        }

        // Kiralama süresi (gün)
        public int Duration => (EndDate - StartDate).Days + 1;

        // Kiralama durumunu kontrol eder
        public bool IsActive => DateTime.Now >= StartDate && DateTime.Now <= EndDate && !ReturnDate.HasValue;

        // Kiralama gecikme durumunu kontrol eder
        public bool IsOverdue => DateTime.Now > EndDate && !ReturnDate.HasValue;

        // Kilometre farkını hesaplar
        public int? KmDifference => EndKm.HasValue ? EndKm - StartKm : null;
    }
}