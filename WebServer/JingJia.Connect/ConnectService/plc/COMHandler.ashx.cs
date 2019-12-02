using JingJia.PLCDriver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectService.plc
{
    /// <summary>
    /// COMHandler 的摘要说明
    /// </summary>
    public class COMHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            int com =0;
            int command = 0;

            if (!int.TryParse(context.Request.Params[0], out com))
            {
                context.Response.Write(Common.ResultJsonStringNew(1, "串口参数错误", null));
                return;
            }

            if (!int.TryParse(context.Request.Params[1], out command))
            {
                context.Response.Write(Common.ResultJsonStringNew(1, "启动关闭参数错误", null));
                return;
            }
            GR10 gR10 = new GR10();
            string comstr = "COM" + com;
            gR10.Open(comstr);

            context.Response.Write("串口 " + comstr + "已打开。");
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