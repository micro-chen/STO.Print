using System;
using System.ComponentModel; 
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace STO.Print.Utilities
{
    /// <summary> 
    /// 1时，显示，隐藏或关闭动画形式。
    /// </summary> 
    /// <remarks> 
    /// MDI子窗体不支持混合的方法，只支持其他
    /// 而被首次关闭时显示的方法。
    /// </remarks> 
    public sealed class FormAnimator : IDisposable
    {
        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern bool AnimateWindow(IntPtr hWnd, int dwTime, int dwFlags); 

        #region " Types "

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// 动画可用的方法。
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <history> 
        /// [约翰]31/08/2005创建
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public enum AnimationMethod
        {
            [Description("Default animation method. Rolls out from edge when showing and into edge when hiding. Requires a direction.")]
            Roll = 0x0,
            [Description("Expands out from centre when showing and collapses into centre when hiding.")]
            Centre = 0x10,
            [Description("Slides out from edge when showing and slides into edge when hiding. Requires a direction.")]
            Slide = 0x40000,
            [Description("Fades from transaprent to opaque when showing and from opaque to transparent when hiding.")]
            Blend = 0x80000
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// 在其中轧辊和幻灯片动画可以显示方向。
        /// </summary> 
        /// <remarks> 
        /// 水平和垂直方向可以结合创建对角线动画。
        /// </remarks> 
        /// <history> 
        /// [约翰]31/08/2005创建
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        [Flags()]
        public enum AnimationDirection
        {
            [Description("From left to right.")]
            Right = 0x1,
            [Description("From right to left.")]
            Left = 0x2,
            [Description("From top to bottom.")]
            Down = 0x4,
            [Description("From bottom to top.")]
            Up = 0x8
        }

        #endregion

        #region " Constants "

        //隐藏的表格。
        private const int AW_HIDE = 0x10000;
        //激活的形式。
        private const int AW_ACTIVATE = 0x20000;

        #endregion

        #region " Variables "

        //动画的形式。
        private Form m_Form;

        //动画的方法来显示和隐藏的形式。
        private AnimationMethod m_Method;
        //哪个方向滚动或滑动的形式。
        private AnimationDirection m_Direction;
        //动画播放的毫秒数。
        private int m_Duration;

        #endregion

        #region " Properties "

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///获取或设置用于显示和隐藏的形式动画的方法
        /// </summary> 
        /// <value> 
        /// 动画的方法来显示和隐藏的形式。
        /// </value> 
        /// <remarks> 
        /// 轧辊使用默认情况下，如果没有指定方法。 
        /// </remarks> 
        /// <history> 
        /// [John] 31/08/2005 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        [Description("Gets or sets the animation method used to show and hide the form.")]
        public AnimationMethod Method
        {
            get { return this.m_Method; }
            set { this.m_Method = value; }
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// 获取或设置在动画执行的方向。
        /// </summary> 
        /// <value> 
        /// 在动画执行的方向。
        /// </value> 
        /// <remarks> 
        /// 方向是只适用于辊和幻灯片的方法。
        /// </remarks> 
        /// <history> 
        /// [John] 31/08/2005 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        [Description("Gets or sets the direction in which the animation is performed.")]
        public AnimationDirection Direction
        {
            get { return this.m_Direction; }
            set { this.m_Direction = value; }
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// 获取或设置动画播放的毫秒数。
        /// </summary> 
        /// <value> 
        /// 动画播放的毫秒数。
        /// </value> 
        /// <remarks> 
        /// </remarks> 
        /// <history> 
        /// [John] 5/09/2005 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        [Description("Gets or sets the number of milliseconds over which the animation is played.")]
        public int Duration
        {
            get { return this.m_Duration; }
            set { this.m_Duration = value; }
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///获取动画的形式。 
        /// </summary> 
        /// <value> 
        /// 动画的形式。
        /// </value> 
        /// <remarks> 
        /// </remarks> 
        /// <history> 
        /// [John] 5/09/2005 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        [Description("Gets the form to be animated.")]
        public Form Form
        {
            get { return this.m_Form; }
        }


        #endregion

        #region " Constructors "

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// 创建一个新的指明表格FormAnimator对象。
        /// </summary> 
        /// <param name="form"> 
        /// 动画的形式。
        /// </param> 
        /// <remarks> 
        /// 除非方法和/或方向的属性，将使用无动画
        /// 独立设置。持续时间设置一个默认情况下，第二季度。
        /// </remarks> 
        /// <history> 
        /// [John] 5/09/2005 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public FormAnimator(Form form)
        {
            this.m_Form = form;
            this.m_Form.Load += new EventHandler(m_Form_Load);
            this.m_Form.VisibleChanged += new EventHandler(m_Form_VisibleChanged);
            this.m_Form.Closing += new CancelEventHandler(m_Form_Closing);
            //默认动画的长度是第二季度。
            this.m_Duration = 250;
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// 创建一个新的指明表格FormAnimator使用指定的对象 
        /// 在指定的时间的方法。
        /// </summary> 
        /// <param name="form"> 
        /// 动画的形式。
        /// </param> 
        /// <param name="method"> 
        /// 动画的方法来显示和隐藏的形式。
        /// </param> 
        /// <param name="duration"> 
        /// 动画播放的毫秒数。
        /// </param> 
        /// <remarks> 
        ///除非的方向滚动或滑动的方法将被用于没有动画
        ///独立设置属性。
        /// </remarks> 
        /// <history> 
        /// [John] 5/09/2005 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public FormAnimator(Form form, AnimationMethod method, int duration)
            : this(form)
        {
            this.m_Method = method;
            this.m_Duration = duration;
        }

        // 
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// 创建一个新的指明表格FormAnimator使用指定的对象
        /// 在指定的时间在指定的方向的方法。 
        /// </summary> 
        /// <param name="form"> 
        /// 动画的形式。
        /// </param> 
        /// <param name="method"> 
        /// 动画的方法来显示和隐藏的形式。
        /// </param> 
        /// <param name="direction"> 
        /// 的方向，以动画的形式。 
        /// </param> 
        /// <param name="duration"> 
        /// 动画播放的毫秒数。
        /// </param> 
        /// <remarks> 
        /// 如果中心或混合方法是参数的方向不会有任何效果 
        /// specified. 
        /// </remarks> 
        /// <history> 
        /// [John] 5/09/2005 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public FormAnimator(Form form, AnimationMethod method, AnimationDirection direction, int duration)
            : this(form, method, duration)
        {

            this.m_Direction = direction;
        }
        ~FormAnimator()
        {
            this.m_Form = null;
        }


        #endregion

        #region " Event Handlers "

        //动画的形式时自动加载。
        private void m_Form_Load(object sender, System.EventArgs e)
        {
            //MDI子窗体不支持，所以不要尝试使用混合方法的透明度。
            if (this.m_Form.MdiParent == null || this.m_Method != AnimationMethod.Blend)
            {
                //激活的形式。
                AnimateWindow(this.m_Form.Handle, this.m_Duration, (int)FormAnimator.AW_ACTIVATE | (int)this.m_Method | (int)this.m_Direction);
            }
        }

        //动画的形式时，它会自动显示或隐藏。
        private void m_Form_VisibleChanged(object sender, System.EventArgs e)  // 错误：把手条款不支持在C＃
        {
            //不要试图同时显示或隐藏，因为他们没有像预期的那样，动画MDI子窗体。
            if (this.m_Form.MdiParent == null)
            {
                int flags = (int)this.m_Method | (int)this.m_Direction;

                if (this.m_Form.Visible)
                {
                    //激活的形式。 
                    flags = flags | FormAnimator.AW_ACTIVATE;
                }
                else
                {
                    //隐藏的表格。
                    flags = flags | FormAnimator.AW_HIDE;
                }
                AnimateWindow(this.m_Form.Handle, this.m_Duration, flags);
            }
        }

        //动画的形式时，它将自动关闭。
        private void m_Form_Closing(object sender, System.ComponentModel.CancelEventArgs e) // 错误：把手条款不支持在C＃
        {
            if (!e.Cancel)
            {
                //MDI子窗体不支持，所以不要尝试使用混合方法的透明度。
                if (this.m_Form.MdiParent == null || this.m_Method != AnimationMethod.Blend)
                {
                    //隐藏的表格。
                    AnimateWindow(this.m_Form.Handle, this.m_Duration, (int)FormAnimator.AW_HIDE | (int)this.m_Method | (int)AnimationDirection.Down);
                }

            }
        }
        #endregion
        
        public void Dispose()
        {
            this.m_Form.Load -= new EventHandler(m_Form_Load);
            this.m_Form.VisibleChanged -= new EventHandler(m_Form_VisibleChanged);
            this.m_Form.Closing -= new CancelEventHandler(m_Form_Closing);

        }
    }
}
