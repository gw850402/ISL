using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JingJia.PLCDriver
{
   public class DriveFactory
    {
       static GR10 _gr10 = null;

        //
       public static GR10 GetPLCInstence()
       {
           if (_gr10 == null)
           {
               _gr10 = new GR10();
               _gr10.Open(System.Configuration.ConfigurationSettings.AppSettings["COM"]);           
           }
           return _gr10;
       }
    }
}
