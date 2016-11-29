//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace STO.Print
{
    using DevExpress.XtraEditors.Controls;
    using DotNet.Business;
    using DotNet.Model;
    using DotNet.Utilities;
    using Manager;
    using Model;
    using Newtonsoft.Json;
    using Utilities;
    using STO.Print.AddBillForm;

    /// <summary>
    /// 新增修改发件人窗体
    /// 
    /// 修改记录
    /// 
    ///     2015-08-10  版本：1.0 YangHengLian 创建
    ///     2015-12-04  客户ID和密码需要去掉空格，获取电子面单会返回非法账号信息
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-08-10</date>
    /// </author>
    /// </summary>
    public partial class FrmAddSendMan : BaseForm
    {
        /// <summary>
        /// 更新用户的ID
        /// </summary>
        public string Id;

        /// <summary>
        /// 是否是收件人新增或者修改窗体
        /// </summary>
        public bool IsReceiveForm;

        public FrmAddSendMan()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmSendMan_Load(object sender, EventArgs e)
        {
            splitContainerControl1.Panel1.Height = Convert.ToInt32(this.Height * 0.8);
            splitContainerControl1.Panel2.Height = Convert.ToInt32(this.Height * 0.2);
            var userManager = new ZtoUserManager(BillPrintHelper.DbHelper);
            var userList = userManager.GetList<ZtoUserEntity>(new KeyValuePair<string, object>(ZtoUserEntity.FieldIsDefault, 1), new KeyValuePair<string, object>(ZtoUserEntity.FieldIssendorreceive, 1));
            if (!userList.Any())
            {
                ckDefault.Checked = true;
            }
            else
            {
                ckDefault.Checked = false;
            }
            if (IsReceiveForm)
            {
                Text = @"收件人";
                this.tabPageSendMan.Text = "收件人信息";
                ckDefault.Text = "是否默认收件人";
                lblCustomerID.Visible = false;
                txtCustomerID.Visible = false;
                lblCustomerPassword.Visible = false;
                txtCustomerPassword.Visible = false;
                linkLabel1.Visible = false;
                ckProclaimText.Visible = false;
                txtCustomerID.Enabled = false;
                txtCustomerPassword.Enabled = false;
                btnGetElecCount.Enabled = false;
            }
            BindArea();
            if (!string.IsNullOrEmpty(Id))
            {
                ZtoUserEntity userEntity = new ZtoUserManager(BillPrintHelper.DbHelper).GetObject(Id);
                if (userEntity != null)
                {
                    txtRealName.Text = userEntity.Realname;
                    dgvSearchSendArea.Text = string.Format("{0}-{1}-{2}", userEntity.Province, userEntity.City, userEntity.County);
                    txtSendAddress.Text = userEntity.Address;
                    txtCompanyName.Text = userEntity.Company;
                    txtMobile.Text = userEntity.Mobile;
                    txtTelePhone.Text = userEntity.TelePhone;
                    txtPostCode.Text = userEntity.Postcode;
                    txtRemark.Text = userEntity.Remark;
                    ckDefault.Checked = userEntity.IsDefault == "1";
                }
                
            }
            // 发件人的情况下
            if (!IsReceiveForm)
            {
                ZtoElecUserInfoEntity elecUserInfoEntity = BillPrintHelper.GetElecUserInfoEntity();
                if (elecUserInfoEntity != null)
                {
                    txtCustomerID.Text = elecUserInfoEntity.Kehuid;
                    txtCustomerPassword.Text = elecUserInfoEntity.Pwd;
                }
            }
            txtRealName.Focus();
            txtRealName.Select();
        }

        /// <summary>
        /// 检查页面输入参数
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
            if (string.IsNullOrEmpty(txtRealName.Text))
            {
                txtRealName.ShowTip("姓名必填");
                txtRealName.Focus();
                txtRealName.Select();
                return false;
            }
            if (string.IsNullOrEmpty(dgvSearchSendArea.Text.Trim()))
            {
                dgvSearchSendArea.ShowTip("省市区必填");
                dgvSearchSendArea.Focus();
                dgvSearchSendArea.Select();
                return false;
            }
            if (string.IsNullOrEmpty(txtSendAddress.Text.Trim()))
            {
                txtSendAddress.ShowTip("详细地址必填");
                txtSendAddress.Focus();
                txtSendAddress.Select();
                return false;
            }
            if (string.IsNullOrEmpty(txtMobile.Text.Trim()) && string.IsNullOrEmpty(txtTelePhone.Text.Trim()))
            {
                txtMobile.ShowTip("手机必填");
                txtMobile.Focus();
                txtMobile.Select();
                return false;
            }
            return true;
        }

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
                    dgvSearchSendArea.EditValueChanging += dgvSearchSendArea_EditValueChanging;
                    dgvSearchSendArea.KeyUp += dgvSearchSendArea_KeyUp;
                    dgvSearchSendArea.Enter += dgvSearchSendArea_Enter;
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
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

        private void dgvSearchSendArea_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (string.IsNullOrEmpty(dgvSearchSendArea.Text)) return;
            BeginInvoke(new MethodInvoker(() => GridLookUpEditHelper.FilterLookup(sender, BaseAreaEntity.FieldFullName, BaseAreaEntity.FieldSimpleSpelling)));
        }

        void dgvSearchSendArea_Enter(object sender, EventArgs e)
        {
            if (dgvSearchSendArea.Text == @"格式：江苏省-苏州市-吴中区")
            {
                dgvSearchSendArea.ShowPopup();
            }
        }

        #endregion

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            AddEntity();
            if (!IsReceiveForm)
            {
                AddElecCustomerInfo();
            }
        }

        /// <summary>
        /// 保存并关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            if (AddEntity())
            {
                if (!IsReceiveForm)
                {
                    AddElecCustomerInfo();
                }
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private bool AddEntity()
        {
            if (CheckInput())
            {
                ZtoUserManager userManager = new ZtoUserManager(BillPrintHelper.DbHelper);
                ZtoUserEntity userEntity = new ZtoUserEntity();
                userEntity.Realname = txtRealName.Text.Trim();
                var areaArray = dgvSearchSendArea.Text.Split('-');
                if (areaArray.Length > 0 && areaArray.Length == 3)
                {
                    userEntity.Province = areaArray[0];
                    userEntity.City = areaArray[1];
                    userEntity.County = areaArray[2];
                }
                userEntity.Address = txtSendAddress.Text.Trim();
                userEntity.Company = txtCompanyName.Text.Trim();
                userEntity.TelePhone = txtTelePhone.Text.Trim();
                userEntity.Mobile = txtMobile.Text.Trim();
                userEntity.Postcode = txtPostCode.Text.Trim();
                userEntity.Remark = txtRemark.Text;
                userEntity.IsDefault = ckDefault.Checked ? "1" : "0";
                userEntity.Issendorreceive = IsReceiveForm ? "0" : "1";
                string result;
                if (ckDefault.Checked)
                {
                    if (IsReceiveForm)
                    {
                        userManager.SetProperty(new KeyValuePair<string, object>(ZtoUserEntity.FieldIssendorreceive, 0), new KeyValuePair<string, object>(ZtoUserEntity.FieldIsDefault, 0));
                    }
                    else
                    {
                        userManager.SetProperty(new KeyValuePair<string, object>(ZtoUserEntity.FieldIssendorreceive, 1), new KeyValuePair<string, object>(ZtoUserEntity.FieldIsDefault, 0));
                    }
                }
                // 更新（发件人或者收件人）
                if (!string.IsNullOrEmpty(Id))
                {
                    userEntity.Id = Convert.ToDecimal(Id);
                    result = userManager.Update(userEntity).ToString();
                }
                else
                {
                    // 新增
                    result = userManager.Add(userEntity, true);
                }
                if (!string.IsNullOrEmpty(result))
                {
                    MessageBox.Show(@"保存成功。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                MessageBox.Show(@"保存失败。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtRemark_EditValueChanged(object sender, EventArgs e)
        {
            if (this.txtRemark.Text.Length > 0)
            {
                this.lblContentsLength.Text = "已输入 " + this.txtRemark.Text.Length + " 字符";
            }
            else
            {
                this.lblContentsLength.Text = "";
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (tabSendMan.SelectedTabPage == tabPageRemark)
            {
                txtRemark.Focus();
            }
        }

        /// <summary>
        /// 智能获取邮编
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSearchSendArea_EditValueChanged(object sender, EventArgs e)
        {
            if (dgvSearchSendArea.Text.Trim().Length == 0) return;
            var county = dgvSearchSendArea.Text.Split('-')[2];
            var city = dgvSearchSendArea.Text.Split('-')[1];
            var postCode = NetworkHelper.GetPostCodeByAddress(city, county);
            if (!string.IsNullOrEmpty(postCode))
            {
                txtPostCode.Text = postCode;
            }
        }

        /// <summary>
        /// 窗体正在关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmAddSendMan_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 自动获取到地址所在省市区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSendAddress_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSendAddress.Text))
            {
                var result = BaiduMapHelper.GetProvCityDistFromBaiduMap(dgvSearchSendArea.Text + txtSendAddress.Text);
                if (result != null)
                {
                    var area = string.Format("{0}-{1}-{2}", result.Result.AddressComponent.Province.Trim(),
                                             result.Result.AddressComponent.City.Trim(),
                                             result.Result.AddressComponent.District.Trim());
                    dgvSearchSendArea.Text = area;
                    if (string.IsNullOrEmpty(dgvSearchSendArea.Text))
                    {
                        BaseAreaManager areaManager = new BaseAreaManager(BillPrintHelper.DbHelper);
                        // 去数据库找
                        var countyEntity =
                            areaManager.GetList<BaseAreaEntity>(
                                new KeyValuePair<string, object>(BaseAreaEntity.FieldFullName,
                                                                 result.Result.AddressComponent.District.Trim())).
                                FirstOrDefault();
                        if (countyEntity != null)
                        {
                            var cityEntity = areaManager.GetObject(countyEntity.ParentId);
                            var provinceEntity = areaManager.GetObject(cityEntity.ParentId);
                            dgvSearchSendArea.Text = string.Format("{0}-{1}-{2}", provinceEntity.FullName,
                                                                   cityEntity.FullName, countyEntity.FullName);
                            // 详细地址把省市区过滤掉
                            txtSendAddress.Text =
                                txtSendAddress.Text.Replace(provinceEntity.FullName, "").Replace(
                                    cityEntity.FullName, "").Replace(
                                        countyEntity.FullName, "");
                        }
                    }
                    else
                    {
                        // 详细地址把省市区过滤掉
                        txtSendAddress.Text =
                            txtSendAddress.Text.Replace(result.Result.AddressComponent.Province.Trim(), "").Replace(
                                result.Result.AddressComponent.City.Trim(), "").Replace(
                                    result.Result.AddressComponent.District.Trim(), "");
                    }
                }
            }
        }

        /// <summary>
        /// 绑定申通商家ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblBindElecUserInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //FrmBindZtoElecUserInfo bindElecUserInfo = new FrmBindZtoElecUserInfo();
            //if (bindElecUserInfo.ShowDialog() == DialogResult.OK)
            //{
            //    txtMobile.Text = bindElecUserInfo.UserInfoEntity.Phone;
            //    dgvSearchSendArea.Text = string.Format("{0}-{1}-{2}", bindElecUserInfo.UserInfoEntity.Province, bindElecUserInfo.UserInfoEntity.City, bindElecUserInfo.UserInfoEntity.Area);
            //}
        }

        /// <summary>
        /// 保存商家ID实体序列化加密字符串到本地数据库
        /// </summary>
        void AddElecCustomerInfo()
        {
            if (!string.IsNullOrEmpty(txtCustomerID.Text) && !string.IsNullOrEmpty(txtCustomerPassword.Text))
            {
                ZtoElecUserInfoEntity elecUserInfoEntity = new ZtoElecUserInfoEntity();
                elecUserInfoEntity.Kehuid = txtCustomerID.Text.Replace(" ", "");
                elecUserInfoEntity.Pwd = txtCustomerPassword.Text.Replace(" ", "");
                elecUserInfoEntity.Phone = string.IsNullOrEmpty(txtMobile.Text.Trim()) ? txtTelePhone.Text : txtMobile.Text;
                var areaArray = dgvSearchSendArea.Text.Split('-');
                if (areaArray.Length > 0 && areaArray.Length == 3)
                {
                    elecUserInfoEntity.Province = areaArray[0];
                    elecUserInfoEntity.City = areaArray[1];
                    elecUserInfoEntity.Area = areaArray[2];
                }
                elecUserInfoEntity.Result = "false";
                elecUserInfoEntity.InterfaceType = "0";
                string encryInfo = JsonConvert.SerializeObject(elecUserInfoEntity);
                BillPrintHelper.SetZtoCustomerInfo(SecretUtil.Encrypt(encryInfo, BaseSystemInfo.SecurityKey));
            }
            else
            {
                BillPrintHelper.DeleteElecUserInfoEntity();
            }
        }

        private void ckProclaimText_CheckedChanged(object sender, EventArgs e)
        {
            txtCustomerPassword.Properties.UseSystemPasswordChar = !ckProclaimText.Checked;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ToolHelper.OpenBrowserUrl("http://note.youdao.com/share/web/file.html?id=ff498eac3764c42d7c4c6090f201a620&type=note");
        }

        /// <summary>
        /// 查询剩余电子面单数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetElecCount_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCustomerID.Text.Trim()))
            {
                txtCustomerID.Focus();
                txtCustomerID.Select();
                txtCustomerID.ShowTip("商家ID必填");
                return;
            }
            if (string.IsNullOrEmpty(txtCustomerPassword.Text.Trim()))
            {
                txtCustomerPassword.Focus();
                txtCustomerPassword.Select();
                txtCustomerPassword.ShowTip("商家ID密码必填");
                return;
            }
            var elecUserInfoEntity = new ZtoElecUserInfoEntity();
            elecUserInfoEntity.Kehuid = txtCustomerID.Text.Replace(" ", "");
            elecUserInfoEntity.Pwd = txtCustomerPassword.Text.Replace(" ", "");
            var result = ZtoElecBillHelper.GetElecBillCount(elecUserInfoEntity);
            int elecBillCount = 0;
            if (int.TryParse(result, out elecBillCount))
            {
                XtraMessageBox.Show(string.Format("商家ID为：{1}可用线下电子面单数量为：{0}条", result, elecUserInfoEntity.Kehuid), AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(result, AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
