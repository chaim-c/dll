using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

namespace BUTR.DependencyInjection.ButterLib
{
	// Token: 0x02000059 RID: 89
	internal class ButterLibGenericServiceProviderScope : IGenericServiceProviderScope, IDisposable
	{
		// Token: 0x060003A4 RID: 932 RVA: 0x0000F642 File Offset: 0x0000D842
		[NullableContext(1)]
		public ButterLibGenericServiceProviderScope(IServiceScope serviceScope)
		{
			this._serviceScope = serviceScope;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000F652 File Offset: 0x0000D852
		[NullableContext(1)]
		[return: Nullable(2)]
		public TService GetService<TService>() where TService : class
		{
			return ServiceProviderServiceExtensions.GetRequiredService<TService>(this._serviceScope.ServiceProvider);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000F664 File Offset: 0x0000D864
		public void Dispose()
		{
			this._serviceScope.Dispose();
		}

		// Token: 0x040000FE RID: 254
		[Nullable(1)]
		private readonly IServiceScope _serviceScope;
	}
}
