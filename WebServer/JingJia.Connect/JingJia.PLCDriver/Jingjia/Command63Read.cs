﻿using Jingjia.PLCModel;
using Jingjia.PLCModel.Result63Read;
using JingJia.PLCCache;
using JingJia.PLCComm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JingJia.PLCDriver.Jingjia
{
    /// <summary>
    /// 抄收指定表号数据及状态（单项）（63）
    /// </summary>
    public class Command63Read : PLCCommandBase
    {

        public int DeviceNum { get; set; }

        /// <summary>
        /// 根据设备编号构建命令
        /// </summary>
        /// <param name="deviceNum"></param>
        /// <returns></returns>
        public override byte[] BuildCommandByte(int deviceNum)
        {
            DeviceNum = deviceNum;

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
        public override string BuildResultDataJson(byte[] data, EnumDeviceType enumDeviceType)
        {
            string str = "无此类型设备";

            Result63ReadBase result63ReadBase = null;

            if (enumDeviceType == EnumDeviceType.电表)
            {
                Result63ReadAmmeter res = new Result63ReadAmmeter(DeviceNum, data);
                result63ReadBase = res;
                //PLCDeviceCacheObject.Instance[res.Num.ToString()] = res;
                str = JsonConvert.SerializeObject(res);
            }
            else if (enumDeviceType == EnumDeviceType.水表)
            {
                Result63ReadWatermeter res = new Result63ReadWatermeter(DeviceNum, data);
                //PLCDeviceCacheObject.Instance[res.Num.ToString()] = res;
                result63ReadBase = res;
                str = JsonConvert.SerializeObject(res);
            }
            else if (enumDeviceType == EnumDeviceType.阀门)
            {
                Result63ReadValve res = new Result63ReadValve(DeviceNum, data);
                //PLCDeviceCacheObject.Instance[res.Num.ToString()] = res;
                result63ReadBase = res;
                str = JsonConvert.SerializeObject(res);
            }
            else if (enumDeviceType == EnumDeviceType.温度)
            {
                Result63ReadTemperature res = new Result63ReadTemperature(DeviceNum, data);
                //PLCDeviceCacheObject.Instance[res.Num.ToString()] = res;
                result63ReadBase = res;
                str = JsonConvert.SerializeObject(res);
            }
            else if (enumDeviceType == EnumDeviceType.湿度)
            {
                Result63ReadHumidity res = new Result63ReadHumidity(DeviceNum, data);
                //PLCDeviceCacheObject.Instance[res.Num.ToString()] = res;
                result63ReadBase = res;
                str = JsonConvert.SerializeObject(res);
            }
            else if (enumDeviceType == EnumDeviceType.灯控)
            {
                Result63ReadLight res = new Result63ReadLight(DeviceNum, data);
                //PLCDeviceCacheObject.Instance[res.Num.ToString()] = res;
                result63ReadBase = res;
                str = JsonConvert.SerializeObject(res);
            }
            else if (enumDeviceType == EnumDeviceType.光感度)
            {
                Result63ReadOptical res = new Result63ReadOptical(DeviceNum, data);
                //PLCDeviceCacheObject.Instance[res.Num.ToString()] = res;
                result63ReadBase = res;
                str = JsonConvert.SerializeObject(res);
            }
            else if (enumDeviceType == EnumDeviceType.水浸)
            {
                Result63ReadWater res = new Result63ReadWater(DeviceNum, data);
                //PLCDeviceCacheObject.Instance[res.Num.ToString()] = res;
                result63ReadBase = res;
                str = JsonConvert.SerializeObject(res);
            }

            if (result63ReadBase != null)
            {
                PLCDeviceCacheObject.Instance.AddDeviceData(result63ReadBase);
            }



            return str;
        }
    }
}