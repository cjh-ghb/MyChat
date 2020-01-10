namespace Forms_TcpClinet
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnRecord = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.TextNetstat = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btn_sendMessage = new System.Windows.Forms.Button();
            this.btn_sendFile = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.status_online = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ipText = new System.Windows.Forms.TextBox();
            this.portText = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnRecord
            // 
            this.btnRecord.Location = new System.Drawing.Point(334, 13);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(87, 23);
            this.btnRecord.TabIndex = 0;
            this.btnRecord.Text = "看看聊天记录";
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(440, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(85, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "不看聊天记录";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // TextNetstat
            // 
            this.TextNetstat.Location = new System.Drawing.Point(13, 58);
            this.TextNetstat.Name = "TextNetstat";
            this.TextNetstat.Size = new System.Drawing.Size(555, 86);
            this.TextNetstat.TabIndex = 2;
            this.TextNetstat.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Sitka Text", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "netstat:";
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(224, 289);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(236, 21);
            this.textBox.TabIndex = 4;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(224, 314);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(236, 21);
            this.textBox2.TabIndex = 5;
            this.textBox2.DoubleClick += new System.EventHandler(this.textBox2_DoubleClick);
            // 
            // btn_sendMessage
            // 
            this.btn_sendMessage.Location = new System.Drawing.Point(466, 287);
            this.btn_sendMessage.Name = "btn_sendMessage";
            this.btn_sendMessage.Size = new System.Drawing.Size(102, 23);
            this.btn_sendMessage.TabIndex = 6;
            this.btn_sendMessage.Text = "Enter";
            this.btn_sendMessage.UseVisualStyleBackColor = true;
            this.btn_sendMessage.Click += new System.EventHandler(this.btn_sendMessage_Click);
            // 
            // btn_sendFile
            // 
            this.btn_sendFile.Location = new System.Drawing.Point(466, 314);
            this.btn_sendFile.Name = "btn_sendFile";
            this.btn_sendFile.Size = new System.Drawing.Size(102, 23);
            this.btn_sendFile.TabIndex = 7;
            this.btn_sendFile.Text = "AddAttachments";
            this.btn_sendFile.UseVisualStyleBackColor = true;
            this.btn_sendFile.Click += new System.EventHandler(this.btn_sendFile_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "在线好友：";
            // 
            // comboBox
            // 
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Items.AddRange(new object[] {
            "所有人都听着"});
            this.comboBox.Location = new System.Drawing.Point(88, 158);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(121, 20);
            this.comboBox.TabIndex = 9;
            this.comboBox.Text = "所有人都听着";
            this.comboBox.TextChanged += new System.EventHandler(this.comboBox_TextChanged);
            // 
            // status_online
            // 
            this.status_online.FormattingEnabled = true;
            this.status_online.Items.AddRange(new object[] {
            "离线状态",
            "在线状态"});
            this.status_online.Location = new System.Drawing.Point(88, 184);
            this.status_online.Name = "status_online";
            this.status_online.Size = new System.Drawing.Size(121, 20);
            this.status_online.TabIndex = 10;
            this.status_online.Text = "离线状态";
            this.status_online.TextChanged += new System.EventHandler(this.status_online_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 189);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "用户状态：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(224, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "聊天记录：";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(224, 178);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(344, 96);
            this.richTextBox1.TabIndex = 13;
            this.richTextBox1.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(74, 221);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 17);
            this.label5.TabIndex = 14;
            this.label5.Text = "ip:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(65, 244);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 17);
            this.label6.TabIndex = 15;
            this.label6.Text = "port:";
            // 
            // ipText
            // 
            this.ipText.Location = new System.Drawing.Point(102, 220);
            this.ipText.Name = "ipText";
            this.ipText.Size = new System.Drawing.Size(107, 21);
            this.ipText.TabIndex = 16;
            // 
            // portText
            // 
            this.portText.Location = new System.Drawing.Point(102, 243);
            this.portText.Name = "portText";
            this.portText.Size = new System.Drawing.Size(107, 21);
            this.portText.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("等线", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(19, 275);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(183, 26);
            this.label7.TabIndex = 18;
            this.label7.Text = "Tips:先启动服务器，设置ip和port\r\n后用户状态选择在线";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 357);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.portText);
            this.Controls.Add(this.ipText);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.status_online);
            this.Controls.Add(this.comboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_sendFile);
            this.Controls.Add(this.btn_sendMessage);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TextNetstat);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnRecord);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form1";
            this.Text = "TcpClient";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox TextNetstat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btn_sendMessage;
        private System.Windows.Forms.Button btn_sendFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.ComboBox status_online;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox ipText;
        private System.Windows.Forms.TextBox portText;
        private System.Windows.Forms.Label label7;
    }
}

