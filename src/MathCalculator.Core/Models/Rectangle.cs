using MathCalculator.Core.Models.Common;
using System;
using System.Drawing;
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
            Color backColor = BackColor;
            SolidBrush brush = new(backColor);
            int x = rectangle.X;
            int y = rectangle.Y;
            float width = rectangle.Width * 10;
            float height = rectangle.Height * 10;

            graphics.FillRectangle(brush, x, y, width, height);
            brush.Dispose();
        }
    }
}