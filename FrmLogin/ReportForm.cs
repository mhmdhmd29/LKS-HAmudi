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
    public partial class ReportForm : Form
    {
        int bulanPertama = 0;
        int bulanKedua = 0;
        private SqlCommand cmd;
        private SqlDataAdapter da;
        private DataSet ds;
        Koneksi Konn = new Koneksi();

        public ReportForm()
        {
            InitializeComponent();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String query = "select DATENAME(MONTH, dbo.OrderHeader.Date) as Month, SUM(dbo.OrderHeader.total_bayar) as Income FROM dbo.OrderHeader WHERE MONTH(dbo.OrderHeader.Date) BETWEEN '"+bulanPertama+ "' AND '" + bulanKedua + "' GROUP BY DATENAME(MONTH, dbo.OrderHeader.Date)";
            SqlConnection conn = Konn.getKoneksi();
            try
            {
                conn.Open();
                cmd = new SqlCommand(query, conn);
                da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                chart1.DataSource = dt;
                chart1.Series["Income"].XValueMember = "Month";
                chart1.Series["Income"].YValueMembers = "Income";
                chart1.Titles.Add("Income Chart");

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

        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bulanPertama = (comboBox1.SelectedIndex + 1);
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            bulanKedua = (comboBox2.SelectedIndex + 1);
                
        }
    }
}
