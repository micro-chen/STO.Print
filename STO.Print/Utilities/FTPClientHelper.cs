//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace STO.Print.Utilities
{
    /// <summary>
    /// FTP操作帮助类
    ///
    /// 修改纪录
    ///
    ///		  2016-4-4  版本：1.0 YangHengLian 创建主键，注意命名空间的排序，测试非常好。
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2016-4-4</date>
    /// </author>
    /// </summary>
    public class FTPClientHelper
    {
        public static object Obj = new object();

        #region 构造函数
        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public FTPClientHelper()
        {
            RemoteHost = "";
            _strRemotePath = "";
            _strRemoteUser = "";
            _strRemotePass = "";
            _strRemotePort = 21;
            _bConnected = false;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public FTPClientHelper(string remoteHost, string remotePath, string remoteUser, string remotePass, int remotePort)
        {
            RemoteHost = remoteHost;
            _strRemotePath = remotePath;
            _strRemoteUser = remoteUser;
            _strRemotePass = remotePass;
            _strRemotePort = remotePort;
            Connect();
        }
        #endregion

        #region 字段
        private int _strRemotePort;
        private Boolean _bConnected;
        private string _strRemotePass;
        private string _strRemoteUser;
        private string _strRemotePath;

        /// <summary>
        /// 服务器返回的应答信息(包含应答码)
        /// </summary>
        private string _strMsg;
        /// <summary>
        /// 服务器返回的应答信息(包含应答码)
        /// </summary>
        private string _strReply;
        /// <summary>
        /// 服务器返回的应答码
        /// </summary>
        private int _iReplyCode;
        /// <summary>
        /// 进行控制连接的socket
        /// </summary>
        private Socket _socketControl;
        /// <summary>
        /// 传输模式
        /// </summary>
        private TransferType _trType;

        /// <summary>
        /// 接收和发送数据的缓冲区
        /// </summary>
        private const int BlockSize = 512;

        /// <summary>
        /// 编码方式
        /// </summary>
        readonly Encoding _ascii = Encoding.ASCII;
        /// <summary>
        /// 字节数组
        /// </summary>
        readonly Byte[] _buffer = new Byte[BlockSize];
        #endregion

        #region 属性

        /// <summary>
        /// FTP服务器IP地址
        /// </summary>
        public string RemoteHost { get; set; }

        /// <summary>
        /// FTP服务器端口
        /// </summary>
        public int RemotePort
        {
            get
            {
                return _strRemotePort;
            }
            set
            {
                _strRemotePort = value;
            }
        }

        /// <summary>
        /// 当前服务器目录
        /// </summary>
        public string RemotePath
        {
            get
            {
                return _strRemotePath;
            }
            set
            {
                _strRemotePath = value;
            }
        }

        /// <summary>
        /// 登录用户账号
        /// </summary>
        public string RemoteUser
        {
            set
            {
                _strRemoteUser = value;
            }
        }

        /// <summary>
        /// 用户登录密码
        /// </summary>
        public string RemotePass
        {
            set
            {
                _strRemotePass = value;
            }
        }

        /// <summary>
        /// 是否登录
        /// </summary>
        public bool Connected
        {
            get
            {
                return _bConnected;
            }
        }
        #endregion

        #region 链接
        /// <summary>
        /// 建立连接 
        /// </summary>
        public void Connect()
        {
            lock (Obj)
            {
                _socketControl = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                var ep = new IPEndPoint(IPAddress.Parse(RemoteHost), _strRemotePort);
                try
                {
                    _socketControl.Connect(ep);
                }
                catch (Exception)
                {
                    throw new IOException("不能连接ftp服务器");
                }
            }
            ReadReply();
            if (_iReplyCode != 220)
            {
                DisConnect();
                throw new IOException(_strReply.Substring(4));
            }
            SendCommand("USER " + _strRemoteUser);
            if (!(_iReplyCode == 331 || _iReplyCode == 230))
            {
                CloseSocketConnect();
                throw new IOException(_strReply.Substring(4));
            }
            if (_iReplyCode != 230)
            {
                SendCommand("PASS " + _strRemotePass);
                if (!(_iReplyCode == 230 || _iReplyCode == 202))
                {
                    CloseSocketConnect();
                    throw new IOException(_strReply.Substring(4));
                }
            }
            _bConnected = true;
            ChDir(_strRemotePath);
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void DisConnect()
        {
            if (_socketControl != null)
            {
                SendCommand("QUIT");
            }
            CloseSocketConnect();
        }
        #endregion

        #region 传输模式
        /// <summary>
        /// 传输模式:二进制类型、ASCII类型
        /// </summary>
        public enum TransferType { Binary, ASCII };

        /// <summary>
        /// 设置传输模式
        /// </summary>
        /// <param name="ttType">传输模式</param>
        public void SetTransferType(TransferType ttType)
        {
            SendCommand(ttType == TransferType.Binary ? "TYPE I" : "TYPE A");
            if (_iReplyCode != 200)
            {
                throw new IOException(_strReply.Substring(4));
            }
            _trType = ttType;
        }

        /// <summary>
        /// 获得传输模式
        /// </summary>
        /// <returns>传输模式</returns>
        public TransferType GetTransferType()
        {
            return _trType;
        }
        #endregion

        #region 文件操作
        /// <summary>
        /// 获得文件列表
        /// </summary>
        /// <param name="strMask">文件名的匹配字符串</param>
        public string[] Dir(string strMask)
        {
            if (!_bConnected)
            {
                Connect();
            }
            Socket socketData = CreateDataSocket();
            SendCommand("NLST " + strMask);
            if (!(_iReplyCode == 150 || _iReplyCode == 125 || _iReplyCode == 226))
            {
                throw new IOException(_strReply.Substring(4));
            }
            _strMsg = "";
            Thread.Sleep(2000);
            while (true)
            {
                int iBytes = socketData.Receive(_buffer, _buffer.Length, 0);
                _strMsg += _ascii.GetString(_buffer, 0, iBytes);
                if (iBytes < _buffer.Length)
                {
                    break;
                }
            }
            char[] seperator = { '\n' };
            string[] strsFileList = _strMsg.Split(seperator);
            socketData.Close(); //数据socket关闭时也会有返回码
            if (_iReplyCode != 226)
            {
                ReadReply();
                if (_iReplyCode != 226)
                {

                    throw new IOException(_strReply.Substring(4));
                }
            }
            return strsFileList;
        }

        public void NewPutByGuid(string strFileName, string strGuid)
        {
            if (!_bConnected)
            {
                Connect();
            }
            string str = strFileName.Substring(0, strFileName.LastIndexOf("\\", StringComparison.Ordinal));
            string strTypeName = strFileName.Substring(strFileName.LastIndexOf(".", StringComparison.Ordinal));
            strGuid = str + "\\" + strGuid;
            Socket socketData = CreateDataSocket();
            SendCommand("STOR " + Path.GetFileName(strGuid));
            if (!(_iReplyCode == 125 || _iReplyCode == 150))
            {
                throw new IOException(_strReply.Substring(4));
            }
            var input = new FileStream(strGuid, FileMode.Open);
            input.Flush();
            int iBytes;
            while ((iBytes = input.Read(_buffer, 0, _buffer.Length)) > 0)
            {
                socketData.Send(_buffer, iBytes, 0);
            }
            input.Close();
            if (socketData.Connected)
            {
                socketData.Close();
            }
            if (!(_iReplyCode == 226 || _iReplyCode == 250))
            {
                ReadReply();
                if (!(_iReplyCode == 226 || _iReplyCode == 250))
                {
                    throw new IOException(_strReply.Substring(4));
                }
            }
        }

        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="strFileName">文件名</param>
        /// <returns>文件大小</returns>
        public long GetFileSize(string strFileName)
        {
            if (!_bConnected)
            {
                Connect();
            }
            SendCommand("SIZE " + Path.GetFileName(strFileName));
            long lSize;
            if (_iReplyCode == 213)
            {
                lSize = Int64.Parse(_strReply.Substring(4));
            }
            else
            {
                throw new IOException(_strReply.Substring(4));
            }
            return lSize;
        }


        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="strFileName">文件名</param>
        /// <returns>文件大小</returns>
        public string GetFileInfo(string strFileName)
        {
            if (!_bConnected)
            {
                Connect();
            }
            Socket socketData = CreateDataSocket();
            SendCommand("LIST " + strFileName);
            if (!(_iReplyCode == 150 || _iReplyCode == 125
                || _iReplyCode == 226 || _iReplyCode == 250))
            {
                throw new IOException(_strReply.Substring(4));
            }
            byte[] b = new byte[512];
            MemoryStream ms = new MemoryStream();

            while (true)
            {
                int iBytes = socketData.Receive(b, b.Length, 0);
                ms.Write(b, 0, iBytes);
                if (iBytes <= 0)
                {

                    break;
                }
            }
            byte[] bt = ms.GetBuffer();
            string strResult = Encoding.ASCII.GetString(bt);
            ms.Close();
            return strResult;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="strFileName">待删除文件名</param>
        public void Delete(string strFileName)
        {
            if (!_bConnected)
            {
                Connect();
            }
            SendCommand("DELE " + strFileName);
            if (_iReplyCode != 250)
            {
                throw new IOException(_strReply.Substring(4));
            }
        }

        /// <summary>
        /// 重命名(如果新文件名与已有文件重名,将覆盖已有文件)
        /// </summary>
        /// <param name="strOldFileName">旧文件名</param>
        /// <param name="strNewFileName">新文件名</param>
        public void Rename(string strOldFileName, string strNewFileName)
        {
            if (!_bConnected)
            {
                Connect();
            }
            SendCommand("RNFR " + strOldFileName);
            if (_iReplyCode != 350)
            {
                throw new IOException(_strReply.Substring(4));
            }
            //  如果新文件名与原有文件重名,将覆盖原有文件
            SendCommand("RNTO " + strNewFileName);
            if (_iReplyCode != 250)
            {
                throw new IOException(_strReply.Substring(4));
            }
        }
        #endregion

        #region 上传和下载
        /// <summary>
        /// 下载一批文件
        /// </summary>
        /// <param name="strFileNameMask">文件名的匹配字符串</param>
        /// <param name="strFolder">本地目录(不得以\结束)</param>
        public void Get(string strFileNameMask, string strFolder)
        {
            if (!_bConnected)
            {
                Connect();
            }
            string[] strFiles = Dir(strFileNameMask);
            foreach (string strFile in strFiles)
            {
                if (!strFile.Equals(""))//一般来说strFiles的最后一个元素可能是空字符串
                {
                    Get(strFile, strFolder, strFile);
                }
            }
        }

        /// <summary>
        /// 下载一个文件
        /// </summary>
        /// <param name="strRemoteFileName">要下载的文件名</param>
        /// <param name="strFolder">本地目录(不得以\结束)</param>
        /// <param name="strLocalFileName">保存在本地时的文件名</param>
        public void Get(string strRemoteFileName, string strFolder, string strLocalFileName)
        {
            Socket socketData = CreateDataSocket();
            try
            {
                if (!_bConnected)
                {
                    Connect();
                }
                SetTransferType(TransferType.Binary);
                if (strLocalFileName.Equals(""))
                {
                    strLocalFileName = strRemoteFileName;
                }
                SendCommand("RETR " + strRemoteFileName);
                if (!(_iReplyCode == 150 || _iReplyCode == 125 || _iReplyCode == 226 || _iReplyCode == 250))
                {
                    throw new IOException(_strReply.Substring(4));
                }
                var output = new FileStream(strFolder + "\\" + strLocalFileName, FileMode.Create);
                while (true)
                {
                    int iBytes = socketData.Receive(_buffer, _buffer.Length, 0);
                    output.Write(_buffer, 0, iBytes);
                    if (iBytes <= 0)
                    {
                        break;
                    }
                }
                output.Close();
                if (socketData.Connected)
                {
                    socketData.Close();
                }
                if (!(_iReplyCode == 226 || _iReplyCode == 250))
                {
                    ReadReply();
                    if (!(_iReplyCode == 226 || _iReplyCode == 250))
                    {
                        throw new IOException(_strReply.Substring(4));
                    }
                }
            }
            catch
            {
                socketData.Close();
                _socketControl.Close();
                _bConnected = false;
                _socketControl = null;
            }
        }

        /// <summary>
        /// 下载一个文件
        /// </summary>
        /// <param name="strRemoteFileName">要下载的文件名</param>
        /// <param name="strFolder">本地目录(不得以\结束)</param>
        /// <param name="strLocalFileName">保存在本地时的文件名</param>
        public void GetNoBinary(string strRemoteFileName, string strFolder, string strLocalFileName)
        {
            if (!_bConnected)
            {
                Connect();
            }

            if (strLocalFileName.Equals(""))
            {
                strLocalFileName = strRemoteFileName;
            }
            Socket socketData = CreateDataSocket();
            SendCommand("RETR " + strRemoteFileName);
            if (!(_iReplyCode == 150 || _iReplyCode == 125 || _iReplyCode == 226 || _iReplyCode == 250))
            {
                throw new IOException(_strReply.Substring(4));
            }
            var output = new FileStream(strFolder + "\\" + strLocalFileName, FileMode.Create);
            while (true)
            {
                int iBytes = socketData.Receive(_buffer, _buffer.Length, 0);
                output.Write(_buffer, 0, iBytes);
                if (iBytes <= 0)
                {
                    break;
                }
            }
            output.Close();
            if (socketData.Connected)
            {
                socketData.Close();
            }
            if (!(_iReplyCode == 226 || _iReplyCode == 250))
            {
                ReadReply();
                if (!(_iReplyCode == 226 || _iReplyCode == 250))
                {
                    throw new IOException(_strReply.Substring(4));
                }
            }
        }

        /// <summary>
        /// 上传一批文件
        /// </summary>
        /// <param name="strFolder">本地目录(不得以\结束)</param>
        /// <param name="strFileNameMask">文件名匹配字符(可以包含*和?)</param>
        public void Put(string strFolder, string strFileNameMask)
        {
            string[] strFiles = Directory.GetFiles(strFolder, strFileNameMask);
            foreach (string strFile in strFiles)
            {
                Put(strFile);
            }
        }

        /// <summary>
        /// 上传一个文件
        /// </summary>
        /// <param name="strFileName">本地文件名</param>
        public void Put(string strFileName)
        {
            if (!_bConnected)
            {
                Connect();
            }
            Socket socketData = CreateDataSocket();
            if (Path.GetExtension(strFileName) == "")
                SendCommand("STOR " + Path.GetFileNameWithoutExtension(strFileName));
            else
                SendCommand("STOR " + Path.GetFileName(strFileName));

            if (!(_iReplyCode == 125 || _iReplyCode == 150))
            {
                throw new IOException(_strReply.Substring(4));
            }

            var input = new FileStream(strFileName, FileMode.Open);
            int iBytes;
            while ((iBytes = input.Read(_buffer, 0, _buffer.Length)) > 0)
            {
                socketData.Send(_buffer, iBytes, 0);
            }
            input.Close();
            if (socketData.Connected)
            {
                socketData.Close();
            }
            if (!(_iReplyCode == 226 || _iReplyCode == 250))
            {
                ReadReply();
                if (!(_iReplyCode == 226 || _iReplyCode == 250))
                {
                    throw new IOException(_strReply.Substring(4));
                }
            }
        }


        /// <summary>
        /// 上传一个文件
        /// </summary>
        /// <param name="strFileName">本地文件名</param>
        /// <param name="strGuid"> </param>
        public void PutByGuid(string strFileName, string strGuid)
        {
            if (!_bConnected)
            {
                Connect();
            }
            string str = strFileName.Substring(0, strFileName.LastIndexOf("\\", StringComparison.Ordinal));
            string strTypeName = strFileName.Substring(strFileName.LastIndexOf(".", System.StringComparison.Ordinal));
            strGuid = str + "\\" + strGuid;
            File.Copy(strFileName, strGuid);
            File.SetAttributes(strGuid, FileAttributes.Normal);
            Socket socketData = CreateDataSocket();
            SendCommand("STOR " + Path.GetFileName(strGuid));
            if (!(_iReplyCode == 125 || _iReplyCode == 150))
            {
                throw new IOException(_strReply.Substring(4));
            }
            var input = new FileStream(strGuid, FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
            int iBytes = 0;
            while ((iBytes = input.Read(_buffer, 0, _buffer.Length)) > 0)
            {
                socketData.Send(_buffer, iBytes, 0);
            }
            input.Close();
            File.Delete(strGuid);
            if (socketData.Connected)
            {
                socketData.Close();
            }
            if (!(_iReplyCode == 226 || _iReplyCode == 250))
            {
                ReadReply();
                if (!(_iReplyCode == 226 || _iReplyCode == 250))
                {
                    throw new IOException(_strReply.Substring(4));
                }
            }
        }
        #endregion

        #region 目录操作
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="strDirName">目录名</param>
        public void MkDir(string strDirName)
        {
            if (!_bConnected)
            {
                Connect();
            }
            SendCommand("MKD " + strDirName);
            if (_iReplyCode != 257)
            {
                throw new IOException(_strReply.Substring(4));
            }
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="strDirName">目录名</param>
        public void RmDir(string strDirName)
        {
            if (!_bConnected)
            {
                Connect();
            }
            SendCommand("RMD " + strDirName);
            if (_iReplyCode != 250)
            {
                throw new IOException(_strReply.Substring(4));
            }
        }

        /// <summary>
        /// 改变目录
        /// </summary>
        /// <param name="strDirName">新的工作目录名</param>
        public void ChDir(string strDirName)
        {
            if (strDirName.Equals(".") || strDirName.Equals(""))
            {
                return;
            }
            if (!_bConnected)
            {
                Connect();
            }
            SendCommand("CWD " + strDirName);
            if (_iReplyCode != 250)
            {
                throw new IOException(_strReply.Substring(4));
            }
            this._strRemotePath = strDirName;
        }
        #endregion

        #region 内部函数
        /// <summary>
        /// 将一行应答字符串记录在strReply和strMsg,应答码记录在iReplyCode
        /// </summary>
        private void ReadReply()
        {
            _strMsg = "";
            _strReply = ReadLine();
            _iReplyCode = Int32.Parse(_strReply.Substring(0, 3));
        }

        /// <summary>
        /// 建立进行数据连接的socket
        /// </summary>
        /// <returns>数据连接socket</returns>
        private Socket CreateDataSocket()
        {
            SendCommand("PASV");
            if (_iReplyCode != 227)
            {
                throw new IOException(_strReply.Substring(4));
            }
            int index1 = _strReply.IndexOf('(');
            int index2 = _strReply.IndexOf(')');
            string ipData = _strReply.Substring(index1 + 1, index2 - index1 - 1);
            int[] parts = new int[6];
            int len = ipData.Length;
            int partCount = 0;
            string buf = "";
            for (int i = 0; i < len && partCount <= 6; i++)
            {
                char ch = Char.Parse(ipData.Substring(i, 1));
                if (Char.IsDigit(ch))
                    buf += ch;
                else if (ch != ',')
                {
                    throw new IOException("Malformed PASV strReply: " + _strReply);
                }
                if (ch == ',' || i + 1 == len)
                {
                    try
                    {
                        parts[partCount++] = Int32.Parse(buf);
                        buf = "";
                    }
                    catch (Exception)
                    {
                        throw new IOException("Malformed PASV strReply: " + _strReply);
                    }
                }
            }
            string ipAddress = parts[0] + "." + parts[1] + "." + parts[2] + "." + parts[3];
            int port = (parts[4] << 8) + parts[5];
            var s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var ep = new IPEndPoint(IPAddress.Parse(ipAddress), port);
            try
            {
                s.Connect(ep);
            }
            catch (Exception)
            {
                throw new IOException("无法连接ftp服务器");
            }
            return s;
        }

        /// <summary>
        /// 关闭socket连接(用于登录以前)
        /// </summary>
        private void CloseSocketConnect()
        {
            lock (Obj)
            {
                if (_socketControl != null)
                {
                    _socketControl.Close();
                    _socketControl = null;
                }
                _bConnected = false;
            }
        }

        /// <summary>
        /// 读取Socket返回的所有字符串
        /// </summary>
        /// <returns>包含应答码的字符串行</returns>
        private string ReadLine()
        {
            lock (Obj)
            {
                while (true)
                {
                    int iBytes = _socketControl.Receive(_buffer, _buffer.Length, 0);
                    _strMsg += _ascii.GetString(_buffer, 0, iBytes);
                    if (iBytes < _buffer.Length)
                    {
                        break;
                    }
                }
            }
            char[] seperator = { '\n' };
            string[] mess = _strMsg.Split(seperator);
            if (_strMsg.Length > 2)
            {
                _strMsg = mess[mess.Length - 2];
            }
            else
            {
                _strMsg = mess[0];
            }
            if (!_strMsg.Substring(3, 1).Equals(" ")) //返回字符串正确的是以应答码(如220开头,后面接一空格,再接问候字符串)
            {
                return ReadLine();
            }
            return _strMsg;
        }

        /// <summary>
        /// 发送命令并获取应答码和最后一行应答字符串
        /// </summary>
        /// <param name="strCommand">命令</param>
        public void SendCommand(String strCommand)
        {
            lock (Obj)
            {
                Byte[] cmdBytes = Encoding.ASCII.GetBytes((strCommand + "\r\n").ToCharArray());
                _socketControl.Send(cmdBytes, cmdBytes.Length, 0);
                Thread.Sleep(500);
                ReadReply();
            }
        }
        #endregion
    }
}