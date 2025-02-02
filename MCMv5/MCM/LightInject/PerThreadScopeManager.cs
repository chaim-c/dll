using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace MCM.LightInject
{
	// Token: 0x02000109 RID: 265
	[ExcludeFromCodeCoverage]
	internal class PerThreadScopeManager : ScopeManager
	{
		// Token: 0x06000661 RID: 1633 RVA: 0x00013D7F File Offset: 0x00011F7F
		public PerThreadScopeManager(IServiceFactory serviceFactory) : base(serviceFactory)
		{
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x00013D98 File Offset: 0x00011F98
		// (set) Token: 0x06000663 RID: 1635 RVA: 0x00013DBB File Offset: 0x00011FBB
		public override Scope CurrentScope
		{
			get
			{
				return base.GetThisScopeOrFirstValidAncestor(this.threadLocalScope.Value);
			}
			set
			{
				this.threadLocalScope.Value = value;
			}
		}

		// Token: 0x040001D7 RID: 471
		private readonly ThreadLocal<Scope> threadLocalScope = new ThreadLocal<Scope>();
	}
}
