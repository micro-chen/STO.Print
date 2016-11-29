//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace STO.Print
{
    using DevExpress.Utils;
    using DevExpress.XtraEditors;
    using DotNet.Utilities;
    using Model;
    using Utilities;
    using STO.Print.AddBillForm;
    using STO.Print.Manager;

    /// <summary>
    /// Excel导入窗体
    /// 
    /// 修改记录
    ///     
    ///     2015-07-20 版本：1.0 YangHengLian 创建
    ///     2016-1-23 发现没有读取Excel里面的订单号，改进了一下，默认读取Excel里面的，如果没有就自动生成一个
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-11-12</date>
    /// </author>
    /// </summary>
    public partial class FrmImportExcel : BaseForm
    {
        /// <summary>
        /// 提取检查单号刷新界面委托
        /// </summary>
        /// <param name="id"></param>
        private delegate void CheckDelegate(object id);

        /// <summary>
        /// 线程键值对集合，用于管理线程
        /// </summary>
        public Hashtable CheckBillManager = new Hashtable();

        /// <summary>
        /// 每次导入充Excel获取到的所有单号个数
        /// </summary>
        public int BillCodeCount;

        /// <summary>
        /// 线程单号计数器，每个线程处理了一批单号就对应加上去，最后和总单号比较，相等就表示所有线程已经处理完成了，进行过滤和导入正确单号动作
        /// </summary>
        public int BillCodeIndex;

        /// <summary>
        /// 开始导入时间点
        /// </summary>
        public DateTime StartDt;

        /// <summary>
        /// 需要处理的运单实体
        /// </summary>
        public List<ZtoPrintBillEntity> PrintBillEntities = new List<ZtoPrintBillEntity>();

        /// <summary>
        /// 原来打印的源数据集合
        /// </summary>
        public List<ZtoPrintBillEntity> PrintSourceBillList = new List<ZtoPrintBillEntity>();

        public FrmImportExcel()
        {
            InitializeComponent();
        }

        #region private void CheckBillCode(List<string> billCodeList) 开启线程对单号进行校验
        /// <summary>
        /// 开启线程对单号进行校验
        /// </summary>
        /// <param name="billCodeList">需要校验的所有单号</param>
        private void CheckBillCode(List<ZtoPrintBillEntity> billCodeList)
        {
            BillCodeIndex = 0;
            BillCodeCount = billCodeList.Count;
            if (billCodeList.Any())
            {
                marqueeProgressBarControl1.Properties.Stopped = false;
                PrintSourceBillList.Clear();
                PrintSourceBillList = billCodeList;
                // 最大线程数可以让用户自己选择，如果提取不超过50就直接使用一个线程就行了，没必要浪费
                var maxThreadCount = billCodeList.Count > 50 ? Convert.ToInt32(Math.Ceiling(billCodeList.Count / 50.0)) : 1;
                if (maxThreadCount > 5)
                {
                    // 超过3000个就开启5个线程执行导入
                    if (billCodeList.Count >= 200)
                    {
                        maxThreadCount = 5;
                        // 超过10000要开8个线程
                        if (billCodeList.Count >= 500)
                        {
                            maxThreadCount = 10;
                        }
                    }
                    else
                    {
                        // 开启3个就够了，防止电脑卡
                        maxThreadCount = 3;
                    }
                }
                //如果是N个线程，需要先对数据进行处理
                var listNo = billCodeList.Count / maxThreadCount;
                // 先停止所有的线程，防止开启太多的线程
                StopCheckBillThread();
                for (int i = 1; i < maxThreadCount + 1; i++)
                {
                    List<ZtoPrintBillEntity> billCodeListSend;
                    if (i == 1)
                    {
                        billCodeListSend = billCodeList.Skip((i - 1) * listNo).Take(listNo + 1).ToList();
                    }
                    else
                    {
                        billCodeListSend = billCodeList.Skip((i - 1) * listNo + i - 1).Take(listNo + 1).ToList();
                    }
                    RegisterUploadPicture(billCodeListSend, i.ToString(CultureInfo.InvariantCulture));
                }
            }
        }
        #endregion

        #region private void RegisterUploadPicture(List<string> billCodeList, string iThread) 开启线程
        /// <summary>
        /// 开启线程
        /// </summary>
        /// <param name="printBillEntities">线程所需要实体集合</param>
        /// <param name="iThread">线程名称</param>
        private void RegisterUploadPicture(List<ZtoPrintBillEntity> printBillEntities, string iThread)
        {
            var item = CheckBillManager[BaseSystemInfo.UserInfo.Code + "_" + iThread] as LoadPrintMarkEntity;
            if (item == null)
            {
                item = CheckBillManager[BaseSystemInfo.UserInfo.Code + "_" + iThread] as LoadPrintMarkEntity;
                if (item == null)
                {
                    item = new LoadPrintMarkEntity();
                    CheckBillManager[BaseSystemInfo.UserInfo.Code + "_" + iThread] = item;
                    item.PrintBillEntities = printBillEntities;
                    item.Code = BaseSystemInfo.UserInfo.Code + "---" + iThread;
                    item.Looker = new Thread(CheckBillList)
                    {
                        Priority = ThreadPriority.Normal
                    };
                    item.Looker.Start(item);
                }
            }
        }
        #endregion

        #region public void CheckBillList(object updateBillItem) 线程执行校验单号函数
        /// <summary>
        /// 线程执行校验单号函数
        /// </summary>
        /// <param name="updateBillItem"></param>
        public void CheckBillList(object updateBillItem)
        {
            var item = updateBillItem as LoadPrintMarkEntity;
            try
            {
                if (item != null)
                {
                    // 循环次数 65  2 
                    var length = item.PrintBillEntities.Count > 50 ? Convert.ToInt32(Math.Ceiling(item.PrintBillEntities.Count / 50.0)) : 1;
                    // 每次循环单号个数 33
                    var listNo = item.PrintBillEntities.Count / length;

                    for (int i = 1; i < length + 1; i++)
                    {
                        List<ZtoPrintBillEntity> billCodeListSend;
                        if (i == 1)
                        {
                            billCodeListSend = item.PrintBillEntities.Skip((i - 1) * listNo).Take(listNo + 1).ToList();
                        }
                        else
                        {
                            billCodeListSend = item.PrintBillEntities.Skip((i - 1) * listNo + i - 1).Take(listNo + 1).ToList();
                        }
                        // 获取发件人的省市区
                        foreach (ZtoPrintBillEntity ztoPrintBillEntity in billCodeListSend)
                        {
                            if (string.IsNullOrEmpty(ztoPrintBillEntity.SendProvince) && string.IsNullOrEmpty(ztoPrintBillEntity.SendCity) && string.IsNullOrEmpty(ztoPrintBillEntity.SendCounty))
                            {
                                if (!string.IsNullOrEmpty(ztoPrintBillEntity.SendAddress))
                                {
                                    var baiAddressEntity = BaiduMapHelper.GetProvCityDistFromBaiduMap(ztoPrintBillEntity.SendAddress);
                                    if (baiAddressEntity != null)
                                    {
                                        ztoPrintBillEntity.SendProvince = baiAddressEntity.Result.AddressComponent.Province;
                                        ztoPrintBillEntity.SendCity = baiAddressEntity.Result.AddressComponent.City;
                                        ztoPrintBillEntity.SendCounty = baiAddressEntity.Result.AddressComponent.District;
                                        ZtoPrintBillEntity entity = ztoPrintBillEntity;
                                        marqueeProgressBarControl1.BeginInvoke(new Action(() =>
                                        {
                                            marqueeProgressBarControl1.Text = string.Format("正在获取发件人省市区({0}-{1}-{2}),数据加载中,请稍后...", entity.SendProvince, entity.SendCity, entity.SendCounty);
                                        }));
                                    }
                                }
                            }
                        }
                        // 获取收件人的省市区
                        foreach (ZtoPrintBillEntity ztoPrintBillEntity in billCodeListSend)
                        {
                            if (string.IsNullOrEmpty(ztoPrintBillEntity.ReceiveProvince) && string.IsNullOrEmpty(ztoPrintBillEntity.ReceiveCity) && string.IsNullOrEmpty(ztoPrintBillEntity.ReceiveCounty))
                            {
                                if (!string.IsNullOrEmpty(ztoPrintBillEntity.ReceiveAddress))
                                {
                                    var baiAddressEntity = BaiduMapHelper.GetProvCityDistFromBaiduMap(ztoPrintBillEntity.ReceiveAddress);
                                    if (baiAddressEntity != null)
                                    {
                                        ztoPrintBillEntity.ReceiveProvince = baiAddressEntity.Result.AddressComponent.Province;
                                        ztoPrintBillEntity.ReceiveCity = baiAddressEntity.Result.AddressComponent.City;
                                        ztoPrintBillEntity.ReceiveCounty = baiAddressEntity.Result.AddressComponent.District;
                                        ZtoPrintBillEntity entity = ztoPrintBillEntity;
                                        marqueeProgressBarControl1.BeginInvoke(new Action(() =>
                                        {
                                            marqueeProgressBarControl1.Text = string.Format("正在获取收件人省市区({0}-{1}-{2}),数据加载中,请稍后...", entity.ReceiveProvince, entity.ReceiveCity, entity.ReceiveCounty);
                                        }));
                                    }
                                }
                            }
                        }
                        // 绑定大头笔信息
                        foreach (ZtoPrintBillEntity ztoPrintBillEntity in billCodeListSend)
                        {
                            // 如果大头笔已经有了就不用联网获取了
                            var selectedRemark = new List<string> { ztoPrintBillEntity.SendProvince, ztoPrintBillEntity.SendCity, ztoPrintBillEntity.SendCounty };
                            var selectedReceiveMark = new List<string> { ztoPrintBillEntity.ReceiveProvince, ztoPrintBillEntity.ReceiveCity, ztoPrintBillEntity.ReceiveCounty };
                            var printMark = BillPrintHelper.GetRemaike(string.Join(",", selectedRemark), ztoPrintBillEntity.SendAddress, string.Join(",", selectedReceiveMark), ztoPrintBillEntity.ReceiveAddress);
                            ztoPrintBillEntity.BigPen = printMark;
                            PrintBillEntities.Add(ztoPrintBillEntity);
                            ZtoPrintBillEntity entity = ztoPrintBillEntity;
                            marqueeProgressBarControl1.BeginInvoke(new Action(() =>
                            {
                                marqueeProgressBarControl1.Text = string.Format("正在获取大头笔({0}),数据加载中,请稍后...", entity.BigPen);
                            }));
                        }
                        BillCodeIndex += billCodeListSend.Count;
                        //指定委托方法
                        CheckDelegate refresh = Refresh;
                        Invoke(refresh, item.Code);
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        #endregion

        #region private void Refresh(object id) 刷新界面消息，最后执行导入
        /// <summary>
        /// 刷新界面消息，最后执行导入
        /// </summary>
        /// <param name="id"></param>
        private void Refresh(object id)
        {
            if (!splashScreenManagerImportExcel.IsSplashFormVisible)
            {
                splashScreenManagerImportExcel.ShowWaitForm();
            }
            splashScreenManagerImportExcel.SetWaitFormCaption("请稍后......");
            splashScreenManagerImportExcel.SetWaitFormDescription(string.Format("导入{0}条,成功：{1},失败：{2}", BillCodeCount, PrintBillEntities.Count, 0));
            if (BillCodeCount == BillCodeIndex)
            {
                marqueeProgressBarControl1.Properties.Stopped = true;
                marqueeProgressBarControl1.BeginInvoke(new Action(() =>
                {
                    marqueeProgressBarControl1.Text = "获取结束，开始导入,请稍后...";
                }));
                if (!splashScreenManagerImportExcel.IsSplashFormVisible)
                {
                    splashScreenManagerImportExcel.ShowWaitForm();
                }
                Application.DoEvents();
                splashScreenManagerImportExcel.SetWaitFormCaption("请稍后");
                splashScreenManagerImportExcel.SetWaitFormDescription(string.Format("保存{0}条记录到本地.....", PrintBillEntities.Count));
                if (PrintBillEntities.Any())
                {
                    ZtoPrintBillManager printBillManager = new ZtoPrintBillManager(BillPrintHelper.DbHelper);
                    if (PrintSourceBillList.Count == PrintBillEntities.Count)
                    {
                        PrintBillEntities.Clear();
                        PrintBillEntities.AddRange(PrintSourceBillList);
                    }
                    foreach (ZtoPrintBillEntity ztoPrintBillEntity in PrintBillEntities)
                    {
                        if (ztoPrintBillEntity != null)
                        {
                            printBillManager.Add(ztoPrintBillEntity, true);
                        }
                    }
                    GridDataBind();
                }
                Thread.Sleep(1500);
                splashScreenManagerImportExcel.CloseWaitForm();
                var ts = DateTime.Now - StartDt;
                lblTime.Text += string.Format(" 耗时:{0}时{1}分{2}秒{3}毫秒", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
                Close();
            }
        }
        #endregion

        #region private void StopCheckBillThread() 停止所有单号检查的线程
        /// <summary>
        /// 停止所有单号检查的线程
        ///  </summary>
        private void StopCheckBillThread()
        {
            if (CheckBillManager.Count == 0)
            {
                return;
            }
            foreach (LoadPrintMarkEntity item in CheckBillManager.Values)
            {
                if (item.Looker.ThreadState == System.Threading.ThreadState.WaitSleepJoin || item.Looker.ThreadState == System.Threading.ThreadState.Running)
                {
                    item.Looker.Interrupt();
                }
                item.Looker.Abort();
            }
            CheckBillManager.Clear();
        }
        #endregion

        #region private void FrmImportExcel_Load(object sender, EventArgs e) 窗体加载事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmImportExcel_Load(object sender, EventArgs e)
        {
            var result = BillPrintHelper.GetTodaySend();
            if (string.IsNullOrEmpty(result))
            {
                ckTodaySend.Checked = false;
            }
            else
            {
                ckTodaySend.Checked = result == "1";
            }
            var result1 = BillPrintHelper.GetLoadDefaultSendMan();
            if (string.IsNullOrEmpty(result1))
            {
                ckUserDefaultSendMan.Checked = true;
            }
            else
            {
                ckUserDefaultSendMan.Checked = result == "1";
            }
            //var result2 = BillPrintHelper.GetPrintMarkFromServer();
            //if (string.IsNullOrEmpty(result2))
            //{
            //    ckGetServerPrintMark.Checked = false;
            //}
            //else
            //{
            //    ckGetServerPrintMark.Checked = result == "1";
            //}
            GridDataBind();
            var gridLocalizer = BuilderGridLocalizer.SetGridLocalizer();
            BuilderGridLocalizer.CustomButtonText(gridViewImportExcel, gridLocalizer);
            gridViewImportExcel.OptionsFind.AlwaysVisible = true;
            gridViewImportExcel.FindPanelVisible = true;
            gridViewImportExcel.ShowFindPanel();
            txtFileFullPath.Text = BillPrintHelper.GetDefaultExcelPath();
        }
        #endregion

        #region private void btnOpenExcel_Click(object sender, EventArgs e)
        /// <summary>
        /// 浏览打开本地磁盘上的EXCEL文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenExcel_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog { Filter = @"Excel文件|*.xls;*.xlsx" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtFileFullPath.Text = ofd.FileName;
                }
            }
        }
        #endregion

        #region public bool Import() 导入Excel数据到本地数据库
        /// <summary>
        /// 导入Excel数据到本地数据库
        /// </summary>
        public bool Import()
        {
            if (string.IsNullOrEmpty(txtFileFullPath.Text.Trim()))
            {
                XtraMessageBox.Show(@"请选择录单模板", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnOpenExcel_Click(this, null);
                return false;
            }
            if (!File.Exists(txtFileFullPath.Text))
            {
                XtraMessageBox.Show(@"选中文件不存在，请重新选择导入Excel文件", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnOpenExcel_Click(this, null);
                return false;
            }
            var startDateTime = DateTime.Now;
            StartDt = startDateTime;
            if (!splashScreenManagerImportExcel.IsSplashFormVisible)
            {
                splashScreenManagerImportExcel.ShowWaitForm();
            }
            Application.DoEvents();
            splashScreenManagerImportExcel.SetWaitFormCaption("请稍后");
            splashScreenManagerImportExcel.SetWaitFormDescription("开始导入Excel数据...");
            try
            {
                DataTable chooseDt = ExcelHelper.ExcelToDataTable(txtFileFullPath.Text.Trim(), 38, 0, 3);
                if (chooseDt != null && chooseDt.Rows.Count > 0)
                {
                    var list = new List<ZtoPrintBillEntity>();
                    int temp = 0;
                    var defaultUserList = new ZtoUserManager(BillPrintHelper.DbHelper).GetList<ZtoUserEntity>(new KeyValuePair<string, object>(ZtoUserEntity.FieldIsDefault, 1), new KeyValuePair<string, object>(ZtoUserEntity.FieldIssendorreceive, 1));
                    ZtoUserEntity defaultUserEntity = null;
                    if (defaultUserList.Any())
                    {
                        defaultUserEntity = defaultUserList.First();
                    }
                    foreach (DataRow dr in chooseDt.Rows)
                    {
                        ++temp;
                        splashScreenManagerImportExcel.SetWaitFormDescription(string.Format("正在导入Excel数据：{0}/{1}", temp, chooseDt.Rows.Count));
                        ZtoPrintBillEntity entity = new ZtoPrintBillEntity();
                        if (ckTodaySend.Checked)
                        {
                            entity.SendDate = DateTime.Now.ToString(BaseSystemInfo.DateFormat);
                        }
                        if (ckUserDefaultSendMan.Checked)
                        {
                            if (defaultUserEntity != null)
                            {
                                entity.SendMan = defaultUserEntity.Realname;
                                entity.SendPhone = defaultUserEntity.Mobile + " " + defaultUserEntity.TelePhone;
                                entity.SendProvince = defaultUserEntity.Province;
                                entity.SendCity = defaultUserEntity.City;
                                entity.SendCounty = defaultUserEntity.County;
                                entity.SendAddress = defaultUserEntity.Address;
                                entity.SendSite = "";
                                entity.SendDeparture = entity.SendProvince;
                                entity.SendCompany = defaultUserEntity.Company;
                                entity.SendDepartment = defaultUserEntity.Department;
                                entity.SendPostcode = defaultUserEntity.Postcode;
                            }
                        }
                        else
                        {
                            if (this.Tag != null)
                            {
                                var sendUserEntity = this.Tag as ZtoUserEntity;
                                // 表示选择了一个发件人的信息，这样也不用读取Excel里面的发件人了
                                if (sendUserEntity != null)
                                {
                                    entity.SendMan = sendUserEntity.Realname;
                                    entity.SendPhone = sendUserEntity.Mobile + " " + sendUserEntity.TelePhone;
                                    entity.SendProvince = sendUserEntity.Province;
                                    entity.SendCity = sendUserEntity.City;
                                    entity.SendCounty = sendUserEntity.County;
                                    entity.SendAddress = sendUserEntity.Address;
                                    entity.SendSite = "";
                                    entity.SendDeparture = sendUserEntity.Province;
                                    entity.SendCompany = sendUserEntity.Company;
                                    entity.SendDepartment = sendUserEntity.Department;
                                    entity.SendPostcode = sendUserEntity.Postcode;
                                }
                            }
                            else
                            {
                                entity.SendMan = BaseBusinessLogic.ConvertToString(dr[2]);
                                entity.SendPhone = BaseBusinessLogic.ConvertToString(dr[3]);
                                entity.SendProvince = BaseBusinessLogic.ConvertToString(dr[4]);
                                entity.SendCity = BaseBusinessLogic.ConvertToString(dr[5]);
                                entity.SendCounty = BaseBusinessLogic.ConvertToString(dr[6]);
                                entity.SendAddress = BaseBusinessLogic.ConvertToString(dr[7]);
                                entity.SendSite = "";
                                entity.SendDeparture = BaseBusinessLogic.ConvertToString(dr[26]);
                                if (string.IsNullOrEmpty(entity.SendDeparture))
                                {
                                    entity.SendDeparture = entity.SendProvince;
                                }
                                entity.SendCompany = BaseBusinessLogic.ConvertToString(dr[27]);
                                entity.SendDepartment = BaseBusinessLogic.ConvertToString(dr[28]);
                                entity.SendPostcode = BaseBusinessLogic.ConvertToString(dr[29]);
                            }
                        }
                        entity.ReceiveMan = BaseBusinessLogic.ConvertToString(dr[8]);
                        entity.ReceivePhone = BaseBusinessLogic.ConvertToString(dr[9]);
                        entity.ReceiveProvince = BaseBusinessLogic.ConvertToString(dr[10]);
                        entity.ReceiveCity = BaseBusinessLogic.ConvertToString(dr[11]);
                        entity.ReceiveCounty = BaseBusinessLogic.ConvertToString(dr[12]);
                        entity.ReceiveAddress = BaseBusinessLogic.ConvertToString(dr[13]);
                        entity.ReceiveDestination = BaseBusinessLogic.ConvertToString(dr[30]);
                        if (string.IsNullOrEmpty(entity.ReceiveDestination))
                        {
                            entity.ReceiveDestination = entity.ReceiveProvince;
                        }
                        entity.ReceiveCompany = BaseBusinessLogic.ConvertToString(dr[31]);
                        entity.ReceivePostcode = BaseBusinessLogic.ConvertToString(dr[32]);
                        entity.GoodsName = BaseBusinessLogic.ConvertToString(dr[14]);
                        entity.Weight = BaseBusinessLogic.ConvertToString(dr[15]);
                        entity.TranFee = BaseBusinessLogic.ConvertToString(dr[16]);
                        entity.GOODS_PAYMENT = BaseBusinessLogic.ConvertToDecimal(dr[17]);
                        entity.TOPAYMENT = BaseBusinessLogic.ConvertToDecimal(dr[18]);
                        entity.Length = BaseBusinessLogic.ConvertToString(dr[33]);
                        entity.Width = BaseBusinessLogic.ConvertToString(dr[34]);
                        entity.Height = BaseBusinessLogic.ConvertToString(dr[35]);
                        entity.TotalNumber = BaseBusinessLogic.ConvertToString(dr[36]);
                        entity.OrderNumber = BaseBusinessLogic.ConvertToString(dr[37]);
                        entity.Remark = BaseBusinessLogic.ConvertToString(dr[22]);
                        entity.CreateUserName = "";
                        entity.CreateSite = "";
                        entity.CreateOn = DateTime.Now;
                        entity.PaymentType = "现金";
                        // 如果Excel里面没有填写订单号系统自动生成一个订单号，这样提取电子面单单号就不用怕了，2016-1-23 14:07:12
                        if (string.IsNullOrEmpty(entity.OrderNumber))
                        {
                            // 导入自动生成订单号（电子面单）79170-南昌昌南  18779176845 这个qq提供的思路，2016-1-20 20:08:50
                            entity.OrderNumber = Guid.NewGuid().ToString("N").ToLower();
                        }
                        if (!ckGetServerPrintMark.Checked)
                        {
                            entity.BigPen = string.Format("{0} {1} {2}", entity.ReceiveProvince, entity.ReceiveCity, entity.ReceiveCounty);
                        }
                        else
                        {
                            entity.BigPen = "";
                        }
                        list.Add(entity);
                    }
                    if (!ckGetServerPrintMark.Checked)
                    {
                        var manager = new ZtoPrintBillManager(BillPrintHelper.DbHelper);
                        foreach (ZtoPrintBillEntity ztoPrintBillEntity in list)
                        {
                            manager.Add(ztoPrintBillEntity, true);
                        }
                        if (splashScreenManagerImportExcel != null && splashScreenManagerImportExcel.IsSplashFormVisible)
                        {
                            splashScreenManagerImportExcel.CloseWaitForm();
                        }
                        GridDataBind();
                        var ts = DateTime.Now - startDateTime;
                        lblTime.Text = string.Format("耗时:{0}时{1}分{2}秒{3}毫秒", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
                        Close();
                    }
                    else
                    {
                        splashScreenManagerImportExcel.SetWaitFormDescription("正在联网获取大头笔信息，请稍后......");
                        // 开线程去读取大头笔的
                        CheckBillCode(list);
                    }
                }
                else
                {
                    XtraMessageBox.Show(@"模板没有填写任何数据，导入失败", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
                return false;
            }
            finally
            {
                if (splashScreenManagerImportExcel != null && splashScreenManagerImportExcel.IsSplashFormVisible)
                {
                    splashScreenManagerImportExcel.CloseWaitForm();
                }
            }
            return true;
        }
        #endregion

        #region private void btnClose_Click(object sender, EventArgs e) 关闭窗体
        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                Import();
            }
            catch (Exception exception)
            {
                ProcessException(exception);
            }
        }
        #endregion

        #region private void lblDownload_Click(object sender, EventArgs e) 下载EXCEL数据模版
        /// <summary>
        /// 下载EXCEL数据模版
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblDownload_Click(object sender, EventArgs e)
        {
            DownloadTemplate.DownloadExcelTemplate("ImportBill");
        }
        #endregion

        #region private void GridDataBind() 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void GridDataBind()
        {
            DataTable dt = BillPrintHelper.DbHelper.Fill(BillPrintHelper.CmdStrForZtoBillPrinter);
            // 增加个CheckBox列
            dt.Columns.Add("Check", typeof(bool));
            // 设置选择列的位置
            dt.Columns["Check"].SetOrdinal(0);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Check"] = false;
            }
            // 设置gridview列头的字体大小
            gridViewImportExcel.Appearance.HeaderPanel.Font = new Font("Tahoma", 9);
            // 设置gridview列头居中
            gridViewImportExcel.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;
            gridControlImportExcel.DataSource = dt;
            gridViewImportExcel.OptionsView.ColumnAutoWidth = false;
            gridViewImportExcel.Columns["Id"].Visible = false;
            gridViewImportExcel.Columns["Check"].Width = 22;
            gridViewImportExcel.Columns["Check"].OptionsColumn.ShowCaption = false;
            gridViewImportExcel.Columns["Check"].OptionsColumn.AllowSort = DefaultBoolean.False;
            gridViewImportExcel.Columns["Check"].OptionsColumn.AllowEdit = false;
            gridViewImportExcel.Columns["单号"].Width = 100;
            gridViewImportExcel.Columns["订单号"].Width = 100;
            gridViewImportExcel.Columns["发件日期"].Width = 100;
            gridViewImportExcel.Columns["创建时间"].Width = 160;
            gridViewImportExcel.Columns["发件网点"].Width = 80;
            gridViewImportExcel.Columns["发件人姓名"].Width = 80;
            gridViewImportExcel.Columns["始发地"].Width = 80;
            gridViewImportExcel.Columns["发件省份"].Width = 80;
            gridViewImportExcel.Columns["发件城市"].Width = 80;
            gridViewImportExcel.Columns["发件区县"].Width = 80;
            gridViewImportExcel.Columns["发件单位"].Width = 200;
            gridViewImportExcel.Columns["发件部门"].Width = 80;
            gridViewImportExcel.Columns["发件电话"].Width = 100;
            gridViewImportExcel.Columns["发件邮编"].Width = 80;
            gridViewImportExcel.Columns["发件详细地址"].Width = 300;
            gridViewImportExcel.Columns["收件人姓名"].Width = 80;
            gridViewImportExcel.Columns["收件人姓名"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            gridViewImportExcel.Columns["收件人姓名"].SummaryItem.DisplayFormat = @"总计：{0}";
            gridViewImportExcel.Columns["目的地"].Width = 80;
            gridViewImportExcel.Columns["收件省份"].Width = 80;
            gridViewImportExcel.Columns["收件城市"].Width = 80;
            gridViewImportExcel.Columns["收件区县"].Width = 80;
            gridViewImportExcel.Columns["收件电话"].Width = 100;
            gridViewImportExcel.Columns["收件人单位"].Width = 200;
            gridViewImportExcel.Columns["收件人邮编"].Width = 80;
            gridViewImportExcel.Columns["收件人详细地址"].Width = 300;
            gridViewImportExcel.Columns["物品类型"].Width = 80;
            gridViewImportExcel.Columns["大头笔"].Width = 80;
            gridViewImportExcel.Columns["长"].Width = 30;
            gridViewImportExcel.Columns["宽"].Width = 30;
            gridViewImportExcel.Columns["高"].Width = 30;
            gridViewImportExcel.Columns["重量"].Width = 60;
            gridViewImportExcel.Columns["重量"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridViewImportExcel.Columns["重量"].SummaryItem.DisplayFormat = @"{0}";
            gridViewImportExcel.Columns["付款方式"].Width = 60;
            gridViewImportExcel.Columns["运费"].Width = 60;
            gridViewImportExcel.Columns["运费"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridViewImportExcel.Columns["运费"].SummaryItem.DisplayFormat = @"{0}";
            gridViewImportExcel.Columns["数量"].Width = 60;
            gridViewImportExcel.Columns["数量"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridViewImportExcel.Columns["数量"].SummaryItem.DisplayFormat = @"{0}";
            gridViewImportExcel.Columns["备注"].Width = 100;
            gridViewImportExcel.Columns["创建时间"].DisplayFormat.FormatString = "yyyy/MM/dd hh:mm:ss";
            gridViewImportExcel.MoveLast();
        }
        #endregion

        #region private void btnOpenFile_Click(object sender, EventArgs e) 打开Excel文件
        /// <summary>
        /// 打开Excel文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtFileFullPath.Text))
                {
                    Process.Start(txtFileFullPath.Text);
                }
                else
                {
                    XtraMessageBox.Show("请选择Excel文件", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                ProcessException(exception);
            }
        }
        #endregion

        #region private void gridViewImportExcel_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e) 渲染GridControl序号
        /// <summary>
        /// 渲染GridControl序号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewImportExcel_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
            if (e.Info.IsRowIndicator)
            {
                if (e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString(CultureInfo.InvariantCulture);
                }
                else if (e.RowHandle < 0 && e.RowHandle > -1000)
                {
                    e.Info.Appearance.BackColor = Color.AntiqueWhite;
                    e.Info.DisplayText = "G" + e.RowHandle;
                }
            }
        }
        #endregion

        #region private void FrmImportExcel_FormClosing(object sender, FormClosingEventArgs e) 窗体正在关闭事件
        /// <summary>
        /// 窗体正在关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmImportExcel_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFileFullPath.Text.Trim()))
            {
                if (File.Exists(txtFileFullPath.Text))
                {
                    if (XtraMessageBox.Show(@"确定关闭当前窗体吗？", AppMessage.MSG0000, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        BillPrintHelper.SaveDefaultExcelPath(txtFileFullPath.Text);
                        DialogResult = DialogResult.OK;
                    }
                }
            }
        }
        #endregion

        private void ckTodaySend_CheckedChanged(object sender, EventArgs e)
        {
            // 是否今天发货
            BillPrintHelper.SetTodaySend(ckTodaySend.Checked);
        }

        private void ckUserDefaultSendMan_CheckedChanged(object sender, EventArgs e)
        {
            // 是否使用默认发件人
            BillPrintHelper.SetLoadDefaultSendMan(ckUserDefaultSendMan.Checked);
        }

        private void ckGetServerPrintMark_CheckedChanged(object sender, EventArgs e)
        {
            // 是否联网获取大头笔
            // BillPrintHelper.SetPrintMarkFromServer(ckGetServerPrintMark.Checked);
        }

        /// <summary>
        /// 复制Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopyExcel_Click(object sender, EventArgs e)
        {
            if (File.Exists(txtFileFullPath.Text))
            {
                var fileList = new StringCollection { txtFileFullPath.Text };
                Clipboard.SetFileDropList(fileList);
                btnCopyExcel.ShowTip("复制成功");
            }
            else
            {
                btnCopyExcel.ShowTip("源文件不存在");
            }
        }

        /// <summary>
        /// 选择发件人作为发件人来打印，网点不想总是切换默认发件人来打印，这种不好
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckChooseSendMan_CheckedChanged(object sender, EventArgs e)
        {
            if (ckChooseSendMan.Checked)
            {
                FrmChooseSendMan chooseSendMan = new FrmChooseSendMan();
                if (chooseSendMan.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(chooseSendMan.sendManControl1.ChooseId))
                    {
                        var userManager = new ZtoUserManager(BillPrintHelper.DbHelper);
                        ZtoUserEntity userEntity = userManager.GetObject(chooseSendMan.sendManControl1.ChooseId);
                        if (userEntity != null)
                        {
                            this.Tag = userEntity;
                            this.Text = this.Text + string.Format(" 正在使用({0}-{1}-{2})作为发件人来进行导入", userEntity.Realname, userEntity.Mobile, userEntity.TelePhone);
                        }
                    }
                }
                chooseSendMan.Dispose();
            }
        }
    }
}
