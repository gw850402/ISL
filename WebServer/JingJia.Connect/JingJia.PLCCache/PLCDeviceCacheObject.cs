using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using Jingjia.PLCModel;

namespace JingJia.PLCCache
{
    /// <summary>
    ///  单例缓存
    ///  zhw
    ///  2019-11-19 17:25:11
    /// </summary>
    public class PLCDeviceCacheObject
    {

        /// <summary>
        /// 串口
        /// </summary>
        public SerialPort cachePort { get; set; }

        /// <summary>
        /// 字典
        /// </summary>
        private static Dictionary<string, object> _dataDic = new Dictionary<string, object>();



        /// <summary>
        /// 定义一个静态变量来保存类的实例
        /// </summary>
        private static PLCDeviceCacheObject _session;



        // <summary>
        /// 定义一个标识确保线程同步
        /// </summary>
        private static readonly object _locker = new object();



        /// <summary>
        /// 单例
        /// </summary>
        /// <returns>返回类型为Session</returns>
        public static PLCDeviceCacheObject Instance
        {
            get
            {
                if (_session == null)
                {
                    lock (_locker)
                    {
                        if (_session == null)// 如果类的实例不存在则创建，否则直接返回
                        {
                            _session = new PLCDeviceCacheObject();
                            _dataDic.Add("com", _comCache);//添加串口缓存
                            _dataDic.Add("device", _deviceCache);//添加设备信息缓存
                        }
                    }
                }
                return _session;
            }
        }

        /// <summary>
        /// 获取全部缓存
        /// </summary>
        public Dictionary<string, object> GetAll()
        {
            return _dataDic;
        }

        #region Remove 移除

        /// <summary>
        /// 删除成员
        /// </summary>
        /// <param name="name"></param>
        public void Remove(string name)
        {
            _dataDic.Remove(name);
        }

        /// <summary>
        /// 删除全部成员
        /// </summary>
        public void RemoveAll()
        {
            _dataDic.Clear();
        }
        #endregion


        #region 本类的索引器

        /// <summary>
        /// 本类的索引器
        /// </summary>
        /// <returns>返回Object成员</returns>
        public Object this[string index]
        {
            get
            {
                if (_dataDic.ContainsKey(index))
                {
                    Object obj = (Object)_dataDic[index];
                    return obj;
                }
                return null;
            }
            set
            {
                if (_dataDic.ContainsKey(index))
                {
                    _dataDic.Remove(index);
                }
                _dataDic.Add(index, value);
            }
        }
        #endregion




        #region 串口驱动缓存
        /// <summary>
        /// 串口字典
        /// </summary>
        private static Dictionary<string, SerialPort> _comCache = new Dictionary<string, SerialPort>();


        /// <summary>
        /// 添加串口缓存
        /// </summary>
        /// <param name="com"></param>
        /// <param name="serialPort"></param>
        public void AddSerialPort(string com , SerialPort serialPort)
        {
            if (_comCache.ContainsKey(com))
            {
                _comCache.Remove(com);
            }
            _comCache.Add(com, serialPort);
        }

        /// <summary>
        /// 获取串口缓存
        /// </summary>
        /// <param name="com"></param>
        /// <returns></returns>
        public SerialPort GetSerialPort(string com)
        {
            if (_comCache.ContainsKey(com))
            {
                SerialPort obj = _comCache[com];
                return obj;
            }
            return null;
        }

        #endregion

        #region 设备信息缓存

        /// <summary>
        /// 设备信息缓存
        /// </summary>
        private static Dictionary<int, Result63ReadBase> _deviceCache = new Dictionary<int, Result63ReadBase>();

        /// <summary>
        /// 添加设备信息
        /// </summary>
        /// <param name="result63ReadBase"></param>
        public void AddDeviceData(Result63ReadBase result63ReadBase)
        {
            if (_deviceCache.ContainsKey(result63ReadBase.Num))
            {
                _deviceCache.Remove(result63ReadBase.Num);
            }
            _deviceCache.Add(result63ReadBase.Num, result63ReadBase);
        }

        /// <summary>
        /// 获取单个设备缓存
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public Result63ReadBase GetDeviceData(int num)
        {
            if (_deviceCache.ContainsKey(num))
            {
                Result63ReadBase obj = _deviceCache[num];
                return obj;
            }
            return null;
        }

        /// <summary>
        /// 获取全部设备信息缓存
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Result63ReadBase> GetResult63ReadBases()
        {
            return _deviceCache;
        }
        #endregion

    }
}
