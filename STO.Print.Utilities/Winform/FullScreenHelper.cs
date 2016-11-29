using System.Drawing;
using System.Windows.Forms;

namespace STO.Print.Utilities
{
    /// <summary>
    /// 窗体全屏操作辅助类
    /// </summary>
    public class FullscreenHelper
    {
        /// <summary>
        /// 构造函数，传入需要进行全屏操作的窗体
        /// </summary>
        /// <param name="form">需要进行全屏操作的窗体</param>
        public FullscreenHelper(Form form)
        {
            m_form = form;
        }

        /// <summary>
        /// 全屏切换操作
        /// </summary>
        public void Toggle()
        {
            Fullscreen = !Fullscreen;
        }

        /// <summary>
        /// 设置窗体是否为全屏显示
        /// </summary>
        public bool Fullscreen
        {
            set
            {
                if (m_fullscreen == value)
                {
                    return;
                }

                m_fullscreen = value;

                if (m_fullscreen)
                {
                    m_bounds = m_form.Bounds;
                    m_formBorderStyle = m_form.FormBorderStyle;
                    m_windowState = m_form.WindowState;

                    if (m_form.MainMenuStrip != null)
                    {
                        m_form.MainMenuStrip.Visible = false;
                    }

                    m_form.FormBorderStyle = FormBorderStyle.None;
                    m_form.Bounds = Screen.PrimaryScreen.Bounds;
                    m_form.WindowState = FormWindowState.Normal;
                }
                else
                {
                    m_form.TopMost = false;
                    m_form.WindowState = m_windowState;
                    m_form.Bounds = m_bounds;
                    m_form.FormBorderStyle = m_formBorderStyle;
                    if (m_form.MainMenuStrip != null)
                    {
                        m_form.MainMenuStrip.Visible = true;
                    }
                }
            }
            get
            {
                return m_fullscreen;
            }

        }

        Form m_form = null;
        bool m_fullscreen = false;
        Rectangle m_bounds;
        FormBorderStyle m_formBorderStyle;
        FormWindowState m_windowState;
    }
}
