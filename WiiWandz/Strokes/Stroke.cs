using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiiWandz.Positions;

namespace WiiWandz.Strokes
{
    public class Stroke
    {
        public StrokeDirection direction;
        public Position start;
        public Position end;

        public Stroke(StrokeDirection direction, Position start, Position end)
        {
            this.direction = direction;
            this.start = start;
            this.end = end;
        }

    }
}
