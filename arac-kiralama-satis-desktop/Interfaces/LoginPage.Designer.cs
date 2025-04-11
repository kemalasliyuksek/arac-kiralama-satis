using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using FontAwesome.Sharp;

namespace arac_kiralama_satis_desktop.Interfaces
{
    partial class LoginPage
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
            pnlLeft = new Panel();
            picLogo = new PictureBox();
            pnlContent = new Panel();
            btnMinimize = new IconButton();
            btnClose = new IconButton();
            label1 = new Label();
            lblStatus = new Label();
            btnLogin = new Button();
            lnkForgotPassword = new LinkLabel();
            chkRememberMe = new CheckBox();
            pnlPassword = new Panel();
            btnShowPassword = new Button();
            txtPassword = new TextBox();
            picLock = new PictureBox();
            pnlUsername = new Panel();
            txtUsername = new TextBox();
            picUser = new PictureBox();
            lblSubtitle = new Label();
            lblTitle = new Label();
            pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            pnlContent.SuspendLayout();
            pnlPassword.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLock).BeginInit();
            pnlUsername.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picUser).BeginInit();
            SuspendLayout();
            // 
            // pnlLeft
            // 
            pnlLeft.BackColor = Color.FromArgb(26, 115, 232);
            pnlLeft.Controls.Add(picLogo);
            pnlLeft.Dock = DockStyle.Left;
            pnlLeft.Location = new Point(0, 0);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Size = new Size(350, 600);
            pnlLeft.TabIndex = 0;
            pnlLeft.MouseDown += PnlLeft_MouseDown;
            pnlLeft.MouseMove += PnlLeft_MouseMove;
            pnlLeft.MouseUp += PnlLeft_MouseUp;
            // 
            // picLogo
            // 
            picLogo.BackColor = Color.FromArgb(49, 76, 143);
            picLogo.Location = new Point(0, 0);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(350, 600);
            picLogo.TabIndex = 3;
            picLogo.TabStop = false;
            picLogo.MouseDown += PnlLeft_MouseDown;
            picLogo.MouseMove += PnlLeft_MouseMove;
            picLogo.MouseUp += PnlLeft_MouseUp;
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.WhiteSmoke;
            pnlContent.Controls.Add(btnMinimize);
            pnlContent.Controls.Add(btnClose);
            pnlContent.Controls.Add(label1);
            pnlContent.Controls.Add(lblStatus);
            pnlContent.Controls.Add(btnLogin);
            pnlContent.Controls.Add(lnkForgotPassword);
            pnlContent.Controls.Add(chkRememberMe);
            pnlContent.Controls.Add(pnlPassword);
            pnlContent.Controls.Add(pnlUsername);
            pnlContent.Controls.Add(lblSubtitle);
            pnlContent.Controls.Add(lblTitle);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(350, 0);
            pnlContent.Name = "pnlContent";
            pnlContent.Padding = new Padding(50);
            pnlContent.Size = new Size(450, 600);
            pnlContent.TabIndex = 1;
            pnlContent.MouseDown += PnlContent_MouseDown;
            pnlContent.MouseMove += PnlContent_MouseMove;
            pnlContent.MouseUp += PnlContent_MouseUp;
            // 
            // btnMinimize
            // 
            btnMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMinimize.FlatAppearance.BorderSize = 0;
            btnMinimize.FlatStyle = FlatStyle.Flat;
            btnMinimize.IconChar = IconChar.WindowMinimize;
            btnMinimize.IconColor = Color.FromArgb(49, 76, 143);
            btnMinimize.IconFont = IconFont.Auto;
            btnMinimize.IconSize = 24;
            btnMinimize.Location = new Point(375, 5);
            btnMinimize.Name = "btnMinimize";
            btnMinimize.Size = new Size(32, 32);
            btnMinimize.TabIndex = 9;
            btnMinimize.UseVisualStyleBackColor = true;
            btnMinimize.Click += BtnMinimize_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.IconChar = IconChar.Close;
            btnClose.IconColor = Color.FromArgb(49, 76, 143);
            btnClose.IconFont = IconFont.Auto;
            btnClose.IconSize = 24;
            btnClose.Location = new Point(413, 5);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(32, 32);
            btnClose.TabIndex = 8;
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += BtnClose_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(375, 576);
            label1.Name = "label1";
            label1.Size = new Size(63, 15);
            label1.TabIndex = 8;
            label1.Text = "Version 1.0";
            // 
            // lblStatus
            // 
            lblStatus.Font = new Font("Segoe UI", 10F);
            lblStatus.ForeColor = Color.FromArgb(220, 53, 69);
            lblStatus.Location = new Point(50, 481);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(350, 20);
            lblStatus.TabIndex = 7;
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(49, 76, 143);
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(50, 411);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(350, 50);
            btnLogin.TabIndex = 6;
            btnLogin.Text = "Giriş Yap";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += BtnLogin_Click;
            // 
            // lnkForgotPassword
            // 
            lnkForgotPassword.ActiveLinkColor = Color.FromArgb(26, 115, 232);
            lnkForgotPassword.AutoSize = true;
            lnkForgotPassword.Font = new Font("Segoe UI", 10F);
            lnkForgotPassword.LinkColor = Color.FromArgb(49, 76, 143);
            lnkForgotPassword.Location = new Point(290, 362);
            lnkForgotPassword.Name = "lnkForgotPassword";
            lnkForgotPassword.Size = new Size(110, 19);
            lnkForgotPassword.TabIndex = 5;
            lnkForgotPassword.TabStop = true;
            lnkForgotPassword.Text = "Şifremi Unuttum";
            lnkForgotPassword.TextAlign = ContentAlignment.MiddleRight;
            lnkForgotPassword.LinkClicked += LnkForgotPassword_LinkClicked;
            // 
            // chkRememberMe
            // 
            chkRememberMe.AutoSize = true;
            chkRememberMe.Font = new Font("Segoe UI", 10F);
            chkRememberMe.ForeColor = Color.Gray;
            chkRememberMe.Location = new Point(50, 361);
            chkRememberMe.Name = "chkRememberMe";
            chkRememberMe.Size = new Size(98, 23);
            chkRememberMe.TabIndex = 4;
            chkRememberMe.Text = "Beni Hatırla";
            chkRememberMe.UseVisualStyleBackColor = true;
            // 
            // pnlPassword
            // 
            pnlPassword.BackColor = Color.White;
            pnlPassword.BorderStyle = BorderStyle.FixedSingle;
            pnlPassword.Controls.Add(btnShowPassword);
            pnlPassword.Controls.Add(txtPassword);
            pnlPassword.Controls.Add(picLock);
            pnlPassword.Location = new Point(50, 301);
            pnlPassword.Name = "pnlPassword";
            pnlPassword.Size = new Size(350, 45);
            pnlPassword.TabIndex = 3;
            // 
            // btnShowPassword
            // 
            btnShowPassword.BackColor = Color.Transparent;
            btnShowPassword.FlatAppearance.BorderSize = 0;
            btnShowPassword.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnShowPassword.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnShowPassword.FlatStyle = FlatStyle.Flat;
            btnShowPassword.Location = new Point(315, 7);
            btnShowPassword.Name = "btnShowPassword";
            btnShowPassword.Size = new Size(30, 30);
            btnShowPassword.TabIndex = 3;
            btnShowPassword.UseVisualStyleBackColor = false;
            btnShowPassword.MouseDown += BtnShowPassword_MouseDown;
            btnShowPassword.MouseUp += BtnShowPassword_MouseUp;
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.White;
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Font = new Font("Segoe UI", 12F);
            txtPassword.ForeColor = Color.FromArgb(64, 64, 64);
            txtPassword.Location = new Point(40, 10);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '•';
            txtPassword.Size = new Size(270, 22);
            txtPassword.TabIndex = 2;
            // 
            // picLock
            // 
            picLock.Location = new Point(10, 10);
            picLock.Name = "picLock";
            picLock.Size = new Size(24, 24);
            picLock.SizeMode = PictureBoxSizeMode.Zoom;
            picLock.TabIndex = 0;
            picLock.TabStop = false;
            // 
            // pnlUsername
            // 
            pnlUsername.BackColor = Color.White;
            pnlUsername.BorderStyle = BorderStyle.FixedSingle;
            pnlUsername.Controls.Add(txtUsername);
            pnlUsername.Controls.Add(picUser);
            pnlUsername.Location = new Point(50, 231);
            pnlUsername.Name = "pnlUsername";
            pnlUsername.Size = new Size(350, 45);
            pnlUsername.TabIndex = 2;
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.White;
            txtUsername.BorderStyle = BorderStyle.None;
            txtUsername.Font = new Font("Segoe UI", 12F);
            txtUsername.ForeColor = Color.FromArgb(64, 64, 64);
            txtUsername.Location = new Point(40, 10);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(300, 22);
            txtUsername.TabIndex = 1;
            // 
            // picUser
            // 
            picUser.Location = new Point(10, 10);
            picUser.Name = "picUser";
            picUser.Size = new Size(24, 24);
            picUser.SizeMode = PictureBoxSizeMode.Zoom;
            picUser.TabIndex = 0;
            picUser.TabStop = false;
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 12F);
            lblSubtitle.ForeColor = Color.Gray;
            lblSubtitle.Location = new Point(46, 190);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(359, 21);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Lütfen sisteme giriş yapmak için bilgilerinizi giriniz";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 30F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(49, 76, 143);
            lblTitle.Location = new Point(96, 80);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(258, 54);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Hoş Geldiniz";
            // 
            // LoginPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(800, 600);
            Controls.Add(pnlContent);
            Controls.Add(pnlLeft);
            FormBorderStyle = FormBorderStyle.None;
            Name = "LoginPage";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Kullanıcı Girişi";
            pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            pnlContent.ResumeLayout(false);
            pnlContent.PerformLayout();
            pnlPassword.ResumeLayout(false);
            pnlPassword.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLock).EndInit();
            pnlUsername.ResumeLayout(false);
            pnlUsername.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picUser).EndInit();
            ResumeLayout(false);
        }

        #endregion

        // Form kontrollerini tanımla
        private Panel pnlLeft;

        private Panel pnlContent;
        private Label lblTitle;
        private Label lblSubtitle;

        private Panel pnlUsername;
        private PictureBox picUser;
        private TextBox txtUsername;

        private Panel pnlPassword;
        private PictureBox picLock;
        private TextBox txtPassword;
        private Button btnShowPassword;

        private CheckBox chkRememberMe;
        private LinkLabel lnkForgotPassword;
        private Button btnLogin;
        private Label lblStatus;
        private PictureBox picLogo;
        private Label label1;
        private IconButton btnClose;
        private IconButton btnMinimize;
    }
}