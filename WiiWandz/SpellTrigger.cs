using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiiWandz
{
    interface SpellTrigger
    {
        Boolean triggered();
        void castSpell();
        Boolean casting();
    }
}
