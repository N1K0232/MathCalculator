using System;
using System.Collections;
using System.Collections.Generic;

namespace MathCalculator.Core.Models.Common
{
    public abstract partial class Shape
    {
        public ShapeCollection Shapes { get; }

        public sealed class ShapeCollection : IList<Shape>, ICollection<Shape>, ICollection
        {
            private readonly List<Shape> _shapes;

            public ShapeCollection()
            {
                _shapes = new List<Shape>();
            }

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

            public int Count => _shapes.Count;

            public bool IsReadOnly => false;

            public bool IsSynchronized => false;

            public object SyncRoot => this;

            public void Add(Shape item)
            {
                bool contains = Contains(item);
                if (contains)
                {
                    return;
                }

                _shapes.Add(item);
            }
            public void Clear()
            {
                _shapes.Clear();
            }
            public bool Contains(Shape item)
            {
                return _shapes.Contains(item);
            }
            public void CopyTo(Shape[] array, int arrayIndex)
            {
                ICollection collection = this;
                collection.CopyTo(array, arrayIndex);
            }
            public IEnumerator<Shape> GetEnumerator()
            {
                IEnumerable<Shape> shapes = this;
                return shapes.GetEnumerator();
            }
            public int IndexOf(Shape item)
            {
                if (item is null)
                {
                    return -1; //the shape wasn't found
                }

                return _shapes.IndexOf(item);
            }
            public void Insert(int index, Shape item)
            {
                Shape oldValue = this[index];
                if (item == oldValue)
                {
                    return;
                }

                _shapes[index] = item;
            }
            public bool Remove(Shape item)
            {
                if (item is null)
                {
                    return false;
                }

                return _shapes.Remove(item);
            }
            public void RemoveAt(int index)
            {
                Shape item = this[index];
                Remove(item);
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