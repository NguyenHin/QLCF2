using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace QuanLyHightLandsCofffe.DAL.Entities
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<BillInfo> BillInfoes { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<FoodCategory> FoodCategories { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Promotion> Promotions { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<TableFood> TableFoods { get; set; }
        public virtual DbSet<Waitstaff> Waitstaffs { get; set; }
        public static class UserSession
        {
            // Thuộc tính tĩnh để lưu trữ tên đăng nhập của người dùng hiện tại
            public static string CurrentUserName { get; set; }
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(e => e.Staffs)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bill>()
                .HasMany(e => e.BillInfoes)
                .WithRequired(e => e.Bill)
                .HasForeignKey(e => e.idBill)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bill>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Bill)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Reviews)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FoodCategory>()
                .HasMany(e => e.Menus)
                .WithRequired(e => e.FoodCategory)
                .HasForeignKey(e => e.CategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Menu>()
                .HasMany(e => e.BillInfoes)
                .WithRequired(e => e.Menu)
                .HasForeignKey(e => e.idMenu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Menu>()
                .HasMany(e => e.Reviews)
                .WithRequired(e => e.Menu)
                .HasForeignKey(e => e.FoodId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Promotion>()
                .HasMany(e => e.Bills)
                .WithOptional(e => e.Promotion)
                .HasForeignKey(e => e.idPromotion);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.Waitstaffs)
                .WithRequired(e => e.Staff)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TableFood>()
                .HasMany(e => e.Bills)
                .WithRequired(e => e.TableFood)
                .HasForeignKey(e => e.idTable)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TableFood>()
                .HasMany(e => e.Waitstaffs)
                .WithRequired(e => e.TableFood)
                .HasForeignKey(e => e.TableId)
                .WillCascadeOnDelete(false);
        }
    }
}
