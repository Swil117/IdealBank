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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

    public static DataSet ds = new DataSet();
    OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OleDb.4.0; Data Source='H:\\ibs2.mdb'");
    OleDbDataAdapter da = new OleDbDataAdapter();

  
        private void btnWithdraw_Click(object sender, EventArgs e)
        {
        Withdraw frmWithdraw = new Withdraw();
        Close();
        frmWithdraw.Show();
    }

        private void btnExit_Click(object sender, EventArgs e)
        {
        Login log = new Login();
        Hide();
        log.Show();
    }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
        Deposit dep = new Deposit();
        Hide();
        dep.Show();
    }

        private void btnDets_Click(object sender, EventArgs e)
        {
        UpdateDets dets = new UpdateDets();
        Hide();
        dets.Show();

    }
      
        private void btnTrans_Click(object sender, EventArgs e)
        {
            Transfer trans = new Transfer();
            this.Hide();
            trans.Show();
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {

        }

        private void Home_Load(object sender, EventArgs e)
        {
           lblBal.Text = Convert.ToInt32(Login.ds1.Tables["tblAccount"].Rows[0][4]).ToString();
        }

        private void lblBal_Click(object sender, EventArgs e)
        {

        }
    }
}