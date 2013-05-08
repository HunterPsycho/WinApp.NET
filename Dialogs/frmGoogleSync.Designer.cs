using MetroFramework.Controls;
namespace WinAppNET.Dialogs
{
    partial class frmGoogleSync
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGoogleSync));
            this.label1 = new MetroFramework.Controls.MetroLabel();
            this.label2 = new MetroFramework.Controls.MetroLabel();
            this.txtEmail = new MetroFramework.Controls.MetroTextBox();
            this.txtPassword = new MetroFramework.Controls.MetroTextBox();
            this.lblError = new MetroFramework.Controls.MetroLabel();
            this.btnSync = new MetroFramework.Controls.MetroButton();
            this.progressBar = new MetroFramework.Controls.MetroProgressBar();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 19);
            this.label1.Style = MetroFramework.MetroColorStyle.Blue;
            this.label1.TabIndex = 0;
            this.label1.Text = "E-mail address";
            this.label1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 19);
            this.label2.Style = MetroFramework.MetroColorStyle.Blue;
            this.label2.TabIndex = 1;
            this.label2.Text = "Password";
            this.label2.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(110, 98);
            this.txtEmail.MaxLength = 32767;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PasswordChar = '\0';
            this.txtEmail.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtEmail.SelectedText = "";
            this.txtEmail.Size = new System.Drawing.Size(151, 20);
            this.txtEmail.Style = MetroFramework.MetroColorStyle.Red;
            this.txtEmail.TabIndex = 2;
            this.txtEmail.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtEmail.UseSelectable = true;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(110, 124);
            this.txtPassword.MaxLength = 32767;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPassword.SelectedText = "";
            this.txtPassword.Size = new System.Drawing.Size(151, 20);
            this.txtPassword.Style = MetroFramework.MetroColorStyle.Red;
            this.txtPassword.TabIndex = 3;
            this.txtPassword.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtPassword.UseSelectable = true;
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblError.Location = new System.Drawing.Point(8, 67);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(262, 19);
            this.lblError.Style = MetroFramework.MetroColorStyle.Blue;
            this.lblError.TabIndex = 4;
            this.lblError.Text = "Please enter your Google login information";
            this.lblError.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // btnSync
            // 
            this.btnSync.Location = new System.Drawing.Point(134, 161);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(127, 27);
            this.btnSync.Style = MetroFramework.MetroColorStyle.Red;
            this.btnSync.TabIndex = 5;
            this.btnSync.Text = "Synchronize contacts";
            this.btnSync.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnSync.UseSelectable = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(8, 161);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(253, 27);
            this.progressBar.Style = MetroFramework.MetroColorStyle.Red;
            this.progressBar.TabIndex = 7;
            this.progressBar.Theme = MetroFramework.MetroThemeStyle.Light;
            this.progressBar.Visible = false;
            // 
            // frmGoogleSync
            // 
            this.AcceptButton = this.btnSync;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 203);
            this.Controls.Add(this.btnSync);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmGoogleSync";
            this.Padding = new System.Windows.Forms.Padding(0, 60, 0, 0);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Google contacts";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroLabel label1;
        private MetroLabel label2;
        private MetroTextBox txtEmail;
        private MetroTextBox txtPassword;
        private MetroLabel lblError;
        private MetroButton btnSync;
        private MetroProgressBar progressBar;
    }
}