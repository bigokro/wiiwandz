using System.Collections.Generic;
using WiiWandz.Strokes;

namespace WiiWandz.Spells
{
    class Silencio : CloudBitSpell
    {
        public Silencio(double confidence)
            : base(confidence)
        {
            minPercentOfTotalBetweenStartAndEndPoints = 50;
            maxPercentOfTotalBetweenStartAndEndPoints = 100;
            acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.DownToTheLeft);
        }

        public Silencio(string device, string authorization, int voltage, int duration, string iftttKey, string iftttEvent)
            : base(device, authorization, voltage, duration, iftttKey, iftttEvent)
        {
            var directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.Down);
            directions.Add(StrokeDirection.DownToTheLeft);
            directions.Add(StrokeDirection.Left);
            directions.Add(StrokeDirection.UpToTheLeft);
            directions.Add(StrokeDirection.Up);
            directions.Add(StrokeDirection.Down);
            strokesForSpell.Add(directions);

            directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.Down);
            directions.Add(StrokeDirection.DownToTheLeft);
            directions.Add(StrokeDirection.UpToTheLeft);
            directions.Add(StrokeDirection.Down);
            strokesForSpell.Add(directions);
        }
    }
}
