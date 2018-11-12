using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RFIDGateSystem.Forms
{
    public partial class frmLogin : Form
    {
        string uname;
        string pword;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // ReadSQLDataBase();
            // Find username and password

            if (tbUserName.Text == "admin" && tbPassword.Text == "admin1234")
            {
                this.Hide();
                new frmMain().FormClosed += (s, args) => this.Close();
                new frmMain().Show();
            }
            else
            {
                MessageBox.Show(this, "User not found, incorrect username or password", "Login failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
