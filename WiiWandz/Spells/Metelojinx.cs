using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiiWandz.Strokes;
using WiiWandz.CloudBit;

namespace WiiWandz.Spells
{
	class Metelojinx : CloudBitSpell
	{
        public Metelojinx(double confidence)
            : base(confidence)
        {
            this.minPercentOfTotalBetweenStartAndEndPoints = 5;
            this.maxPercentOfTotalBetweenStartAndEndPoints = 60;
            this.acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.Right);
        }

        public Metelojinx(String device, String authorization, int order, int duration)
            : base(device, authorization, order, duration)
		{
            List<StrokeDirection> directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.Down);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.Right);
            directions.Add(StrokeDirection.UpToTheRight);
            directions.Add(StrokeDirection.Up);
            directions.Add(StrokeDirection.Down);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.Right);
            directions.Add(StrokeDirection.UpToTheRight);
            directions.Add(StrokeDirection.Up);
            this.strokesForSpell.Add(directions);

            directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.Down);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.UpToTheRight);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.UpToTheRight);
            this.strokesForSpell.Add(directions);
        }
	}
}
