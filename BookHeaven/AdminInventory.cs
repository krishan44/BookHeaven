using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookHeaven
{
    public partial class AdminInventory : Form
    {
        private readonly string connectionString = @"Data Source=DESKTOP-OEI0948;Initial Catalog=BookHeaven;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

        public AdminInventory()
        {
            InitializeComponent();
        }

        private async void AdminInventory_Load(object sender, EventArgs e)
        {
            await LoadBooksToDataGridView();
            await LoadBookIDsToComboBox();
        }

        private async Task LoadBooksToDataGridView()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT BookID, Title, Genre, Author, Price, StockQuantity FROM BooksTable";
                    using (SqlCommand command = new SqlCommand(query, conn))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataViewBooks.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading books: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadBookIDsToComboBox()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT BookID FROM BooksTable";
                    using (SqlCommand command = new SqlCommand(query, conn))
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        cmbBookID.Items.Clear();
                        cmbBookID.Items.Add("--Select--");
                        cmbBookID.SelectedIndex = 0;

                        while (await reader.ReadAsync())
                        {
                            cmbBookID.Items.Add(reader["BookID"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Book IDs: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            ReportMain reportMain = new ReportMain();
            reportMain.Show();
            this.Hide();
        }

        private async void cmbBookID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBookID.SelectedIndex <= 0 || cmbBookID.SelectedItem == null)
            {
                ClearBookNameAndSupplierFields();
                return;
            }

            string selectedBookID = cmbBookID.SelectedItem.ToString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();

                    // Fetch Book Details
                    string bookQuery = "SELECT Title, SupplierID FROM BooksTable WHERE BookID = @BookID";
                    string bookTitle = null;
                    string supplierID = null;

                    using (SqlCommand bookCommand = new SqlCommand(bookQuery, conn))
                    {
                        bookCommand.Parameters.AddWithValue("@BookID", selectedBookID);
                        using (SqlDataReader bookReader = await bookCommand.ExecuteReaderAsync())
                        {
                            if (await bookReader.ReadAsync())
                            {
                                bookTitle = bookReader["Title"].ToString();
                                supplierID = bookReader["SupplierID"].ToString();
                            }
                        }
                    }

                    // If no book is found, clear fields and return
                    if (string.IsNullOrEmpty(bookTitle))
                    {
                        ClearBookNameAndSupplierFields();
                        return;
                    }

                    txtBookName.Text = bookTitle;

                    // Fetch Supplier Details if supplierID is valid
                    if (!string.IsNullOrEmpty(supplierID))
                    {
                        string supplierQuery = "SELECT BusinessName, ContactNumber, Email FROM SuppliersTable WHERE SupplierID = @SupplierID";
                        using (SqlCommand supplierCommand = new SqlCommand(supplierQuery, conn))
                        {
                            supplierCommand.Parameters.AddWithValue("@SupplierID", supplierID);
                            using (SqlDataReader supplierReader = await supplierCommand.ExecuteReaderAsync())
                            {
                                if (await supplierReader.ReadAsync())
                                {
                                    txtSupplier.Text = supplierReader["BusinessName"].ToString();
                                    txtConNo.Text = supplierReader["ContactNumber"].ToString();
                                    txtEmail.Text = supplierReader["Email"].ToString();
                                }
                                else
                                {
                                    ClearSupplierFields(); // Clear if supplier not found
                                }
                            }
                        }
                    }
                    else
                    {
                        ClearSupplierFields(); // No supplier associated with book
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading book details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearBookNameAndSupplierFields();
            }
        }

        private void ClearBookNameAndSupplierFields()
        {
            txtBookName.Clear();
            ClearSupplierFields();
        }

        private void ClearSupplierFields()
        {
            txtSupplier.Clear();
            txtConNo.Clear();
            txtEmail.Clear();
        }
    }
}
