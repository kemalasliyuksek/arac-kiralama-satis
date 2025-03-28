using FontAwesome.Sharp;
using System.Drawing;
using System.Windows.Forms;

namespace arac_kiralama_satis_desktop.Interfaces
{
    partial class VehicleAddForm
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
            chkHasSalePrice = new CheckBox();
            nudSalePrice = new NumericUpDown();
            label15 = new Label();
            nudPurchasePrice = new NumericUpDown();
            label14 = new Label();
            cmbBranch = new ComboBox();
            label13 = new Label();
            cmbVehicleClass = new ComboBox();
            label12 = new Label();
            dtpPurchaseDate = new DateTimePicker();
            label11 = new Label();
            cmbStatus = new ComboBox();
            label10 = new Label();
            cmbTransmissionType = new ComboBox();
            label9 = new Label();
            cmbFuelType = new ComboBox();
            label8 = new Label();
            nudKilometers = new NumericUpDown();
            label7 = new Label();
            txtColor = new TextBox();
            label6 = new Label();
            txtChassisNo = new TextBox();
            label5 = new Label();
            txtEngineNo = new TextBox();
            label4 = new Label();
            nudYear = new NumericUpDown();
            label3 = new Label();
            txtModel = new TextBox();
            label2 = new Label();
            txtBrand = new TextBox();
            label1 = new Label();
            txtPlate = new TextBox();
            lblPlate = new Label();
            pnlHeader = new Panel();
            btnClose = new IconButton();
            lblTitle = new Label();
            pnlMain.SuspendLayout();
            pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudSalePrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudPurchasePrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudKilometers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudYear).BeginInit();
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
            pnlContent.Controls.Add(chkHasSalePrice);
            pnlContent.Controls.Add(nudSalePrice);
            pnlContent.Controls.Add(label15);
            pnlContent.Controls.Add(nudPurchasePrice);
            pnlContent.Controls.Add(label14);
            pnlContent.Controls.Add(cmbBranch);
            pnlContent.Controls.Add(label13);
            pnlContent.Controls.Add(cmbVehicleClass);
            pnlContent.Controls.Add(label12);
            pnlContent.Controls.Add(dtpPurchaseDate);
            pnlContent.Controls.Add(label11);
            pnlContent.Controls.Add(cmbStatus);
            pnlContent.Controls.Add(label10);
            pnlContent.Controls.Add(cmbTransmissionType);
            pnlContent.Controls.Add(label9);
            pnlContent.Controls.Add(cmbFuelType);
            pnlContent.Controls.Add(label8);
            pnlContent.Controls.Add(nudKilometers);
            pnlContent.Controls.Add(label7);
            pnlContent.Controls.Add(txtColor);
            pnlContent.Controls.Add(label6);
            pnlContent.Controls.Add(txtChassisNo);
            pnlContent.Controls.Add(label5);
            pnlContent.Controls.Add(txtEngineNo);
            pnlContent.Controls.Add(label4);
            pnlContent.Controls.Add(nudYear);
            pnlContent.Controls.Add(label3);
            pnlContent.Controls.Add(txtModel);
            pnlContent.Controls.Add(label2);
            pnlContent.Controls.Add(txtBrand);
            pnlContent.Controls.Add(label1);
            pnlContent.Controls.Add(txtPlate);
            pnlContent.Controls.Add(lblPlate);
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
            lblError.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblError.ForeColor = Color.FromArgb(220, 53, 69);
            lblError.Location = new Point(20, 487);
            lblError.Name = "lblError";
            lblError.Size = new Size(104, 17);
            lblError.TabIndex = 36;
            lblError.Text = "Hata mesajı...";
            lblError.Visible = false;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(108, 117, 125);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(579, 478);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 40);
            btnCancel.TabIndex = 18;
            btnCancel.Text = "İptal";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += BtnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(40, 167, 69);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(685, 478);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(90, 40);
            btnSave.TabIndex = 19;
            btnSave.Text = "Kaydet";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += BtnSave_Click;
            // 
            // chkHasSalePrice
            // 
            chkHasSalePrice.AutoSize = true;
            chkHasSalePrice.Location = new Point(579, 430);
            chkHasSalePrice.Name = "chkHasSalePrice";
            chkHasSalePrice.Size = new Size(113, 19);
            chkHasSalePrice.TabIndex = 17;
            chkHasSalePrice.Text = "Satış Fiyatı Belirle";
            chkHasSalePrice.UseVisualStyleBackColor = true;
            chkHasSalePrice.CheckedChanged += ChkHasSalePrice_CheckedChanged;
            // 
            // nudSalePrice
            // 
            nudSalePrice.Enabled = false;
            nudSalePrice.Location = new Point(579, 402);
            nudSalePrice.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            nudSalePrice.Name = "nudSalePrice";
            nudSalePrice.Size = new Size(196, 23);
            nudSalePrice.TabIndex = 16;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(579, 384);
            label15.Name = "label15";
            label15.Size = new Size(64, 15);
            label15.TabIndex = 33;
            label15.Text = "Satış Fiyatı:";
            // 
            // nudPurchasePrice
            // 
            nudPurchasePrice.Location = new Point(579, 352);
            nudPurchasePrice.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            nudPurchasePrice.Name = "nudPurchasePrice";
            nudPurchasePrice.Size = new Size(196, 23);
            nudPurchasePrice.TabIndex = 15;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(579, 334);
            label14.Name = "label14";
            label14.Size = new Size(62, 15);
            label14.TabIndex = 31;
            label14.Text = "Alış Fiyatı:";
            // 
            // cmbBranch
            // 
            cmbBranch.FormattingEnabled = true;
            cmbBranch.Location = new Point(579, 299);
            cmbBranch.Name = "cmbBranch";
            cmbBranch.Size = new Size(196, 23);
            cmbBranch.TabIndex = 14;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(579, 281);
            label13.Name = "label13";
            label13.Size = new Size(37, 15);
            label13.TabIndex = 29;
            label13.Text = "Şube:";
            // 
            // cmbVehicleClass
            // 
            cmbVehicleClass.FormattingEnabled = true;
            cmbVehicleClass.Location = new Point(579, 246);
            cmbVehicleClass.Name = "cmbVehicleClass";
            cmbVehicleClass.Size = new Size(196, 23);
            cmbVehicleClass.TabIndex = 13;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(579, 228);
            label12.Name = "label12";
            label12.Size = new Size(64, 15);
            label12.TabIndex = 27;
            label12.Text = "Araç Sınıfı:";
            // 
            // dtpPurchaseDate
            // 
            dtpPurchaseDate.Format = DateTimePickerFormat.Short;
            dtpPurchaseDate.Location = new Point(579, 193);
            dtpPurchaseDate.Name = "dtpPurchaseDate";
            dtpPurchaseDate.Size = new Size(196, 23);
            dtpPurchaseDate.TabIndex = 12;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(579, 175);
            label11.Name = "label11";
            label11.Size = new Size(65, 15);
            label11.TabIndex = 25;
            label11.Text = "Alış Tarihi:";
            // 
            // cmbStatus
            // 
            cmbStatus.FormattingEnabled = true;
            cmbStatus.Location = new Point(579, 140);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(196, 23);
            cmbStatus.TabIndex = 11;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(579, 122);
            label10.Name = "label10";
            label10.Size = new Size(83, 15);
            label10.TabIndex = 23;
            label10.Text = "Araç Durumu:";
            // 
            // cmbTransmissionType
            // 
            cmbTransmissionType.FormattingEnabled = true;
            cmbTransmissionType.Location = new Point(579, 87);
            cmbTransmissionType.Name = "cmbTransmissionType";
            cmbTransmissionType.Size = new Size(196, 23);
            cmbTransmissionType.TabIndex = 10;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(579, 69);
            label9.Name = "label9";
            label9.Size = new Size(59, 15);
            label9.TabIndex = 21;
            label9.Text = "Vites Tipi:";
            // 
            // cmbFuelType
            // 
            cmbFuelType.FormattingEnabled = true;
            cmbFuelType.Location = new Point(302, 352);
            cmbFuelType.Name = "cmbFuelType";
            cmbFuelType.Size = new Size(209, 23);
            cmbFuelType.TabIndex = 9;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(302, 334);
            label8.Name = "label8";
            label8.Size = new Size(58, 15);
            label8.TabIndex = 19;
            label8.Text = "Yakıt Tipi:";
            // 
            // nudKilometers
            // 
            nudKilometers.Location = new Point(302, 299);
            nudKilometers.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudKilometers.Name = "nudKilometers";
            nudKilometers.Size = new Size(209, 23);
            nudKilometers.TabIndex = 8;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(302, 281);
            label7.Name = "label7";
            label7.Size = new Size(64, 15);
            label7.TabIndex = 17;
            label7.Text = "Kilometre:";
            // 
            // txtColor
            // 
            txtColor.Location = new Point(302, 246);
            txtColor.Name = "txtColor";
            txtColor.Size = new Size(209, 23);
            txtColor.TabIndex = 7;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(302, 228);
            label6.Name = "label6";
            label6.Size = new Size(39, 15);
            label6.TabIndex = 15;
            label6.Text = "Renk:";
            // 
            // txtChassisNo
            // 
            txtChassisNo.Location = new Point(302, 193);
            txtChassisNo.Name = "txtChassisNo";
            txtChassisNo.Size = new Size(209, 23);
            txtChassisNo.TabIndex = 6;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(302, 175);
            label5.Name = "label5";
            label5.Size = new Size(60, 15);
            label5.TabIndex = 13;
            label5.Text = "Şasi No:";
            // 
            // txtEngineNo
            // 
            txtEngineNo.Location = new Point(302, 140);
            txtEngineNo.Name = "txtEngineNo";
            txtEngineNo.Size = new Size(209, 23);
            txtEngineNo.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(302, 122);
            label4.Name = "label4";
            label4.Size = new Size(66, 15);
            label4.TabIndex = 11;
            label4.Text = "Motor No:";
            // 
            // nudYear
            // 
            nudYear.Location = new Point(302, 87);
            nudYear.Maximum = new decimal(new int[] { 3000, 0, 0, 0 });
            nudYear.Minimum = new decimal(new int[] { 1900, 0, 0, 0 });
            nudYear.Name = "nudYear";
            nudYear.Size = new Size(209, 23);
            nudYear.TabIndex = 4;
            nudYear.Value = new decimal(new int[] { 2023, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(302, 69);
            label3.Name = "label3";
            label3.Size = new Size(61, 15);
            label3.TabIndex = 9;
            label3.Text = "Yapım Yılı:";
            // 
            // txtModel
            // 
            txtModel.Location = new Point(20, 193);
            txtModel.Name = "txtModel";
            txtModel.Size = new Size(217, 23);
            txtModel.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 175);
            label2.Name = "label2";
            label2.Size = new Size(44, 15);
            label2.TabIndex = 7;
            label2.Text = "Model:";
            // 
            // txtBrand
            // 
            txtBrand.Location = new Point(20, 140);
            txtBrand.Name = "txtBrand";
            txtBrand.Size = new Size(217, 23);
            txtBrand.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 122);
            label1.Name = "label1";
            label1.Size = new Size(44, 15);
            label1.TabIndex = 5;
            label1.Text = "Marka:";
            // 
            // txtPlate
            // 
            txtPlate.CharacterCasing = CharacterCasing.Upper;
            txtPlate.Location = new Point(20, 87);
            txtPlate.Name = "txtPlate";
            txtPlate.Size = new Size(217, 23);
            txtPlate.TabIndex = 1;
            // 
            // lblPlate
            // 
            lblPlate.AutoSize = true;
            lblPlate.Location = new Point(20, 69);
            lblPlate.Name = "lblPlate";
            lblPlate.Size = new Size(39, 15);
            lblPlate.TabIndex = 3;
            lblPlate.Text = "Plaka:";
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
            btnClose.IconChar = IconChar.Times;
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
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(120, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Araç Ekle";
            // 
            // VehicleAddForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 600);
            Controls.Add(pnlMain);
            FormBorderStyle = FormBorderStyle.None;
            Name = "VehicleAddForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Araç Ekle";
            pnlMain.ResumeLayout(false);
            pnlContent.ResumeLayout(false);
            pnlContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudSalePrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudPurchasePrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudKilometers).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudYear).EndInit();
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
        private TextBox txtPlate;
        private Label lblPlate;
        private TextBox txtModel;
        private Label label2;
        private TextBox txtBrand;
        private Label label1;
        private NumericUpDown nudYear;
        private Label label3;
        private TextBox txtEngineNo;
        private Label label4;
        private TextBox txtChassisNo;
        private Label label5;
        private TextBox txtColor;
        private Label label6;
        private NumericUpDown nudKilometers;
        private Label label7;
        private ComboBox cmbFuelType;
        private Label label8;
        private ComboBox cmbTransmissionType;
        private Label label9;
        private ComboBox cmbStatus;
        private Label label10;
        private DateTimePicker dtpPurchaseDate;
        private Label label11;
        private ComboBox cmbVehicleClass;
        private Label label12;
        private ComboBox cmbBranch;
        private Label label13;
        private NumericUpDown nudPurchasePrice;
        private Label label14;
        private NumericUpDown nudSalePrice;
        private Label label15;
        private CheckBox chkHasSalePrice;
        private Button btnCancel;
        private Button btnSave;
        private Label lblError;
    }
}