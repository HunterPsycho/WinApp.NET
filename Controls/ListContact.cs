using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinAppNET.AppCode;

namespace WinAppNET.Controls
{
    public partial class ListContact : UserControl
    {
        public string jid;
        public ListContact(string jid)
        {
            InitializeComponent();
            int vertScrollWidth = SystemInformation.VerticalScrollBarWidth;
            this.Width -= vertScrollWidth;
            this.BackColor = MetroFramework.MetroColors.Red;
            this.jid = jid;
            Contact c = ContactStore.GetContactByJid(jid);
            if (c != null)
            {
                this.lblName.Text = c.FullName;
                string imgpath = ChatWindow.getCacheImagePath(jid);
                try
                {
                    Image i = Image.FromFile(imgpath);
                    Bitmap b = new Bitmap(i, new Size(48, 48));
                    this.pictureBox1.Image = b;
                    i.Dispose();
                }
                catch (Exception e)
                {

                }
            }
        }
    }
}
