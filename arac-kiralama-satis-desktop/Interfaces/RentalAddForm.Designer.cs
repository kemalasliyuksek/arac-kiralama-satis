using FontAwesome.Sharp;
using System.Drawing;
using System.Windows.Forms;

namespace arac_kiralama_satis_desktop.Interfaces
{
    partial class RentalAddForm
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
            pnlMain = new Panel();
            pnlContent = new Panel();
            lblError = new Label();
            btnCancel = new Button();
            btnSave = new Button();
            grpPayment = new GroupBox();
            cmbPaymentType = new ComboBox();
            label10 = new Label();
            nudDepositAmount = new NumericUpDown();
            chkDeposit = new CheckBox();
            nudRentalAmount = new NumericUpDown();
            label8 = new Label();
            grpVehicleInfo = new GroupBox();
            nudEndKm = new NumericUpDown();
            chkEndKm = new CheckBox();
            nudStartKm = new NumericUpDown();
            label7 = new Label();
            cmbVehicle = new ComboBox();
            label6 = new Label();
            grpRentalPeriod = new GroupBox();
            dtpReturnDate = new DateTimePicker();
            chkReturnDate = new CheckBox();
            dtpEndDate = new DateTimePicker();
            label4 = new Label();
            dtpStartDate = new DateTimePicker();
            label3 = new Label();
            grpCustomerInfo = new GroupBox();
            cmbCustomer = new ComboBox();
            label2 = new Label();
            pnlHeader = new Panel();
            btnClose = new IconButton();
            lblTitle = new Label();
            errorProvider = new ErrorProvider(components);
            label1 = new Label();
            pnlMain.SuspendLayout();
            pnlContent.SuspendLayout();
            grpPayment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudDepositAmount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudRentalAmount).BeginInit();
            grpVehicleInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudEndKm).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStartKm).BeginInit();
            grpRentalPeriod.SuspendLayout();
            grpCustomerInfo.SuspendLayout();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
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
            pnlMain.Size = new Size(800, 700);
            pnlMain.TabIndex = 0;
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.White;
            pnlContent.Controls.Add(lblError);
            pnlContent.Controls.Add(btnCancel);
            pnlContent.Controls.Add(btnSave);
            pnlContent.Controls.Add(grpPayment);
            pnlContent.Controls.Add(grpVehicleInfo);
            pnlContent.Controls.Add(grpRentalPeriod);
            pnlContent.Controls.Add(grpCustomerInfo);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 60);
            pnlContent.Name = "pnlContent";
            pnlContent.Padding = new Padding(20);
            pnlContent.Size = new Size(798, 638);
            pnlContent.TabIndex = 1;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            lblError.ForeColor = Color.FromArgb(220, 53, 69);
            lblError.Location = new Point(20, 587);
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
            btnCancel.Location = new Point(579, 578);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 40);
            btnCancel.TabIndex = 15;
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
            btnSave.Location = new Point(685, 578);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(90, 40);
            btnSave.TabIndex = 16;
            btnSave.Text = "Kaydet";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += BtnSave_Click;
            // 
            // grpPayment
            // 
            grpPayment.Controls.Add(cmbPaymentType);
            grpPayment.Controls.Add(label10);
            grpPayment.Controls.Add(nudDepositAmount);
            grpPayment.Controls.Add(chkDeposit);
            grpPayment.Controls.Add(nudRentalAmount);
            grpPayment.Controls.Add(label8);
            grpPayment.Location = new Point(20, 437);
            grpPayment.Name = "grpPayment";
            grpPayment.Size = new Size(755, 125);
            grpPayment.TabIndex = 3;
            grpPayment.TabStop = false;
            grpPayment.Text = "Ödeme Bilgileri";
            // 
            // cmbPaymentType
            // 
            cmbPaymentType.FormattingEnabled = true;
            cmbPaymentType.Location = new Point(133, 77);
            cmbPaymentType.Name = "cmbPaymentType";
            cmbPaymentType.Size = new Size(200, 23);
            cmbPaymentType.TabIndex = 13;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(20, 80);
            label10.Name = "label10";
            label10.Size = new Size(69, 15);
            label10.TabIndex = 12;
            label10.Text = "Ödeme Tipi";
            // 
            // nudDepositAmount
            // 
            nudDepositAmount.DecimalPlaces = 2;
            nudDepositAmount.Enabled = false;
            nudDepositAmount.Location = new Point(534, 35);
            nudDepositAmount.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudDepositAmount.Name = "nudDepositAmount";
            nudDepositAmount.Size = new Size(200, 23);
            nudDepositAmount.TabIndex = 11;
            nudDepositAmount.ThousandsSeparator = true;
            // 
            // chkDeposit
            // 
            chkDeposit.AutoSize = true;
            chkDeposit.Location = new Point(400, 36);
            chkDeposit.Name = "chkDeposit";
            chkDeposit.Size = new Size(114, 19);
            chkDeposit.TabIndex = 10;
            chkDeposit.Text = "Depozit Alınacak";
            chkDeposit.UseVisualStyleBackColor = true;
            chkDeposit.CheckedChanged += ChkDeposit_CheckedChanged;
            // 
            // nudRentalAmount
            // 
            nudRentalAmount.DecimalPlaces = 2;
            nudRentalAmount.Location = new Point(133, 35);
            nudRentalAmount.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudRentalAmount.Name = "nudRentalAmount";
            nudRentalAmount.Size = new Size(200, 23);
            nudRentalAmount.TabIndex = 9;
            nudRentalAmount.ThousandsSeparator = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(20, 37);
            label8.Name = "label8";
            label8.Size = new Size(87, 15);
            label8.TabIndex = 8;
            label8.Text = "Kiralama Tutarı";
            // 
            // grpVehicleInfo
            // 
            grpVehicleInfo.Controls.Add(nudEndKm);
            grpVehicleInfo.Controls.Add(chkEndKm);
            grpVehicleInfo.Controls.Add(nudStartKm);
            grpVehicleInfo.Controls.Add(label7);
            grpVehicleInfo.Controls.Add(cmbVehicle);
            grpVehicleInfo.Controls.Add(label6);
            grpVehicleInfo.Location = new Point(20, 301);
            grpVehicleInfo.Name = "grpVehicleInfo";
            grpVehicleInfo.Size = new Size(755, 125);
            grpVehicleInfo.TabIndex = 2;
            grpVehicleInfo.TabStop = false;
            grpVehicleInfo.Text = "Araç Bilgileri";
            // 
            // nudEndKm
            // 
            nudEndKm.Enabled = false;
            nudEndKm.Location = new Point(534, 77);
            nudEndKm.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudEndKm.Name = "nudEndKm";
            nudEndKm.Size = new Size(200, 23);
            nudEndKm.TabIndex = 8;
            // 
            // chkEndKm
            // 
            chkEndKm.AutoSize = true;
            chkEndKm.Location = new Point(400, 78);
            chkEndKm.Name = "chkEndKm";
            chkEndKm.Size = new Size(99, 19);
            chkEndKm.TabIndex = 7;
            chkEndKm.Text = "Dönüş Km Gir";
            chkEndKm.UseVisualStyleBackColor = true;
            chkEndKm.CheckedChanged += ChkEndKm_CheckedChanged;
            // 
            // nudStartKm
            // 
            nudStartKm.Location = new Point(133, 77);
            nudStartKm.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudStartKm.Name = "nudStartKm";
            nudStartKm.Size = new Size(200, 23);
            nudStartKm.TabIndex = 6;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(20, 79);
            label7.Name = "label7";
            label7.Size = new Size(78, 15);
            label7.TabIndex = 5;
            label7.Text = "Başlangıç Km";
            // 
            // cmbVehicle
            // 
            cmbVehicle.FormattingEnabled = true;
            cmbVehicle.Location = new Point(133, 35);
            cmbVehicle.Name = "cmbVehicle";
            cmbVehicle.Size = new Size(601, 23);
            cmbVehicle.TabIndex = 4;
            cmbVehicle.SelectedIndexChanged += CmbVehicle_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(20, 38);
            label6.Name = "label6";
            label6.Size = new Size(31, 15);
            label6.TabIndex = 3;
            label6.Text = "Araç";
            // 
            // grpRentalPeriod
            // 
            grpRentalPeriod.Controls.Add(label1);
            grpRentalPeriod.Controls.Add(dtpReturnDate);
            grpRentalPeriod.Controls.Add(chkReturnDate);
            grpRentalPeriod.Controls.Add(dtpEndDate);
            grpRentalPeriod.Controls.Add(label4);
            grpRentalPeriod.Controls.Add(dtpStartDate);
            grpRentalPeriod.Controls.Add(label3);
            grpRentalPeriod.Location = new Point(20, 165);
            grpRentalPeriod.Name = "grpRentalPeriod";
            grpRentalPeriod.Size = new Size(755, 125);
            grpRentalPeriod.TabIndex = 1;
            grpRentalPeriod.TabStop = false;
            grpRentalPeriod.Text = "Kiralama Süresi";
            // 
            // dtpReturnDate
            // 
            dtpReturnDate.Enabled = false;
            dtpReturnDate.Format = DateTimePickerFormat.Short;
            dtpReturnDate.Location = new Point(534, 77);
            dtpReturnDate.Name = "dtpReturnDate";
            dtpReturnDate.Size = new Size(200, 23);
            dtpReturnDate.TabIndex = 5;
            // 
            // chkReturnDate
            // 
            chkReturnDate.AutoSize = true;
            chkReturnDate.Location = new Point(400, 79);
            chkReturnDate.Name = "chkReturnDate";
            chkReturnDate.Size = new Size(127, 19);
            chkReturnDate.TabIndex = 4;
            chkReturnDate.Text = "Dönüş Tarihi Belirle";
            chkReturnDate.UseVisualStyleBackColor = true;
            chkReturnDate.CheckedChanged += ChkReturnDate_CheckedChanged;
            // 
            // dtpEndDate
            // 
            dtpEndDate.Format = DateTimePickerFormat.Short;
            dtpEndDate.Location = new Point(133, 77);
            dtpEndDate.Name = "dtpEndDate";
            dtpEndDate.Size = new Size(200, 23);
            dtpEndDate.TabIndex = 3;
            dtpEndDate.ValueChanged += DtpEndDate_ValueChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(20, 83);
            label4.Name = "label4";
            label4.Size = new Size(61, 15);
            label4.TabIndex = 2;
            label4.Text = "Bitiş Tarihi";
            // 
            // dtpStartDate
            // 
            dtpStartDate.Format = DateTimePickerFormat.Short;
            dtpStartDate.Location = new Point(133, 35);
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Size = new Size(200, 23);
            dtpStartDate.TabIndex = 1;
            dtpStartDate.ValueChanged += DtpStartDate_ValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 41);
            label3.Name = "label3";
            label3.Size = new Size(89, 15);
            label3.TabIndex = 0;
            label3.Text = "Başlangıç Tarihi";
            // 
            // grpCustomerInfo
            // 
            grpCustomerInfo.Controls.Add(cmbCustomer);
            grpCustomerInfo.Controls.Add(label2);
            grpCustomerInfo.Location = new Point(20, 30);
            grpCustomerInfo.Name = "grpCustomerInfo";
            grpCustomerInfo.Size = new Size(755, 80);
            grpCustomerInfo.TabIndex = 0;
            grpCustomerInfo.TabStop = false;
            grpCustomerInfo.Text = "Müşteri Bilgileri";
            // 
            // cmbCustomer
            // 
            cmbCustomer.FormattingEnabled = true;
            cmbCustomer.Location = new Point(133, 35);
            cmbCustomer.Name = "cmbCustomer";
            cmbCustomer.Size = new Size(601, 23);
            cmbCustomer.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 38);
            label2.Name = "label2";
            label2.Size = new Size(47, 15);
            label2.TabIndex = 0;
            label2.Text = "Müşteri";
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
            lblTitle.Size = new Size(152, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Yeni Kiralama";
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.Red;
            label1.Location = new Point(360, 35);
            label1.Name = "label1";
            label1.Size = new Size(120, 15);
            label1.TabIndex = 6;
            label1.Text = "*Saat kısmı eklenecek";
            // 
            // RentalAddForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 700);
            Controls.Add(pnlMain);
            FormBorderStyle = FormBorderStyle.None;
            Name = "RentalAddForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Kiralama Ekle";
            Load += RentalAddForm_Load;
            pnlMain.ResumeLayout(false);
            pnlContent.ResumeLayout(false);
            pnlContent.PerformLayout();
            grpPayment.ResumeLayout(false);
            grpPayment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudDepositAmount).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudRentalAmount).EndInit();
            grpVehicleInfo.ResumeLayout(false);
            grpVehicleInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudEndKm).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStartKm).EndInit();
            grpRentalPeriod.ResumeLayout(false);
            grpRentalPeriod.PerformLayout();
            grpCustomerInfo.ResumeLayout(false);
            grpCustomerInfo.PerformLayout();
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMain;
        private Panel pnlHeader;
        private Label lblTitle;
        private Panel pnlContent;
        private FontAwesome.Sharp.IconButton btnClose;
        private GroupBox grpCustomerInfo;
        private Label label2;
        private ComboBox cmbCustomer;
        private GroupBox grpRentalPeriod;
        private Label label3;
        private DateTimePicker dtpStartDate;
        private DateTimePicker dtpEndDate;
        private Label label4;
        private CheckBox chkReturnDate;
        private DateTimePicker dtpReturnDate;
        private GroupBox grpVehicleInfo;
        private ComboBox cmbVehicle;
        private Label label6;
        private NumericUpDown nudStartKm;
        private Label label7;
        private CheckBox chkEndKm;
        private NumericUpDown nudEndKm;
        private GroupBox grpPayment;
        private NumericUpDown nudRentalAmount;
        private Label label8;
        private CheckBox chkDeposit;
        private NumericUpDown nudDepositAmount;
        private ComboBox cmbPaymentType;
        private Label label10;
        private Button btnCancel;
        private Button btnSave;
        private Label lblError;
        private ErrorProvider errorProvider;
        private Label label1;
    }
}