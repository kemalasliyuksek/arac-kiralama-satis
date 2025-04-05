using FontAwesome.Sharp;

namespace arac_kiralama_satis_desktop.Controls
{
    partial class StaffControl
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
            pnlStaff = new Panel();
            pnlStaffContent = new Panel();
            dgvStaff = new DataGridView();
            pnlStaffHeader = new Panel();
            lblStaffTitle = new Label();
            btnAddStaff = new IconButton();
            btnRefreshStaff = new IconButton();
            txtSearchStaff = new TextBox();
            pnlStaff.SuspendLayout();
            pnlStaffContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStaff).BeginInit();
            pnlStaffHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlStaff
            // 
            pnlStaff.Controls.Add(pnlStaffContent);
            pnlStaff.Controls.Add(pnlStaffHeader);
            pnlStaff.Dock = DockStyle.Fill;
            pnlStaff.Location = new Point(0, 0);
            pnlStaff.Name = "pnlStaff";
            pnlStaff.Size = new Size(1010, 700);
            pnlStaff.TabIndex = 0;
            // 
            // pnlStaffContent
            // 
            pnlStaffContent.Controls.Add(dgvStaff);
            pnlStaffContent.Dock = DockStyle.Fill;
            pnlStaffContent.Location = new Point(0, 80);
            pnlStaffContent.Name = "pnlStaffContent";
            pnlStaffContent.Padding = new Padding(10);
            pnlStaffContent.Size = new Size(1010, 620);
            pnlStaffContent.TabIndex = 1;
            // 
            // dgvStaff
            // 
            dgvStaff.AllowUserToAddRows = false;
            dgvStaff.AllowUserToDeleteRows = false;
            dgvStaff.BackgroundColor = Color.White;
            dgvStaff.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStaff.Dock = DockStyle.Fill;
            dgvStaff.Location = new Point(10, 10);
            dgvStaff.Name = "dgvStaff";
            dgvStaff.ReadOnly = true;
            dgvStaff.Size = new Size(990, 600);
            dgvStaff.TabIndex = 0;
            // 
            // pnlStaffHeader
            // 
            pnlStaffHeader.BackColor = Color.White;
            pnlStaffHeader.Controls.Add(lblStaffTitle);
            pnlStaffHeader.Controls.Add(btnAddStaff);
            pnlStaffHeader.Controls.Add(btnRefreshStaff);
            pnlStaffHeader.Controls.Add(txtSearchStaff);
            pnlStaffHeader.Dock = DockStyle.Top;
            pnlStaffHeader.Location = new Point(0, 0);
            pnlStaffHeader.Name = "pnlStaffHeader";
            pnlStaffHeader.Size = new Size(1010, 80);
            pnlStaffHeader.TabIndex = 0;
            // 
            // lblStaffTitle
            // 
            lblStaffTitle.AutoSize = true;
            lblStaffTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblStaffTitle.ForeColor = Color.FromArgb(49, 76, 143);
            lblStaffTitle.Location = new Point(23, 25);
            lblStaffTitle.Name = "lblStaffTitle";
            lblStaffTitle.Size = new Size(171, 30);
            lblStaffTitle.TabIndex = 3;
            lblStaffTitle.Text = "Personel Listesi";
            // 
            // btnAddStaff
            // 
            btnAddStaff.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddStaff.BackColor = Color.FromArgb(40, 167, 69);
            btnAddStaff.FlatAppearance.BorderSize = 0;
            btnAddStaff.FlatStyle = FlatStyle.Flat;
            btnAddStaff.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnAddStaff.ForeColor = Color.White;
            btnAddStaff.IconChar = IconChar.Add;
            btnAddStaff.IconColor = Color.White;
            btnAddStaff.IconFont = IconFont.Auto;
            btnAddStaff.IconSize = 20;
            btnAddStaff.Location = new Point(870, 20);
            btnAddStaff.Name = "btnAddStaff";
            btnAddStaff.Size = new Size(130, 40);
            btnAddStaff.TabIndex = 2;
            btnAddStaff.Text = "Yeni Personel";
            btnAddStaff.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAddStaff.UseVisualStyleBackColor = false;
            btnAddStaff.Click += BtnAddStaff_Click;
            // 
            // btnRefreshStaff
            // 
            btnRefreshStaff.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefreshStaff.BackColor = Color.FromArgb(108, 117, 125);
            btnRefreshStaff.FlatAppearance.BorderSize = 0;
            btnRefreshStaff.FlatStyle = FlatStyle.Flat;
            btnRefreshStaff.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnRefreshStaff.ForeColor = Color.White;
            btnRefreshStaff.IconChar = IconChar.SyncAlt;
            btnRefreshStaff.IconColor = Color.White;
            btnRefreshStaff.IconFont = IconFont.Auto;
            btnRefreshStaff.IconSize = 20;
            btnRefreshStaff.Location = new Point(789, 20);
            btnRefreshStaff.Name = "btnRefreshStaff";
            btnRefreshStaff.Size = new Size(40, 40);
            btnRefreshStaff.TabIndex = 1;
            btnRefreshStaff.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRefreshStaff.UseVisualStyleBackColor = false;
            btnRefreshStaff.Click += BtnRefreshStaff_Click;
            // 
            // txtSearchStaff
            // 
            txtSearchStaff.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSearchStaff.Font = new Font("Segoe UI", 12F);
            txtSearchStaff.Location = new Point(475, 26);
            txtSearchStaff.Name = "txtSearchStaff";
            txtSearchStaff.PlaceholderText = "Arama...";
            txtSearchStaff.Size = new Size(300, 29);
            txtSearchStaff.TabIndex = 0;
            txtSearchStaff.TextChanged += TxtSearchStaff_TextChanged;
            // 
            // StaffControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlStaff);
            Name = "StaffControl";
            Size = new Size(1010, 700);
            pnlStaff.ResumeLayout(false);
            pnlStaffContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvStaff).EndInit();
            pnlStaffHeader.ResumeLayout(false);
            pnlStaffHeader.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlStaff;
        private System.Windows.Forms.Panel pnlStaffContent;
        private System.Windows.Forms.DataGridView dgvStaff;
        private System.Windows.Forms.Panel pnlStaffHeader;
        private System.Windows.Forms.Label lblStaffTitle;
        private IconButton btnAddStaff;
        private IconButton btnRefreshStaff;
        private System.Windows.Forms.TextBox txtSearchStaff;
    }
}