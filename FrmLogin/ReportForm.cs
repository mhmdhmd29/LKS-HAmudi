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
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String query = "select DATENAME(MONTH, dbo.OrderHeader.Date) as Month, SUM(dbo.OrderHeader.total_bayar) as Income FROM dbo.OrderHeader WHERE MONTH(dbo.OrderHeader.Date) BETWEEN 1 AND 12 GROUP BY DATENAME(MONTH, dbo.OrderHeader.Date)";

        }
    }
}
