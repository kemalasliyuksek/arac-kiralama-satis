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
            pnlContent = new Panel();
            pnlBranches = new Panel();
            pnlBranchesContent = new Panel();
            dgvBranches = new DataGridView();
            pnlBranchesHeader = new Panel();
            lblBranchesTitle = new Label();
            btnAddBranch = new IconButton();
            btnRefreshBranches = new IconButton();
            txtSearchBranches = new TextBox();
            pnlVehicles = new Panel();
            pnlVehiclesContent = new Panel();
            dgvVehicles = new DataGridView();
            pnlVehiclesHeader = new Panel();
            lblVehiclesTitle = new Label();
            btnAddVehicle = new IconButton();
            btnRefreshVehicles = new IconButton();
            txtSearchVehicles = new TextBox();
            pnlCustomers = new Panel();
            pnlCustomersContent = new Panel();
            dgvCustomers = new DataGridView();
            pnlCustomersHeader = new Panel();
            lblCustomersTitle = new Label();
            btnAddCustomer = new IconButton();
            btnRefreshCustomers = new IconButton();
            txtSearchCustomers = new TextBox();
            pnlDashboard = new Panel();
            pnlCharts = new Panel();
            pnlSidebar.SuspendLayout();
            pnlUserInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picUserAvatar).BeginInit();
            pnlMenu.SuspendLayout();
            pnlLogo.SuspendLayout();
            pnlTopbar.SuspendLayout();
            pnlContent.SuspendLayout();
            pnlBranches.SuspendLayout();
            pnlBranchesContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBranches).BeginInit();
            pnlBranchesHeader.SuspendLayout();
            pnlVehicles.SuspendLayout();
            pnlVehiclesContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVehicles).BeginInit();
            pnlVehiclesHeader.SuspendLayout();
            pnlCustomers.SuspendLayout();
            pnlCustomersContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
            pnlCustomersHeader.SuspendLayout();
            pnlDashboard.SuspendLayout();
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
            btnSettings.Location = new Point(0, 400);
            btnSettings.Name = "btnSettings";
            btnSettings.Padding = new Padding(15, 0, 0, 0);
            btnSettings.Size = new Size(250, 50);
            btnSettings.TabIndex = 8;
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
            btnReports.Location = new Point(0, 350);
            btnReports.Name = "btnReports";
            btnReports.Padding = new Padding(15, 0, 0, 0);
            btnReports.Size = new Size(250, 50);
            btnReports.TabIndex = 7;
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
            btnMaintenance.Location = new Point(0, 300);
            btnMaintenance.Name = "btnMaintenance";
            btnMaintenance.Padding = new Padding(15, 0, 0, 0);
            btnMaintenance.Size = new Size(250, 50);
            btnMaintenance.TabIndex = 6;
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
            btnSales.Location = new Point(0, 250);
            btnSales.Name = "btnSales";
            btnSales.Padding = new Padding(15, 0, 0, 0);
            btnSales.Size = new Size(250, 50);
            btnSales.TabIndex = 5;
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
            btnRentals.Location = new Point(0, 200);
            btnRentals.Name = "btnRentals";
            btnRentals.Padding = new Padding(15, 0, 0, 0);
            btnRentals.Size = new Size(250, 50);
            btnRentals.TabIndex = 4;
            btnRentals.Text = "  Kiralamalar";
            btnRentals.TextAlign = ContentAlignment.MiddleLeft;
            btnRentals.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRentals.UseVisualStyleBackColor = true;
            btnRentals.Click += BtnRentals_Click;
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
            // pnlContent
            // 
            pnlContent.BackColor = Color.FromArgb(245, 245, 250);
            pnlContent.Controls.Add(pnlDashboard);
            pnlContent.Controls.Add(pnlBranches);
            pnlContent.Controls.Add(pnlVehicles);
            pnlContent.Controls.Add(pnlCustomers);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(250, 60);
            pnlContent.Name = "pnlContent";
            pnlContent.Padding = new Padding(20);
            pnlContent.Size = new Size(1050, 740);
            pnlContent.TabIndex = 2;
            // 
            // pnlBranches
            // 
            pnlBranches.Controls.Add(pnlBranchesContent);
            pnlBranches.Controls.Add(pnlBranchesHeader);
            pnlBranches.Dock = DockStyle.Fill;
            pnlBranches.Location = new Point(20, 20);
            pnlBranches.Name = "pnlBranches";
            pnlBranches.Size = new Size(1010, 700);
            pnlBranches.TabIndex = 3;
            pnlBranches.Visible = false;
            // 
            // pnlBranchesContent
            // 
            pnlBranchesContent.Controls.Add(dgvBranches);
            pnlBranchesContent.Dock = DockStyle.Fill;
            pnlBranchesContent.Location = new Point(0, 80);
            pnlBranchesContent.Name = "pnlBranchesContent";
            pnlBranchesContent.Padding = new Padding(10);
            pnlBranchesContent.Size = new Size(1010, 620);
            pnlBranchesContent.TabIndex = 1;
            // 
            // dgvBranches
            // 
            dgvBranches.AllowUserToAddRows = false;
            dgvBranches.AllowUserToDeleteRows = false;
            dgvBranches.BackgroundColor = Color.White;
            dgvBranches.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBranches.Dock = DockStyle.Fill;
            dgvBranches.Location = new Point(10, 10);
            dgvBranches.Name = "dgvBranches";
            dgvBranches.ReadOnly = true;
            dgvBranches.Size = new Size(990, 600);
            dgvBranches.TabIndex = 0;
            // 
            // pnlBranchesHeader
            // 
            pnlBranchesHeader.BackColor = Color.White;
            pnlBranchesHeader.Controls.Add(lblBranchesTitle);
            pnlBranchesHeader.Controls.Add(btnAddBranch);
            pnlBranchesHeader.Controls.Add(btnRefreshBranches);
            pnlBranchesHeader.Controls.Add(txtSearchBranches);
            pnlBranchesHeader.Dock = DockStyle.Top;
            pnlBranchesHeader.Location = new Point(0, 0);
            pnlBranchesHeader.Name = "pnlBranchesHeader";
            pnlBranchesHeader.Size = new Size(1010, 80);
            pnlBranchesHeader.TabIndex = 0;
            // 
            // lblBranchesTitle
            // 
            lblBranchesTitle.AutoSize = true;
            lblBranchesTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblBranchesTitle.ForeColor = Color.FromArgb(49, 76, 143);
            lblBranchesTitle.Location = new Point(23, 25);
            lblBranchesTitle.Name = "lblBranchesTitle";
            lblBranchesTitle.Size = new Size(134, 30);
            lblBranchesTitle.TabIndex = 3;
            lblBranchesTitle.Text = "Şube Listesi";
            // 
            // btnAddBranch
            // 
            btnAddBranch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddBranch.BackColor = Color.FromArgb(40, 167, 69);
            btnAddBranch.FlatAppearance.BorderSize = 0;
            btnAddBranch.FlatStyle = FlatStyle.Flat;
            btnAddBranch.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnAddBranch.ForeColor = Color.White;
            btnAddBranch.IconChar = IconChar.Add;
            btnAddBranch.IconColor = Color.White;
            btnAddBranch.IconFont = IconFont.Auto;
            btnAddBranch.IconSize = 20;
            btnAddBranch.Location = new Point(869, 20);
            btnAddBranch.Name = "btnAddBranch";
            btnAddBranch.Size = new Size(120, 40);
            btnAddBranch.TabIndex = 2;
            btnAddBranch.Text = "Yeni Şube";
            btnAddBranch.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAddBranch.UseVisualStyleBackColor = false;
            btnAddBranch.Click += BtnAddBranch_Click;
            // 
            // btnRefreshBranches
            // 
            btnRefreshBranches.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefreshBranches.BackColor = Color.FromArgb(108, 117, 125);
            btnRefreshBranches.FlatAppearance.BorderSize = 0;
            btnRefreshBranches.FlatStyle = FlatStyle.Flat;
            btnRefreshBranches.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnRefreshBranches.ForeColor = Color.White;
            btnRefreshBranches.IconChar = IconChar.SyncAlt;
            btnRefreshBranches.IconColor = Color.White;
            btnRefreshBranches.IconFont = IconFont.Auto;
            btnRefreshBranches.IconSize = 20;
            btnRefreshBranches.Location = new Point(789, 20);
            btnRefreshBranches.Name = "btnRefreshBranches";
            btnRefreshBranches.Size = new Size(40, 40);
            btnRefreshBranches.TabIndex = 1;
            btnRefreshBranches.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRefreshBranches.UseVisualStyleBackColor = false;
            btnRefreshBranches.Click += BtnRefreshBranches_Click;
            // 
            // txtSearchBranches
            // 
            txtSearchBranches.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSearchBranches.Font = new Font("Segoe UI", 12F);
            txtSearchBranches.Location = new Point(475, 26);
            txtSearchBranches.Name = "txtSearchBranches";
            txtSearchBranches.PlaceholderText = "Arama...";
            txtSearchBranches.Size = new Size(300, 29);
            txtSearchBranches.TabIndex = 0;
            txtSearchBranches.TextChanged += TxtSearchBranches_TextChanged;
            // 
            // pnlVehicles
            // 
            pnlVehicles.Controls.Add(pnlVehiclesContent);
            pnlVehicles.Controls.Add(pnlVehiclesHeader);
            pnlVehicles.Dock = DockStyle.Fill;
            pnlVehicles.Location = new Point(20, 20);
            pnlVehicles.Name = "pnlVehicles";
            pnlVehicles.Size = new Size(1010, 700);
            pnlVehicles.TabIndex = 2;
            pnlVehicles.Visible = false;
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
            btnAddVehicle.Location = new Point(869, 20);
            btnAddVehicle.Name = "btnAddVehicle";
            btnAddVehicle.Size = new Size(120, 40);
            btnAddVehicle.TabIndex = 2;
            btnAddVehicle.Text = "Yeni Araç";
            btnAddVehicle.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAddVehicle.UseVisualStyleBackColor = false;
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
            // pnlCustomers
            // 
            pnlCustomers.Controls.Add(pnlCustomersContent);
            pnlCustomers.Controls.Add(pnlCustomersHeader);
            pnlCustomers.Dock = DockStyle.Fill;
            pnlCustomers.Location = new Point(20, 20);
            pnlCustomers.Name = "pnlCustomers";
            pnlCustomers.Size = new Size(1010, 700);
            pnlCustomers.TabIndex = 1;
            pnlCustomers.Visible = false;
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
            btnAddCustomer.Size = new Size(120, 40);
            btnAddCustomer.TabIndex = 2;
            btnAddCustomer.Text = "Yeni Müşteri";
            btnAddCustomer.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAddCustomer.UseVisualStyleBackColor = false;
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
            // pnlDashboard
            // 
            pnlDashboard.BackColor = Color.FromArgb(245, 245, 250);
            pnlDashboard.Controls.Add(pnlCharts);
            pnlDashboard.Dock = DockStyle.Fill;
            pnlDashboard.Location = new Point(20, 20);
            pnlDashboard.Name = "pnlDashboard";
            pnlDashboard.Size = new Size(1010, 700);
            pnlDashboard.TabIndex = 0;
            // 
            // pnlCharts
            // 
            pnlCharts.Dock = DockStyle.Fill;
            pnlCharts.Location = new Point(0, 0);
            pnlCharts.Name = "pnlCharts";
            pnlCharts.Size = new Size(1010, 700);
            pnlCharts.TabIndex = 1;
            // 
            // MainPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1300, 800);
            Controls.Add(pnlContent);
            Controls.Add(pnlTopbar);
            Controls.Add(pnlSidebar);
            MinimumSize = new Size(1000, 600);
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
            pnlContent.ResumeLayout(false);
            pnlBranches.ResumeLayout(false);
            pnlBranchesContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvBranches).EndInit();
            pnlBranchesHeader.ResumeLayout(false);
            pnlBranchesHeader.PerformLayout();
            pnlVehicles.ResumeLayout(false);
            pnlVehiclesContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvVehicles).EndInit();
            pnlVehiclesHeader.ResumeLayout(false);
            pnlVehiclesHeader.PerformLayout();
            pnlCustomers.ResumeLayout(false);
            pnlCustomersContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            pnlCustomersHeader.ResumeLayout(false);
            pnlCustomersHeader.PerformLayout();
            pnlDashboard.ResumeLayout(false);
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
        private FontAwesome.Sharp.IconButton btnRentals;
        private FontAwesome.Sharp.IconButton btnSales;
        private FontAwesome.Sharp.IconButton btnMaintenance;
        private FontAwesome.Sharp.IconButton btnReports;
        private FontAwesome.Sharp.IconButton btnSettings;
        private Label lblPageTitle;
        private FontAwesome.Sharp.IconButton btnLogout;
        private Label lblBranchName;
        private Panel pnlDashboard;
        private Panel pnlCharts;
        private Panel pnlVehicles;
        private Panel pnlCustomers;
        private Panel pnlVehiclesContent;
        private DataGridView dgvVehicles;
        private Panel pnlVehiclesHeader;
        private Label lblVehiclesTitle;
        private IconButton btnAddVehicle;
        private IconButton btnRefreshVehicles;
        private TextBox txtSearchVehicles;
        private Panel pnlCustomersContent;
        private DataGridView dgvCustomers;
        private Panel pnlCustomersHeader;
        private Label lblCustomersTitle;
        private IconButton btnAddCustomer;
        private IconButton btnRefreshCustomers;
        private TextBox txtSearchCustomers;
        private Panel pnlBranches;
        private Panel pnlBranchesContent;
        private DataGridView dgvBranches;
        private Panel pnlBranchesHeader;
        private Label lblBranchesTitle;
        private IconButton btnAddBranch;
        private IconButton btnRefreshBranches;
        private TextBox txtSearchBranches;
    }
}