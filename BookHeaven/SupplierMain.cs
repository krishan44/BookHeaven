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
    public partial class SupplierMain : Form
    {
        public SupplierMain()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Supplier supplier = new Supplier();
            supplier.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateSupplier updateSupplier = new UpdateSupplier();
            updateSupplier.Show();
            this.Hide();
        }
    }
}
