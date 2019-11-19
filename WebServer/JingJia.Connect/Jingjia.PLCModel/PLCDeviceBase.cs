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
        }

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
        public double Value
        {
            set {
                Value = value;
            }
            get
            {
                double vaule = (Metbase[0] + Metbase[1] * 256 + Metbase[2] * 256 * 256) / 100F;
                return vaule;
            }
        }


        public String Msg { get; set; }


     

    }
}
