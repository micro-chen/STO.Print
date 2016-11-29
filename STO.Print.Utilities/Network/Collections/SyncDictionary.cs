using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace STO.Print.Utilities.Collections
{
    #region SyncDictionary

    /// <summary>
    /// Syncronized Dictionary
    /// </summary>
    [Serializable]
    internal class SyncDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        /// <summary>
        /// Gets the value associated with the specified key. 
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value">When this method returns, contains the value associated with the specified key, if the key is found; otherwise, the default value for the type of the value parameter. This parameter is passed uninitialized.</param>
        /// <returns>true if the Dictionary contains an element with the specified key; otherwise, false. </returns>
        public new bool TryGetValue(TKey key, out TValue value)
        {
            bool flag;
            lock (this.dictionary)
            {
                flag = this.dictionary.TryGetValue(key, out value);
            } // lock

            return flag;
        }



        /// <summary>Gets or sets the value associated with the specified key.</summary>
        /// <returns>The value associated with the specified key. If the specified key is not found, a get operation throws a <see cref="System.Collections.Generic.KeyNotFoundException"></see>, and a set operation creates a new element with the specified key.</returns>
        /// <param name="key">The key of the value to get or set.</param>
        /// <exception cref="System.ArgumentNullException">key is null.</exception>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">The property is retrieved and key does not exist in the collection.</exception>
        public new TValue this[TKey key]
        {
            get
            {
                return this.dictionary[key];
            }
            set
            {
                lock (this.dictionary)
                {
                    this.dictionary[key] = value;
                } // lock
            } // set
        }

        /// <summary>Gets a collection containing the values in the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see>.</summary>
        /// <returns>A <see cref="System.Collections.Generic.Dictionary{TKey, TValue}.KeyCollection"></see> containing the values in the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see>.</returns>
        public new KeyCollection Keys
        {
            get
            {
                KeyCollection collection;

                lock (this.dictionary)
                {
                    collection = new KeyCollection(this.dictionary);
                }
                return collection;

            }
        }



        /// <summary>Gets a collection containing the values in the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see>.</summary>
        /// <returns>A <see cref="System.Collections.Generic.Dictionary{TKey, TValue}.ValueCollection"></see> containing the values in the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see>.</returns>
        public new ValueCollection Values
        {
            get
            {

                ValueCollection collection;

                lock (this.dictionary)
                {
                    collection = new ValueCollection(this.dictionary);
                }
                return collection;

            }
        }

        /// <summary>Gets the <see cref="System.Collections.Generic.IEqualityComparer{TKey}"></see> that is used to determine equality of values for the dictionarytable. </summary>
        /// <returns>The <see cref="System.Collections.Generic.IEqualityComparer{TKey}"></see> generic interface implementation that is used to determine equality of values for the current <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see> and to provide hash values for the values.</returns>
        public new IEqualityComparer<TKey> Comparer
        {
            get
            {
                IEqualityComparer<TKey> tempComparer;
                lock (this.dictionary)
                {
                    tempComparer = this.dictionary.Comparer;
                }
                return tempComparer;
            }
        }



        /// <summary>Gets the number of key/value pairs contained in the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see>.</summary>
        /// <returns>The number of key/value pairs contained in the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see>.</returns>
        public new virtual int Count
        {
            get
            {
                return this.dictionary.Count;

            }
        }
        /// <summary>Removes the value with the specified key from the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see>.</summary>
        /// <returns>true if the element is successfully found and removed; otherwise, false.  This method returns false if key is not found in the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see>.</returns>
        /// <param name="key">The key of the element to remove.</param>
        /// <exception cref="System.ArgumentNullException">key is null.</exception>
        public new bool Remove(TKey key)
        {
            lock (this.dictionary)
            {
                return this.dictionary.Remove(key);
            }

        } // Remove

        /// <summary>Returns an enumerator that iterates through the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see>.</summary>
        /// <returns>A structure for the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see>.</returns>
        public new Enumerator GetEnumerator()
        {
            Enumerator temp;
            lock (this.dictionary)
            {
                temp = this.dictionary.GetEnumerator();
            } // lock

            return temp;
        } // GetEnumerator

        private Dictionary<TKey, TValue> dictionary;


        /// <summary>
        /// Initializes a new instance of the <see cref="SyncDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        internal SyncDictionary(Dictionary<TKey, TValue> dictionary)
        {
            this.dictionary = dictionary;
        }


        /// <summary>
        /// Adds the specified key and value to the dictionarytable.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add. The value can be null for reference types.</param>
        /// <exception cref="System.ArgumentException">An element with the same key already exists in the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see>.</exception>
        /// <exception cref="System.ArgumentNullException">key is null.</exception>
        public new void Add(TKey key, TValue value)
        {
            lock (this.dictionary)
            {
                this.dictionary.Add(key, value);
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
        public new void Clear()
        {
            lock (this.dictionary)
            {
                this.dictionary.Clear();
            }
        }

        /// <summary>
        /// Implements the <see cref="System.Runtime.Serialization.ISerializable"></see> interface and returns the data needed to serialize the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see> instance.
        /// </summary>
        /// <param name="info">A <see cref="System.Runtime.Serialization.SerializationInfo"></see> object that contains the information required to serialize the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see> instance.</param>
        /// <param name="context">A <see cref="System.Runtime.Serialization.StreamingContext"></see> structure that contains the source and destination of the serialized stream associated with the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see> instance.</param>
        /// <exception cref="System.ArgumentNullException">info is null.</exception>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }
            info.AddValue("ParentTable", this.dictionary, typeof(Dictionary<TKey, TValue>));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="info">A <see cref="System.Runtime.Serialization.SerializationInfo"></see> object containing the information required to serialize the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see>.</param>
        /// <param name="context">A <see cref="System.Runtime.Serialization.StreamingContext"></see> structure containing the source and destination of the serialized stream associated with the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see>.</param>
        internal SyncDictionary(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.dictionary = (Dictionary<TKey, TValue>)info.GetValue("ParentTable", typeof(Dictionary<TKey, TValue>));
            if (this.dictionary == null)
            {
                throw new SerializationException("Insufficient state to return the real object.");
            }
        }

        /// <summary>
        /// Copies the elements of the ICollection to an array of type KeyValuePair, starting at the specified array index. 
        /// </summary>
        /// <param name="array">The one-dimensional array of type KeyValuePair that is the destination of the KeyValuePair elements copied from the ICollection. The array must have zero-based indexing.</param>
        /// <param name="index">The zero-based index in array at which copying begins.</param>
        public new void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
        {
            lock (this.dictionary)
            {
                this.dictionary.CopyTo(array, index);
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
        /// Determines whether the specified key contains key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// 	<c>true</c> if the specified key contains key; otherwise, <c>false</c>.
        /// </returns>
        public new bool ContainsKey(TKey key)
        {
            return this.dictionary.ContainsKey(key);
        } // ContainsKey



        /// <summary>
        /// Determines whether the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see> contains a specific value.
        /// </summary>
        /// <param name="value">The value to locate in the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see>. The value can be null for reference types.</param>
        /// <returns>
        /// true if the <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see> contains an element with the specified value; otherwise, false.
        /// </returns>
        public new bool ContainsValue(TValue value)
        {
            bool flag;
            lock (this.dictionary)
            {
                flag = this.dictionary.ContainsValue(value);
            }
            return flag;
        } // ContainsValue

    } // class SyncDictionary 
    #endregion

}
