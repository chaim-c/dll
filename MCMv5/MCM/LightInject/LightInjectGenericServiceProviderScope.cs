using System;
using System.Runtime.CompilerServices;
using BUTR.DependencyInjection;

namespace MCM.LightInject
{
	// Token: 0x020000C6 RID: 198
	internal class LightInjectGenericServiceProviderScope : IGenericServiceProviderScope, IDisposable
	{
		// Token: 0x06000425 RID: 1061 RVA: 0x0000C6B4 File Offset: 0x0000A8B4
		[NullableContext(1)]
		public LightInjectGenericServiceProviderScope(Scope scope)
		{
			this._scope = scope;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000C6C4 File Offset: 0x0000A8C4
		[NullableContext(1)]
		[return: Nullable(2)]
		public TService GetService<TService>() where TService : class
		{
			return this._scope.GetInstance<TService>();
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000C6D1 File Offset: 0x0000A8D1
		public void Dispose()
		{
			this._scope.Dispose();
		}

		// Token: 0x0400016C RID: 364
		[Nullable(1)]
		private readonly Scope _scope;
	}
}
