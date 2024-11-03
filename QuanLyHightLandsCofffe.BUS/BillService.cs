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

        // Phương thức để lưu hóa đơn
        public void ConfirmBill(int billId)
        {
            var bill = context.Bills.Find(billId);
            if (bill != null)
            {
                bill.status = 1; // Đặt trạng thái là đã thanh toán
                context.SaveChanges();
            }
        }

        // Phương thức để lấy danh sách hóa đơn
        



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
        // Sửa phương thức lấy ID hóa đơn theo ID bàn
        public int GetBillIdByTableId(int tableId)
        {
            var bill = context.Bills.FirstOrDefault(b => b.idTable == tableId && b.status != 1); // Giả sử 1 là trạng thái đã thanh toán
            return bill?.id ?? 0; // Trả về ID hoặc 0 nếu không có
        }


       
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
