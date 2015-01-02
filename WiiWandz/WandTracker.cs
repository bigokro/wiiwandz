using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiiWandz.Spells;
using WiiWandz.Strokes;
using WiiWandz.Test;
using System.Windows.Forms;
using WiiWandz.NN;
using WiiWandz.Positions;

namespace WiiWandz
{
    class WandTracker
    {

		public String device;
		public String authorization;
        public static Boolean cloudBitWarningShown = false;

		public List<Position> positions;
        public List<Stroke> strokes;
		public List<Spell> spells;
		public StrokeDecomposer decomposer;
        public Brain brain;

		public Spell spell;
        public DateTime startSpell;

        private List<String> spellNames;
        private List<int> spellDurations;

        public WandTracker()
        {
            this.positions = new List<Position>();
			this.decomposer = new StrokeDecomposer (1023, 1023, 4);
            this.brain = new Brain();

            spells = new List<Spell>();

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
            if (device != null && authorization != null)
            {
                spells = new List<Spell>();

                for (int i = 0; i < spellNames.Count; i++)
                {
                    String name = spellNames[i];
                    int duration = spellDurations[i];

                    var type = Type.GetType("WiiWandz.Spells."+name);
                    object[] parms = new object[] { device, authorization, i+1, duration };
                    Spell spell = (Spell) Activator.CreateInstance(type, parms);

                    spells.Add(spell);
                }
            }
        }

        public Spell addPosition(WiimoteLib.Point pointF, DateTime dateTime)
        {
            if (spells.Count == 0 && !cloudBitWarningShown)
            {
                cloudBitWarningShown = true;
                MessageBox.Show(
                    "You need to choose the spells and set the cloudBit configurations before casting spells", 
                    "Configuration required", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
                return null;
            }


            if (spell == null || true)
            {
                addPosition(new Position(pointF, dateTime));

                if (positions.Count > 2)
                {
                    spell = brain.chooseSpell(positions);
                    if (authorization != null && device != null)
                    {
                        ((CloudBitSpell)spell).device = device;
                        ((CloudBitSpell)spell).authorization = authorization;
                        ((CloudBitSpell)spell).duration = 3;
                        ((CloudBitSpell)spell).order = 1;
                        spell.castSpell();
                    }
                    startSpell = DateTime.Now;

                    /*
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
                     */
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

			// Remove all but last 2 seconds
			DateTime timeOfLatest = position.time;

			int idx = positions.Count - 1;
			while (idx >= 0 
				&& timeOfLatest.Subtract(positions.ElementAt(idx).time).TotalSeconds < 2)
			{
				idx--;
			}

			if (idx > 0) {
				positions.RemoveRange (0, idx);
			}
		}

        public void setSpells(List<String> spellNames, List<int> spellDurations)
        {
            this.spellNames = spellNames;
            this.spellDurations = spellDurations;
            initializeSpells();
        }
    }
}
