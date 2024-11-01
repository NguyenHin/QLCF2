using QuanLyHightLandsCofffe.BUS;
using QuanLyHightLandsCofffe.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyHightLandsCofffe
{
    public partial class frmHoaDon : Form
    {
        private int billId; // Store the bill ID
        private readonly BillService billService = BillService.Instance; // Singleton instance of BillService
        private readonly MenuService menuService = MenuService.Instance; // Singleton instance of BillService
        private readonly BillInfoService billInfoService = BillInfoService.Instance; // Singleton instance of BillInfoService
        private readonly PromotionService promotionService = PromotionService.Instance; // Singleton instance of BillInfoService
        private readonly TableFoodService tableFoodService = TableFoodService.Instance; // Singleton instance of BillInfoService
        
        private Promotion currentDiscount; // Lưu mã giảm giá hiện tại
        public double TotalAmount { get; private set; }
        public string CheckInDate { get; private set; }
        public string DiscountCode { get; private set; }
        public frmHoaDon(int billId, Promotion discount)
        {
            InitializeComponent();
            this.billId = billId; // Store the provided bill ID
            this.currentDiscount = discount; // Lưu mã giảm giá hiện tại
            LoadBillDetails(billId); // Load the bill details

        }

        private void LoadBillDetails(int billId)
        {
            var bill = billService.GetBillById(billId); // Lấy thông tin hóa đơn
            if (bill != null)
            {
                txtBan.Text = bill.idTable.ToString(); // Hiển thị số bàn
                txtNgay.Text = bill.dateCheckin.ToString("dd/MM/yyyy"); // Hiển thị ngày check-in

                double totalAmount = CalculateTotalAmount(billId); // Tính tổng tiền hóa đơn

                // Kiểm tra mã giảm giá
                double finalAmount;
                if (currentDiscount != null && currentDiscount.DiscountRate > 0)
                {
                    double discountAmount = totalAmount * (currentDiscount.DiscountRate / 100.0);
                    finalAmount = totalAmount - discountAmount;
                    txtGiamgia.Text = currentDiscount.DiscountRate.ToString("N0") + "%"; // Hiển thị tỷ lệ giảm giá
                }
                else
                {
                    txtGiamgia.Text = ""; // Trống nếu không có mã giảm giá
                    finalAmount = totalAmount; // Không giảm giá
                }

                txtTong.Text = finalAmount.ToString("N0") + "đ"; // Hiển thị tổng tiền sau giảm giá

                // Tải các món ăn trong hóa đơn
                LoadBillItems(billId);
            }
        }

        private void LoadBillItems(int billId)
        {
            // Clear existing items
            dataGridView1.Rows.Clear();

            var billInfos = billInfoService.GetBillInfosByBillId(billId); // Fetch bill details
            foreach (var billInfo in billInfos)
            {
                var menuItem = menuService.GetMenuItemById(billInfo.idMenu); // Get menu item details
                if (menuItem != null)
                {
                    // Add row to DataGridView
                    dataGridView1.Rows.Add(menuItem.Name, billInfo.Count, menuItem.Price.ToString("N0") + "đ", (menuItem.Price * billInfo.Count).ToString("N0") + "đ");
                }
            }
        }

        private double CalculateTotalAmount(int billId)
        {
            double total = 0;
            var billInfos = billInfoService.GetBillInfosByBillId(billId);
            foreach (var billInfo in billInfos)
            {
                var menuItem = menuService.GetMenuItemById(billInfo.idMenu);
                if (menuItem != null)
                {
                    total += menuItem.Price * billInfo.Count;
                }
            }
            return total;
        }

        private TableFood currentTable; // Biến này lưu thông tin bàn hiện tại

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các trường
            TotalAmount = double.Parse(txtTong.Text.Replace("đ", "").Trim());
            CheckInDate = txtNgay.Text;
            DiscountCode = txtGiamgia.Text;

            // Tạo đối tượng để lưu thông tin hóa đơn
            var billSummary = new
            {
                TableName = this.Owner is frmTacVu parentForm ?
                            (parentForm.Controls.Find("btn" + txtBan.Text, true).FirstOrDefault() as Button)?.Text ?? "Không xác định" :
                            "Không xác định",
                TotalAmount,
                CheckInDate,
                CheckOutDate = DateTime.Now.ToString("dd/MM/yyyy"),
                DiscountCode
            };

            // Lấy đối tượng cha
            var parent = this.Owner as frmQuanLY;
            if (parent != null)
            {
                parent.AddBillSummary(billSummary); // Gọi phương thức thêm hóa đơn
            }

            // Cập nhật trạng thái bàn
            if (currentTable != null)
            {
                tableFoodService.UpdateTableStatus(currentTable.id, "Trống"); // Cập nhật trạng thái bàn

                // Thay đổi màu sắc của nút bàn
                var selectedButton = parent.Controls.Find("btn" + currentTable.id, true).FirstOrDefault() as Button; // Tìm nút tương ứng với bàn
                if (selectedButton != null)
                {
                    selectedButton.BackColor = Color.Aqua; // Đặt màu cho bàn trống
                }
            }

            // Xóa hóa đơn và kiểm tra kết quả
            if (billService.DeleteBill(billId)) // Xóa hóa đơn bằng ID
            {
                MessageBox.Show("Hóa đơn đã thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi xóa hóa đơn. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Close(); // Đóng form hóa đơn sau khi xác nhận
        }
    }
    
}
