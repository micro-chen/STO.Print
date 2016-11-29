using System;
using System.IO;
using System.Runtime.InteropServices;

namespace STO.Print.Utilities
{
    public class POSPrinter
    {
        const int OPEN_EXISTING = 3;
        string prnPort = "LPT1";

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr CreateFile(string lpFileName,
        int dwDesiredAccess,
        int dwShareMode,
        int lpSecurityAttributes,
        int dwCreationDisposition,
        int dwFlagsAndAttributes,
        int hTemplateFile);

        public POSPrinter()
        {
        }
        public POSPrinter(string prnPort)
        {
            this.prnPort = prnPort;//打印机端口 
        }
        public string PrintLine(string str)
        {
            try
            {
                IntPtr iHandle = CreateFile(prnPort, 0x40000000, 0, 0, OPEN_EXISTING, 0, 0);
                if (iHandle.ToInt32() == -1)
                {
                    return "没有连接打印机或者打印机端口不是LPT1";
                }
                else
                {
                    using (FileStream fs = new FileStream(iHandle, FileAccess.ReadWrite))
                    {
                        using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default)) //写数据 
                        {
                            sw.WriteLine(str);
                            //开钱箱 
                            //sw.WriteLine(Chr(&H1B) & Chr(70) & Chr(0) & Chr(20) & Chr(&HA0)) 
                        }
                    }
                    return "";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }

}