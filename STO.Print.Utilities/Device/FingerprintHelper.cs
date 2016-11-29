
namespace STO.Print.Utilities
{
    /// <summary>
    /// 提供用户硬件唯一信息的辅助类
    /// </summary>
    public class FingerprintHelper
    {
        public static string Value()
        {
            return pack(cpuId()
                    + biosId()
                    + diskId()
                    + baseId()
                    + videoId()
                    + macId());
        }

        //返回一个硬件标识符
        private static string identifier(string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            string result="";
            System.Management.ManagementClass mc = new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                if (mo[wmiMustBeTrue].ToString()=="True")
                {

                    //仅获得第一个
                    if (result=="")
                    {
                        try
                        {
                            result = mo[wmiProperty].ToString();
                            break;
                        }
                        catch
                        {
                        }
                    }

                }
            }
            return result;
        }

        //返回一个硬件标识符
        private static string identifier(string wmiClass, string wmiProperty)
        {
            string result="";
            System.Management.ManagementClass mc = new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {

                //Only get the first one
                if (result=="")
                {
                    try
                    {
                        result = mo[wmiProperty].ToString();
                        break;
                    }
                    catch
                    {
                    }
                }

            }
            return result;
        }

        private static string cpuId()
        {
            //使用优先顺序的第一个CPU标识，不要让所有的标识符，非常耗时
            string retVal = identifier("Win32_Processor", "UniqueId");
            if (retVal == "") //如果没有的UniqueID，使用ProcessorID
            {
                retVal = identifier("Win32_Processor", "ProcessorId");

                if (retVal == "") //如果没有ProcessorId，使用名称
                {
                    retVal = identifier("Win32_Processor", "Name");


                    if (retVal == "") //如果没有名称，使用制造商
                    {
                        retVal = identifier("Win32_Processor", "Manufacturer");
                    }

                    //添加额外的安全时钟速度
                    retVal +=identifier("Win32_Processor", "MaxClockSpeed");
                }
            }

            return retVal;
        }

        //BIOS标识符
        private static string biosId()
        {
            return identifier("Win32_BIOS", "Manufacturer")
                    + identifier("Win32_BIOS", "SMBIOSBIOSVersion")
                    + identifier("Win32_BIOS", "IdentificationCode")
                    + identifier("Win32_BIOS", "SerialNumber")
                    + identifier("Win32_BIOS", "ReleaseDate")
                    + identifier("Win32_BIOS", "Version");
        }

        //主要的物理硬盘驱动器ID
        private static string diskId()
        {
            return identifier("Win32_DiskDrive", "Model")
                    + identifier("Win32_DiskDrive", "Manufacturer")
                    + identifier("Win32_DiskDrive", "Signature")
                    + identifier("Win32_DiskDrive", "TotalHeads");
        }

        //主板ID
        private static string baseId()
        {
            return identifier("Win32_BaseBoard", "Model")
                    + identifier("Win32_BaseBoard", "Manufacturer")
                    + identifier("Win32_BaseBoard", "Name")
                    + identifier("Win32_BaseBoard", "SerialNumber");
        }

        //主视频控制器ID
        private static string videoId()
        {
            return identifier("Win32_VideoController", "DriverVersion")
                    + identifier("Win32_VideoController", "Name");
        }

        //首次启用的网卡ID
        private static string macId()
        {
            return identifier("Win32_NetworkAdapterConfiguration", "MACAddress", "IPEnabled");
        }

        //包装至8位数字的字符串
        private static string pack(string text)
        {
            string retVal;
            int x = 0;
            int y = 0;
            foreach (char n in text)
            {
                y++;
                x += (n*y);
            }
            retVal = x.ToString() + "00000000";

            return retVal.Substring(0, 8);
        }
    }
}
