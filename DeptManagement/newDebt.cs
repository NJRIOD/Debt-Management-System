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
   
    public partial class newDebt : Form
    {
        string datetimetoday = DateTime.UtcNow.ToString("MM-dd-yyyy");
        
       
        
        public newDebt()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newDebt_Load(object sender, EventArgs e)
        {
           
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null && textBox2.Text !=null && textBox3.Text != null && textBox4.Text != null && textBox5.Text != null && textBox6.Text != null)
            {
                SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=debtManagementDb;Integrated Security=True");
                SqlCommand cmd = new SqlCommand("Insert into transactionLogs(name,item,price,quantity,dateAdded,description,personIncharge,total) values(@name,@item,@price,@quantity,@dateAdded,@description,@personIncharge,@total)", con);


                con.Open();
                cmd.Parameters.AddWithValue("@name", label6.Text);
                cmd.Parameters.AddWithValue("@item", textBox1.Text);
                cmd.Parameters.AddWithValue("@price", textBox2.Text);
                cmd.Parameters.AddWithValue("@quantity", textBox3.Text);
                cmd.Parameters.AddWithValue("@dateAdded", datetimetoday);
                cmd.Parameters.AddWithValue("@description", textBox4.Text);
                cmd.Parameters.AddWithValue("@personIncharge", textBox5.Text);
                cmd.Parameters.AddWithValue("@total", textBox6.Text);

                cmd.ExecuteNonQuery();

                con.Close();


                UpdateBalance();
                
               
            }

        }
        private void UpdateBalance()
        {
            double balance = Convert.ToDouble(currentBalancetxt.Text);
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=debtManagementDb;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("update debtorInformation set total=@total where id=@id", con);

            con.Open();
            cmd.Parameters.AddWithValue("@id", label8.Text);
            cmd.Parameters.AddWithValue("@total", balance);
            
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Transaction Complete", "Success",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            this.Close();
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            Multiply();         
        }
        private void Multiply()
        {
            double price, quantity,result;

            double.TryParse(textBox2.Text, out price);
            double.TryParse(textBox3.Text, out quantity);

            result = price * quantity;
            if (result > 0)
            {
                textBox6.Text = result.ToString("c").Remove(0,1);
                //label8.Text has ID from main form
                AddTotal();
            }

        }
        private void AddTotal()
        {
            double currentbal, tots, result;

            double.TryParse(currentBalancetxt.Text, out currentbal);
            double.TryParse(textBox6.Text, out tots);

            result = currentbal + tots;
            if (result > 0)
            {
                currentBalancetxt.Text = result.ToString("c").Remove(0, 1);
                //label8.Text has ID from main form

            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {

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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
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

        private void currentBalancetxt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
