using MetroFramework.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace WinAppNET.AppCode
{
    class Helper
    {
        public static MetroStyleManager GlobalStyleManager = new MetroStyleManager() { Style = MetroFramework.MetroColorStyle.Green, Theme = MetroFramework.MetroThemeStyle.Light };

        public class ChatWindowParameters
        {
            public string jid;
            public bool stealFocus;
            public bool onTop;
            public ChatWindowParameters(string jid, bool stealFocus, bool onTop)
            {
                this.jid = jid;
                this.stealFocus = stealFocus;
                this.onTop = onTop;
            }
        }

        public static void CreateFolderTree()
        {
            string targetdir = Directory.GetCurrentDirectory() + "\\data";
            if (!Directory.Exists(targetdir))
            {
                Directory.CreateDirectory(targetdir);
            }
            string foo = targetdir + "\\profilecache";
            if (!Directory.Exists(foo))
            {
                Directory.CreateDirectory(foo);
            }
            foo = targetdir + "\\sqlite";
            if (!Directory.Exists(foo))
            {
                Directory.CreateDirectory(foo);
            }
            foo = targetdir + "\\media";
            if (!Directory.Exists(foo))
            {
                Directory.CreateDirectory(foo);
            }
        }

        public static Color GetMetroThemeColor(MetroFramework.MetroColorStyle style)
        {
            switch (style)
            {
                case MetroFramework.MetroColorStyle.Black:
                    return MetroFramework.MetroColors.Black;
                case MetroFramework.MetroColorStyle.Brown:
                    return MetroFramework.MetroColors.Brown;
                case MetroFramework.MetroColorStyle.Green:
                    return MetroFramework.MetroColors.Green;
                case MetroFramework.MetroColorStyle.Lime:
                    return MetroFramework.MetroColors.Lime;
                case MetroFramework.MetroColorStyle.Magenta:
                    return MetroFramework.MetroColors.Magenta;
                case MetroFramework.MetroColorStyle.Orange:
                    return MetroFramework.MetroColors.Orange;
                case MetroFramework.MetroColorStyle.Pink:
                    return MetroFramework.MetroColors.Pink;
                case MetroFramework.MetroColorStyle.Purple:
                    return MetroFramework.MetroColors.Purple;
                case MetroFramework.MetroColorStyle.Red:
                    return MetroFramework.MetroColors.Red;
                case MetroFramework.MetroColorStyle.Silver:
                    return MetroFramework.MetroColors.Silver;
                case MetroFramework.MetroColorStyle.Teal:
                    return MetroFramework.MetroColors.Teal;
                case MetroFramework.MetroColorStyle.White:
                    return MetroFramework.MetroColors.White;
                case MetroFramework.MetroColorStyle.Yellow:
                    return MetroFramework.MetroColors.Yellow;
                default:
                    return MetroFramework.MetroColors.Blue;
            }
        }
    }
}
