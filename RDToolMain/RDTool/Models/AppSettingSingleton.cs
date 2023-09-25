using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace RDTool.Models
{
    public class VMSConfig
    {
        /// <summary>
        /// 服务端地址
        /// </summary>
        public string BaseURL { get; set; }
    }
    public class Connections
    {
        public string Name { get; set; }

        public string IP { get; set; }
        public uint Port { get; set; }

        public string Username { get; set; }
    }

    public class AppSettingSingleton
    {
        private static readonly object _lock = new object();
        private static AppSettingSingleton Ins { get; set; }
        private AppSettingSingleton()
        {

        }


        #region 业务参数
        /// <summary>
        /// 连接配置
        /// </summary>
        public List<Connections> Connections { get; set; }

        public VMSConfig VMS { get; set; }
        #endregion

        public static AppSettingSingleton Instance
        {
            get
            {
                if (Ins != null)
                {
                    return Ins;
                }

                lock (_lock)
                {
                    {
                        if (Ins != null)
                        {
                            return Ins;
                        }

                        var appsettingPath = AppDomain.CurrentDomain.BaseDirectory + "appsetting.json";

                        if (!File.Exists(appsettingPath))
                        {
                            throw new Exception("setting err");
                        }

                        var appsettingContent = File.ReadAllText(appsettingPath);
                        if (string.IsNullOrEmpty(appsettingContent))
                        {
                            throw new Exception("setting cannot parse");
                        }

                        try
                        {
                            Ins = JsonConvert.DeserializeObject<AppSettingSingleton>(appsettingContent);
                        }
                        catch (Exception e)
                        {
                            throw new Exception("setting cannot parse," + e.Message);
                        }
                    }
                }


                return Ins;
            }
        }
    }
}
