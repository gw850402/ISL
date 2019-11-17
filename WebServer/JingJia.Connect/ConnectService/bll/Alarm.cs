using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectService.bll
{
    public class Alarm
    {
        public bool _isScan = true;
        public void Start()
        {
            System.Threading.Thread thd = new System.Threading.Thread(new System.Threading.ThreadStart(ScanWater));
            thd.Start();
           
        }
        private void ScanWater()
        {
            while (_isScan)
            {
               // if (JingJia.PLCDriver.DriveFactory.GetPLCInstence().GetElectricity(22)>0)
                //    CommonDefinition.MsgServer.SetMessage(ConnectService.Common.ResultJsonString(22, "有水", "成功"));
            
                System.Threading.Thread.Sleep(2000);
            }
        }
        public void Stop()
        {
            _isScan = false;
        }
    }
}