using System;

namespace TaleWorlds.Diamond
{
	// Token: 0x0200002F RID: 47
	internal abstract class ThreadedClientSessionTask
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00003A96 File Offset: 0x00001C96
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00003A9E File Offset: 0x00001C9E
		public IClientSession Session { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00003AA7 File Offset: 0x00001CA7
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00003AAF File Offset: 0x00001CAF
		public bool Finished { get; protected set; }

		// Token: 0x060000F2 RID: 242 RVA: 0x00003AB8 File Offset: 0x00001CB8
		protected ThreadedClientSessionTask(IClientSession session)
		{
			this.Session = session;
		}

		// Token: 0x060000F3 RID: 243
		public abstract void BeginJob();

		// Token: 0x060000F4 RID: 244
		public abstract void DoMainThreadJob();
	}
}
