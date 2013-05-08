using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinAppNET.AppCode;
using System.Threading;
using MetroFramework.Forms;
using WinAppNET.Controls;

namespace WinAppNET
{
    public partial class ContactsSelector : MetroForm
    {
        protected Contact[] _initialContacts;
        protected BindingList<Contact> _matchingContacts = new BindingList<Contact>();
        public string SelectedJID = String.Empty;

        public ContactsSelector()
        {
            InitializeComponent();
            this._initialContacts = ContactStore.GetAllContacts();
            foreach(Contact c in this._initialContacts)
            {
                this._matchingContacts.Add(c);
            }
            this.redraw();
        }

        private void redraw()
        {
            this.flowLayoutPanel1.Controls.Clear();
            int i = 0;
            foreach (Contact contact in this._matchingContacts)
            {
                i++;
                if (i == 10)
                {
                    return;
                }
                ListContact lc = new ListContact(contact.jid);
                lc.DoubleClick += this.listContact_DoubleClick;
                this.flowLayoutPanel1.Controls.Add(lc);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this._matchingContacts.Clear();
            foreach (Contact contact in this._initialContacts)
            {
                if(contact.ToString().ToLower().Contains(this.textBox1.Text.ToLower()))
                {
                    this._matchingContacts.Add(contact);
                }
            }
            this.redraw();
        }

        private void listContact_DoubleClick(object sender, EventArgs e)
        {
            ListContact lc = sender as ListContact;
            if (lc != null)
            {
                this.SelectedJID = lc.jid;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
