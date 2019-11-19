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

    }
}
