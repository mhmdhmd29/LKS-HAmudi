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
    public partial class PaymentForm : Form
    {
        private SqlCommand cmd;
        private SqlDataAdapter da;
        private DataSet ds;
        String orderid = "";
        String PaymentType = "";
        String CardNumber = "";
        String Bank = "";

        Koneksi Konn = new Koneksi();

        void getPayment()
        {
            SqlConnection conn = Konn.getKoneksi();
            try
            {
                conn.Open();
                da = new SqlDataAdapter("SELECT id From OrderHeader", conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "orderid");
                comboBox3.DisplayMember = "id";
                comboBox3.ValueMember = "id";
                comboBox3.DataSource = ds.Tables["orderid"];

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



        void payment()
        {
            SqlConnection conn = Konn.getKoneksi();
            try
            {
                conn.Open();
                cmd = new SqlCommand("SELECT dbo.MsMenu.Name, dbo.OrderDetail.Qty, dbo.MsMenu.Price , dbo.OrderDetail.Qty * dbo.MsMenu.Price as Total FROM dbo.MsMenu INNER JOIN dbo.OrderDetail ON dbo.MsMenu.Id = dbo.OrderDetail.Menuid where Orderid =  '" + orderid.ToString() + "' and dbo.OrderDetail.Status = 'UNPAID' ", conn);
                da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                int totalBayar = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                    totalBayar += Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value);
                }

                txtTotalBayar.Text = totalBayar.ToString();
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
            public PaymentForm()
        {
            InitializeComponent();
            label6.Visible = false;
            edtJumlahUang.Visible = false;
        }

        private void PaymentForm_Load(object sender, EventArgs e)
        {
            getPayment();
            payment();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            PaymentType = comboBox1.SelectedItem.ToString();
            if (PaymentType == "Cash")
            {
                label6.Visible = true;
                edtJumlahUang.Visible = true;
               
            }
            else
            {
                label6.Visible = false;
                edtJumlahUang.Visible = false;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

            orderid = comboBox3.SelectedValue.ToString();
           

            payment();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bank = comboBox2.SelectedText.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(PaymentType);
            if (PaymentType == "Cash")
            {
                int harga = Int32.Parse(txtTotalBayar.Text);
                int jumlahUang = Int32.Parse(edtJumlahUang.Text);
                int selisihUang = jumlahUang - harga;

                SqlConnection conn = Konn.getKoneksi();

                if (selisihUang == 0)
                {
                   
                    try
                    {
                        conn.Open();
                        cmd = new SqlCommand("update OrderHeader set PaymentType = '"+ PaymentType.ToString() +"',total_bayar = '" + Int32.Parse(edtJumlahUang.Text) + "' where Id = '"+ orderid.ToString() +"' ;", conn);
                        cmd.ExecuteNonQuery();
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        conn.Close();
                    }
                    conn.Open();
                    cmd = new SqlCommand("update OrderDetail set Status = 'PAID' where Orderid = '20210722011 ';", conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Lunas");
                }
                else if (selisihUang > 0)
                {
                    try
                    {
                        conn.Open();
                        cmd = new SqlCommand("update OrderHeader set PaymentType = '" + PaymentType.ToString() + "',total_bayar = '" + Int32.Parse(edtJumlahUang.Text) + "' where Id = '" + orderid.ToString() + "' ;", conn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Pembayaran Berhasil Kembalian Anda senilai " + selisihUang.ToString());
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
                else
                {
                    
                    MessageBox.Show("Uang Anda Kurang " + Math.Abs(selisihUang).ToString());
                }


            }

            else
            {
                SqlConnection conn = Konn.getKoneksi();
                try
                {
                    conn.Open();
                    cmd = new SqlCommand("update OrderHeader set PaymentType = '" + PaymentType.ToString() + "',CardNumber='" + textBox2.Text.ToString() + "',Bank = '" + comboBox2.SelectedItem.ToString() + "',total_bayar = '" + Int32.Parse(txtTotalBayar.Text) +"' where Id = '"+orderid+"';",conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Berhasil melakukan pembayaran menggunakan kartu kredit");
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
}
