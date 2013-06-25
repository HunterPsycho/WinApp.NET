using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhatsAppApi.Helper;

namespace WinAppNET.AppCode
{
    public class WappMessage
    {
        public int id;
        public String data;
        public bool from_me;
        public string jid;
        public string author = null;
        public DateTime timestamp;
        public string type;
        public string preview;

        public WappMessage(string message, string jid)
        {
            this.data = message;
            this.timestamp = DateTime.UtcNow;
            this.from_me = true;
            this.jid = jid;
        }

        public WappMessage(int id, string data, bool from_me, string jid, DateTime timestamp, string type, string preview)
        {
            this.id = id;
            this.data = data;
            this.from_me = from_me;
            this.jid = jid;
            this.timestamp = timestamp;
            this.type = type;
            this.preview = preview;
        }

        public WappMessage(ProtocolTreeNode node, string jid)
        {
            ProtocolTreeNode body = node.GetChild("body");
            ProtocolTreeNode media = node.GetChild("media");
            if (node.tag == "message")
            {
                if (node.GetAttribute("type") == "subject")
                {
                    Contact c = ContactStore.GetContactByJid(node.GetAttribute("author"));
                    this.data = c.ToString() + " changed subject to \"" + Encoding.ASCII.GetString(node.GetChild("body").GetData()) + "\"";
                }
                else
                {
                    if (body != null)
                    {
                        this.data = Encoding.ASCII.GetString(node.GetChild("body").GetData());
                        this.type = "text";
                    }
                    else if (media != null)
                    {
                        this.type = media.GetAttribute("type");
                        if (media.data != null && media.data.Length > 0)
                        {
                            this.preview = Convert.ToBase64String(media.data);
                        }
                        this.data = media.GetAttribute("url");
                    }
                }
                this.from_me = false;
                this.timestamp = DateTime.UtcNow;
                this.jid = jid;
            }
        }

        public override String ToString()
        {
            if (this.from_me)
                return "Me: " + this.data;
            else
                return "Him: " + this.data;
        }
    }
}
