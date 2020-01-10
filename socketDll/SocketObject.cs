using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;   //socket编程

namespace socketDll
{
    /// <summary>
    /// 对于客户端和服务器的抽象基类
    /// </summary>
    public abstract class SocketObject
    {
        /// <summary>  
        /// 客户端和服务器分别重写初始化Socket方法
        /// </summary>  
        /// <param name="ipAddress"></param>  
        /// <param name="port"></param>  
        public abstract void InitSocket(IPAddress ipAddress, int port);
        public abstract void InitSocket(string ipAddress, int port);

        /// <summary>  
        /// Socket启动方法  
        /// </summary>  
        public abstract void Start();

        /// <summary>  
        /// Sockdet停止方法  
        /// </summary>  
        public abstract void Stop();

    }
}
