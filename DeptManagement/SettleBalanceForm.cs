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
    public partial class SettleBalanceForm : Form
    {
        string datetimetoday = DateTime.UtcNow.ToString("MM-dd-yyyy");
        decimal currentbalance;
        public SettleBalanceForm()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to save?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                
                double balance, amount, result;

                double.TryParse(balancetextbox.Text, out balance);
                double.TryParse(textBox2.Text, out amount);

                result = balance - amount;
                if (result > 0)
                {
                    balancetextbox.Text = result.ToString("c").Remove(0, 1);
                    SettleBalance();
                    

                }
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
            
        }
        private void SettleBalance()
        {
            double balance = Convert.ToDouble(balancetextbox.Text);
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=debtManagementDb;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("update debtorInformation set total=@total where id=@id", con);

            con.Open();
            cmd.Parameters.AddWithValue("@id", label2.Text);
            cmd.Parameters.AddWithValue("@total", balance);

            cmd.ExecuteNonQuery();
            con.Close();
            PaymentLog();
        }
        private void PaymentLog()
        {
            double balance = Convert.ToDouble(balancetextbox.Text);
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=debtManagementDb;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("Insert into paymentLogs(name,amount,date,currentBalance,remainingBalance) values(@name,@amount,@date,@currentBalance,@remainingBalance)", con);


            con.Open();
            cmd.Parameters.AddWithValue("@name", settleBalanceName.Text);
            cmd.Parameters.AddWithValue("@amount", textBox2.Text);
            cmd.Parameters.AddWithValue("@date", datetimetoday);
            cmd.Parameters.AddWithValue("@currentBalance", currentbalance);
            cmd.Parameters.AddWithValue("@remainingBalance", balance);


            cmd.ExecuteNonQuery();

            con.Close();
            MessageBox.Show("Transaction Complete", "Success",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void SettleBalanceForm_Load(object sender, EventArgs e)
        {
            currentbalance = Convert.ToDecimal(balancetextbox.Text);
        }
    }
}
