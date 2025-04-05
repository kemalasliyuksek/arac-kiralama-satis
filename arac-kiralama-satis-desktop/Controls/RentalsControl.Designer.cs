using FontAwesome.Sharp;

namespace arac_kiralama_satis_desktop.Controls
{
    partial class RentalsControl
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
            pnlRentals = new Panel();
            pnlRentalsContent = new Panel();
            dgvRentals = new DataGridView();
            pnlRentalsHeader = new Panel();
            lblRentalsTitle = new Label();
            btnAddRental = new IconButton();
            btnRefreshRentals = new IconButton();
            txtSearchRentals = new TextBox();
            pnlRentals.SuspendLayout();
            pnlRentalsContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRentals).BeginInit();
            pnlRentalsHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlRentals
            // 
            pnlRentals.Controls.Add(pnlRentalsContent);
            pnlRentals.Controls.Add(pnlRentalsHeader);
            pnlRentals.Dock = DockStyle.Fill;
            pnlRentals.Location = new Point(0, 0);
            pnlRentals.Name = "pnlRentals";
            pnlRentals.Size = new Size(1010, 700);
            pnlRentals.TabIndex = 0;
            // 
            // pnlRentalsContent
            // 
            pnlRentalsContent.Controls.Add(dgvRentals);
            pnlRentalsContent.Dock = DockStyle.Fill;
            pnlRentalsContent.Location = new Point(0, 80);
            pnlRentalsContent.Name = "pnlRentalsContent";
            pnlRentalsContent.Padding = new Padding(10);
            pnlRentalsContent.Size = new Size(1010, 620);
            pnlRentalsContent.TabIndex = 1;
            // 
            // dgvRentals
            // 
            dgvRentals.AllowUserToAddRows = false;
            dgvRentals.AllowUserToDeleteRows = false;
            dgvRentals.BackgroundColor = Color.White;
            dgvRentals.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRentals.Dock = DockStyle.Fill;
            dgvRentals.Location = new Point(10, 10);
            dgvRentals.Name = "dgvRentals";
            dgvRentals.ReadOnly = true;
            dgvRentals.Size = new Size(990, 600);
            dgvRentals.TabIndex = 0;
            // 
            // pnlRentalsHeader
            // 
            pnlRentalsHeader.BackColor = Color.White;
            pnlRentalsHeader.Controls.Add(lblRentalsTitle);
            pnlRentalsHeader.Controls.Add(btnAddRental);
            pnlRentalsHeader.Controls.Add(btnRefreshRentals);
            pnlRentalsHeader.Controls.Add(txtSearchRentals);
            pnlRentalsHeader.Dock = DockStyle.Top;
            pnlRentalsHeader.Location = new Point(0, 0);
            pnlRentalsHeader.Name = "pnlRentalsHeader";
            pnlRentalsHeader.Size = new Size(1010, 80);
            pnlRentalsHeader.TabIndex = 0;
            // 
            // lblRentalsTitle
            // 
            lblRentalsTitle.AutoSize = true;
            lblRentalsTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblRentalsTitle.ForeColor = Color.FromArgb(49, 76, 143);
            lblRentalsTitle.Location = new Point(23, 25);
            lblRentalsTitle.Name = "lblRentalsTitle";
            lblRentalsTitle.Size = new Size(174, 30);
            lblRentalsTitle.TabIndex = 3;
            lblRentalsTitle.Text = "Kiralama Listesi";
            // 
            // btnAddRental
            // 
            btnAddRental.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddRental.BackColor = Color.FromArgb(40, 167, 69);
            btnAddRental.FlatAppearance.BorderSize = 0;
            btnAddRental.FlatStyle = FlatStyle.Flat;
            btnAddRental.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnAddRental.ForeColor = Color.White;
            btnAddRental.IconChar = IconChar.Add;
            btnAddRental.IconColor = Color.White;
            btnAddRental.IconFont = IconFont.Auto;
            btnAddRental.IconSize = 20;
            btnAddRental.Location = new Point(870, 20);
            btnAddRental.Name = "btnAddRental";
            btnAddRental.Size = new Size(130, 40);
            btnAddRental.TabIndex = 2;
            btnAddRental.Text = "Yeni Kiralama";
            btnAddRental.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAddRental.UseVisualStyleBackColor = false;
            btnAddRental.Click += BtnAddRental_Click;
            // 
            // btnRefreshRentals
            // 
            btnRefreshRentals.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefreshRentals.BackColor = Color.FromArgb(108, 117, 125);
            btnRefreshRentals.FlatAppearance.BorderSize = 0;
            btnRefreshRentals.FlatStyle = FlatStyle.Flat;
            btnRefreshRentals.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnRefreshRentals.ForeColor = Color.White;
            btnRefreshRentals.IconChar = IconChar.SyncAlt;
            btnRefreshRentals.IconColor = Color.White;
            btnRefreshRentals.IconFont = IconFont.Auto;
            btnRefreshRentals.IconSize = 20;
            btnRefreshRentals.Location = new Point(790, 20);
            btnRefreshRentals.Name = "btnRefreshRentals";
            btnRefreshRentals.Size = new Size(40, 40);
            btnRefreshRentals.TabIndex = 1;
            btnRefreshRentals.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRefreshRentals.UseVisualStyleBackColor = false;
            btnRefreshRentals.Click += BtnRefreshRentals_Click;
            // 
            // txtSearchRentals
            // 
            txtSearchRentals.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSearchRentals.Font = new Font("Segoe UI", 12F);
            txtSearchRentals.Location = new Point(475, 26);
            txtSearchRentals.Name = "txtSearchRentals";
            txtSearchRentals.PlaceholderText = "Arama...";
            txtSearchRentals.Size = new Size(300, 29);
            txtSearchRentals.TabIndex = 0;
            txtSearchRentals.TextChanged += TxtSearchRentals_TextChanged;
            // 
            // RentalsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlRentals);
            Name = "RentalsControl";
            Size = new Size(1010, 700);
            pnlRentals.ResumeLayout(false);
            pnlRentalsContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvRentals).EndInit();
            pnlRentalsHeader.ResumeLayout(false);
            pnlRentalsHeader.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlRentals;
        private System.Windows.Forms.Panel pnlRentalsContent;
        private System.Windows.Forms.DataGridView dgvRentals;
        private System.Windows.Forms.Panel pnlRentalsHeader;
        private System.Windows.Forms.Label lblRentalsTitle;
        private IconButton btnAddRental;
        private IconButton btnRefreshRentals;
        private System.Windows.Forms.TextBox txtSearchRentals;
    }
}