using System;

namespace arac_kiralama_satis_desktop.Models
{
    public class Vehicle
    {
        public int VehicleID { get; set; }
        public string Plate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string EngineNo { get; set; }
        public string ChassisNo { get; set; }
        public string Color { get; set; }
        public int Kilometers { get; set; }
        public string FuelType { get; set; }
        public string TransmissionType { get; set; }
        public int StatusID { get; set; }
        public string StatusName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal? SalePrice { get; set; }
        public int? BranchID { get; set; }
        public string BranchName { get; set; }
        public int? VehicleClassID { get; set; }
        public string VehicleClassName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public Vehicle()
        {
            Plate = string.Empty;
            Brand = string.Empty;
            Model = string.Empty;
            EngineNo = string.Empty;
            ChassisNo = string.Empty;
            Color = string.Empty;
            FuelType = string.Empty;
            TransmissionType = string.Empty;
            StatusName = string.Empty;
            BranchName = string.Empty;
            VehicleClassName = string.Empty;
        }

        public System.Drawing.Color GetStatusColor()
        {
            return StatusName switch
            {
                "Müsait" => System.Drawing.Color.FromArgb(40, 167, 69),
                "Satılık" => System.Drawing.Color.FromArgb(0, 123, 255),
                "Satıldı" => System.Drawing.Color.FromArgb(108, 117, 125),
                "Kirada" => System.Drawing.Color.FromArgb(255, 193, 7),
                "Rezerve Edildi" => System.Drawing.Color.FromArgb(23, 162, 184),
                "Serviste" => System.Drawing.Color.FromArgb(220, 53, 69),
                "Arızalı" => System.Drawing.Color.FromArgb(220, 53, 69),
                "Bakımda" => System.Drawing.Color.FromArgb(255, 193, 7),
                _ => System.Drawing.Color.FromArgb(108, 117, 125)
            };
        }
    }
}