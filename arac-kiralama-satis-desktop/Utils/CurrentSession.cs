namespace arac_kiralama_satis_desktop.Utils
{
    public static class CurrentSession
    {
        public static int UserID { get; set; }
        public static string FullName { get; set; }
        public static string UserName { get; set; }
        public static int RoleID { get; set; }
        public static string RoleName { get; set; }
        public static int? BranchID { get; set; }
        public static string BranchName { get; set; }

        public static void ClearSession()
        {
            UserID = 0;
            FullName = string.Empty;
            UserName = string.Empty;
            RoleID = 0;
            RoleName = string.Empty;
            BranchID = null;
            BranchName = string.Empty;
        }
    }
}