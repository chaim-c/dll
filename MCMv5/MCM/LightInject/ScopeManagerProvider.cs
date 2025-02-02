using System;
using System.Diagnostics.CodeAnalysis;

namespace MCM.LightInject
{
	// Token: 0x020000EE RID: 238
	[ExcludeFromCodeCoverage]
	internal abstract class ScopeManagerProvider : IScopeManagerProvider
	{
		// Token: 0x060005CC RID: 1484 RVA: 0x000120EC File Offset: 0x000102EC
		public IScopeManager GetScopeManager(IServiceFactory serviceFactory)
		{
			bool flag = this.scopeManager == null;
			if (flag)
			{
				object obj = this.lockObject;
				lock (obj)
				{
					bool flag3 = this.scopeManager == null;
					if (flag3)
					{
						this.scopeManager = this.CreateScopeManager(serviceFactory);
					}
				}
			}
			return this.scopeManager;
		}

		// Token: 0x060005CD RID: 1485
		protected abstract IScopeManager CreateScopeManager(IServiceFactory serviceFactory);

		// Token: 0x0400019F RID: 415
		private readonly object lockObject = new object();

		// Token: 0x040001A0 RID: 416
		private IScopeManager scopeManager;
	}
}
