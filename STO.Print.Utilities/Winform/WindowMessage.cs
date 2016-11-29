using System;

namespace STO.Print.Utilities
{
    public class WindowMessage
    {
        #region Window
        public const int WM_COMMAND = 0x111;
        public const int WM_SYSCOMMAND = 0x112;

        // public const int SC_RESTORE = 0xF120;
        public const int SC_CLOSE = 0xF060;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_MINIMIZE = 0xF020;

        public const int WM_USER = 0x0400;
        public const int WM_CLOSE = 0x0010;
        public static IntPtr HWND_TOPMOST = new IntPtr(-1);
        public static IntPtr HWND_TOP = new IntPtr(0);
        public const int SWP_NOACTIVATE = 0x0010;
        public const int SWP_NOMOVE = 0x0002;
        public const int SWP_SHOWWINDOW = 0x0040;
        public const int IDOK = 011;

        public const int GWL_STYLE = -16;
        public const int GWL_EXSTYLE = -20;
        public const int GWL_ID = -12;
        public const int BS_DEFPUSHBUTTON = 0x1;
        public const int BS_GROUPBOX = 0x7;
        public const int BS_CHECKBOX = 0x2;
        public const int BS_AUTOCHECKBOX = 0x3;
        public const int BS_RADIOBUTTON = 0x4;
        public const int BS_AUTORADIOBUTTON = 0x9;
        public const int WM_ACTIVATE = 0x06;
        public const int WM_SETTEXT = 0xC;
        public const int WM_GETTEXT = 0xD;
        #endregion

        #region Edit
        public const int EM_GETPASSWORDCHAR = 0x00D2;
        public const int EM_SETPASSWORDCHAR = 0x00CC;
        #endregion

        #region Button
        public const int BM_GETSTATE = 0x00F2;
        public const int BST_UNCHECKED = 0x0000;
        public const int BST_CHECKED = 0x0001;
        #endregion

        #region Process
        public const int PROCESS_VM_READ = 0x10;
        public const int PROCESS_VM_WRITE = 0x20;
        public const int PROCESS_VM_OPERATION = 0x8;
        public const int PROCESS_ALL_ACCESS = 0x000F0000 | 0x00100000 | 0xFFF;
        #endregion

        #region Memory
        public const int MEM_COMMIT = 0x1000;
        public const int MEM_RESERVE = 0x2000;
        public const int MEM_RELEASE = 0x8000;
        public const int PAGE_READWRITE = 0x4;
        #endregion

        #region Mouse
        public const int WM_LBUTTONDOWN = 0x201;
        public const int WM_LBUTTONUP = 0x202;
        public const int WM_LBUTTONDBLCLK = 0x203; //Left mousebutton doubleclick

        public const int WM_RBUTTONDOWN = 0x204; //Right mousebutton down
        public const int WM_RBUTTONUP = 0x205;   //Right mousebutton up
        public const int WM_RBUTTONDBLCLK = 0x206; //Right mousebutton doubleclick

        public const int WM_KEYDOWN = 0x100;  //Key down
        public const int WM_KEYUP = 0x101;   //Key up
        #endregion

        #region PropertySheet
        public const int PSM_SETCURSEL = WM_USER + 101;
        public const int PSM_GETCURRENTPAGEHWND = WM_USER + 118;
        #endregion

        #region TreeView
        public const int TV_FIRST = 4352;
        public const int TVSIL_NORMAL = 0;
        public const int TVSIL_STATE = 2;
        public const int TVM_EXPAND = TV_FIRST + 2;
        public const int TVM_SETIMAGELIST = TV_FIRST + 9;
        public const int TVM_GETNEXTITEM = TV_FIRST + 10;
        public const int TVM_SELECTITEM = TV_FIRST + 11;
        public const int TVM_GETITEMRECT = TV_FIRST + 4;
        public const int TVM_GETITEMSTATE = TV_FIRST + 39;
        public const int TVIF_HANDLE = 16;
        public const int TVIF_STATE = 8;
        public const int TVIF_TEXT = 0x0001;
        public const int TVIS_STATEIMAGEMASK = 61440;
        public const int TVIS_SELECTED = 0x0002;
        public const int TVM_GETITEM = TV_FIRST + 62;
        public const int TVM_SETITEM = TV_FIRST + 13;
        public const int TVGN_ROOT = 0x0;
        public const int TVGN_NEXT = 0x1;
        public const int TVGN_PARENT = 0x3;
        public const int TVGN_CHILD = 0x4;
        public const int TVGN_DROPHILITE = 0x8;
        public const int TVGN_CARET = 0x9;
        public const int TVE_COLLAPSE = 0x1;
        public const int TVE_EXPAND = 0x2;
        #endregion TreeView

        #region ListView
        public const int LVIF_TEXT = 0x0001;
        public const int LVIF_IMAGE = 0x0002;
        public const int LVIF_PARAM = 0x0004;
        public const int LVIF_STATE = 0x0008;
        public const int LVIF_INDENT = 0x0010;
        public const int LVIF_NORECOMPUTE = 0x0800;
        public const int LVM_FIRST = 0x1000;
        public const int LVM_GETSUBITEMRECT = (LVM_FIRST + 56);
        public const int LVM_GETITEMSTATE = (LVM_FIRST + 44);
        public const int LVM_GETITEMTEXTW = (LVM_FIRST + 115);
        public const int LVM_GETNEXTITEM = (LVM_FIRST + 12);
        public const int LVM_GETITEMCOUNT = (LVM_FIRST + 4);
        public const int LVM_GETITEM = (LVM_FIRST + 75);
        public const int LVM_ENSUREVISIBLE = (LVM_FIRST + 19);
        public const int LVM_SETITEMSTATE = (LVM_FIRST + 43);
        public const int LVM_SETITEMTEXT = (LVM_FIRST + 116);
        public const int LVM_GETHEADER = (LVM_FIRST + 31);
        public const int LVM_GETEDITCONTROL = (LVM_FIRST + 24);
        public const int LVNI_ALL = 0x0000;
        public const int LVNI_FOCUSED = 0x0001;
        public const int LVNI_SELECTED = 0x0002;
        public const int LVNI_CUT = 0x0004;
        public const int LVNI_DROPHILITED = 0x0008;
        public const int LVNI_ABOVE = 0x0100;
        public const int LVNI_BELOW = 0x0200;
        public const int LVNI_TOLEFT = 0x0400;
        public const int LVNI_TORIGHT = 0x0800;
        public const int LVIS_FOCUSED = 0x0001;
        public const int LVIS_SELECTED = 0x0002;
        public const int LVIS_CUT = 0x0004;
        public const int LVIS_DROPHILITED = 0x0008;
        public const int LVIS_GLOW = 0x0010;
        public const int LVIS_ACTIVATING = 0x0020;
        public const int LVIR_BOUNDS = 0;
        #endregion

        #region Header
        public const int HDM_FIRST = 0x1200;
        public const int HDM_GETITEMCOUNT = HDM_FIRST;
        public const int HDM_GETITEMRECT = HDM_FIRST + 7;
        #endregion

        #region Toolbar
        public const int TB_GETBUTTON = WM_USER + 23;
        public const int TB_BUTTONCOUNT = WM_USER + 24;
        public const int TB_GETBUTTONTEXT = WM_USER + 45;
        public const int TB_ISBUTTONHIDDEN = WM_USER + 12;
        public const int TB_GETRECT = WM_USER + 51;
        public const int TB_GETITEMRECT = WM_USER + 29;
        public const int TB_ISBUTTONCHECKED = WM_USER + 10;
        public const int TB_GETSTYLE = WM_USER + 57;

        public const int TBSTYLE_BUTTON = 0x0000;
        public const int TBSTYLE_SEP = 0x0001;
        public const int TBSTYLE_CHECK = 0x0002;
        public const int TBSTYLE_DROPDOWN = 0x0008;
        public const int TBSTATE_CHECKED = 0x01;
        #endregion

        #region Menu
        public const uint MF_BYPOSITION = 0x400;
        #endregion

        #region Tab Control
        public static int TCM_FIRST = 0x1300;
        public static int TCM_SETCURSEL = TCM_FIRST + 12;
        #endregion

        #region ComboBox
        public const int CB_ERR = -1;
        public const int CBS_SIMPLE = 0x0001;
        public const int CBS_DROPDOWN = 0x0002;
        public const int CBS_DROPDOWNLIST = 0x0003;
        public const int CBS_OWNERDRAWFIXED = 0x0010;
        public const int CBS_OWNERDRAWVARIABLE = 0x0020;
        public const int CBS_AUTOHSCROLL = 0x0040;
        public const int CBS_OEMCONVERT = 0x0080;
        public const int CBS_SORT = 0x0100;
        public const int CBS_HASSTRINGS = 0x0200;
        public const int CBS_NOINTEGRALHEIGHT = 0x0400;
        public const int CBS_DISABLENOSCROLL = 0x0800;
        public const int CB_SETCURSEL = 0x014E;
        public const int CB_SELECTSTRING = 0x014D;
        public const int CB_GETCOUNT = 0x146;
        public const int CB_GETCURSEL = 0x0147;
        public const int CB_GETLBTEXT = 0x0148;
        public const int CBN_SELCHANGE = 1;
        public const int CBN_DBLCLK = 2;
        public const int CBN_SETFOCUS = 3;
        public const int CBN_KILLFOCUS = 4;
        public const int CBN_EDITCHANGE = 5;
        public const int CBN_EDITUPDATE = 6;
        public const int CBN_DROPDOWN = 7;
        public const int CBN_CLOSEUP = 8;
        public const int CBN_SELENDOK = 9;
        public const int CBN_SELENDCANCEL = 10;
        #endregion

        #region ListBox
        public const int LB_GETCOUNT = 0x018B;
        public const int LB_GETITEMRECT = 0x0198;
        public const int LB_ADDSTRING = 0x00000180;
        public const int LB_SETCURSEL = 0x00000186;
        public const int LB_GETITEMHEIGHT = 0x01A1;
        public const int LBS_OWNERDRAWVARIABLE = 0x0020;
        public const int LBS_MULTIPLESEL = 0x0008;
        public const int LBS_EXTENDEDSEL = 0x0800;
        public const int LB_GETCURSEL = 0x00000188;
        public const int LB_GETTEXTLEN = 0x0000018A;
        public const int LB_GETTEXT = 0x00000189;
        public const int LB_DELETESTRING = 0x00000182;
        public const int LB_SETSEL = 0x0185;
        public const int LB_SETTOPINDEX = 0x0197;
        #endregion

        #region BCGDockControlBar
        public const int ID_DOCKBAR_AUTOHIDE = 16037;
        public const int ID_DOCKBAR_UNDOCK = 16038;
        public const int ID_DOCKBAR_CLOSE = 16039;
        public const int ID_DOCKBAR_DOCK = 16040;
        #endregion

        #region Cursor
        public const int CURSOR_SHOWING = 0x00000001;
        #endregion
    }
}
