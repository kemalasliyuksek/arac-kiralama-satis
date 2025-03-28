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
            btnCustomers = new IconButton();
            btnVehicles = new IconButton();
            btnDashboard = new IconButton();
            pnlLogo = new Panel();
            lblAppName = new Label();
            picLogo = new PictureBox();
            pnlTopbar = new Panel();
            lblBranchName = new Label();
            lblPageTitle = new Label();
            btnLogout = new IconButton();
            pnlContent = new Panel();
            pnlDashboard = new Panel();
            pnlCharts = new Panel();
            pnlCarsByLocation = new Panel();
            chartCarsByLocation = new Panel();
            lblLocationSubtitle = new Label();
            lblCarsByLocation = new Label();
            pnlRentalsByYear = new Panel();
            chartRentalsByYear = new Panel();
            lblRentalsSubtitle = new Label();
            lblRentalsByYear = new Label();
            pnlCarsByBrand = new Panel();
            chartCarsByBrand = new Panel();
            lblBrandSubtitle = new Label();
            lblCarsByBrand = new Label();
            pnlCards = new Panel();
            pnlAvgRentalPrice = new Panel();
            lblPriceTimeFrame = new Label();
            lblAvgPrice = new Label();
            lblAvgRentalPrice = new Label();
            iconAvgPrice = new IconPictureBox();
            pnlCarBrands = new Panel();
            lblBrandCount = new Label();
            lblCarBrands = new Label();
            iconBrands = new IconPictureBox();
            pnlLocations = new Panel();
            lblLocationCount = new Label();
            lblLocations = new Label();
            iconLocations = new IconPictureBox();
            pnlTotalCars = new Panel();
            lblCarCount = new Label();
            lblTotalCars = new Label();
            iconCars = new IconPictureBox();
            pnlSidebar.SuspendLayout();
            pnlUserInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picUserAvatar).BeginInit();
            pnlMenu.SuspendLayout();
            pnlLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            pnlTopbar.SuspendLayout();
            pnlContent.SuspendLayout();
            pnlDashboard.SuspendLayout();
            pnlCharts.SuspendLayout();
            pnlCarsByLocation.SuspendLayout();
            pnlRentalsByYear.SuspendLayout();
            pnlCarsByBrand.SuspendLayout();
            pnlCards.SuspendLayout();
            pnlAvgRentalPrice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconAvgPrice).BeginInit();
            pnlCarBrands.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconBrands).BeginInit();
            pnlLocations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconLocations).BeginInit();
            pnlTotalCars.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconCars).BeginInit();
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
            lblUserRole.Size = new Size(56, 15);
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
            lblUserName.Size = new Size(92, 19);
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
            pnlMenu.Controls.Add(btnCustomers);
            pnlMenu.Controls.Add(btnVehicles);
            pnlMenu.Controls.Add(btnDashboard);
            pnlMenu.Dock = DockStyle.Fill;
            pnlMenu.Location = new Point(0, 80);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Size = new Size(250, 650);
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
            btnSettings.Location = new Point(0, 350);
            btnSettings.Name = "btnSettings";
            btnSettings.Padding = new Padding(15, 0, 0, 0);
            btnSettings.Size = new Size(250, 50);
            btnSettings.TabIndex = 7;
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
            btnReports.Location = new Point(0, 300);
            btnReports.Name = "btnReports";
            btnReports.Padding = new Padding(15, 0, 0, 0);
            btnReports.Size = new Size(250, 50);
            btnReports.TabIndex = 6;
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
            btnMaintenance.Location = new Point(0, 250);
            btnMaintenance.Name = "btnMaintenance";
            btnMaintenance.Padding = new Padding(15, 0, 0, 0);
            btnMaintenance.Size = new Size(250, 50);
            btnMaintenance.TabIndex = 5;
            btnMaintenance.Text = "  Bakım & Servis";
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
            btnSales.Location = new Point(0, 200);
            btnSales.Name = "btnSales";
            btnSales.Padding = new Padding(15, 0, 0, 0);
            btnSales.Size = new Size(250, 50);
            btnSales.TabIndex = 4;
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
            btnRentals.Location = new Point(0, 150);
            btnRentals.Name = "btnRentals";
            btnRentals.Padding = new Padding(15, 0, 0, 0);
            btnRentals.Size = new Size(250, 50);
            btnRentals.TabIndex = 3;
            btnRentals.Text = "  Kiralamalar";
            btnRentals.TextAlign = ContentAlignment.MiddleLeft;
            btnRentals.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRentals.UseVisualStyleBackColor = true;
            btnRentals.Click += BtnRentals_Click;
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
            pnlLogo.Controls.Add(picLogo);
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
            lblAppName.Location = new Point(70, 27);
            lblAppName.Name = "lblAppName";
            lblAppName.Size = new Size(88, 25);
            lblAppName.TabIndex = 1;
            lblAppName.Text = "CarBook";
            // 
            // picLogo
            // 
            picLogo.Location = new Point(20, 20);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(40, 40);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabIndex = 0;
            picLogo.TabStop = false;
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
            lblBranchName.Size = new Size(90, 15);
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
            lblPageTitle.Size = new Size(115, 25);
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
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(250, 60);
            pnlContent.Name = "pnlContent";
            pnlContent.Padding = new Padding(20);
            pnlContent.Size = new Size(1050, 740);
            pnlContent.TabIndex = 2;
            // 
            // pnlDashboard
            // 
            pnlDashboard.Dock = DockStyle.Fill;
            pnlDashboard.Location = new Point(20, 20);
            pnlDashboard.Name = "pnlDashboard";
            pnlDashboard.Size = new Size(1010, 700);
            pnlDashboard.BackColor = Color.FromArgb(245, 245, 250);
            pnlDashboard.TabIndex = 0;
            // 
            // pnlCharts
            // 
            pnlCharts.Controls.Add(pnlCarsByLocation);
            pnlCharts.Controls.Add(pnlRentalsByYear);
            pnlCharts.Controls.Add(pnlCarsByBrand);
            pnlCharts.Dock = DockStyle.Fill;
            pnlCharts.Location = new Point(0, 160);
            pnlCharts.Name = "pnlCharts";
            pnlCharts.Size = new Size(1010, 540);
            pnlCharts.TabIndex = 1;
            // 
            // pnlCarsByLocation
            // 
            pnlCarsByLocation.BackColor = Color.White;
            pnlCarsByLocation.Controls.Add(chartCarsByLocation);
            pnlCarsByLocation.Controls.Add(lblLocationSubtitle);
            pnlCarsByLocation.Controls.Add(lblCarsByLocation);
            pnlCarsByLocation.Location = new Point(674, 10);
            pnlCarsByLocation.Name = "pnlCarsByLocation";
            pnlCarsByLocation.Size = new Size(324, 500);
            pnlCarsByLocation.TabIndex = 3;
            // 
            // chartCarsByLocation
            // 
            chartCarsByLocation.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            chartCarsByLocation.BackColor = Color.White;
            chartCarsByLocation.BorderStyle = BorderStyle.None;
            chartCarsByLocation.Location = new Point(20, 78);
            chartCarsByLocation.Name = "chartCarsByLocation";
            chartCarsByLocation.Size = new Size(284, 402);
            chartCarsByLocation.TabIndex = 2;
            // 
            // lblLocationSubtitle
            // 
            lblLocationSubtitle.AutoSize = true;
            lblLocationSubtitle.Font = new Font("Segoe UI", 9F);
            lblLocationSubtitle.ForeColor = Color.Gray;
            lblLocationSubtitle.Location = new Point(20, 45);
            lblLocationSubtitle.Name = "lblLocationSubtitle";
            lblLocationSubtitle.Size = new Size(210, 15);
            lblLocationSubtitle.TabIndex = 1;
            lblLocationSubtitle.Text = "You can see the number of cars by location";
            // 
            // lblCarsByLocation
            // 
            lblCarsByLocation.AutoSize = true;
            lblCarsByLocation.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblCarsByLocation.Location = new Point(20, 20);
            lblCarsByLocation.Name = "lblCarsByLocation";
            lblCarsByLocation.Size = new Size(173, 21);
            lblCarsByLocation.TabIndex = 0;
            lblCarsByLocation.Text = "Car Count By Location";
            // 
            // pnlRentalsByYear
            // 
            pnlRentalsByYear.BackColor = Color.White;
            pnlRentalsByYear.Controls.Add(chartRentalsByYear);
            pnlRentalsByYear.Controls.Add(lblRentalsSubtitle);
            pnlRentalsByYear.Controls.Add(lblRentalsByYear);
            pnlRentalsByYear.Location = new Point(340, 10);
            pnlRentalsByYear.Name = "pnlRentalsByYear";
            pnlRentalsByYear.Size = new Size(324, 500);
            pnlRentalsByYear.TabIndex = 1;
            // 
            // chartRentalsByYear
            // 
            chartRentalsByYear.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            chartRentalsByYear.BackColor = Color.White;
            chartRentalsByYear.BorderStyle = BorderStyle.None;
            chartRentalsByYear.Location = new Point(20, 78);
            chartRentalsByYear.Name = "chartRentalsByYear";
            chartRentalsByYear.Size = new Size(284, 402);
            chartRentalsByYear.TabIndex = 2;
            // 
            // lblRentalsSubtitle
            // 
            lblRentalsSubtitle.AutoSize = true;
            lblRentalsSubtitle.Font = new Font("Segoe UI", 9F);
            lblRentalsSubtitle.ForeColor = Color.Gray;
            lblRentalsSubtitle.Location = new Point(20, 45);
            lblRentalsSubtitle.Name = "lblRentalsSubtitle";
            lblRentalsSubtitle.Size = new Size(171, 15);
            lblRentalsSubtitle.TabIndex = 1;
            lblRentalsSubtitle.Text = "Rental counts by year in the system";
            // 
            // lblRentalsByYear
            // 
            lblRentalsByYear.AutoSize = true;
            lblRentalsByYear.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblRentalsByYear.Location = new Point(20, 20);
            lblRentalsByYear.Name = "lblRentalsByYear";
            lblRentalsByYear.Size = new Size(136, 21);
            lblRentalsByYear.TabIndex = 0;
            lblRentalsByYear.Text = "Rentals By Year";
            // 
            // pnlCarsByBrand
            // 
            pnlCarsByBrand.BackColor = Color.White;
            pnlCarsByBrand.Controls.Add(chartCarsByBrand);
            pnlCarsByBrand.Controls.Add(lblBrandSubtitle);
            pnlCarsByBrand.Controls.Add(lblCarsByBrand);
            pnlCarsByBrand.Location = new Point(6, 10);
            pnlCarsByBrand.Name = "pnlCarsByBrand";
            pnlCarsByBrand.Size = new Size(324, 500);
            pnlCarsByBrand.TabIndex = 0;
            // 
            // chartCarsByBrand
            // 
            chartCarsByBrand.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            chartCarsByBrand.BackColor = Color.White;
            chartCarsByBrand.BorderStyle = BorderStyle.None;
            chartCarsByBrand.Location = new Point(20, 78);
            chartCarsByBrand.Name = "chartCarsByBrand";
            chartCarsByBrand.Size = new Size(284, 402);
            chartCarsByBrand.TabIndex = 2;
            // 
            // lblBrandSubtitle
            // 
            lblBrandSubtitle.AutoSize = true;
            lblBrandSubtitle.Font = new Font("Segoe UI", 9F);
            lblBrandSubtitle.ForeColor = Color.Gray;
            lblBrandSubtitle.Location = new Point(20, 45);
            lblBrandSubtitle.Name = "lblBrandSubtitle";
            lblBrandSubtitle.Size = new Size(169, 15);
            lblBrandSubtitle.TabIndex = 1;
            lblBrandSubtitle.Text = "Car count by brand in the system";
            // 
            // lblCarsByBrand
            // 
            lblCarsByBrand.AutoSize = true;
            lblCarsByBrand.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblCarsByBrand.Location = new Point(20, 20);
            lblCarsByBrand.Name = "lblCarsByBrand";
            lblCarsByBrand.Size = new Size(155, 21);
            lblCarsByBrand.TabIndex = 0;
            lblCarsByBrand.Text = "Car Count By Brand";
            // 
            // pnlCards
            // 
            pnlCards.Controls.Add(pnlAvgRentalPrice);
            pnlCards.Controls.Add(pnlCarBrands);
            pnlCards.Controls.Add(pnlLocations);
            pnlCards.Controls.Add(pnlTotalCars);
            pnlCards.Dock = DockStyle.Top;
            pnlCards.Location = new Point(0, 0);
            pnlCards.Name = "pnlCards";
            pnlCards.Size = new Size(1010, 160);
            pnlCards.TabIndex = 0;
            // 
            // pnlAvgRentalPrice
            // 
            pnlAvgRentalPrice.BackColor = Color.FromArgb(47, 192, 120);
            pnlAvgRentalPrice.Controls.Add(lblPriceTimeFrame);
            pnlAvgRentalPrice.Controls.Add(lblAvgPrice);
            pnlAvgRentalPrice.Controls.Add(lblAvgRentalPrice);
            pnlAvgRentalPrice.Controls.Add(iconAvgPrice);
            pnlAvgRentalPrice.Location = new Point(761, 10);
            pnlAvgRentalPrice.Name = "pnlAvgRentalPrice";
            pnlAvgRentalPrice.Size = new Size(237, 130);
            pnlAvgRentalPrice.TabIndex = 3;
            // 
            // lblPriceTimeFrame
            // 
            lblPriceTimeFrame.AutoSize = true;
            lblPriceTimeFrame.Font = new Font("Segoe UI", 8F);
            lblPriceTimeFrame.ForeColor = Color.WhiteSmoke;
            lblPriceTimeFrame.Location = new Point(1165, 14);
            lblPriceTimeFrame.Name = "lblPriceTimeFrame";
            lblPriceTimeFrame.Size = new Size(32, 13);
            lblPriceTimeFrame.TabIndex = 3;
            lblPriceTimeFrame.Text = "week";
            // 
            // lblAvgPrice
            // 
            lblAvgPrice.AutoSize = true;
            lblAvgPrice.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblAvgPrice.ForeColor = Color.White;
            lblAvgPrice.Location = new Point(20, 54);
            lblAvgPrice.Name = "lblAvgPrice";
            lblAvgPrice.Size = new Size(140, 41);
            lblAvgPrice.TabIndex = 2;
            lblAvgPrice.Text = "₺ 632,70";
            // 
            // lblAvgRentalPrice
            // 
            lblAvgRentalPrice.AutoSize = true;
            lblAvgRentalPrice.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblAvgRentalPrice.ForeColor = Color.White;
            lblAvgRentalPrice.Location = new Point(20, 20);
            lblAvgRentalPrice.Name = "lblAvgRentalPrice";
            lblAvgRentalPrice.Size = new Size(177, 21);
            lblAvgRentalPrice.TabIndex = 1;
            lblAvgRentalPrice.Text = "Average Car Rental Price";
            // 
            // iconAvgPrice
            // 
            iconAvgPrice.BackColor = Color.FromArgb(47, 192, 120);
            iconAvgPrice.ForeColor = Color.FromArgb(229, 229, 229);
            iconAvgPrice.IconChar = IconChar.MoneyBillWave;
            iconAvgPrice.IconColor = Color.FromArgb(229, 229, 229);
            iconAvgPrice.IconFont = IconFont.Auto;
            iconAvgPrice.IconSize = 56;
            iconAvgPrice.Location = new Point(172, 54);
            iconAvgPrice.Name = "iconAvgPrice";
            iconAvgPrice.Size = new Size(56, 56);
            iconAvgPrice.TabIndex = 4;
            iconAvgPrice.TabStop = false;
            // 
            // pnlCarBrands
            // 
            pnlCarBrands.BackColor = Color.FromArgb(156, 136, 255);
            pnlCarBrands.Controls.Add(lblBrandCount);
            pnlCarBrands.Controls.Add(lblCarBrands);
            pnlCarBrands.Controls.Add(iconBrands);
            pnlCarBrands.Location = new Point(509, 10);
            pnlCarBrands.Name = "pnlCarBrands";
            pnlCarBrands.Size = new Size(237, 130);
            pnlCarBrands.TabIndex = 2;
            // 
            // lblBrandCount
            // 
            lblBrandCount.AutoSize = true;
            lblBrandCount.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblBrandCount.ForeColor = Color.White;
            lblBrandCount.Location = new Point(20, 54);
            lblBrandCount.Name = "lblBrandCount";
            lblBrandCount.Size = new Size(34, 41);
            lblBrandCount.TabIndex = 2;
            lblBrandCount.Text = "8";
            // 
            // lblCarBrands
            // 
            lblCarBrands.AutoSize = true;
            lblCarBrands.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblCarBrands.ForeColor = Color.White;
            lblCarBrands.Location = new Point(20, 20);
            lblCarBrands.Name = "lblCarBrands";
            lblCarBrands.Size = new Size(135, 21);
            lblCarBrands.TabIndex = 1;
            lblCarBrands.Text = "Car Brand Count";
            // 
            // iconBrands
            // 
            iconBrands.BackColor = Color.FromArgb(156, 136, 255);
            iconBrands.ForeColor = Color.FromArgb(229, 229, 229);
            iconBrands.IconChar = IconChar.Building;
            iconBrands.IconColor = Color.FromArgb(229, 229, 229);
            iconBrands.IconFont = IconFont.Auto;
            iconBrands.IconSize = 56;
            iconBrands.Location = new Point(172, 54);
            iconBrands.Name = "iconBrands";
            iconBrands.Size = new Size(56, 56);
            iconBrands.TabIndex = 4;
            iconBrands.TabStop = false;
            // 
            // pnlLocations
            // 
            pnlLocations.BackColor = Color.FromArgb(29, 35, 49);
            pnlLocations.Controls.Add(lblLocationCount);
            pnlLocations.Controls.Add(lblLocations);
            pnlLocations.Controls.Add(iconLocations);
            pnlLocations.Location = new Point(257, 10);
            pnlLocations.Name = "pnlLocations";
            pnlLocations.Size = new Size(237, 130);
            pnlLocations.TabIndex = 1;
            // 
            // lblLocationCount
            // 
            lblLocationCount.AutoSize = true;
            lblLocationCount.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblLocationCount.ForeColor = Color.White;
            lblLocationCount.Location = new Point(20, 54);
            lblLocationCount.Name = "lblLocationCount";
            lblLocationCount.Size = new Size(34, 41);
            lblLocationCount.TabIndex = 2;
            lblLocationCount.Text = "6";
            // 
            // lblLocations
            // 
            lblLocations.AutoSize = true;
            lblLocations.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblLocations.ForeColor = Color.White;
            lblLocations.Location = new Point(20, 20);
            lblLocations.Name = "lblLocations";
            lblLocations.Size = new Size(128, 21);
            lblLocations.TabIndex = 1;
            lblLocations.Text = "Location Count";
            // 
            // iconLocations
            // 
            iconLocations.BackColor = Color.FromArgb(29, 35, 49);
            iconLocations.ForeColor = Color.FromArgb(229, 229, 229);
            iconLocations.IconChar = IconChar.LocationDot;
            iconLocations.IconColor = Color.FromArgb(229, 229, 229);
            iconLocations.IconFont = IconFont.Auto;
            iconLocations.IconSize = 56;
            iconLocations.Location = new Point(172, 54);
            iconLocations.Name = "iconLocations";
            iconLocations.Size = new Size(56, 56);
            iconLocations.TabIndex = 3;
            iconLocations.TabStop = false;
            // 
            // pnlTotalCars
            // 
            pnlTotalCars.BackColor = Color.FromArgb(156, 136, 255);
            pnlTotalCars.Controls.Add(lblCarCount);
            pnlTotalCars.Controls.Add(lblTotalCars);
            pnlTotalCars.Controls.Add(iconCars);
            pnlTotalCars.Location = new Point(6, 10);
            pnlTotalCars.Name = "pnlTotalCars";
            pnlTotalCars.Size = new Size(237, 130);
            pnlTotalCars.TabIndex = 0;
            // 
            // lblCarCount
            // 
            lblCarCount.AutoSize = true;
            lblCarCount.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblCarCount.ForeColor = Color.White;
            lblCarCount.Location = new Point(20, 54);
            lblCarCount.Name = "lblCarCount";
            lblCarCount.Size = new Size(50, 41);
            lblCarCount.TabIndex = 2;
            lblCarCount.Text = "11";
            // 
            // lblTotalCars
            // 
            lblTotalCars.AutoSize = true;
            lblTotalCars.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblTotalCars.ForeColor = Color.White;
            lblTotalCars.Location = new Point(20, 20);
            lblTotalCars.Name = "lblTotalCars";
            lblTotalCars.Size = new Size(120, 21);
            lblTotalCars.TabIndex = 1;
            lblTotalCars.Text = "Total Car Count";
            // 
            // iconCars
            // 
            iconCars.BackColor = Color.FromArgb(156, 136, 255);
            iconCars.ForeColor = Color.FromArgb(229, 229, 229);
            iconCars.IconChar = IconChar.CarSide;
            iconCars.IconColor = Color.FromArgb(229, 229, 229);
            iconCars.IconFont = IconFont.Auto;
            iconCars.IconSize = 56;
            iconCars.Location = new Point(172, 54);
            iconCars.Name = "iconCars";
            iconCars.Size = new Size(56, 56);
            iconCars.TabIndex = 3;
            iconCars.TabStop = false;
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
            pnlSidebar.ResumeLayout(false);
            pnlUserInfo.ResumeLayout(false);
            pnlUserInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picUserAvatar).EndInit();
            pnlMenu.ResumeLayout(false);
            pnlLogo.ResumeLayout(false);
            pnlLogo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            pnlTopbar.ResumeLayout(false);
            pnlTopbar.PerformLayout();
            pnlContent.ResumeLayout(false);
            pnlDashboard.ResumeLayout(false);
            pnlCharts.ResumeLayout(false);
            pnlCarsByLocation.ResumeLayout(false);
            pnlCarsByLocation.PerformLayout();
            pnlRentalsByYear.ResumeLayout(false);
            pnlRentalsByYear.PerformLayout();
            pnlCarsByBrand.ResumeLayout(false);
            pnlCarsByBrand.PerformLayout();
            pnlCards.ResumeLayout(false);
            pnlAvgRentalPrice.ResumeLayout(false);
            pnlAvgRentalPrice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconAvgPrice).EndInit();
            pnlCarBrands.ResumeLayout(false);
            pnlCarBrands.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconBrands).EndInit();
            pnlLocations.ResumeLayout(false);
            pnlLocations.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconLocations).EndInit();
            pnlTotalCars.ResumeLayout(false);
            pnlTotalCars.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconCars).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlSidebar;
        private Panel pnlTopbar;
        private Panel pnlContent;
        private Panel pnlLogo;
        private Panel pnlMenu;
        private Panel pnlUserInfo;
        private PictureBox picLogo;
        private Label lblAppName;
        private PictureBox picUserAvatar;
        private Label lblUserName;
        private Label lblUserRole;
        private FontAwesome.Sharp.IconButton btnDashboard;
        private FontAwesome.Sharp.IconButton btnVehicles;
        private FontAwesome.Sharp.IconButton btnCustomers;
        private FontAwesome.Sharp.IconButton btnRentals;
        private FontAwesome.Sharp.IconButton btnSales;
        private FontAwesome.Sharp.IconButton btnMaintenance;
        private FontAwesome.Sharp.IconButton btnReports;
        private FontAwesome.Sharp.IconButton btnSettings;
        private Label lblPageTitle;
        private FontAwesome.Sharp.IconButton btnLogout;
        private Label lblBranchName;
        private Panel pnlDashboard;
        private Panel pnlCards;
        private Panel pnlTotalCars;
        private Panel pnlLocations;
        private Panel pnlCarBrands;
        private Panel pnlAvgRentalPrice;
        private Label lblTotalCars;
        private Label lblCarCount;
        private FontAwesome.Sharp.IconPictureBox iconCars;
        private Label lblLocationCount;
        private Label lblLocations;
        private FontAwesome.Sharp.IconPictureBox iconLocations;
        private Label lblBrandCount;
        private Label lblCarBrands;
        private FontAwesome.Sharp.IconPictureBox iconBrands;
        private Label lblPriceTimeFrame;
        private Label lblAvgPrice;
        private Label lblAvgRentalPrice;
        private FontAwesome.Sharp.IconPictureBox iconAvgPrice;
        private Panel pnlCharts;
        private Panel pnlCarsByBrand;
        private Label lblBrandSubtitle;
        private Label lblCarsByBrand;
        private Panel chartCarsByBrand;
        private Panel pnlRentalsByYear;
        private Panel chartRentalsByYear;
        private Label lblRentalsSubtitle;
        private Label lblRentalsByYear;
        private Panel pnlCarsByLocation;
        private Panel chartCarsByLocation;
        private Label lblLocationSubtitle;
        private Label lblCarsByLocation;
    }
}