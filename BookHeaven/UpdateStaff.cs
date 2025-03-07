using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookHeaven
{
    public partial class UpdateStaff : Form
    {
        private string connectionString = @"Data Source=DESKTOP-OEI0948;Initial Catalog=BookHeaven;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

        public UpdateStaff()
        {
            InitializeComponent();
        }

        private void UpdateStaff_Load(object sender, EventArgs e)
        {
            LoadStaffIDs();
            cmbStfId.SelectedIndex = 0;
        }

        private void LoadStaffIDs()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT StaffID FROM StaffTable";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    cmbStfId.Items.Clear();
                    cmbStfId.Items.Add("--Select--");  // Add the default option

                    while (reader.Read())
                    {
                        cmbStfId.Items.Add(reader["StaffID"].ToString());
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading staff IDs: " + ex.Message);
                }
            }
        }


        private void cmbStfId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStfId.SelectedIndex == -1) return;  // Check if a valid staff ID is selected
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM StaffTable WHERE StaffID = @StaffID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StaffID", cmbStfId.SelectedItem.ToString());

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtName.Text = reader["FullName"].ToString();
                        txtAddress.Text = reader["Address"].ToString();
                        txtContact.Text = reader["PhoneNumber"].ToString();
                        txtEmail.Text = reader["Email"].ToString();
                        txtNIC.Text = reader["NIC"].ToString();
                        cmbGender.SelectedItem = reader["Gender"].ToString();
                        DoBPick.Value = Convert.ToDateTime(reader["DoB"]);

                        if (reader["Photo"] != DBNull.Value)
                        {
                            byte[] imageData = (byte[])reader["Photo"];
                            MemoryStream ms = new MemoryStream(imageData);
                            picCover.Image = Image.FromStream(ms);
                        }
                        else
                        {
                            picCover.Image = null;
                        }
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading staff details: " + ex.Message);
                }
            }
        }




        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cmbStfId.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a staff ID.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE StaffTable SET FullName=@Name, Address=@Address, PhoneNumber=@Contact, Email=@Email, NIC=@NIC, Gender=@Gender, DoB=@DoB, Photo=@Photo WHERE StaffID=@StaffID";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@StaffID", cmbStfId.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@Contact", txtContact.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@NIC", txtNIC.Text);
                    cmd.Parameters.AddWithValue("@Gender", cmbGender.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@DoB", DoBPick.Value);

                    if (picCover.Image != null)
                    {
                        MemoryStream ms = new MemoryStream();
                        picCover.Image.Save(ms, picCover.Image.RawFormat);
                        cmd.Parameters.AddWithValue("@Photo", ms.ToArray());
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Photo", DBNull.Value);
                    }

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Staff details updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating staff details: " + ex.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (cmbStfId.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a staff ID.");
                return;
            }

            DialogResult confirm = MessageBox.Show("Are you sure you want to delete this staff member?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM StaffTable WHERE StaffID=@StaffID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@StaffID", cmbStfId.SelectedItem.ToString());

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Staff deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        clear();
                        LoadStaffIDs();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting staff: " + ex.Message);
                    }
                }
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                picCover.Image = Image.FromFile(openFileDialog.FileName);
            }
        }

        private void clear()
        {
            picCover.Image = null;
            txtName.Clear();
            txtAddress.Clear();
            txtContact.Clear();
            txtEmail.Clear();
            txtNIC.Clear();
            cmbGender.SelectedIndex = -1;
            DoBPick.Value = DateTime.Now;
            cmbStfId.SelectedIndex = 0;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Staff staff = new Staff();
            staff.Show();
            this.Hide();
        }
    }
}
