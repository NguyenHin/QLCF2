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
            this.danhMụcToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.kháchHàngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tácVụToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Arial", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.taiToolStripMenuItem,
            this.adminToolStripMenuItem,
            this.tácVụToolStripMenuItem,
            this.danhMụcToolStripMenuItem1,
            this.kháchHàngToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1153, 52);
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
            this.taiToolStripMenuItem.Size = new System.Drawing.Size(214, 48);
            this.taiToolStripMenuItem.Text = "Tài Khoản";
            this.taiToolStripMenuItem.Click += new System.EventHandler(this.taiToolStripMenuItem_Click);
            // 
            // thôngTinTàiKhoảnToolStripMenuItem
            // 
            this.thôngTinTàiKhoảnToolStripMenuItem.Name = "thôngTinTàiKhoảnToolStripMenuItem";
            this.thôngTinTàiKhoảnToolStripMenuItem.Size = new System.Drawing.Size(456, 48);
            this.thôngTinTàiKhoảnToolStripMenuItem.Text = "Thông tin tài khoản";
            this.thôngTinTàiKhoảnToolStripMenuItem.Click += new System.EventHandler(this.thôngTinTàiKhoảnToolStripMenuItem_Click);
            // 
            // thoátToolStripMenuItem
            // 
            this.thoátToolStripMenuItem.Name = "thoátToolStripMenuItem";
            this.thoátToolStripMenuItem.Size = new System.Drawing.Size(456, 48);
            this.thoátToolStripMenuItem.Text = "Đăng xuất ";
            this.thoátToolStripMenuItem.Visible = false;
            this.thoátToolStripMenuItem.Click += new System.EventHandler(this.thoátToolStripMenuItem_Click);
            // 
            // adminToolStripMenuItem
            // 
            this.adminToolStripMenuItem.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adminToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("adminToolStripMenuItem.Image")));
            this.adminToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.adminToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.adminToolStripMenuItem.Name = "adminToolStripMenuItem";
            this.adminToolStripMenuItem.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.adminToolStripMenuItem.Size = new System.Drawing.Size(168, 48);
            this.adminToolStripMenuItem.Text = "Quản Lý";
            this.adminToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.adminToolStripMenuItem.Click += new System.EventHandler(this.adminToolStripMenuItem_Click_1);
            // 
            // danhMụcToolStripMenuItem1
            // 
            this.danhMụcToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("danhMụcToolStripMenuItem1.Image")));
            this.danhMụcToolStripMenuItem1.Name = "danhMụcToolStripMenuItem1";
            this.danhMụcToolStripMenuItem1.Size = new System.Drawing.Size(154, 48);
            this.danhMụcToolStripMenuItem1.Text = "Order";
            this.danhMụcToolStripMenuItem1.Click += new System.EventHandler(this.danhMụcToolStripMenuItem1_Click);
            // 
            // kháchHàngToolStripMenuItem
            // 
            this.kháchHàngToolStripMenuItem.Name = "kháchHàngToolStripMenuItem";
            this.kháchHàngToolStripMenuItem.Size = new System.Drawing.Size(252, 48);
            this.kháchHàngToolStripMenuItem.Text = "Khách Hàng";
            // 
            // tácVụToolStripMenuItem
            // 
            this.tácVụToolStripMenuItem.Name = "tácVụToolStripMenuItem";
            this.tácVụToolStripMenuItem.Size = new System.Drawing.Size(152, 48);
            this.tácVụToolStripMenuItem.Text = "Tác vụ";
            this.tácVụToolStripMenuItem.Click += new System.EventHandler(this.tácVụToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 55);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1153, 632);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // frmChucNang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1153, 681);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmChucNang";
            this.Text = "QUẢN LÍ HIGHTLANDS COFFEE";
            this.Load += new System.EventHandler(this.frmChucNang_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

            }

            #endregion

            private System.Windows.Forms.MenuStrip menuStrip1;
            private System.Windows.Forms.ToolStripMenuItem taiToolStripMenuItem;
            private System.Windows.Forms.ToolStripMenuItem thôngTinTàiKhoảnToolStripMenuItem;
            private System.Windows.Forms.ToolStripMenuItem thoátToolStripMenuItem;
            private System.Windows.Forms.ToolStripMenuItem adminToolStripMenuItem;
            private System.Windows.Forms.ToolStripMenuItem danhMụcToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem kháchHàngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tácVụToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}