using System;

namespace TaleWorlds.Diamond
{
	// Token: 0x02000030 RID: 48
	internal sealed class ThreadedClientSessionConnectTask : ThreadedClientSessionTask
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x00003AC7 File Offset: 0x00001CC7
		public ThreadedClientSessionConnectTask(IClientSession session) : base(session)
		{
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00003AD0 File Offset: 0x00001CD0
		public override void BeginJob()
		{
			base.Session.Connect();
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00003ADD File Offset: 0x00001CDD
		public override void DoMainThreadJob()
		{
			base.Finished = true;
		}
	}
}
