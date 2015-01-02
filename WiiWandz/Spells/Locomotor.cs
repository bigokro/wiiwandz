using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiiWandz.Strokes;
using WiiWandz.CloudBit;

namespace WiiWandz.Spells
{
	class Locomotor : CloudBitSpell
	{
        public Locomotor(double confidence)
            : base(confidence)
        {
            this.minConfidence = 0.999;
            this.minPercentOfTotalBetweenStartAndEndPoints = 20;
            this.maxPercentOfTotalBetweenStartAndEndPoints = 50;
            this.acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.UpToTheRight);
            this.acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.Right);
        }

        public Locomotor(String device, String authorization, int order, int duration)
            : base(device, authorization, order, duration)
		{
            List<StrokeDirection> directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.Up);
            directions.Add(StrokeDirection.DownToTheLeft);
            directions.Add(StrokeDirection.Right);
            this.strokesForSpell.Add(directions);
        }
    }
}
