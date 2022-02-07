using MathCalculator.Core.Models;
using System;
using System.Windows.Forms;

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
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Square square = new();
            square.Side = 12;
            square.Name = "Square";
            square.Draw(pe);

            Rectangle rectangle = new();
            rectangle.Width = 40;
            rectangle.Height = 30;
            rectangle.X = 400;
            rectangle.Y = 300;
            rectangle.Name = "Rectangle";
            rectangle.Draw(pe);
        }
    }
}