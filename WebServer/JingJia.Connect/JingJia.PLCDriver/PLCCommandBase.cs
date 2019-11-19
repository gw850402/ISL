using JingJia.PLCComm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JingJia.PLCDriver
{
    public class PLCCommandBase
    {


        //_sendData[0] = Convert.ToByte('S');//


        /// <summary>
        /// 针头
        /// </summary>
        protected byte SendHead;

        /// <summary>
        /// 数据长度
        /// </summary>
        protected byte SendLen { get; set; }


        /// <summary>
        /// 命令码
        /// </summary>
        protected byte SendCode { get; set; }

        /// <summary>
        /// 表号
        /// </summary>
        protected byte[] MetNum { get; set; }


        /// <summary>
        /// 中继表号
        /// </summary>
        protected byte[] RlyNum { get; set; }

        /// <summary>
        /// 集中器号
        /// </summary>
        protected byte[] CnSno { get; set; }


        /// <summary>
        /// 内容
        /// </summary>
        protected byte[] DetailBytes { get; set; }


        /// <summary>
        /// 发送命令
        /// </summary>
        public byte[] SendDataByte { get; set; }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected byte[] BuildCommand()
        {
            List<byte> tempList = new List<byte>();
            tempList.AddRange(new byte[] { SendHead });//针头
            tempList.AddRange(new byte[] { SendLen });//数据长度
            tempList.AddRange(new byte[] { SendCode });//命令码
            tempList.AddRange(DetailBytes);//差异内容
            byte[] byte1 = tempList.ToArray();
            tempList.AddRange(new byte[] { Tools.Add(byte1, 1, SendLen) });//和校验
            tempList.AddRange(new byte[] { Tools.Crc(byte1, 1, SendLen) });//异或校验
            SendDataByte = tempList.ToArray();
            return SendDataByte;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceNum"></param>
        /// <returns></returns>
        public virtual byte[] BuildCommandByte(int deviceNum)
        {
            throw new Exception("基类方法不允许直接调用！");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">返回结果</param>
        /// <returns>JSON</returns>
        public virtual string BuildResultDataJson(byte[] data)
        {
            throw new Exception("基类方法不允许直接调用！");
        }

    }
}
