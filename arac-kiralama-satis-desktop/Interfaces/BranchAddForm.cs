﻿using System;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using arac_kiralama_satis_desktop.Models;
using arac_kiralama_satis_desktop.Methods;
using arac_kiralama_satis_desktop.Utils;

namespace arac_kiralama_satis_desktop.Interfaces
{
    public partial class BranchAddForm : Form
    {
        private bool isEdit = false;
        private Branch currentBranch;

        public BranchAddForm()
        {
            InitializeComponent();
            CustomizeComponents();
            currentBranch = new Branch();
        }

        public BranchAddForm(int branchId)
        {
            InitializeComponent();
            CustomizeComponents();
            LoadBranchData(branchId);
            isEdit = true;
        }

        private void CustomizeComponents()
        {
            lblTitle.Text = isEdit ? "Şube Düzenle" : "Yeni Şube Ekle";
            btnSave.Text = isEdit ? "Güncelle" : "Kaydet";

            btnSave.IconChar = isEdit ? IconChar.Edit : IconChar.Save;
            btnCancel.IconChar = IconChar.Times;

            UIUtils.ApplyButtonStyle(btnSave, Color.FromArgb(40, 167, 69), Color.FromArgb(46, 204, 113));
            UIUtils.ApplyButtonStyle(btnCancel, Color.FromArgb(220, 53, 69), Color.FromArgb(231, 76, 60));

            UIUtils.ApplyShadowEffect(pnlContent);
            pnlContent.BackColor = Color.White;
            this.BackColor = Color.FromArgb(245, 245, 250);

            txtBranchName.Focus();
            txtPhoneNumber.KeyPress += TxtPhoneNumber_KeyPress;
            txtCityCode.KeyPress += TxtCityCode_KeyPress;

            txtCountryCode.Text = "+90";
        }

        private void LoadBranchData(int branchId)
        {
            try
            {
                Branch branchData = BranchMethods.GetBranchById(branchId);
                if (branchData != null)
                {
                    currentBranch = branchData;
                    txtBranchName.Text = currentBranch.BranchName;
                    txtAddress.Text = currentBranch.Address;
                    txtCountryCode.Text = currentBranch.CountryCode;
                    txtPhoneNumber.Text = currentBranch.PhoneNumber;
                    txtEmail.Text = currentBranch.Email;
                    txtCityCode.Text = currentBranch.CityCode;
                    chkIsActive.Checked = currentBranch.IsActive;
                }
                else
                {
                    MessageBox.Show("Şube bilgisi bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Şube bilgileri yüklenirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void TxtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void TxtCityCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;

            if (txtCityCode.Text.Length >= 2 && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private bool ValidateForm()
        {
            bool isValid = true;
            errorProvider.Clear();

            if (string.IsNullOrWhiteSpace(txtBranchName.Text))
            {
                errorProvider.SetError(txtBranchName, "Şube adı boş olamaz");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                errorProvider.SetError(txtAddress, "Adres boş olamaz");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
            {
                errorProvider.SetError(txtPhoneNumber, "Telefon numarası boş olamaz");
                isValid = false;
            }
            else if (txtPhoneNumber.Text.Length != 10)
            {
                errorProvider.SetError(txtPhoneNumber, "Telefon numarası 10 haneli olmalıdır");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtCityCode.Text))
            {
                errorProvider.SetError(txtCityCode, "Şehir plaka kodu boş olamaz");
                isValid = false;
            }
            else if (txtCityCode.Text.Length < 1 || txtCityCode.Text.Length > 2)
            {
                errorProvider.SetError(txtCityCode, "Şehir plaka kodu 1-2 haneli olmalıdır");
                isValid = false;
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                string emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                bool isValidEmail = System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text, emailPattern);
                if (!isValidEmail)
                {
                    errorProvider.SetError(txtEmail, "Geçerli bir e-posta adresi giriniz");
                    isValid = false;
                }
            }

            return isValid;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            try
            {
                currentBranch.BranchName = txtBranchName.Text.Trim();
                currentBranch.Address = txtAddress.Text.Trim();
                currentBranch.CountryCode = txtCountryCode.Text.Trim();
                currentBranch.PhoneNumber = txtPhoneNumber.Text.Trim();
                currentBranch.Email = txtEmail.Text.Trim();
                currentBranch.CityCode = txtCityCode.Text.Trim();
                currentBranch.IsActive = chkIsActive.Checked;

                if (isEdit)
                {
                    BranchMethods.UpdateBranch(currentBranch);
                    MessageBox.Show("Şube bilgileri başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    int newBranchId = BranchMethods.AddBranch(currentBranch);
                    MessageBox.Show("Yeni şube başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Şube kaydetme işlemi sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}