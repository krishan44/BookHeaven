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

namespace BookHeaven
{
    public partial class UpdateSupplier : Form
    {
        public UpdateSupplier()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-OEI0948;Initial Catalog=BookHeaven;Integrated Security=True;Connect Timeout=30;Encrypt=False;");

        private void UpdateSupplier_Load(object sender, EventArgs e)
        {
            LoadSupplierIDs();
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

        private void cmbSupId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSupId.SelectedIndex > 0) // Ensure a valid SupplierID is selected (not "--Select--")
            {
                try
                {
                    conn.Open();
                    string selectedSupplierID = cmbSupId.SelectedItem.ToString();
                    string query = "SELECT BusinessName, Address, ContactNumber, AgentName, Email, NIC FROM SuppliersTable WHERE SupplierID = @SupplierID";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@SupplierID", selectedSupplierID);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        txtBname.Text = reader["BusinessName"].ToString();
                        txtAddress.Text = reader["Address"].ToString();
                        txtConNo.Text = reader["ContactNumber"].ToString();
                        txtAgent.Text = reader["AgentName"].ToString();
                        txtEmail.Text = reader["Email"].ToString();
                        txtID.Text = reader["NIC"].ToString();
                        label1.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Supplier details not found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearSupplierDetails();
                    }

                    reader.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading supplier details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    ClearSupplierDetails();
                }
            }
            else
            {
                ClearSupplierDetails(); // If "--Select--" is selected, clear the textboxes
            }
        }

        private void ClearSupplierDetails()
        {
            txtBname.Clear();
            txtAddress.Clear();
            txtConNo.Clear();
            txtAgent.Clear();
            txtEmail.Clear();
            txtID.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = "UPDATE SuppliersTable SET BusinessName = @BusinessName, Address = @Address, ContactNumber = @ContactNumber, AgentName = @AgentName, Email = @Email, NIC = @NIC WHERE SupplierID = @SupplierID";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@SupplierID", cmbSupId.SelectedItem.ToString());
                command.Parameters.AddWithValue("@BusinessName", txtBname.Text);
                command.Parameters.AddWithValue("@Address", txtAddress.Text);
                command.Parameters.AddWithValue("@ContactNumber", txtConNo.Text);
                command.Parameters.AddWithValue("@AgentName", txtAgent.Text);
                command.Parameters.AddWithValue("@Email", txtEmail.Text);
                command.Parameters.AddWithValue("@NIC", txtID.Text);

                int rowsAffected = command.ExecuteNonQuery();
                conn.Close();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Supplier details updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadSupplierIDs(); // Refresh the combo box
                    ClearSupplierDetails();
                }
                else
                {
                    MessageBox.Show("Supplier not found or no changes made.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating supplier: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = "DELETE FROM SuppliersTable WHERE SupplierID = @SupplierID";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@SupplierID", cmbSupId.SelectedItem.ToString());

                int rowsAffected = command.ExecuteNonQuery();
                conn.Close();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Supplier deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadSupplierIDs(); // Refresh the combo box
                    ClearSupplierDetails();
                }
                else
                {
                    MessageBox.Show("Supplier not found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting supplier: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            SupplierMain supplierMain = new SupplierMain();
            supplierMain.Show();
            this.Hide();
        }
    }
}