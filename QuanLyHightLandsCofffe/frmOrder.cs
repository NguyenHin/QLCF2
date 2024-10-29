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
    public partial class frmOrder : Form
    {
        private List<string> cartItems = new List<string>();
        public frmOrder()
        {
            InitializeComponent();
            pictureBoxCoffee.Click += PictureBoxCoffee_Click;
            pictureBox1.Click += PictureBox1_Click;
            pictureBox2.Click += PictureBox2_Click;
            pictureBoxTea.Click += PictureBoxTea_Click;
            pictureBoxFreeze.Click += PictureBoxFreeze_Click;
            pictureBoxCake.Click += PictureBoxCake_Click;
            pictureBoxCombo.Click += PictureBoxCombo_Click;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ ComboBox và NumericUpDown cho trà
            string teaType = comboBoxTea.SelectedItem?.ToString();
            int quantity = (int)numericUpDownTeaQuantity.Value;
            // Tạo chuỗi hiển thị cho sản phẩm trong giỏ hàng
            string cartItem = $"{quantity} x {teaType}";
            // Thêm sản phẩm vào danh sách giỏ hàng
            cartItems.Add(cartItem);
            // Hiển thị sản phẩm trong ListBox
            listBoxCart.Items.Add(cartItem);
            // Hiển thị thông báo xác nhận
            MessageBox.Show($"{cartItem} đã được thêm vào giỏ hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonAddToCart_Click(object sender, EventArgs e)
        {
            string coffeeType = comboBoxCoffee.SelectedItem?.ToString();
            int quantity = (int)numericUpDownQuantity.Value;
            string cartItem = $"{quantity} x {coffeeType}";
            cartItems.Add(cartItem);
            listBoxCart.Items.Add(cartItem);
            MessageBox.Show($"{cartItem} đã được thêm vào giỏ hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonAddFreezeToCart_Click(object sender, EventArgs e)
        {
            string freezeType = comboBoxFreeze.SelectedItem?.ToString();
            int quantity = (int)numericUpDownFreezeQuantity.Value;
            string cartItem = $"{quantity} x {freezeType}";
            cartItems.Add(cartItem);
            listBoxCart.Items.Add(cartItem);
            MessageBox.Show($"{cartItem} đã được thêm vào giỏ hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonAddCakeToCart_Click(object sender, EventArgs e)
        {
            string cakeType = comboBoxCake.SelectedItem?.ToString();
            int quantity = (int)numericUpDownCakeQuantity.Value;
            string cartItem = $"{quantity} x {cakeType}";
            cartItems.Add(cartItem);
            listBoxCart.Items.Add(cartItem);
            MessageBox.Show($"{cartItem} đã được thêm vào giỏ hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonAddComboToCart_Click(object sender, EventArgs e)
        {
            string comboType = comboBoxCombo.SelectedItem?.ToString();
            int quantity = (int)numericUpDownComboQuantity.Value;
            string cartItem = $"{quantity} x {comboType}";
            cartItems.Add(cartItem);
            listBoxCart.Items.Add(cartItem);
            MessageBox.Show($"{cartItem} đã được thêm vào giỏ hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            comboBoxCoffee.Text = "Cà phê đen";
        }

        private void PictureBoxCoffee_Click(object sender, EventArgs e)
        {
            comboBoxCoffee.Text = "Cà phê sữa";
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            comboBoxCoffee.Text = "Cappuccino";
        }

        private void PictureBoxTea_Click(object sender, EventArgs e)
        {
            comboBoxTea.Text = "Trà Sen Vàng";
        }
        private void PictureBoxFreeze_Click(object sender, EventArgs e)
        {
            comboBoxFreeze.Text = "Freeze Matcha";
        }

        private void pictureBoxFreeze1_Click(object sender, EventArgs e)
        {
            comboBoxFreeze.Text = "Freeze Socola";
        }

        private void PictureBoxCake_Click(object sender, EventArgs e)
        {
            comboBoxCake.Text = "Bánh Tiramisu";
        }

        private void PictureBoxCombo_Click(object sender, EventArgs e)
        {
            comboBoxCombo.Text = "Combo Hứng Khởi";
        }

        private void pictureBoxCombo1_Click(object sender, EventArgs e)
        {
            comboBoxCombo.Text = "Combo Chuyện Trò";
        }

        private void pictureBoxFreeze2_Click(object sender, EventArgs e)
        {
            comboBoxFreeze.Text = "Caramel Phin Freeze";
        }

        private void pictureBoxCake_Click_1(object sender, EventArgs e)
        {
            comboBoxCake.Text = "Bánh Mì Que Pate";
        }
    }
 }