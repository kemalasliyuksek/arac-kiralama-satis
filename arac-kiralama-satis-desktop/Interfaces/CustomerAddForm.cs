using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using arac_kiralama_satis_desktop.Methods;
using arac_kiralama_satis_desktop.Models;
using arac_kiralama_satis_desktop.Utils;
using FontAwesome.Sharp;

namespace arac_kiralama_satis_desktop.Interfaces
{
    public partial class CustomerAddForm : Form
    {
        private bool _isDragging = false;
        private Point _dragCursorPoint;
        private Point _dragFormPoint;

        // Form başarıyla tamamlandığında ana formun listeyi güncellemesi için olay
        public event EventHandler? CustomerAdded;

        public CustomerAddForm()
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

            // Tarih seçicileri için varsayılan değerler
            DateTime today = DateTime.Now;
            dtpBirthDate.Value = new DateTime(today.Year - 25, today.Month, today.Day); // 25 yaş varsayılan
            dtpBirthDate.MaxDate = today.AddYears(-18); // En az 18 yaşında olmalı
            dtpBirthDate.MinDate = today.AddYears(-100); // En fazla 100 yaşında olabilir

            dtpLicenseDate.Value = today.AddYears(-5); // 5 yıl önce ehliyet almış varsayılan
            dtpLicenseDate.MaxDate = today;
            dtpLicenseDate.MinDate = today.AddYears(-50); // En fazla 50 yıl önce ehliyet almış olabilir

            // Telefon numarası maskeleri
            txtPhoneNumber.MaxLength = 10; // 5XX XXX XXXX formatı için 10 karakter

            // TC Kimlik numarası için maksimum uzunluk
            txtIdentityNumber.MaxLength = 11;
        }

        private void LoadDropdownData()
        {
            try
            {
                // Ehliyet sınıfları
                cmbLicenseClass.Items.Clear();
                cmbLicenseClass.Items.AddRange(new string[] { "A", "A1", "A2", "B", "B1", "BE", "C", "C1", "CE", "D", "D1", "DE", "F", "G", "M" });

                // Ülke kodları
                cmbCountryCode.Items.Clear();
                cmbCountryCode.Items.AddRange(new string[] { "+90", "+1", "+44", "+49", "+33", "+7", "+39", "+34", "+31", "+46" });

                // Müşteri tipleri
                cmbCustomerType.Items.Clear();
                cmbCustomerType.Items.AddRange(new string[] { "Bireysel", "Kurumsal" });

                // Varsayılan değerler
                cmbCountryCode.SelectedItem = "+90";
                cmbCustomerType.SelectedItem = "Bireysel";
                cmbLicenseClass.SelectedItem = "B";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Dropdown verileri yüklenirken bir hata oluştu: {ex.Message}",
                    "Veri Yükleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Form Dragging - Form sürükleme işlemleri

        // MouseDown, MouseMove ve MouseUp metodlarını şu şekilde güncelleyin:
        private void PnlHeader_MouseDown(object? sender, MouseEventArgs e)
        {
            _isDragging = true;
            _dragCursorPoint = Cursor.Position;
            _dragFormPoint = this.Location;
        }

        private void PnlHeader_MouseMove(object? sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                Point difference = Point.Subtract(Cursor.Position, new Size(_dragCursorPoint));
                this.Location = Point.Add(_dragFormPoint, new Size(difference));
            }
        }

        private void PnlHeader_MouseUp(object? sender, MouseEventArgs e)
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
                Customer customer = new Customer
                {
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    IdentityNumber = txtIdentityNumber.Text.Trim(),
                    BirthDate = dtpBirthDate.Value,
                    LicenseNumber = txtLicenseNumber.Text.Trim(),
                    LicenseClass = cmbLicenseClass.SelectedItem.ToString(),
                    LicenseDate = dtpLicenseDate.Value,
                    CountryCode = cmbCountryCode.SelectedItem.ToString(),
                    PhoneNumber = txtPhoneNumber.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    CustomerType = cmbCustomerType.SelectedItem.ToString(),
                    IsAvailable = true
                };

                // Müşteri ekleme işlemi
                int newCustomerId = CustomerMethods.AddCustomer(customer);

                if (newCustomerId > 0)
                {
                    MessageBox.Show("Müşteri başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Müşteri eklendiğini bildir
                    CustomerAdded?.Invoke(this, EventArgs.Empty);

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Müşteri eklenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Müşteri eklenirken bir hata oluştu: {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateForm()
        {
            // Zorunlu alanların kontrolü
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                ShowError("Ad alanı boş bırakılamaz.");
                txtFirstName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                ShowError("Soyad alanı boş bırakılamaz.");
                txtLastName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtIdentityNumber.Text))
            {
                ShowError("TC Kimlik No alanı boş bırakılamaz.");
                txtIdentityNumber.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
            {
                ShowError("Telefon numarası boş bırakılamaz.");
                txtPhoneNumber.Focus();
                return false;
            }

            // TC Kimlik numarası kontrolü
            if (txtIdentityNumber.Text.Length != 11 || !IsNumeric(txtIdentityNumber.Text))
            {
                ShowError("TC Kimlik No 11 haneli sayısal bir değer olmalıdır.");
                txtIdentityNumber.Focus();
                return false;
            }

            // Telefon numarası kontrolü
            if (txtPhoneNumber.Text.Length != 10 || !IsNumeric(txtPhoneNumber.Text))
            {
                ShowError("Telefon numarası 10 haneli sayısal bir değer olmalıdır (5XXXXXXXXX).");
                txtPhoneNumber.Focus();
                return false;
            }

            // E-posta kontrolü (basit)
            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !IsValidEmail(txtEmail.Text))
            {
                ShowError("Geçerli bir e-posta adresi giriniz.");
                txtEmail.Focus();
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

        private bool IsNumeric(string text)
        {
            return long.TryParse(text, out _);
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void TxtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Sadece sayı girişi
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtIdentityNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Sadece sayı girişi
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}