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

namespace WinAppNET.Controls
{
    public partial class ListChat : MetroTextBox
    {
        public ListChat(WappMessage msg, MetroFramework.MetroColorStyle style)
        {
            InitializeComponent();
            this.Style = style;
            this.metroTextBox1.Style = style;
            this.metroTextBox1.BackColor = Helper.GetMetroThemeColor(style);
            if (msg.from_me)
            {
                this.Padding = new Padding(50, this.Padding.Top, 4, this.Padding.Bottom);
            }
            this.metroTextBox1.Text = msg.data;
            this.metroTextBox1.ForeColor = Color.White;
        }
    }
}
