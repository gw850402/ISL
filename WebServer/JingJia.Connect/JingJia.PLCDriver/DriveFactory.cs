using JingJia.PLCComm;
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
        public static PLCCommandBase GetPLCCommander(EnumHandleType enumHandleType, EnumDeviceType enumDeviceType)
        {

            if (enumHandleType == EnumHandleType.抄录)
            {
                return new Jingjia.Command63Read();
            }
            else if (enumHandleType == EnumHandleType.通电Or全开 ) { 
            
                return new Jingjia.Command50Set(enumHandleType,enumDeviceType);
            }
            else if (enumHandleType == EnumHandleType.断电Or关三分之二)
            {

                return new Jingjia.Command50Set(enumHandleType, enumDeviceType);
            }
            else if (enumHandleType == EnumHandleType.告警Or全关)
            {

                return new Jingjia.Command50Set(enumHandleType, enumDeviceType);
            }
            else if (enumHandleType == EnumHandleType.关告警Or关三分之一)
            {

                return new Jingjia.Command50Set(enumHandleType, enumDeviceType);
            }
            else
                throw new Exception("未定义");
        }
    }
}
