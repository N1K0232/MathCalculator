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
        protected override void Draw(Control control)
        {
            if (control is not Form || control is not UserControl)
            {
                MessageBox.Show("Can't draw the shape", "Calculator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            control.Paint += new PaintEventHandler(Draw_Shape);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="pe"></param>
        private void Draw_Shape(object sender, PaintEventArgs pe)
        {
            Graphics graphics = pe.Graphics;
            SolidBrush brush = new(Color);
            PointF location = Location;
            RectangleF rectangle = new(location.X, location.Y, _side, _side);
            graphics.FillRectangle(brush, rectangle);
            brush.Dispose();
        }
    }
}