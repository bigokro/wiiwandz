using WiiWandz.Ifttt;
using System.Threading;

namespace WiiWandz.Spells
{
    class IftttStartStopSpell : CloudBitSpell
	{
        private readonly static TimerCallback timer = new TimerCallback(ExecuteDelayedAction);

        string stopEvent;
        int seconds;

        public IftttStartStopSpell(string iftttKey, string iftttEvent, string stopEvent, int seconds)
            : base(null, null, 0, 0, iftttKey, iftttEvent)
		{
            this.seconds = seconds;
            this.stopEvent = stopEvent;
		}

        public override void castSpell()
        {
            ifttt.sendSignal();

            if (stopEvent != null)
            {
                ifttt.eventName = stopEvent;
                new Timer(timer, ifttt, seconds * 1000, Timeout.Infinite);
            }
        }

        private static void ExecuteDelayedAction(object o)
        {
            IftttSignal ifttt = (IftttSignal)o;
            ifttt.sendSignal();
        }
    }

}
