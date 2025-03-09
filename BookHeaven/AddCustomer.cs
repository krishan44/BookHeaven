using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookHeaven
{
    public partial class AddCustomer : Form
    {
        public AddCustomer()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-OEI0948;Initial Catalog=BookHeaven;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
                {
                    conn.Open();

                    // Generate CustomerID
                    string customerId = GenerateCustomerID(conn);

                    // Insert into CustomerTable
                    string insertCustomerQuery = @"
                INSERT INTO CustomersTable (CustomerID, Name, Email, PhoneNumber, Address) 
                VALUES (@CustomerID, @Name, @Email, @PhoneNumber, @Address)";

                    using (SqlCommand customerCommand = new SqlCommand(insertCustomerQuery, conn))
                    {
                        customerCommand.Parameters.AddWithValue("@CustomerID", customerId);
                        customerCommand.Parameters.AddWithValue("@Name", txtName.Text);
                        customerCommand.Parameters.AddWithValue("@Email", txtEmail.Text);
                        customerCommand.Parameters.AddWithValue("@PhoneNumber", txtConNo.Text);
                        customerCommand.Parameters.AddWithValue("@Address", txtAddress.Text);

                        int customerRowsAffected = customerCommand.ExecuteNonQuery();

                        if (customerRowsAffected > 0)
                        {
                            MessageBox.Show("Customer added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearCustomerFields();
                        }
                        else
                        {
                            MessageBox.Show("Failed to add customer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerateCustomerID(SqlConnection conn)
        {
            string customerId = "CUS_01"; // Default CustomerID

            try
            {
                string query = "SELECT TOP 1 CustomerID FROM CustomersTable ORDER BY CustomerID DESC";
                using (SqlCommand command = new SqlCommand(query, conn))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string lastCustomerId = reader["CustomerID"].ToString();
                        int lastNumber = int.Parse(lastCustomerId.Substring(4)); // Extract the numeric part
                        customerId = "CUS_" + (lastNumber + 1).ToString("D2"); // Increment and format
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating CustomerID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return customerId;
        }

        private void ClearCustomerFields()
        {
            txtName.Clear();
            txtAddress.Clear();
            txtEmail.Clear();
            txtConNo.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DashboardStaff dashboardStaff = new DashboardStaff();
            dashboardStaff.Show();
            this.Hide();
        }

        private void AddCustomer_Load(object sender, EventArgs e)
        {

        }
    }
}
