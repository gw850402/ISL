using JingJia.PLCCache;
using JingJia.PLCComm;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jingjia.Task
{
    /// <summary>
    /// 
    /// </summary>
    public class TimeJob : IJob
    {



        public void Execute(IJobExecutionContext context)
        {
            //Console.WriteLine("我被触发了：" + a++ + "次。");
            //向c:\Quartz.txt写入当前时间并换行


            if (PLCDeviceCacheObject.Instance["66"] == null)
            {
                PLCDeviceCacheObject.Instance["66"] = EnumHandleType.通电Or全开;
            }

            if ((EnumHandleType)PLCDeviceCacheObject.Instance["66"] == EnumHandleType.通电Or全开) {
                PLCDeviceCacheObject.Instance["66"] = EnumHandleType.断电Or关三分之二;
            }

            else
            {
                PLCDeviceCacheObject.Instance["66"] = EnumHandleType.通电Or全开;
            }

            string json = JingJia.PLCDriver.CommandQueueDriver.ExecuteCommand(66, (EnumHandleType)PLCDeviceCacheObject.Instance["66"], EnumDeviceType.灯控);

            System.IO.File.AppendAllText(@"c:\Quartz.txt", DateTime.Now + json + Environment.NewLine);
        }
    }
}
