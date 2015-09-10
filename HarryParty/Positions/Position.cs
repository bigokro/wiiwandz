using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiiWandz.Positions
{
    public class Position
    {
        public WiimoteLib.Point wiiPoint;
        public System.Drawing.Point point;
        public DateTime time;

        public Position(WiimoteLib.Point point, DateTime time)
        {
            this.wiiPoint = point;
            this.point = new System.Drawing.Point((1023 - point.X) / 4, (760 - point.Y) / 4);
            this.time = time;
        }

        public Position(System.Drawing.Point point, DateTime time)
        {
            this.point = point;
            this.time = time;
        }

        public System.Drawing.Point SystemPoint()
        {
            return point;
        }

    }
}
