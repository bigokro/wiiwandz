using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiiWandz
{
    class Locomotor : SpellTrigger
    {
        private List<Position> positions;

        public Locomotor(List<Position> positions)
        {
            this.positions = positions;
        }

        public void castSpell()
        {
            // todo
        }

        public Boolean casting()
        {
            Position lastPoint = positions.Last();

            return triggered() && (DateTime.Now.Subtract(lastPoint.time).Seconds < 25);
        }

        public Boolean triggered()
        {
            return false;
        }
    }
}
