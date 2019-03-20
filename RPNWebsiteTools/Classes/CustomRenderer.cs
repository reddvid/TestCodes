using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPNWebsiteTools.Classes
{
    class CustomRenderer : ToolStripProfessionalRenderer
    {
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs myMenu)
        {
            if (!myMenu.Item.Selected)
                base.OnRenderMenuItemBackground(myMenu);
            else
            {
                if (myMenu.Item.Enabled)
                {
                    var menuRectangle = new Rectangle(Point.Empty, myMenu.Item.Size);
                    myMenu.Graphics.FillRectangle(Brushes.LightSkyBlue, menuRectangle);
                }
                else
                {
                    Rectangle menuRectangle = new Rectangle(Point.Empty, myMenu.Item.Size);
                    myMenu.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(20, 128, 128, 128)), menuRectangle);
                }

            }
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var r = new Rectangle(e.ImageRectangle.Location, e.ImageRectangle.Size);
            r.Inflate(1, 1);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(20, 128, 128, 128)), r);
            e.Graphics.DrawLines(Pens.Gray, new Point[]
            {
                    new Point(r.Left + 4, 10), 
                    new Point(r.Left - 2 + r.Width / 2,  r.Height / 2 + 4),
                    new Point(r.Right - 4, r.Top + 4)
            });
        }
    }
}
