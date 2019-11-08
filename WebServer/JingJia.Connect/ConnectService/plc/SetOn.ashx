<%@ WebHandler Language="C#" Class="SetOn" %>

using System;
using System.Web;

public class SetOn : IHttpHandler {
   
    public void ProcessRequest (HttpContext context) {
        int code=0;
       if(int.TryParse( context.Request.Params[0],out code))
       {
           JingJia.PLCDriver.DriveFactory.GetPLCInstence().SetOn(code);
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