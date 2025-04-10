﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using FontAwesome.Sharp;
using System.IO;
using arac_kiralama_satis_desktop.Methods;
using arac_kiralama_satis_desktop.Utils;
using System.Data;
using System.Collections.Generic;

namespace arac_kiralama_satis_desktop.Interfaces
{
    public partial class LoginPage : Form
    {
        private System.Windows.Forms.Timer? loginTimer = null;
        private Dictionary<TextBox, string> placeholderTexts = new Dictionary<TextBox, string>();
        private bool isDragging = false;
        private Point dragStartPoint;

        public LoginPage()
        {
            InitializeComponent();
            CustomizeComponents();

            string savedUsername = LoginMethods.LoadSavedUsername();
            if (!string.IsNullOrEmpty(savedUsername))
            {
                txtUsername.Text = savedUsername;
                txtUsername.ForeColor = Color.Black;
                chkRememberMe.Checked = true;
                txtPassword.Focus();
            }
        }

        private void CustomizeComponents()
        {
            txtPassword.PasswordChar = '\0';

            SetCustomPlaceholder(txtUsername, "Kullanıcı Adı");
            SetCustomPlaceholder(txtPassword, "Şifre");

            try
            {
                string projectPath = Application.StartupPath;
                string imagePath = Path.Combine(projectPath, "Images", "company_logo.png");

                if (File.Exists(imagePath))
                {
                    picLogo.Image = Image.FromFile(imagePath);
                    picLogo.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    MessageBox.Show("Logo dosyası bulunamadı: " + imagePath, "Uyarı",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Logo yüklenirken bir hata oluştu: " + ex.Message, "Hata",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            pnlUsername.BackColor = Color.White;
            pnlPassword.BackColor = Color.White;
            SetupInputPanel(pnlUsername, txtUsername);
            SetupInputPanel(pnlPassword, txtPassword);

            pnlUsername.Paint += (s, e) => DrawTextBoxBorder(s as Panel, e);
            pnlPassword.Paint += (s, e) => DrawTextBoxBorder(s as Panel, e);

            picUser.Image = IconChar.User.ToBitmap(Color.Gray, 24);
            picLock.Image = IconChar.Lock.ToBitmap(Color.Gray, 24);
            btnShowPassword.Image = IconChar.EyeSlash.ToBitmap(Color.Gray, 24);
            btnClose.IconChar = IconChar.Times;
            btnMinimize.IconChar = IconChar.WindowMinimize;

            ApplyRoundedCorners(btnLogin, 25);

            btnLogin.MouseEnter += (s, e) => btnLogin.BackColor = Color.FromArgb(24, 100, 200);
            btnLogin.MouseLeave += (s, e) => btnLogin.BackColor = Color.FromArgb(26, 115, 232);

            this.Paint += (s, e) =>
            {
                if (e != null && e.Graphics != null)
                {
                    CreateFormShadow(e.Graphics);
                }
            };

            this.Load += (s, e) =>
            {
                pnlUsername.BorderStyle = BorderStyle.None;
                pnlPassword.BorderStyle = BorderStyle.None;
                this.Invalidate(true);

                this.ActiveControl = null;
            };
        }

        #region Form Dragging

        private void PnlContent_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location;
            }
        }

        private void PnlContent_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - dragStartPoint.X, p.Y - dragStartPoint.Y);
            }
        }

        private void PnlContent_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void PnlLeft_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location;
            }
        }

        private void PnlLeft_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - dragStartPoint.X, p.Y - dragStartPoint.Y);
            }
        }

        private void PnlLeft_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        #endregion

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void SetCustomPlaceholder(TextBox textBox, string placeholderText)
        {
            placeholderTexts[textBox] = placeholderText;

            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = placeholderText;
                textBox.ForeColor = Color.Gray;
            }

            textBox.Enter += (s, e) =>
            {
                if (textBox.Text == placeholderText)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;

                    if (textBox == txtPassword)
                        textBox.PasswordChar = '•';
                }
            };

            textBox.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholderText;
                    textBox.ForeColor = Color.Gray;

                    if (textBox == txtPassword)
                        txtPassword.PasswordChar = '\0';
                }
            };
        }

        private void SetupInputPanel(Panel? panel, TextBox? textBox)
        {
            if (panel == null || textBox == null) return;

            panel.BackColor = Color.White;
            panel.Padding = new Padding(1);

            textBox.BorderStyle = BorderStyle.None;
            textBox.BackColor = Color.White;
            textBox.Font = new Font("Segoe UI", 12F);
            textBox.Location = new Point(40, (panel.Height - textBox.Height) / 2);

            textBox.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    BtnLogin_Click(this, EventArgs.Empty);
                }
            };

            textBox.Enter += (s, e) => { panel.Tag = "focused"; panel.Invalidate(); };
            textBox.Leave += (s, e) => { panel.Tag = null; panel.Invalidate(); };
        }

        private void DrawTextBoxBorder(Panel? panel, PaintEventArgs e)
        {
            if (panel == null || e == null || e.Graphics == null) return;

            Color borderColor;

            if (panel.Tag != null && panel.Tag.ToString() == "error")
            {
                borderColor = Color.FromArgb(220, 53, 69);
            }
            else if (panel.Tag != null && panel.Tag.ToString() == "focused")
            {
                borderColor = Color.FromArgb(26, 115, 232);
            }
            else
            {
                borderColor = Color.FromArgb(210, 210, 210);
            }

            float thickness = (panel.Tag != null) ? 2f : 1f;

            e.Graphics.Clear(panel.BackColor);

            Rectangle rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);

            using (Pen pen = new Pen(borderColor, thickness))
            {
                e.Graphics.DrawLine(pen, rect.Left, rect.Top, rect.Right, rect.Top);
                e.Graphics.DrawLine(pen, rect.Left, rect.Top, rect.Left, rect.Bottom);
                e.Graphics.DrawLine(pen, rect.Right, rect.Top, rect.Right, rect.Bottom);

                using (Pen bottomPen = new Pen(borderColor, thickness + 1))
                {
                    e.Graphics.DrawLine(bottomPen, rect.Left, rect.Bottom, rect.Right, rect.Bottom);
                }
            }
        }

        private void ApplyRoundedCorners(Control? control, int radius)
        {
            if (control != null)
            {
                control.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, control.Width, control.Height, radius, radius));
            }
        }

        private void CreateFormShadow(Graphics g)
        {
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect, Color.FromArgb(20, 0, 0, 0), Color.Transparent,
                LinearGradientMode.Horizontal))
            {
                g.FillRectangle(brush, rect);
            }
        }

        [System.Runtime.InteropServices.DllImport("Gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            bool usernameEmpty = string.IsNullOrWhiteSpace(txtUsername.Text) || txtUsername.Text == placeholderTexts[txtUsername];
            bool passwordEmpty = string.IsNullOrWhiteSpace(txtPassword.Text) || txtPassword.Text == placeholderTexts[txtPassword];

            if (usernameEmpty || passwordEmpty)
            {
                ShowError("Kullanıcı adı ve şifre boş bırakılamaz!");
                HighlightEmptyFields();
                return;
            }

            try
            {
                bool isValid = LoginMethods.VerifyLogin(txtUsername.Text, txtPassword.Text);

                if (isValid)
                {
                    DataRow userInfo = LoginMethods.GetUserInfo(txtUsername.Text);
                    if (userInfo != null)
                    {
                        int userID = Convert.ToInt32(userInfo["KullaniciID"]);

                        LoginMethods.UpdateLastLogin(userID);

                        LoginMethods.LogLoginAttempt(userID, true, LoginMethods.GetIPAddress());

                        if (chkRememberMe.Checked)
                        {
                            LoginMethods.SaveUserCredentials(txtUsername.Text);
                        }
                        else
                        {
                            LoginMethods.ClearSavedCredentials();
                        }

                        CurrentSession.UserID = userID;
                        CurrentSession.UserName = txtUsername.Text;
                        CurrentSession.FullName = $"{userInfo["Ad"]} {userInfo["Soyad"]}";
                        CurrentSession.RoleID = Convert.ToInt32(userInfo["RolID"]);
                        CurrentSession.RoleName = userInfo["RolAdi"].ToString();
                        if (userInfo["SubeID"] != DBNull.Value)
                        {
                            CurrentSession.BranchID = Convert.ToInt32(userInfo["SubeID"]);
                            CurrentSession.BranchName = userInfo["SubeAdi"].ToString();
                        }

                        ShowSuccess("Giriş başarılı! Yönlendiriliyorsunuz...");

                        loginTimer = new System.Windows.Forms.Timer();
                        loginTimer.Interval = 1500;
                        loginTimer.Tick += (s, args) =>
                        {
                            loginTimer.Stop();

                            MainPage mainForm = new MainPage();

                            mainForm.Show();
                            this.Hide();
                        };
                        loginTimer.Start();
                    }
                }
                else
                {
                    LoginMethods.LogLoginAttempt(null, false, LoginMethods.GetIPAddress());

                    ShowError("Kullanıcı adı veya şifre hatalı!");
                    ShakeForm();
                }
            }
            catch (Exception ex)
            {
                ShowError("Giriş işlemi sırasında bir hata oluştu: " + ex.Message);
            }
        }

        private void ShowSuccess(string message)
        {
            lblStatus.ForeColor = Color.FromArgb(40, 167, 69);
            lblStatus.Text = message;
        }

        private void ShowError(string message)
        {
            lblStatus.ForeColor = Color.FromArgb(220, 53, 69);
            lblStatus.Text = message;
        }

        private void HighlightEmptyFields()
        {
            bool usernameEmpty = string.IsNullOrWhiteSpace(txtUsername.Text) || txtUsername.Text == placeholderTexts[txtUsername];
            bool passwordEmpty = string.IsNullOrWhiteSpace(txtPassword.Text) || txtPassword.Text == placeholderTexts[txtPassword];

            if (usernameEmpty)
            {
                pnlUsername.Tag = "error";
                pnlUsername.Invalidate();
            }
            else
            {
                pnlUsername.Tag = null;
                pnlUsername.Invalidate();
            }

            if (passwordEmpty)
            {
                pnlPassword.Tag = "error";
                pnlPassword.Invalidate();
            }
            else
            {
                pnlPassword.Tag = null;
                pnlPassword.Invalidate();
            }
        }

        private void ShakeForm()
        {
            Point originalLocation = this.Location;
            int shakeDistance = 10;
            int shakeSpeed = 80;

            for (int i = 0; i < 6; i++)
            {
                int direction = (i % 2 == 0) ? 1 : -1;
                this.Location = new Point(originalLocation.X + (shakeDistance * direction), originalLocation.Y);
                System.Threading.Thread.Sleep(shakeSpeed);
            }

            this.Location = originalLocation;
        }

        private void BtnShowPassword_MouseDown(object sender, MouseEventArgs e)
        {
            if (txtPassword.Text != placeholderTexts[txtPassword])
            {
                txtPassword.PasswordChar = '\0';
                btnShowPassword.Image = IconChar.Eye.ToBitmap(Color.Gray, 24);
            }
        }

        private void BtnShowPassword_MouseUp(object sender, MouseEventArgs e)
        {
            if (txtPassword.Text != placeholderTexts[txtPassword])
            {
                txtPassword.PasswordChar = '•';
                btnShowPassword.Image = IconChar.EyeSlash.ToBitmap(Color.Gray, 24);
            }
        }

        private void LnkForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Şifre sıfırlama talebi için lütfen yönetici ile iletişime geçin. (admin@arackiralamasatis.com)",
                          "Şifremi Unuttum", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}