using JingJia.PLCCache;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;

namespace JingJia.PLCDriver
{
    /// <summary>
    /// 串口同步驱动
    /// </summary>
    public class GR10
    {
        SerialPort _port = null;
       
        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="comPort"></param>
        public void Open(string comPort)
        {
            byte[] _sendData = new byte[1024];
            byte[] _reciveData = new byte[1024];
            if (_port==null)
                _port = new SerialPort(comPort, 19200);

            //连接
            _sendData[0] = Convert.ToByte('S');
            _sendData[1] = 0x07;
            _sendData[2] = Convert.ToByte('U');
            _sendData[3] = Convert.ToByte('Z');
            _sendData[4] = Convert.ToByte('U');
            _sendData[5] = Convert.ToByte('Z');
            _sendData[6] = Convert.ToByte('U');
            _sendData[7] = Convert.ToByte('Z');
            if (!_port.IsOpen)
                _port.Open();
            _port.Write(_sendData, 0, 8);
            _port.Read(_reciveData, 0, 2);
            // _port.Close();
            // 修改串口流保存至缓存 zhw  2019-11-25 20:30:50
            PLCDeviceCacheObject.Instance.AddSerialPort(comPort, _port);
        }
        
        /// <summary>
        /// 串口缓冲区
        /// </summary>
        private List<byte> buffer = new List<byte>(4096);

        /// <summary>
        /// 发送串口字节数据 同步方法 zhw
        /// </summary>
        /// <param name="sendData">发送的数据</param>
        /// <returns>返回命令结果 buf == null 为超时</returns>
        public byte[] SendData(string com, byte[] sendData)
        {
            try
            {
                _port = PLCDeviceCacheObject.Instance.GetSerialPort(com);

                lock (_port)
                {
                    buffer.Clear();//清空缓冲区
                    int clock = 300;//初始化时钟 
                    _port.Write(sendData, 0, sendData.Length);//写入数据
                    _port.DataReceived += _port_DataReceived;//订阅串口数据

                    for (int i = clock; i > 0; i--)
                    {
                        Thread.Sleep(10);//延迟
                        byte[] clist = CheckBuffer(buffer);//校验数据包
                        if (clist != null)
                        {
                            return clist;
                        }
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 串口数据订阅函数
        /// </summary>
        private void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int n = _port.BytesToRead;
            byte[] buf = new byte[n];
            _port.Read(buf, 0, n);
            buffer.AddRange(buf);
        }

        /// <summary>
        /// 校验数据包
        /// </summary>
        private byte[] CheckBuffer(List<byte> buffer) {

            if (buffer.Count < 5) {
                return null;
            }

            while (buffer[0] != 0x54) {
                buffer.RemoveAt(0);
            }

            if (buffer.Count < 5)
            {
                return null;
            }

            int len = buffer[1];

            if (buffer.Count > len)
            {
                byte[] copyBytes = new byte[len + 1];
                buffer.CopyTo(0, copyBytes, 0, len + 1);
                return copyBytes;
            }
            else {
                
                return null;
            }

        } 

    }
}
