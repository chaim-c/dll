using System;
using System.Net.Http;
using System.Text;

namespace TaleWorlds.Library.Http
{
	// Token: 0x020000AB RID: 171
	public class HttpPostRequest : IHttpRequestTask
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x000143B9 File Offset: 0x000125B9
		// (set) Token: 0x0600063D RID: 1597 RVA: 0x000143C1 File Offset: 0x000125C1
		public HttpRequestTaskState State { get; private set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x000143CA File Offset: 0x000125CA
		// (set) Token: 0x0600063F RID: 1599 RVA: 0x000143D2 File Offset: 0x000125D2
		public bool Successful { get; private set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x000143DB File Offset: 0x000125DB
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x000143E3 File Offset: 0x000125E3
		public string ResponseData { get; private set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x000143EC File Offset: 0x000125EC
		// (set) Token: 0x06000643 RID: 1603 RVA: 0x000143F4 File Offset: 0x000125F4
		public Exception Exception { get; private set; }

		// Token: 0x06000644 RID: 1604 RVA: 0x000143FD File Offset: 0x000125FD
		public HttpPostRequest(HttpClient httpClient, string address, string postData) : this(httpClient, address, postData, new Version("1.1"))
		{
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00014412 File Offset: 0x00012612
		public HttpPostRequest(HttpClient httpClient, string address, string postData, Version version)
		{
			this._httpClient = httpClient;
			this._postData = postData;
			this._address = address;
			this.State = HttpRequestTaskState.NotStarted;
			this.ResponseData = "";
			this._versionToUse = version;
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00014449 File Offset: 0x00012649
		private void SetFinishedAsSuccessful(string responseData)
		{
			this.Successful = true;
			this.ResponseData = responseData;
			this.State = HttpRequestTaskState.Finished;
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00014460 File Offset: 0x00012660
		private void SetFinishedAsUnsuccessful(Exception e)
		{
			this.Successful = false;
			this.Exception = e;
			this.State = HttpRequestTaskState.Finished;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00014477 File Offset: 0x00012677
		public void Start()
		{
			this.DoTask();
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00014480 File Offset: 0x00012680
		private async void DoTask()
		{
			this.State = HttpRequestTaskState.Working;
			try
			{
				Debug.Print("Http Post Request to " + this._address, 0, Debug.DebugColor.White, 17592186044416UL);
				using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, this._address))
				{
					requestMessage.Version = this._versionToUse;
					requestMessage.Headers.Add("Accept", "application/json");
					requestMessage.Headers.Add("UserAgent", "TaleWorlds Client");
					requestMessage.Content = new StringContent(this._postData, Encoding.Unicode, "application/json");
					HttpResponseMessage httpResponseMessage = await this._httpClient.SendAsync(requestMessage);
					using (HttpResponseMessage response = httpResponseMessage)
					{
						bool isSuccessStatusCode = response.IsSuccessStatusCode;
						response.EnsureSuccessStatusCode();
						Debug.Print(string.Concat(new object[]
						{
							"Protocol version used for post request to ",
							this._address,
							" is: ",
							response.Version
						}), 0, Debug.DebugColor.White, 17592186044416UL);
						using (HttpContent content = response.Content)
						{
							this.SetFinishedAsSuccessful(await content.ReadAsStringAsync());
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

		// Token: 0x040001D5 RID: 469
		private HttpClient _httpClient;

		// Token: 0x040001D6 RID: 470
		private readonly string _address;

		// Token: 0x040001D7 RID: 471
		private string _postData;

		// Token: 0x040001DC RID: 476
		private Version _versionToUse;
	}
}
