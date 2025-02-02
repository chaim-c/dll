using System;

namespace TaleWorlds.Diamond
{
	// Token: 0x02000033 RID: 51
	internal sealed class ThreadedClientSessionMessageTask : ThreadedClientSessionTask
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00003BED File Offset: 0x00001DED
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00003BF5 File Offset: 0x00001DF5
		public Message Message { get; private set; }

		// Token: 0x06000104 RID: 260 RVA: 0x00003BFE File Offset: 0x00001DFE
		public ThreadedClientSessionMessageTask(IClientSession session, Message message) : base(session)
		{
			this.Message = message;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00003C0E File Offset: 0x00001E0E
		public override void BeginJob()
		{
			base.Session.SendMessage(this.Message);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00003C21 File Offset: 0x00001E21
		public override void DoMainThreadJob()
		{
			base.Finished = true;
		}
	}
}
