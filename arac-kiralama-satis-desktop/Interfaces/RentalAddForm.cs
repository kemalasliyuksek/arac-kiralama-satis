using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using arac_kiralama_satis_desktop.Methods;
using arac_kiralama_satis_desktop.Models;
using arac_kiralama_satis_desktop.Utils;
using FontAwesome.Sharp;

namespace arac_kiralama_satis_desktop.Interfaces
{
    public partial class RentalAddForm : Form
    {
        private bool _isDragging = false;
        private Point _dragCursorPoint;
        private Point _dragFormPoint;
        private bool _isEdit = false;
        private int _rentalId = 0;
        private readonly Dictionary<int, Vehicle> _availableVehicles = new Dictionary<int, Vehicle>();
        private readonly Dictionary<int, Customer> _customers = new Dictionary<int, Customer>();

        // Form başarıyla tamamlandığında ana formun listeyi güncellemesi için olay
        public event EventHandler RentalAdded;

        public RentalAddForm()
        {
            InitializeComponent();
        }

        // Düzenleme modu için constructor
        public RentalAddForm(int rentalId)
        {
            InitializeComponent();
            _isEdit = true;
            _rentalId = rentalId;
            lblTitle.Text = "Kiralama Düzenle";
            btnSave.Text = "Güncelle";
        }

        private void RentalAddForm_Load(object sender, EventArgs e)
        {
            CustomizeComponents();
            LoadDropdownData();

            // Varsayılan değerler
            dtpStartDate.Value = DateTime.Now;
            dtpEndDate.Value = DateTime.Now.AddDays(1);
            nudStartKm.Value = 0;
            nudRentalAmount.Value = 0;
            nudDepositAmount.Value = 0;

            // Düzenleme modu ise verileri yükle
            if (_isEdit && _rentalId > 0)
            {
                LoadRentalData();
            }
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

        private void LoadDropdownData()
        {
            try
            {
                // Müşterileri yükle
                cmbCustomer.Items.Clear();
                _customers.Clear();

                List<Customer> customers = CustomerMethods.GetCustomers();
                foreach (var customer in customers)
                {
                    string displayText = $"{customer.FullName} - {customer.IdentityNumber}";
                    cmbCustomer.Items.Add(displayText);
                    _customers.Add(customer.CustomerID, customer);
                }

                // Araçları yükle
                cmbVehicle.Items.Clear();
                _availableVehicles.Clear();

                List<Vehicle> vehicles = VehicleMethods.GetAvailableVehiclesForRental();
                foreach (var vehicle in vehicles)
                {
                    string displayText = $"{vehicle.Plate} - {vehicle.Brand} {vehicle.Model}";
                    cmbVehicle.Items.Add(displayText);
                    _availableVehicles.Add(vehicle.VehicleID, vehicle);
                }

                // Ödeme tipleri
                cmbPaymentType.Items.Clear();
                cmbPaymentType.Items.AddRange(new string[] { "Nakit", "Kredi Kartı", "Havale/EFT", "Diğer" });
                cmbPaymentType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Dropdown verileri yüklenirken bir hata oluştu: {ex.Message}",
                   "Veri Yükleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRentalData()
        {
            try
            {
                Rental rental = RentalMethods.GetRentalById(_rentalId);
                if (rental != null)
                {
                    // Müşteri seçimi
                    int customerIndex = -1;
                    for (int i = 0; i < cmbCustomer.Items.Count; i++)
                    {
                        string itemText = cmbCustomer.Items[i].ToString();
                        if (itemText.Contains(rental.CustomerFullName))
                        {
                            customerIndex = i;
                            break;
                        }
                    }
                    if (customerIndex >= 0)
                        cmbCustomer.SelectedIndex = customerIndex;

                    // Araç seçimi
                    int vehicleIndex = -1;
                    for (int i = 0; i < cmbVehicle.Items.Count; i++)
                    {
                        string itemText = cmbVehicle.Items[i].ToString();
                        if (itemText.Contains(rental.VehiclePlate))
                        {
                            vehicleIndex = i;
                            break;
                        }
                    }
                    if (vehicleIndex >= 0)
                        cmbVehicle.SelectedIndex = vehicleIndex;

                    // Diğer alanlar
                    dtpStartDate.Value = rental.StartDate;
                    dtpEndDate.Value = rental.EndDate;
                    if (rental.ReturnDate.HasValue)
                        dtpReturnDate.Value = rental.ReturnDate.Value;
                    else
                        chkReturnDate.Checked = false;

                    nudStartKm.Value = rental.StartKm;
                    if (rental.EndKm.HasValue)
                        nudEndKm.Value = rental.EndKm.Value;
                    else
                        chkEndKm.Checked = false;

                    nudRentalAmount.Value = rental.RentalAmount;

                    if (rental.DepositAmount.HasValue)
                    {
                        chkDeposit.Checked = true;
                        nudDepositAmount.Value = rental.DepositAmount.Value;
                    }
                    else
                    {
                        chkDeposit.Checked = false;
                    }

                    // Ödeme tipi
                    int paymentTypeIndex = cmbPaymentType.FindStringExact(rental.PaymentType);
                    if (paymentTypeIndex >= 0)
                        cmbPaymentType.SelectedIndex = paymentTypeIndex;
                    else
                        cmbPaymentType.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kiralama bilgileri yüklenirken bir hata oluştu: {ex.Message}",
                    "Veri Yükleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateRentalAmount()
        {
            try
            {
                if (cmbVehicle.SelectedIndex >= 0 && dtpStartDate.Value <= dtpEndDate.Value)
                {
                    // Seçilen aracın ID'sini al
                    int vehicleId = -1;
                    string selectedVehicleText = cmbVehicle.SelectedItem.ToString();

                    foreach (var vehicle in _availableVehicles)
                    {
                        if (selectedVehicleText.Contains(vehicle.Value.Plate))
                        {
                            vehicleId = vehicle.Key;
                            break;
                        }
                    }

                    if (vehicleId != -1 && _availableVehicles.ContainsKey(vehicleId))
                    {
                        Vehicle selectedVehicle = _availableVehicles[vehicleId];

                        // Araç sınıfına göre günlük ücret
                        decimal dailyRate = 0;

                        // Basit bir hesaplama - gerçek uygulamada veritabanından alınabilir
                        switch (selectedVehicle.VehicleClassName)
                        {
                            case "Ekonomik":
                                dailyRate = 250;
                                break;
                            case "Orta Sınıf":
                                dailyRate = 400;
                                break;
                            case "Üst Sınıf":
                                dailyRate = 700;
                                break;
                            case "Premium":
                                dailyRate = 1200;
                                break;
                            case "SUV":
                                dailyRate = 600;
                                break;
                            case "Ticari":
                                dailyRate = 500;
                                break;
                            default:
                                dailyRate = 300; // Varsayılan fiyat
                                break;
                        }

                        // Kiralama süresi (gün)
                        int days = (dtpEndDate.Value - dtpStartDate.Value).Days + 1;

                        // Toplam tutarı hesapla
                        decimal totalAmount = dailyRate * days;

                        // NumericUpDown'a ata
                        nudRentalAmount.Value = totalAmount;
                    }
                }
            }
            catch (Exception ex)
            {
                // Hesaplama hatası olursa görmezden gel
                Console.WriteLine($"Kiralama tutarı hesaplanırken hata: {ex.Message}");
            }
        }

        private bool ValidateForm()
        {
            bool isValid = true;
            errorProvider.Clear();

            // Müşteri seçimi kontrolü
            if (cmbCustomer.SelectedIndex < 0)
            {
                errorProvider.SetError(cmbCustomer, "Lütfen bir müşteri seçin");
                isValid = false;
            }

            // Araç seçimi kontrolü
            if (cmbVehicle.SelectedIndex < 0)
            {
                errorProvider.SetError(cmbVehicle, "Lütfen bir araç seçin");
                isValid = false;
            }

            // Başlangıç tarihi kontrolü
            if (dtpStartDate.Value > dtpEndDate.Value)
            {
                errorProvider.SetError(dtpStartDate, "Başlangıç tarihi bitiş tarihinden sonra olamaz");
                isValid = false;
            }

            // Kiralama tutarı kontrolü
            if (nudRentalAmount.Value <= 0)
            {
                errorProvider.SetError(nudRentalAmount, "Kiralama tutarı sıfırdan büyük olmalıdır");
                isValid = false;
            }

            return isValid;
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
                // Müşteri ID'sini al
                int customerId = -1;
                string selectedCustomerText = cmbCustomer.SelectedItem.ToString();

                foreach (var customer in _customers)
                {
                    if (selectedCustomerText.Contains(customer.Value.FullName))
                    {
                        customerId = customer.Key;
                        break;
                    }
                }

                // Araç ID'sini al
                int vehicleId = -1;
                string selectedVehicleText = cmbVehicle.SelectedItem.ToString();

                foreach (var vehicle in _availableVehicles)
                {
                    if (selectedVehicleText.Contains(vehicle.Value.Plate))
                    {
                        vehicleId = vehicle.Key;
                        break;
                    }
                }

                if (customerId == -1 || vehicleId == -1)
                {
                    MessageBox.Show("Lütfen geçerli bir müşteri ve araç seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiralama nesnesi oluştur
                Rental rental = new Rental
                {
                    CustomerID = customerId,
                    VehicleID = vehicleId,
                    StartDate = dtpStartDate.Value,
                    EndDate = dtpEndDate.Value,
                    ReturnDate = chkReturnDate.Checked ? dtpReturnDate.Value : (DateTime?)null,
                    StartKm = (int)nudStartKm.Value,
                    EndKm = chkEndKm.Checked ? (int?)nudEndKm.Value : null,
                    RentalAmount = nudRentalAmount.Value,
                    DepositAmount = chkDeposit.Checked ? nudDepositAmount.Value : (decimal?)null,
                    PaymentType = cmbPaymentType.SelectedItem.ToString(),
                    UserID = CurrentSession.UserID
                };

                if (_isEdit)
                {
                    rental.RentalID = _rentalId;
                    // Kiralama güncelleme
                    RentalMethods.UpdateRental(rental);
                    MessageBox.Show("Kiralama bilgileri başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Yeni kiralama
                    int newRentalId = RentalMethods.AddRental(rental);

                    if (newRentalId > 0)
                    {
                        // Araç durumunu güncelle (Kirada)
                        VehicleMethods.UpdateVehicleStatus(vehicleId, 4); // 4: Kirada durumu

                        MessageBox.Show("Kiralama başarıyla oluşturuldu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                // RentalAdded event'ini çağır
                RentalAdded?.Invoke(this, EventArgs.Empty);

                // Formu kapat
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kiralama işlemi sırasında bir hata oluştu: {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChkReturnDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpReturnDate.Enabled = chkReturnDate.Checked;
        }

        private void ChkEndKm_CheckedChanged(object sender, EventArgs e)
        {
            nudEndKm.Enabled = chkEndKm.Checked;
        }

        private void ChkDeposit_CheckedChanged(object sender, EventArgs e)
        {
            nudDepositAmount.Enabled = chkDeposit.Checked;
        }

        private void CmbVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateRentalAmount();
        }

        private void DtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            CalculateRentalAmount();
        }

        private void DtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            CalculateRentalAmount();
        }
    }
}