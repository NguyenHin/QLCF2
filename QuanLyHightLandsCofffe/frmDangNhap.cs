using QuanLyHightLandsCofffe.BUS;
using System;
using System.Windows.Forms;
using static QuanLyHightLandsCofffe.DAL.Entities.Model1;

namespace QuanLyHightLandsCofffe
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {

        }

        private void txttenDN_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDangnhap_Click(object sender, EventArgs e)
        {
            string enteredUsername = txttenDN.Text.Trim();
            string enteredPassword = txtMatkhau.Text.Trim();

            AccountService accountService = AccountService.Instance;

            if (accountService.ValidateUser(enteredUsername, enteredPassword))
            {
                UserSession.CurrentUserName = enteredUsername;

                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                frmChucNang f = new frmChucNang();
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Optionally handle other button clicks
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult q = MessageBox.Show("Bạn Có Muốn Thoát Không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (q.Equals(DialogResult.Yes))
            {
                this.Close();
            }
        }
    }
}
