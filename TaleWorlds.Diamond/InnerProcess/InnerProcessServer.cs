using System;
using System.Collections.Generic;

namespace TaleWorlds.Diamond.InnerProcess
{
	// Token: 0x02000053 RID: 83
	public abstract class InnerProcessServer<T> : IInnerProcessServer where T : InnerProcessServerSession, new()
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x000059C8 File Offset: 0x00003BC8
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x000059D0 File Offset: 0x00003BD0
		public InnerProcessManager InnerProcessManager { get; private set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x000059D9 File Offset: 0x00003BD9
		public IEnumerable<T> Sessions
		{
			get
			{
				return this._clientSessions;
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000059E1 File Offset: 0x00003BE1
		protected InnerProcessServer(InnerProcessManager innerProcessManager)
		{
			this.InnerProcessManager = innerProcessManager;
			this._clientSessions = new List<T>();
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000059FB File Offset: 0x00003BFB
		public void Host(int port)
		{
			this.InnerProcessManager.Activate(this, port);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00005A0C File Offset: 0x00003C0C
		InnerProcessServerSession IInnerProcessServer.AddNewConnection(IInnerProcessClient client)
		{
			T t = Activator.CreateInstance<T>();
			this._clientSessions.Add(t);
			return t;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00005A34 File Offset: 0x00003C34
		void IInnerProcessServer.Update()
		{
			foreach (T t in this._clientSessions)
			{
				while (t.HasMessage)
				{
					InnerProcessMessageTask innerProcessMessageTask = t.DequeueMessage();
					SessionCredentials sessionCredentials = innerProcessMessageTask.SessionCredentials;
					Message message = innerProcessMessageTask.Message;
					switch (innerProcessMessageTask.Type)
					{
					case InnerProcessMessageTaskType.Login:
					{
						LoginResult loginResult = this.Login(t, (LoginMessage)message, new InnerProcessConnectionInformation());
						if (loginResult.Successful)
						{
							innerProcessMessageTask.SetFinishedAsSuccessful(loginResult);
						}
						break;
					}
					case InnerProcessMessageTaskType.Message:
						this.HandleMessage(t, sessionCredentials, message);
						innerProcessMessageTask.SetFinishedAsSuccessful(null);
						break;
					case InnerProcessMessageTaskType.Function:
					{
						Tuple<HandlerResult, FunctionResult> tuple = this.CallFunction(t, sessionCredentials, message);
						if (tuple.Item1.IsSuccessful)
						{
							innerProcessMessageTask.SetFinishedAsSuccessful(tuple.Item2);
						}
						break;
					}
					}
				}
			}
			this.OnUpdate();
		}

		// Token: 0x060001EB RID: 491
		protected abstract void HandleMessage(T serverSession, SessionCredentials sessionCredentials, Message message);

		// Token: 0x060001EC RID: 492
		protected abstract Tuple<HandlerResult, FunctionResult> CallFunction(T serverSession, SessionCredentials sessionCredentials, Message message);

		// Token: 0x060001ED RID: 493
		protected abstract LoginResult Login(T serverSession, LoginMessage message, InnerProcessConnectionInformation connectionInformation);

		// Token: 0x060001EE RID: 494 RVA: 0x00005B3C File Offset: 0x00003D3C
		protected virtual void OnUpdate()
		{
		}

		// Token: 0x040000AE RID: 174
		private List<T> _clientSessions;
	}
}
