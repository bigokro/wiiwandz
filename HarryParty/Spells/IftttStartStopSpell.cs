using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiiWandz.Strokes;
using WiiWandz.Ifttt;
using System.Threading;

namespace WiiWandz.Spells
{
	class IftttStartStopSpell : CloudBitSpell
	{
        private readonly static TimerCallback timer = new TimerCallback(IftttStartStopSpell.ExecuteDelayedAction);

        String stopEvent;
        int seconds;

        public IftttStartStopSpell(String iftttKey, String iftttEvent, String stopEvent, int seconds)
            : base(null, null, 0, 0, iftttKey, iftttEvent)
		{
            this.seconds = seconds;
            this.stopEvent = stopEvent;
		}

        public override void castSpell()
        {
            ifttt.sendSignal();

            ifttt.eventName = stopEvent;
            new Timer(timer, ifttt, seconds * 1000, Timeout.Infinite);
        }

        private static void ExecuteDelayedAction(object o)
        {
            IftttSignal ifttt = (IftttSignal)o;
            ifttt.sendSignal();
        }
    }

}
