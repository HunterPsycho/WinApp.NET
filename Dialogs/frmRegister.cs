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
    public partial class frmRegister : MetroForm
    {
        private string[] methods = { "sms", "voice" };
        public string phonenumber;
        public string password = string.Empty;
        public string identity;

        public frmRegister()
        {
            InitializeComponent();
            this.txtNumber.Text = this.phonenumber;
            this.cmbMethod.DataSource = methods;
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            this.phonenumber = this.txtNumber.Text;
            if (!string.IsNullOrEmpty(this.phonenumber))
            {
                try
                {
                    WhatsAppApi.Parser.PhoneNumber ph = new WhatsAppApi.Parser.PhoneNumber(this.phonenumber);
                    this.identity = WhatsAppApi.Register.WhatsRegisterV2.GenerateIdentity(ph.Number, this.txtPersonalPass.Text);
                    string method = this.cmbMethod.Text;
                    if (WhatsAppApi.Register.WhatsRegisterV2.RequestCode(ph.CC, ph.Number, out this.password, method, this.identity, ph.ISO639, ph.ISO3166, ph.MCC))
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("Could not request code");
                    }
                }
                catch (Exception)
                {
                    return;
                }
            }
        }
    }
}
