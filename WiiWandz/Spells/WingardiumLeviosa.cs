using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiiWandz.Strokes;
using WiiWandz.CloudBit;

namespace WiiWandz.Spells
{
    class WingardiumLeviosa : CloudBitSpell
    {
        public WingardiumLeviosa(double confidence) : base(confidence) 
        {
            this.minPercentOfTotalBetweenStartAndEndPoints = 50;
            this.maxPercentOfTotalBetweenStartAndEndPoints = 100;
            this.acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.DownToTheRight);
        }

        public WingardiumLeviosa(String device, String authorization, int voltage, int duration)
            : base(device, authorization, voltage, duration)
        {
            List<StrokeDirection> directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.Down);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.Right);
            directions.Add(StrokeDirection.UpToTheRight);
            directions.Add(StrokeDirection.Up);
            directions.Add(StrokeDirection.Down);
            this.strokesForSpell.Add(directions);

            directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.Down);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.UpToTheRight);
            directions.Add(StrokeDirection.Down);
            this.strokesForSpell.Add(directions);

        }
    }
}
