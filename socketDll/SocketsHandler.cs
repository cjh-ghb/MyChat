using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace socketDll
{
    /// <summary>  
    /// 推送器  委托事件中记录客户端的消息（netstat）
    /// </summary>  
    /// <param name="sockets"></param>  
    public delegate void PushSockets(Sockets sockets);
}
