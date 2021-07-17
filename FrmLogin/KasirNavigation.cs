using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmLogin
{
    public partial class KasirNavigation : Form
    {
        public String employeeid;
        public KasirNavigation()
        {
            InitializeComponent();
            txtName.Text = Form1.SetValueForText1;
        }

        private void txtName_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Hide();
        }

        private void KasirNavigation_Load(object sender, EventArgs e)
        {

        }
    }
}
