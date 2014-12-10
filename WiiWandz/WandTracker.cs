using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiiWandz.Spells;
using WiiWandz.Strokes;

namespace WiiWandz
{
    class WandTracker
    {

		public String device = "00e04c223418";
		public String authorization = "f44dbe4b28c2f26eb9a10eb7cb3510dd465d3fe34355cefe6fdfddf4ce2c5ae6";

		private List<Position> positions;
		private List<SpellTrigger> spells;
		private StrokeDecomposer decomposer;

		public SpellTrigger spell;
        public DateTime startSpell;

        public WandTracker()
        {
            this.positions = new List<Position>();
			this.decomposer = new StrokeDecomposer (1023, 1023, 10);

			spells = new List<SpellTrigger> ();
			spells.Add (new Incendio (device, authorization));
        }

        internal SpellTrigger addPosition(WiimoteLib.Point pointF, DateTime dateTime)
        {

            if (spell == null)
            {
                addPosition(new Position(pointF, dateTime));

				List<Stroke> strokes = decomposer.determineStrokes (positions);

                // TODO: move to class for choosing which spell was cast
				foreach (SpellTrigger trigger in spells)
				{
					if (trigger.triggered(strokes))
					{
						spell = trigger;
						trigger.castSpell();
						startSpell = DateTime.Now;
					}
				}
            }
            else if (!spell.casting()) 
            {
                spell = null;
                positions.Clear();
            }

            return spell;
        }

		private void addPosition(Position position)
		{
			positions.Add(position);

			// Remove all but last 3 seconds
			DateTime timeOfLatest = position.time;

			int idx = positions.Count - 1;
			while (idx >= 0 
				&& timeOfLatest.Subtract(positions.ElementAt(idx).time).TotalSeconds > 3)
			{
				idx--;
			}

			if (idx > 0) {
				positions.RemoveRange (0, idx);
			}
		}

    }
}
