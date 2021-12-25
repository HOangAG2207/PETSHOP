﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms.VisualStyles;


namespace petStore
{
    public partial class fQuanLyChinh : Form
    {
        
        ConnectData dataTable = new ConnectData();
        
        public fQuanLyChinh()
        {
            InitializeComponent();
        }
        #region Biến toàn cục
        public static string HovaTen = "";
        string userDisplay = fDangNhap.user;
        bool flag = true;
        bool maximum = false;
        FormChuongTrinh.fTaiKhoanVaNhanVien TKvaNV = null;
        FormChuongTrinh.fAbout about = null;
        FormChuongTrinh.fHangHoa hanghoa = null;
        FormChuongTrinh.fLoaiHangHoa loaihanghoa = null;
        FormChuongTrinh.fKhachHang khachhang = null;
        FormChuongTrinh.fNhaCungCap nhacungcap = null;

        FormChuongTrinh.fShowLoaiHH showloaihh = null;
        FormChuongTrinh.fShowNhaCungCap showncc = null;
        FormChuongTrinh.fShowNhanVien shownv = null;

        FormChuongTrinh.fHoaDonBan HDban = null;
        #endregion
        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("T");
        }
        private void fQuanLyChinh_Load(object sender, EventArgs e)
        {
            DangNhap();
            //Application.VisualStyleState = VisualStyleState.NonClientAreaEnabled;
            timer1.Start();
            pnlLeft.Width = 250;
        }
        public void QuanTriVien()
        {
            label2.Text = "Quản trị viên: \n( " + HovaTen + " )";
        }
        public void NhanVien()
        {
            mnuAdmin.Visible = false;
            label2.Text = "Nhân viên: \n( " + HovaTen + " )";
        }
        byte[] text;
        private void DangNhap()
        {
            dataTable.OpenConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM TAIKHOAN tk, NHANVIEN nv WHERE tk.TENDANGNHAP = @TDN and tk.MANV = nv.MANV");
            cmd.Parameters.Add("@TDN", SqlDbType.VarChar).Value = userDisplay;
            dataTable.Fill(cmd);
            HovaTen = dataTable.Rows[0]["TENNV"].ToString();
            if (dataTable.Rows[0]["ANH"].ToString() != "")
            {
                text = (byte[])dataTable.Rows[0]["ANH"];
                pictureBox1.Image = chuyenBytethanhImage(text);
            }
            string quyenHan = dataTable.Rows[0]["QUYENHAN"].ToString();
                if (quyenHan == "ad")
                    QuanTriVien();
                else if (quyenHan == "nv")
                    NhanVien();
        }
        #region Thao tác với Menu admin
        private void mnuNhanVien_Click(object sender, EventArgs e)
        {
            TKvaNV = new FormChuongTrinh.fTaiKhoanVaNhanVien();
            TabCreating(tabControl1, "Thông tin nhân viên", TKvaNV);
            /*
            if (TKvaNV == null || TKvaNV.IsDisposed)
            {
                TKvaNV = new FormChuongTrinh.fTaiKhoanVaNhanVien();
                //TKvaNV.MdiParent = this;
                //TKvaNV.Show();
                TabCreating(tabControl1, "Thông tin nhân viên", TKvaNV);
            }
            else
            {
                TKvaNV.Activate();
            }
            */
        }
        private void mnuLoaiHangHoa_Click(object sender, EventArgs e)
        {
            loaihanghoa = new FormChuongTrinh.fLoaiHangHoa();
            TabCreating(tabControl1, "Thêm loại hàng hóa", loaihanghoa);
            /*
            if (loaihanghoa == null || loaihanghoa.IsDisposed)
            {
                loaihanghoa = new FormChuongTrinh.fLoaiHangHoa();
                loaihanghoa.MdiParent = this;
                loaihanghoa.Show();
            }
            else
            {
                loaihanghoa.Activate();
            }
            */
        }
        // Chuyển Byte sang dạng Ảnh
        private Image chuyenBytethanhImage(byte[] byteArr)
        {
            MemoryStream ms = new MemoryStream(byteArr);
            Image img = Image.FromStream(ms);
            return img;
        }
        #endregion
        #region Thao tác mới Menu Danh mục
        private void mnuHangHoa_Click(object sender, EventArgs e)
        {
            hanghoa = new FormChuongTrinh.fHangHoa();
            TabCreating(tabControl1, "Thông tin hàng hóa", hanghoa);
            /*
            if (hanghoa == null || hanghoa.IsDisposed)
            {
                hanghoa = new FormChuongTrinh.fHangHoa();
                hanghoa.MdiParent = this;
                hanghoa.Show();
            }
            else
            {
                hanghoa.Activate();
            }
            */
        }
        private void mnuThemKhachHang_Click(object sender, EventArgs e)
        {
            khachhang = new FormChuongTrinh.fKhachHang();
            TabCreating(tabControl1, "Thêm khách hàng", khachhang);
            /*
            if (khachhang == null || khachhang.IsDisposed)
            {
                khachhang = new FormChuongTrinh.fKhachHang();
                khachhang.MdiParent = this;
                khachhang.Show();
            }
            else
            {
                khachhang.Activate();
            }
            */
        }

        private void mnuThemNhaCungCap_Click(object sender, EventArgs e)
        {
            nhacungcap = new FormChuongTrinh.fNhaCungCap();
            TabCreating(tabControl1, "Thêm nhà cung cấp", nhacungcap);
            /*
            if (nhacungcap == null || nhacungcap.IsDisposed)
            {
                nhacungcap = new FormChuongTrinh.fNhaCungCap();
                nhacungcap.MdiParent = this;
                nhacungcap.Show();
            }
            else
            {
                nhacungcap.Activate();
            }
            */
        }
        private void mnuBanHang_Click(object sender, EventArgs e)
        {
            HDban = new FormChuongTrinh.fHoaDonBan();
            TabCreating(tabControl1, "Lập HD bán", HDban);
        }
        #endregion
        #region Thao tác với Menu Tra cứu
        private void mnuShowLoaiHH_Click(object sender, EventArgs e)
        {
            showloaihh = new FormChuongTrinh.fShowLoaiHH();
            TabCreating(tabControl1, "Tra cứu Loại hàng hóa", showloaihh);
        }
        private void mnuShowNCC_Click(object sender, EventArgs e)
        {
            showncc = new FormChuongTrinh.fShowNhaCungCap();
            TabCreating(tabControl1, "Tra cứu Nhà cung cấp", showncc);
        }
        private void mnuShowNhanVien_Click(object sender, EventArgs e)
        {
            shownv = new FormChuongTrinh.fShowNhanVien();
            TabCreating(tabControl1, "Tra cứu Nhân viên", shownv);
        }
        #endregion
        #region Thao tác với Menu Trợ giúp
        private void mnuThongTinPM_Click(object sender, EventArgs e)
        {
            about = new FormChuongTrinh.fAbout();
            TabCreating(tabControl1, "Thông tin phần mềm", about);
            /*
            if (about == null || about.IsDisposed)
            {
                about = new FormChuongTrinh.fAbout();
                about.MdiParent = this;
                about.Show();
            }
            else
                about.Activate();
            */
        }
        #endregion
        #region Thao tác với Form
        // button ẩn hiện panel Trái
        private void btnPnlShowHide_Click(object sender, EventArgs e)
        {
            if (!flag)
            {
                pnlLeft.Width = 250;
                monthCalendar1.Visible = true;
            }
            else
            {
                pnlLeft.Width = 50;
                monthCalendar1.Visible = false;
            }
            flag = !flag;
        }
        //button Thoát Form
        private void vbtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //button Maximum Form
        private void vbtnMaximum_Click(object sender, EventArgs e)
        {
            if (!maximum)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
            maximum = !maximum;
        }
        //button Minimize Form
        private void vbtnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        //button Đăng xuất
        private void vbtnDangXuat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
        #region tabcontrol
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            Graphics g = e.Graphics;
            Font drawFont = new Font("Arial", 10, FontStyle.Bold);
            g.FillRectangle(new SolidBrush(Color.PeachPuff), e.Bounds);
            e.Graphics.DrawString("x", drawFont, Brushes.DarkSlateGray, e.Bounds.Right - 15, e.Bounds.Top + 4);
            e.Graphics.DrawString(this.tabControl1.TabPages[e.Index].Text, e.Font, Brushes.DarkCyan, e.Bounds.Left + 1, e.Bounds.Top + 4);
            e.DrawFocusRectangle();
        }

        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < this.tabControl1.TabPages.Count; i++)
            {
                Rectangle r = tabControl1.GetTabRect(i);
                //Lấy tọa độ cho X
                Rectangle closeButton = new Rectangle(r.Right - 12, r.Top + 4, 9, 7);
                if (closeButton.Contains(e.Location))
                {
                    //if (MessageBox.Show("Would you like to Close this Tab ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                        this.tabControl1.TabPages.RemoveAt(i);
                        break;
                    //}
                }
            }
        }
        static int KiemTraTonTai(TabControl TabControlName, string TabName)
        {
            int temp = -1;
            for (int i = 0; i < TabControlName.TabPages.Count; i++)
            {
                if (TabControlName.TabPages[i].Text == TabName)
                {
                    temp = i;
                    break;
                }
            }
            return temp;
        }
        public void TabCreating(TabControl TabControl, string Text, Form Form)
        {
            int Index = KiemTraTonTai(TabControl, Text);
            if (Index >= 0)
            {
                TabControl.SelectedTab = TabControl.TabPages[Index];
                TabControl.SelectedTab.Text = Text;
            }
            else
            {
                TabPage TabPage = new TabPage { Text = Text };
                TabControl.TabPages.Add(TabPage);
                TabControl.SelectedTab = TabPage;
                Form.TopLevel = false;
                Form.Parent = TabPage;
                //  Form.MdiParent = this;
                Form.Show();
                Form.Dock = DockStyle.Fill;
            }
        }

        #endregion

    }
}
