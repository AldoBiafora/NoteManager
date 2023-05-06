using It.Plugin.IDC.Core.Exceptions;
using It.Plugin.IDC.Core;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Script.Serialization;
using System.Text;

namespace It.Plugin.IDC.Core
{
	public class MobytApiClient
	{

		private string UserKey;
		private string AccessToken;
		private readonly string Endpoint;
		private readonly string Proxy;
		//private readonly ILog Logger = LogManager.GetLogger(nameof(MobytApiClient));

		public class SendResponse
		{
			public string result { get; set; }
			public string order_id { get; set; }
			public int total_sent { get; set; }
			public string internal_order_id { get; set; }
			public int CountSentSMS { get { return total_sent; } }
		}

		public MobytApiClient(string username, string password, string endpoint, string proxy = null)
		{

			string missing = null;
			if (string.IsNullOrWhiteSpace(username)) missing = nameof(username);
			else if (string.IsNullOrWhiteSpace(password)) missing = nameof(password);
			else if (string.IsNullOrWhiteSpace(endpoint)) missing = nameof(endpoint);
			if (missing != null) throw new ArgumentNullException(missing);

			//
			string http = "http://", https = "https://";
			if (!endpoint.StartsWith(http) && !endpoint.StartsWith(https))
			{

				throw new ArgumentException($"Mobyt enpoint '{endpoint}' is invalid, as it should start with {http} or {https}");

			}
			if (endpoint.EndsWith("/")) endpoint = endpoint.Substring(0, endpoint.Length - 1);
			Endpoint = endpoint;
			Proxy = proxy;

			using (HttpClient client = CreateHttpClient())
			{
				Uri uri = new Uri($"{Endpoint}/API/v1.0/REST/token?username={username}&password={password}");

				var resp1 = client.GetAsync(uri.ToString()).Result;
				if (resp1.IsSuccessStatusCode)
				{

					resp1.EnsureSuccessStatusCode();
					var resp = resp1.Content.ReadAsStringAsync().Result;

					if (String.IsNullOrEmpty(resp))
						throw new MobytApiClientLoginFailedException("Login Failed", username);

					try
					{
						var tmp = resp.Split(new char[] { ';' });

						UserKey = tmp[0];
						AccessToken = tmp[1];
					}
					catch (Exception ex)
					{
						throw new MobytApiClientLoginErrorPArsingException("Error parsing login response", ex.ToString());
					}

				}

			}

		}

		public void Logout()
		{
			UserKey = null;
			AccessToken = null;
		}

		public SendResponse SendSMS(string sender, string recipient, string message)
		{
			if (!IsConnected())
				throw new MobytApiClientNotAuthenticatedException("Client not authenticated for API call to mobyt", "");

			using (HttpClient client = CreateHttpClient())
			{
				client.DefaultRequestHeaders.Add("user_key", UserKey);
				client.DefaultRequestHeaders.Add("Access_token", AccessToken);
                var resp = client.PostAsJsonAsync($"{Endpoint}/API/v1.0/REST/sms",
                    new
                    {
                        recipient = new string[] { recipient },
                        message = message,
                        message_type = "N",
                        sender = sender
                    }).Result;

                if (resp.IsSuccessStatusCode)
                {
                    var rtn = JsonConvert.DeserializeObject<SendResponse>(resp.Content.ReadAsStringAsync().Result);
                    return rtn;
                }
                else
                    throw new MobytApiClientSendingSMSException(resp.Content.ReadAsStringAsync().Result, "");
            }
		}

		public bool IsConnected() => !string.IsNullOrWhiteSpace(UserKey) && !string.IsNullOrWhiteSpace(AccessToken);

		private HttpClient CreateHttpClient()
		{

			if (string.IsNullOrWhiteSpace(Proxy))
			{

				return new HttpClient();

			}

			HttpClientHandler handler = new HttpClientHandler()
			{
				Proxy = new WebProxy(Proxy, false),
				UseProxy = true
			};

			return new HttpClient(handler);

		}

	}
}
