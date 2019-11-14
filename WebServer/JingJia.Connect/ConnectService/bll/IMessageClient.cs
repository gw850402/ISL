using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnectService
{
  public  interface IMessageClient
    {
        /// <summary>
        /// 接受服务器的信息
        /// </summary>
        /// <param name="msg"></param>
        void ReceiveMsg(string msg);
    }
}
