using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiiWandz.Strokes;
using WiiWandz.CloudBit;

namespace WiiWandz.Spells
{
	class Locomotor : StrokeBasedSpell
	{
        public Locomotor(String device, String authorization, int order, int duration)
            : base(device, authorization, order, duration)
		{
            List<StrokeDirection> directions = new List<StrokeDirection>();
            directions.Add(StrokeDirection.Up);
            directions.Add(StrokeDirection.DownToTheLeft);
            directions.Add(StrokeDirection.Right);
            this.strokesForSpell.Add(directions);
		}
    }
}
