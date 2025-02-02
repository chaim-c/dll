using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

namespace BUTR.DependencyInjection.ButterLib
{
	// Token: 0x02000057 RID: 87
	[NullableContext(1)]
	[Nullable(0)]
	internal class ButterLibGenericServiceFactory : IGenericServiceFactory
	{
		// Token: 0x0600039D RID: 925 RVA: 0x0000F5D0 File Offset: 0x0000D7D0
		public ButterLibGenericServiceFactory(IServiceProvider factory)
		{
			this._serviceProvider = factory;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000F5E0 File Offset: 0x0000D7E0
		public TService GetService<TService>() where TService : class
		{
			return ServiceProviderServiceExtensions.GetRequiredService<TService>(this._serviceProvider);
		}

		// Token: 0x040000FD RID: 253
		private readonly IServiceProvider _serviceProvider;
	}
}
