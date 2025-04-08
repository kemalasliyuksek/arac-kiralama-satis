using System;
using System.Windows.Forms;
using System.Drawing;
using FontAwesome.Sharp;

namespace arac_kiralama_satis_desktop.Interfaces
{
    partial class BranchAddForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pnlTitle = new Panel();
            lblTitle = new Label();
            pnlContent = new Panel();
            btnCancel = new IconButton();
            btnSave = new IconButton();
            chkIsActive = new CheckBox();
            lblIsActive = new Label();
            txtEmail = new TextBox();
            lblEmail = new Label();
            txtCityCode = new TextBox();
            lblCityCode = new Label();
            pnlPhone = new Panel();
            txtPhoneNumber = new TextBox();
            txtCountryCode = new TextBox();
            lblPhone = new Label();
            txtAddress = new TextBox();
            lblAddress = new Label();
            txtBranchName = new TextBox();
            lblBranchName = new Label();
            errorProvider = new ErrorProvider(components);
            pnlTitle.SuspendLayout();
            pnlContent.SuspendLayout();
            pnlPhone.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // pnlTitle
            // 
            pnlTitle.BackColor = Color.FromArgb(49, 76, 143);
            pnlTitle.Controls.Add(lblTitle);
            pnlTitle.Dock = DockStyle.Top;
            pnlTitle.Location = new Point(0, 0);
            pnlTitle.Name = "pnlTitle";
            pnlTitle.Size = new Size(500, 60);
            pnlTitle.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 18);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(140, 25);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Yeni Şube Ekle";
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.White;
            pnlContent.Controls.Add(btnCancel);
            pnlContent.Controls.Add(btnSave);
            pnlContent.Controls.Add(chkIsActive);
            pnlContent.Controls.Add(lblIsActive);
            pnlContent.Controls.Add(txtEmail);
            pnlContent.Controls.Add(lblEmail);
            pnlContent.Controls.Add(txtCityCode);
            pnlContent.Controls.Add(lblCityCode);
            pnlContent.Controls.Add(pnlPhone);
            pnlContent.Controls.Add(lblPhone);
            pnlContent.Controls.Add(txtAddress);
            pnlContent.Controls.Add(lblAddress);
            pnlContent.Controls.Add(txtBranchName);
            pnlContent.Controls.Add(lblBranchName);
            pnlContent.Location = new Point(20, 80);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(460, 440);
            pnlContent.TabIndex = 1;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(220, 53, 69);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.IconChar = IconChar.Close;
            btnCancel.IconColor = Color.White;
            btnCancel.IconFont = IconFont.Auto;
            btnCancel.IconSize = 20;
            btnCancel.Location = new Point(240, 380);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 40);
            btnCancel.TabIndex = 13;
            btnCancel.Text = "İptal";
            btnCancel.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += BtnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(40, 167, 69);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.IconChar = IconChar.Save;
            btnSave.IconColor = Color.White;
            btnSave.IconFont = IconFont.Auto;
            btnSave.IconSize = 20;
            btnSave.Location = new Point(120, 380);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 40);
            btnSave.TabIndex = 12;
            btnSave.Text = "Kaydet";
            btnSave.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += BtnSave_Click;
            // 
            // chkIsActive
            // 
            chkIsActive.AutoSize = true;
            chkIsActive.Checked = true;
            chkIsActive.CheckState = CheckState.Checked;
            chkIsActive.Location = new Point(120, 330);
            chkIsActive.Name = "chkIsActive";
            chkIsActive.Size = new Size(48, 19);
            chkIsActive.TabIndex = 11;
            chkIsActive.Text = "Evet";
            chkIsActive.UseVisualStyleBackColor = true;
            // 
            // lblIsActive
            // 
            lblIsActive.AutoSize = true;
            lblIsActive.Font = new Font("Segoe UI", 10F);
            lblIsActive.Location = new Point(20, 330);
            lblIsActive.Name = "lblIsActive";
            lblIsActive.Size = new Size(63, 19);
            lblIsActive.TabIndex = 10;
            lblIsActive.Text = "Aktif Mi?";
            // 
            // txtEmail
            // 
            txtEmail.Font = new Font("Segoe UI", 10F);
            txtEmail.Location = new Point(120, 280);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(320, 25);
            txtEmail.TabIndex = 9;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 10F);
            lblEmail.Location = new Point(20, 280);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(47, 19);
            lblEmail.TabIndex = 8;
            lblEmail.Text = "E-mail";
            // 
            // txtCityCode
            // 
            txtCityCode.Font = new Font("Segoe UI", 10F);
            txtCityCode.Location = new Point(120, 230);
            txtCityCode.MaxLength = 2;
            txtCityCode.Name = "txtCityCode";
            txtCityCode.Size = new Size(80, 25);
            txtCityCode.TabIndex = 7;
            // 
            // lblCityCode
            // 
            lblCityCode.AutoSize = true;
            lblCityCode.Font = new Font("Segoe UI", 10F);
            lblCityCode.Location = new Point(20, 230);
            lblCityCode.Name = "lblCityCode";
            lblCityCode.Size = new Size(75, 19);
            lblCityCode.TabIndex = 6;
            lblCityCode.Text = "Şehir Plaka";
            // 
            // pnlPhone
            // 
            pnlPhone.Controls.Add(txtPhoneNumber);
            pnlPhone.Controls.Add(txtCountryCode);
            pnlPhone.Location = new Point(120, 180);
            pnlPhone.Name = "pnlPhone";
            pnlPhone.Size = new Size(320, 30);
            pnlPhone.TabIndex = 5;
            // 
            // txtPhoneNumber
            // 
            txtPhoneNumber.Font = new Font("Segoe UI", 10F);
            txtPhoneNumber.Location = new Point(50, 2);
            txtPhoneNumber.MaxLength = 10;
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.Size = new Size(270, 25);
            txtPhoneNumber.TabIndex = 1;
            // 
            // txtCountryCode
            // 
            txtCountryCode.Font = new Font("Segoe UI", 10F);
            txtCountryCode.Location = new Point(0, 2);
            txtCountryCode.Name = "txtCountryCode";
            txtCountryCode.Size = new Size(45, 25);
            txtCountryCode.TabIndex = 0;
            txtCountryCode.Text = "+90";
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Font = new Font("Segoe UI", 10F);
            lblPhone.Location = new Point(20, 180);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(52, 19);
            lblPhone.TabIndex = 4;
            lblPhone.Text = "Telefon";
            // 
            // txtAddress
            // 
            txtAddress.Font = new Font("Segoe UI", 10F);
            txtAddress.Location = new Point(120, 80);
            txtAddress.Multiline = true;
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(320, 80);
            txtAddress.TabIndex = 3;
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Font = new Font("Segoe UI", 10F);
            lblAddress.Location = new Point(20, 80);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(44, 19);
            lblAddress.TabIndex = 2;
            lblAddress.Text = "Adres";
            // 
            // txtBranchName
            // 
            txtBranchName.Font = new Font("Segoe UI", 10F);
            txtBranchName.Location = new Point(120, 30);
            txtBranchName.Name = "txtBranchName";
            txtBranchName.Size = new Size(320, 25);
            txtBranchName.TabIndex = 1;
            // 
            // lblBranchName
            // 
            lblBranchName.AutoSize = true;
            lblBranchName.Font = new Font("Segoe UI", 10F);
            lblBranchName.Location = new Point(20, 30);
            lblBranchName.Name = "lblBranchName";
            lblBranchName.Size = new Size(63, 19);
            lblBranchName.TabIndex = 0;
            lblBranchName.Text = "Şube Adı";
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // BranchAddForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 250);
            ClientSize = new Size(500, 540);
            Controls.Add(pnlContent);
            Controls.Add(pnlTitle);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "BranchAddForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Şube Ekle";
            pnlTitle.ResumeLayout(false);
            pnlTitle.PerformLayout();
            pnlContent.ResumeLayout(false);
            pnlContent.PerformLayout();
            pnlPhone.ResumeLayout(false);
            pnlPhone.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.TextBox txtBranchName;
        private System.Windows.Forms.Label lblBranchName;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Panel pnlPhone;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private System.Windows.Forms.TextBox txtCountryCode;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtCityCode;
        private System.Windows.Forms.Label lblCityCode;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.Label lblIsActive;
        private FontAwesome.Sharp.IconButton btnSave;
        private FontAwesome.Sharp.IconButton btnCancel;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}