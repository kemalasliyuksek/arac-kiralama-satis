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
            pnlDashboard = new Panel();
            pnlCharts = new Panel();
            MetricsPanel = new Panel();
            tableLayoutMetrics = new TableLayoutPanel();
            metricPanel1 = new Panel();
            metricTitle1 = new Label();
            metricValue1 = new Label();
            iconLabel1 = new Label();
            metricPanel2 = new Panel();
            metricTitle2 = new Label();
            metricValue2 = new Label();
            iconLabel2 = new Label();
            metricPanel3 = new Panel();
            metricTitle3 = new Label();
            metricValue3 = new Label();
            iconLabel3 = new Label();
            metricPanel4 = new Panel();
            metricTitle4 = new Label();
            metricValue4 = new Label();
            iconLabel4 = new Label();
            metricPanel5 = new Panel();
            metricTitle5 = new Label();
            metricValue5 = new Label();
            iconLabel5 = new Label();
            metricPanel6 = new Panel();
            metricTitle6 = new Label();
            metricValue6 = new Label();
            iconLabel6 = new Label();
            metricPanel7 = new Panel();
            metricTitle7 = new Label();
            metricValue7 = new Label();
            iconLabel7 = new Label();
            metricPanel8 = new Panel();
            metricTitle8 = new Label();
            metricValue8 = new Label();
            iconLabel8 = new Label();
            pnlDashboard.SuspendLayout();
            pnlCharts.SuspendLayout();
            MetricsPanel.SuspendLayout();
            tableLayoutMetrics.SuspendLayout();
            metricPanel1.SuspendLayout();
            metricPanel2.SuspendLayout();
            metricPanel3.SuspendLayout();
            metricPanel4.SuspendLayout();
            metricPanel5.SuspendLayout();
            metricPanel6.SuspendLayout();
            metricPanel7.SuspendLayout();
            metricPanel8.SuspendLayout();
            SuspendLayout();
            // 
            // pnlDashboard
            // 
            pnlDashboard.BackColor = Color.FromArgb(245, 245, 250);
            pnlDashboard.Controls.Add(pnlCharts);
            pnlDashboard.Dock = DockStyle.Fill;
            pnlDashboard.Location = new Point(0, 0);
            pnlDashboard.Name = "pnlDashboard";
            pnlDashboard.Size = new Size(1010, 700);
            pnlDashboard.TabIndex = 0;
            // 
            // pnlCharts
            // 
            pnlCharts.Controls.Add(MetricsPanel);
            pnlCharts.Dock = DockStyle.Fill;
            pnlCharts.Location = new Point(0, 0);
            pnlCharts.Name = "pnlCharts";
            pnlCharts.Size = new Size(1010, 700);
            pnlCharts.TabIndex = 1;
            // 
            // MetricsPanel
            // 
            MetricsPanel.Controls.Add(tableLayoutMetrics);
            MetricsPanel.Dock = DockStyle.Top;
            MetricsPanel.Location = new Point(0, 0);
            MetricsPanel.Name = "MetricsPanel";
            MetricsPanel.Size = new Size(1010, 400);
            MetricsPanel.TabIndex = 0;
            // 
            // tableLayoutMetrics
            // 
            tableLayoutMetrics.ColumnCount = 4;
            tableLayoutMetrics.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutMetrics.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutMetrics.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutMetrics.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutMetrics.Controls.Add(metricPanel1, 0, 0);
            tableLayoutMetrics.Controls.Add(metricPanel2, 1, 0);
            tableLayoutMetrics.Controls.Add(metricPanel3, 2, 0);
            tableLayoutMetrics.Controls.Add(metricPanel4, 3, 0);
            tableLayoutMetrics.Controls.Add(metricPanel5, 0, 1);
            tableLayoutMetrics.Controls.Add(metricPanel6, 1, 1);
            tableLayoutMetrics.Controls.Add(metricPanel7, 2, 1);
            tableLayoutMetrics.Controls.Add(metricPanel8, 3, 1);
            tableLayoutMetrics.Dock = DockStyle.Fill;
            tableLayoutMetrics.Location = new Point(0, 0);
            tableLayoutMetrics.Name = "tableLayoutMetrics";
            tableLayoutMetrics.RowCount = 2;
            tableLayoutMetrics.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutMetrics.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutMetrics.Size = new Size(1010, 400);
            tableLayoutMetrics.TabIndex = 0;
            // 
            // metricPanel1
            // 
            metricPanel1.Anchor = AnchorStyles.None;
            metricPanel1.BackColor = Color.White;
            metricPanel1.Controls.Add(metricTitle1);
            metricPanel1.Controls.Add(metricValue1);
            metricPanel1.Controls.Add(iconLabel1);
            metricPanel1.Location = new Point(16, 40);
            metricPanel1.Margin = new Padding(10);
            metricPanel1.Name = "metricPanel1";
            metricPanel1.Size = new Size(220, 120);
            metricPanel1.TabIndex = 10;
            // 
            // metricTitle1
            // 
            metricTitle1.AutoSize = true;
            metricTitle1.Font = new Font("Segoe UI", 10F);
            metricTitle1.ForeColor = Color.FromArgb(100, 100, 100);
            metricTitle1.Location = new Point(15, 15);
            metricTitle1.Name = "metricTitle1";
            metricTitle1.Size = new Size(84, 19);
            metricTitle1.TabIndex = 0;
            metricTitle1.Text = "Toplam Araç";
            // 
            // metricValue1
            // 
            metricValue1.AutoSize = true;
            metricValue1.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            metricValue1.ForeColor = Color.FromArgb(83, 107, 168);
            metricValue1.Location = new Point(15, 45);
            metricValue1.Name = "metricValue1";
            metricValue1.Size = new Size(35, 41);
            metricValue1.TabIndex = 1;
            metricValue1.Text = "0";
            // 
            // iconLabel1
            // 
            iconLabel1.AutoSize = true;
            iconLabel1.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            iconLabel1.ForeColor = Color.FromArgb(83, 107, 168);
            iconLabel1.Location = new Point(175, 35);
            iconLabel1.Name = "iconLabel1";
            iconLabel1.Size = new Size(38, 51);
            iconLabel1.TabIndex = 2;
            iconLabel1.Text = "•";
            // 
            // metricPanel2
            // 
            metricPanel2.Anchor = AnchorStyles.None;
            metricPanel2.BackColor = Color.White;
            metricPanel2.Controls.Add(metricTitle2);
            metricPanel2.Controls.Add(metricValue2);
            metricPanel2.Controls.Add(iconLabel2);
            metricPanel2.Location = new Point(268, 40);
            metricPanel2.Margin = new Padding(10);
            metricPanel2.Name = "metricPanel2";
            metricPanel2.Size = new Size(220, 120);
            metricPanel2.TabIndex = 11;
            // 
            // metricTitle2
            // 
            metricTitle2.AutoSize = true;
            metricTitle2.Font = new Font("Segoe UI", 10F);
            metricTitle2.ForeColor = Color.FromArgb(100, 100, 100);
            metricTitle2.Location = new Point(15, 15);
            metricTitle2.Name = "metricTitle2";
            metricTitle2.Size = new Size(76, 19);
            metricTitle2.TabIndex = 0;
            metricTitle2.Text = "Şube Sayısı";
            // 
            // metricValue2
            // 
            metricValue2.AutoSize = true;
            metricValue2.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            metricValue2.ForeColor = Color.FromArgb(40, 167, 69);
            metricValue2.Location = new Point(15, 45);
            metricValue2.Name = "metricValue2";
            metricValue2.Size = new Size(35, 41);
            metricValue2.TabIndex = 1;
            metricValue2.Text = "0";
            // 
            // iconLabel2
            // 
            iconLabel2.AutoSize = true;
            iconLabel2.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            iconLabel2.ForeColor = Color.FromArgb(40, 167, 69);
            iconLabel2.Location = new Point(175, 35);
            iconLabel2.Name = "iconLabel2";
            iconLabel2.Size = new Size(38, 51);
            iconLabel2.TabIndex = 2;
            iconLabel2.Text = "•";
            // 
            // metricPanel3
            // 
            metricPanel3.Anchor = AnchorStyles.None;
            metricPanel3.BackColor = Color.White;
            metricPanel3.Controls.Add(metricTitle3);
            metricPanel3.Controls.Add(metricValue3);
            metricPanel3.Controls.Add(iconLabel3);
            metricPanel3.Location = new Point(520, 40);
            metricPanel3.Margin = new Padding(10);
            metricPanel3.Name = "metricPanel3";
            metricPanel3.Size = new Size(220, 120);
            metricPanel3.TabIndex = 12;
            // 
            // metricTitle3
            // 
            metricTitle3.AutoSize = true;
            metricTitle3.Font = new Font("Segoe UI", 10F);
            metricTitle3.ForeColor = Color.FromArgb(100, 100, 100);
            metricTitle3.Location = new Point(15, 15);
            metricTitle3.Name = "metricTitle3";
            metricTitle3.Size = new Size(93, 19);
            metricTitle3.TabIndex = 0;
            metricTitle3.Text = "Müşteri Sayısı";
            // 
            // metricValue3
            // 
            metricValue3.AutoSize = true;
            metricValue3.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            metricValue3.ForeColor = Color.FromArgb(255, 193, 7);
            metricValue3.Location = new Point(15, 45);
            metricValue3.Name = "metricValue3";
            metricValue3.Size = new Size(35, 41);
            metricValue3.TabIndex = 1;
            metricValue3.Text = "0";
            // 
            // iconLabel3
            // 
            iconLabel3.AutoSize = true;
            iconLabel3.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            iconLabel3.ForeColor = Color.FromArgb(255, 193, 7);
            iconLabel3.Location = new Point(175, 35);
            iconLabel3.Name = "iconLabel3";
            iconLabel3.Size = new Size(38, 51);
            iconLabel3.TabIndex = 2;
            iconLabel3.Text = "•";
            // 
            // metricPanel4
            // 
            metricPanel4.Anchor = AnchorStyles.None;
            metricPanel4.BackColor = Color.White;
            metricPanel4.Controls.Add(metricTitle4);
            metricPanel4.Controls.Add(metricValue4);
            metricPanel4.Controls.Add(iconLabel4);
            metricPanel4.Location = new Point(773, 40);
            metricPanel4.Margin = new Padding(10);
            metricPanel4.Name = "metricPanel4";
            metricPanel4.Size = new Size(220, 120);
            metricPanel4.TabIndex = 13;
            // 
            // metricTitle4
            // 
            metricTitle4.AutoSize = true;
            metricTitle4.Font = new Font("Segoe UI", 10F);
            metricTitle4.ForeColor = Color.FromArgb(100, 100, 100);
            metricTitle4.Location = new Point(15, 15);
            metricTitle4.Name = "metricTitle4";
            metricTitle4.Size = new Size(105, 19);
            metricTitle4.TabIndex = 0;
            metricTitle4.Text = "Toplam Gelir (₺)";
            // 
            // metricValue4
            // 
            metricValue4.AutoSize = true;
            metricValue4.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            metricValue4.ForeColor = Color.FromArgb(23, 162, 184);
            metricValue4.Location = new Point(15, 45);
            metricValue4.Name = "metricValue4";
            metricValue4.Size = new Size(35, 41);
            metricValue4.TabIndex = 1;
            metricValue4.Text = "0";
            // 
            // iconLabel4
            // 
            iconLabel4.AutoSize = true;
            iconLabel4.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            iconLabel4.ForeColor = Color.FromArgb(23, 162, 184);
            iconLabel4.Location = new Point(175, 35);
            iconLabel4.Name = "iconLabel4";
            iconLabel4.Size = new Size(38, 51);
            iconLabel4.TabIndex = 2;
            iconLabel4.Text = "•";
            // 
            // metricPanel5
            // 
            metricPanel5.Anchor = AnchorStyles.None;
            metricPanel5.BackColor = Color.White;
            metricPanel5.Controls.Add(metricTitle5);
            metricPanel5.Controls.Add(metricValue5);
            metricPanel5.Controls.Add(iconLabel5);
            metricPanel5.Location = new Point(16, 240);
            metricPanel5.Margin = new Padding(10);
            metricPanel5.Name = "metricPanel5";
            metricPanel5.Size = new Size(220, 120);
            metricPanel5.TabIndex = 14;
            // 
            // metricTitle5
            // 
            metricTitle5.AutoSize = true;
            metricTitle5.Font = new Font("Segoe UI", 10F);
            metricTitle5.ForeColor = Color.FromArgb(100, 100, 100);
            metricTitle5.Location = new Point(15, 15);
            metricTitle5.Name = "metricTitle5";
            metricTitle5.Size = new Size(108, 19);
            metricTitle5.TabIndex = 0;
            metricTitle5.Text = "Aktif Kiralamalar";
            // 
            // metricValue5
            // 
            metricValue5.AutoSize = true;
            metricValue5.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            metricValue5.ForeColor = Color.FromArgb(220, 53, 69);
            metricValue5.Location = new Point(15, 45);
            metricValue5.Name = "metricValue5";
            metricValue5.Size = new Size(35, 41);
            metricValue5.TabIndex = 1;
            metricValue5.Text = "0";
            // 
            // iconLabel5
            // 
            iconLabel5.AutoSize = true;
            iconLabel5.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            iconLabel5.ForeColor = Color.FromArgb(220, 53, 69);
            iconLabel5.Location = new Point(175, 35);
            iconLabel5.Name = "iconLabel5";
            iconLabel5.Size = new Size(38, 51);
            iconLabel5.TabIndex = 2;
            iconLabel5.Text = "•";
            // 
            // metricPanel6
            // 
            metricPanel6.Anchor = AnchorStyles.None;
            metricPanel6.BackColor = Color.White;
            metricPanel6.Controls.Add(metricTitle6);
            metricPanel6.Controls.Add(metricValue6);
            metricPanel6.Controls.Add(iconLabel6);
            metricPanel6.Location = new Point(268, 240);
            metricPanel6.Margin = new Padding(10);
            metricPanel6.Name = "metricPanel6";
            metricPanel6.Size = new Size(220, 120);
            metricPanel6.TabIndex = 15;
            // 
            // metricTitle6
            // 
            metricTitle6.AutoSize = true;
            metricTitle6.Font = new Font("Segoe UI", 10F);
            metricTitle6.ForeColor = Color.FromArgb(100, 100, 100);
            metricTitle6.Location = new Point(15, 15);
            metricTitle6.Name = "metricTitle6";
            metricTitle6.Size = new Size(92, 19);
            metricTitle6.TabIndex = 0;
            metricTitle6.Text = "Bu Ay Satışlar";
            // 
            // metricValue6
            // 
            metricValue6.AutoSize = true;
            metricValue6.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            metricValue6.ForeColor = Color.FromArgb(111, 66, 193);
            metricValue6.Location = new Point(15, 45);
            metricValue6.Name = "metricValue6";
            metricValue6.Size = new Size(35, 41);
            metricValue6.TabIndex = 1;
            metricValue6.Text = "0";
            // 
            // iconLabel6
            // 
            iconLabel6.AutoSize = true;
            iconLabel6.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            iconLabel6.ForeColor = Color.FromArgb(111, 66, 193);
            iconLabel6.Location = new Point(175, 35);
            iconLabel6.Name = "iconLabel6";
            iconLabel6.Size = new Size(38, 51);
            iconLabel6.TabIndex = 2;
            iconLabel6.Text = "•";
            // 
            // metricPanel7
            // 
            metricPanel7.Anchor = AnchorStyles.None;
            metricPanel7.BackColor = Color.White;
            metricPanel7.Controls.Add(metricTitle7);
            metricPanel7.Controls.Add(metricValue7);
            metricPanel7.Controls.Add(iconLabel7);
            metricPanel7.Location = new Point(520, 240);
            metricPanel7.Margin = new Padding(10);
            metricPanel7.Name = "metricPanel7";
            metricPanel7.Size = new Size(220, 120);
            metricPanel7.TabIndex = 16;
            // 
            // metricTitle7
            // 
            metricTitle7.AutoSize = true;
            metricTitle7.Font = new Font("Segoe UI", 10F);
            metricTitle7.ForeColor = Color.FromArgb(100, 100, 100);
            metricTitle7.Location = new Point(15, 15);
            metricTitle7.Name = "metricTitle7";
            metricTitle7.Size = new Size(102, 19);
            metricTitle7.TabIndex = 0;
            metricTitle7.Text = "Servis Bekleyen";
            // 
            // metricValue7
            // 
            metricValue7.AutoSize = true;
            metricValue7.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            metricValue7.ForeColor = Color.FromArgb(253, 126, 20);
            metricValue7.Location = new Point(15, 45);
            metricValue7.Name = "metricValue7";
            metricValue7.Size = new Size(35, 41);
            metricValue7.TabIndex = 1;
            metricValue7.Text = "0";
            // 
            // iconLabel7
            // 
            iconLabel7.AutoSize = true;
            iconLabel7.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            iconLabel7.ForeColor = Color.FromArgb(253, 126, 20);
            iconLabel7.Location = new Point(175, 35);
            iconLabel7.Name = "iconLabel7";
            iconLabel7.Size = new Size(38, 51);
            iconLabel7.TabIndex = 2;
            iconLabel7.Text = "•";
            // 
            // metricPanel8
            // 
            metricPanel8.Anchor = AnchorStyles.None;
            metricPanel8.BackColor = Color.White;
            metricPanel8.Controls.Add(metricTitle8);
            metricPanel8.Controls.Add(metricValue8);
            metricPanel8.Controls.Add(iconLabel8);
            metricPanel8.Location = new Point(773, 240);
            metricPanel8.Margin = new Padding(10);
            metricPanel8.Name = "metricPanel8";
            metricPanel8.Size = new Size(220, 120);
            metricPanel8.TabIndex = 17;
            // 
            // metricTitle8
            // 
            metricTitle8.AutoSize = true;
            metricTitle8.Font = new Font("Segoe UI", 10F);
            metricTitle8.ForeColor = Color.FromArgb(100, 100, 100);
            metricTitle8.Location = new Point(15, 15);
            metricTitle8.Name = "metricTitle8";
            metricTitle8.Size = new Size(80, 19);
            metricTitle8.TabIndex = 0;
            metricTitle8.Text = "Ekip Üyeleri";
            // 
            // metricValue8
            // 
            metricValue8.AutoSize = true;
            metricValue8.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            metricValue8.ForeColor = Color.FromArgb(108, 117, 125);
            metricValue8.Location = new Point(15, 45);
            metricValue8.Name = "metricValue8";
            metricValue8.Size = new Size(35, 41);
            metricValue8.TabIndex = 1;
            metricValue8.Text = "0";
            // 
            // iconLabel8
            // 
            iconLabel8.AutoSize = true;
            iconLabel8.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            iconLabel8.ForeColor = Color.FromArgb(108, 117, 125);
            iconLabel8.Location = new Point(175, 35);
            iconLabel8.Name = "iconLabel8";
            iconLabel8.Size = new Size(38, 51);
            iconLabel8.TabIndex = 2;
            iconLabel8.Text = "•";
            // 
            // DashboardControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlDashboard);
            Name = "DashboardControl";
            Size = new Size(1010, 700);
            pnlDashboard.ResumeLayout(false);
            pnlCharts.ResumeLayout(false);
            MetricsPanel.ResumeLayout(false);
            tableLayoutMetrics.ResumeLayout(false);
            metricPanel1.ResumeLayout(false);
            metricPanel1.PerformLayout();
            metricPanel2.ResumeLayout(false);
            metricPanel2.PerformLayout();
            metricPanel3.ResumeLayout(false);
            metricPanel3.PerformLayout();
            metricPanel4.ResumeLayout(false);
            metricPanel4.PerformLayout();
            metricPanel5.ResumeLayout(false);
            metricPanel5.PerformLayout();
            metricPanel6.ResumeLayout(false);
            metricPanel6.PerformLayout();
            metricPanel7.ResumeLayout(false);
            metricPanel7.PerformLayout();
            metricPanel8.ResumeLayout(false);
            metricPanel8.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlDashboard;
        private System.Windows.Forms.Panel pnlCharts;
        private Panel MetricsPanel;
        private TableLayoutPanel tableLayoutMetrics;
        private Panel metricPanel1;
        private Label metricTitle1;
        private Label metricValue1;
        private Label iconLabel1;
        private Panel metricPanel2;
        private Label metricTitle2;
        private Label metricValue2;
        private Label iconLabel2;
        private Panel metricPanel3;
        private Label metricTitle3;
        private Label metricValue3;
        private Label iconLabel3;
        private Panel metricPanel4;
        private Label metricTitle4;
        private Label metricValue4;
        private Label iconLabel4;
        private Panel metricPanel5;
        private Label metricTitle5;
        private Label metricValue5;
        private Label iconLabel5;
        private Panel metricPanel6;
        private Label metricTitle6;
        private Label metricValue6;
        private Label iconLabel6;
        private Panel metricPanel7;
        private Label metricTitle7;
        private Label metricValue7;
        private Label iconLabel7;
        private Panel metricPanel8;
        private Label metricTitle8;
        private Label metricValue8;
        private Label iconLabel8;
    }
}