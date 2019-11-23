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
    public class Result63ReadBase
    {
        public Result63ReadBase(int deviceNum, byte[] data)
        {
            MetSts = data[3];
            Metbase = new byte[] { data[4], data[5], data[6] };
            Num = deviceNum;
        }

        /// <summary>
        /// 数值单位
        /// </summary>
        protected string Unit { get; set; }

        /// <summary>
        /// 表号
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 表计状态
        /// </summary>
        protected byte MetSts { get; set; }

        /// <summary>
        /// 状态参数数组
        /// </summary>
        protected byte[] MetStsArray
        {
            get
            {
                byte[] staes = Tools.GetBooleanArray(MetSts);
                return staes;
            }
        }

        /// <summary>
        /// 表计底数
        /// </summary>
        protected byte[] Metbase { get; set; }

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
                return vaule + Unit;
            }
        }


        public String Msg { get; set; }
    }
}
