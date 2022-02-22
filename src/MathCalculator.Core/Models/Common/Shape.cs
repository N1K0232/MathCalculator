using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MathCalculator.Core.Models.Common
{
    /// <summary>
    /// represents the base class for the all the shapes
    /// this class cannot be instantiated
    /// </summary>
    public abstract partial class Shape : IDisposable, ICloneable
    {
        private readonly EventHandlerList Events = new();

        private static readonly object s_createEvent = new();
        private static readonly object s_destroyEvent = new();

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
            BorderColor = from.BorderColor;
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
        internal bool IsDestroyed { get; set; } = false;

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
        /// clones the shape
        /// </summary>
        /// <returns></returns>
        public Shape Clone()
        {
            ICloneable cloneable = this;
            object clone = cloneable.Clone();
            return clone as Shape;
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        object ICloneable.Clone() => CopyFrom(this);

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
        internal virtual bool CheckEquals(Shape other)
        {
            return other != null
                && X == other.X
                && Y == other.Y
                && Form == other.Form
                && Location == other.Location
                && Perimeter == other.Perimeter
                && Area == other.Area
                && Shapes == other.Shapes
                && BackColor == other.BackColor
                && BorderColor == other.BorderColor;
        }
    }
}