using Jingjia.PLCModel;
using Jingjia.PLCModel.Result63Read;
using JingJia.PLCComm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JingJia.PLCDriver.Jingjia
{
    /// <summary>
    /// 设置电表的工作状态（50）
    /// </summary>
    public class Command50Set : PLCCommandBase
    {
        public Command50Set(EnumDeviceType enumDeviceType) {
            EnumDeviceType = enumDeviceType;
        }

        public int DeviceNum { get; set; }

        protected EnumDeviceType EnumDeviceType;
        /// <summary>
        /// 根据设备编号构建命令
        /// </summary>
        /// <param name="deviceNum"></param>
        /// <returns></returns>
        public override byte[] BuildCommandByte(int deviceNum)
        {
            DeviceNum = deviceNum;

            SendHead = Convert.ToByte('S');//针头
            SendLen = 11;//长度
            SendCode = 50;//命令码
            byte[] MetOpt = new byte[] { 128 };
            byte[] MetSetSts = new byte[] { 0 };

            byte[] MetPwr  = new byte[] { 0 }; //MetPwr
            byte[] Group = new byte[] { 0 }; //Group

            MetNum = Tools.NumIntToArray(deviceNum);//表号

            List<byte> tempList = new List<byte>();
            tempList.AddRange(MetOpt);
            tempList.AddRange(MetSetSts);
            tempList.AddRange(MetPwr);
            tempList.AddRange(Group);

            tempList.AddRange(MetNum);

            DetailBytes = tempList.ToArray();//转化内容

            byte[] data = base.BuildCommand();
            return data;
        }



        /// <summary>
        /// 根据返回结果 转化JSON
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override string BuildResultDataJson(byte[] data, EnumDeviceType enumDeviceType)
        {
            string str = "无此类型设备";
            if (enumDeviceType == EnumDeviceType.电表)
            {
                Result63ReadAmmeter res = new Result63ReadAmmeter(DeviceNum, data);
                str = JsonConvert.SerializeObject(res);
            }
            else if (enumDeviceType == EnumDeviceType.阀门)
            {
                Result63ReadValve res = new Result63ReadValve(DeviceNum, data);
                str = JsonConvert.SerializeObject(res);
            }
            else if (enumDeviceType == EnumDeviceType.灯控)
            {
                Result63ReadLight res = new Result63ReadLight(DeviceNum, data);
                str = JsonConvert.SerializeObject(res);
            }
            return str;
        }

    }
}
