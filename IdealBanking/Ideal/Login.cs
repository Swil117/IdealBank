using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;   // required for database access
using System.Data.OleDb;

namespace Ideal
{
    public partial class Login : Form
    {

        // CREATE DATA ADAPTER FOR ACCESS 
        public static OleDbDataAdapter da;

        // CREATE DATASET VARIABLE ds1 TO HOLD THE DATABASE 
        public static DataSet ds1 = new DataSet();

        /*
         *CALL NEW ACCOUNT METHOD
         */
        account acc = new account();

        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OleDb.4.0; Data Source='H:\\ibs2.mdb'");

        int pinNo;
        //string pinNo;
        //access stores this as a number probably best practice to change to int
        //shows how good access is..
        int rownum = 0;
        bool valid = false;

        public static string funds;
        public static int accountNo;

        public Login()

        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            //sql statement to select record containing username from tblPerson
            cmd.CommandText = "Select * from tblAccount";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            da = new OleDbDataAdapter(cmd);
            //  acc.setAccountId(0); //reeeeesets the accountNo
            da.Fill(dt);
            con.Close();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            valid = validate(); // CALL METHOD validate to determine if PIN and account number are valid credentials

            // Continue if valid
            accountNo = Convert.ToInt32(txtAccount.Text);
            if (valid == false)
            {
                MessageBox.Show("Please enter the correct details");
                con.Close();

            }

            if (valid == true)
            {
                // writes the data to the correct correspondence attribute in access
                // the dataset inserts the data into the table and selects the correct row
                accountNo = Convert.ToInt32(ds1.Tables["tblAccount"].Rows[0][0]); // don't string accountNo!! 
                //no other SQL app would allow that and then a datamismatch exception will be thrown
                pinNo = Convert.ToInt32(ds1.Tables["tblAccount"].Rows[0][1]);
                funds = ds1.Tables["tblAccount"].Rows[0][4].ToString();

                //set the accountId
                accountNo = acc.accountNo;

                Home balance = new Home();
                Close();
                balance.Show();

                //  txtAccount.Clear(); 
                txtPin.Clear();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
            System.Environment.Exit(0);  // terminates program
        }

        private bool validate()
        {
            ds1 = new DataSet();
            int acc = Convert.ToInt32(txtAccount.Text);

            // Find Login within dataset must equal a RegNo 
            string sqlQ = "SELECT tblAccount.* FROM tblAccount WHERE ((tblAccount.idAccount) = " + Convert.ToInt32(txtAccount.Text) + ")"; // it's done here so wtf --;

            da = new OleDbDataAdapter(sqlQ, con);
            rownum = da.Fill(ds1, "tblAccount");

            con.Close();

            if (rownum != 1)
            {
                MessageBox.Show("Not a valid Registration No Login - Try Again ");
                return false;
            }
            else
            {
                // pin validation
                pinNo = Convert.ToInt32(ds1.Tables["tblAccount"].Rows[0][1]);
                if (pinNo == Convert.ToInt32(txtPin.Text))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Not a valid password - please try again ");
                    return false;
                }
            }
        }
    }
}

