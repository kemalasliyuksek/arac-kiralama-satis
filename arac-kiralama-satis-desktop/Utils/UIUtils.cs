using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace arac_kiralama_satis_desktop.Utils
{
    public static class UIUtils
    {
        /// <summary>
        /// Panel kontrolü için yuvarlak köşeler oluşturur
        /// </summary>
        public static void ApplyRoundedCorners(Control control, int radius)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(0, 0, radius, radius, 180, 90);
                path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
                path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
                path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
                path.CloseFigure();

                control.Region = new Region(path);
            }
        }

        /// <summary>
        /// Controls a gölge efekti uygular
        /// </summary>
        public static void ApplyShadowEffect(Control control)
        {
            control.Paint += (s, e) =>
            {
                // Kontrol sınırlarını çiz
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (GraphicsPath path = new GraphicsPath())
                {
                    int radius = 10;
                    path.AddArc(0, 0, radius, radius, 180, 90);
                    path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
                    path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
                    path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
                    path.CloseFigure();

                    // Hafif bir kenar çizgisi
                    using (Pen pen = new Pen(Color.FromArgb(20, 0, 0, 0), 1))
                    {
                        e.Graphics.DrawPath(pen, path);
                    }
                }
            };

            // Kontrol boyutu değiştiğinde bölgeyi yeniden ayarla
            control.Resize += (s, e) =>
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    int radius = 10;
                    path.AddArc(0, 0, radius, radius, 180, 90);
                    path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
                    path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
                    path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
                    path.CloseFigure();

                    control.Region = new Region(path);
                }
            };
        }

        /// <summary>
        /// Butonlar için gölgeli efekt uygular
        /// </summary>
        public static void ApplyButtonStyle(Button button, Color primaryColor, Color hoverColor)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = primaryColor;
            button.ForeColor = Color.White;
            button.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            button.Cursor = Cursors.Hand;

            // Mouse üzerine gelince renk değişimi
            button.MouseEnter += (s, e) => button.BackColor = hoverColor;
            button.MouseLeave += (s, e) => button.BackColor = primaryColor;

            // Yuvarlak köşeli buton
            button.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (GraphicsPath path = new GraphicsPath())
                {
                    int radius = 5;
                    path.AddArc(0, 0, radius, radius, 180, 90);
                    path.AddArc(button.Width - radius, 0, radius, radius, 270, 90);
                    path.AddArc(button.Width - radius, button.Height - radius, radius, radius, 0, 90);
                    path.AddArc(0, button.Height - radius, radius, radius, 90, 90);
                    path.CloseFigure();

                    button.Region = new Region(path);
                }
            };
        }

        /// <summary>
        /// DataGridView kontrolünü özelleştirir
        /// </summary>
        public static void SetupDataGridView(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 250);
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(83, 107, 168);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.BackgroundColor = Color.White;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(49, 76, 143);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 40;
            dgv.RowTemplate.Height = 35;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgv.DefaultCellStyle.Padding = new Padding(5);
        }
    }
}