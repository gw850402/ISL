using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jingjia.PLCModel.Result63Read
{
    /// <summary>
    /// 阀门返回结果
    /// </summary>
    public class Result64ReadValve : Result63ReadBase
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
        public Result64ReadValve(int deviceNum, byte[] data)
                 : base(deviceNum, data)
        {
            if (MetStsArray[0] == 0 || MetStsArray[1] == 0)

            {
                onOff = "全开";
            }
            else if (MetStsArray[0] == 1 || MetStsArray[1] == 0)
            {
                onOff = "1/3开";
            }
            else if (MetStsArray[0] == 0 || MetStsArray[1] == 1)
            {
                onOff = "2/3开";
            }
            else if (MetStsArray[0] == 1 || MetStsArray[1] == 1)
            {
                onOff = "全关";
            }
            else
            {
                onOff = "未知";
            }

        }

    }
}
