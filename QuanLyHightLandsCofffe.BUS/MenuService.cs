using QuanLyHightLandsCofffe.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyHightLandsCofffe.BUS
{
    public class MenuService
    {
        Model1 context = new Model1();
        private static MenuService instance;

        public static MenuService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MenuService();
                }
                return instance;
            }
        }



        public List<Menu> GetAllMenuItems()
        {
            return context.Menus.ToList();
        }

        // Thêm món ăn mới
        public void AddMenu(string name, string description, float price, int categoryId)
        {
            var menu = new Menu
            {
                Name = name,
                Description = description,
                Price = price,
                CategoryId = categoryId
            };

            context.Menus.Add(menu);
            context.SaveChanges();
        }

        public void UpdateMenuItem(int id, Menu updatedMenuItem)
        {
            var menuItem = context.Menus.Find(id);
            if (menuItem != null)
            {
                menuItem.Name = updatedMenuItem.Name;
                menuItem.Description = updatedMenuItem.Description;
                menuItem.Price = updatedMenuItem.Price;
                menuItem.CategoryId = updatedMenuItem.CategoryId;
                context.SaveChanges();
            }
        }

        public void DeleteMenuItem(int id)
        {
            var menuItem = context.Menus.Find(id);
            if (menuItem != null)
            {
                context.Menus.Remove(menuItem);
                context.SaveChanges();
            }
        }

        public Menu GetMenuItemById(int id)
        {
            return context.Menus.Find(id);
        }

        public List<Menu> GetMenuItemsByCategoryId(int categoryId)
        {
            return context.Menus.Where(m => m.CategoryId == categoryId).ToList();
        }

        public List<Menu> GetMenuItemsForBill(int tableId)
        {
            return (from bi in context.BillInfoes
                    join b in context.Bills on bi.idBill equals b.id
                    join f in context.Menus on bi.idMenu equals f.id
                    where b.idTable == tableId
                    select new Menu
                    {
                        id = f.id, // Giả sử Menu có thuộc tính id
                        Name = f.Name,
                        Price = f.Price,
                        // Không có Count và TotalPrice trong Menu, nếu cần có thể tính toán bên ngoài
                    }).ToList();
        }
    }
}
