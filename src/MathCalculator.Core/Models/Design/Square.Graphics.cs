using MathCalculator.Core.Models.Common;
using System.Drawing;
using System.Windows.Forms;

namespace MathCalculator.Core.Models
{
    public partial class Square : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        public override void Draw(PaintEventArgs pe)
        {
            Graphics graphics = pe.Graphics;
            SolidBrush brush = new(Color);
            float side = Side * 10;
            RectangleF rectangle = new(X, Y, side, side);
            graphics.FillRectangle(brush, rectangle);
            brush.Dispose();
        }
    }
}