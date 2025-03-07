using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BookHeaven
{
    public partial class NewOrder : Form
    {
        private readonly string connectionString = @"Data Source=DESKTOP-OEI0948;Initial Catalog=BookHeaven;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
        private ListBox bookListBox;

        // OrderData.cs
        public static class OrderData
        {
            public static string BookName { get; set; }
        }
        public NewOrder()
        {
            InitializeComponent();
            InitializeBookListBox();
        }

        private void InitializeBookListBox()
        {
            bookListBox = new ListBox();
            bookListBox.Visible = false;
            bookListBox.Dock = DockStyle.None;
            bookListBox.Location = new Point(txtBookName.Left, txtBookName.Bottom);
            bookListBox.Width = txtBookName.Width;
            bookListBox.Height = 150;
            bookListBox.SelectedIndexChanged += BookListBox_SelectedIndexChanged;
            this.Controls.Add(bookListBox);
            bookListBox.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OrderPayment orderPayment = new OrderPayment();
            orderPayment.Show();
            this.Hide();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DashboardStaff dashboardStaff = new DashboardStaff();
            dashboardStaff.Show();
            this.Hide();
        }

        private void txtBookName_TextChanged(object sender, EventArgs e)
        {
            LoadBookTitles(txtBookName.Text);
        }

        private void txtBookName_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBookName.Text))
            {
                LoadBookTitles("");
            }
        }

        private void LoadBookTitles(string searchText)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT Title FROM BooksTable";
                    if (!string.IsNullOrEmpty(searchText))
                    {
                        query += " WHERE Title LIKE @SearchText";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(searchText))
                        {
                            cmd.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");
                        }

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            bookListBox.Items.Clear();
                            while (reader.Read())
                            {
                                bookListBox.Items.Add(reader["Title"].ToString());
                            }
                        }
                    }
                }

                bookListBox.Visible = bookListBox.Items.Count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading book titles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BookListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bookListBox.SelectedIndex != -1)
            {
                txtBookName.Text = bookListBox.SelectedItem.ToString();
                bookListBox.Visible = false;
                OrderData.BookName = txtBookName.Text; // Set the global variable
                LoadBookDetails(txtBookName.Text);
            }
        }

        private void LoadBookDetails(string bookTitle)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT ISBN, Author, StockQuantity, Price, BookImage FROM BooksTable WHERE Title = @Title";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Title", bookTitle);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtISBN.Text = reader["ISBN"].ToString();
                                txtAuthor.Text = reader["Author"].ToString();
                                txtStock.Text = reader["StockQuantity"].ToString();
                                txtPrice.Text = reader["Price"].ToString();

                                if (reader["BookImage"] != DBNull.Value)
                                {
                                    byte[] imageData = (byte[])reader["BookImage"];
                                    using (MemoryStream ms = new MemoryStream(imageData))
                                    {
                                        picCover.Image = Image.FromStream(ms);
                                    }
                                    picCover.SizeMode = PictureBoxSizeMode.Zoom;
                                }
                                else
                                {
                                    picCover.Image = null;
                                }
                            }
                            else
                            {
                                ClearBookDetails();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading book details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearBookDetails();
            }
        }

        private void ClearBookDetails()
        {
            txtISBN.Clear();
            txtAuthor.Clear();
            txtStock.Clear();
            txtPrice.Clear();
            picCover.Image = null;
        }

        
        private void NewOrder_Load(object sender, EventArgs e)
        {

        }
    }
}