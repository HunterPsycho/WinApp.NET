using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinAppNET.Dialogs
{
    public partial class WappCredentials : MetroForm
    {
        public WappCredentials()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = this.txtPhonenumber.Text;
            string password = this.txtPassword.Text;
            if (!string.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
            {
                System.Configuration.ConfigurationManager.AppSettings.Set("Username", username);
                System.Configuration.ConfigurationManager.AppSettings.Set("Password", password);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Dispose();
            }
        }

        private void WappCredentials_Load(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            frmRegister regform = new frmRegister();
            regform.phonenumber = this.txtPhonenumber.Text;
            DialogResult regres = regform.ShowDialog();
            if (regres == System.Windows.Forms.DialogResult.OK)
            {

            }
        }
    }
}
