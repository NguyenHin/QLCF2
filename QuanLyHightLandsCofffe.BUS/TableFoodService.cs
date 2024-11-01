using QuanLyHightLandsCofffe.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyHightLandsCofffe.BUS
{
    public class TableFoodService
    {
        Model1 context = new Model1();
        private static TableFoodService instance;

        public static TableFoodService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TableFoodService();
                }
                return instance;
            }
            private set { instance = value; }
        }

        public static int TableWidth = 80;
        public static int TableHeight = 80;
        // Phương thức để tải danh sách bàn
        public List<TableFood> LoadTableList()
        {
            return context.TableFoods.ToList();
        }

        public void AddTable(TableFood table)
        {
            context.TableFoods.Add(table);
            context.SaveChanges();
        }

        public void UpdateTable(int id, TableFood updatedTable)
        {
            var table = context.TableFoods.Find(id);
            if (table != null)
            {
                table.Ten = updatedTable.Ten; // Cập nhật tên bàn
                table.status = updatedTable.status; // Cập nhật trạng thái bàn
                context.SaveChanges();
            }
        }

        public void DeleteTable(int id)
        {
            var table = context.TableFoods.Find(id);
            if (table != null)
            {
                context.TableFoods.Remove(table);
                context.SaveChanges();
            }
        }

        public TableFood GetTableById(int id)
        {
            return context.TableFoods.Find(id);
        }

        public List<TableFood> GetTablesByStatus(string status)
        {
            return context.TableFoods.Where(t => t.status == status).ToList();
        }

        //Lay tong so ban
        public int GetTotalTables()
        {
            return context.TableFoods.Count();
        }

        public void UpdateTableStatus(int tableId, string status)
        {
            var table = context.TableFoods.Find(tableId);
            if (table != null)
            {
                table.status = status;
                context.SaveChanges();
            }
        }
    }
}
