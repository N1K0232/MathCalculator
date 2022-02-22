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
            Color borderColor = circle.BorderColor;
            Color backColor = circle.BackColor;
            Pen border = new(borderColor);
            SolidBrush background = new(backColor);
            int x = circle.X;
            int y = circle.Y;
            float radius = circle.Radius * 10;

            graphics.DrawEllipse(border, x, y, radius, radius);
            graphics.FillEllipse(background, x, y, radius, radius);
            background.Dispose();
            border.Dispose();
        }

        internal override bool CheckEquals(Shape other)
        {
            Circle left = this;
            Circle right = other as Circle;
            return left.Radius == right.Radius
                && base.CheckEquals(right);
        }
    }
}