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
        private static List<string> picturesToSync = new List<string>();
        private static Thread WAlistener;

        private string GetPassword()
        {
            string pass = System.Configuration.ConfigurationManager.AppSettings.Get("Password");
            try
            {
                byte[] foo = Convert.FromBase64String(pass);
                return pass;
            }
            catch (Exception)
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
            //resync pictures
            picturesToSync = this.refreshContactPictures();
            this.requestProfilePicture();

            this._loadConversations();
        }

        private void contact_dblClick(object sender, EventArgs e)
        {
            ListContact c = sender as ListContact;
            if (c != null)
            {
                this.OpenConversationThread(c.contact.jid, true, true);
            }
        }

        public void OpenConversation(object jid)
        {
            Helper.ChatWindowParameters cwp = jid as Helper.ChatWindowParameters;
            this._openConversation(cwp.jid, cwp.stealFocus, cwp.onTop);
        }

        protected void _openConversation(object jid, bool stealFocus, bool onTop)
        {
            if (!this.ChatWindows.ContainsKey(jid.ToString()))
            {
                ChatWindow c = new ChatWindow(jid.ToString(), stealFocus, onTop);
                c.TopMost = false;
                this.ChatWindows.Add(jid.ToString(), c);//create
            }
            else if (this.ChatWindows[jid.ToString()].IsDisposed)
            {
                ChatWindow c = new ChatWindow(jid.ToString(), stealFocus, onTop);
                c.TopMost = false;
                this.ChatWindows[jid.ToString()] = c;//renew
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
                throw e;
            }
        }

        public void OpenConversationThread(string jid, bool stealFocus, bool onTop)
        {
            Helper.ChatWindowParameters cwp = new Helper.ChatWindowParameters(jid, stealFocus, onTop);
            try
            {
                Thread t = new Thread(new ParameterizedThreadStart(OpenConversation));
                t.IsBackground = true;
                t.Start(cwp);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        protected ChatWindow getChat(string jid, bool forceOpen, bool onTop)
        {
            if (forceOpen)
            {
                this.OpenConversationThread(jid, forceOpen, onTop);
            }

            if (this.ChatWindows.ContainsKey(jid) && !this.ChatWindows[jid].IsDisposed)
            {
                return this.ChatWindows[jid];
            }

            while (forceOpen)
            {
                Thread.Sleep(100);
                if (this.ChatWindows.ContainsKey(jid) && this.ChatWindows[jid].IsLoaded)
                {
                    return this.ChatWindows[jid];
                }
            }
            return null;
        }

        protected void ProcessMessages(ProtocolTreeNode[] nodes)
        {
            foreach (ProtocolTreeNode node in nodes)
            {
                if (node.tag.Equals("message"))
                {
                    ProtocolTreeNode body = node.GetChild("body");
                    ProtocolTreeNode media = node.GetChild("media");
                    ProtocolTreeNode paused = node.GetChild("paused");
                    ProtocolTreeNode composing = node.GetChild("composing");
                    ProtocolTreeNode notification = node.GetChild("notification");
                    string jid = node.GetAttribute("from");

                    if (body != null || media != null)
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
                            this.getChat(jid, true, false).AddMessage(node);
                        }
                        catch (Exception)
                        { }

                        //refresh list
                        this._loadConversations();
                    }
                    if (paused != null)
                    {
                        try
                        {
                            if (this.getChat(jid, false, false) != null)
                            {
                                this.getChat(jid, false, false).SetOnline();
                            }
                        }
                        catch (Exception) 
                        {
                            //throw e;
                        }
                    }
                    if (composing != null)
                    {
                        try
                        {
                            if (this.getChat(jid, false, false) != null)
                            {
                                this.getChat(jid, false, false).SetTyping();
                            }
                        }
                        catch (Exception) 
                        {
                            //throw e;
                        }
                    }
                    if (notification != null)
                    {
                        if (notification.GetAttribute("type") == "picture")
                        {
                            ChatWindow.GetImageAsync(notification.GetChild("set").GetAttribute("jid"), false);
                        }
                    }
                }
                else if (node.tag.Equals("presence"))
                {
                    string jid = node.GetAttribute("from");
                    if (node.GetAttribute("type") != null && node.GetAttribute("type").Equals("available"))
                    {
                        try
                        {
                            if (this.getChat(jid, false, false) != null)
                            {
                                this.getChat(jid, false, false).SetOnline();
                            }
                        }
                        catch (Exception) 
                        {
                            //throw e;
                        }
                    }
                    if (node.GetAttribute("type") != null && node.GetAttribute("type").Equals("unavailable"))
                    {
                        try
                        {
                            if (this.getChat(jid, false, false) != null)
                            {
                                this.getChat(jid, false, false).SetUnavailable();
                            }
                        }
                        catch (Exception) 
                        { }
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
                            if (this.getChat(jid, false, false) != null)
                            {
                                getChat(jid, false, false).SetLastSeen(lastseen);
                            }
                        }
                        catch (Exception)
                        { }
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
                        byte[] rawpicture = node.GetChild("picture").GetData();
                        Contact c = ContactStore.GetContactByJid(pjid);
                        if (c != null)
                        {
                            Image img = null;
                            using (var ms = new MemoryStream(rawpicture))
                            {
                                try
                                {
                                    img = Image.FromStream(ms);
                                    string targetdir = Directory.GetCurrentDirectory() + "\\data\\profilecache";
                                    if (!Directory.Exists(targetdir))
                                    {
                                        Directory.CreateDirectory(targetdir);
                                    }
                                    img.Save(targetdir + "\\" + c.jid + ".jpg");
                                    try
                                    {
                                        if (this.getChat(pjid, false, false) != null)
                                        {
                                            this.getChat(pjid, false, false).SetPicture(img);
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        throw e;
                                    }
                                }
                                catch (Exception e)
                                {
                                    throw e;
                                }
                            }
                        }
                        if(picturesToSync.Remove(pjid))
                            this.requestProfilePicture();
                        
                    }
                    else if (node.children.First().tag.Equals("error") && node.children.First().GetAttribute("code") == "404")
                    {
                        string pjid = node.GetAttribute("from");
                        picturesToSync.Remove(pjid);
                        this.requestProfilePicture();
                    }
                }
            }
        }

        private void KeepAlive()
        {
            while (true)
            {
                Thread.Sleep(150000);
                WappSocket.Instance.WhatsSendHandler.SendActive();
            }
        }

        protected void Listen()
        {
            while (true)
            {
                try
                {
                    WappSocket.Instance.PollMessages();
                    ProtocolTreeNode[] nodes = WappSocket.Instance.GetAllMessages();
                    this.ProcessMessages(nodes);
                }
                catch (Exception)
                {
                    WappSocket.Instance.ClearIncomplete();
                    //throw new Exception("Socket timed out. Reconnect please. (TO BE IMPLEMENTED)");
                    WappSocket.Instance.Disconnect();
                    WappSocket.Instance.Connect();
                    WappSocket.Instance.Login();
                    //retry
                    this.requestProfilePicture();
                }
                Thread.Sleep(500);
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

        private bool checkCredentials()
        {
            if (!(WappSocket.Instance != null && WappSocket.Instance.ConnectionStatus == WhatsAppApi.WhatsApp.CONNECTION_STATUS.LOGGEDIN))
            {
                this.username = this.getUsername();
                this.password = this.GetPassword();
                if (!string.IsNullOrEmpty(this.username) && !string.IsNullOrEmpty(this.password))
                {
                    WappSocket.Create(username, password, "WinApp.NET", false);
                    WappSocket.Instance.Connect();
                    WappSocket.Instance.Login();
                    if (WappSocket.Instance.ConnectionStatus == WhatsAppApi.WhatsApp.CONNECTION_STATUS.LOGGEDIN)
                    {
                        return true;
                    }
                    else
                    {
                        WappSocket.Instance.Disconnect();
                    }
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        private void ContactsList_Load(object sender, EventArgs e)
        {
            string nickname = "WhatsAPINet";
            this.username = this.getUsername();
            this.password = this.GetPassword();

            if (!this.checkCredentials())
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
                        WappSocket.Create(username, password, "WinApp.NET", false);
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

            WappSocket.Create(this.username, this.password, nickname, true);
            WappSocket.Instance.Connect();
            WappSocket.Instance.Login();
            WappSocket.Instance.sendNickname(nickname);

            WAlistener = new Thread(new ThreadStart(Listen));
            WAlistener.IsBackground = true;
            WAlistener.Start();

            //this.SyncWaContactsAsync();
            Thread t = new Thread(new ThreadStart(SyncWaContactsAsync));
            t.IsBackground = true;
            t.Start();

            Thread alive = new Thread(new ThreadStart(KeepAlive));
            alive.IsBackground = true;
            alive.Start();

            int vertScrollWidth = SystemInformation.VerticalScrollBarWidth;
            this.flowLayoutPanel1.Padding = new Padding(0, 0, vertScrollWidth, 0);
        }

        private void requestProfilePicture()
        {
            if (picturesToSync.Count > 0)
            {
                string jid = picturesToSync.First();
                ChatWindow.GetImageAsync(jid, false);
            }
        }

        private void tileNew_Click(object sender, EventArgs e)
        {
            ContactsSelector selector = new ContactsSelector();

            DialogResult res = selector.ShowDialog();
            if (res == DialogResult.OK)
            {
                this.OpenConversationThread(selector.SelectedJID, true, true);
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

            //picturesToSync = this.refreshContactPictures();
            //this.requestProfilePicture();

            Thread t = new Thread(new ThreadStart(SyncWaContactsAsync));
            t.IsBackground = true;
            t.Start();
        }
    }
}
