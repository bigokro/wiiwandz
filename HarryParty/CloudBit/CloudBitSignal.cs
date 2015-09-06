using System;
using System.Net;
using System.Collections.Specialized;
using System.Text;

namespace WiiWandz.CloudBit
{
	public class CloudBitSignal
	{
		public String device;
        public String authorization;
		public int percent;
		public int duration;

        public CloudBitSignal(String device, String authorization, int percent, int duration)
		{
			this.device = device;
            this.authorization = authorization;
			this.percent = percent;
			this.duration = duration;
		}

		public void sendSignal() 
		{
			sendSignal (device, authorization, percent, duration);
		}

		public void sendSignal(String device, String authorization, int percent, int duration)
		{
			using (var client = new WebClient())
			{
				client.Headers.Set("Authorization", "Bearer " + authorization);
				client.Headers.Set("Accept", "application/vnd.littlebits.v2+json");

				var values = new NameValueCollection();
				values["percent"] = percent.ToString();
				values["duration_ms"] = duration.ToString();

                try
                {
                    var response = client.UploadValues("https://api-http.littlebitscloud.cc/devices/" + device + "/output", values);
                    var responseString = Encoding.Default.GetString(response);
                }
                catch (Exception e)
                {
                    // ignore for now
                }
			}
		}
	}
}

