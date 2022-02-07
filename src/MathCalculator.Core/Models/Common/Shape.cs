namespace MathCalculator.Core.Models.Common
{
    public abstract partial class Shape
    {
        private string _name;
        private Shape _parent;

        protected Shape()
        {
            _name = "";
            _parent = null;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value == Name)
                {
                    return;
                }

                _name = value;
            }
        }
        public Shape Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                _parent = value;
            }
        }
        public abstract float Area { get; }
        public abstract float Perimeter { get; }

        protected void Update()
        {
            Calculate();
        }
        protected abstract void Calculate();
    }
}