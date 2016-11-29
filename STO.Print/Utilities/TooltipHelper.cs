//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Drawing;
using System.Windows.Forms;

namespace STO.Print.Utilities
{
    using DevExpress.Utils;
    using DotNet.Utilities;

    /// <summary>
    /// 提示气泡控件帮助类（ToolTip）
    ///
    /// 修改纪录
    ///
    ///		  2014-09-13  版本：1.0 YangHengLian 创建主键，注意命名空间的排序。
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2014-09-13</date>
    /// </author>
    /// </summary>
    public static class TooltipHelper
    {
        /// <summary>
        /// 为控件提供Tooltip
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="tip">ToolTip</param>
        /// <param name="message">提示消息</param>
        public static void ShowTooltip(this Control control, ToolTip tip, string message)
        {
            Point mousePoint = Control.MousePosition;
            int x = control.PointToClient(mousePoint).X;
            int y = control.PointToClient(mousePoint).Y;
            tip.Show(message, control, x, y);
            tip.Active = true;
        }
        /// <summary>
        /// 为控件提供Tooltip
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="tip">ToolTip</param>
        /// <param name="message">提示消息</param>
        /// <param name="durationTime">保持提示的持续时间</param>
        public static void ShowTooltip(this Control control, ToolTip tip, string message, int durationTime)
        {
            Point mousePoint = Control.MousePosition;
            int x = control.PointToClient(mousePoint).X;
            int y = control.PointToClient(mousePoint).Y;
            tip.Show(message, control, x, y, durationTime);
            tip.Active = true;
        }
        /// <summary>
        /// 为控件提供Tooltip
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="tip">ToolTip</param>
        /// <param name="message">提示消息</param>
        /// <param name="xoffset">水平偏移量</param>
        /// <param name="yoffset">垂直偏移量</param>
        public static void ShowTooltip(this Control control, ToolTip tip, string message, int xoffset, int yoffset)
        {
            Point mousePoint = Control.MousePosition;
            int x = control.PointToClient(mousePoint).X;
            int y = control.PointToClient(mousePoint).Y;
            tip.Show(message, control, x + xoffset, y + yoffset);
            tip.Active = true;
        }
        /// <summary>
        /// 为控件提供Tooltip
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="tip">ToolTip</param>
        /// <param name="message">提示消息</param>
        /// <param name="xoffset">水平偏移量</param>
        /// <param name="yoffset">垂直偏移量</param>
        /// <param name="durationTime">保持提示的持续时间</param>
        public static void ShowTooltip(this Control control, ToolTip tip, string message, int xoffset, int yoffset, int durationTime)
        {
            Point mousePoint = Control.MousePosition;
            int x = control.PointToClient(mousePoint).X;
            int y = control.PointToClient(mousePoint).Y;
            tip.Show(message, control, x + xoffset, y + yoffset, durationTime);
            tip.Active = true;
        }

        /// <summary>
        /// 冒泡提示
        /// </summary>
        /// <param name="ctl"> System.Windows.Forms的一个控件，在其上面提示显示</param>
        /// <param name="title"> 提示的标题默认（提示信息）</param>
        /// <param name="content">提示的信息默认（???）</param>
        /// <param name="showTime">提示显示等待时间</param>
        /// <param name="toolTipType">DevExpress.Utils.ToolTipType 显示的类型</param>
        /// <param name="tipLocation">DevExpress.Utils.ToolTipLocation 在控件显示的位置</param>
        /// <param name="isAutoHide">是否自动隐藏提示信息</param>
        /// <param name="tipIconType">DevExpress.Utils.ToolTipIconType 显示框图表的类型</param>
        /// <param name="imgList">一个System.Windows.Forms.ImageList 装载Icon图标的List，显示的ToolTipIconType上，可以为Null</param>
        /// <param name="imgIndex">图标在ImageList上的索引，ImageList为Null时传0进去</param>
        public static void ShowTip(this Control ctl, string title, string content, ToolTipLocation tipLocation = ToolTipLocation.BottomCenter, ToolTipType toolTipType = ToolTipType.Standard, int showTime = 2000, bool isAutoHide = true, ToolTipIconType tipIconType = ToolTipIconType.Application, ImageList imgList = null, int imgIndex = 0)
        {
            try
            {
                var myToolTipClt = new ToolTipController();
                ToolTipControllerShowEventArgs args = myToolTipClt.CreateShowArgs();
                content = (string.IsNullOrEmpty(content) ? "???" : content);
                title = string.IsNullOrEmpty(title) ? "" : title;
                myToolTipClt.ImageList = imgList;
                myToolTipClt.ImageIndex = (imgList == null ? 0 : imgIndex);
                args.AutoHide = isAutoHide;
                myToolTipClt.Appearance.BackColor = Color.FromArgb(254, 254, 254);
                myToolTipClt.ShowBeak = true;
                myToolTipClt.AllowHtmlText = true;
                myToolTipClt.ShowShadow = true;
                myToolTipClt.Rounded = true;
                myToolTipClt.AutoPopDelay = (showTime == 0 ? 2000 : showTime);
                myToolTipClt.SetToolTip(ctl, content);
                myToolTipClt.SetTitle(ctl, title);
                myToolTipClt.SetToolTipIconType(ctl, tipIconType);
                myToolTipClt.Active = true;
                myToolTipClt.ToolTipType = toolTipType;
                myToolTipClt.HideHint();
                myToolTipClt.ShowHint(content, title, ctl, tipLocation);
            }
            catch (Exception ex)
            {
                LogUtil.WriteException(ex);
            }
        }

        public static void ShowTip(this Control ctl, string content, ToolTipLocation tipLocation = ToolTipLocation.BottomCenter, ToolTipType toolTipType = ToolTipType.Standard, int showTime = 2000, bool isAutoHide = true, ToolTipIconType tipIconType = ToolTipIconType.Application, ImageList imgList = null, int imgIndex = 0)
        {
            try
            {
                var myToolTipClt = new ToolTipController();
                ToolTipControllerShowEventArgs args = myToolTipClt.CreateShowArgs();
                myToolTipClt.ImageList = imgList;
                myToolTipClt.ImageIndex = (imgList == null ? 0 : imgIndex);
                args.AutoHide = isAutoHide;
                myToolTipClt.Appearance.BackColor = Color.FromArgb(254, 254, 254);
                myToolTipClt.ShowBeak = true;
                myToolTipClt.AllowHtmlText = true;
                myToolTipClt.ShowShadow = true;
                myToolTipClt.Rounded = true;
                myToolTipClt.AutoPopDelay = (showTime == 0 ? 2000 : showTime);
                myToolTipClt.SetToolTip(ctl, content);
                myToolTipClt.SetToolTipIconType(ctl, tipIconType);
                myToolTipClt.Active = true;
                myToolTipClt.ToolTipType = toolTipType;
                myToolTipClt.HideHint();
                myToolTipClt.ShowHint(content, null, ctl, tipLocation);
            }
            catch (Exception ex)
            {
                LogUtil.WriteException(ex);
            }
        }
    }
}
