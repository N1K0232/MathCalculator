using System.Drawing;
using System.Windows.Forms;

namespace MathCalculator.Core.Models.Common
{
    public abstract partial class Shape
    {
        private PointF _location = new(250, 100);
        private Color _color = Color.RoyalBlue;
        private Control _control;

        public PointF Location
        {
            get
            {
                return _location;
            }
            set
            {
                if (value == Location)
                {
                    return;
                }

                _location = value;
                Invalidate();
            }
        }
        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                if (value == Color)
                {
                    return;
                }

                _color = value;
                Invalidate();
            }
        }
        public Control Control
        {
            get
            {
                return _control;
            }
            set
            {
                if (value == Control)
                {
                    return;
                }

                _control = value;
                Invalidate();
            }
        }

        protected void Invalidate()
        {
            Control control = Control;

            if (control is not null)
            {
                Draw(control);
            }
        }
        protected abstract void Draw(Control control);
    }
}