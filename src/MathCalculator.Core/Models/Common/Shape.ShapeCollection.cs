using System;
using System.Collections;
using System.Collections.Generic;

namespace MathCalculator.Core.Models.Common
{
    public partial class Shape
    {
        /// <summary>
        /// gets the list of shapes
        /// </summary>
        public ShapeCollection Shapes { get; }

        /// <summary>
        /// defines a class that stores all the shapes
        /// </summary>
        public sealed class ShapeCollection : IList<Shape>, ICollection<Shape>, ICollection
        {
            private readonly List<Shape> _shapes;

            /// <summary>
            /// creates a new instance of the <see cref="ShapeCollection"/>
            /// class
            /// </summary>
            internal ShapeCollection()
            {
                _shapes = new List<Shape>();
            }

            /// <summary>
            /// returns the <see cref="Shape"/> from the collection
            /// by giving the specified index
            /// </summary>
            /// <param name="index">the index of the shape</param>
            /// <returns>the <see cref="Shape"/> from the collection</returns>
            public Shape this[int index]
            {
                get
                {
                    Shape item = _shapes[index];
                    return item;
                }
                set
                {
                    Shape oldValue = this[index];
                    if (value == oldValue)
                    {
                        return;
                    }

                    _shapes[index] = value;
                }
            }

            /// <summary>
            /// returns the number of shapes in the collection
            /// </summary>
            public int Count => _shapes.Count;

            /// <summary>
            /// returns true if the collection can be read 
            /// </summary>
            public bool IsReadOnly => true;

            /// <summary>
            /// 
            /// </summary>
            public bool IsSynchronized => false;

            /// <summary>
            /// 
            /// </summary>
            public object SyncRoot => this;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="item"></param>
            public void Add(Shape item)
            {
                bool contains = Contains(item);
                if (contains)
                {
                    return;
                }

                if (item.IsDestroyed)
                {
                    throw new ShapeException("the shape was destroyed");
                }

                _shapes.Add(item);
            }

            /// <summary>
            /// Removes all the elements from the collection
            /// </summary>
            public void Clear()
            {
                _shapes.Clear();
            }

            /// <summary>
            /// determines whether an element is in the shapes collection
            /// </summary>
            /// <param name="item">The object to locate in the collection</param>
            /// <returns>true if item is found in the collection otherwise false</returns>
            public bool Contains(Shape item)
            {
                return _shapes.Contains(item);
            }

            /// <summary>
            /// copies the elements of an <see cref="ICollection"/> to an <see cref="Array"/>
            /// starting at a particular <see cref="Array"/> index
            /// </summary>
            /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied</param>
            /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
            /// <exception cref="ArgumentNullException">the array is null</exception>
            /// <exception cref="ArgumentOutOfRangeException">index is less than 0</exception>
            /// <exception cref="ArgumentException">array is multidimensional. -or- The number of elements in the source <see cref="ICollection"/>
            /// is greater than the available space from index to the end of the destination
            /// array. -or- The type of the source <see cref="ICollection"/> cannot be cast
            /// automatically to the type of the destination array.</exception>
            public void CopyTo(Shape[] array, int arrayIndex)
            {
                ICollection collection = this;
                collection.CopyTo(array, arrayIndex);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public IEnumerator<Shape> GetEnumerator()
            {
                IEnumerable<Shape> shapes = this;
                return shapes.GetEnumerator();
            }

            /// <summary>
            /// gets the index of the specified <paramref name="item"/>
            /// in the collection
            /// </summary>
            /// <param name="item">the item in the collection</param>
            /// <returns>the index of the specified <paramref name="item"/> in the collection</returns>
            public int IndexOf(Shape item)
            {
                if (item is null)
                {
                    return -1; //the shape wasn't found
                }

                return _shapes.IndexOf(item);
            }

            /// <summary>
            /// updates the item of the list at the specified index
            /// </summary>
            /// <param name="index">the index of the item</param>
            /// <param name="item">the new item</param>
            public void Insert(int index, Shape item)
            {
                this[index] = item;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="item"></param>
            /// <returns></returns>
            public bool Remove(Shape item)
            {
                if (item is null)
                {
                    return false;
                }

                return _shapes.Remove(item);
            }

            /// <summary>
            /// removes the item of the list with the specified index
            /// </summary>
            /// <param name="index"></param>
            public void RemoveAt(int index)
            {
                _shapes.RemoveAt(index);
            }

            void ICollection.CopyTo(Array array, int index)
            {
                Shape[] items = array as Shape[];
                _shapes.CopyTo(items, index);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return _shapes.GetEnumerator();
            }
        }
    }
}