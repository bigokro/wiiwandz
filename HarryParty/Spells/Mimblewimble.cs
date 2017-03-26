using System.Collections.Generic;
using WiiWandz.Strokes;

namespace WiiWandz.Spells
{
    class Mimblewimble : CloudBitSpell
	{
        public Mimblewimble(double confidence)
            : base(confidence)
        {
            minPercentOfTotalBetweenStartAndEndPoints = 10;
            maxPercentOfTotalBetweenStartAndEndPoints = 30;
            acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.DownToTheRight);
            acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.Right);
        }

        public Mimblewimble(string device, string authorization, int voltage, int duration, string iftttKey, string iftttEvent)
            : base(device, authorization, voltage, duration, iftttKey, iftttEvent)
		{
            var directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.Right);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.Down);
            directions.Add(StrokeDirection.DownToTheLeft);
            directions.Add(StrokeDirection.Left);
            directions.Add(StrokeDirection.UpToTheLeft);
            directions.Add(StrokeDirection.Up);
            strokesForSpell.Add(directions);

            directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.Right);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.DownToTheLeft);
            directions.Add(StrokeDirection.UpToTheLeft);
            directions.Add(StrokeDirection.Up);
            strokesForSpell.Add(directions);
        }
	}
}
