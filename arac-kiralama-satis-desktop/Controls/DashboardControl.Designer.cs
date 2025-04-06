using System;
using System.Windows.Forms;
using System.Drawing;
using FontAwesome.Sharp;

namespace arac_kiralama_satis_desktop.Controls
{
    partial class DashboardControl
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
            // Main container
            this.pnlMain = new System.Windows.Forms.Panel();

            // Welcome panel
            this.pnlWelcome = new System.Windows.Forms.Panel();
            this.lblWelcomeUser = new System.Windows.Forms.Label();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.lblWelcomeSubtitle = new System.Windows.Forms.Label();

            // Top stats flow layout
            this.flpStatCards = new System.Windows.Forms.FlowLayoutPanel();

            // Stat cards
            this.pnlTotalVehicles = new System.Windows.Forms.Panel();
            this.lblTotalVehiclesValue = new System.Windows.Forms.Label();
            this.lblTotalVehiclesTitle = new System.Windows.Forms.Label();
            this.icnTotalVehicles = new FontAwesome.Sharp.IconPictureBox();

            this.pnlActiveRentals = new System.Windows.Forms.Panel();
            this.lblActiveRentalsValue = new System.Windows.Forms.Label();
            this.lblActiveRentalsTitle = new System.Windows.Forms.Label();
            this.icnActiveRentals = new FontAwesome.Sharp.IconPictureBox();

            this.pnlMonthlySales = new System.Windows.Forms.Panel();
            this.lblMonthlySalesValue = new System.Windows.Forms.Label();
            this.lblMonthlySalesTitle = new System.Windows.Forms.Label();
            this.icnMonthlySales = new FontAwesome.Sharp.IconPictureBox();

            this.pnlPendingServices = new System.Windows.Forms.Panel();
            this.lblPendingServicesValue = new System.Windows.Forms.Label();
            this.lblPendingServicesTitle = new System.Windows.Forms.Label();
            this.icnPendingServices = new FontAwesome.Sharp.IconPictureBox();

            // Charts layout
            this.pnlCharts = new System.Windows.Forms.Panel();

            // Brand Distribution Chart Panel
            this.pnlBrandDistribution = new System.Windows.Forms.Panel();
            this.pnlBrandChartContent = new System.Windows.Forms.Panel();
            this.lblNoBrandData = new System.Windows.Forms.Label();
            this.lblBrandDistributionTitle = new System.Windows.Forms.Label();

            // Location Distribution Chart Panel
            this.pnlLocationDistribution = new System.Windows.Forms.Panel();
            this.pnlLocationChartContent = new System.Windows.Forms.Panel();
            this.lblNoLocationData = new System.Windows.Forms.Label();
            this.lblLocationDistributionTitle = new System.Windows.Forms.Label();

            // Revenue Panel
            this.pnlRevenue = new System.Windows.Forms.Panel();
            this.lblTotalRevenue = new System.Windows.Forms.Label();
            this.lblTotalRevenueValue = new System.Windows.Forms.Label();
            this.lblRevenueTitle = new System.Windows.Forms.Label();
            this.lblRevenueSubtitle = new System.Windows.Forms.Label();
            this.icnRevenue = new FontAwesome.Sharp.IconPictureBox();

            // Recent Activities Panel
            this.pnlRecentActivity = new System.Windows.Forms.Panel();
            this.lvwRecentActivity = new System.Windows.Forms.ListView();
            this.lblRecentActivityTitle = new System.Windows.Forms.Label();

            // Initialize all components
            this.pnlMain.SuspendLayout();
            this.pnlWelcome.SuspendLayout();
            this.flpStatCards.SuspendLayout();
            this.pnlTotalVehicles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icnTotalVehicles)).BeginInit();
            this.pnlActiveRentals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icnActiveRentals)).BeginInit();
            this.pnlMonthlySales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icnMonthlySales)).BeginInit();
            this.pnlPendingServices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icnPendingServices)).BeginInit();
            this.pnlCharts.SuspendLayout();
            this.pnlBrandDistribution.SuspendLayout();
            this.pnlLocationDistribution.SuspendLayout();
            this.pnlRevenue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icnRevenue)).BeginInit();
            this.pnlRecentActivity.SuspendLayout();
            this.SuspendLayout();

            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlCharts);
            this.pnlMain.Controls.Add(this.flpStatCards);
            this.pnlMain.Controls.Add(this.pnlWelcome);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(15);
            this.pnlMain.Size = new System.Drawing.Size(1000, 700);
            this.pnlMain.TabIndex = 0;

            // 
            // pnlWelcome
            // 
            this.pnlWelcome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pnlWelcome.Controls.Add(this.lblWelcomeSubtitle);
            this.pnlWelcome.Controls.Add(this.lblDateTime);
            this.pnlWelcome.Controls.Add(this.lblWelcomeUser);
            this.pnlWelcome.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlWelcome.Location = new System.Drawing.Point(15, 15);
            this.pnlWelcome.Name = "pnlWelcome";
            this.pnlWelcome.Size = new System.Drawing.Size(970, 70);
            this.pnlWelcome.TabIndex = 0;

            // 
            // lblWelcomeUser
            // 
            this.lblWelcomeUser.AutoSize = true;
            this.lblWelcomeUser.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblWelcomeUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(76)))), ((int)(((byte)(143)))));
            this.lblWelcomeUser.Location = new System.Drawing.Point(10, 10);
            this.lblWelcomeUser.Name = "lblWelcomeUser";
            this.lblWelcomeUser.Size = new System.Drawing.Size(168, 30);
            this.lblWelcomeUser.TabIndex = 0;
            this.lblWelcomeUser.Text = "Hoş Geldiniz, ";

            // 
            // lblDateTime
            // 
            this.lblDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDateTime.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDateTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblDateTime.Location = new System.Drawing.Point(770, 18);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(190, 19);
            this.lblDateTime.TabIndex = 1;
            this.lblDateTime.Text = "01 Nisan 2023, 09:00";
            this.lblDateTime.TextAlign = System.Drawing.ContentAlignment.TopRight;

            // 
            // lblWelcomeSubtitle
            // 
            this.lblWelcomeSubtitle.AutoSize = true;
            this.lblWelcomeSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblWelcomeSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblWelcomeSubtitle.Location = new System.Drawing.Point(12, 40);
            this.lblWelcomeSubtitle.Name = "lblWelcomeSubtitle";
            this.lblWelcomeSubtitle.Size = new System.Drawing.Size(337, 19);
            this.lblWelcomeSubtitle.TabIndex = 2;
            this.lblWelcomeSubtitle.Text = "İşletme durumunu görebileceğiniz dashboard paneli";

            // 
            // flpStatCards
            // 
            this.flpStatCards.AutoSize = true;
            this.flpStatCards.Controls.Add(this.pnlTotalVehicles);
            this.flpStatCards.Controls.Add(this.pnlActiveRentals);
            this.flpStatCards.Controls.Add(this.pnlMonthlySales);
            this.flpStatCards.Controls.Add(this.pnlPendingServices);
            this.flpStatCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.flpStatCards.Location = new System.Drawing.Point(15, 85);
            this.flpStatCards.Name = "flpStatCards";
            this.flpStatCards.Size = new System.Drawing.Size(970, 120);
            this.flpStatCards.TabIndex = 1;
            this.flpStatCards.WrapContents = false;

            // 
            // pnlTotalVehicles
            // 
            this.pnlTotalVehicles.BackColor = System.Drawing.Color.White;
            this.pnlTotalVehicles.Controls.Add(this.icnTotalVehicles);
            this.pnlTotalVehicles.Controls.Add(this.lblTotalVehiclesTitle);
            this.pnlTotalVehicles.Controls.Add(this.lblTotalVehiclesValue);
            this.pnlTotalVehicles.Location = new System.Drawing.Point(3, 3);
            this.pnlTotalVehicles.Margin = new System.Windows.Forms.Padding(3, 3, 15, 3);
            this.pnlTotalVehicles.Name = "pnlTotalVehicles";
            this.pnlTotalVehicles.Size = new System.Drawing.Size(222, 100);
            this.pnlTotalVehicles.TabIndex = 0;

            // 
            // lblTotalVehiclesValue
            // 
            this.lblTotalVehiclesValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTotalVehiclesValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(76)))), ((int)(((byte)(143)))));
            this.lblTotalVehiclesValue.Location = new System.Drawing.Point(10, 40);
            this.lblTotalVehiclesValue.Name = "lblTotalVehiclesValue";
            this.lblTotalVehiclesValue.Size = new System.Drawing.Size(128, 37);
            this.lblTotalVehiclesValue.TabIndex = 0;
            this.lblTotalVehiclesValue.Text = "50";

            // 
            // lblTotalVehiclesTitle
            // 
            this.lblTotalVehiclesTitle.AutoSize = true;
            this.lblTotalVehiclesTitle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTotalVehiclesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblTotalVehiclesTitle.Location = new System.Drawing.Point(12, 15);
            this.lblTotalVehiclesTitle.Name = "lblTotalVehiclesTitle";
            this.lblTotalVehiclesTitle.Size = new System.Drawing.Size(101, 20);
            this.lblTotalVehiclesTitle.TabIndex = 1;
            this.lblTotalVehiclesTitle.Text = "Toplam Araç";

            // 
            // icnTotalVehicles
            // 
            this.icnTotalVehicles.BackColor = System.Drawing.Color.White;
            this.icnTotalVehicles.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(76)))), ((int)(((byte)(143)))));
            this.icnTotalVehicles.IconChar = FontAwesome.Sharp.IconChar.Car;
            this.icnTotalVehicles.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(76)))), ((int)(((byte)(143)))));
            this.icnTotalVehicles.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.icnTotalVehicles.IconSize = 50;
            this.icnTotalVehicles.Location = new System.Drawing.Point(153, 30);
            this.icnTotalVehicles.Name = "icnTotalVehicles";
            this.icnTotalVehicles.Size = new System.Drawing.Size(50, 50);
            this.icnTotalVehicles.TabIndex = 2;
            this.icnTotalVehicles.TabStop = false;

            // 
            // pnlActiveRentals
            // 
            this.pnlActiveRentals.BackColor = System.Drawing.Color.White;
            this.pnlActiveRentals.Controls.Add(this.icnActiveRentals);
            this.pnlActiveRentals.Controls.Add(this.lblActiveRentalsTitle);
            this.pnlActiveRentals.Controls.Add(this.lblActiveRentalsValue);
            this.pnlActiveRentals.Location = new System.Drawing.Point(243, 3);
            this.pnlActiveRentals.Margin = new System.Windows.Forms.Padding(3, 3, 15, 3);
            this.pnlActiveRentals.Name = "pnlActiveRentals";
            this.pnlActiveRentals.Size = new System.Drawing.Size(222, 100);
            this.pnlActiveRentals.TabIndex = 1;

            // 
            // lblActiveRentalsValue
            // 
            this.lblActiveRentalsValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblActiveRentalsValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.lblActiveRentalsValue.Location = new System.Drawing.Point(10, 40);
            this.lblActiveRentalsValue.Name = "lblActiveRentalsValue";
            this.lblActiveRentalsValue.Size = new System.Drawing.Size(128, 37);
            this.lblActiveRentalsValue.TabIndex = 0;
            this.lblActiveRentalsValue.Text = "12";

            // 
            // lblActiveRentalsTitle
            // 
            this.lblActiveRentalsTitle.AutoSize = true;
            this.lblActiveRentalsTitle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblActiveRentalsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblActiveRentalsTitle.Location = new System.Drawing.Point(12, 15);
            this.lblActiveRentalsTitle.Name = "lblActiveRentalsTitle";
            this.lblActiveRentalsTitle.Size = new System.Drawing.Size(112, 20);
            this.lblActiveRentalsTitle.TabIndex = 1;
            this.lblActiveRentalsTitle.Text = "Aktif Kiralama";

            // 
            // icnActiveRentals
            // 
            this.icnActiveRentals.BackColor = System.Drawing.Color.White;
            this.icnActiveRentals.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.icnActiveRentals.IconChar = FontAwesome.Sharp.IconChar.CalendarCheck;
            this.icnActiveRentals.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.icnActiveRentals.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.icnActiveRentals.IconSize = 50;
            this.icnActiveRentals.Location = new System.Drawing.Point(153, 30);
            this.icnActiveRentals.Name = "icnActiveRentals";
            this.icnActiveRentals.Size = new System.Drawing.Size(50, 50);
            this.icnActiveRentals.TabIndex = 2;
            this.icnActiveRentals.TabStop = false;

            // 
            // pnlMonthlySales
            // 
            this.pnlMonthlySales.BackColor = System.Drawing.Color.White;
            this.pnlMonthlySales.Controls.Add(this.icnMonthlySales);
            this.pnlMonthlySales.Controls.Add(this.lblMonthlySalesTitle);
            this.pnlMonthlySales.Controls.Add(this.lblMonthlySalesValue);
            this.pnlMonthlySales.Location = new System.Drawing.Point(483, 3);
            this.pnlMonthlySales.Margin = new System.Windows.Forms.Padding(3, 3, 15, 3);
            this.pnlMonthlySales.Name = "pnlMonthlySales";
            this.pnlMonthlySales.Size = new System.Drawing.Size(222, 100);
            this.pnlMonthlySales.TabIndex = 2;

            // 
            // lblMonthlySalesValue
            // 
            this.lblMonthlySalesValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblMonthlySalesValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.lblMonthlySalesValue.Location = new System.Drawing.Point(10, 40);
            this.lblMonthlySalesValue.Name = "lblMonthlySalesValue";
            this.lblMonthlySalesValue.Size = new System.Drawing.Size(128, 37);
            this.lblMonthlySalesValue.TabIndex = 0;
            this.lblMonthlySalesValue.Text = "8";

            // 
            // lblMonthlySalesTitle
            // 
            this.lblMonthlySalesTitle.AutoSize = true;
            this.lblMonthlySalesTitle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblMonthlySalesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblMonthlySalesTitle.Location = new System.Drawing.Point(12, 15);
            this.lblMonthlySalesTitle.Name = "lblMonthlySalesTitle";
            this.lblMonthlySalesTitle.Size = new System.Drawing.Size(87, 20);
            this.lblMonthlySalesTitle.TabIndex = 1;
            this.lblMonthlySalesTitle.Text = "Aylık Satış";

            // 
            // icnMonthlySales
            // 
            this.icnMonthlySales.BackColor = System.Drawing.Color.White;
            this.icnMonthlySales.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.icnMonthlySales.IconChar = FontAwesome.Sharp.IconChar.ChartLine;
            this.icnMonthlySales.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.icnMonthlySales.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.icnMonthlySales.IconSize = 50;
            this.icnMonthlySales.Location = new System.Drawing.Point(153, 30);
            this.icnMonthlySales.Name = "icnMonthlySales";
            this.icnMonthlySales.Size = new System.Drawing.Size(50, 50);
            this.icnMonthlySales.TabIndex = 2;
            this.icnMonthlySales.TabStop = false;

            // 
            // pnlPendingServices
            // 
            this.pnlPendingServices.BackColor = System.Drawing.Color.White;
            this.pnlPendingServices.Controls.Add(this.icnPendingServices);
            this.pnlPendingServices.Controls.Add(this.lblPendingServicesTitle);
            this.pnlPendingServices.Controls.Add(this.lblPendingServicesValue);
            this.pnlPendingServices.Location = new System.Drawing.Point(723, 3);
            this.pnlPendingServices.Name = "pnlPendingServices";
            this.pnlPendingServices.Size = new System.Drawing.Size(222, 100);
            this.pnlPendingServices.TabIndex = 3;

            // 
            // lblPendingServicesValue
            // 
            this.lblPendingServicesValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblPendingServicesValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.lblPendingServicesValue.Location = new System.Drawing.Point(10, 40);
            this.lblPendingServicesValue.Name = "lblPendingServicesValue";
            this.lblPendingServicesValue.Size = new System.Drawing.Size(128, 37);
            this.lblPendingServicesValue.TabIndex = 0;
            this.lblPendingServicesValue.Text = "3";

            // 
            // lblPendingServicesTitle
            // 
            this.lblPendingServicesTitle.AutoSize = true;
            this.lblPendingServicesTitle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblPendingServicesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblPendingServicesTitle.Location = new System.Drawing.Point(12, 15);
            this.lblPendingServicesTitle.Name = "lblPendingServicesTitle";
            this.lblPendingServicesTitle.Size = new System.Drawing.Size(124, 20);
            this.lblPendingServicesTitle.TabIndex = 1;
            this.lblPendingServicesTitle.Text = "Bekleyen Servis";

            // 
            // icnPendingServices
            // 
            this.icnPendingServices.BackColor = System.Drawing.Color.White;
            this.icnPendingServices.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.icnPendingServices.IconChar = FontAwesome.Sharp.IconChar.Wrench;
            this.icnPendingServices.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.icnPendingServices.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.icnPendingServices.IconSize = 50;
            this.icnPendingServices.Location = new System.Drawing.Point(153, 30);
            this.icnPendingServices.Name = "icnPendingServices";
            this.icnPendingServices.Size = new System.Drawing.Size(50, 50);
            this.icnPendingServices.TabIndex = 2;
            this.icnPendingServices.TabStop = false;

            // 
            // pnlCharts
            // 
            this.pnlCharts.Controls.Add(this.pnlRecentActivity);
            this.pnlCharts.Controls.Add(this.pnlRevenue);
            this.pnlCharts.Controls.Add(this.pnlLocationDistribution);
            this.pnlCharts.Controls.Add(this.pnlBrandDistribution);
            this.pnlCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCharts.Location = new System.Drawing.Point(15, 205);
            this.pnlCharts.Name = "pnlCharts";
            this.pnlCharts.Size = new System.Drawing.Size(970, 480);
            this.pnlCharts.TabIndex = 2;

            // 
            // pnlBrandDistribution
            // 
            this.pnlBrandDistribution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBrandDistribution.BackColor = System.Drawing.Color.White;
            this.pnlBrandDistribution.Controls.Add(this.lblBrandDistributionTitle);
            this.pnlBrandDistribution.Controls.Add(this.lblNoBrandData);
            this.pnlBrandDistribution.Controls.Add(this.pnlBrandChartContent);
            this.pnlBrandDistribution.Location = new System.Drawing.Point(3, 3);
            this.pnlBrandDistribution.Name = "pnlBrandDistribution";
            this.pnlBrandDistribution.Size = new System.Drawing.Size(470, 230);
            this.pnlBrandDistribution.TabIndex = 0;

            // 
            // pnlBrandChartContent
            // 
            this.pnlBrandChartContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBrandChartContent.Location = new System.Drawing.Point(15, 50);
            this.pnlBrandChartContent.Name = "pnlBrandChartContent";
            this.pnlBrandChartContent.Size = new System.Drawing.Size(440, 165);
            this.pnlBrandChartContent.TabIndex = 0;

            // 
            // lblNoBrandData
            // 
            this.lblNoBrandData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNoBrandData.BackColor = System.Drawing.Color.Transparent;
            this.lblNoBrandData.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblNoBrandData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblNoBrandData.Location = new System.Drawing.Point(15, 120);
            this.lblNoBrandData.Name = "lblNoBrandData";
            this.lblNoBrandData.Size = new System.Drawing.Size(440, 23);
            this.lblNoBrandData.TabIndex = 1;
            this.lblNoBrandData.Text = "Yeterli veri bulunmamaktadır.";
            this.lblNoBrandData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNoBrandData.Visible = false;

            // 
            // lblBrandDistributionTitle
            // 
            this.lblBrandDistributionTitle.AutoSize = true;
            this.lblBrandDistributionTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblBrandDistributionTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(76)))), ((int)(((byte)(143)))));
            this.lblBrandDistributionTitle.Location = new System.Drawing.Point(15, 15);
            this.lblBrandDistributionTitle.Name = "lblBrandDistributionTitle";
            this.lblBrandDistributionTitle.Size = new System.Drawing.Size(146, 21);
            this.lblBrandDistributionTitle.TabIndex = 2;
            this.lblBrandDistributionTitle.Text = "Marka Dağılımları";

            // 
            // pnlLocationDistribution
            // 
            this.pnlLocationDistribution.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlLocationDistribution.BackColor = System.Drawing.Color.White;
            this.pnlLocationDistribution.Controls.Add(this.lblLocationDistributionTitle);
            this.pnlLocationDistribution.Controls.Add(this.lblNoLocationData);
            this.pnlLocationDistribution.Controls.Add(this.pnlLocationChartContent);
            this.pnlLocationDistribution.Location = new System.Drawing.Point(497, 3);
            this.pnlLocationDistribution.Name = "pnlLocationDistribution";
            this.pnlLocationDistribution.Size = new System.Drawing.Size(470, 230);
            this.pnlLocationDistribution.TabIndex = 1;

            // 
            // pnlLocationChartContent
            // 
            this.pnlLocationChartContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlLocationChartContent.Location = new System.Drawing.Point(15, 50);
            this.pnlLocationChartContent.Name = "pnlLocationChartContent";
            this.pnlLocationChartContent.Size = new System.Drawing.Size(440, 165);
            this.pnlLocationChartContent.TabIndex = 0;

            // 
            // lblNoLocationData
            // 
            this.lblNoLocationData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNoLocationData.BackColor = System.Drawing.Color.Transparent;
            this.lblNoLocationData.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblNoLocationData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblNoLocationData.Location = new System.Drawing.Point(15, 120);
            this.lblNoLocationData.Name = "lblNoLocationData";
            this.lblNoLocationData.Size = new System.Drawing.Size(440, 23);
            this.lblNoLocationData.TabIndex = 1;
            this.lblNoLocationData.Text = "Yeterli veri bulunmamaktadır.";
            this.lblNoLocationData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNoLocationData.Visible = false;

            // 
            // lblLocationDistributionTitle
            // 
            this.lblLocationDistributionTitle.AutoSize = true;
            this.lblLocationDistributionTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblLocationDistributionTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(76)))), ((int)(((byte)(143)))));
            this.lblLocationDistributionTitle.Location = new System.Drawing.Point(15, 15);
            this.lblLocationDistributionTitle.Name = "lblLocationDistributionTitle";
            this.lblLocationDistributionTitle.Size = new System.Drawing.Size(126, 21);
            this.lblLocationDistributionTitle.TabIndex = 2;
            this.lblLocationDistributionTitle.Text = "Şube Dağılımları";

            // 
            // pnlRevenue
            // 
            this.pnlRevenue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlRevenue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.pnlRevenue.Controls.Add(this.icnRevenue);
            this.pnlRevenue.Controls.Add(this.lblRevenueSubtitle);
            this.pnlRevenue.Controls.Add(this.lblRevenueTitle);
            this.pnlRevenue.Controls.Add(this.lblTotalRevenueValue);
            this.pnlRevenue.Controls.Add(this.lblTotalRevenue);
            this.pnlRevenue.Location = new System.Drawing.Point(3, 247);
            this.pnlRevenue.Name = "pnlRevenue";
            this.pnlRevenue.Size = new System.Drawing.Size(470, 230);
            this.pnlRevenue.TabIndex = 2;

            // 
            // lblTotalRevenue
            // 
            this.lblTotalRevenue.AutoSize = true;
            this.lblTotalRevenue.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTotalRevenue.ForeColor = System.Drawing.Color.White;
            this.lblTotalRevenue.Location = new System.Drawing.Point(15, 65);
            this.lblTotalRevenue.Name = "lblTotalRevenue";
            this.lblTotalRevenue.Size = new System.Drawing.Size(114, 20);
            this.lblTotalRevenue.TabIndex = 0;
            this.lblTotalRevenue.Text = "Toplam Kazanç";

            // 
            // lblTotalRevenueValue
            // 
            this.lblTotalRevenueValue.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblTotalRevenueValue.ForeColor = System.Drawing.Color.White;
            this.lblTotalRevenueValue.Location = new System.Drawing.Point(15, 95);
            this.lblTotalRevenueValue.Name = "lblTotalRevenueValue";
            this.lblTotalRevenueValue.Size = new System.Drawing.Size(350, 50);
            this.lblTotalRevenueValue.TabIndex = 1;
            this.lblTotalRevenueValue.Text = "₺120,350.00";

            // 
            // lblRevenueTitle
            // 
            this.lblRevenueTitle.AutoSize = true;
            this.lblRevenueTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblRevenueTitle.ForeColor = System.Drawing.Color.White;
            this.lblRevenueTitle.Location = new System.Drawing.Point(15, 15);
            this.lblRevenueTitle.Name = "lblRevenueTitle";
            this.lblRevenueTitle.Size = new System.Drawing.Size(70, 21);
            this.lblRevenueTitle.TabIndex = 2;
            this.lblRevenueTitle.Text = "Finansal";

            // 
            // lblRevenueSubtitle
            // 
            this.lblRevenueSubtitle.AutoSize = true;
            this.lblRevenueSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRevenueSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(220)))));
            this.lblRevenueSubtitle.Location = new System.Drawing.Point(16, 145);
            this.lblRevenueSubtitle.Name = "lblRevenueSubtitle";
            this.lblRevenueSubtitle.Size = new System.Drawing.Size(242, 15);
            this.lblRevenueSubtitle.TabIndex = 3;
            this.lblRevenueSubtitle.Text = "Kiralamalar ve satışlardan elde edilen kazanç";

            // 
            // icnRevenue
            // 
            this.icnRevenue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.icnRevenue.ForeColor = System.Drawing.Color.White;
            this.icnRevenue.IconChar = FontAwesome.Sharp.IconChar.MoneyBillWave;
            this.icnRevenue.IconColor = System.Drawing.Color.White;
            this.icnRevenue.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.icnRevenue.IconSize = 80;
            this.icnRevenue.Location = new System.Drawing.Point(370, 75);
            this.icnRevenue.Name = "icnRevenue";
            this.icnRevenue.Size = new System.Drawing.Size(80, 80);
            this.icnRevenue.TabIndex = 4;
            this.icnRevenue.TabStop = false;

            // 
            // pnlRecentActivity
            // 
            this.pnlRecentActivity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlRecentActivity.BackColor = System.Drawing.Color.White;
            this.pnlRecentActivity.Controls.Add(this.lblRecentActivityTitle);
            this.pnlRecentActivity.Controls.Add(this.lvwRecentActivity);
            this.pnlRecentActivity.Location = new System.Drawing.Point(497, 247);
            this.pnlRecentActivity.Name = "pnlRecentActivity";
            this.pnlRecentActivity.Size = new System.Drawing.Size(470, 230);
            this.pnlRecentActivity.TabIndex = 3;

            // 
            // lvwRecentActivity
            // 
            this.lvwRecentActivity.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwRecentActivity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvwRecentActivity.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lvwRecentActivity.HideSelection = false;
            this.lvwRecentActivity.Location = new System.Drawing.Point(15, 50);
            this.lvwRecentActivity.Name = "lvwRecentActivity";
            this.lvwRecentActivity.Size = new System.Drawing.Size(440, 165);
            this.lvwRecentActivity.TabIndex = 0;
            this.lvwRecentActivity.UseCompatibleStateImageBehavior = false;
            this.lvwRecentActivity.View = System.Windows.Forms.View.List;

            // 
            // lblRecentActivityTitle
            // 
            this.lblRecentActivityTitle.AutoSize = true;
            this.lblRecentActivityTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblRecentActivityTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(76)))), ((int)(((byte)(143)))));
            this.lblRecentActivityTitle.Location = new System.Drawing.Point(15, 15);
            this.lblRecentActivityTitle.Name = "lblRecentActivityTitle";
            this.lblRecentActivityTitle.Size = new System.Drawing.Size(153, 21);
            this.lblRecentActivityTitle.TabIndex = 1;
            this.lblRecentActivityTitle.Text = "Son Gerçekleşenler";

            // 
            // DashboardControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "DashboardControl";
            this.Size = new System.Drawing.Size(1000, 700);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlWelcome.ResumeLayout(false);
            this.pnlWelcome.PerformLayout();
            this.flpStatCards.ResumeLayout(false);
            this.pnlTotalVehicles.ResumeLayout(false);
            this.pnlTotalVehicles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icnTotalVehicles)).EndInit();
            this.pnlActiveRentals.ResumeLayout(false);
            this.pnlActiveRentals.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icnActiveRentals)).EndInit();
            this.pnlMonthlySales.ResumeLayout(false);
            this.pnlMonthlySales.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icnMonthlySales)).EndInit();
            this.pnlPendingServices.ResumeLayout(false);
            this.pnlPendingServices.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icnPendingServices)).EndInit();
            this.pnlCharts.ResumeLayout(false);
            this.pnlBrandDistribution.ResumeLayout(false);
            this.pnlBrandDistribution.PerformLayout();
            this.pnlLocationDistribution.ResumeLayout(false);
            this.pnlLocationDistribution.PerformLayout();
            this.pnlRevenue.ResumeLayout(false);
            this.pnlRevenue.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icnRevenue)).EndInit();
            this.pnlRecentActivity.ResumeLayout(false);
            this.pnlRecentActivity.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlWelcome;
        private System.Windows.Forms.Label lblWelcomeUser;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.Label lblWelcomeSubtitle;
        private System.Windows.Forms.FlowLayoutPanel flpStatCards;
        private System.Windows.Forms.Panel pnlTotalVehicles;
        private FontAwesome.Sharp.IconPictureBox icnTotalVehicles;
        private System.Windows.Forms.Label lblTotalVehiclesTitle;
        private System.Windows.Forms.Label lblTotalVehiclesValue;
        private System.Windows.Forms.Panel pnlActiveRentals;
        private FontAwesome.Sharp.IconPictureBox icnActiveRentals;
        private System.Windows.Forms.Label lblActiveRentalsTitle;
        private System.Windows.Forms.Label lblActiveRentalsValue;
        private System.Windows.Forms.Panel pnlMonthlySales;
        private FontAwesome.Sharp.IconPictureBox icnMonthlySales;
        private System.Windows.Forms.Label lblMonthlySalesTitle;
        private System.Windows.Forms.Label lblMonthlySalesValue;
        private System.Windows.Forms.Panel pnlPendingServices;
        private FontAwesome.Sharp.IconPictureBox icnPendingServices;
        private System.Windows.Forms.Label lblPendingServicesTitle;
        private System.Windows.Forms.Label lblPendingServicesValue;
        private System.Windows.Forms.Panel pnlCharts;
        private System.Windows.Forms.Panel pnlRecentActivity;
        private System.Windows.Forms.Label lblRecentActivityTitle;
        private System.Windows.Forms.ListView lvwRecentActivity;
        private System.Windows.Forms.Panel pnlRevenue;
        private FontAwesome.Sharp.IconPictureBox icnRevenue;
        private System.Windows.Forms.Label lblRevenueSubtitle;
        private System.Windows.Forms.Label lblRevenueTitle;
        private System.Windows.Forms.Label lblTotalRevenueValue;
        private System.Windows.Forms.Label lblTotalRevenue;
        private System.Windows.Forms.Panel pnlLocationDistribution;
        private System.Windows.Forms.Label lblLocationDistributionTitle;
        private System.Windows.Forms.Label lblNoLocationData;
        private System.Windows.Forms.Panel pnlLocationChartContent;
        private System.Windows.Forms.Panel pnlBrandDistribution;
        private System.Windows.Forms.Label lblBrandDistributionTitle;
        private System.Windows.Forms.Label lblNoBrandData;
        private System.Windows.Forms.Panel pnlBrandChartContent;
    }
}