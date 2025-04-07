using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using arac_kiralama_satis_desktop.Models;
using arac_kiralama_satis_desktop.Utils;

namespace arac_kiralama_satis_desktop.Repositories
{
    public class SaleRepository : IRepository<Sale, int>
    {
        private const string TABLE_NAME = "Satislar";

        public List<Sale> GetAll()
        {
            try
            {
                string query = $@"SELECT s.SatisID, s.MusteriID, CONCAT(m.Ad, ' ', m.Soyad) AS MusteriAdSoyad,
                                         s.AracID, a.Plaka, a.Marka, a.Model,
                                         s.SatisTarihi, s.SatisTutari, s.OdemeTipi, s.TaksitSayisi, s.NoterTarihi,
                                         s.SozlesmeID, s.KullaniciID, CONCAT(u.Ad, ' ', u.Soyad) AS KullaniciAdSoyad,
                                         s.OlusturmaTarihi, s.GuncellenmeTarihi
                                  FROM {TABLE_NAME} s
                                  JOIN Musteriler m ON s.MusteriID = m.MusteriID
                                  JOIN Araclar a ON s.AracID = a.AracID
                                  JOIN Kullanicilar u ON s.KullaniciID = u.KullaniciID
                                  ORDER BY s.SatisTarihi DESC";

                ErrorManager.Instance.LogInfo("Tüm satış kayıtları getiriliyor", "SaleRepository.GetAll");
                DataTable dt = DatabaseHelper.ExecuteQuery(query);
                List<Sale> sales = MapDataTableToSales(dt);
                ErrorManager.Instance.LogInfo($"{sales.Count} satış kaydı başarıyla listelendi", "SaleRepository.GetAll");

                return sales;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Satışlar listelenirken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Satışlar listelenirken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        public Sale GetById(int id)
        {
            try
            {
                string query = $@"SELECT s.SatisID, s.MusteriID, CONCAT(m.Ad, ' ', m.Soyad) AS MusteriAdSoyad,
                                         s.AracID, a.Plaka, a.Marka, a.Model,
                                         s.SatisTarihi, s.SatisTutari, s.OdemeTipi, s.TaksitSayisi, s.NoterTarihi,
                                         s.SozlesmeID, s.KullaniciID, CONCAT(u.Ad, ' ', u.Soyad) AS KullaniciAdSoyad,
                                         s.OlusturmaTarihi, s.GuncellenmeTarihi
                                  FROM {TABLE_NAME} s
                                  JOIN Musteriler m ON s.MusteriID = m.MusteriID
                                  JOIN Araclar a ON s.AracID = a.AracID
                                  JOIN Kullanicilar u ON s.KullaniciID = u.KullaniciID
                                  WHERE s.SatisID = @id";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@id", id)
                };

                ErrorManager.Instance.LogInfo($"Satış ID: {id} ile aranıyor", "SaleRepository.GetById");
                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    Sale sale = MapDataRowToSale(dt.Rows[0]);
                    ErrorManager.Instance.LogInfo($"Satış bulundu: ID: {sale.SaleID}, Müşteri: {sale.CustomerFullName}, Araç: {sale.VehiclePlate}, Tutar: {sale.SaleAmount:C2}", "SaleRepository.GetById");
                    return sale;
                }
                else
                {
                    ErrorManager.Instance.LogWarning($"Satış kaydı bulunamadı. ID: {id}", "SaleRepository.GetById");
                    throw new Exception($"ID: {id} ile satış kaydı bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Satış bilgisi alınırken bir hata oluştu (ID: {id})",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Satış bilgisi alınırken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        public int Add(Sale entity)
        {
            try
            {
                string query = $@"INSERT INTO {TABLE_NAME}
                                  (MusteriID, AracID, SatisTarihi, SatisTutari, OdemeTipi, TaksitSayisi, NoterTarihi,
                                   SozlesmeID, KullaniciID)
                                  VALUES
                                  (@musteriId, @aracId, @satisTarihi, @tutar, @odeme, @taksit, @noter,
                                   @sozlesme, @kulId);
                                  SELECT LAST_INSERT_ID();";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@musteriId", entity.CustomerID),
                    DatabaseHelper.CreateParameter("@aracId", entity.VehicleID),
                    DatabaseHelper.CreateParameter("@satisTarihi", entity.SaleDate),
                    DatabaseHelper.CreateParameter("@tutar", entity.SaleAmount),
                    DatabaseHelper.CreateParameter("@odeme", entity.PaymentType),
                    DatabaseHelper.CreateParameter("@taksit", entity.InstallmentCount),
                    DatabaseHelper.CreateParameter("@noter", entity.NotaryDate ?? (object)DBNull.Value),
                    DatabaseHelper.CreateParameter("@sozlesme", entity.ContractID ?? (object)DBNull.Value),
                    DatabaseHelper.CreateParameter("@kulId", entity.UserID)
                };

                string odemeDetay = entity.IsCashPayment ?
                    "Nakit Ödeme" :
                    $"Taksitli Ödeme ({entity.InstallmentCount} taksit, taksit tutarı: {entity.InstallmentAmount:C2})";

                ErrorManager.Instance.LogInfo($"Yeni satış ekleniyor: Müşteri: {entity.CustomerFullName}, " +
                    $"Araç: {entity.VehiclePlate} {entity.VehicleBrand} {entity.VehicleModel}, " +
                    $"Tutar: {entity.SaleAmount:C2}, {odemeDetay}", "SaleRepository.Add");

                object result = DatabaseHelper.ExecuteScalar(query, parameters);
                int newId = Convert.ToInt32(result);

                ErrorManager.Instance.LogInfo($"Yeni satış başarıyla eklendi. ID: {newId}, Müşteri: {entity.CustomerFullName}, " +
                    $"Araç: {entity.VehiclePlate}, Tutar: {entity.SaleAmount:C2}", "SaleRepository.Add");

                return newId;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Satış eklenirken bir hata oluştu: Müşteri: {entity.CustomerFullName}, Araç: {entity.VehiclePlate}",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Satış eklenirken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        public void Update(Sale entity)
        {
            try
            {
                string query = $@"UPDATE {TABLE_NAME} SET
                                  MusteriID = @musteriId,
                                  AracID = @aracId,
                                  SatisTarihi = @satisTarihi,
                                  SatisTutari = @tutar,
                                  OdemeTipi = @odeme,
                                  TaksitSayisi = @taksit,
                                  NoterTarihi = @noter,
                                  SozlesmeID = @sozlesme,
                                  KullaniciID = @kulId,
                                  GuncellenmeTarihi = NOW()
                                  WHERE SatisID = @id";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@id", entity.SaleID),
                    DatabaseHelper.CreateParameter("@musteriId", entity.CustomerID),
                    DatabaseHelper.CreateParameter("@aracId", entity.VehicleID),
                    DatabaseHelper.CreateParameter("@satisTarihi", entity.SaleDate),
                    DatabaseHelper.CreateParameter("@tutar", entity.SaleAmount),
                    DatabaseHelper.CreateParameter("@odeme", entity.PaymentType),
                    DatabaseHelper.CreateParameter("@taksit", entity.InstallmentCount),
                    DatabaseHelper.CreateParameter("@noter", entity.NotaryDate ?? (object)DBNull.Value),
                    DatabaseHelper.CreateParameter("@sozlesme", entity.ContractID ?? (object)DBNull.Value),
                    DatabaseHelper.CreateParameter("@kulId", entity.UserID)
                };

                string odemeDetay = entity.IsCashPayment ?
                    "Nakit Ödeme" :
                    $"Taksitli Ödeme ({entity.InstallmentCount} taksit, taksit tutarı: {entity.InstallmentAmount:C2})";

                string noterDurumu = entity.NotaryDate.HasValue ?
                    $"Noter Tarihi: {entity.NotaryDate.Value:dd.MM.yyyy}" :
                    "Noter işlemi henüz yapılmadı";

                ErrorManager.Instance.LogInfo($"Satış güncelleniyor. ID: {entity.SaleID}, Müşteri: {entity.CustomerFullName}, " +
                    $"Araç: {entity.VehiclePlate}, Tutar: {entity.SaleAmount:C2}, {odemeDetay}, {noterDurumu}", "SaleRepository.Update");

                int affectedRows = DatabaseHelper.ExecuteNonQuery(query, parameters);

                if (affectedRows > 0)
                {
                    ErrorManager.Instance.LogInfo($"Satış başarıyla güncellendi. ID: {entity.SaleID}, Müşteri: {entity.CustomerFullName}", "SaleRepository.Update");
                }
                else
                {
                    ErrorManager.Instance.LogWarning($"Satış güncellenemedi, kayıt bulunamadı. ID: {entity.SaleID}", "SaleRepository.Update");
                }
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Satış güncellenirken bir hata oluştu. ID: {entity.SaleID}, Müşteri: {entity.CustomerFullName}",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Satış güncellenirken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                // Önce satış bilgilerini al
                Sale sale = null;
                try
                {
                    sale = GetById(id);
                    ErrorManager.Instance.LogInfo($"Satış silme işlemi için veri alındı. ID: {id}, Müşteri: {sale.CustomerFullName}, Araç: {sale.VehiclePlate}, Tutar: {sale.SaleAmount:C2}", "SaleRepository.Delete");
                }
                catch
                {
                    ErrorManager.Instance.LogWarning($"Silinecek satış kaydı bulunamadı. ID: {id}", "SaleRepository.Delete");
                }

                // Satış kaydını sil
                string query = $@"DELETE FROM {TABLE_NAME}
                                  WHERE SatisID = @id";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@id", id)
                };

                ErrorManager.Instance.LogInfo($"Satış siliniyor. ID: {id}", "SaleRepository.Delete");
                int affectedRows = DatabaseHelper.ExecuteNonQuery(query, parameters);

                if (affectedRows > 0)
                {
                    string saleInfo = sale != null ?
                        $", Müşteri: {sale.CustomerFullName}, Araç: {sale.VehiclePlate}, Tutar: {sale.SaleAmount:C2}" : "";

                    ErrorManager.Instance.LogInfo($"Satış başarıyla silindi. ID: {id}{saleInfo}", "SaleRepository.Delete");
                }
                else
                {
                    ErrorManager.Instance.LogWarning($"Satış silinemedi, kayıt bulunamadı. ID: {id}", "SaleRepository.Delete");
                }
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Satış silinirken bir hata oluştu. ID: {id}",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Satış silinirken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        public List<Sale> Search(string searchText)
        {
            try
            {
                // Müşteri adı, plaka vb. üzerinden arama örneği
                string query = $@"SELECT s.SatisID, s.MusteriID, CONCAT(m.Ad, ' ', m.Soyad) AS MusteriAdSoyad,
                                         s.AracID, a.Plaka, a.Marka, a.Model,
                                         s.SatisTarihi, s.SatisTutari, s.OdemeTipi, s.TaksitSayisi, s.NoterTarihi,
                                         s.SozlesmeID, s.KullaniciID, CONCAT(u.Ad, ' ', u.Soyad) AS KullaniciAdSoyad,
                                         s.OlusturmaTarihi, s.GuncellenmeTarihi
                                  FROM {TABLE_NAME} s
                                  JOIN Musteriler m ON s.MusteriID = m.MusteriID
                                  JOIN Araclar a ON s.AracID = a.AracID
                                  JOIN Kullanicilar u ON s.KullaniciID = u.KullaniciID
                                  WHERE m.Ad LIKE @search
                                     OR m.Soyad LIKE @search
                                     OR a.Plaka LIKE @search
                                     OR a.Marka LIKE @search
                                     OR a.Model LIKE @search
                                  ORDER BY s.SatisTarihi DESC";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@search", "%" + searchText + "%")
                };

                ErrorManager.Instance.LogInfo($"Satış arama yapılıyor. Arama metni: '{searchText}'", "SaleRepository.Search");
                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
                List<Sale> sales = MapDataTableToSales(dt);

                ErrorManager.Instance.LogInfo($"Satış araması tamamlandı. '{searchText}' için {sales.Count} sonuç bulundu", "SaleRepository.Search");
                return sales;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Satış arama sırasında bir hata oluştu. Arama metni: '{searchText}'",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Satış arama sırasında bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// Belirli bir tarih aralığındaki satışları getirir
        /// </summary>
        public List<Sale> GetSalesByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                string query = $@"SELECT s.SatisID, s.MusteriID, CONCAT(m.Ad, ' ', m.Soyad) AS MusteriAdSoyad,
                                         s.AracID, a.Plaka, a.Marka, a.Model,
                                         s.SatisTarihi, s.SatisTutari, s.OdemeTipi, s.TaksitSayisi, s.NoterTarihi,
                                         s.SozlesmeID, s.KullaniciID, CONCAT(u.Ad, ' ', u.Soyad) AS KullaniciAdSoyad,
                                         s.OlusturmaTarihi, s.GuncellenmeTarihi
                                  FROM {TABLE_NAME} s
                                  JOIN Musteriler m ON s.MusteriID = m.MusteriID
                                  JOIN Araclar a ON s.AracID = a.AracID
                                  JOIN Kullanicilar u ON s.KullaniciID = u.KullaniciID
                                  WHERE s.SatisTarihi BETWEEN @startDate AND @endDate
                                  ORDER BY s.SatisTarihi DESC";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@startDate", startDate),
                    DatabaseHelper.CreateParameter("@endDate", endDate)
                };

                ErrorManager.Instance.LogInfo($"Tarih aralığına göre satış getiriliyor. Başlangıç: {startDate:dd.MM.yyyy}, Bitiş: {endDate:dd.MM.yyyy}", "SaleRepository.GetSalesByDateRange");
                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
                List<Sale> sales = MapDataTableToSales(dt);

                ErrorManager.Instance.LogInfo($"Tarih aralığı araması tamamlandı. {startDate:dd.MM.yyyy} - {endDate:dd.MM.yyyy} aralığında {sales.Count} satış bulundu", "SaleRepository.GetSalesByDateRange");
                return sales;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Tarih aralığına göre satış getirme sırasında bir hata oluştu. Başlangıç: {startDate:dd.MM.yyyy}, Bitiş: {endDate:dd.MM.yyyy}",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Tarih aralığına göre satış getirme sırasında bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// Belirli bir ödeme tipine göre satışları getirir
        /// </summary>
        public List<Sale> GetSalesByPaymentType(string paymentType)
        {
            try
            {
                string query = $@"SELECT s.SatisID, s.MusteriID, CONCAT(m.Ad, ' ', m.Soyad) AS MusteriAdSoyad,
                                         s.AracID, a.Plaka, a.Marka, a.Model,
                                         s.SatisTarihi, s.SatisTutari, s.OdemeTipi, s.TaksitSayisi, s.NoterTarihi,
                                         s.SozlesmeID, s.KullaniciID, CONCAT(u.Ad, ' ', u.Soyad) AS KullaniciAdSoyad,
                                         s.OlusturmaTarihi, s.GuncellenmeTarihi
                                  FROM {TABLE_NAME} s
                                  JOIN Musteriler m ON s.MusteriID = m.MusteriID
                                  JOIN Araclar a ON s.AracID = a.AracID
                                  JOIN Kullanicilar u ON s.KullaniciID = u.KullaniciID
                                  WHERE s.OdemeTipi = @paymentType
                                  ORDER BY s.SatisTarihi DESC";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@paymentType", paymentType)
                };

                ErrorManager.Instance.LogInfo($"Ödeme tipine göre satış getiriliyor. Ödeme Tipi: {paymentType}", "SaleRepository.GetSalesByPaymentType");
                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
                List<Sale> sales = MapDataTableToSales(dt);

                ErrorManager.Instance.LogInfo($"Ödeme tipi araması tamamlandı. '{paymentType}' tipinde {sales.Count} satış bulundu", "SaleRepository.GetSalesByPaymentType");
                return sales;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Ödeme tipine göre satış getirme sırasında bir hata oluştu. Ödeme Tipi: {paymentType}",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Ödeme tipine göre satış getirme sırasında bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// Taksitli satışları getirir
        /// </summary>
        public List<Sale> GetInstallmentSales()
        {
            try
            {
                string query = $@"SELECT s.SatisID, s.MusteriID, CONCAT(m.Ad, ' ', m.Soyad) AS MusteriAdSoyad,
                                         s.AracID, a.Plaka, a.Marka, a.Model,
                                         s.SatisTarihi, s.SatisTutari, s.OdemeTipi, s.TaksitSayisi, s.NoterTarihi,
                                         s.SozlesmeID, s.KullaniciID, CONCAT(u.Ad, ' ', u.Soyad) AS KullaniciAdSoyad,
                                         s.OlusturmaTarihi, s.GuncellenmeTarihi
                                  FROM {TABLE_NAME} s
                                  JOIN Musteriler m ON s.MusteriID = m.MusteriID
                                  JOIN Araclar a ON s.AracID = a.AracID
                                  JOIN Kullanicilar u ON s.KullaniciID = u.KullaniciID
                                  WHERE s.TaksitSayisi > 0
                                  ORDER BY s.SatisTarihi DESC";

                ErrorManager.Instance.LogInfo("Taksitli satışlar getiriliyor", "SaleRepository.GetInstallmentSales");
                DataTable dt = DatabaseHelper.ExecuteQuery(query);
                List<Sale> sales = MapDataTableToSales(dt);

                ErrorManager.Instance.LogInfo($"Taksitli satışlar getirildi. {sales.Count} taksitli satış bulundu", "SaleRepository.GetInstallmentSales");
                return sales;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Taksitli satışlar listelenirken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Taksitli satışlar listelenirken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// Noter işlemi eksik olan satışları getirir
        /// </summary>
        public List<Sale> GetSalesWithoutNotary()
        {
            try
            {
                string query = $@"SELECT s.SatisID, s.MusteriID, CONCAT(m.Ad, ' ', m.Soyad) AS MusteriAdSoyad,
                                         s.AracID, a.Plaka, a.Marka, a.Model,
                                         s.SatisTarihi, s.SatisTutari, s.OdemeTipi, s.TaksitSayisi, s.NoterTarihi,
                                         s.SozlesmeID, s.KullaniciID, CONCAT(u.Ad, ' ', u.Soyad) AS KullaniciAdSoyad,
                                         s.OlusturmaTarihi, s.GuncellenmeTarihi
                                  FROM {TABLE_NAME} s
                                  JOIN Musteriler m ON s.MusteriID = m.MusteriID
                                  JOIN Araclar a ON s.AracID = a.AracID
                                  JOIN Kullanicilar u ON s.KullaniciID = u.KullaniciID
                                  WHERE s.NoterTarihi IS NULL
                                  ORDER BY s.SatisTarihi DESC";

                ErrorManager.Instance.LogInfo("Noter işlemi eksik olan satışlar getiriliyor", "SaleRepository.GetSalesWithoutNotary");
                DataTable dt = DatabaseHelper.ExecuteQuery(query);
                List<Sale> sales = MapDataTableToSales(dt);

                ErrorManager.Instance.LogInfo($"Noter işlemi eksik olan satışlar getirildi. {sales.Count} satış bulundu", "SaleRepository.GetSalesWithoutNotary");
                return sales;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Noter işlemi eksik olan satışlar listelenirken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Noter işlemi eksik olan satışlar listelenirken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        /// <summary>
        /// Noter işlemini günceller
        /// </summary>
        public void UpdateNotary(int saleId, DateTime notaryDate)
        {
            try
            {
                string query = $@"UPDATE {TABLE_NAME} SET
                                  NoterTarihi = @noterTarihi,
                                  GuncellenmeTarihi = NOW()
                                  WHERE SatisID = @id";

                MySqlParameter[] parameters = {
                    DatabaseHelper.CreateParameter("@id", saleId),
                    DatabaseHelper.CreateParameter("@noterTarihi", notaryDate)
                };

                ErrorManager.Instance.LogInfo($"Noter işlemi güncelleniyor. Satış ID: {saleId}, Noter Tarihi: {notaryDate:dd.MM.yyyy}", "SaleRepository.UpdateNotary");
                int affectedRows = DatabaseHelper.ExecuteNonQuery(query, parameters);

                if (affectedRows > 0)
                {
                    ErrorManager.Instance.LogInfo($"Noter işlemi başarıyla güncellendi. Satış ID: {saleId}, Noter Tarihi: {notaryDate:dd.MM.yyyy}", "SaleRepository.UpdateNotary");
                }
                else
                {
                    ErrorManager.Instance.LogWarning($"Noter işlemi güncellenemedi, satış kaydı bulunamadı. ID: {saleId}", "SaleRepository.UpdateNotary");
                    throw new Exception($"Satış kaydı bulunamadı. ID: {saleId}");
                }
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Noter işlemi güncellenirken bir hata oluştu. Satış ID: {saleId}",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Noter işlemi güncellenirken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        private List<Sale> MapDataTableToSales(DataTable dt)
        {
            try
            {
                var sales = new List<Sale>();
                foreach (DataRow row in dt.Rows)
                {
                    sales.Add(MapDataRowToSale(row));
                }
                return sales;
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Satış verileri dönüştürülürken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Business);

                throw new Exception($"Satış verileri dönüştürülürken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }

        private Sale MapDataRowToSale(DataRow row)
        {
            try
            {
                return new Sale
                {
                    SaleID = row.GetValue<int>("SatisID"),
                    CustomerID = row.GetValue<int>("MusteriID"),
                    CustomerFullName = row.GetValue<string>("MusteriAdSoyad"),
                    VehicleID = row.GetValue<int>("AracID"),
                    VehiclePlate = row.GetValue<string>("Plaka"),
                    VehicleBrand = row.GetValue<string>("Marka"),
                    VehicleModel = row.GetValue<string>("Model"),
                    SaleDate = row.GetValue<DateTime>("SatisTarihi"),
                    SaleAmount = row.GetValue<decimal>("SatisTutari"),
                    PaymentType = row.GetValue<string>("OdemeTipi"),
                    InstallmentCount = row.GetValue<int>("TaksitSayisi"),
                    NotaryDate = row.GetValue<DateTime?>("NoterTarihi"),
                    ContractID = row.GetValue<int?>("SozlesmeID"),
                    UserID = row.GetValue<int>("KullaniciID"),
                    UserFullName = row.GetValue<string>("KullaniciAdSoyad"),
                    CreatedDate = row.GetValue<DateTime>("OlusturmaTarihi"),
                    UpdatedDate = row.GetValue<DateTime?>("GuncellenmeTarihi")
                };
            }
            catch (Exception ex)
            {
                // Hangi satış kaydıyla ilgili sorun olduğunu belirlemek için
                int saleId = 0;
                try { saleId = row.GetValue<int>("SatisID"); } catch { }

                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Satış verisi dönüştürme sırasında hata oluştu (SatisID: {saleId})",
                    ErrorSeverity.Error,
                    ErrorSource.Business);

                throw new Exception($"Satış verisi dönüştürülürken bir hata oluştu. (Hata ID: {errorId})", ex);
            }
        }
    }
}