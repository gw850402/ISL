using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectService
{
    /// <summary>
    /// MessageRegister 的摘要说明
    /// </summary>
    public class MessageRegister : IHttpHandler, IMessageClient
    {
        string _message = "";
        public void ProcessRequest(HttpContext context)
        {
            CommonDefinition.MsgServer.Register(context.Request.UserHostAddress, this); //注册事件
            int i = 0;
          while (_message=="")
            {
             //循环1小时，或者接收到新字符为止   
                System.Threading.Thread.Sleep(200);
                i++;
                if (i > 18000) break;
            }
            lock (_message)
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write(_message);
            }

        }

        public void ReceiveMsg(string msg)
        {
            lock (_message)
            {
                _message = msg;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}