using QuanLyHightLandsCofffe.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyHightLandsCofffe.BUS
{
    public class BillInfoService
    {
        Model1 context = new Model1();
        private static BillInfoService instance;
        public static BillInfoService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BillInfoService();
                }
                return instance;
            }
        }

        //Them hoa don
        public void AddBillInfo(int billId, int menuId, int count)
        {

            var billInfo = new BillInfo
            {
                idBill = billId,
                idMenu = menuId,
                Count = count
            };

            context.BillInfoes.Add(billInfo);
            try
            {
                context.SaveChanges(); // Lưu thay đổi
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Lỗi khi lưu hóa đơn: {ex.InnerException?.Message ?? ex.Message}");
            }
        }



        // Lấy ID hóa đơn theo ID bàn
        public int GetBillIdByTableId(int tableId)
        {
            var existingBill = context.Bills.FirstOrDefault(b => b.idTable == tableId && b.status == 0);
            return existingBill?.id ?? -1; // Trả về -1 nếu không tìm thấy hóa đơn
        }

        //Hàm này để lấy thông tin chi tiết của một hóa đơn dựa trên ID của nó.
        public BillInfo GetBillInfoById(int id)
        {
            return context.BillInfoes.Find(id);
        }
        //Hàm này để lấy tất cả chi tiết hóa đơn theo ID hóa đơn.
        public List<BillInfo> GetBillInfosByBillId(int billId)
        {
            return context.BillInfoes.Where(bi => bi.idBill == billId).ToList();
        }
        // Phương thức lấy thông tin chi tiết hóa đơn theo ID hóa đơn và ID món ăn
        public BillInfo GetBillInfoByBillIdAndMenuId(int billId, int menuId)
        {
            return context.BillInfoes
                          .FirstOrDefault(b => b.idBill == billId && b.idMenu == menuId);
        }


        //Cap nhat hoa don
        //public void UpdateBillInfo(int id, int newCount)
        //{
        //    var billInfo = context.BillInfoes.Find(id);
        //    if (billInfo != null)
        //    {
        //        billInfo.Count = newCount; // Cập nhật số lượng mới
        //        context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
        //    }
        //}



        public void DeleteBillInfo(int id)
        {
            var billInfo = context.BillInfoes.Find(id);
            if (billInfo != null)
            {
                context.BillInfoes.Remove(billInfo);
                context.SaveChanges();
            }
        }

        // Lấy tổng số tiền cho hóa đơn
        public double CalculateTotal(int billId)
        {
            return context.BillInfoes
                .Where(b => b.idBill == billId)
                .Sum(b => b.Count * context.Menus.Find(b.idMenu).Price);
        }

        public double CalculateTotalAmount(int billId)
        {
            // Lấy danh sách các thông tin hóa đơn dựa trên billId
            var billInfos = context.BillInfoes.Where(b => b.idBill == billId).ToList();

            double totalAmount = 0;

            // Tính tổng số tiền
            foreach (var billInfo in billInfos)
            {
                var menuItem = context.Menus.Find(billInfo.idMenu); // Lấy món ăn tương ứng
                if (menuItem != null)
                {
                    totalAmount += billInfo.Count * menuItem.Price; // Tính thành tiền
                }
            }

            return totalAmount;
        }

        public void RemoveBillInfo(int billId, int menuId)
        {
            var billInfo = context.BillInfoes.FirstOrDefault(bi => bi.idBill == billId && bi.idMenu == menuId);
            if (billInfo != null)
            {
                context.BillInfoes.Remove(billInfo);
                context.SaveChanges();
            }
        }

        public int CreateBillInfo(int tableId, int menuItemId, int promotionId, int quantity)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    // Kiểm tra hóa đơn hiện có cho bàn
                    var existingBill = context.Bills.FirstOrDefault(b => b.idTable == tableId && b.status == 0);
                    int billId;

                    if (existingBill != null)
                    {
                        billId = existingBill.id; // Nếu hóa đơn đã tồn tại, lấy ID hóa đơn hiện có
                    }
                    else
                    {
                        // Tạo hóa đơn mới
                        var bill = new Bill
                        {
                            dateCheckin = DateTime.Now,
                            idTable = tableId,
                            status = 0 // 0: chưa thanh toán
                        };

                        context.Bills.Add(bill);
                        context.SaveChanges(); // Lưu hóa đơn để có ID
                        billId = bill.id; // Lấy ID hóa đơn mới tạo
                    }

                    // Kiểm tra món ăn
                    if (!context.Menus.Any(m => m.id == menuItemId))
                    {
                        Console.WriteLine($"Món ăn với ID {menuItemId} không tồn tại.");
                        return -1; // Trả về -1 nếu món ăn không tồn tại
                    }

                    // Thêm thông tin hóa đơn
                    var billInfo = new BillInfo
                    {
                        idBill = billId,
                        idMenu = menuItemId,
                       
                        Count = quantity
                    };

                    context.BillInfoes.Add(billInfo);
                    context.SaveChanges(); // Lưu thay đổi

                    transaction.Commit(); // Cam kết giao dịch

                    Console.WriteLine("Thêm món ăn vào hóa đơn thành công.");
                    return billId; // Trả về billId
                }
                catch (DbUpdateException ex)
                {
                    transaction.Rollback(); // Hoàn tác giao dịch
                    Console.WriteLine($"Lỗi khi thêm món vào hóa đơn: {ex.InnerException?.Message ?? ex.Message}");
                    return -1;
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Hoàn tác giao dịch
                    Console.WriteLine($"Lỗi không xác định: {ex.Message}");
                    return -1;
                }
            }
        }


        public void UpdateBillInfo(int billId, int menuId, int count)
        {
            // Kiểm tra nếu số lượng là âm và hóa đơn chưa có món nào
            if (count < 0 && !context.BillInfoes.Any(bi => bi.idBill == billId))
            {
                // Nếu chưa có món nào, không cho phép giảm số lượng
                return; // Hoặc có thể thông báo cho người dùng nếu cần
            }

            var billInfo = GetBillInfoByBillIdAndMenuId(billId, menuId);

            if (billInfo != null)
            {
                int newQuantity = billInfo.Count + count;

                // Nếu số lượng mới <= 0, xóa món khỏi BillInfo
                if (newQuantity <= 0)
                {
                    DeleteBillInfo(billInfo.id); // Xóa khỏi BillInfo

                    // Kiểm tra xem còn món nào trong hóa đơn không
                    var remainingItems = context.BillInfoes.Where(bi => bi.idBill == billId).ToList();
                    if (!remainingItems.Any())
                    {
                        // Nếu không còn món nào, xóa hóa đơn
                        var billToDelete = context.Bills.Find(billId);
                        if (billToDelete != null)
                        {
                            context.Bills.Remove(billToDelete);
                            context.SaveChanges(); // Lưu thay đổi để xóa hóa đơn
                        }
                    }
                }
                else
                {
                    billInfo.Count = newQuantity; // Cập nhật số lượng mới
                    context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
                }
            }
            else if (count > 0) // Nếu không tìm thấy BillInfo nhưng count > 0, thêm mới
            {
                var billInfoNew = new BillInfo
                {
                    idBill = billId,
                    idMenu = menuId,
                    Count = count
                };

                context.BillInfoes.Add(billInfoNew);
                context.SaveChanges(); // Lưu thay đổi
            }
        }






    }

}
