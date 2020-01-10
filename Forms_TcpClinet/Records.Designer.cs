namespace Forms_TcpClinet
{
    partial class Records
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
            this.chatchatchat = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // chatchatchat
            // 
            this.chatchatchat.Location = new System.Drawing.Point(1, 2);
            this.chatchatchat.Name = "chatchatchat";
            this.chatchatchat.Size = new System.Drawing.Size(377, 554);
            this.chatchatchat.TabIndex = 0;
            this.chatchatchat.Text = "";
            // 
            // Records
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 557);
            this.Controls.Add(this.chatchatchat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Records";
            this.Text = "Records";
            this.Load += new System.EventHandler(this.Records_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox chatchatchat;
    }
}