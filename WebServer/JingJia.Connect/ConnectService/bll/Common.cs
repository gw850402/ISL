using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectService
{
    /// <summary>
    /// 常量定义
    /// </summary>
    public class Common
    {
        /// <summary>
        /// 返回执行结果字符串
        /// </summary>
        /// <param name="deviceCode">表号</param>
        /// <param name="state">设备状态，开 = 1 关=0,或者返回参数</param>
        /// <param name="msg">成功 or 失败</param>
        /// <returns></returns>
        public static String ResultJsonString(int deviceCode, string state, string msg)
        {
            return "{\"code\": 0,\"msg\": \"" + msg + "\",\"device\":" + deviceCode + ",\"state\":"+state+"}";

        }



    }
}