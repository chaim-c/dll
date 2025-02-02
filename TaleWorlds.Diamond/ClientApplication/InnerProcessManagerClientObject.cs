using System;
using TaleWorlds.Diamond.InnerProcess;

namespace TaleWorlds.Diamond.ClientApplication
{
	// Token: 0x02000062 RID: 98
	public class InnerProcessManagerClientObject : DiamondClientApplicationObject
	{
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00006A23 File Offset: 0x00004C23
		// (set) Token: 0x0600024B RID: 587 RVA: 0x00006A2B File Offset: 0x00004C2B
		public InnerProcessManager InnerProcessManager { get; private set; }

		// Token: 0x0600024C RID: 588 RVA: 0x00006A34 File Offset: 0x00004C34
		public InnerProcessManagerClientObject(DiamondClientApplication application, InnerProcessManager innerProcessManager) : base(application)
		{
			this.InnerProcessManager = innerProcessManager;
		}
	}
}
