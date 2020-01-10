namespace Forms_TcpServer
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
            this.btnListen = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox = new System.Windows.Forms.ListBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textNetStat = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textChat = new System.Windows.Forms.TextBox();
            this.textMessage = new System.Windows.Forms.TextBox();
            this.fileName = new System.Windows.Forms.TextBox();
            this.btnMessage = new System.Windows.Forms.Button();
            this.btnFile = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textIp = new System.Windows.Forms.TextBox();
            this.textPort = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnInit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnListen
            // 
            this.btnListen.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnListen.Location = new System.Drawing.Point(23, 21);
            this.btnListen.Name = "btnListen";
            this.btnListen.Size = new System.Drawing.Size(75, 23);
            this.btnListen.TabIndex = 0;
            this.btnListen.Text = "开始监听";
            this.btnListen.UseVisualStyleBackColor = true;
            this.btnListen.Click += new System.EventHandler(this.btnListen_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "客户端列表：";
            // 
            // listBox
            // 
            this.listBox.FormattingEnabled = true;
            this.listBox.ItemHeight = 12;
            this.listBox.Location = new System.Drawing.Point(23, 115);
            this.listBox.Name = "listBox";
            this.listBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox.Size = new System.Drawing.Size(137, 160);
            this.listBox.TabIndex = 2;
            this.listBox.Click += new System.EventHandler(this.listBox_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.Location = new System.Drawing.Point(104, 21);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "启动服务器";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Sitka Text", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(195, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "netstat:";
            // 
            // textNetStat
            // 
            this.textNetStat.Font = new System.Drawing.Font("Times New Roman", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textNetStat.Location = new System.Drawing.Point(198, 29);
            this.textNetStat.Multiline = true;
            this.textNetStat.Name = "textNetStat";
            this.textNetStat.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textNetStat.Size = new System.Drawing.Size(370, 137);
            this.textNetStat.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(197, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "聊天记录：";
            // 
            // textChat
            // 
            this.textChat.Font = new System.Drawing.Font("Nirmala UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textChat.Location = new System.Drawing.Point(199, 184);
            this.textChat.Multiline = true;
            this.textChat.Name = "textChat";
            this.textChat.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textChat.Size = new System.Drawing.Size(370, 104);
            this.textChat.TabIndex = 7;
            // 
            // textMessage
            // 
            this.textMessage.Location = new System.Drawing.Point(198, 297);
            this.textMessage.Name = "textMessage";
            this.textMessage.Size = new System.Drawing.Size(249, 21);
            this.textMessage.TabIndex = 8;
            // 
            // fileName
            // 
            this.fileName.Location = new System.Drawing.Point(198, 324);
            this.fileName.Name = "fileName";
            this.fileName.Size = new System.Drawing.Size(249, 21);
            this.fileName.TabIndex = 9;
            this.fileName.DoubleClick += new System.EventHandler(this.fileName_DoubleClick);
            // 
            // btnMessage
            // 
            this.btnMessage.Location = new System.Drawing.Point(453, 297);
            this.btnMessage.Name = "btnMessage";
            this.btnMessage.Size = new System.Drawing.Size(112, 23);
            this.btnMessage.TabIndex = 10;
            this.btnMessage.Text = "Enter";
            this.btnMessage.UseVisualStyleBackColor = true;
            this.btnMessage.Click += new System.EventHandler(this.btnMessage_Click);
            // 
            // btnFile
            // 
            this.btnFile.Location = new System.Drawing.Point(453, 322);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(112, 23);
            this.btnFile.TabIndex = 11;
            this.btnFile.Text = "AddAttachments";
            this.btnFile.UseVisualStyleBackColor = true;
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Ink Free", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(38, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "ip:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Ink Free", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(32, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "port:";
            // 
            // textIp
            // 
            this.textIp.Location = new System.Drawing.Point(70, 48);
            this.textIp.Name = "textIp";
            this.textIp.Size = new System.Drawing.Size(100, 21);
            this.textIp.TabIndex = 14;
            // 
            // textPort
            // 
            this.textPort.Location = new System.Drawing.Point(70, 73);
            this.textPort.Name = "textPort";
            this.textPort.Size = new System.Drawing.Size(100, 21);
            this.textPort.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("等线", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 282);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(171, 39);
            this.label6.TabIndex = 16;
            this.label6.Text = "Tips:服务器可以向客户端发\r\n送消息及附件，应注意客户端\r\nip和port为服务器的套接字地址\r\n";
            // 
            // btnInit
            // 
            this.btnInit.Font = new System.Drawing.Font("等线", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInit.Location = new System.Drawing.Point(104, 322);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(75, 23);
            this.btnInit.TabIndex = 17;
            this.btnInit.Text = "服务器重启";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 357);
            this.Controls.Add(this.btnInit);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textPort);
            this.Controls.Add(this.textIp);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnFile);
            this.Controls.Add(this.btnMessage);
            this.Controls.Add(this.fileName);
            this.Controls.Add(this.textMessage);
            this.Controls.Add(this.textChat);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textNetStat);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnListen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form1";
            this.Text = "TcpServer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnListen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textNetStat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textChat;
        private System.Windows.Forms.TextBox textMessage;
        private System.Windows.Forms.TextBox fileName;
        private System.Windows.Forms.Button btnMessage;
        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textIp;
        private System.Windows.Forms.TextBox textPort;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnInit;
    }
}

