//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using STO.Print.AddBillForm;

namespace STO.Print
{
    using DevExpress.XtraEditors;
    using DotNet.Utilities;
    using STO.Print.Manager;
    using STO.Print.Model;
    using STO.Print.Utilities;

    /// <summary>
    /// 导入Excel选择字段窗体
    ///
    /// 修改纪录
    ///
    ///		2015-09-21  版本：1.0 YangHengLian 创建主键，注意命名空间的排序。
    ///       2016-2-1   YangHengLian添加可以选择发件人来进行导入，这样可以解决只读取默认发件人的问题，每次都切换发件人很麻烦的。
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-09-21</date>
    /// </author>
    /// </summary>
    public partial class FrmSelectFieldForImportFreeExcel : BaseForm
    {
        readonly DataTable _execldt = new DataTable();

        readonly List<string> _removeItemsArray = new List<string>
                                                    {
                                                        "发件人姓名", "发件电话", "始发地", 
                                                        "发件省份", "发件城市", "发件区县",
                                                        "发件详细地址","发件日期","发件网点",
                                                        "发件单位","发件部门","发件邮编"
                                                    };

        public FrmSelectFieldForImportFreeExcel()
        {
            InitializeComponent();
        }

        public FrmSelectFieldForImportFreeExcel(DataTable excelDataTable)
        {
            InitializeComponent();
            BindColumn(excelDataTable);
            _execldt = excelDataTable;
        }

        /// <summary>
        /// 绑定导入列和目标列
        /// </summary>
        /// <param name="dt"></param>
        public void BindColumn(DataTable dt)
        {
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName == "创建时间")
                {
                    continue;
                }
                lbSource.Items.Add(dt.Columns[i].ColumnName);
            }
            var targetDataTable = BillPrintHelper.DbHelper.Fill(BillPrintHelper.CmdStrForZtoBillPrinter);
            if (targetDataTable.Columns.Contains("Id"))
            {
                targetDataTable.Columns.Remove("Id");
            }
            if (targetDataTable.Columns.Contains("创建时间"))
            {
                targetDataTable.Columns.Remove("创建时间");
            }
            if (targetDataTable.Columns.Contains("单号"))
            {
                targetDataTable.Columns.Remove("单号");
            }
            for (int i = 0; i < targetDataTable.Columns.Count; i++)
            {
                lbTarget.Items.Add(targetDataTable.Columns[i].ColumnName);
            }
            // 是否使用默认发件人，如果使用默认发件人那么就要把邮编的一些项删除，不进行导入，从系统默认发件人来提取
            if (ckUseDefaultSendMan.Checked)
            {
                foreach (var tempItem in _removeItemsArray)
                {
                    if (lbTarget.Items.Contains(tempItem))
                    {
                        lbTarget.Items.Remove(tempItem);
                    }
                }
            }
        }

        private void ImpEnd()
        {
            try
            {
                ImprnBut.Enabled = lbOpposite.Items.Count > 0;
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        /// <summary>
        /// 控制按钮 
        /// </summary>
        private void ButtEnd()
        {
            if (lbSource.SelectedItem != null && lbTarget.SelectedItem != null)
            {
                butOnAdd.Enabled = true;
            }
            else
            {
                butOnAdd.Enabled = false;
            }
            if (lbSource.SelectedItem != null)
            {
                list1Up.Enabled = lbSource.SelectedIndex != 0;
                list1Next.Enabled = lbSource.SelectedIndex != lbSource.Items.Count - 1;
            }
            else
            {
                list1Up.Enabled = false;
                list1Next.Enabled = false;
            }
        }

        #region private void butOnAdd_Click(object sender, EventArgs e) 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButOnAddClick(object sender, EventArgs e)
        {
            lbOpposite.Items.Add(lbSource.SelectedItem + "-" + lbTarget.SelectedItem);
            lbSource.Items.Remove(lbSource.SelectedItem.ToString());
            lbTarget.Items.Remove(lbTarget.SelectedItem.ToString());
            ImpEnd();
        }
        #endregion

        #region private void butRemove_Click(object sender, EventArgs e) 移除
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butRemove_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lbOpposite.SelectedItems.Count; i++)
            {
                string str = lbOpposite.SelectedItems[i].ToString();
                lbSource.Items.Add(str.Split('-')[0]);
                lbTarget.Items.Add(str.Split('-')[1]);
                lbOpposite.Items.Remove(str);
                i--;
            }
            //移除选中的
            ImpEnd();
        }
        #endregion

        #region  private void ImprnBut_Click(object sender, EventArgs e) 导入
        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImprnBut_Click(object sender, EventArgs e)
        {
            try
            {
                ImprnBut.Enabled = false;
                DateTime start = DateTime.Now;
                if (lbOpposite.Items.Count > 0)
                {
                    var keyValuePairs = new List<KeyValuePair<string, string>>();
                    foreach (var item in lbOpposite.Items)
                    {
                        var tempArray = item.ToString().Split('-');
                        keyValuePairs.Add(new KeyValuePair<string, string>(tempArray[0], tempArray[1]));
                    }
                    if (keyValuePairs.Count > 0)
                    {
                        var listField = new List<string>();
                        var listValue = new List<string>();
                        var listCommandText = new List<string>();
                        List<ZtoUserEntity> userList = null;
                        if (ckUseDefaultSendMan.Checked)
                        {
                            var userManager = new ZtoUserManager(BillPrintHelper.DbHelper);
                            // 获取默认发件人
                            userList = userManager.GetList<ZtoUserEntity>(new KeyValuePair<string, object>(ZtoUserEntity.FieldIsDefault, 1), new KeyValuePair<string, object>(ZtoUserEntity.FieldIssendorreceive, 1));
                        }
                        foreach (DataRow data in _execldt.Rows)
                        {
                            foreach (var keyValuePair in keyValuePairs)
                            {
                                // Excel每一列的值
                                var value = data[keyValuePair.Key].ToString();
                                // Excel的值要特殊处理一些符号（不然插入数据库有问题的）
                                if (!string.IsNullOrEmpty(value))
                                {
                                    value = value.Replace("'", "").Replace("\"", "").Replace("=", "").Replace(" ", "");
                                }
                                // 获取到实际的数据库列名称
                                var dataField = BillPrintHelper.GetFieldByName(keyValuePair.Value);
                                listField.Add(dataField);
                                //if (dataField == ZtoPrintBillEntity.FieldOrderNumber)
                                //{
                                //    if (string.IsNullOrEmpty(value))
                                //    {
                                //        value = Guid.NewGuid().ToString("N");
                                //    }
                                //}
                                listValue.Add(value);
                            }
                            // 使用默认发件人
                            if (ckUseDefaultSendMan.Checked)
                            {
                                if (userList.Any())
                                {
                                    var userEntity = userList.FirstOrDefault();
                                    listField.Add(ZtoPrintBillEntity.FieldSendMan);
                                    listValue.Add(userEntity.Realname);
                                    if (!string.IsNullOrEmpty(userEntity.Mobile) && string.IsNullOrEmpty(userEntity.TelePhone))
                                    {
                                        listField.Add(ZtoPrintBillEntity.FieldSendPhone);
                                        listValue.Add(userEntity.Mobile);
                                    }
                                    if (string.IsNullOrEmpty(userEntity.Mobile) && !string.IsNullOrEmpty(userEntity.TelePhone))
                                    {
                                        listField.Add(ZtoPrintBillEntity.FieldSendPhone);
                                        listValue.Add(userEntity.TelePhone);
                                    }
                                    if (!string.IsNullOrEmpty(userEntity.Mobile) && !string.IsNullOrEmpty(userEntity.TelePhone))
                                    {
                                        listField.Add(ZtoPrintBillEntity.FieldSendPhone);
                                        listValue.Add(userEntity.Mobile);
                                    }
                                    listField.Add(ZtoPrintBillEntity.FieldSendProvince);
                                    listValue.Add(userEntity.Province);
                                    listField.Add(ZtoPrintBillEntity.FieldSendCity);
                                    listValue.Add(userEntity.City);
                                    listField.Add(ZtoPrintBillEntity.FieldSendCounty);
                                    listValue.Add(userEntity.County);
                                    listField.Add(ZtoPrintBillEntity.FieldSendDeparture);
                                    listValue.Add(userEntity.Province);
                                    listField.Add(ZtoPrintBillEntity.FieldSendAddress);
                                    listValue.Add(userEntity.Address);
                                    listField.Add(ZtoPrintBillEntity.FieldSendPostcode);
                                    listValue.Add(userEntity.Postcode);
                                    listField.Add(ZtoPrintBillEntity.FieldSendDate);
                                    listValue.Add(DateTime.Now.ToString(BaseSystemInfo.DateFormat));
                                    listField.Add(ZtoPrintBillEntity.FieldCreateOn);
                                    listValue.Add(DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat));
                                }
                            }
                            // 如果没有导入订单号这个字段就要手动导入了，因为这个很重要
                            if (listField.All(p => p != ZtoPrintBillEntity.FieldOrderNumber))
                            {
                                listField.Add(ZtoPrintBillEntity.FieldOrderNumber);
                                listValue.Add(Guid.NewGuid().ToString("N"));
                            }
                            // 到付
                            if (listField.All(p => p != ZtoPrintBillEntity.FieldToPayMent))
                            {
                                listField.Add(ZtoPrintBillEntity.FieldToPayMent);
                                listValue.Add("0");
                            }
                            // 代收
                            if (listField.All(p => p != ZtoPrintBillEntity.FieldGoodsPayMent))
                            {
                                listField.Add(ZtoPrintBillEntity.FieldGoodsPayMent);
                                listValue.Add("0");
                            }
                            string tempCommandText = string.Format("INSERT INTO {0} ({1}) VALUES({2})", ZtoPrintBillEntity.TableName, string.Join(",", listField), "'" + string.Join("','", listValue) + "'");
                            listCommandText.Add(tempCommandText);
                            listField.Clear();
                            listValue.Clear();
                        }
                        if (listCommandText.Count > 0)
                        {
                            var resultCount = 0;
                            // 用事务插入最快还有删除
                            using (IDbHelper dbHelper = DbHelperFactory.GetHelper(CurrentDbType.SQLite, BillPrintHelper.BillPrintConnectionString))
                            {
                                dbHelper.BeginTransaction();
                                foreach (var commandText in listCommandText)
                                {
                                    try
                                    {
                                        resultCount = resultCount + (dbHelper.ExecuteNonQuery(commandText));
                                    }
                                    catch (Exception ex)
                                    {
                                        dbHelper.RollbackTransaction();
                                    }
                                }
                                dbHelper.CommitTransaction();
                            }
                            TimeSpan ts = DateTime.Now - start;
                            XtraMessageBox.Show(string.Format("成功导入{0}条，耗时:{1}", resultCount, string.Format("{0}分{1}秒", ts.Minutes, ts.Seconds)), AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Close();
                        }
                        else
                        {
                            XtraMessageBox.Show("导入失败", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    XtraMessageBox.Show("请从左边选择对应的导入关系列", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message, AppMessage.MSG0000);
            }
            finally
            {
                ImprnBut.Enabled = true;
            }
        }
        #endregion

        #region private void listBox1_SelectedIndexChanged(object sender, EventArgs e) 改变事件
        /// <summary>
        /// 改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ButtEnd();
        }
        #endregion

        #region private void listBox3_SelectedIndexChanged(object sender, EventArgs e) 改变事件
        /// <summary>
        /// 改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            butRemove.Enabled = lbOpposite.SelectedItem != null;
        }
        #endregion

        #region private void Improve_FormClosing(object sender, FormClosingEventArgs e) 关闭窗体
        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Improve_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
        #endregion

        #region  private void butAllAdd_Click(object sender, EventArgs e) 添加全部
        /// <summary>
        /// 添加全部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butAllAdd_Click(object sender, EventArgs e)
        {
            if (lbTarget.Items.Count > 0 && lbSource.Items.Count > 0)
            {
                lbOpposite.Items.Clear();
                var list1Count = lbSource.Items.Count;
                var list2Count = lbTarget.Items.Count;
                var count = list1Count >= list2Count ? list2Count : list1Count;
                for (var i = 0; i < count; i++)
                {
                    if (count > i)
                    {
                        lbOpposite.Items.Add(lbSource.Items[i] + "-" + lbTarget.Items[i]);
                        lbSource.Items.Remove(lbSource.Items[i].ToString());
                        lbTarget.Items.Remove(lbTarget.Items[i].ToString());
                        i--;
                        count--;
                    }
                }
            }
            else
            {
                XtraMessageBox.Show(@"导入数据不能为空。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            ImpEnd();
        }
        #endregion

        #region private void list1Up_Click(object sender, EventArgs e) 上移
        /// <summary>
        /// 上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void list1Up_Click(object sender, EventArgs e)
        {
            int position = lbSource.SelectedIndex;
            string value = lbSource.SelectedItem.ToString();
            if (position == 0)
            {
                return;
            }
            lbSource.Items.RemoveAt(position);
            lbSource.Items.Insert(position - 1, value);
            lbSource.SelectedIndex = position - 1;
        }
        #endregion

        #region  private void list1Next_Click(object sender, EventArgs e) 下移
        /// <summary>
        /// 下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void list1Next_Click(object sender, EventArgs e)
        {
            int position = lbSource.SelectedIndex;
            string value = lbSource.SelectedItem.ToString();
            if (position == lbSource.Items.Count - 1)
            {
                return;
            }
            lbSource.Items.RemoveAt(position);
            lbSource.Items.Insert(position + 1, value);
            lbSource.SelectedIndex = position + 1;
        }
        #endregion

        /// <summary>
        /// 使用默认发件人复选框改变值事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckUseDefaultSendMan_CheckedChanged(object sender, EventArgs e)
        {
            if (!ckUseDefaultSendMan.Checked)
            {
                foreach (var tempItem in _removeItemsArray)
                {
                    if (!lbTarget.Items.Contains(tempItem))
                    {
                        lbTarget.Items.Add(tempItem);
                    }
                }
            }
            else
            {
                foreach (var tempItem in _removeItemsArray)
                {
                    if (lbTarget.Items.Contains(tempItem))
                    {
                        lbTarget.Items.Remove(tempItem);
                    }
                }
            }
        }
    }
}
