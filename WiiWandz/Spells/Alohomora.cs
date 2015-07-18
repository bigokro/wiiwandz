using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiiWandz.Strokes;
using WiiWandz.Ifttt;

namespace WiiWandz.Spells
{
	class Alohomora : CloudBitSpell
	{
        public Alohomora(double confidence) : base(confidence) 
        {
            this.minConfidence = 0.999;
            this.minPercentOfTotalBetweenStartAndEndPoints = 80;
            this.maxPercentOfTotalBetweenStartAndEndPoints = 95;
            this.acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.Down);

        }

        public Alohomora(String device, String authorization, int voltage, int duration)
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
            directions.Add(StrokeDirection.UpToTheRight);
            directions.Add(StrokeDirection.Down);
            this.strokesForSpell.Add(directions);

            directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.Right);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.DownToTheLeft);
            directions.Add(StrokeDirection.Left);
            directions.Add(StrokeDirection.UpToTheLeft);
            directions.Add(StrokeDirection.UpToTheRight);
            directions.Add(StrokeDirection.Down);
            this.strokesForSpell.Add(directions);

		}
	}
}
