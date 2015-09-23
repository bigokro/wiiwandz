using System.Collections.Generic;
using WiiWandz.Strokes;

namespace WiiWandz.Spells
{
    class Descendo : CloudBitSpell
	{
        public Descendo(double confidence) : base(confidence)
        {
            minConfidence = 0.999;
            minPercentOfTotalBetweenStartAndEndPoints = 30;
            maxPercentOfTotalBetweenStartAndEndPoints = 60;
            acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.DownToTheLeft);
            acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.Down);
        }

        public Descendo(string device, string authorization, int voltage, int duration, string iftttKey, string iftttEvent)
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
