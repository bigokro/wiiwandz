using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiiWandz.Strokes;
using WiiWandz.Positions;

namespace WiiWandz.Spells
{
    public interface Spell
    {
		Boolean triggered(List<Position> positions);
        void castSpell();
        Boolean casting();
        double getConfidence();
    }
}
