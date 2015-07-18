using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiiWandz.Strokes;
using WiiWandz.Ifttt;

namespace WiiWandz.Spells
{
	class SpecialisRevelio : CloudBitSpell
	{
        public SpecialisRevelio(double confidence)
            : base(confidence)
        {
            this.minPercentOfTotalBetweenStartAndEndPoints = 5;
            this.maxPercentOfTotalBetweenStartAndEndPoints = 30;
            this.acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.UpToTheLeft);
            this.acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.Up);
        }

        public SpecialisRevelio(String device, String authorization, int voltage, int duration)
            : base(device, authorization, voltage, duration)
		{
            List<StrokeDirection> directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.Left);
            directions.Add(StrokeDirection.DownToTheLeft);
            directions.Add(StrokeDirection.Down);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.Right);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.Down);
            directions.Add(StrokeDirection.DownToTheLeft);
            directions.Add(StrokeDirection.Left);
            directions.Add(StrokeDirection.UpToTheLeft);
            directions.Add(StrokeDirection.Up);
            directions.Add(StrokeDirection.UpToTheRight);
            directions.Add(StrokeDirection.Right);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.Down);
            this.strokesForSpell.Add(directions);

            directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.Left);
            directions.Add(StrokeDirection.DownToTheLeft);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.Right);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.DownToTheLeft);
            directions.Add(StrokeDirection.Left);
            directions.Add(StrokeDirection.UpToTheLeft);
            directions.Add(StrokeDirection.UpToTheRight);
            directions.Add(StrokeDirection.Right);
            this.strokesForSpell.Add(directions);

		}
	}
}
