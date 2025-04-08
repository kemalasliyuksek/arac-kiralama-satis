using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using arac_kiralama_satis_desktop.Methods;
using arac_kiralama_satis_desktop.Utils;
using FontAwesome.Sharp;

namespace arac_kiralama_satis_desktop.Interfaces
{
    public partial class PersonelAddForm : Form
    {
        private bool _isDragging = false;
        private Point _dragCursorPoint;
        private Point _dragFormPoint;
        private bool _isEdit = false;
        private int _userId = 0;

        public event EventHandler? StaffAdded;

        public PersonelAddForm()
        {
            InitializeComponent();
        }

        public PersonelAddForm(int userId)
        {
            InitializeComponent();
            _isEdit = true;
            _userId = userId;
            lblTitle.Text = "Personel Düzenle";
            btnSave.Text = "Güncelle";
        }

        private void PersonelAddForm_Load(object sender, EventArgs e)
        {
            CustomizeComponents();
            LoadDropdownData();

            if (_isEdit && _userId > 0)
            {
                LoadStaffData();
            }
        }

        private void CustomizeComponents()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Icon = System.Drawing.SystemIcons.Application;

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
            }

            txtPhoneNumber.MaxLength = 10;
        }

        private void LoadDropdownData()
        {
            try
            {
                cmbCountryCode.Items.Clear();
                cmbCountryCode.Items.AddRange(new string[] { "+90", "+1", "+44", "+49", "+33", "+7", "+39", "+34", "+31", "+46" });
                cmbCountryCode.SelectedItem = "+90";

                cmbRole.Items.Clear();
                DataTable roles = StaffMethods.GetRoles();
                Dictionary<int, string> roleDict = new Dictionary<int, string>();

                foreach (DataRow row in roles.Rows)
                {
                    int roleId = Convert.ToInt32(row["RolID"]);
                    string roleName = row["RolAdi"].ToString();
                    roleDict.Add(roleId, roleName);
                    cmbRole.Items.Add(roleName);
                }

                if (cmbRole.Items.Count > 0)
                    cmbRole.SelectedIndex = 0;

                cmbBranch.Items.Clear();
                DataTable branches = BranchMethods.GetBranchList();
                Dictionary<int, string> branchDict = new Dictionary<int, string>();

                cmbBranch.Items.Add("Şube Seçiniz");

                foreach (DataRow row in branches.Rows)
                {
                    int branchId = Convert.ToInt32(row["SubeID"]);
                    string branchName = row["SubeAdi"].ToString();
                    branchDict.Add(branchId, branchName);
                    cmbBranch.Items.Add(branchName);
                }

                if (cmbBranch.Items.Count > 0)
                    cmbBranch.SelectedIndex = 0;

                cmbRole.Tag = roleDict;
                cmbBranch.Tag = branchDict;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Açılır menüler yüklenirken bir hata oluştu: {ex.Message}",
                   "Veri Yükleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStaffData()
        {
            try
            {
                DataRow userData = StaffMethods.GetUserById(_userId);
                if (userData != null)
                {
                    txtFirstName.Text = userData["Ad"].ToString();
                    txtLastName.Text = userData["Soyad"].ToString();
                    txtUsername.Text = userData["KullaniciAdi"].ToString();
                    txtEmail.Text = userData["Email"].ToString();

                    string fullPhone = userData["Telefon"].ToString();
                    if (!string.IsNullOrEmpty(fullPhone))
                    {
                        string countryCode = fullPhone.Substring(0, 3); 
                        string phoneNumber = fullPhone.Substring(3);   

                        if (cmbCountryCode.Items.Contains(countryCode))
                            cmbCountryCode.SelectedItem = countryCode;

                        txtPhoneNumber.Text = phoneNumber;
                    }

                    if (userData["RolAdi"] != DBNull.Value)
                    {
                        string roleName = userData["RolAdi"].ToString();
                        int index = cmbRole.FindStringExact(roleName);
                        if (index != -1)
                            cmbRole.SelectedIndex = index;
                    }

                    if (userData["SubeID"] != DBNull.Value)
                    {
                        string branchName = userData["SubeAdi"].ToString();
                        int index = cmbBranch.FindStringExact(branchName);
                        if (index != -1)
                            cmbBranch.SelectedIndex = index;
                    }

                    chkIsActive.Checked = Convert.ToBoolean(userData["Durum"]);

                    txtPassword.Text = "";
                    txtPasswordConfirm.Text = "";
                    txtPassword.PlaceholderText = "Değiştirmek istemiyorsanız boş bırakın";
                    txtPasswordConfirm.PlaceholderText = "Değiştirmek istemiyorsanız boş bırakın";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Personel bilgileri yüklenirken bir hata oluştu: {ex.Message}",
                    "Veri Yükleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateForm()
        {
            bool isValid = true;
            errorProvider.Clear();

            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                errorProvider.SetError(txtFirstName, "Ad alanı boş bırakılamaz");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                errorProvider.SetError(txtLastName, "Soyad alanı boş bırakılamaz");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                errorProvider.SetError(txtUsername, "Kullanıcı adı boş bırakılamaz");
                isValid = false;
            }
            else if (txtUsername.Text.Length < 3)
            {
                errorProvider.SetError(txtUsername, "Kullanıcı adı en az 3 karakter olmalıdır");
                isValid = false;
            }

            if (!_isEdit)
            {
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    errorProvider.SetError(txtPassword, "Şifre boş bırakılamaz");
                    isValid = false;
                }
                else if (txtPassword.Text.Length < 6)
                {
                    errorProvider.SetError(txtPassword, "Şifre en az 6 karakter olmalıdır");
                    isValid = false;
                }

                if (txtPassword.Text != txtPasswordConfirm.Text)
                {
                    errorProvider.SetError(txtPasswordConfirm, "Şifreler eşleşmiyor");
                    isValid = false;
                }
            }
            else if (!string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                if (txtPassword.Text.Length < 6)
                {
                    errorProvider.SetError(txtPassword, "Şifre en az 6 karakter olmalıdır");
                    isValid = false;
                }

                if (txtPassword.Text != txtPasswordConfirm.Text)
                {
                    errorProvider.SetError(txtPasswordConfirm, "Şifreler eşleşmiyor");
                    isValid = false;
                }
            }

            if (string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
            {
                errorProvider.SetError(txtPhoneNumber, "Telefon numarası boş bırakılamaz");
                isValid = false;
            }
            else if (txtPhoneNumber.Text.Length != 10)
            {
                errorProvider.SetError(txtPhoneNumber, "Telefon numarası 10 haneli olmalıdır");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                errorProvider.SetError(txtEmail, "E-posta alanı boş bırakılamaz");
                isValid = false;
            }
            else if (!IsValidEmail(txtEmail.Text))
            {
                errorProvider.SetError(txtEmail, "Geçerli bir e-posta adresi giriniz");
                isValid = false;
            }

            if (cmbRole.SelectedIndex < 0)
            {
                errorProvider.SetError(cmbRole, "Rol seçimi yapılmalıdır");
                isValid = false;
            }

            return isValid;
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
                int roleId = -1;
                int? branchId = null;

                if (cmbRole.SelectedIndex >= 0 && cmbRole.Tag is Dictionary<int, string> roleDict)
                {
                    string selectedRoleName = cmbRole.SelectedItem.ToString();
                    foreach (var pair in roleDict)
                    {
                        if (pair.Value == selectedRoleName)
                        {
                            roleId = pair.Key;
                            break;
                        }
                    }
                }

                if (cmbBranch.SelectedIndex > 0 && cmbBranch.Tag is Dictionary<int, string> branchDict)
                {
                    string selectedBranchName = cmbBranch.SelectedItem.ToString();
                    foreach (var pair in branchDict)
                    {
                        if (pair.Value == selectedBranchName)
                        {
                            branchId = pair.Key;
                            break;
                        }
                    }
                }

                if (roleId == -1)
                {
                    MessageBox.Show("Lütfen bir rol seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Dictionary<string, object> userParams = new Dictionary<string, object>
                {
                    { "Ad", txtFirstName.Text.Trim() },
                    { "Soyad", txtLastName.Text.Trim() },
                    { "KullaniciAdi", txtUsername.Text.Trim() },
                    { "Email", txtEmail.Text.Trim() },
                    { "UlkeKodu", cmbCountryCode.SelectedItem.ToString() },
                    { "TelefonNo", txtPhoneNumber.Text.Trim() },
                    { "RolID", roleId },
                    { "SubeID", branchId },
                    { "Durum", chkIsActive.Checked }
                };

                if (!string.IsNullOrEmpty(txtPassword.Text))
                {
                    userParams.Add("Sifre", txtPassword.Text);
                }

                bool success;
                if (_isEdit)
                {
                    userParams.Add("KullaniciID", _userId);
                    success = StaffMethods.UpdateUser(userParams);
                    if (success)
                    {
                        MessageBox.Show("Personel bilgileri başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        StaffAdded?.Invoke(this, EventArgs.Empty);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    if (!userParams.ContainsKey("Sifre"))
                    {
                        MessageBox.Show("Yeni kayıt için şifre alanı zorunludur.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    success = StaffMethods.AddUser(userParams);
                    if (success)
                    {
                        MessageBox.Show("Yeni personel başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        StaffAdded?.Invoke(this, EventArgs.Empty);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Personel {(_isEdit ? "güncellenirken" : "eklenirken")} bir hata oluştu: {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}