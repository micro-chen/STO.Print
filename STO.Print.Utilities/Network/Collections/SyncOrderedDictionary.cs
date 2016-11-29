using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace STO.Print.Utilities.Collections
{
    #region SyncOrderedList

    /// <summary>
    /// Syncronized Dictionary
    /// </summary>
    /// <typeparam name="TKey">The type of the values in the dictionarytable.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionarytable.</typeparam>
    [Serializable]
    internal class SyncOrderedDictionary<TKey, TValue> : OrderedDictionary<TKey, TValue>
    {
        /// <summary>
        /// Gets the value associated with the specified key. 
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value">When this method returns, ContainsKey the value associated with the specified key, if the key is found; otherwise, the default value for the type of the value parameter. This parameter is passed uninitialized.</param>
        /// <returns>true if the Dictionary ContainsKey an element with the specified key; otherwise, false. </returns>
        public override bool TryGetValue(TKey key, out TValue value)
        {
            bool flag;
            lock (this.dictionarytable)
            {
                flag = this.dictionarytable.TryGetValue(key, out value);
            } // lock

            return flag;
        }



        /// <summary>Gets or sets the value associated with the specified key.</summary>
        /// <returns>The value associated with the specified key. If the specified key is not found, a get operation throws a <see cref="System.Collections.Generic.KeyNotFoundException"></see>, and a set operation creates a new element with the specified key.</returns>
        /// <param name="key">The key of the value to get or set.</param>
        /// <exception cref="System.ArgumentNullException">key is null.</exception>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">The property is retrieved and key does not exist in the collection.</exception>
        public override TValue this[TKey key]
        {
            get
            {
                return this.dictionarytable[key];
            }
            set
            {
                lock (this.dictionarytable)
                {
                    this.dictionarytable[key] = value;
                } // lock
            } // set
        }

        /// <summary>
        /// Gets or sets the Value at the specified index.
        /// </summary>
        /// <value></value>
        public override TValue this[int index]
        {
            get
            {
                return this.dictionarytable[index];
            }
            set
            {
                lock (this.dictionarytable)
                {
                    this.dictionarytable[index] = value;
                } // lock
            }
        }

        /// <summary>Gets a collection containing the values in the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see>.</summary>
        /// <returns>A <see cref="System.Collections.Generic.Dictionary{TKey, TValue}.KeyCollection"></see> containing the values in the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see>.</returns>
        public override ICollection<TKey> Keys
        {
            get
            {
                ICollection<TKey> keys;

                lock (this.dictionarytable)
                {
                    keys = this.dictionarytable.Keys;
                }
                return keys;

            }
        }


        /// <summary>Gets a collection containing the values in the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see>.</summary>
        /// <returns>A <see cref="System.Collections.Generic.Dictionary{TKey, TValue}.ValueCollection"></see> containing the values in the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see>.</returns>
        public override ICollection<TValue> Values
        {
            get
            {
                ICollection<TValue> collection;


                lock (this.dictionarytable)
                {
                    collection = this.dictionarytable.Values;
                }
                return collection;

            }
        }

        /// <summary>Gets the <see cref="System.Collections.Generic.IEqualityComparer{TKey}"></see> that is used to determine equality of values for the dictionarytable. </summary>
        /// <returns>The <see cref="System.Collections.Generic.IEqualityComparer{TKey}"></see> generic interface implementation that is used to determine equality of values for the current <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see> and to provide hash values for the values.</returns>
        public override IEqualityComparer<TKey> Comparer
        {
            get
            {
                IEqualityComparer<TKey> tempComparer;
                lock (this.dictionarytable)
                {
                    tempComparer = this.dictionarytable.Comparer;
                }
                return tempComparer;
            }
        }



        /// <summary>Gets the number of key/value pairs contained in the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see>.</summary>
        /// <returns>The number of key/value pairs contained in the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see>.</returns>
        public override int Count
        {
            get
            {
                return this.dictionarytable.Count;

            }
        }
        /// <summary>Removes the value with the specified key from the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see>.</summary>
        /// <returns>true if the element is successfully found and removed; otherwise, false.  This method returns false if key is not found in the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see>.</returns>
        /// <param name="key">The key of the element to remove.</param>
        /// <exception cref="System.ArgumentNullException">key is null.</exception>
        public override bool Remove(TKey key)
        {
            lock (this.dictionarytable)
            {
                return this.dictionarytable.Remove(key);
            }

        } // Remove

        /// <summary>Returns an enumerator that iterates through the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see>.</summary>
        /// <returns>A structure for the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see>.</returns>
        public override OrderedDictionaryEnumerator<TKey, TValue> GetEnumerator()
        {
            OrderedDictionaryEnumerator<TKey, TValue> temp;
            lock (this.dictionarytable)
            {
                temp = this.dictionarytable.GetEnumerator();
            } // lock

            return temp;
        } // GetEnumerator

        private OrderedDictionary<TKey, TValue> dictionarytable;


        /// <summary>
        /// Initializes a new instance of the <see cref="SyncOrderedDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="dictionary">The dictionarytable.</param>
        internal SyncOrderedDictionary(OrderedDictionary<TKey, TValue> dictionary)
        {
            this.dictionarytable = dictionary;
        }


        /// <summary>
        /// Adds the specified key and value to the dictionarytable.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add. The value can be null for reference types.</param>
        /// <exception cref="System.ArgumentException">An element with the same key already exists in the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see>.</exception>
        /// <exception cref="System.ArgumentNullException">key is null.</exception>
        public override void Add(TKey key, TValue value)
        {
            lock (this.dictionarytable)
            {
                this.dictionarytable.Add(key, value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether access to the <see cref="System.Collections.ICollection"></see> is synchronized (thread safe).
        /// </summary>
        /// <value>true if access to the Dictionary is synchronized (thread safe); otherwise, false. The default is false. </value>
        /// <returns>true if access to the <see cref="System.Collections.ICollection"></see> is synchronized (thread safe); otherwise, false.</returns>
        public override bool IsSynchronized
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Removes all values and values from the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see>.
        /// </summary>
        public override void Clear()
        {
            lock (this.dictionarytable)
            {
                this.dictionarytable.Clear();
            }
        }

        /// <summary>
        /// Implements the <see cref="System.Runtime.Serialization.ISerializable"></see> interface and returns the data needed to serialize the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see> instance.
        /// </summary>
        /// <param name="info">A <see cref="System.Runtime.Serialization.SerializationInfo"></see> object that ContainsKey the information required to serialize the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see> instance.</param>
        /// <param name="context">A <see cref="System.Runtime.Serialization.StreamingContext"></see> structure that ContainsKey the source and destination of the serialized stream associated with the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see> instance.</param>
        /// <exception cref="System.ArgumentNullException">info is null.</exception>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }
            info.AddValue("ParentTable", this.dictionarytable, typeof(Dictionary<TKey, TValue>));
        }


        /// <summary>
        /// Copies the elements of the ICollection to an array of type KeyValuePair, starting at the specified array index. 
        /// </summary>
        /// <param name="array">The one-dimensional array of type KeyValuePair that is the destination of the KeyValuePair elements copied from the ICollection. The array must have zero-based indexing.</param>
        /// <param name="index">The zero-based index in array at which copying begins.</param>
        public override void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
        {
            lock (this.dictionarytable)
            {
                this.dictionarytable.CopyTo(array, index);
            }

        } // CopyTo


        /// <summary>
        /// Implements the <see cref="System.Runtime.Serialization.ISerializable"></see> interface and raises the deserialization event when the deserialization is complete.
        /// </summary>
        /// <param name="sender">The source of the deserialization event.</param>
        /// <exception cref="System.Runtime.Serialization.SerializationException">The <see cref="System.Runtime.Serialization.SerializationInfo"></see> object associated with the current <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see> instance is invalid.</exception>
        public override void OnDeserialization(object sender)
        {
            //Syncronized Dictionory Cannot throw event
        }

        /// <summary>
        /// Determines whether the specified key ContainsKey key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// 	<c>true</c> if the specified key ContainsKey key; otherwise, <c>false</c>.
        /// </returns>
        public override bool ContainsKey(TKey key)
        {
            return this.dictionarytable.ContainsKey(key);
        } // ContainsKey



        /// <summary>
        /// Determines whether the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see> ContainsKey a specific value.
        /// </summary>
        /// <param name="value">The value to locate in the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see>. The value can be null for reference types.</param>
        /// <returns>
        /// true if the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see> ContainsKey an element with the specified value; otherwise, false.
        /// </returns>
        public override bool ContainsValue(TValue value)
        {
            bool flag;
            lock (this.dictionarytable)
            {
                flag = this.dictionarytable.ContainsValue(value);
            }
            return flag;
        } // ContainsValue

    } // class SyncOrderedList 
    #endregion
}
