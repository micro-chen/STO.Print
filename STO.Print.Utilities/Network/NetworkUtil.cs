using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace STO.Print.Utilities
{
    public class NetworkUtil
    {
        /// <summary>
        /// 获取本地机器IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIP()
        {        
            string strHostIP = string.Empty;
            IPHostEntry oIPHost = Dns.GetHostEntry(Environment.MachineName);
            if (oIPHost.AddressList.Length > 0)
            {
                strHostIP = oIPHost.AddressList[0].ToString();
            }
            return strHostIP;
        }

        #region 检查设置的IP地址是否正确，返回正确的IP地址
        /// <summary>
        /// 检查设置的IP地址是否正确，并返回正确的IP地址,无效IP地址返回"-1"。
        /// </summary>
        /// <param name="ip">设置的IP地址</param>
        /// <returns>非法IP 则返回 -1 </returns>
        public static string GetValidIP(string ip)
        {
            if (ValidateUtil.IsValidIP(ip))
            {
                return ip;
            }
            else
            {
                return "-1";
            }
        }
        #endregion

        #region 检查设置的端口号是否正确，返回正确的端口号
        /// <summary>
        /// 检查设置的端口号是否正确，并返回正确的端口号,无效端口号返回-1。
        /// </summary>
        /// <param name="port">设置的端口号</param>        
        public static int GetValidPort(string port)
        {
            //声明返回的正确端口号
            int validPort = -1;

            validPort = ConvertHelper.ConvertTo<int>(port);

            //最小有效端口号
            const int MINPORT = 0;
            //最大有效端口号
            const int MAXPORT = 65535;

            if (validPort <= MINPORT || validPort > MAXPORT)
            {
                throw new ArgumentException("参数port端口号范围无效！");
            }

            return validPort;
        }
        #endregion

        #region 将字符串形式的IP地址转换成IPAddress对象
        /// <summary>
        /// 将字符串形式的IP地址转换成IPAddress对象
        /// </summary>
        /// <param name="ip">字符串形式的IP地址</param>        
        public static IPAddress StringToIPAddress(string ip)
        {
            return IPAddress.Parse(ip);
        }
        #endregion

        #region 获取本机的计算机名
        /// <summary>
        /// 获取本机的计算机名
        /// </summary>
        public static string LocalHostName
        {
            get
            {
                return Dns.GetHostName();
            }
        }
        #endregion

        #region 获取本机的局域网IP
        /// <summary>
        /// 获取本机的局域网IP
        /// </summary>        
        public static string LANIP
        {
            get
            {
                //获取本机的IP列表,IP列表中的第一项是局域网IP，第二项是广域网IP
                IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;

                //如果本机IP列表为空，则返回空字符串
                if (addressList.Length < 1)
                {
                    return "";
                }

                //返回本机的局域网IP
                return addressList[0].ToString();
            }
        }
        #endregion

        #region 获取本机在Internet网络的广域网IP
        /// <summary>
        /// 获取本机在Internet网络的广域网IP
        /// </summary>        
        public static string WANIP
        {
            get
            {
                //获取本机的IP列表,IP列表中的第一项是局域网IP，第二项是广域网IP
                IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;

                //如果本机IP列表小于2，则返回空字符串
                if (addressList.Length < 2)
                {
                    return "";
                }

                //返回本机的广域网IP
                return addressList[1].ToString();
            }
        }
        #endregion

        #region 获取远程客户机的IP地址
        /// <summary>
        /// 获取远程客户机的IP地址
        /// </summary>
        /// <param name="clientSocket">客户端的socket对象</param>        
        public static string GetClientIP(Socket clientSocket)
        {
            IPEndPoint client = (IPEndPoint)clientSocket.RemoteEndPoint;
            return client.Address.ToString();
        }
        #endregion

        #region 创建一个IPEndPoint对象
        /// <summary>
        /// 创建一个IPEndPoint对象
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <param name="port">端口号</param>        
        public static IPEndPoint CreateIPEndPoint(string ip, int port)
        {
            IPAddress ipAddress = StringToIPAddress(ip);
            return new IPEndPoint(ipAddress, port);
        }
        #endregion

        #region 创建一个TcpListener对象
        /// <summary>
        /// 创建一个自动分配IP和端口的TcpListener对象
        /// </summary>        
        public static TcpListener CreateTcpListener()
        {
            //创建一个自动分配的网络节点
            IPAddress ipAddress = IPAddress.Any;
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 0);

            return new TcpListener(localEndPoint);
        }
        /// <summary>
        /// 创建一个TcpListener对象
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <param name="port">端口</param>        
        public static TcpListener CreateTcpListener(string ip, int port)
        {
            //创建一个网络节点
            IPAddress ipAddress = StringToIPAddress(ip);
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);

            return new TcpListener(localEndPoint);
        }
        #endregion

        #region 创建一个基于TCP协议的Socket对象
        /// <summary>
        /// 创建一个基于TCP协议的Socket对象
        /// </summary>        
        public static Socket CreateTcpSocket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        #endregion

        #region 创建一个基于UDP协议的Socket对象
        /// <summary>
        /// 创建一个基于UDP协议的Socket对象
        /// </summary>        
        public static Socket CreateUdpSocket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }
        #endregion

        #region 获取本地终结点

        #region 获取TcpListener对象的本地终结点
        /// <summary>
        /// 获取TcpListener对象的本地终结点
        /// </summary>
        /// <param name="tcpListener">TcpListener对象</param>        
        public static IPEndPoint GetLocalPoint(TcpListener tcpListener)
        {
            return (IPEndPoint)tcpListener.LocalEndpoint;
        }

        /// <summary>
        /// 获取TcpListener对象的本地终结点的IP地址
        /// </summary>
        /// <param name="tcpListener">TcpListener对象</param>        
        public static string GetLocalPoint_IP(TcpListener tcpListener)
        {
            IPEndPoint localEndPoint = (IPEndPoint)tcpListener.LocalEndpoint;
            return localEndPoint.Address.ToString();
        }

        /// <summary>
        /// 获取TcpListener对象的本地终结点的端口号
        /// </summary>
        /// <param name="tcpListener">TcpListener对象</param>        
        public static int GetLocalPoint_Port(TcpListener tcpListener)
        {
            IPEndPoint localEndPoint = (IPEndPoint)tcpListener.LocalEndpoint;
            return localEndPoint.Port;
        }
        #endregion

        #region 获取Socket对象的本地终结点
        /// <summary>
        /// 获取Socket对象的本地终结点
        /// </summary>
        /// <param name="socket">Socket对象</param>        
        public static IPEndPoint GetLocalPoint(Socket socket)
        {
            return (IPEndPoint)socket.LocalEndPoint;
        }

        /// <summary>
        /// 获取Socket对象的本地终结点的IP地址
        /// </summary>
        /// <param name="socket">Socket对象</param>        
        public static string GetLocalPoint_IP(Socket socket)
        {
            IPEndPoint localEndPoint = (IPEndPoint)socket.LocalEndPoint;
            return localEndPoint.Address.ToString();
        }

        /// <summary>
        /// 获取Socket对象的本地终结点的端口号
        /// </summary>
        /// <param name="socket">Socket对象</param>        
        public static int GetLocalPoint_Port(Socket socket)
        {
            IPEndPoint localEndPoint = (IPEndPoint)socket.LocalEndPoint;
            return localEndPoint.Port;
        }
        #endregion

        #endregion

        #region 绑定终结点
        /// <summary>
        /// 绑定终结点
        /// </summary>
        /// <param name="socket">Socket对象</param>
        /// <param name="endPoint">要绑定的终结点</param>
        public static void BindEndPoint(Socket socket, IPEndPoint endPoint)
        {
            if (!socket.IsBound)
            {
                socket.Bind(endPoint);
            }
        }

        /// <summary>
        /// 绑定终结点
        /// </summary>
        /// <param name="socket">Socket对象</param>        
        /// <param name="ip">服务器IP地址</param>
        /// <param name="port">服务器端口</param>
        public static void BindEndPoint(Socket socket, string ip, int port)
        {
            //创建终结点
            IPEndPoint endPoint = CreateIPEndPoint(ip, port);

            //绑定终结点
            if (!socket.IsBound)
            {
                socket.Bind(endPoint);
            }
        }
        #endregion

        #region 指定Socket对象执行监听
        /// <summary>
        /// 指定Socket对象执行监听，默认允许的最大挂起连接数为100
        /// </summary>
        /// <param name="socket">执行监听的Socket对象</param>
        /// <param name="port">监听的端口号</param>
        public static void StartListen(Socket socket, int port)
        {
            //创建本地终结点
            IPEndPoint localPoint = CreateIPEndPoint(NetworkUtil.LocalHostName, port);

            //绑定到本地终结点
            BindEndPoint(socket, localPoint);

            //开始监听
            socket.Listen(100);
        }

        /// <summary>
        /// 指定Socket对象执行监听
        /// </summary>
        /// <param name="socket">执行监听的Socket对象</param>
        /// <param name="port">监听的端口号</param>
        /// <param name="maxConnection">允许的最大挂起连接数</param>
        public static void StartListen(Socket socket, int port, int maxConnection)
        {
            //创建本地终结点
            IPEndPoint localPoint = CreateIPEndPoint(NetworkUtil.LocalHostName, port);

            //绑定到本地终结点
            BindEndPoint(socket, localPoint);

            //开始监听
            socket.Listen(maxConnection);
        }

        /// <summary>
        /// 指定Socket对象执行监听
        /// </summary>
        /// <param name="socket">执行监听的Socket对象</param>
        /// <param name="ip">监听的IP地址</param>
        /// <param name="port">监听的端口号</param>
        /// <param name="maxConnection">允许的最大挂起连接数</param>
        public static void StartListen(Socket socket, string ip, int port, int maxConnection)
        {
            //绑定到本地终结点
            BindEndPoint(socket, ip, port);

            //开始监听
            socket.Listen(maxConnection);
        }
        #endregion

        #region 连接到基于TCP协议的服务器
        /// <summary>
        /// 连接到基于TCP协议的服务器,连接成功返回true，否则返回false
        /// </summary>
        /// <param name="socket">Socket对象</param>
        /// <param name="ip">服务器IP地址</param>
        /// <param name="port">服务器端口号</param>     
        public static bool Connect(Socket socket, string ip, int port)
        {
            //连接服务器
            socket.Connect(ip, port);

            //检测连接状态
            return socket.Poll(-1, SelectMode.SelectWrite);

        }
        #endregion

        #region 以同步方式发送消息
        /// <summary>
        /// 以同步方式向指定的Socket对象发送消息
        /// </summary>
        /// <param name="socket">socket对象</param>
        /// <param name="msg">发送的消息</param>
        public static void SendMsg(Socket socket, byte[] msg)
        {
            //发送消息
            socket.Send(msg, msg.Length, SocketFlags.None);
        }

        /// <summary>
        /// 使用UTF8编码格式以同步方式向指定的Socket对象发送消息
        /// </summary>
        /// <param name="socket">socket对象</param>
        /// <param name="msg">发送的消息</param>
        public static void SendMsg(Socket socket, string msg)
        {
            //将字符串消息转换成字符数组
            byte[] buffer = ConvertHelper.StringToBytes(msg);

            //发送消息
            socket.Send(buffer, buffer.Length, SocketFlags.None);
        }
        #endregion

        #region 以同步方式接收消息
        /// <summary>
        /// 以同步方式接收消息
        /// </summary>
        /// <param name="socket">socket对象</param>
        /// <param name="buffer">接收消息的缓冲区</param>
        public static void ReceiveMsg(Socket socket, byte[] buffer)
        {
            socket.Receive(buffer);
        }

        /// <summary>
        /// 以同步方式接收消息，并转换为UTF8编码格式的字符串,使用5000字节的默认缓冲区接收。
        /// </summary>
        /// <param name="socket">socket对象</param>        
        public static string ReceiveMsg(Socket socket)
        {
            //定义接收缓冲区
            byte[] buffer = new byte[5000];
            //接收数据，获取接收到的字节数
            int receiveCount = socket.Receive(buffer);

            //定义临时缓冲区
            byte[] tempBuffer = new byte[receiveCount];
            //将接收到的数据写入临时缓冲区
            Buffer.BlockCopy(buffer, 0, tempBuffer, 0, receiveCount);
            //转换成字符串，并将其返回
            return ConvertHelper.BytesToString(tempBuffer);
        }
        #endregion

        #region 关闭基于Tcp协议的Socket对象
        /// <summary>
        /// 关闭基于Tcp协议的Socket对象
        /// </summary>
        /// <param name="socket">要关闭的Socket对象</param>
        public static void Close(Socket socket)
        {
            try
            {
                //禁止Socket对象接收和发送数据
                socket.Shutdown(SocketShutdown.Both);
            }
            catch (SocketException ex)
            {
                throw ex;
            }
            finally
            {
                //关闭Socket对象
                socket.Close();
            }
        }
        #endregion

        #region 检测本机是否联网（互联网）

        [DllImport("wininet")]
        private extern static bool InternetGetConnectedState(out int connectionDescription, int reservedValue);


        /// <summary>
        /// 检测本机是否联网
        /// </summary>
        /// <returns></returns>
        public static bool IsConnectedInternet()
        {
            int i = 0;
            if (InternetGetConnectedState(out i, 0))
            {
                //已联网
                return true;
            }
            else
            {
                //未联网
                return false;
            }

        }

        #endregion
    }
}
