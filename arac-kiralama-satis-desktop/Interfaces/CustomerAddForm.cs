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

        public event EventHandler? CustomerAdded;

        public CustomerAddForm()
        {
            InitializeComponent();
            CustomizeComponents();
            LoadDropdownData();
        }

        private void CustomizeComponents()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Icon = System.Drawing.SystemIcons.Application; // Uygulama ikonu

            pnlMain.BackColor = Color.White;
            UIUtils.ApplyShadowEffect(pnlMain);

            pnlHeader.BackColor = Color.FromArgb(49, 76, 143);
            lblTitle.ForeColor = Color.White;

            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.IconColor = Color.White;

            SetupInputFields();

            UIUtils.ApplyButtonStyle(btnSave, Color.FromArgb(40, 167, 69), Color.FromArgb(33, 136, 56));
            UIUtils.ApplyButtonStyle(btnCancel, Color.FromArgb(108, 117, 125), Color.FromArgb(90, 98, 104));

            pnlHeader.MouseDown += PnlHeader_MouseDown;
            pnlHeader.MouseMove += PnlHeader_MouseMove;
            pnlHeader.MouseUp += PnlHeader_MouseUp;
            lblTitle.MouseDown += PnlHeader_MouseDown;
            lblTitle.MouseMove += PnlHeader_MouseMove;
            lblTitle.MouseUp += PnlHeader_MouseUp;
        }

        private void SetupInputFields()
        {
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

            DateTime today = DateTime.Now;
            dtpBirthDate.Value = new DateTime(today.Year - 25, today.Month, today.Day);
            dtpBirthDate.MaxDate = today.AddYears(-18);
            dtpBirthDate.MinDate = today.AddYears(-100);

            dtpLicenseDate.Value = today.AddYears(-5);
            dtpLicenseDate.MaxDate = today;
            dtpLicenseDate.MinDate = today.AddYears(-50);

            txtPhoneNumber.MaxLength = 10;

            txtIdentityNumber.MaxLength = 11;
        }

        private void LoadDropdownData()
        {
            try
            {
                cmbLicenseClass.Items.Clear();
                cmbLicenseClass.Items.AddRange(new string[] { "A", "A1", "A2", "B", "B1", "BE", "C", "C1", "CE", "D", "D1", "DE", "F", "G", "M" });

                cmbCountryCode.Items.Clear();
                cmbCountryCode.Items.AddRange(new string[] { "+90", "+1", "+44", "+49", "+33", "+7", "+39", "+34", "+31", "+46" });

                cmbCustomerType.Items.Clear();
                cmbCustomerType.Items.AddRange(new string[] { "Bireysel", "Kurumsal" });

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

                int newCustomerId = CustomerMethods.AddCustomer(customer);

                if (newCustomerId > 0)
                {
                    MessageBox.Show("Müşteri başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

            if (txtIdentityNumber.Text.Length != 11 || !IsNumeric(txtIdentityNumber.Text))
            {
                ShowError("TC Kimlik No 11 haneli sayısal bir değer olmalıdır.");
                txtIdentityNumber.Focus();
                return false;
            }

            if (txtPhoneNumber.Text.Length != 10 || !IsNumeric(txtPhoneNumber.Text))
            {
                ShowError("Telefon numarası 10 haneli sayısal bir değer olmalıdır (5XXXXXXXXX).");
                txtPhoneNumber.Focus();
                return false;
            }

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
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtIdentityNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}