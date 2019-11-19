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
        电表抄录 = 10,
        电表通电 = 11,
        电表断电 = 12,
        电表告警 = 13,
        电表关告警 = 14,
        水表抄录 = 20,
        阀门全开 = 30,
        阀门关三分之一 = 31,
        阀门关三分之二 = 32,
        阀门全关 = 33,
        灯控通电 = 40,
        灯控断电 = 41,
        灯控抄录 = 42,
        湿度抄录 = 50,
        温度抄录 = 60,
        光感度抄录 = 70,
        水侵抄录 = 80,
        阀控水表 = 90
    }


    public enum EnumStateAmmeter
    {
        电表通电 = 0,
        电表断电 = 1,
    }
}
