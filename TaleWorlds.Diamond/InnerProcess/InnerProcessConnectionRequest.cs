using System;

namespace TaleWorlds.Diamond.InnerProcess
{
	// Token: 0x02000052 RID: 82
	internal class InnerProcessConnectionRequest
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00005990 File Offset: 0x00003B90
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x00005998 File Offset: 0x00003B98
		public IInnerProcessClient Client { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x000059A1 File Offset: 0x00003BA1
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x000059A9 File Offset: 0x00003BA9
		public int Port { get; private set; }

		// Token: 0x060001E3 RID: 483 RVA: 0x000059B2 File Offset: 0x00003BB2
		public InnerProcessConnectionRequest(IInnerProcessClient client, int port)
		{
			this.Client = client;
			this.Port = port;
		}
	}
}
