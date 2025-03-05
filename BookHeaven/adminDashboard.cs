using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookHeaven
{
    public partial class adminDashboard : Form
    {
        public adminDashboard()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NewBook newBook = new NewBook();
            newBook.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Staff staff = new Staff();
            staff.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReportMain reportMain = new ReportMain();
            reportMain.Show();
            this.Hide();
        }
    }
}
