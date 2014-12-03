using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiiWandz
{
    interface SpellTrigger
    {
        public Boolean triggered();
        public void castSpell();
        public Boolean casting();
    }
}
