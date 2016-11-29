using System;

namespace STO.Print.Utilities.Threading
{
    /// <summary>
    /// Class that implements <see cref="T:System.IDisposable"/> to provide disposing 
    /// functions to inherited classes.
    /// </summary>
    [Serializable()]
    public abstract class DisposableObject : IDisposable
    {
        #region Destructors & IDisposable Members
        /// <summary>
        /// This is called when the object is garbage collected unless the Dispose() method is called.
        /// </summary>
        ~DisposableObject()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing,
        /// or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        #endregion

        #region Fields & Properties

        /// <summary>
        /// Track whether Dispose has been called.
        /// </summary>
        private DisposeState _disposeState = DisposeState.None;

        /// <summary>
        /// Occurs When the Object is disposed.
        /// </summary>
        public event EventHandler Disposed;



        /// <summary>
        /// Get Whether Object is Disposed.
        /// </summary>
        /// <value>True If Object is disposed else false.</value>
        public bool IsDisposed
        {
            get { return (this._disposeState == DisposeState.Disposed); }

        }

        /// <summary>
        /// Get Whether Object is Disposing.
        /// </summary>
        /// <value>True If Object is Disposing.</value>
        public bool IsDisposing
        {
            get { return (this._disposeState == DisposeState.Disposing); }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Method for checking the disposed flag and raising the 
        /// <see cref="T:System.ObjectDisposedException"/> exception
        /// </summary>
        /// <exception cref="T:System.ObjectDisposedException">
        ///		Thrown when a Object is Disposing Or Disposed
        ///	</exception>
        protected void CheckDisposed()
        {
            if (this._disposeState == DisposeState.Disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }
        }

        /// <summary>
        /// Raises the <see cref="DisposableObject.Disposed"/> event.
        /// </summary>
        /// <remarks>
        /// 	<note type="inheritinfo">
        ///        When overriding <see ref="M:SmartCore.DisposableObject.OnDisposed"/>
        /// 	   in a derived class, be sure to call the base class's 
        /// 	   <see cref="DisposableObject.OnDisposed"/> method so that
        /// 	   registered delegates receive the event.
        /// 	</note>
        /// </remarks>
        protected virtual void OnDisposed()
        {
            if (this.Disposed != null)
            {
                //Invokes the delegates
                this.Disposed(this, EventArgs.Empty);
            }
        }

  

        /// <summary>
        /// Override This Method To Dispose Unmanaged Resources.
        /// </summary>
        protected virtual void DisposeManagedResources() { }

        /// <summary>
        /// Override This Method To Dispose Managed Resources.
        /// </summary>
        protected virtual void DisposeUnmanagedResources() { }

        /// <summary>
        /// Used Internally To Dipose Object.
        /// </summary>
        /// <param name="disposing">If true, this method is called because the object is being disposed with the Dispose() method. If false, the object is being disposed by the garbage collector.</param>
        protected virtual void Dispose(bool disposing)
        {
            //Check to see if Dispose has already been called.
            if ((this._disposeState == DisposeState.None))
            {
                //change the state to Disposing
                this._disposeState = DisposeState.Disposing;

                try
                {
                    //If disposing equals true, dispose all managed 
                    // and unmanaged resources.
                    if (disposing)
                    {
                      
                        this.DisposeManagedResources();
                        this.DisposeUnmanagedResources();
                        this._disposeState = DisposeState.Disposed;
                        this.OnDisposed();
                        GC.SuppressFinalize(this);
                    }
                    else
                    {
                        this.DisposeUnmanagedResources();
                        this._disposeState = DisposeState.Disposed;
                    }
                }
                catch
                {
                    this._disposeState = DisposeState.None;
                    throw;
                }
            }
        }

        #endregion
    }
}
