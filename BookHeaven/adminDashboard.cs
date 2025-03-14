using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; // Add this using directive

namespace BookHeaven
{

    public partial class adminDashboard : Form
    {
        public adminDashboard()
        {
            InitializeComponent();
        }

        public static class GlobalSales
        {
            public static decimal TotalSales { get; set; }
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

        private void button4_Click(object sender, EventArgs e)
        {
            SupplierMain supplierMain = new SupplierMain();
            supplierMain.Show();
            this.Hide();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            BookOrder bookOrder = new BookOrder();
            bookOrder.Show();
            this.Hide();
        }

        private void adminDashboard_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-OEI0948;Initial Catalog=BookHeaven;Integrated Security=True;Connect Timeout=30;Encrypt=False;"; // Use your connection string here

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Get total number of books
                    SqlCommand cmdBooks = new SqlCommand("SELECT COUNT(*) FROM BooksTable", connection);
                    int totalBooks = (int)cmdBooks.ExecuteScalar();
                    lblBooks.Text = totalBooks.ToString();

                    // Get total number of staff
                    SqlCommand cmdStaff = new SqlCommand("SELECT COUNT(*) FROM StaffTable", connection);
                    int totalStaff = (int)cmdStaff.ExecuteScalar();
                    lblStaff.Text = totalStaff.ToString();

                    // Get total number of customers
                    SqlCommand cmdCustomers = new SqlCommand("SELECT COUNT(*) FROM CustomersTable", connection);
                    int totalCustomers = (int)cmdCustomers.ExecuteScalar();
                    lblCustomers.Text = totalCustomers.ToString();

                    // Get total number of suppliers
                    SqlCommand cmdSuppliers = new SqlCommand("SELECT COUNT(*) FROM SuppliersTable", connection);
                    int totalSuppliers = (int)cmdSuppliers.ExecuteScalar();
                    lblSuppliers.Text = totalSuppliers.ToString();

                    // Get total sales
                    SqlCommand cmdSales = new SqlCommand("SELECT ISNULL(SUM(Total), 0) FROM OrdersTable", connection);
                    object result = cmdSales.ExecuteScalar();
                    decimal totalSales = result != DBNull.Value ? (decimal)result : 0;

                    // Format as LKR currency
                    System.Globalization.CultureInfo lkrCulture = new System.Globalization.CultureInfo("en-LK"); 
                    lblSales.Text = totalSales.ToString("C", lkrCulture);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}