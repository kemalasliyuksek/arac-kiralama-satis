using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using arac_kiralama_satis_desktop.Methods;
using arac_kiralama_satis_desktop.Utils;
using arac_kiralama_satis_desktop.Interfaces;
using FontAwesome.Sharp;

namespace arac_kiralama_satis_desktop.Controls
{
    public partial class BranchesControl : UserControl
    {
        private DataTable branchesTable;

        public event EventHandler BranchAdded;

        public BranchesControl()
        {
            InitializeComponent();

            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            UIUtils.SetupDataGridView(dgvBranches);
        }

        public void LoadData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                ErrorManager.Instance.LogInfo("Şubeler listesi yükleniyor", "BranchesControl.LoadData");
                branchesTable = BranchMethods.GetBranchesAsDataTable();

                dgvBranches.DataSource = branchesTable;

                if (dgvBranches.Columns.Count > 0)
                {
                    dgvBranches.Columns["SubeID"].Visible = false;
                    dgvBranches.Columns["SubeAdi"].Width = 150;
                    dgvBranches.Columns["Adres"].Width = 250;
                    dgvBranches.Columns["SehirPlaka"].Width = 80;
                    dgvBranches.Columns["Telefon"].Width = 120;
                    dgvBranches.Columns["Email"].Width = 180;
                    dgvBranches.Columns["AktifMi"].Width = 70;
                    dgvBranches.Columns["OlusturmaTarihi"].Width = 120;
                    dgvBranches.Columns["OlusturmaTarihi"].DefaultCellStyle.Format = "dd.MM.yyyy";
                }

                lblBranchesTitle.Text = $"Şube Listesi ({branchesTable.Rows.Count})";
                ErrorManager.Instance.LogInfo($"Şubeler listesi başarıyla yüklendi. Toplam {branchesTable.Rows.Count} şube.", "BranchesControl.LoadData");
            }
            catch (Exception ex)
            {
                string errorId = ErrorManager.Instance.HandleException(
                    ex,
                    "Şube verileri yüklenirken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.UI,
                    true); // Show to user

                // Hata zaten kullanıcıya gösterildiği için MessageBox'a gerek yok
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void SearchBranches(string searchText)
        {
            try
            {
                if (branchesTable == null) return;

                if (string.IsNullOrWhiteSpace(searchText))
                {
                    dgvBranches.DataSource = branchesTable;
                    lblBranchesTitle.Text = $"Şube Listesi ({branchesTable.Rows.Count})";
                    return;
                }

                string filter = "";

                filter = $"[Şube Adı] LIKE '%{searchText}%' OR " +
                         $"Adres LIKE '%{searchText}%' OR " +
                         $"[Şehir Plaka] LIKE '%{searchText}%' OR " +
                         $"Telefon LIKE '%{searchText}%' OR " +
                         $"Email LIKE '%{searchText}%'";

                DataView dv = branchesTable.DefaultView;
                dv.RowFilter = filter;

                lblBranchesTitle.Text = $"Şube Listesi ({dv.Count})";
                ErrorManager.Instance.LogInfo($"Şube araması yapıldı. Arama metni: '{searchText}', Sonuç: {dv.Count} şube", "BranchesControl.SearchBranches");
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.LogWarning(
                    $"Şube arama sırasında hata oluştu. Arama metni: '{searchText}', Hata: {ex.Message}",
                    "BranchesControl.SearchBranches");

                if (branchesTable != null)
                {
                    dgvBranches.DataSource = branchesTable;
                    lblBranchesTitle.Text = $"Şube Listesi ({branchesTable.Rows.Count})";
                }
            }
        }

        private void BtnAddBranch_Click(object sender, EventArgs e)
        {
            try
            {
                ErrorManager.Instance.LogInfo("Yeni şube ekleme formu açılıyor", "BranchesControl.BtnAddBranch_Click");
                BranchAddForm branchForm = new BranchAddForm();
                if (branchForm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                    BranchAdded?.Invoke(this, EventArgs.Empty);
                    ErrorManager.Instance.LogInfo("Yeni şube başarıyla eklendi", "BranchesControl.BtnAddBranch_Click");
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.HandleException(
                    ex,
                    "Şube ekleme formu açılırken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.UI,
                    true);
            }
        }

        private void BtnRefreshBranches_Click(object sender, EventArgs e)
        {
            try
            {
                ErrorManager.Instance.LogInfo("Şube listesi yenileniyor", "BranchesControl.BtnRefreshBranches_Click");
                LoadData();
                txtSearchBranches.Clear();
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.HandleException(
                    ex,
                    "Şube listesi yenilenirken bir hata oluştu",
                    ErrorSeverity.Error,
                    ErrorSource.UI,
                    true);
            }
        }

        private void TxtSearchBranches_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SearchBranches(txtSearchBranches.Text);
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.HandleException(
                    ex,
                    "Şube arama işlemi sırasında beklenmeyen bir hata oluştu",
                    ErrorSeverity.Warning,
                    ErrorSource.UI,
                    false);
            }
        }
    }
}