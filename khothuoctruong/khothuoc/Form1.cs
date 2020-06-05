using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace khothuoc
{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        MyDatabase db = new MyDatabase();
        public Form1()
        {
            InitializeComponent();
        }
        private void refreshDataGridView()
        {
            grvthuoc.DataSource = db.getData("select IDThuoc,TenThuoc,LoaiThuoc,HSD from tblthuoc");
        }
        private int check(string st, string str, string txt)
        {
            string sql = "select COUNT(*) from " + str + " where " + st + " = '" + txt + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            int a = (int)cmd.ExecuteScalar();
            return a;
        }
        private Boolean checkAll()
        {
            if (txttenthuoc.Text.Equals("") | txtloaithuoc.Text.Equals("") | txthsd.Text.Equals("") | txtdang.Text.Equals("") | txtngaysx.Text.Equals("") | txtgia.Text.Equals(""))
                return true;
            return false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            refreshDataGridView();
        }

        private void grvthuoc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = grvthuoc.CurrentRow.Index;
            txttenthuoc.Text = grvthuoc.Rows[i].Cells[1].Value.ToString();
            txtloaithuoc.Text = grvthuoc.Rows[i].Cells[2].Value.ToString();
            txthsd.Text = grvthuoc.Rows[i].Cells[3].Value.ToString();

        }
         //string sql = "select .... like N'%" + txttenthuoc.Text + "%'";
         //   grvthuoc.DataSource = db.getData(sql);
        private void btntim_Click(object sender, EventArgs e)
        {
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (checkAll())
            {
                MessageBox.Show("Không để trống!", "Thông báo!");
            }
            else
            {
                conn = db.connected();
                conn.Open();
                if (check("TenThuoc", "tblthuoc", txttenthuoc.Text) == 0)
                {
                    string sql = "insert into tblthuoc values('" + txttenthuoc.Text + "','" + txtloaithuoc.Text + "','" + txtngaysx.Text + "','" + txthsd.Text + "','" + txtgia.Text + "','" + txtdang.Text + "')";
                    db.excute(sql);
                    refreshDataGridView();
                }
                else
                {
                    MessageBox.Show("Khoa Viện đã tồn tại!", "Thông báo!");
                }
                conn.Close();
            }
        }

        private void txthsd_TextChanged(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //int i = dgvNhapThuoc.CurrentRow.Index;
            //cboIDThuoc.Text = dgvNhapThuoc.Rows[i].Cells[0].Value.ToString();
            //txtIDDonNhap.Text = dgvNhapThuoc.Rows[i].Cells[1].Value.ToString();
            //txtNgayNhap.Text = dgvNhapThuoc.Rows[i].Cells[2].Value.ToString();
            //txtSoLuongNhap.Text = dgvNhapThuoc.Rows[i].Cells[3].Value.ToString();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void xoatext(Control ctr)
        {
            if (ctr is TextBox)
            {
                ctr.Text = string.Empty;
            }
            foreach (Control i in ctr.Controls)
            {
                xoatext(i);
            }
        }

        private void btbThem_Click(object sender, EventArgs e)
        {
            xoatext(this);
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            if (checkAll())
            {
                MessageBox.Show("Không được để trống!", "Thông báo");
            }
            else
            {
                conn = db.connected();
                conn.Open();
                if (check("IDDonNhap", "tblnhaphang", txtIDDonNhap.Text) == 0)
                {
                    String sql = "insert into tblnhaphang values('" + txtIDDonNhap.Text + "','" + cboIDThuoc.Text + "','" + txtSoLuongNhap.Text + "','" + txtNgayNhap.Text + "')";
                    db.excute(sql);
                    RefreshDataGridView();
                }
                else
                {
                    MessageBox.Show("don nhap nay da ton tai!", "Thong bao!");
                }
                conn.Close();
            }
        }
    }
    }
}
       
    

