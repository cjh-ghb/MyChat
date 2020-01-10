using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using socketDll;    //逻辑类库
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;

namespace Forms_TcpClinet 
{
    public delegate void MyDelegate(string recordsText);
    public partial class Form1 : Form
    {
        #region 初始化
        TcpClinet tcpClient;
        string ip = string.Empty;
        string port = string.Empty;
        private int sendInt = 0;
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }
  
        private void Form1_Load(object sender, EventArgs e)
        {
            TcpClinet.pushSockets = new PushSockets(Rec);

            tcpClient = new TcpClinet();
            this.ip = ipText.Text;
            port = portText.Text;
            TextNetstat.Text = "请设置ip和port后，进入在线状态 \r\n";
        }
        #endregion

        #region 连接并且接收数据
        private string recStr = null;
        private long fileLength = 0;    //声明为全局变量
        //客户端在线状态事件，判断是否为客户端上线
        private void status_online_TextChanged(object sender, EventArgs e)
        {
            if (status_online.Text == "在线状态")
            {
                //MessageBox.Show("right 1");
                try
                {
                    ip = ipText.Text;
                    port = portText.Text;
                    tcpClient.InitSocket(ip, int.Parse(port));
                    tcpClient.Start();
                    TextNetstat.Text = sendInt + ": 连接成功! \r\n";
                }
                catch (Exception ex)
                {
                    TextNetstat.Text = sendInt + $": 连接失败：{ex.Message} \r\n";
                }
                sendInt++;
            }
            else
            {
                tcpClient.Stop();
                sendInt = 0;
                TextNetstat.Text = sendInt + ": 请设置ip和port后，进入在线状态 \r\n";
            }
        }

        /// <summary>
        /// 连接和接收信息
        /// </summary>
        /// <param name="sks"></param>
        private void Rec(Sockets sks)
        {
            
            this.Invoke(new ThreadStart(delegate
            {
                if (sks.ex != null)
                {
                    if (sks.ClientDispose == true)
                    {
                        //由于未知原因引发异常.导致客户端下线.   比如网络故障.或服务器断开连接.  
                        TextNetstat.Text += sendInt + ": 客户端下线.! \r\n";
                    }
                    TextNetstat.Text += sendInt + $": 异常消息：{sks.ex} \r\n";
                }
                else if (sks.Offset == 0)
                {
                    //客户端主动下线  
                    TextNetstat.Text += sendInt + ": 客户端下线.! \r\n";
                }
                else
                {
                    byte[] buffer = new byte[sks.Offset];
                    Array.Copy(sks.RecBuffer, buffer, sks.Offset);
                    //接收的数据，转换编码
                    string str = Encoding.UTF8.GetString(buffer);
                    if(str.Length > 0)
                    {
                        if (buffer[0] != '1' && buffer[0] != '2' && buffer[0] != '3')
                        {
                            //是消息
                            string dataDecrypt = DecryptDES(str, Encoding.UTF8.GetString(Keys));
                            richTextBox1.Text += DateTime.Now.ToString()
                                + $": 服务端{sks.Ip}发来消息：{dataDecrypt} \r\n";
                        }
                        if (buffer[0] == '1')  //如果接收的是文件
                        {
                            //save suffix   保存后缀
                            
                            string filenameSuffix = recStr.Substring(recStr.LastIndexOf("."));
                            SaveFileDialog sfDialog = new SaveFileDialog()
                            {
                                Filter = "(*" + filenameSuffix + ")|*" + filenameSuffix + "",
                                FileName = recStr
                            };

                            if (sfDialog.ShowDialog(this) == DialogResult.OK)   //确认
                            {
                                string savePath = sfDialog.FileName;
                                //int rec = 0;
                                long recFileLength = 0;
                                bool firstWrite = true;
                                using (FileStream fs = new FileStream(savePath, FileMode.Create, FileAccess.Write))
                                {
                                    while (recFileLength < fileLength)
                                    {
                                        if (firstWrite)
                                        {
                                            fs.Write(buffer, 1, sks.Offset - 1);
                                            fs.Flush();
                                            recFileLength += sks.Offset - 1;
                                            firstWrite = false;
                                        }
                                        else
                                        {
                                            //rec = socketClient.Receive(buffer);
                                            fs.Write(buffer, 0, sks.Offset);
                                            fs.Flush();
                                            recFileLength += sks.Offset;
                                        }
                                    }
                                    fs.Close();
                                }
                                string fName = savePath.Substring(savePath.LastIndexOf("\\") + 1);
                                string fPath = savePath.Substring(0, savePath.LastIndexOf("\\"));
                                richTextBox1.Text += DateTime.Now.ToString() + $": 你成功接收了文件... "
                                    + fName + " 保存路径为：" + fPath + "\r\n";
                            }
                            
                        }
                        if (buffer[0] == '2')  //如果接收的是文件名-文件大小
                        {
                            string fileNameWithLength = Encoding.UTF8.GetString(buffer, 1, sks.Offset - 1);
                            //把文件名和文件长度截取
                            recStr = fileNameWithLength.Split('-').First();
                            fileLength = Convert.ToInt64(fileNameWithLength.Split('-').Last());
                            richTextBox1.Text += DateTime.Now.ToString() + ": 你开始接收文件. "
                                    + recStr + " 文件大小为：" + fileLength + "\r\n";
                        }
                        if (buffer[0] == '3')  //如果接收的是其他客户端上线
                        {
                            string clinetText = Encoding.UTF8.GetString(buffer, 1, sks.Offset - 1);
                            //在这里排除一次重名的客户端添加
                            for(int i = 0; i < comboBox.Items.Count; i++)
                            {
                                //组合框中与指定字符串没有匹配，那么就添加
                                if(comboBox.FindStringExact(clinetText) < 0)
                                {
                                    comboBox.Items.Add(clinetText);
                                }
                            }
                        }
                        if (buffer[0] == '4')  //如果接收的是其他客户端下线
                        {
                            string clinetText = Encoding.UTF8.GetString(buffer, 1, sks.Offset - 1);
                            comboBox.Items.Remove(clinetText);
                        }
                    }
                }
                sendInt++;
            }
            ));
        }
        #endregion

        #region 发送数据
        //按钮点击事件，确认发送消息
        private void btn_sendMessage_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += DateTime.Now.ToString()
                + $": 向服务器发送消息 " + textBox.Text + " \r\n";
            string dataEncrypt = EncryptDES(textBox.Text, Encoding.UTF8.GetString(Keys));
            tcpClient.SendData(dataEncrypt);
        }
        //按钮点击事件，确认发送附件
        private void btn_sendFile_Click(object sender, EventArgs e)
        {
            //string filename = sendFileName;
            SendFile();
        }
        private string fileName = null;
        private string filePath = null;
        /// <summary>
        /// 双击消息框选择文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox2_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "all files (*) | *";
            openFileDialog.InitialDirectory = @"C:\Users\MTSW\Desktop\vs";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //定义不包含路径的字符串，客户端不需要知道你的路径
                filePath = openFileDialog.FileName;
                fileName = openFileDialog.SafeFileName;
                textBox2.Text = openFileDialog.SafeFileName;

                //tcpServer.SendToAll("file:" + filename);
                //StreamReader reader = new StreamReader(fileName, Encoding.Default);
                //string buffer = reader.ReadToEnd();
                //tcpServer.SendToAll(buffer);
            }
        }
        public void SendFile()
        {
            long fileLength = new FileInfo(filePath).Length;
            string totalMsg = string.Format("{0}-{1}", textBox2.Text, fileLength);
            ClientSend(totalMsg, 2);    //发送 2文件名-文件大小

            byte[] buffer = new byte[8 * 1024];

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                int readLength = 0;
                bool firstRead = true;
                long sentFileLength = 0;
                while ((readLength = fs.Read(buffer, 0, buffer.Length)) > 0 
                    && sentFileLength < fileLength)
                {
                    sentFileLength += readLength;
                    //第一次发送的字节流上加个前缀1
                    if (firstRead)
                    {
                        byte[] firstBuffer = new byte[readLength + 1];
                        //标记1，代表为文件
                        firstBuffer[0] = 1;
                        Buffer.BlockCopy(buffer, 0, firstBuffer, 1, readLength);

                        tcpClient.SendData('1' + Encoding.UTF8.GetString(firstBuffer));

                        firstRead = false;
                        continue;
                    }
                    else
                    {
                        tcpClient.SendData(buffer.ToString());
                    }
                }
                fs.Close();
                richTextBox1.Text += DateTime.Now.ToString()
                    + $":您发送了文件:" + fileName + "\r\n";
            }
        }
        private void ClientSend(string SendStr, byte symbol)    //服务器代码详细注释
        {
            string buffer = symbol + SendStr;
            tcpClient.SendData(buffer);
            richTextBox1.Text += DateTime.Now.ToString()
                 + $": (客户端)准备发送文件 " + buffer + " \r\n";
        }

        //选择一个客户端进行聊天
        private void comboBox_TextChanged(object sender, EventArgs e)
        {
            string clinetText = comboBox.Text;
            MessageBox.Show(clinetText);
            //将数据带标志符5 客户端用户名 数据，通过服务器转发
            //tcpClient.SendData()
        }
        #endregion

        #region 聊天记录?
        //查看聊天记录文件
        private void btnRecord_Click(object sender, EventArgs e)
        {
            MessageBox.Show("聊天记录已更新，打开ChatRecords.txt查看");
        }

        public event MyDelegate MyEvent;
        private void button2_Click(object sender, EventArgs e)
        {
            Records records = new Records();
            MyEvent(this.richTextBox1.Text);     //主窗体中事件去影响订阅函数
            records.Show();
            //System.Diagnostics.Process.Start(filename);
        }
        #endregion

        #region 登录、字符串及文件加密
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

        //DES需要秘钥desKey和desIv向量一起使用加密，它们的要求都是8个数组的字节数组，如desKey=12345678转成字节数组
        private static byte[] desIv = new byte[] { 0xF, 0x56, 0x52, 0xCD, 0xFF, 0x3F, 0x5D, 0x4 };
        //加密文件 ,先实现将文件加密，传输文件即为加密的文件
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
