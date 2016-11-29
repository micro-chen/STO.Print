//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Net.NetworkInformation;
using System.Threading;

namespace STO.Print
{
    using DevExpress.XtraSplashScreen;
    using DotNet.Utilities;
    using Utilities;

    /// <summary>
    /// 加载窗体
    /// 修改纪录
    ///
    ///		2015-08-01  版本：1.0 YangHengLian 创建主键，注意命名空间的排序。
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-08-01</date>
    /// </author>
    /// </summary>
    public partial class FrmWaiting : SplashScreen
    {
        /// <summary>
        /// 刷新窗体信息委托
        /// </summary>
        private delegate void RefreshDelegate();

        private readonly System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();

        /// <summary>
        /// 同步状态字符串
        /// </summary>
        private string _syncStatus = string.Empty;

        public FrmWaiting()
        {
            InitializeComponent();
            // 显示置顶
            this.TopMost = true;
            _timer.Interval = 100;
            _timer.Tick += timer_Tick;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                RefreshDelegate refresh = Refresh;
                Invoke(refresh);
                if (BaseSystemInfo.Synchronized)
                {
                    _timer.Stop();
                    BillPrintHelper.SetSyncTime();
                    Close();
                }
            }
            catch (Exception exception)
            {
                _timer.Stop();
                LogUtil.WriteException(exception);
                Close();
            }
        }

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        public enum SplashScreenCommand
        {

        }

        /// <summary>
        /// 窗体显示事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmWaiting_Shown(object sender, EventArgs e)
        {
            try
            {
                BaseSystemInfo.OnInternet = NetworkHelper.IsConnectedInternet();
                if (BaseSystemInfo.OnInternet)
                {
                    var syncTime = BillPrintHelper.GetSyncTime();
                    if (string.IsNullOrEmpty(syncTime))
                    {
                        SyncEvent();
                    }
                    else
                    {
                        // 距离上次同步过去7天再次同步一次
                        if ((DateTime.Now - Convert.ToDateTime(syncTime)).TotalDays > 7)
                        {
                            SyncEvent();
                        }
                        else
                        {
                            Close();
                        }
                    }
                }
                else
                {
                    // 都没有连接网络，就不要同步了，直接关闭窗体了
                    Close();
                }
            }
            catch (Exception ex)
            {
                LogUtil.WriteException(ex);
                Close();
            }
        }

        private void Refresh()
        {
            lblLoadingInfo.Text = _syncStatus;
        }

        /// <summary>
        /// 同步数据
        /// </summary>
        private void SyncEvent()
        {
            lblLoadingInfo.Text = "正在同步基础数据";
            _timer.Start();
            ThreadPool.QueueUserWorkItem(delegate
            {
                _syncStatus = "省市区基础资料同步开始";
                var result = Synchronous.Synchronous.SynchronousArea(true);
                _syncStatus = "省市区基础资料同步结束，同步总记录为：" + result;
                Thread.Sleep(300);

                _syncStatus = "大头笔基础资料同步开始";
                result = Synchronous.Synchronous.SynchronousPrintMark(true);
                _syncStatus = "大头笔基础资料同步结束，同步总记录为：" + result;
                Thread.Sleep(300);
                _syncStatus = "同步完成";
                Thread.Sleep(300);
                _syncStatus = "加载成功";
                Thread.Sleep(2000);
                BaseSystemInfo.Synchronized = true;
            });
        }

        public void NetworkChangeNetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            if (!e.IsAvailable)
            {
                BaseSystemInfo.OnInternet = false;
            }
            else
            {
                if (e.IsAvailable)
                {
                    BaseSystemInfo.OnInternet = true;
                }
                else
                {
                    BaseSystemInfo.OnInternet = false;
                }
            }
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmWaiting_Load(object sender, EventArgs e)
        {
            // 注册网络连接状态监听事件（网络是否连接）
            // NetworkChange.NetworkAvailabilityChanged += NetworkChangeNetworkAvailabilityChanged;
        }
    }
}
