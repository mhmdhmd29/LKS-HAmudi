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
        String orderHeader = "";
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
            getMember();

            refresh();
            
            //btnOrder.Visible = false;
        }

        void refresh()
        {

            String lastOrderHeader = "";
            SqlConnection conn = Konn.getKoneksi();
            try
            {

                conn.Open();
                cmd = new SqlCommand("SELECT COUNT(*) FROM OrderHeader", conn);

                sdr = cmd.ExecuteReader();
                sdr.Read();
                if (sdr.HasRows)
                {
                    lastOrderHeader = sdr.GetInt32(0).ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            finally
            {
                orderHeader = DateTime.Now.ToString("yyyyMMdd") + "0" + lastOrderHeader;
                conn.Close();
            }
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
                cmd = new SqlCommand("SELECT dbo.OrderDetail.Id, dbo.MsMenu.Name, dbo.OrderDetail.Qty, dbo.MsMenu.Carbo, dbo.MsMenu.Protein, dbo.MsMenu.Price, dbo.MsMenu.Id AS Id1 FROM dbo.OrderDetail INNER JOIN dbo.MsMenu ON dbo.OrderDetail.Menuid = dbo.MsMenu.Id where Orderid ='"+orderHeader+"' ;", conn);
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
                    totalHarga += Convert.ToInt32(dataGridView2.Rows[i].Cells[5].Value);
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
                String path = "E:\\ks\\LKS-HAmudi\\FrmLogin\\images\\" + row.Cells["Photo"].Value.ToString();
                picture.ImageLocation = "E:\\lks\\LKS-HAmudi\\FrmLogin\\images\\" + row.Cells["Photo"].Value.ToString();
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
                    cmd = new SqlCommand("insert into OrderDetail values('"+ orderHeader.ToString() +"','" + Id + "', '" + textBox2.Text + "', 'UNPAID') ", conn);
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


            SqlConnection conn = Konn.getKoneksi();


            try
            {
                string query = "insert into [OrderHeader] values ('"+orderHeader.ToString()+"','1','"+ comboBox1.SelectedValue +"',GETDATE(),null,null,null,'"+Int32.Parse(txtTotal.Text)+"')";
                conn.Open();
                cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();


                refresh();
                MessageBox.Show("berhasil melakukan order");
                ResetDataGridView();


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
            }

        }

        private void ResetDataGridView()
        {
            dataGridView2.CancelEdit();
            dataGridView2.Columns.Clear();
            //dataGridView2.DataSource = null;
            //InitializeDataGridView();
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show(comboBox1.SelectedValue.ToString());
        }

        void getMember()
        {
            SqlConnection conn = Konn.getKoneksi();
            try
            {
                
                conn.Open();
                da = new SqlDataAdapter("SELECT id,name From MsMember", conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "member");
                comboBox1.DisplayMember = "name";
                comboBox1.ValueMember = "id";
                comboBox1.DataSource = ds.Tables["member"];





            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
