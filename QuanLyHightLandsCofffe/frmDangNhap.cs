using System;
using System.Windows.Forms;

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
            // Example username and password for validation
            string username = "admin";
            string password = "123";

            // Get input from text boxes
            string enteredUsername = txttenDN.Text;
            string enteredPassword = txtMatkhau.Text; // Assume you have a txtMatKhau textbox for the password

            // Check the username and password
            if (enteredUsername == username && enteredPassword == password)
            {
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
            frmDangki f =  new frmDangki();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }
    }
}
