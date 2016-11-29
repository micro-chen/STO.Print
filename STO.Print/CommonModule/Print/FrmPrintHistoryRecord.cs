//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STO.Print
{
    using DevExpress.Utils;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Repository;
    using DevExpress.XtraGrid.Views.Base;
    using DevExpress.XtraGrid.Views.Grid;
    using DevExpress.XtraGrid.Views.Grid.ViewInfo;
    using DotNet.Utilities;
    using grproLib;
    using STO.Print.AddBillForm;
    using STO.Print.Manager;
    using STO.Print.Model;
    using STO.Print.Utilities;

    /// <summary>
    /// 打印历史记录
    ///  
    /// 修改记录
    /// 
    ///     2015-12-01  版本：1.0 YangHengLian 创建
    ///     2015-12-10  删除一些多余的按钮，只需要一点点的按钮
    ///     2016-1-26 大部分的功能都移植过来了，好多小功能在历史记录也是用到的
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-12-01</date>
    /// </author>
    /// </summary>
    public partial class FrmPrintHistoryRecord : BaseForm
    {
        /// <summary>
        /// 定义Grid++Report报表主对象
        /// </summary>
        readonly GridppReport _report = new GridppReport();

        /// <summary>
        /// 打印实体集合
        /// </summary>
        List<ZtoPrintBillEntity> _list = new List<ZtoPrintBillEntity>();

        protected delegate void UpdateControlText1();

        public FrmPrintHistoryRecord()
        {
            InitializeComponent();
        }

        #region public void GridDataBind() 打印数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void GridDataBind()
        {
            string commandText = "SELECT Id,RECEIVE_MAN AS 收件人姓名,RECEIVE_PHONE AS 收件电话,RECEIVE_PROVINCE AS 收件省份,RECEIVE_CITY AS 收件城市,RECEIVE_COUNTY AS 收件区县,RECEIVE_ADDRESS AS 收件人详细地址,BIG_PEN AS 大头笔,BILL_CODE AS 单号,ORDER_NUMBER AS 订单号,TOPAYMENT AS 到付款,GOODS_PAYMENT AS 代收货款,RECEIVE_COMPANY AS 收件人单位,RECEIVE_DESTINATION AS 目的地,RECEIVE_POSTCODE AS 收件人邮编,SEND_MAN AS 发件人姓名,SEND_PHONE AS 发件电话,SEND_DEPARTURE AS 始发地,SEND_PROVINCE AS 发件省份,SEND_CITY AS 发件城市,SEND_COUNTY AS 发件区县,SEND_ADDRESS AS 发件详细地址,SEND_DATE AS 发件日期,SEND_SITE AS 发件网点,SEND_COMPANY AS 发件单位,SEND_DEPARTMENT AS 发件部门,SEND_POSTCODE AS 发件邮编,CREATEON AS 创建时间,GOODS_NAME AS 物品类型,LENGTH AS 长,WIDTH AS 宽,HEIGHT AS 高,WEIGHT AS 重量,PAYMENT_TYPE AS 付款方式,TRAN_FEE AS 运费,TOTAL_NUMBER AS 数量,REMARK AS 备注 FROM ZTO_PRINT_BILL " + string.Format(" WHERE CREATEON BETWEEN '{0}' AND '{1}'", Convert.ToDateTime(barStartDate.EditValue).ToString(BaseSystemInfo.DateFormat), Convert.ToDateTime(barEndDate.EditValue).ToString(BaseSystemInfo.DateFormat)) + " ORDER BY CREATEON";
            if (barStartDate.EditValue == null || barEndDate.EditValue == null)
            {
                commandText = BillPrintHelper.CmdStrForZtoBillPrinter;
            }
            var dt = BillPrintHelper.BackupDbHelper.Fill(commandText);
            // 增加个CheckBox列
            dt.Columns.Add("Check", typeof(bool));
            // 设置选择列的位置
            dt.Columns["Check"].SetOrdinal(0);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Check"] = false;
            }
            // 设置gridview列头的字体大小
            gridViewBills.Appearance.HeaderPanel.Font = new Font("Tahoma", 9);
            // 设置gridview列头居中
            gridViewBills.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;
            gridControlBills.DataSource = dt;
            gridViewBills.OptionsView.ColumnAutoWidth = false;
            gridViewBills.Columns["Id"].Visible = false;
            gridViewBills.Columns["Id"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gridViewBills.Columns["Check"].Width = 22;
            gridViewBills.Columns["Check"].OptionsColumn.ShowCaption = false;
            gridViewBills.Columns["Check"].OptionsColumn.AllowSort = DefaultBoolean.False;
            gridViewBills.Columns["Check"].OptionsColumn.AllowEdit = false;
            gridViewBills.Columns["Check"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gridViewBills.Columns["收件人姓名"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            gridViewBills.Columns["收件人姓名"].SummaryItem.DisplayFormat = @"总计：{0}";
            gridViewBills.Columns["收件人姓名"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gridViewBills.Columns["单号"].Width = 100;
            gridViewBills.Columns["订单号"].Width = 120;
            // gridViewBills.Columns["单号"].Visible = false;
            gridViewBills.Columns["发件日期"].Width = 100;
            gridViewBills.Columns["创建时间"].Width = 160;
            gridViewBills.Columns["发件网点"].Width = 80;
            gridViewBills.Columns["发件人姓名"].Width = 80;
            gridViewBills.Columns["始发地"].Width = 80;
            gridViewBills.Columns["发件省份"].Width = 80;
            gridViewBills.Columns["发件城市"].Width = 80;
            gridViewBills.Columns["发件区县"].Width = 80;
            gridViewBills.Columns["发件单位"].Width = 200;
            gridViewBills.Columns["发件部门"].Width = 80;
            gridViewBills.Columns["发件电话"].Width = 100;
            gridViewBills.Columns["发件邮编"].Width = 80;
            gridViewBills.Columns["发件详细地址"].Width = 300;
            gridViewBills.Columns["收件人姓名"].Width = 80;
            gridViewBills.Columns["目的地"].Width = 80;
            gridViewBills.Columns["收件省份"].Width = 80;
            gridViewBills.Columns["收件城市"].Width = 80;
            gridViewBills.Columns["收件区县"].Width = 80;
            gridViewBills.Columns["收件电话"].Width = 100;
            gridViewBills.Columns["收件电话"].Width = 100;
            gridViewBills.Columns["收件人单位"].Width = 200;
            gridViewBills.Columns["收件人邮编"].Width = 80;
            gridViewBills.Columns["收件人详细地址"].Width = 300;
            gridViewBills.Columns["物品类型"].Width = 80;
            gridViewBills.Columns["大头笔"].Width = 80;
            gridViewBills.Columns["长"].Width = 30;
            gridViewBills.Columns["宽"].Width = 30;
            gridViewBills.Columns["高"].Width = 30;
            gridViewBills.Columns["重量"].Width = 60;
            gridViewBills.Columns["重量"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridViewBills.Columns["重量"].SummaryItem.DisplayFormat = @"{0}";
            gridViewBills.Columns["付款方式"].Width = 60;
            gridViewBills.Columns["运费"].Width = 60;
            gridViewBills.Columns["运费"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridViewBills.Columns["运费"].SummaryItem.DisplayFormat = @"{0}";
            gridViewBills.Columns["数量"].Width = 60;
            gridViewBills.Columns["数量"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridViewBills.Columns["数量"].SummaryItem.DisplayFormat = @"{0}";
            gridViewBills.Columns["备注"].Width = 100;
            gridViewBills.Columns["备注"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            gridViewBills.Columns["创建时间"].DisplayFormat.FormatString = "yyyy/MM/dd hh:mm:ss";
            gridViewBills.Columns["到付款"].Width = 60;
            gridViewBills.Columns["到付款"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridViewBills.Columns["到付款"].SummaryItem.DisplayFormat = STO.Print.Properties.Resources.FrmPrintData_GridDataBind__0_;
            gridViewBills.Columns["代收货款"].Width = 60;
            gridViewBills.Columns["代收货款"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridViewBills.Columns["代收货款"].SummaryItem.DisplayFormat = STO.Print.Properties.Resources.FrmPrintData_GridDataBind__0_;
            // 选中之后的值要重新打上勾，不然总是打勾好烦人 2016年2月2日14:36:18
            // 网点反馈的建议： 不是 勾选单子不是前面有个框要打上勾才会获取单号 但是获取单号之后 这些打勾就会自动取消 然后我还要在重新勾选一遍单子在去获取大头笔 获取完了之后勾选的又没了 还要在勾一遍才可以打印单子
            // http://biancheng.dnbcw.info/net/438728.html 灵感来自这里
            // 网点不想每次都滚动到最上面，所以这里也要控制滚动条位置的问题，杨恒连，2016年7月2日11:18:03 
            List<int> focusRowIndex = new List<int>();
            if (_list.Any())
            {
                foreach (ZtoPrintBillEntity ztoPrintBillEntity in _list)
                {
                    for (int i = 0; i < gridViewBills.DataRowCount; i++)
                    {
                        if (gridViewBills.GetRowCellValue(i, "订单号").ToString() == ztoPrintBillEntity.OrderNumber)
                        {
                            gridViewBills.SetRowCellValue(i, gridViewBills.Columns["Check"], true);
                            focusRowIndex.Add(gridViewBills.GetRowHandle(i));
                        }
                    }
                }
                //如果是全选的就滚动到最后一个就行了
                if (focusRowIndex.Count == gridViewBills.RowCount)
                {
                    gridViewBills.FocusedRowHandle = gridViewBills.RowCount - 1;
                }
                else
                {
                    if (focusRowIndex.Count > 0)
                    {
                        gridViewBills.FocusedRowHandle = focusRowIndex.First();
                    }
                    else
                    {
                        gridViewBills.FocusedRowHandle = gridViewBills.RowCount - 1;
                    }
                }
            }
            else
            {
                // 首次加载如果有数据就滚动到最后一行了，没有就在第一行
                gridViewBills.FocusedRowHandle = gridViewBills.RowCount - 1;
            }
        }
        #endregion

        private void FrmPrintHistoryRecord_Load(object sender, EventArgs e)
        {
            try
            {
                txtEndDate.MaxValue = DateTime.Now;
                txtStartDate.MaxValue = DateTime.Now;
                barStartDate.EditValue = DateTime.Now.AddDays(-5);
                barEndDate.EditValue = DateTime.Now.AddDays(1);
                var t = new Task(BindPrintData);
                t.Start();
                _report.Title = BaseSystemInfo.SoftFullName;
                // 一定要注册，否则发布后会有水印
                _report.Register(BillPrintHelper.GridReportKey);
                _report.Initialize += ReportInitialize;
                _report.FetchRecord += ReportFetchRecord;
                _report.PrintBegin += _report_PrintBegin;
                _report.PrintEnd += ReportPrintEnd;
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        void _report_PrintBegin()
        {
            // 检查打印服务是否启动了，很多的Ghost版本的windows操作系统把这服务关闭了，导致没办法打印，程序会异常
            var result = PrinterHelper.CheckServerState("RpcSs");
            var result1 = PrinterHelper.CheckServerState("Spooler");
            if (result != "服务已经启动" || result1 != "服务已经启动")
            {
                XtraMessageBox.Show("系统RpcSs或则Spooler两个打印服务未启动", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ReportPrintEnd()
        {
            var t = new Task(() =>
            {
                var frmExportBillImage = new FrmExportBillImage(_list);
                frmExportBillImage.BuildImage();
            });
            t.Start();
            // 还需要加入备份库（打印历史记录表中,重复的数据打印不要重复创建到历史记录中）
            var filePath = BillPrintHelper.GetDefaultTemplatePath();
            var templateManager = new BaseTemplateManager(BillPrintHelper.DbHelper);
            var templateList = templateManager.GetList<BaseTemplateEntity>(new KeyValuePair<string, object>(BaseTemplateEntity.FieldFilePath, filePath));
            var printBillManager = new ZtoPrintBillManager(BillPrintHelper.BackupDbHelper);
            var resultCount = printBillManager.AddHistory(_list, templateList.First());
            XtraMessageBox.Show(string.Format("打印结束，可以在历史记录中查询打印数据，新增{0}条打印记录。", resultCount), AppMessage.MSG0000, MessageBoxButtons.OK);
        }

        #region private void GreatReport() 生成Report内容
        /// <summary>
        /// 生成Report内容
        /// </summary>
        private void GreatReport()
        {
            var filePath = BillPrintHelper.GetDefaultTemplatePath();
            if (string.IsNullOrEmpty(filePath))
            {
                if (XtraMessageBox.Show(@"没有设置默认模板，是否设置？", AppMessage.MSG0000, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                {
                    var frmTemplateSetting = new FrmTemplateSetting { Owner = this };
                    if (frmTemplateSetting.ShowDialog() == DialogResult.OK)
                    {
                        var defaultTemplate = BillPrintHelper.GetDefaultTemplatePath();
                        if (!string.IsNullOrEmpty(defaultTemplate))
                        {
                            if (File.Exists(defaultTemplate))
                            {
                                _report.LoadFromFile(defaultTemplate);
                            }
                            else
                            {
                                _report.LoadFromFile(Application.StartupPath + "\\Template\\ZTOBill.grf");
                            }
                        }
                        else
                        {
                            XtraMessageBox.Show(@"没有设置默认模板，将采用系统默认模板", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _report.LoadFromFile(Application.StartupPath + "\\Template\\ZTOBill.grf");
                        }
                    }
                }
                else
                {
                    _report.LoadFromFile(Application.StartupPath + "\\Template\\ZTOBill.grf");
                }
            }
            else
            {
                if (File.Exists(filePath))
                {
                    _report.LoadFromFile(filePath);
                }
                else
                {
                    _report.LoadFromFile(Application.StartupPath + "\\Template\\ZTOBill.grf");
                }
            }
        }
        #endregion

        #region private void ReportInitialize() 初始化报表列字段对象
        private IGRField _billCode, _senderName, _senderAddress, _senderCompany, _senderPhone, _departure,
                         _receiverName, _receiverAddress, _receiverCompany, _receiverPhone, _destination,
                         _description, _amount, _remarks, _sendTime, _weight, _totalMoney, _bigPen, _pageField, _countField, _qRCodeField, _orderNumberField, _toPayMentField, _goodsPayMentField, _sendSiteField;
        /// <summary>
        /// 初始化报表列字段对象
        /// </summary>
        private void ReportInitialize()
        {
            _billCode = _report.FieldByName("单号");
            _senderName = _report.FieldByName("寄件人姓名");
            _departure = _report.FieldByName("始发地");
            _senderAddress = _report.FieldByName("寄件人详址");
            _senderCompany = _report.FieldByName("寄件人公司");
            _senderPhone = _report.FieldByName("寄件人电话");
            _receiverName = _report.FieldByName("收件人姓名");
            _destination = _report.FieldByName("目的地");
            _receiverAddress = _report.FieldByName("收件人详址");
            _receiverCompany = _report.FieldByName("收件人公司");
            _receiverPhone = _report.FieldByName("收件人电话");
            _description = _report.FieldByName("品名");
            _amount = _report.FieldByName("数量");
            _remarks = _report.FieldByName("备注");
            _sendTime = _report.FieldByName("寄件时间");
            _weight = _report.FieldByName("重量");
            _totalMoney = _report.FieldByName("费用总计");
            _bigPen = _report.FieldByName("大头笔");
            _pageField = _report.FieldByName("Page");
            _countField = _report.FieldByName("Count");
            _qRCodeField = _report.FieldByName("二维码");
            _orderNumberField = _report.FieldByName("订单号");
            _toPayMentField = _report.FieldByName("到付款");
            _goodsPayMentField = _report.FieldByName("代收货款");
            _sendSiteField = _report.FieldByName("发件网点");
        }

        /// <summary>
        /// 报表字段和数据转换函数
        /// </summary>
        private void ReportFetchRecord()
        {
            if (_list != null && _list.Count > 0)
            {
                var elecExtendInfoEntity = BillPrintHelper.GetElecUserInfoExtendEntity();
                var tempCount = 0;
                foreach (ZtoPrintBillEntity billEntity in _list)
                {
                    ++tempCount;
                    _report.DetailGrid.Recordset.Append();
                    if (_billCode != null)
                    {
                        _billCode.AsString = billEntity.BillCode;
                    }
                    if (_senderName != null)
                    {
                        _senderName.AsString = billEntity.SendMan;
                    }
                    if (_senderAddress != null)
                    {
                        _senderAddress.AsString = billEntity.SendProvince + billEntity.SendCity + billEntity.SendCounty + billEntity.SendAddress;
                    }
                    if (_senderCompany != null)
                    {
                        _senderCompany.AsString = billEntity.SendCompany;
                    }
                    if (_senderPhone != null)
                    {
                        _senderPhone.AsString = billEntity.SendPhone;
                    }
                    if (_departure != null)
                    {
                        _departure.AsString = billEntity.SendDeparture;
                    }
                    if (_receiverName != null)
                    {
                        _receiverName.AsString = billEntity.ReceiveMan;
                    }
                    if (_receiverAddress != null)
                    {
                        var tempAddress = billEntity.ReceiveAddress;
                        if (!string.IsNullOrEmpty(tempAddress))
                        {
                            if (!string.IsNullOrEmpty(billEntity.ReceiveProvince))
                            {
                                tempAddress = tempAddress.Replace(billEntity.ReceiveProvince, "");
                            }
                            if (!string.IsNullOrEmpty(billEntity.ReceiveCity))
                            {
                                tempAddress = tempAddress.Replace(billEntity.ReceiveCity, "");
                            }
                            if (!string.IsNullOrEmpty(billEntity.ReceiveCounty))
                            {
                                tempAddress = tempAddress.Replace(billEntity.ReceiveCounty, "");
                            }
                        }
                        _receiverAddress.AsString = billEntity.ReceiveProvince + billEntity.ReceiveCity + billEntity.ReceiveCounty + tempAddress;
                    }
                    if (_receiverCompany != null)
                    {
                        _receiverCompany.AsString = billEntity.ReceiveCompany;
                    }
                    if (_receiverPhone != null)
                    {
                        _receiverPhone.AsString = billEntity.ReceivePhone;
                    }
                    if (_destination != null)
                    {
                        _destination.AsString = billEntity.ReceiveDestination;
                    }
                    if (_description != null)
                    {
                        _description.AsString = billEntity.GoodsName;
                    }
                    if (_amount != null)
                    {
                        _amount.AsString = billEntity.TotalNumber;
                    }
                    if (_remarks != null)
                    {
                        _remarks.AsString = billEntity.Remark;
                    }
                    if (_sendTime != null)
                    {
                        _sendTime.AsString = billEntity.SendDate;
                    }
                    if (_weight != null)
                    {
                        _weight.AsString = billEntity.Weight;
                    }
                    //if (totalMoney != null)
                    //{
                    //    totalMoney.AsString = billEntity.TranFee;
                    //}
                    if (_bigPen != null)
                    {
                        _bigPen.AsString = billEntity.BigPen;
                    }
                    if (_countField != null)
                    {
                        _countField.AsString = _list.Count.ToString(CultureInfo.InvariantCulture);
                    }
                    if (_pageField != null)
                    {
                        _pageField.AsString = tempCount.ToString(CultureInfo.InvariantCulture);
                    }

                    if (_qRCodeField != null)
                    {

                    }
                    if (_orderNumberField != null)
                    {
                        _orderNumberField.AsString = billEntity.OrderNumber;
                    }
                    if (_toPayMentField != null)
                    {
                        _toPayMentField.AsString = billEntity.TOPAYMENT + "元";
                    }
                    if (_goodsPayMentField != null)
                    {
                        _goodsPayMentField.AsString = billEntity.GOODS_PAYMENT + "元";
                    }
                    if (_sendSiteField != null)
                    {
                        if (elecExtendInfoEntity != null)
                        {
                            // 上海(02100)
                            _sendSiteField.AsString = string.Format("{0}({1})", elecExtendInfoEntity.siteName, elecExtendInfoEntity.siteCode);
                        }
                    }
                    _report.DetailGrid.Recordset.Post();
                }
            }
        }
        #endregion

        private void BindPrintData()
        {
            var updateControl = new UpdateControlText1(UpdateControlText);
            Invoke(updateControl);
        }

        private void UpdateControlText()
        {
            GridDataBind();
            for (int i = 1; i < gridViewBills.Columns.Count; i++)
            {
                if (gridViewBills.Columns[i].FieldName == "单号" || gridViewBills.Columns[i].FieldName == "订单号" || gridViewBills.Columns[i].FieldName == "创建时间")
                {
                    // 只读
                    gridViewBills.Columns[i].OptionsColumn.ReadOnly = true;
                    // 不可编辑
                    gridViewBills.Columns[i].AppearanceCell.Options.UseTextOptions = false;
                }
                else
                {
                    // 可以编辑
                    gridViewBills.Columns[i].AppearanceCell.Options.UseTextOptions = true;
                }
            }
            gridViewBills.OptionsBehavior.Editable = true;
            gridViewBills.OptionsFind.AlwaysVisible = true;
            gridViewBills.FindPanelVisible = true;
            gridViewBills.ShowFindPanel();
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridViewBills.RowCount > 0)
            {
                _list = GetCheckedRecord(gridViewBills);
                if (_list == null || _list.Count == 0)
                {
                    XtraMessageBox.Show(@"请至少选中一条运单记录", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                GreatReport();
                _report.PrintPreview(true);
            }
            else
            {
                XtraMessageBox.Show(@"请添加运单数据。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefreshBill_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GridDataBind();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteBill_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridViewBills.RowCount > 0)
            {
                List<string> idList = new List<string>();
                for (var rowIndex = 0; rowIndex < gridViewBills.RowCount; rowIndex++)
                {
                    var objValue = gridViewBills.GetRowCellValue(rowIndex, gridViewBills.Columns["Check"]);
                    if (objValue == null) continue;
                    bool isCheck;
                    bool.TryParse(objValue.ToString(), out isCheck);
                    if (isCheck)
                    {
                        var id = gridViewBills.GetRowCellValue(rowIndex, "Id");
                        idList.Add(id.ToString());
                    }
                }
                List<ZtoPrintBillEntity> recycleBillEntities = new List<ZtoPrintBillEntity>();
                if (idList.Any())
                {
                    if (XtraMessageBox.Show(string.Format("确定删除选中的{0}条记录吗？删除之前建议将数据导出Excel", idList.Count), AppMessage.MSG0000, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    {
                        return;
                    }
                    // 写入到备份库
                    using (IDbHelper dbHelper = DbHelperFactory.GetHelper(CurrentDbType.SQLite, BillPrintHelper.BillPrintBackConnectionString))
                    {
                        try
                        {
                            dbHelper.BeginTransaction();
                            ZtoPrintBillManager manager = new ZtoPrintBillManager(dbHelper);
                            foreach (string id in idList)
                            {
                                var entity = manager.GetObject(id);
                                if (entity != null)
                                {
                                    recycleBillEntities.Add(entity);
                                }
                                manager.Delete(id);
                            }
                            dbHelper.CommitTransaction();
                            manager.DbHelper = DbHelperFactory.GetHelper(CurrentDbType.SQLite, BillPrintHelper.BillPrintConnectionString);
                            manager.CurrentTableName = "ZTO_RECYCLE_BILL";
                            foreach (var ztoPrintBillEntity in recycleBillEntities)
                            {
                                manager.AddToRecycleBill(ztoPrintBillEntity, true, false, this.Text);
                            }
                            GridDataBind();
                        }
                        catch (Exception ex)
                        {
                            dbHelper.RollbackTransaction();
                            ProcessException(ex);
                        }
                    }
                }
                else
                {
                    XtraMessageBox.Show(@"请至少选中一条运单记录。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                XtraMessageBox.Show(@"请添加运单信息。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 导出xls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport2003Excel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            STO.Print.Utilities.ExportHelper.Export(ExportEnum.Xls, gridControlBills, gridViewBills);
        }

        /// <summary>
        /// 导出xlsx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport2007Excel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            STO.Print.Utilities.ExportHelper.Export(ExportEnum.Xlsx, gridControlBills, gridViewBills);
        }

        /// <summary>
        /// 导出CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportCSV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          STO.Print.Utilities.ExportHelper.Export(ExportEnum.Csv, gridControlBills, gridViewBills);
        }

        private void gridViewBills_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (e.Valid)
            {
                // 还有点小问题，不能获取到当前编辑的行的索引
                DataRow currentDataRow = gridViewBills.GetFocusedDataRow();
                // 获取选中行的索引，不然开启线程选中行变了，就更新错误了，这是大的bug不能有
                int rowHandler = gridViewBills.FocusedRowHandle;
                if (currentDataRow != null)
                {
                    // 进行更新动作
                    var printBillManager = new ZtoPrintBillManager(BillPrintHelper.DbHelper);
                    var updateColumnName = BillPrintHelper.GetFieldByName(gridViewBills.FocusedColumn.FieldName);
                    printBillManager.SetProperty(new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldId, currentDataRow["Id"]), new KeyValuePair<string, object>(updateColumnName, e.Value));
                    gridControlBills.ShowTip(gridViewBills.FocusedColumn.FieldName + "更新成功", ToolTipLocation.TopCenter);
                    #region 更新看看省市区和大头笔的信息
                    // 如果改了收件人的详细地址
                    if (gridViewBills.FocusedColumn.FieldName == "收件人详细地址" && currentDataRow["收件人详细地址"].ToString() != e.Value.ToString())
                    {
                        // 开线程来操作，这个动作还是比较耗时间的，不然有卡顿了，2016年1月30日16:37:11
                        var t = new Task(delegate
                        {
                            // 最好更新收件的省市区和大头笔，减少操作步骤
                            var result = BaiduMapHelper.GetProvCityDistFromBaiduMap(e.Value.ToString());
                            if (result != null)
                            {
                                // 进行更新选中记录的（收件）省市区（包括省市区的ID）
                                var updateParameters = new List<KeyValuePair<string, object>>
                                                                                 {
                                                                                     new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldReceiveProvince, result.Result.AddressComponent.Province.Trim()),
                                                                                     new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldReceiveCity, result.Result.AddressComponent.City.Trim()),
                                                                                     new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldReceiveCounty,result.Result.AddressComponent.District.Trim()),
                                                                                     new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldModifiedOn, DateTime.Now)
                                                                                 };
                                printBillManager.SetProperty(new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldId, currentDataRow["Id"]), updateParameters);
                                BeginInvoke(new Action(() =>
                                {
                                    gridViewBills.SetRowCellValue(rowHandler, "收件省份", result.Result.AddressComponent.Province.Trim());
                                    gridViewBills.SetRowCellValue(rowHandler, "收件城市", result.Result.AddressComponent.City.Trim());
                                    gridViewBills.SetRowCellValue(rowHandler, "收件区县", result.Result.AddressComponent.District.Trim());
                                }));
                                // 在更新大头笔
                                var selectedRemark = new List<string> { currentDataRow["发件省份"].ToString(), currentDataRow["发件城市"].ToString(), currentDataRow["发件区县"].ToString() };
                                var selectedReceiveMark = new List<string> { result.Result.AddressComponent.Province.Trim(), result.Result.AddressComponent.City.Trim(), result.Result.AddressComponent.District.Trim() };
                                var printMark = BillPrintHelper.GetRemaike(string.Join(",", selectedRemark), currentDataRow["发件详细地址"].ToString(), string.Join(",", selectedReceiveMark), e.Value.ToString());
                                // 根据记录的ID更新大头笔字段
                                printBillManager.SetProperty(new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldId, currentDataRow["Id"]), new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldBigPen, printMark));
                                BeginInvoke(new Action(() => gridViewBills.SetRowCellValue(rowHandler, "大头笔", printMark)));
                            }
                        });
                        t.Start();
                    }
                    // 如果改了发件人的详细地址
                    if (gridViewBills.FocusedColumn.FieldName == "发件详细地址" && currentDataRow["发件详细地址"].ToString() != e.Value.ToString())
                    {
                        var t = new Task(delegate
                        {
                            // 最好更新发件的省市区和大头笔，减少操作步骤
                            var result = BaiduMapHelper.GetProvCityDistFromBaiduMap(e.Value.ToString());
                            if (result != null)
                            {
                                // 进行更新选中记录的（发件）省市区（包括省市区的ID）
                                var updateParameters1 = new List<KeyValuePair<string, object>>
                                                                                  {
                                                                                      new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldSendProvince, result.Result.AddressComponent.Province.Trim()),
                                                                                      new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldSendCity, result.Result.AddressComponent.City.Trim()),
                                                                                      new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldSendCounty, result.Result.AddressComponent.District.Trim()),
                                                                                      new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldModifiedOn, DateTime.Now)
                                                                                  };
                                printBillManager.SetProperty(new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldId, currentDataRow["Id"]), updateParameters1);
                                BeginInvoke(new Action(() =>
                                {
                                    gridViewBills.SetRowCellValue(rowHandler, "发件省份", result.Result.AddressComponent.Province.Trim());
                                    gridViewBills.SetRowCellValue(rowHandler, "发件城市", result.Result.AddressComponent.City.Trim());
                                    gridViewBills.SetRowCellValue(rowHandler, "发件区县", result.Result.AddressComponent.District.Trim());
                                }));
                            }
                        });
                        t.Start();
                    }
                    #endregion
                }
            }
        }

        /// <summary>
        /// 快速打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuickPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridViewBills.RowCount > 0)
            {
                _list = GetCheckedRecord(gridViewBills);
                if (_list == null || _list.Count == 0)
                {
                    XtraMessageBox.Show(@"请至少选中一条运单记录", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                GreatReport();
                var defaultPrinter = BillPrintHelper.GetDefaultPrinter();
                if (!string.IsNullOrEmpty(defaultPrinter))
                {
                    _report.Printer.PrinterName = defaultPrinter;
                }
                else
                {
                    if (XtraMessageBox.Show(@"没有设置默认打印机，立刻设置", AppMessage.MSG0000, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                    {
                        FrmPrinterSetting frmPrinterSetting = new FrmPrinterSetting() { Owner = this };
                        if (frmPrinterSetting.ShowDialog() == DialogResult.OK)
                        {
                            var defaultPrinter1 = BillPrintHelper.GetDefaultPrinter();
                            if (!string.IsNullOrEmpty(defaultPrinter1))
                            {
                                _report.Printer.PrinterName = defaultPrinter;
                            }
                        }
                    }
                }
                if (XtraMessageBox.Show(string.Format("是否立刻打印{0}条记录？打印电子面单请一定检查是否有单号和大头笔，防止浪费热敏纸，普通面单检查是否有大头笔，防止浪费面单，土豪随意。", _list.Count), AppMessage.MSG0000, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                {
                    _report.PrintEx(GRPrintGenerateStyle.grpgsPreviewAll, false);
                }
            }
            else
            {
                XtraMessageBox.Show(@"请添加打印数据", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #region public List<ZtoPrintBillEntity> GetCheckedRecord(GridView gridView) 获取到选择的运单实体
        /// <summary>
        /// 获取到选择的运单实体
        /// </summary>
        /// <param name="gridView"></param>
        /// <returns></returns>
        public List<ZtoPrintBillEntity> GetCheckedRecord(GridView gridView)
        {
            var list = new List<ZtoPrintBillEntity>();
            if (gridView.RowCount > 0)
            {
                for (var rowIndex = 0; rowIndex < gridView.RowCount; rowIndex++)
                {
                    var objValue = gridView.GetRowCellValue(rowIndex, gridView.Columns["Check"]);
                    if (objValue == null) continue;
                    bool isCheck;
                    bool.TryParse(objValue.ToString(), out isCheck);
                    if (isCheck)
                    {
                        var entity = new ZtoPrintBillEntity
                        {
                            BillCode = gridView.GetRowCellValue(rowIndex, "单号").ToString(),
                            SendMan = gridView.GetRowCellValue(rowIndex, "发件人姓名").ToString(),
                            SendAddress = gridView.GetRowCellValue(rowIndex, "发件详细地址").ToString(),
                            SendCompany = gridView.GetRowCellValue(rowIndex, "发件单位").ToString(),
                            SendPhone = gridView.GetRowCellValue(rowIndex, "发件电话").ToString()
                        };
                        entity.SendDeparture = gridView.GetRowCellValue(rowIndex, "始发地").ToString();
                        entity.ReceiveMan = gridView.GetRowCellValue(rowIndex, "收件人姓名").ToString();
                        entity.ReceiveAddress = gridView.GetRowCellValue(rowIndex, "收件人详细地址").ToString();
                        entity.ReceiveCompany = gridView.GetRowCellValue(rowIndex, "收件人单位").ToString();
                        entity.ReceivePhone = gridView.GetRowCellValue(rowIndex, "收件电话").ToString();
                        entity.ReceiveProvince = gridView.GetRowCellValue(rowIndex, "收件省份").ToString();
                        entity.ReceiveCity = gridView.GetRowCellValue(rowIndex, "收件城市").ToString();
                        entity.ReceiveCounty = gridView.GetRowCellValue(rowIndex, "收件区县").ToString();
                        entity.ReceiveDestination = gridView.GetRowCellValue(rowIndex, "目的地").ToString();
                        entity.GoodsName = gridView.GetRowCellValue(rowIndex, "物品类型").ToString();
                        entity.TotalNumber = gridView.GetRowCellValue(rowIndex, "数量").ToString();
                        entity.SendDate = gridView.GetRowCellValue(rowIndex, "发件日期").ToString();
                        entity.SendSite = BaseBusinessLogic.ConvertToString(gridView.GetRowCellValue(rowIndex, "发件网点"));
                        entity.Weight = gridView.GetRowCellValue(rowIndex, "重量").ToString();
                        entity.TranFee = gridView.GetRowCellValue(rowIndex, "运费").ToString();
                        entity.SendProvince = gridView.GetRowCellValue(rowIndex, "发件省份").ToString();
                        entity.SendCity = gridView.GetRowCellValue(rowIndex, "发件城市").ToString();
                        entity.SendCounty = gridView.GetRowCellValue(rowIndex, "发件区县").ToString();
                        entity.BigPen = gridView.GetRowCellValue(rowIndex, "大头笔").ToString();
                        entity.Id = Convert.ToDecimal(gridView.GetRowCellValue(rowIndex, "Id").ToString());
                        entity.OrderNumber = gridView.GetRowCellValue(rowIndex, "订单号").ToString();
                        entity.TOPAYMENT = string.IsNullOrEmpty(gridView.GetRowCellValue(rowIndex, "到付款").ToString()) ? 0 : Convert.ToDecimal(gridView.GetRowCellValue(rowIndex, "到付款").ToString());
                        entity.GOODS_PAYMENT = string.IsNullOrEmpty(gridView.GetRowCellValue(rowIndex, "代收货款").ToString()) ? 0 : Convert.ToDecimal(gridView.GetRowCellValue(rowIndex, "代收货款").ToString());
                        entity.CreateOn = BaseBusinessLogic.ConvertToDateTime(gridView.GetRowCellValue(rowIndex, "创建时间"));
                        entity.ReceivePostcode = BaseBusinessLogic.ConvertToString(gridView.GetRowCellValue(rowIndex, "收件人邮编"));
                        entity.SendPostcode = BaseBusinessLogic.ConvertToString(gridView.GetRowCellValue(rowIndex, "发件邮编"));
                        entity.Length = BaseBusinessLogic.ConvertToString(gridView.GetRowCellValue(rowIndex, "长"));
                        entity.Width = BaseBusinessLogic.ConvertToString(gridView.GetRowCellValue(rowIndex, "宽"));
                        entity.Height = BaseBusinessLogic.ConvertToString(gridView.GetRowCellValue(rowIndex, "高"));
                        entity.PaymentType = BaseBusinessLogic.ConvertToString(gridView.GetRowCellValue(rowIndex, "付款方式"));
                        entity.Remark = BaseBusinessLogic.ConvertToString(gridView.GetRowCellValue(rowIndex, "备注"));
                        // 发件日期如果为空就用当前的时间作为发件日期
                        if (string.IsNullOrEmpty(entity.SendDate))
                        {
                            entity.SendDate = DateTime.Now.ToString(BaseSystemInfo.DateFormat);
                        }
                        list.Add(entity);
                    }
                }
            }
            return list;
        }
        #endregion

        /// <summary>
        /// 画GridControl首列复选框
        /// </summary>
        /// <param name="g"></param>
        /// <param name="r"></param>
        /// <param name="Checked"></param>
        protected void DrawCheckBox(Graphics g, Rectangle r, bool Checked)
        {
            DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo info;
            DevExpress.XtraEditors.Drawing.CheckEditPainter painter;
            DevExpress.XtraEditors.Drawing.ControlGraphicsInfoArgs args = null;
            var chkEdit = new RepositoryItemCheckEdit();
            info = chkEdit.CreateViewInfo() as DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo;
            painter = chkEdit.CreatePainter() as DevExpress.XtraEditors.Drawing.CheckEditPainter;
            if (info != null)
            {
                info.EditValue = Checked;
                info.Bounds = r;
                info.PaintAppearance.ForeColor = Color.Black;
                info.CalcViewInfo(g);
                args = new DevExpress.XtraEditors.Drawing.ControlGraphicsInfoArgs(info, new DevExpress.Utils.Drawing.GraphicsCache(g), r);
            }
            painter.Draw(args);
            args.Cache.Dispose();
        }

        /// <summary>
        /// 获取到已选择的数量
        /// </summary>
        /// <returns></returns>
        public int GetCheckedCount(GridView gridViewBills)
        {
            int count = 0;
            for (int i = 0; i < gridViewBills.DataRowCount; i++)
            {
                if ((bool)gridViewBills.GetRowCellValue(i, gridViewBills.Columns["Check"]))
                {
                    count++;
                }
            }
            return count;
        }

        private void gridViewBills_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 1 && e.Button == MouseButtons.Left)
            {
                Point point = gridViewBills.GridControl.PointToClient(MousePosition);
                GridHitInfo gridHitInfo = gridViewBills.CalcHitInfo(point);

                if (gridHitInfo.InColumn && gridHitInfo.Column.FieldName == "Check")
                {
                    if (GetCheckedCount(gridViewBills) == gridViewBills.DataRowCount)
                    {
                        UnCheckAll(gridViewBills);
                    }
                    else
                    {
                        CheckAll(gridViewBills);
                    }
                }

                if (gridHitInfo.InRow && gridHitInfo.InRowCell && gridHitInfo.Column.FieldName == "Check")
                {
                    if (Convert.ToBoolean(gridViewBills.GetFocusedRowCellValue("Check")) == false)
                    {
                        CheckSingle(gridViewBills);
                    }
                    else
                    {
                        UnCheckSingle(gridViewBills);
                    }
                }
            }
        }

        /// <summary>
        /// 选择全部 
        /// </summary>
        private void CheckAll(GridView gridViewBills)
        {
            for (int i = 0; i < gridViewBills.DataRowCount; i++)
            {
                gridViewBills.SetRowCellValue(i, gridViewBills.Columns["Check"], true);
            }
        }

        /// <summary>
        /// 反选全部
        /// </summary>
        private void UnCheckAll(GridView gridViewBills)
        {
            for (int i = 0; i < gridViewBills.DataRowCount; i++)
            {
                gridViewBills.SetRowCellValue(i, gridViewBills.Columns["Check"], false);
            }
        }

        /// <summary>
        /// 选择一个
        /// </summary>
        private void CheckSingle(GridView gridViewBills)
        {
            gridViewBills.SetRowCellValue(gridViewBills.FocusedRowHandle, gridViewBills.Columns["Check"], true);
        }

        /// <summary>
        /// 反选一个
        /// </summary>
        private void UnCheckSingle(GridView gridViewBills)
        {
            gridViewBills.SetRowCellValue(gridViewBills.FocusedRowHandle, gridViewBills.Columns["Check"], false);
        }

        private void gridViewBills_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            if (e.Column == (sender as GridView).Columns["Check"])
            {
                e.Info.InnerElements.Clear();
                e.Info.Appearance.ForeColor = Color.Blue;
                e.Painter.DrawObject(e.Info);
                DrawCheckBox(e.Graphics, e.Bounds, GetCheckedCount(gridViewBills) == gridViewBills.DataRowCount);
                e.Handled = true;
            }
        }

        private void gridViewBills_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
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

        private void gridViewBills_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            UnCheckAll(gridViewBills);
            var t = gridViewBills.GetSelectedCells();
            foreach (GridCell gridCell in t)
            {
                gridViewBills.SetRowCellValue(gridCell.RowHandle, gridViewBills.Columns["Check"], true);
            }
        }

        /// <summary>
        /// 取消电子面单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelElecBillCode_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                ZtoElecUserInfoEntity elecUserInfoEntity = BillPrintHelper.GetElecUserInfoEntity();
                if (elecUserInfoEntity != null)
                {
                    _list = GetCheckedRecord(gridViewBills);
                    FrmBackElecBillByOrderNumber frmBackElecBill = null;
                    if (_list == null || _list.Count == 0)
                    {
                        frmBackElecBill = new FrmBackElecBillByOrderNumber(true);
                    }
                    else
                    {
                        frmBackElecBill = new FrmBackElecBillByOrderNumber(_list, true);
                    }
                    if (frmBackElecBill.ShowDialog() == DialogResult.OK)
                    {
                        GridDataBind();
                    }
                }
                else
                {
                    // 在默认发件人那边修改个人的商家ID信息
                    // 获取系统是否有默认发件人，如果有救修改，如果没有就新增
                    ZtoUserManager userManager = new ZtoUserManager(BillPrintHelper.DbHelper);
                    var userList = userManager.GetList<ZtoUserEntity>(new KeyValuePair<string, object>(ZtoUserEntity.FieldIsDefault, 1), new KeyValuePair<string, object>(ZtoUserEntity.FieldIssendorreceive, 1));
                    if (!userList.Any())
                    {
                        XtraMessageBox.Show("系统未绑定默认发件人和商家ID，请进行绑定", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FrmAddSendMan addSendMan = new FrmAddSendMan();
                        addSendMan.ShowDialog();
                        addSendMan.Dispose();
                    }
                    else
                    {
                        XtraMessageBox.Show("默认发件人未绑定商家ID，请进行绑定", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FrmAddSendMan frmSendMan = new FrmAddSendMan { Id = userList.First().Id.ToString() };
                        frmSendMan.ShowDialog();
                        frmSendMan.Dispose();
                    }
                }
            }
            catch (Exception exception)
            {
                ProcessException(exception);
            }
        }

        /// <summary>
        /// 查询备份库的历史打印记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GridDataBind();
        }

        /// <summary>
        /// 打印报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintBillChart_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FrmPrintBillChart frmPrintBillChart = new FrmPrintBillChart();
            //frmPrintBillChart.ShowDialog();
            ChildFormManagementHelper.LoadMdiForm(this.ParentForm, "FrmPrintBillChart");
        }

        /// <summary>
        /// 复制单元格选中值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspCopyCellText_Click(object sender, EventArgs e)
        {
            if (gridViewBills.GetFocusedDataRow() == null) return;
            var text = gridViewBills.GetFocusedRowCellValue(gridViewBills.FocusedColumn.GetCaption()).ToString();
            Clipboard.SetText(text);
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspSelectAll_Click(object sender, EventArgs e)
        {
            CheckAll(gridViewBills);
        }

        /// <summary>
        /// 反选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspUnSelectAll_Click(object sender, EventArgs e)
        {
            UnCheckAll(gridViewBills);
        }

        /// <summary>
        ///  打开保存电子面单底单文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspOpenElecFolder_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(BillPrintHelper.SaveFilePath))
            {
                Directory.CreateDirectory(BillPrintHelper.SaveFilePath);
            }
            Process.Start(BillPrintHelper.SaveFilePath);
        }

        /// <summary>
        /// 查看可用电子面单数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspGetElecBillCount_Click(object sender, EventArgs e)
        {
            ZtoElecUserInfoEntity elecUserInfoEntity = BillPrintHelper.GetElecUserInfoEntity();
            try
            {
                if (elecUserInfoEntity != null)
                {
                    var result = ZtoElecBillHelper.GetElecBillCount(elecUserInfoEntity);
                    int elecBillCount = 0;
                    if (int.TryParse(result, out elecBillCount))
                    {
                        XtraMessageBox.Show(string.Format("商家ID为：{1}可用线下电子面单数量为：{0}条", result, elecUserInfoEntity.Kehuid), AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show(result, AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // 在默认发件人那边修改个人的商家ID信息
                    // 获取系统是否有默认发件人，如果有救修改，如果没有就新增
                    ZtoUserManager userManager = new ZtoUserManager(BillPrintHelper.DbHelper);
                    var userList = userManager.GetList<ZtoUserEntity>(new KeyValuePair<string, object>(ZtoUserEntity.FieldIsDefault, 1), new KeyValuePair<string, object>(ZtoUserEntity.FieldIssendorreceive, 1));
                    if (!userList.Any())
                    {
                        XtraMessageBox.Show("系统未绑定默认发件人和商家ID，请进行绑定", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FrmAddSendMan addSendMan = new FrmAddSendMan();
                        addSendMan.ShowDialog();
                        addSendMan.Dispose();
                    }
                    else
                    {
                        XtraMessageBox.Show("默认发件人未绑定商家ID，请进行绑定", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FrmAddSendMan frmSendMan = new FrmAddSendMan { Id = userList.First().Id.ToString() };
                        frmSendMan.ShowDialog();
                        frmSendMan.Dispose();
                    }
                }
            }
            catch (Exception exception)
            {
                ProcessException(exception);
            }
        }

        /// <summary>
        /// 再次打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintAgain_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridViewBills.RowCount > 0)
            {
                _list = GetCheckedRecord(gridViewBills);
                if (_list != null && _list.Any())
                {
                    var addPrintBySelectedRow = new FrmAddPrintBySelectedRow(_list, false);
                    if (addPrintBySelectedRow.ShowDialog() == DialogResult.OK)
                    {
                        GridDataBind();
                    }
                    addPrintBySelectedRow.Dispose();
                }
            }
        }

        /// <summary>
        /// 获取申通电子面单号码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetElecBill_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                ZtoElecUserInfoEntity elecUserInfoEntity = BillPrintHelper.GetElecUserInfoEntity();
                if (elecUserInfoEntity != null)
                {
                    _list = GetCheckedRecord(gridViewBills);
                    if (_list == null || _list.Count == 0)
                    {
                        XtraMessageBox.Show(@"请至少选中一条运单记录", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // 需要检查已经存在的单号的订单号（订单号和单号都有了就不要再去请求了，请求等于浪费带宽，增加接口的压力）
                    for (int k = 0; k < _list.Count; k++)
                    {
                        var ztoPrintBillEntity = _list[k];
                        if (!string.IsNullOrEmpty(ztoPrintBillEntity.OrderNumber) && !string.IsNullOrEmpty(ztoPrintBillEntity.BillCode))
                        {
                            _list.Remove(ztoPrintBillEntity);
                            k--;
                        }
                    }

                    if (_list.Count == 0)
                    {
                        MessageUtil.ShowTips("选中记录都已经获取到单号了，建议不要重复获取");
                        return;
                    }

                    var result = ZtoElecBillHelper.GetElecBillCount(elecUserInfoEntity);
                    int elecBillCount;
                    if (int.TryParse(result, out elecBillCount))
                    {
                        //if (elecBillCount < _list.Count)
                        //{
                        //    XtraMessageBox.Show(string.Format("当前可用电子面单数量为：{0}条，需要打印数量为：{1}条，可用单号不足，请到物料系统充值。", elecBillCount, _list.Count), AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    ToolHelper.OpenBrowserUrl("https://sso.zt-express.com/?SystemCode=WULIAO&openId=false");
                        //    return;
                        //}
                    }
                    else
                    {
                        XtraMessageBox.Show(result, AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (XtraMessageBox.Show(string.Format("当前可用电子面单数量为：{1},确定获取{0}条电子面单吗？一次性获取不建议超过100条，网络也需要好", _list.Count, elecBillCount), AppMessage.MSG0000, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    {
                        return;
                    }
                    var list = ZtoElecBillHelper.BindElecBillByCustomerId(_list, elecUserInfoEntity);
                    if (list != null && list.Any())
                    {
                        int tempCount = 0;
                        // 更新数据库
                        var printBillManager = new ZtoPrintBillManager(BillPrintHelper.BackupDbHelper);
                        foreach (var billEntity in list)
                        {
                            ++tempCount;
                            // 进行更新选中记录的（收件）省市区（包括省市区的ID）
                            var updateParameters = new List<KeyValuePair<string, object>>
                            {
                                new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldBigPen,billEntity.BigPen),
                                new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldOrderNumber,billEntity.OrderNumber),
                                new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldBillCode,billEntity.BillCode),
                                new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldModifiedOn, DateTime.Now)
                            };
                            var whereParameters = new List<KeyValuePair<string, object>>
                            {
                                new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldId, billEntity.Id)
                            };
                            printBillManager.SetProperty(whereParameters, updateParameters);
                        }
                        if (tempCount > 0)
                        {
                            XtraMessageBox.Show(@"获取成功" + tempCount + "条电子面单。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            XtraMessageBox.Show(@"获取失败。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show(@"全部获取失败。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    GridDataBind();
                }
                else
                {
                    // 在默认发件人那边修改个人的商家ID信息
                    // 获取系统是否有默认发件人，如果有救修改，如果没有就新增
                    ZtoUserManager userManager = new ZtoUserManager(BillPrintHelper.DbHelper);
                    var userList = userManager.GetList<ZtoUserEntity>(new KeyValuePair<string, object>(ZtoUserEntity.FieldIsDefault, 1), new KeyValuePair<string, object>(ZtoUserEntity.FieldIssendorreceive, 1));
                    if (!userList.Any())
                    {
                        XtraMessageBox.Show("系统未绑定默认发件人和商家ID，请进行绑定", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FrmAddSendMan addSendMan = new FrmAddSendMan();
                        addSendMan.ShowDialog();
                        addSendMan.Dispose();
                    }
                    else
                    {
                        XtraMessageBox.Show("默认发件人未绑定商家ID，请进行绑定", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FrmAddSendMan frmSendMan = new FrmAddSendMan { Id = userList.First().Id.ToString() };
                        frmSendMan.ShowDialog();
                        frmSendMan.Dispose();
                    }
                }
            }
            catch (Exception exception)
            {
                ProcessException(exception);
                GridDataBind();
            }
        }
    }
}
