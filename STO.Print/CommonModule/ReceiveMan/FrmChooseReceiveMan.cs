//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System.Windows.Forms;

namespace STO.Print
{
    using STO.Print.AddBillForm;

    /// <summary>
    /// 选择收件人窗体
    /// 
    /// 修改记录
    /// 
    ///     2015-09-30  版本：1.0 YangHengLian 创建
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-09-30</date>
    /// </author>
    /// </summary>
    public partial class FrmChooseReceiveMan : BaseForm
    {
        public FrmChooseReceiveMan()
        {
            InitializeComponent();
        }

        private void FrmChooseReceiveManFormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
