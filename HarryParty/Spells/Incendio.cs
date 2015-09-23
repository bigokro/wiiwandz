using System.Collections.Generic;
using WiiWandz.Strokes;

namespace WiiWandz.Spells
{
    class Incendio : CloudBitSpell
    {
        public Incendio(double confidence)
            : base(confidence)
        {
            minConfidence = 0.99;
            minPercentOfTotalBetweenStartAndEndPoints = 0;
            maxPercentOfTotalBetweenStartAndEndPoints = 10;
            acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.UpToTheRight);
            acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.Right);
            acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.DownToTheRight);
            acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.Down);
            acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.DownToTheLeft);
            acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.Left);
            acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.UpToTheLeft);
            acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.Up);
        }

        public Incendio(string device, string authorization, int voltage, int duration, string iftttKey, string iftttEvent)
            : base(device, authorization, voltage, duration, iftttKey, iftttEvent)
        {
            var directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.UpToTheRight);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.Left);
            strokesForSpell.Add(directions);
        }
    }
}
