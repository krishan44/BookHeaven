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
    public partial class Orders : Form
    {
        public Orders()
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

        private void Orders_Load(object sender, EventArgs e)
        {
            LoadOrders();
            cmbStatus.SelectedIndex = 0;
        }

        private void LoadOrders()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT OrderID, OrderedBook, Status, DeliveryType, Total FROM OrdersTable";
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
                MessageBox.Show("Error loading orders: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtOrderID_TextChanged(object sender, EventArgs e)
        {
           
        }

       

        private void txtOrderID_TextChanged_1(object sender, EventArgs e)
        {
            FilterOrders();
        }
        private void FilterOrders()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT OrderID, OrderedBook, Status, DeliveryType, Total FROM OrdersTable";
                    if (!string.IsNullOrEmpty(txtOrderID.Text))
                    {
                        query += " WHERE OrderID LIKE @OrderID";
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        if (!string.IsNullOrEmpty(txtOrderID.Text))
                        {
                            adapter.SelectCommand.Parameters.AddWithValue("@OrderID", "%" + txtOrderID.Text + "%");
                        }

                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridViewOrders.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error filtering orders: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtOrderID.Text))
            {
                MessageBox.Show("Please select an Order ID to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a status to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string orderID = txtOrderID.Text;
            string newStatus = cmbStatus.SelectedItem.ToString();
            DateTime? completedDate = null;

            if (newStatus == "Picked" || newStatus == "Delivered")
            {
                completedDate = DateTime.Now;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE OrdersTable SET Status = @Status, CompletedDate = @CompletedDate WHERE OrderID = @OrderID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Status", newStatus);
                        cmd.Parameters.AddWithValue("@OrderID", orderID);
                        cmd.Parameters.AddWithValue("@CompletedDate", (object)completedDate ?? DBNull.Value);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Order status updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadOrders(); // Refresh the DataGridView
                        }
                        else
                        {
                            MessageBox.Show("Order ID not found.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating order status: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) // Ensure a valid cell is clicked
            {
                DataGridViewRow selectedRow = dataGridViewOrders.Rows[e.RowIndex];
                if (selectedRow.Cells["OrderID"].Value != null) // Ensure the OrderID cell has a value
                {
                    txtOrderID.Text = selectedRow.Cells["OrderID"].Value.ToString();
                    FilterOrders(); // Optional: Immediately filter based on the selected OrderID
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtOrderID.Text))
            {
                MessageBox.Show("Please select an Order ID to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string orderID = txtOrderID.Text;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM OrdersTable WHERE OrderID = @OrderID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@OrderID", orderID);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Order deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadOrders(); // Refresh the DataGridView
                            txtOrderID.Clear(); // Clear the text box
                        }
                        else
                        {
                            MessageBox.Show("Order ID not found.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting order: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
