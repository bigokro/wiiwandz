using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiiWandz.CloudBit;
using WiiWandz.Strokes;

namespace WiiWandz.Spells
{
    abstract class StrokeBasedSpell : SpellTrigger
    {
		public String device;
		public String authorization;
        public int order;
        public int duration;
		protected CloudBitSignal cloudBit;
		protected List<List<StrokeDirection>> strokesForSpell;

		public DateTime lastTrigger;

        public StrokeBasedSpell(String device, String authorization, int order, int duration)
		{
			this.device = device;
			this.authorization = authorization;
            this.order = order;
            this.duration = duration;
			this.cloudBit = new CloudBitSignal (this.device, this.authorization, order * 25, duration * 1000);

			this.strokesForSpell = new List<List<StrokeDirection>>();
		}

		public void castSpell()
		{
			cloudBit.sendSignal ();
		}

		public Boolean casting()
		{
			return lastTrigger != null 
				&& (DateTime.Now.Subtract(lastTrigger).TotalSeconds < 10);
		}

		public Boolean triggered(List<Stroke> strokes)
		{
            StrokeDecomposer decomposer = new StrokeDecomposer(1023, 1023, 10);
            List<StrokeDirection> directions = new List<StrokeDirection>();
            foreach (Stroke stroke in strokes)
            {
                directions.Add(stroke.direction);
            }

            Boolean trig = false;
            foreach (List<StrokeDirection> strokeSet in this.strokesForSpell)
            {
                trig = trig || decomposer.strokesMatch(directions, strokeSet);
            }

            if (trig)
            {
                lastTrigger = DateTime.Now;
            }

            return trig;
        }

    }
}
