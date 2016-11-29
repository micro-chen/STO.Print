//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , ZTO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace STO.Print
{
    using AddBillForm;
    using DevExpress.XtraBars.Helpers;
    using DevExpress.XtraEditors;
    using DotNet.Business;
    using DotNet.Model;
    using DotNet.ScreenShot;
    using DotNet.Utilities;
    using Manager;
    using Model;
    using Synchronous;
    using Utilities;

    /// <summary>
    /// 打印主窗体
    /// 
    /// 修改纪录
    ///
    ///		2015-07-17  版本：1.0 YangHengLian 创建主键，注意命名空间的排序。
    ///     2015-07-20  添加注释等信息，把默认打印机设置成功
    ///     2015-08-01  添加加载窗体，方便基础数据同步等，更专业
    ///     2015-09-02  增加了大头笔查询窗体，暂时只能查询申通的大头笔
    ///     2015-09-03  今天是大阅兵，增加了定时检查更新的功能，这样及时让用户知道系统已经有了新版本
    ///     2015-09-23  增加申通内部面单打印模板，暂时是总部的电子商务部使用的，打印效果可以，位置没有便宜
    ///     2015-10-24  今天是程序员节,增加了二维码识别的小工具,后面可以有所用途
    ///     2015-11-21  增加表格调节字体的小功能
    ///     2016-01-18  支持了电子面单的打印，效果反向还是不错的，新年升级一下
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-07-17</date>
    /// </author>
    /// </summary>
    public partial class FrmMain : BaseForm
    {

        public FrmMain()
        {
            InitializeComponent();
            notifyIcon.Text = BaseSystemInfo.SoftFullName;
        }

        #region private void MainForm_Load(object sender, EventArgs e) 窗体加载事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFormLoad(object sender, EventArgs e)
        {
            try
            {
                ribbonControl1.Minimized = true;
                // 加载一些默认皮肤
                SkinHelper.InitSkinGallery(rbSkin, true);
                // ChildFormManagementHelper.Navigate(this, "http://yd.zt-express.com/Help/Index2", "帮助");
                ChildFormManagementHelper.LoadMdiForm(this, "FrmPrintData");
                // ChildFormManagementHelper.Navigate(this, "http://zto.com", "申通官网");
                // radialMenu1.ShowPopup(Control.MousePosition, true);
                //ChildFormManagement.LoadMdiForm(this, "FrmSendManData");
                //ChildFormManagement.LoadMdiForm(this, "FrmReceiveManData");
                if (xtraTabbedMdiManager1.Pages.Count > 1)
                {
                    xtraTabbedMdiManager1.SelectedPage = xtraTabbedMdiManager1.Pages[1];
                }

                #region 底部一些基本信息绑定
                barItemsUser.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barItemWeather.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                barItemWelcome.Caption = string.Format("欢迎使用{1}-当前版本：{0}", version, BaseSystemInfo.SoftFullName);
                // 得到数据库的版本
                BaseParameterManager parameterManager = new BaseParameterManager(BillPrintHelper.DbHelper, BaseSystemInfo.UserInfo, BaseParameterEntity.TableName);
                var synchronous = parameterManager.GetParameter("Bill", "DBVersion", "Synchronous");
                if (!string.IsNullOrEmpty(synchronous))
                {
                    barItemWelcome.Caption += " 主库版本：" + synchronous;
                }
                this.Text = string.Format("{0}-当前版本：{1}", this.Text, version);
                barItemTime.Caption = string.Format("登录时间：{0}  {1}", DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat), DateUtil.GetDayOfWeek(DateTime.Now.DayOfWeek.ToString(), true) + " " + DateHelper.GetChineseDateTime(DateTime.Now));

                #endregion

                var userManager = new ZtoUserManager(BillPrintHelper.DbHelper);
                var userList = userManager.GetList<ZtoUserEntity>(new KeyValuePair<string, object>(ZtoUserEntity.FieldIsDefault, 1), new KeyValuePair<string, object>(ZtoUserEntity.FieldIssendorreceive, 1));
                if (!userList.Any())
                {
                    if (XtraMessageBox.Show(@"未添加默认发件人信息，请添加默认发件人信息，有利于提取申通大头笔", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        var addSendMan = new FrmAddSendMan();
                        addSendMan.ShowDialog();
                        addSendMan.Dispose();
                    }
                }
                timerUpdate.Start();
                //FrmChatMessage chatMessage = new FrmChatMessage();
                //chatMessage.Show();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        #endregion

        #region private void barBtnDesign_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 设计模板
        /// <summary>
        /// 设计模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarBtnDesignItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChildFormManagementHelper.LoadMdiForm(this, "FrmDesigner");
        }
        #endregion

        #region private void btnQQ_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) QQ客服1
        /// <summary>
        /// QQ客服1【杨恒连的qq】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnQqItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            QQHelper.ChatQQ();
        }
        #endregion

        #region private void btnSinaWeiBo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 分享新浪微博
        /// <summary>
        /// 分享新浪微博
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSinaWeiBoItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ToolHelper.OpenBrowserUrl("http://service.weibo.com/share/share.php?title=我刚在申通打印专家上进行普通面单和电子面单打印，你们也来试试，下载连接：http://www.zto.com/GuestService/Print&ue=utf8&keyfrom=web.");
        }
        #endregion

        #region private void btnDownloadBill_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 下载普通打印面单模板
        /// <summary>
        /// 下载普通打印面单模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDownloadBillItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BillPrintHelper.DownloadPrintTemplate("ZTOBill");
        }
        #endregion

        #region private void btnDownloadTaoBaoBill_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 下载淘宝专用面单打印模板
        /// <summary>
        /// 下载淘宝专用面单打印模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDownloadTaoBaoBillItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BillPrintHelper.DownloadPrintTemplate("ZTOTaoBaoBill");
        }
        #endregion

        #region private void btnDownloadCODBill_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 下载COD专用面单打印模板

        /// <summary>
        /// 下载COD专用面单打印模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDownloadCodBillItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BillPrintHelper.DownloadPrintTemplate("ZTOCODBill");
        }
        #endregion

        #region private void btnDownloadToPayMentBill_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 下载到付专用面单打印模板
        /// <summary>
        /// 下载到付专用面单打印模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDownloadToPayMentBillItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BillPrintHelper.DownloadPrintTemplate("ZTOToPayMentBill");
        }

        #endregion

        #region private void btnSetDefaultTemplate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 设置默认打印模板
        /// <summary>
        /// 设置默认打印模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSetDefaultTemplateItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChildFormManagementHelper.LoadMdiForm(this, "FrmTemplateSetting");
        }
        #endregion

        #region private void btnZTO_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 申通官网
        /// <summary>
        /// 申通官网
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnZtoItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ToolHelper.OpenBrowserUrl("http://www.zto.com");
        }
        #endregion

        #region private void btnZtoSinaWeiBo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 申通新浪微博
        /// <summary>
        /// 申通新浪微博
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void BtnZtoSinaWeiBoItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ToolHelper.OpenBrowserUrl("http://weibo.com/zto200258");
            ToolHelper.OpenBrowserUrl("http://weibo.com/u/2292206260");
        }
        #endregion

        #region private void btnAbout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 关于软件
        /// <summary>
        /// 关于软件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAboutItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new FrmAboutThis().ShowDialog();
        }
        #endregion

        #region private void btnFeedBack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 用户反馈
        /// <summary>
        /// 用户反馈
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFeedBackItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new FrmFeedBack().ShowDialog();
        }
        #endregion

        #region private void btnCheckUpdate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 软件升级
        /// <summary>
        /// 软件升级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCheckUpdateItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateHelper.ForcedUpdate();
        }
        #endregion

        #region private void FrmMain_FormClosing(object sender, FormClosingEventArgs e) 窗体关闭
        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMainFormClosing(object sender, FormClosingEventArgs e)
        {
            if (XtraMessageBox.Show(@"确定退出吗？", AppMessage.MSG0000, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                //不执行操作
                e.Cancel = true;
            }
            else
            {
                //关闭窗体
                e.Cancel = false;
            }
        }
        #endregion

        #region private void btnDownloadMainBill_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 下载主面单
        /// <summary>
        /// 下载主面单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDownloadMainBillItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BillPrintHelper.DownloadPrintTemplate("ZTOMainBill");
        }
        #endregion

        #region private void btnNoteBook_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 帮专家册
        /// <summary>
        /// 帮专家册
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNoteBookItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ToolHelper.OpenBrowserUrl("http://pan.baidu.com/s/1ntxM11B");
            ToolHelper.OpenBrowserUrl("http://yd.zt-express.com/Help/Index2");
        }
        #endregion

        #region private void btnMessages_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 留言板
        /// <summary>
        /// 留言板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMessagesItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ToolHelper.OpenBrowserUrl("http://www.zto.com/GuestService/Guestbook");
        }
        #endregion

        #region private void barOderTemplate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 定制模板
        /// <summary>
        /// 定制模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarOderTemplateItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChildFormManagementHelper.LoadMdiForm(this, "FrmOrderTemplate");
        }
        #endregion

        #region private void btnVideoHelp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 操作视频
        /// <summary>
        /// 操作视频
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnVideoHelpItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ToolHelper.OpenBrowserUrl("http://pan.baidu.com/s/1kTzSTlt?qq-pf-to=pcqq.c2c");
            ToolHelper.OpenBrowserUrl("http://v.youku.com/v_show/id_XMTMxMzk1OTM0NA==.html?from=y1.7-1.2&qq-pf-to=pcqq.discussion");
        }
        #endregion

        #region private void btnSearchBill_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 查快递
        /// <summary>
        /// 查快递
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearchBillItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChildFormManagementHelper.LoadMdiForm(this, "FrmSearchBill");
        }
        #endregion

        #region private void btnPrintMark_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 申通大头笔查询
        /// <summary>
        /// 申通大头笔查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrintMarkItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var checkPrintMark = new FrmZtoSearchPrintMark();
            checkPrintMark.Show();
            //ChildFormManagement.LoadMdiForm(this, "FrmZtoSearchPrintMark");
        }
        #endregion

        #region private void FrmMain_Activated(object sender, EventArgs e) 窗体激活事件
        /// <summary>
        /// 窗体激活事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMainActivated(object sender, EventArgs e)
        {
            // 窗体激活注册快捷键
            // HotKey.RegisterHotKey(Handle, 102, HotKey.KeyModifiers.Alt | HotKey.KeyModifiers.Ctrl, Keys.Q);
            HotKey.RegisterHotKey(Handle, 102, HotKey.KeyModifiers.Alt, Keys.Q);
        }
        #endregion

        #region private void FrmMain_Leave(object sender, EventArgs e) 窗体离开事件
        /// <summary>
        /// 窗体离开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMainLeave(object sender, EventArgs e)
        {
            // 窗体不激活取消注册快捷键
            HotKey.UnregisterHotKey(Handle, 102);
        }
        #endregion

        #region protected override void WndProc(ref Message m) 监视Windows消息
        /// <summary>
        /// 监视Windows消息
        /// 重载WndProc方法，用于实现热键响应
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            const int wmHotkey = 0x0312;//如果m.Msg的值为0x0312那么表示用户按下了热键
            //按快捷键 
            switch (m.Msg)
            {
                case wmHotkey:
                    switch (m.WParam.ToInt32())
                    {
                        case 100:    //按下的是Shift+S
                            //此处填写快捷键响应代码         
                            break;
                        case 101:    //按下的是Ctrl+B
                            //此处填写快捷键响应代码
                            break;
                        case 102:    //按下的是Ctrl+Alt+S
                            var capture = new CaptureImageTool();
                            if (capture.ShowDialog() == DialogResult.OK)
                            {
                                Clipboard.SetImage(capture.Image);
                            }
                            break;
                    }
                    break;
            }
            base.WndProc(ref m);
        }
        #endregion

        #region private void btnScreenShot_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 截图
        /// <summary>
        /// 截图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnScreenShotItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var capture = new CaptureImageTool();
            if (capture.ShowDialog() == DialogResult.OK)
            {
                Clipboard.SetImage(capture.Image);
            }
        }
        #endregion

        #region private void btnWeather_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 天气
        /// <summary>
        /// 天气
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnWeatherItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var weather = new FrmBaiduWeather();
            weather.ShowDialog();
            weather.Dispose();
        }
        #endregion

        #region private void btnDownLoadInnerBill_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 下载申通内部模板
        /// <summary>
        /// 下载申通内部模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDownLoadInnerBillItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BillPrintHelper.DownloadPrintTemplate("ZTOInnerBill");
        }
        #endregion

        #region private void btnBaiduLocation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 识别百度地址
        /// <summary>
        /// 识别百度地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBaiduLocationItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChildFormManagementHelper.LoadMdiForm(this, "FrmBaiduLocation");
        }
        #endregion

        #region private void btnClac_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 计算器
        /// <summary>
        /// 计算器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClac_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ToolHelper.OpenCalculator();
        }
        #endregion

        #region private void btnMobileAttribution_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 手机号归属地查询
        /// <summary>
        /// 手机号归属地查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMobileAttribution_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChildFormManagementHelper.LoadMdiForm(this, "FrmMobileInfo");
        }
        #endregion

        #region private void btnQrCode_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 二维码工具
        /// <summary>
        /// 二维码工具
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQrCode_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChildFormManagementHelper.LoadMdiForm(this, "FrmQRCode");
        }
        #endregion

        /// <summary>
        /// 打印数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChildFormManagementHelper.LoadMdiForm(this, "FrmPrintData");
        }

        /// <summary>
        /// 导入淘宝订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImportTaoBaoOrder_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        /// <summary>
        /// 导入京东订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImportJingDongOrder_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        /// <summary>
        /// 发件人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendManData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChildFormManagementHelper.LoadMdiForm(this, "FrmSendManData");
        }

        /// <summary>
        /// 收件人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReceiveManData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChildFormManagementHelper.LoadMdiForm(this, "FrmReceiveManData");
        }

        /// <summary>
        /// 自动检查更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            UpdateHelper.ForcedUpdate();
        }

        /// <summary>
        /// 条形码工具
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBarCode_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChildFormManagementHelper.LoadMdiForm(this, "FrmBarCode");
        }

        /// <summary>
        /// 系统设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSystemSetting_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frmSystemSetting = new FrmSystemSetting() { Owner = this };
            frmSystemSetting.ShowDialog();
        }

        /// <summary>
        /// QQ客服2【刘云峰的qq】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQQ2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            QQHelper.ChatQQ();
        }

        private void btnQQGroup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            QQHelper.JoinQQGroup();
            Clipboard.SetText("600952565");
        }

        private void btnQQGroup3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            QQHelper.JoinQQGroup2();
            Clipboard.SetText("342190881");
        }

        private void btnQQGroup2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            QQHelper.JoinQQGroup3();
            Clipboard.SetText("207444366");
        }

        /// <summary>
        /// 下载链接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownLoad_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ToolHelper.OpenBrowserUrl("http://www.zto.com/GuestService/Print");
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmMain_MouseDown(object sender, MouseEventArgs e)
        {

        }

        /// <summary>
        /// 右击弹出功能菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraTabbedMdiManager1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //  radialMenu1.ShowPopup(Control.MousePosition, true);
            }
        }

        /// <summary>
        /// 新增打印记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddBill_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //radialMenu1.HidePopup();
            //ChildFormManagementHelper.LoadMdiForm(this, "FrmPrintData");
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            // Application.ExitThread(); // 这是直接杀死线程的
        }

        /// <summary>
        /// 回收站
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeletedBill_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChildFormManagementHelper.LoadMdiForm(this, "FrmRecycleRecord");
        }

        /// <summary>
        /// 电脑信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnComputerInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new FrmComputerInfo().Show(this);
        }

        /// <summary>
        /// 取消订单回收站
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelHistory_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChildFormManagementHelper.LoadMdiForm(this, "FrmCancelRecord");
        }

        /// <summary>
        /// 微店订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWeiDian_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }


        /// <summary>
        /// 重启打印专家
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspRestart_Click(object sender, EventArgs e)
        {
            Program.Restart();
        }

        /// <summary>
        /// 系统设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspSystemSetting_Click(object sender, EventArgs e)
        {
            var frmSystemSetting = new FrmSystemSetting() { Owner = this };
            frmSystemSetting.ShowDialog();
        }

        /// <summary>
        /// 检查新版本【不使用强制升级，用手动升级】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspCheckUpdate_Click(object sender, EventArgs e)
        {
            UpdateHelper.ManualUpdate();
        }

        /// <summary>
        /// 关于打印专家
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspAbout_Click(object sender, EventArgs e)
        {
            new FrmAboutThis().ShowDialog();
        }

        private void tspLock_Click(object sender, EventArgs e)
        {
            myLock1.Visible = true;
        }

        /// <summary>
        /// 手动改【自动启动】复选框的选中状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspAutoStart_Click(object sender, EventArgs e)
        {
            tspAutoStart.Checked = !tspAutoStart.Checked;
        }

        /// <summary>
        /// 自动启动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspAutoStart_CheckedChanged(object sender, EventArgs e)
        {
            var result = ToolHelper.SetAutoRunWhenStart(Application.StartupPath + @"\STO.Print.exe", tspAutoStart.Checked);
            if (result == "1")
            {
                notifyIcon.ShowBalloonTip(3000, "系统设置", "设置成功，下次开机自动启动" + BaseSystemInfo.SoftFullName, ToolTipIcon.Info);
                BillPrintHelper.SetAutoRunWhenStart(true);
            }
            else if (result == "2")
            {
                notifyIcon.ShowBalloonTip(3000, "系统设置", "取消自启动成功", ToolTipIcon.Info);
                BillPrintHelper.SetAutoRunWhenStart(false);
            }
            else
            {
                notifyIcon.ShowBalloonTip(3000, "系统设置", result, ToolTipIcon.Error);
            }
        }

        /// <summary>
        /// 窗体加载完成后触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_Shown(object sender, EventArgs e)
        {
            // 得到是否开机启动
            // tspAutoStart.Checked = BillPrintHelper.GetAutoRunWhenStart();
        }

        /// <summary>
        /// 窗体大小改变的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_Resize(object sender, EventArgs e)
        {
            //窗体最小化时  
            if (this.WindowState == FormWindowState.Minimized)
            {
                notifyIcon.ShowBalloonTip(5000, "信息提示", "亲，打开打印专家主界面在这里哦！", ToolTipIcon.Info);
            }

            //窗体恢复正常时  
            if (this.WindowState == FormWindowState.Normal)
            {
                // notifyIcon.ShowBalloonTip(5000, "信息提示", "亲，打开打印专家主界面在这里哦！", ToolTipIcon.Info);
            }

        }

        /// <summary>
        /// 双击
        /// 想实现窗口最小化后隐藏,双击NotifyIcon后显视窗口并恢复大小
        /// http://bbs.csdn.net/topics/80239857
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            if (this.Visible == false)
            {
                this.WindowState = FormWindowState.Maximized;
                this.Visible = true;
                this.Activate();
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;
                this.Visible = false;
            }
        }

        private void myLock1_Load(object sender, EventArgs e)
        {
            }
    }
}
