using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectService
{
    /// <summary>
    /// 消息管理服务
    /// </summary>
    public class MessageServer
    {
        private Dictionary<String,IMessageClient> _messageClients = new Dictionary<String, IMessageClient>();
        /// <summary>
        /// 用户处理多线程锁
        /// </summary>
        public static Object ContextObject = new object();
        public void Register(String ip, IMessageClient messageClient)
        {

            lock(ContextObject)
            {
                // if(_messageClients.Where(n=>n.Key==ip).Count()==0)
                // _messageClients.Add(ip,messageClient);
                _messageClients.Add(Guid.NewGuid().ToString(), messageClient);

            }

        }
       
       public void SetMessage(string inf)
        {
            lock(ContextObject)
            {
                foreach(var v in _messageClients)
                {
                    if(v.Value!=null)
                    v.Value.ReceiveMsg(inf);
                }
                _messageClients.Clear();
            }
        }

    }
}