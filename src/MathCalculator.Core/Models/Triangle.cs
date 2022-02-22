using MathCalculator.Core.Models.Common;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MathCalculator.Core.Models
{
    public class Triangle : Shape
    {
        private float _firstSide = 0;
        private float _secondSide = 0;
        private float _thirdSide = 0;
        private float _height = 0;

        public Triangle(Form form) : base(form)
        {
        }
        private Triangle(Triangle from) : base(from)
        {
            FirstSide = from.FirstSide;
            SecondSide = from.SecondSide;
            ThirdSide = from.ThirdSide;
            Height = from.Height;
        }

        public float FirstSide
        {
            get
            {
                return _firstSide;
            }
            set
            {
                if (value == FirstSide || value <= 0)
                {
                    return;
                }

                _firstSide = value;
                Update();
            }
        }
        public float SecondSide
        {
            get
            {
                return _secondSide;
            }
            set
            {
                if (value == SecondSide || value <= 0)
                {
                    return;
                }

                _secondSide = value;
                Update();
            }
        }
        public float ThirdSide
        {
            get
            {
                return _thirdSide;
            }
            set
            {
                if (value == ThirdSide || value <= 0)
                {
                    return;
                }

                _thirdSide = value;
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
                float firstSide = FirstSide;
                float secondSide = SecondSide;
                float thirdSide = ThirdSide;
                return firstSide + secondSide + thirdSide;
            }
        }
        public override float Area
        {
            get
            {
                double sp = Convert.ToDouble(Perimeter / 2);
                double firstSide = Convert.ToDouble(FirstSide);
                double secondSide = Convert.ToDouble(SecondSide);
                double thirdSide = Convert.ToDouble(ThirdSide);
                double result = Math.Sqrt(sp * (sp - firstSide) * (sp - secondSide) * (sp - thirdSide));
                return Convert.ToSingle(result);
            }
        }

        protected override Shape CopyFrom(Shape from)
        {
            Triangle triangle = from as Triangle;
            return new Triangle(triangle);
        }

        protected override void Draw(Graphics graphics)
        {
            Triangle triangle = this;

            if (triangle.FirstSide == 0 && triangle.SecondSide == 0 && triangle.ThirdSide == 0)
            {
                return;
            }

            Color borderColor = triangle.BorderColor;
            Color backColor = triangle.BackColor;
            Pen pen = new(borderColor, 2);
            SolidBrush brush = new(backColor);
            int x = triangle.X;
            int y = triangle.Y;
            float firstSide = triangle.FirstSide;
            float secondSide = triangle.SecondSide;
            float thirdSide = triangle.ThirdSide;
        }

        internal override bool CheckEquals(Shape other)
        {
            Triangle right = other as Triangle;
            return FirstSide == right.FirstSide
                && SecondSide == right.SecondSide
                && ThirdSide == right.ThirdSide
                && Height == right.Height
                && base.Equals(right);
        }
    }
}