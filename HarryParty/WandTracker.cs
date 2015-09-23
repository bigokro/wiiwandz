using System;
using System.Collections.Generic;
using System.Linq;
using WiiWandz.Spells;
using WiiWandz.Strokes;
using WiiWandz.NN;
using WiiWandz.Positions;

namespace WiiWandz
{
    class WandTracker
    {
		public string device;
		public string authorization;
        public string iftttUserKey;
        public static bool cloudBitWarningShown = false;

		public List<Position> positions;
        public List<Stroke> strokes;
		public List<Spell> spells;
		public StrokeDecomposer decomposer;
        public Brain brain;

		public Spell spell;
        public DateTime startSpell;

        private List<string> spellNames;
        private List<int> spellDurations;
        private List<int> spellVoltages;
        private List<string> spellIftttEvents;

        public WandTracker()
        {
            positions = new List<Position>();
			decomposer = new StrokeDecomposer (1023, 1023, 4);
            brain = new Brain();

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
            /*
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
            */
            spells = new List<Spell>();

            for (int i = 0; i < spellNames.Count; i++)
            {
                string name = spellNames[i];

                var type = Type.GetType("WiiWandz.Spells." + name);
                object[] parms = new object[] { 1.0 };
                Spell spell = (Spell)Activator.CreateInstance(type, parms);
                spells.Add(spell);
            }
        }

        public Spell addMousePosition(System.Drawing.Point pointF, DateTime dateTime)
        {
            return handleAdd(new Position(pointF, dateTime));
        }

        public Spell addPosition(WiimoteLib.Point pointF, DateTime dateTime)
        {
            return handleAdd(new Position(pointF, dateTime));
        }

        public Spell handleAdd(Position pos)
        {
            if (spell == null)
            {
                addPosition(pos);

                if (positions.Count > 10)
                {
                    if (wandIsPaused(positions))
                    {
                        Spell chosen = brain.chooseSpell(positions);
                        if (chosen != null)
                        {
                            for (int i = 0; i < spellNames.Count; i++)
                            {
                                if (spellNames[i].Equals(chosen.GetType().Name))
                                {
                                    spell = chosen;
                                    /*
                                    ((CloudBitSpell)spell).setConfigurations(device, authorization, spellVoltages[i], spellDurations[i], iftttUserKey, spellIftttEvents[i]);
                                    spell.castSpell();
                                    */
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

        public void setDeviceInfo(string device, string authorization, string iftttUserKey)
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
			var timeOfLatest = position.time;

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
            var lastFive = positions.GetRange(positions.Count - 5, 5);
            var lastFiveStats = new PositionStatistics(lastFive);

            if (lastFiveStats.Diagonal() < 10)
            {
                paused = true;
                //Console.WriteLine("Paused. Diagonal: " + lastFiveStats.Diagonal());
            }
            //paused = true;
            return paused;
        }

        public void setSpells(List<string> spellNames, List<int> spellDurations, List<int> spellVoltages, List<string> iftttEvents)
        {
            this.spellNames = spellNames;
            this.spellDurations = spellDurations;
            this.spellVoltages = spellVoltages;
            spellIftttEvents = iftttEvents;
            //initializeSpells();
        }
    }
}
