using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace screensaver
{
    class Snowball
    {
        private readonly int MINRADIUS = 5;
        private readonly int MAXRADIUS = 10;
        private Pen magicPen = new Pen(Color.White);
        private PointF c; // center of the snowball
        private int r; // radius of the snowball
        private readonly int sid; // screen id

        public Snowball(int id)
        {
            Random seed = new Random();
            int x = seed.Next(MAXRADIUS, Screen.AllScreens[id].Bounds.Width - MAXRADIUS);
            c = new PointF(x, -MAXRADIUS);
            r = seed.Next(MINRADIUS, MAXRADIUS);
            sid = id;
        }

        public void Update_Position(PointF force)
        {
            c.X += force.X * r / MAXRADIUS;
            c.Y += force.Y * r / MAXRADIUS;
        }

        public void Draw(Graphics g)
        {
            // If the snowball is outside the window, regenerate its new position.
            if (Check_Bound())
            {
                Random seed = new Random();
                int x = seed.Next(MAXRADIUS, Screen.AllScreens[sid].Bounds.Width - MAXRADIUS);
                c = new Point(x, MAXRADIUS);
                r = seed.Next(MINRADIUS, MAXRADIUS);
            }
            g.DrawEllipse(magicPen, new RectangleF(c, new SizeF(r, r)));
        }

        private bool Check_Bound()
        {
            return c.Y + r > Screen.AllScreens[sid].Bounds.Height;
        }
    }
}
