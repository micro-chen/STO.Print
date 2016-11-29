using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace STO.Print.Utilities.Collections
{
    /// <summary>
    /// Represents a Ordered collection of values and values. 
    /// </summary>
    /// <typeparam name="TKey">The type of the values in the dictionarytable.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionarytable.</typeparam>
    [Serializable()]
    public class OrderedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICloneable,
        ICloneable<OrderedDictionary<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, 
        IEnumerable<TValue>
    {

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderedDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <summary>Initializes a new instance of the
        /// <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see> class that is empty, has the default initial capacity, and uses the default equality comparer for the key type.</summary>
        public OrderedDictionary()
        {
            dictionary = new Dictionary<TKey, TValue>();
            list = new List<KeyValuePair<TKey, TValue>>();

        } // OrderedDictionary

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderedDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="dictionary">The dictionarytable.</param>
        public OrderedDictionary(IDictionary<TKey, TValue> dictionary)
        {
            this.dictionary = new Dictionary<TKey, TValue>(dictionary);
            list = new List<KeyValuePair<TKey, TValue>>();
        } // OrderedDictionary


        /// <summary>
        /// Initializes a new instance of the <see cref="OrderedDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        public OrderedDictionary(IEqualityComparer<TKey> comparer)
        {
            dictionary = new Dictionary<TKey, TValue>(comparer);
            list = new List<KeyValuePair<TKey, TValue>>();
        } // OrderedDictionary

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderedDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        public OrderedDictionary(int capacity)
        {
            dictionary = new Dictionary<TKey, TValue>(capacity);
            list = new List<KeyValuePair<TKey, TValue>>();
        } // OrderedDictionary

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderedDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="dictionary">The dictionarytable.</param>
        /// <param name="comparer">The comparer.</param>
        public OrderedDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
        {
            this.dictionary = new Dictionary<TKey, TValue>(dictionary, comparer);
            list = new List<KeyValuePair<TKey, TValue>>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderedDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <param name="comparer">The comparer.</param>
        public OrderedDictionary(int capacity, IEqualityComparer<TKey> comparer)
        {
            dictionary = new Dictionary<TKey, TValue>(capacity, comparer);
            list = new List<KeyValuePair<TKey, TValue>>();
        }
        #endregion

        #region Destructor

        #endregion

        #region Fields
        private Dictionary<TKey, TValue> dictionary;
        private List<KeyValuePair<TKey, TValue>> list;

        #endregion

        #region Events

        #endregion

        #region Operators

        #endregion

        #region Properties
        /// <summary>Gets the <see cref="System.Collections.Generic.IEqualityComparer{TKey}"></see> that is used to determine equality of keys for the dictionarytable. </summary>
        /// <returns>The <see cref="System.Collections.Generic.IEqualityComparer{TKey}"></see> generic interface implementation that is used to determine equality of keys for the current <see cref="T:System.Collections.Generic.Dictionary`2"></see> and to provide hash values for the keys.</returns>
        public virtual IEqualityComparer<TKey> Comparer
        {
            get
            {
                return this.dictionary.Comparer;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Determines whether the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see> ContainsKey a specific value.
        /// </summary>
        /// <param name="value">The value to locate in the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see>. The value can be null for reference types.</param>
        /// <returns>
        /// true if the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see> ContainsKey an element with the specified value; otherwise, false.
        /// </returns>
        public virtual bool ContainsValue(TValue value)
        {
            return this.dictionary.ContainsValue(value);
        } // ContainsValue

        #endregion

        #region IDictionary<TKey,TValue> Members

        /// <summary>
        /// Adds an element with the provided key and value to the 
        /// <see cref="System.Collections.Generic.IDictionary{TKey, TValue}"></see>.
        /// </summary>
        /// <param name="key">The object to use as the key of the element to add.</param>
        /// <param name="value">The object to use as the value of the element to add.</param>
        /// <exception cref="System.NotSupportedException">The 
        /// <see cref="System.Collections.Generic.IDictionary{TKey, TValue}"></see> 
        /// is read-only.</exception>
        /// <exception cref="System.ArgumentException">An element with the 
        /// same key already exists in the 
        /// <see cref="System.Collections.Generic.IDictionary{TKey, TValue}"></see>.</exception>
        /// <exception cref="System.ArgumentNullException">key is null.</exception>
        public virtual void Add(TKey key, TValue value)
        {
            this.dictionary.Add(key, value);
            this.list.Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        /// <summary>
        /// Determines whether the <see cref="System.Collections.Generic.IDictionary{TKey, TValue}"></see> ContainsKey an element with the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the <see cref="System.Collections.Generic.IDictionary{TKey, TValue}"></see>.</param>
        /// <returns>
        /// true if the <see cref="System.Collections.Generic.IDictionary{TKey, TValue}"></see> ContainsKey an element with the key; otherwise, false.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">key is null.</exception>
        public virtual bool ContainsKey(TKey key)
        {
            return this.dictionary.ContainsKey(key);

        }

        /// <summary>
        /// Inserts an item to the <see cref="T:System.Collections.Generic.IList`1"></see> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="key">The object to use as the key of the element to insert.</param>
        /// <param name="value">The object to use as the value of the element to insert.</param>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.IList`1"></see> is read-only.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="T:System.Collections.Generic.IList`1"></see>.</exception>
        public void Insert(int index, TKey key, TValue value)
        {
            if ((index < 0) || (index >= this.Count))
            {
                throw new ArgumentOutOfRangeException("index");
            }

            this.dictionary.Add(key, value);
            this.list.Insert(index, new KeyValuePair<TKey, TValue>(key, value));
        }


        /// <summary>
        /// Determines whether the specified key ContainsKey key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// 	<c>true</c> if the specified key ContainsKey key; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool Contains(TKey key)
        {
            return this.ContainsKey(key);
        } // ContainsKey


        /// <summary>
        /// Gets an collection containing the values of the <see cref="System.Collections.Generic.IDictionary{TKey, TValue}"></see>.
        /// </summary>
        /// <value></value>
        /// <returns>An collection containing the values of the object that implements <see cref="System.Collections.Generic.IDictionary{TKey, TValue}"></see>.</returns>
        public virtual ICollection<TKey> Keys
        {
            get
            {
                List<TKey> keys = new List<TKey>();

                for (int index = 0; index < this.list.Count; index++)
                {
                    KeyValuePair<TKey, TValue> keyValue = this.list[index];
                    keys.Add(keyValue.Key);
                }
                return keys;
            }
        }

        /// <summary>
        /// Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1"></see> by key.
        /// </summary>
        /// <param name="key">The object key to locate in the <see cref="T:System.Collections.Generic.IList`1"></see>.</param>
        /// <returns>
        /// The index of item if found in the list; otherwise, -1.
        /// </returns>
        public int IndexOf(TKey key)
        {
            for (int index = 0; index < this.list.Count; index++)
            {
                KeyValuePair<TKey, TValue> keyValue = this.list[index];
                if (keyValue.Key.Equals(key))
                {
                    return index;
                }
            }
            return -1;
        }


        /// <summary>
        /// Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1"></see>.
        /// </summary>
        /// <param name="value">The object to locate in the <see cref="T:System.Collections.Generic.IList`1"></see>.</param>
        /// <returns>
        /// The index of item if found in the list; otherwise, -1.
        /// </returns>
        public int IndexOf(TValue value)
        {
            for (int index = 0; index < this.list.Count; index++)
            {
                KeyValuePair<TKey, TValue> keyValue = this.list[index];
                if (keyValue.Value.Equals(value))
                {
                    return index;
                }
            }
            return -1;
        }




        /// <summary>
        /// Removes the element with the specified key from the <see cref="System.Collections.Generic.IDictionary{TKey, TValue}"></see>.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>
        /// true if the element is successfully removed; otherwise, false.  This method also returns false if key was not found in the original <see cref="System.Collections.Generic.IDictionary{TKey, TValue}"></see>.
        /// </returns>
        /// <exception cref="System.NotSupportedException">The <see cref="System.Collections.Generic.IDictionary{TKey, TValue}"></see> is read-only.</exception>
        /// <exception cref="System.ArgumentNullException">key is null.</exception>
        public virtual bool Remove(TKey key)
        {
            bool flag = this.dictionary.Remove(key);
            this.list.RemoveAt(this.IndexOf(key));
            return flag;
        }
    

        /// <summary>
        /// Removes the <see cref="T:System.Collections.Generic.IList`1"></see> item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.IList`1"></see> is read-only.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="T:System.Collections.Generic.IList`1"></see>.</exception>
        public void RemoveAt(int index)
        {
            if ((index < 0) || (index >= this.Count))
            {
                throw new ArgumentOutOfRangeException("index");
            }

            KeyValuePair<TKey, TValue> keyValue = this.list[index];
            this.dictionary.Remove(keyValue.Key);
            this.list.RemoveAt(index);
        }


        /// <summary>
        /// Tries the get value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual bool TryGetValue(TKey key, out TValue value)
        {
            bool flag = this.dictionary.TryGetValue(key, out value);
            return flag;
        }

        /// <summary>
        /// Gets an collection containing the values in the <see cref="System.Collections.Generic.IDictionary{TKey, TValue}"></see>.
        /// </summary>
        /// <value></value>
        /// <returns>An collection containing the values in the object that implements <see cref="System.Collections.Generic.IDictionary{TKey, TValue}"></see>.</returns>
        public virtual ICollection<TValue> Values
        {
            get
            {
                List<TValue> values = new List<TValue>();

                for (int index = 0; index < this.list.Count; index++)
                {
                    KeyValuePair<TKey, TValue> keyValue = this.list[index];
                    values.Add(keyValue.Value);
                }
                return values;
            } // get
        }

        /// <summary>
        /// Gets or sets the value with the specified key.
        /// </summary>
        /// <value></value>
        public virtual TValue this[TKey key]
        {
            get
            {
                if (this.dictionary.ContainsKey(key))
                {
                    return this.dictionary[key];
                }
                return default(TValue);
            }
            set
            {
                if (this.dictionary.ContainsKey(key))
                {
                    this.dictionary[key] = value;
                    this.list[this.IndexOf(key)] = new KeyValuePair<TKey, TValue>(key, value);
                }
                else
                {
                    this.Add(key, value);
                }
            }
        }

        /// <summary>Gets or sets the value associated on the specified index.</summary>
        /// <returns>The value associated on the specified index. If the specified index is invalid, 
        /// a get or set operation throws a <see cref="ArgumentOutOfRangeException"></see>
        /// </returns>
        /// <param name="index">The index at which the value is get or set.</param>
        /// <exception cref="ArgumentOutOfRangeException">index is greater than or less than zero</exception>
        public virtual TValue this[int index]
        {
            get
            {
                if ((index < 0) || (index >= this.Count))
                {
                    throw new ArgumentOutOfRangeException("index");
                }

                return this.list[index].Value;
            }
            set
            {
                if ((index < 0) || (index >= this.Count))
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                TKey key = this.list[index].Key;

                if (this.dictionary.ContainsKey(key))
                {
                    this.dictionary[key] = value;
                    this.list[index] = new KeyValuePair<TKey, TValue>(key, value);
                }
                else
                {
                    this.Add(key, value);
                }
            }
        }

        /// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable"></see> interface and returns the data needed to serialize the <see cref="T:System.Collections.Generic.Dictionary`2"></see> instance.</summary>
        /// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext"></see> structure that ContainsKey the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.Dictionary`2"></see> instance.</param>
        /// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> object that ContainsKey the information required to serialize the <see cref="T:System.Collections.Generic.Dictionary`2"></see> instance.</param>
        /// <exception cref="T:System.ArgumentNullException">info is null.</exception>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            this.dictionary.GetObjectData(info, context);
        }

        /// <summary>
        /// Gets a value indicating whether access to the <see cref="System.Collections.ICollection"></see> is synchronized (thread safe).
        /// </summary>
        /// <value>true if access to the Dictionary is synchronized (thread safe); otherwise, false. The default is false. </value>
        /// <returns>true if access to the <see cref="System.Collections.ICollection"></see> is synchronized (thread safe); otherwise, false.</returns>
        public virtual bool IsSynchronized
        {
            get
            {
                return this.dictionary.IsSynchronized;
            } // get
        }


        /// <summary>
        /// Returns a synchronized (thread safe) wrapper for the Hashtable. 
        /// </summary>
        /// <param name="dictionary">The dictionarytable to synchronize. </param>
        /// <returns>A synchronized (thread safe) wrapper for the dictionarytable. </returns>
        [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
        public static OrderedDictionary<TKey, TValue> Synchronized(OrderedDictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }
            return new SyncOrderedDictionary<TKey, TValue>(dictionary);
        }

        /// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable"></see> interface and raises the deserialization event when the deserialization is complete.</summary>
        /// <param name="sender">The source of the deserialization event.</param>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> object associated with the current <see cref="T:System.Collections.Generic.Dictionary`2"></see> instance is invalid.</exception>
        public virtual void OnDeserialization(object sender)
        {
            this.dictionary.OnDeserialization(sender);
        } // OnDeserialization


        #endregion

        #region ICollection<KeyValuePair<TKey,TValue>> Members

        /// <summary>
        /// Adds an item
        /// </summary>
        /// <param name="keyValuePair">The key value pair.</param>
        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
        {
            this.Add(keyValuePair.Key, keyValuePair.Value);
        }

        /// <summary>
        /// Removes all items
        /// </summary>
        /// <exception cref="System.NotSupportedException">The collection is read-only. </exception>
        public virtual void Clear()
        {
            this.dictionary.Clear();
            this.list.Clear();
        }

        /// <summary>
        /// Determines whether the collection ContainsKey a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the collection.</param>
        /// <returns>
        /// true if item is found in the collection; otherwise, false.
        /// </returns>
        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            if (this.ContainsKey(item.Key))
            {
                TValue keyValue = this[item.Key];

                if (keyValue.Equals(item.Value))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Copies the elements of the collection to an <see cref="System.Array"></see>, starting at a particular <see cref="System.Array"></see> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="System.Array"></see> that is the destination of the elements copied from collection. The <see cref="System.Array"></see> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
        /// <exception cref="System.ArgumentNullException">array is null.</exception>
        /// <exception cref="System.ArgumentException">array is multidimensional.-or-arrayIndex is equal to or greater than the length of array.-or-The number of elements in the source collection is greater than the available space from arrayIndex to the end of the destination array.-or-Type TKey cannot be cast automatically to the type of the destination array.</exception>
        public virtual void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            // Calling CopyTo on a Dictionary
            // Copies all KeyValuePair objects in Dictionary object to objs[]
           this.dictionary.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of elements contained in the collection.
        /// </summary>
        /// <value></value>
        /// <returns>The number of elements contained in the collection.</returns>
        public virtual int Count
        {
            get { return this.list.Count; }
        }



        /// <summary>
        /// Removes the first occurrence of a specific object from the collection.
        /// </summary>
        /// <param name="item">The object to remove from the collection.</param>
        /// <returns>
        /// true if item was successfully removed from the collection; otherwise, false. This method also returns false if item is not found in the original collection.
        /// </returns>
        /// <exception cref="System.NotSupportedException">The collection is read-only.</exception>
        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            if (this.ContainsKey(item.Key))
            {
                TValue keyValue = this[item.Key];

                if (keyValue.Equals(item.Value))
                {
                    return this.dictionary.Remove(item.Key);
                }
            }
            return false;

        }

        /// <summary>
        /// Gets a value indicating whether the collection is read-only.
        /// </summary>
        /// <value></value>
        /// <returns>true if the collection is read-only; otherwise, false.</returns>
        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get { return false; }
        }

        #endregion

        #region IEnumerable<KeyValuePair<TKey,TValue>> Members


        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"></see> that can be used to iterate through the collection.
        /// </returns>
        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return new OrderedDictionaryEnumerator<TKey, TValue>(this.list);
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns></returns>
        public virtual OrderedDictionaryEnumerator<TKey, TValue> GetEnumerator()
        {
            return new OrderedDictionaryEnumerator<TKey, TValue>(this.list);
        }




        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new OrderedDictionaryEnumerator<TKey, TValue>(this.list);
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

        #region ICloneable<OrderedDictionary<TKey,TValue>> Members

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public OrderedDictionary<TKey, TValue> Clone()
        {
            return new OrderedDictionary<TKey, TValue>(this);
        }

        #endregion


        #region IEnumerable<TValue> Members

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"></see> that can be used to iterate through the collection.
        /// </returns>
        IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
        {
            return new OrderedDictionaryEnumerator<TKey, TValue>(this.list);
        }

        #endregion
    }
}
