//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DotNet.Utilities;
using STO.Print.Manager;
using STO.Print.Model;
using STO.Print.Utilities;

namespace STO.Print.UserControl
{
    using DevExpress.XtraRichEdit.API.Native;

    /// <summary>
    /// 绑定大面单打印时候起始单号和结束单号
    ///
    /// 修改纪录
    ///
    ///		  2014-09-17  版本：1.0 YangHengLian 创建主键，注意命名空间的排序。
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2014-09-17</date>
    /// </author>
    /// </summary>
    public partial class MyInputStartBillAndEndBill : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// 需要绑定的录单数据的单子
        /// </summary>
        public List<ZtoPrintBillEntity> PrintBillEntities { get; set; }

        public MyInputStartBillAndEndBill()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 光标离开自动计算数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtEndBill_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtStartBill.Text) && !string.IsNullOrEmpty(txtStartBill.Text))
            {
                var start = BaseBusinessLogic.ConvertToLong(txtStartBill.Text);
                var end = BaseBusinessLogic.ConvertToLong(txtEndBill.Text);
                var count = end - start;
                lblTotal.Text = "总计：" + count;
                btnSaveBill.Focus();
            }
        }

        /// <summary>
        /// 光标进入结算单号的自动计算结尾单号，根据需要打印的数量来
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtEndBill_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEndBill.Text) || txtEndBill.Text == txtEndBill.Properties.NullValuePrompt)
            {
                SetEndBill();
                btnSaveBill.Focus();
            }
        }

        /// <summary>
        /// 需要打印数量值改变的时候
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPrintBillCount_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbPrintBillCount.Text == "0" || string.IsNullOrEmpty(cmbPrintBillCount.Text))
            {
                cmbPrintBillCount.Text = "1";
            }
            SetEndBill();
        }

        void SetEndBill()
        {
            if (!string.IsNullOrEmpty(txtStartBill.Text))
            {
                var start = BaseBusinessLogic.ConvertToLong(txtStartBill.Text);
                var end = start + BaseBusinessLogic.ConvertToLong(cmbPrintBillCount.Text);
                lblTotal.Text = "总计：" + cmbPrintBillCount.Text;
                txtEndBill.Text = end.ToString();
            }
        }

        private void MyInputStartBillAndEndBill_Load(object sender, EventArgs e)
        {
            txtStartBill.Focus();
            cmbPrintBillCount.Text = PrintBillEntities.Count.ToString();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveBill_Click(object sender, EventArgs e)
        {
            if (this.PrintBillEntities.Any())
            {
                var start = BaseBusinessLogic.ConvertToLong(txtStartBill.Text);
                var end = BaseBusinessLogic.ConvertToLong(txtEndBill.Text);
                var count = end - start;
                if (count.ToString() == cmbPrintBillCount.Text)
                {
                    for (int i = 0; i < PrintBillEntities.Count; i++)
                    {
                        var billCode = BaseBusinessLogic.ConvertToLong(txtStartBill.Text) + i;
                        PrintBillEntities[i].BillCode = billCode.ToString();
                    }
                    if (MessageUtil.ConfirmYesNo(string.Format("确定保存{0}条单号吗？", PrintBillEntities.Count)))
                    {
                        using (IDbHelper dbHelper = DbHelperFactory.GetHelper(CurrentDbType.SQLite, BillPrintHelper.BillPrintConnectionString))
                        {
                            try
                            {
                                dbHelper.BeginTransaction();
                                var manager = new ZtoPrintBillManager(dbHelper);
                                foreach (var itemBill in PrintBillEntities)
                                {
                                    manager.SetProperty(new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldId, itemBill.Id), new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldBillCode, itemBill.BillCode));
                                }
                                dbHelper.CommitTransaction();
                                MessageUtil.ShowTips("保存成功");
                            }
                            catch (Exception ex)
                            {
                                dbHelper.RollbackTransaction();
                                LogUtil.WriteException(ex);
                            }
                        }
                    }
                }
                else
                {
                    string error = string.Format("需要打印{0}，结束单号-开始单号和需要打印数据不相等，不能保存", PrintBillEntities.Count);
                    MessageUtil.ShowError(error);
                }
            }
        }
    }
}
