using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectService.plc
{
    /// <summary>
    /// GetWaterAlarm 的摘要说明
    /// </summary>
    public class GetWaterAlarm : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int code = 0;
            float result = 0F;
            context.Response.ContentType = "text/plain";
            try
            {
                if (int.TryParse(context.Request.Params[0], out code))
                {
                    result = JingJia.PLCDriver.DriveFactory.GetPLCInstence().GetElectricity(code);
                    if(result>0)
                        context.Response.Write(ConnectService.Common.ResultJsonString(code, "有水", "成功"));
                    else
                        context.Response.Write(ConnectService.Common.ResultJsonString(code, "无水", "成功"));
                }
            }
            catch (Exception ex)
            {
                context.Response.Write(ConnectService.Common.ResultJsonString(code, "未知", "失败"));
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