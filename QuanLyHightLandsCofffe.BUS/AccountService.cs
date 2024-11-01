using QuanLyHightLandsCofffe.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyHightLandsCofffe.BUS
{
    public class AccountService
    {
        Model1 context = new Model1();
        private static AccountService instance;
        public static AccountService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AccountService();
                }
                return instance;
            }
        }

        // Lấy tất cả tài khoản
        public List<Account> GetAllAccounts()
        {
            return context.Accounts.ToList();
        }

        //private string HashPassword(string password)
        //{
        //    return BCrypt.Net.BCrypt.HashPassword(password);
        //}

        // Thêm tài khoản mới
        public void AddAccount(Account account)
        {
            //// Mã hóa mật khẩu
            //account.PassWord = HashPassword(account.PassWord);
            //context.Accounts.Add(account);
            //context.SaveChanges();

            // Lưu mật khẩu trực tiếp (không mã hóa)
            context.Accounts.Add(account);
            context.SaveChanges();
        }


        // Cập nhật tài khoản
        //public void UpdatePassword(string userName, string newPassword)
        //{
        //    var account = GetAccountByUserName(userName);
        //    if (account != null)
        //    {
        //        account.PassWord = HashPassword(newPassword);
        //        context.SaveChanges();
        //    }
        //}


        // Xóa tài khoản
        public void DeleteAccount(string userName)
        {
            var account = GetAccountByUserName(userName);
            if (account != null)
            {
                context.Accounts.Remove(account);
                context.SaveChanges();
            }
        }

        // Tìm tài khoản theo UserName
        public Account GetAccountByUserName(string userName)
        {
            return context.Accounts.FirstOrDefault(a => a.UserName == userName);
        }

        public bool ValidateUser(string userName, string password)
        {
            //var account = GetAccountByUserName(userName);
            //// Kiểm tra xem tài khoản có tồn tại và xác thực mật khẩu
            //return account != null && BCrypt.Net.BCrypt.Verify(password, account.PassWord);

            var account = GetAccountByUserName(userName);
            if (account != null)
            {
                // So sánh mật khẩu trực tiếp (không mã hóa)
                return account.PassWord == password; // So sánh thẳng
            }
            return false; // Tài khoản không tồn tại
        }

        // Kiem tra loai tai khoan
        public bool IsAdmin(string userName)
        {
            var account = GetAccountByUserName(userName);
            return account?.Type == 1; // 1: admin
        }
    }
}
