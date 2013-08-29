using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinAppNET.AppCode;

namespace WinAppNET.Dialogs
{
    public partial class frmRegister : MetroForm
    {
        private string[] methods = { "sms", "voice" };
        private string _phonenumber;
        public string phonenumber
        {
            get
            {
                return this._phonenumber;
            }
        }
        public string password = string.Empty;
        public string identity;

        public void SetNumber(string phonenumber)
        {
            this._phonenumber = phonenumber;
            this.txtNumber.Text = phonenumber;
        }

        public frmRegister()
        {
            this.StyleManager = Helper.GlobalStyleManager;
            InitializeComponent();
            this.cmbMethod.DataSource = methods;
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            this._phonenumber = this.txtNumber.Text;
            if (!string.IsNullOrEmpty(this.phonenumber))
            {
                try
                {
                    WhatsAppApi.Parser.PhoneNumber ph = new WhatsAppApi.Parser.PhoneNumber(this.phonenumber);
                    this.identity = WhatsAppApi.Register.WhatsRegisterV2.GenerateIdentity(ph.Number, this.txtPersonalPass.Text);
                    string method = this.cmbMethod.Text;
                    string response = string.Empty;
                    if (WhatsAppApi.Register.WhatsRegisterV2.RequestCode(ph.CC, ph.Number, out this.password, out response, method, this.identity, ph.ISO639, ph.ISO3166, ph.MCC))
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show(String.Format("Could not request code:\r\n{0}", response));
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
