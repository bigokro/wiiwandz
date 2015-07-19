using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiiWandz.Strokes;
using WiiWandz.Ifttt;
using WiiWandz.Positions;

namespace WiiWandz.Spells
{
	class Metelojinx : CloudBitSpell
	{
        public Metelojinx(double confidence)
            : base(confidence)
        {
            this.minConfidence = 0.999;
            this.minPercentOfTotalBetweenStartAndEndPoints = 5;
            this.maxPercentOfTotalBetweenStartAndEndPoints = 60;
            this.acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.Right);
        }

        public Metelojinx(String device, String authorization, int voltage, int duration, String iftttKey, String iftttEvent)
            : base(device, authorization, voltage, duration, iftttKey, iftttEvent)
		{
            List<StrokeDirection> directions = new List<StrokeDirection>();
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
            this.strokesForSpell.Add(directions);

            directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.Down);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.UpToTheRight);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.UpToTheRight);
            this.strokesForSpell.Add(directions);
        }


        protected override bool verifyTrigger(List<Position> positions)
        {
            bool verified = base.verifyTrigger(positions);
            if (verified)
            {
                // Verify that there is no line underneath the endpoint (to avoid confusion with Reparo)
                Position last = positions[positions.Count - 1];
                for (int i = 0; i < positions.Count - 1; i++)
                {
                    Position start = positions[i];
                    Position end = positions[i + 1];

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
