using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;


namespace Ideal
{
    public partial class Deposit : Form
    {
        public Deposit()
        {
            InitializeComponent();
        }

        private void Deposit_Load(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            //sql statement to select record containing username from tblPerson
            cmd.CommandText = "Select * from tblAccount where idAccount = " + Login.accountNo.ToString() + ";";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
        }



        public static DataSet ds = new DataSet();
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OleDb.4.0; Data Source='H:\\ibs2.mdb'");
        OleDbDataAdapter da = new OleDbDataAdapter();
        int count = 0;
        int pinNo;
        // int rownum = 0;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            /************
             * Better to call home here instead of withdraw, logically better..
             */
            Home bal = new Home();
            this.Hide();
            bal.Show();
  
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {

            int dep = Convert.ToInt32(txtDeposit.Text);
            da.Fill(ds, "tblAccount");
            pinNo = Convert.ToInt32(ds.Tables["tblAccount"].Rows[0][1]);

            if (txtDeposit.Text == null || txtPin.Text == null)
            {
                MessageBox.Show("Please fill in all information or cancel!");
            }

            if (count == 3)
            {
                MessageBox.Show("Maximum limit reached, you have been logged out..");
                this.Hide();
                Login log = new Login();
                log.Show();
                con.Close(); //max number of pin entries
            }

            if (String.IsNullOrWhiteSpace(txtPin.Text)) //enter a pin... 
            {
                MessageBox.Show("Pin is mandatory.. ");

                if (Convert.ToInt32(txtPin.Text) != pinNo)
                {
                    MessageBox.Show("Please enter correct PIN..");
                    txtPin.Clear();
                    pinNo = 0;
                }
            }
            else
            {
                dep = dep + Convert.ToInt32(ds.Tables["tblAccount"].Rows[0][4]);
                if (dep < 5)
                {
                    MessageBox.Show("The minimum deposit is £5");
                    txtDeposit.Clear();
                }
                else
                {
                    con.Open();
                    using (var cmd = new OleDbCommand())
                    {
                        cmd.Connection = con; //creates the connection
                        cmd.CommandType = CommandType.Text; //declares the command to execute
                        cmd.CommandText = @"UPDATE tblAccount SET balance = " + dep.ToString()
                            + " where idAccount = " + Convert.ToInt32(Login.ds1.Tables["tblAccount"].Rows[0][0]) + ";"; // sql query to update funds

                        //dep isn't a string.... dep.ToString() .old <<
                        cmd.ExecuteNonQuery(); // excutes the query
                        MessageBox.Show("You have deposited £" + txtDeposit.Text.ToString()
                            + "\r\nYour updated balance is £" + dep.ToString());

                        this.Hide();
                        Home bal = new Home();
                        bal.Show();
                    }
                    con.Close();
                }
            }
        }
    }
}
