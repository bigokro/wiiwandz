using System.Collections.Generic;
using WiiWandz.Strokes;
using WiiWandz.Positions;

namespace WiiWandz.Spells
{
    class Metelojinx : CloudBitSpell
	{
        public Metelojinx(double confidence)
            : base(confidence)
        {
            minConfidence = 0.999;
            minPercentOfTotalBetweenStartAndEndPoints = 5;
            maxPercentOfTotalBetweenStartAndEndPoints = 60;
            acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.Right);
        }

        public Metelojinx(string device, string authorization, int voltage, int duration, string iftttKey, string iftttEvent)
            : base(device, authorization, voltage, duration, iftttKey, iftttEvent)
		{
            var directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.Down);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.Right);
            directions.Add(StrokeDirection.UpToTheRight);
            directions.Add(StrokeDirection.Up);
            directions.Add(StrokeDirection.Down);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.Right);
            directions.Add(StrokeDirection.UpToTheRight);
            directions.Add(StrokeDirection.Up);
            strokesForSpell.Add(directions);

            directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.Down);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.UpToTheRight);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.UpToTheRight);
            strokesForSpell.Add(directions);
        }


        protected override bool verifyTrigger(List<Position> positions)
        {
            bool verified = base.verifyTrigger(positions);
            if (verified)
            {
                // Verify that there is no line underneath the endpoint (to avoid confusion with Reparo)
                var last = positions[positions.Count - 1];
                for (int i = 0; i < positions.Count - 1; i++)
                {
                    var start = positions[i];
                    var end = positions[i + 1];

                    if ((start.point.X <= last.point.X && end.point.X >= last.point.X)
                        || (start.point.X >= last.point.X && end.point.X <= last.point.X))
                    {
                        if (start.point.Y < last.point.Y && end.point.Y < last.point.Y)
                        {
                            verified = false;
                            break;
                        }
                    }
                }
            }

            return verified;
        }
	}
}
