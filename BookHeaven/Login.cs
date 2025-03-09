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
using BCrypt.Net; 

namespace BookHeaven
{
    public partial class Login : Form
    {
        private readonly string connectionString = @"Data Source=DESKTOP-OEI0948;Initial Catalog=BookHeaven;Integrated Security=True;Connect Timeout=30;Encrypt=False;"; // Replace with your connection string

        public Login()
        {
            InitializeComponent();
        }

        private void txtUsername_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
        }

        private void txtPassword_Click(object sender, EventArgs e)
        {
            txtPassword.Clear();
            txtPassword.PasswordChar = '*'; // Set password character
        }

        private void Login_Load(object sender, EventArgs e)
        {
            // Optional: You can add code here to set default values or perform other initialization tasks
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Role, PasswordHash FROM UserTable WHERE Username = @Username";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string role = reader["Role"].ToString();
                                string hashedPassword = reader["PasswordHash"].ToString(); // Corrected column name

                                if (BCrypt.Net.BCrypt.Verify(password, hashedPassword))
                                {
                                    if (role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                                    {
                                        adminDashboard adminDashboard = new adminDashboard();
                                        adminDashboard.Show();
                                        this.Hide();
                                    }
                                    else if (role.Equals("Staff", StringComparison.OrdinalIgnoreCase))
                                    {
                                        DashboardStaff dashboardStaff = new DashboardStaff();
                                        dashboardStaff.Show();
                                        this.Hide();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Invalid Role.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Incorrect password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Username not found.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred during login: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
        }

       

        private void txtPassword_TabIndexChanged(object sender, EventArgs e)
        {
            txtPassword.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}