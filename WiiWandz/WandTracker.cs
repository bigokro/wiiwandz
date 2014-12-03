using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiiWandz
{
    class WandTracker
    {
        private List<Position> positions;
        private SpellTrigger spell;

        public WandTracker()
        {
            positions = new List<Position>();
        }

        internal SpellTrigger addPosition(WiimoteLib.PointF pointF, DateTime dateTime)
        {

            if (spell == null)
            {
                positions.Add(new Position(pointF, dateTime));

                // TODO: move to class for choosing which spell was cast
                SpellTrigger trigger = new Incendio(positions);
                if (trigger.triggered())
                {
                    spell = trigger;
                }
                else
                {
                    trigger = new Locomotor(positions);
                    if (trigger.triggered())
                    {
                        spell = trigger;
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
