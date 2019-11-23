using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jingjia.PLCModel.Result63Read
{
    public class Result63ReadOptical : Result63ReadBase
    {
        public Result63ReadOptical(int deviceNum, byte[] data) : base(deviceNum, data)
        {

            Unit = "lx";

        }
    }
}
