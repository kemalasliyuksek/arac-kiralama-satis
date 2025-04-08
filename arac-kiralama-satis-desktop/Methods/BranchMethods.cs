using System;
using System.Collections.Generic;
using System.Data;
using arac_kiralama_satis_desktop.Models;
using arac_kiralama_satis_desktop.Repositories;
using arac_kiralama_satis_desktop.Utils;

namespace arac_kiralama_satis_desktop.Methods
{
    public class BranchMethods
    {
        private static readonly BranchRepository _repository = new BranchRepository();

        public static List<Branch> GetBranches()
        {
            try
            {
                // İşlem başlangıcını logla
                ErrorManager.Instance.LogInfo("Şubeler listeleniyor", "BranchMethods.GetBranches");
                return _repository.GetAll();
            }
            catch (Exception ex)
            {
                // ErrorManager ile hata yönetimi
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Şubeler listelenirken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                // Hata ID'si ile yeni exception fırlat
                throw new Exception($"Şubeler listelenirken bir hata oluştu. (Hata ID: {errorId})");
            }
        }

        public static Branch GetBranchById(int branchId)
        {
            try
            {
                ErrorManager.Instance.LogInfo($"Şube bilgisi alınıyor. Şube ID: {branchId}", "BranchMethods.GetBranchById");
                return _repository.GetById(branchId);
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Şube bilgisi alınırken bir hata oluştu. Şube ID: {branchId}",
                    ErrorSeverity.Error,
                    ErrorSource.Database);

                throw new Exception($"Şube bilgisi alınırken bir hata oluştu. (Hata ID: {errorId})");
            }
        }

        public static int AddBranch(Branch branch)
        {
            try
            {
                ErrorManager.Instance.LogInfo($"Yeni şube ekleniyor. Şube adı: {branch.BranchName}", "BranchMethods.AddBranch");
                return _repository.Add(branch);
            }
            catch (Exception ex)
            {
                // Şube ekleme kritik bir işlem olduğu için kullanıcıya göster (showToUser=true)
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Şube eklenirken bir hata oluştu. Şube adı: {branch.BranchName}",
                    ErrorSeverity.Error,
                    ErrorSource.Database,
                    true); // Kullanıcıya hata gösterilsin

                throw new Exception($"Şube eklenirken bir hata oluştu. (Hata ID: {errorId})");
            }
        }

        public static void UpdateBranch(Branch branch)
        {
            try
            {
                ErrorManager.Instance.LogInfo($"Şube güncelleniyor. Şube ID: {branch.BranchID}, Şube adı: {branch.BranchName}",
                    "BranchMethods.UpdateBranch");
                _repository.Update(branch);
            }
            catch (Exception ex)
            {
                // Güncelleme işlemi için kullanıcıya göster
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Şube güncellenirken bir hata oluştu. Şube ID: {branch.BranchID}",
                    ErrorSeverity.Error,
                    ErrorSource.Database,
                    true); // Kullanıcıya hata gösterilsin

                throw new Exception($"Şube güncellenirken bir hata oluştu. (Hata ID: {errorId})");
            }
        }

        public static void DeleteBranch(int branchId)
        {
            try
            {
                ErrorManager.Instance.LogInfo($"Şube siliniyor. Şube ID: {branchId}", "BranchMethods.DeleteBranch");
                _repository.Delete(branchId);
            }
            catch (Exception ex)
            {
                // Silme işlemi için kullanıcıya göster
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    $"Şube silinirken bir hata oluştu. Şube ID: {branchId}",
                    ErrorSeverity.Error,
                    ErrorSource.Database,
                    true); // Kullanıcıya hata gösterilsin

                throw new Exception($"Şube silinirken bir hata oluştu. (Hata ID: {errorId})");
            }
        }

        // DataTable dönüşümü için yardımcı metot
        public static DataTable GetBranchesAsDataTable()
        {
            try
            {
                ErrorManager.Instance.LogInfo("Şubeler DataTable olarak alınıyor", "BranchMethods.GetBranchesAsDataTable");

                List<Branch> branches = _repository.GetAll();
                DataTable dt = new DataTable();

                // DataTable sütunlarını oluştur
                dt.Columns.Add("SubeID", typeof(int));
                dt.Columns.Add("SubeAdi", typeof(string));
                dt.Columns.Add("Adres", typeof(string));
                dt.Columns.Add("SehirPlaka", typeof(string));
                dt.Columns.Add("Telefon", typeof(string));
                dt.Columns.Add("Email", typeof(string));
                dt.Columns.Add("AktifMi", typeof(bool));
                dt.Columns.Add("OlusturmaTarihi", typeof(DateTime));

                // Şubeleri DataTable'a ekle
                foreach (var branch in branches)
                {
                    dt.Rows.Add(
                        branch.BranchID,
                        branch.BranchName,
                        branch.Address,
                        branch.CityCode,
                        branch.FullPhoneNumber,
                        branch.Email,
                        branch.IsActive,
                        branch.CreatedDate
                    );
                }

                return dt;
            }
            catch (Exception ex)
            {
                // DataTable dönüşümü bir iş mantığı işlemi olduğu için ErrorSource.Business olarak işaretlendi
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Şubeler DataTable'a dönüştürülürken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.Business);

                throw new Exception($"Şubeler DataTable'a dönüştürülürken bir hata oluştu. (Hata ID: {errorId})");
            }
        }

        // Eski metodu koruyalım çağrılar bozulmasın
        public static DataTable GetBranchList()
        {
            ErrorManager.Instance.LogInfo("GetBranchList metodu çağrıldı, GetBranchesAsDataTable'a yönlendiriliyor",
                "BranchMethods.GetBranchList");
            return GetBranchesAsDataTable();
        }
    }
}