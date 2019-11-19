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
        public static string ExecuteCommand(int num, EnumHandleType handleType)
        {
            GR10 gr10 = DriveFactory.GetPLCInstence();

            //    PLCDeviceBase pLCDeviceBase = new PLCDeviceBase();

            //    if (handleType == EnumHandleType.抄录) {
            //         pLCDeviceBase = gr10.Read(num, handleType);
            //    }



            //    return pLCDeviceBase;

            //打开串口
            gr10.Open("COM4");

            //命令对象工厂
            PLCCommandBase pLCCommandBase = DriveFactory.GetPLCCommander(handleType);

            //获取命令码
            byte[] sendData = pLCCommandBase.BuildCommandByte(num);

            //发送命令 返回结果
            byte[] resData = gr10.SendData(sendData);

            //转化返回结果
            return pLCCommandBase.BuildResultDataJson(resData);
            
        }
    }
}
