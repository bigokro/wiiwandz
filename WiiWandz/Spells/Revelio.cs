using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiiWandz.Strokes;
using WiiWandz.CloudBit;

namespace WiiWandz.Spells
{
	class Revelio : CloudBitSpell
	{
        public Revelio(double confidence)
            : base(confidence)
        {
            this.minPercentOfTotalBetweenStartAndEndPoints = 10;
            this.maxPercentOfTotalBetweenStartAndEndPoints = 30;
            this.acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.UpToTheRight);
            this.acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.Right);
        }

        public Revelio(String device, String authorization, int order, int duration)
            : base(device, authorization, order, duration)
		{
            List<StrokeDirection> directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.Up);
            directions.Add(StrokeDirection.Right);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.Down);
            directions.Add(StrokeDirection.DownToTheLeft);
            directions.Add(StrokeDirection.Left);
            directions.Add(StrokeDirection.DownToTheRight);
            this.strokesForSpell.Add(directions);

            directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.Up);
            directions.Add(StrokeDirection.Right);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.DownToTheLeft);
            directions.Add(StrokeDirection.DownToTheRight);
            this.strokesForSpell.Add(directions);

		}
	}
}
