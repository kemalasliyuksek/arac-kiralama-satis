using FontAwesome.Sharp;

namespace arac_kiralama_satis_desktop.Controls
{
    partial class VehiclesControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlVehicles = new Panel();
            pnlVehiclesContent = new Panel();
            dgvVehicles = new DataGridView();
            pnlVehiclesHeader = new Panel();
            lblVehiclesTitle = new Label();
            btnAddVehicle = new IconButton();
            btnRefreshVehicles = new IconButton();
            txtSearchVehicles = new TextBox();
            pnlVehicles.SuspendLayout();
            pnlVehiclesContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVehicles).BeginInit();
            pnlVehiclesHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlVehicles
            // 
            pnlVehicles.Controls.Add(pnlVehiclesContent);
            pnlVehicles.Controls.Add(pnlVehiclesHeader);
            pnlVehicles.Dock = DockStyle.Fill;
            pnlVehicles.Location = new Point(0, 0);
            pnlVehicles.Name = "pnlVehicles";
            pnlVehicles.Size = new Size(1010, 700);
            pnlVehicles.TabIndex = 0;
            // 
            // pnlVehiclesContent
            // 
            pnlVehiclesContent.Controls.Add(dgvVehicles);
            pnlVehiclesContent.Dock = DockStyle.Fill;
            pnlVehiclesContent.Location = new Point(0, 80);
            pnlVehiclesContent.Name = "pnlVehiclesContent";
            pnlVehiclesContent.Padding = new Padding(10);
            pnlVehiclesContent.Size = new Size(1010, 620);
            pnlVehiclesContent.TabIndex = 1;
            // 
            // dgvVehicles
            // 
            dgvVehicles.AllowUserToAddRows = false;
            dgvVehicles.AllowUserToDeleteRows = false;
            dgvVehicles.BackgroundColor = Color.White;
            dgvVehicles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVehicles.Dock = DockStyle.Fill;
            dgvVehicles.Location = new Point(10, 10);
            dgvVehicles.Name = "dgvVehicles";
            dgvVehicles.ReadOnly = true;
            dgvVehicles.Size = new Size(990, 600);
            dgvVehicles.TabIndex = 0;
            // 
            // pnlVehiclesHeader
            // 
            pnlVehiclesHeader.BackColor = Color.White;
            pnlVehiclesHeader.Controls.Add(lblVehiclesTitle);
            pnlVehiclesHeader.Controls.Add(btnAddVehicle);
            pnlVehiclesHeader.Controls.Add(btnRefreshVehicles);
            pnlVehiclesHeader.Controls.Add(txtSearchVehicles);
            pnlVehiclesHeader.Dock = DockStyle.Top;
            pnlVehiclesHeader.Location = new Point(0, 0);
            pnlVehiclesHeader.Name = "pnlVehiclesHeader";
            pnlVehiclesHeader.Size = new Size(1010, 80);
            pnlVehiclesHeader.TabIndex = 0;
            // 
            // lblVehiclesTitle
            // 
            lblVehiclesTitle.AutoSize = true;
            lblVehiclesTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblVehiclesTitle.ForeColor = Color.FromArgb(49, 76, 143);
            lblVehiclesTitle.Location = new Point(22, 25);
            lblVehiclesTitle.Name = "lblVehiclesTitle";
            lblVehiclesTitle.Size = new Size(130, 30);
            lblVehiclesTitle.TabIndex = 3;
            lblVehiclesTitle.Text = "Araç Listesi";
            // 
            // btnAddVehicle
            // 
            btnAddVehicle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddVehicle.BackColor = Color.FromArgb(40, 167, 69);
            btnAddVehicle.FlatAppearance.BorderSize = 0;
            btnAddVehicle.FlatStyle = FlatStyle.Flat;
            btnAddVehicle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnAddVehicle.ForeColor = Color.White;
            btnAddVehicle.IconChar = IconChar.Add;
            btnAddVehicle.IconColor = Color.White;
            btnAddVehicle.IconFont = IconFont.Auto;
            btnAddVehicle.IconSize = 20;
            btnAddVehicle.Location = new Point(870, 20);
            btnAddVehicle.Name = "btnAddVehicle";
            btnAddVehicle.Size = new Size(130, 40);
            btnAddVehicle.TabIndex = 2;
            btnAddVehicle.Text = "Yeni Araç";
            btnAddVehicle.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAddVehicle.UseVisualStyleBackColor = false;
            btnAddVehicle.Click += BtnAddVehicle_Click;
            // 
            // btnRefreshVehicles
            // 
            btnRefreshVehicles.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefreshVehicles.BackColor = Color.FromArgb(108, 117, 125);
            btnRefreshVehicles.FlatAppearance.BorderSize = 0;
            btnRefreshVehicles.FlatStyle = FlatStyle.Flat;
            btnRefreshVehicles.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnRefreshVehicles.ForeColor = Color.White;
            btnRefreshVehicles.IconChar = IconChar.SyncAlt;
            btnRefreshVehicles.IconColor = Color.White;
            btnRefreshVehicles.IconFont = IconFont.Auto;
            btnRefreshVehicles.IconSize = 20;
            btnRefreshVehicles.Location = new Point(789, 20);
            btnRefreshVehicles.Name = "btnRefreshVehicles";
            btnRefreshVehicles.Size = new Size(40, 40);
            btnRefreshVehicles.TabIndex = 1;
            btnRefreshVehicles.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRefreshVehicles.UseVisualStyleBackColor = false;
            btnRefreshVehicles.Click += BtnRefreshVehicles_Click;
            // 
            // txtSearchVehicles
            // 
            txtSearchVehicles.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSearchVehicles.Font = new Font("Segoe UI", 12F);
            txtSearchVehicles.Location = new Point(475, 26);
            txtSearchVehicles.Name = "txtSearchVehicles";
            txtSearchVehicles.PlaceholderText = "Arama...";
            txtSearchVehicles.Size = new Size(300, 29);
            txtSearchVehicles.TabIndex = 0;
            txtSearchVehicles.TextChanged += TxtSearchVehicles_TextChanged;
            // 
            // VehiclesControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlVehicles);
            Name = "VehiclesControl";
            Size = new Size(1010, 700);
            pnlVehicles.ResumeLayout(false);
            pnlVehiclesContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvVehicles).EndInit();
            pnlVehiclesHeader.ResumeLayout(false);
            pnlVehiclesHeader.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlVehicles;
        private System.Windows.Forms.Panel pnlVehiclesContent;
        private System.Windows.Forms.DataGridView dgvVehicles;
        private System.Windows.Forms.Panel pnlVehiclesHeader;
        private System.Windows.Forms.Label lblVehiclesTitle;
        private IconButton btnAddVehicle;
        private IconButton btnRefreshVehicles;
        private System.Windows.Forms.TextBox txtSearchVehicles;
    }
}