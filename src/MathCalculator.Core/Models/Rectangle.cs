using MathCalculator.Core.Models.Common;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MathCalculator.Core.Models
{
    public class Rectangle : Shape
    {
        private float _width;
        private float _height;

        public Rectangle(Form form) : base(form)
        {
        }
        private Rectangle(Rectangle from) : base(from)
        {
            Width = from.Width;
            Height = from.Height;
        }

        public float Width
        {
            get
            {
                return _width;
            }
            set
            {
                if (value == Width || value <= 0)
                {
                    return;
                }

                _width = value;
                Update();
            }
        }
        public float Height
        {
            get
            {
                return _height;
            }
            set
            {
                if (value == Height || value <= 0)
                {
                    return;
                }

                _height = value;
                Update();
            }
        }
        public override float Perimeter
        {
            get
            {
                float width = Width;
                float height = Height;
                return (width * 2) + (height * 2);
            }
        }
        public override float Area
        {
            get
            {
                float width = Width;
                float height = Height;
                return width * height;
            }
        }
        public float Diagonal
        {
            get
            {
                double width = Convert.ToDouble(Width);
                double height = Convert.ToDouble(Height);
                double result = Math.Sqrt((width * width) + (height * height));
                return Convert.ToSingle(result);
            }
        }

        protected override Shape CopyFrom(Shape from)
        {
            Rectangle rectangle = from as Rectangle;
            return new Rectangle(rectangle);
        }
        protected override void Draw(Graphics graphics)
        {
            Rectangle rectangle = this;

            if (rectangle.Width == 0 && rectangle.Height == 0)
            {
                return;
            }

            Color borderColor = rectangle.BorderColor;
            Color backColor = rectangle.BackColor;
            Pen pen = new(borderColor);
            SolidBrush brush = new(backColor);
            int x = rectangle.X;
            int y = rectangle.Y;
            float width = rectangle.Width * 10;
            float height = rectangle.Height * 10;

            graphics.DrawRectangle(pen, x, y, width, height);
            graphics.FillRectangle(brush, x, y, width, height);
            brush.Dispose();
            pen.Dispose();
        }

        internal override string PrintMembers()
        {
            StringBuilder builder = new();
            builder.AppendLine($"Width = {Width}");
            builder.AppendLine($"Height = {Height}");
            builder.AppendLine($"Diagonal = {Diagonal}");
            return builder.ToString() + base.PrintMembers();
        }
        internal override bool CheckEquals(Shape other)
        {
            Rectangle right = other as Rectangle;

            return Diagonal == right.Diagonal
                && Width == right.Width
                && Height == right.Height
                && base.CheckEquals(right);
        }
    }
}