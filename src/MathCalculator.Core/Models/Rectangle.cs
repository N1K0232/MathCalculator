using MathCalculator.Core.Models.Common;
using System;

namespace MathCalculator.Core.Models
{
    public partial class Rectangle : Shape
    {
        private float _width;
        private float _height;
        private float _area;
        private float _perimeter;
        private float _diagonal;

        public Rectangle()
        {

        }

        public override float Area
        {
            get
            {
                return _area;
            }
        }
        public override float Perimeter
        {
            get
            {
                return _perimeter;
            }
        }
        public float Diagonal
        {
            get
            {
                return _diagonal;
            }
        }

        public float Width
        {
            get
            {
                return _width;
            }
            set
            {
                if (value == Width || value < 0)
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
                if (value == Height || value < 0)
                {
                    return;
                }

                _height = value;
                Update();
            }
        }

        protected override void Calculate()
        {
            _area = _width * _height;
            _perimeter = (_width + _height) * 2;
            _diagonal = CalculateDiagonal();
        }

        private float CalculateDiagonal()
        {
            double width = Convert.ToDouble(_width * _width);
            double height = Convert.ToDouble(_height * _height);
            return Convert.ToSingle(Math.Sqrt(width + height));
        }
    }
}