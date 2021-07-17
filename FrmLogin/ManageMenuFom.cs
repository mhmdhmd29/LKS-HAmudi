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
    public partial class ManageMenuFom : Form
    {
        private SqlCommand cmd;
        private SqlDataAdapter da;
        private DataSet ds;
        String NamaFoto = "";
        public String employeeId;

        Koneksi Konn = new Koneksi();


        void Bersihkan()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }


        public ManageMenuFom()
        {
            InitializeComponent();
            btnSave.Visible = false;
            btnCancel.Visible = false;
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

        void cariMenu()
        {
            SqlConnection Conn = Konn.getKoneksi();
            try
            {
                Conn.Open();
                cmd = new SqlCommand("Select * From MsMenu where Id like '%" + txtSearch.Text + "%' or Name like '%" + txtSearch.Text + "%' or Carbo like '%" + txtSearch.Text + "%' or Protein like '%" + txtSearch.Text + "%' ", Conn);
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
                Conn.Close();
            }
        }
        private void ManageMenuFom_Load(object sender, EventArgs e)
        {
            tampilMenu();
            Bersihkan();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            btnCancel.Visible = false;


            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" || textBox5.Text.Trim() == "" || textBox6.Text.Trim() == "")
            {
                MessageBox.Show("Data belum lengkap!");
            }
            else
            {
                SqlConnection conn = Konn.getKoneksi();
                try
                {
                    cmd = new SqlCommand("insert into MsMenu values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "')", conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Insert data berhasil");
                    tampilMenu();
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //OpenFileDialog dialog = new OpenFileDialog();
            //PictureBox picture = pictureBox1;
            //String name;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            PictureBox picture = pictureBox1;
            String name;

            if (picture != null)
            {
                dialog.Filter = "(*.jpg;*.png;) | *.jpg;*.png; ";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    picture.Image = Image.FromFile(dialog.FileName);


                    String location = dialog.FileName;
                    String[] part = location.Split(new char[] { '\\' });

                    String[] extension = part.Last().Split(new char[] { '.' });
                    //String[] extension = part.Last().Split(new char[] { '.' });

                    //MessageBox.Show(extension.Last());

                    String path = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

                    System.IO.File.Copy(dialog.FileName, path + "\\images\\" + part.Last().ToString());
                    name = path + "\\images\\" + "." + extension;
                    NamaFoto = part.Last().ToString();
                    textBox4.Text = NamaFoto;

                }

            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["Id"].Value.ToString();
                textBox2.Text = row.Cells["Name"].Value.ToString();
                textBox3.Text = row.Cells["Price"].Value.ToString();
                textBox5.Text = row.Cells["Carbo"].Value.ToString();
                textBox6.Text = row.Cells["Protein"].Value.ToString();
            }
            catch (Exception h)
            {
                MessageBox.Show(h.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" || textBox5.Text.Trim() == "" || textBox6.Text.Trim() == "")
            {
                MessageBox.Show("Data belum lengkap!");
            }
            else
            {
                SqlConnection conn = Konn.getKoneksi();
                try
                {
                    cmd = new SqlCommand("Update MsMenu Set Name= '" + textBox2.Text + "', Price='" + textBox3.Text + "',Photo='" + textBox4.Text + "',Carbo='" + textBox5.Text + "',Protein='" + textBox6.Text + "' where Id= '" + textBox1.Text + "'", conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Update data berhasil");
                    tampilMenu();
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

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Yakin akan menghapus Menu " + textBox2.Text + " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SqlConnection conn = Konn.getKoneksi();

                {
                    cmd = new SqlCommand("Delete MsMenu where Id='" + textBox1.Text + "'", conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Hapus data berhasil");
                    tampilMenu();
                    Bersihkan();
                }

            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            btnSave.Visible = true;
            btnCancel.Visible = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            btnCancel.Visible = false;
            Bersihkan();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cariMenu();
        }
    }
}
