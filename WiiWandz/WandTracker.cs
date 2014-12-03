using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiiWandz
{
    class WandTracker
    {
        public List<Position> positions;
        public SpellTrigger spell;
        public DateTime startSpell;

        public WandTracker()
        {
            positions = new List<Position>();
        }

        internal SpellTrigger addPosition(WiimoteLib.Point pointF, DateTime dateTime)
        {

            if (spell == null)
            {
                positions.Add(new Position(pointF, dateTime));

                // TODO: move to class for choosing which spell was cast
                SpellTrigger trigger = new Incendio(positions);
                if (trigger.triggered())
                {
                    spell = trigger;
                    startSpell = DateTime.Now;
                }
                else
                {
                    trigger = new Locomotor(positions);
                    if (trigger.triggered())
                    {
                        spell = trigger;
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

    }
}
