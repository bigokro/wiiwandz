using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiiWandz.Spells;
using WiiWandz.Strokes;
using WiiWandz.Test;

namespace WiiWandz
{
    class WandTracker
    {

		public String device;
		public String authorization;

		public List<Position> positions;
        public List<Stroke> strokes;
		public List<SpellTrigger> spells;
		public StrokeDecomposer decomposer;

		public SpellTrigger spell;
        public DateTime startSpell;

        public WandTracker()
        {
            this.positions = new List<Position>();
			this.decomposer = new StrokeDecomposer (1023, 1023, 2);

            spells = new List<SpellTrigger>();

            /*
            StrokeDecomposerTest test = new StrokeDecomposerTest();
            if (!test.testDetermineStroke())
            {
                throw new Exception("testDetermineStroke failed!");
            }
            else
            {
                Console.WriteLine("testDetermineStroke passed");
            }
            */
        }

        public void initializeSpells()
        {
            spells = new List<SpellTrigger>();
            spells.Add(new Incendio(device, authorization));
            spells.Add(new Locomotor(device, authorization));
            spells.Add(new Aguamenti(device, authorization));
        }

        internal SpellTrigger addPosition(WiimoteLib.Point pointF, DateTime dateTime)
        {

            if (spell == null)
            {
                addPosition(new Position(pointF, dateTime));

                if (positions.Count > 2)
                {
                    strokes = decomposer.determineStrokes(positions);

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
            }
            else if (!spell.casting()) 
            {
                spell = null;
                positions.Clear();
                strokes.Clear();
            }

            return spell;
        }

        public void setDeviceInfo(String device, String authorization)
        {
            this.device = device;
            this.authorization = authorization;
            initializeSpells();
        }

		private void addPosition(Position position)
		{
			positions.Add(position);

			// Remove all but last 3 seconds
			DateTime timeOfLatest = position.time;

			int idx = positions.Count - 1;
			while (idx >= 0 
				&& timeOfLatest.Subtract(positions.ElementAt(idx).time).TotalSeconds < 3)
			{
				idx--;
			}

			if (idx > 0) {
				positions.RemoveRange (0, idx);
			}
		}

    }
}
