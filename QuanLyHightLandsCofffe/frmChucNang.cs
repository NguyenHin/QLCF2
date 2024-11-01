using QuanLyHightLandsCofffe.BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static QuanLyHightLandsCofffe.DAL.Entities.Model1;

namespace QuanLyHightLandsCofffe
{
    public partial class frmChucNang : Form
    {
        public frmChucNang()
        {
            InitializeComponent();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tàiToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void danhMụcToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void taiToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void danhMụcToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmOrder f =new frmOrder();
            f.ShowDialog();
            this.Show();
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void adminToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // Lấy tên đăng nhập hiện tại từ UserSession
            string currentUserName = UserSession.CurrentUserName;

            // Kiểm tra xem tài khoản có phải admin không
            if (AccountService.Instance.IsAdmin(currentUserName))
            {
                // Mở form quản lý nếu là admin
                frmQuanLY f = new frmQuanLY();
                f.ShowDialog();
            }
            else
            {
                // Thông báo nếu không phải admin
                MessageBox.Show("Chỉ tài khoản admin mới có quyền mở chức năng này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void thôngTinTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTaikhoan f = new frmTaikhoan();
            f.ShowDialog();
            this.Show();
        }

        private void tácVụToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTacVu f = new frmTacVu();
            f.ShowDialog();
            this.Show();

        }

        private void frmChucNang_Load(object sender, EventArgs e)
        {

        }
    }
}
