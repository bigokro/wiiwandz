using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiiWandz.Ifttt;
using WiiWandz.Strokes;
using WiiWandz.Positions;
using WiiWandz.CloudBit;

namespace WiiWandz.Spells
{
    abstract class CloudBitSpell : Spell
    {
		public String device;
		public String authorization;
        public int voltage;
        public int duration;

        public String iftttSecretKey;
        public String iftttEvent;

        public double confidence;

        protected double minConfidence;
        protected int minPercentOfTotalBetweenStartAndEndPoints;
        protected int maxPercentOfTotalBetweenStartAndEndPoints;
        protected List<StrokeDirection> acceptableDirectionsFromStartToEndPoint;

		protected CloudBitSignal cloudBit;
        protected IftttSignal ifttt;
        protected List<List<StrokeDirection>> strokesForSpell;

		public DateTime lastTrigger;

        public CloudBitSpell(double confidence) 
        {
            this.confidence = confidence;
            this.acceptableDirectionsFromStartToEndPoint = new List<StrokeDirection>();
            this.minConfidence = 0.9999;
        }

        public CloudBitSpell(String device, String authorization, int voltage, int duration, String iftttKey, String iftttEvent)
		{
            setConfigurations(device, authorization, voltage, duration, iftttKey, iftttEvent);
			this.strokesForSpell = new List<List<StrokeDirection>>();
		}

        public void setConfigurations(String device, String authorization, int voltage, int duration, String iftttSecretKey, String iftttEvent)
        {
            this.device = device;
            this.authorization = authorization;
            this.voltage = voltage;
            this.duration = duration;
            this.iftttSecretKey = iftttSecretKey;
            this.iftttEvent = iftttEvent;
            this.cloudBit = new CloudBitSignal(this.device, this.authorization, voltage, duration * 1000);
            this.ifttt = new IftttSignal(this.iftttSecretKey, iftttEvent);
        }

		public void castSpell()
		{
            if (iftttSecretKey != null && iftttEvent != null && ifttt != null)
            {
                ifttt.sendSignal();
            } 
            else if (cloudBit != null)
            {
                cloudBit.sendSignal();
            }
		}

		public Boolean casting()
		{
			return lastTrigger != null 
				&& (DateTime.Now.Subtract(lastTrigger).TotalMilliseconds < (this.duration * 1000));
		}

		public bool triggered(List<Position> positions)
		{
            bool trig = false;

            /*
            StrokeDecomposer decomposer = new StrokeDecomposer(PositionStatistics.MAX_X, PositionStatistics.MAX_Y, 10);
            List<Stroke> strokes = decomposer.determineStrokes(positions);
            List<StrokeDirection> directions = new List<StrokeDirection>();
            foreach (Stroke stroke in strokes)
            {
                directions.Add(stroke.direction);
            }

            foreach (List<StrokeDirection> strokeSet in this.strokesForSpell)
            {
                trig = trig || StrokeDecomposer.strokesMatch(directions, strokeSet);
            }
            */

            trig = verifyTrigger(positions);

            if (trig)
            {
                lastTrigger = DateTime.Now;
            }

            return trig;
        }

        protected virtual bool verifyTrigger(List<Position> positions)
        {
            PositionStatistics stats = new PositionStatistics(positions);

            StrokeDirection direction = StrokeDecomposer.determineDirection(stats.Start(), stats.End());
            double distance = stats.Diagonal();
            double relativeStartAndEndDistance = stats.FractionOfTotal(stats.Start(), stats.End());

            /*
            if (this.GetType() == typeof(Ascendio))
            {
                Console.WriteLine(
                    "Confidence: " + confidence 
                    + " direction: " + direction
                    + " (" + stats.Start().point.ToString() + " -> " + stats.End().point.ToString() + ")"
                    + " distance: " + distance + " (" + relativeStartAndEndDistance + ")");
            }
            */ 

            bool verified = false;

            foreach (StrokeDirection dir in acceptableDirectionsFromStartToEndPoint)
            {
                if (dir == direction)
                {
                    verified = true;
                }
            }

            verified = verified && confidence >= minConfidence;

            verified = verified && distance >= 400.0;
            verified = verified && (relativeStartAndEndDistance * 100) >= minPercentOfTotalBetweenStartAndEndPoints;
            verified = verified && (relativeStartAndEndDistance * 100) <= maxPercentOfTotalBetweenStartAndEndPoints;

            return verified;
        }

        public double getConfidence()
        {
            return this.confidence;
        }

    }
}
