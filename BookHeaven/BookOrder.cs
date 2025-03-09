using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BookHeaven
{
    public partial class BookOrder : Form
    {
        private readonly string connectionString = @"Data Source=DESKTOP-OEI0948;Initial Catalog=BookHeaven;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
        private string supplierID; // Add this field to store supplierID

        public BookOrder()
        {
            InitializeComponent();
        }

        private void BookOrder_Load(object sender, EventArgs e)
        {
            LoadBooks();
            LoadBookOrders();
            btnUpdate.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            cmbBkOrderID.Visible = false;
            cmbStatus.Visible = false;
        }

        private void LoadBooks()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT BookID, Title, Author FROM BooksTable"; // Load BookID and Title
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        cmbBookID.Items.Clear();
                        cmbBookID.Items.Add("--Select--"); // Add select option
                        while (reader.Read())
                        {
                            cmbBookID.Items.Add(reader["BookID"].ToString());
                        }
                        cmbBookID.SelectedIndex = 0; // Set default to select option
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading books: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBookOrders()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT OrderID,BookName, Quantity, Status, SupplierID FROM BookOrders";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        gridViewSupplier.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading book orders: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbBookID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBookID.SelectedIndex == -1 || cmbBookID.SelectedIndex == 0) return; // No selection or default selection

            string bookID = cmbBookID.SelectedItem.ToString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string bookQuery = "SELECT Title, Author, SupplierID FROM BooksTable WHERE BookID = @BookID";
                    using (SqlCommand bookCmd = new SqlCommand(bookQuery, conn))
                    {
                        bookCmd.Parameters.AddWithValue("@BookID", bookID);
                        using (SqlDataReader bookReader = bookCmd.ExecuteReader())
                        {
                            if (bookReader.Read())
                            {
                                txtBookName.Text = bookReader["Title"].ToString();
                                txtAuthor.Text = bookReader["Author"].ToString();
                                supplierID = bookReader["SupplierID"].ToString(); // Store supplierID
                            }
                            bookReader.Close();
                        }
                    }

                    // Load supplier details only if supplierID has a value
                    if (!string.IsNullOrEmpty(supplierID))
                    {
                        string supplierQuery = "SELECT BusinessName, AgentName FROM SuppliersTable WHERE SupplierID = @SupplierID";
                        using (SqlCommand supplierCmd = new SqlCommand(supplierQuery, conn))
                        {
                            supplierCmd.Parameters.AddWithValue("@SupplierID", supplierID);
                            using (SqlDataReader supplierReader = supplierCmd.ExecuteReader())
                            {
                                if (supplierReader.Read())
                                {
                                    txtSupplier.Text = supplierReader["BusinessName"].ToString();
                                    txtAgent.Text = supplierReader["AgentName"].ToString();
                                }
                                else
                                {
                                    txtSupplier.Text = "";
                                    txtAgent.Text = "";
                                }
                                supplierReader.Close();
                            }
                        }
                    }
                    else
                    {
                        txtSupplier.Text = "";
                        txtAgent.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading book details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            if (cmbBookID.SelectedIndex <= 0 || string.IsNullOrEmpty(txtBookName.Text) || string.IsNullOrEmpty(txtQuantity.Text))
            {
                MessageBox.Show("Please fill all the required fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity))
            {
                MessageBox.Show("Quantity must be a valid number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Generate new OrderID
                    string newOrderID = GenerateNewOrderID(conn);

                    string query = "INSERT INTO BookOrders (OrderID, BookName, Quantity, Status, SupplierID) VALUES (@OrderID, @BookName, @Quantity, @Status, @SupplierID)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@OrderID", newOrderID);
                        cmd.Parameters.AddWithValue("@BookName", txtBookName.Text);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@Status", "Pending");
                        cmd.Parameters.AddWithValue("@SupplierID", supplierID);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Order placed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBookOrders();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error placing order: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerateNewOrderID(SqlConnection conn)
        {
            string query = "SELECT TOP 1 OrderID FROM BookOrders ORDER BY OrderID DESC";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    string lastOrderID = result.ToString();
                    if (lastOrderID.StartsWith("ORDBK_") && lastOrderID.Length == 8 && int.TryParse(lastOrderID.Substring(6), out int lastOrderNumber))
                    {
                        return "ORDBK_" + (lastOrderNumber + 1).ToString("D2");
                    }
                    else
                    {
                        // Handle invalid OrderID format (e.g., log an error or reset to "ORDBK_01")
                        MessageBox.Show("Invalid OrderID format found. Resetting to ORDBK_01.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return "ORDBK_01";
                    }

                }
                else
                {
                    return "ORDBK_01";
                }
            }
        }

        private void gridViewSupplier_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = gridViewSupplier.Rows[e.RowIndex];

                // Retrieve data from the selected row
                string orderID = row.Cells["OrderID"].Value.ToString();
                string bookName = row.Cells["BookName"].Value.ToString();
                string quantity = row.Cells["Quantity"].Value.ToString();
                string supplierID = row.Cells["SupplierID"].Value.ToString();

                // Populate the fields
                cmbBkOrderID.Items.Clear(); // Clear existing items
                cmbBkOrderID.Items.Add("--Select--");

                // Load all OrderIDs into the combobox
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "SELECT OrderID FROM BookOrders";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cmbBkOrderID.Items.Add(reader["OrderID"].ToString());
                            }
                            reader.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading OrderIDs: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                cmbBkOrderID.SelectedIndex = cmbBkOrderID.Items.IndexOf(orderID); // Select the clicked OrderID

                txtBookName.Text = bookName;
                txtQuantity.Text = quantity;

                // Retrieve and populate supplier details
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string supplierQuery = "SELECT BusinessName, AgentName FROM SuppliersTable WHERE SupplierID = @SupplierID";
                        using (SqlCommand supplierCmd = new SqlCommand(supplierQuery, conn))
                        {
                            supplierCmd.Parameters.AddWithValue("@SupplierID", supplierID);
                            using (SqlDataReader supplierReader = supplierCmd.ExecuteReader())
                            {
                                if (supplierReader.Read())
                                {
                                    txtSupplier.Text = supplierReader["BusinessName"].ToString();
                                    txtAgent.Text = supplierReader["AgentName"].ToString();
                                }
                                else
                                {
                                    txtSupplier.Text = "";
                                    txtAgent.Text = "";
                                }
                                supplierReader.Close();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading supplier details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Show and hide relevant controls
                btnUpdate.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                cmbBkOrderID.Visible = true;
                cmbStatus.Visible = true;
                cmbBookID.Visible = false;
                btnOrder.Visible = false;
                cmbStatus.SelectedIndex = 0;
            }
        }

        private void gridViewSupplier_CellCotentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbBkOrderID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBkOrderID.SelectedIndex <= 0) return; // No selection or default selection

            string orderID = cmbBkOrderID.SelectedItem.ToString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT BookName, Quantity, Status, SupplierID FROM BookOrders WHERE OrderID = @OrderID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@OrderID", orderID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtBookName.Text = reader["BookName"].ToString();
                                txtQuantity.Text = reader["Quantity"].ToString();
                                cmbStatus.SelectedItem = reader["Status"].ToString();
                                supplierID = reader["SupplierID"].ToString();
                            }
                            reader.Close();
                        }
                    }

                    // Retrieve and populate supplier details
                    string supplierQuery = "SELECT BusinessName, AgentName FROM SuppliersTable WHERE SupplierID = @SupplierID";
                    using (SqlCommand supplierCmd = new SqlCommand(supplierQuery, conn))
                    {
                        supplierCmd.Parameters.AddWithValue("@SupplierID", supplierID);
                        using (SqlDataReader supplierReader = supplierCmd.ExecuteReader())
                        {
                            if (supplierReader.Read())
                            {
                                txtSupplier.Text = supplierReader["BusinessName"].ToString();
                                txtAgent.Text = supplierReader["AgentName"].ToString();
                            }
                            else
                            {
                                txtSupplier.Text = "";
                                txtAgent.Text = "";
                            }
                            supplierReader.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading order details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cmbBkOrderID.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select an order to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbStatus.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select a status.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string orderID = cmbBkOrderID.SelectedItem.ToString();
            string status = cmbStatus.SelectedItem.ToString();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Update BookOrders table  
                    string updateOrderQuery = "UPDATE BookOrders SET Status = @Status WHERE OrderID = @OrderID";
                    using (SqlCommand cmd = new SqlCommand(updateOrderQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Status", status);
                        cmd.Parameters.AddWithValue("@OrderID", orderID);
                        cmd.ExecuteNonQuery();
                    }

                    // If the status is "Arrived", update the stock quantity in BooksTable  
                    if (status == "Arrived")
                    {
                        string bookIDQuery = "SELECT BookName, Quantity FROM BookOrders WHERE OrderID = @OrderID";
                        using (SqlCommand bookIDCmd = new SqlCommand(bookIDQuery, conn))
                        {
                            bookIDCmd.Parameters.AddWithValue("@OrderID", orderID);
                            using (SqlDataReader reader = bookIDCmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string bookName = reader["BookName"].ToString();
                                    int quantity = Convert.ToInt32(reader["Quantity"]);
                                    reader.Close(); // Close the reader before executing another command

                                    string updateStockQuery = "UPDATE BooksTable SET StockQuantity = StockQuantity + @Quantity WHERE Title = @BookName";
                                    using (SqlCommand updateStockCmd = new SqlCommand(updateStockQuery, conn))
                                    {
                                        updateStockCmd.Parameters.AddWithValue("@Quantity", quantity);
                                        updateStockCmd.Parameters.AddWithValue("@BookName", bookName);
                                        updateStockCmd.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    reader.Close(); // Ensure the reader is closed if no data is read
                                }
                            }
                        }
                    }

                    MessageBox.Show("Order updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBookOrders();
                    cmbBkOrderID.SelectedIndex = 0;
                    cmbStatus.SelectedIndex = 0;
                    txtBookName.Text = "";
                    txtQuantity.Text = "";
                    txtSupplier.Text = "";
                    txtAgent.Text = "";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating order: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            adminDashboard adminDashboard = new adminDashboard();
            adminDashboard.Show();
            this.Hide();
        }
    }
}