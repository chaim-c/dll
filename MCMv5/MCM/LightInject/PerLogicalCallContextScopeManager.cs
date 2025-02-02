using System;
using System.Diagnostics.CodeAnalysis;

namespace MCM.LightInject
{
	// Token: 0x020000F0 RID: 240
	[ExcludeFromCodeCoverage]
	internal class PerLogicalCallContextScopeManager : ScopeManager
	{
		// Token: 0x060005D1 RID: 1489 RVA: 0x00012199 File Offset: 0x00010399
		public PerLogicalCallContextScopeManager(IServiceFactory serviceFactory) : base(serviceFactory)
		{
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x000121B0 File Offset: 0x000103B0
		// (set) Token: 0x060005D3 RID: 1491 RVA: 0x000121D3 File Offset: 0x000103D3
		public override Scope CurrentScope
		{
			get
			{
				return base.GetThisScopeOrFirstValidAncestor(this.currentScope.Value);
			}
			set
			{
				this.currentScope.Value = value;
			}
		}

		// Token: 0x040001A1 RID: 417
		private readonly LogicalThreadStorage<Scope> currentScope = new LogicalThreadStorage<Scope>();
	}
}
