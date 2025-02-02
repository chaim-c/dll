using System;
using System.Diagnostics.CodeAnalysis;

namespace MCM.LightInject
{
	// Token: 0x020000F1 RID: 241
	[ExcludeFromCodeCoverage]
	internal class PerLogicalCallContextScopeManagerProvider : ScopeManagerProvider
	{
		// Token: 0x060005D4 RID: 1492 RVA: 0x000121E4 File Offset: 0x000103E4
		protected override IScopeManager CreateScopeManager(IServiceFactory serviceFactory)
		{
			return new PerLogicalCallContextScopeManager(serviceFactory);
		}
	}
}
