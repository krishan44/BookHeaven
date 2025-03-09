using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BookHeaven
{
    public partial class Inventory : Form
    {
        private readonly string connectionString = @"Data Source=DESKTOP-OEI0948;Initial Catalog=BookHeaven;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

        public Inventory()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DashboardStaff dashboardStaff = new DashboardStaff();
            dashboardStaff.Show();
            this.Hide();
        }

        private void Inventory_Load(object sender, EventArgs e)
        {
            LoadBooks();
        }

        private void LoadBooks()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT Title, Author, Genre, ISBN, Price, StockQuantity FROM BooksTable";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridViewInventory.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading books: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBookName_TextChanged(object sender, EventArgs e)
        {
            FilterBooks();
        }

        private void FilterBooks()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT Title, Author, Genre, ISBN, Price, StockQuantity FROM BooksTable";
                    if (!string.IsNullOrEmpty(txtBookName.Text))
                    {
                        query += " WHERE Title LIKE @BookName";
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        if (!string.IsNullOrEmpty(txtBookName.Text))
                        {
                            adapter.SelectCommand.Parameters.AddWithValue("@BookName", "%" + txtBookName.Text + "%");
                        }

                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridViewInventory.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error filtering books: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}