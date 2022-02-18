using MathCalculator.Core.Models.Common;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MathCalculator.Core.Models
{
    public class Square : Shape
    {
        private float _side = 0;

        public Square(Form form) : base(form)
        {
        }
        private Square(Square from) : base(from)
        {
            Side = from.Side;
        }

        public float Side
        {
            get
            {
                return _side;
            }
            set
            {
                _side = value;
                Update();
            }
        }
        public override float Perimeter
        {
            get
            {
                float side = Side;
                return side * 4;
            }
        }
        public override float Area
        {
            get
            {
                float side = Side;
                return side * side;
            }
        }
        public float Diagonal
        {
            get
            {
                double side = Convert.ToDouble(Side);
                double result = 2 * Math.Sqrt(side);
                return Convert.ToSingle(result);
            }
        }

        protected override Shape CopyFrom(Shape from)
        {
            Square square = from as Square;
            return new Square(square);
        }
        protected override void Draw(Graphics graphics)
        {
            Square square = this;
            Color backColor = BackColor;
            SolidBrush brush = new(backColor);
            int x = square.X;
            int y = square.Y;
            float side = square.Side * 10;

            graphics.FillRectangle(brush, x, y, side, side);
            brush.Dispose();
        }
    }
}