using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MathCalculator.Core.Models.Common
{
    /// <summary>
    /// 
    /// </summary>
    public abstract partial class Shape : IDisposable
    {
        private static readonly Color s_backColor = Color.RoyalBlue;

        private Form _form;
        private int _x = 0;
        private int _y = 0;
        private Color _backColor = Color.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        protected Shape(Form form)
        {
            Form = form;
            OnCreate(EventArgs.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        internal Shape(Shape from)
        {
            X = from.X;
            Y = from.Y;
            BackColor = from.BackColor;
            Form = from.Form;
            Location = from.Location;
            Shapes = from.Shapes;
        }

        ~Shape()
        {
            Dispose(false);
        }

        public abstract float Perimeter { get; }
        public abstract float Area { get; }
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

        protected Point Location { get; set; }

        public event EventHandler Create;
        public event EventHandler Destroy;

        protected void OnCreate(EventArgs e)
        {
            Shape shape = this;
            EventHandler handler = Create;
            handler?.Invoke(shape, e);
            Shapes.Add(shape);
            Update();
        }
        protected void OnDestroy(EventArgs e)
        {
            Shape shape = this;
            EventHandler handler = Destroy;
            handler?.Invoke(shape, e);
            Shapes.Remove(shape);
        }
        protected void Update()
        {
            if (Form is null)
            {
                return;
            }

            Form form = Form;
            form.Paint += new PaintEventHandler(DrawShape);
        }
        private void DrawShape(object sender, PaintEventArgs pe)
        {
            Graphics graphics = pe.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Draw(graphics);
        }
        protected abstract void Draw(Graphics graphics);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (disposing && _form != null)
            {
                _form.Dispose();
            }
            OnDestroy(EventArgs.Empty);
        }

        private void SetNewLocation()
        {
            int x = X;
            int y = Y;
            Location = new Point(x, y);
        }

        public Shape Clone()
        {
            return CopyFrom(this);
        }
        protected abstract Shape CopyFrom(Shape from);


        public static bool operator ==(Shape left, Shape right)
        {
            return left.CheckEquals(right);
        }
        public static bool operator !=(Shape left, Shape right)
        {
            return !(left == right);
        }

        internal virtual bool CheckEquals(Shape right)
        {
            Shape left = this;
            return left.X == right.X
                && left.Y == right.Y
                && left.Form == right.Form
                && left.Location == right.Location
                && left.Perimeter == right.Perimeter
                && left.Area == right.Area
                && left.Shapes == right.Shapes
                && left.BackColor == right.BackColor;
        }
    }
}