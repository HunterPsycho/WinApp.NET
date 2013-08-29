using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinAppNET.AppCode;
using System.IO;

namespace WinAppNET.Controls
{
    public partial class ListChat : UserControl
    {
        public ListChat(WappMessage msg)
        {
            InitializeComponent();
            if (msg.from_me)
            {
                this.Padding = new Padding(50, this.Padding.Top, 4, this.Padding.Bottom);
            }
            if (!String.IsNullOrEmpty(msg.author))
            {
                Contact c = ContactStore.GetContactByJid(msg.author);
                if (c != null)
                {
                    msg.author = c.FullName;
                }
                msg.data = String.Format("{0}\r\n{1}", msg.author, msg.data);
            }
            if (msg.type == "image")
            {
                if (!string.IsNullOrEmpty(msg.preview))
                {
                    MemoryStream ms = new MemoryStream(Convert.FromBase64String(msg.preview));
                    Image i = Image.FromStream(ms);
                    this.Height += i.Height;
                    //this.Controls.Remove(this.metroTile1);
                    PictureBox pb = new PictureBox();
                    pb.Width = i.Width;
                    pb.Height = i.Height;
                    pb.Image = i;
                    this.Controls.Add(pb);
                }
                
            }
            else
            {
                //Font f = MetroFonts.Tile(this.metroTile1.TileTextFontSize, this.metroTile1.TileTextFontWeight);
                //int lineHeight = Int32.Parse(Math.Round((decimal)this.metroTile1.CreateGraphics().MeasureString("X", f).Height).ToString());
                //SizeF sf = new SizeF();
                //sf = this.metroTile1.CreateGraphics().MeasureString(msg.data, f, this.metroTile1.Width);
                //this.metroTile1.Text = msg.data;
                //int newHeight = (int)Math.Round(decimal.Parse(sf.Height.ToString()));
                //int lines = newHeight / f.Height;
                //lines--;

                //if (lines > 0)
                //{
                //    this.Height = (lines * this.Height);
                //}
            }
        }
    }
}
