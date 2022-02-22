using MathCalculator.Core.Models.Common;
using System;
using System.Drawing;
using System.Text;
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
                if (value == Side || value <= 0)
                {
                    return;
                }

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

            if (square.Side == 0)
            {
                return;
            }

            Color borderColor = square.BorderColor;
            Color backColor = square.BackColor;
            Pen pen = new(borderColor, 2);
            SolidBrush brush = new(backColor);
            int x = square.X;
            int y = square.Y;
            float side = square.Side * 10;

            graphics.DrawRectangle(pen, x, y, side, side);
            graphics.FillRectangle(brush, x, y, side, side);
            pen.Dispose();
            brush.Dispose();
        }

        internal override string PrintMembers()
        {
            StringBuilder builder = new();
            builder.AppendLine($"Side = {Side}");
            builder.AppendLine($"Diagonal = {Diagonal}");
            return builder.ToString() + base.PrintMembers();
        }
        internal override bool CheckEquals(Shape other)
        {
            Square right = other as Square;
            return Side == right.Side
                && Diagonal == right.Diagonal
                && base.CheckEquals(right);
        }
    }
}