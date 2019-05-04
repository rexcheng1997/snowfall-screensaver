using System.Drawing;

namespace screensaver
{
    class Gravity
    {
        private static readonly int GRAV = 5;
        private readonly PointF grvVector = new Point(0, GRAV);

        public Gravity() {}

        public PointF Get_Vector()
        {
            return grvVector;
        }
    }
}
