using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookHeaven
{
    public partial class UpdateBook : Form
    {
        public UpdateBook()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-OEI0948;Initial Catalog=BookHeaven;Integrated Security=True;Connect Timeout=30;Encrypt=False;");

        private void UpdateBook_Load(object sender, EventArgs e)
        {
            LoadBookIDs();
            LoadSupplierIDs();
            cmbGenre.SelectedIndex = 0;
            cmbBookId.SelectedIndex = 0;
            btnUploadCover.Enabled = false;
            label1.Focus();
        }

        private void LoadBookIDs()
        {
            try
            {
                conn.Open();
                string query = "SELECT BookID FROM BooksTable";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();

                cmbBookId.Items.Clear();
                cmbBookId.Items.Add("--Select--");
                //cmbBookId.SelectedIndex = 0;

                while (reader.Read())
                {
                    cmbBookId.Items.Add(reader["BookID"].ToString());
                }

                reader.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Book IDs: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        private void LoadSupplierIDs()
        {
            try
            {
                conn.Open();
                string query = "SELECT SupplierID FROM SuppliersTable"; // Corrected table name
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();

                cmbSupId.Items.Clear();
                cmbSupId.Items.Add("--Select--");
                cmbSupId.SelectedIndex = 0;

                while (reader.Read())
                {
                    cmbSupId.Items.Add(reader["SupplierID"].ToString());
                }

                reader.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Supplier IDs: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        private bool isImageUploaded = false;
        private void cmbBookId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBookId.SelectedIndex > 0)
            {
                try
                {
                    conn.Open();
                    string selectedBookId = cmbBookId.SelectedItem.ToString();
                    string query = "SELECT Title, Author, Genre, ISBN, Price, StockQuantity, SupplierID, BookImage FROM BooksTable WHERE BookId = @BookId";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@BookId", selectedBookId);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        txtBookTitle.Text = reader["Title"].ToString();
                        txtAuthor.Text = reader["Author"].ToString();

                        if (reader["Genre"] != DBNull.Value)
                        {
                            cmbGenre.SelectedItem = reader["Genre"].ToString();
                        }
                        else
                        {
                            cmbGenre.SelectedIndex = 0;
                        }

                        txtISBN.Text = reader["ISBN"].ToString();
                        txtPrice.Text = reader["Price"].ToString();
                        txtStock.Text = reader["StockQuantity"].ToString();

                        if (reader["SupplierID"] != DBNull.Value)
                        {
                            cmbSupId.SelectedItem = reader["SupplierID"].ToString();
                        }
                        else
                        {
                            cmbSupId.SelectedIndex = 0;
                        }

                        if (reader["BookImage"] != DBNull.Value)
                        {
                            byte[] imageBytes = (byte[])reader["BookImage"];
                            picCover.Image = ByteArrayToImage(imageBytes);
                            picCover.SizeMode = PictureBoxSizeMode.Zoom;

                            if (!isImageUploaded) // Check the flag before moving the button
                            {
                                btnUploadCover.Location = new Point(btnUploadCover.Location.X, btnUploadCover.Location.Y + 140);
                                isImageUploaded = true; // Set the flag after moving
                            }

                            btnUploadCover.Enabled = true;
                            label1.Focus();
                        }
                        else
                        {
                            picCover.Image = null;
                        }
                    }
                    else
                    {
                        ClearFields();
                    }

                    reader.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading book details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    ClearFields();
                }
            }
            else
            {
                ClearFields();
            }
        }

        private void ClearFields()
        {
            txtBookTitle.Clear();
            txtAuthor.Clear();
            cmbGenre.SelectedIndex = 0;
            txtISBN.Clear();
            txtPrice.Clear();
            cmbBookId.SelectedIndex = 0;
            txtStock.Clear();
            cmbSupId.SelectedIndex = 0;
            picCover.Image = null;
            btnUploadCover.Location = new Point(btnUploadCover.Location.X, btnUploadCover.Location.Y);
            isImageUploaded = false;
        }

        public Image ByteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                Image returnImage = Image.FromStream(ms);
                return returnImage;
            }
        }

        private void cmbSupId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSupId.SelectedIndex > 0)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-OEI0948;Initial Catalog=BookHeaven;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
                    {
                        conn.Open();
                        string selectedSupplierID = cmbSupId.SelectedItem.ToString();
                        string query = "SELECT BusinessName FROM SuppliersTable WHERE SupplierID = @SupplierID";
                        using (SqlCommand command = new SqlCommand(query, conn))
                        {
                            command.Parameters.AddWithValue("@SupplierID", selectedSupplierID);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    if (reader["BusinessName"] != DBNull.Value)
                                    {
                                        txtSupplier.Text = reader["BusinessName"].ToString();
                                    }
                                    else
                                    {
                                        txtSupplier.Clear();
                                    }
                                }
                                else
                                {
                                    txtSupplier.Clear();
                                }
                                
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading Business Name: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSupplier.Clear();
                }
            }
            else
            {
                txtSupplier.Clear();
            }
        }
        private string selectedImagePath;
        private void btnUploadCover_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";
            openFileDialog.Title = "Select Book Cover Image";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    selectedImagePath = openFileDialog.FileName;
                    picCover.Image = Image.FromFile(selectedImagePath);
                    picCover.SizeMode = PictureBoxSizeMode.Zoom;

                    if (!isImageUploaded) // Only adjust location on the first upload
                    {
                        btnUploadCover.Location = new Point(btnUploadCover.Location.X, btnUploadCover.Location.Y + 140);
                        isImageUploaded = true; // Set the flag
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-OEI0948;Initial Catalog=BookHeaven;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
                {
                    conn.Open();

                    if (cmbBookId.SelectedIndex <= 0)
                    {
                        MessageBox.Show("Please select a Book ID to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string selectedBookId = cmbBookId.SelectedItem.ToString();
                    string updateQuery = @"
        UPDATE BooksTable
        SET
            Title = @Title,
            Author = @Author,
            Genre = @Genre,
            ISBN = @ISBN,
            Price = @Price,
            StockQuantity = @StockQuantity,
            SupplierID = @SupplierID,
            BookImage = @BookImage,
            Discount = @Discount
        WHERE
            BookId = @BookId";

                    using (SqlCommand command = new SqlCommand(updateQuery, conn))
                    {
                        command.Parameters.AddWithValue("@BookId", selectedBookId);
                        command.Parameters.AddWithValue("@Title", txtBookTitle.Text);
                        command.Parameters.AddWithValue("@Author", txtAuthor.Text);
                        command.Parameters.AddWithValue("@Genre", cmbGenre.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@ISBN", txtISBN.Text);
                        command.Parameters.AddWithValue("@Price", txtPrice.Text);
                        command.Parameters.AddWithValue("@StockQuantity", txtStock.Text);
                        command.Parameters.AddWithValue("@SupplierID", cmbSupId.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@Discount", txtDiscount.Text); // Added discount parameter

                        if (picCover.Image != null)
                        {
                            command.Parameters.AddWithValue("@BookImage", ImageToByteArray(picCover.Image));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@BookImage", DBNull.Value);
                        }

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Book updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearFields();
                            btnUploadCover.Location = new Point(btnUploadCover.Location.X, btnUploadCover.Location.Y - 140);
                        }
                        else
                        {
                            MessageBox.Show("Book update failed. No matching Book ID found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating book: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private byte[] ImageToByteArray(Image imageIn)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            NewBook newBook = new NewBook();
            newBook.Show();
            this.Hide();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-OEI0948;Initial Catalog=BookHeaven;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
                {
                    conn.Open();

                    if (cmbBookId.SelectedIndex <= 0)
                    {
                        MessageBox.Show("Please select a Book ID to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string selectedBookId = cmbBookId.SelectedItem.ToString();
                    string deleteQuery = "DELETE FROM BooksTable WHERE BookId = @BookId";

                    using (SqlCommand command = new SqlCommand(deleteQuery, conn))
                    {
                        command.Parameters.AddWithValue("@BookId", selectedBookId);

                        // Prompt the user for confirmation
                        DialogResult result = MessageBox.Show("Are you sure you want to delete this book?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Book deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ClearFields();
                                LoadBookIDs(); // Refresh the Book IDs in the combo box
                                btnUploadCover.Location = new Point(btnUploadCover.Location.X, btnUploadCover.Location.Y - 140);
                                cmbBookId.SelectedIndex = 0;
                            }
                            else
                            {
                                MessageBox.Show("Book deletion failed. No matching Book ID found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting book: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    }