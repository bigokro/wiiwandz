using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiiWandz.Strokes;

namespace WiiWandz.Spells
{
    interface SpellTrigger
    {
		Boolean triggered(List<Stroke> strokes);
        void castSpell();
        Boolean casting();
    }
}
