using System;
using System.Collections.Specialized;
using System.Net;
using TaleWorlds.Library;
using TaleWorlds.Library.Http;

namespace TaleWorlds.Diamond.Rest
{
	// Token: 0x02000040 RID: 64
	internal class SessionlessClientRestSessionTask
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00004219 File Offset: 0x00002419
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00004221 File Offset: 0x00002421
		public SessionlessRestRequestMessage RestRequestMessage { get; private set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000147 RID: 327 RVA: 0x0000422A File Offset: 0x0000242A
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00004232 File Offset: 0x00002432
		public bool Finished { get; private set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000149 RID: 329 RVA: 0x0000423B File Offset: 0x0000243B
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00004243 File Offset: 0x00002443
		public bool Successful { get; private set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600014B RID: 331 RVA: 0x0000424C File Offset: 0x0000244C
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00004254 File Offset: 0x00002454
		public IHttpRequestTask Request { get; private set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000425D File Offset: 0x0000245D
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00004265 File Offset: 0x00002465
		public SessionlessRestResponse RestResponse { get; private set; }

		// Token: 0x0600014F RID: 335 RVA: 0x0000426E File Offset: 0x0000246E
		public SessionlessClientRestSessionTask(SessionlessRestRequestMessage restRequestMessage)
		{
			this.RestRequestMessage = restRequestMessage;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00004284 File Offset: 0x00002484
		public void SetRequestData(string address, ushort port, bool isSecure, IHttpDriver networkClient)
		{
			this._requestAddress = address;
			this._requestPort = port;
			this._isSecure = isSecure;
			this._postData = this.RestRequestMessage.SerializeAsJson();
			this._networkClient = networkClient;
			this.CreateAndSetRequest();
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000042BC File Offset: 0x000024BC
		public void Tick()
		{
			switch (this.Request.State)
			{
			case HttpRequestTaskState.NotStarted:
				this.Request.Start();
				return;
			case HttpRequestTaskState.Working:
				break;
			case HttpRequestTaskState.Finished:
				if (!this.Request.Successful && this._currentIterationCount < this._maxIterationCount)
				{
					if (this.Request.Exception != null && this.Request.Exception is WebException)
					{
						WebException ex = (WebException)this.Request.Exception;
						if (ex.Status == WebExceptionStatus.ConnectionClosed || ex.Status == WebExceptionStatus.ConnectFailure || ex.Status == WebExceptionStatus.KeepAliveFailure || ex.Status == WebExceptionStatus.ReceiveFailure || ex.Status == WebExceptionStatus.RequestCanceled || ex.Status == WebExceptionStatus.Timeout || ex.Status == WebExceptionStatus.ProtocolError || ex.Status == WebExceptionStatus.UnknownError)
						{
							Debug.Print("Http Post Request with message failed. Retrying. (" + ex.Status + ")", 0, Debug.DebugColor.White, 17592186044416UL);
							this.CreateAndSetRequest();
						}
						else
						{
							Debug.Print("Http Post Request with message failed. Exception status: " + ex.Status, 0, Debug.DebugColor.White, 17592186044416UL);
						}
					}
					else if (this.Request.Exception != null && this.Request.Exception is InvalidOperationException)
					{
						object[] array = new object[4];
						array[0] = "Http Post Request with message failed. Retrying: (";
						int num = 1;
						Exception exception = this.Request.Exception;
						array[num] = ((exception != null) ? exception.GetType() : null);
						array[2] = ") ";
						array[3] = this.Request.Exception;
						Debug.Print(string.Concat(array), 0, Debug.DebugColor.White, 17592186044416UL);
						this.CreateAndSetRequest();
					}
					else
					{
						object[] array2 = new object[4];
						array2[0] = "Http Post Request with message failed. Exception: (";
						int num2 = 1;
						Exception exception2 = this.Request.Exception;
						array2[num2] = ((exception2 != null) ? exception2.GetType() : null);
						array2[2] = ") ";
						array2[3] = this.Request.Exception;
						Debug.Print(string.Concat(array2), 0, Debug.DebugColor.White, 17592186044416UL);
					}
					this._currentIterationCount++;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000044D4 File Offset: 0x000026D4
		public void SetFinishedAsSuccessful(SessionlessRestResponse restResponse)
		{
			Debug.Print("SessionlessClientRestSessionTask::SetFinishedAsSuccessful", 0, Debug.DebugColor.White, 17592186044416UL);
			this.RestResponse = restResponse;
			this.Successful = true;
			this.Finished = true;
			Debug.Print("SessionlessClientRestSessionTask::SetFinishedAsSuccessful done", 0, Debug.DebugColor.White, 17592186044416UL);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00004522 File Offset: 0x00002722
		public void SetFinishedAsFailed()
		{
			this.SetFinishedAsFailed(null);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000452C File Offset: 0x0000272C
		public void SetFinishedAsFailed(SessionlessRestResponse restResponse)
		{
			Debug.Print("SessionlessClientRestSessionTask::SetFinishedAsFailed", 0, Debug.DebugColor.White, 17592186044416UL);
			this.RestResponse = restResponse;
			this.Successful = false;
			this.Finished = true;
			Debug.Print("SessionlessClientRestSessionTask::SetFinishedAsFailed done", 0, Debug.DebugColor.White, 17592186044416UL);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000457C File Offset: 0x0000277C
		private void CreateAndSetRequest()
		{
			string text = "http://";
			if (this._isSecure)
			{
				text = "https://";
			}
			string text2 = string.Concat(new object[]
			{
				text,
				this._requestAddress,
				":",
				this._requestPort,
				"/SessionlessData/ProcessMessage"
			});
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection.Add("url", text2);
			nameValueCollection.Add("body", this._postData);
			nameValueCollection.Add("verb", "POST");
			this.Request = this._networkClient.CreateHttpPostRequestTask(text2, this._postData, true);
		}

		// Token: 0x04000060 RID: 96
		private string _requestAddress;

		// Token: 0x04000061 RID: 97
		private ushort _requestPort;

		// Token: 0x04000062 RID: 98
		private string _postData;

		// Token: 0x04000063 RID: 99
		private bool _isSecure;

		// Token: 0x04000064 RID: 100
		private int _maxIterationCount = 5;

		// Token: 0x04000065 RID: 101
		private int _currentIterationCount;

		// Token: 0x04000066 RID: 102
		private IHttpDriver _networkClient;
	}
}
