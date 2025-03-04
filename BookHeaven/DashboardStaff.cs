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
    public partial class DashboardStaff : Form
    {
        public DashboardStaff()
        {
            InitializeComponent();
        }

        private void DashboardStaff_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            NewOrder newOrder = new NewOrder();
            newOrder.Show();
            this.Hide();
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
           Orders orders = new Orders();
            orders.Show();
            this.Hide();
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            Inventory inventory = new Inventory();
            inventory.Show();
            this.Hide();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            AddCustomer addCustomer = new AddCustomer();
            addCustomer.Show();
            this.Hide();
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            History history = new History();
            history.Show();
            this.Hide();
        }
    }
}
