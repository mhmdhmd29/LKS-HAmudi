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
    public partial class Form1 : Form
    {
        public static string SetValueForText1 = "";
        Koneksi conn = new Koneksi();
        SqlCommand cmd;
        SqlDataReader sdr;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Username dan Password tidak boleh kosong!");
            }
            else
            {

            }
            SqlConnection koneksi = conn.getKoneksi();
            try
            {
                koneksi.Open();
                cmd = new SqlCommand("Select * from [MsEmployee] where email = @username and password = @password", koneksi);
                cmd.Parameters.AddWithValue("username", txtUsername.Text.ToString());
                cmd.Parameters.AddWithValue("password", txtPassword.Text.ToString());

                sdr = cmd.ExecuteReader();
                sdr.Read();

                if (sdr.HasRows)
                {
                    MessageBox.Show("Login Berhasil");
                    if (sdr.GetString(5).ToString() == "Admin")
                    {
                        SetValueForText1 = (sdr.GetString(1));
                        AdminNavigation admnav = new AdminNavigation();
                        admnav.Show();
                        admnav.employeeId = sdr.GetString(0).ToString();
                        this.Hide();
                    }
                    else if (sdr.GetString(5).ToString() == "Kasir")
                    {
                        SetValueForText1 = (sdr.GetString(1));
                        KasirNavigation kasnav = new KasirNavigation();
                        kasnav.Show();
                        kasnav.employeeid = sdr.GetString(0).ToString();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Maaf, Data tidak valid!");
                }

            }
            catch (Exception h)
            {
                MessageBox.Show(h.ToString());
            }
            finally
            {
                koneksi.Close();
            }
        }
    }
}
