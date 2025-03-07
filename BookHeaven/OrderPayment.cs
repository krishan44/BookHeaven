using System;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using static BookHeaven.NewOrder;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace BookHeaven
{
    public partial class OrderPayment : Form
    {
        private readonly string connectionString = @"Data Source=DESKTOP-OEI0948;Initial Catalog=BookHeaven;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

        public OrderPayment()
        {
            InitializeComponent();
        }

        private void OrderPayment_Load(object sender, EventArgs e)
        {
            txtOrderedBook.Text = OrderData.BookName;
            LoadCustomerIDs();
            LoadBookPriceAndDiscount();
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
                        cmbCusID.Items.Add("--Select--"); // Add a default selection
                        cmbCusID.SelectedIndex = 0; // Select the default item

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

        private void LoadBookPriceAndDiscount()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT Price, Discount FROM BooksTable WHERE Title = @Title";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Title", OrderData.BookName);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtPrice.Text = reader["Price"].ToString();
                                txtDiscount.Text = reader["Discount"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Book not found or price/discount not available.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtPrice.Clear();
                                txtDiscount.Clear();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading book price and discount: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPrice.Clear();
                txtDiscount.Clear();
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            NewOrder newOrder = new NewOrder();
            newOrder.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DashboardStaff dashboardStaff = new DashboardStaff();
            dashboardStaff.Show();
            this.Hide();
        }

        private void cmbCusID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCusID.SelectedIndex > 0) // Ensure a valid CustomerID is selected
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string selectedCustomerID = cmbCusID.SelectedItem.ToString();
                        string query = @"SELECT Name, Address, PhoneNumber, Email 
                                       FROM CustomersTable 
                                       WHERE CustomerID = @CustomerID";

                        using (SqlCommand command = new SqlCommand(query, conn))
                        {
                            command.Parameters.AddWithValue("@CustomerID", selectedCustomerID);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    txtCustomer.Text = reader["Name"].ToString();
                                    txtAddress.Text = reader["Address"].ToString();
                                    txtContact.Text = reader["PhoneNumber"].ToString();
                                    txtEmail.Text = reader["Email"].ToString();
                                }
                                else
                                {
                                    ClearCustomerDetails(); // Clear if CustomerID not found
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading customer details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearCustomerDetails();
                }
            }
            else
            {
                ClearCustomerDetails(); // Clear if "--Select--" is selected
            }
        }

        private void ClearCustomerDetails()
        {
            txtCustomer.Clear();
            txtAddress.Clear();
            txtContact.Clear();
            txtEmail.Clear();
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private void GeneratePDF(string orderID, string customerName, string address, string contact, string email,
                         string bookName, int quantity, decimal price, decimal discount, decimal total, string deliveryType)
        {
            try
            {
                // Define file path
                string invoicesFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Invoices");

                // Ensure the directory exists
                if (!Directory.Exists(invoicesFolder))
                {
                    Directory.CreateDirectory(invoicesFolder); // Create the directory if it doesn't exist
                }

                string filePath = Path.Combine(invoicesFolder, $"Invoice_{orderID}.pdf");


                // Create a PDF document
                Document doc = new Document(PageSize.A4);
                PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
                doc.Open();

                // Use iTextSharp.text.Font explicitly to avoid conflict
                iTextSharp.text.Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
                Paragraph title = new Paragraph("Book Heaven - Sales Invoice", titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);
                doc.Add(new Paragraph("\n"));

                // Order Details
                iTextSharp.text.Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                iTextSharp.text.Font normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);

                doc.Add(new Paragraph($"Order ID: {orderID}", boldFont));
                doc.Add(new Paragraph($"Customer Name: {customerName}", normalFont));
                doc.Add(new Paragraph($"Address: {address}", normalFont));
                doc.Add(new Paragraph($"Contact: {contact}", normalFont));
                doc.Add(new Paragraph($"Email: {email}", normalFont));
                doc.Add(new Paragraph($"Delivery Type: {deliveryType}", normalFont));
                doc.Add(new Paragraph("\n"));

                // Order Summary Table
                PdfPTable table = new PdfPTable(5); // 5 columns
                table.WidthPercentage = 100;

                // FIX: Ensure SetWidths uses float values instead of strings
                table.SetWidths(new float[] { 3f, 1f, 1f, 1f, 1f });

                // Add table headers
                table.AddCell(new PdfPCell(new Phrase("Book Name", boldFont)));
                table.AddCell(new PdfPCell(new Phrase("Quantity", boldFont)));
                table.AddCell(new PdfPCell(new Phrase("Price", boldFont)));
                table.AddCell(new PdfPCell(new Phrase("Discount (%)", boldFont)));
                table.AddCell(new PdfPCell(new Phrase("Total", boldFont)));

                // Add table data
                table.AddCell(new PdfPCell(new Phrase(bookName, normalFont)));
                table.AddCell(new PdfPCell(new Phrase(quantity.ToString(), normalFont)));
                table.AddCell(new PdfPCell(new Phrase(price.ToString("0.00"), normalFont)));
                table.AddCell(new PdfPCell(new Phrase(discount.ToString("0.00"), normalFont)));
                table.AddCell(new PdfPCell(new Phrase(total.ToString("0.00"), normalFont)));

                doc.Add(table);
                doc.Add(new Paragraph("\n"));

                // Closing message
                doc.Add(new Paragraph("Thank you for your purchase!", normalFont));
                doc.Close();

                MessageBox.Show($"Invoice saved successfully!\nLocation: {filePath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void CalculateTotal()
        {
            if (string.IsNullOrEmpty(txtQuantity.Text) || string.IsNullOrEmpty(txtPrice.Text) || string.IsNullOrEmpty(txtDiscount.Text))
            {
                txtTotal.Text = "";
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity) || !decimal.TryParse(txtPrice.Text, out decimal price) || !decimal.TryParse(txtDiscount.Text, out decimal discount))
            {
                txtTotal.Text = "Invalid input";
                return;
            }

            decimal discountedPrice = price - (price * (discount / 100));
            decimal total = quantity * discountedPrice;

            txtTotal.Text = total.ToString("0.00"); // Format to two decimal places
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction(); // Begin transaction

                    try
                    {
                        // Generate OrderID
                        string orderID = GenerateOrderID(conn, transaction);

                        // Get values from controls
                        string orderedBook = txtOrderedBook.Text;
                        DateTime orderDate = DateTime.Now;
                        string status = "Pending";
                        string deliveryType = PickUp.Checked ? "PickUp" : "Delivery";
                        decimal discount = decimal.Parse(txtDiscount.Text);
                        decimal total = decimal.Parse(txtTotal.Text);
                        object completedDate = DBNull.Value;
                        string customerID = cmbCusID.SelectedItem.ToString();
                        int quantity = int.Parse(txtQuantity.Text);

                        // Insert into OrdersTable
                        string insertOrderQuery = @"
                            INSERT INTO OrdersTable (OrderID, OrderedBook, OrderDate, Status, DeliveryType, Discount, Total, CompletedDate, CustomerID, Quantity)
                            VALUES (@OrderID, @OrderedBook, @OrderDate, @Status, @DeliveryType, @Discount, @Total, @CompletedDate, @CustomerID, @Quantity)";

                        using (SqlCommand command = new SqlCommand(insertOrderQuery, conn, transaction))
                        {
                            command.Parameters.AddWithValue("@OrderID", orderID);
                            command.Parameters.AddWithValue("@OrderedBook", orderedBook);
                            command.Parameters.AddWithValue("@OrderDate", orderDate);
                            command.Parameters.AddWithValue("@Status", status);
                            command.Parameters.AddWithValue("@DeliveryType", deliveryType);
                            command.Parameters.AddWithValue("@Discount", discount);
                            command.Parameters.AddWithValue("@Total", total);
                            command.Parameters.AddWithValue("@CompletedDate", completedDate);
                            command.Parameters.AddWithValue("@CustomerID", customerID);
                            command.Parameters.AddWithValue("@Quantity", quantity);

                            command.ExecuteNonQuery();
                        }

                        // Update Book Stock
                        UpdateBookStock(conn, transaction, orderedBook, quantity);

                        transaction.Commit(); // Commit transaction

                        MessageBox.Show("Order confirmed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Generate PDF Invoice
                        GeneratePDF(orderID, txtCustomer.Text, txtAddress.Text, txtContact.Text, txtEmail.Text,
                            txtOrderedBook.Text, quantity, decimal.Parse(txtPrice.Text),
                            discount, total, deliveryType);

                        ClearOrderFields();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); // Rollback transaction on error
                        throw ex; // Re-throw the exception
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error confirming order: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerateOrderID(SqlConnection conn, SqlTransaction transaction)
        {
            string orderId = "ORD_01";

            try
            {
                string query = "SELECT TOP 1 OrderID FROM OrdersTable ORDER BY OrderID DESC";
                using (SqlCommand command = new SqlCommand(query, conn, transaction))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string lastOrderId = reader["OrderID"].ToString();
                        int lastNumber = int.Parse(lastOrderId.Substring(4));
                        orderId = "ORD_" + (lastNumber + 1).ToString("D2");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating OrderID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return orderId;
        }

        private void UpdateBookStock(SqlConnection conn, SqlTransaction transaction, string orderedBook, int quantity)
        {
            try
            {
                string updateQuery = "UPDATE BooksTable SET StockQuantity = StockQuantity - @Quantity WHERE Title = @Title";
                using (SqlCommand command = new SqlCommand(updateQuery, conn, transaction))
                {
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@Title", orderedBook);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating book stock: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw ex; // Re-throw to rollback the transaction
            }
        }

        private void ClearOrderFields()
        {
            cmbCusID.SelectedIndex = 0;
            txtCustomer.Clear();
            txtAddress.Clear();
            txtContact.Clear();
            txtEmail.Clear();
            txtQuantity.Clear();
            txtTotal.Clear();
        }
    }
}