using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using arac_kiralama_satis_desktop.Methods;
using arac_kiralama_satis_desktop.Utils;
using arac_kiralama_satis_desktop.Interfaces;
using FontAwesome.Sharp;

namespace arac_kiralama_satis_desktop.Controls
{
    public partial class RentalsControl : UserControl
    {
        private DataTable rentalsTable;

        public event EventHandler RentalAdded;

        public RentalsControl()
        {
            InitializeComponent();

            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            UIUtils.SetupDataGridView(dgvRentals);
        }

        public void LoadData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                rentalsTable = RentalMethods.GetRentalsAsDataTable();

                dgvRentals.DataSource = rentalsTable;

                if (dgvRentals.Columns.Count > 0)
                {
                    dgvRentals.Columns["KiralamaID"].Visible = false;
                    dgvRentals.Columns["MusteriAdSoyad"].Width = 150;
                    dgvRentals.Columns["Plaka"].Width = 80;
                    dgvRentals.Columns["Marka"].Width = 100;
                    dgvRentals.Columns["Model"].Width = 100;
                    dgvRentals.Columns["BaslangicTarihi"].Width = 120;
                    dgvRentals.Columns["BitisTarihi"].Width = 120;
                    dgvRentals.Columns["TeslimTarihi"].Width = 120;
                    dgvRentals.Columns["KiralamaTutari"].Width = 120;
                    dgvRentals.Columns["OdemeTipi"].Width = 100;

                    dgvRentals.Columns["BaslangicTarihi"].DefaultCellStyle.Format = "dd.MM.yyyy";
                    dgvRentals.Columns["BitisTarihi"].DefaultCellStyle.Format = "dd.MM.yyyy";
                    dgvRentals.Columns["TeslimTarihi"].DefaultCellStyle.Format = "dd.MM.yyyy";
                    dgvRentals.Columns["KiralamaTutari"].DefaultCellStyle.Format = "N2";
                    dgvRentals.Columns["KiralamaTutari"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    dgvRentals.CellFormatting += (s, e) => {
                        if (e.RowIndex >= 0)
                        {
                            DateTime baslangicTarihi = Convert.ToDateTime(dgvRentals.Rows[e.RowIndex].Cells["BaslangicTarihi"].Value);
                            DateTime bitisTarihi = Convert.ToDateTime(dgvRentals.Rows[e.RowIndex].Cells["BitisTarihi"].Value);
                            object teslimTarihiValue = dgvRentals.Rows[e.RowIndex].Cells["TeslimTarihi"].Value;

                            bool teslimEdildi = teslimTarihiValue != DBNull.Value && teslimTarihiValue != null;
                            bool aktifKiralama = DateTime.Now >= baslangicTarihi && DateTime.Now <= bitisTarihi && !teslimEdildi;
                            bool gecikmisKiralama = DateTime.Now > bitisTarihi && !teslimEdildi;

                            if (gecikmisKiralama)
                            {
                                dgvRentals.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 235);
                            }
                            else if (aktifKiralama)
                            {
                                dgvRentals.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(235, 255, 235);
                            }
                        }
                    };
                }

                lblRentalsTitle.Text = $"Kiralama Listesi ({rentalsTable.Rows.Count})";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kiralama verileri yüklenirken bir hata oluştu: {ex.Message}",
                    "Veri Yükleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void SearchRentals(string searchText)
        {
            try
            {
                if (rentalsTable == null) return;

                if (string.IsNullOrWhiteSpace(searchText))
                {
                    dgvRentals.DataSource = rentalsTable;
                    lblRentalsTitle.Text = $"Kiralama Listesi ({rentalsTable.Rows.Count})";
                    return;
                }

                string filter = "";

                filter = $"Müşteri LIKE '%{searchText}%' OR " +
                         $"Plaka LIKE '%{searchText}%' OR " +
                         $"Marka LIKE '%{searchText}%' OR " +
                         $"Model LIKE '%{searchText}%' OR " +
                         $"[Ödeme Tipi] LIKE '%{searchText}%'";

                DataView dv = rentalsTable.DefaultView;
                dv.RowFilter = filter;

                lblRentalsTitle.Text = $"Kiralama Listesi ({dv.Count})";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kiralama aramada hata: {ex.Message}");
                if (rentalsTable != null)
                {
                    dgvRentals.DataSource = rentalsTable;
                    lblRentalsTitle.Text = $"Kiralama Listesi ({rentalsTable.Rows.Count})";
                }
            }
        }

        private void BtnAddRental_Click(object sender, EventArgs e)
        {
            RentalAddForm rentalForm = new RentalAddForm();
            rentalForm.RentalAdded += (s, args) => {
                LoadData();

                RentalAdded?.Invoke(this, EventArgs.Empty);
            };

            rentalForm.ShowDialog();
        }

        private void BtnRefreshRentals_Click(object sender, EventArgs e)
        {
            LoadData();
            txtSearchRentals.Clear();
        }

        private void TxtSearchRentals_TextChanged(object sender, EventArgs e)
        {
            SearchRentals(txtSearchRentals.Text);
        }
    }
}