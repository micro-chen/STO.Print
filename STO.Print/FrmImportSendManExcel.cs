//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , ZTO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ZTO.Print.AddBillForm;

namespace ZTO.Print
{
    using DevExpress.Utils;
    using DevExpress.XtraEditors;
    using DotNet.Business;
    using DotNet.Model;
    using DotNet.Utilities;
    using Model;
    using Utilities;
    using ZTO.Print.Manager;

    /// <summary>
    /// Excel导入窗体（收件人）
    /// 
    /// 修改记录
    ///     
    ///     2015-08-31 版本：1.0 YangHengLian 创建
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-08-31</date>
    /// </author>
    /// </summary>

    public partial class FrmImportSendManExcel : BaseForm
    {
        private readonly BaseAreaManager areaManager = new BaseAreaManager(BillPrintHelper.DbHelper);

        public FrmImportSendManExcel()
        {
            InitializeComponent();
        }

        #region private void FrmImportExcel_Load(object sender, EventArgs e) 窗体加载事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmImportExcel_Load(object sender, EventArgs e)
        {
            GridDataBind();
            var gridLocalizer = BuilderGridLocalizer.SetGridLocalizer();
            BuilderGridLocalizer.CustomButtonText(gridViewImportExcel, gridLocalizer);
            gridViewImportExcel.OptionsFind.AlwaysVisible = true;
            gridViewImportExcel.FindPanelVisible = true;
            gridViewImportExcel.ShowFindPanel();
            txtFileFullPath.Text = BillPrintHelper.GetDefaultExcelPath();
        }
        #endregion

        #region private void btnOpenExcel_Click(object sender, EventArgs e)
        /// <summary>
        /// 浏览打开本地磁盘上的EXCEL文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenExcel_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog { Filter = @"Excel文件|*.xls;*.xlsx" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtFileFullPath.Text = ofd.FileName;
                }
            }
        }
        #endregion

        #region public bool Import() 导入Excel数据到本地数据库
        /// <summary>
        /// 导入Excel数据到本地数据库
        /// </summary>
        public bool Import()
        {
            if (string.IsNullOrEmpty(txtFileFullPath.Text.Trim()))
            {
                XtraMessageBox.Show(@"请选择录单模板", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnOpenExcel_Click(this, null);
                return false;
            }
            if (!File.Exists(txtFileFullPath.Text))
            {
                XtraMessageBox.Show(@"选中文件不存在，请重新选择导入Excel文件", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnOpenExcel_Click(this, null);
                return false;
            }
            var startDateTime = DateTime.Now;
            if (!splashScreenManagerImportExcel.IsSplashFormVisible)
            {
                splashScreenManagerImportExcel.ShowWaitForm();
            }
            Application.DoEvents();
            splashScreenManagerImportExcel.SetWaitFormCaption("请稍后");
            splashScreenManagerImportExcel.SetWaitFormDescription("开始导入Excel数据...");
            try
            {
                DataTable chooseDt = ExcelHelper.ExcelToDataTable(txtFileFullPath.Text.Trim(), 38, 0, 3);
                if (chooseDt != null && chooseDt.Rows.Count > 0)
                {
                    var list = new List<ZtoUserEntity>();
                    int temp = 0;
                    foreach (DataRow dr in chooseDt.Rows)
                    {
                        ++temp;
                        splashScreenManagerImportExcel.SetWaitFormDescription(string.Format("正在导入Excel数据：{0}/{1}", temp, chooseDt.Rows.Count));
                        ZtoUserEntity userEntity = new ZtoUserEntity();
                        userEntity.Realname = BaseBusinessLogic.ConvertToString(dr[2]);
                        if (ValidateUtil.IsMobile(BaseBusinessLogic.ConvertToString(dr[3])))
                        {
                            userEntity.Mobile = BaseBusinessLogic.ConvertToString(dr[3]);
                        }
                        else
                        {
                            userEntity.TelePhone = BaseBusinessLogic.ConvertToString(dr[3]);
                        }
                        userEntity.Address = BaseBusinessLogic.ConvertToString(dr[7]);
                        var tempProvince = BaseBusinessLogic.ConvertToString(dr[4]);
                        // 收件人地址（有可能用户不填写收件省市区）
                        if (tempProvince != null)
                        {
                            userEntity.Province = tempProvince.Trim();
                        }
                        var tempCity = BaseBusinessLogic.ConvertToString(dr[5]);
                        if (tempCity != null)
                        {
                            userEntity.City = tempCity.Trim();
                        }
                        var tempCounty = BaseBusinessLogic.ConvertToString(dr[6]);
                        if (tempCounty != null)
                        {
                            userEntity.County = tempCounty.Trim();
                        }
                        if (string.IsNullOrEmpty(userEntity.Province) && string.IsNullOrEmpty(userEntity.City) && string.IsNullOrEmpty(userEntity.County))
                        {
                            if (!string.IsNullOrEmpty(userEntity.Address))
                            {
                                var result = BaiduMapHelper.GetProvCityDistFromBaiduMap(userEntity.Address);
                                if (result != null)
                                {
                                    userEntity.Province = result.Result.AddressComponent.Province;
                                    userEntity.City = result.Result.AddressComponent.City;
                                    userEntity.County = result.Result.AddressComponent.District;
                                }
                            }
                        }
                        userEntity.Company = BaseBusinessLogic.ConvertToString(dr[27]);
                        userEntity.Postcode = BaseBusinessLogic.ConvertToString(dr[29]);
                        userEntity.Issendorreceive = "1";
                        if (string.IsNullOrEmpty(userEntity.Postcode))
                        {
                            if (!string.IsNullOrEmpty(userEntity.City) && !string.IsNullOrEmpty(userEntity.County))
                            {
                                userEntity.Postcode = NetworkHelper.GetPostCodeByAddress(userEntity.City, userEntity.County);
                            }
                        }
                        if (!string.IsNullOrEmpty(userEntity.Province))
                        {
                            var areaEntity = areaManager.GetList<BaseAreaEntity>(new KeyValuePair<string, object>(BaseAreaEntity.FieldFullName, userEntity.Province)).FirstOrDefault();
                            if (areaEntity != null)
                            {
                                userEntity.ProvinceId = areaEntity.Id;
                            }
                        }
                        if (!string.IsNullOrEmpty(userEntity.City))
                        {
                            var areaEntity = areaManager.GetList<BaseAreaEntity>(new KeyValuePair<string, object>(BaseAreaEntity.FieldFullName, userEntity.City)).FirstOrDefault();
                            if (areaEntity != null)
                            {
                                userEntity.CityId = areaEntity.Id;
                            }
                        }
                        if (!string.IsNullOrEmpty(userEntity.County))
                        {
                            var areaEntity = areaManager.GetList<BaseAreaEntity>(new KeyValuePair<string, object>(BaseAreaEntity.FieldFullName, userEntity.County)).FirstOrDefault();
                            if (areaEntity != null)
                            {
                                userEntity.CountyId = areaEntity.Id;
                            }
                        }
                        userEntity.CreateOn = DateTime.Now;
                        list.Add(userEntity);
                    }
                    using (IDbHelper dbHelper = DbHelperFactory.GetHelper(CurrentDbType.SQLite, BillPrintHelper.BillPrintConnectionString))
                    {
                        try
                        {
                            dbHelper.BeginTransaction();
                            var manager = new ZtoUserManager(dbHelper);
                            foreach (ZtoUserEntity ztoUserEntity in list)
                            {
                                manager.Add(ztoUserEntity, true);
                            }
                            dbHelper.CommitTransaction();
                            GridDataBind();
                        }
                        catch (Exception ex)
                        {
                            dbHelper.RollbackTransaction();
                            ProcessException(ex);
                        }
                    }
                    if (splashScreenManagerImportExcel != null && splashScreenManagerImportExcel.IsSplashFormVisible)
                    {
                        splashScreenManagerImportExcel.CloseWaitForm();
                    }
                    GridDataBind();
                    var ts = DateTime.Now - startDateTime;
                    Close();
                }
                else
                {
                    XtraMessageBox.Show(@"模板没有填写任何数据，导入失败", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
                return false;
            }
            finally
            {
                if (splashScreenManagerImportExcel != null && splashScreenManagerImportExcel.IsSplashFormVisible)
                {
                    splashScreenManagerImportExcel.CloseWaitForm();
                }
            }
            return true;
        }
        #endregion

        #region private void btnClose_Click(object sender, EventArgs e) 关闭窗体
        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region private void lblDownload_Click(object sender, EventArgs e) 下载EXCEL数据模版
        /// <summary>
        /// 下载EXCEL数据模版
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblDownload_Click(object sender, EventArgs e)
        {
            DownloadTemplate.DownloadExcelTemplate("ImportBill");
        }
        #endregion

        #region private void GridDataBind() 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void GridDataBind()
        {
            DataTable dt = BillPrintHelper.DbHelper.Fill(BillPrintHelper.CmdStrForSendUser);
            // 增加个CheckBox列
            dt.Columns.Add("Check", typeof(bool));
            // 设置选择列的位置
            dt.Columns["Check"].SetOrdinal(0);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Check"] = false;
                var value = dt.Rows[i]["默认发件人"].ToString();
                if (!string.IsNullOrEmpty(value))
                {
                    if (value == "1")
                    {
                        dt.Rows[i]["默认发件人"] = "是";
                    }
                    else
                    {
                        dt.Rows[i]["默认发件人"] = "否";
                    }
                }
            }
            // 设置gridview列头的字体大小
            gridViewImportExcel.Appearance.HeaderPanel.Font = new Font("Tahoma", 9);
            // 设置gridview列头居中
            gridViewImportExcel.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;
            gridControlImportExcel.DataSource = dt;
            gridViewImportExcel.OptionsView.ColumnAutoWidth = false;
            gridViewImportExcel.Columns["Id"].Visible = false;
            gridViewImportExcel.Columns["Check"].Width = 22;
            gridViewImportExcel.Columns["Check"].OptionsColumn.ShowCaption = false;
            gridViewImportExcel.Columns["Check"].OptionsColumn.AllowSort = DefaultBoolean.False;
            gridViewImportExcel.Columns["Check"].OptionsColumn.AllowEdit = false;
            gridViewImportExcel.Columns["姓名"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            gridViewImportExcel.Columns["姓名"].SummaryItem.DisplayFormat = @"总计：{0}";
            gridViewImportExcel.Columns["姓名"].Width = 100;
            gridViewImportExcel.Columns["省份"].Width = 100;
            gridViewImportExcel.Columns["城市"].Width = 80;
            gridViewImportExcel.Columns["区县"].Width = 80;
            gridViewImportExcel.Columns["单位"].Width = 80;
            gridViewImportExcel.Columns["部门"].Width = 80;
            gridViewImportExcel.Columns["电话"].Width = 120;
            gridViewImportExcel.Columns["手机"].Width = 120;
            gridViewImportExcel.Columns["邮编"].Width = 80;
            gridViewImportExcel.Columns["详细地址"].Width = 200;
            gridViewImportExcel.Columns["备注"].Width = 150;
        }
        #endregion

        #region private void btnOpenFile_Click(object sender, EventArgs e) 打开Excel文件
        /// <summary>
        /// 打开Excel文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtFileFullPath.Text))
                {
                    Process.Start(txtFileFullPath.Text);
                }
                else
                {
                    XtraMessageBox.Show("请选择Excel文件", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                ProcessException(exception);
            }
        }
        #endregion

        #region private void gridViewImportExcel_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e) 渲染GridControl序号
        /// <summary>
        /// 渲染GridControl序号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewImportExcel_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
            if (e.Info.IsRowIndicator)
            {
                if (e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString(CultureInfo.InvariantCulture);
                }
                else if (e.RowHandle < 0 && e.RowHandle > -1000)
                {
                    e.Info.Appearance.BackColor = Color.AntiqueWhite;
                    e.Info.DisplayText = "G" + e.RowHandle;
                }
            }
        }
        #endregion

        #region private void FrmImportExcel_FormClosing(object sender, FormClosingEventArgs e) 窗体正在关闭事件
        /// <summary>
        /// 窗体正在关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmImportExcel_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFileFullPath.Text.Trim()))
            {
                if (File.Exists(txtFileFullPath.Text))
                {
                    if (XtraMessageBox.Show(@"确定关闭当前窗体吗？", AppMessage.MSG0000, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        BillPrintHelper.SaveDefaultExcelPath(txtFileFullPath.Text);
                        DialogResult = DialogResult.OK;
                    }
                }
            }
        }
        #endregion

        #region private void btnSave_Click(object sender, EventArgs e) 保存收件人记录，并且关闭
        /// <summary>
        /// 保存收件人记录，并且关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Enabled = false;
                Import();
            }
            catch (Exception exception)
            {
                ProcessException(exception);
            }
            finally
            {
                btnSave.Enabled = true;
            }
        }
        #endregion
    }
}
