using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaleWorlds.Diamond
{
	// Token: 0x02000028 RID: 40
	public class ThreadedClient : IClient
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00003499 File Offset: 0x00001699
		public ILoginAccessProvider AccessProvider
		{
			get
			{
				return this._client.AccessProvider;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000CB RID: 203 RVA: 0x000034A6 File Offset: 0x000016A6
		public bool IsInCriticalState
		{
			get
			{
				return this._client.IsInCriticalState;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000CC RID: 204 RVA: 0x000034B3 File Offset: 0x000016B3
		public long AliveCheckTimeInMiliSeconds
		{
			get
			{
				return this._client.AliveCheckTimeInMiliSeconds;
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000034C0 File Offset: 0x000016C0
		public ThreadedClient(IClient client)
		{
			this._client = client;
			this._tasks = new Queue<ThreadedClientTask>();
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000034DC File Offset: 0x000016DC
		public void Tick()
		{
			ThreadedClientTask threadedClientTask = null;
			Queue<ThreadedClientTask> tasks = this._tasks;
			lock (tasks)
			{
				if (this._tasks.Count > 0)
				{
					threadedClientTask = this._tasks.Dequeue();
				}
			}
			if (threadedClientTask != null)
			{
				threadedClientTask.DoJob();
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000353C File Offset: 0x0000173C
		void IClient.HandleMessage(Message message)
		{
			ThreadedClientHandleMessageTask item = new ThreadedClientHandleMessageTask(this._client, message);
			Queue<ThreadedClientTask> tasks = this._tasks;
			lock (tasks)
			{
				this._tasks.Enqueue(item);
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003590 File Offset: 0x00001790
		void IClient.OnConnected()
		{
			ThreadedClientConnectedTask item = new ThreadedClientConnectedTask(this._client);
			Queue<ThreadedClientTask> tasks = this._tasks;
			lock (tasks)
			{
				this._tasks.Enqueue(item);
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000035E4 File Offset: 0x000017E4
		void IClient.OnDisconnected()
		{
			ThreadedClientDisconnectedTask item = new ThreadedClientDisconnectedTask(this._client);
			Queue<ThreadedClientTask> tasks = this._tasks;
			lock (tasks)
			{
				this._tasks.Enqueue(item);
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003638 File Offset: 0x00001838
		void IClient.OnCantConnect()
		{
			ThreadedClientCantConnectTask item = new ThreadedClientCantConnectTask(this._client);
			Queue<ThreadedClientTask> tasks = this._tasks;
			lock (tasks)
			{
				this._tasks.Enqueue(item);
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000368C File Offset: 0x0000188C
		public Task<bool> CheckConnection()
		{
			return this._client.CheckConnection();
		}

		// Token: 0x04000038 RID: 56
		private IClient _client;

		// Token: 0x04000039 RID: 57
		private Queue<ThreadedClientTask> _tasks;
	}
}
