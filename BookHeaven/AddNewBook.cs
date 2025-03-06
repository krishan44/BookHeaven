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
using System.IO;

namespace BookHeaven
{
    public partial class AddNewBook : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-OEI0948;Initial Catalog=BookHeaven;Integrated Security=True;Connect Timeout=30;Encrypt=False;");

        public AddNewBook()
        {
            InitializeComponent();
        }

        private void AddNewBook_Load(object sender, EventArgs e)
        {
            LoadSupplierIDs();
            cmbGenre.SelectedIndex = 0;
        }

        private void LoadSupplierIDs()
        {
            try
            {
                conn.Open();
                string query = "SELECT SupplierID FROM SuppliersTable";
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

        private void cmbSupID_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            NewBook newBook = new NewBook();
            newBook.Show();
            this.Hide();
        }

        private void cmbSupId_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbSupId.SelectedIndex > 0) // Ensure a valid SupplierID is selected (not "--Select--")
            {
                try
                {
                    conn.Open();
                    string selectedSupplierID = cmbSupId.SelectedItem.ToString();
                    string query = "SELECT BusinessName FROM SuppliersTable WHERE SupplierID = @SupplierID";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@SupplierID", selectedSupplierID);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        txtSupplier.Text = reader["BusinessName"].ToString();
                    }
                    else
                    {
                        txtSupplier.Clear(); // Clear if supplier not found.
                    }
                    label1.Focus();
                    reader.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading Business Name: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    txtSupplier.Clear();
                }
            }
            else
            {
                txtSupplier.Clear(); // Clear if "--Select--" is selected.
            }
        }
        private string selectedImagePath; // To store the path of the selected image

        private bool isImageUploaded = false; // Flag to track if an image has been uploaded

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

        public byte[] ImageToByteArray(Image imageIn)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        public Image ByteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                Image returnImage = Image.FromStream(ms);
                return returnImage;
            }
        }

        private void clearFields()
        {
            txtBookTitle.Clear();
            txtAuthor.Clear();
            txtPrice.Clear();
            txtSupplier.Clear();
            txtStock.Clear();
            cmbSupId.SelectedIndex = 0;
            cmbGenre.SelectedIndex = 0;
            picCover.Image = null;
            selectedImagePath = null;
            txtISBN.Clear();
            btnUploadCover.Location = new Point(btnUploadCover.Location.X, btnUploadCover.Location.Y - 140);
            isImageUploaded = false; // Set the flag
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string bookID = "";

            try
            {
                string query = "SELECT TOP 1 BookId FROM BooksTable ORDER BY BookId DESC";
                SqlCommand command = new SqlCommand(query, conn);

                conn.Open();
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    string lastBookID = result.ToString();
                    string numericPart = lastBookID.Substring(3);
                    int lastIndex = int.Parse(numericPart);
                    int newIndex = lastIndex + 1;
                    bookID = "BK_" + newIndex.ToString("D2");
                }
                else
                {
                    bookID = "BK_01";
                }

                string insertQuery = "INSERT INTO BooksTable (BookId, Title, Author, Genre, ISBN, Price, BookImage, StockQuantity, SupplierID) VALUES (@BookId, @Title, @Author, @Genre, @ISBN, @Price, @BookImage, @Stock, @SupplierID)";

                command = new SqlCommand(insertQuery, conn);
                command.Parameters.AddWithValue("@BookId", bookID);
                command.Parameters.AddWithValue("@Title", txtBookTitle.Text);
                command.Parameters.AddWithValue("@Author", txtAuthor.Text);
                command.Parameters.AddWithValue("@Genre", cmbGenre.SelectedItem.ToString());
                command.Parameters.AddWithValue("@ISBN", txtISBN.Text);
                command.Parameters.AddWithValue("@Price", txtPrice.Text);
                if (picCover.Image != null)
                {
                    command.Parameters.AddWithValue("@BookImage", ImageToByteArray(picCover.Image));
                }
                else
                {
                    command.Parameters.AddWithValue("@BookImage", DBNull.Value);
                }
                command.Parameters.AddWithValue("@Stock", txtStock.Text);
                command.Parameters.AddWithValue("@SupplierID", cmbSupId.SelectedItem.ToString());

                command.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Book added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clearFields();
                if (picCover.Image != null)
                {
                    try
                    {
                        byte[] imageBytes = ImageToByteArray(picCover.Image);
                        command.Parameters.AddWithValue("@BookImage", imageBytes);
                    }
                    catch (Exception imageEx)
                    {
                        MessageBox.Show($"Error converting image: {imageEx.Message}", "Image Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        command.Parameters.AddWithValue("@BookImage", DBNull.Value);
                    }

                }
                else
                {
                    command.Parameters.AddWithValue("@BookImage", DBNull.Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding book: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
    }
}