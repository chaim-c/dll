using System;
using System.Threading.Tasks;

namespace TaleWorlds.Diamond
{
	// Token: 0x02000032 RID: 50
	internal sealed class ThreadedClientSessionLoginTask : ThreadedClientSessionTask
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00003B05 File Offset: 0x00001D05
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00003B0D File Offset: 0x00001D0D
		public LoginResult LoginResult { get; private set; }

		// Token: 0x060000FD RID: 253 RVA: 0x00003B16 File Offset: 0x00001D16
		public ThreadedClientSessionLoginTask(IClientSession session, LoginMessage message) : base(session)
		{
			this._message = message;
			this._taskCompletionSource = new TaskCompletionSource<bool>();
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00003B31 File Offset: 0x00001D31
		public override void BeginJob()
		{
			this._task = this.Login();
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00003B40 File Offset: 0x00001D40
		private async Task Login()
		{
			LoginResult loginResult = await base.Session.Login(this._message);
			this.LoginResult = loginResult;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00003B85 File Offset: 0x00001D85
		public override void DoMainThreadJob()
		{
			if (this._task.IsCompleted)
			{
				this._taskCompletionSource.SetResult(true);
				base.Finished = true;
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00003BA8 File Offset: 0x00001DA8
		public async Task Wait()
		{
			await this._taskCompletionSource.Task;
		}

		// Token: 0x04000044 RID: 68
		private TaskCompletionSource<bool> _taskCompletionSource;

		// Token: 0x04000045 RID: 69
		private LoginMessage _message;

		// Token: 0x04000047 RID: 71
		private Task _task;
	}
}
