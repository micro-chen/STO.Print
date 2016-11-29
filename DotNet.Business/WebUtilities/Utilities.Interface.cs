//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace DotNet.Business
{
    using DotNet.Utilities;
    using DotNet.Model;

    public partial class Utilities
    {
        #region public static string BuildUrl(string url, string paramText, string paramValue)
        /// <summary>
        /// url里有key的值，就替换为value,没有的话就追加.构造Url的参数 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="ParamText"></param>
        /// <param name="ParamValue"></param>
        /// <returns></returns>
        public static string BuildUrl(string url, string paramText, string paramValue)
        {
            Regex reg = new Regex(string.Format("{0}=[^&]*", paramText), RegexOptions.IgnoreCase);
            Regex reg1 = new Regex("[&]{2,}", RegexOptions.IgnoreCase);
            string _url = reg.Replace(url, "");
            //_url = reg1.Replace(_url, "");
            if (_url.IndexOf("?") == -1)
            {
                _url += string.Format("?{0}={1}", paramText, paramValue);//?
            }
            else
            {
                _url += string.Format("&{0}={1}", paramText, paramValue);//&
            }
            _url = reg1.Replace(_url, "&");
            _url = _url.Replace("?&", "?");
            return _url;
        }
        #endregion

        #region public static bool SetDropDownListValue(DropDownList dropDownList, string selectedValue)
        /// <summary>
        /// 设置下拉框的被选中值
        /// </summary>
        /// <param name="dropDownList">下拉框</param>
        /// <param name="selectedValue">被选中的值</param>
        /// <returns>是否找到</returns>
        public static bool SetDropDownListValue(DropDownList dropDownList, string selectedValue)
        {
            bool result = false;
            if (dropDownList.SelectedItem != null)
            {
                dropDownList.SelectedItem.Selected = false;
            }
            // 按值找
            ListItem listItem = dropDownList.Items.FindByValue(selectedValue);
            if (listItem == null)
            {
                // 按显示的文本找
                for (int i = 0; i < dropDownList.Items.Count; i++)
                {
                    if (dropDownList.Items[i].Text.Equals(selectedValue))
                    {
                        dropDownList.Items[i].Selected = true;
                        result = true;
                        break;
                    }
                }
                // 还是没找到
                if (dropDownList.SelectedItem == null)
                {
                    listItem = new ListItem(selectedValue, selectedValue);
                    dropDownList.Items.Insert(0, listItem);
                }
            }
            else
            {
                // 设置为被选中状态
                listItem.Selected = true;
            }
            return result;
        }
        #endregion

        #region public static void SetDropDownList(DropDownList dropDownList, DataTable result, string fieldValue = "Id", string fieldText = "Name", string sortCode = "SortCode", bool addEmptyItem = true)
        /// <summary>
        /// 绑定下拉列表
        /// </summary>
        /// <param name="dropDownList">下拉列表</param>
        /// <param name="tableName">表名</param>
        /// <param name="fieldValue">邦定的值字段</param>
        /// <param name="fieldText">邦定的名称字段</param>
        /// <param name="sortCode">排序</param>
        /// <param name="addEmptyItem">是否增加空行</param>
        public static void SetDropDownList(DropDownList dropDownList, DataTable dt, string fieldValue = "Id", string fieldText = "Name", string sortCode = "SortCode", bool addEmptyItem = true)
        {
            //BaseItemDetailsManager manager = new BaseItemDetailsManager(tableName);
            //var result = manager.GetDataTable(new KeyValuePair<string, object>(BaseItemDetailsEntity.FieldEnabled, 1)
            //    , new KeyValuePair<string, object>(BaseItemDetailsEntity.FieldDeletionStateCode, 0));
            dropDownList.DataValueField = fieldValue;
            dropDownList.DataTextField =fieldText;
            dt.DefaultView.Sort = sortCode;
            dropDownList.DataSource = dt.DefaultView;
            dropDownList.DataBind();
            if (addEmptyItem)
            {
                dropDownList.Items.Insert(0, new ListItem());
            }
        }
        #endregion

        #region public static void GetItemDetails(BaseUserInfo userInfo,DropDownList dropDownList, string itemsTableName,bool addEmptyItem=true)
        /// <summary>
        /// 绑定下拉列表
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="dropDownList">下拉列表</param>
        /// <param name="itemsTableName">表名</param>
        /// <param name="addEmptyItem">是否增加空行</param>
        public static void GetItemDetails(BaseUserInfo userInfo, DropDownList dropDownList, string itemsTableName, bool addEmptyItem = true)
        {
            BaseItemDetailsManager manager = new BaseItemDetailsManager(itemsTableName);
            var dt = manager.GetDataTable(new KeyValuePair<string, object>(BaseItemDetailsEntity.FieldEnabled, 1)
                , new KeyValuePair<string, object>(BaseItemDetailsEntity.FieldDeletionStateCode, 0));
            dropDownList.DataValueField = BaseItemDetailsEntity.FieldItemCode;
            dropDownList.DataTextField = BaseItemDetailsEntity.FieldItemName;
            dt.DefaultView.Sort = BaseItemDetailsEntity.FieldSortCode;
            dropDownList.DataSource = dt.DefaultView;
            dropDownList.DataBind();
            if (addEmptyItem)
            {
                dropDownList.Items.Insert(0, new ListItem());
            }
        }
        #endregion

        #region public void CheckTreeParentId(DataTable result, string fieldId, string fieldParentId)
        /// <summary>
        /// 查找 ParentId 字段的值是否在 Id字段 里
        /// </summary>
        /// <param name="result">数据表</param>
        /// <param name="fieldId">主键字段</param>
        /// <param name="fieldParentId">父节点字段</param>
        public void CheckTreeParentId(DataTable dt, string fieldId, string fieldParentId)
        {
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dt.Rows[i];
                // if (result.Columns[fieldId].GetType() == typeof(int))
                if (dr[fieldParentId].ToString().Length > 0)
                {
                    if (dt.Select(fieldId + " = " + dr[fieldParentId].ToString() + "").Length == 0)
                    {
                        dr[fieldParentId] = DBNull.Value;
                    }
                }
            }
        }
        #endregion
    }
}