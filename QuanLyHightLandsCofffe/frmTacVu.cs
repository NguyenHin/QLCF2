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
    public partial class frmTacVu : Form
    {
        private readonly TableFoodService tableFoodService = TableFoodService.Instance; // Sử dụng singleton
        private readonly AccountService accountService = AccountService.Instance; // Sử dụng singleton
        private readonly BillService billService = BillService.Instance; // Sử dụng singleton
        private readonly BillInfoService billInfoService = BillInfoService.Instance; // Sử dụng singleton
        private readonly FoodCategoryService foodCategoryService = FoodCategoryService.Instance;
        private readonly MenuService menuService = MenuService.Instance;
        private readonly PromotionService promotionService = PromotionService.Instance; // Singleton instance of BillInfoService


        private int currentBillId; // Biến để lưu ID hóa đơn hiện tại
        private Button currentSelectedButton; // Biến để lưu nút bàn được chọn
        private Promotion currentDiscount; // Biến để lưu mã giảm giá hiện tại



        public frmTacVu()
        {
            InitializeComponent();
            LoadTable();
            LoadCategories(); // Tải danh sách loại món ăn
            LoadDiscountCodes(); // Tải danh sách mã giảm giá
            LoadChangeTable();
        }

        #region Method

        
        private void LoadTable()
        {
            List<TableFood> tableList = tableFoodService.LoadTableList();
            flpTable.Controls.Clear();

            foreach (TableFood item in tableList)
            {
                Button btn = CreateTableButton(item);
                flpTable.Controls.Add(btn);
            }
        }

        private Button CreateTableButton(TableFood item)
        {
            Button btn = new Button()
            {
                Width = TableFoodService.TableWidth,
                Height = TableFoodService.TableHeight,
                Text = $"{item.Ten}{Environment.NewLine}{item.status}",
                Tag = item,
                BackColor = item.status == "Trống" ? Color.Aqua : Color.LightPink
            };

            btn.Click += (sender, e) => OnTableButtonClick(btn, item);
            return btn;
        }

        private void OnTableButtonClick(Button btn, TableFood item)
        {
            if (currentSelectedButton != null)
            {
                currentSelectedButton.BackColor = currentSelectedButton.Text.Contains("Trống") ? Color.Aqua : Color.LightPink;
            }

            if (currentSelectedButton == btn)
            {
                currentSelectedButton = null;
                currentBillId = 0;
                lsBill.Items.Clear();
            }
            else
            {
                currentSelectedButton = btn;
                currentSelectedButton.BackColor = Color.LightGreen;
                currentBillId = item.id;
                ShowBill(currentBillId);
            }
        }

        void ShowBill(int id)
        {
            lsBill.Items.Clear(); // Xóa danh sách cũ

            var billInfos = billInfoService.GetBillInfosByBillId(id); // Lấy thông tin hóa đơn

            foreach (var billInfo in billInfos)
            {
                var menuItem = menuService.GetMenuItemById(billInfo.idMenu); // Lấy thông tin món ăn

                if (menuItem != null)
                {
                    // Kiểm tra giá món ăn
                    double price = menuItem.Price;
                    if (price <= 0)
                    {
                        MessageBox.Show($"Giá món '{menuItem.Name}' không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    ListViewItem lsvItem = new ListViewItem(menuItem.Name);
                    lsvItem.SubItems.Add(billInfo.Count.ToString());
                    lsvItem.SubItems.Add(price.ToString("N0") + "đ"); // Hiển thị đơn giá với định dạng
                    lsvItem.SubItems.Add((price * billInfo.Count).ToString("N0") + "đ"); // Tính thành tiền với định dạng

                    lsBill.Items.Add(lsvItem); // Thêm món vào danh sách
                }
            }
        }

        void LoadCategories()
        {
            var categories = foodCategoryService.GetAllFoodCategories(); // Sử dụng FoodCategoryService
            cmbCategory.DataSource = categories;
            cmbCategory.DisplayMember = "Ten"; // Tên hiển thị
            cmbCategory.ValueMember = "id"; // Giá trị
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int categoryId = (cmbCategory.SelectedItem as FoodCategory).id; // Lấy ID loại món ăn
            LoadFoodsByCategory(categoryId); // Tải món ăn theo loại
        }

        private void LoadFoodsByCategory(int categoryId)
        {
            var foods = menuService.GetMenuItemsByCategoryId(categoryId); // Lấy danh sách món ăn
            cmbFood.DataSource = foods;
            cmbFood.DisplayMember = "Name"; // Tên hiển thị
            cmbFood.ValueMember = "id"; // Giá trị
        }

        private void cmbDiscount_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lưu thông tin giảm giá hiện tại
            if (cmbDiscount.SelectedItem is Promotion selectedPromotion)
            {
                // Kiểm tra mã giảm giá có hợp lệ không
                if (selectedPromotion.StartDate <= DateTime.Now && selectedPromotion.EndDate >= DateTime.Now)
                {
                    // Lưu mã giảm giá
                    currentDiscount = selectedPromotion;
                }
                else
                {
                    currentDiscount = null;
                    cmbDiscount.SelectedItem = null; // Reset nếu mã không hợp lệ
                }
            }
            else
            {
                currentDiscount = null; // Không có mã nào được chọn
            }

        }



        void LoadDiscountCodes()
        {
            var discounts = promotionService.GetActivePromotions(); // Lấy danh sách mã giảm giá từ dịch vụ

            // Tạo một danh sách mới để chứa tùy chọn "None"
            var allDiscounts = new List<Promotion>
            {
                new Promotion { id = 0, Name = "None", DiscountRate = 0, StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue }
            };

            // Thêm các mã giảm giá hiện có
            allDiscounts.AddRange(discounts);

            // Cập nhật dữ liệu cho ComboBox
            cmbDiscount.DataSource = allDiscounts;
            cmbDiscount.DisplayMember = "Name"; // Tên hiển thị
            cmbDiscount.ValueMember = "id"; // Giá trị

            // Thiết lập giá trị mặc định cho ComboBox
            cmbDiscount.SelectedItem = allDiscounts[0]; // Chọn "None" làm giá trị mặc định
        }


      

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var selectedFood = cmbFood.SelectedItem as DAL.Entities.Menu;
            int count = (int)nmFoodCount.Value;
            Console.WriteLine($"currentBillId: {currentBillId}, selectedFoodId: {selectedFood?.id}");

            if (selectedFood != null)
            {
                // Kiểm tra nếu hóa đơn chưa được tạo
                if (currentBillId <= 0)
                {
                    CreateBill();
                }

                // Kiểm tra xem hóa đơn đã tồn tại
                if (currentBillId > 0)
                {
                    var existingBillInfo = billInfoService.GetBillInfoByBillIdAndMenuId(currentBillId, selectedFood.id);
                    if (existingBillInfo != null)
                    {
                        // Nếu món đã có, cập nhật số lượng
                        billInfoService.UpdateBillInfo(existingBillInfo.id, existingBillInfo.Count + count);
                    }
                    else
                    {
                        // Nếu món chưa có, thêm món mới vào hóa đơn
                        if (billInfoService.CreateBillInfo(currentBillId, selectedFood.id, count))
                        {
                            MessageBox.Show("Thêm món ăn vào hóa đơn thành công.");
                        }
                        else
                        {
                            MessageBox.Show("Thêm món ăn vào hóa đơn thất bại.");
                        }
                    }

                    ShowBill(currentBillId); // Cập nhật lại danh sách hóa đơn
                }
                else
                {
                    MessageBox.Show("Không thể tạo hóa đơn.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn món ăn.");
            }
        }

        private void CreateBill()
        {
            var selectedTable = currentSelectedButton.Tag as TableFood;
            if (selectedTable != null)
            {
                // Kiểm tra xem bàn ăn đã có hóa đơn chưa
                if (billService.GetBillsByStatus(0).Any(b => b.idTable == selectedTable.id))
                {
                    MessageBox.Show("Bàn này đã có hóa đơn chưa thanh toán.");
                    return; // Không tạo hóa đơn mới nếu đã có hóa đơn chưa thanh toán
                }

                currentBillId = billService.CreateBill(selectedTable.id);
                if (currentBillId <= 0)
                {
                    MessageBox.Show("Lỗi khi tạo hóa đơn cho bàn ăn.");
                }
            }
        }
        //private void CreateBillForTable(int tableId)
        //{
        //    try
        //    {
        //        // Tạo hóa đơn mới cho bàn và lấy ID hóa đơn
        //        currentBillId = billService.CreateBill(tableId);

        //        // Kiểm tra xem hóa đơn đã được tạo thành công
        //        if (currentBillId <= 0)
        //        {
        //            MessageBox.Show("Bàn đã có hóa đơn chưa thanh toán.");
        //        }
        //        else
        //        {
        //            // Cập nhật trạng thái bàn nếu hóa đơn được tạo thành công
        //            tableFoodService.UpdateTableStatus(tableId, "Có người");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Lỗi khi tạo hóa đơn: {ex.Message}");
        //    }
        //}

        private void btnChangeTable_Click(object sender, EventArgs e)
        {
            if (currentSelectedButton == null)
            {
                MessageBox.Show("Vui lòng chọn bàn trước!");
                return;
            }

            var selectedTable = currentSelectedButton.Tag as TableFood;
            if (selectedTable == null) return;

            int newTableId = (int)cmbChangeTable.SelectedValue;
            if (MessageBox.Show($"Bạn có muốn chuyển bàn {selectedTable.Ten} đến bàn {newTableId}?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // Lưu thông tin của bàn cũ
                var oldTable = tableFoodService.GetTableById(selectedTable.id);

                // Thực hiện chuyển bàn
                billService.ChangeTable(currentBillId, newTableId);

                // Cập nhật màu sắc cho bàn cũ
                currentSelectedButton.BackColor = (oldTable.status == "Trống") ? Color.Aqua : Color.LightPink;

                // Cập nhật bàn mới
                var newTableButton = flpTable.Controls.OfType<Button>().FirstOrDefault(b => (b.Tag as TableFood).id == newTableId);
                if (newTableButton != null)
                {
                    newTableButton.BackColor = Color.LightGreen; // Màu nền mới cho bàn đã chọn
                }

                currentSelectedButton = null; // Bỏ chọn bàn
                currentBillId = 0; // Reset ID hóa đơn
                lsBill.Items.Clear(); // Xóa danh sách hóa đơn
                LoadTable(); // Tải lại danh sách bàn
                MessageBox.Show("Đã chuyển bàn thành công.");
            }
        }

        void LoadChangeTable()
        {
            var availableTables = tableFoodService.GetTablesByStatus("Trống");
            cmbChangeTable.DataSource = availableTables;
            cmbChangeTable.DisplayMember = "Ten"; // Tên hiển thị
            cmbChangeTable.ValueMember = "id"; // Giá trị
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            if (currentBillId > 0)
            {
                frmHoaDon hoaDonForm = new frmHoaDon(currentBillId, currentDiscount); // Truyền mã giảm giá hiện tại
                hoaDonForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn bàn và tạo hóa đơn trước.");
            }
        }

        #endregion
    }
}
