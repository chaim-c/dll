using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TaleWorlds.Library;
using TaleWorlds.Library.Http;

namespace TaleWorlds.Diamond.Rest
{
	// Token: 0x02000046 RID: 70
	public class ClientRestSession : IClientSession
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00004730 File Offset: 0x00002930
		// (set) Token: 0x06000171 RID: 369 RVA: 0x00004738 File Offset: 0x00002938
		public bool IsConnected { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00004741 File Offset: 0x00002941
		// (set) Token: 0x06000173 RID: 371 RVA: 0x00004749 File Offset: 0x00002949
		public IClient Client { get; private set; }

		// Token: 0x06000174 RID: 372 RVA: 0x00004754 File Offset: 0x00002954
		public ClientRestSession(IClient client, string address, ushort port, bool isSecure, IHttpDriver platformNetworkClient)
		{
			this.Client = client;
			this._sessionInitialized = false;
			this._isSecure = isSecure;
			this._platformNetworkClient = platformNetworkClient;
			this.ResetTimer();
			this._address = address;
			this._port = port;
			this._messageTaskQueue = new Queue<ClientRestSessionTask>();
			this._currentConnectionResultType = ClientRestSession.ConnectionResultType.None;
			this._restDataJsonConverter = new RestDataJsonConverter();
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000047B6 File Offset: 0x000029B6
		private void ResetTimer()
		{
			this._timer = new Stopwatch();
			this._timer.Start();
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000047D0 File Offset: 0x000029D0
		private void AssignRequestJob(ClientRestSessionTask requestMessageTask)
		{
			RestRequestMessage restRequestMessage = requestMessageTask.RestRequestMessage;
			bool flag = false;
			if (restRequestMessage is ConnectMessage)
			{
				if (!this.IsConnected)
				{
					flag = true;
				}
			}
			else if (restRequestMessage is DisconnectMessage)
			{
				if (this.IsConnected)
				{
					flag = true;
				}
			}
			else if (this.IsConnected)
			{
				flag = true;
			}
			if (flag)
			{
				this._currentMessageTask = requestMessageTask;
				this._currentMessageTask.SetRequestData(this._userCertificate, this._address, this._port, this._isSecure, this._platformNetworkClient);
				restRequestMessage.SerializeAsJson();
				this._lastRequestOperationTime = this._timer.ElapsedMilliseconds;
				return;
			}
			Debug.Print("Setting new request message as failed because can't assign it", 0, Debug.DebugColor.White, 17592186044416UL);
			requestMessageTask.SetFinishedAsFailed();
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00004880 File Offset: 0x00002A80
		private void RemoveRequestJob()
		{
			this._currentMessageTask = null;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000488C File Offset: 0x00002A8C
		void IClientSession.Tick()
		{
			this.TryAssignJob();
			if (this._currentMessageTask != null)
			{
				this._currentMessageTask.Tick();
				if (this._currentMessageTask.IsCompletelyFinished)
				{
					if (this._currentMessageTask.Request.Successful)
					{
						if (this._currentMessageTask.RestRequestMessage is ConnectMessage)
						{
							this._currentConnectionResultType = ClientRestSession.ConnectionResultType.Connected;
							this._currentMessageTask.SetFinishedAsSuccessful(null);
						}
						else if (this._currentMessageTask.RestRequestMessage is DisconnectMessage)
						{
							this._currentConnectionResultType = ClientRestSession.ConnectionResultType.Disconnected;
							this._currentMessageTask.SetFinishedAsSuccessful(null);
						}
						else
						{
							string responseData = this._currentMessageTask.Request.ResponseData;
							if (!string.IsNullOrEmpty(responseData))
							{
								RestResponse restResponse = JsonConvert.DeserializeObject<RestResponse>(responseData, new JsonConverter[]
								{
									this._restDataJsonConverter
								});
								if (restResponse.Successful)
								{
									this._userCertificate = restResponse.UserCertificate;
									this._currentMessageTask.SetFinishedAsSuccessful(restResponse);
									while (restResponse.RemainingMessageCount > 0)
									{
										RestResponseMessage restResponseMessage = restResponse.TryDequeueMessage();
										this.HandleMessage(restResponseMessage.GetMessage());
									}
								}
								else
								{
									this._currentConnectionResultType = ClientRestSession.ConnectionResultType.Disconnected;
									Debug.Print("Setting current request message as failed because server returned unsuccessful response(" + restResponse.SuccessfulReason + ")", 0, Debug.DebugColor.White, 17592186044416UL);
									this._currentMessageTask.SetFinishedAsFailed(restResponse);
								}
							}
							else
							{
								this._currentConnectionResultType = ClientRestSession.ConnectionResultType.Disconnected;
								Debug.Print("Setting current request message as failed because server returned empty response", 0, Debug.DebugColor.White, 17592186044416UL);
								this._currentMessageTask.SetFinishedAsFailed();
							}
						}
					}
					else
					{
						if (this._currentMessageTask.RestRequestMessage is ConnectMessage)
						{
							this._currentConnectionResultType = ClientRestSession.ConnectionResultType.CantConnect;
						}
						else
						{
							this._currentConnectionResultType = ClientRestSession.ConnectionResultType.Disconnected;
						}
						Debug.Print("Setting current request message as failed because server request is failed", 0, Debug.DebugColor.White, 17592186044416UL);
						this._currentMessageTask.SetFinishedAsFailed();
					}
					this.RemoveRequestJob();
				}
				if (this._currentConnectionResultType != ClientRestSession.ConnectionResultType.None)
				{
					switch (this._currentConnectionResultType)
					{
					case ClientRestSession.ConnectionResultType.Connected:
						this.IsConnected = true;
						this.OnConnected();
						break;
					case ClientRestSession.ConnectionResultType.Disconnected:
						this.IsConnected = false;
						this.ClearMessageTaskQueueDueToDisconnect();
						this._sessionCredentials = null;
						this._sessionInitialized = false;
						this._userCertificate = null;
						this.ResetTimer();
						this.OnDisconnected();
						break;
					case ClientRestSession.ConnectionResultType.CantConnect:
						this._userCertificate = null;
						this.ResetTimer();
						this.OnCantConnect();
						break;
					}
					this._currentConnectionResultType = ClientRestSession.ConnectionResultType.None;
				}
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00004AD0 File Offset: 0x00002CD0
		private void TryAssignJob()
		{
			if (this._currentMessageTask == null)
			{
				if (this._messageTaskQueue.Count > 0)
				{
					ClientRestSessionTask requestMessageTask = this._messageTaskQueue.Dequeue();
					this.AssignRequestJob(requestMessageTask);
					return;
				}
				if (this.IsConnected && this._sessionInitialized && this._timer.ElapsedMilliseconds - this._lastRequestOperationTime > (this.Client.IsInCriticalState ? ClientRestSession.CriticalStateCheckTime : this.Client.AliveCheckTimeInMiliSeconds) && this._userCertificate != null)
				{
					this.AssignRequestJob(new ClientRestSessionTask(new AliveMessage(this._sessionCredentials)));
				}
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00004B6C File Offset: 0x00002D6C
		private void ClearMessageTaskQueueDueToDisconnect()
		{
			foreach (ClientRestSessionTask clientRestSessionTask in this._messageTaskQueue)
			{
				clientRestSessionTask.SetFinishedAsFailed();
			}
			this._messageTaskQueue.Clear();
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00004BC8 File Offset: 0x00002DC8
		public void Connect()
		{
			this.ResetTimer();
			this.SendMessage(new ConnectMessage());
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00004BDB File Offset: 0x00002DDB
		public void Disconnect()
		{
			this.SendMessage(new DisconnectMessage());
			this.ResetTimer();
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00004BEE File Offset: 0x00002DEE
		private void SendMessage(RestRequestMessage message)
		{
			this._messageTaskQueue.Enqueue(new ClientRestSessionTask(message));
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00004C04 File Offset: 0x00002E04
		async Task<LoginResult> IClientSession.Login(LoginMessage message)
		{
			ClientRestSessionTask clientRestSessionTask = new ClientRestSessionTask(new RestObjectRequestMessage(null, message, MessageType.Login));
			this._messageTaskQueue.Enqueue(clientRestSessionTask);
			await clientRestSessionTask.WaitUntilFinished();
			LoginResult result;
			if (!clientRestSessionTask.Successful && !clientRestSessionTask.Request.Successful)
			{
				result = new LoginResult(LoginErrorCode.LoginRequestFailed.ToString(), null);
			}
			else
			{
				RestFunctionResult functionResult = clientRestSessionTask.RestResponse.FunctionResult;
				LoginResult loginResult = null;
				if (functionResult != null)
				{
					loginResult = (LoginResult)functionResult.GetFunctionResult();
					if (clientRestSessionTask.Successful)
					{
						this._sessionCredentials = new SessionCredentials(loginResult.PeerId, loginResult.SessionKey);
						this._sessionInitialized = true;
					}
				}
				result = loginResult;
			}
			return result;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00004C51 File Offset: 0x00002E51
		void IClientSession.SendMessage(Message message)
		{
			this.SendMessage(new RestObjectRequestMessage(this._sessionCredentials, message, MessageType.Message));
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00004C68 File Offset: 0x00002E68
		async Task<TResult> IClientSession.CallFunction<TResult>(Message message)
		{
			ClientRestSessionTask clientRestSessionTask = new ClientRestSessionTask(new RestObjectRequestMessage(this._sessionCredentials, message, MessageType.Function));
			this._messageTaskQueue.Enqueue(clientRestSessionTask);
			await clientRestSessionTask.WaitUntilFinished();
			if (clientRestSessionTask.Successful)
			{
				return (TResult)((object)clientRestSessionTask.RestResponse.FunctionResult.GetFunctionResult());
			}
			throw new Exception("Could not call function with " + message.GetType().Name);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00004CB5 File Offset: 0x00002EB5
		private void HandleMessage(Message message)
		{
			this.Client.HandleMessage(message);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00004CC3 File Offset: 0x00002EC3
		private void OnConnected()
		{
			this.Client.OnConnected();
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00004CD0 File Offset: 0x00002ED0
		private void OnDisconnected()
		{
			this.Client.OnDisconnected();
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00004CDD File Offset: 0x00002EDD
		private void OnCantConnect()
		{
			this.Client.OnCantConnect();
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00004CEC File Offset: 0x00002EEC
		async Task<bool> IClientSession.CheckConnection()
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

		// Token: 0x0400006F RID: 111
		private static readonly long CriticalStateCheckTime = 1000L;

		// Token: 0x04000070 RID: 112
		private readonly Queue<ClientRestSessionTask> _messageTaskQueue;

		// Token: 0x04000071 RID: 113
		private readonly string _address;

		// Token: 0x04000072 RID: 114
		private readonly ushort _port;

		// Token: 0x04000073 RID: 115
		private byte[] _userCertificate;

		// Token: 0x04000074 RID: 116
		private ClientRestSessionTask _currentMessageTask;

		// Token: 0x04000076 RID: 118
		private ClientRestSession.ConnectionResultType _currentConnectionResultType;

		// Token: 0x04000077 RID: 119
		private Stopwatch _timer;

		// Token: 0x04000078 RID: 120
		private long _lastRequestOperationTime;

		// Token: 0x04000079 RID: 121
		private bool _sessionInitialized;

		// Token: 0x0400007A RID: 122
		private SessionCredentials _sessionCredentials;

		// Token: 0x0400007C RID: 124
		private RestDataJsonConverter _restDataJsonConverter;

		// Token: 0x0400007D RID: 125
		private bool _isSecure;

		// Token: 0x0400007E RID: 126
		private IHttpDriver _platformNetworkClient;

		// Token: 0x02000077 RID: 119
		private enum ConnectionResultType
		{
			// Token: 0x0400012C RID: 300
			None,
			// Token: 0x0400012D RID: 301
			Connected,
			// Token: 0x0400012E RID: 302
			Disconnected,
			// Token: 0x0400012F RID: 303
			CantConnect
		}
	}
}
