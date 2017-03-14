using System;
using System.Net;
using System.Collections.Specialized;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

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
			ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
			ServicePointManager.Expect100Continue = true;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
			
			using (var client = new WebClient())
			{
				client.Headers.Set("Authorization", authorization);
				client.Headers.Set("Accept", "application/json");
                		client.Headers[HttpRequestHeader.ContentType] = "application/json";

				string json = "{\"percent\":" + percent + ",\"duration_ms\":" + duration + "}";
				
				string responseString;
				try
				{
				   	string url = "https://api-http.littlebitscloud.cc/v2/devices/" + device + "/output";
                    			responseString = client.UploadString(url, "POST", json);
				}
				catch (Exception e)
				{
				    throw new Exception(e.Message);
				}
			}
		}
		
		private static bool ValidateRemoteCertificate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
		{
		    // If the certificate is a valid, signed certificate, return true.
		    if (error == SslPolicyErrors.None)
		    {
			return true;
		    }

		    Console.WriteLine("X509Certificate [{0}] Policy Error: '{1}'",
			cert.Subject,
			error.ToString());

		    return false;
		}
	}
}

