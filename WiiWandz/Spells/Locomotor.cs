using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiiWandz.Strokes;
using WiiWandz.CloudBit;

namespace WiiWandz.Spells
{
	class Locomotor : SpellTrigger
	{
		public String device;
		public String authorization;
		private CloudBitSignal cloudBit;
		private List<Stroke> strokesForSpell;

		public DateTime lastTrigger;

		public Locomotor(String device, String authorization)
		{
			this.device = device;
			this.authorization = authorization;
			this.cloudBit = new CloudBitSignal (this.device, this.authorization, 50, 10 * 1000);

			this.strokesForSpell = new List<Stroke> ();
			this.strokesForSpell.Add (Stroke.Up);
			this.strokesForSpell.Add (Stroke.DownToTheLeft);
			this.strokesForSpell.Add (Stroke.Right);
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
			return decomposer.strokesMatch(strokes, this.strokesForSpell);
		}

    }
}
