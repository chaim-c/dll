using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaleWorlds.Diamond.InnerProcess
{
	// Token: 0x0200004F RID: 79
	public class InnerProcessClient : IClientSession, IInnerProcessClient
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x000056B0 File Offset: 0x000038B0
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x000056B8 File Offset: 0x000038B8
		public InnerProcessManager InnerProcessManager { get; private set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x000056C1 File Offset: 0x000038C1
		internal bool HasMessage
		{
			get
			{
				return this._receivedMessages.Count > 0;
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000056D1 File Offset: 0x000038D1
		public InnerProcessClient(InnerProcessManager innerProcessManager, IClient client, int port)
		{
			this.InnerProcessManager = innerProcessManager;
			this._receivedMessages = new Queue<Message>();
			this._associatedServerSession = null;
			this._client = client;
			this._port = port;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00005700 File Offset: 0x00003900
		void IClientSession.Connect()
		{
			this.InnerProcessManager.RequestConnection(this, this._port);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00005714 File Offset: 0x00003914
		void IClientSession.Disconnect()
		{
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00005718 File Offset: 0x00003918
		void IClientSession.Tick()
		{
			while (this.HasMessage)
			{
				Message message = this.DequeueMessage();
				this.HandleMessage(message);
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000573D File Offset: 0x0000393D
		void IInnerProcessClient.EnqueueMessage(Message message)
		{
			this._receivedMessages.Enqueue(message);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000574C File Offset: 0x0000394C
		internal Message DequeueMessage()
		{
			Message result = null;
			if (this._receivedMessages.Count > 0)
			{
				result = this._receivedMessages.Dequeue();
			}
			return result;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00005778 File Offset: 0x00003978
		async Task<LoginResult> IClientSession.Login(LoginMessage message)
		{
			InnerProcessMessageTask innerProcessMessageTask = new InnerProcessMessageTask(this._sessionCredentials, message, InnerProcessMessageTaskType.Login);
			this._associatedServerSession.EnqueueMessageTask(innerProcessMessageTask);
			await innerProcessMessageTask.WaitUntilFinished();
			LoginResult loginResult = (LoginResult)innerProcessMessageTask.FunctionResult;
			this._sessionCredentials = new SessionCredentials(loginResult.PeerId, loginResult.SessionKey);
			return loginResult;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000057C8 File Offset: 0x000039C8
		void IClientSession.SendMessage(Message message)
		{
			InnerProcessMessageTask messageTask = new InnerProcessMessageTask(this._sessionCredentials, message, InnerProcessMessageTaskType.Message);
			this._associatedServerSession.EnqueueMessageTask(messageTask);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000057F0 File Offset: 0x000039F0
		async Task<TResult> IClientSession.CallFunction<TResult>(Message message)
		{
			InnerProcessMessageTask innerProcessMessageTask = new InnerProcessMessageTask(this._sessionCredentials, message, InnerProcessMessageTaskType.Function);
			this._associatedServerSession.EnqueueMessageTask(innerProcessMessageTask);
			await innerProcessMessageTask.WaitUntilFinished();
			return (TResult)((object)innerProcessMessageTask.FunctionResult);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000583D File Offset: 0x00003A3D
		void IInnerProcessClient.HandleConnected(InnerProcessServerSession serverSession)
		{
			this._associatedServerSession = serverSession;
			this.OnConnected();
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000584C File Offset: 0x00003A4C
		private void HandleMessage(Message message)
		{
			this._client.HandleMessage(message);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000585A File Offset: 0x00003A5A
		private void OnConnected()
		{
			this._client.OnConnected();
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00005867 File Offset: 0x00003A67
		private void OnCantConnect()
		{
			this._client.OnCantConnect();
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00005874 File Offset: 0x00003A74
		private void OnDisconnected()
		{
			this._client.OnDisconnected();
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00005881 File Offset: 0x00003A81
		public Task<bool> CheckConnection()
		{
			return this._client.CheckConnection();
		}

		// Token: 0x040000A4 RID: 164
		private InnerProcessServerSession _associatedServerSession;

		// Token: 0x040000A5 RID: 165
		private Queue<Message> _receivedMessages;

		// Token: 0x040000A6 RID: 166
		private SessionCredentials _sessionCredentials;

		// Token: 0x040000A7 RID: 167
		private IClient _client;

		// Token: 0x040000A8 RID: 168
		private int _port;
	}
}
