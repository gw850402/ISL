using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jingjia.PLCModel.Result63Read
{
    /// <summary>
    /// 水表返回结果
    /// </summary>
    public class Result63ReadWatermeter : Result63ReadBase
    {


        // <summary>
        /// 状态
        /// </summary>
        public string onOff { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="deviceNum"></param>
        /// <param name="data"></param>
        public Result63ReadWatermeter(int deviceNum, byte[] data)
                 : base(deviceNum, data)
        {
            Unit = "m³";


            if (MetStsArray[0] == 0)
            {
                onOff = "正常";
            }
            else
            {
                onOff = "水表断水";
            }

        }



    }
}
