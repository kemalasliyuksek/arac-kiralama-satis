using System;

namespace arac_kiralama_satis_desktop.Models
{
    /// <summary>
    /// Dashboard üzerinde gösterilecek özet veriler için model sınıfı
    /// </summary>
    public class DashboardData
    {
        /// <summary>
        /// Toplam araç sayısı
        /// </summary>
        public int TotalCarCount { get; set; }

        /// <summary>
        /// Toplam şube sayısı
        /// </summary>
        public int LocationCount { get; set; }

        /// <summary>
        /// Toplam müşteri sayısı
        /// </summary>
        public int CustomerCount { get; set; }

        /// <summary>
        /// Toplam gelir (Kiralama + Satış)
        /// </summary>
        public decimal TotalRevenue { get; set; }

        /// <summary>
        /// Farklı marka sayısı
        /// </summary>
        public int BrandCount { get; set; }

        /// <summary>
        /// Ortalama kiralama fiyatı
        /// </summary>
        public double AverageRentalPrice { get; set; }

        /// <summary>
        /// Aktif kiralama sayısı
        /// </summary>
        public int ActiveRentalsCount { get; set; }

        /// <summary>
        /// Bu ay yapılan satış sayısı
        /// </summary>
        public int MonthlySalesCount { get; set; }

        /// <summary>
        /// Servis/bakım bekleyen araç sayısı
        /// </summary>
        public int PendingServiceCount { get; set; }

        /// <summary>
        /// Aktif personel sayısı
        /// </summary>
        public int TeamMembersCount { get; set; }

        /// <summary>
        /// DashboardData'nın yeni bir örneğini oluşturur
        /// </summary>
        public DashboardData()
        {
            TotalCarCount = 0;
            LocationCount = 0;
            CustomerCount = 0;
            TotalRevenue = 0;
            BrandCount = 0;
            AverageRentalPrice = 0;
            ActiveRentalsCount = 0;
            MonthlySalesCount = 0;
            PendingServiceCount = 0;
            TeamMembersCount = 0;
        }
    }
}