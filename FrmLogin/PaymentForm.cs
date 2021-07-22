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
                cmd = new SqlCommand("SELECT dbo.MsMenu.Name, dbo.OrderDetail.Qty, dbo.MsMenu.Price FROM dbo.MsMenu INNER JOIN dbo.OrderDetail ON dbo.MsMenu.Id = dbo.OrderDetail.Menuid where Orderid = '"+orderid.ToString()+"' ", conn);
                da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

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
            public PaymentForm()
        {
            InitializeComponent();
            label6.Visible = false;
            textBox1.Visible = false;
        }

        private void PaymentForm_Load(object sender, EventArgs e)
        {
            getPayment();
            payment();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Cash")
            {
                label6.Visible = true;
                textBox1.Visible = true;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            orderid = comboBox3.SelectedValue.ToString();
            MessageBox.Show(orderid);

            payment();
        }
    }
}
