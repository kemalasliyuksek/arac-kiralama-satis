using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using arac_kiralama_satis_desktop.Methods;
using arac_kiralama_satis_desktop.Utils;
using FontAwesome.Sharp;

namespace arac_kiralama_satis_desktop.Controls
{
    public partial class VehiclesControl : UserControl
    {
        // DataTable for search/filter
        private DataTable vehiclesTable;

        // Events for main form to handle
        public event EventHandler VehicleAdded;

        public VehiclesControl()
        {
            InitializeComponent();

            // Setup data grid view properties
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            UIUtils.SetupDataGridView(dgvVehicles);
        }

        public void LoadData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                vehiclesTable = VehicleMethods.GetVehiclesAsDataTable();

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
                    dgvVehicles.Columns["YakitTipi"].Width = 80;
                    dgvVehicles.Columns["VitesTipi"].Width = 80;
                    dgvVehicles.Columns["Durum"].Width = 100;
                    dgvVehicles.Columns["Şube"].Width = 120;
                    dgvVehicles.Columns["Sınıf"].Width = 100;
                }

                // Update count information
                lblVehiclesTitle.Text = $"Araç Listesi ({vehiclesTable.Rows.Count})";
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

        // Event handlers
        private void BtnAddVehicle_Click(object sender, EventArgs e)
        {
            // Aslında burada gerçek bir form açacaktık, ancak şu an için sadece event'i tetikleyelim
            VehicleAdded?.Invoke(this, EventArgs.Empty);

            // Şu an için bir örnek mesaj gösterelim
            MessageBox.Show("Yeni araç ekleme formu burada açılacak.",
                "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Form işlemi sonrası veriyi yeniliyoruz
            LoadData();
        }

        private void BtnRefreshVehicles_Click(object sender, EventArgs e)
        {
            LoadData();
            txtSearchVehicles.Clear();
        }

        private void TxtSearchVehicles_TextChanged(object sender, EventArgs e)
        {
            SearchVehicles(txtSearchVehicles.Text);
        }
    }
}