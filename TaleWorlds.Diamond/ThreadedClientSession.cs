using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaleWorlds.Diamond
{
	// Token: 0x0200002E RID: 46
	public class ThreadedClientSession : IClientSession
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x0000372F File Offset: 0x0000192F
		public ThreadedClientSession(ThreadedClient threadedClient, IClientSession session, int threadSleepTime)
		{
			this._session = session;
			this._threadedClient = threadedClient;
			this._tasks = new Queue<ThreadedClientSessionTask>();
			this._task = null;
			this._tasBegunJob = false;
			this._threadSleepTime = threadSleepTime;
			this.RefreshTask(null);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003770 File Offset: 0x00001970
		private void RefreshTask(Task previousTask)
		{
			if (previousTask == null || previousTask.IsCompleted)
			{
				Task.Run(async delegate()
				{
					this.ThreadMain();
					await Task.Delay(this._threadSleepTime);
				}).ContinueWith(delegate(Task t)
				{
					this.RefreshTask(t);
				}, TaskContinuationOptions.ExecuteSynchronously);
				return;
			}
			if (previousTask.IsFaulted)
			{
				throw new Exception("ThreadedClientSession.ThreadMain Task is faulted", previousTask.Exception);
			}
			throw new Exception("RefreshTask is called before task is completed");
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000037D4 File Offset: 0x000019D4
		private void ThreadMain()
		{
			this._session.Tick();
			if (!this._tasBegunJob)
			{
				Queue<ThreadedClientSessionTask> tasks = this._tasks;
				lock (tasks)
				{
					if (this._tasks.Count > 0)
					{
						this._task = this._tasks.Dequeue();
					}
				}
				if (this._task != null)
				{
					this._task.BeginJob();
					this._tasBegunJob = true;
				}
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00003860 File Offset: 0x00001A60
		void IClientSession.Connect()
		{
			ThreadedClientSessionConnectTask item = new ThreadedClientSessionConnectTask(this._session);
			Queue<ThreadedClientSessionTask> tasks = this._tasks;
			lock (tasks)
			{
				this._tasks.Enqueue(item);
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000038B4 File Offset: 0x00001AB4
		void IClientSession.Disconnect()
		{
			ThreadedClientSessionDisconnectTask item = new ThreadedClientSessionDisconnectTask(this._session);
			Queue<ThreadedClientSessionTask> tasks = this._tasks;
			lock (tasks)
			{
				this._tasks.Enqueue(item);
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00003908 File Offset: 0x00001B08
		void IClientSession.Tick()
		{
			this._threadedClient.Tick();
			if (this._tasBegunJob)
			{
				this._task.DoMainThreadJob();
				if (this._task.Finished)
				{
					this._task = null;
					this._tasBegunJob = false;
				}
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00003948 File Offset: 0x00001B48
		async Task<LoginResult> IClientSession.Login(LoginMessage message)
		{
			ThreadedClientSessionLoginTask task = new ThreadedClientSessionLoginTask(this._session, message);
			Queue<ThreadedClientSessionTask> tasks = this._tasks;
			lock (tasks)
			{
				this._tasks.Enqueue(task);
			}
			await task.Wait();
			return task.LoginResult;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00003998 File Offset: 0x00001B98
		void IClientSession.SendMessage(Message message)
		{
			ThreadedClientSessionMessageTask item = new ThreadedClientSessionMessageTask(this._session, message);
			Queue<ThreadedClientSessionTask> tasks = this._tasks;
			lock (tasks)
			{
				this._tasks.Enqueue(item);
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000039EC File Offset: 0x00001BEC
		async Task<TReturn> IClientSession.CallFunction<TReturn>(Message message)
		{
			ThreadedClientSessionFunctionTask task = new ThreadedClientSessionFunctionTask(this._session, message);
			Queue<ThreadedClientSessionTask> tasks = this._tasks;
			lock (tasks)
			{
				this._tasks.Enqueue(task);
			}
			await task.Wait();
			return (TReturn)((object)task.FunctionResult);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00003A39 File Offset: 0x00001C39
		Task<bool> IClientSession.CheckConnection()
		{
			return this._session.CheckConnection();
		}

		// Token: 0x0400003C RID: 60
		private IClientSession _session;

		// Token: 0x0400003D RID: 61
		private ThreadedClient _threadedClient;

		// Token: 0x0400003E RID: 62
		private Queue<ThreadedClientSessionTask> _tasks;

		// Token: 0x0400003F RID: 63
		private ThreadedClientSessionTask _task;

		// Token: 0x04000040 RID: 64
		private volatile bool _tasBegunJob;

		// Token: 0x04000041 RID: 65
		private readonly int _threadSleepTime;
	}
}
