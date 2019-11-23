using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jingjia.PLCModel.Result63Read
{
    /// <summary>
    /// 
    /// </summary>
    public class Result63ReadTemperature : Result63ReadBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="deviceNum"></param>
        /// <param name="data"></param>
        public Result63ReadTemperature(int deviceNum, byte[] data)
                 : base(deviceNum, data)
        {
            Unit = "℃";
        }
    }
}
