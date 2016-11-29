//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd .
//-----------------------------------------------------------------

using System.Collections.Generic;

namespace STO.Print.Utilities
{
    using DevExpress.XtraTreeList;
    using DevExpress.XtraTreeList.Localization;

    /// <summary>
    /// TreeList帮助类
    ///
    /// 修改纪录
    ///
    ///		2015-08-03 版本：1.0 YangHengLian 创建主键。
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-08-03</date>
    /// </author>
    /// </summary>
    public class BuilderTreeListLocalizer : TreeListLocalizer
    {
        #region 带参数的构造函数以及变量

        private Dictionary<TreeListStringId, string> CusLocalizedKeyValue;

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="cusLocalizedKeyValue">需要转移的TreeListStringId，其对应的文字描述</param>
        public BuilderTreeListLocalizer(Dictionary<TreeListStringId, string> cusLocalizedKeyValue)
        {
            CusLocalizedKeyValue = cusLocalizedKeyValue;
        }

        #endregion 带参数的构造函数以及变量

        #region public override string GetLocalizedString(TreeListStringId id) GetLocalizedString重载

        /// <summary>
        /// GetLocalizedString重载
        /// </summary>
        /// <param name="id">TreeListStringId</param>
        /// <returns>string</returns>
        public override string GetLocalizedString(TreeListStringId id)
        {
            if (CusLocalizedKeyValue != null)
            {
                string gridStringDisplay = string.Empty;
                foreach (KeyValuePair<TreeListStringId, string> gridLocalizer in CusLocalizedKeyValue)
                {
                    if (gridLocalizer.Key.Equals(id))
                    {
                        gridStringDisplay = gridLocalizer.Value;
                        break;
                    }
                }
                return gridStringDisplay;
            }
            return base.GetLocalizedString(id);
        }

        #endregion GetLocalizedString重载

        #region public static void CustomButtonText(GridView girdview, Dictionary<TreeListStringId, string> cusLocalizedKeyValue) 自定义TreeList按钮文字
        /// <summary>
        ///  自定义TreeList按钮文字
        /// </summary>
        /// <param name="girdview">GridView</param>
        /// <param name="cusLocalizedKeyValue">需要转移的TreeListStringId，其对应的文字描述</param>
        public static void CustomButtonText(TreeList girdview, Dictionary<TreeListStringId, string> cusLocalizedKeyValue)
        {
            BuilderTreeListLocalizer bGridLocalizer = new BuilderTreeListLocalizer(cusLocalizedKeyValue);
            TreeListLocalizer.Active = bGridLocalizer;
        }
        #endregion

        #region public static Dictionary<TreeListStringId, string> SetGridLocalizer() 设置TreeList的FindPanel按钮文字
        /// <summary>
        /// 设置TreeList的FindPanel按钮文字
        /// </summary>
        /// <returns></returns>
        public static Dictionary<TreeListStringId, string> SetGridLocalizer()
        {
            Dictionary<TreeListStringId, string> gridLocalizer = new Dictionary<TreeListStringId, string>
            {
                {TreeListStringId.FindControlFindButton, "查找"},
                {TreeListStringId.FindControlClearButton, "清空"}
            };
            return gridLocalizer;
        }
        #endregion
    }
}
