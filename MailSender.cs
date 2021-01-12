using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SendMail
{
    public class MailSender
    {
		public static async Task<JObject> SendMail(List<MailSendRequest> request)
		{
			var data = JsonConvert.SerializeObject(request);
			JObject JsonResponse = null;

			try
			{

				JToken responseToken;

				var valuesToken = new Dictionary<string, string>
				{
					{"client_id", "MobileTeam"},
					{"client_secret", "2792f5ca-1702-a686-628a-9412358f637d"},
					{"grant_type", "client_credentials"},
					{"scope", "commonapi"}
				};

				using (var client = new HttpClient())
				{
					var content = new FormUrlEncodedContent(valuesToken);
					var response = await client.PostAsync("https://auth.thyteknik.com.tr/connect/token", content);
					string responseString = await response.Content.ReadAsStringAsync();
					JsonResponse = JObject.Parse(responseString);
					responseToken = JsonResponse["access_token"];

					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", responseToken.ToString());
					var stringContent = new StringContent(data, UnicodeEncoding.UTF8, "application/json");

					response = await client.PostAsync("https://api.turkishtechnic.com/" + "commonapi/SendMail", stringContent);
					string outputJson = await response.Content.ReadAsStringAsync();
					JsonResponse = JObject.Parse(outputJson);
				}
			}
			catch
			{
				//Serilog.Log.Error($"Email gonderilemedi {data} -- {ex.Message}");
			}
			return JsonResponse;
		}
	}
}
