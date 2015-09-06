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
        public String iftttUserKey;
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
        private List<int> spellVoltages;
        private List<String> spellIftttEvents;

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
            if (device != null && authorization != null && spellNames != null)
            {
                spells = new List<Spell>();

                for (int i = 0; i < spellNames.Count; i++)
                {
                    String name = spellNames[i];
                    int duration = spellDurations[i];
                    int voltage = spellVoltages[i];
                    String iftttEvent = spellIftttEvents[i];

                    var type = Type.GetType("WiiWandz.Spells."+name);
                    object[] parms = new object[] { device, authorization, voltage, duration, iftttEvent };
                    Spell spell = (Spell) Activator.CreateInstance(type, parms);
                    spells.Add(spell);
                }
            }
        }

        public Spell addPosition(WiimoteLib.Point pointF, DateTime dateTime)
        {
            if ((spellNames == null || spellNames.Count == 0) && !cloudBitWarningShown)
            {
                cloudBitWarningShown = true;
                MessageBox.Show(
                    "You need to choose the spells and set the cloudBit or IFTTT configurations before casting spells", 
                    "Configuration required", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
                return null;
            }


            if (spell == null)
            {
                addPosition(new Position(pointF, dateTime));

                if (positions.Count > 10)
                {
                    if (wandIsPaused(positions))
                    {
                        Spell chosen = brain.chooseSpell(positions);
                        if (chosen != null && authorization != null && device != null && spellNames != null)
                        {
                            for (int i = 0; i < spellNames.Count; i++)
                            {
                                if (spellNames[i].Equals(chosen.GetType().Name))
                                {
                                    spell = chosen;
                                    ((CloudBitSpell)spell).setConfigurations(device, authorization, spellVoltages[i], spellDurations[i], iftttUserKey, spellIftttEvents[i]);
                                    spell.castSpell();
                                    startSpell = DateTime.Now;
                                }
                            }
                        }

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
            }
            else if (!spell.casting()) 
            {
                spell = null;
                positions.Clear();
                if (strokes != null)
                {
                    strokes.Clear();
                }
            }

            return spell;
        }

        public void setDeviceInfo(String device, String authorization, String iftttUserKey)
        {
            this.device = device;
            this.authorization = authorization;
            this.iftttUserKey = iftttUserKey;
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

        protected bool wandIsPaused(List<Position> positions)
        {
            bool paused = false;
            //PositionStatistics stats = new PositionStatistics(positions);
            List<Position> lastFive = positions.GetRange(positions.Count - 5, 5);
            PositionStatistics lastFiveStats = new PositionStatistics(lastFive);

            if (lastFiveStats.Diagonal() < 10)
            {
                paused = true;
                //Console.WriteLine("Paused. Diagonal: " + lastFiveStats.Diagonal());
            }

            return paused;
        }

        public void setSpells(List<String> spellNames, List<int> spellDurations, List<int> spellVoltages, List<String> iftttEvents)
        {
            this.spellNames = spellNames;
            this.spellDurations = spellDurations;
            this.spellVoltages = spellVoltages;
            this.spellIftttEvents = iftttEvents;
            //initializeSpells();
        }
    }
}
