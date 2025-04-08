using FontAwesome.Sharp;
using System.Drawing;
using System.Windows.Forms;

namespace arac_kiralama_satis_desktop.Interfaces
{
    partial class CustomerAddForm
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
            pnlMain = new Panel();
            pnlContent = new Panel();
            lblError = new Label();
            btnCancel = new Button();
            btnSave = new Button();
            cmbCustomerType = new ComboBox();
            label13 = new Label();
            txtAddress = new TextBox();
            label12 = new Label();
            txtEmail = new TextBox();
            label11 = new Label();
            txtPhoneNumber = new TextBox();
            label10 = new Label();
            cmbCountryCode = new ComboBox();
            label9 = new Label();
            dtpLicenseDate = new DateTimePicker();
            label8 = new Label();
            cmbLicenseClass = new ComboBox();
            label7 = new Label();
            txtLicenseNumber = new TextBox();
            label6 = new Label();
            dtpBirthDate = new DateTimePicker();
            label5 = new Label();
            txtIdentityNumber = new TextBox();
            label4 = new Label();
            txtLastName = new TextBox();
            label3 = new Label();
            txtFirstName = new TextBox();
            label2 = new Label();
            pnlHeader = new Panel();
            btnClose = new IconButton();
            lblTitle = new Label();
            pnlMain.SuspendLayout();
            pnlContent.SuspendLayout();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.BorderStyle = BorderStyle.FixedSingle;
            pnlMain.Controls.Add(pnlContent);
            pnlMain.Controls.Add(pnlHeader);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(800, 600);
            pnlMain.TabIndex = 0;
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.White;
            pnlContent.Controls.Add(lblError);
            pnlContent.Controls.Add(btnCancel);
            pnlContent.Controls.Add(btnSave);
            pnlContent.Controls.Add(cmbCustomerType);
            pnlContent.Controls.Add(label13);
            pnlContent.Controls.Add(txtAddress);
            pnlContent.Controls.Add(label12);
            pnlContent.Controls.Add(txtEmail);
            pnlContent.Controls.Add(label11);
            pnlContent.Controls.Add(txtPhoneNumber);
            pnlContent.Controls.Add(label10);
            pnlContent.Controls.Add(cmbCountryCode);
            pnlContent.Controls.Add(label9);
            pnlContent.Controls.Add(dtpLicenseDate);
            pnlContent.Controls.Add(label8);
            pnlContent.Controls.Add(cmbLicenseClass);
            pnlContent.Controls.Add(label7);
            pnlContent.Controls.Add(txtLicenseNumber);
            pnlContent.Controls.Add(label6);
            pnlContent.Controls.Add(dtpBirthDate);
            pnlContent.Controls.Add(label5);
            pnlContent.Controls.Add(txtIdentityNumber);
            pnlContent.Controls.Add(label4);
            pnlContent.Controls.Add(txtLastName);
            pnlContent.Controls.Add(label3);
            pnlContent.Controls.Add(txtFirstName);
            pnlContent.Controls.Add(label2);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 60);
            pnlContent.Name = "pnlContent";
            pnlContent.Padding = new Padding(20);
            pnlContent.Size = new Size(798, 538);
            pnlContent.TabIndex = 1;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            lblError.ForeColor = Color.FromArgb(220, 53, 69);
            lblError.Location = new Point(20, 487);
            lblError.Name = "lblError";
            lblError.Size = new Size(93, 17);
            lblError.TabIndex = 36;
            lblError.Text = "Hata mesajı...";
            lblError.Visible = false;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(108, 117, 125);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(579, 478);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 40);
            btnCancel.TabIndex = 14;
            btnCancel.Text = "İptal";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += BtnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(40, 167, 69);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(685, 478);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(90, 40);
            btnSave.TabIndex = 15;
            btnSave.Text = "Kaydet";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += BtnSave_Click;
            // 
            // cmbCustomerType
            // 
            cmbCustomerType.FormattingEnabled = true;
            cmbCustomerType.Location = new Point(579, 352);
            cmbCustomerType.Name = "cmbCustomerType";
            cmbCustomerType.Size = new Size(200, 23);
            cmbCustomerType.TabIndex = 13;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(579, 334);
            label13.Name = "label13";
            label13.Size = new Size(73, 15);
            label13.TabIndex = 24;
            label13.Text = "Müşteri Tipi:";
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(579, 246);
            txtAddress.Multiline = true;
            txtAddress.Name = "txtAddress";
            txtAddress.ScrollBars = ScrollBars.Vertical;
            txtAddress.Size = new Size(200, 76);
            txtAddress.TabIndex = 12;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(579, 228);
            label12.Name = "label12";
            label12.Size = new Size(40, 15);
            label12.TabIndex = 22;
            label12.Text = "Adres:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(579, 193);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(200, 23);
            txtEmail.TabIndex = 11;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(579, 175);
            label11.Name = "label11";
            label11.Size = new Size(50, 15);
            label11.TabIndex = 20;
            label11.Text = "E-posta:";
            // 
            // txtPhoneNumber
            // 
            txtPhoneNumber.Location = new Point(649, 140);
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.Size = new Size(130, 23);
            txtPhoneNumber.TabIndex = 10;
            txtPhoneNumber.KeyPress += TxtPhoneNumber_KeyPress;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(649, 122);
            label10.Name = "label10";
            label10.Size = new Size(103, 15);
            label10.TabIndex = 18;
            label10.Text = "Telefon Numarası:";
            // 
            // cmbCountryCode
            // 
            cmbCountryCode.FormattingEnabled = true;
            cmbCountryCode.Location = new Point(579, 140);
            cmbCountryCode.Name = "cmbCountryCode";
            cmbCountryCode.Size = new Size(64, 23);
            cmbCountryCode.TabIndex = 9;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(579, 122);
            label9.Name = "label9";
            label9.Size = new Size(64, 15);
            label9.TabIndex = 16;
            label9.Text = "Ülke Kodu:";
            // 
            // dtpLicenseDate
            // 
            dtpLicenseDate.Format = DateTimePickerFormat.Short;
            dtpLicenseDate.Location = new Point(302, 352);
            dtpLicenseDate.Name = "dtpLicenseDate";
            dtpLicenseDate.Size = new Size(217, 23);
            dtpLicenseDate.TabIndex = 8;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(302, 334);
            label8.Name = "label8";
            label8.Size = new Size(77, 15);
            label8.TabIndex = 14;
            label8.Text = "Ehliyet Tarihi:";
            // 
            // cmbLicenseClass
            // 
            cmbLicenseClass.FormattingEnabled = true;
            cmbLicenseClass.Location = new Point(302, 299);
            cmbLicenseClass.Name = "cmbLicenseClass";
            cmbLicenseClass.Size = new Size(217, 23);
            cmbLicenseClass.TabIndex = 7;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(302, 281);
            label7.Name = "label7";
            label7.Size = new Size(71, 15);
            label7.TabIndex = 12;
            label7.Text = "Ehliyet Sınıfı";
            // 
            // txtLicenseNumber
            // 
            txtLicenseNumber.Location = new Point(302, 246);
            txtLicenseNumber.Name = "txtLicenseNumber";
            txtLicenseNumber.Size = new Size(217, 23);
            txtLicenseNumber.TabIndex = 6;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(302, 228);
            label6.Name = "label6";
            label6.Size = new Size(64, 15);
            label6.TabIndex = 10;
            label6.Text = "Ehliyet No:";
            // 
            // dtpBirthDate
            // 
            dtpBirthDate.Format = DateTimePickerFormat.Short;
            dtpBirthDate.Location = new Point(302, 193);
            dtpBirthDate.Name = "dtpBirthDate";
            dtpBirthDate.Size = new Size(217, 23);
            dtpBirthDate.TabIndex = 5;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(302, 175);
            label5.Name = "label5";
            label5.Size = new Size(82, 15);
            label5.TabIndex = 8;
            label5.Text = "Doğum Tarihi:";
            // 
            // txtIdentityNumber
            // 
            txtIdentityNumber.Location = new Point(302, 140);
            txtIdentityNumber.Name = "txtIdentityNumber";
            txtIdentityNumber.Size = new Size(217, 23);
            txtIdentityNumber.TabIndex = 4;
            txtIdentityNumber.KeyPress += TxtIdentityNumber_KeyPress;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(302, 122);
            label4.Name = "label4";
            label4.Size = new Size(79, 15);
            label4.TabIndex = 6;
            label4.Text = "TC Kimlik No:";
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(20, 193);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(217, 23);
            txtLastName.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 175);
            label3.Name = "label3";
            label3.Size = new Size(42, 15);
            label3.TabIndex = 4;
            label3.Text = "Soyad:";
            // 
            // txtFirstName
            // 
            txtFirstName.Location = new Point(20, 140);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(217, 23);
            txtFirstName.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 122);
            label2.Name = "label2";
            label2.Size = new Size(25, 15);
            label2.TabIndex = 2;
            label2.Text = "Ad:";
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(49, 76, 143);
            pnlHeader.Controls.Add(btnClose);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(798, 60);
            pnlHeader.TabIndex = 0;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.ForeColor = Color.White;
            btnClose.IconChar = IconChar.Close;
            btnClose.IconColor = Color.White;
            btnClose.IconFont = IconFont.Auto;
            btnClose.IconSize = 24;
            btnClose.Location = new Point(762, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(32, 32);
            btnClose.TabIndex = 0;
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += BtnClose_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(141, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Müşteri Ekle";
            // 
            // CustomerAddForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 600);
            Controls.Add(pnlMain);
            FormBorderStyle = FormBorderStyle.None;
            Name = "CustomerAddForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Müşteri Ekle";
            pnlMain.ResumeLayout(false);
            pnlContent.ResumeLayout(false);
            pnlContent.PerformLayout();
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMain;
        private Panel pnlHeader;
        private Label lblTitle;
        private Panel pnlContent;
        private FontAwesome.Sharp.IconButton btnClose;
        private TextBox txtFirstName;
        private Label label2;
        private TextBox txtLastName;
        private Label label3;
        private TextBox txtIdentityNumber;
        private Label label4;
        private DateTimePicker dtpBirthDate;
        private Label label5;
        private TextBox txtLicenseNumber;
        private Label label6;
        private ComboBox cmbLicenseClass;
        private Label label7;
        private DateTimePicker dtpLicenseDate;
        private Label label8;
        private Label label9;
        private ComboBox cmbCountryCode;
        private TextBox txtPhoneNumber;
        private Label label10;
        private TextBox txtEmail;
        private Label label11;
        private TextBox txtAddress;
        private Label label12;
        private ComboBox cmbCustomerType;
        private Label label13;
        private Button btnCancel;
        private Button btnSave;
        private Label lblError;
    }
}