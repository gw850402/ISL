using Jingjia.PLCModel;
using JingJia.PLCComm;
using Newtonsoft.Json;
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

            int code = 0;
            float result = 0F;
            string str = "";
            context.Response.ContentType = "text/plain";

            if (int.TryParse(context.Request.Params[0], out code))
            {
                PLCDeviceBase pLCDeviceBase = JingJia.PLCDriver.CommandQueueDriver.ExecuteCommand(code, EnumHandleType.电表抄录);
                str = JsonConvert.SerializeObject(pLCDeviceBase);
            }
            else
            {
                code = 1;
            }
            context.Response.Write(Common.ResultJsonString(code, str, ""));
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