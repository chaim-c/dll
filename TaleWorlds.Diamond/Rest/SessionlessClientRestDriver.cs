using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TaleWorlds.Library.Http;

namespace TaleWorlds.Diamond.Rest
{
	// Token: 0x0200003F RID: 63
	public class SessionlessClientRestDriver : ISessionlessClientDriver
	{
		// Token: 0x0600013E RID: 318 RVA: 0x00004061 File Offset: 0x00002261
		public SessionlessClientRestDriver(string address, ushort port, bool isSecure, IHttpDriver platformNetworkClient)
		{
			this._isSecure = isSecure;
			this._platformNetworkClient = platformNetworkClient;
			this._address = address;
			this._port = port;
			this._restDataJsonConverter = new RestDataJsonConverter();
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004091 File Offset: 0x00002291
		private void AssignRequestJob(SessionlessClientRestSessionTask requestMessageTask)
		{
			requestMessageTask.SetRequestData(this._address, this._port, this._isSecure, this._platformNetworkClient);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000040B4 File Offset: 0x000022B4
		private void TickTask(SessionlessClientRestSessionTask messageTask)
		{
			messageTask.Tick();
			IHttpRequestTask request = messageTask.Request;
			if (request.State == HttpRequestTaskState.Finished)
			{
				if (request.Successful)
				{
					string responseData = messageTask.Request.ResponseData;
					if (string.IsNullOrEmpty(responseData))
					{
						messageTask.SetFinishedAsFailed();
						return;
					}
					SessionlessRestResponse sessionlessRestResponse = JsonConvert.DeserializeObject<SessionlessRestResponse>(responseData, new JsonConverter[]
					{
						this._restDataJsonConverter
					});
					if (sessionlessRestResponse.Successful)
					{
						messageTask.SetFinishedAsSuccessful(sessionlessRestResponse);
						return;
					}
					messageTask.SetFinishedAsFailed(sessionlessRestResponse);
					return;
				}
				else
				{
					messageTask.SetFinishedAsFailed();
				}
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004130 File Offset: 0x00002330
		private void SendMessage(SessionlessRestRequestMessage message)
		{
			SessionlessClientRestDriver.<>c__DisplayClass8_0 CS$<>8__locals1 = new SessionlessClientRestDriver.<>c__DisplayClass8_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.clientRestSessionTask = new SessionlessClientRestSessionTask(message);
			this.AssignRequestJob(CS$<>8__locals1.clientRestSessionTask);
			Task.Run(delegate()
			{
				SessionlessClientRestDriver.<>c__DisplayClass8_0.<<SendMessage>b__0>d <<SendMessage>b__0>d;
				<<SendMessage>b__0>d.<>4__this = CS$<>8__locals1;
				<<SendMessage>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<SendMessage>b__0>d.<>1__state = -1;
				AsyncTaskMethodBuilder <>t__builder = <<SendMessage>b__0>d.<>t__builder;
				<>t__builder.Start<SessionlessClientRestDriver.<>c__DisplayClass8_0.<<SendMessage>b__0>d>(ref <<SendMessage>b__0>d);
				return <<SendMessage>b__0>d.<>t__builder.Task;
			});
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004174 File Offset: 0x00002374
		void ISessionlessClientDriver.SendMessage(Message message)
		{
			this.SendMessage(new SessionlessRestDataRequestMessage(message, MessageType.Message));
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00004184 File Offset: 0x00002384
		async Task<TResult> ISessionlessClientDriver.CallFunction<TResult>(Message message)
		{
			SessionlessClientRestDriver.<>c__DisplayClass10_0<TResult> CS$<>8__locals1 = new SessionlessClientRestDriver.<>c__DisplayClass10_0<TResult>();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.clientRestSessionTask = new SessionlessClientRestSessionTask(new SessionlessRestDataRequestMessage(message, MessageType.Function));
			this.AssignRequestJob(CS$<>8__locals1.clientRestSessionTask);
			await Task.Run(delegate()
			{
				SessionlessClientRestDriver.<>c__DisplayClass10_0<TResult>.<<TaleWorlds-Diamond-ISessionlessClientDriver-CallFunction>b__0>d <<TaleWorlds-Diamond-ISessionlessClientDriver-CallFunction>b__0>d;
				<<TaleWorlds-Diamond-ISessionlessClientDriver-CallFunction>b__0>d.<>4__this = CS$<>8__locals1;
				<<TaleWorlds-Diamond-ISessionlessClientDriver-CallFunction>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<TaleWorlds-Diamond-ISessionlessClientDriver-CallFunction>b__0>d.<>1__state = -1;
				AsyncTaskMethodBuilder <>t__builder = <<TaleWorlds-Diamond-ISessionlessClientDriver-CallFunction>b__0>d.<>t__builder;
				<>t__builder.Start<SessionlessClientRestDriver.<>c__DisplayClass10_0<TResult>.<<TaleWorlds-Diamond-ISessionlessClientDriver-CallFunction>b__0>d>(ref <<TaleWorlds-Diamond-ISessionlessClientDriver-CallFunction>b__0>d);
				return <<TaleWorlds-Diamond-ISessionlessClientDriver-CallFunction>b__0>d.<>t__builder.Task;
			});
			if (CS$<>8__locals1.clientRestSessionTask.Successful)
			{
				return (TResult)((object)CS$<>8__locals1.clientRestSessionTask.RestResponse.FunctionResult.GetFunctionResult());
			}
			throw new Exception("Could not call function with " + message.GetType().Name);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000041D4 File Offset: 0x000023D4
		async Task<bool> ISessionlessClientDriver.CheckConnection()
		{
			bool result;
			try
			{
				string text = "http://";
				if (this._isSecure)
				{
					text = "https://";
				}
				string url = string.Concat(new object[]
				{
					text,
					this._address,
					":",
					this._port,
					"/Data/Ping"
				});
				await this._platformNetworkClient.HttpGetString(url, false);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x04000056 RID: 86
		private readonly string _address;

		// Token: 0x04000057 RID: 87
		private readonly ushort _port;

		// Token: 0x04000058 RID: 88
		private RestDataJsonConverter _restDataJsonConverter;

		// Token: 0x04000059 RID: 89
		private bool _isSecure;

		// Token: 0x0400005A RID: 90
		private IHttpDriver _platformNetworkClient;
	}
}
