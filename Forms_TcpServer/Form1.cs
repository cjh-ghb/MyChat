using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Net;
using socketDll;    //引用自定义逻辑类库
using System.Threading;
using System.IO;
using System.Security.Cryptography;

/****************************************
 * FileName:     Forms_TcpServer 
 * Author:       JiahongChen
 * Date:         2019-12-21 16:40:33
 * Description:  服务器代码
 * Version:      V1.0.0.10
 * FunctionList: 
 * History:
 * 	<author> <time> <version> <desc>
****************************************/
namespace Forms_TcpServer
{
    public partial class Form1 : Form
    {
        #region⭐初始化数据
        private static int offlineIP = 0;     //客户端用户记录
        //private static int port;            //port
        //private object obj = new object();  //obj对象  
        private static int sendInt = 0;     //客户端用户
        IPEndPoint[] iP = new IPEndPoint[5]; //最多5个客户端
        IPEndPoint[] iPoffline = new IPEndPoint[5];//记载上线后又下线了的客户端
        private static int netstatInt = 0;  //网络监控的行数
        //private static Dictionary<TreeNode, IPEndPoint>   //客户端通过dictionary制作列表
            //DicTreeIPEndPoint = new Dictionary<TreeNode, IPEndPoint>();

        public Form1()
        {
            InitializeComponent();

            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private TcpServer tcpServer;    //服务器基础设置
        #endregion

        #region⭐⭐处理界面时的一些初始化操作
        private void Form1_Load(object sender, EventArgs e)
        {
            if (tcpServer == null)
            {
                textNetStat.Text += string.Format(netstatInt 
                    + ": 服务端程序尚未开启，请输入serverIP+port \r\n");
            }
            netstatInt++;
            listBox.Items.Clear();
            //在界面加载之后，便开始处理接收到的消息和文件，推送器报告信息
            TcpServer.pushSockets = new PushSockets(Rev);
            //实例化socketDll中TcpServer
            tcpServer = new TcpServer();
        }
        //服务器列表
        private void listBox_Click(object sender, EventArgs e)
        {
            //选中一个或多个客户端实现通信
            listBox.Focus();

            //listBox.SelectedNode = listBox.GetNodeAt(e.X, e.Y);
        }

        //重启服务器
        private void btnInit_Click(object sender, EventArgs e)
        {
            textIp.Text = null;
            textPort.Text = "0";
            Control.CheckForIllegalCrossThreadCalls = false;
            listBox.Items.Clear();  //初始化列表
            btnListen.Text = "开始监听";
            btnClose.Text = "启动服务器";
            textNetStat.Text = "";
            textChat.Text = "";
            netstatInt = 0;
        }

        //开启和关闭服务器按钮
        private void btnClose_Click(object sender, EventArgs e)
        {
            string text = this.btnClose.Text;
            this.btnClose.Text = text == "启动服务器" ? "关闭服务器" : "启动服务器";
            if (btnClose.Text == "关闭服务器")
            {
                textNetStat.Text += netstatInt + ": 服务器正在启动！\r\n";
            }
            else
            {
                textNetStat.Text += netstatInt + ": 服务器已关闭！\r\n";
                tcpServer.Stop();
            }
            netstatInt++;
        }

        /// <summary>
        /// 开启监听按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnListen_Click(object sender, EventArgs e)
        {
            btnListen.Text = "正在监听";
            if(btnListen.Text == "正在监听" && btnClose.Text == "关闭服务器")
            {
                textNetStat.Text += netstatInt + ": 服务器开始监听！\r\n";
                netstatInt++;
                StartHandle();
            }
        }
        //是否满足开启监听条件
        public void StartHandle()
        {
            try
            {
                if (textIp.Text != null && textPort.Text != null)
                {
                    tcpServer.InitSocket(IPAddress.Parse(textIp.Text),
                        int.Parse(textPort.Text));
                    tcpServer.Start();
                    textNetStat.Text += string.Format(netstatInt
                        + ": {0}服务端程序监听启动成功！监听：{1}:{2} \r\n",
                        DateTime.Now.ToString(), textIp.Text, textPort.Text);
                }
            }
            catch (Exception ex)
            {
                textNetStat.Text += string.Format(netstatInt
                    + ": 服务器监听失败！" + ex.Message + " \r\n");
                //StartServerToolStripMenuItem.Enabled = true;
            }
            netstatInt++;
        }
        #endregion

        #region⭐⭐⭐处理接收到的客户端和数据

        private long fileLength = 0;
        private string recStr = null;   //保存文件名或者消息
        /// <summary>  
        /// 处理接收到客户端的数据  当有客户端更改时更新ListBox
        /// </summary>  
        /// <param name="sks"></param>  
        private void Rev(Sockets sks) => _ = Invoke(new ThreadStart(
            delegate
            {
                if (sks.ex != null)
                {
                    if (sks.ClientDispose)  //客户端退出了
                    {
                        textNetStat.Text += string.Format(netstatInt
                            + ": {0}客户端：{1}下线！",
                            DateTime.Now.ToString(), sks.Ip);

                        //下线了客户端列表移除该客户端并通知其他客户端
                        for (int i = 0; i < sendInt + 1; i++)
                        {
                            if (sks.Ip == iP[i])
                            {
                                listBox.Items.Remove("user" + i);
                                for (int j = 0; j < sendInt; j++)
                                {
                                    if (i == j) //不需要告诉自己自己下线了
                                    {
                                        continue;
                                    }
                                    //采用标志符4，告诉其他客户端，该客户端下线了
                                    tcpServer.SendToClient(iP[j], "4user" + j);
                                    //还要告诉之后加入的客户端，该客户端已经下线了
                                    iPoffline[offlineIP++] = iP[i];
                                }
                            }
                        }
                    }
                    textNetStat.Text += sks.ex.Message + "\r\n";
                }
                else    //没有异常的情况
                {
                    if (sks.NewClientFlag)  //客户端上线
                    {
                        textNetStat.Text += string.Format("{0}新的客户端：{0}链接成功 \r\n",
                            DateTime.Now.ToString(), sks.Ip);
                        listBox.Items.Add("user" + sendInt); //怎么显示客户端的id呢

                        iP[sendInt] = sks.Ip;
                        sendInt++;
                        //当大于1个客户端时，给所有客户端发送其他客户端的存在
                        //新客户端上线时，也需要知道所有在线的客户端
                        if (sendInt > 1)
                        {
                            for (int i = 0; i < sendInt; i++)
                            {
                                for (int j = 0; j < sendInt; j++)
                                {
                                    if (i != j)   //不需要更新自己，添加其他所有客户端
                                    {
                                        if (iP[j] != iPoffline[j])   //不是自己，然后判断是否已退出
                                        {
                                            tcpServer.SendToClient(iP[i], "3user" + j);
                                        }
                                    }
                                }
                            }
                        }
                        //当前连接客户端+1
                        //toolStripStatusLabelClientNum.Text = (int.Parse(toolStripStatusLabelClientNum.Text) + 1).ToString();
                    }
                    else if (sks.Offset == 0)   //接收的包大小为0表示下线
                    {
                        textNetStat.Text += string.Format("{0}客户端:{1}下线.! \r\n",
                            DateTime.Now.ToString(), sks.Ip);
                    }
                    else    //接收数据
                    {

                        byte[] buffer = new byte[sks.Offset];
                        //将接收缓冲区数据复制到buffer中
                        Array.Copy(sks.RecBuffer, buffer, sks.Offset);
                        string tempBuffer = Encoding.UTF8.GetString(buffer);

                        //可使用 Encoding.Default.GetString 默认编码
                        //string str = Encoding.UTF8.GetString(buffer);
                        if (buffer[0] != '2' && buffer[0] != '1')//文字消息
                        {
                            recStr = Encoding.UTF8.GetString(buffer, 0, sks.Offset);
                            string dataDecrypt = DecryptDES(recStr, Encoding.UTF8.GetString(Keys));
                            textChat.Text += string.Format("{0}客户端{1}发来消息：{2} \r\n",
                                DateTime.Now.ToString(), sks.Ip, recStr);
                        }
                        if (buffer[0] == '1')//1对应文件信息
                        {
                            string filenameSuffix = recStr.Substring(recStr.LastIndexOf("."));
                            SaveFileDialog sfDialog = new SaveFileDialog()
                            {
                                //确定保存文件对话框的文件后缀 文件名
                                Filter = "(*" + filenameSuffix + ")|*" + filenameSuffix + "",
                                FileName = recStr
                            };

                            if (sfDialog.ShowDialog(this) == DialogResult.OK)   //需要点击保存吗
                            {
                                string savePath = sfDialog.FileName;
                                //int rec = 0;  //保存在默认路径下
                                long recFileLength = 0;
                                bool firstWrite = true;
                                using (FileStream fs = new FileStream(savePath, FileMode.Create,
                                    FileAccess.Write))
                                {
                                    //接收到的文件大小（0） 小于 文件大小
                                    while (recFileLength < fileLength)
                                    {
                                        if (firstWrite)
                                        {
                                            fs.Write(buffer, 1, sks.Offset - 1);    //跳过标志符1
                                            fs.Flush();     //冲洗缓冲区
                                            recFileLength += sks.Offset - 1;    //接收到的文件大小
                                            firstWrite = false;
                                        }
                                        else
                                        {
                                            //循环写剩下的文件内容
                                            fs.Write(buffer, 0, sks.Offset);
                                            fs.Flush();
                                            recFileLength += sks.Offset;
                                        }
                                    }
                                    fs.Close();
                                }
                                string fName = savePath.Substring(savePath.LastIndexOf("\\") + 1);
                                string fPath = savePath.Substring(0, savePath.LastIndexOf("\\"));
                                textChat.Text += DateTime.Now.ToString() + ": 你成功接收了文件... "
                                    + fName + " 保存路径为：" + fPath + "\r\n";
                            }
                        }
                        if (buffer[0] == '2')//2对应文件名字和长度
                        {
                            string fileNameWithLength = Encoding.UTF8.GetString(buffer, 1, sks.Offset - 1);
                            //把文件名和文件长度截取
                            recStr = fileNameWithLength.Split('-').First();
                            fileLength = Convert.ToInt64(fileNameWithLength.Split('-').Last());
                            textChat.Text += DateTime.Now.ToString() + ": 你开始接收文件. "
                                    + recStr + " 文件大小为：" + fileLength + "\r\n";
                        }
                    }
                }
                netstatInt++;
            }
            ));
        #endregion

        #region⭐⭐⭐⭐发送数据

        /// <summary>
        /// 发送消息按钮和发送附件按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMessage_Click(object sender, EventArgs e)
        {
            //通过listview选择单个客户端通信
            textChat.Text += DateTime.Now.ToString() + ": 服务器发送消息:"
                + textMessage.Text + "\r\n";
            string dataEncrypt = EncryptDES(textMessage.Text, Encoding.UTF8.GetString(Keys));
            tcpServer.SendToAll(dataEncrypt); 
        }

        private string filePath = null;
        private string fileSafeName = null;
        //双击选择附件
        private void fileName_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "all files (*) | *";
            openFileDialog.InitialDirectory = @"C:\Users\MTSW\Desktop\vs";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
                fileSafeName = openFileDialog.SafeFileName;
                fileName.Text = fileSafeName;
                //tcpServer.SendToAll("file:" + filename);
                //StreamReader reader = new StreamReader(filename, Encoding.Default);
                //string buffer = reader.ReadToEnd();
                //tcpServer.SendToAll(buffer);
            }
        }
        private void btnFile_Click(object sender, EventArgs e)
        {
            sendFile();
        }
        /// <summary>
        /// 使用标志符来标识文件和消息，没有标志符就是消息
        /// 标志符2表示  "2 文件名 - 文件长度"
        /// 标志符1表示  "1 文件内容"
        /// </summary>
        public void sendFile()
        {
            //发送文件前，将文件名和长度发过去
            long fileLength = new FileInfo(filePath).Length;
            string totalMsg = string.Format("{0}-{1}", fileName.Text, fileLength);
            ServerSend(totalMsg, 2);    //标志符2 文件名和长度已经发过去了

            byte[] buffer = new byte[8 * 1024];     //文件内容的缓冲区

            //打开文件并读出文件流，然后发送
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                int readLength = 0;
                bool firstRead = true;
                long sentFileLength = 0;
                //从流中读出数据放入缓冲区，满足读出数据大于0 和 满足发送数据长度小于文件长度
                while ((readLength = fs.Read(buffer, 0, buffer.Length)) > 0
                    && sentFileLength < fileLength)
                {
                    //发送数据长度 += 缓冲区中数据长度,统计发送的数据长度
                    sentFileLength += readLength;
                    //第一次发送的字节流
                    if (firstRead)
                    {
                        //byte[] firstBuffer = new byte[readLength + 1];
                        //标记1，代表为文件
                        //firstBuffer[0] = 1;
                        //Buffer.BlockCopy(buffer, 0, firstBuffer, 1, readLength);
                        //发送给所有客户端
                        tcpServer.SendToAll('1' + Encoding.UTF8.GetString(buffer));

                        firstRead = false;
                        //进行下一次循环
                        continue;
                    }
                    //移除掉缓冲区中有效长度之后的数据，可以不用移除
                    tcpServer.SendToAll(buffer.ToString().Remove(buffer.Length + 1));
                }
                //发送文件流完毕后，关闭文件流
                fs.Close();
                //聊天记录中报告文件已发送
                textChat.Text += DateTime.Now.ToString() + " :您发送了文件:"
                    + fileName.Text + "\r\n";
            }
        }

        //发送 2文件名-文件大小
        public void ServerSend(string SendStr, byte symbol)
        {
            /*
                //用UTF8能接受文字信息      文件名-文件大小
                byte[] buffer = Encoding.UTF8.GetBytes(SendStr);
                //实际发送的字节数组比实际输入的长度多1，用于存取标识符
                byte[] newBuffer = new byte[buffer.Length + 1];
                //标识符添加在位置为0的地方     标志符
                newBuffer[0] = symbol;
                Buffer.BlockCopy(buffer, 0, newBuffer, 1, buffer.Length);
                //把 标志符 文件名 - 文件大小 传输过去
                tcpServer.SendToAll(newBuffer.ToString());
                textChat.Text += DateTime.Now.ToString() + ": (服务器)准备开始发送附件（文件名-文件大小）"
                    + newBuffer.ToString() + "\r\n";
            */
            string buffer = symbol + SendStr;
            tcpServer.SendToAll(buffer);
            textChat.Text += DateTime.Now.ToString()
                 + $": (客户端)准备发送文件 " + buffer + " \r\n";
        }

        #endregion

        #region⭐⭐⭐⭐⭐加密操作
        /// <summary>
        /// MD5登录加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EncryptByMD5(string input)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();
            //将每个字节转为16进制
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
        //DES默认密钥向量 
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        /// <summary> 
        /// DES加密字符串 
        /// </summary> 
        /// <param name="encryptString">待加密的字符串</param> 
        /// <param name="encryptKey">加密密钥,要求为8位</param> 
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns> 
        public static string EncryptDES(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }
        /// <summary> 
        /// DES解密字符串 
        /// </summary> 
        /// <param name="decryptString">待解密的字符串</param> 
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param> 
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns> 
        public static string DecryptDES(string decryptString, string decryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }

        private static byte[] desIv = new byte[] { 0xF, 0x56, 0x52, 0xCD, 0xFF, 0x3F, 0x5D, 0x4 };
        //加密文件 
        private static void EncryptData(String inName, String outName, byte[] desKey, byte[] desIV)
        {
            //创建文件流来处理输入和输出文件
            FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
            FileStream fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
            fout.SetLength(0);

            //创建变量来帮助读写
            byte[] bin = new byte[100];     //用于加密的中间存储
            long rdlen = 0;                 //写入的字节总数
            long totlen = fin.Length;       //输入文件的总长度
            int len;                        //一次写入的字节数

            DES des = new DESCryptoServiceProvider();
            CryptoStream encStream = new CryptoStream(fout, des.CreateEncryptor(desKey, desIV), CryptoStreamMode.Write);

            //读取输入文件，然后加密并写入输出文件 
            while (rdlen < totlen)
            {
                len = fin.Read(bin, 0, 100);
                encStream.Write(bin, 0, len);
                rdlen = rdlen + len;
            }

            encStream.Close();
            fout.Close();
            fin.Close();
        }

        //解密文件 
        private static void DecryptData(String inName, String outName, byte[] desKey, byte[] desIV)
        {
            //创建文件流来处理输入和输出文件
            FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
            FileStream fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
            fout.SetLength(0);

            //创建变量来帮助读写
            byte[] bin = new byte[100];     //用于加密的中间存储
            long rdlen = 0;                 //写入的字节总数
            long totlen = fin.Length;       //输入文件的总长度
            int len;                        //一次写入的字节数

            DES des = new DESCryptoServiceProvider();
            CryptoStream encStream = new CryptoStream(fout, des.CreateDecryptor(desKey, desIV), CryptoStreamMode.Write);

            //从输入文件读取，然后解密并写入输出文件
            while (rdlen < totlen)
            {
                len = fin.Read(bin, 0, 100);
                encStream.Write(bin, 0, len);
                rdlen = rdlen + len;
            }

            encStream.Close();
            fout.Close();
            fin.Close();
        }
        #endregion

    }
}
