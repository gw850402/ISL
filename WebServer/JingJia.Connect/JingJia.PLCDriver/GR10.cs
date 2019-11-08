using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace JingJia.PLCDriver
{
    /// <summary>
    /// 京甲GR10集中器通讯协议
    /// </summary>
    public class GR10
    {
        SerialPort _port = null;
        byte[] _sendData = new byte[1024];
        byte[] _reciveData = new byte[1024];

        public void Open(string comPort)
        {
            if(_port==null)
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
            _port.Close();

        }
        public void Close()
        {
            if (_port.IsOpen)
            {
                _port.Close();
            }
        }
        /// <summary>
        /// 断路器打开
        /// </summary>
        /// <param name="code">表号</param>
        public void SetOn(int code)
        {
            //通 53 0B 32 80 00 00 00 4D 00 00 0B F4  | S\#112\#0\#0\#0M\#0\#0\#11ô
            _sendData[0] = Convert.ToByte('S');
            _sendData[1] = 11;
            _sendData[2] = 50;
            _sendData[3] = 128;
            _sendData[4] = 0; //MetSetSts
            _sendData[5] = 0; //MetPwr
            _sendData[6] = 0; //Group

            _sendData[7] = (byte) ((code & 0x000000ff)); ; //表号
            _sendData[8] = (byte) ((code & 0x0000FF00)>>8);
            _sendData[9] = (byte) ((code & 0x00FF0000) >> 16); 


            _sendData[10] = Add(_sendData, 1, 9);
            _sendData[11] = Crc(_sendData, 1, 9);


            if (!_port.IsOpen)
                _port.Open();
            _port.Write(_sendData, 0, 12);
            _port.Read(_reciveData, 0, 6);
            _port.Close();
            if ((_reciveData[3] & 128) > 0)
                throw new Exception("打开失败");
           
        }
        /// <summary>
        /// 断路器关闭
        /// </summary>
        /// <param name="code"></param>
        public void SetOff(int code)
        {
            _sendData[0] = Convert.ToByte('S');
            _sendData[1] = 11;
            _sendData[2] = 50;
            _sendData[3] = 128;
            _sendData[4] = 128; //MetSetSts
            _sendData[5] = 0; //MetPwr
            _sendData[6] = 0; //Group

            _sendData[7] = (byte)((code & 0x000000ff)); //表号
            _sendData[8] = (byte)((code & 0x0000FF00) >> 8);
            _sendData[9] = (byte)((code & 0x00FF0000) >> 16); 


            _sendData[10] = Add(_sendData, 1, 9);
            _sendData[11] = Crc(_sendData, 1, 9);


            if (!_port.IsOpen)
                _port.Open();
            _port.Write(_sendData, 0, 12);
            _port.Read(_reciveData, 0, 6);
            _port.Close();
            if ((_reciveData[3] & 128) == 0)
                throw new Exception("关闭失败");

        }

        private byte Add(byte[] data, int start, int length)
        {
            int temp = 0;

            for (int i = start; i < length; i++)
            {
                temp += data[i];
                if (temp >= 256)
                    temp = temp % 256;
            }
            return (byte)temp;
        }
        private byte Crc(byte[] data, int start, int length)
        {
            byte CheckCode = 0;

            for (int i = start; i < length; i++)
            {
                CheckCode ^= data[i];
            }
            return CheckCode;
        }
    }
}
