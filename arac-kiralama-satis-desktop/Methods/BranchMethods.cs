using System;
using System.Collections.Generic;
using System.Data;
using arac_kiralama_satis_desktop.Models;
using arac_kiralama_satis_desktop.Repositories;

namespace arac_kiralama_satis_desktop.Methods
{
    public class BranchMethods
    {
        private static readonly BranchRepository _repository = new BranchRepository();

        public static List<Branch> GetBranches()
        {
            try
            {
                return _repository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Şubeler listelenirken bir hata oluştu: " + ex.Message);
            }
        }

        public static Branch GetBranchById(int branchId)
        {
            try
            {
                return _repository.GetById(branchId);
            }
            catch (Exception ex)
            {
                throw new Exception("Şube bilgisi alınırken bir hata oluştu: " + ex.Message);
            }
        }

        public static int AddBranch(Branch branch)
        {
            try
            {
                return _repository.Add(branch);
            }
            catch (Exception ex)
            {
                throw new Exception("Şube eklenirken bir hata oluştu: " + ex.Message);
            }
        }

        public static void UpdateBranch(Branch branch)
        {
            try
            {
                _repository.Update(branch);
            }
            catch (Exception ex)
            {
                throw new Exception("Şube güncellenirken bir hata oluştu: " + ex.Message);
            }
        }

        public static void DeleteBranch(int branchId)
        {
            try
            {
                _repository.Delete(branchId);
            }
            catch (Exception ex)
            {
                throw new Exception("Şube silinirken bir hata oluştu: " + ex.Message);
            }
        }

        // DataTable dönüşümü için yardımcı metot
        public static DataTable GetBranchesAsDataTable()
        {
            try
            {
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
                throw new Exception("Şubeler DataTable'a dönüştürülürken bir hata oluştu: " + ex.Message);
            }
        }

        // Eski metodu koruyalım çağrılar bozulmasın
        public static DataTable GetBranchList()
        {
            return GetBranchesAsDataTable();
        }
    }
}