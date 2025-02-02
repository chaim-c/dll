using System;
using System.Diagnostics.CodeAnalysis;

namespace MCM.LightInject
{
	// Token: 0x020000EF RID: 239
	[ExcludeFromCodeCoverage]
	internal class PerThreadScopeManagerProvider : ScopeManagerProvider
	{
		// Token: 0x060005CF RID: 1487 RVA: 0x00012178 File Offset: 0x00010378
		protected override IScopeManager CreateScopeManager(IServiceFactory serviceFactory)
		{
			return new PerThreadScopeManager(serviceFactory);
		}
	}
}
