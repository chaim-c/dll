using System;

namespace TaleWorlds.Diamond
{
	// Token: 0x0200002B RID: 43
	internal sealed class ThreadedClientConnectedTask : ThreadedClientTask
	{
		// Token: 0x060000DC RID: 220 RVA: 0x000036ED File Offset: 0x000018ED
		public ThreadedClientConnectedTask(IClient client) : base(client)
		{
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000036F6 File Offset: 0x000018F6
		public override void DoJob()
		{
			base.Client.OnConnected();
		}
	}
}
