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
            pnlRentals = new Panel();
            pnlRentalsContent = new Panel();
            dgvRentals = new DataGridView();
            pnlRentalsHeader = new Panel();
            lblRentalsTitle = new Label();
            btnAddRental = new IconButton();
            btnRefreshRentals = new IconButton();
            txtSearchRentals = new TextBox();
            pnlStaff = new Panel();
            pnlStaffContent = new Panel();
            dgvStaff = new DataGridView();
            pnlStaffHeader = new Panel();
            lblStaffTitle = new Label();
            btnAddStaff = new IconButton();
            btnRefreshStaff = new IconButton();
            txtSearchStaff = new TextBox();
            pnlDashboard = new Panel();
            pnlCharts = new Panel();
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
            pnlSidebar.SuspendLayout();
            pnlUserInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picUserAvatar).BeginInit();
            pnlMenu.SuspendLayout();
            pnlLogo.SuspendLayout();
            pnlTopbar.SuspendLayout();
            pnlContent.SuspendLayout();
            pnlRentals.SuspendLayout();
            pnlRentalsContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRentals).BeginInit();
            pnlRentalsHeader.SuspendLayout();
            pnlStaff.SuspendLayout();
            pnlStaffContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStaff).BeginInit();
            pnlStaffHeader.SuspendLayout();
            pnlDashboard.SuspendLayout();
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
            pnlContent.Controls.Add(pnlRentals);
            pnlContent.Controls.Add(pnlStaff);
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
            // pnlRentals
            // 
            pnlRentals.Controls.Add(pnlRentalsContent);
            pnlRentals.Controls.Add(pnlRentalsHeader);
            pnlRentals.Dock = DockStyle.Fill;
            pnlRentals.Location = new Point(20, 20);
            pnlRentals.Name = "pnlRentals";
            pnlRentals.Size = new Size(1010, 700);
            pnlRentals.TabIndex = 5;
            pnlRentals.Visible = false;
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
            lblRentalsTitle.Size = new Size(178, 30);
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
            btnAddRental.Size = new Size(120, 40);
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
            // pnlStaff
            // 
            pnlStaff.Controls.Add(pnlStaffContent);
            pnlStaff.Controls.Add(pnlStaffHeader);
            pnlStaff.Dock = DockStyle.Fill;
            pnlStaff.Location = new Point(20, 20);
            pnlStaff.Name = "pnlStaff";
            pnlStaff.Size = new Size(1010, 700);
            pnlStaff.TabIndex = 4;
            pnlStaff.Visible = false;
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
            btnAddStaff.Location = new Point(869, 20);
            btnAddStaff.Name = "btnAddStaff";
            btnAddStaff.Size = new Size(120, 40);
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
            pnlContent.ResumeLayout(false);
            pnlRentals.ResumeLayout(false);
            pnlRentalsContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvRentals).EndInit();
            pnlRentalsHeader.ResumeLayout(false);
            pnlRentalsHeader.PerformLayout();
            pnlStaff.ResumeLayout(false);
            pnlStaffContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvStaff).EndInit();
            pnlStaffHeader.ResumeLayout(false);
            pnlStaffHeader.PerformLayout();
            pnlDashboard.ResumeLayout(false);
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
        private Panel pnlStaff;
        private Panel pnlStaffContent;
        private DataGridView dgvStaff;
        private Panel pnlStaffHeader;
        private Label lblStaffTitle;
        private IconButton btnAddStaff;
        private IconButton btnRefreshStaff;
        private TextBox txtSearchStaff;
        private Panel pnlRentals;
        private Panel pnlRentalsContent;
        private DataGridView dgvRentals;
        private Panel pnlRentalsHeader;
        private Label lblRentalsTitle;
        private IconButton btnAddRental;
        private IconButton btnRefreshRentals;
        private TextBox txtSearchRentals;
    }
}