using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Forms_TcpClinet
{
    
    public partial class Records : Form
    {
        public Records()
        {
            InitializeComponent();
        }
        
        public string str = File.ReadAllText(@"C:\Users\MTSW\Desktop\vs\code\ChatRecords.txt");
        
        private void Records_Load(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();

            form1.MyEvent += new MyDelegate(RecordsText);
            //MyEvent(str);
            chatchatchat.Text = str;
        }
        public void RecordsText(string message)
        {
            str += message;
        }

    }
}
