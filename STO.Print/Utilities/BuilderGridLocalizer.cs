//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd .
//-----------------------------------------------------------------

using System.Collections.Generic;

namespace STO.Print.Utilities
{
    using DevExpress.XtraGrid.Localization;
    using DevExpress.XtraGrid.Views.Grid;

    /// <summary>
    /// GridControl帮助类
    ///
    /// 修改纪录
    ///
    ///		2015-07-23 版本：1.0 YangHengLian 创建主键。
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-07-23</date>
    /// </author>
    /// </summary>
    public class BuilderGridLocalizer : GridLocalizer
    {
        #region 带参数的构造函数以及变量

        private Dictionary<GridStringId, string> CusLocalizedKeyValue;
        private Dictionary<DevExpress.XtraTreeList.Localization.TreeListStringId, string> cusLocalizedKeyValue;

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="cusLocalizedKeyValue">需要转移的GridStringId，其对应的文字描述</param>
        public BuilderGridLocalizer(Dictionary<GridStringId, string> cusLocalizedKeyValue)
        {
            CusLocalizedKeyValue = cusLocalizedKeyValue;
        }

        public BuilderGridLocalizer(Dictionary<DevExpress.XtraTreeList.Localization.TreeListStringId, string> cusLocalizedKeyValue)
        {
            // TODO: Complete member initialization
            this.cusLocalizedKeyValue = cusLocalizedKeyValue;
        }

        #endregion 带参数的构造函数以及变量

        #region public override string GetLocalizedString(GridStringId id) GetLocalizedString重载

        /// <summary>
        /// GetLocalizedString重载
        /// </summary>
        /// <param name="id">GridStringId</param>
        /// <returns>string</returns>
        public override string GetLocalizedString(GridStringId id)
        {
            if (CusLocalizedKeyValue != null)
            {
                string gridStringDisplay = string.Empty;
                foreach (KeyValuePair<GridStringId, string> gridLocalizer in CusLocalizedKeyValue)
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

        #region public static void CustomButtonText(GridView girdview, Dictionary<GridStringId, string> cusLocalizedKeyValue) 自定义GridControl按钮文字
        /// <summary>
        ///  自定义GridControl按钮文字
        /// </summary>
        /// <param name="girdview">GridView</param>
        /// <param name="cusLocalizedKeyValue">需要转移的GridStringId，其对应的文字描述</param>
        public static void CustomButtonText(GridView girdview, Dictionary<GridStringId, string> cusLocalizedKeyValue)
        {
            BuilderGridLocalizer bGridLocalizer = new BuilderGridLocalizer(cusLocalizedKeyValue);
            GridLocalizer.Active = bGridLocalizer;
        }
        #endregion

        #region public static Dictionary<GridStringId, string> SetGridLocalizer() 设置GridControl的FindPanel按钮文字
        /// <summary>
        /// 设置GridControl的FindPanel按钮文字
        /// </summary>
        /// <returns></returns>
        public static Dictionary<GridStringId, string> SetGridLocalizer()
        {
            Dictionary<GridStringId, string> gridLocalizer = new Dictionary<GridStringId, string>
            {
                {GridStringId.FindControlFindButton, "查找"},
                {GridStringId.FindControlClearButton, "清空"}
            };
            return gridLocalizer;
        }
        #endregion
    }
}
