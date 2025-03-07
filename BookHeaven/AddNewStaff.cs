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
    public partial class AddNewStaff : Form
    {
        public AddNewStaff()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loginInfo loginInfo = new loginInfo();
            loginInfo.Show();
            this.Hide();
        }
        private string selectedImagePath; // To store the path of the selected image

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

                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void AddNewStaff_Load(object sender, EventArgs e)
        {
            cmbGender.SelectedIndex = 0;
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
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-OEI0948;Initial Catalog=BookHeaven;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
                {
                    conn.Open();

                    // Generate StaffID (STF_01 if no existing records)
                    string staffId = GenerateStaffID(conn);

                    string insertQuery = @"
            INSERT INTO StaffTable (StaffID, FullName, Photo, Email, Gender, PhoneNumber, Address, NIC, DoB) 
            VALUES (@StaffID, @FullName, @Photo, @Email, @Gender, @PhoneNumber, @Address, @NIC, @DoB)";

                    using (SqlCommand command = new SqlCommand(insertQuery, conn))
                    {
                        command.Parameters.AddWithValue("@StaffID", staffId);
                        command.Parameters.AddWithValue("@FullName", txtName.Text);
                        command.Parameters.AddWithValue("@Email", txtEmail.Text);
                        command.Parameters.AddWithValue("@Gender", cmbGender.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@PhoneNumber", txtContact.Text);
                        command.Parameters.AddWithValue("@Address", txtAddress.Text);
                        command.Parameters.AddWithValue("@NIC", txtNIC.Text);
                        command.Parameters.AddWithValue("@DoB", DoBPick.Value); 

                        if (picCover.Image != null)
                        {
                            command.Parameters.AddWithValue("@Photo", ImageToByteArray(picCover.Image));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Photo", DBNull.Value);
                        }

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Staff member added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            clear(); // Clear the form fields
                        }
                        else
                        {
                            MessageBox.Show("Failed to add staff member.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding staff member: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private string GenerateStaffID(SqlConnection conn)
        {
            string staffId = "STF_01"; // Default StaffID

            try
            {
                string query = "SELECT TOP 1 StaffID FROM StaffTable ORDER BY StaffID DESC";
                using (SqlCommand command = new SqlCommand(query, conn))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string lastStaffId = reader["StaffID"].ToString();
                        int lastNumber = int.Parse(lastStaffId.Substring(4)); // Extract the numeric part
                        staffId = "STF_" + (lastNumber + 1).ToString("D2"); // Increment and format
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating StaffID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return staffId;
        }

        private void clear()
        {
            picCover.Image = null;
            txtName.Clear();
            txtAddress.Clear();
            txtContact.Clear();
            txtEmail.Clear();
            txtNIC.Clear();
            cmbGender.SelectedIndex = 0;
            DoBPick.Value = DateTime.Now;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Staff staff = new Staff();
            staff.Show();
            this.Hide();

        }
    }
}
