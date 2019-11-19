using Jingjia.PLCModel;
using JingJia.PLCComm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JingJia.PLCDriver.Jingjia
{
    public class AmmeterRead : PLCCommandBase
    {


        /// <summary>
        /// 根据设备编号构建命令
        /// </summary>
        /// <param name="deviceNum"></param>
        /// <returns></returns>
        public override byte[] BuildCommandByte(int deviceNum)
        {

            SendHead = Convert.ToByte('S');//针头
            SendLen = 0x07;//长度
            SendCode = 0x3F;//命令码
            MetNum = Tools.NumIntToArray(deviceNum);//表号

            List<byte> tempList = new List<byte>();
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
        public override string BuildResultDataJson(byte[] data)
        {
            PLCDeviceBase pLCDeviceBase = new PLCDeviceBase(data[3], new byte[] { data[4], data[5], data[6] });
            string str = JsonConvert.SerializeObject(pLCDeviceBase);
            return str;
        }


    }
}