using System;
using System.Net;
using System.Collections.Specialized;
using System.Text;

namespace WiiWandz.Ifttt
{
	public class IftttSignal
	{
		public String secretKey;
        public String eventName;

		public IftttSignal (String secretKey, String eventName)
		{
			this.secretKey = secretKey;
            this.eventName = eventName;
		}

		public void sendSignal() 
		{
			sendSignal (secretKey, eventName);
		}

		public void sendSignal(String secretKey, String eventName)
		{
			using (var client = new WebClient())
			{

                try
                {
                    var responseString = client.DownloadString("https://maker.ifttt.com/trigger/" + eventName + "/with/key/" + secretKey);
                }
                catch (Exception e)
                {
                    // ignore for now
                }
			}
		}
	}
}

