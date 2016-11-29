using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Xilium.CefGlue;

namespace Xilium.CefGlue.Client.bs
{
    public class BsContextMenuHandler : CefContextMenuHandler
    {
        protected override void OnBeforeContextMenu(CefBrowser browser, CefFrame frame, CefContextMenuParams state, CefMenuModel model)
        {
            //清除默认右键菜单
            //model.Clear();

            //model.AddItem(0, "测试右键");

            //model.SetCommandIdAt()
            base.OnBeforeContextMenu(browser, frame, state, model);
        }
        
    }
}
