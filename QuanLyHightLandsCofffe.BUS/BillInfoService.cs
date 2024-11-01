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



        //Lay chi tiet hos don
        public List<BillInfo> GetAllBillInfos()
        {
            return context.BillInfoes.ToList();
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
            using (var context = new Model1())
            {
                return context.BillInfoes
                              .FirstOrDefault(b => b.idBill == billId && b.idMenu == menuId);
            }
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
        public bool UpdateBillInfo(int id, int newCount)
        {
            var billInfo = context.BillInfoes.Find(id);
            if (billInfo != null)
            {
                billInfo.Count = newCount; // Update the new count
                context.SaveChanges(); // Save changes to the database
                return true; // Indicate success
            }
            return false; // Indicate failure if the item was not found
        }


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

        //public bool CreateBillInfo(int billId, int menuItemId, int quantity)
        //{
        //    try
        //    {
        //        // Ensure that the referenced Bill exists
        //        if (!context.Bills.Any(b => b.id == billId))
        //        {
        //            Console.WriteLine("Referenced Bill not found.");
        //            return false;
        //        }

        //        // Create and add new BillInfo
        //        var billInfo = new BillInfo
        //        {
        //            idBill = billId,
        //            idMenu = menuItemId, // Sử dụng idMenu thay cho menuItemId
        //            Count = quantity // Sử dụng Count thay cho quantity
        //                             // other properties can be set here if needed
        //        };
        //        context.BillInfoes.Add(billInfo);
        //        context.SaveChanges();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error creating BillInfo: {ex.Message}");
        //        return false;
        //    }
        //}
        public bool CreateBillInfo(int billId, int menuItemId, int quantity)
        {
            // Kiểm tra xem billId có tồn tại không
            if (!context.Bills.Any(b => b.id == billId))
            {
                Console.WriteLine($"Hóa đơn với ID {billId} không tồn tại.");
                return false; // Hóa đơn không tồn tại
            }

            // Kiểm tra xem menuItemId có tồn tại không
            if (!context.Menus.Any(m => m.id == menuItemId))
            {
                Console.WriteLine($"Món ăn với ID {menuItemId} không tồn tại.");
                return false; // Món ăn không tồn tại
            }

            var billInfo = new BillInfo
            {
                idBill = billId,
                idMenu = menuItemId,
                Count = quantity
            };

            context.BillInfoes.Add(billInfo);
            try
            {
                context.SaveChanges();
                return true; // Thêm thành công
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Lỗi khi thêm món vào hóa đơn: {ex.InnerException?.Message ?? ex.Message}");
                return false; // Thêm thất bại
            }
        }

    }

}
