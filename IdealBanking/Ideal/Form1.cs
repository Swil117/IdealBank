using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ideal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login newLogin = new Login();
            this.Hide();
            newLogin.Show();
        }

        private void newUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newUser frmUser = new newUser();
            Hide();
            frmUser.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
            System.Environment.Exit(0);  // terminates program
        }
    }
}
