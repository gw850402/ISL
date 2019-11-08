<%@ WebHandler Language="C#" Class="SetOn" %>

using System;
using System.Web;

public class SetOn : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        int code=0;
        context.Response.ContentType = "text/plain";
        try
        {
            if (int.TryParse(context.Request.Params[0], out code))
            {
                JingJia.PLCDriver.DriveFactory.GetPLCInstence().SetOn(code);
            }
           context.Response.Write(ConnectService.Common.ResultJsonString(code,1,"成功"));
        }
        catch(Exception ex)
        {
            context.Response.Write(ConnectService.Common.ResultJsonString(code,0,"失败"));
        }
       
       


    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}