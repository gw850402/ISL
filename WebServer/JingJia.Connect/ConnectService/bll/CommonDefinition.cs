using ConnectService.bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectService
{
    public class CommonDefinition
    {
        public static MessageServer MsgServer = new MessageServer();
        public static Alarm Alarm = new Alarm();
    }
}