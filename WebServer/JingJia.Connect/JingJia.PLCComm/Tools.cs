using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JingJia.PLCComm
{
    public class Tools
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte Add(byte[] data, int start, int length)
        {
            int temp = 0;

            for (int i = start; i < length-1; i++)
            {
                temp += data[i];
                if (temp >= 256)
                    temp = temp % 256;
            }
            return (byte)temp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte Crc(byte[] data, int start, int length)
        {
            byte CheckCode = 0;

            for (int i = start; i < length-1; i++)
            {
                CheckCode ^= data[i];
            }
            return CheckCode;
        }


        /// <summary>
        /// 转化表号
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static byte[] NumIntToArray(int num) 
        {
            byte[] numBytes = new byte[3];
            numBytes[0] = (byte)(num & 0x000000ff); //表号
            numBytes[1] = (byte)((num & 0x0000FF00) >> 8);
            numBytes[2] = (byte)((num & 0x00FF0000) >> 16);
            return numBytes;
        }
    }
}
