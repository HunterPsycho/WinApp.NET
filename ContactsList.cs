using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinAppNET.AppCode;
using System.Data.Common;
using System.Data.SQLite;
using System.Threading;
using System.IO;
using WhatsAppApi.Helper;
using MetroFramework.Forms;
using WinAppNET.Controls;
using WinAppNET.Dialogs;
using System.Configuration;

namespace WinAppNET
{
    public partial class ContactsList : MetroForm
    {
        public BindingList<Contact> contacts = new BindingList<Contact>();
        public Dictionary<string, ChatWindow> ChatWindows = new Dictionary<string, ChatWindow>();
        protected string username;
        protected string password;

        private string GetPassword()
        {
            string pass = System.Configuration.ConfigurationManager.AppSettings.Get("Password");
            try
            {
                byte[] foo = Convert.FromBase64String(pass);
                return pass;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        private string getUsername()
        {
            return System.Configuration.ConfigurationManager.AppSettings.Get("Username");
        }

        public ContactsList()
        {
            InitializeComponent();
            this.FormClosing += this.ContactsList_OnClosing;
        }

        private void ContactsList_OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (ChatWindow w in this.ChatWindows.Values)
            {
                //close chat windows
                w.DoDispose();
            }
        }

        delegate void remoteDelegate();

        protected void _loadConversations()
        {
            
            if (this.InvokeRequired)
            {
                remoteDelegate r = new remoteDelegate(_loadConversations);
                this.Invoke(r);
            }
            else
            {
                this.flowLayoutPanel1.Controls.Clear();
                DbProviderFactory fact = DbProviderFactories.GetFactory("System.Data.SQLite");
                using (DbConnection cnn = fact.CreateConnection())
                {
                    cnn.ConnectionString = MessageStore.ConnectionString;
                    cnn.Open();
                    DbCommand cmd = cnn.CreateCommand();
                    cmd = cnn.CreateCommand();
                    cmd.CommandText = "SELECT jid FROM Messages GROUP BY jid";
                    DbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string jid = reader["jid"].ToString();
                        Contact contact = ContactStore.GetContactByJid(jid);
                        if (contact != null)
                        {
                            this.contacts.Add(contact);

                            //add to richtextbox
                            ListContact c = new ListContact(contact.jid, this.Style);
                            c.DoubleClick += this.contact_dblClick;
                            this.flowLayoutPanel1.Controls.Add(c);
                        }
                    }
                }

                //done
                this.label1.Hide();
            }
        }

        protected void SyncWaContactsAsync()
        {
            ContactStore.SyncWaContacts(this.username, this.password);
            this._loadConversations();
        }

        private void contact_dblClick(object sender, EventArgs e)
        {
            ListContact c = sender as ListContact;
            if (c != null)
            {
                Thread chat = new Thread(new ParameterizedThreadStart(OpenConversationWithFocus));
                chat.Start(c.contact.jid);
            }
        }

        public void OpenConversation(object jid)
        {
            this._openConversation(jid, false);
        }

        public void OpenConversationWithFocus(object jid)
        {
            this._openConversation(jid, true);
        }

        protected void _openConversation(object jid, bool stealFocus)
        {
            if (!this.ChatWindows.ContainsKey(jid.ToString()))
            {
                this.ChatWindows.Add(jid.ToString(), new ChatWindow(jid.ToString(), stealFocus));//create
            }
            else if (this.ChatWindows[jid.ToString()].IsDisposed)
            {
                this.ChatWindows[jid.ToString()] = new ChatWindow(jid.ToString(), stealFocus);//renew
            }
            else
            {
                this.ChatWindows[jid.ToString()].DoActivate();
                return;
            }
            try
            {
                Application.Run(this.ChatWindows[jid.ToString()]);
            }
            catch (Exception e)
            {

            }
        }

        public void OpenConversationThread(string jid, bool stealFocus)
        {
            try
            {
                Thread t;
                if (stealFocus)
                {
                    t = new Thread(new ParameterizedThreadStart(OpenConversationWithFocus));
                }
                else
                {
                    t = new Thread(new ParameterizedThreadStart(OpenConversation));
                }
                t.IsBackground = true;
                t.Start(jid);
            }
            catch (Exception e)
            {

            }
        }

        protected ChatWindow getChat(string jid, bool forceOpen)
        {
            if (forceOpen)
            {
                this.OpenConversationThread(jid, forceOpen);
            }

            if (this.ChatWindows.ContainsKey(jid) && !this.ChatWindows[jid].IsDisposed)
            {
                return this.ChatWindows[jid];
            }

            while (forceOpen)
            {
                Thread.Sleep(100);
                if (this.ChatWindows.ContainsKey(jid) && !this.ChatWindows[jid].IsDisposed)
                {
                    return this.ChatWindows[jid];
                }
            }
            return null;
        }

        protected bool ProcessMessages(ProtocolTreeNode[] nodes, string syncID)
        {
            bool synced = false;
            foreach (ProtocolTreeNode node in nodes)
            {
                if (node.tag.Equals("message"))
                {
                    ProtocolTreeNode body = node.GetChild("body");
                    ProtocolTreeNode paused = node.GetChild("paused");
                    ProtocolTreeNode composing = node.GetChild("composing");
                    string jid = node.GetAttribute("from");
                    if (body != null)
                    {
                        //extract and save nickname
                        if (node.GetChild("notify") != null && node.GetChild("notify").GetAttribute("name") != null)
                        {
                            string nick = node.GetChild("notify").GetAttribute("name");
                            Contact c = ContactStore.GetContactByJid(jid);
                            if (c != null)
                            {
                                c.nickname = nick;
                                ContactStore.UpdateNickname(c);
                            }
                        }

                        try
                        {
                            getChat(jid, true).AddMessage(node);
                        }
                        catch (Exception ex)
                        { }
                    }
                    if (paused != null)
                    {
                        try
                        {
                            getChat(jid, false).SetOnline();
                        }
                        catch (Exception e) { }
                    }
                    if (composing != null)
                    {
                        try
                        {
                            getChat(jid, false).SetTyping();
                        }
                        catch (Exception e) { }
                    }
                }
                else if (node.tag.Equals("presence"))
                {
                    string jid = node.GetAttribute("from");
                    if (node.GetAttribute("type") != null && node.GetAttribute("type").Equals("available"))
                    {
                        try
                        {
                            getChat(jid, false).SetOnline();
                        }
                        catch (Exception e) { }
                    }
                    if (node.GetAttribute("type") != null && node.GetAttribute("type").Equals("unavailable"))
                    {
                        try
                        {
                            getChat(jid, false).SetUnavailable();
                        }
                        catch (Exception e) { }
                    }
                }
                else if (node.tag.Equals("iq"))
                {
                    string jid = node.GetAttribute("from");

                    if (node.children.First().tag.Equals("query"))
                    {
                        DateTime lastseen = DateTime.Now;
                        int seconds = Int32.Parse(node.GetChild("query").GetAttribute("seconds"));
                        lastseen = lastseen.Subtract(new TimeSpan(0, 0, seconds));
                        try
                        {
                            getChat(jid, false).SetLastSeen(lastseen);
                        }
                        catch (Exception e)
                        {

                        }
                    }
                    else if (node.children.First().tag.Equals("group"))
                    {
                        string subject = node.children.First().GetAttribute("subject");
                        Contact cont = ContactStore.GetContactByJid(jid);
                        if (cont != null)
                        {
                            cont.nickname = subject;
                            ContactStore.UpdateNickname(cont);
                        }
                        else
                        {

                        }
                    }
                    else if (node.children.First().tag.Equals("picture"))
                    {
                        string pjid = node.GetAttribute("from");
                        string id = node.GetAttribute("id");
                        if (id == syncID)
                        {
                            synced = true;
                        }
                        byte[] rawpicture = node.GetChild("picture").GetData();
                        Contact c = ContactStore.GetContactByJid(pjid);

                        Image img = null;
                        using (var ms = new MemoryStream(rawpicture))
                        {
                            try
                            {
                                img = Image.FromStream(ms);
                                string targetdir = Directory.GetCurrentDirectory() + "\\data\\profilecache";
                                if(!Directory.Exists(targetdir))
                                {
                                    Directory.CreateDirectory(targetdir);
                                }
                                img.Save(targetdir + "\\" + c.jid + ".jpg");
                                this.getChat(pjid, false).SetPicture(img);
                            }
                            catch (Exception e)
                            {

                            }
                        }
                    }
                    else if (node.children.First().tag.Equals("error") && node.children.First().GetAttribute("code") == "404")
                    {
                        string id = node.GetAttribute("id");
                        if (id == syncID)
                        {
                            synced = true;
                        }
                    }
                }
            }
            return synced;
        }

        private void KeepAlive()
        {
            while (true)
            {
                Thread.Sleep(150000);
                WappSocket.Instance.WhatsSendHandler.SendActive();
            }
        }

        protected void Listen(object foo)
        {
            List<string> toSync = foo as List<string>;
            string sync = string.Empty;
            string syncID = string.Empty;
            if (toSync.Count > 0)
            {
                sync = toSync.First();
                syncID = ChatWindow.GetImageAsync(sync);
                toSync.Remove(sync);
            }
            
            while (true)
            {
                try
                {
                    if (!WappSocket.Instance.HasMessages())
                    {
                        try
                        {
                            WappSocket.Instance.PollMessages();
                        }
                        catch (Exception ex)
                        {
                            if (ex.GetType().Name == "ConnectionException")
                            {
                                //throw new Exception("Socket timed out. Reconnect please. (TO BE IMPLEMENTED)");
                                WappSocket.Instance.Disconnect();
                                WappSocket.Instance.Connect();
                                WappSocket.Instance.Login();
                            }
                        }
                        Thread.Sleep(100);
                    }
                    else
                    {
                        ProtocolTreeNode[] nodes = WappSocket.Instance.GetAllMessages();
                        if (this.ProcessMessages(nodes, syncID))
                        {
                            if (toSync.Count > 0)
                            {
                                //sync next
                                sync = toSync.First();
                                toSync.Remove(sync);
                                syncID = ChatWindow.GetImageAsync(sync);
                            }
                            else
                            {
                                sync = string.Empty;
                                syncID = string.Empty;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    break;
                }
            }
        }

        private List<string> refreshContactPictures()
        {
            Contact[] contacts = ContactStore.GetAllContacts();
            List<string> toSync = new List<string>();
            foreach (Contact c in contacts)
            {
                string path = ChatWindow.getCacheImagePath(c.jid);
                if(!File.Exists(path))
                {
                    toSync.Add(c.jid);
                }
            }
            return toSync;
        }

        private void ContactsList_Load(object sender, EventArgs e)
        {
            string nickname = "WhatsAPINet";
            this.username = this.getUsername();
            this.password = this.GetPassword();

            if (string.IsNullOrEmpty(this.username) || string.IsNullOrEmpty(this.password))
            {
                bool validCredentials = false;
                do
                {
                    //throw new Exception("Please enter credentials!");
                    WappCredentials creds = new WappCredentials();
                    DialogResult r = creds.ShowDialog();
                    if (r != System.Windows.Forms.DialogResult.OK)
                    {
                        //cancelled, close application
                        Application.Exit();
                        return;
                    }
                    this.username = this.getUsername();
                    this.password = this.GetPassword();
                    if (!string.IsNullOrEmpty(this.username) && !string.IsNullOrEmpty(this.password))
                    {
                        WappSocket.Create(username, password, "WinApp.NET", true);
                        WappSocket.Instance.Connect();
                        WappSocket.Instance.Login();
                        if (WappSocket.Instance.ConnectionStatus == WhatsAppApi.WhatsApp.CONNECTION_STATUS.LOGGEDIN)
                        {
                            validCredentials = true;
                            //write to config
                            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                            AppSettingsSection app = config.AppSettings;
                            app.Settings.Remove("Username");
                            app.Settings.Add("Username", this.username);
                            app.Settings.Remove("Password");
                            app.Settings.Add("Password", this.password);
                            config.Save(ConfigurationSaveMode.Modified);
                        }
                        else
                        {
                            WappSocket.Instance.Disconnect();
                        }
                    }
                } while (!validCredentials);
            }
            ContactStore.CheckTable();
            MessageStore.CheckTable();


            Thread t = new Thread(new ThreadStart(SyncWaContactsAsync));
            t.IsBackground = true;
            t.Start();

            WappSocket.Create(this.username, this.password, nickname, true);
            WappSocket.Instance.Connect();
            WappSocket.Instance.Login();
            WappSocket.Instance.sendNickname(nickname);

            List<string> toSync = this.refreshContactPictures();
            Thread listener = new Thread(new ParameterizedThreadStart(Listen));
            listener.IsBackground = true;
            listener.Start(toSync);

            Thread alive = new Thread(new ThreadStart(KeepAlive));
            alive.IsBackground = true;
            alive.Start();

            int vertScrollWidth = SystemInformation.VerticalScrollBarWidth;
            this.flowLayoutPanel1.Padding = new Padding(0, 0, vertScrollWidth, 0);
        }

        private void tileNew_Click(object sender, EventArgs e)
        {
            ContactsSelector selector = new ContactsSelector();

            DialogResult res = selector.ShowDialog();
            if (res == DialogResult.OK)
            {
                this.OpenConversationThread(selector.SelectedJID, true);
            }
            selector.Dispose();
        }

        private void tileGoogle_Click(object sender, EventArgs e)
        {
            //sync contacts
            Dialogs.frmGoogleSync gsync = new Dialogs.frmGoogleSync();
            gsync.ShowDialog();

            //reset
            this.contacts.Clear();
            this.label1.Text = "Updating contacts...";
            this.label1.Show();

            Thread t = new Thread(new ThreadStart(SyncWaContactsAsync));
            t.IsBackground = true;
            t.Start();
        }
    }
}
