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
    public partial class newUser : Form
    {
        public newUser()
        {
            InitializeComponent();

        }
        //conection variables
        // public static DataSet ds1 = new DataSet();
        public static OleDbConnection con = new OleDbConnection();
        public static OleDbDataAdapter da;
        account acc = new account();

        //acts as a constant?
        public static Random rand = new Random();
        public static int accountNo = rand.Next(100001, 999999);

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // auto generate accountNo


            //alternative connection method
            string myConnectionString = "Provider=Microsoft.Jet.OleDb.4.0; Data Source='H:\\ibs2.mdb'";

            // using the connection
            using (var con = new OleDbConnection())
            {
                con.ConnectionString = myConnectionString;
                con.Open(); //.. allows the connection to open

                // error handling for empty fields 

                if (String.IsNullOrEmpty(txtPin.Text) || String.IsNullOrEmpty(txtFunds.Text))
                {
                    MessageBox.Show("Please enter all information");
                }
                else
                {
                    //using cmd as the command 
                    using (var cmd = new OleDbCommand())
                    {

                        cmd.Connection = con; //creates the connection
                        cmd.CommandType = CommandType.Text; //declares the command to execute
                        cmd.CommandText =
                            @"INSERT INTO tblAccount (idAccount, pin, balance) VALUES (" + accountNo + "," + txtPin.Text + "," + txtFunds.Text + ")"; // sql query to insert a new record

                        cmd.ExecuteNonQuery(); // excutes the query
                        MessageBox.Show("Your account no is:" + accountNo + "\r\nDo not lose this.");

                        accountNo = acc.accountNo;

                        Hide();
                        cusDetails cus = new cusDetails();
                        cus.Show();
                    }
                    con.Close(); // closes the connection
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Hide();
            Form1 form = new Form1();
            form.Show(); //return to login screen
        }
    }
}