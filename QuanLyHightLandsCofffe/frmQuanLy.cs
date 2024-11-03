using QuanLyHightLandsCofffe.BUS;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyHightLandsCofffe
{
    public partial class frmQuanLY : Form
    {
        private readonly BillService billService = BillService.Instance; // Sử dụng singleton
        private readonly PromotionService promotionService = PromotionService.Instance; // Singleton instance of BillInfoService
        private readonly TableFoodService tableFoodService = TableFoodService.Instance; // Singleton instance of BillInfoService
        private readonly MenuService menuService = MenuService.Instance; // Singleton instance of BillService


        public frmQuanLY()
        {
            InitializeComponent();
        }

        private void frmQuanLY_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            // Tải dữ liệu vào bảng 'TaiKhoan'.
            this.taiKhoanTableAdapter1.Fill(this.qLHightLandsCFDataSet1.TaiKhoan);

            // Gán dữ liệu vào DataGridView.
            dgvDoanhthu.DataSource = this.qLHightLandsCFDataSet1.TaiKhoan;
        }

        private void btntimac_Click(object sender, EventArgs e)
        {
            // Thực hiện tìm kiếm khi nút được nhấn
            SearchAccount();
        }

        private void txtTimm_TextChanged(object sender, EventArgs e)
        {
            // Có thể thực hiện tìm kiếm theo thời gian thực khi văn bản thay đổi
            SearchAccount();
        }

        private void SearchAccount()
        {
            string searchTerm = txtTimm.Text.Trim();
            DataView dv = new DataView(this.qLHightLandsCFDataSet1.TaiKhoan);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                try
                {
                    dv.RowFilter = $"Username LIKE '%{searchTerm}%'"; // Thay 'Username' bằng tên cột thích hợp.
                    MessageBox.Show($"Đã tìm kiếm với từ khóa: {searchTerm}"); // Thông báo từ khóa tìm kiếm.
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lọc dữ liệu: " + ex.Message);
                    return; // Thoát nếu có lỗi trong việc lọc.
                }
            }
            else
            {
                dv.RowFilter = string.Empty; // Hiển thị tất cả khi từ khóa trống.
            }

            dgvDoanhthu.DataSource = dv; // Cập nhật DataGridView với dữ liệu đã lọc.
            MessageBox.Show($"Số bản ghi tìm thấy: {dv.Count}"); // Thông báo số lượng bản ghi tìm thấy.
        }

        private void btnXoatk_Click(object sender, EventArgs e)
        {
            if (dgvDoanhthu.SelectedRows.Count > 0)
            {
                // Lấy hàng đã chọn.
                DataGridViewRow selectedRow = dgvDoanhthu.SelectedRows[0];
                int accountId = Convert.ToInt32(selectedRow.Cells["IdColumn"].Value); // Thay 'IdColumn' bằng tên cột ID thực tế của bạn.

                // Xác nhận việc xóa
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này không?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Gọi phương thức để xóa tài khoản khỏi cơ sở dữ liệu.
                    try
                    {
                        // Phương thức xóa tài khoản thực tế của bạn.
                        this.taiKhoanTableAdapter1.Delete(accountId);
                        LoadData(); // Tải lại dữ liệu để làm mới DataGridView.
                        MessageBox.Show("Tài khoản đã được xóa thành công.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xóa tài khoản: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một tài khoản để xóa.");
            }
        }

        public void AddBillSummary(dynamic billSummary)
        {
            // Thêm thông tin hóa đơn vào DataGridView
            dgvDoanhthu.Rows.Add(
                billSummary.TableName,
                billSummary.TotalAmount,
                billSummary.CheckInDate,
                billSummary.CheckOutDate,
                billSummary.DiscountCode
            );
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            
        }
    }
}
