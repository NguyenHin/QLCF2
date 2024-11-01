using QuanLyHightLandsCofffe.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyHightLandsCofffe.BUS
{
    public class FoodCategoryService
    {
        Model1 context = new Model1();
        private static FoodCategoryService instance;
        public static FoodCategoryService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FoodCategoryService();
                }
                return instance;
            }
        }

        public List<FoodCategory> GetAllFoodCategories()
        {
            return context.FoodCategories.ToList();
        }

        public void AddFoodCategory(FoodCategory category)
        {
            context.FoodCategories.Add(category);
            context.SaveChanges();
        }

        public void UpdateFoodCategory(int id, FoodCategory updatedCategory)
        {
            var category = context.FoodCategories.Find(id);
            if (category != null)
            {
                category.Ten = updatedCategory.Ten; // Cập nhật tên danh mục
                context.SaveChanges();
            }
        }

        public void DeleteFoodCategory(int id)
        {
            var category = context.FoodCategories.Find(id);
            if (category != null)
            {
                context.FoodCategories.Remove(category);
                context.SaveChanges();
            }
        }

        public FoodCategory GetFoodCategoryById(int id)
        {
            return context.FoodCategories.Find(id);

        }
    }
}
