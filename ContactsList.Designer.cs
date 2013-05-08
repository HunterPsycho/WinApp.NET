using MetroFramework.Controls;
namespace WinAppNET
{
    partial class ContactsList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContactsList));
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new MetroFramework.Controls.MetroLabel();
            this.tileNew = new MetroFramework.Controls.MetroTile();
            this.tileGoogle = new MetroFramework.Controls.MetroTile();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Enabled = false;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(23, 117);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(248, 290);
            this.listBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 19);
            this.label1.Style = MetroFramework.MetroColorStyle.Blue;
            this.label1.TabIndex = 2;
            this.label1.Text = "Updating contacts...";
            this.label1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // tileNew
            // 
            this.tileNew.ActiveControl = null;
            this.tileNew.Location = new System.Drawing.Point(213, 60);
            this.tileNew.Name = "tileNew";
            this.tileNew.Size = new System.Drawing.Size(58, 49);
            this.tileNew.Style = MetroFramework.MetroColorStyle.Red;
            this.tileNew.TabIndex = 4;
            this.tileNew.Text = "New\r\nchat";
            this.tileNew.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tileNew.UseSelectable = true;
            this.tileNew.Click += new System.EventHandler(this.tileNew_Click);
            // 
            // tileGoogle
            // 
            this.tileGoogle.ActiveControl = null;
            this.tileGoogle.Location = new System.Drawing.Point(149, 60);
            this.tileGoogle.Name = "tileGoogle";
            this.tileGoogle.Size = new System.Drawing.Size(58, 49);
            this.tileGoogle.Style = MetroFramework.MetroColorStyle.Red;
            this.tileGoogle.TabIndex = 5;
            this.tileGoogle.Text = "Import\r\nGoogle";
            this.tileGoogle.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tileGoogle.UseSelectable = true;
            this.tileGoogle.Click += new System.EventHandler(this.tileGoogle_Click);
            // 
            // ContactsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 428);
            this.Controls.Add(this.tileGoogle);
            this.Controls.Add(this.tileNew);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ContactsList";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Chats";
            this.Load += new System.EventHandler(this.ContactsList_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private MetroLabel label1;
        private MetroTile tileNew;
        private MetroTile tileGoogle;
    }
}