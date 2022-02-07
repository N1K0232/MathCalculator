using System.Drawing;
using System.Windows.Forms;

namespace MathCalculator.Core.Models
{
    public partial class Rectangle
    {
        public override void Draw(PaintEventArgs pe)
        {
            Graphics graphics = pe.Graphics;
            SolidBrush brush = new(Color);
            float width = Width * 10;
            float height = Height * 10;
            RectangleF rectangle = new(X, Y, width, height);
            graphics.FillRectangle(brush, rectangle);
            brush.Dispose();
        }
    }
}