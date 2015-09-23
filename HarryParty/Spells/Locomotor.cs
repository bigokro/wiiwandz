using System.Collections.Generic;
using WiiWandz.Strokes;

namespace WiiWandz.Spells
{
    class Locomotor : CloudBitSpell
	{
        public Locomotor(double confidence)
            : base(confidence)
        {
            minConfidence = 0.999;
            minPercentOfTotalBetweenStartAndEndPoints = 20;
            maxPercentOfTotalBetweenStartAndEndPoints = 50;
            acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.UpToTheRight);
            acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.Right);
        }

        public Locomotor(string device, string authorization, int voltage, int duration, string iftttKey, string iftttEvent)
            : base(device, authorization, voltage, duration, iftttKey, iftttEvent)
		{
            var directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.Up);
            directions.Add(StrokeDirection.DownToTheLeft);
            directions.Add(StrokeDirection.Right);
            strokesForSpell.Add(directions);
        }
    }
}
