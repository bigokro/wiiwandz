using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiiWandz.Strokes;
using WiiWandz.CloudBit;

namespace WiiWandz.Spells
{
	class Descendo : CloudBitSpell
	{
        public Descendo(double confidence) : base(confidence)
        {
            this.minConfidence = 0.999;
            this.minPercentOfTotalBetweenStartAndEndPoints = 30;
            this.maxPercentOfTotalBetweenStartAndEndPoints = 60;
            this.acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.DownToTheLeft);
            this.acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.Down);
        }

        public Descendo(String device, String authorization, int voltage, int duration)
            : base(device, authorization, voltage, duration)
		{
            List<StrokeDirection> directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.Right);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.Down);
            directions.Add(StrokeDirection.DownToTheLeft);
            directions.Add(StrokeDirection.Left);
            directions.Add(StrokeDirection.UpToTheLeft);
            directions.Add(StrokeDirection.Up);
            this.strokesForSpell.Add(directions);

            directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.Right);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.DownToTheLeft);
            directions.Add(StrokeDirection.UpToTheLeft);
            directions.Add(StrokeDirection.Up);
            this.strokesForSpell.Add(directions);

		}
	}
}
