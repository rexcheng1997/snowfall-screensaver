using System;
using System.Drawing;
using System.Windows.Forms;

namespace screensaver
{
    class Snowball
    {
        private readonly int MINRADIUS = 2;
        private readonly int MAXRADIUS = 6;
        private readonly int MAXVELOCITY = 2;
        private Brush magicBrush = new SolidBrush(Color.White);
        private PointF c; // center of the snowball
        private PointF v; // velocity of the snowball
        private PointF a; // acceleration of the snowball
        private int r; // radius of the snowball
        private int dir; // direction of the swing
        private float angle;
        private readonly int sid; // screen id

        public Snowball(int id)
        {
            int seed = DateTime.UtcNow.Millisecond;
            Random prf = new Random(seed);
            float x = Screen.AllScreens[id].Bounds.Width * (float)prf.NextDouble();
            float y = prf.Next(-2 * MAXRADIUS, -MAXRADIUS);
            c = new PointF(x, y);
            v = new PointF(0, 0);
            a = new PointF(0, 0);
            r = prf.Next(MINRADIUS, MAXRADIUS);
            dir = (prf.Next(-2, 2) > 0)? 1 : -1;
            angle = 0;
            sid = id;
        }

        public void Apply_Force(PointF force)
        {
            float ratio = r / MINRADIUS;
            a.X += ratio * force.X;
            a.Y += ratio * force.Y;
        }

        public void Update_Position()
        {
            if (v.Y <= (float) r / MAXRADIUS * MAXVELOCITY)
            {
                v.X += a.X;
                v.Y += a.Y;
            }
            c.X += v.X;
            c.Y += v.Y;
            angle += dir * (float) Math.PI / 90 * r / MAXRADIUS;
            a = new PointF(0, 0);
            c.X += 1.6F * (1 - c.Y / Screen.AllScreens[sid].Bounds.Height) / 2 * (float) Math.Sin(angle);
            // If the snowball is outside the window, regenerate its new position.
            if (Off_Screen())
            {
                Regenerate();
            }
            // If the snowball is out of the left or right boundary of the window, move it to the opposite side.
            if (c.X < 0 || c.X > Screen.AllScreens[sid].Bounds.Width)
            {
                c.X = Screen.AllScreens[sid].Bounds.Width - c.X;
            }
        }

        public void Draw(Graphics g)
        {
            g.FillEllipse(magicBrush, new RectangleF(new PointF(c.X - r, c.Y - r), new SizeF(2 * r, 2 * r)));
        }

        private bool Off_Screen()
        {
            return c.Y > Screen.AllScreens[sid].Bounds.Height + r;
        }

        private void Regenerate()
        {
            int seed = DateTime.UtcNow.Millisecond;
            Random prf = new Random(seed);
            float x = Screen.AllScreens[sid].Bounds.Width * (float)prf.NextDouble();
            float y = prf.Next(-2 * MAXRADIUS, -MAXRADIUS);
            c = new PointF(x, y);
            v = new PointF(0, 0);
            a = new PointF(0, 0);
            r = prf.Next(MINRADIUS, MAXRADIUS);
            dir = (prf.Next(-2, 2) > 0) ? 1 : -1;
            angle = 0;
        }
    }
}
