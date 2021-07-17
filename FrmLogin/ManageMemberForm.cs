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
    public partial class ManageMemberForm : Form
    {
        private SqlCommand cmd;
        private SqlDataAdapter da;
        private DataSet ds;
        String NamaFoto = "";
        public String employeeid;

        Koneksi Konn = new Koneksi();


        void Bersihkan()
        {
            txtSearch.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }
        void tampilMember()
        {
            SqlConnection conn = Konn.getKoneksi();
            try
            {
                conn.Open();
                cmd = new SqlCommand("Select * From MsMember", conn);
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
        void cariMember()
            {
                SqlConnection Conn = Konn.getKoneksi();
                try
                {
                    Conn.Open();
                    cmd = new SqlCommand("Select * From MsMember where Id like '%" + txtSearch.Text + "%' or Name like '%" + txtSearch.Text + "%' or Email like '%" + txtSearch.Text + "%' or HandPhone like '%" + txtSearch.Text + "%' ", Conn);
                    ds = new DataSet();
                    da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
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
                    Conn.Close();
                }


        }

        public ManageMemberForm()
        {
            InitializeComponent();
            btnSave.Visible = false;
            btnCancel.Visible = false;
        }

        private void ManageMemberForm_Load(object sender, EventArgs e)
        {
            tampilMember();
            Bersihkan();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            btnCancel.Visible = false;

            if (textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" || textBox5.Text.Trim() == "")
            {
                MessageBox.Show("Data belum lengkap!");
            }
            else
            {
                SqlConnection conn = Konn.getKoneksi();
                try
                {
                    cmd = new SqlCommand("insert into MsMember values('" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + DateTime.Now.ToString("yyyy/MM/dd") + "')", conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Insert data berhasil");
                    tampilMember();
                    Bersihkan();
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
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            btnSave.Visible = true;
            btnCancel.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection conn = Konn.getKoneksi();
            try
            {
                cmd = new SqlCommand("Update MsMember Set Name= '" + textBox3.Text + "', Email='" + textBox4.Text + "',HandPhone='" + textBox5.Text + "'  where Id= '" + textBox2.Text + "'", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Update data berhasil");
                tampilMember();
                Bersihkan();
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox2.Text = row.Cells["Id"].Value.ToString();
                textBox3.Text = row.Cells["Name"].Value.ToString();
                textBox4.Text = row.Cells["Email"].Value.ToString();
                textBox5.Text = row.Cells["HandPhone"].Value.ToString();
            }
            catch (Exception h)
            {
                MessageBox.Show(h.ToString());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Yakin akan menghapus Data " + textBox3.Text + " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SqlConnection conn = Konn.getKoneksi();

                {
                    cmd = new SqlCommand("Delete MsMember where Id='" + textBox2.Text + "'", conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Hapus data berhasil");
                    tampilMember();
                    Bersihkan();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            btnCancel.Visible = false;
            Bersihkan();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cariMember();
        }
    }
}
