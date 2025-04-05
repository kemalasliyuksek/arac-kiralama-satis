using System;
using System.Collections.Generic;
using arac_kiralama_satis_desktop.Models;
using arac_kiralama_satis_desktop.Repositories;

namespace arac_kiralama_satis_desktop.Methods
{
    public class BranchMethods
    {
        private static readonly BranchRepository _repository = new BranchRepository();

        public static List<Branch> GetBranchList()
        {
            try
            {
                return _repository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Şube listesi alınırken bir hata oluştu: " + ex.Message);
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
    }
}
