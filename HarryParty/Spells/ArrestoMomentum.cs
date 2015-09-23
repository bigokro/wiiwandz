using System.Collections.Generic;
using WiiWandz.Strokes;

namespace WiiWandz.Spells
{
    class ArrestoMomentum : CloudBitSpell
	{
        public ArrestoMomentum(double confidence) : base(confidence)
        {
            minPercentOfTotalBetweenStartAndEndPoints = 70;
            maxPercentOfTotalBetweenStartAndEndPoints = 90;
            acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.Right);
        }

        public ArrestoMomentum(string device, string authorization, int voltage, int duration, string iftttKey, string iftttEvent)
            : base(device, authorization, voltage, duration, iftttKey, iftttEvent)
		{
            var directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.UpToTheRight);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.UpToTheRight);
            directions.Add(StrokeDirection.DownToTheRight);
            strokesForSpell.Add(directions);
		}
	}
}
