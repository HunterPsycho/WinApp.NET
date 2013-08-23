namespace WinAppNET.Dialogs
{
    partial class frmRegister
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
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.txtNumber = new MetroFramework.Controls.MetroTextBox();
            this.btnRequest = new MetroFramework.Controls.MetroButton();
            this.cmbMethod = new MetroFramework.Controls.MetroComboBox();
            this.SuspendLayout();
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(25, 64);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(96, 19);
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.Text = "Phone number";
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(127, 64);
            this.txtNumber.MaxLength = 32767;
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.PasswordChar = '\0';
            this.txtNumber.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNumber.SelectedText = "";
            this.txtNumber.Size = new System.Drawing.Size(150, 23);
            this.txtNumber.TabIndex = 1;
            this.txtNumber.UseSelectable = true;
            // 
            // btnRequest
            // 
            this.btnRequest.Location = new System.Drawing.Point(179, 94);
            this.btnRequest.Name = "btnRequest";
            this.btnRequest.Size = new System.Drawing.Size(98, 29);
            this.btnRequest.TabIndex = 2;
            this.btnRequest.Text = "Request code";
            this.btnRequest.UseSelectable = true;
            this.btnRequest.Click += new System.EventHandler(this.btnRequest_Click);
            // 
            // cmbMethod
            // 
            this.cmbMethod.FormattingEnabled = true;
            this.cmbMethod.ItemHeight = 23;
            this.cmbMethod.Location = new System.Drawing.Point(24, 94);
            this.cmbMethod.Name = "cmbMethod";
            this.cmbMethod.Size = new System.Drawing.Size(121, 29);
            this.cmbMethod.TabIndex = 3;
            this.cmbMethod.UseSelectable = true;
            // 
            // frmRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 151);
            this.Controls.Add(this.cmbMethod);
            this.Controls.Add(this.btnRequest);
            this.Controls.Add(this.txtNumber);
            this.Controls.Add(this.metroLabel1);
            this.Name = "frmRegister";
            this.Text = "Register: Step 1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox txtNumber;
        private MetroFramework.Controls.MetroButton btnRequest;
        private MetroFramework.Controls.MetroComboBox cmbMethod;
    }
}