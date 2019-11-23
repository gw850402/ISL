using JingJia.PLCComm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jingjia.PLCModel
{
    /// <summary>
    /// 电表子类
    /// zhw
    /// 2019-11-19
    /// </summary>
    public class PLCAmmeterDeviceEntity : Result63ReadBase
    {
        public PLCAmmeterDeviceEntity(int deviceNum, byte[] data)
            : base(deviceNum, data)
        {

            Unit = "kWh";


            if (MetStsArray[0] == 0)
            {
                onOff = "电表通电";
            }
            else
            {
                onOff = "电表断电";
            }

            if (MetStsArray[1] == 0)
            {
                alarm = "电表不告警 ";
            }
            else
            {
                alarm = "电表告警";
            }

            if (MetStsArray[1] == 0)
            {
                alarm = "电表不告警 ";
            }
            else
            {
                alarm = "电表告警";
            }

            if (MetStsArray[2] == 0)
            {
                rom1 = "正常 ";
            }
            else
            {
                rom1 = "ROM出错旦纠正";
            }

            if (MetStsArray[3] == 0)
            {
                rom2 = "ROM正常 ";
            }
            else
            {
                rom2 = "ROM坏掉";
            }


            if (MetStsArray[4] == 1)
            {
                line = "ROM正常 ";
            }
            else
            {
                line = "ROM坏掉";
            }


            if (MetStsArray[5] == 1)
            {
                line = "信号线异常 ";
            }
            else
            {
                line = "不确定 ";
            }



            if (MetStsArray[6] == 0)
            {
                clock = "时钟正常 ";
            }
            else
            {
                clock = "无时钟";
            }



            if (MetStsArray[7] == 0)
            {
                power = "正常 ";
            }
            else
            {
                power = "有过功率限制";
            }
        }

        /// <summary>
        /// 通断电
        /// </summary>
        public string onOff { get; set; }
        /// <summary>
        /// 告警
        /// </summary>
        public string alarm { get; set; }

        public string rom1 { get; set; }

        public string rom2 { get; set; }

        /// <summary>
        /// 信号线
        /// </summary>
        public string line { get; set; }

        /// <summary>
        /// 时钟
        /// </summary>
        public string clock { get; set; }

        /// <summary>
        /// 功率
        /// </summary>
        public string power { get; set; }
    }
}
