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
              //  CommonDefinition.MsgServer.SetMessage(DateTime.Now.ToString());
                System.Threading.Thread.Sleep(20000);
            }
        }
        public void Stop()
        {
            _isScan = false;
        }
    }
}