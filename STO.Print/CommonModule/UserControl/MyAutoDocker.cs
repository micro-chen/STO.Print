//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace STO.Print.UserControl
{
    /// <summary>
    /// 自动停靠窗体用户控件，类似qq停靠那种方式
    /// </summary>
    public partial class MyAutoDocker : Component
    {
        private Form _dockedForm;
        private bool _isOrg;
        private Rectangle _lastBoard;
        private FormDockHideStatus _formDockHideStatus = FormDockHideStatus.ShowNormally;
        public DockHideType DockHideType;
        private Timer _checkPosTimer;

        public MyAutoDocker()
        {
            InitializeComponent();
        }
        public MyAutoDocker(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        /// <summary>
        /// 初始化。
        /// </summary>
        /// <param name="needDockedForm">需要靠边停靠的窗体</param>
        public void Initialize(Form needDockedForm)
        {
            _dockedForm = needDockedForm;
            if (_dockedForm != null)
            {
                _dockedForm.LocationChanged += dockedForm_LocationChanged;
                _dockedForm.SizeChanged += _form_SizeChanged;
                _dockedForm.TopMost = true;
            }
        }

        /// <summary>
        /// 定时器循环判断。        
        /// </summary>       
        private void CheckPosTimer_Tick(object sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }
            if (_dockedForm == null || !_isOrg)
            {
                return;
            }

            if (_dockedForm.Bounds.Contains(Cursor.Position))
            {
                _showOnce = false;
            }

            if (_showOnce)
            {
                if (DockHideType == DockHideType.Top)
                {
                    _dockedForm.Location = new Point(_dockedForm.Location.X, 0);
                }
                else if (DockHideType == DockHideType.Right)
                {
                    _dockedForm.Location = new Point(Screen.PrimaryScreen.Bounds.Width - _dockedForm.Width, _dockedForm.Location.Y);
                }
                else if (DockHideType == DockHideType.Left)
                {
                    _dockedForm.Location = new Point(0, _dockedForm.Location.Y);
                }
                _dockedForm.Size = new Size(_lastBoard.Width, _lastBoard.Height);
                return;
            }

            //当鼠标移动到窗体的范围内（此时，窗体的位置位于屏幕之外）
            if (_dockedForm.Bounds.Contains(Cursor.Position))
            {
                if (DockHideType != DockHideType.Top)
                {
                    if (DockHideType != DockHideType.Left)
                    {
                        if (DockHideType != DockHideType.Right)
                        {
                            return;
                        }
                        if (_formDockHideStatus == FormDockHideStatus.Hide)
                        {
                            _dockedForm.Location = new Point(Screen.PrimaryScreen.Bounds.Width - _dockedForm.Width, _dockedForm.Location.Y);
                        }
                    }
                    else
                    {
                        if (_formDockHideStatus == FormDockHideStatus.Hide)
                        {
                            _dockedForm.Location = new Point(0, _dockedForm.Location.Y);
                        }
                    }
                }
                else
                {
                    if (_formDockHideStatus == FormDockHideStatus.Hide)
                    {
                        _dockedForm.Location = new Point(_dockedForm.Location.X, 0);
                    }
                }
            }
            else //当鼠标位于窗体范围之外，则根据DockHideType的值，决定窗体的位置。
            {
                switch (DockHideType)
                {
                    case DockHideType.None:
                        {
                            if (_isOrg && _formDockHideStatus == FormDockHideStatus.ShowNormally && (_dockedForm.Bounds.Width != _lastBoard.Width || _dockedForm.Bounds.Height != _lastBoard.Height))
                            {
                                _dockedForm.Size = new Size(_lastBoard.Width, _lastBoard.Height);
                            }
                            break;
                        }
                    case DockHideType.Top:
                        {
                            _dockedForm.Location = new Point(_dockedForm.Location.X, (_dockedForm.Height - 4) * -1);
                            return;
                        }
                    case DockHideType.Left:
                        {
                            _dockedForm.Location = new Point(-1 * (_dockedForm.Width - 4), _dockedForm.Location.Y);
                            return;
                        }
                    default:
                        {
                            if (DockHideType != DockHideType.Right)
                            {
                                return;
                            }
                            _dockedForm.Location = new Point(Screen.PrimaryScreen.Bounds.Width - 4, _dockedForm.Location.Y);
                            return;
                        }
                }
            }
        }

        private void dockedForm_LocationChanged(object sender, EventArgs e)
        {
            ComputeDockHideType();
            if (!_isOrg)
            {
                _lastBoard = _dockedForm.Bounds;
                _isOrg = true;
            }
        }

        /// <summary>
        /// 判断是否达到了隐藏的条件？以及是哪种类型的隐藏。
        /// </summary>
        private void ComputeDockHideType()
        {
            if (_dockedForm.Top <= 0)
            {
                DockHideType = DockHideType.Top;
                if (_dockedForm.Bounds.Contains(Cursor.Position))
                {
                    _formDockHideStatus = FormDockHideStatus.ReadyToHide;
                    return;
                }
                _dockedForm.TopMost = true;
                _formDockHideStatus = FormDockHideStatus.Hide;
            }
            else
            {
                if (_dockedForm.Left <= 0)
                {
                    DockHideType = DockHideType.Left;
                    if (_dockedForm.Bounds.Contains(Cursor.Position))
                    {
                        _formDockHideStatus = FormDockHideStatus.ReadyToHide;
                        return;
                    }
                    _dockedForm.TopMost = true;
                    _formDockHideStatus = FormDockHideStatus.Hide;
                }
                else
                {
                    if (_dockedForm.Left < Screen.PrimaryScreen.Bounds.Width - _dockedForm.Width)
                    {
                        DockHideType = DockHideType.None;
                        _formDockHideStatus = FormDockHideStatus.ShowNormally;
                        return;
                    }
                    DockHideType = DockHideType.Right;
                    if (_dockedForm.Bounds.Contains(Cursor.Position))
                    {
                        _formDockHideStatus = FormDockHideStatus.ReadyToHide;
                        return;
                    }
                    _dockedForm.TopMost = true;
                    _formDockHideStatus = FormDockHideStatus.Hide;
                }
            }
        }

        private void _form_SizeChanged(object sender, EventArgs e)
        {
            if (_isOrg && _formDockHideStatus == FormDockHideStatus.ShowNormally)
            {
                _lastBoard = _dockedForm.Bounds;
            }
        }

        private bool _showOnce;
        public void ShowOnce()
        {
            _showOnce = true;
            _formDockHideStatus = FormDockHideStatus.ShowNormally;
        }
    }

    /// <summary>
    /// 靠边隐藏的类型。
    /// </summary>
    public enum DockHideType
    {
        /// <summary>
        /// 不隐藏
        /// </summary>
        None = 0,
        /// <summary>
        /// 靠上边沿隐藏
        /// </summary>
        Top,
        /// <summary>
        /// 靠左边沿隐藏
        /// </summary>
        Left,
        /// <summary>
        /// 靠右边沿隐藏
        /// </summary>
        Right
    }

    /// <summary>
    /// 窗体的显示或隐藏状态
    /// </summary>
    public enum FormDockHideStatus
    {
        /// <summary>
        /// 已隐藏
        /// </summary>
        Hide = 0,

        /// <summary>
        /// 准备隐藏
        /// </summary>
        ReadyToHide,

        /// <summary>
        /// 正常显示
        /// </summary>
        ShowNormally
    }
}
