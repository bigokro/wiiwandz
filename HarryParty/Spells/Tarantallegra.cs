using System.Collections.Generic;
using WiiWandz.Strokes;

namespace WiiWandz.Spells
{
    class Tarantallegra : CloudBitSpell
	{
        public Tarantallegra(double confidence)
            : base(confidence)
        {
            minConfidence = 0.999;
            minPercentOfTotalBetweenStartAndEndPoints = 70;
            maxPercentOfTotalBetweenStartAndEndPoints = 95;
            acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.Right);
        }

        public Tarantallegra(string device, string authorization, int voltage, int duration, string iftttKey, string iftttEvent)
            : base(device, authorization, voltage, duration, iftttKey, iftttEvent)
		{
            var directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.DownToTheLeft);
            directions.Add(StrokeDirection.Down);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.Right);
            directions.Add(StrokeDirection.UpToTheRight);
            directions.Add(StrokeDirection.Right);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.Down);
            directions.Add(StrokeDirection.DownToTheLeft);
            strokesForSpell.Add(directions);

            directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.DownToTheLeft);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.Right);
            directions.Add(StrokeDirection.UpToTheRight);
            directions.Add(StrokeDirection.Right);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.DownToTheLeft);
            strokesForSpell.Add(directions);
		}
    }
}
