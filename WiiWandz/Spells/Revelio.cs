using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiiWandz.Strokes;
using WiiWandz.CloudBit;
using WiiWandz.Positions;

namespace WiiWandz.Spells
{
	class Revelio : CloudBitSpell
	{
        public Revelio(double confidence)
            : base(confidence)
        {
            this.minConfidence = 0.999;
            this.minPercentOfTotalBetweenStartAndEndPoints = 30;
            this.maxPercentOfTotalBetweenStartAndEndPoints = 70;
            this.acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.Right);
            this.acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.DownToTheRight);
        }

        public Revelio(String device, String authorization, int voltage, int duration)
            : base(device, authorization, voltage, duration)
		{
            List<StrokeDirection> directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.Up);
            directions.Add(StrokeDirection.Right);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.Down);
            directions.Add(StrokeDirection.DownToTheLeft);
            directions.Add(StrokeDirection.Left);
            directions.Add(StrokeDirection.DownToTheRight);
            this.strokesForSpell.Add(directions);

            directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.Up);
            directions.Add(StrokeDirection.Right);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.DownToTheLeft);
            directions.Add(StrokeDirection.DownToTheRight);
            this.strokesForSpell.Add(directions);

		}

        protected override bool verifyTrigger(List<Position> positions)
        {
            bool verified = base.verifyTrigger(positions);
            if (verified)
            {
                // Verify that the start and end points are near the bottom, to avoid confusion with Tarantallegra
                PositionStatistics stats = new PositionStatistics(positions);

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
