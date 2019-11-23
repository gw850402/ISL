using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 设备状态枚举
/// 
/// </summary>
namespace JingJia.PLCComm
{
    /// <summary>
    /// 设备类型 功能枚举
    /// </summary>
    public enum EnumDeviceType
    {
        电表 = 1,
        水表 = 2,
        阀门 = 4,
        灯控 = 8,
        湿度 = 16,
        温度 = 32,
        光感度 = 64,
        水浸 = 128,
        阀控水表 = 256

    }

    /// <summary>
    /// 命令类型枚举
    /// </summary>
    public enum EnumHandleType
    {
        抄录 = 1,
        通电 = 2,
        断电 = 3
    }

    
    public enum EnumStateAmmeter
    {
        电表通电 = 0,
        电表断电 = 1,
    }
}
