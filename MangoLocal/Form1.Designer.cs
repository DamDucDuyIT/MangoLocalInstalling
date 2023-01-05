using Microsoft.Extensions.Configuration;

namespace MangoLocal
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxClientConnectionString = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxServerConnectionString = new System.Windows.Forms.TextBox();
            this.buttonChecking = new System.Windows.Forms.Button();
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.SyncButton = new System.Windows.Forms.Button();
            this.textBoxError = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(23, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(291, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "ClientConnectionString";
            // 
            // textBoxClientConnectionString
            // 
            this.textBoxClientConnectionString.Location = new System.Drawing.Point(320, 124);
            this.textBoxClientConnectionString.Name = "textBoxClientConnectionString";
            this.textBoxClientConnectionString.Size = new System.Drawing.Size(389, 23);
            this.textBoxClientConnectionString.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(19, 201);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(295, 37);
            this.label2.TabIndex = 2;
            this.label2.Text = "ServerConnectionString";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // textBoxServerConnectionString
            // 
            this.textBoxServerConnectionString.Location = new System.Drawing.Point(320, 215);
            this.textBoxServerConnectionString.Name = "textBoxServerConnectionString";
            this.textBoxServerConnectionString.Size = new System.Drawing.Size(389, 23);
            this.textBoxServerConnectionString.TabIndex = 3;
            // 
            // buttonChecking
            // 
            this.buttonChecking.Location = new System.Drawing.Point(320, 313);
            this.buttonChecking.Name = "buttonChecking";
            this.buttonChecking.Size = new System.Drawing.Size(75, 23);
            this.buttonChecking.TabIndex = 4;
            this.buttonChecking.Text = "Testing";
            this.buttonChecking.UseVisualStyleBackColor = true;
            this.buttonChecking.Click += new System.EventHandler(this.buttonChecking_Click);
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.Location = new System.Drawing.Point(110, 342);
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.Size = new System.Drawing.Size(610, 23);
            this.textBoxMessage.TabIndex = 5;
            this.textBoxMessage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxMessage.Visible = false;
            // 
            // SyncButton
            // 
            this.SyncButton.Location = new System.Drawing.Point(447, 313);
            this.SyncButton.Name = "SyncButton";
            this.SyncButton.Size = new System.Drawing.Size(75, 23);
            this.SyncButton.TabIndex = 6;
            this.SyncButton.Text = "Sync";
            this.SyncButton.UseVisualStyleBackColor = true;
            this.SyncButton.Click += new System.EventHandler(this.syncButton_Click);
            // 
            // textBoxError
            // 
            this.textBoxError.Location = new System.Drawing.Point(110, 371);
            this.textBoxError.Multiline = true;
            this.textBoxError.Name = "textBoxError";
            this.textBoxError.Size = new System.Drawing.Size(610, 67);
            this.textBoxError.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBoxError);
            this.Controls.Add(this.SyncButton);
            this.Controls.Add(this.textBoxMessage);
            this.Controls.Add(this.buttonChecking);
            this.Controls.Add(this.textBoxServerConnectionString);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxClientConnectionString);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxClientConnectionString;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxServerConnectionString;
        private System.Windows.Forms.Button buttonChecking;
        private System.Windows.Forms.TextBox textBoxMessage;
        private System.Windows.Forms.Button SyncButton;
        private System.Windows.Forms.TextBox textBoxError;
    }
}
