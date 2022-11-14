using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms.DataVisualization.Charting;

namespace DeptManagement
{
    public partial class mainMenu : Form
    {
        public string idval;
        public string displayTotal;
        public double totalBalance;
        public mainMenu()
        {
            InitializeComponent();

            

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = dashboard;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = accounts;
            

        }

        private void button5_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = list;
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            
        }

        private void mainMenu_Load(object sender, EventArgs e)
        {

            var loginform = new Form1();
            loginform.Close();
            FillChart();
            
            

        }
        public void LoadDbData() // load Transaction Logs
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=debtManagementDb;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("Select * from transactionLogs", con);
            DataTable dt = new DataTable();

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();    
            dt.Load(sdr);
            con.Close();

            dataGridView3.DataSource = dt;
            LoadinformationDb();
        }

        public void LoadinformationDb() // load Debtors Information
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=debtManagementDb;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("Select * from debtorInformation", con);
            DataTable dt = new DataTable();

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            dataGridView1.DataSource = dt;
            LoadPaymentsLogsDb();
        }

        public void LoadPaymentsLogsDb() //load Payments Logs
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=debtManagementDb;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("Select * from paymentLogs", con);
            DataTable dt = new DataTable();

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            dataGridView2.DataSource = dt;
            TotalDebts();
        }
        public void FillChart() //load pie Chart
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=debtManagementDb;Integrated Security=True");
            DataSet ds = new DataSet();
            con.Open();
            SqlDataAdapter adapt = new SqlDataAdapter("Select total, name from debtorInformation", con);
            adapt.Fill(ds, "s1");


            chart1.DataSource = ds.Tables["s1"];
            chart1.Series["s1"].XValueMember = "name";
            chart1.Series["s1"].YValueMembers = "total";       
            chart1.Series["s1"].IsValueShownAsLabel = true;

            con.Close();

            LoadDbData();
        }
        public void TotalDebts()
        {
            int sum = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                sum += Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value);
            }
            label11.Text = sum.ToString("c").Remove(0, 1);
            NoOfDebtors();
        }
        public void NoOfDebtors()
        {
            int count;
            count = dataGridView1.Rows.Count - 1;
            label12.Text = count.ToString();
            TotalAmountPaid();
        }
        public void TotalAmountPaid()
        {
            int sum = 0;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                sum += Convert.ToInt32(dataGridView2.Rows[i].Cells[2].Value);
            }
            label15.Text = sum.ToString("c").Remove(0, 1);
        }
        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (idval == null)
            {
                MessageBox.Show("No Data Given, please select in the table before proceeding ", "No Value Given",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                var addDebtform = new newDebt();
                addDebtform.label6.Text = nametxt.Text;
                addDebtform.label8.Text = idval;
                addDebtform.currentBalancetxt.Text = displayTotal;              
                addDebtform.ShowDialog();
                nametxt.Text = null;
                total.Text = null;
                statustxt.Text = null;
                datamtxt.Text = null;
                idval = null;
            }
        }
        //DataGrid rows Click Populate to textboxes and Labels.
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) 
        {
           
            if (e.RowIndex >= 0)
            {
                
                idval = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                nametxt.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                total.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                displayTotal = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                statustxt.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                datamtxt.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                CurencyFormat();
            }
        }
        //format string to curency
        private void CurencyFormat()
        {
            try
            {
                var result = Convert.ToDouble(displayTotal);
                total.Text = result.ToString("c").Remove(0, 1);
            }
            catch(Exception)
            {
                
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timdatelabel.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(idval == null)
            {
                MessageBox.Show("No Data Given, please select in the table before proceeding ", "No Value Given",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                var setbalanceform = new SettleBalanceForm();
                setbalanceform.balancetextbox.Text = displayTotal;
                setbalanceform.settleBalanceName.Text = nametxt.Text;
                setbalanceform.label2.Text = idval;
                setbalanceform.ShowDialog();
                nametxt.Text = null;
                total.Text = null;
                statustxt.Text = null;
                datamtxt.Text = null;
                idval = null;

            }
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = logs;
        }

        private void panel6_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void panel18_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pieChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {
            
        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            var addnewuser = new NewIndividual();
            addnewuser.Show();
        }

        private void total_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            FillChart();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            FillChart();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
           
            
        }
    }
}
