

using System;
using System.Collections.Generic;
using System.IO.Ports;
using Jingjia.PLCModel;
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
                Dictionary<int, Result63ReadBase> _dataDic = _pLCDeviceCache.GetResult63ReadBases();

                //List<Result63ReadBase> ress = new List<Result63ReadBase>();

                //foreach (Result63ReadBase item in _dataDic.Values)
                //{
                //    ress.Add(item);
                //}

                string str = JsonConvert.SerializeObject(_dataDic);

                return str;


            }

            // if (handleType == EnumHandleType.打开任务调度器) {

            //    Class1 class1 = new Class1();
            //    class1.Star();

            //    return "已经打开";
            //}




                GR10 gr10 = DriveFactory.GetPLCInstence();

            //打开串口
            //gr10.Open("COM4");

            //命令对象工厂
            PLCCommandBase pLCCommandBase = DriveFactory.GetPLCCommander(handleType, enumDeviceType);

            //获取命令码
            byte[] sendData = pLCCommandBase.BuildCommandByte(num);

            byte[] resData;
             
            string json;

            try
            {
                resData = gr10.SendData("COM4",sendData);

                if (resData == null)
                {
                    json = "返回数据超时";
                }
                else
                {
                    json = pLCCommandBase.BuildResultDataJson(resData, enumDeviceType);
                }
            }
            catch (Exception ex)
            {
                json = ex.Message;
            }
         
            return json;
        }

        private static void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
