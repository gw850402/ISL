using Jingjia.PLCModel;
using JingJia.PLCComm;
using System;
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
        static object comLock = new object();
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
        /// 发送串口字节数据
        /// </summary>
        /// <param name="sendData"></param>
        /// <returns></returns>
        public byte[] SendData(byte[] sendData)
        {
            byte[] resData = new byte[512];
            lock (comLock)
            {
                if (!_port.IsOpen)
                    _port.Open();
                _port.Write(sendData, 0, sendData.Length);
                System.Threading.Thread.Sleep(2000);
                _port.Read(resData, 0, 12);
                _port.Close();
            }
            return resData;
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

            lock (comLock)
            {
                if (!_port.IsOpen)
                    _port.Open();
                _port.Write(_sendData, 0, 12);
                _reciveData[0] = 0;
                _reciveData[1] = 0;
                _reciveData[2] = 0;

                System.Threading.Thread.Sleep(2000);
                _port.Read(_reciveData, 0, 6);
                _port.Close();
            }
            if ((_reciveData[3] & 128) > 0 || _reciveData[1] == 0)
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

            lock (comLock)
            {
                if (!_port.IsOpen)
                    _port.Open();
                _port.Write(_sendData, 0, 12);
                _reciveData[0] = 0;
                _reciveData[1] = 0;
                _reciveData[2] = 0;
                System.Threading.Thread.Sleep(2000);
                _port.Read(_reciveData, 0, 6);
                _port.Close();
                if ((_reciveData[3] & 128) == 0 || _reciveData[1] == 0)
                    throw new Exception("关闭失败");
            }
        }
        /// <summary>
        /// 根据单项电表表号获取表底读数
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public float GetElectricity(int code)
        {
            float r=0f;
            _sendData[0] = Convert.ToByte('S');
            _sendData[1] = 7;
            _sendData[2] = 63;
            _sendData[3] = (byte)((code & 0x000000ff)); //表号
            _sendData[4] = (byte)((code & 0x0000FF00) >> 8);
            _sendData[5] = (byte)((code & 0x00FF0000) >> 16);
            //_sendData[6] = 0;
            _sendData[6] = Add(_sendData, 1, 6);
            _sendData[7] = Crc(_sendData, 1, 6);
            
            try
            {
                lock (comLock)
                {

                    if (!_port.IsOpen)
                        _port.Open();
                    _port.Write(_sendData, 0, 9);
                    _reciveData[0] = 0;
                    _reciveData[1] = 0;
                    _reciveData[2] = 0;
                    //实践证明必须等待大约2秒钟，否则获取的数据为空 zhws
                    System.Threading.Thread.Sleep(2000);
                    _port.Read(_reciveData, 0, 9);
                }
                if (_reciveData[0] != 0)
                {
                    r = (_reciveData[4] + _reciveData[5] * 256 + _reciveData[6] * 256 * 256) / 100F;
                    return r;
                }
                else
                {
                    throw new Exception("获取表底信息失败");
                }
            }
            catch
            {
                throw new Exception("获取表底信息失败");
            }
            finally
            {
                _port.Close();
            }
        }

        /// <summary>
        /// 抄录
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public PLCDeviceBase Read(int code, EnumHandleType enumDeviceType) {

            float r = 0f;
            _sendData[0] = Convert.ToByte('S');
            _sendData[1] = 7;
            _sendData[2] = 63;
            _sendData[3] = (byte)((code & 0x000000ff)); //表号
            _sendData[4] = (byte)((code & 0x0000FF00) >> 8);
            _sendData[5] = (byte)((code & 0x00FF0000) >> 16);
            //_sendData[6] = 0;
            _sendData[6] = Add(_sendData, 1, 6);
            _sendData[7] = Crc(_sendData, 1, 6);

            PLCDeviceBase pLCDeviceBase;
            try
            {
                byte[] resByte = SendData(_sendData);

                 pLCDeviceBase = new PLCDeviceBase(resByte[3],new byte[] { resByte[4], resByte[5], resByte[6] });

                //待实现
                if (enumDeviceType == EnumHandleType.电表抄录) 
                {
                    
                }
            }
            catch
            {
                pLCDeviceBase = new PLCDeviceBase();
                pLCDeviceBase.Msg = "串口数据通信异常。";
            }
        
            return pLCDeviceBase;
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
