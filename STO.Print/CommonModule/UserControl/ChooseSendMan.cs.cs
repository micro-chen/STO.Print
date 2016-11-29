//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Windows.Forms;

namespace STO.Print.UserControl
{
    using AddBillForm;
    using DevExpress.XtraEditors;
    using Manager;
    using Model;
    using Utilities;

    /// <summary>
    /// 选择发件人用户控件
    /// 
    /// 修改记录
    /// 
    ///     2016-6-22  版本：1.0 YangHengLian 创建
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2016-6-22</date>
    /// </author>
    /// </summary>
    public partial class ChooseSendMan : XtraUserControl
    {
        public ChooseSendMan()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 选择发件人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CkChooseSendManCheckedChanged(object sender, EventArgs e)
        {
            if (ckChooseSendMan.Checked)
            {
                var chooseSendMan = new FrmChooseSendMan();
                if (chooseSendMan.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(chooseSendMan.sendManControl1.ChooseId))
                    {
                        var userManager = new ZtoUserManager(BillPrintHelper.DbHelper);
                        ZtoUserEntity userEntity = userManager.GetObject(chooseSendMan.sendManControl1.ChooseId);
                        if (userEntity != null)
                        {
                            var parentForm = ParentForm;
                            if (parentForm is BaseForm)
                            {
                                // 设置发件人
                                (parentForm as BaseForm).SendManEntity = userEntity;
                            }
                        }
                    }
                }
                chooseSendMan.Dispose();
            }
        }
    }
}
