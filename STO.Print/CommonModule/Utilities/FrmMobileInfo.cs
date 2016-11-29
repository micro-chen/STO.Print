//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Windows.Forms;
using STO.Print.AddBillForm;

namespace STO.Print
{
    using DevExpress.XtraEditors;
    using DotNet.Utilities;
    using STO.Print.Utilities;

    /// <summary>
    /// 查询手机号码归属地信息
    ///
    /// 修改纪录
    ///
    ///		  2015-1-5  版本：1.0 YangHengLian 创建主键，注意命名空间的排序。
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-1-5</date>
    /// </author>
    /// </summary>
    public partial class FrmMobileInfo : BaseForm
    {
        public FrmMobileInfo()
        {
            InitializeComponent();
        }

        public FrmMobileInfo(string mobile)
        {
            InitializeComponent();
            txtQuery.Text = mobile;
            btnSearch_Click(this, null);
        }

        /// <summary>
        /// 按下回车键自动检索信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(this, null);
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtQuery.Text.Trim()))
                {
                    var mobileEntity = MobileHelper.GetMobileInfo(txtQuery.Text.Trim());
                    if (mobileEntity != null)
                    {
                        if (mobileEntity.QueryResult)
                        {
                            txtMobile.Text = mobileEntity.Mobile;
                            txtProvince.Text = mobileEntity.Province;
                            txtCity.Text = mobileEntity.City;
                            txtAreaCode.Text = mobileEntity.AreaCode;
                            txtPostCode.Text = mobileEntity.PostCode;
                            txtCorp.Text = mobileEntity.Corp;
                        }
                        else
                        {
                            XtraMessageBox.Show("未查询到该号码信息", AppMessage.MSG0000);
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("未查询到该号码信息", AppMessage.MSG0000);
                    }
                }
                else
                {
                    txtMobile.Text = "";
                    txtProvince.Text = "";
                    txtCity.Text = "";
                    txtAreaCode.Text = "";
                    txtPostCode.Text = "";
                    txtCorp.Text = "";
                    XtraMessageBox.Show("请输入手机号码！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
    }
}
