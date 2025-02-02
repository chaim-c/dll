using System;
using System.Threading.Tasks;

namespace TaleWorlds.Diamond.InnerProcess
{
	// Token: 0x02000056 RID: 86
	internal class InnerProcessMessageTask
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00005BE5 File Offset: 0x00003DE5
		// (set) Token: 0x060001FD RID: 509 RVA: 0x00005BED File Offset: 0x00003DED
		public SessionCredentials SessionCredentials { get; private set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00005BF6 File Offset: 0x00003DF6
		// (set) Token: 0x060001FF RID: 511 RVA: 0x00005BFE File Offset: 0x00003DFE
		public Message Message { get; private set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00005C07 File Offset: 0x00003E07
		// (set) Token: 0x06000201 RID: 513 RVA: 0x00005C0F File Offset: 0x00003E0F
		public InnerProcessMessageTaskType Type { get; private set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00005C18 File Offset: 0x00003E18
		// (set) Token: 0x06000203 RID: 515 RVA: 0x00005C20 File Offset: 0x00003E20
		public bool Finished { get; private set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00005C29 File Offset: 0x00003E29
		// (set) Token: 0x06000205 RID: 517 RVA: 0x00005C31 File Offset: 0x00003E31
		public bool Successful { get; private set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00005C3A File Offset: 0x00003E3A
		// (set) Token: 0x06000207 RID: 519 RVA: 0x00005C42 File Offset: 0x00003E42
		public FunctionResult FunctionResult { get; private set; }

		// Token: 0x06000208 RID: 520 RVA: 0x00005C4B File Offset: 0x00003E4B
		public InnerProcessMessageTask(SessionCredentials sessionCredentials, Message message, InnerProcessMessageTaskType type)
		{
			this.SessionCredentials = sessionCredentials;
			this.Message = message;
			this.Type = type;
			this._taskCompletionSource = new TaskCompletionSource<bool>();
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00005C74 File Offset: 0x00003E74
		public async Task WaitUntilFinished()
		{
			await this._taskCompletionSource.Task;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00005CB9 File Offset: 0x00003EB9
		public void SetFinishedAsSuccessful(FunctionResult functionResult)
		{
			this.FunctionResult = functionResult;
			this.Successful = true;
			this.Finished = true;
			this._taskCompletionSource.SetResult(true);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00005CDC File Offset: 0x00003EDC
		public void SetFinishedAsFailed()
		{
			this.Successful = false;
			this.Finished = true;
			this._taskCompletionSource.SetResult(true);
		}

		// Token: 0x040000B9 RID: 185
		private TaskCompletionSource<bool> _taskCompletionSource;
	}
}
