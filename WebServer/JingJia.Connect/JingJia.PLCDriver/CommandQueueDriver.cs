

using System;
using System.Collections.Generic;
using System.IO.Ports;
using JingJia.PLCCache;
using JingJia.PLCComm;
using Newtonsoft.Json;

namespace JingJia.PLCDriver
{
    public class CommandQueueDriver
    {

        static PLCDeviceCacheObject _pLCDeviceCache;
        public CommandQueueDriver()
        {
            _pLCDeviceCache = PLCDeviceCacheObject.Instance;
        }



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
            if (handleType == EnumHandleType.获取所有设备缓存)
            {

                _pLCDeviceCache = PLCDeviceCacheObject.Instance;
                Dictionary<string, object> _dataDic = _pLCDeviceCache.GetAll();

                List<object> ress = new List<object>();

                foreach (object item in _dataDic.Values)
                {
                    ress.Add(item);
                }

                ress.RemoveAt(0);
                string str = JsonConvert.SerializeObject(ress);

                return str;
            }


            GR10 gr10 = DriveFactory.GetPLCInstence();

            //打开串口
            //gr10.Open("COM4");

            //命令对象工厂
            PLCCommandBase pLCCommandBase = DriveFactory.GetPLCCommander(handleType, enumDeviceType);

            //获取命令码
            byte[] sendData = pLCCommandBase.BuildCommandByte(num);

            //发送命令 返回结果
            byte[] resData = gr10.SendData(sendData);
            string json = "";

            if (resData.Length > 4)
            {
                json = pLCCommandBase.BuildResultDataJson(resData, enumDeviceType);
            }
            else
            {
                json = "集中器返回数据异常";
            }

            //_pLCDeviceCache = PLCDeviceCacheObject.Instance;
            //_pLCDeviceCache[num.ToString()] = json;
            //转化返回结果
            return json;
        }

        private static void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
