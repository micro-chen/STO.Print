//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using DevExpress.XtraGrid.Views.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;

namespace STO.Print.UserControl
{
    using DevExpress.Utils;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Repository;
    using DevExpress.XtraGrid.Views.Grid;
    using DevExpress.XtraGrid.Views.Grid.ViewInfo;
    using DotNet.Utilities;
    using grproLib;
    using Manager;
    using Model;
    using System.Linq;
    using System.Windows.Forms;
    using Utilities;

    /// <summary>
    /// 发件人信息操作用户控件
    /// 
    /// 修改记录
    /// 
    ///     2015-08-03  版本：1.0 YangHengLian 创建
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-08-03</date>
    /// </author>
    /// </summary>
    public partial class SendManControl : XtraUserControl
    {
        /// <summary>
        /// 定义Grid++Report报表主对象
        /// </summary>
        readonly GridppReport _report = new GridppReport();

        /// <summary>
        /// 打印实体集合
        /// </summary>
        List<ZtoPrintBillEntity> _list;

        /// <summary>
        /// 确认选中的用户ID（发件人）
        /// </summary>
        public string ChooseId;

        public SendManControl()
        {
            InitializeComponent();
            Load += BillPanel_Load;
        }

        void BillPanel_Load(object sender, EventArgs e)
        {
            SendManDataBind();
            for (int i = 1; i < gvSendMan.Columns.Count; i++)
            {
                // 可以编辑
                gvSendMan.Columns[i].AppearanceCell.Options.UseTextOptions = true;
            }
            gvSendMan.OptionsBehavior.Editable = true;
            gvSendMan.OptionsFind.AlwaysVisible = true;
            gvSendMan.OptionsFind.AlwaysVisible = true;
            gvSendMan.FindPanelVisible = true;
            // 显示查找面板
            gvSendMan.ShowFindPanel();
            _report.Initialize += ReportInitialize;
            _report.FetchRecord += ReportFetchRecord;
        }

        #region private void ReportInitialize() 初始化报表列字段对象
        private IGRField billCode, senderName, senderAddress, senderCompany, senderPhone, departure,
                         receiverName, receiverAddress, receiverCompany, receiverPhone, destination,
                         description, amount, remarks, sendTime, weight, totalMoney, bigPen, _sendSiteField;
        /// <summary>
        /// 初始化报表列字段对象
        /// </summary>
        private void ReportInitialize()
        {
            billCode = _report.FieldByName("单号");
            senderName = _report.FieldByName("寄件人姓名");
            departure = _report.FieldByName("始发地");
            senderAddress = _report.FieldByName("寄件人详址");
            senderCompany = _report.FieldByName("寄件人公司");
            senderPhone = _report.FieldByName("寄件人电话");
            receiverName = _report.FieldByName("收件人姓名");
            destination = _report.FieldByName("目的地");
            receiverAddress = _report.FieldByName("收件人详址");
            receiverCompany = _report.FieldByName("收件人公司");
            receiverPhone = _report.FieldByName("收件人电话");
            description = _report.FieldByName("品名");
            amount = _report.FieldByName("数量");
            remarks = _report.FieldByName("备注");
            sendTime = _report.FieldByName("寄件时间");
            weight = _report.FieldByName("重量");
            totalMoney = _report.FieldByName("费用总计");
            bigPen = _report.FieldByName("大头笔");
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
                foreach (ZtoPrintBillEntity billEntity in _list)
                {
                    _report.DetailGrid.Recordset.Append();
                    if (billCode != null)
                    {
                        billCode.AsString = billEntity.BillCode;
                    }
                    if (senderName != null)
                    {
                        senderName.AsString = billEntity.SendMan;
                    }
                    if (senderAddress != null)
                    {
                        senderAddress.AsString = billEntity.SendProvince + billEntity.SendCity + billEntity.SendCounty + billEntity.SendAddress;
                    }
                    if (senderCompany != null)
                    {
                        senderCompany.AsString = billEntity.SendCompany;
                    }
                    if (senderPhone != null)
                    {
                        senderPhone.AsString = billEntity.SendPhone;
                    }
                    if (departure != null)
                    {
                        departure.AsString = billEntity.SendDeparture;
                    }
                    if (receiverName != null)
                    {
                        receiverName.AsString = billEntity.ReceiveMan;
                    }
                    if (receiverAddress != null)
                    {
                        //var tempReceiveAddress = billEntity.ReceiveProvince +
                        //    billEntity.ReceiveCity +
                        //    billEntity.ReceiveCounty +
                        //    billEntity.ReceiveAddress.Replace(billEntity.ReceiveProvince, "").
                        //    Replace(billEntity.ReceiveCity, "").
                        //    Replace(billEntity.ReceiveCounty, "");
                        //receiverAddress.AsString = tempReceiveAddress;
                        receiverAddress.AsString = "";
                    }
                    if (receiverCompany != null)
                    {
                        receiverCompany.AsString = billEntity.ReceiveCompany;
                    }
                    if (receiverPhone != null)
                    {
                        receiverPhone.AsString = billEntity.ReceivePhone;
                    }
                    if (destination != null)
                    {
                        destination.AsString = billEntity.ReceiveDestination;
                    }
                    if (description != null)
                    {
                        description.AsString = billEntity.GoodsName;
                    }
                    if (amount != null)
                    {
                        amount.AsString = string.IsNullOrEmpty(billEntity.TotalNumber) ? "" : billEntity.TotalNumber;
                    }
                    if (remarks != null)
                    {
                        remarks.AsString = billEntity.Remark;
                    }
                    if (sendTime != null)
                    {
                        sendTime.AsString = billEntity.SendDate;
                    }
                    if (weight != null)
                    {
                        weight.AsString = string.IsNullOrEmpty(billEntity.Weight) ? "" : billEntity.Weight;
                    }
                    if (totalMoney != null)
                    {
                        totalMoney.AsString = string.IsNullOrEmpty(billEntity.TranFee) ? "" : billEntity.TranFee;
                    }
                    if (bigPen != null)
                    {
                        bigPen.AsString = billEntity.BigPen;
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

        private void gvSendMan_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Clicks == 1 && e.Button == MouseButtons.Left)
            {
                Point point = gvSendMan.GridControl.PointToClient(Control.MousePosition);
                GridHitInfo gridHitInfo = gvSendMan.CalcHitInfo(point);

                if (gridHitInfo.InColumn && gridHitInfo.Column.FieldName == "Check")
                {
                    if (GetCheckedCount(gvSendMan) == gvSendMan.DataRowCount)
                    {
                        UnCheckAll(gvSendMan);
                    }
                    else
                    {
                        CheckAll(gvSendMan);
                    }
                }

                if (gridHitInfo.InRow && gridHitInfo.InRowCell && gridHitInfo.Column.FieldName == "Check")
                {
                    if (Convert.ToBoolean(gvSendMan.GetFocusedRowCellValue("Check")) == false)
                    {
                        CheckSingle(gvSendMan);
                    }
                    else
                    {
                        UnCheckSingle(gvSendMan);
                    }
                }
            }
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

        private void gvSendMan_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
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

        private void gvSendMan_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            if (e.Column == (sender as GridView).Columns["Check"])
            {
                e.Info.InnerElements.Clear();
                e.Info.Appearance.ForeColor = Color.Blue;
                e.Painter.DrawObject(e.Info);
                DrawCheckBox(e.Graphics, e.Bounds, GetCheckedCount(gvSendMan) == gvSendMan.DataRowCount);
                e.Handled = true;
            }
        }

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
            DevExpress.XtraEditors.Drawing.ControlGraphicsInfoArgs args;
            RepositoryItemCheckEdit chkEdit = new RepositoryItemCheckEdit();
            info = chkEdit.CreateViewInfo() as DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo;
            painter = chkEdit.CreatePainter() as DevExpress.XtraEditors.Drawing.CheckEditPainter;
            info.EditValue = Checked;
            info.Bounds = r;
            info.PaintAppearance.ForeColor = Color.Black;
            info.CalcViewInfo(g);
            args = new DevExpress.XtraEditors.Drawing.ControlGraphicsInfoArgs(info, new DevExpress.Utils.Drawing.GraphicsCache(g), r);
            painter.Draw(args);
            args.Cache.Dispose();
        }

        private void gvSendMan_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            gvSendMan.Appearance.OddRow.BackColor = Color.White; // 设置奇数行颜色 // 默认也是白色 可以省略 
            gvSendMan.OptionsView.EnableAppearanceOddRow = true; // 使能 // 和和上面绑定 同时使用有效 
            gvSendMan.Appearance.EvenRow.BackColor = Color.FromArgb(255, 250, 205); // 设置偶数行颜色 
            gvSendMan.OptionsView.EnableAppearanceEvenRow = true; // 使能 // 和和上面绑定 同时使用有效
            if (e.RowHandle == gvSendMan.FocusedRowHandle)
            {
                e.Appearance.Font = new Font("宋体", 9, FontStyle.Bold);
            }
        }

        #region public void SendManDataBind() 发件人数据绑定
        /// <summary>
        /// 发件人数据绑定
        /// </summary>
        public void SendManDataBind()
        {
            DataTable dt = BillPrintHelper.DbHelper.Fill(BillPrintHelper.CmdStrForSendUser);
            // 增加个CheckBox列
            dt.Columns.Add("Check", typeof(bool));
            // 设置选择列的位置
            dt.Columns["Check"].SetOrdinal(0);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Check"] = false;
                var value = dt.Rows[i]["默认发件人"].ToString();
                if (!string.IsNullOrEmpty(value))
                {
                    if (value == "1")
                    {
                        dt.Rows[i]["默认发件人"] = "是";
                    }
                    else
                    {
                        dt.Rows[i]["默认发件人"] = "否";
                    }
                }
            }
            // 设置gridview列头的字体大小
            gvSendMan.Appearance.HeaderPanel.Font = new Font("Tahoma", 9);
            // 设置gridview列头居中
            gvSendMan.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;
            gcSendMan.DataSource = dt;
            gvSendMan.OptionsView.ColumnAutoWidth = false;
            gvSendMan.Columns["Id"].Visible = false;
            gvSendMan.Columns["Check"].Width = 22;
            gvSendMan.Columns["Check"].OptionsColumn.ShowCaption = false;
            gvSendMan.Columns["Check"].OptionsColumn.AllowSort = DefaultBoolean.False;
            gvSendMan.Columns["Check"].OptionsColumn.AllowEdit = false;
            gvSendMan.Columns["姓名"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            gvSendMan.Columns["姓名"].SummaryItem.DisplayFormat = @"总计：{0}";
            gvSendMan.Columns["姓名"].Width = 100;
            gvSendMan.Columns["省份"].Width = 100;
            gvSendMan.Columns["城市"].Width = 80;
            gvSendMan.Columns["区县"].Width = 80;
            gvSendMan.Columns["单位"].Width = 80;
            gvSendMan.Columns["部门"].Width = 80;
            gvSendMan.Columns["电话"].Width = 120;
            gvSendMan.Columns["手机"].Width = 120;
            gvSendMan.Columns["邮编"].Width = 80;
            gvSendMan.Columns["详细地址"].Width = 200;
            gvSendMan.Columns["备注"].Width = 150;
            // 列宽自定适应
            gvSendMan.BestFitColumns();
        }
        #endregion

        #region private void btnAddSendMan_Click(object sender, EventArgs e) 新增发件人
        /// <summary>
        /// 新增发件人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddSendMan_Click(object sender, EventArgs e)
        {
            FrmAddSendMan frmSendMan = new FrmAddSendMan();
            frmSendMan.IsReceiveForm =false;
            if (frmSendMan.ShowDialog() == DialogResult.OK)
            {
                SendManDataBind();
            }
            frmSendMan.Dispose();
        }
        #endregion

        #region private void btnDeleteSendMan_Click(object sender, EventArgs e) 删除发件人
        /// <summary>
        /// 删除发件人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteSendMan_Click(object sender, EventArgs e)
        {
            if (gvSendMan.RowCount == 0)
            {
                XtraMessageBox.Show(@"请添加发件人数据。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var list = GetCheckedUserRecord(gvSendMan);
            if (list.Any())
            {
                if (XtraMessageBox.Show(string.Format("确定删除选中的{0}条发件人记录吗？", list.Count), AppMessage.MSG0000, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                {
                    return;
                }
                using (IDbHelper dbHelper = DbHelperFactory.GetHelper(CurrentDbType.SQLite, BillPrintHelper.BillPrintConnectionString))
                {
                    try
                    {
                        dbHelper.BeginTransaction();
                        ZtoUserManager userManager = new ZtoUserManager(dbHelper);
                        var idArray = (from p in list select p.Id.ToString()).ToArray();
                        var result = userManager.Delete(idArray);
                        if (result > 0)
                        {
                            dbHelper.CommitTransaction();
                            SendManDataBind();
                            XtraMessageBox.Show(string.Format("成功删除{0}条发件人记录。", result), AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            dbHelper.RollbackTransaction();
                            XtraMessageBox.Show(@"删除失败", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch
                    {
                        dbHelper.RollbackTransaction();
                    }
                }
            }
        }
        #endregion

        #region public List<ZtoPrintBillEntity> GetCheckedRecord(GridView gridView) 获取到选择的发件人实体
        /// <summary>
        /// 获取到选择的发件人实体
        /// </summary>
        /// <param name="gridView"></param>
        /// <returns></returns>
        public List<ZtoUserEntity> GetCheckedUserRecord(GridView gridView)
        {
            var list = new List<ZtoUserEntity>();
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
                        var entity = new ZtoUserEntity
                        {
                            Id = BaseBusinessLogic.ConvertToDecimal(gridView.GetRowCellValue(rowIndex, "Id").ToString()),
                            Realname = gridView.GetRowCellValue(rowIndex, "姓名").ToString(),
                            Province = gridView.GetRowCellValue(rowIndex, "省份").ToString(),
                            City = gridView.GetRowCellValue(rowIndex, "城市").ToString(),
                            County = gridView.GetRowCellValue(rowIndex, "区县").ToString(),
                            Address = gridView.GetRowCellValue(rowIndex, "详细地址").ToString(),
                            Company = gridView.GetRowCellValue(rowIndex, "单位").ToString(),
                            Department = gridView.GetRowCellValue(rowIndex, "部门").ToString(),
                            TelePhone = gridView.GetRowCellValue(rowIndex, "电话").ToString(),
                            Mobile = gridView.GetRowCellValue(rowIndex, "手机").ToString(),
                            Postcode = gridView.GetRowCellValue(rowIndex, "邮编").ToString()
                        };
                        list.Add(entity);
                    }
                }
            }
            return list;
        }
        #endregion

        #region private void btnEditSendMan_Click(object sender, EventArgs e) 更新发件人
        /// <summary>
        /// 更新发件人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditSendMan_Click(object sender, EventArgs e)
        {
            var list = GetCheckedUserRecord(gvSendMan);
            if (list.Any())
            {
                if (list.Count > 1)
                {
                    XtraMessageBox.Show(@"请勿选择多条发件人记录。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                FrmAddSendMan frmSendMan = new FrmAddSendMan { Id = list.First().Id.ToString() };
                if (frmSendMan.ShowDialog() == DialogResult.OK)
                {
                    SendManDataBind();
                }
                frmSendMan.Dispose();
            }
            else
            {
                XtraMessageBox.Show(@"请添加发件人数据。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region private void btnRefreshSendMan_Click(object sender, EventArgs e) 刷新发件人信息
        /// <summary>
        /// 刷新发件人信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefreshSendMan_Click(object sender, EventArgs e)
        {
            SendManDataBind();
        }
        #endregion

        #region private void btnPrintSendMan_Click(object sender, EventArgs e) 批量打印发件人
        /// <summary>
        /// 批量打印发件人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrintSendManClick(object sender, EventArgs e)
        {
            if (gvSendMan.RowCount > 0)
            {
                var list = GetCheckedUserRecord(gvSendMan);
                if (list.Any())
                {
                    _list = new List<ZtoPrintBillEntity>();
                    foreach (ZtoUserEntity userEntity in list)
                    {
                        var printBillEntity = new ZtoPrintBillEntity
                        {
                            SendMan = userEntity.Realname,
                            SendProvince = userEntity.Province,
                            SendCity = userEntity.City,
                            SendCounty = userEntity.County,
                            SendPhone = userEntity.Mobile + " " + userEntity.TelePhone
                        };
                        var tempAddress = userEntity.Address;
                        if (!string.IsNullOrEmpty(tempAddress))
                        {
                            if (!string.IsNullOrEmpty(printBillEntity.SendProvince))
                            {
                                tempAddress = tempAddress.Replace(printBillEntity.SendProvince, "");
                            }
                            if (!string.IsNullOrEmpty(printBillEntity.SendCounty))
                            {
                                tempAddress = tempAddress.Replace(printBillEntity.SendCounty, "");
                            }
                            if (!string.IsNullOrEmpty(printBillEntity.SendCity))
                            {
                                tempAddress = tempAddress.Replace(printBillEntity.SendCity, "");
                            }
                        }
                        if (ckTodaySend.Checked)
                        {
                            printBillEntity.SendDate = DateTime.Now.ToString(BaseSystemInfo.DateFormat);
                        }
                        printBillEntity.SendAddress = printBillEntity.SendProvince + printBillEntity.SendCity + printBillEntity.SendCounty + tempAddress;
                        printBillEntity.SendCompany = userEntity.Company;
                        printBillEntity.SendDeparture = userEntity.Department;
                        printBillEntity.SendPostcode = userEntity.Postcode;
                        _list.Add(printBillEntity);
                    }
                    GreatReport();
                    _report.PrintPreview(true);
                }
                else
                {
                    XtraMessageBox.Show(@"请选择发件人数据。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                XtraMessageBox.Show(@"请添加发件人数据。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

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
                    FrmTemplateSetting frmTemplateSetting = new FrmTemplateSetting();
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

        #region private void btnPrintMore_Click(object sender, EventArgs e) 打印多份
        /// <summary>
        /// 打印多份
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintMore_Click(object sender, EventArgs e)
        {
            int printNumber;
            if (string.IsNullOrEmpty(cmbPrintNumber.Text.Trim()))
            {
                printNumber = 1;
            }
            else
            {
                var result = int.TryParse(cmbPrintNumber.Text, out printNumber);
                if (!result)
                {
                    printNumber = 1;
                }
            }
            if (gvSendMan.RowCount > 0)
            {
                var list = GetCheckedUserRecord(gvSendMan);
                if (list.Any())
                {
                    _list = new List<ZtoPrintBillEntity>();
                    foreach (ZtoUserEntity userEntity in list)
                    {
                        ZtoPrintBillEntity printBillEntity = new ZtoPrintBillEntity
                        {
                            SendMan = userEntity.Realname,
                            SendProvince = userEntity.Province,
                            SendCity = userEntity.City,
                            SendCounty = userEntity.County,
                            SendPhone = userEntity.Mobile + " " + userEntity.TelePhone
                        };
                        var tempAddress = userEntity.Address;
                        if (!string.IsNullOrEmpty(tempAddress))
                        {
                            if (!string.IsNullOrEmpty(printBillEntity.SendProvince))
                            {
                                tempAddress = tempAddress.Replace(printBillEntity.SendProvince, "");
                            }
                            if (!string.IsNullOrEmpty(printBillEntity.SendCounty))
                            {
                                tempAddress = tempAddress.Replace(printBillEntity.SendCounty, "");
                            }
                            if (!string.IsNullOrEmpty(printBillEntity.SendCity))
                            {
                                tempAddress = tempAddress.Replace(printBillEntity.SendCity, "");
                            }
                        }
                        if (ckTodaySend.Checked)
                        {
                            printBillEntity.SendDate = DateTime.Now.ToString(BaseSystemInfo.DateFormat);
                        }
                        printBillEntity.SendAddress = printBillEntity.SendProvince + printBillEntity.SendCity + printBillEntity.SendCounty + tempAddress;
                        printBillEntity.SendCompany = userEntity.Company;
                        printBillEntity.SendDeparture = userEntity.Department;
                        printBillEntity.SendPostcode = userEntity.Postcode;
                        for (int i = 0; i < printNumber; i++)
                        {
                            _list.Add(printBillEntity);
                        }
                    }
                    GreatReport();
                    _report.PrintPreview(true);
                }
                else
                {
                    XtraMessageBox.Show(@"请选择发件人数据。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                XtraMessageBox.Show(@"请添加发件人数据。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        private void cmbPrintNumber_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbPrintNumber.Text == "0" || string.IsNullOrEmpty(cmbPrintNumber.Text))
            {
                cmbPrintNumber.Text = "1";
            }
        }

        /// <summary>
        /// 复制单元格内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspCopyCellText_Click(object sender, EventArgs e)
        {
            if (gvSendMan.GetFocusedDataRow() == null) return;
            var text = gvSendMan.GetFocusedRowCellValue(gvSendMan.FocusedColumn.GetCaption()).ToString();
            Clipboard.SetText(text);
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspSelectAll_Click(object sender, EventArgs e)
        {
            CheckAll(gvSendMan);
        }

        /// <summary>
        /// 反选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspUnSelectAll_Click(object sender, EventArgs e)
        {
            UnCheckAll(gvSendMan);
        }

        /// <summary>
        /// 设置表格字体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspbtnSetFont_Click(object sender, EventArgs e)
        {
            FontDialog fontDlg = new FontDialog();
            fontDlg.Font = AppearanceObject.DefaultFont;
            if (fontDlg.ShowDialog() == DialogResult.OK)
            {
                foreach (AppearanceObject ap in gvSendMan.Appearance)
                {
                    ap.Font = fontDlg.Font;
                }
            }
        }

        /// <summary>
        /// 选中区域改变事件（很牛逼）2016-1-20 16:00:35
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvSendMan_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            UnCheckAll(gvSendMan);
            var t = gvSendMan.GetSelectedCells();
            foreach (GridCell gridCell in t)
            {
                gvSendMan.SetRowCellValue(gridCell.RowHandle, gvSendMan.Columns["Check"], true);
            }
        }

        /// <summary>
        /// 单元格光标离开保存修改的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvSendMan_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (e.Valid)
            {
                DataRow currentDataRow = gvSendMan.GetFocusedDataRow();
                if (currentDataRow != null)
                {
                    //gcSendMan.ShowTip("更新成功", ToolTipLocation.TopCenter);
                }
            }
        }

        private void btnChooseOk_Click(object sender, EventArgs e)
        {
            if (gvSendMan.RowCount == 0)
            {
                XtraMessageBox.Show("请添加发件人数据。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var row = gvSendMan.GetFocusedDataRow();
            ChooseId = row["Id"].ToString();
            if (this.ParentForm != null && this.ParentForm.Name == "FrmChooseSendMan")
            {
                this.ParentForm.Close();
            }
        }

        /// <summary>
        /// 设为默认发件人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspSetDefaultSendMan_Click(object sender, EventArgs e)
        {
            if (gvSendMan.RowCount > 0)
            {
                for (var rowIndex = 0; rowIndex < gvSendMan.RowCount; rowIndex++)
                {
                    var objValue = gvSendMan.GetRowCellValue(rowIndex, gvSendMan.Columns["Check"]);
                    if (objValue == null) continue;
                    bool isCheck;
                    bool.TryParse(objValue.ToString(), out isCheck);
                    if (isCheck)
                    {
                        var selectedId = BaseBusinessLogic.ConvertToDecimal(gvSendMan.GetRowCellValue(rowIndex, "Id").ToString());
                        ZtoUserManager userManager = new ZtoUserManager(BillPrintHelper.DbHelper);
                        userManager.SetProperty(new KeyValuePair<string, object>(ZtoUserEntity.FieldIssendorreceive, 1),
                                              new KeyValuePair<string, object>(ZtoUserEntity.FieldIsDefault, 0));
                        var result = userManager.SetProperty(new KeyValuePair<string, object>(ZtoUserEntity.FieldId, selectedId),
                                          new KeyValuePair<string, object>(ZtoUserEntity.FieldIsDefault, 1));
                        if (result > 0)
                        {
                            SendManDataBind();
                            XtraMessageBox.Show("设置成功", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            XtraMessageBox.Show("设置失败", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    }
                }
            }
            //var list = GetCheckedUserRecord(gvSendMan);
            //if (list.Any())
            //{
            //    if (list.Count > 1)
            //    {
            //        XtraMessageBox.Show(@"请勿选择多条发件人记录。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }
            //    ZtoUserManager userManager = new ZtoUserManager(BillPrintHelper.DbHelper);
            //    userManager.SetProperty(new KeyValuePair<string, object>(ZtoUserEntity.FieldId, list.First().Id),
            //                            new KeyValuePair<string, object>(ZtoUserEntity.FieldIsDefault, 1));
            //    var selectedId = gvSendMan.GetRowCellValue(rowIndex, gvSendMan.Columns["Id"]).ToString();

            //}
            //else
            //{
            //    XtraMessageBox.Show(@"请添加发件人数据。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        /// <summary>
        /// 导入发件人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImportSendMan_Click(object sender, EventArgs e)
        {
            //FrmImportSendManExcel importSendMan = new FrmImportSendManExcel();
            //if (importSendMan.ShowDialog() == DialogResult.OK)
            //{
            //    SendManDataBind();
            //}
            //importSendMan.Dispose();
            var importExcelForSendManOrReceiveMan = new FrmImportExcelForSendMan();
            if (importExcelForSendManOrReceiveMan.ShowDialog() == DialogResult.OK)
            {
                SendManDataBind();
            }
            importExcelForSendManOrReceiveMan.Dispose();

        }

        /// <summary>
        /// 导出发件人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportSendMan_Click(object sender, EventArgs e)
        {
            STO.Print.Utilities.ExportHelper.Export(ExportEnum.Xlsx, gcSendMan, gvSendMan);
        }

    }
}
