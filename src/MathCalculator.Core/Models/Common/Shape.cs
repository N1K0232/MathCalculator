using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MathCalculator.Core.Models.Common
{
    /// <summary>
    /// represents the base class for the all the shapes
    /// this class cannot be instantiated
    /// </summary>
    public abstract partial class Shape : IDisposable
    {
        private readonly EventHandlerList Events = new();

        private static readonly object s_createEvent = new();
        private static readonly object s_destroyEvent = new();

        private static readonly Color s_backColor = Color.RoyalBlue;

        private Form _form;
        private int _x = 0;
        private int _y = 0;
        private Color _backColor = Color.Empty;

        /// <summary>
        /// creates a new instance of the <see cref="Shape"/>
        /// class
        /// </summary>
        /// <param name="form">the form where the Shape will be drawed</param>
        protected Shape(Form form)
        {
            Form = form;
            Shapes = new();
            OnCreate(EventArgs.Empty);
        }

        /// <summary>
        /// creates a new instance of the <see cref="Shape"/>
        /// class
        /// this constructor is called when the Shape is cloned
        /// </summary>
        /// <param name="from">the <see cref="Shape"/> that will be cloned</param>
        internal Shape(Shape from)
        {
            X = from.X;
            Y = from.Y;
            BackColor = from.BackColor;
            Form = from.Form;
            Location = from.Location;
            Shapes = from.Shapes;
        }

        /// <summary>
        /// performs some operations before the Garbage Collector
        /// destroys the object
        /// </summary>
        ~Shape()
        {
            Dispose(false);
        }

        /// <summary>
        /// when overridden in a derived class,
        /// it gets the perimeter of the shape
        /// </summary>
        public abstract float Perimeter { get; }

        /// <summary>
        /// when overridden in a derived class,
        /// it gets the area of the shape
        /// </summary>
        public abstract float Area { get; }

        /// <summary>
        /// 
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
        /// 
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
        /// the color of the background of the Shape
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
        /// 
        /// </summary>
        internal bool IsDestroyed { get; set; } = false;

        /// <summary>
        /// gets or sets the Location of the Shape in the form
        /// </summary>
        protected Point Location { get; set; }

        /// <summary>
        /// this event is raised when the Shape is created
        /// </summary>
        public event EventHandler Create
        {
            add
            {
                Events.AddHandler(s_createEvent, value);
            }
            remove
            {
                Events.RemoveHandler(s_destroyEvent, value);
            }
        }

        /// <summary>
        /// raises the <see cref="Create"/>
        /// event
        /// </summary>
        /// <param name="e">the event informations</param>
        protected void OnCreate(EventArgs e)
        {
            Shape shape = this;
            ((EventHandler)Events[s_createEvent])?.Invoke(shape, e);
            Shapes.Add(shape);
            Update();
        }

        /// <summary>
        /// raises the <see cref="Destroy"/>
        /// event
        /// </summary>
        /// <param name="e">the event informations</param>
        protected void OnDestroy(EventArgs e)
        {
            Shape shape = this;
            ((EventHandler)Events[s_destroyEvent])?.Invoke(shape, e);
            if (Shapes.Remove(shape) && !IsDestroyed)
            {
                IsDestroyed = true;
            }
        }

        /// <summary>
        /// when a property of the Shape changes its value
        /// this method redraws the control
        /// </summary>
        protected void Update()
        {
            if (Form is null)
            {
                return;
            }

            Form form = Form;
            form.Paint += new PaintEventHandler(DrawShape);
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

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// releases all the resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// releases all the resources
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (disposing && _form != null)
            {
                _form.Dispose();
            }
            OnDestroy(EventArgs.Empty);
        }

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

        /// <summary>
        /// clones the shape
        /// </summary>
        /// <returns></returns>
        public Shape Clone()
        {
            return CopyFrom(this);
        }

        public override bool Equals(object obj)
        {
            return CheckEquals(obj as Shape);
        }

        /// <summary>
        /// when overridden in a derived class 
        /// this method creates a copy of the current object
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        protected abstract Shape CopyFrom(Shape from);

        /// <summary>
        /// check if the two shapes are equals
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Shape left, Shape right)
        {
            return left.CheckEquals(right);
        }

        /// <summary>
        /// returns the opposite result of the == operator
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>    
        /// <returns></returns>
        public static bool operator !=(Shape left, Shape right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Checks if the right shape is equals to the left shape
        /// </summary>
        /// <param name="right">the right shape</param>
        /// <returns></returns>
        internal virtual bool CheckEquals(Shape right)
        {
            return right != null
                && X == right.X
                && Y == right.Y
                && Form == right.Form
                && Location == right.Location
                && Perimeter == right.Perimeter
                && Area == right.Area
                && Shapes == right.Shapes
                && BackColor == right.BackColor;
        }
    }
}