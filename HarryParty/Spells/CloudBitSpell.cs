using System;
using System.Collections.Generic;
using WiiWandz.Ifttt;
using WiiWandz.Strokes;
using WiiWandz.Positions;
using WiiWandz.CloudBit;

namespace WiiWandz.Spells
{
    abstract class CloudBitSpell : Spell
    {
		public string device;
		public string authorization;
        public int voltage;
        public int duration;

        public string iftttSecretKey;
        public string iftttEvent;

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
            acceptableDirectionsFromStartToEndPoint = new List<StrokeDirection>();
            minConfidence = 0.9999;
        }

        public CloudBitSpell(string device, string authorization, int voltage, int duration, string iftttKey, string iftttEvent)
		{
            setConfigurations(device, authorization, voltage, duration, iftttKey, iftttEvent);
			strokesForSpell = new List<List<StrokeDirection>>();
		}

        public void setConfigurations(string device, string authorization, int voltage, int duration, string iftttSecretKey, string iftttEvent)
        {
            this.device = device;
            this.authorization = authorization;
            this.voltage = voltage;
            this.duration = duration;
            this.iftttSecretKey = iftttSecretKey;
            this.iftttEvent = iftttEvent;
            cloudBit = new CloudBitSignal(this.device, this.authorization, voltage, duration * 1000);
            ifttt = new IftttSignal(this.iftttSecretKey, iftttEvent);
        }

		public virtual void castSpell()
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

		public bool casting()
		{
			return lastTrigger != null 
				&& (DateTime.Now.Subtract(lastTrigger).TotalMilliseconds < (duration * 1000));
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
            var stats = new PositionStatistics(positions);

            var direction = StrokeDecomposer.determineDirection(stats.Start(), stats.End());
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
            return confidence;
        }

    }
}
