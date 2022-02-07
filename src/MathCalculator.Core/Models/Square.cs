using MathCalculator.Core.Models.Common;

namespace MathCalculator.Core.Models
{
    public partial class Square : Shape
    {
        private float _side;
        private float _area;
        private float _perimeter;

        public Square()
        {
            _side = 0;
            _area = 0;
            _perimeter = 0;
        }

        public float Side
        {
            get
            {
                return _side;
            }
            set
            {
                if (value == Side)
                {
                    return;
                }

                _side = value;
                Update();
            }
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

        protected override void Calculate()
        {
            _area = _side * _side;
            _perimeter = _side * 4;
        }
    }
}