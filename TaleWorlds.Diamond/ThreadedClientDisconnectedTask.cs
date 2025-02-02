using System;

namespace TaleWorlds.Diamond
{
	// Token: 0x0200002C RID: 44
	internal sealed class ThreadedClientDisconnectedTask : ThreadedClientTask
	{
		// Token: 0x060000DE RID: 222 RVA: 0x00003703 File Offset: 0x00001903
		public ThreadedClientDisconnectedTask(IClient client) : base(client)
		{
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000370C File Offset: 0x0000190C
		public override void DoJob()
		{
			base.Client.OnDisconnected();
		}
	}
}
