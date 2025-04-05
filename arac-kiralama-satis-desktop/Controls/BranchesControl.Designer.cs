using FontAwesome.Sharp;

namespace arac_kiralama_satis_desktop.Controls
{
    partial class BranchesControl
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
            pnlBranches = new Panel();
            pnlBranchesContent = new Panel();
            dgvBranches = new DataGridView();
            pnlBranchesHeader = new Panel();
            lblBranchesTitle = new Label();
            btnAddBranch = new IconButton();
            btnRefreshBranches = new IconButton();
            txtSearchBranches = new TextBox();
            pnlBranches.SuspendLayout();
            pnlBranchesContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBranches).BeginInit();
            pnlBranchesHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlBranches
            // 
            pnlBranches.Controls.Add(pnlBranchesContent);
            pnlBranches.Controls.Add(pnlBranchesHeader);
            pnlBranches.Dock = DockStyle.Fill;
            pnlBranches.Location = new Point(0, 0);
            pnlBranches.Name = "pnlBranches";
            pnlBranches.Size = new Size(1010, 700);
            pnlBranches.TabIndex = 0;
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
            btnAddBranch.Location = new Point(870, 20);
            btnAddBranch.Name = "btnAddBranch";
            btnAddBranch.Size = new Size(130, 40);
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
            // BranchesControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlBranches);
            Name = "BranchesControl";
            Size = new Size(1010, 700);
            pnlBranches.ResumeLayout(false);
            pnlBranchesContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvBranches).EndInit();
            pnlBranchesHeader.ResumeLayout(false);
            pnlBranchesHeader.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlBranches;
        private System.Windows.Forms.Panel pnlBranchesContent;
        private System.Windows.Forms.DataGridView dgvBranches;
        private System.Windows.Forms.Panel pnlBranchesHeader;
        private System.Windows.Forms.Label lblBranchesTitle;
        private IconButton btnAddBranch;
        private IconButton btnRefreshBranches;
        private System.Windows.Forms.TextBox txtSearchBranches;
    }
}