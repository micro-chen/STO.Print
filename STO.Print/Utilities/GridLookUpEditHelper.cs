//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Data;
using System.Reflection;

namespace STO.Print.Utilities
{
    using DevExpress.Data.Filtering;
    using DevExpress.XtraEditors;
    using DevExpress.XtraGrid.Views.Grid;
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// DevExpress GridLookUpEdit控件帮助类
    ///
    /// 修改纪录
    ///
    ///		  2014-06-23  版本：1.0 YangHengLian 创建主键，注意命名空间的排序。
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2014-06-23</date>
    /// </author>
    /// </summary>
    public class GridLookUpEditHelper
    {
        /// <summary>
        /// 列表下拉弹出控件初始化
        /// </summary>
        /// <param name="gridLookUpEdit"></param>
        /// <param name="dataSource"></param>
        /// <param name="valueMember"></param>
        /// <param name="displayMember"></param>
        public static void GridLookUpEditInit(GridLookUpEdit gridLookUpEdit, object dataSource, string valueMember, string displayMember)
        {
            gridLookUpEdit.Properties.View.OptionsBehavior.AutoPopulateColumns = false;
            gridLookUpEdit.Properties.DataSource = dataSource;
            gridLookUpEdit.Properties.ValueMember = valueMember;
            gridLookUpEdit.Properties.DisplayMember = displayMember;
            gridLookUpEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            gridLookUpEdit.Properties.View.BestFitColumns();
            gridLookUpEdit.Properties.ShowFooter = false;
            gridLookUpEdit.Properties.AutoComplete = false;
            gridLookUpEdit.Properties.ImmediatePopup = true;
            gridLookUpEdit.Properties.PopupFilterMode = PopupFilterMode.Contains;
            gridLookUpEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
        }

        /// <summary>
        /// 多列过滤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="colName1"></param>
        /// <param name="colName2"></param>
        public static void FilterLookup(object sender, string colName1, string colName2)
        {
            try
            {
                GridLookUpEdit edit = sender as GridLookUpEdit;
                if (edit != null)
                {
                    if (!string.IsNullOrEmpty(edit.AutoSearchText.Trim()))
                    {
                        GridView gridView = edit.Properties.View;
                        FieldInfo fi = gridView.GetType().GetField("extraFilter", BindingFlags.NonPublic | BindingFlags.Instance);
                        BinaryOperator op1 = new BinaryOperator(colName1, "%" + edit.AutoSearchText + "%", BinaryOperatorType.Like);
                        BinaryOperator op2 = new BinaryOperator(colName2, "%" + edit.AutoSearchText + "%", BinaryOperatorType.Like);
                        var filterCondition = new GroupOperator(GroupOperatorType.Or, new CriteriaOperator[] { op1, op2 }).ToString();
                        if (fi != null)
                        {
                            fi.SetValue(gridView, filterCondition);
                            edit.ShowPopup();
                            edit.SelectionStart = edit.Text.Length + 10;//设置选中文字的开始位置为文本框的文字的长度，如果超过了文本长度，则默认为文本的最后。
                            edit.SelectionLength = 0;//设置被选中文字的长度为0（将光标移动到文字最后）
                            edit.ScrollToCaret();//讲滚动条移动到光标位置
                        }
                        MethodInfo mi = gridView.GetType().GetMethod("ApplyColumnsFilterEx", BindingFlags.NonPublic | BindingFlags.Instance);
                        mi.Invoke(gridView, null);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(edit.Text.Trim()))
                        {
                            if (edit.Properties.NullValuePrompt == "格式：江苏省-苏州市-吴中区")
                            {
                                return;
                            }
                            edit.ShowPopup();
                            edit.SelectionStart = edit.Text.Length + 10;//设置选中文字的开始位置为文本框的文字的长度，如果超过了文本长度，则默认为文本的最后。
                            edit.SelectionLength = 0;//设置被选中文字的长度为0（将光标移动到文字最后）
                            edit.ScrollToCaret();//讲滚动条移动到光标位置
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtil.WriteException(ex);
            }
        }

        /// <summary>
        /// 获取DataTable的区域匹配的ID信息
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public static string FilterAreaId(DataTable dataSource, string fullName)
        {
            if (dataSource != null && dataSource.Rows.Count > 0)
            {
                var dataRowArray = dataSource.Select(string.Format(" {0} ='{1}' ", BaseAreaEntity.FieldFullName, fullName));
                return dataRowArray[0][BaseAreaEntity.FieldId].ToString();
            }
            return null;
        }
    }
}
