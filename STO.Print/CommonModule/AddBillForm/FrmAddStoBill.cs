//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace STO.Print.AddBillForm
{
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using DotNet.Business;
    using DotNet.Model;
    using DotNet.Utilities;
    using grproLib;
    using STO.Print.Manager;
    using STO.Print.Model;
    using STO.Print.Utilities;

    /// <summary>
    /// 申通面单窗体
    /// 
    /// 修改记录
    /// 
    ///     2015-07-10  版本：1.0 YangHengLian 创建
    ///     2015-07-20  YangHengLian 完善注释
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-07-18</date>
    /// </author>
    /// </summary>
    public partial class FrmAddStoBill : BaseForm
    {
        readonly GridppReport _report = new GridppReport();

        readonly ZtoPrintBillManager _printBillManager = new ZtoPrintBillManager(BillPrintHelper.DbHelper);

        List<ZtoPrintBillEntity> _list;

        public FrmAddStoBill()
        {
            InitializeComponent();
        }

        #region private void Konad_FormClosing(object sender, FormClosedEventArgs e) 窗体关闭
        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Konad_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool haveInput = false;
            haveInput = !string.IsNullOrEmpty(txtSendMan.Text) || !string.IsNullOrEmpty(dgvSearchSendArea.Text) ||
                        !string.IsNullOrEmpty(txtSendAddress.Text) || !string.IsNullOrEmpty(txtSendCompany.Text) ||
                        !string.IsNullOrEmpty(txtSendPhone.Text) || !string.IsNullOrEmpty(txtSendDeparture.Text) ||
                        !string.IsNullOrEmpty(txtReceiveMan.Text) || !string.IsNullOrEmpty(txtReceiveCompany.Text) ||
                        !string.IsNullOrEmpty(txtReceiveAddress.Text) || !string.IsNullOrEmpty(dgvSearchReceiveArea.Text) ||
                        !string.IsNullOrEmpty(txtReceiveDestination.Text) || !string.IsNullOrEmpty(txtReceivePhone.Text);

            if (haveInput)
            {
                if (XtraMessageBox.Show("确定关闭录单吗？", AppMessage.MSG0000, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                {
                    DialogResult = DialogResult.Abort;
                    e.Cancel = true;
                }
                else
                {
                    txtSendMan.Text = "";
                    txtSendAddress.Text = "";
                    txtSendCompany.Text = "";
                    txtSendPhone.Text = "";
                    txtSendDeparture.Text = "";
                    dgvSearchSendArea.Text = "";
                    txtReceiveMan.Text = "";
                    txtReceiveAddress.Text = "";
                    txtReceiveDestination.Text = "";
                    txtReceivePhone.Text = "";
                    txtReceiveCompany.Text = "";
                    dgvSearchReceiveArea.Text = "";
                }
            }
            DialogResult = DialogResult.OK;
        }
        #endregion

        #region public void ClearText() 清空文本框

        /// <summary>
        /// 清空文本框
        /// </summary>
        public void ClearText()
        {
            if (ckGetDefaultSendMan.Checked)
            {
                txtReceiveMan.Text = string.Empty;
                txtReceivePhone.Text = string.Empty;
                txtReceiveDestination.Text = string.Empty;
                txtReceiveCompany.Text = string.Empty;
                txtReceiveAddress.Text = string.Empty;
                dgvSearchReceiveArea.Text = string.Empty;
            }
            else
            {
                foreach (Control ctr in panel1.Controls)
                {
                    // 清空DevExpress文本框
                    if (ctr is TextEdit)//判断是否是Dev的TextEdit控件
                    {
                        var textEdit = ctr as TextEdit;
                        textEdit.Text = string.Empty;
                    }
                    // 清空winform文本框
                    if (ctr is TextBox)//判断是否是普通Winform的TextBox控件
                    {
                        var textBox = ctr as TextBox;
                        textBox.Text = string.Empty;
                    }
                }
                dgvSearchSendArea.Text = string.Empty;
                dgvSearchReceiveArea.Text = string.Empty;
            }
        }
        #endregion

        #region private void btnSave_Click(object sender, EventArgs e) 保存本地
        /// <summary>
        /// 保存本地
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            var entity = BuildPrintBillEntity();
            if (entity != null)
            {
                string result;
                if (!string.IsNullOrEmpty(SelectedId))
                {
                    entity.Id = Convert.ToDecimal(SelectedId);
                    // 更新
                    result = _printBillManager.Update(entity).ToString();
                }
                else
                {
                    result = _printBillManager.Add(entity, true);
                }
                if (!string.IsNullOrEmpty(result))
                {
                    XtraMessageBox.Show(@"保存成功", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (string.IsNullOrEmpty(SelectedId))
                    {
                        if (ckKeep.Checked)
                        {
                            ClearText();
                        }
                    }
                    if (ckGetDefaultSendMan.Checked)
                    {
                        txtReceiveMan.Focus();
                        txtReceiveMan.Select();
                    }
                    else
                    {
                        txtSendMan.Focus();
                    }
                }
                else
                {
                    XtraMessageBox.Show("保存失败", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region private ZtoPrintBillEntity BuildPrintBillEntity() 构建打印实体
        /// <summary>
        /// 构建打印实体
        /// </summary>
        /// <returns></returns>
        private ZtoPrintBillEntity BuildPrintBillEntity()
        {
            var printBillEntity = new ZtoPrintBillEntity
            {
                SendMan = txtSendMan.Text.Trim(),
                SendDeparture = txtSendDeparture.Text.Trim(),
                SendAddress = txtSendAddress.Text.Trim(),
                SendPhone = txtSendPhone.Text.Trim(),
                SendCompany = txtSendCompany.Text.Trim(),
                SendDate = "",
                ReceiveMan = txtReceiveMan.Text.Trim(),
                ReceiveDestination = txtReceiveDestination.Text.Trim(),
                ReceiveAddress = txtReceiveAddress.Text.Trim(),
                ReceiveCompany = txtReceiveCompany.Text.Trim(),
                ReceivePhone = txtReceivePhone.Text.Trim(),
                TotalNumber = string.IsNullOrEmpty(txtNumber.Text.Trim()) ? "" : txtNumber.Text,
                Weight = string.IsNullOrEmpty(txtWeight.Text.Trim()) ? "" : txtWeight.Text,
                TranFee = "",
                GoodsName = txtGoodsName.Text,
            };
            if (ckTodaySend.Checked)
            {
                printBillEntity.SendDate = DateTime.Now.ToString(BaseSystemInfo.DateFormat);
            }
            else
            {
                printBillEntity.SendDate = txtSendDate.Text;
            }
            if (!string.IsNullOrEmpty(dgvSearchSendArea.Text.Trim()))
            {
                var sendAreaArray = dgvSearchSendArea.Text.Split('-');
                printBillEntity.SendProvince = sendAreaArray[0];
                printBillEntity.SendCity = sendAreaArray[1];
                printBillEntity.SendCounty = sendAreaArray[2];
            }
            if (!string.IsNullOrEmpty(dgvSearchReceiveArea.Text.Trim()))
            {
                var receiveAreaArray = dgvSearchReceiveArea.Text.Split('-');
                printBillEntity.ReceiveProvince = receiveAreaArray[0];
                printBillEntity.ReceiveCity = receiveAreaArray[1];
                printBillEntity.ReceiveCounty = receiveAreaArray[2];
            }
            return printBillEntity;
        }
        #endregion

        #region private void btnPrint_Click(object sender, EventArgs e) 打印事件
        /// <summary>
        /// 打印事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            var entity = BuildPrintBillEntity();
            if (entity != null)
            {
                _list = new List<ZtoPrintBillEntity> { entity };
                var defaultTemplate = BillPrintHelper.GetDefaultTemplatePath();
                if (string.IsNullOrEmpty(defaultTemplate))
                {
                    _report.LoadFromFile(Application.StartupPath + "\\Template\\ZTOBill.grf");
                }
                else
                {
                    _report.LoadFromFile(defaultTemplate);
                }
                _report.PrintPreview(true);
            }
        }
        #endregion

        #region private void FrmAddBill_Load(object sender, EventArgs e) 窗体加载事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmAddBill_Load(object sender, EventArgs e)
        {
            LoadDefaultData();
            BindArea();
            var result = BillPrintHelper.GetLoadDefaultSendMan();
            if (!string.IsNullOrEmpty(result))
            {
                if (result == "1")
                {
                    ckGetDefaultSendMan.Checked = true;
                    ckGetDefaultSendMan_CheckedChanged(this, null);
                    txtReceiveMan.Focus();
                    txtReceiveMan.Select();
                }
            }
            if (!string.IsNullOrEmpty(SelectedId))
            {
                this.Text = @"编辑申通运单信息";
                // 绑定收发件人的信息
                ZtoPrintBillEntity billEntity = _printBillManager.GetObject(SelectedId);
                txtSendMan.Text = billEntity.SendMan;
                txtSendPhone.Text = billEntity.SendPhone;
                txtSendDeparture.Text = billEntity.SendDeparture;
                txtSendCompany.Text = billEntity.SendCompany;
                txtSendAddress.Text = billEntity.SendAddress;
                dgvSearchSendArea.Text = string.Format("{0}-{1}-{2}", billEntity.SendProvince, billEntity.SendCity,
                                                       billEntity.SendCounty).Replace(" ", "");
                txtReceiveMan.Text = billEntity.ReceiveMan;
                txtReceivePhone.Text = billEntity.ReceivePhone;
                txtReceiveDestination.Text = billEntity.ReceiveDestination;
                txtReceiveCompany.Text = billEntity.ReceiveCompany;
                txtReceiveAddress.Text = billEntity.ReceiveAddress;
                dgvSearchReceiveArea.Text = string.Format("{0}-{1}-{2}", billEntity.ReceiveProvince,
                    billEntity.ReceiveCity, billEntity.ReceiveCounty).Replace(" ", "");
            }
            _report.Initialize += ReportInitialize;
            _report.FetchRecord += ReportFetchRecord;
        }
        #endregion

        #region private void ReportInitialize() 初始化报表列字段对象
        private IGRField billCode, senderName, senderAddress, senderCompany, senderPhone, departure,
                         receiverName, receiverAddress, receiverCompany, receiverPhone, destination,
                         description, amount, remarks, sendTime, weight, totalMoney, bigPen;

        private void ckTodaySend_CheckedChanged(object sender, EventArgs e)
        {
            // 是否今天发货
            BillPrintHelper.SetTodaySend(ckTodaySend.Checked);
            txtSendDate.Text = DateTime.Now.ToString(BaseSystemInfo.DateFormat);
        }

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
        }

        /// <summary>
        /// 报表字段和数据转换函数
        /// </summary>
        private void ReportFetchRecord()
        {
            if (_list != null && _list.Count > 0)
            {
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
                        receiverAddress.AsString = billEntity.ReceiveProvince + billEntity.ReceiveCity + billEntity.ReceiveCounty + tempAddress;
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
                        amount.AsString = billEntity.TotalNumber.ToString();
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
                        weight.AsString = billEntity.Weight.ToString();
                    }
                    if (totalMoney != null)
                    {
                        totalMoney.AsString = billEntity.TranFee.ToString();
                    }
                    if (bigPen != null)
                    {
                        bigPen.AsString = billEntity.BigPen;
                    }
                    _report.DetailGrid.Recordset.Post();
                }
            }
        }
        #endregion

        #region private void BindArea() 绑定省市区到内存中

        /// <summary>
        /// 绑定省市区到内存中
        /// </summary>
        private void BindArea()
        {
            try
            {
                var areaDt = BillPrintHelper.GetArea();
                if (areaDt.Rows.Count > 0)
                {
                    GridLookUpEditHelper.GridLookUpEditInit(dgvSearchSendArea, areaDt, BaseAreaEntity.FieldFullName, BaseAreaEntity.FieldFullName);
                    GridLookUpEditHelper.GridLookUpEditInit(dgvSearchReceiveArea, areaDt, BaseAreaEntity.FieldFullName, BaseAreaEntity.FieldFullName);
                    dgvSearchSendArea.EditValueChanging += dgvSearchSendArea_EditValueChanging;
                    dgvSearchSendArea.KeyUp += dgvSearchSendArea_KeyUp;
                    dgvSearchSendArea.Enter += dgvSearchSendArea_Enter;
                    dgvSearchReceiveArea.KeyUp += dgvSearchReceiveArea_KeyUp;
                    dgvSearchReceiveArea.EditValueChanging += dgvSearchReceiveArea_EditValueChanging;
                    dgvSearchReceiveArea.Enter += dgvSearchReceiveArea_Enter;
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        void dgvSearchReceiveArea_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(dgvSearchReceiveArea.Text))
            {
                dgvSearchReceiveArea.ShowPopup();
                return;
            }
            GridLookUpEditHelper.FilterLookup(sender, BaseAreaEntity.FieldFullName, BaseAreaEntity.FieldSimpleSpelling);
        }

        void dgvSearchSendArea_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(dgvSearchSendArea.Text))
            {
                dgvSearchSendArea.ShowPopup();
                return;
            }
            GridLookUpEditHelper.FilterLookup(sender, BaseAreaEntity.FieldFullName, BaseAreaEntity.FieldSimpleSpelling);
        }


        private void dgvSearchReceiveArea_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (string.IsNullOrEmpty(dgvSearchReceiveArea.Text)) return;
            BeginInvoke(new MethodInvoker(() => GridLookUpEditHelper.FilterLookup(sender, BaseAreaEntity.FieldFullName, BaseAreaEntity.FieldSimpleSpelling)));
        }

        private void dgvSearchSendArea_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (string.IsNullOrEmpty(dgvSearchSendArea.Text)) return;
            BeginInvoke(new MethodInvoker(() => GridLookUpEditHelper.FilterLookup(sender, BaseAreaEntity.FieldFullName, BaseAreaEntity.FieldSimpleSpelling)));
        }
        void dgvSearchReceiveArea_Enter(object sender, EventArgs e)
        {
            if (dgvSearchReceiveArea.Text == @"格式：江苏省-苏州市-吴中区")
            {
                dgvSearchReceiveArea.ShowPopup();
            }
        }

        void dgvSearchSendArea_Enter(object sender, EventArgs e)
        {
            if (dgvSearchSendArea.Text == @"格式：江苏省-苏州市-吴中区")
            {
                dgvSearchSendArea.ShowPopup();
            }
        }

        #endregion

        #region private void dgvSearchSendArea_EditValueChanged(object sender, EventArgs e) 选择过发件地址后，提取始发地
        /// <summary>
        /// 选择过发件地址后，提取始发地
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSearchSendArea_EditValueChanged(object sender, EventArgs e)
        {
            if (dgvSearchSendArea.Text.Trim().Length == 0) return;
            if (string.IsNullOrEmpty(txtSendDeparture.Text.Trim()))
            {
                txtSendDeparture.Text = dgvSearchSendArea.Text.Split('-')[0];
            }
        }

        #endregion

        #region private void btnCancel_Click(object sender, EventArgs e) 取消
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
        #endregion

        #region private void ckGetDefaultSendMan_CheckedChanged(object sender, EventArgs e) 是否加载系统默认发件人
        /// <summary>
        /// 是否加载系统默认发件人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckGetDefaultSendMan_CheckedChanged(object sender, EventArgs e)
        {
            if (ckGetDefaultSendMan.Checked)
            {
                // 加载默认发件人
                ZtoUserManager userManager = new ZtoUserManager(BillPrintHelper.DbHelper);
                var userList = userManager.GetList<ZtoUserEntity>(new KeyValuePair<string, object>(ZtoUserEntity.FieldIsDefault, 1), new KeyValuePair<string, object>(ZtoUserEntity.FieldIssendorreceive, 1));
                if (userList.Any())
                {
                    var userEntity = userList.First();
                    txtSendMan.Text = userEntity.Realname;
                    dgvSearchSendArea.Text = string.Format("{0}-{1}-{2}", userEntity.Province, userEntity.City, userEntity.County);
                    txtSendDeparture.Text = userEntity.Province;
                    txtSendAddress.Text = userEntity.Address;
                    txtSendPhone.Text = userEntity.Mobile + " " + userEntity.TelePhone;
                    txtSendCompany.Text = userEntity.Company;
                    txtReceiveMan.Focus();
                    txtReceiveMan.Select();
                    BillPrintHelper.SetLoadDefaultSendMan();
                }
                else
                {
                    if (XtraMessageBox.Show(@"未添加默认发件人信息，是否添加？", AppMessage.MSG0000, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        FrmAddSendMan addSendMan = new FrmAddSendMan();
                        addSendMan.ShowDialog();
                        addSendMan.Dispose();
                    }
                }
            }
            else
            {
                BillPrintHelper.SetLoadDefaultSendMan(false);
            }
        }
        #endregion

        #region void LoadDefaultData() 加载默认的复选框状态值
        /// <summary>
        /// 加载默认的复选框状态值
        /// </summary>
        void LoadDefaultData()
        {
            var result = BillPrintHelper.GetLoadDefaultSendMan();
            if (string.IsNullOrEmpty(result))
            {
                ckTodaySend.Checked = false;
            }
            else
            {
                ckTodaySend.Checked = result == "1" ? true : false;
            }

            var result1 = BillPrintHelper.GetLoadDefaultSendMan();
            if (string.IsNullOrEmpty(result1))
            {
                ckGetDefaultSendMan.Checked = false;
            }
            else
            {
                ckGetDefaultSendMan.Checked = result1 == "1" ? true : false;
            }
        }
        #endregion

        /// <summary>
        /// 自动提取发件省市区（根据发件详细地址）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSendAddress_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(dgvSearchSendArea.Text))
            {
                return;
            }
            var result = BaiduMapHelper.GetProvCityDistFromBaiduMap(txtSendAddress.Text);
            if (result != null)
            {
                var area = string.Format("{0}-{1}-{2}", result.Result.AddressComponent.Province.Trim(), result.Result.AddressComponent.City.Trim(), result.Result.AddressComponent.District.Trim());
                dgvSearchSendArea.Text = area;
                if (string.IsNullOrEmpty(dgvSearchSendArea.Text))
                {
                    BaseAreaManager areaManager = new BaseAreaManager(BillPrintHelper.DbHelper);
                    // 去数据库找
                    var countyEntity = areaManager.GetList<BaseAreaEntity>(new KeyValuePair<string, object>(BaseAreaEntity.FieldFullName, result.Result.AddressComponent.District.Trim())).FirstOrDefault();
                    if (countyEntity != null)
                    {
                        var cityEntity = areaManager.GetObject(countyEntity.ParentId);
                        var provinceEntity = areaManager.GetObject(cityEntity.ParentId);
                        dgvSearchSendArea.Text = string.Format("{0}-{1}-{2}", provinceEntity.FullName, cityEntity.FullName, countyEntity.FullName);
                    }
                }
            }
        }

        /// <summary>
        /// 自动提取收件省市区（根据收件详细地址）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtReceiveAddress_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(dgvSearchReceiveArea.Text))
            {
                return;
            }
            var result = BaiduMapHelper.GetProvCityDistFromBaiduMap(txtReceiveAddress.Text);
            if (result != null)
            {
                var area = string.Format("{0}-{1}-{2}", result.Result.AddressComponent.Province.Trim(), result.Result.AddressComponent.City.Trim(), result.Result.AddressComponent.District.Trim());
                dgvSearchReceiveArea.Text = area;
                if (string.IsNullOrEmpty(dgvSearchReceiveArea.Text))
                {
                    BaseAreaManager areaManager = new BaseAreaManager(BillPrintHelper.DbHelper);
                    // 去数据库找
                    var countyEntity = areaManager.GetList<BaseAreaEntity>(new KeyValuePair<string, object>(BaseAreaEntity.FieldFullName, result.Result.AddressComponent.District.Trim())).FirstOrDefault();
                    if (countyEntity != null)
                    {
                        var cityEntity = areaManager.GetObject(countyEntity.ParentId);
                        var provinceEntity = areaManager.GetObject(cityEntity.ParentId);
                        dgvSearchReceiveArea.Text = string.Format("{0}-{1}-{2}", provinceEntity.FullName, cityEntity.FullName, countyEntity.FullName);
                    }
                }
            }
        }

        private void ckChooseReceiveMan_CheckedChanged(object sender, EventArgs e)
        {
            if (ckChooseReceiveMan.Checked)
            {
                FrmChooseReceiveMan chooseReceiveMan = new FrmChooseReceiveMan();
                if (chooseReceiveMan.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(chooseReceiveMan.receiveManControl1.ChooseId))
                    {
                        ZtoUserManager userManager = new ZtoUserManager(BillPrintHelper.DbHelper);
                        ZtoUserEntity userEntity = userManager.GetObject(chooseReceiveMan.receiveManControl1.ChooseId);
                        if (userEntity != null)
                        {
                            // 赋值
                            txtReceiveMan.Text = userEntity.Realname;
                            // 湖北省-荆州市-公安县
                            dgvSearchReceiveArea.Text = string.Format("{0}-{1}-{2}", userEntity.Province, userEntity.City, userEntity.County).Replace(" ", "");
                            txtReceiveDestination.Text = userEntity.Province;
                            txtReceiveAddress.Text = userEntity.Address;
                            txtReceivePhone.Text = string.Format("{0} {1}", userEntity.Mobile, userEntity.TelePhone);
                            txtReceiveCompany.Text = userEntity.Company;
                        }
                    }
                }
                chooseReceiveMan.Dispose();
            }
        }

    }
}
