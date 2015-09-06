using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiiWandz.Strokes;
using WiiWandz.Ifttt;

namespace WiiWandz.Spells
{
	class Aguamenti : CloudBitSpell
	{
        public Aguamenti(double confidence) : base(confidence) 
        {
            this.minConfidence = 0.99;
            this.minPercentOfTotalBetweenStartAndEndPoints = 70;
            this.maxPercentOfTotalBetweenStartAndEndPoints = 100;
            this.acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.UpToTheRight);
            this.acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.Right);
        }

        public Aguamenti(String device, String authorization, int voltage, int duration, String iftttKey, String iftttEvent)
            : base(device, authorization, voltage, duration, iftttKey, iftttEvent)
		{
            List<StrokeDirection> directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.UpToTheRight);
            directions.Add(StrokeDirection.Right);
            this.strokesForSpell.Add(directions);

        }
	}
}
