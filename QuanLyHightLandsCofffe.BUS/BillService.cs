using QuanLyHightLandsCofffe.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity; // Thêm dòng này nếu chưa có


namespace QuanLyHightLandsCofffe.BUS
{
    public class BillService
    {
        Model1 context = new Model1();
        private static BillService instance;
        public static BillService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BillService();
                }
                return instance;
            }
        }

        public List<Bill> GetAllBills()
        {
            return context.Bills.ToList();
        }

        public Bill GetBillById(int id)
        {
            return context.Bills.Find(id);
        }

        //Lấy Hóa Đơn Theo Trạng Thái
        public List<Bill> GetBillsByStatus(int status)
        {
            return context.Bills.Where(b => b.status == status).ToList();
        }

        public void AddBill(Bill bill)
        {
            context.Bills.Add(bill);
            context.SaveChanges();
        }

        public void UpdateBill(int id, Bill updatedBill)
        {
            var bill = context.Bills.Find(id);
            if (bill != null)
            {
                bill.dateCheckin = updatedBill.dateCheckin;
                bill.dateCheckOut = updatedBill.dateCheckOut;
                bill.idTable = updatedBill.idTable;
                bill.status = updatedBill.status;
                context.SaveChanges();
            }
        }

        // Phương thức xóa hóa đơn
        // Phương thức xóa hóa đơn
        public bool DeleteBill(int billId)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    // Tìm hóa đơn theo ID và bao gồm các bản ghi liên quan
                    var bill = context.Bills
                                      .Include(b => b.BillInfoes)
                                      .Include(b => b.Orders) // Bao gồm đơn hàng liên quan
                                      .SingleOrDefault(b => b.id == billId);

                    if (bill != null)
                    {
                        // Xóa tất cả các thông tin liên quan trong BillInfo
                        var billInfos = context.BillInfoes.Where(bi => bi.idBill == billId).ToList();
                        context.BillInfoes.RemoveRange(billInfos);

                        // Xóa tất cả các đơn hàng liên quan nếu có
                        var orders = context.Orders.Where(o => o.BillId == billId).ToList();
                        context.Orders.RemoveRange(orders);

                        // Lưu thay đổi trước
                        context.SaveChanges();

                        // Xóa hóa đơn
                        context.Bills.Remove(bill);
                        context.SaveChanges(); // Lưu thay đổi hóa đơn

                        transaction.Commit(); // Cam kết giao dịch
                        return true; // Xóa thành công
                    }
                    return false; // Hóa đơn không tồn tại
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Hoàn tác giao dịch nếu có lỗi
                    Console.WriteLine($"Lỗi khi xóa hóa đơn: {ex.Message}");
                    return false; // Có lỗi xảy ra
                }
            }
        }








        // Tạo hóa đơn mới
        //public int CreateBill(int tableId)
        //{
        //    // Kiểm tra xem bàn đã có hóa đơn hay chưa
        //    var existingBill = context.Bills.FirstOrDefault(b => b.idTable == tableId && b.status == 0);
        //    if (existingBill != null)
        //    {
        //        return -1; // Nếu đã có hóa đơn chưa thanh toán
        //    }

        //    var bill = new Bill
        //    {
        //        dateCheckin = DateTime.Now,
        //        idTable = tableId,
        //        status = 0 // 0: chưa thanh toán
        //    };

        //    context.Bills.Add(bill);
        //    context.SaveChanges();
        //    return bill.id; // Trả về ID của hóa đơn mới
        //}

        //public int CreateBill(int tableId, List<BillInfo> billInfos = null)
        //{
        //    using (var transaction = context.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            // Check if an unpaid bill exists for the table
        //            var existingBill = context.Bills
        //                .FirstOrDefault(b => b.idTable == tableId && b.status == 0);
        //            if (existingBill != null)
        //            {
        //                return -1; // Bill already exists, return -1 to indicate error
        //            }

        //            // Create new Bill
        //            var bill = new Bill
        //            {
        //                dateCheckin = DateTime.Now,
        //                idTable = tableId,
        //                status = 0 // 0: unpaid
        //            };
        //            context.Bills.Add(bill);
        //            context.SaveChanges(); // Save the Bill first to get its ID

        //            // If there are BillInfo records to add, link them to this Bill
        //            if (billInfos != null && billInfos.Any())
        //            {
        //                foreach (var billInfo in billInfos)
        //                {
        //                    billInfo.idBill = bill.id; // Set the foreign key to the newly created Bill
        //                    context.BillInfoes.Add(billInfo); // Add BillInfo to the context
        //                }
        //                context.SaveChanges(); // Save BillInfo records
        //            }

        //            transaction.Commit(); // Commit the transaction if everything is successful
        //            return bill.id; // Return the new Bill ID
        //        }
        //        catch (Exception ex)
        //        {
        //            transaction.Rollback(); // Rollback if there is any error
        //            Console.WriteLine($"Error creating Bill: {ex.Message}");
        //            return -1; // Indicate an error occurred
        //        }
        //    }
        //}

        public int CreateBill(int tableId)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var existingBill = context.Bills
                        .FirstOrDefault(b => b.idTable == tableId && b.status == 0);
                    if (existingBill != null)
                    {
                        Console.WriteLine($"Hóa đơn đã tồn tại cho bàn {tableId}");
                        return -1; // Đã tồn tại hóa đơn chưa thanh toán
                    }

                    var bill = new Bill
                    {
                        dateCheckin = DateTime.Now,
                        idTable = tableId,
                        status = 0 // 0: chưa thanh toán
                    };
                    context.Bills.Add(bill);
                    context.SaveChanges(); // Lưu hóa đơn
                    transaction.Commit(); // Cam kết giao dịch

                    Console.WriteLine($"Hóa đơn ID {bill.id} đã được tạo thành công cho bàn {tableId}");
                    return bill.id; // Trả về ID hóa đơn
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Hoàn tác giao dịch
                    Console.WriteLine($"Lỗi khi tạo hóa đơn: {ex.Message}");
                    return -1; // Thất bại
                }
            }
        }



        // Chuyển hóa đơn sang bàn khác
        public void ChangeTable(int billId, int newTableId)
        {
            var bill = context.Bills.Find(billId);
            if (bill != null)
            {
                bill.idTable = newTableId;
                context.SaveChanges();
            }
        }
    }
}
