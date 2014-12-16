using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiiWandz
{
    public class Position
    {
        public WiimoteLib.Point point;
        public DateTime time;

        public Position(WiimoteLib.Point point, DateTime time)
        {
            this.point = point;
            this.time = time;
        }

        public System.Drawing.Point SystemPoint()
        {
            return new System.Drawing.Point((1023-point.X)/4, (1023-point.Y)/4);
        }

    }
}
