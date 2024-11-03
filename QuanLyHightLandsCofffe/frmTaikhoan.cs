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

namespace QuanLyHightLandsCofffe
{
    public partial class frmTaikhoan : Form
    {
        private readonly AccountService accountService = AccountService.Instance;
        public frmTaikhoan()
        {
            InitializeComponent();
            LoadAccounts();

        }

        private void frmTaikhoan_Load(object sender, EventArgs e)
        {
        //    // TODO: This line of code loads data into the 'qLHightLandsCFDataSet.TaiKhoan' table. You can move, or remove it, as needed.
        //    this.taiKhoanTableAdapter.Fill(this.qLHightLandsCFDataSet.TaiKhoan);

        }
        private void LoadAccounts()
        {
            // Load accounts into DataGridView
            dataGridView1.DataSource = accountService.GetAllAccounts();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Optional: Handle cell click event to fill text boxes with selected account info
            if (e.RowIndex >= 0)
            {
                var selectedRow = dataGridView1.Rows[e.RowIndex];
                txtTenDangNhap.Text = selectedRow.Cells["UserName"].Value.ToString();
                txtMatKhau.Text = string.Empty; // Do not show the current password for security
                txtMatKhauMoi.Text = string.Empty; // Reset new password field
                txtNhapLaiMK.Text = string.Empty; // Reset confirm password field
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text) ||
                string.IsNullOrWhiteSpace(txtMatKhau.Text) ||
                string.IsNullOrWhiteSpace(txtMatKhauMoi.Text) ||
                string.IsNullOrWhiteSpace(txtNhapLaiMK.Text))
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            if (txtMatKhauMoi.Text != txtNhapLaiMK.Text)
            {
                MessageBox.Show("New passwords do not match.");
                return;
            }

            // Validate current password
            if (!accountService.ValidateUser(txtTenDangNhap.Text, txtMatKhau.Text))
            {
                MessageBox.Show("Current password is incorrect.");
                return;
            }

            // Update password
            accountService.UpdatePassword(txtTenDangNhap.Text, txtMatKhauMoi.Text);
            MessageBox.Show("Password updated successfully.");

            // Reload accounts to refresh DataGridView
            LoadAccounts();
        }
    }
}
