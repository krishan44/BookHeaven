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
    public partial class History : Form
    {
        public History()
        {
            InitializeComponent();
        }
        private readonly string connectionString = @"Data Source=DESKTOP-OEI0948;Initial Catalog=BookHeaven;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

        private void btnBack_Click(object sender, EventArgs e)
        {
            DashboardStaff dashboardStaff = new DashboardStaff();
            dashboardStaff.Show();
            this.Hide();
        }
        private void History_Load(object sender, EventArgs e)
        {
            LoadHistory();
            LoadCustomerIDs();
        }

        private void LoadHistory()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT OrderID, OrderedBook, CustomerID, DeliveryType, Total FROM OrdersTable WHERE Status IN ('Delivered', 'Picked')";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridViewOrders.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading history: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCustomerIDs()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT CustomerID FROM CustomersTable";
                    using (SqlCommand command = new SqlCommand(query, conn))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        cmbCusID.Items.Clear();
                        cmbCusID.Items.Add("Select"); // Add "Select" item for no filter
                        cmbCusID.SelectedIndex = 0;
                        while (reader.Read())
                        {
                            cmbCusID.Items.Add(reader["CustomerID"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Customer IDs: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FilterHistory()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT OrderID, OrderedBook, CustomerID, DeliveryType, Total FROM OrdersTable WHERE Status IN ('Delivered', 'Picked')";
                    string whereClause = "";
                    bool whereAdded = false; // Track if WHERE has been added

                    if (!string.IsNullOrEmpty(txtOrderID.Text))
                    {
                        whereClause += (whereAdded ? " AND " : " AND ") + "OrderID LIKE @OrderID";
                        whereAdded = true; // Mark WHERE as added
                    }

                    if (!string.IsNullOrEmpty(txtBookName.Text))
                    {
                        whereClause += (whereAdded ? " AND " : " AND ") + "OrderedBook LIKE @OrderedBook";
                        whereAdded = true;
                    }

                    if (cmbCusID.SelectedIndex > 0)
                    {
                        whereClause += (whereAdded ? " AND " : " AND ") + "CustomerID = @CustomerID";
                        whereAdded = true;
                    }

                    query += whereClause;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        if (!string.IsNullOrEmpty(txtOrderID.Text))
                        {
                            adapter.SelectCommand.Parameters.AddWithValue("@OrderID", "%" + txtOrderID.Text + "%");
                        }
                        if (!string.IsNullOrEmpty(txtBookName.Text))
                        {
                            adapter.SelectCommand.Parameters.AddWithValue("@OrderedBook", "%" + txtBookName.Text + "%");
                        }
                        if (cmbCusID.SelectedIndex > 0)
                        {
                            adapter.SelectCommand.Parameters.AddWithValue("@CustomerID", cmbCusID.SelectedItem.ToString());
                        }

                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridViewOrders.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error filtering history: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtOrderID_TextChanged(object sender, EventArgs e)
        {
            FilterHistory();
        }

        private void txtBookName_TextChanged(object sender, EventArgs e)
        {
            FilterHistory();
        }

        private void cmbCusID_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterHistory();
        }
    }
}