using System;
using System.Net;
using System.Net.Http;

namespace TaleWorlds.Library.Http
{
	// Token: 0x020000AA RID: 170
	public class HttpGetRequest : IHttpRequestTask
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0001429E File Offset: 0x0001249E
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x000142A6 File Offset: 0x000124A6
		public HttpRequestTaskState State { get; private set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x000142AF File Offset: 0x000124AF
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x000142B7 File Offset: 0x000124B7
		public bool Successful { get; private set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x000142C0 File Offset: 0x000124C0
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x000142C8 File Offset: 0x000124C8
		public string ResponseData { get; private set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x000142D1 File Offset: 0x000124D1
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x000142D9 File Offset: 0x000124D9
		public HttpStatusCode ResponseStatusCode { get; private set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x000142E2 File Offset: 0x000124E2
		// (set) Token: 0x06000635 RID: 1589 RVA: 0x000142EA File Offset: 0x000124EA
		public Exception Exception { get; private set; }

		// Token: 0x06000636 RID: 1590 RVA: 0x000142F3 File Offset: 0x000124F3
		public HttpGetRequest(HttpClient httpClient, string address) : this(httpClient, address, new Version("1.1"))
		{
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00014307 File Offset: 0x00012507
		public HttpGetRequest(HttpClient httpClient, string address, Version version)
		{
			this._versionToUse = version;
			this._address = address;
			this._httpClient = httpClient;
			this.State = HttpRequestTaskState.NotStarted;
			this.ResponseData = "";
			this.ResponseStatusCode = HttpStatusCode.OK;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00014341 File Offset: 0x00012541
		private void SetFinishedAsSuccessful(string responseData, HttpStatusCode statusCode)
		{
			this.Successful = true;
			this.ResponseData = responseData;
			this.ResponseStatusCode = statusCode;
			this.State = HttpRequestTaskState.Finished;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0001435F File Offset: 0x0001255F
		private void SetFinishedAsUnsuccessful(Exception e)
		{
			this.Successful = false;
			this.Exception = e;
			this.State = HttpRequestTaskState.Finished;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00014376 File Offset: 0x00012576
		public void Start()
		{
			this.DoTask();
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00014380 File Offset: 0x00012580
		private async void DoTask()
		{
			this.State = HttpRequestTaskState.Working;
			try
			{
				using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, this._address))
				{
					requestMessage.Version = this._versionToUse;
					requestMessage.Headers.Add("Accept", "application/json");
					requestMessage.Headers.Add("UserAgent", "TaleWorlds Client");
					HttpResponseMessage httpResponseMessage = await this._httpClient.SendAsync(requestMessage);
					using (HttpResponseMessage response = httpResponseMessage)
					{
						bool isSuccessStatusCode = response.IsSuccessStatusCode;
						response.EnsureSuccessStatusCode();
						Debug.Print(string.Concat(new object[]
						{
							"Protocol version used for get request to ",
							this._address,
							" is: ",
							response.Version
						}), 0, Debug.DebugColor.White, 17592186044416UL);
						using (HttpContent content = response.Content)
						{
							this.SetFinishedAsSuccessful(await content.ReadAsStringAsync(), response.StatusCode);
						}
						HttpContent content = null;
					}
					HttpResponseMessage response = null;
				}
				HttpRequestMessage requestMessage = null;
			}
			catch (Exception finishedAsUnsuccessful)
			{
				this.SetFinishedAsUnsuccessful(finishedAsUnsuccessful);
			}
		}

		// Token: 0x040001CC RID: 460
		private const int BufferSize = 1024;

		// Token: 0x040001CD RID: 461
		private HttpClient _httpClient;

		// Token: 0x040001CE RID: 462
		private readonly string _address;

		// Token: 0x040001D4 RID: 468
		private Version _versionToUse;
	}
}
