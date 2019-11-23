

using System;
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
        public static string ExecuteCommand(int num, EnumHandleType handleType, EnumDeviceType enumDeviceType)
        {
            GR10 gr10 = DriveFactory.GetPLCInstence();

            //打开串口
            gr10.Open("COM4");

            //命令对象工厂
            PLCCommandBase pLCCommandBase = DriveFactory.GetPLCCommander(handleType, enumDeviceType);

            //获取命令码
            byte[] sendData = pLCCommandBase.BuildCommandByte(num);

            //发送命令 返回结果
            byte[] resData = gr10.SendData(sendData);

            //转化返回结果
            return pLCCommandBase.BuildResultDataJson(resData, enumDeviceType);
            
        }
    }
}
