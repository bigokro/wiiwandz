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
            Position nextPoint = lastPoint;
            Position switchPoint = lastPoint;
            Position startPoint = lastPoint;

            // At least half a second of upward motion
            int i = positions.Count();
            int increment = 10;

            while (i > 0 && nextPoint.point.Y - currentPoint.point.Y >= 0)
            {
                currentPoint = nextPoint;
                nextPoint = positions.ElementAt(i - 1);
                i -= increment;
            }

            if (i == 0)
            {
//                Console.WriteLine("Returning false because there was no switch point");
                return false;
            }

            switchPoint = currentPoint;

            if (lastPoint.time.Subtract(switchPoint.time).Milliseconds < 500)
            {
//                Console.WriteLine("Returning false because time between switch and now less than half a second - "
//                    + switchPoint.point.ToString() + " to " + lastPoint.point.ToString() 
//                    + lastPoint.time.Subtract(switchPoint.time).Milliseconds + "ms");
                return false;
            }
            if (lastPoint.point.Y <= switchPoint.point.Y)
            {
//                Console.WriteLine("Returning false because end point below or at switch point");
                return false;
            }
            if (lastPoint.point.Y - switchPoint.point.Y < Math.Abs(lastPoint.point.X - switchPoint.point.X))
            {
//                Console.WriteLine("Returning false because angle was mostly horizontal - " + switchPoint.point.ToString() + " to " + lastPoint.point.ToString());
                return false;
            }

            // Find start point
            while (i > 0 && nextPoint.point.Y - currentPoint.point.Y >= 0)
            {
                currentPoint = nextPoint;
                nextPoint = positions.ElementAt(i - 1);
                i -= increment;
            }
            startPoint = currentPoint;

            if (switchPoint.time.Subtract(startPoint.time).Milliseconds < 1000)
            {
                Console.WriteLine("Returning false because time between start and switch less than a second");
                return false;
            }
            if (startPoint.point.Y <= switchPoint.point.Y)
            {
                Console.WriteLine("Returning false because start point below or at switch point");
                return false;
            }
            if (startPoint.point.Y - switchPoint.point.Y < Math.Abs(startPoint.point.X - switchPoint.point.X))
            {
                Console.WriteLine("Returning false because angle was mostly horizontal - " + startPoint.point.ToString() + " to " + switchPoint.point.ToString() + " to " + lastPoint.point.ToString());
                //                Console.WriteLine("Returning false because angle was mostly horizontal");
                return false;
            }

            Console.WriteLine("Returning true!!!!!");
            return true;
        }
    }
}
