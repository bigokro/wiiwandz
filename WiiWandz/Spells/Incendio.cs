using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.Specialized;
using WiiWandz.Strokes;
using WiiWandz.CloudBit;

namespace WiiWandz.Spells
{
    class Incendio : SpellTrigger
    {
		public String device;
		public String authorization;
		private CloudBitSignal cloudBit;
		private List<StrokeDirection> strokesForSpell;

		public DateTime lastTrigger;

		public Incendio(String device, String authorization)
        {
			this.device = device;
			this.authorization = authorization;
			this.cloudBit = new CloudBitSignal (this.device, this.authorization, 25, 10 * 1000);

			this.strokesForSpell = new List<StrokeDirection> ();
			this.strokesForSpell.Add (StrokeDirection.UpToTheRight);
			this.strokesForSpell.Add (StrokeDirection.DownToTheRight);
			this.strokesForSpell.Add (StrokeDirection.Left);
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
            Boolean trig = decomposer.strokesMatch(directions, this.strokesForSpell);

            if (trig)
            {
                lastTrigger = DateTime.Now;
            }

            return trig;
        }

    }
}
