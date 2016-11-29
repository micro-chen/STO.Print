//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace STO.Print
{
    using STO.Print.AddBillForm;
    using STO.Print.Model;
    using STO.Print.Utilities;

    /// <summary>
    /// 补填单号
    ///  
    /// 修改记录
    /// 
    ///     2016-07-13  版本：1.0 YangHengLian 创建
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2016-07-13</date>
    /// </author>
    /// </summary>
    public partial class FrmInputStartBillAndEndBill : BaseForm
    {

        public FrmInputStartBillAndEndBill()
        {
            InitializeComponent();
        }

        public FrmInputStartBillAndEndBill(List<ZtoPrintBillEntity> ZtoPrintBillEntities)
        {
            InitializeComponent();
            myInputStartBillAndEndBill1.PrintBillEntities = ZtoPrintBillEntities;
        }

        private void FrmInputStartBillAndEndBill_Load(object sender, EventArgs e)
        {
        }

        private void FrmInputStartBillAndEndBill_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (string.IsNullOrEmpty(myInputStartBillAndEndBill1.txtStartBill.Text) || string.IsNullOrEmpty(myInputStartBillAndEndBill1.txtEndBill.Text))
            {
                DialogResult = DialogResult.Abort;
            }
            else
            {
                if (MessageUtil.ConfirmYesNo("确定关闭吗？"))
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
}
