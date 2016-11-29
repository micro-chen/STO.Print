
namespace OrderSDK
{
    /// <summary>
    /// 设置类
    /// </summary>
    public static class Config
    {
        private static int _timeOut = 5000 * 12;

        public static int TimeOut
        {
            get
            {
                return _timeOut;
            }
            set { _timeOut = value; }
        }

    }
}
