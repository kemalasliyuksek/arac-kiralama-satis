namespace arac_kiralama_satis_desktop.Utils
{
    partial class ConnectionStringEncryptor
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblDescription = new System.Windows.Forms.Label();
            this.cmbConnectionNames = new System.Windows.Forms.ComboBox();
            this.lblConnectionName = new System.Windows.Forms.Label();
            this.lblConnectionString = new System.Windows.Forms.Label();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.lblEncryptedString = new System.Windows.Forms.Label();
            this.txtEncryptedString = new System.Windows.Forms.TextBox();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlHeader.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(248, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Bağlantı Dizesi Şifreleme";
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(76)))), ((int)(((byte)(143)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(684, 60);
            this.pnlHeader.TabIndex = 1;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(20, 25);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(568, 15);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Bu araç, veritabanı bağlantı dizelerini şifreleyerek güvenlik sağlar. Şifreli değeri kopyalayıp manuel ekleyebilirsiniz.";
            // 
            // cmbConnectionNames
            // 
            this.cmbConnectionNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConnectionNames.FormattingEnabled = true;
            this.cmbConnectionNames.Location = new System.Drawing.Point(164, 60);
            this.cmbConnectionNames.Name = "cmbConnectionNames";
            this.cmbConnectionNames.Size = new System.Drawing.Size(432, 23);
            this.cmbConnectionNames.TabIndex = 3;
            this.cmbConnectionNames.SelectedIndexChanged += new System.EventHandler(this.cmbConnectionNames_SelectedIndexChanged);
            // 
            // lblConnectionName
            // 
            this.lblConnectionName.AutoSize = true;
            this.lblConnectionName.Location = new System.Drawing.Point(20, 63);
            this.lblConnectionName.Name = "lblConnectionName";
            this.lblConnectionName.Size = new System.Drawing.Size(138, 15);
            this.lblConnectionName.TabIndex = 4;
            this.lblConnectionName.Text = "Bağlantı Dizesi Seçiniz:";
            // 
            // lblConnectionString
            // 
            this.lblConnectionString.AutoSize = true;
            this.lblConnectionString.Location = new System.Drawing.Point(20, 103);
            this.lblConnectionString.Name = "lblConnectionString";
            this.lblConnectionString.Size = new System.Drawing.Size(89, 15);
            this.lblConnectionString.TabIndex = 5;
            this.lblConnectionString.Text = "Bağlantı Dizesi:";
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(20, 121);
            this.txtConnectionString.Multiline = true;
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConnectionString.Size = new System.Drawing.Size(644, 60);
            this.txtConnectionString.TabIndex = 6;
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(76)))), ((int)(((byte)(143)))));
            this.btnEncrypt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEncrypt.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnEncrypt.ForeColor = System.Drawing.Color.White;
            this.btnEncrypt.Location = new System.Drawing.Point(20, 196);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(150, 30);
            this.btnEncrypt.TabIndex = 7;
            this.btnEncrypt.Text = "Şifrele";
            this.btnEncrypt.UseVisualStyleBackColor = false;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // lblEncryptedString
            // 
            this.lblEncryptedString.AutoSize = true;
            this.lblEncryptedString.Location = new System.Drawing.Point(20, 243);
            this.lblEncryptedString.Name = "lblEncryptedString";
            this.lblEncryptedString.Size = new System.Drawing.Size(114, 15);
            this.lblEncryptedString.TabIndex = 8;
            this.lblEncryptedString.Text = "Şifrelenmiş Değer:";
            // 
            // txtEncryptedString
            // 
            this.txtEncryptedString.Location = new System.Drawing.Point(20, 261);
            this.txtEncryptedString.Multiline = true;
            this.txtEncryptedString.Name = "txtEncryptedString";
            this.txtEncryptedString.ReadOnly = true;
            this.txtEncryptedString.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEncryptedString.Size = new System.Drawing.Size(644, 60);
            this.txtEncryptedString.TabIndex = 9;
            // 
            // btnCopy
            // 
            this.btnCopy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCopy.ForeColor = System.Drawing.Color.White;
            this.btnCopy.Location = new System.Drawing.Point(20, 336);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(150, 30);
            this.btnCopy.TabIndex = 10;
            this.btnCopy.Text = "Panoya Kopyala";
            this.btnCopy.UseVisualStyleBackColor = false;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnTest
            // 
            this.btnTest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTest.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnTest.ForeColor = System.Drawing.Color.White;
            this.btnTest.Location = new System.Drawing.Point(185, 336);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(150, 30);
            this.btnTest.TabIndex = 12;
            this.btnTest.Text = "Şifre Çözmeyi Test Et";
            this.btnTest.UseVisualStyleBackColor = false;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.lblDescription);
            this.pnlMain.Controls.Add(this.btnTest);
            this.pnlMain.Controls.Add(this.cmbConnectionNames);
            this.pnlMain.Controls.Add(this.lblConnectionName);
            this.pnlMain.Controls.Add(this.btnCopy);
            this.pnlMain.Controls.Add(this.lblConnectionString);
            this.pnlMain.Controls.Add(this.txtEncryptedString);
            this.pnlMain.Controls.Add(this.txtConnectionString);
            this.pnlMain.Controls.Add(this.lblEncryptedString);
            this.pnlMain.Controls.Add(this.btnEncrypt);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 60);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(20);
            this.pnlMain.Size = new System.Drawing.Size(684, 391);
            this.pnlMain.TabIndex = 13;
            // 
            // ConnectionStringEncryptor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 451);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConnectionStringEncryptor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bağlantı Dizesi Şifreleme Aracı";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.ComboBox cmbConnectionNames;
        private System.Windows.Forms.Label lblConnectionName;
        private System.Windows.Forms.Label lblConnectionString;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Label lblEncryptedString;
        private System.Windows.Forms.TextBox txtEncryptedString;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Panel pnlMain;
    }
}