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
    public partial class ChooseReceiveMan : XtraUserControl
    {
        public ChooseReceiveMan()
        {
            InitializeComponent();
        }

        private void CkChooseReceiveManCheckedChanged(object sender, EventArgs e)
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
                            var parentForm = ParentForm;
                            if (parentForm is BaseForm)
                            {
                                // 设置收件人
                                (parentForm as BaseForm).ReceiveManEntity = userEntity;
                               /// (parentForm as BaseForm).Controls["txtReceiveMan"].Text = userEntity.Realname;
                            }
                        }
                    }
                }
                chooseReceiveMan.Dispose();
            }
        }
    }
}
