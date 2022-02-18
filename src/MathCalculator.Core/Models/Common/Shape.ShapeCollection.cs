using System.Collections.Generic;

namespace MathCalculator.Core.Models.Common
{
    public abstract partial class Shape
    {
        public static ShapeCollection Shapes { get; }

        public sealed class ShapeCollection
        {
            private readonly List<Shape> _shapes;

            internal ShapeCollection()
            {
                _shapes = new List<Shape>();
            }

            public Shape this[int index]
            {
                get
                {
                    return _shapes[index];
                }
                set
                {
                    Shape old = _shapes[index];
                    if (value == old)
                    {
                        return;
                    }

                    _shapes[index] = value;
                }
            }

            public void Add(Shape shape)
            {
                _shapes.Add(shape);
            }

            public bool Remove(int index)
            {
                Shape shape = this[index];
                if (shape is null)
                {
                    return false;
                }

                return _shapes.Remove(shape);
            }

            public int IndexOf(Shape shape)
            {
                if (shape is null)
                {
                    return -1;
                }
                else
                {
                    return _shapes.IndexOf(shape);
                }
            }
        }
    }
}