using Jingjia.PLCModel;
using JingJia.PLCComm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectService.plc
{
    /// <summary>
    /// SendCommand 的摘要说明
    /// </summary>
    public class SendCommand : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            int deviceNum = 0;
            int handleType = 0;
            if (!int.TryParse(context.Request.Params[0], out deviceNum))
            {

                context.Response.Write(Common.ResultJsonStringNew(1, "设备ID参数错误", null));
                return;
            }
            if (!int.TryParse(context.Request.Params[1], out handleType))
            {
                context.Response.Write(Common.ResultJsonStringNew(1, "命令类型参数错误", null));
                return;
            }
            EnumHandleType enumHandleType = (EnumHandleType)handleType;
            
            context.Response.ContentType = "text/plain";
            string data = JingJia.PLCDriver.CommandQueueDriver.ExecuteCommand(deviceNum, enumHandleType);
           
            context.Response.Write(Common.ResultJsonStringNew(deviceNum, "ok", data));
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