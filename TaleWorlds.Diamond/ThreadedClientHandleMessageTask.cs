using System;

namespace TaleWorlds.Diamond
{
	// Token: 0x0200002A RID: 42
	internal sealed class ThreadedClientHandleMessageTask : ThreadedClientTask
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000036B9 File Offset: 0x000018B9
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x000036C1 File Offset: 0x000018C1
		public Message Message { get; private set; }

		// Token: 0x060000DA RID: 218 RVA: 0x000036CA File Offset: 0x000018CA
		public ThreadedClientHandleMessageTask(IClient client, Message message) : base(client)
		{
			this.Message = message;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000036DA File Offset: 0x000018DA
		public override void DoJob()
		{
			base.Client.HandleMessage(this.Message);
		}
	}
}
