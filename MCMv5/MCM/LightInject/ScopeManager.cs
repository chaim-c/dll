using System;
using System.Diagnostics.CodeAnalysis;

namespace MCM.LightInject
{
	// Token: 0x02000108 RID: 264
	[ExcludeFromCodeCoverage]
	internal abstract class ScopeManager : IScopeManager
	{
		// Token: 0x0600065A RID: 1626 RVA: 0x00013CD4 File Offset: 0x00011ED4
		protected ScopeManager(IServiceFactory serviceFactory)
		{
			this.ServiceFactory = serviceFactory;
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600065B RID: 1627
		// (set) Token: 0x0600065C RID: 1628
		public abstract Scope CurrentScope { get; set; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x00013CE5 File Offset: 0x00011EE5
		public IServiceFactory ServiceFactory { get; }

		// Token: 0x0600065E RID: 1630 RVA: 0x00013CF0 File Offset: 0x00011EF0
		public Scope BeginScope()
		{
			Scope currentScope = this.CurrentScope;
			Scope scope = new Scope(this, currentScope);
			this.CurrentScope = scope;
			return scope;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00013D1C File Offset: 0x00011F1C
		public void EndScope(Scope scope)
		{
			Scope parentScope = scope.ParentScope;
			bool flag = this.CurrentScope == scope;
			if (flag)
			{
				this.CurrentScope = parentScope;
			}
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00013D48 File Offset: 0x00011F48
		protected Scope GetThisScopeOrFirstValidAncestor(Scope scope)
		{
			while (scope != null && scope.IsDisposed)
			{
				scope = scope.ParentScope;
			}
			this.CurrentScope = scope;
			return scope;
		}
	}
}
