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
using System.Configuration;
namespace QLKhachSan
{
    public partial class FormKhachHang : Form
    {
        string cnStr = @"server = .\SQLEXPRESS; database = QLKhachSan; integrated security = true;";
        SqlConnection cnn;
        List<KhachHang> lst = new List<KhachHang>();
        public FormKhachHang()
        {
            InitializeComponent();
        }

        private void FormKhachHang_Load(object sender, EventArgs e)
        {
            cnn = new SqlConnection(cnStr);
            try
            {
                Connection();
                LoadData();
                //dataGridView1_CellClick(sender, );
                Disconnection();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }
        }

        private void Connection()
        {
            try
            {
                if (cnn != null && cnn.State != ConnectionState.Open)
                {
                    cnn.Open();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                // throw;
            }
        }

        private void Disconnection()
        {
            try
            {
                if (cnn != null && cnn.State != ConnectionState.Closed)
                {
                    cnn.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                // throw;
            }
        }

        private void Init()
        {
            txtMaKH.Text = "";
            txtHoKH.Text = "";
            txtTenKH.Text = "";
            txtGioiTinhKH.Text = "";
            txtDiaChiKH.Text = "";
            txtSdtKH.Text = "";
            txtEmailKH.Text = "";
            txtQuocTichKH.Text = "";
        }

        private void LoadData()
        {
            string sql = "select * from KhachHang";
            SqlCommand cmd = new SqlCommand(sql, cnn);  //sql + cnStr de ket noi du lieu
            SqlDataReader dr = cmd.ExecuteReader();
            lst.Clear();
            while (dr.Read() == true)
            {
                KhachHang kh = new KhachHang();
                kh.MaKH = dr.GetString(0);
                kh.HoKH = dr.GetString(1);
                kh.TenKH = dr.GetString(2);
                kh.GioiTinhKH = dr.GetString(3);
                kh.DiaChiKH = dr.GetString(4);
                kh.SdtKH = dr.GetString(5);
                kh.EmailKH = dr.GetString(6);
                kh.QuocTichKH = dr.GetString(7);
                lst.Add(kh);
            }
            cmd.Dispose();  //giai phong bien cmd
            dr.Close();
            if (dataGridView1.DataSource != null)
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = lst;
            }
            dataGridView1.DataSource = lst;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                Connection();
                string sql = "insert into KhachHang values('" + txtMaKH.Text + "', N'" + txtHoKH.Text + "', N'" + txtTenKH.Text + "', N'" + txtGioiTinhKH.Text + "', N'" + txtDiaChiKH.Text + "', '" + txtSdtKH.Text + "', '" + txtEmailKH.Text + "', N'" + txtQuocTichKH.Text + "')";
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                LoadData();
                MessageBox.Show("Đã thêm thành công!");
                Disconnection();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                // throw;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                Connection();
                string sql = "DELETE FROM KhachHang WHERE IDKhachHang = N'" + txtMaKH.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                LoadData();
                MessageBox.Show("Đã xóa thành công!");
                Disconnection();
                Init();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                // throw;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                Connection();
                string sql = "UPDATE KhachHang SET Ho =  N'" + txtHoKH.Text + "', Ten = N'" + txtTenKH.Text + "', GioiTinh = N'" + txtGioiTinhKH.Text + "', DiaChi = N'" + txtDiaChiKH.Text + "', SDT = N'" + txtSdtKH.Text + "', Email = N'" + txtEmailKH.Text + "', QuocTich = N'" + txtQuocTichKH.Text + "' WHERE IDKhachHang = N'" + txtMaKH.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                LoadData();
                MessageBox.Show("Đã cập nhật thành công!");
                Disconnection();
                Init();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                // throw;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Init();
        }

        private void txtTimKiem_KeyUp(object sender, KeyEventArgs e)
        {
            dataGridView1.CurrentCell = null;
            if (txtTimKiem == null)
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Visible = true;
                }
            }
            else
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    if (dataGridView1.Rows[i].Cells[1].Value.ToString().ToLower().Contains(txtTimKiem.Text.ToLower()) == true
                        || dataGridView1.Rows[i].Cells[0].Value.ToString().ToLower().Contains(txtTimKiem.Text.ToLower()) == true
                        || dataGridView1.Rows[i].Cells[3].Value.ToString().ToLower().Contains(txtTimKiem.Text.ToLower()) == true)
                    {
                        dataGridView1.Rows[i].Visible = true;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Visible = false;
                    }
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtMaKH.Text = row.Cells[0].Value.ToString();
                txtHoKH.Text = row.Cells[1].Value.ToString();
                txtTenKH.Text = row.Cells[2].Value.ToString();
                txtGioiTinhKH.Text = row.Cells[3].Value.ToString();
                txtDiaChiKH.Text = row.Cells[4].Value.ToString();
                txtSdtKH.Text = row.Cells[5].Value.ToString();
                txtEmailKH.Text = row.Cells[6].Value.ToString();
                txtQuocTichKH.Text = row.Cells[7].Value.ToString();
            }
        }
    }
}
