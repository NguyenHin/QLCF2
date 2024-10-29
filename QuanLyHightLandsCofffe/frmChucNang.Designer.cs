namespace QuanLyHightLandsCofffe
{
    partial class frmChucNang
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

            #region Windows Form Designer generated code

            /// <summary>
            /// Required method for Designer support - do not modify
            /// the contents of this method with the code editor.
            /// </summary>
            private void InitializeComponent()
            {
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChucNang));
                this.menuStrip1 = new System.Windows.Forms.MenuStrip();
                this.taiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                this.thôngTinTàiKhoảnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                this.thoátToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                this.adminToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                this.panel1 = new System.Windows.Forms.Panel();
                this.flpTable = new System.Windows.Forms.FlowLayoutPanel();
                this.panel2 = new System.Windows.Forms.Panel();
                this.panel3 = new System.Windows.Forms.Panel();
                this.lsBill = new System.Windows.Forms.ListView();
                this.cmbCategory = new System.Windows.Forms.ComboBox();
                this.cmbFood = new System.Windows.Forms.ComboBox();
                this.btnAdd = new System.Windows.Forms.Button();
                this.nmFoodCount = new System.Windows.Forms.NumericUpDown();
                this.btnPay = new System.Windows.Forms.Button();
                this.btnDisCount = new System.Windows.Forms.Button();
                this.btnChangeTable = new System.Windows.Forms.Button();
                this.nmDisCount = new System.Windows.Forms.NumericUpDown();
                this.comboBox3 = new System.Windows.Forms.ComboBox();
                this.moẻToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                this.danhMụcToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
                this.menuStrip1.SuspendLayout();
                this.panel1.SuspendLayout();
                this.panel2.SuspendLayout();
                this.panel3.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.nmFoodCount)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.nmDisCount)).BeginInit();
                this.SuspendLayout();
                // 
                // menuStrip1
                // 
                this.menuStrip1.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
                this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
                this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.taiToolStripMenuItem,
            this.adminToolStripMenuItem,
            this.danhMụcToolStripMenuItem1,
            this.moẻToolStripMenuItem});
                this.menuStrip1.Location = new System.Drawing.Point(0, 0);
                this.menuStrip1.Name = "menuStrip1";
                this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
                this.menuStrip1.Size = new System.Drawing.Size(1122, 41);
                this.menuStrip1.TabIndex = 0;
                this.menuStrip1.Text = "menuStrip1";
                this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
                // 
                // taiToolStripMenuItem
                // 
                this.taiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.thôngTinTàiKhoảnToolStripMenuItem,
            this.thoátToolStripMenuItem});
                this.taiToolStripMenuItem.Name = "taiToolStripMenuItem";
                this.taiToolStripMenuItem.Size = new System.Drawing.Size(110, 37);
                this.taiToolStripMenuItem.Text = "Accout";
                this.taiToolStripMenuItem.Click += new System.EventHandler(this.taiToolStripMenuItem_Click);
                // 
                // thôngTinTàiKhoảnToolStripMenuItem
                // 
                this.thôngTinTàiKhoảnToolStripMenuItem.Name = "thôngTinTàiKhoảnToolStripMenuItem";
                this.thôngTinTàiKhoảnToolStripMenuItem.Size = new System.Drawing.Size(335, 42);
                this.thôngTinTàiKhoảnToolStripMenuItem.Text = "Thông tin tài khoản";
                // 
                // thoátToolStripMenuItem
                // 
                this.thoátToolStripMenuItem.Name = "thoátToolStripMenuItem";
                this.thoátToolStripMenuItem.Size = new System.Drawing.Size(335, 42);
                this.thoátToolStripMenuItem.Text = "Đăng xuất ";
                this.thoátToolStripMenuItem.Visible = false;
                this.thoátToolStripMenuItem.Click += new System.EventHandler(this.thoátToolStripMenuItem_Click);
                // 
                // adminToolStripMenuItem
                // 
                this.adminToolStripMenuItem.Name = "adminToolStripMenuItem";
                this.adminToolStripMenuItem.Size = new System.Drawing.Size(102, 37);
                this.adminToolStripMenuItem.Text = "Admin";
                this.adminToolStripMenuItem.Click += new System.EventHandler(this.adminToolStripMenuItem_Click_1);
                // 
                // panel1
                //
                this.btnAdd.Size = new System.Drawing.Size(98, 76);
                this.btnAdd.TabIndex = 2;
                this.btnAdd.Text = "Add Food";
                this.btnAdd.UseVisualStyleBackColor = true;
                // 
                // nmFoodCount
                // 
                this.nmFoodCount.Location = new System.Drawing.Point(442, 29);
                this.nmFoodCount.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
                this.nmFoodCount.Name = "nmFoodCount";
                this.nmFoodCount.Size = new System.Drawing.Size(82, 26);
                this.nmFoodCount.TabIndex = 3;
                this.nmFoodCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
                // 
                // btnPay
                // 
                this.btnPay.Location = new System.Drawing.Point(441, 7);
                this.btnPay.Name = "btnPay";
                this.btnPay.Size = new System.Drawing.Size(98, 76);
                this.btnPay.TabIndex = 0;
                this.btnPay.Text = "Pay";
                this.btnPay.UseVisualStyleBackColor = true;
                // 
                // btnDisCount
                // 
                this.btnDisCount.Location = new System.Drawing.Point(213, 3);
                this.btnDisCount.Name = "btnDisCount";
                this.btnDisCount.Size = new System.Drawing.Size(132, 48);
                this.btnDisCount.TabIndex = 1;
                this.btnDisCount.Text = "Apply DisCount";
                this.btnDisCount.UseVisualStyleBackColor = true;
                // 
                // btnChangeTable
                // 
                this.btnChangeTable.Location = new System.Drawing.Point(4, 0);
                this.btnChangeTable.Name = "btnChangeTable";
                this.btnChangeTable.Size = new System.Drawing.Size(132, 48);
                this.btnChangeTable.TabIndex = 1;
                this.btnChangeTable.Text = "Change Table";
                this.btnChangeTable.UseVisualStyleBackColor = true;
                // 
                // nmDisCount
                // 
                this.nmDisCount.Location = new System.Drawing.Point(213, 54);
                this.nmDisCount.Name = "nmDisCount";
                this.nmDisCount.Size = new System.Drawing.Size(132, 26);
                this.nmDisCount.TabIndex = 2;
                this.nmDisCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
                // 
                // comboBox3
                // 
                this.comboBox3.FormattingEnabled = true;
                this.comboBox3.Location = new System.Drawing.Point(4, 54);
                this.comboBox3.Name = "comboBox3";
                this.comboBox3.Size = new System.Drawing.Size(132, 28);
                this.comboBox3.TabIndex = 3;
                // 
                // moẻToolStripMenuItem
                // 
                this.moẻToolStripMenuItem.Name = "moẻToolStripMenuItem";
                this.moẻToolStripMenuItem.Size = new System.Drawing.Size(86, 37);
                this.moẻToolStripMenuItem.Text = "More";
                //
                this.panel1.Controls.Add(this.lsBill);
                this.panel1.Location = new System.Drawing.Point(566, 152);
                this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
                this.panel1.Name = "panel1";
                this.panel1.Size = new System.Drawing.Size(542, 564);
                this.panel1.TabIndex = 1;
                // 
                // flpTable
                // 
                this.flpTable.Location = new System.Drawing.Point(12, 59);
                this.flpTable.Name = "flpTable";
                this.flpTable.Size = new System.Drawing.Size(548, 750);
                this.flpTable.TabIndex = 2;
                // 
                // panel2
                // 
                this.panel2.Controls.Add(this.comboBox3);
                this.panel2.Controls.Add(this.nmDisCount);
                this.panel2.Controls.Add(this.btnChangeTable);
                this.panel2.Controls.Add(this.btnDisCount);
                this.panel2.Controls.Add(this.btnPay);
                this.panel2.Location = new System.Drawing.Point(566, 723);
                this.panel2.Name = "panel2";
                this.panel2.Size = new System.Drawing.Size(542, 86);
                this.panel2.TabIndex = 3;
                // 
                // panel3
                // 
                this.panel3.Controls.Add(this.nmFoodCount);
                this.panel3.Controls.Add(this.btnAdd);
                this.panel3.Controls.Add(this.cmbFood);
                this.panel3.Controls.Add(this.cmbCategory);
                this.panel3.Location = new System.Drawing.Point(567, 59);
                this.panel3.Name = "panel3";
                this.panel3.Size = new System.Drawing.Size(542, 86);
                this.panel3.TabIndex = 4;
                // 
                // lsBill
                // 
                this.lsBill.HideSelection = false;
                this.lsBill.Location = new System.Drawing.Point(4, 4);
                this.lsBill.Name = "lsBill";
                this.lsBill.Size = new System.Drawing.Size(535, 557);
                this.lsBill.TabIndex = 0;
                this.lsBill.UseCompatibleStateImageBehavior = false;
                // 
                // cmbCategory
                // 
                this.cmbCategory.FormattingEnabled = true;
                this.cmbCategory.Location = new System.Drawing.Point(3, 3);
                this.cmbCategory.Name = "cmbCategory";
                this.cmbCategory.Size = new System.Drawing.Size(290, 28);
                this.cmbCategory.TabIndex = 0;
                // 
                // cmbFood
                // 
                this.cmbFood.FormattingEnabled = true;
                this.cmbFood.Location = new System.Drawing.Point(3, 46);
                this.cmbFood.Name = "cmbFood";
                this.cmbFood.Size = new System.Drawing.Size(290, 28);
                this.cmbFood.TabIndex = 1;
                // 
                // btnAdd
                // 
                this.btnAdd.Location = new System.Drawing.Point(322, 3);
                this.btnAdd.Name = "btnAdd";
                // danhMụcToolStripMenuItem1
                // 
                this.danhMụcToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("danhMụcToolStripMenuItem1.Image")));
                this.danhMụcToolStripMenuItem1.Name = "danhMụcToolStripMenuItem1";
                this.danhMụcToolStripMenuItem1.Size = new System.Drawing.Size(114, 37);
                this.danhMụcToolStripMenuItem1.Text = "Order";
                this.danhMụcToolStripMenuItem1.Click += new System.EventHandler(this.danhMụcToolStripMenuItem1_Click);
                // 
                // frmChucNang
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.ClientSize = new System.Drawing.Size(1122, 821);
                this.Controls.Add(this.panel3);
                this.Controls.Add(this.panel2);
                this.Controls.Add(this.flpTable);
                this.Controls.Add(this.panel1);
                this.Controls.Add(this.menuStrip1);
                this.MainMenuStrip = this.menuStrip1;
                this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
                this.Name = "frmChucNang";
                this.Text = "QUẢN LÍ HIGHTLANDS COFFEE";
                this.menuStrip1.ResumeLayout(false);
                this.menuStrip1.PerformLayout();
                this.panel1.ResumeLayout(false);
                this.panel2.ResumeLayout(false);
                this.panel3.ResumeLayout(false);
                ((System.ComponentModel.ISupportInitialize)(this.nmFoodCount)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.nmDisCount)).EndInit();
                this.ResumeLayout(false);
                this.PerformLayout();

            }

            #endregion

            private System.Windows.Forms.MenuStrip menuStrip1;
            private System.Windows.Forms.ToolStripMenuItem taiToolStripMenuItem;
            private System.Windows.Forms.Panel panel1;
            private System.Windows.Forms.ToolStripMenuItem thôngTinTàiKhoảnToolStripMenuItem;
            private System.Windows.Forms.ToolStripMenuItem thoátToolStripMenuItem;
            private System.Windows.Forms.ToolStripMenuItem adminToolStripMenuItem;
            private System.Windows.Forms.ListView lsBill;
            private System.Windows.Forms.FlowLayoutPanel flpTable;
            private System.Windows.Forms.Panel panel2;
            private System.Windows.Forms.ComboBox comboBox3;
            private System.Windows.Forms.NumericUpDown nmDisCount;
            private System.Windows.Forms.Button btnChangeTable;
            private System.Windows.Forms.Button btnDisCount;
            private System.Windows.Forms.Button btnPay;
            private System.Windows.Forms.Panel panel3;
            private System.Windows.Forms.NumericUpDown nmFoodCount;
            private System.Windows.Forms.Button btnAdd;
            private System.Windows.Forms.ComboBox cmbFood;
            private System.Windows.Forms.ComboBox cmbCategory;
            private System.Windows.Forms.ToolStripMenuItem danhMụcToolStripMenuItem1;
            private System.Windows.Forms.ToolStripMenuItem moẻToolStripMenuItem;
        }
}