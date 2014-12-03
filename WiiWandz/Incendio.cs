using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiiWandz
{
    class Incendio : SpellTrigger
    {
        private List<Position> positions;

        public Incendio(List<Position> positions)
        {
            this.positions = positions;
        }

        public void castSpell()
        {
            // todo
        }

        public Boolean casting()
        {
            Position lastPoint = positions.Last();

            return triggered() && (DateTime.Now.Subtract(lastPoint.time).Seconds < 25);
        }

        public Boolean triggered()
        {
            Position lastPoint = positions.Last();
            Position currentPoint = lastPoint;

            // At least half a second of upward motion
            int i = positions.Count();
            while (i > 0 && lastPoint.time.Subtract(currentPoint.time).Milliseconds < 500)
            {
                Position nextPoint = positions.ElementAt(i - 1);
                if (currentPoint.point.Y < nextPoint.point.Y)
                {
                    // Falling when it should be rising
                    return false;
                }

                currentPoint = nextPoint;
                i--;
            }

            // Look for the point where we switch directions
            Boolean switched = false;
            while (i > 0 && !switched)
            {
                Position nextPoint = positions.ElementAt(i - 1);
                if (currentPoint.point.Y < nextPoint.point.Y)
                {
                    switched = true;
                }

                currentPoint = nextPoint;
                i--;
            }
            Position switchPoint = currentPoint;

            // Verify that the angle was mostly vertical
            if (lastPoint.point.Y - switchPoint.point.Y < Math.Abs(lastPoint.point.X - switchPoint.point.X))
            {
                return false;
            }

            // Check that we were descending for at least 1 second before switching
            while (i > 0 && switchPoint.time.Subtract(currentPoint.time).Milliseconds < 1000)
            {
                Position nextPoint = positions.ElementAt(i - 1);
                if (currentPoint.point.Y > nextPoint.point.Y)
                {
                    // Rising when it should be falling
                    return false;
                }

                currentPoint = nextPoint;
                i--;
            }

            // Verify that the angle was mostly vertical
            if (currentPoint.point.Y - switchPoint.point.Y < Math.Abs(currentPoint.point.X - switchPoint.point.X))
            {
                return false;
            }

            return true;
        }
    }
}
