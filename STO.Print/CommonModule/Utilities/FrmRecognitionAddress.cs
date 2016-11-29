//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using STO.Print.AddBillForm;

namespace STO.Print
{
    using DevExpress.Utils;
    using DevExpress.XtraEditors;
    using DotNet.Business;
    using DotNet.Model;
    using DotNet.Utilities;
    using STO.Print.Manager;
    using STO.Print.Model;
    using STO.Print.Utilities;

    /// <summary>
    /// 识别淘宝或者其他联系人信息
    /// 
    /// 修改记录
    ///  
    ///     2015-09-08  版本：1.0 YangHengLian 创建
    ///     2015-10-17  改进识别的方法，调用百度地图来识别
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-09-08</date>
    /// </author>
    /// </summary>
    public partial class FrmRecognitionAddress : BaseForm
    {
        public FrmRecognitionAddress()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 查看淘宝示例
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDemoContent_Click(object sender, EventArgs e)
        {
            txtContent.Text = "张三，13111111111，江苏省 徐州市 沛县 张庄镇 崔寨村马楼196号 ，221600";
            Recognition();
        }

        /// <summary>
        /// 粘贴剪贴板并识别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPasteAndRecognition_Click(object sender, EventArgs e)
        {
            txtContent.Text = Clipboard.GetText();
            Recognition();
        }

        /// <summary>
        /// 只识别以上内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRecognition_Click(object sender, EventArgs e)
        {
            Recognition();
        }

        /// <summary>
        /// 打开本界面时自动粘贴剪贴板并识别地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckAutoPasteAndRecognition_CheckedChanged(object sender, EventArgs e)
        {
            BillPrintHelper.SetAutoPasteAndRecognition(ckAutoPasteAndRecognition.Checked);
            if (ckAutoPasteAndRecognition.Checked)
            {
                ckAutoPasteAndRecognition.ShowTip(null, "打开界面自动识别剪贴板内容", ToolTipLocation.RightBottom, ToolTipType.SuperTip);
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 保存为发件人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveAsSendMan_Click(object sender, EventArgs e)
        {
            AddMan(true);
        }

        /// <summary>
        /// 保存为收件人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveAsReceiveMan_Click(object sender, EventArgs e)
        {
            AddMan(false);
        }

        private void FrmRecognitionTaoBao_Load(object sender, EventArgs e)
        {
            txtContent.Focus();
            txtContent.Select();
            var result = BillPrintHelper.GetAutoPasteAndRecognition();
            if (!string.IsNullOrEmpty(result))
            {
                ckAutoPasteAndRecognition.Checked = result == "1";
            }
            if (ckAutoPasteAndRecognition.Checked)
            {
                txtContent.Text = Clipboard.GetText();
                Recognition();
            }
        }

        /// <summary>
        /// 识别地址方法
        /// </summary>
        void Recognition()
        {
            if (!string.IsNullOrEmpty(txtContent.Text))
            {
                ClearContent();
                var result = RecognitionTaoBao();
                if (!result)
                {
                    var result1 = RecognitionJD();
                    if (!result1)
                    {
                        RecognitionOther();
                    }
                }
                var address = txtContent.Text.Replace(" ", "").Replace("，", "").Replace(",", "").Replace("\r\n", "");
                if (!string.IsNullOrEmpty(txtRealName.Text))
                {
                    address = address.Replace(txtRealName.Text, "");
                }
                if (!string.IsNullOrEmpty(txtMobile.Text))
                {
                    address = address.Replace(txtMobile.Text, "");
                }
                if (!string.IsNullOrEmpty(txtTelephone.Text))
                {
                    address = address.Replace(txtTelephone.Text, "");
                }
                if (!string.IsNullOrEmpty(txtPostCode.Text))
                {
                    address = address.Replace(txtPostCode.Text, "");
                }
                var resultAddress = BaiduMapHelper.GetProvCityDistFromBaiduMap(address);
                if (resultAddress != null)
                {
                    txtProvince.Text = resultAddress.Result.AddressComponent.Province.Trim();
                    txtCity.Text = resultAddress.Result.AddressComponent.City.Trim();
                    txtCounty.Text = resultAddress.Result.AddressComponent.District.Trim();
                    txtAddress.Text = address.Replace(txtProvince.Text, "").Replace(txtCity.Text, "").Replace(txtCounty.Text, "");
                    if (!string.IsNullOrEmpty(txtPostCode.Text))
                    {
                        txtAddress.Text = txtAddress.Text.Replace(txtPostCode.Text, "");
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("识别内容不能为空", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtContent.Focus();
                txtContent.Select();
            }
        }

        /// <summary>
        /// 保存联系人
        /// </summary>
        /// <param name="isSendMan"></param>
        void AddMan(bool isSendMan)
        {
            try
            {
                ZtoUserManager userManager = new ZtoUserManager(BillPrintHelper.DbHelper);
                ZtoUserEntity userEntity = BuildUser(isSendMan);
                // 新增
                var result = userManager.Add(userEntity, true);
                if (!string.IsNullOrEmpty(result))
                {
                    XtraMessageBox.Show(@"保存成功。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show(@"保存失败。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        ZtoUserEntity BuildUser(bool isSendMan)
        {
            ZtoUserEntity userEntity = new ZtoUserEntity();
            userEntity.Realname = txtRealName.Text.Trim();
            userEntity.Province = txtProvince.Text;
            userEntity.City = txtCity.Text;
            userEntity.County = txtCounty.Text;
            userEntity.Address = txtAddress.Text;
            userEntity.Company = txtCompany.Text;
            userEntity.TelePhone = txtTelephone.Text;
            userEntity.Mobile = txtMobile.Text.Trim();
            userEntity.Postcode = txtPostCode.Text.Trim();
            userEntity.IsDefault = "0";
            userEntity.Issendorreceive = isSendMan ? "1" : "0";
            return userEntity;
        }
        void ClearContent()
        {
            var controls = grpResult.Controls;
            foreach (var control in controls)
            {
                var textEdit = control as TextEdit;
                if (textEdit != null)
                {
                    textEdit.Text = "";
                }
            }
        }

        private void FrmRecognitionAddress_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 识别淘宝收货人地址
        /// </summary>
        /// <returns></returns>
        bool RecognitionTaoBao()
        {
            try
            {
                var content = txtContent.Text.Replace("，", ",").Replace(" ", "").Replace("\r\n", "");
                var spliteArray = content.Split(',');
                var count = 0;
                // 第一种情况 赵昌武，13917557858，江苏省 徐州市 沛县 张庄镇 崔寨村马楼196号 ，221600
                // 第二种情况 江苏省徐州市沛县 张庄镇 崔寨村马楼196号，221600，赵昌武， 13917557858
                // 第三种情况 赵昌武，13917557858，，江苏省 徐州市 沛县 张庄镇 崔寨村马楼196号 ，221600
                // 第四种情况 安徽省淮南市田家庵区 舜耕镇 安徽理工大学本部28层公寓一号楼。，232001，戴金旺，  
                foreach (var s in spliteArray)
                {
                    if (string.IsNullOrEmpty(s))
                    {
                        continue;
                    }
                    if (DotNet.Utilities.ValidateUtil.IsChineseCharacters(s))
                    {
                        if (s.Length <= 4)
                        {
                            txtRealName.Text = s;
                            count++;
                        }
                    }
                    if (DotNet.Utilities.ValidateUtil.IsMobile(s))
                    {
                        txtMobile.Text = s;
                        count++;
                    }
                    if ((Regex.IsMatch(s, @"^\d{3,4}-\d{6,8}$", RegexOptions.IgnoreCase) || Regex.IsMatch(s, @"^\d{8}$", RegexOptions.IgnoreCase)) && (!DotNet.Utilities.ValidateUtil.IsMobile(s)))
                    {
                        txtTelephone.Text = s;
                        count++;
                    }
                    if (Regex.IsMatch(s, @"^\d{6}$", RegexOptions.IgnoreCase) && s.Length == 6)
                    {
                        txtPostCode.Text = s;
                        count++;
                    }
                }
                var address = content.Replace(",", "");
                if (!string.IsNullOrEmpty(txtRealName.Text))
                {
                    address = address.Replace(txtRealName.Text, "");
                }
                if (!string.IsNullOrEmpty(txtMobile.Text))
                {
                    address = address.Replace(txtMobile.Text, "");
                }
                if (!string.IsNullOrEmpty(txtTelephone.Text))
                {
                    address = address.Replace(txtTelephone.Text, "");
                }
                if (!string.IsNullOrEmpty(txtPostCode.Text))
                {
                    address = address.Replace(txtPostCode.Text, "");
                }
                //var result = BaiduHelper.GetProvCityDistFromBaiduMap(address);
                //if (result != null)
                //{
                //    txtProvince.Text = result.Result.AddressComponent.Province.Trim();
                //    txtCity.Text = result.Result.AddressComponent.City.Trim();
                //    txtCounty.Text = result.Result.AddressComponent.District.Trim();
                //    txtAddress.Text = address.Replace(txtProvince.Text, "").Replace(txtCity.Text, "").Replace(txtCounty.Text, "");
                //    count++;
                //}
                return count > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 识别京东收货人地址
        /// </summary>
        /// <returns></returns>
        bool RecognitionJD()
        {
            try
            {
                /*
                     * 收 货 人：劉志
                       地    址：广东佛山市顺德区陈村镇石洲村太平街路29号
                       手机号码：13980755856
                     */
                var count = 0;
                var jdContent = txtContent.Text.Replace("：", ":").Split(new string[] { "\r\n" }, StringSplitOptions.None);
                foreach (var s in jdContent)
                {
                    if (s.Replace(" ", "").Contains("收货人"))
                    {
                        txtRealName.Text = s.Replace(" ", "").Replace("收货人:", "");
                        count++;
                    }
                    if (s.Replace(" ", "").Contains("手机号码"))
                    {
                        txtMobile.Text = s.Replace(" ", "").Replace("手机号码:", "");
                        count++;
                    }
                    if (s.Replace(" ", "").Contains("地址"))
                    {
                        var address = s.Replace(" ", "").Replace("地址:", "");
                        var result = BaiduMapHelper.GetProvCityDistFromBaiduMap(address);
                        if (result != null)
                        {
                            txtProvince.Text = result.Result.AddressComponent.Province.Trim();
                            txtCity.Text = result.Result.AddressComponent.City.Trim();
                            txtCounty.Text = result.Result.AddressComponent.District.Trim();
                            txtAddress.Text = address.Replace(txtProvince.Text, "").Replace(txtCity.Text, "").Replace(txtCounty.Text, "");
                            count++;
                        }
                    }
                }
                return count > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 识别其他收货人地址
        /// </summary>
        /// <returns></returns>
        bool RecognitionOther()
        {
            try
            {
                var content = txtContent.Text;
                var spliteArray = txtContent.Text.Split(' ');
                var count = 0;
                BaseAreaManager areaManager = new BaseAreaManager(BillPrintHelper.DbHelper);
                // 第一种情况 周天颖 13916641126 上海 上海市 嘉定区 嘉定镇街道 上海市嘉定区城中路20号上海大学C楼507室 
                // 第二种情况 河北省  廊坊市  三河市  燕郊經濟技術開發區北京东燕郊开发区燕顺路星河皓月小区A9号楼4单元101  065201  丁雪莲  15811223286
                foreach (var s in spliteArray)
                {
                    if (string.IsNullOrEmpty(s))
                    {
                        continue;
                    }
                    if (DotNet.Utilities.ValidateUtil.IsChineseCharacters(s))
                    {
                        if (!areaManager.Exists(new KeyValuePair<string, object>(BaseAreaEntity.FieldFullName, s)) || !areaManager.Exists(new KeyValuePair<string, object>(BaseAreaEntity.FieldShortName, s)))
                        {
                            if (s.Length <= 4)
                            {
                                txtRealName.Text = s;
                                count++;
                            }
                        }
                    }
                    if (DotNet.Utilities.ValidateUtil.IsMobile(s))
                    {
                        txtMobile.Text = s;
                        count++;
                    }
                    if ((Regex.IsMatch(s, @"^\d{3,4}-\d{6,8}$", RegexOptions.IgnoreCase) || Regex.IsMatch(s, @"^\d{8}$", RegexOptions.IgnoreCase)) && (!DotNet.Utilities.ValidateUtil.IsMobile(s)))
                    {
                        txtTelephone.Text = s;
                        count++;
                    }
                    if (Regex.IsMatch(s, @"^\d{6}$", RegexOptions.IgnoreCase) && s.Length == 6)
                    {
                        txtPostCode.Text = s;
                        count++;
                    }
                }
                var address = content.Replace(" ", "");
                if (!string.IsNullOrEmpty(txtRealName.Text))
                {
                    address = address.Replace(txtRealName.Text, "");
                }
                if (!string.IsNullOrEmpty(txtMobile.Text))
                {
                    address = address.Replace(txtMobile.Text, "");
                }
                if (!string.IsNullOrEmpty(txtTelephone.Text))
                {
                    address = address.Replace(txtTelephone.Text, "");
                }
                if (!string.IsNullOrEmpty(txtPostCode.Text))
                {
                    address = address.Replace(txtPostCode.Text, "");
                }
                //var result = BaiduHelper.GetProvCityDistFromBaiduMap(address);
                //if (result != null)
                //{
                //    txtProvince.Text = result.Result.AddressComponent.Province.Trim();
                //    txtCity.Text = result.Result.AddressComponent.City.Trim();
                //    txtCounty.Text = result.Result.AddressComponent.District.Trim();
                //    txtAddress.Text = address.Replace(txtProvince.Text, "").Replace(txtCity.Text, "").Replace(txtCounty.Text, "");
                //    count++;
                //}
                return count > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 粘贴为发件人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPasteSendMan_Click(object sender, EventArgs e)
        {
            ZtoUserEntity userEntity = BuildUser(true);
            if (this.Owner != null)
            {
                this.Owner.Tag = userEntity;
            }
            else
            {
                this.Tag = userEntity;
            }
            Close();
        }

        /// <summary>
        /// 粘贴为收件人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPasteReceiveMan_Click(object sender, EventArgs e)
        {
            ZtoUserEntity userEntity = BuildUser(false);
            if (this.Owner != null)
            {
                this.Owner.Tag = userEntity;
            }
            else
            {
                this.Tag = userEntity;
            }
            Close();
        }
    }
}
