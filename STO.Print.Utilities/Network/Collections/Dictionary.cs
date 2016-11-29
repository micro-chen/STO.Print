#region Copyright(c) 2006 ZO, All right reserved.
// -----------------------------------------------------------------------------
// Copyright(c) 2006 ZO, All right reserved.
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//
//   1.  Redistribution of source code must retain the above copyright notice,
//       this list of conditions and the following disclaimer.
//   2.  Redistribution in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//   3.  The name of the author may not be used to endorse or promote products
//       derived from this software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE AUTHOR ``AS IS'' AND ANY EXPRESS OR IMPLIED
// WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF
// MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO
// EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
// SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
// PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS;
// OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,
// WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR
// OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
// ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
// -----------------------------------------------------------------------------
#endregion


#region Using directives

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Generics = System.Collections.Generic;

#endregion

namespace STO.Print.Utilities.Collections
{
    /// <summary>
    /// 表示键和值的集合。 
    /// </summary>
    /// <typeparam name="TKey">字典中的键的类型。</typeparam>
    /// <typeparam name="TValue">在字典中的值的类型.</typeparam>
    [Serializable()]
    [XmlRoot("dictionary")]
    public class Dictionary<TKey, TValue> : 
        System.Collections.Generic.Dictionary<TKey, TValue>, ICloneable, 
        ICloneable<Dictionary<TKey, TValue>>, IXmlSerializable
    {

        #region Constructors
        /// <summary>
        /// 初始化一个新的实例
        /// <see cref="Dictionary{TKey, TValue}"></see> class that is empty, has the default initial capacity, and uses the default equality comparer for the key type.
        /// </summary>
     
        public Dictionary() : base() { } // Dictionary

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="Dictionary{TKey, TValue}"></see> class that contains elements copied from the specified <see cref="System.Collections.Generic.IDictionary{TKey, TValue}"></see> and uses the default equality comparer for the key type.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <exception cref="System.ArgumentException">dictionarytable contains one or more duplicate values.</exception>
        /// <exception cref="System.ArgumentNullException">dictionarytable is null.</exception>
        public Dictionary(Generics.IDictionary<TKey, TValue> dictionary) : base(dictionary) { } // Dictionary

        /// <summary>Initializes a new instance of the 
        /// <see cref="Dictionary{TKey, TValue}"></see> class that is empty, has the default initial capacity, and uses the specified <see cref="System.Collections.Generic.IEqualityComparer{TKey}"></see>.</summary>
        /// <param name="comparer">The <see cref="System.Collections.Generic.IEqualityComparer{TKey}"></see> implementation to use when comparing values, or null to use the default <see cref="System.Collections.Generic.EqualityComparer{TKey}"></see> for the type of the key.</param>
        public Dictionary(Generics.IEqualityComparer<TKey> comparer) : base(comparer) { } // Dictionary

        /// <summary>Initializes a new instance of the 
        /// <see cref="Dictionary{TKey, TValue}"></see> class that is empty, has the specified initial capacity, and uses the default equality comparer for the key type.</summary>
        /// <param name="capacity">The initial number of elements that the <see cref="Dictionary{TKey, TValue}"></see> can contain.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">capacity is less than 0.</exception>
        public Dictionary(int capacity) : base(capacity) { } // Dictionary

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="Dictionary{TKey, TValue}"></see> class that contains elements copied from the specified <see cref="System.Collections.Generic.IDictionary{TKey, TValue}"></see> and uses the specified <see cref="System.Collections.Generic.IEqualityComparer{TKey}"></see>.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="comparer">The <see cref="System.Collections.Generic.IEqualityComparer{TKey}"></see> implementation to use when comparing values, or null to use the default <see cref="System.Collections.Generic.EqualityComparer{TKey}"></see> for the type of the key.</param>
        /// <exception cref="System.ArgumentException">dictionarytable contains one or more duplicate values.</exception>
        /// <exception cref="System.ArgumentNullException">dictionarytable is null.</exception>
        public Dictionary(Generics.IDictionary<TKey, TValue> dictionary, Generics.IEqualityComparer<TKey> comparer) : base(dictionary, comparer) { }

        /// <summary>Initializes a new instance of the <see cref="Dictionary{TKey, TValue}"></see> class that is empty, has the specified initial capacity, and uses the specified <see cref="System.Collections.Generic.IEqualityComparer{TKey}"></see>.</summary>
        /// <param name="capacity">The initial number of elements that the <see cref="Dictionary{TKey, TValue}"></see> can contain.</param>
        /// <param name="comparer">The <see cref="System.Collections.Generic.IEqualityComparer{TKey}"></see> implementation to use when comparing values, or null to use the default <see cref="System.Collections.Generic.EqualityComparer{TKey}"></see> for the type of the key.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">capacity is less than 0.</exception>
        public Dictionary(int capacity, Generics.IEqualityComparer<TKey> comparer) : base(capacity, comparer) { }


        /// <summary>Initializes a new instance of the <see cref="Dictionary{TKey, TValue}"></see> class with serialized data.</summary>
        /// <param name="context">A <see cref="System.Runtime.Serialization.StreamingContext"></see> structure containing the source and destination of the serialized stream associated with the <see cref="Dictionary{TKey, TValue}"></see>.</param>
        /// <param name="info">A <see cref="System.Runtime.Serialization.SerializationInfo"></see> object containing the information required to serialize the <see cref="Dictionary{TKey, TValue}"></see>.</param>
        protected Dictionary(SerializationInfo info, StreamingContext context) : base(info, context) { }

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
        /// Determines whether the specified key contains key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// 	<c>true</c> if the specified key contains key; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(TKey key)
        {
            return this.ContainsKey(key);
        } // ContainsKey

        /// <summary>
        /// Returns a synchronized (thread safe) wrapper for the Hashtable.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <returns>
        /// A synchronized (thread safe) wrapper for the dictionarytable.
        /// </returns>2
        [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
        public static Dictionary<TKey, TValue> Synchronized(Dictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }
            return new SyncDictionary<TKey, TValue>(dictionary);
        }


        /// <summary>Implements the <see cref="System.Runtime.Serialization.ISerializable"></see> interface and returns the data needed to serialize the <see cref="Dictionary{TKey, TValue}"></see> instance.</summary>
        /// <param name="context">A <see cref="System.Runtime.Serialization.StreamingContext"></see> structure that contains the source and destination of the serialized stream associated with the <see cref="Dictionary{TKey, TValue}"></see> instance.</param>
        /// <param name="info">A <see cref="System.Runtime.Serialization.SerializationInfo"></see> object that contains the information required to serialize the <see cref="Dictionary{TKey, TValue}"></see> instance.</param>
        /// <exception cref="System.ArgumentNullException">info is null.</exception>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
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
                return false;
            } // get
        }


        #endregion

        #region Methods


        /// <summary>
        /// Copies the elements of the ICollection to an array of type KeyValuePair,
        /// starting at the specified array index.
        /// </summary>
        /// <param name="array">The one-dimensional array of type KeyValuePair that is
        /// the destination of the KeyValuePair elements copied from the ICollection.
        /// The array must have zero-based indexing.</param>
        /// <param name="index">The zero-based index in array at which copying begins.</param>
        public void CopyTo(Generics.KeyValuePair<TKey, TValue>[] array, int index)
        {
            // Calling CopyTo on a Dictionary
            // Copies all KeyValuePair objects in Dictionary object to objs[]
            ((Generics.IDictionary<TKey, TValue>)this).CopyTo(array, index);

        } // CopyTo
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
            return Clone();
        }

        #endregion

        #region ICloneable<Dictionary<TKey,TValue>> Members

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public Dictionary<TKey, TValue> Clone()
        {
            return new Dictionary<TKey, TValue>(this);
        }

        #endregion

        #region IXmlSerializable Members

        /// <summary>
        /// This property is reserved, apply the <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute"></see> to the class instead.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Xml.Schema.XmlSchema"></see> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)"></see> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)"></see> method.
        /// </returns>
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Generates an object from its XML representation.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader"></see> stream from which the object is deserialized.</param>
        public void ReadXml(XmlReader reader)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));

            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));



            bool wasEmpty = reader.IsEmptyElement;

            reader.Read();



            if (wasEmpty)

                return;



            while (reader.NodeType != XmlNodeType.EndElement)
            {

                reader.ReadStartElement("item");



                reader.ReadStartElement("key");

                TKey key = (TKey)keySerializer.Deserialize(reader);

                reader.ReadEndElement();



                reader.ReadStartElement("value");

                TValue value = (TValue)valueSerializer.Deserialize(reader);

                reader.ReadEndElement();



                Add(key, value);



                reader.ReadEndElement();

                reader.MoveToContent();

            }

            reader.ReadEndElement();
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter"></see> stream to which the object is serialized.</param>
        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));

            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));



            foreach (TKey key in Keys)
            {

                writer.WriteStartElement("item");



                writer.WriteStartElement("key");

                keySerializer.Serialize(writer, key);

                writer.WriteEndElement();



                writer.WriteStartElement("value");

                TValue value = this[key];

                valueSerializer.Serialize(writer, value);

                writer.WriteEndElement();



                writer.WriteEndElement();

            }
        }

        #endregion
    }
}
