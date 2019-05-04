using System.Drawing;

namespace screensaver
{
    class Wind
    {
        private readonly float MAGNITUDEX = 0.2F;
        private readonly float MAGNITUDEY = 0.02F;
        private PointF windVector;
        private readonly int w, h;

        public Wind(int width, int height)
        {
            w = width;
            h = height;
        }

        public void Blow(PointF cursor)
        {
            float mx = 2 * (cursor.X - w / 2) / w;
            float my = 2 * (cursor.Y - h / 2) / h;
            windVector = new PointF(mx * MAGNITUDEX, my * MAGNITUDEY);
        }

        public PointF Get_Vector()
        {
            return windVector;
        }
    }
}
