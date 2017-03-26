using System.Collections.Generic;
using WiiWandz.Strokes;

namespace WiiWandz.Spells
{
    class Aguamenti : CloudBitSpell
	{
        public Aguamenti(double confidence) : base(confidence) 
        {
            minConfidence = 0.99;
            minPercentOfTotalBetweenStartAndEndPoints = 70;
            maxPercentOfTotalBetweenStartAndEndPoints = 100;
            acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.UpToTheRight);
            acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.Right);
        }

        public Aguamenti(string device, string authorization, int voltage, int duration, string iftttKey, string iftttEvent)
            : base(device, authorization, voltage, duration, iftttKey, iftttEvent)
		{
            var directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.UpToTheRight);
            directions.Add(StrokeDirection.Right);
            strokesForSpell.Add(directions);

        }
	}
}
