using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using arac_kiralama_satis_desktop.Interfaces;
using arac_kiralama_satis_desktop.Methods;
using arac_kiralama_satis_desktop.Utils;
using FontAwesome.Sharp;

namespace arac_kiralama_satis_desktop.Controls
{
    public partial class VehiclesControl : UserControl
    {
        private DataTable vehiclesTable;

        public event EventHandler VehicleAdded;

        public VehiclesControl()
        {
            InitializeComponent();

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

                dgvVehicles.DataSource = vehiclesTable;

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
                    dgvVehicles.DataSource = vehiclesTable;
                    lblVehiclesTitle.Text = $"Araç Listesi ({vehiclesTable.Rows.Count})";
                    return;
                }

                string filter = "";
                string searchLower = searchText.ToLower();

                filter = $"Plaka LIKE '%{searchText}%' OR " +
                         $"Marka LIKE '%{searchText}%' OR " +
                         $"Model LIKE '%{searchText}%' OR " +
                         $"Durum LIKE '%{searchText}%' OR " +
                         $"Şube LIKE '%{searchText}%' OR " +
                         $"CONVERT(Yıl, System.String) LIKE '%{searchText}%'";

                DataView dv = vehiclesTable.DefaultView;
                dv.RowFilter = filter;

                lblVehiclesTitle.Text = $"Araç Listesi ({dv.Count})";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Araç aramada hata: {ex.Message}");
                if (vehiclesTable != null)
                {
                    dgvVehicles.DataSource = vehiclesTable;
                    lblVehiclesTitle.Text = $"Araç Listesi ({vehiclesTable.Rows.Count})";
                }
            }
        }

        private void BtnAddVehicle_Click(object sender, EventArgs e)
        {
            VehicleAddForm vehicleForm = new VehicleAddForm();
            vehicleForm.VehicleAdded += (s, args) => {
                LoadData();

                VehicleAdded?.Invoke(this, EventArgs.Empty);
            };

            vehicleForm.ShowDialog();
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