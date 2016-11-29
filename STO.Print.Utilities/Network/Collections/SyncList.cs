using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace STO.Print.Utilities.Collections
{
    /// <summary>
    /// Syncronized List
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    [Serializable]
    internal class SyncList<T> : List<T>
    {
        protected List<T> list;


        /// <summary>
        /// Initializes a new instance of the <see cref="T:SyncList"/> class.
        /// </summary>
        /// <param name="list">The list.</param>
        internal SyncList(List<T> list)
        {
            this.list = list;
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

        /// <summary>Adds an object to the end of the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.</summary>
        /// <param name="item">The object to be added to the end of the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>. The value can be null for reference types.</param>
        public new void Add(T item)
        {
            lock (this.list)
            {
                this.list.Add(item);
            }
        }

        /// <summary>Adds the elements of the specified collection to the end of the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.</summary>
        /// <param name="collection">The collection whose elements should be added to the end of the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>. The collection itself cannot be null, but it can contain elements that are null, if type TKey is a reference type.</param>
        /// <exception cref="System.ArgumentNullException">collection is null.</exception>
        public new void AddRange(IEnumerable<T> collection)
        {
            lock (this.list)
            {
                this.list.InsertRange(this.Count, collection);
            }
        }


        /// <summary>
        /// Returns a read-only <see cref="System.Collections.Generic.IList&lt;T&gt;"></see> wrapper for the current collection.
        /// </summary>
        /// <returns>
        /// A <see cref="System.Collections.ObjectModel.ReadOnlyCollection&lt;T&gt;"></see> that acts as a read-only wrapper around the current <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.
        /// </returns>
        public new ReadOnlyCollection<T> AsReadOnly()
        {
            lock (this.list)
            {
                return new ReadOnlyCollection<T>(this.list);
            }
        }

        /// <summary>Removes all elements from the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.</summary>
        public new void Clear()
        {
            lock (this.list)
            {
                this.list.Clear();
            }
        }


        /// <summary>Determines whether an element is in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.</summary>
        /// <returns>true if item is found in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>; otherwise, false.</returns>
        /// <param name="item">The object to locate in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>. The value can be null for reference types.</param>
        public new bool Contains(T item)
        {
            bool flag;

            lock (this.list)
            {
                flag = this.list.Contains(item);
            }

            return flag;
        } // Contains

        /// <summary>Converts the elements in the current <see cref="System.Collections.Generic.List&lt;T&gt;"></see> to another type, and returns a list containing the converted elements.</summary>
        /// <returns>A <see cref="System.Collections.Generic.List&lt;T&gt;"></see> of the target type containing the converted elements from the current <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.</returns>
        /// <param name="converter">A delegate that converts each element from one type to another type.</param>
        /// <exception cref="System.ArgumentNullException">converter is null.</exception>
        public new List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
        {
            List<TOutput> list;
            lock (this.list)
            {
                list = new List<TOutput>(this.list.ConvertAll(converter));
            }

            return list;

        }

        /// <summary>Copies the entire <see cref="System.Collections.Generic.List&lt;T&gt;"></see> to a compatible one-dimensional array, starting at the beginning of the target array.</summary>
        /// <param name="array">The one-dimensional <see cref="System.Array"></see> that is the destination of the elements copied from <see cref="System.Collections.Generic.List&lt;T&gt;"></see>. The <see cref="System.Array"></see> must have zero-based indexing.</param>
        /// <exception cref="System.ArgumentException">The number of elements in the source <see cref="System.Collections.Generic.List&lt;T&gt;"></see> is greater than the number of elements that the destination array can contain.</exception>
        /// <exception cref="System.ArgumentNullException">array is null.</exception>
        public new void CopyTo(T[] array)
        {
            lock (this.list)
            {
                this.list.CopyTo(array, 0);
            }
        }


        /// <summary>Copies the entire <see cref="System.Collections.Generic.List&lt;T&gt;"></see> to a compatible one-dimensional array, starting at the specified index of the target array.</summary>
        /// <param name="array">The one-dimensional <see cref="System.Array"></see> that is the destination of the elements copied from <see cref="System.Collections.Generic.List&lt;T&gt;"></see>. The <see cref="System.Array"></see> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <exception cref="System.ArgumentException">arrayIndex is equal to or greater than the length of array.-or-The number of elements in the source <see cref="System.Collections.Generic.List&lt;T&gt;"></see> is greater than the available space from arrayIndex to the end of the destination array.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
        /// <exception cref="System.ArgumentNullException">array is null.</exception>
        public new void CopyTo(T[] array, int arrayIndex)
        {
            lock (this.list)
            {
                this.list.CopyTo(array, arrayIndex);
            }
        }


        /// <summary>Copies a range of elements from the <see cref="System.Collections.Generic.List&lt;T&gt;"></see> to a compatible one-dimensional array, starting at the specified index of the target array.</summary>
        /// <param name="array">The one-dimensional <see cref="System.Array"></see> that is the destination of the elements copied from <see cref="System.Collections.Generic.List&lt;T&gt;"></see>. The <see cref="System.Array"></see> must have zero-based indexing.</param>
        /// <param name="count">The number of elements to copy.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <param name="index">The zero-based index in the source <see cref="System.Collections.Generic.List&lt;T&gt;"></see> at which copying begins.</param>
        /// <exception cref="System.ArgumentNullException">array is null. </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">index is less than 0.-or-arrayIndex is less than 0.-or-count is less than 0. </exception>
        /// <exception cref="System.ArgumentException">index is equal to or greater than the <see cref="P:System.Collections.Generic.List&lt;T&gt;.Count"></see> of the source <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.-or-arrayIndex is equal to or greater than the length of array.-or-The number of elements from index to the end of the source <see cref="System.Collections.Generic.List&lt;T&gt;"></see> is greater than the available space from arrayIndex to the end of the destination array. </exception>
        public new void CopyTo(int index, T[] array, int arrayIndex, int count)
        {
            lock (this.list)
            {
                this.list.CopyTo(index, array, arrayIndex, count);
            }
        }

        /// <summary>Determines whether the <see cref="System.Collections.Generic.List&lt;T&gt;"></see> contains elements that match the conditions defined by the specified predicate.</summary>
        /// <returns>true if the <see cref="System.Collections.Generic.List&lt;T&gt;"></see> contains one or more elements that match the conditions defined by the specified predicate; otherwise, false.</returns>
        /// <param name="match">The <see cref="System.Predicate&lt;T&gt;"></see> delegate that defines the conditions of the elements to search for.</param>
        /// <exception cref="System.ArgumentNullException">match is null.</exception>
        public new bool Exists(Predicate<T> match)
        {
            return (this.FindIndex(match) != -1);
        }


        /// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the first occurrence within the entire <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.</summary>
        /// <returns>The first element that matches the conditions defined by the specified predicate, if found; otherwise, the default value for type TKey.</returns>
        /// <param name="match">The <see cref="System.Predicate&lt;T&gt;"></see> delegate that defines the conditions of the element to search for.</param>
        /// <exception cref="System.ArgumentNullException">match is null.</exception>
        public new T Find(Predicate<T> match)
        {
            T temp;

            lock (this.list)
            {
                temp = this.list.Find(match);
            }

            return temp;
        }

        /// <summary>Retrieves the all the elements that match the conditions defined by the specified predicate.</summary>
        /// <returns>A <see cref="System.Collections.Generic.List&lt;T&gt;"></see> containing all the elements that match the conditions defined by the specified predicate, if found; otherwise, an empty <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.</returns>
        /// <param name="match">The <see cref="System.Predicate&lt;T&gt;"></see> delegate that defines the conditions of the elements to search for.</param>
        /// <exception cref="System.ArgumentNullException">match is null.</exception>
        public new List<T> FindAll(Predicate<T> match)
        {
            List<T> temp;

            lock (this.list)
            {
                temp = new List<T>(this.list.FindAll(match));
            }

            return temp;
        }

        /// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the first occurrence within the entire <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.</summary>
        /// <returns>The zero-based index of the first occurrence of an element that matches the conditions defined by match, if found; otherwise, –1.</returns>
        /// <param name="match">The <see cref="System.Predicate&lt;T&gt;"></see> delegate that defines the conditions of the element to search for.</param>
        /// <exception cref="System.ArgumentNullException">match is null.</exception>
        public new int FindIndex(Predicate<T> match)
        {
            int temp;

            lock (this.list)
            {
                temp = this.list.FindIndex(match);
            }

            return temp;
        }

        /// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the first occurrence within the range of elements in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see> that extends from the specified index to the last element.</summary>
        /// <returns>The zero-based index of the first occurrence of an element that matches the conditions defined by match, if found; otherwise, –1.</returns>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="match">The <see cref="System.Predicate&lt;T&gt;"></see> delegate that defines the conditions of the element to search for.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">startIndex is outside the range of valid indexes for the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.</exception>
        /// <exception cref="System.ArgumentNullException">match is null.</exception>
        public new int FindIndex(int startIndex, Predicate<T> match)
        {
            int temp;

            lock (this.list)
            {
                temp = this.list.FindIndex(startIndex, match);
            }

            return temp;

        }


        /// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the first occurrence within the range of elements in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see> that starts at the specified index and contains the specified number of elements.</summary>
        /// <returns>The zero-based index of the first occurrence of an element that matches the conditions defined by match, if found; otherwise, –1.</returns>
        /// <param name="count">The number of elements in the section to search.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="match">The <see cref="System.Predicate&lt;T&gt;"></see> delegate that defines the conditions of the element to search for.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">startIndex is outside the range of valid indexes for the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.-or-count is less than 0.-or-startIndex and count do not specify a valid section in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.</exception>
        /// <exception cref="System.ArgumentNullException">match is null.</exception>
        public new int FindIndex(int startIndex, int count, Predicate<T> match)
        {
            int temp;

            lock (this.list)
            {
                temp = this.list.FindIndex(startIndex, count, match);
            }

            return temp;

        }


        /// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the last occurrence within the entire <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.</summary>
        /// <returns>The last element that matches the conditions defined by the specified predicate, if found; otherwise, the default value for type TKey.</returns>
        /// <param name="match">The <see cref="System.Predicate&lt;T&gt;"></see> delegate that defines the conditions of the element to search for.</param>
        /// <exception cref="System.ArgumentNullException">match is null.</exception>
        public new T FindLast(Predicate<T> match)
        {
            T temp;

            lock (this.list)
            {
                temp = this.list.FindLast(match);
            }

            return temp;

        }

        /// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the last occurrence within the entire <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.</summary>
        /// <returns>The zero-based index of the last occurrence of an element that matches the conditions defined by match, if found; otherwise, –1.</returns>
        /// <param name="match">The <see cref="System.Predicate&lt;T&gt;"></see> delegate that defines the conditions of the element to search for.</param>
        /// <exception cref="System.ArgumentNullException">match is null.</exception>
        public new int FindLastIndex(Predicate<T> match)
        {
            int temp;

            lock (this.list)
            {
                temp = this.list.FindLastIndex(match);
            }

            return temp;
        }


        /// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the last occurrence within the range of elements in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see> that extends from the first element to the specified index.</summary>
        /// <returns>The zero-based index of the last occurrence of an element that matches the conditions defined by match, if found; otherwise, –1.</returns>
        /// <param name="startIndex">The zero-based starting index of the backward search.</param>
        /// <param name="match">The <see cref="System.Predicate&lt;T&gt;"></see> delegate that defines the conditions of the element to search for.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">startIndex is outside the range of valid indexes for the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.</exception>
        /// <exception cref="System.ArgumentNullException">match is null.</exception>
        public new int FindLastIndex(int startIndex, Predicate<T> match)
        {
            int temp;

            lock (this.list)
            {
                temp = this.list.FindLastIndex(startIndex, match);
            }

            return temp;
        }


        /// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the last occurrence within the range of elements in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see> that contains the specified number of elements and ends at the specified index.</summary>
        /// <returns>The zero-based index of the last occurrence of an element that matches the conditions defined by match, if found; otherwise, –1.</returns>
        /// <param name="count">The number of elements in the section to search.</param>
        /// <param name="startIndex">The zero-based starting index of the backward search.</param>
        /// <param name="match">The <see cref="System.Predicate&lt;T&gt;"></see> delegate that defines the conditions of the element to search for.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">startIndex is outside the range of valid indexes for the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.-or-count is less than 0.-or-startIndex and count do not specify a valid section in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.</exception>
        /// <exception cref="System.ArgumentNullException">match is null.</exception>
        public new int FindLastIndex(int startIndex, int count, Predicate<T> match)
        {
            int temp;

            lock (this.list)
            {
                temp = this.list.FindLastIndex(startIndex, count, match);
            }

            return temp;

        }



        /// <summary>Performs the specified action on each element of the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.</summary>
        /// <param name="action">The <see cref="System.Action&lt;T&gt;"></see> delegate to perform on each element of the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.</param>
        /// <exception cref="System.ArgumentNullException">action is null.</exception>
        public new void ForEach(Action<T> action)
        {

            lock (this.list)
            {
                this.list.ForEach(action);
            }
        }


        /// <summary>Returns an enumerator that iterates through the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.</summary>
        /// <returns>A <see cref="System.Collections.Generic.List&lt;T&gt;.Enumerator"></see> for the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.</returns>
        public new List<T>.Enumerator GetEnumerator()
        {
            Enumerator temp;
            lock (this.list)
            {
                temp = this.list.GetEnumerator();
            }

            return temp;
        }


        /// <summary>Creates a shallow copy of a range of elements in the source <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.</summary>
        /// <returns>A shallow copy of a range of elements in the source <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.</returns>
        /// <param name="count">The number of elements in the range.</param>
        /// <param name="index">The zero-based <see cref="System.Collections.Generic.List&lt;T&gt;"></see> index at which the range starts.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">index is less than 0.-or-count is less than 0.</exception>
        /// <exception cref="System.ArgumentException">index and count do not denote a valid range of elements in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.</exception>
        public new List<T> GetRange(int index, int count)
        {
            List<T> list;
            lock (this.list)
            {
                list = new List<T>(this.list.GetRange(index, count));
            }

            return list;
        }

        /// <summary>Searches for the specified object and returns the zero-based index of the first occurrence within the entire <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.</summary>
        /// <returns>The zero-based index of the first occurrence of item within the entire <see cref="System.Collections.Generic.List&lt;T&gt;"></see>, if found; otherwise, –1.</returns>
        /// <param name="item">The object to locate in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>. The value can be null for reference types.</param>
        public new int IndexOf(T item)
        {
            int temp;

            lock (this.list)
            {
                temp = this.list.IndexOf(item);
            }

            return temp;
        }


        /// <summary>Searches for the specified object and returns the zero-based index of the first occurrence within the range of elements in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see> that extends from the specified index to the last element.</summary>
        /// <returns>The zero-based index of the first occurrence of item within the range of elements in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see> that extends from index to the last element, if found; otherwise, –1.</returns>
        /// <param name="item">The object to locate in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>. The value can be null for reference types.</param>
        /// <param name="index">The zero-based starting index of the search.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">index is outside the range of valid indexes for the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.</exception>
        public new int IndexOf(T item, int index)
        {
            int temp;

            lock (this.list)
            {
                temp = this.list.IndexOf(item, index);
            }

            return temp;
        }


        /// <summary>Searches for the specified object and returns the zero-based index of the first occurrence within the range of elements in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see> that starts at the specified index and contains the specified number of elements.</summary>
        /// <returns>The zero-based index of the first occurrence of item within the range of elements in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see> that starts at index and contains count number of elements, if found; otherwise, –1.</returns>
        /// <param name="count">The number of elements in the section to search.</param>
        /// <param name="item">The object to locate in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>. The value can be null for reference types.</param>
        /// <param name="index">The zero-based starting index of the search.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">index is outside the range of valid indexes for the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.-or-count is less than 0.-or-index and count do not specify a valid section in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.</exception>
        public new int IndexOf(T item, int index, int count)
        {
            int temp;

            lock (this.list)
            {
                temp = this.list.IndexOf(item, index, count);
            }

            return temp;
        }


        /// <summary>Inserts an element into the <see cref="System.Collections.Generic.List&lt;T&gt;"></see> at the specified index.</summary>
        /// <param name="item">The object to insert. The value can be null for reference types.</param>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">index is less than 0.-or-index is greater than <see cref="P:System.Collections.Generic.List&lt;T&gt;.Count"></see>.</exception>
        public new void Insert(int index, T item)
        {
            lock (this.list)
            {
                this.list.Insert(index, item);
            }
        }


        /// <summary>Inserts the elements of a collection into the <see cref="System.Collections.Generic.List&lt;T&gt;"></see> at the specified index.</summary>
        /// <param name="collection">The collection whose elements should be inserted into the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>. The collection itself cannot be null, but it can contain elements that are null, if type TKey is a reference type.</param>
        /// <param name="index">The zero-based index at which the new elements should be inserted.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">index is less than 0.-or-index is greater than <see cref="P:System.Collections.Generic.List&lt;T&gt;.Count"></see>.</exception>
        /// <exception cref="System.ArgumentNullException">collection is null.</exception>
        public new void InsertRange(int index, IEnumerable<T> collection)
        {
            lock (this.list)
            {
                this.list.InsertRange(index, collection);
            }
        } // InsertRange



        /// <summary>Searches for the specified object and returns the zero-based index of the last occurrence within the entire <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.</summary>
        /// <returns>The zero-based index of the last occurrence of item within the entire the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>, if found; otherwise, –1.</returns>
        /// <param name="item">The object to locate in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>. The value can be null for reference types.</param>
        public new int LastIndexOf(T item)
        {
            int flag;
            lock (this.list)
            {
                flag = this.list.LastIndexOf(item);
            }

            return flag;
        }


        /// <summary>Searches for the specified object and returns the zero-based index of the last occurrence within the range of elements in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see> that extends from the first element to the specified index.</summary>
        /// <returns>The zero-based index of the last occurrence of item within the range of elements in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see> that extends from the first element to index, if found; otherwise, –1.</returns>
        /// <param name="item">The object to locate in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>. The value can be null for reference types.</param>
        /// <param name="index">The zero-based starting index of the backward search.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">index is outside the range of valid indexes for the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>. </exception>
        public new int LastIndexOf(T item, int index)
        {
            int flag;
            lock (this.list)
            {
                flag = this.list.LastIndexOf(item, index);
            }

            return flag;
        }


        /// <summary>Searches for the specified object and returns the zero-based index of the last occurrence within the range of elements in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see> that contains the specified number of elements and ends at the specified index.</summary>
        /// <returns>The zero-based index of the last occurrence of item within the range of elements in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see> that contains count number of elements and ends at index, if found; otherwise, –1.</returns>
        /// <param name="count">The number of elements in the section to search.</param>
        /// <param name="item">The object to locate in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>. The value can be null for reference types.</param>
        /// <param name="index">The zero-based starting index of the backward search.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">index is outside the range of valid indexes for the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.-or-count is less than 0.-or-index and count do not specify a valid section in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>. </exception>
        public new int LastIndexOf(T item, int index, int count)
        {
            int flag;
            lock (this.list)
            {
                flag = this.list.LastIndexOf(item, index, count);
            }

            return flag;
        }

        /// <summary>Removes the first occurrence of a specific object from the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.</summary>
        /// <returns>true if item is successfully removed; otherwise, false.  This method also returns false if item was not found in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.</returns>
        /// <param name="item">The object to remove from the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>. The value can be null for reference types.</param>
        public new bool Remove(T item)
        {
            bool flag;
            lock (this.list)
            {
                flag = this.list.Remove(item);
            }

            return flag;
        }

        /// <summary>Removes the all the elements that match the conditions defined by the specified predicate.</summary>
        /// <returns>The number of elements removed from the <see cref="System.Collections.Generic.List&lt;T&gt;"></see> .</returns>
        /// <param name="match">The <see cref="System.Predicate&lt;T&gt;"></see> delegate that defines the conditions of the elements to remove.</param>
        /// <exception cref="System.ArgumentNullException">match is null.</exception>
        public new int RemoveAll(Predicate<T> match)
        {
            int flag;
            lock (this.list)
            {
                flag = this.list.RemoveAll(match);
            }

            return flag;

        }


        /// <summary>Removes the element at the specified index of the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.</summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">index is less than 0.-or-index is equal to or greater than <see cref="P:System.Collections.Generic.List&lt;T&gt;.Count"></see>.</exception>
        public new void RemoveAt(int index)
        {
            lock (this.list)
            {
                this.list.RemoveAt(index);
            }
        }


        /// <summary>Removes a range of elements from the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.</summary>
        /// <param name="count">The number of elements to remove.</param>
        /// <param name="index">The zero-based starting index of the range of elements to remove.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">index is less than 0.-or-count is less than 0.</exception>
        /// <exception cref="System.ArgumentException">index and count do not denote a valid range of elements in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.</exception>
        public new void RemoveRange(int index, int count)
        {
            lock (this.list)
            {
                this.list.RemoveRange(index, count);
            }

        }


        /// <summary>Reverses the order of the elements in the entire <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.</summary>
        public new void Reverse()
        {
            lock (this.list)
            {
                this.list.Reverse();
            }
        }


        /// <summary>Reverses the order of the elements in the specified range.</summary>
        /// <param name="count">The number of elements in the range to reverse.</param>
        /// <param name="index">The zero-based starting index of the range to reverse.</param>
        /// <exception cref="System.ArgumentException">index and count do not denote a valid range of elements in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>. </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">index is less than 0.-or-count is less than 0. </exception>
        public new void Reverse(int index, int count)
        {
            lock (this.list)
            {
                this.list.Reverse(index, count);
            }
        }

        /// <summary>Sorts the elements in the entire <see cref="System.Collections.Generic.List&lt;T&gt;"></see> using the default comparer.</summary>
        /// <exception cref="System.InvalidOperationException">The default comparer <see cref="P:System.Collections.Generic.Comparer&lt;T&gt;.Default"></see> cannot find an implementation of the <see cref="System.IComparable&lt;T&gt;"></see> generic interface or the <see cref="System.IComparable"></see> interface for type TKey.</exception>
        public new void Sort()
        {
            lock (this.list)
            {
                this.list.Sort();
            }
        }


        /// <summary>Sorts the elements in the entire <see cref="System.Collections.Generic.List&lt;T&gt;"></see> using the specified comparer.</summary>
        /// <param name="comparer">The <see cref="System.Collections.Generic.IComparer&lt;T&gt;"></see> implementation to use when comparing elements, or null to use the default comparer <see cref="P:System.Collections.Generic.Comparer&lt;T&gt;.Default"></see>.</param>
        /// <exception cref="System.ArgumentException">The implementation of comparer caused an error during the sort. For example, comparer might not return 0 when comparing an item with itself.</exception>
        /// <exception cref="System.InvalidOperationException">comparer is null, and the default comparer <see cref="P:System.Collections.Generic.Comparer&lt;T&gt;.Default"></see> cannot find implementation of the <see cref="System.IComparable&lt;T&gt;"></see> generic interface or the <see cref="System.IComparable"></see> interface for type TKey.</exception>
        public new void Sort(IComparer<T> comparer)
        {
            lock (this.list)
            {
                this.list.Sort(comparer);
            }
        }


        /// <summary>Sorts the elements in the entire <see cref="System.Collections.Generic.List&lt;T&gt;"></see> using the specified <see cref="System.Comparison&lt;T&gt;"></see>.</summary>
        /// <param name="comparison">The <see cref="System.Comparison&lt;T&gt;"></see> to use when comparing elements.</param>
        /// <exception cref="System.ArgumentException">The implementation of comparison caused an error during the sort. For example, comparison might not return 0 when comparing an item with itself.</exception>
        /// <exception cref="System.ArgumentNullException">comparison is null.</exception>
        public new void Sort(Comparison<T> comparison)
        {
            lock (this.list)
            {
                this.list.Sort(comparison);
            }
        }


        /// <summary>Sorts the elements in a range of elements in <see cref="System.Collections.Generic.List&lt;T&gt;"></see> using the specified comparer.</summary>
        /// <param name="count">The length of the range to sort.</param>
        /// <param name="index">The zero-based starting index of the range to sort.</param>
        /// <param name="comparer">The <see cref="System.Collections.Generic.IComparer&lt;T&gt;"></see> implementation to use when comparing elements, or null to use the default comparer <see cref="P:System.Collections.Generic.Comparer&lt;T&gt;.Default"></see>.</param>
        /// <exception cref="System.ArgumentException">index and count do not specify a valid range in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.-or-The implementation of comparer caused an error during the sort. For example, comparer might not return 0 when comparing an item with itself.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">index is less than 0.-or-count is less than 0.</exception>
        /// <exception cref="System.InvalidOperationException">comparer is null, and the default comparer <see cref="P:System.Collections.Generic.Comparer&lt;T&gt;.Default"></see> cannot find implementation of the <see cref="System.IComparable&lt;T&gt;"></see> generic interface or the <see cref="System.IComparable"></see> interface for type TKey.</exception>
        public new void Sort(int index, int count, IComparer<T> comparer)
        {
            lock (this.list)
            {
                this.list.Sort(index, count, comparer);
            }
        }

        /// <summary>Copies the elements of the <see cref="System.Collections.Generic.List&lt;T&gt;"></see> to a new array.</summary>
        /// <returns>An array containing copies of the elements of the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.</returns>
        public new T[] ToArray()
        {
            T[] localArray1 = new T[this.Count];
            lock (this.list)
            {
                localArray1 = this.list.ToArray();
            }

            return localArray1;
        }



        /// <summary>Sets the capacity to the actual number of elements in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>, if that number is less than a threshold value.</summary>
        public new void TrimExcess()
        {
            lock (this.list)
            {
                this.list.TrimExcess();
            }
        }



        /// <summary>Determines whether every element in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see> matches the conditions defined by the specified predicate.</summary>
        /// <returns>true if every element in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see> matches the conditions defined by the specified predicate; otherwise, false. If the list has no elements, the return value is true.</returns>
        /// <param name="match">The <see cref="System.Predicate&lt;T&gt;"></see> delegate that defines the conditions to check against the elements.</param>
        /// <exception cref="System.ArgumentNullException">match is null.</exception>
        public new bool TrueForAll(Predicate<T> match)
        {
            bool flag;
            lock (this.list)
            {
                flag = this.list.TrueForAll(match);
            }
            return flag;
        }



        /// <summary>Gets or sets the total number of elements the internal data structure can hold without resizing.</summary>
        /// <returns>The number of elements that the <see cref="System.Collections.Generic.List&lt;T&gt;"></see> can contain before resizing is required.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException"><see cref="P:System.Collections.Generic.List&lt;T&gt;.Capacity"></see> is set to a value that is less than <see cref="P:System.Collections.Generic.List&lt;T&gt;.Count"></see>. </exception>
        public new int Capacity
        {
            get
            {
                int temp;
                lock (this.list)
                {
                    temp = this.list.Capacity;
                }
                return temp;
            }
            set
            {
                lock (this.list)
                {
                    this.list.Capacity = value;
                }

            }
        }

        /// <summary>Gets the number of elements actually contained in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.</summary>
        /// <returns>The number of elements actually contained in the <see cref="System.Collections.Generic.List&lt;T&gt;"></see>.</returns>
        public new int Count
        {
            get
            {
                lock (this.list)
                {
                    return this.list.Count;
                }
            }
        }

        /// <summary>Gets or sets the element at the specified index.</summary>
        /// <returns>The element at the specified index.</returns>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">index is less than 0.-or-index is equal to or greater than <see cref="P:System.Collections.Generic.List&lt;T&gt;.Count"></see>. </exception>
        public new T this[int index]
        {
            get
            {
                lock (this.list)
                {
                    return this.list[index];
                }
            }
            set
            {
                lock (this.list)
                {
                    this.list[index] = value;
                }
            }
        }

    } // class SyncList

}
