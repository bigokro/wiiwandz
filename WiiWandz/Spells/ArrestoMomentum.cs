using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiiWandz.Strokes;
using WiiWandz.CloudBit;

namespace WiiWandz.Spells
{
	class ArrestoMomentum : CloudBitSpell
	{
        public ArrestoMomentum(double confidence) : base(confidence)
        {

            this.minPercentOfTotalBetweenStartAndEndPoints = 70;
            this.maxPercentOfTotalBetweenStartAndEndPoints = 90;
            this.acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.Right);
        }

        public ArrestoMomentum(String device, String authorization, int voltage, int duration)
            : base(device, authorization, voltage, duration)
		{
            List<StrokeDirection> directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.UpToTheRight);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.UpToTheRight);
            directions.Add(StrokeDirection.DownToTheRight);
            this.strokesForSpell.Add(directions);

		}
	}
}
