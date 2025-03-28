using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using arac_kiralama_satis_desktop.Methods;
using arac_kiralama_satis_desktop.Models;
using arac_kiralama_satis_desktop.Utils;
using FontAwesome.Sharp;
using Org.BouncyCastle.Asn1.Cmp;

namespace arac_kiralama_satis_desktop.Interfaces
{
    public partial class VehicleAddForm : Form
    {
        private bool _isDragging = false;
        private Point _dragCursorPoint;
        private Point _dragFormPoint;
        private readonly Dictionary<int, string> _vehicleStatuses = new Dictionary<int, string>();
        private readonly Dictionary<int, string> _vehicleClasses = new Dictionary<int, string>();
        private readonly Dictionary<int, string> _branches = new Dictionary<int, string>();

        // Form başarıyla tamamlandığında ana formun listeyi güncellemesi için olay
        public event EventHandler VehicleAdded;

        public VehicleAddForm()
        {
            InitializeComponent();
            CustomizeComponents();
            LoadDropdownData();
        }

        private void CustomizeComponents()
        {
            // Form ayarları
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Icon = System.Drawing.SystemIcons.Application; // Uygulama ikonu

            // Panel border ve gölge efekti
            pnlMain.BackColor = Color.White;
            UIUtils.ApplyShadowEffect(pnlMain);

            // Form header
            pnlHeader.BackColor = Color.FromArgb(49, 76, 143);
            lblTitle.ForeColor = Color.White;

            // Header içindeki butonlar
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.IconColor = Color.White;

            // Input alanlarını ayarla
            SetupInputFields();

            // Butonlar
            UIUtils.ApplyButtonStyle(btnSave, Color.FromArgb(40, 167, 69), Color.FromArgb(33, 136, 56));
            UIUtils.ApplyButtonStyle(btnCancel, Color.FromArgb(108, 117, 125), Color.FromArgb(90, 98, 104));

            // Form sürükleme işlemleri için event atamaları
            pnlHeader.MouseDown += PnlHeader_MouseDown;
            pnlHeader.MouseMove += PnlHeader_MouseMove;
            pnlHeader.MouseUp += PnlHeader_MouseUp;
            lblTitle.MouseDown += PnlHeader_MouseDown;
            lblTitle.MouseMove += PnlHeader_MouseMove;
            lblTitle.MouseUp += PnlHeader_MouseUp;
        }

        private void SetupInputFields()
        {
            // Tüm TextBox'lar için stil uygula
            foreach (Control control in pnlContent.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                    textBox.BackColor = Color.White;
                    textBox.ForeColor = Color.FromArgb(64, 64, 64);
                    textBox.Font = new Font("Segoe UI", 10F);
                    textBox.Height = 30;
                }
                else if (control is ComboBox comboBox)
                {
                    comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                    comboBox.BackColor = Color.White;
                    comboBox.ForeColor = Color.FromArgb(64, 64, 64);
                    comboBox.Font = new Font("Segoe UI", 10F);
                    comboBox.Height = 30;
                    comboBox.FlatStyle = FlatStyle.Flat;
                }
                else if (control is DateTimePicker dateTimePicker)
                {
                    dateTimePicker.Format = DateTimePickerFormat.Short;
                    dateTimePicker.Font = new Font("Segoe UI", 10F);
                }
                else if (control is Label label && !label.Name.Contains("lbl"))
                {
                    label.ForeColor = Color.FromArgb(64, 64, 64);
                    label.Font = new Font("Segoe UI", 9F);
                }
            }

            // NumericUpDown kontrollerini özelleştir
            nudYear.Minimum = 1990;
            nudYear.Maximum = DateTime.Now.Year;
            nudYear.Value = DateTime.Now.Year;

            nudKilometers.Maximum = 1000000;
            nudKilometers.Minimum = 0;
            nudKilometers.Value = 0;

            nudPurchasePrice.Maximum = 10000000;
            nudPurchasePrice.Minimum = 0;
            nudPurchasePrice.Value = 0;
            nudPurchasePrice.DecimalPlaces = 2;
            nudPurchasePrice.ThousandsSeparator = true;

            nudSalePrice.Maximum = 10000000;
            nudSalePrice.Minimum = 0;
            nudSalePrice.Value = 0;
            nudSalePrice.DecimalPlaces = 2;
            nudSalePrice.ThousandsSeparator = true;

            // Tarih seçici ayarları
            dtpPurchaseDate.Value = DateTime.Now;
            dtpPurchaseDate.MaxDate = DateTime.Now;
        }

        private void LoadDropdownData()
        {
            try
            {
                // Araç Durumları
                DataTable vehicleStatusesTable = VehicleMethods.GetVehicleStatuses();
                cmbStatus.Items.Clear();
                _vehicleStatuses.Clear();

                foreach (DataRow row in vehicleStatusesTable.Rows)
                {
                    int statusId = Convert.ToInt32(row["DurumID"]);
                    string statusName = row["DurumAdi"].ToString();
                    _vehicleStatuses.Add(statusId, statusName);
                    cmbStatus.Items.Add(statusName);
                }

                if (cmbStatus.Items.Count > 0)
                    cmbStatus.SelectedIndex = 0;

                // Araç Sınıfları
                DataTable vehicleClassesTable = VehicleMethods.GetVehicleClasses();
                cmbVehicleClass.Items.Clear();
                _vehicleClasses.Clear();

                foreach (DataRow row in vehicleClassesTable.Rows)
                {
                    int classId = Convert.ToInt32(row["AracSinifID"]);
                    string className = row["SinifAdi"].ToString();
                    _vehicleClasses.Add(classId, className);
                    cmbVehicleClass.Items.Add(className);
                }

                if (cmbVehicleClass.Items.Count > 0)
                    cmbVehicleClass.SelectedIndex = 0;

                // Şubeler
                DataTable branchesTable = BranchMethods.GetBranchList();
                cmbBranch.Items.Clear();
                _branches.Clear();

                cmbBranch.Items.Add("Şube Seçiniz");
                foreach (DataRow row in branchesTable.Rows)
                {
                    int branchId = Convert.ToInt32(row["SubeID"]);
                    string branchName = row["SubeAdi"].ToString();
                    _branches.Add(branchId, branchName);
                    cmbBranch.Items.Add(branchName);
                }

                if (cmbBranch.Items.Count > 0)
                    cmbBranch.SelectedIndex = 0;

                // Yakıt ve Vites Tipleri
                cmbFuelType.Items.AddRange(new string[] { "Benzin", "Dizel", "Hibrit", "Elektrik", "LPG" });
                cmbTransmissionType.Items.AddRange(new string[] { "Manuel", "Otomatik", "Yarı Otomatik" });

                if (cmbFuelType.Items.Count > 0)
                    cmbFuelType.SelectedIndex = 0;

                if (cmbTransmissionType.Items.Count > 0)
                    cmbTransmissionType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Dropdown verileri yüklenirken bir hata oluştu: {ex.Message}",
                    "Veri Yükleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Form Dragging - Form sürükleme işlemleri

        private void PnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            _isDragging = true;
            _dragCursorPoint = Cursor.Position;
            _dragFormPoint = this.Location;
        }

        private void PnlHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                Point difference = Point.Subtract(Cursor.Position, new Size(_dragCursorPoint));
                this.Location = Point.Add(_dragFormPoint, new Size(difference));
            }
        }

        private void PnlHeader_MouseUp(object sender, MouseEventArgs e)
        {
            _isDragging = false;
        }

        #endregion

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            try
            {
                Vehicle vehicle = new Vehicle
                {
                    Plate = txtPlate.Text.Trim(),
                    Brand = txtBrand.Text.Trim(),
                    Model = txtModel.Text.Trim(),
                    Year = (int)nudYear.Value,
                    EngineNo = txtEngineNo.Text.Trim(),
                    ChassisNo = txtChassisNo.Text.Trim(),
                    Color = txtColor.Text.Trim(),
                    Kilometers = (int)nudKilometers.Value,
                    FuelType = cmbFuelType.SelectedItem.ToString(),
                    TransmissionType = cmbTransmissionType.SelectedItem.ToString(),
                    StatusID = GetKeyByValue(_vehicleStatuses, cmbStatus.SelectedItem.ToString()),
                    PurchaseDate = dtpPurchaseDate.Value,
                    PurchasePrice = nudPurchasePrice.Value,
                    SalePrice = chkHasSalePrice.Checked ? nudSalePrice.Value : (decimal?)null
                };

                // Şube ve Sınıf ataması
                if (cmbBranch.SelectedIndex > 0) // İlk eleman "Şube Seçiniz" olduğu için
                {
                    vehicle.BranchID = GetKeyByValue(_branches, cmbBranch.SelectedItem.ToString());
                }

                if (cmbVehicleClass.SelectedIndex >= 0)
                {
                    vehicle.VehicleClassID = GetKeyByValue(_vehicleClasses, cmbVehicleClass.SelectedItem.ToString());
                }

                // Araç ekleme işlemi
                int newVehicleId = VehicleMethods.AddVehicle(vehicle);

                if (newVehicleId > 0)
                {
                    MessageBox.Show("Araç başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Araç eklendiğini bildir
                    VehicleAdded?.Invoke(this, EventArgs.Empty);

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Araç eklenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Araç eklenirken bir hata oluştu: {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateForm()
        {
            // Zorunlu alanların kontrolü
            if (string.IsNullOrWhiteSpace(txtPlate.Text))
            {
                ShowError("Plaka alanı boş bırakılamaz.");
                txtPlate.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtBrand.Text))
            {
                ShowError("Marka alanı boş bırakılamaz.");
                txtBrand.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtModel.Text))
            {
                ShowError("Model alanı boş bırakılamaz.");
                txtModel.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtChassisNo.Text))
            {
                ShowError("Şasi numarası boş bırakılamaz.");
                txtChassisNo.Focus();
                return false;
            }

            if (cmbFuelType.SelectedIndex < 0)
            {
                ShowError("Yakıt tipi seçilmelidir.");
                cmbFuelType.Focus();
                return false;
            }

            if (cmbTransmissionType.SelectedIndex < 0)
            {
                ShowError("Vites tipi seçilmelidir.");
                cmbTransmissionType.Focus();
                return false;
            }

            if (cmbStatus.SelectedIndex < 0)
            {
                ShowError("Araç durumu seçilmelidir.");
                cmbStatus.Focus();
                return false;
            }

            // Plaka formatı kontrolü
            if (!IsValidPlate(txtPlate.Text))
            {
                ShowError("Geçerli bir plaka giriniz. (Örn: 34ABC123)");
                txtPlate.Focus();
                return false;
            }

            // Fiyat kontrolü
            if (nudPurchasePrice.Value <= 0)
            {
                ShowError("Alış fiyatı sıfırdan büyük olmalıdır.");
                nudPurchasePrice.Focus();
                return false;
            }

            if (chkHasSalePrice.Checked && nudSalePrice.Value <= 0)
            {
                ShowError("Satış fiyatı sıfırdan büyük olmalıdır.");
                nudSalePrice.Focus();
                return false;
            }

            lblError.Visible = false;
            return true;
        }

        private void ShowError(string message)
        {
            lblError.Text = message;
            lblError.Visible = true;
        }

        private bool IsValidPlate(string plate)
        {
            // Basit bir plaka kontrolü, gerçek uygulamada daha kapsamlı olabilir
            return !string.IsNullOrWhiteSpace(plate) && plate.Length >= 5 && plate.Length <= 11;
        }

        private static int GetKeyByValue(Dictionary<int, string> dictionary, string value)
        {
            return dictionary.FirstOrDefault(x => x.Value == value).Key;
        }

        private void ChkHasSalePrice_CheckedChanged(object sender, EventArgs e)
        {
            nudSalePrice.Enabled = chkHasSalePrice.Checked;
        }
    }
}