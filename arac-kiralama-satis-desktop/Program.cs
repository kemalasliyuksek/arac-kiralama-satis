using arac_kiralama_satis_desktop.Interfaces;
using System;
using System.Windows.Forms;

namespace arac_kiralama_satis_desktop
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Uygulama LoginPage ile baþlayacak
            Application.Run(new LoginPage());
        }
    }
}