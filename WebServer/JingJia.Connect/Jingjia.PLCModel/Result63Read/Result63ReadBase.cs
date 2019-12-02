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
            if (data.Length > 4)
            {
                MetSts = data[3];
            }
            if (data.Length > 7)
            {
                Metbase = new byte[] { data[4], data[5], data[6] };
            }
            Num = deviceNum;
            date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");
        }

        /// <summary>
        /// 数据时间
        /// </summary>
        public string date { get; set; }

        /// <summary>
        /// 数值单位
        /// </summary>
        protected  string Unit { get; set; }

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
                double vaule = 0;
                if (Metbase != null)
                {
                    vaule = (Metbase[0] + Metbase[1] * 256 + Metbase[2] * 256 * 256) / 100F;
                }
                return vaule.ToString("F2") + Unit;
            }
        }


        public String Msg { get; set; }
    }
}
