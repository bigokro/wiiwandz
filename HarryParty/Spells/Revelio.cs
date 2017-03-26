using System;
using System.Collections.Generic;
using WiiWandz.Strokes;
using WiiWandz.Positions;

namespace WiiWandz.Spells
{
    class Revelio : CloudBitSpell
	{
        public Revelio(double confidence)
            : base(confidence)
        {
            minConfidence = 0.999;
            minPercentOfTotalBetweenStartAndEndPoints = 30;
            maxPercentOfTotalBetweenStartAndEndPoints = 70;
            acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.Right);
            acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.DownToTheRight);
        }

        public Revelio(string device, string authorization, int voltage, int duration, string iftttKey, string iftttEvent)
            : base(device, authorization, voltage, duration, iftttKey, iftttEvent)
		{
            var directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.Up);
            directions.Add(StrokeDirection.Right);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.Down);
            directions.Add(StrokeDirection.DownToTheLeft);
            directions.Add(StrokeDirection.Left);
            directions.Add(StrokeDirection.DownToTheRight);
            strokesForSpell.Add(directions);

            directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.Up);
            directions.Add(StrokeDirection.Right);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.DownToTheLeft);
            directions.Add(StrokeDirection.DownToTheRight);
            strokesForSpell.Add(directions);
		}

        protected override bool verifyTrigger(List<Position> positions)
        {
            bool verified = base.verifyTrigger(positions);
            if (verified)
            {
                // Verify that the start and end points are near the bottom, to avoid confusion with Tarantallegra
                var stats = new PositionStatistics(positions);

                if ((Math.Abs(stats.Start().point.Y - stats.yMin) > Math.Abs(stats.Start().point.Y - stats.yMax))
                    || (Math.Abs(stats.End().point.Y - stats.yMin) > Math.Abs(stats.End().point.Y - stats.yMax)))
                {
                    verified = false;
                }
            }

            return verified;
        }
    }
}
