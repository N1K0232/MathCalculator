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
            Color borderColor = square.BorderColor;
            Color backColor = square.BackColor;
            Pen border = new(borderColor);
            SolidBrush background = new(backColor);
            int x = square.X;
            int y = square.Y;
            float side = square.Side * 10;

            graphics.DrawRectangle(border, x, y, side, side);
            graphics.FillRectangle(background, x, y, side, side);
            background.Dispose();
            border.Dispose();
        }

        internal override bool CheckEquals(Shape other)
        {
            Square left = this;
            Square right = other as Square;
            return left.Side == right.Side
                && left.Diagonal == right.Diagonal
                && base.CheckEquals(right);
        }
    }
}