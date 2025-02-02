using System;
using System.Collections.Generic;
using System.Threading;

namespace TaleWorlds.Library
{
	// Token: 0x02000087 RID: 135
	public sealed class SingleThreadedSynchronizationContext : SynchronizationContext
	{
		// Token: 0x060004A3 RID: 1187 RVA: 0x0000FC1E File Offset: 0x0000DE1E
		public SingleThreadedSynchronizationContext()
		{
			this._worksLock = new object();
			this._futureWorks = new List<SingleThreadedSynchronizationContext.WorkRequest>(100);
			this._currentWorks = new List<SingleThreadedSynchronizationContext.WorkRequest>(100);
			this._mainThreadId = Thread.CurrentThread.ManagedThreadId;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0000FC5C File Offset: 0x0000DE5C
		public override void Send(SendOrPostCallback callback, object state)
		{
			if (this._mainThreadId == Thread.CurrentThread.ManagedThreadId)
			{
				callback.DynamicInvokeWithLog(new object[]
				{
					state
				});
				return;
			}
			using (ManualResetEvent manualResetEvent = new ManualResetEvent(false))
			{
				object worksLock = this._worksLock;
				lock (worksLock)
				{
					this._futureWorks.Add(new SingleThreadedSynchronizationContext.WorkRequest(callback, state, manualResetEvent));
				}
				manualResetEvent.WaitOne();
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0000FCF4 File Offset: 0x0000DEF4
		public override void Post(SendOrPostCallback callback, object state)
		{
			SingleThreadedSynchronizationContext.WorkRequest item = new SingleThreadedSynchronizationContext.WorkRequest(callback, state, null);
			object worksLock = this._worksLock;
			lock (worksLock)
			{
				this._futureWorks.Add(item);
			}
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0000FD44 File Offset: 0x0000DF44
		public void Tick()
		{
			object worksLock = this._worksLock;
			lock (worksLock)
			{
				List<SingleThreadedSynchronizationContext.WorkRequest> currentWorks = this._currentWorks;
				this._currentWorks = this._futureWorks;
				this._futureWorks = currentWorks;
			}
			foreach (SingleThreadedSynchronizationContext.WorkRequest workRequest in this._currentWorks)
			{
				workRequest.Invoke();
			}
			this._currentWorks.Clear();
		}

		// Token: 0x04000163 RID: 355
		private List<SingleThreadedSynchronizationContext.WorkRequest> _futureWorks;

		// Token: 0x04000164 RID: 356
		private List<SingleThreadedSynchronizationContext.WorkRequest> _currentWorks;

		// Token: 0x04000165 RID: 357
		private readonly object _worksLock;

		// Token: 0x04000166 RID: 358
		private readonly int _mainThreadId;

		// Token: 0x020000D5 RID: 213
		private struct WorkRequest
		{
			// Token: 0x06000715 RID: 1813 RVA: 0x00016497 File Offset: 0x00014697
			public WorkRequest(SendOrPostCallback callback, object state, ManualResetEvent waitHandle = null)
			{
				this._callback = callback;
				this._state = state;
				this._waitHandle = waitHandle;
			}

			// Token: 0x06000716 RID: 1814 RVA: 0x000164AE File Offset: 0x000146AE
			public void Invoke()
			{
				this._callback.DynamicInvokeWithLog(new object[]
				{
					this._state
				});
				ManualResetEvent waitHandle = this._waitHandle;
				if (waitHandle == null)
				{
					return;
				}
				waitHandle.Set();
			}

			// Token: 0x040002A4 RID: 676
			private readonly SendOrPostCallback _callback;

			// Token: 0x040002A5 RID: 677
			private readonly object _state;

			// Token: 0x040002A6 RID: 678
			private readonly ManualResetEvent _waitHandle;
		}
	}
}
