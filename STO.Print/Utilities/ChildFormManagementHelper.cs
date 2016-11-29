//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using DotNet.Utilities;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace STO.Print.Utilities
{
    /// <summary>
    /// 窗口管理辅助类
    /// </summary>
    public sealed class ChildFormManagementHelper
    {
        /// <summary> 
        /// 获取MDI父窗口是否有窗口标题为指定字符串的子窗口（如果已经存在把此子窗口推向前台） 
        /// </summary> 
        /// <param name="formMain">M父窗口</param> 
        /// <param name="caption">窗口标题</param> 
        /// <returns></returns> 
        public static bool FormExist(Form formMain, string caption)
        {
            bool result = false;
            foreach (Form form in formMain.MdiChildren)
            {
                if (form.Text == caption)
                {
                    result = true;
                    form.Show();
                    form.Activate();
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// 唯一加载某个类型的窗体，如果存在则显示，否则创建。
        /// </summary>
        /// <param name="formMain">主窗体对象</param>
        /// <param name="formType">待显示的窗体类型</param>
        /// <returns></returns>
        public static Form LoadMdiForm(Form formMain, Type formType)
        {
            bool found = false;
            Form tableForm = null;
            foreach (Form form in formMain.MdiChildren)
            {
                if (form.GetType() == formType)
                {
                    found = true;
                    tableForm = form;
                    break;
                }
            }
            if (!found)
            {
                tableForm = (Form)Activator.CreateInstance(formType);
                tableForm.MdiParent = formMain;
                tableForm.Show();
            }
            // tableForm.Dock = DockStyle.Fill;
            // tableForm.WindowState = FormWindowState.Maximized;
            tableForm.BringToFront();
            tableForm.Activate();
            return tableForm;
        }

        public static Form LoadMdiForm(Form formMain, string formName, string assemblyName = "STO.Print")
        {
            bool found = false;
            Form tableForm = null;
            foreach (Form form in formMain.MdiChildren)
            {
                if (form.Name == formName)
                {
                    found = true;
                    tableForm = form;
                    break;
                }
            }
            if (!found)
            {
                // 这里用了缓存技术，若已经被创建过就不反复创建了
                Assembly assembly = CacheManagerHelper.Instance.Load(assemblyName);
                string frmName = assemblyName + "." + formName;
                Type type = assembly.GetType(frmName, true, false);
                tableForm = (Form)Activator.CreateInstance(type);
                // 判断是否弹出窗口，进行特殊处理
                var field = type.GetField("ShowDialogOnly");
                if (field != null && (bool)field.GetValue(tableForm))
                {
                    tableForm.HelpButton = false;
                    tableForm.ShowInTaskbar = false;
                    tableForm.MinimizeBox = false;
                    tableForm.ShowDialog(formMain);
                    return tableForm;
                }
                // tableForm = (Form)Activator.CreateInstance(formType);
                tableForm.MdiParent = formMain;
                tableForm.Show();
            }
            // tableForm.Dock = DockStyle.Fill;
            // tableForm.WindowState = FormWindowState.Maximized;
            tableForm.BringToFront();
            tableForm.Activate();
            return tableForm;
        }

        /// <summary>
        /// 打开一个页面
        /// </summary>
        /// <param name="formMain">父窗体</param>
        /// <param name="url">打开的链接地址</param>
        /// <param name="title">标题</param>
        /// <param name="formName">浏览器窗体名称</param>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public static Form Navigate(Form formMain, string url, string title = "首页", string formName = "BrowserForm", string assemblyName = "STO.Print")
        {
            bool found = false;
            Form tableForm = null;
            foreach (Form form in formMain.MdiChildren)
            {
                if (form.Name == title)
                {
                    tableForm = form;
                    ((IBaseBrowser)tableForm).Navigate(url);
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                // 这里用了缓存技术，若已经被创建过就不反复创建了
                Assembly assembly = CacheManagerHelper.Instance.Load(assemblyName);
                string frmName = assemblyName + "." + formName;
                Type type = assembly.GetType(frmName, true, false);
                tableForm = (Form)Activator.CreateInstance(type);
                // 判断是否弹出窗口，进行特殊处理
                var field = type.GetField("ShowDialogOnly");
                if (field != null && (bool)field.GetValue(tableForm))
                {
                    tableForm.HelpButton = false;
                    tableForm.ShowInTaskbar = false;
                    tableForm.MinimizeBox = false;
                    tableForm.ShowDialog(formMain);
                    return tableForm;
                }
                // tableForm = (Form)Activator.CreateInstance(formType);
                tableForm.MdiParent = formMain;
                tableForm.Name = formName;
                tableForm.Text = title;
                ((IBaseBrowser)tableForm).Navigate(url);
                tableForm.Show();
            }
            tableForm.Dock = DockStyle.Fill;
            tableForm.WindowState = FormWindowState.Maximized;
            tableForm.BringToFront();
            tableForm.Activate();
            return tableForm;
        }

    }
}