//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Windows.Forms;

namespace STO.Print.AddBillForm
{
    using DevExpress.XtraEditors;
    using DotNet.Utilities;
    using STO.Print.Manager;
    using STO.Print.Model;
    using STO.Print.Utilities;

    /// <summary>
    /// 申通普通电子面单
    /// 
    /// 修改记录
    /// 
    ///     2015-07-10  版本：1.0 YangHengLian 创建
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-07-18</date>
    /// </author>
    /// </summary>
    public partial class FrmAddZto180ElecBill : BaseForm
    {
        readonly ZtoPrintBillManager _printBillManager = new ZtoPrintBillManager(BillPrintHelper.DbHelper);

        public FrmAddZto180ElecBill()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmAddZto180ElecBill_Load(object sender, EventArgs e)
        {
            // 到付和代收不可以编辑的还有单号也不能自己填写的
            myEnterBillControl1.txtToPayMent.Enabled = false;
            myEnterBillControl1.txtGoodsPayment.Enabled = false;
            myEnterBillControl1.txtBillCode.Enabled = false;

            var result = BillPrintHelper.GetLoadDefaultSendMan();
            if (!string.IsNullOrEmpty(result))
            {
                if (result == "1")
                {
                    myEnterBillControl1.txtReceiveMan.Focus();
                    myEnterBillControl1.txtReceiveMan.Select();
                }
            }
            if (!string.IsNullOrEmpty(SelectedId))
            {
                this.Text = @"编辑申通普通电子面单";
                
                myEnterBillControl1.PrintBillId = SelectedId;
                // 绑定收发件人的信息
                ZtoPrintBillEntity billEntity = _printBillManager.GetObject(SelectedId);
                myEnterBillControl1.txtSendMan.Text = billEntity.SendMan;
                myEnterBillControl1.txtSendPhone.Text = billEntity.SendPhone;
                myEnterBillControl1.txtSendCompany.Text = billEntity.SendCompany;
                myEnterBillControl1.txtSendAddress.Text = billEntity.SendAddress;
                myEnterBillControl1.dgvSendArea.Text = string.Format("{0}-{1}-{2}", billEntity.SendProvince, billEntity.SendCity, billEntity.SendCounty).Replace(" ", "");
                myEnterBillControl1.txtReceiveMan.Text = billEntity.ReceiveMan;
                myEnterBillControl1.txtReceivePhone.Text = billEntity.ReceivePhone;
                myEnterBillControl1.txtReceiveCompany.Text = billEntity.ReceiveCompany;
                myEnterBillControl1.txtReceiveAddress.Text = billEntity.ReceiveAddress;
                myEnterBillControl1.txtBigPen.Text = billEntity.BigPen;
                myEnterBillControl1.dgvReceiveArea.Text = string.Format("{0}-{1}-{2}", billEntity.ReceiveProvince, billEntity.ReceiveCity, billEntity.ReceiveCounty).Replace(" ", "");
                myEnterBillControl1.cmbGoodsName.Text = billEntity.GoodsName;
                myEnterBillControl1.txtGoodsWeight.Text = billEntity.Weight;
                myEnterBillControl1.txtRemark.Text = billEntity.Remark;
                myEnterBillControl1.txtOrderNumber.Text = billEntity.OrderNumber;
                myEnterBillControl1.txtBillCode.Text = billEntity.BillCode;
                // 这里防止重复获取，订单号一样，重复获取没关系啊，除非你改了订单号
                if (!string.IsNullOrEmpty(myEnterBillControl1.txtBillCode.Text))
                {
                    myEnterBillControl1.btnGetZtoElecBill.Enabled = false;
                    myEnterBillControl1.txtOrderNumber.Enabled = false;
                }
                else
                {
                    myEnterBillControl1.btnGetZtoElecBill.Enabled = true;
                    myEnterBillControl1.txtOrderNumber.Enabled = true;
                }
                if (!string.IsNullOrEmpty(billEntity.SendDate))
                {
                    myEnterBillControl1.txtSendDate.Text = BaseBusinessLogic.ConvertToDateToString(billEntity.SendDate);
                }
                myEnterBillControl1.txtSendMan.Select(0, 0);
                myEnterBillControl1.txtSendMan.Focus();
            }
        }

        /// <summary>
        /// 窗体关闭时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmAddZto180ElecBill_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (XtraMessageBox.Show("确定关闭录单吗？", AppMessage.MSG0000, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Abort;
                e.Cancel = true; 
            }
        }
    }
}
