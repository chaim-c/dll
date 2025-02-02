using System;
using System.Runtime.CompilerServices;
using Bannerlord.ButterLib.Common.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace BUTR.DependencyInjection.ButterLib
{
	// Token: 0x02000058 RID: 88
	internal class ButterLibGenericServiceProvider : IGenericServiceProvider, IDisposable
	{
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600039F RID: 927 RVA: 0x0000F5ED File Offset: 0x0000D7ED
		[Nullable(2)]
		private static IServiceProvider ServiceProvider
		{
			[NullableContext(2)]
			get
			{
				return DependencyInjectionExtensions.GetTempServiceProvider(null) ?? DependencyInjectionExtensions.GetServiceProvider(null);
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000F5FF File Offset: 0x0000D7FF
		[NullableContext(1)]
		public IGenericServiceProviderScope CreateScope()
		{
			return new ButterLibGenericServiceProviderScope(ServiceProviderServiceExtensions.CreateScope(ButterLibGenericServiceProvider.ServiceProvider));
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000F610 File Offset: 0x0000D810
		[NullableContext(1)]
		[return: Nullable(2)]
		public TService GetService<TService>() where TService : class
		{
			IServiceProvider serviceProvider = ButterLibGenericServiceProvider.ServiceProvider;
			return (serviceProvider != null) ? ServiceProviderServiceExtensions.GetRequiredService<TService>(serviceProvider) : default(TService);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000F636 File Offset: 0x0000D836
		public void Dispose()
		{
		}
	}
}
