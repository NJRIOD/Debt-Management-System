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

namespace DeptManagement
{
    public partial class NewIndividual : Form
    {
        string datetoday;
        string status;

        public NewIndividual()
        {
            InitializeComponent();
           
        }

        private void NewIndividual_Load(object sender, EventArgs e)
        {
            datetoday = dateTimePicker1.Value.ToString("MM-dd-yyyy");
            status = "UNPAID";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            NewInformationLogs();


        }
        private void NewInformationLogs()
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=debtManagementDb;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("Insert into transactionLogs(name,item,price,quantity,dateAdded,description,personIncharge,total) values(@name,@item,@price,@quantity,@dateAdded,@description,@personIncharge,@total)", con);


            con.Open();
            cmd.Parameters.AddWithValue("@name", textBox1.Text);
            cmd.Parameters.AddWithValue("@item", textBox2.Text);
            cmd.Parameters.AddWithValue("@price", textBox3.Text);
            cmd.Parameters.AddWithValue("@quantity", textBox5.Text);           
            cmd.Parameters.AddWithValue("@description", textBox4.Text);
            cmd.Parameters.AddWithValue("@personIncharge", textBox7.Text);
            cmd.Parameters.AddWithValue("@total", textBox6.Text);
            cmd.Parameters.AddWithValue("@dateAdded", datetoday);

            cmd.ExecuteNonQuery();

            con.Close();
            NewInformation();
        }
        private void NewInformation()
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=debtManagementDb;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("Insert into debtorInformation(name,total,status,dateModified) values(@name,@total,@status,@dateModified)", con);


            con.Open();
            cmd.Parameters.AddWithValue("@name", textBox1.Text);           
            cmd.Parameters.AddWithValue("@total", textBox6.Text);
            cmd.Parameters.AddWithValue("@status",status);
            cmd.Parameters.AddWithValue("@dateModified", datetoday);
            

            cmd.ExecuteNonQuery();

            con.Close();
            MessageBox.Show("Transaction Complete", "Success",
            MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        private void Multiply()
        {
            double price, quantity, result;

            double.TryParse(textBox3.Text, out price);
            double.TryParse(textBox5.Text, out quantity);

            result = price * quantity;
            if (result > 0)
            {
                textBox6.Text = result.ToString("c").Remove(0, 1);
                
                
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Multiply();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            datetoday = dateTimePicker1.Value.ToString("MM-dd-yyyy");
        }
    }
}
