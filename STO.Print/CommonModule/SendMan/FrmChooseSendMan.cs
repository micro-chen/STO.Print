//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System.Windows.Forms;

namespace STO.Print
{
    using STO.Print.AddBillForm;

    /// <summary>
    /// 选择发件人窗体
    /// 
    /// 修改记录
    /// 
    ///     2016-1-28  版本：1.0 YangHengLian 创建,适合导入的时候选择发件人,反正只要是录入单子信息都可以用这个
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2016-1-28</date>
    /// </author>
    /// </summary>
    public partial class FrmChooseSendMan : BaseForm
    {
        public FrmChooseSendMan()
        {
            InitializeComponent();
        }

        private void FrmChooseSendManFormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
