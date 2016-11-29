//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Collections.Specialized;

namespace DotNet.Utilities
{
    public class IPInfo
    {
        public string Province { get; set; }

        public string City { get; set; }

        public string Ip { get; set; }
    }

    public class IpHelper
    {
        //readonly string ipBinaryFilePath = HttpRuntime.AppDomainAppPath + "\\DataSource\\17monipdb.dat";
        string ipBinaryFilePath = string.Empty;
        readonly byte[] dataBuffer, indexBuffer;
        readonly uint[] index = new uint[256];
        readonly int offset;

        private static IpHelper instance;

        private static object _lock = new object();

        public static IpHelper GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new IpHelper();
                        return instance;
                    }
                }
            }
            return instance;
        }


        public IpHelper(string filePath = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(filePath))
                {
                    ipBinaryFilePath = filePath;
                }
                else
                {
                    ipBinaryFilePath = HttpRuntime.AppDomainAppPath + "\\DataBase\\17monipdb.dat";
                }

                FileInfo file = new FileInfo(ipBinaryFilePath);
                dataBuffer = new byte[file.Length];
                using (var fin = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
                {
                    fin.Read(dataBuffer, 0, dataBuffer.Length);
                }

                var indexLength = BytesToLong(dataBuffer[0], dataBuffer[1], dataBuffer[2], dataBuffer[3]);
                indexBuffer = new byte[indexLength];
                Array.Copy(dataBuffer, 4, indexBuffer, 0, indexLength);
                offset = (int)indexLength;

                for (int loop = 0; loop < 256; loop++)
                {
                    index[loop] = BytesToLong(indexBuffer[loop * 4 + 3], indexBuffer[loop * 4 + 2], indexBuffer[loop * 4 + 1], indexBuffer[loop * 4]);
                }
            }
            catch
            {
            }
        }

        private static uint BytesToLong(byte a, byte b, byte c, byte d)
        {
            return ((uint)a << 24) | ((uint)b << 16) | ((uint)c << 8) | (uint)d;
        }

        private string[] Find(string ip)
        {
            //处理端口号的异常数据
            if (ip.IndexOf(':') > 0)
            {
                ip = ip.Split(':')[0];
            }
            var match =
                new Regex(@"((?:(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d))))");
            if (!match.IsMatch(ip))
            {
                return new[] { "", "", "", "" };
            }
            var ips = ip.Split('.');
            
            int ipPrefixValue = int.Parse(ips[0]);
            long ip2LongValue = BytesToLong(byte.Parse(ips[0]), byte.Parse(ips[1]), byte.Parse(ips[2]), byte.Parse(ips[3]));
            uint start = index[ipPrefixValue];
            int maxCompLen = offset - 1028;
            long indexOffset = -1;
            int indexLength = -1;
            byte b = 0;
            for (start = start * 8 + 1024; start < maxCompLen; start += 8)
            {
                if (BytesToLong(indexBuffer[start + 0], indexBuffer[start + 1], indexBuffer[start + 2], indexBuffer[start + 3]) >= ip2LongValue)
                {
                    indexOffset = BytesToLong(b, indexBuffer[start + 6], indexBuffer[start + 5], indexBuffer[start + 4]);
                    indexLength = 0xFF & indexBuffer[start + 7];
                    break;
                }
            }
            if (indexLength > 0)
            {
                var areaBytes = new byte[indexLength];
                Array.Copy(dataBuffer, offset + (int)indexOffset - 1024, areaBytes, 0, indexLength);
                return Encoding.UTF8.GetString(areaBytes).Split('\t');
            }
            return null;
        }

        public IPInfo FindIp(string ip)
        {
            ip = ip.Split(',')[0].Trim();
            if (string.IsNullOrWhiteSpace(ip) || ip.Length < 7)
            {
                // 错误的地址，不进行处理
                return null;
            }

            var location = Find(ip);
            if (location == null)
            {
                return null;
            }
            else if (string.IsNullOrEmpty(location[2]))
            {
                location[2] = location[1];
            }
            return new IPInfo { Province = location[1], City = location[2], Ip = ip };
        }

        public string FindName(string ip)
        {
            string result = string.Empty;
            IPInfo ipInfo = FindIp(ip);
            if (ipInfo != null)
            {
                if (!string.IsNullOrEmpty(ipInfo.Province))
                {
                    result = ipInfo.Province;
                    if (!string.IsNullOrEmpty(ipInfo.City))
                    {
                        if (!ipInfo.Province.Equals(ipInfo.City))
                        {
                            result += "-" + ipInfo.City;
                        }
                    }
                }
            }
            return result;
        }

        public static bool IsLocalIp(string ipAddress)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(ipAddress))
            {
                if (ipAddress.StartsWith("192.168.")
                    || ipAddress.StartsWith("172.")
                    || ipAddress.StartsWith("10."))
                {
                    result = true;
                }
                // 检查是否在公司新任的列表里
                if (!result)
                {
                    if (!string.IsNullOrEmpty(BaseSystemInfo.WhiteList))
                    {
                        string[] whiteLists = BaseSystemInfo.WhiteList.Split(',');
                        for (int i = 0; i < whiteLists.Length; i++)
                        {
                            if (whiteLists[i].Equals(ipAddress))
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}
