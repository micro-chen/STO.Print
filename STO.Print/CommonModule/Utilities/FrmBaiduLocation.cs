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
    using Newtonsoft.Json;
    using STO.Print.Utilities;

    /// <summary>
    /// 通过百度地图识别地址的完整信息
    /// 
    /// 修改记录
    ///     
    ///     2015-07-20 版本：1.0 YangHengLian 创建
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-11-12</date>
    /// </author>
    /// </summary>
    public partial class FrmBaiduLocation : BaseForm
    {
        public FrmBaiduLocation()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Recognition();
        }

        private void btnDemo_Click(object sender, EventArgs e)
        {
            txtAddress.Text = "上海市青浦区华新镇华志路1685号";
            Recognition();
        }

        void Recognition()
        {
            if (string.IsNullOrEmpty(txtAddress.Text.Trim()))
            {
                XtraMessageBox.Show("地址必填", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAddress.Focus();
                txtAddress.Select();
            }
            var baiAddressEntity = BaiduMapHelper.GetProvCityDistFromBaiduMap(txtAddress.Text);
            if (baiAddressEntity != null)
            {
                txtJson.Text = JsonConvert.SerializeObject(baiAddressEntity);
                // {"status":0,"result":{"location":{"lng":117.27923772506,"lat":23.787243812736},"formatted_address":"福建省漳州市诏安县G324","business":"","addressComponent":{"city":"漳州市","country":"中国","direction":"","distance":"","district":"诏安县","province":"福建省","street":"G324","street_number":"","country_code":0},"poiRegions":[],"sematic_description":"诏安县四都派出所东北78米","cityCode":255}}
                txtCountry.Text = baiAddressEntity.Result.AddressComponent.Country;
                txtProvinceCityCounty.Text = string.Format("{0}-{1}-{2}",
                                                           baiAddressEntity.Result.AddressComponent.Province,
                                                           baiAddressEntity.Result.AddressComponent.City,
                                                           baiAddressEntity.Result.AddressComponent.District);
                txtTown.Text = "";
                txtStreet.Text = baiAddressEntity.Result.AddressComponent.Street;
                // 
                txtLocation.Text = baiAddressEntity.Result.Location.Lng + "," + baiAddressEntity.Result.Location.Lat;
            }
            else
            {
                XtraMessageBox.Show("未能识别成功", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
