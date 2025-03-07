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
using BCrypt.Net;

namespace BookHeaven
{
    public partial class loginInfo : Form
    {
        public loginInfo()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            AddNewStaff addNewStaff = new AddNewStaff();
            addNewStaff.Show();
            this.Hide();
        }

        private void loginInfo_Load(object sender, EventArgs e)
        {
            LoadStaffIDs();
        }

        private void LoadStaffIDs()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-OEI0948;Initial Catalog=BookHeaven;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
                {
                    conn.Open();
                    string query = "SELECT StaffID FROM StaffTable";
                    using (SqlCommand command = new SqlCommand(query, conn))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        cmbStfID.Items.Clear();
                        cmbStfID.Items.Add("--Select--"); // Add a default selection
                        cmbStfID.SelectedIndex = 0; // Select the default item

                        while (reader.Read())
                        {
                            cmbStfID.Items.Add(reader["StaffID"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Staff IDs: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cmbStfID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStfID.SelectedIndex > 0) // Ensure a valid StaffID is selected (not "--Select--")
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-OEI0948;Initial Catalog=BookHeaven;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
                    {
                        conn.Open();
                        string selectedStaffID = cmbStfID.SelectedItem.ToString();
                        string query = "SELECT FullName FROM StaffTable WHERE StaffID = @StaffID";
                        using (SqlCommand command = new SqlCommand(query, conn))
                        {
                            command.Parameters.AddWithValue("@StaffID", selectedStaffID);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    txtName.Text = reader["FullName"].ToString();
                                }
                                else
                                {
                                    txtName.Clear(); // Clear if StaffID not found.
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading Staff Name: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtName.Clear();
                }
            }
            else
            {
                txtName.Clear(); // Clear if "--Select--" is selected.
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-OEI0948;Initial Catalog=BookHeaven;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
                {
                    conn.Open();

                    // Generate UserID
                    string userId = GenerateUserID(conn);

                    // Hash the password
                    string passwordHash = HashPassword(txtPassword.Text);

                    // Insert into UserTable
                    string insertUserQuery = @"
                INSERT INTO UserTable (UserID, Username, PasswordHash, Role) 
                VALUES (@UserID, @Username, @PasswordHash, @Role)";

                    using (SqlCommand userCommand = new SqlCommand(insertUserQuery, conn))
                    {
                        userCommand.Parameters.AddWithValue("@UserID", userId);
                        userCommand.Parameters.AddWithValue("@Username", txtUsername.Text);
                        userCommand.Parameters.AddWithValue("@PasswordHash", passwordHash);
                        userCommand.Parameters.AddWithValue("@Role", "Staff");

                        int userRowsAffected = userCommand.ExecuteNonQuery();

                        if (userRowsAffected > 0)
                        {
                            // Update StaffTable with UserID
                            if (cmbStfID.SelectedIndex > 0) // Ensure a staff member is selected
                            {
                                string staffId = cmbStfID.SelectedItem.ToString();
                                string updateUserQuery = "UPDATE StaffTable SET UserID = @UserID WHERE StaffID = @StaffID";

                                using (SqlCommand updateStaffCommand = new SqlCommand(updateUserQuery, conn))
                                {
                                    updateStaffCommand.Parameters.AddWithValue("@UserID", userId);
                                    updateStaffCommand.Parameters.AddWithValue("@StaffID", staffId);

                                    int staffRowsAffected = updateStaffCommand.ExecuteNonQuery();

                                    if (staffRowsAffected > 0)
                                    {
                                        MessageBox.Show("User and Staff record updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        ClearFields();
                                    }
                                    else
                                    {
                                        MessageBox.Show("User created, but failed to update Staff record.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("User created, but no staff member selected to associate with.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                ClearFields();
                            }

                        }
                        else
                        {
                            MessageBox.Show("Failed to create user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerateUserID(SqlConnection conn)
        {
            string userId = "USER_01"; // Default UserID

            try
            {
                string query = "SELECT TOP 1 UserID FROM UserTable ORDER BY UserID DESC";
                using (SqlCommand command = new SqlCommand(query, conn))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string lastUserId = reader["UserID"].ToString();
                        int lastNumber = int.Parse(lastUserId.Substring(5)); // Extract the numeric part
                        userId = "USER_" + (lastNumber + 1).ToString("D2"); // Increment and format
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating UserID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return userId;
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
        }

        private void ClearFields()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            cmbStfID.SelectedIndex = 0;
        }
    }
}
