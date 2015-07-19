using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.Specialized;
using WiiWandz.Strokes;
using WiiWandz.Ifttt;

namespace WiiWandz.Spells
{
    class Incendio : CloudBitSpell
    {
        public Incendio(double confidence)
            : base(confidence)
        {
            this.minConfidence = 0.99;
            this.minPercentOfTotalBetweenStartAndEndPoints = 0;
            this.maxPercentOfTotalBetweenStartAndEndPoints = 10;
            this.acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.UpToTheRight);
            this.acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.Right);
            this.acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.DownToTheRight);
            this.acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.Down);
            this.acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.DownToTheLeft);
            this.acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.Left);
            this.acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.UpToTheLeft);
            this.acceptableDirectionsFromStartToEndPoint.Add(StrokeDirection.Up);
        }

        public Incendio(String device, String authorization, int voltage, int duration, String iftttKey, String iftttEvent)
            : base(device, authorization, voltage, duration, iftttKey, iftttEvent)
        {
            List<StrokeDirection> directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.UpToTheRight);
            directions.Add(StrokeDirection.DownToTheRight);
            directions.Add(StrokeDirection.Left);
            this.strokesForSpell.Add(directions);

        }
    }
}
