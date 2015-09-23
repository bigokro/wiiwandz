using System.Collections.Generic;
using WiiWandz.Strokes;

namespace WiiWandz.Spells
{
    class Herbivicus : CloudBitSpell
	{
        public Herbivicus(double confidence) : base(confidence) 
        {
            minConfidence = 0.9999;
            minPercentOfTotalBetweenStartAndEndPoints = 85;
            maxPercentOfTotalBetweenStartAndEndPoints = 100;
            acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.DownToTheRight);
        }

        public Herbivicus(string device, string authorization, int voltage, int duration, string iftttKey, string iftttEvent)
            : base(device, authorization, voltage, duration, iftttKey, iftttEvent)
		{
            var directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.Down);
            directions.Add(StrokeDirection.Up);
            directions.Add(StrokeDirection.UpToTheRight);
            directions.Add(StrokeDirection.Right);
            directions.Add(StrokeDirection.DownToTheLeft);
            directions.Add(StrokeDirection.Down);
            strokesForSpell.Add(directions);

            directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.Down);
            directions.Add(StrokeDirection.UpToTheRight);
            directions.Add(StrokeDirection.DownToTheLeft);
            directions.Add(StrokeDirection.Down);
            strokesForSpell.Add(directions);
		}
	}
}
