using QuanLyHightLandsCofffe.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyHightLandsCofffe.BUS
{
    public class PromotionService
    {
        Model1 context = new Model1();
        private static PromotionService instance;
        public static PromotionService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PromotionService();
                }
                return instance;
            }
        }
        // Phương thức lấy tất cả khuyến mãi
        public List<Promotion> GetAllPromotions()
        {
            return context.Promotions.ToList();
        }

        public Promotion GetActivePromotionForDate(DateTime date)
        {
            return context.Promotions
                .FirstOrDefault(p => p.StartDate <= date && p.EndDate >= date);
        }

        public double GetDiscountForBill(int billId)
        {
            // Lấy thông tin hóa đơn từ BillService
            var bill = BillService.Instance.GetBillById(billId); // Giả sử bạn đã có phương thức này

            if (bill == null)
                return 0; // Nếu không tìm thấy hóa đơn

            // Kiểm tra xem có khuyến mãi nào đang áp dụng không
            var currentPromotion = GetActivePromotionForDate(DateTime.Now);

            if (currentPromotion != null)
            {
                return currentPromotion.DiscountRate; // Trả về tỷ lệ giảm giá
            }

            return 0; // Không có giảm giá
        }

        public List<Promotion> GetActivePromotions()
        {
            return context.Promotions
                .Where(p => p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now)
                .ToList();
        }

    }
}
