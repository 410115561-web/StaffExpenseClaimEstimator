using System;
using System.Globalization;
using System.Windows.Forms;

namespace StaffExpenseClaimEstimator
{
    public partial class Form1 : Form
    {
        private const decimal MileageRate = 0.85m;
        private const decimal MealLimit = 35m;
        private const decimal ParkingLimit = 40m;
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Clear();

            comboBox1.Items.Add("Mileage");
            comboBox1.Items.Add("Meal");
            comboBox1.Items.Add("Parking");

            comboBox1.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string staff = textBox1.Text.Trim();

            string category = comboBox1.SelectedItem?.ToString() ?? "";

            string description = textBox4.Text.Trim();

            // Validate staff name
            if (staff.Length == 0)
            {
                MessageBox.Show("Please enter the staff name.");
                textBox1.Focus();
                return;
            }

            // Validate description
            if (description.Length == 0)
            {
                MessageBox.Show("Please enter a description or purpose.");
                textBox4.Focus();
                return;
            }

            // Validate category
            if (category == "")
            {
                MessageBox.Show("Please select an expense category.");
                comboBox1.Focus();
                return;
            }

            decimal estimate;
            string message;

            // Mileage calculation
            if (category == "Mileage")
            {
                if (!decimal.TryParse(textBox2.Text, out decimal km))
                {
                    MessageBox.Show("Please enter valid kilometres.");
                    textBox2.Focus();
                    return;
                }

                if (km <= 0)
                {
                    MessageBox.Show("Kilometres must be greater than zero.");
                    textBox2.Focus();
                    return;
                }

                estimate = km * MileageRate;

                message = "Mileage calculated at $0.85 per kilometre.";
            }

            // Meal and Parking calculation
            else
            {
                if (!decimal.TryParse(textBox3.Text, out decimal amount))
                {
                    MessageBox.Show("Please enter a valid expense amount.");
                    textBox3.Focus();
                    return;
                }

                if (amount <= 0)
                {
                    MessageBox.Show("Expense amount must be greater than zero.");
                    textBox3.Focus();
                    return;
                }

                if (category == "Meal")
                {
                    estimate = Math.Min(amount, MealLimit);

                    if (amount > MealLimit)
                    {
                        message = "Meal exceeds $35.00. Estimate adjusted to $35.00.";
                    }
                    else
                    {
                        message = "Meal amount is within the $35.00 limit.";
                    }
                }
                else if (category == "Parking")
                {
                    estimate = Math.Min(amount, ParkingLimit);

                    if (amount > ParkingLimit)
                    {
                        message = "Parking exceeds $40.00. Estimate adjusted to $40.00.";
                    }
                    else
                    {
                        message = "Parking amount is within the $40.00 limit.";
                    }
                }
                else
                {
                    MessageBox.Show("Invalid category. Please select Mileage, Meal or Parking.");
                    return;
                }
            }

            // Round to two decimal places
            estimate = Math.Round(estimate, 2);

            // Display result
            label7.Text = estimate.ToString("C2", CultureInfo.CurrentCulture);

            label8.Text = message;

            // Show expense summary
            MessageBox.Show(
                "Staff: " + staff +
                "\nDate: " + dateTimePicker1.Value.ToString("dd/MM/yyyy") +
                "\nCategory: " + category +
                "\nDescription: " + description +
                "\nEstimated Amount: " + estimate.ToString("C2") +
                "\n\n" + message +
                "\n\nEstimate only; formal claim approval is not included.",
                "Expense Estimate",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
                );

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();

            dateTimePicker1.Value = DateTime.Today;

            comboBox1.SelectedIndex = 0;

            textBox2.Clear();

            textBox3.Clear();

            textBox4.Clear();

            label7.Text = "$0.00";

            label8.Text = "Choose a category and enter the required details.";

            textBox1.Focus();
        }
    }
    
}
