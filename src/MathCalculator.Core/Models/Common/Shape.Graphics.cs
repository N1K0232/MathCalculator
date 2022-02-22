using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MathCalculator.Core.Models.Common
{
    public partial class Shape
    {
        private static readonly Color s_backColor = Color.RoyalBlue;
        private static readonly Color s_borderColor = Color.Yellow;

        private Form _form;
        private Color _backColor = Color.Empty;
        private Color _borderColor = Color.Empty;

        private int _x = 0;
        private int _y = 0;

        /// <summary>
        /// gets or sets the X coordinates of the shape
        /// </summary>
        public int X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
                SetNewLocation();
                Update();
            }
        }

        /// <summary>
        /// gets or sets the Y coordinates of the shape
        /// </summary>
        public int Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
                SetNewLocation();
                Update();
            }
        }

        /// <summary>
        /// gets or sets the color of the background of the Shape
        /// when will be draw in the form
        /// default value is <see cref="Color.RoyalBlue"/>
        /// </summary>
        public Color BackColor
        {
            get
            {
                Color c = _backColor;

                if (c.IsEmpty)
                {
                    c = s_backColor;
                }

                return c;
            }
            set
            {
                Color c = value;

                if (c == BackColor)
                {
                    return;
                }

                if (c.IsEmpty)
                {
                    c = s_backColor;
                }

                _backColor = c;
                Update();
            }
        }

        /// <summary>
        /// gets or sets the color of the border of the Shape
        /// when will be draw in the form
        /// default value is <see cref="Color.Yellow"/>
        /// </summary>
        public Color BorderColor
        {
            get
            {
                Color c = _borderColor;

                if (c.IsEmpty)
                {
                    c = s_borderColor;
                }

                return c;
            }
            set
            {
                Color c = value;

                if (c == BorderColor)
                {
                    return;
                }

                if (c.IsEmpty)
                {
                    c = s_borderColor;
                }

                _borderColor = c;
                Update();
            }
        }

        /// <summary>
        /// gets or sets the Location of the Shape in the form
        /// </summary>
        protected Point Location { get; set; }

        /// <summary>
        /// gets or sets the Form where the control will be draw
        /// </summary>
        internal Form Form
        {
            get
            {
                return _form;
            }
            set
            {
                _form = value;
            }
        }

        /// <summary>
        /// Raises the <see cref="Control.Paint"/>
        /// event
        /// </summary>
        /// <param name="sender">the object that called the event</param>
        /// <param name="pe">the event informations</param>
        private void DrawShape(object sender, PaintEventArgs pe)
        {
            Graphics graphics = pe.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Draw(graphics);
        }

        /// <summary>
        /// when overridden in a derived class, this method draws the shape
        /// </summary>
        /// <param name="graphics">the graphics of the form</param>
        protected abstract void Draw(Graphics graphics);

        /// <summary>
        /// when the X or the Y point changes this method sets the new location
        /// for the shape
        /// </summary>
        private void SetNewLocation()
        {
            int x = X;
            int y = Y;
            Location = new Point(x, y);
        }
    }
}