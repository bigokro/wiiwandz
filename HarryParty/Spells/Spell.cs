using System.Collections.Generic;
using WiiWandz.Positions;

namespace WiiWandz.Spells
{
    public interface Spell
    {
		bool triggered(List<Position> positions);
        void castSpell();
        bool casting();
        double getConfidence();
    }
}
