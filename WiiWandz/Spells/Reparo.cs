using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiiWandz.Strokes;
using WiiWandz.CloudBit;

namespace WiiWandz.Spells
{
	class Reparo : CloudBitSpell
	{
        public Reparo(double confidence)
            : base(confidence)
        {
            this.minPercentOfTotalBetweenStartAndEndPoints = 5;
            this.maxPercentOfTotalBetweenStartAndEndPoints = 50;
            this.acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.UpToTheRight);
            this.acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.Up);
        }

        public Reparo(String device, String authorization, int order, int duration)
            : base(device, authorization, order, duration)
		{
            List<StrokeDirection> directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.UpToTheLeft);
            directions.Add(StrokeDirection.Up);
            directions.Add(StrokeDirection.UpToTheRight);
            directions.Add(StrokeDirection.Right);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.Down);
            directions.Add(StrokeDirection.DownToTheLeft);
            directions.Add(StrokeDirection.Left);
            directions.Add(StrokeDirection.UpToTheLeft);
            directions.Add(StrokeDirection.Up);
            directions.Add(StrokeDirection.UpToTheRight);
            directions.Add(StrokeDirection.Right);
            this.strokesForSpell.Add(directions);

            directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.UpToTheLeft);
            directions.Add(StrokeDirection.UpToTheRight);
            directions.Add(StrokeDirection.Right);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.DownToTheLeft);
            directions.Add(StrokeDirection.Left);
            directions.Add(StrokeDirection.UpToTheLeft);
            directions.Add(StrokeDirection.UpToTheRight);
            directions.Add(StrokeDirection.Right);
            this.strokesForSpell.Add(directions);

        }
	}
}
