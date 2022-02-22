using System.Drawing;

namespace MathCalculator.Core.Models.Common
{
    public abstract partial class Shape
    {
        /// <summary>
        /// represents a null shape
        /// </summary>
        private sealed class EmptyShape : Shape
        {
            internal EmptyShape() : base(form: new())
            {

            }

            public override float Perimeter => 0F;

            public override float Area => 0F;

            protected override Shape CopyFrom(Shape from) => null;

            protected override void Draw(Graphics graphics)
            {
            }
        }
    }
}