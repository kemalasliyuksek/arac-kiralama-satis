using System;
using System.Windows.Forms;
using arac_kiralama_satis_desktop.Utils;
using FontAwesome.Sharp;

namespace arac_kiralama_satis_desktop.Interfaces
{
    public partial class MainPage
    {
        private void BtnRentals_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowPanel(null);
            lblPageTitle.Text = "Kiralamalar";
        }

        private void BtnSales_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowPanel(null);
            lblPageTitle.Text = "Satışlar";
        }

        private void BtnMaintenance_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowPanel(null);
            lblPageTitle.Text = "Bakım & Servis";
        }

        private void BtnReports_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowPanel(null);
            lblPageTitle.Text = "Raporlar";
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowPanel(null);
            lblPageTitle.Text = "Ayarlar";
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            // Oturumu kapat
            DialogResult result = MessageBox.Show("Oturumu kapatmak istediğinize emin misiniz?",
                "Oturumu Kapat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Oturum bilgilerini temizle
                CurrentSession.ClearSession();

                // Login sayfasına yönlendir
                LoginPage loginPage = new LoginPage();
                loginPage.Show();
                this.Close();
            }
        }
    }
}