using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Controls;
using WinAppNET.AppCode;
using MetroFramework;

namespace WinAppNET.Controls
{
    public partial class ListChat : MetroUserControl
    {
        public ListChat(WappMessage msg, MetroFramework.MetroColorStyle style)
        {
            InitializeComponent();
            this.Style = style;
            this.metroTile1.Style = style;
            this.metroTile1.BackColor = Helper.GetMetroThemeColor(style);
            if (msg.from_me)
            {
                this.Padding = new Padding(50, this.Padding.Top, 4, this.Padding.Bottom);
            }
            Font f = MetroFonts.Tile(this.metroTile1.TileTextFontSize, this.metroTile1.TileTextFontWeight);
            SizeF sf = new SizeF();
            sf = this.metroTile1.CreateGraphics().MeasureString(msg.data, f, this.metroTile1.Width);
            this.metroTile1.Text = msg.data;
            int newHeight = (int)Math.Round(decimal.Parse(sf.Height.ToString()));
            int lines = newHeight / f.Height;
            lines--;
            
            if (lines > 0)
            {
                this.Height += (lines * f.Height);
            }
        }
    }
}
