using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiiWandz.Strokes;
using WiiWandz.Ifttt;

namespace WiiWandz.Spells
{
	class Ascendio : CloudBitSpell
	{
        public Ascendio(double confidence) : base(confidence) 
        {
            this.minConfidence = 0.99;
            this.minPercentOfTotalBetweenStartAndEndPoints = 30;
            this.maxPercentOfTotalBetweenStartAndEndPoints = 70;
            this.acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.Up);

        }

        public Ascendio(String device, String authorization, int voltage, int duration, String iftttKey, String iftttEvent)
            : base(device, authorization, voltage, duration, iftttKey, iftttEvent)
		{
            List<StrokeDirection> directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.Right);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.Down);
            directions.Add(StrokeDirection.DownToTheLeft);
            directions.Add(StrokeDirection.Left);
            directions.Add(StrokeDirection.UpToTheLeft);
            directions.Add(StrokeDirection.Up);
            this.strokesForSpell.Add(directions);

            directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.Right);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.DownToTheLeft);
            directions.Add(StrokeDirection.UpToTheLeft);
            directions.Add(StrokeDirection.Up);
            this.strokesForSpell.Add(directions);

		}
	}
}
