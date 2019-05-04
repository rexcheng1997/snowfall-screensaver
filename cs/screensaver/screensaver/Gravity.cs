using System.Drawing;

namespace screensaver
{
    class Gravity
    {
        private static readonly float GRAV = 0.4F;
        private readonly PointF grvVector = new PointF(0, GRAV);

        public Gravity() {}

        public PointF Get_Vector()
        {
            return grvVector;
        }
    }
}
