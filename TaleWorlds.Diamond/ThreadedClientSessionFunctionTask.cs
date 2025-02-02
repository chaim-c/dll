using System;
using System.Threading.Tasks;

namespace TaleWorlds.Diamond
{
	// Token: 0x02000034 RID: 52
	internal sealed class ThreadedClientSessionFunctionTask : ThreadedClientSessionTask
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00003C2A File Offset: 0x00001E2A
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00003C32 File Offset: 0x00001E32
		public FunctionResult FunctionResult { get; private set; }

		// Token: 0x06000109 RID: 265 RVA: 0x00003C3B File Offset: 0x00001E3B
		public ThreadedClientSessionFunctionTask(IClientSession session, Message message) : base(session)
		{
			this._message = message;
			this._taskCompletionSource = new TaskCompletionSource<bool>();
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00003C56 File Offset: 0x00001E56
		public override void BeginJob()
		{
			this._task = this.CallFunction();
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00003C64 File Offset: 0x00001E64
		private async Task CallFunction()
		{
			FunctionResult functionResult = await base.Session.CallFunction<FunctionResult>(this._message);
			this.FunctionResult = functionResult;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00003CA9 File Offset: 0x00001EA9
		public override void DoMainThreadJob()
		{
			if (this._task.IsCompleted)
			{
				this._taskCompletionSource.SetResult(true);
				base.Finished = true;
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00003CCC File Offset: 0x00001ECC
		public async Task Wait()
		{
			await this._taskCompletionSource.Task;
		}

		// Token: 0x04000049 RID: 73
		private TaskCompletionSource<bool> _taskCompletionSource;

		// Token: 0x0400004A RID: 74
		private Message _message;

		// Token: 0x0400004C RID: 76
		private Task _task;
	}
}
