using MathCalculator.Core.Models.Common;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MathCalculator.Core.Models
{
    public class Circle : Shape
    {
        private const double PI = Math.PI;

        private float _radius;

        public Circle(Form form) : base(form)
        {
        }
        private Circle(Circle circle) : base(circle)
        {
            Radius = circle.Radius;
        }

        public float Radius
        {
            get
            {
                return _radius;
            }
            set
            {
                if (value == Radius || value <= 0)
                {
                    return;
                }

                _radius = value;
                Update();
            }
        }
        public override float Perimeter
        {
            get
            {
                float radius = Radius;
                double result = radius * 2 * PI;
                return Convert.ToSingle(result);
            }
        }
        public override float Area
        {
            get
            {
                float radius = Radius;
                double result = radius * radius * PI;
                return Convert.ToSingle(result);
            }
        }

        protected override Shape CopyFrom(Shape from)
        {
            Circle circle = from as Circle;
            return new Circle(circle);
        }

        protected override void Draw(Graphics graphics)
        {
            Circle circle = this;

            if (circle.Radius == 0)
            {
                return;
            }

            Color borderColor = circle.BorderColor;
            Color backColor = circle.BackColor;
            Pen pen = new(borderColor);
            SolidBrush brush = new(backColor);
            int x = circle.X;
            int y = circle.Y;
            float radius = circle.Radius * 10;

            graphics.DrawEllipse(pen, x, y, radius, radius);
            graphics.FillEllipse(brush, x, y, radius, radius);
            brush.Dispose();
            pen.Dispose();
        }

        internal override bool CheckEquals(Shape other)
        {
            Circle right = other as Circle;
            return Radius == right.Radius
                && base.CheckEquals(right);
        }
    }
}