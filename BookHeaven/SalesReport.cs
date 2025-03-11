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
using System.Windows.Forms.DataVisualization.Charting;
using System.Globalization; // Add this namespace

namespace BookHeaven
{
    public partial class SalesReport : Form
    {
        private readonly string connectionString = @"Data Source=DESKTOP-OEI0948;Initial Catalog=BookHeaven;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

        public SalesReport()
        {
            InitializeComponent();
            PopulateMonths();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            adminDashboard adminDashboard = new adminDashboard();
            adminDashboard.Show();
            this.Hide();
        }

        private void SalesReport_Load(object sender, EventArgs e)
        {
            LoadSalesData();
        }

        private void PopulateMonths()
        {
            cmbMonth.Items.Add("All");
            cmbMonth.Items.Add("January");
            cmbMonth.Items.Add("February");
            cmbMonth.Items.Add("March");
            cmbMonth.Items.Add("April");
            cmbMonth.Items.Add("May");
            cmbMonth.Items.Add("June");
            cmbMonth.Items.Add("July");
            cmbMonth.Items.Add("August");
            cmbMonth.Items.Add("September");
            cmbMonth.Items.Add("October");
            cmbMonth.Items.Add("November");
            cmbMonth.Items.Add("December");

            cmbMonth.SelectedIndex = 0;
        }

        private void LoadSalesData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT OrderedBook, OrderDate, Total
                FROM OrdersTable
                WHERE 1=1";

                    if (!string.IsNullOrEmpty(txtBookName.Text))
                    {
                        query += " AND OrderedBook LIKE @BookName";
                    }

                    if (cmbMonth.SelectedIndex > 0)
                    {
                        query += " AND MONTH(OrderDate) = @Month";
                    }

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (!string.IsNullOrEmpty(txtBookName.Text))
                        {
                            command.Parameters.AddWithValue("@BookName", "%" + txtBookName.Text + "%");
                        }

                        if (cmbMonth.SelectedIndex > 0)
                        {
                            command.Parameters.AddWithValue("@Month", cmbMonth.SelectedIndex);
                        }

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Dictionary<string, int> bookCounts = new Dictionary<string, int>();
                            decimal totalSales = 0; // Initialize totalSales here

                            while (reader.Read())
                            {
                                string orderedBooks = reader["OrderedBook"].ToString();
                                decimal total = Convert.ToDecimal(reader["Total"]);

                                // Split the OrderedBook string and count individual books
                                string[] books = orderedBooks.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (string book in books)
                                {
                                    string trimmedBook = book.Trim();
                                    if (string.IsNullOrEmpty(trimmedBook)) continue;

                                    if (bookCounts.ContainsKey(trimmedBook))
                                    {
                                        bookCounts[trimmedBook]++;
                                    }
                                    else
                                    {
                                        bookCounts[trimmedBook] = 1;
                                    }
                                }

                                // Add the total for the current row to totalSales
                                totalSales += total;
                            }

                            // Update the chart with book counts
                            chartSales.Series.Clear();
                            Series series = new Series("Sales");
                            series.ChartType = SeriesChartType.Column;

                            foreach (var pair in bookCounts)
                            {
                                series.Points.AddXY(pair.Key, pair.Value);
                            }

                            chartSales.Series.Add(series);

                            // Use Sri Lankan Rupee (LKR) format
                            CultureInfo lkCulture = new CultureInfo("en-LK");
                            lblMostSales.Text = "Total Sales: " + totalSales.ToString("C", lkCulture);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading sales data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBookName_TextChanged(object sender, EventArgs e)
        {
            LoadSalesData();
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSalesData();
        }
    }
}