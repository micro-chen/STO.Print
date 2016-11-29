//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace DotNet.Utilities
{
    /// <summary>
    /// MachineInfo
    /// 获取硬件信息的部分
    /// 
    /// 修改记录
    ///
    ///		2011.07.15 版本：1.0 JiRiGaLa	主键创建。
    ///		
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2011.07.15</date>
    /// </author>
    /// </summary>
    public class MachineInfo
    {
        /// <summary>
        /// 获取当前使用的IPV4地址
        /// </summary>
        /// <returns></returns>
        public static string GetIPAddress()
        {
            string result = string.Empty;
            // System.Net.IPHostEntry ipHostEntrys = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            List<string> ipList = GetIPAddressList();
            foreach (string ip in ipList)
            {
                result = ip.ToString();
                break;
            }
            return result;
        }

        /// <summary>
        /// 获取IPv4地址列表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetIPAddressList()
        {
            List<string> ipAddressList = new List<string>();
            IPHostEntry ipHostEntrys = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            foreach (IPAddress ip in ipHostEntrys.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ipAddressList.Add(ip.ToString());
                }
            }
            return ipAddressList;
        }

        /// <summary>
        /// GetWirelessIPList 获得无线网络接口的IpV4 地址列表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetWirelessIPAddressList()
        {
            List<string> ipAddressList = new List<string>();
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface ni in networkInterfaces)
            {
                if (ni.Description.Contains("Wireless"))
                {
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            ipAddressList.Add(ip.Address.ToString());
                        }
                    }
                }
            }
            return ipAddressList;
        }

        /// <summary>
        /// 获取地址
        /// </summary>
        /// <returns>地址</returns>
        public static string GetMacAddress(bool single = true)
        {
            string result = string.Empty;
            string macAddress = string.Empty;
            List<string> macAddressList = GetMacAddressList().OrderBy(ip => ip).Take(2).ToList();
            foreach (string mac in macAddressList)
            {
                if (!string.IsNullOrEmpty(mac))
                {
                    macAddress = mac.ToString();
                    // 格式化
                    macAddress = string.Format("{0}-{1}-{2}-{3}-{4}-{5}",
                        macAddress.Substring(0, 2),
                        macAddress.Substring(2, 2),
                        macAddress.Substring(4, 2),
                        macAddress.Substring(6, 2),
                        macAddress.Substring(8, 2),
                        macAddress.Substring(10, 2));
                    if (single)
                    {
                        result = macAddress;
                        break;
                    }
                    else
                    {
                        result += macAddress + ";";
                    }
                }
            }
            result = result.TrimEnd(';');
            return result;
        }

        /// <summary>
        /// 获取MAC地址列表，注意优先级高的放在了后面
        /// </summary>
        /// <returns></returns>
        public static List<string> GetMacAddressList()
        {
            List<string> macAddressList = new List<string>();
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface ni in networkInterfaces)
            {
                // 网卡备注中有wireless，则判断是无限网卡,过滤掉虚拟网卡和移动网卡
                // !ni.Description.Contains("WiFi") && 
                if (!ni.Description.Contains("Loopback")
                    && !ni.Description.Contains("VMware")
                    && !ni.Description.Contains("Teredo")
                    && !ni.Description.Contains("Microsoft")
                    && !ni.Description.Contains("Virtual")
                    && !ni.Description.Contains("Microsoft")
                    && !ni.Description.Contains("IEEE 1394")
                    && ni.OperationalStatus == OperationalStatus.Up)
                {
                    string macAddress = ni.GetPhysicalAddress().ToString();
                    if (!string.IsNullOrEmpty(macAddress) && !macAddress.Equals("000000000000") && macAddress.Length == 12)
                    {
                        macAddressList.Add(ni.GetPhysicalAddress().ToString());
                    }
                }
            }
            return macAddressList;
        }

        /// <summary>
        /// GetWirelessMacList 获得无线网络接口的MAC地址列表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetWirelessMacAddressList()
        {
            List<string> macAddressList = new List<string>();
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface ni in networkInterfaces)
            {
                // 网卡备注中有wireless，则判断是无限 网卡
                if (ni.Description.Contains("Wireless") && ni.OperationalStatus == OperationalStatus.Up)
                {
                    macAddressList.Add(ni.GetPhysicalAddress().ToString());
                }
            }
            return macAddressList;
        }

        /// <summary>
        /// 获取cpu序列号
        /// </summary>
        /// <returns>序列号</returns>
        public static string GetCPUSerialNo()
        {
            string cpuSerialNo = string.Empty;
            ManagementClass managementClass = new ManagementClass("Win32_Processor");
            ManagementObjectCollection managementObjectCollection = managementClass.GetInstances();
            foreach (ManagementObject managementObject in managementObjectCollection)
            {
                // 可能是有多个
                cpuSerialNo = managementObject.Properties["ProcessorId"].Value.ToString();
                break;
            }
            return cpuSerialNo;
        }

        public static string GetHardDiskInfo()
        {
            string hardDisk = string.Empty;
            ManagementClass managementClass = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection managementObjectCollection = managementClass.GetInstances();
            foreach (ManagementObject managementObject in managementObjectCollection)
            {
                // 可能是有多个
                hardDisk = (string)managementObject.Properties["Model"].Value;
                break;
            }
            return hardDisk;
        }

        public static void SetLocalTime(DateTime dateTime)
        {
            SystemTime systemTime = new SystemTime();
            SetSystemDateTime.GetLocalTime(systemTime);
            systemTime.vYear = (ushort)dateTime.Year;
            systemTime.vMonth = (ushort)dateTime.Month;
            systemTime.vDay = (ushort)dateTime.Day;
            systemTime.vHour = (ushort)dateTime.Hour;
            systemTime.vMinute = (ushort)dateTime.Minute;
            systemTime.vSecond = (ushort)dateTime.Second;
            SetSystemDateTime.SetLocalTime(systemTime);
        }

        [DllImport("kernel32.dll", EntryPoint = "GetSystemDefaultLCID")]
        public static extern int GetSystemDefaultLCID();
        [DllImport("kernel32.dll", EntryPoint = "SetLocaleInfoA")]
        public static extern int SetLocaleInfo(int Locale, int LCType, string lpLCData);
        public const int LOCALE_SLONGDATE = 0x20;
        public const int LOCALE_SSHORTDATE = 0x1F;
        public const int LOCALE_STIME = 0x1003;

        public static void SetDateTimeFormat()
        {
            try
            {
                int x = GetSystemDefaultLCID();
                // 时间格式 
                SetLocaleInfo(x, LOCALE_STIME, "HH:mm:ss");
                // 短日期格式
                SetLocaleInfo(x, LOCALE_SSHORTDATE, "yyyy-MM-dd");
                // 长日期格式
                SetLocaleInfo(x, LOCALE_SLONGDATE, "yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// 获得真实IP信息 通过能提供IP查询的网站
        /// 目前是GIS系统提供的IP地址：http://userCenter.zt-express.com/UserCenterV42/PermissionService.ashx?function=GetClientIP
        /// 2016-01-24 吉日嘎拉 改进服务器诊断能力
        /// </summary>
        /// <param name="ipUrl">提供IP显示的网址</param>
        /// <returns>IP地址</returns>
        public static string GetIPByWebRequest(string ipUrl = "")
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(ipUrl))
            {
                ipUrl = BaseSystemInfo.UserCenterHost + "/UserCenterV42/PermissionService.ashx?function=GetClientIP";
            }
            try
            {
                Uri uri = new Uri(ipUrl);
                WebRequest webRequest = WebRequest.Create(uri);
                using (System.IO.Stream stream = webRequest.GetResponse().GetResponseStream())
                {
                    System.IO.StreamReader streamReader = new System.IO.StreamReader(stream, System.Text.Encoding.Default);
                    result = streamReader.ReadToEnd();
                }
            }
            catch (Exception)
            {
            }

            return result;
        }
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public class SystemTime
    {
        public ushort vYear;
        public ushort vMonth;
        public ushort vDayOfWeek;
        public ushort vDay;
        public ushort vHour;
        public ushort vMinute;
        public ushort vSecond;
    }

    public class SetSystemDateTime
    {
        [DllImportAttribute("Kernel32.dll")]
        public static extern void GetLocalTime(SystemTime systemTime);
        [DllImportAttribute("Kernel32.dll")]
        public static extern void SetLocalTime(SystemTime systemTime);
    }
}