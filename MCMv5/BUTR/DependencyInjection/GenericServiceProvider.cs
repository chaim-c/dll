using System;
using System.Runtime.CompilerServices;

namespace BUTR.DependencyInjection
{
	// Token: 0x02000144 RID: 324
	[NullableContext(2)]
	[Nullable(0)]
	public static class GenericServiceProvider
	{
		// Token: 0x060008AB RID: 2219 RVA: 0x0001D294 File Offset: 0x0001B494
		[NullableContext(1)]
		[return: Nullable(2)]
		public static TService GetService<TService>() where TService : class
		{
			TService result;
			if (GenericServiceProvider.GameScopeServiceProvider == null)
			{
				IGenericServiceProvider globalServiceProvider = GenericServiceProvider.GlobalServiceProvider;
				result = ((globalServiceProvider != null) ? globalServiceProvider.GetService<TService>() : default(TService));
			}
			else
			{
				result = GenericServiceProvider.GameScopeServiceProvider.GetService<TService>();
			}
			return result;
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0001D2CD File Offset: 0x0001B4CD
		public static IGenericServiceProviderScope CreateScope()
		{
			IGenericServiceProvider globalServiceProvider = GenericServiceProvider.GlobalServiceProvider;
			return (globalServiceProvider != null) ? globalServiceProvider.CreateScope() : null;
		}

		// Token: 0x0400028A RID: 650
		internal static IGenericServiceProvider GlobalServiceProvider;

		// Token: 0x0400028B RID: 651
		internal static IGenericServiceProviderScope GameScopeServiceProvider;
	}
}
