using System.Windows.Forms;
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
            this.Label1 = new Label();
            this.txtNumber = new TextBox();
            this.btnRequest = new Button();
            this.cmbMethod = new ComboBox();
            this.Label2 = new Label();
            this.txtPersonalPass = new TextBox();
            this.SuspendLayout();
            // 
            // metroLabel1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(23, 64);
            this.Label1.Name = "metroLabel1";
            this.Label1.Size = new System.Drawing.Size(96, 19);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Phone number";
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(147, 64);
            this.txtNumber.MaxLength = 32767;
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.PasswordChar = '\0';
            this.txtNumber.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNumber.SelectedText = "";
            this.txtNumber.Size = new System.Drawing.Size(170, 23);
            this.txtNumber.TabIndex = 1;
            // 
            // btnRequest
            // 
            this.btnRequest.Location = new System.Drawing.Point(219, 122);
            this.btnRequest.Name = "btnRequest";
            this.btnRequest.Size = new System.Drawing.Size(98, 29);
            this.btnRequest.TabIndex = 2;
            this.btnRequest.Text = "Request code";
            this.btnRequest.Click += new System.EventHandler(this.btnRequest_Click);
            // 
            // cmbMethod
            // 
            this.cmbMethod.FormattingEnabled = true;
            this.cmbMethod.ItemHeight = 23;
            this.cmbMethod.Location = new System.Drawing.Point(24, 122);
            this.cmbMethod.Name = "cmbMethod";
            this.cmbMethod.Size = new System.Drawing.Size(121, 29);
            this.cmbMethod.TabIndex = 3;
            // 
            // metroLabel2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(24, 93);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(117, 19);
            this.Label2.TabIndex = 4;
            this.Label2.Text = "Personal password";
            // 
            // txtPersonalPass
            // 
            this.txtPersonalPass.Location = new System.Drawing.Point(147, 93);
            this.txtPersonalPass.MaxLength = 32767;
            this.txtPersonalPass.Name = "txtPersonalPass";
            this.txtPersonalPass.PasswordChar = '*';
            this.txtPersonalPass.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPersonalPass.SelectedText = "";
            this.txtPersonalPass.Size = new System.Drawing.Size(170, 23);
            this.txtPersonalPass.TabIndex = 5;
            // 
            // frmRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 173);
            this.Controls.Add(this.txtPersonalPass);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.cmbMethod);
            this.Controls.Add(this.btnRequest);
            this.Controls.Add(this.txtNumber);
            this.Controls.Add(this.Label1);
            this.Name = "frmRegister";
            this.Text = "Register: Step 1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox txtNumber;
        private Button btnRequest;
        private ComboBox cmbMethod;
        private Label Label1;
        private Label Label2;
        private TextBox txtPersonalPass;
    }
}