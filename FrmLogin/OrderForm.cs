using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrmLogin
{
    public partial class OrderForm : Form
    {
        private SqlCommand cmd;
        private SqlDataAdapter da;
        SqlDataReader sdr;
        private DataSet ds;
        String NamaFoto = "";
        String Id = "";
        String Id2 = "";
        String Memberid = "";
        public String employeeid;

        Koneksi Konn = new Koneksi();

        void hapusFoto()
        {
            pictureBox1.Visible = false;
        }

        void Bersihkan()
        {
            textBox1.Text = "";
            textBox2.Text = "";

        }
            public OrderForm()
        {
            InitializeComponent();
            //btnOrder.Visible = false;
        }
        void tampilMenu()
        {
            SqlConnection conn = Konn.getKoneksi();
            try
            {
                conn.Open();
                cmd = new SqlCommand("Select * From MsMenu", conn);
                ds = new DataSet();
                da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[3].Visible = false;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception h)
            {
                MessageBox.Show(h.ToString());
            }
            finally
            {
                conn.Close();
            }
            

        }
        void getOrder()
        {
            btnOrder.Visible = true;

            SqlConnection conn = Konn.getKoneksi();
            try
            {
                conn.Open();
                cmd = new SqlCommand("SELECT dbo.OrderDetail.Id AS ID, dbo.MsMenu.Name, dbo.OrderDetail.Qty, dbo.MsMenu.Carbo, dbo.MsMenu.Protein, dbo.MsMenu.Price, dbo.MsMenu.Id AS Id1, cast(dbo.MsMenu.Price as bigint) * cast(dbo.OrderDetail.Qty as bigint) as total FROM dbo.OrderDetail INNER JOIN dbo.MsMenu ON dbo.OrderDetail.Menuid = dbo.MsMenu.Id", conn);
                ds = new DataSet();
                da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView2.DataSource = dt;
                dataGridView2.Columns[0].Visible = false;
                dataGridView2.Columns[6].Visible = false;

                int totalHarga = 0;

                for (int i = 0; i < dataGridView2.Rows.Count; ++i)
                {
                    totalHarga += Convert.ToInt32(dataGridView2.Rows[i].Cells[7].Value);
                }

                txtTotal.Text = totalHarga.ToString();


                int totalCarbo = 0;

                for (int i = 0; i < dataGridView2.Rows.Count; ++i)
                {
                    totalCarbo += Convert.ToInt32(dataGridView2.Rows[i].Cells[3].Value);
                }

                txtCarbo.Text = totalCarbo.ToString();


                int totalProtein = 0;

                for (int i = 0; i < dataGridView2.Rows.Count; ++i)
                {
                    totalProtein += Convert.ToInt32(dataGridView2.Rows[i].Cells[4].Value);
                }

                txtProtein.Text = totalProtein.ToString();

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception h)
            {
                MessageBox.Show(h.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
            
            
            private void OrderForm_Load(object sender, EventArgs e)
        {
            tampilMenu();
        }
            
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            PictureBox picture = pictureBox1;

            try
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["Name"].Value.ToString();               
                String path = "C:\\Users\\PHANTOM GAMING\\source\\repos\\FrmLogin\\FrmLogin\\images\\" + row.Cells["Photo"].Value.ToString();
                picture.ImageLocation = "C:\\Users\\PHANTOM GAMING\\source\\repos\\FrmLogin\\FrmLogin\\images\\" + row.Cells["Photo"].Value.ToString();
                Id = row.Cells["Id"].Value.ToString();
            }
            catch (Exception h)
            {
                MessageBox.Show(h.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "")
            {
                MessageBox.Show("Data belum lengkap!");
            }
            else
            {
                SqlConnection conn = Konn.getKoneksi();
                try
                {
                    cmd = new SqlCommand("insert into OrderDetail values('1','" + Id + "', '" + textBox2.Text + "', null) ", conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Insert data berhasil");
                    Bersihkan();
                    tampilMenu();
                }
                catch (Exception h)
                {
                    MessageBox.Show(h.ToString());
                }
                finally
                {
                    conn.Close();
                }
                getOrder();
                hapusFoto();
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Memberid = txtMember.Text.ToString();
            String lastOrderHeader = "";
            try
            {
                SqlConnection conn = Konn.getKoneksi();
                conn.Open();
                cmd = new SqlCommand("SELECT COUNT(*) FROM OrderHeader", conn);

                sdr = cmd.ExecuteReader();
                sdr.Read();
                if (sdr.HasRows)
                {
                    lastOrderHeader = sdr.GetInt32(0).ToString();
                }
                
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }




    
            String orderHeader = DateTime.Now.ToString("yyyyMMdd") + "000" + lastOrderHeader;

            MessageBox.Show(orderHeader.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Yakin akan menghapus Menu " + textBox1.Text + " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SqlConnection conn = Konn.getKoneksi();
                
                {
                    
                    cmd = new SqlCommand("delete from OrderDetail where id = '"+ Convert.ToInt32(Id2) + "' ", conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Hapus data berhasil");
                    getOrder();
                    Id2 = "";
                    hapusFoto();
                }

            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           ;

            try
            {
                DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];
                textBox1.Text = row.Cells["Name"].Value.ToString();
                textBox2.Text = row.Cells["Qty"].Value.ToString();
                Id2 = row.Cells["ID"].Value.ToString();
                MessageBox.Show(Id2.ToString());
            }
            catch (Exception h)
            {
                MessageBox.Show(h.ToString());
            }
        }
    }
}
