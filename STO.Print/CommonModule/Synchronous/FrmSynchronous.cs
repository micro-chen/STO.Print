//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Text;
using STO.Print.AddBillForm;

namespace STO.Print.Synchronous
{
    using DevExpress.Utils;
    using DevExpress.XtraEditors.Repository;
    using DotNet.Business;
    using DotNet.Model;
    using DotNet.Utilities;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>
    /// 同步基础资料
    /// 
    /// 修改纪录
    /// 
    ///     2015-07-28 版本：1.0 YangHengLian 创建。
    ///     2016-1-25 把历史同步去掉了，没有用的功能早点去掉，这样软件还高大上一点，不然误导用户
    ///		
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-07-28</date>
    /// </author> 
    /// </summary>
    public partial class FrmSynchronous : BaseForm
    {
        public FrmSynchronous()
        {
            InitializeComponent();
        }

        RepositoryItemProgressBar progressbar = new RepositoryItemProgressBar();

        private List<SyncEntity> syncEntities = new List<SyncEntity>();

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Load(object sender, EventArgs e)
        {
            progressbar.LookAndFeel.UseDefaultLookAndFeel = false;
            progressbar.ShowTitle = true;
            // 皮肤,这里使用的Dev自带的几款皮肤之一
            progressbar.LookAndFeel.SkinName = "Money Twins";
            // progressbar.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
            InitData();
            InitSyncTime();
        }

        /// <summary>
        /// 初始化表格数据
        /// </summary>
        public void InitData()
        {
            syncEntities.Clear();
            syncEntities.Add(new SyncEntity() { Id = "1", Name = "省市区基础资料", Count = 100, Index = 0, Remark = "" });
            syncEntities.Add(new SyncEntity() { Id = "2", Name = "大头笔基础资料", Count = 100, Index = 0, Remark = "" });
            gridControl1.DataSource = syncEntities;
            gridView1.Columns["Count"].Visible = false;
            gridView1.Columns["Id"].Visible = false;
            gridView1.Columns["Index"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
        }

        private void InitSyncTime()
        {
            var syncTime = BillPrintHelper.GetSyncTime();
            if (string.IsNullOrEmpty(syncTime))
            {
                lblSyncTime.Text = string.Empty;
            }
            else
            {
                lblSyncTime.Text = string.Format("上次同步时间：{0}", syncTime);
            }
            if (!NetworkHelper.IsConnectedInternet())
            {
                btnSync.Enabled = false;
                lblSyncTime.Text = lblSyncTime.Text + @" 未连接网络无法同步";
            }
        }

        /// <summary>
        /// 行单元格编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "Index")
            {
                int count = (int)gridView1.GetRowCellValue(e.RowHandle, "Count");
                progressbar.Maximum = count;
                e.RepositoryItem = progressbar;
            }
        }

        /// <summary>
        /// 行单元格渲染事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "Index")
            {
                int count = (int)gridView1.GetRowCellValue(e.RowHandle, "Count");
                int index = (int)e.CellValue;
                e.DisplayText = string.Format("{0}/{1}", index, count);
            }
        }

        /// <summary>
        /// 全部同步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSync_Click(object sender, EventArgs e)
        {
            try
            {
                //if (BaseSystemInfo.Synchronized)
                //{
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                gridView1.GroupPanelText = @"开始同步";
                btnSync.Enabled = false;
                SyncArea();
                SyncPrintMark();
                BaseSystemInfo.Synchronized = true;
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                string elapsedTime = string.Format("{0}h{1}m{2}s{3}ms", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
                gridView1.GroupPanelText = string.Format("同步完成，耗时：{0}", elapsedTime);
                BillPrintHelper.SetSyncTime();
                InitSyncTime();
                //}
                //else
                //{
                //    XtraMessageBox.Show(@"后台正在同步，请稍后", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            catch (Exception exception)
            {
                gridView1.GroupPanelText = @"同步失败";
                ProcessException(exception);
            }
            finally
            {
                btnSync.Enabled = true;
            }
        }

        /// <summary>
        /// 渲染表格序号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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


        /// <summary>
        /// 同步大头笔
        /// </summary>
        private void SyncPrintMark()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var dbHelper = DbHelperFactory.GetHelper(CurrentDbType.SQLite, BillPrintHelper.BillPrintConnectionString);
            var entity = syncEntities.Find(p => p.Id == "2");
            entity.Index = 0;
            entity.Remark = "开始同步";
            gridControl1.RefreshDataSource();
            DateTime? modifiedOn = new DateTime(2014, 01, 01);
            SQLBuilder sqlBuilder = new SQLBuilder(dbHelper);
            dbHelper.BeginTransaction();
            const string url = "http://userCenter.zt-express.com/WebAPIV42/API/Synchronous/GetTopLimitTable";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection
            {
                {"userInfo", BaseSystemInfo.UserInfo.Serialize()},
                {"systemCode", BaseSystemInfo.SystemCode},
                {"securityKey", BaseSystemInfo.SecurityKey}, 
                {"dataBase", "UserCenter"}, 
                {"tableName", BaseAreaProvinceMarkEntity.TableName}, 
                {"toTableName", BaseAreaProvinceMarkEntity.TableName}, 
                {"topLimit", "10000"}, 
                {"modifiedOn", modifiedOn.Value.ToString(BaseSystemInfo.DateTimeFormat)}
            };
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                dbHelper.ExecuteNonQuery("DELETE FROM " + BaseAreaProvinceMarkEntity.TableName);
                var dataTable = (DataTable)JsonConvert.DeserializeObject(response, typeof(DataTable));
                int r;
                entity.Count = dataTable.Rows.Count;
                entity.Remark = "正在同步";
                gridControl1.RefreshDataSource();
                for (r = 0; r < dataTable.Rows.Count; r++)
                {
                    // 然后插入数据
                    sqlBuilder.BeginInsert(BaseAreaProvinceMarkEntity.TableName);
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        sqlBuilder.SetValue(dataTable.Columns[i].ColumnName, dataTable.Rows[r][dataTable.Columns[i].ColumnName]);
                    }
                    sqlBuilder.EndInsert();
                    entity.Index++;
                    gridControl1.RefreshDataSource();
                }
                // 批量提交
                try
                {
                    dbHelper.CommitTransaction();
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                    dbHelper.RollbackTransaction();
                }
                finally
                {
                    dbHelper.Close();
                    dbHelper.Dispose();
                }
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                string elapsedTime = string.Format("{0}h{1}m{2}s{3}ms", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
                entity.Remark = "同步完成，耗时：" + elapsedTime;
                gridControl1.RefreshDataSource();
            }
        }

        /// <summary>
        /// 同步省市区
        /// </summary>
        private void SyncArea()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var dbHelper = DbHelperFactory.GetHelper(CurrentDbType.SQLite, BillPrintHelper.BillPrintConnectionString);
            var entity = syncEntities.Find(p => p.Id == "1");
            entity.Index = 0;
            entity.Remark = "开始同步";
            gridControl1.RefreshDataSource();
            DateTime? modifiedOn = new DateTime(2014, 01, 01);
            SQLBuilder sqlBuilder = new SQLBuilder(dbHelper);
            dbHelper.BeginTransaction();
            const string url = "http://userCenter.zt-express.com/WebAPIV42/API/Synchronous/GetTopLimitTable";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection
            {
                {"userInfo", BaseSystemInfo.UserInfo.Serialize()},
                {"systemCode", BaseSystemInfo.SystemCode},
                {"securityKey", BaseSystemInfo.SecurityKey}, 
                {"dataBase", "UserCenter"}, 
                {"tableName", BaseAreaEntity.TableName}, 
                {"toTableName", BaseAreaEntity.TableName}, 
                {"topLimit", "10000"}, 
                {"modifiedOn", modifiedOn.Value.ToString(BaseSystemInfo.DateTimeFormat)}
            };
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                dbHelper.ExecuteNonQuery("DELETE FROM " + BaseAreaEntity.TableName);
                var dataTable = (DataTable)JsonConvert.DeserializeObject(response, typeof(DataTable));
                int r;
                entity.Count = dataTable.Rows.Count;
                entity.Remark = "正在同步";
                gridControl1.RefreshDataSource();
                for (r = 0; r < dataTable.Rows.Count; r++)
                {
                    sqlBuilder.BeginInsert(BaseAreaEntity.TableName);
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        sqlBuilder.SetValue(dataTable.Columns[i].ColumnName, dataTable.Rows[r][dataTable.Columns[i].ColumnName]);
                    }
                    sqlBuilder.EndInsert();
                    entity.Index++;
                    gridControl1.RefreshDataSource();
                }
                // 批量提交
                try
                {
                    dbHelper.CommitTransaction();
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                    dbHelper.RollbackTransaction();
                }
                finally
                {
                    dbHelper.Close();
                    dbHelper.Dispose();
                }
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                string elapsedTime = string.Format("{0}h{1}m{2}s{3}ms", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
                entity.Remark = "同步完成，耗时：" + elapsedTime;
                gridControl1.RefreshDataSource();
            }
        }
    }

    public class SyncEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public int Index { get; set; }
        public string Remark { get; set; }

    }
}
