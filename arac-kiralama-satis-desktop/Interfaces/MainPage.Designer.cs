using FontAwesome.Sharp;
using System.Drawing;
using System.Windows.Forms;

namespace arac_kiralama_satis_desktop.Interfaces
{
    partial class MainPage
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
            pnlSidebar = new Panel();
            pnlUserInfo = new Panel();
            lblUserRole = new Label();
            lblUserName = new Label();
            picUserAvatar = new PictureBox();
            pnlMenu = new Panel();
            btnSettings = new IconButton();
            btnReports = new IconButton();
            btnMaintenance = new IconButton();
            btnSales = new IconButton();
            btnRentals = new IconButton();
            btnStaff = new IconButton();
            btnBranches = new IconButton();
            btnCustomers = new IconButton();
            btnVehicles = new IconButton();
            btnDashboard = new IconButton();
            pnlLogo = new Panel();
            lblAppName = new Label();
            pnlTopbar = new Panel();
            lblBranchName = new Label();
            lblPageTitle = new Label();
            btnLogout = new IconButton();
            btnRefresh = new IconButton();
            pnlContent = new Panel();
            pnlSidebar.SuspendLayout();
            pnlUserInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picUserAvatar).BeginInit();
            pnlMenu.SuspendLayout();
            pnlLogo.SuspendLayout();
            pnlTopbar.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.FromArgb(49, 76, 143);
            pnlSidebar.Controls.Add(pnlUserInfo);
            pnlSidebar.Controls.Add(pnlMenu);
            pnlSidebar.Controls.Add(pnlLogo);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(250, 800);
            pnlSidebar.TabIndex = 0;
            // 
            // pnlUserInfo
            // 
            pnlUserInfo.Controls.Add(lblUserRole);
            pnlUserInfo.Controls.Add(lblUserName);
            pnlUserInfo.Controls.Add(picUserAvatar);
            pnlUserInfo.Dock = DockStyle.Bottom;
            pnlUserInfo.Location = new Point(0, 730);
            pnlUserInfo.Name = "pnlUserInfo";
            pnlUserInfo.Size = new Size(250, 70);
            pnlUserInfo.TabIndex = 2;
            // 
            // lblUserRole
            // 
            lblUserRole.AutoSize = true;
            lblUserRole.Font = new Font("Segoe UI", 9F);
            lblUserRole.ForeColor = Color.LightGray;
            lblUserRole.Location = new Point(70, 36);
            lblUserRole.Name = "lblUserRole";
            lblUserRole.Size = new Size(52, 15);
            lblUserRole.TabIndex = 2;
            lblUserRole.Text = "Kullanıcı";
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblUserName.ForeColor = Color.White;
            lblUserName.Location = new Point(70, 16);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(117, 19);
            lblUserName.TabIndex = 1;
            lblUserName.Text = "Kemal Aslıyüksek";
            // 
            // picUserAvatar
            // 
            picUserAvatar.Location = new Point(20, 16);
            picUserAvatar.Name = "picUserAvatar";
            picUserAvatar.Size = new Size(40, 40);
            picUserAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            picUserAvatar.TabIndex = 0;
            picUserAvatar.TabStop = false;
            // 
            // pnlMenu
            // 
            pnlMenu.Controls.Add(btnSettings);
            pnlMenu.Controls.Add(btnReports);
            pnlMenu.Controls.Add(btnMaintenance);
            pnlMenu.Controls.Add(btnSales);
            pnlMenu.Controls.Add(btnRentals);
            pnlMenu.Controls.Add(btnStaff);
            pnlMenu.Controls.Add(btnBranches);
            pnlMenu.Controls.Add(btnCustomers);
            pnlMenu.Controls.Add(btnVehicles);
            pnlMenu.Controls.Add(btnDashboard);
            pnlMenu.Dock = DockStyle.Fill;
            pnlMenu.Location = new Point(0, 80);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Size = new Size(250, 720);
            pnlMenu.TabIndex = 1;
            // 
            // btnSettings
            // 
            btnSettings.Cursor = Cursors.Hand;
            btnSettings.Dock = DockStyle.Top;
            btnSettings.FlatAppearance.BorderSize = 0;
            btnSettings.FlatStyle = FlatStyle.Flat;
            btnSettings.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnSettings.ForeColor = Color.White;
            btnSettings.IconChar = IconChar.Cog;
            btnSettings.IconColor = Color.White;
            btnSettings.IconFont = IconFont.Auto;
            btnSettings.IconSize = 24;
            btnSettings.ImageAlign = ContentAlignment.MiddleLeft;
            btnSettings.Location = new Point(0, 450);
            btnSettings.Name = "btnSettings";
            btnSettings.Padding = new Padding(15, 0, 0, 0);
            btnSettings.Size = new Size(250, 50);
            btnSettings.TabIndex = 9;
            btnSettings.Text = "  Ayarlar";
            btnSettings.TextAlign = ContentAlignment.MiddleLeft;
            btnSettings.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += BtnSettings_Click;
            // 
            // btnReports
            // 
            btnReports.Cursor = Cursors.Hand;
            btnReports.Dock = DockStyle.Top;
            btnReports.FlatAppearance.BorderSize = 0;
            btnReports.FlatStyle = FlatStyle.Flat;
            btnReports.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnReports.ForeColor = Color.White;
            btnReports.IconChar = IconChar.ChartColumn;
            btnReports.IconColor = Color.White;
            btnReports.IconFont = IconFont.Auto;
            btnReports.IconSize = 24;
            btnReports.ImageAlign = ContentAlignment.MiddleLeft;
            btnReports.Location = new Point(0, 400);
            btnReports.Name = "btnReports";
            btnReports.Padding = new Padding(15, 0, 0, 0);
            btnReports.Size = new Size(250, 50);
            btnReports.TabIndex = 8;
            btnReports.Text = "  Raporlar";
            btnReports.TextAlign = ContentAlignment.MiddleLeft;
            btnReports.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnReports.UseVisualStyleBackColor = true;
            btnReports.Click += BtnReports_Click;
            // 
            // btnMaintenance
            // 
            btnMaintenance.Cursor = Cursors.Hand;
            btnMaintenance.Dock = DockStyle.Top;
            btnMaintenance.FlatAppearance.BorderSize = 0;
            btnMaintenance.FlatStyle = FlatStyle.Flat;
            btnMaintenance.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnMaintenance.ForeColor = Color.White;
            btnMaintenance.IconChar = IconChar.Wrench;
            btnMaintenance.IconColor = Color.White;
            btnMaintenance.IconFont = IconFont.Auto;
            btnMaintenance.IconSize = 24;
            btnMaintenance.ImageAlign = ContentAlignment.MiddleLeft;
            btnMaintenance.Location = new Point(0, 350);
            btnMaintenance.Name = "btnMaintenance";
            btnMaintenance.Padding = new Padding(15, 0, 0, 0);
            btnMaintenance.Size = new Size(250, 50);
            btnMaintenance.TabIndex = 7;
            btnMaintenance.Text = "  Bakım ve Servis";
            btnMaintenance.TextAlign = ContentAlignment.MiddleLeft;
            btnMaintenance.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnMaintenance.UseVisualStyleBackColor = true;
            btnMaintenance.Click += BtnMaintenance_Click;
            // 
            // btnSales
            // 
            btnSales.Cursor = Cursors.Hand;
            btnSales.Dock = DockStyle.Top;
            btnSales.FlatAppearance.BorderSize = 0;
            btnSales.FlatStyle = FlatStyle.Flat;
            btnSales.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnSales.ForeColor = Color.White;
            btnSales.IconChar = IconChar.Tag;
            btnSales.IconColor = Color.White;
            btnSales.IconFont = IconFont.Auto;
            btnSales.IconSize = 24;
            btnSales.ImageAlign = ContentAlignment.MiddleLeft;
            btnSales.Location = new Point(0, 300);
            btnSales.Name = "btnSales";
            btnSales.Padding = new Padding(15, 0, 0, 0);
            btnSales.Size = new Size(250, 50);
            btnSales.TabIndex = 6;
            btnSales.Text = "  Satışlar";
            btnSales.TextAlign = ContentAlignment.MiddleLeft;
            btnSales.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSales.UseVisualStyleBackColor = true;
            btnSales.Click += BtnSales_Click;
            // 
            // btnRentals
            // 
            btnRentals.Cursor = Cursors.Hand;
            btnRentals.Dock = DockStyle.Top;
            btnRentals.FlatAppearance.BorderSize = 0;
            btnRentals.FlatStyle = FlatStyle.Flat;
            btnRentals.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnRentals.ForeColor = Color.White;
            btnRentals.IconChar = IconChar.CalendarCheck;
            btnRentals.IconColor = Color.White;
            btnRentals.IconFont = IconFont.Auto;
            btnRentals.IconSize = 24;
            btnRentals.ImageAlign = ContentAlignment.MiddleLeft;
            btnRentals.Location = new Point(0, 250);
            btnRentals.Name = "btnRentals";
            btnRentals.Padding = new Padding(15, 0, 0, 0);
            btnRentals.Size = new Size(250, 50);
            btnRentals.TabIndex = 5;
            btnRentals.Text = "  Kiralamalar";
            btnRentals.TextAlign = ContentAlignment.MiddleLeft;
            btnRentals.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRentals.UseVisualStyleBackColor = true;
            btnRentals.Click += BtnRentals_Click;
            // 
            // btnStaff
            // 
            btnStaff.Cursor = Cursors.Hand;
            btnStaff.Dock = DockStyle.Top;
            btnStaff.FlatAppearance.BorderSize = 0;
            btnStaff.FlatStyle = FlatStyle.Flat;
            btnStaff.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnStaff.ForeColor = Color.White;
            btnStaff.IconChar = IconChar.UserTie;
            btnStaff.IconColor = Color.White;
            btnStaff.IconFont = IconFont.Auto;
            btnStaff.IconSize = 24;
            btnStaff.ImageAlign = ContentAlignment.MiddleLeft;
            btnStaff.Location = new Point(0, 200);
            btnStaff.Name = "btnStaff";
            btnStaff.Padding = new Padding(15, 0, 0, 0);
            btnStaff.Size = new Size(250, 50);
            btnStaff.TabIndex = 4;
            btnStaff.Text = "  Personeller";
            btnStaff.TextAlign = ContentAlignment.MiddleLeft;
            btnStaff.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnStaff.UseVisualStyleBackColor = true;
            btnStaff.Click += BtnStaff_Click;
            // 
            // btnBranches
            // 
            btnBranches.Cursor = Cursors.Hand;
            btnBranches.Dock = DockStyle.Top;
            btnBranches.FlatAppearance.BorderSize = 0;
            btnBranches.FlatStyle = FlatStyle.Flat;
            btnBranches.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnBranches.ForeColor = Color.White;
            btnBranches.IconChar = IconChar.Building;
            btnBranches.IconColor = Color.White;
            btnBranches.IconFont = IconFont.Auto;
            btnBranches.IconSize = 24;
            btnBranches.ImageAlign = ContentAlignment.MiddleLeft;
            btnBranches.Location = new Point(0, 150);
            btnBranches.Name = "btnBranches";
            btnBranches.Padding = new Padding(15, 0, 0, 0);
            btnBranches.Size = new Size(250, 50);
            btnBranches.TabIndex = 3;
            btnBranches.Text = "  Şubeler";
            btnBranches.TextAlign = ContentAlignment.MiddleLeft;
            btnBranches.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnBranches.UseVisualStyleBackColor = true;
            btnBranches.Click += BtnBranches_Click;
            // 
            // btnCustomers
            // 
            btnCustomers.Cursor = Cursors.Hand;
            btnCustomers.Dock = DockStyle.Top;
            btnCustomers.FlatAppearance.BorderSize = 0;
            btnCustomers.FlatStyle = FlatStyle.Flat;
            btnCustomers.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnCustomers.ForeColor = Color.White;
            btnCustomers.IconChar = IconChar.Users;
            btnCustomers.IconColor = Color.White;
            btnCustomers.IconFont = IconFont.Auto;
            btnCustomers.IconSize = 24;
            btnCustomers.ImageAlign = ContentAlignment.MiddleLeft;
            btnCustomers.Location = new Point(0, 100);
            btnCustomers.Name = "btnCustomers";
            btnCustomers.Padding = new Padding(15, 0, 0, 0);
            btnCustomers.Size = new Size(250, 50);
            btnCustomers.TabIndex = 2;
            btnCustomers.Text = "  Müşteriler";
            btnCustomers.TextAlign = ContentAlignment.MiddleLeft;
            btnCustomers.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCustomers.UseVisualStyleBackColor = true;
            btnCustomers.Click += BtnCustomers_Click;
            // 
            // btnVehicles
            // 
            btnVehicles.Cursor = Cursors.Hand;
            btnVehicles.Dock = DockStyle.Top;
            btnVehicles.FlatAppearance.BorderSize = 0;
            btnVehicles.FlatStyle = FlatStyle.Flat;
            btnVehicles.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnVehicles.ForeColor = Color.White;
            btnVehicles.IconChar = IconChar.Car;
            btnVehicles.IconColor = Color.White;
            btnVehicles.IconFont = IconFont.Auto;
            btnVehicles.IconSize = 24;
            btnVehicles.ImageAlign = ContentAlignment.MiddleLeft;
            btnVehicles.Location = new Point(0, 50);
            btnVehicles.Name = "btnVehicles";
            btnVehicles.Padding = new Padding(15, 0, 0, 0);
            btnVehicles.Size = new Size(250, 50);
            btnVehicles.TabIndex = 1;
            btnVehicles.Text = "  Araçlar";
            btnVehicles.TextAlign = ContentAlignment.MiddleLeft;
            btnVehicles.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnVehicles.UseVisualStyleBackColor = true;
            btnVehicles.Click += BtnVehicles_Click;
            // 
            // btnDashboard
            // 
            btnDashboard.BackColor = Color.FromArgb(83, 107, 168);
            btnDashboard.Cursor = Cursors.Hand;
            btnDashboard.Dock = DockStyle.Top;
            btnDashboard.FlatAppearance.BorderSize = 0;
            btnDashboard.FlatStyle = FlatStyle.Flat;
            btnDashboard.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnDashboard.ForeColor = Color.White;
            btnDashboard.IconChar = IconChar.ChartLine;
            btnDashboard.IconColor = Color.White;
            btnDashboard.IconFont = IconFont.Auto;
            btnDashboard.IconSize = 24;
            btnDashboard.ImageAlign = ContentAlignment.MiddleLeft;
            btnDashboard.Location = new Point(0, 0);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Padding = new Padding(15, 0, 0, 0);
            btnDashboard.Size = new Size(250, 50);
            btnDashboard.TabIndex = 0;
            btnDashboard.Text = "  Dashboard";
            btnDashboard.TextAlign = ContentAlignment.MiddleLeft;
            btnDashboard.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDashboard.UseVisualStyleBackColor = false;
            btnDashboard.Click += BtnDashboard_Click;
            // 
            // pnlLogo
            // 
            pnlLogo.Controls.Add(lblAppName);
            pnlLogo.Dock = DockStyle.Top;
            pnlLogo.Location = new Point(0, 0);
            pnlLogo.Name = "pnlLogo";
            pnlLogo.Size = new Size(250, 80);
            pnlLogo.TabIndex = 0;
            // 
            // lblAppName
            // 
            lblAppName.AutoSize = true;
            lblAppName.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblAppName.ForeColor = Color.White;
            lblAppName.Location = new Point(23, 28);
            lblAppName.Name = "lblAppName";
            lblAppName.Size = new Size(205, 25);
            lblAppName.TabIndex = 1;
            lblAppName.Text = "Araç Kiralama ve Satış";
            // 
            // pnlTopbar
            // 
            pnlTopbar.BackColor = Color.White;
            pnlTopbar.BorderStyle = BorderStyle.FixedSingle;
            pnlTopbar.Controls.Add(lblBranchName);
            pnlTopbar.Controls.Add(lblPageTitle);
            pnlTopbar.Controls.Add(btnLogout);
            pnlTopbar.Controls.Add(btnRefresh);
            pnlTopbar.Dock = DockStyle.Top;
            pnlTopbar.Location = new Point(250, 0);
            pnlTopbar.Name = "pnlTopbar";
            pnlTopbar.Size = new Size(1050, 60);
            pnlTopbar.TabIndex = 1;
            // 
            // lblBranchName
            // 
            lblBranchName.AutoSize = true;
            lblBranchName.Font = new Font("Segoe UI", 9F);
            lblBranchName.ForeColor = Color.FromArgb(120, 120, 120);
            lblBranchName.Location = new Point(26, 31);
            lblBranchName.Name = "lblBranchName";
            lblBranchName.Size = new Size(106, 15);
            lblBranchName.TabIndex = 2;
            lblBranchName.Text = "Bursa Merkez Şube";
            // 
            // lblPageTitle
            // 
            lblPageTitle.AutoSize = true;
            lblPageTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblPageTitle.ForeColor = Color.FromArgb(49, 76, 143);
            lblPageTitle.Location = new Point(20, 6);
            lblPageTitle.Name = "lblPageTitle";
            lblPageTitle.Size = new Size(109, 25);
            lblPageTitle.TabIndex = 1;
            lblPageTitle.Text = "Dashboard";
            // 
            // btnLogout
            // 
            btnLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnLogout.ForeColor = Color.FromArgb(49, 76, 143);
            btnLogout.IconChar = IconChar.SignOutAlt;
            btnLogout.IconColor = Color.FromArgb(49, 76, 143);
            btnLogout.IconFont = IconFont.Auto;
            btnLogout.IconSize = 24;
            btnLogout.Location = new Point(983, 10);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(40, 40);
            btnLogout.TabIndex = 0;
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += BtnLogout_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.FromArgb(49, 76, 143);
            btnRefresh.IconChar = IconChar.SyncAlt;
            btnRefresh.IconColor = Color.FromArgb(49, 76, 143);
            btnRefresh.IconFont = IconFont.Auto;
            btnRefresh.IconSize = 24;
            btnRefresh.Location = new Point(938, 10);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(40, 40);
            btnRefresh.TabIndex = 1;
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += BtnRefresh_Click;
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.FromArgb(245, 245, 250);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(250, 60);
            pnlContent.Name = "pnlContent";
            pnlContent.Padding = new Padding(20);
            pnlContent.Size = new Size(1050, 740);
            pnlContent.TabIndex = 2;
            // 
            // MainPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1300, 800);
            Controls.Add(pnlContent);
            Controls.Add(pnlTopbar);
            Controls.Add(pnlSidebar);
            MinimumSize = new Size(1316, 839);
            Name = "MainPage";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Araç Kiralama ve Satış Uygulaması";
            WindowState = FormWindowState.Maximized;
            Load += MainPage_Load;
            pnlSidebar.ResumeLayout(false);
            pnlUserInfo.ResumeLayout(false);
            pnlUserInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picUserAvatar).EndInit();
            pnlMenu.ResumeLayout(false);
            pnlLogo.ResumeLayout(false);
            pnlLogo.PerformLayout();
            pnlTopbar.ResumeLayout(false);
            pnlTopbar.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlSidebar;
        private Panel pnlTopbar;
        private Panel pnlContent;
        private Panel pnlLogo;
        private Panel pnlMenu;
        private Panel pnlUserInfo;
        private Label lblAppName;
        private PictureBox picUserAvatar;
        private Label lblUserName;
        private Label lblUserRole;
        private FontAwesome.Sharp.IconButton btnDashboard;
        private FontAwesome.Sharp.IconButton btnVehicles;
        private FontAwesome.Sharp.IconButton btnCustomers;
        private FontAwesome.Sharp.IconButton btnBranches;
        private FontAwesome.Sharp.IconButton btnStaff;
        private FontAwesome.Sharp.IconButton btnRentals;
        private FontAwesome.Sharp.IconButton btnSales;
        private FontAwesome.Sharp.IconButton btnMaintenance;
        private FontAwesome.Sharp.IconButton btnReports;
        private FontAwesome.Sharp.IconButton btnSettings;
        private FontAwesome.Sharp.IconButton btnRefresh;
        private Label lblPageTitle;
        private FontAwesome.Sharp.IconButton btnLogout;
        private Label lblBranchName;
    }
}