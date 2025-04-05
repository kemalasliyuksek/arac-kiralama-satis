using FontAwesome.Sharp;

namespace arac_kiralama_satis_desktop.Controls
{
    partial class CustomersControl
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
            pnlCustomers = new Panel();
            pnlCustomersContent = new Panel();
            dgvCustomers = new DataGridView();
            pnlCustomersHeader = new Panel();
            lblCustomersTitle = new Label();
            btnAddCustomer = new IconButton();
            btnRefreshCustomers = new IconButton();
            txtSearchCustomers = new TextBox();
            pnlCustomers.SuspendLayout();
            pnlCustomersContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
            pnlCustomersHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlCustomers
            // 
            pnlCustomers.Controls.Add(pnlCustomersContent);
            pnlCustomers.Controls.Add(pnlCustomersHeader);
            pnlCustomers.Dock = DockStyle.Fill;
            pnlCustomers.Location = new Point(0, 0);
            pnlCustomers.Name = "pnlCustomers";
            pnlCustomers.Size = new Size(1010, 700);
            pnlCustomers.TabIndex = 0;
            // 
            // pnlCustomersContent
            // 
            pnlCustomersContent.Controls.Add(dgvCustomers);
            pnlCustomersContent.Dock = DockStyle.Fill;
            pnlCustomersContent.Location = new Point(0, 80);
            pnlCustomersContent.Name = "pnlCustomersContent";
            pnlCustomersContent.Padding = new Padding(10);
            pnlCustomersContent.Size = new Size(1010, 620);
            pnlCustomersContent.TabIndex = 1;
            // 
            // dgvCustomers
            // 
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToDeleteRows = false;
            dgvCustomers.BackgroundColor = Color.White;
            dgvCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCustomers.Dock = DockStyle.Fill;
            dgvCustomers.Location = new Point(10, 10);
            dgvCustomers.Name = "dgvCustomers";
            dgvCustomers.ReadOnly = true;
            dgvCustomers.Size = new Size(990, 600);
            dgvCustomers.TabIndex = 0;
            // 
            // pnlCustomersHeader
            // 
            pnlCustomersHeader.BackColor = Color.White;
            pnlCustomersHeader.Controls.Add(lblCustomersTitle);
            pnlCustomersHeader.Controls.Add(btnAddCustomer);
            pnlCustomersHeader.Controls.Add(btnRefreshCustomers);
            pnlCustomersHeader.Controls.Add(txtSearchCustomers);
            pnlCustomersHeader.Dock = DockStyle.Top;
            pnlCustomersHeader.Location = new Point(0, 0);
            pnlCustomersHeader.Name = "pnlCustomersHeader";
            pnlCustomersHeader.Size = new Size(1010, 80);
            pnlCustomersHeader.TabIndex = 0;
            // 
            // lblCustomersTitle
            // 
            lblCustomersTitle.AutoSize = true;
            lblCustomersTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblCustomersTitle.ForeColor = Color.FromArgb(49, 76, 143);
            lblCustomersTitle.Location = new Point(23, 25);
            lblCustomersTitle.Name = "lblCustomersTitle";
            lblCustomersTitle.Size = new Size(163, 30);
            lblCustomersTitle.TabIndex = 3;
            lblCustomersTitle.Text = "Müşteri Listesi";
            // 
            // btnAddCustomer
            // 
            btnAddCustomer.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddCustomer.BackColor = Color.FromArgb(40, 167, 69);
            btnAddCustomer.FlatAppearance.BorderSize = 0;
            btnAddCustomer.FlatStyle = FlatStyle.Flat;
            btnAddCustomer.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnAddCustomer.ForeColor = Color.White;
            btnAddCustomer.IconChar = IconChar.Add;
            btnAddCustomer.IconColor = Color.White;
            btnAddCustomer.IconFont = IconFont.Auto;
            btnAddCustomer.IconSize = 20;
            btnAddCustomer.Location = new Point(870, 20);
            btnAddCustomer.Name = "btnAddCustomer";
            btnAddCustomer.Size = new Size(130, 40);
            btnAddCustomer.TabIndex = 2;
            btnAddCustomer.Text = "Yeni Müşteri";
            btnAddCustomer.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAddCustomer.UseVisualStyleBackColor = false;
            btnAddCustomer.Click += BtnAddCustomer_Click;
            // 
            // btnRefreshCustomers
            // 
            btnRefreshCustomers.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefreshCustomers.BackColor = Color.FromArgb(108, 117, 125);
            btnRefreshCustomers.FlatAppearance.BorderSize = 0;
            btnRefreshCustomers.FlatStyle = FlatStyle.Flat;
            btnRefreshCustomers.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnRefreshCustomers.ForeColor = Color.White;
            btnRefreshCustomers.IconChar = IconChar.SyncAlt;
            btnRefreshCustomers.IconColor = Color.White;
            btnRefreshCustomers.IconFont = IconFont.Auto;
            btnRefreshCustomers.IconSize = 20;
            btnRefreshCustomers.Location = new Point(790, 20);
            btnRefreshCustomers.Name = "btnRefreshCustomers";
            btnRefreshCustomers.Size = new Size(40, 40);
            btnRefreshCustomers.TabIndex = 1;
            btnRefreshCustomers.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRefreshCustomers.UseVisualStyleBackColor = false;
            btnRefreshCustomers.Click += BtnRefreshCustomers_Click;
            // 
            // txtSearchCustomers
            // 
            txtSearchCustomers.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSearchCustomers.Font = new Font("Segoe UI", 12F);
            txtSearchCustomers.Location = new Point(475, 26);
            txtSearchCustomers.Name = "txtSearchCustomers";
            txtSearchCustomers.PlaceholderText = "Arama...";
            txtSearchCustomers.Size = new Size(300, 29);
            txtSearchCustomers.TabIndex = 0;
            txtSearchCustomers.TextChanged += TxtSearchCustomers_TextChanged;
            // 
            // CustomersControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlCustomers);
            Name = "CustomersControl";
            Size = new Size(1010, 700);
            pnlCustomers.ResumeLayout(false);
            pnlCustomersContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            pnlCustomersHeader.ResumeLayout(false);
            pnlCustomersHeader.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlCustomers;
        private System.Windows.Forms.Panel pnlCustomersContent;
        private System.Windows.Forms.DataGridView dgvCustomers;
        private System.Windows.Forms.Panel pnlCustomersHeader;
        private System.Windows.Forms.Label lblCustomersTitle;
        private IconButton btnAddCustomer;
        private IconButton btnRefreshCustomers;
        private System.Windows.Forms.TextBox txtSearchCustomers;
    }
}