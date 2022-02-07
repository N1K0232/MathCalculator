using System.Drawing;
using System.Windows.Forms;

namespace MathCalculator.Core.Models.Common
{
    public abstract partial class Shape
    {
        private Color _color = Color.RoyalBlue;
        private float _x = 250;
        private float _y = 100;

        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                if (value == Color || value == Color.Empty || value == Color.Transparent)
                {
                    return;
                }

                _color = value;
            }
        }

        public float X
        {
            get
            {
                return _x;
            }
            set
            {
                if (value == X || value < 0)
                {
                    return;
                }

                _x = value;
            }
        }
        public float Y
        {
            get
            {
                return _y;
            }
            set
            {
                if (value == Y || value < 0)
                {
                    return;
                }

                _y = value;
            }
        }

        public abstract void Draw(PaintEventArgs pe);
    }
}