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
    public partial class AdminNavigation : Form
    {
        public String employeeId;
        public AdminNavigation()
        {
            InitializeComponent();
            txtName.Text = Form1.SetValueForText1;
        }

        private void txtName_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ManageMenuFom mngmenu = new ManageMenuFom();
            mngmenu.Show();
            mngmenu.employeeId = this.employeeId;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OrderForm ordform = new OrderForm();
            ordform.Show();
            ordform.employeeid = this.employeeId;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ManageMemberForm mngmember = new ManageMemberForm();
            mngmember.Show();
            mngmember.employeeid = this.employeeId;
        }
    }
}
