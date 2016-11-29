using Microsoft.Win32;

namespace STO.Print.Utilities
{
    public class ExtensionAttachUtil
    {
        /// <summary>
        /// 关联文件
        /// </summary>
        /// <param name="_FilePathString">应用程序路径</param>
        /// <param name="p_FileTypeName">文件类型</param>
        public static void SaveReg(string _FilePathString, string p_FileTypeName)
        {
            RegistryKey _RegKey = Registry.ClassesRoot.OpenSubKey("", true);              //打开注册表
            RegistryKey _VRPkey = _RegKey.OpenSubKey(p_FileTypeName, true);
            if (_VRPkey != null)
            {
                _RegKey.DeleteSubKey(p_FileTypeName, true);
            }

            _RegKey.CreateSubKey(p_FileTypeName);
            _VRPkey = _RegKey.OpenSubKey(p_FileTypeName, true);
            _VRPkey.SetValue("", "Exec");
            _VRPkey = _RegKey.OpenSubKey("Exec", true);
            if (_VRPkey != null) _RegKey.DeleteSubKeyTree("Exec");         //如果等于空就删除注册表DSKJIVR

            _RegKey.CreateSubKey("Exec");
            _VRPkey = _RegKey.OpenSubKey("Exec", true);
            _VRPkey.CreateSubKey("shell");
            _VRPkey = _VRPkey.OpenSubKey("shell", true);                      //写入必须路径
            _VRPkey.CreateSubKey("open");
            _VRPkey = _VRPkey.OpenSubKey("open", true);
            _VRPkey.CreateSubKey("command");
            _VRPkey = _VRPkey.OpenSubKey("command", true);
            string _PathString = "\"" + _FilePathString + "\" \"%1\"";
            _VRPkey.SetValue("", _PathString);                                    //写入数据
        }

        /// <summary>
        /// 取消文件关联
        /// </summary>
        /// <param name="p_FileTypeName">文件类型</param>
        public static void DelReg(string p_FileTypeName)
        {
            RegistryKey _Regkey = Registry.ClassesRoot.OpenSubKey("", true);
            RegistryKey _VRPkey = _Regkey.OpenSubKey(p_FileTypeName);
            if (_VRPkey != null)
            {
                _Regkey.DeleteSubKey(p_FileTypeName, true);
            }
            if (_VRPkey != null)
            {
                _Regkey.DeleteSubKeyTree("Exec");
            }
        }
    }
}
