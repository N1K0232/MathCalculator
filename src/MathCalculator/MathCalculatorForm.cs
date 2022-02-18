using MathCalculator.Core.Models;
using System;
using System.Windows.Forms;

using Color = System.Drawing.Color;

namespace MathCalculator
{
    public partial class MathCalculatorForm : Form
    {
        public MathCalculatorForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Square square = new(this);
            square.X = 200;
            square.Y = 200;
            square.Side = 12;

            Rectangle rectangle = new(this);
            rectangle.X = 100;
            rectangle.Y = 100;
            rectangle.Width = 4;
            rectangle.Height = 3;
            rectangle.BackColor = Color.HotPink;

            Circle circle = new(this);
            circle.X = 300;
            circle.Y = 100;
            circle.Radius = 10;
            circle.BackColor = Color.Gold;
        }
    }
}