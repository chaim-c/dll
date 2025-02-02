using System;

namespace TaleWorlds.Diamond
{
	// Token: 0x0200002D RID: 45
	internal sealed class ThreadedClientCantConnectTask : ThreadedClientTask
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x00003719 File Offset: 0x00001919
		public ThreadedClientCantConnectTask(IClient client) : base(client)
		{
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003722 File Offset: 0x00001922
		public override void DoJob()
		{
			base.Client.OnCantConnect();
		}
	}
}
