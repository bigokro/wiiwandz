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
            this.minPercentOfTotalBetweenStartAndEndPoints = 30;
            this.maxPercentOfTotalBetweenStartAndEndPoints = 60;
            this.acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.DownToTheRight);
        }

        public WingardiumLeviosa(String device, String authorization, int order, int duration)
            : base(device, authorization, order, duration)
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
