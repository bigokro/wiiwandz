using System.Collections.Generic;
using WiiWandz.Strokes;

namespace WiiWandz.Spells
{
    class Reparo : CloudBitSpell
	{
        public Reparo(double confidence)
            : base(confidence)
        {
            minConfidence = 0.999;
            minPercentOfTotalBetweenStartAndEndPoints = 30;
            maxPercentOfTotalBetweenStartAndEndPoints = 70;
            acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.UpToTheRight);
            acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.Right);
            acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.DownToTheRight);
        }

        public Reparo(string device, string authorization, int voltage, int duration, string iftttKey, string iftttEvent)
            : base(device, authorization, voltage, duration, iftttKey, iftttEvent)
		{
            var directions = new List<StrokeDirection>();
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
            strokesForSpell.Add(directions);

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
            strokesForSpell.Add(directions);
        }
	}
}
