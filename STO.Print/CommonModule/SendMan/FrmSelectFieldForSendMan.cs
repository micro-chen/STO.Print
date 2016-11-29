//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace STO.Print
{
    using DevExpress.XtraEditors;
    using DotNet.Utilities;
    using STO.Print.AddBillForm;
    using STO.Print.Model;
    using STO.Print.Utilities;

    /// <summary>
    /// 导入发件人（选择字段）
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
    public partial class FrmSelectFieldForSendMan : BaseForm
    {
        readonly DataTable _execldt = new DataTable();

        public FrmSelectFieldForSendMan()
        {
            InitializeComponent();
        }

        public FrmSelectFieldForSendMan(DataTable excelDataTable)
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
                lbSource.Items.Add(dt.Columns[i].ColumnName);
            }
            var targetDataTable = BillPrintHelper.DbHelper.Fill(BillPrintHelper.CmdStrForSendUser);
            if (targetDataTable.Columns.Contains("Id"))
            {
                targetDataTable.Columns.Remove("Id");
            }
            if (targetDataTable.Columns.Contains("IsDefault"))
            {
                targetDataTable.Columns.Remove("IsDefault");
            }
            for (int i = 0; i < targetDataTable.Columns.Count; i++)
            {
                lbTarget.Items.Add(targetDataTable.Columns[i].ColumnName);
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
        private void butOnAdd_Click(object sender, EventArgs e)
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
                                var dataField = BillPrintHelper.GetUserFieldByName(keyValuePair.Value);
                                listField.Add(dataField);
                                listValue.Add(value);
                            }
                            listField.Add(ZtoUserEntity.FieldIssendorreceive);
                            listValue.Add("1");
                            string tempCommandText = string.Format("INSERT INTO {0} ({1}) VALUES({2})", ZtoUserEntity.TableName, string.Join(",", listField), "'" + string.Join("','", listValue) + "'");
                            listCommandText.Add(tempCommandText);
                            listField.Clear();
                            listValue.Clear();
                        }
                        // 开始执行sql
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
                            XtraMessageBox.Show(string.Format("成功导入{0}条", resultCount), AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
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


    }
}
