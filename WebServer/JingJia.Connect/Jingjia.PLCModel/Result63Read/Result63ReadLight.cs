using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jingjia.PLCModel.Result63Read
{
    public class Result63ReadLight : Result63ReadBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="deviceNum"></param>
        /// <param name="data"></param>
        public Result63ReadLight(int deviceNum, byte[] data)
                 : base(deviceNum, data)
        {

            if (MetStsArray[0] == 0)
            {
                onOff = "通电";
            }
            else
            {
                onOff = "断电";
            }
        }


        /// <summary>
        /// 通断电
        /// </summary>
        public string onOff { get; set; }

    }
}