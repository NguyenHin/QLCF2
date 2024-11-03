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
        private int currentBillId; // Khai báo currentBillId
        private readonly BillService billService = BillService.Instance; // Singleton instance of BillService
        private readonly MenuService menuService = MenuService.Instance; // Singleton instance of BillService
        private readonly BillInfoService billInfoService = BillInfoService.Instance; // Singleton instance of BillInfoService
        private readonly PromotionService promotionService = PromotionService.Instance; // Singleton instance of BillInfoService
        private readonly TableFoodService tableFoodService = TableFoodService.Instance; // Singleton instance of BillInfoService
        
        private Promotion currentDiscount; // Lưu mã giảm giá hiện tại
        private frmTacVu ownerForm; // Tham chiếu đến frmTacVu
        private TableFood currentTable; // Declare currentTable as a member variable

        public double TotalAmount { get; private set; }
        public string CheckInDate { get; private set; }
        public string DiscountCode { get; private set; }
        public frmHoaDon(int billId, Promotion discount, TableFood table, frmTacVu owner)
        {
            InitializeComponent();
            this.billId = billId; // Store the provided bill ID
            this.currentDiscount = discount; // Lưu mã giảm giá hiện tại
            LoadBillDetails(billId); // Load the bill details
            this.currentTable = table; // Assign the current table
            this.ownerForm = owner; // Gán owner
            this.currentBillId = billId; // Gán giá trị cho currentBillId
            tableFoodService.TableUpdated += OnTableUpdated;
        }

        private void OnTableUpdated(TableFood updatedTable)
        {
            if (updatedTable.id == currentTable.id)
            {
                // Cập nhật giao diện trong frmTacVu nếu bàn này được cập nhật
                ownerForm.UpdateTableButton(updatedTable.id, Color.Aqua);
            }
        }

        private void LoadBillDetails(int billId)
        {
            var bill = billService.GetBillById(billId); // Lấy thông tin hóa đơn
            if (bill != null)
            {
                // Set currentTable from the bill
                currentTable = tableFoodService.GetTableById(bill.idTable);

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



        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (currentBillId >= 0)
            {
                MessageBox.Show("Hóa đơn đã thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }

            billService.ConfirmBill(currentBillId, currentDiscount.id);
            MessageBox.Show("Hóa đơn đã được xác nhận.");
            tableFoodService.UpdateTableStatus(currentTable.id, "Trống");
            ownerForm.UpdateTableButton(currentTable.id, Color.Aqua);
        }




        private void btnQuaylai_Click(object sender, EventArgs e)
        {
            // Hủy đăng ký sự kiện
            tableFoodService.TableUpdated -= OnTableUpdated;

            this.Close(); // Đóng form hóa đơn
        }
    }
    
}
