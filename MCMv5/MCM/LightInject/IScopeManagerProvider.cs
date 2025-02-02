using System;

namespace MCM.LightInject
{
	// Token: 0x020000E1 RID: 225
	internal interface IScopeManagerProvider
	{
		// Token: 0x060004AE RID: 1198
		IScopeManager GetScopeManager(IServiceFactory serviceFactory);
	}
}
