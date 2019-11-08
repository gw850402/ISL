<%@ WebHandler Language="C#" Class="SetOff" %>

using System;
using System.Web;

public class SetOff : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        int code = 0;
        if (int.TryParse(context.Request.Params[0], out code))
        {
            JingJia.PLCDriver.DriveFactory.GetPLCInstence().SetOff(code);
        }
        
        context.Response.ContentType = "text/plain";
        context.Response.Write("Hello World");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}