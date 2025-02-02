using System;

namespace TaleWorlds.Diamond
{
	// Token: 0x02000031 RID: 49
	internal sealed class ThreadedClientSessionDisconnectTask : ThreadedClientSessionTask
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x00003AE6 File Offset: 0x00001CE6
		public ThreadedClientSessionDisconnectTask(IClientSession session) : base(session)
		{
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00003AEF File Offset: 0x00001CEF
		public override void BeginJob()
		{
			base.Session.Disconnect();
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00003AFC File Offset: 0x00001CFC
		public override void DoMainThreadJob()
		{
			base.Finished = true;
		}
	}
}
