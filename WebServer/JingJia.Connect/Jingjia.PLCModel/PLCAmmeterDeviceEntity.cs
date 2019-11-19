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
    public class PLCAmmeterDeviceEntity : PLCDeviceBase
    {

        /// <summary>
        /// 通断电
        /// </summary>
        public EnumStateAmmeter OnOff { get; set; }


        /// <summary>
        /// 表底值
        /// </summary>
        public double Value { get; set; }



    }
}
