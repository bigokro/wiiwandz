using System;
using System.Net;

namespace WiiWandz.Ifttt
{
    public class IftttSignal
	{
		public string secretKey;
        public string eventName;

		public IftttSignal (string secretKey, string eventName)
		{
			this.secretKey = secretKey;
            this.eventName = eventName;
		}

		public void sendSignal() 
		{
			sendSignal (secretKey, eventName);
		}

		public void sendSignal(string secretKey, string eventName)
		{
			using (var client = new WebClient())
			{
                try
                {
                    var responseString = client.DownloadString("https://maker.ifttt.com/trigger/" + eventName + "/with/key/" + secretKey);
                }
                catch (Exception)
                {
                    // ignore for now
                }
			}
		}
	}
}

