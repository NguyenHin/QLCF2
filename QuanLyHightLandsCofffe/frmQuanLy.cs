using QuanLyHightLandsCofffe.BUS;
using QuanLyHightLandsCofffe.DAL.Entities;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Windows.Forms;

namespace QuanLyHightLandsCofffe
{
    public partial class frmQuanLY : Form
    {
        private readonly BillService billService = BillService.Instance; // Sử dụng singleton
        private readonly PromotionService promotionService = PromotionService.Instance; // Singleton instance of BillInfoService
        private readonly TableFoodService tableFoodService = TableFoodService.Instance; // Singleton instance of BillInfoService
        private readonly MenuService menuService = MenuService.Instance; // Singleton instance of BillService

        Model1 context = new Model1();
        public frmQuanLY()
        {
            InitializeComponent();
        }
        private void BindGrid()
        {

        }
        private void frmQuanLY_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'qLCF_4DataSet.Promotion' table. You can move, or remove it, as needed.
            this.promotionTableAdapter.Fill(this.qLCF_4DataSet.Promotion);
            // TODO: This line of code loads data into the 'qLCF_4DataSet.Bill' table. You can move, or remove it, as needed.
            this.billTableAdapter.Fill(this.qLCF_4DataSet.Bill);
            LoadData();
        }

        private void LoadData()
        {
            // Tải dữ liệu vào bảng 'TaiKhoan'.

            // Gán dữ liệu vào DataGridView.
            //dgvDoanhthu.DataSource = this.qLHightLandsCFDataSet1.TaiKhoan;
            try
            {
                // Specify date range (could be based on UI elements like DateTimePicker controls)
                DateTime startDate = dateTimePicker1.Value;
                DateTime endDate = dateTimePicker2.Value;

                // Load bills within the specified date range
                var bills = context.Bills
                    .Where(b => b.status == 1
                                && DbFunctions.TruncateTime(b.dateCheckin) >= DbFunctions.TruncateTime(startDate)
                                && DbFunctions.TruncateTime(b.dateCheckOut) <= DbFunctions.TruncateTime(endDate))
                    .Select(b => new
                    {
                        b.id,
                        b.status,
                        EntryDate = b.dateCheckin,
                        ExitDate = b.dateCheckOut
                    })
                    .ToList();
                var bills = (
                    from  b in context.Bills
                    join p in context.Promotions on b.idPromotion equals p.id
                    where (b.status == 1
                                && DbFunctions.TruncateTime(b.dateCheckin) >= DbFunctions.TruncateTime(startDate)
                                && DbFunctions.TruncateTime(b.dateCheckOut) <= DbFunctions.TruncateTime(endDate))
                    select { b.id, b.dateCheckin, b.dateCheckOut, p.name}).
                                
                // Tạo DataTable để lưu kết quả
                DataTable dt = new DataTable();
                dt.Columns.Add("Ngay", typeof(DateTime));
                dt.Columns.Add("TongTien", typeof(decimal));

                // Chuyển đổi kết quả truy vấn sang DataTable
                foreach (var item in bills)
                {
                    dt.Rows.Add(item.Ngay, item.TongTien);
                }
                // Bind data to DataGridView
                dgvDoanhthu.DataSource = bills;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            
        }

        private void dgvDoanhthu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
