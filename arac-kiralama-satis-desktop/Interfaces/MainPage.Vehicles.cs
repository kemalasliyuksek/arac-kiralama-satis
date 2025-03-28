using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using arac_kiralama_satis_desktop.Methods;
using arac_kiralama_satis_desktop.Models;
using FontAwesome.Sharp;

namespace arac_kiralama_satis_desktop.Interfaces
{
    public partial class MainPage
    {
        private void LoadVehiclesData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                List<Vehicle> vehicles = VehicleMethods.GetVehicles();
                vehiclesTable = new DataTable();

                // Add columns
                vehiclesTable.Columns.Add("AracID", typeof(int));
                vehiclesTable.Columns.Add("Plaka", typeof(string));
                vehiclesTable.Columns.Add("Marka", typeof(string));
                vehiclesTable.Columns.Add("Model", typeof(string));
                vehiclesTable.Columns.Add("Yıl", typeof(int));
                vehiclesTable.Columns.Add("Renk", typeof(string));
                vehiclesTable.Columns.Add("Kilometre", typeof(int));
                vehiclesTable.Columns.Add("Yakıt", typeof(string));
                vehiclesTable.Columns.Add("Vites", typeof(string));
                vehiclesTable.Columns.Add("Durum", typeof(string));
                vehiclesTable.Columns.Add("Şube", typeof(string));
                vehiclesTable.Columns.Add("Sınıf", typeof(string));

                // Add rows from the vehicles list
                foreach (var vehicle in vehicles)
                {
                    vehiclesTable.Rows.Add(
                        vehicle.VehicleID,
                        vehicle.Plate,
                        vehicle.Brand,
                        vehicle.Model,
                        vehicle.Year,
                        vehicle.Color,
                        vehicle.Kilometers,
                        vehicle.FuelType,
                        vehicle.TransmissionType,
                        vehicle.StatusName,
                        vehicle.BranchName,
                        vehicle.VehicleClassName
                    );
                }

                // Set DataSource
                dgvVehicles.DataSource = vehiclesTable;

                // Format columns
                if (dgvVehicles.Columns.Count > 0)
                {
                    dgvVehicles.Columns["AracID"].Visible = false;
                    dgvVehicles.Columns["Plaka"].Width = 80;
                    dgvVehicles.Columns["Marka"].Width = 100;
                    dgvVehicles.Columns["Model"].Width = 120;
                    dgvVehicles.Columns["Yıl"].Width = 60;
                    dgvVehicles.Columns["Renk"].Width = 80;
                    dgvVehicles.Columns["Kilometre"].Width = 100;
                    dgvVehicles.Columns["Yakıt"].Width = 80;
                    dgvVehicles.Columns["Vites"].Width = 80;
                    dgvVehicles.Columns["Durum"].Width = 100;
                    dgvVehicles.Columns["Şube"].Width = 120;
                    dgvVehicles.Columns["Sınıf"].Width = 100;
                }

                // Update count information
                lblVehiclesTitle.Text = $"Araç Listesi ({vehicles.Count})";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Araç verileri yüklenirken bir hata oluştu: {ex.Message}",
                    "Veri Yükleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void SearchVehicles(string searchText)
        {
            try
            {
                if (vehiclesTable == null) return;

                if (string.IsNullOrWhiteSpace(searchText))
                {
                    // Show all data
                    dgvVehicles.DataSource = vehiclesTable;
                    lblVehiclesTitle.Text = $"Araç Listesi ({vehiclesTable.Rows.Count})";
                    return;
                }

                // Create a case-insensitive filter
                string filter = "";
                string searchLower = searchText.ToLower();

                // Search in most relevant columns
                filter = $"Plaka LIKE '%{searchText}%' OR " +
                         $"Marka LIKE '%{searchText}%' OR " +
                         $"Model LIKE '%{searchText}%' OR " +
                         $"Durum LIKE '%{searchText}%' OR " +
                         $"Şube LIKE '%{searchText}%' OR " +
                         $"CONVERT(Yıl, System.String) LIKE '%{searchText}%'";

                DataView dv = vehiclesTable.DefaultView;
                dv.RowFilter = filter;

                // Update label with count
                lblVehiclesTitle.Text = $"Araç Listesi ({dv.Count})";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Araç aramada hata: {ex.Message}");
                // Failed filter - just reset
                if (vehiclesTable != null)
                {
                    dgvVehicles.DataSource = vehiclesTable;
                    lblVehiclesTitle.Text = $"Araç Listesi ({vehiclesTable.Rows.Count})";
                }
            }
        }

        private void BtnVehicles_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as IconButton);
            ShowPanel(pnlVehicles);
            LoadVehiclesData();
            lblPageTitle.Text = "Araçlar";
        }

        private void TxtSearchVehicles_TextChanged(object sender, EventArgs e)
        {
            SearchVehicles(txtSearchVehicles.Text);
        }

        private void BtnRefreshVehicles_Click(object sender, EventArgs e)
        {
            LoadVehiclesData();
            txtSearchVehicles.Clear();
        }
    }
}