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
            this.label1 = new MetroFramework.Controls.MetroLabel();
            this.tileNew = new MetroFramework.Controls.MetroTile();
            this.tileGoogle = new MetroFramework.Controls.MetroTile();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Updating contacts...";
            // 
            // tileNew
            // 
            this.tileNew.ActiveControl = null;
            this.tileNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tileNew.Location = new System.Drawing.Point(219, 60);
            this.tileNew.Name = "tileNew";
            this.tileNew.Size = new System.Drawing.Size(58, 49);
            this.tileNew.TabIndex = 4;
            this.tileNew.Text = "New\r\nchat";
            this.tileNew.UseSelectable = true;
            this.tileNew.Click += new System.EventHandler(this.tileNew_Click);
            // 
            // tileGoogle
            // 
            this.tileGoogle.ActiveControl = null;
            this.tileGoogle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tileGoogle.Location = new System.Drawing.Point(155, 60);
            this.tileGoogle.Name = "tileGoogle";
            this.tileGoogle.Size = new System.Drawing.Size(58, 49);
            this.tileGoogle.TabIndex = 5;
            this.tileGoogle.Text = "Import\r\nGoogle";
            this.tileGoogle.UseSelectable = true;
            this.tileGoogle.Click += new System.EventHandler(this.tileGoogle_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(20, 115);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(257, 287);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // ContactsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 425);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.tileGoogle);
            this.Controls.Add(this.tileNew);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(300, 425);
            this.Name = "ContactsList";
            this.Text = "Chats";
            this.Load += new System.EventHandler(this.ContactsList_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroLabel label1;
        private MetroTile tileNew;
        private MetroTile tileGoogle;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}