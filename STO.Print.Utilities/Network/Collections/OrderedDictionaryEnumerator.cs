using System;
using System.Collections.Generic;

namespace STO.Print.Utilities.Collections
{
    /// <summary>
    /// Enumerator class for Ordered Dictionary.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public sealed class OrderedDictionaryEnumerator<TKey, TValue> : IEnumerator<KeyValuePair<TKey, TValue>>, IEnumerator<TValue>
    {

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderedDictionaryEnumerator{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="list">The list.</param>
        internal OrderedDictionaryEnumerator(List<KeyValuePair<TKey, TValue>> list)
        {
            this.index = -1;
            this.list = list;
        } // method

        #endregion

        #region Destructor
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="OrderedDictionaryEnumerator{TKey, TValue}"/> is reclaimed by garbage collection.
        /// </summary>
        ~OrderedDictionaryEnumerator()
        {
            this.Dispose(false);
        }
        #endregion

        #region Fields
        private int index;

        private List<KeyValuePair<TKey, TValue>> list;

        // Track whether Dispose has been called.
        private bool disposed = false;

        #endregion

        #region Events

        #endregion

        #region Operators

        #endregion

        #region Properties

        #endregion

        #region Methods


        /// <summary>
        /// Dispose(bool disposing) executes in two distinct scenarios.
        /// If disposing equals true, the method has been called directly
        /// or indirectly by a user's code. Managed and unmanaged resources
        /// can be disposed.
        /// If disposing equals false, the method has been called by the 
        /// runtime from inside the finalizer and you should not reference 
        /// other objects. Only unmanaged resources can be disposed.
        /// </summary>
        /// <param name="disposing">if set to <see langword="true"/> [disposing].</param>
        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // free managed resources
                    if (list != null)
                    {
                        list = null;
                    }
                    GC.SuppressFinalize(this);
                }
                disposed = true;
            }
        }


        #endregion

        #region IEnumerator<KeyValuePair<TKey,TValue>> Members

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        /// <value></value>
        /// <returns>The element in the collection at the current position of the enumerator.</returns>
        KeyValuePair<TKey, TValue> IEnumerator<KeyValuePair<TKey, TValue>>.Current
        {
            get
            {
                return this.InternalCurrent;

            }
        }

        /// <summary>
        /// Gets the internal current.
        /// </summary>
        /// <value>The internal current.</value>
        private KeyValuePair<TKey, TValue> InternalCurrent
        {
            get
            {
                if ((this.index < 0) || (this.index >= this.list.Count))
                {
                    throw new InvalidOperationException();
                }
                return this.list[this.index];

            }
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>The key.</value>
        public TKey Key
        {
            get
            {
                return this.InternalCurrent.Key;
            }
        }


        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public TValue Value
        {
            get
            {
                return this.InternalCurrent.Value;
            }
        }



        #endregion

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() { this.Dispose(true); }

        #endregion

        #region IEnumerator Members

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        /// <value></value>
        /// <returns>The element in the collection at the current position of the enumerator.</returns>
        object System.Collections.IEnumerator.Current
        {
            get
            {
                return this.InternalCurrent.Value;
            } // get
        }

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
        public bool MoveNext()
        {
            this.index++;
            if (this.index >= this.list.Count)
            {
                return false;
            } // if
            return true;

        } // MoveNext

        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first element in the collection.
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
        public void Reset()
        {
            this.index = -1;
        }

        #endregion


        #region IEnumerator<TValue> Members

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        /// <value></value>
        /// <returns>The element in the collection at the current position of the enumerator.</returns>
        public TValue Current
        {
            get { return this.InternalCurrent.Value; }
        }

        #endregion
 
    }
}
