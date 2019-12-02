using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jingjia.PLCModel.Result63Read
{
    public class Result63ReadWater : Result63ReadBase
    {


        // <summary>
        /// 状态
        /// </summary>
        public string onOff { get; set; }
        public Result63ReadWater(int deviceNum, byte[] data)
                    : base(deviceNum, data)
        {
            if (data[4] == 0x64)
            {
                onOff = "有水";
            }
            else
            {
                onOff = "无水";
            }

        }
    }
}
