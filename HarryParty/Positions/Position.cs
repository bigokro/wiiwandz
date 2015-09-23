using System;
using System.Drawing;

namespace WiiWandz.Positions
{
    public class Position
    {
        public WiimoteLib.Point wiiPoint;
        public Point point;
        public DateTime time;

        public Position(WiimoteLib.Point point, DateTime time)
        {
            wiiPoint = point;
            this.point = new Point((1023 - point.X) / 4, (760 - point.Y) / 4);
            this.time = time;
        }

        public Position(Point point, DateTime time)
        {
            //this.point = point;
            this.point = new Point((1023 - point.X), (760 - point.Y));
            this.time = time;
        }

        public Point SystemPoint()
        {
            return point;
        }
    }
}
