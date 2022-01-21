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
    public partial class cusDetails : Form
    {

        public cusDetails()
        {
            InitializeComponent();
        }

        private void cusDetails_Load(object sender, EventArgs e)
        {

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string myConnectionString = "Provider=Microsoft.Jet.OleDb.4.0; Data Source='H:\\ibs2.mdb'";
            OleDbCommandBuilder combuild = new OleDbCommandBuilder(Login.da);
            account acc = new account();
            int accountId = acc.accountNo;
            using (var con = new OleDbConnection())
            {
                con.ConnectionString = myConnectionString;
                con.Open(); //.. allows the connection to open

                // error handling for empty fields 

                if (String.IsNullOrEmpty(txtFname.Text) || String.IsNullOrEmpty(txtSurname.Text) || String.IsNullOrEmpty(txtDob.Text) || String.IsNullOrEmpty(txtGender.Text) ||
                    String.IsNullOrEmpty(txtHouseNo.Text) || String.IsNullOrEmpty(txtStreet.Text) || String.IsNullOrEmpty(txtPcode.Text))
                {
                    MessageBox.Show("Please enter all information");
                }
                else
                {
                    //using cmd as the command 
                    using (var cmd = new OleDbCommand())
                    {
                        /**
                         * Fixed this by including the text within the following: 
                         * 
                         * /////// "','" MYDATA "','" // which translates as 'MYDATA', missed this since it wasn't text and the db refused it... 
                         * e.g the data being inserted which is text must be wrapped around '' and no just entered...
                         * </lecture>
                         */
                        cmd.Connection = con; //creates the connection
                        cmd.CommandType = CommandType.Text; //declares the command to execute
                        cmd.CommandText =
                            @"INSERT INTO tblPerson (idAccount, first_name, last_name, dob, gender, houseNo, streetName, post_code) VALUES ("
                             + accountId + ",'"
                             + txtFname.Text + "','"
                             + txtSurname.Text + "','"
                             + txtDob.Text + "','"
                             + txtGender.Text + "','"
                             + txtHouseNo.Text + "','"
                             + txtStreet.Text + "','"
                             + txtPcode.Text + "')"; // sql query to insert a new record*/
                        cmd.ExecuteNonQuery(); // excutes the query
                        this.Hide();
                        Login bal = new Login();
                        bal.Show();
                    }
                }
            }
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Home hom = new Ideal.Home();
            hom.Show();
        }
    }
}
