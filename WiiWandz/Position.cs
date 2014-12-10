using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiiWandz
{
    class Position
    {
        public WiimoteLib.Point point;
        public DateTime time;

        public Position(WiimoteLib.Point point, DateTime time)
        {
            this.point = point;
            this.time = time;
        }

    }
}
