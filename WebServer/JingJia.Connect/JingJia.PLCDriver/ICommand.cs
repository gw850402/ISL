using Jingjia.PLCModel;
using JingJia.PLCComm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JingJia.PLCDriver
{
    /// <summary>
    /// 命令操作接口
    /// </summary>
    public interface ICommand
    {

        /// <summary>
        /// 执行单个命令
        /// </summary>
        /// <param name="num">设备编号</param>
        /// <param name="handleType">执行类型</param>
        /// <returns>返回执行结果</returns>
         PLCDeviceBase ExecuteCommand(int num, EnumHandleType handleType);


        /// <summary>
        /// 添加命令队列 暂不启用
        /// </summary>
        /// <param name="num">设备编号</param>
        /// <param name="handleType">执行类型</param>
         void AddCommand(int num, EnumHandleType handleType);

    }
}
