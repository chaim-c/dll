using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;
using TaleWorlds.Library;
using TaleWorlds.Library.Http;

namespace TaleWorlds.Diamond.Rest
{
	// Token: 0x02000047 RID: 71
	internal class ClientRestSessionTask
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00004D3E File Offset: 0x00002F3E
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00004D46 File Offset: 0x00002F46
		public RestRequestMessage RestRequestMessage { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00004D4F File Offset: 0x00002F4F
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00004D57 File Offset: 0x00002F57
		public bool Finished { get; private set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00004D60 File Offset: 0x00002F60
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00004D68 File Offset: 0x00002F68
		public bool Successful { get; private set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00004D71 File Offset: 0x00002F71
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00004D79 File Offset: 0x00002F79
		public IHttpRequestTask Request { get; private set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00004D82 File Offset: 0x00002F82
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00004D8A File Offset: 0x00002F8A
		public RestResponse RestResponse { get; private set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00004D93 File Offset: 0x00002F93
		public bool IsCompletelyFinished
		{
			get
			{
				return !this._willTryAgain && this._resultExamined && this.Request.State == HttpRequestTaskState.Finished;
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00004DB8 File Offset: 0x00002FB8
		public ClientRestSessionTask(RestRequestMessage restRequestMessage)
		{
			this.RestRequestMessage = restRequestMessage;
			this._taskCompletionSource = new TaskCompletionSource<bool>();
			this._sw = new Stopwatch();
			if (this.RestRequestMessage is RestDataRequestMessage)
			{
				RestDataRequestMessage restDataRequestMessage = (RestDataRequestMessage)this.RestRequestMessage;
				this._messageName = restDataRequestMessage.MessageName;
				return;
			}
			this._messageName = this.RestRequestMessage.TypeName;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00004E74 File Offset: 0x00003074
		public void SetRequestData(byte[] userCertificate, string address, ushort port, bool isSecure, IHttpDriver networkClient)
		{
			this.RestRequestMessage.UserCertificate = userCertificate;
			this._requestAddress = address;
			this._requestPort = port;
			this._isSecure = isSecure;
			this._postData = this.RestRequestMessage.SerializeAsJson();
			this._networkClient = networkClient;
			this.CreateAndSetRequest();
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00004EC4 File Offset: 0x000030C4
		private void DetermineNextTry()
		{
			if (this._sw.ElapsedMilliseconds >= (long)ClientRestSessionTask.RequestRetryTimeout)
			{
				this._willTryAgain = false;
				Debug.Print("Retrying http post request, iteration count: " + this._currentIterationCount, 0, Debug.DebugColor.White, 17592186044416UL);
				this.CreateAndSetRequest();
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00004F18 File Offset: 0x00003118
		private static string GetCode(WebException webException)
		{
			if (webException.Response != null && webException.Response is HttpWebResponse)
			{
				return ((HttpWebResponse)webException.Response).StatusCode.ToString();
			}
			return "NoCode";
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00004F60 File Offset: 0x00003160
		private void ExamineResult()
		{
			if (!this.Request.Successful)
			{
				bool flag = false;
				if (this.Request.Exception != null && this.RetryableExceptions.Any((Type e) => e == this.Request.Exception.GetType()))
				{
					object[] array = new object[6];
					array[0] = "Http Post Request with message(";
					array[1] = this.RestRequestMessage;
					array[2] = ")  failed. Retrying: (";
					int num = 3;
					Exception exception = this.Request.Exception;
					array[num] = ((exception != null) ? exception.GetType() : null);
					array[4] = ") ";
					array[5] = this.Request.Exception;
					Debug.Print(string.Concat(array), 0, Debug.DebugColor.White, 17592186044416UL);
					flag = true;
				}
				else
				{
					object[] array2 = new object[6];
					array2[0] = "Http Post Request with message(";
					array2[1] = this.RestRequestMessage;
					array2[2] = ")  failed. Exception: (";
					int num2 = 3;
					Exception exception2 = this.Request.Exception;
					array2[num2] = ((exception2 != null) ? exception2.GetType() : null);
					array2[4] = ") ";
					array2[5] = this.Request.Exception;
					Debug.Print(string.Concat(array2), 0, Debug.DebugColor.White, 17592186044416UL);
				}
				if (this.Request != null && this.Request.Exception != null)
				{
					this.PrintExceptions(this.Request.Exception);
				}
				if (flag)
				{
					if (this._currentIterationCount < this._maxIterationCount)
					{
						this._sw.Restart();
						this._willTryAgain = true;
						this._currentIterationCount++;
						Debug.Print(string.Concat(new object[]
						{
							"Http post request(",
							this.RestRequestMessage,
							")  will try again, iteration count: ",
							this._currentIterationCount
						}), 0, Debug.DebugColor.White, 17592186044416UL);
					}
					else
					{
						this._willTryAgain = false;
						Debug.Print("Passed max retry count for http post request(" + this.RestRequestMessage + ") ", 0, Debug.DebugColor.White, 17592186044416UL);
					}
				}
				else
				{
					Debug.Print("Http post request(" + this.RestRequestMessage + ")  will not try again due to exception type!", 0, Debug.DebugColor.White, 17592186044416UL);
					this._willTryAgain = false;
				}
			}
			else if (this._currentIterationCount > 0)
			{
				Debug.Print(string.Concat(new object[]
				{
					"Http post request(",
					this.RestRequestMessage,
					") is successful with iteration count: ",
					this._currentIterationCount
				}), 0, Debug.DebugColor.White, 17592186044416UL);
			}
			this._resultExamined = true;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000051CC File Offset: 0x000033CC
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
				if (!this._resultExamined)
				{
					this.ExamineResult();
					return;
				}
				this.DetermineNextTry();
				break;
			default:
				return;
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000521C File Offset: 0x0000341C
		public async Task WaitUntilFinished()
		{
			Debug.Print("ClientRestSessionTask::WaitUntilFinished::" + this._messageName, 0, Debug.DebugColor.White, 17592186044416UL);
			await this._taskCompletionSource.Task;
			Debug.Print("ClientRestSessionTask::WaitUntilFinished::" + this._messageName + " done", 0, Debug.DebugColor.White, 17592186044416UL);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00005264 File Offset: 0x00003464
		public void SetFinishedAsSuccessful(RestResponse restResponse)
		{
			Debug.Print("ClientRestSessionTask::SetFinishedAsSuccessful::" + this._messageName, 0, Debug.DebugColor.White, 17592186044416UL);
			this.RestResponse = restResponse;
			this.Successful = true;
			this.Finished = true;
			this._taskCompletionSource.SetResult(true);
			Debug.Print("ClientRestSessionTask::SetFinishedAsSuccessful::" + this._messageName + " done", 0, Debug.DebugColor.White, 17592186044416UL);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000052D9 File Offset: 0x000034D9
		public void SetFinishedAsFailed()
		{
			this.SetFinishedAsFailed(null);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000052E4 File Offset: 0x000034E4
		public void SetFinishedAsFailed(RestResponse restResponse)
		{
			Debug.Print("ClientRestSessionTask::SetFinishedAsFailed::" + this._messageName, 0, Debug.DebugColor.White, 17592186044416UL);
			this.RestResponse = restResponse;
			this.Successful = false;
			this.Finished = true;
			this._taskCompletionSource.SetResult(true);
			Debug.Print("ClientRestSessionTask::SetFinishedAsFailed:: " + this._messageName + " done", 0, Debug.DebugColor.White, 17592186044416UL);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000535C File Offset: 0x0000355C
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
				"/Data/ProcessMessage"
			});
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection.Add("url", text2);
			nameValueCollection.Add("body", this._postData);
			nameValueCollection.Add("verb", "POST");
			this.Request = this._networkClient.CreateHttpPostRequestTask(text2, this._postData, true);
			this._resultExamined = false;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00005408 File Offset: 0x00003608
		private void PrintExceptions(Exception e)
		{
			if (e != null)
			{
				Exception ex = e;
				int num = 0;
				while (ex != null)
				{
					Debug.Print(string.Concat(new object[]
					{
						"Exception #",
						num,
						": ",
						ex.Message,
						" ||| StackTrace: ",
						ex.InnerException
					}), 0, Debug.DebugColor.White, 17592186044416UL);
					ex = ex.InnerException;
					num++;
				}
			}
		}

		// Token: 0x0400007F RID: 127
		private static readonly int RequestRetryTimeout = 1000;

		// Token: 0x04000080 RID: 128
		private readonly Type[] RetryableExceptions = new Type[]
		{
			typeof(HttpRequestException),
			typeof(TaskCanceledException),
			typeof(IOException),
			typeof(SocketException),
			typeof(InvalidOperationException)
		};

		// Token: 0x04000085 RID: 133
		public bool _willTryAgain;

		// Token: 0x04000087 RID: 135
		private string _requestAddress;

		// Token: 0x04000088 RID: 136
		private ushort _requestPort;

		// Token: 0x04000089 RID: 137
		private string _postData;

		// Token: 0x0400008A RID: 138
		private bool _isSecure;

		// Token: 0x0400008B RID: 139
		private string _messageName;

		// Token: 0x0400008C RID: 140
		private int _maxIterationCount = 5;

		// Token: 0x0400008D RID: 141
		private int _currentIterationCount;

		// Token: 0x0400008E RID: 142
		private Stopwatch _sw;

		// Token: 0x0400008F RID: 143
		private TaskCompletionSource<bool> _taskCompletionSource;

		// Token: 0x04000090 RID: 144
		private IHttpDriver _networkClient;

		// Token: 0x04000091 RID: 145
		private bool _resultExamined;
	}
}
