using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Xml.Serialization;

namespace STO.Print.Utilities.Collections
{
    /// <summary>
    /// Represents a strongly typed list of objects that 
    /// can be accessed by index. Provides methods to search, sort, 
    /// and manipulate lists. 
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    [Serializable()]
    [XmlRoot("list")]
    public class List<T> : System.Collections.Generic.List<T>, ICloneable, ICloneable<List<T>>
    {

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="List{T}"></see> class that is empty and has the default initial capacity.
        /// </summary>
        public List() : base() { } // List

        /// <summary>
        /// Initializes a new instance of the <see cref="List{T}"></see> class that contains elements copied from the specified collection and has sufficient capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new list.</param>
        /// <exception cref="System.ArgumentNullException">collection is null.</exception>
        public List(IEnumerable<T> collection) : base(collection) { } // List

        /// <summary>
        /// Initializes a new instance of the <see cref="List{T}"></see> class that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">capacity is less than 0. </exception>
        public List(int capacity) : base(capacity) { } // List

        #endregion

        #region Destructor

        #endregion

        #region Fields

        #endregion

        #region Events

        #endregion

        #region Operators

        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether access to the <see cref="System.Collections.ICollection"></see> is synchronized (thread safe).
        /// </summary>
        /// <value>true if access to the Dictionary is synchronized (thread safe); otherwise, false. The default is false. </value>
        /// <returns>true if access to the <see cref="System.Collections.ICollection"></see> is synchronized (thread safe); otherwise, false.</returns>
        public virtual bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        /// <summary>Returns an list wrapper that is synchronized (thread safe).</summary>
        /// <returns>An List wrapper that is synchronized (thread safe).</returns>
        /// <param name="list">The list to synchronize. </param>
        /// <exception cref="System.ArgumentNullException">list is null. </exception>
        /// <typeparam name="V">The type of elements in the list.</typeparam>
        [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
        public static List<V> Synchronized<V>(List<V> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            return new SyncList<V>(list);
        }

        /// <summary>Returns an <see cref="System.Collections.ArrayList"></see> whose elements are copies of the specified value.</summary>
        /// <returns>An <see cref="System.Collections.ArrayList"></see> with count number of elements, all of which are copies of value.</returns>
        /// <param name="count">The number of times value should be copied. </param>
        /// <param name="value">The <see cref="System.Object"></see> to copy multiple times in the new <see cref="System.Collections.ArrayList"></see>. The value can be null. </param>
        /// <exception cref="System.ArgumentOutOfRangeException">count is less than zero. </exception>
        /// <typeparam name="V">The type of elements in the list.</typeparam>
        public static List<V> Repeat<V>(V value, int count)
        {
            if (count < 0)
            {
                throw new ArgumentException("count", "Non-negative number required.");
            }

            List<V> list = new List<V>(count);
            for (int index = 0; index < count; index++)
            {
                list.Add(value);
            }

            return list;
        }
        #endregion

        #region Methods

        #endregion
        #region ICloneable<T> Members

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public List<T> Clone()
        {
            return new List<T>(this);
        }

        #endregion

        #region ICloneable Members

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        object ICloneable.Clone()
        {
            return this.Clone();
        }

        #endregion




    }
}
