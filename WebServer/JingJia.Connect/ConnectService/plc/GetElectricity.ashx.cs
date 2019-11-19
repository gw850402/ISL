using JingJia.PLCCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectService.plc
{
    /// <summary>
    /// 获取单项电表信息
    /// </summary>
    public class GetElectricity : IHttpHandler
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
                    result= JingJia.PLCDriver.DriveFactory.GetPLCInstence().GetElectricity(code);
                }

               // PLCDeviceCacheObject.Instance[code.ToString()] = result;



                context.Response.Write(ConnectService.Common.ResultJsonString(code, result.ToString(), "成功"));
            }
            catch (Exception ex)
            {
                context.Response.Write(ConnectService.Common.ResultJsonString(code, "0.00", "失败"));
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