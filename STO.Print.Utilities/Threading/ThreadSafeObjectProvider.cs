using System;
using System.Reflection;

namespace STO.Print.Utilities.Threading
{
    /// <summary>
    /// Allows to create thread safe instance for specified object
    /// </summary>
    /// <typeparam name="T">Type of the object for which thread safe instance is created</typeparam>
    public class ThreadSafeObjectProvider<T> : DisposableObject where T : class
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadSafeObjectProvider{T}"/> class.
        /// </summary>
        public ThreadSafeObjectProvider() : this(false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadSafeObjectProvider{T}"/> class.
        /// </summary>
        /// <param name="nonPublic">true if a public or nonpublic default constructor can match; 
        /// false if only a public default constructor can match.</param>
        public ThreadSafeObjectProvider(bool nonPublic)
        {
            this.NonPublic = nonPublic;
        }

        #endregion

        #region Fields

        private T _Instance;

        private bool NonPublic;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether this <see cref="ThreadSafeObjectProvider{T}"/>
        /// Instance is created.
        /// </summary>
        /// <value>
        /// 	<see langword="true"/> if created; otherwise, <see langword="false"/>.
        /// </value>
        public bool Created
        {
            get { return _Instance == null ? false : true; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates an instance of the specified type
        /// </summary>
        /// <returns>Created Instance</returns>
        public T Instance
        {
            get
            {
                if (_Instance == null)
                {
                    if (this.NonPublic)
                    {
                        this.CreateInstance(true);
                    }
                    else
                    {
                        _Instance = Activator.CreateInstance<T>();
                    }
                }
                return _Instance;
            }
        }

        /// <summary>
        /// Creates an instance of the specified type using the constructor that best matches
        /// the specified parameters.
        /// </summary>
        /// <param name="args">An array of one or more arguments that can participate in activation. .</param>
        public void CreateInstance(params Object[] args)
        {
            if (_Instance == null)
            {
                _Instance = (T) Activator.CreateInstance(typeof (T), args);
            }
        }

        /// <summary>
        /// Creates an instance of the specified type using the constructor that best matches
        /// the specified parameters.
        /// </summary>
        /// <param name="bindingAttr">A combination of zero or more bit flags that affect the 
        /// search for the type constructor. If bindingAttr is zero, a case-sensitive search for
        /// public constructors is conducted. .</param>
        /// <param name="args">An array of one or more arguments that can participate in activation. .</param>
        public void CreateInstance(BindingFlags bindingAttr,
                                   params Object[] args)
        {
            if (_Instance == null)
            {
                _Instance =
                    (T)
                    Activator.CreateInstance(typeof (T), bindingAttr, null, args,
                                             null);
            }
        }


        /// <summary>
        /// Creates an instance of the specified type using the constructor that best matches
        /// the specified parameters.
        /// </summary>
        /// <param name="bindingAttr">A combination of zero or more bit flags that affect the 
        /// search for the type constructor. If bindingAttr is zero, a case-sensitive search for
        /// public constructors is conducted. .</param>
        public void CreateInstance(BindingFlags bindingAttr)
        {
            if (_Instance == null)
            {
                _Instance =
                    (T)
                    Activator.CreateInstance(typeof (T), bindingAttr, null, null,
                                             null);
            }
        }

        /// <summary>
        /// Creates an instance of the specified type using the constructor that best matches
        /// the specified parameters.
        /// </summary>
        /// <param name="nonPublic">true if a public or nonpublic default constructor can match; 
        /// false if only a public default constructor can match.</param>
        public void CreateInstance(bool nonPublic)
        {
            if (_Instance == null)
            {
                if (nonPublic)
                {
                    _Instance =
                        (T) Activator.CreateInstance(typeof (T), nonPublic);
                }
            }
        }


        /// <summary>
        /// Override This Method To Dispose Unmanaged Resources.
        /// </summary>
        protected override void DisposeManagedResources()
        {
            this._Instance = null;
            base.DisposeManagedResources();
        }

        #endregion
    }
}