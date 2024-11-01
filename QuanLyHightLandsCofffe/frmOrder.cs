using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuanLyHightLandsCofffe
{
    public partial class frmOrder : Form
    {
        private List<string> drinks = new List<string>();

        public frmOrder()
        {
            InitializeComponent();
        }
        private void btnFreezeTra_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Freeze trà xanh";
        }

        private void btnFreezeS_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Freeze Socola";
        }

        private void btnCaramel_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Carmel Phin Freeze";
        }

        private void btnClassic_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Classic Phin Freeze";
        }

        private void btnCookies_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Cookies & Cream";
        }

        private void btnth_Click(object sender, EventArgs e)
        {
            string newDrink = textBox1.Text.Trim();
            int quantity = (int)numericUpDown1.Value; // Lấy số lượng từ NumericUpDown
  
            if (!string.IsNullOrEmpty(newDrink) && quantity > 0)
            {
                AddDrink(newDrink, quantity);
                textBox1.Clear();
                numericUpDown1.Value = 1; 
            }
            else
            {
                MessageBox.Show("Vui lòng nhập tên đồ uống và số lượng hợp lệ.");
            }
        }

        private void btns_Click(object sender, EventArgs e)
        {
            if (lstDrinks.SelectedItem != null && !string.IsNullOrEmpty(textBox1.Text))
            {
                string selectedDrink = lstDrinks.SelectedItem.ToString().Split(' ')[0];
                int quantity = (int)numericUpDown1.Value;
                drinks.Remove(lstDrinks.SelectedItem.ToString());
                AddDrink(textBox1.Text.Trim(), quantity);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn đồ uống và nhập tên mới.");
            }
        }

        private void btnx_Click(object sender, EventArgs e)
        {

            if (lstDrinks.SelectedItem != null)
            {
                // Hiển thị hộp thoại xác nhận
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa đồ uống này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    drinks.Remove(lstDrinks.SelectedItem.ToString());
                    UpdateDrinkList();
                    MessageBox.Show("Đồ uống đã được xóa.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn đồ uống để xóa.");
            }

        }
        private void AddDrink(string drink, int quantity) 
        {
            drinks.Add($"{drink} ({quantity})"); 
       
            UpdateDrinkList();
        }
        private void UpdateDrinkList() 
        {
            lstDrinks.Items.Clear();
            foreach (var drink in drinks)
            {
                lstDrinks.Items.Add(drink);
            }
        }

        private void btncf_Click(object sender, EventArgs e)
        {
            textBox2.Text = "Cà phê ";
        }

        private void btncfsua_Click(object sender, EventArgs e)
        {
            textBox2.Text = " Cà phê sữa";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string newDrink = textBox2.Text.Trim();
            int quantity = (int)numericUpDown2.Value; // Lấy số lượng từ NumericUpDown

            if (!string.IsNullOrEmpty(newDrink) && quantity > 0)
            {
                AddDrink(newDrink, quantity);
                textBox2.Clear();
                numericUpDown2.Value = 1;
            }
            else
            {
                MessageBox.Show("Vui lòng nhập tên đồ uống và số lượng hợp lệ.");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (lstDrinks.SelectedItem != null && !string.IsNullOrEmpty(textBox2.Text))
            {
                drinks.Remove(lstDrinks.SelectedItem.ToString());
                AddDrink(textBox2.Text.Trim(), (int)numericUpDown2.Value);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn đồ uống và nhập tên mới.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstDrinks.SelectedItem != null)
            {
                // Hiển thị hộp thoại xác nhận
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa đồ uống này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    drinks.Remove(lstDrinks.SelectedItem.ToString());
                    UpdateDrinkList();
                    MessageBox.Show("Đồ uống đã được xóa.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn đồ uống để xóa.");
            }
        }
    }
}
