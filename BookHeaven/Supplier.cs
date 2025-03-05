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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BookHeaven
{
    public partial class Supplier : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-OEI0948;Initial Catalog=BookHeaven;Integrated Security=True;Connect Timeout=30;Encrypt=False;");

        private void clear()
        {
            txtAddress.Clear();
            txtEmail.Clear();
            txtAgent.Clear();
            txtConNo.Clear();
            txtBname.Clear();
            txtNIC.Clear();
        }

        public Supplier()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string supplierID = "";

            try
            {
                // 1. Get the last SupplierID from the database.
                string query = "SELECT TOP 1 SupplierID FROM SuppliersTable ORDER BY SupplierID DESC";
                SqlCommand command = new SqlCommand(query, conn);

                conn.Open();
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    // 2. Extract the numeric part and increment.
                    string lastSupplierID = result.ToString();
                    string numericPart = lastSupplierID.Substring(4); // Assuming "SUP_" prefix
                    int lastIndex = int.Parse(numericPart);
                    int newIndex = lastIndex + 1;
                    supplierID = "SUP_" + newIndex.ToString("D2"); // Format as "SUP_01", "SUP_02", etc.
                }
                else
                {
                    // 3. No existing records, start with SUP_01.
                    supplierID = "SUP_01";
                }

                // 4. Construct the INSERT query using parameterized queries.
                string quesave = "insert into SuppliersTable(SupplierID, BusinessName, AgentName, NIC, Email, ContactNumber, Address) values(@supplierID, @businessName, @agentName, @nic, @email, @contactNumber, @address)";

                command = new SqlCommand(quesave, conn);
                command.Parameters.AddWithValue("@supplierID", supplierID);
                command.Parameters.AddWithValue("@businessName", txtBname.Text);
                command.Parameters.AddWithValue("@agentName", txtAgent.Text);
                command.Parameters.AddWithValue("@nic", txtNIC.Text);
                command.Parameters.AddWithValue("@email", txtEmail.Text);
                command.Parameters.AddWithValue("@contactNumber", txtConNo.Text);
                command.Parameters.AddWithValue("@address", txtAddress.Text);

                command.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Business Supplier " + txtBname.Text + " Successfully Added to the System", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
    }
}