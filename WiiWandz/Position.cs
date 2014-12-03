using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiiWandz
{
    class Position
    {
        public WiimoteLib.PointF point;
        public DateTime time;

        public Position(WiimoteLib.PointF point, DateTime time)
        {
            this.point = point;
            this.time = time;
        }

    }
}
