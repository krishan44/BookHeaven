using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using BCrypt.Net;
using System.Threading.Tasks;


namespace BookHeaven
{
    public partial class loginInfo : Form
    {
        private readonly string connectionString = @"Data Source=DESKTOP-OEI0948;Initial Catalog=BookHeaven;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

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
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT StaffID FROM StaffTable WHERE UserID IS NULL"; // Only load StaffIDs with no UserID
                    using (SqlCommand command = new SqlCommand(query, conn))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        cmbStfID.Items.Clear();
                        cmbStfID.Items.Add("--Select--");
                        cmbStfID.SelectedIndex = 0;

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

        private void cmbStfID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStfID.SelectedIndex > 0)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
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
                                    txtName.Clear();
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
                txtName.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Check if the username already exists
                    string checkUsernameQuery = "SELECT COUNT(*) FROM UserTable WHERE Username = @Username";
                    using (SqlCommand checkUsernameCommand = new SqlCommand(checkUsernameQuery, conn))
                    {
                        checkUsernameCommand.Parameters.AddWithValue("@Username", txtUsername.Text);
                        int usernameCount = (int)checkUsernameCommand.ExecuteScalar();

                        if (usernameCount > 0)
                        {
                            MessageBox.Show("Username already exists. Please choose a different username.", "Duplicate Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return; // Exit the method if the username already exists
                        }
                    }

                    string userId = GenerateUserID(conn);
                    string passwordHash = HashPassword(txtPassword.Text);

                    string insertUserQuery = @"INSERT INTO UserTable (UserID, Username, PasswordHash, Role) VALUES (@UserID, @Username, @PasswordHash, @Role)";

                    using (SqlCommand userCommand = new SqlCommand(insertUserQuery, conn))
                    {
                        userCommand.Parameters.AddWithValue("@UserID", userId);
                        userCommand.Parameters.AddWithValue("@Username", txtUsername.Text);
                        userCommand.Parameters.AddWithValue("@PasswordHash", passwordHash);
                        userCommand.Parameters.AddWithValue("@Role", "Staff");

                        int userRowsAffected = userCommand.ExecuteNonQuery();

                        if (userRowsAffected > 0)
                        {
                            if (cmbStfID.SelectedIndex > 0)
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
                                        LoadStaffIDs(); //Reload Staff IDs to update combobox
                                    }
                                    else
                                    {
                                        MessageBox.Show("User created, but failed to update Staff record.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("User created, but no staff member selected.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            string userId = "USER_01";

            try
            {
                string query = "SELECT TOP 1 UserID FROM UserTable ORDER BY UserID DESC";
                using (SqlCommand command = new SqlCommand(query, conn))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string lastUserId = reader["UserID"].ToString();
                        int lastNumber = int.Parse(lastUserId.Substring(5));
                        userId = "USER_" + (lastNumber + 1).ToString("D2");
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

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Username cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Password cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbStfID.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select a Staff Member.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}