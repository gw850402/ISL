using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jingjia.PLCModel;
using JingJia.PLCComm;

namespace JingJia.PLCDriver
{
    public class CommandQueueDriver 
    {



      
        public static void AddCommand(int num, EnumHandleType handleType)
        {

            throw new NotImplementedException();


        }


        /// <summary>
        /// 执行单个命令
        /// </summary>
        /// <param name="num">设备编号</param>
        /// <param name="handleType">执行类型</param>
        /// <returns>返回执行结果</returns>
        public static PLCDeviceBase ExecuteCommand(int num, EnumHandleType handleType)
        {
            GR10 gr10 = DriveFactory.GetPLCInstence();
            PLCDeviceBase pLCDeviceBase = gr10.Read(num, handleType);
            return pLCDeviceBase;
        }
    }
}
