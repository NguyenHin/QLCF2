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
            tableFoodService.TableUpdated += OnTableUpdated; // Đăng ký sự kiện
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

            // Lấy ID hóa đơn tương ứng với bàn
            currentBillId = billService.GetBillIdByTableId(item.id); // Phương thức giả định để lấy ID hóa đơn

            if (currentSelectedButton == btn)
            {
                currentSelectedButton = null;
                currentBillId = 0;
                lsBill.Items.Clear(); // Xóa danh sách cũ khi bỏ chọn bàn
            }
            else
            {
                currentSelectedButton = btn;
                currentSelectedButton.BackColor = Color.LightGreen;

                // Hiển thị hóa đơn cho bàn được chọn
                ShowBill(currentBillId); // Đảm bảo rằng gọi lại hàm này
            }
        }

        public void UpdateTableButton(int tableId, Color color)
        {
            var tableButton = flpTable.Controls.OfType<Button>().FirstOrDefault(b => (b.Tag as TableFood).id == tableId);
            if (tableButton != null)
            {
                tableButton.BackColor = color; // Cập nhật màu sắc nút bàn
            }
        }

        private void OnTableUpdated(TableFood updatedTable)
        {
            // Cập nhật trạng thái bàn
            var tableButton = flpTable.Controls.OfType<Button>().FirstOrDefault(b => (b.Tag as TableFood).id == updatedTable.id);
            if (tableButton != null)
            {
                tableButton.BackColor = updatedTable.status == "Trống" ? Color.Aqua : Color.LightPink;
                tableButton.Text = $"{updatedTable.Ten}{Environment.NewLine}{updatedTable.status}";
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


        //private void btnAdd_Click(object sender, EventArgs e)
        //{
        //    var selectedFood = cmbFood.SelectedItem as DAL.Entities.Menu;
        //    int count = (int)nmFoodCount.Value;

        //    if (selectedFood != null)
        //    {
        //        var selectedTableFood = currentSelectedButton.Tag as TableFood;
        //        if (selectedTableFood != null)
        //        {
        //            // Gọi hàm CreateBillInfo và nhận giá trị billId trả về
        //            int resultBillId = billInfoService.CreateBillInfo(selectedTableFood.id, selectedFood.id, count);

        //            // Kiểm tra kết quả trả về
        //            if (resultBillId > 0)
        //            {
        //                currentBillId = resultBillId; // Cập nhật currentBillId
        //                MessageBox.Show("Thêm món ăn vào hóa đơn thành công.");
        //                // Cập nhật trạng thái bàn
        //                tableFoodService.UpdateTableStatus(selectedTableFood.id, "Có người");
        //                currentSelectedButton.BackColor = Color.LightPink; // Màu cho bàn có người
        //                LoadTable();
        //            }
        //            else
        //            {
        //                MessageBox.Show("Thêm món ăn vào hóa đơn thất bại.");
        //            }

        //            ShowBill(currentBillId); // Cập nhật lại danh sách hóa đơn
        //        }
        //        else
        //        {
        //            MessageBox.Show("Không tìm thấy thông tin bàn.");
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Vui lòng chọn món ăn.");
        //    }
        //}

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var selectedFood = cmbFood.SelectedItem as DAL.Entities.Menu;
            int count = (int)nmFoodCount.Value;

            if (selectedFood != null)
            {
                var selectedTableFood = currentSelectedButton.Tag as TableFood;
                if (selectedTableFood != null)
                {
                    // Kiểm tra nếu có hóa đơn hiện tại
                    if (currentBillId == 0) // Nếu chưa có hóa đơn
                    {
                        // Tạo hóa đơn mới
                        currentBillId = billInfoService.CreateBillInfo(selectedTableFood.id, selectedFood.id, currentDiscount.id,  count);
                        if (currentBillId > 0)
                        {
                            MessageBox.Show("Tạo hóa đơn mới thành công.");
                        }
                        else
                        {
                            MessageBox.Show("Không thể tạo hóa đơn mới.");
                            return; // Kết thúc hàm nếu không tạo được hóa đơn
                        }
                    }
                    else
                    {
                        // Cập nhật thông tin hóa đơn
                        billInfoService.UpdateBillInfo(currentBillId, selectedFood.id, count);
                    }

                    // Cập nhật lại danh sách hóa đơn
                    ShowBill(currentBillId); // Gọi lại để hiển thị lại danh sách hóa đơn

                    // Cập nhật trạng thái bàn
                    tableFoodService.UpdateTableStatus(selectedTableFood.id, "Có người");
                    currentSelectedButton.BackColor = Color.LightPink; // Màu cho bàn có người
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin bàn.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn món ăn.");
            }
        }









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

            // Lấy ID hóa đơn của bàn hiện tại
            int oldBillId = currentBillId;

            if (MessageBox.Show($"Bạn có muốn chuyển bàn {selectedTable.Ten} đến bàn {newTableId}?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // Chuyển hóa đơn từ bàn cũ sang bàn mới
                if (oldBillId > 0)
                {
                    billService.ChangeTable(oldBillId, newTableId);
                }

                // Cập nhật trạng thái cho bàn cũ
                tableFoodService.UpdateTableStatus(selectedTable.id, "Trống"); // Bàn cũ trở thành trống
                currentSelectedButton.BackColor = Color.Aqua; // Đặt màu cho bàn cũ

                // Cập nhật trạng thái cho bàn mới
                var newTableButton = flpTable.Controls.OfType<Button>().FirstOrDefault(b => (b.Tag as TableFood).id == newTableId);
                if (newTableButton != null)
                {
                    newTableButton.BackColor = Color.LightPink; // Bàn mới có hóa đơn
                    var newTable = tableFoodService.GetTableById(newTableId);
                    if (newTable != null)
                    {
                        newTable.status = "Có người"; // Cập nhật trạng thái của bàn mới
                        tableFoodService.UpdateTable(newTableId, newTable); // Gọi phương thức cập nhật trạng thái bàn
                    }
                }

                // Reset ID hóa đơn và danh sách hóa đơn
                currentBillId = 0;
                lsBill.Items.Clear();

                // Tải lại danh sách bàn và cập nhật ComboBox
                LoadTable();
                LoadChangeTable();

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
            // Kiểm tra xem có hóa đơn cho bàn hiện tại không
            if (currentBillId <= 0)
            {
                MessageBox.Show("Bàn này chưa có hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng lại nếu không có hóa đơn
            }

            // Kiểm tra lại xem hóa đơn có tồn tại không trước khi mở frmHoaDon
            var bill = billService.GetBillById(currentBillId);
            if (bill == null)
            {
                MessageBox.Show("Hóa đơn đã bị xóa hoặc không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng lại nếu hóa đơn không tồn tại
            }

            // Lấy bàn hiện tại
            var selectedTable = currentSelectedButton.Tag as TableFood;
            if (selectedTable != null)
            {
                frmHoaDon hoaDonForm = new frmHoaDon(currentBillId, currentDiscount, selectedTable, this); // Truyền mã giảm giá và bàn hiện tại
                hoaDonForm.ShowDialog();
            }
        }

        #endregion

        
    }
}
