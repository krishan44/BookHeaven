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
            LoadCustomerIDs();
            txtTotal.Text = GlobalTotal.ToString("0.00"); // Display the total from NewOrder
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
                        string query = @"SELECT Name, Address, PhoneNumber, Email FROM CustomersTable WHERE CustomerID = @CustomerID";

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
                        string orderedBook = string.Join(", ", GlobalBookNames);
                        DateTime orderDate = DateTime.Now;
                        string status = "Pending";
                        string deliveryType = PickUp.Checked ? "PickUp" : "Delivery";
                        decimal total = decimal.Parse(txtTotal.Text);
                        object completedDate = DBNull.Value;
                        string customerID = cmbCusID.SelectedItem.ToString();

                        // Insert into OrdersTable
                        string insertOrderQuery = @"INSERT INTO OrdersTable (OrderID, OrderedBook, OrderDate, Status, DeliveryType, Total, CompletedDate, CustomerID) 
                                            VALUES (@OrderID, @OrderedBook, @OrderDate, @Status, @DeliveryType, @Total, @CompletedDate, @CustomerID)";

                        using (SqlCommand command = new SqlCommand(insertOrderQuery, conn, transaction))
                        {
                            command.Parameters.AddWithValue("@OrderID", orderID);
                            command.Parameters.AddWithValue("@OrderedBook", orderedBook);
                            command.Parameters.AddWithValue("@OrderDate", orderDate);
                            command.Parameters.AddWithValue("@Status", status);
                            command.Parameters.AddWithValue("@DeliveryType", deliveryType);
                            command.Parameters.AddWithValue("@Total", total);
                            command.Parameters.AddWithValue("@CompletedDate", completedDate);
                            command.Parameters.AddWithValue("@CustomerID", customerID);

                            command.ExecuteNonQuery();
                        }

                        transaction.Commit(); // Commit transaction

                        // Generate the PDF receipt
                        GeneratePDF(orderID, orderedBook, orderDate, deliveryType, total, txtCustomer.Text, txtAddress.Text, txtContact.Text, txtEmail.Text);

                        MessageBox.Show("Order confirmed successfully. Receipt has been generated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearOrderFields();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); // Rollback transaction on error
                        MessageBox.Show("Error confirming order: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error confirming order: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GeneratePDF(string orderID, string orderedBook, DateTime orderDate, string deliveryType, decimal total, string customerName, string address, string contact, string email)
        {
            try
            {
                // Define file path
                string folderPath = @"C:\BookHeaven\Receipts";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                string filePath = Path.Combine(folderPath, $"Receipt_{orderID}.pdf");

                // Create document
                Document document = new Document(PageSize.A4, 30, 30, 20, 20);
                PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                document.Open();

                // Define fonts
                iTextSharp.text.Font titleFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 18, iTextSharp.text.Font.BOLD, BaseColor.DARK_GRAY);
                iTextSharp.text.Font headerFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
                iTextSharp.text.Font cellFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font footerFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.ITALIC, BaseColor.GRAY);

                // Title
                Paragraph title = new Paragraph("ORDER RECEIPT\n\n", titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);

                // Table for Order Details
                PdfPTable orderTable = new PdfPTable(2);
                orderTable.WidthPercentage = 100;
                orderTable.SetWidths(new float[] { 1, 2 }); // Column widths

                PdfPCell headerCell = new PdfPCell(new Phrase("Order Details", headerFont))
                {
                    Colspan = 2,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = BaseColor.BLACK,
                    Padding = 5
                };
                orderTable.AddCell(headerCell);

                orderTable.AddCell(CreateCell("Order ID:", cellFont, true));
                orderTable.AddCell(CreateCell(orderID, cellFont, false));
                orderTable.AddCell(CreateCell("Date:", cellFont, true));
                orderTable.AddCell(CreateCell(orderDate.ToString("yyyy-MM-dd HH:mm"), cellFont, false));
                orderTable.AddCell(CreateCell("Delivery Type:", cellFont, true));
                orderTable.AddCell(CreateCell(deliveryType, cellFont, false));
                orderTable.AddCell(CreateCell("Total Amount:", cellFont, true));
                orderTable.AddCell(CreateCell($"${total:0.00}", cellFont, false));

                document.Add(orderTable);
                document.Add(new Paragraph("\n")); // Space

                // Table for Customer Details
                PdfPTable customerTable = new PdfPTable(2);
                customerTable.WidthPercentage = 100;
                customerTable.SetWidths(new float[] { 1, 2 });

                PdfPCell customerHeader = new PdfPCell(new Phrase("Customer Details", headerFont))
                {
                    Colspan = 2,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = BaseColor.DARK_GRAY,
                    Padding = 5
                };
                customerTable.AddCell(customerHeader);

                customerTable.AddCell(CreateCell("Name:", cellFont, true));
                customerTable.AddCell(CreateCell(customerName, cellFont, false));
                customerTable.AddCell(CreateCell("Address:", cellFont, true));
                customerTable.AddCell(CreateCell(address, cellFont, false));
                customerTable.AddCell(CreateCell("Contact:", cellFont, true));
                customerTable.AddCell(CreateCell(contact, cellFont, false));
                customerTable.AddCell(CreateCell("Email:", cellFont, true));
                customerTable.AddCell(CreateCell(email, cellFont, false));

                document.Add(customerTable);
                document.Add(new Paragraph("\n")); // Space

                // Ordered Books Section
                Paragraph bookHeader = new Paragraph("Ordered Books", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
                document.Add(bookHeader);
                document.Add(new Paragraph(orderedBook, cellFont));

                // Footer
                Paragraph footer = new Paragraph("\nThank you for shopping with BookHeaven!", footerFont);
                footer.Alignment = Element.ALIGN_CENTER;
                document.Add(footer);

                document.Close();

                // Open the generated PDF
                System.Diagnostics.Process.Start(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Helper function to create formatted table cells
        private PdfPCell CreateCell(string text, iTextSharp.text.Font font, bool isHeader)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, font))
            {
                Padding = 5,
                BackgroundColor = isHeader ? BaseColor.LIGHT_GRAY : BaseColor.WHITE
            };
            return cell;
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
        private void ClearOrderFields()
        {
            cmbCusID.SelectedIndex = 0;
            txtCustomer.Clear();
            txtAddress.Clear();
            txtContact.Clear();
            txtEmail.Clear();
            txtTotal.Clear();
        }
    }
}