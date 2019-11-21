using JingJia.PLCComm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jingjia.PLCModel
{
    /// <summary>
    /// 设备模型基类
    /// zhw
    /// 2019-11-19 
    /// </summary>
    public class PLCDeviceBase
    {


        public PLCDeviceBase()
        {

        }

        public PLCDeviceBase(byte metSts, byte[] metbase)
        {
            MetSts = metSts;
            Metbase = metbase;

            byte[] staes = Tools.GetBooleanArray(metSts);

            if (staes[0] == 0)
            {
                onOff = "电表通电";
            }
            else
            {
                onOff = "电表断电";
            }

            if (staes[1] == 0)
            {
                alarm = "电表不告警 ";
            }
            else
            {
                alarm = "电表告警";
            }

            if (staes[1] == 0)
            {
                alarm = "电表不告警 ";
            }
            else
            {
                alarm = "电表告警";
            }

            if (staes[2] == 0)
            {
                rom1 = "正常 ";
            }
            else
            {
                rom1 = "ROM出错旦纠正";
            }

            if (staes[3] == 0)
            {
                rom2 = "ROM正常 ";
            }
            else
            {
                rom2 = "ROM坏掉";
            }


            if (staes[4] == 1)
            {
                line = "ROM正常 ";
            }
            else
            {
                line = "ROM坏掉";
            }


            if (staes[5] == 1)
            {
                line = "信号线异常 ";
            }
            else
            {
                line = "不确定 ";
            }






            if (staes[6] == 0)
            {
                clock = "时钟正常 ";
            }
            else
            {
                clock = "无时钟";
            }



            if (staes[7] == 0)
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



        /// <summary>
        /// 表号
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 表计状态
        /// </summary>
        public byte MetSts { get; set; }

        /// <summary>
        /// 表计底数
        /// </summary>
        public byte[] Metbase { get; set; }

        /// <summary>
        /// 读取值
        /// </summary>
        public string Value
        {
            set
            {
                Value = value;
            }
            get
            {
                double vaule = (Metbase[0] + Metbase[1] * 256 + Metbase[2] * 256 * 256) / 100F;
                return vaule + " kWh";
            }
        }


        public String Msg { get; set; }




    }
}
