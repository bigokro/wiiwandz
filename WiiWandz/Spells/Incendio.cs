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
		private List<Stroke> strokesForSpell;

		public DateTime lastTrigger;

		public Incendio(String device, String authorization)
        {
			this.device = device;
			this.authorization = authorization;
			this.cloudBit = new CloudBitSignal (this.device, this.authorization, 25, 10 * 1000);

			this.strokesForSpell = new List<Stroke> ();
			//this.strokesForSpell.Add (Stroke.UpToTheRight);
			//this.strokesForSpell.Add (Stroke.DownToTheRight);
			this.strokesForSpell.Add (Stroke.Left);
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
			StrokeDecomposer decomposer = new StrokeDecomposer (1023, 1023, 10);
            Boolean trig = decomposer.strokesMatch(strokes, this.strokesForSpell);

            if (trig)
            {
                lastTrigger = DateTime.Now;
            }

            return trig;
        }

    }
}
