using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace MathCalculator.Core.Models.Common
{
    /// <summary>
    /// represents the base class for the all the shapes
    /// this class cannot be instantiated
    /// </summary>
    public abstract partial class Shape : IDisposable, ICloneable
    {
        public static readonly Shape Null = null;
        public static readonly Shape Empty = new EmptyShape();

        private readonly EventHandlerList Events = new();

        private static readonly object s_createEvent = new();
        private static readonly object s_destroyEvent = new();
        private static readonly object s_clickEvent = new();

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
        /// 
        /// </summary>
        public event EventHandler Click
        {
            add
            {
                Events.AddHandler(s_clickEvent, value);
            }
            remove
            {
                Events.RemoveHandler(s_clickEvent, value);
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
            EventHandler handler = (EventHandler)Events[s_createEvent];
            handler?.Invoke(shape, e);
            Shapes.Add(shape);
            Update();
        }

        protected void OnClick(EventArgs e)
        {
            EventHandler handler = (EventHandler)Events[s_clickEvent];
            handler?.Invoke(this, e);
            ShowInformations();
        }

        /// <summary>
        /// shows the informations of the shape
        /// </summary>
        private void ShowInformations()
        {
            string message = ToString();
            string caption = "Shape informations";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            MessageBoxIcon icon = MessageBoxIcon.Information;
            MessageBox.Show(message, caption, buttons, icon);
        }

        /// <summary>
        /// raises the <see cref="Destroy"/>
        /// event
        /// </summary>
        /// <param name="e">the event informations</param>
        protected void OnDestroy(EventArgs e)
        {
            Shape shape = this;
            EventHandler handler = (EventHandler)Events[s_destroyEvent];
            handler?.Invoke(shape, e);
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

        public override string ToString()
        {
            return PrintMembers();
        }

        /// <summary>
        /// creates a string with the informations of the shape
        /// </summary>
        /// <returns>a string with the informations of the shape</returns>
        internal virtual string PrintMembers()
        {
            StringBuilder builder = new();
            builder.AppendLine($"Perimeter = {Perimeter}");
            builder.AppendLine($"Area = {Area}");
            return builder.ToString();
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        object ICloneable.Clone() => CopyFrom(this);

        public override bool Equals(object obj)
        {
            Shape shape = obj as Shape;

            return obj != null
                && base.Equals(shape)
                && CheckEquals(shape);
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
                && IsDestroyed == other.IsDestroyed
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