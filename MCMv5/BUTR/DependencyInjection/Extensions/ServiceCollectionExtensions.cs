using System;
using System.Runtime.CompilerServices;

namespace BUTR.DependencyInjection.Extensions
{
	// Token: 0x0200014A RID: 330
	[NullableContext(1)]
	[Nullable(0)]
	public static class ServiceCollectionExtensions
	{
		// Token: 0x060008D9 RID: 2265 RVA: 0x0001D8A0 File Offset: 0x0001BAA0
		public static IGenericServiceContainer AddSingleton<TService>(this IGenericServiceContainer services) where TService : class
		{
			services.RegisterSingleton<TService>();
			return services;
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0001D8BC File Offset: 0x0001BABC
		public static IGenericServiceContainer AddSingleton<TService>(this IGenericServiceContainer services, Func<IGenericServiceFactory, TService> factory) where TService : class
		{
			services.RegisterSingleton<TService>(factory);
			return services;
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0001D8D8 File Offset: 0x0001BAD8
		public static IGenericServiceContainer AddSingleton<TService, TImplementation>(this IGenericServiceContainer services) where TService : class where TImplementation : class, TService
		{
			services.RegisterSingleton<TService, TImplementation>();
			return services;
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0001D8F4 File Offset: 0x0001BAF4
		public static IGenericServiceContainer AddSingleton<TService, [Nullable(2)] TImplementation>(this IGenericServiceContainer services, Func<IGenericServiceFactory, TService> factory) where TService : class
		{
			services.RegisterSingleton<TService>(factory);
			return services;
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0001D910 File Offset: 0x0001BB10
		public static IGenericServiceContainer AddSingleton(this IGenericServiceContainer services, Type serviceType, Type implementationType)
		{
			services.RegisterSingleton(serviceType, implementationType);
			return services;
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0001D92C File Offset: 0x0001BB2C
		public static IGenericServiceContainer AddSingleton(this IGenericServiceContainer services, Type serviceType, Func<object> factory)
		{
			services.RegisterSingleton(serviceType, factory);
			return services;
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0001D948 File Offset: 0x0001BB48
		public static IGenericServiceContainer AddScoped<TService>(this IGenericServiceContainer services) where TService : class
		{
			services.RegisterScoped<TService>();
			return services;
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0001D964 File Offset: 0x0001BB64
		public static IGenericServiceContainer AddScoped<TService, TImplementation>(this IGenericServiceContainer services) where TService : class where TImplementation : class, TService
		{
			services.RegisterScoped<TService, TImplementation>();
			return services;
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0001D980 File Offset: 0x0001BB80
		public static IGenericServiceContainer AddScoped<TService>(this IGenericServiceContainer services, Func<IGenericServiceFactory, TService> factory) where TService : class
		{
			services.RegisterScoped<TService>(factory);
			return services;
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0001D99C File Offset: 0x0001BB9C
		public static IGenericServiceContainer AddScoped<TService, [Nullable(2)] TImplementation>(this IGenericServiceContainer services, Func<IGenericServiceFactory, TService> factory) where TService : class
		{
			services.RegisterScoped<TService>(factory);
			return services;
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0001D9B8 File Offset: 0x0001BBB8
		public static IGenericServiceContainer AddScoped(this IGenericServiceContainer services, Type serviceType, Type implementationType)
		{
			services.RegisterScoped(serviceType, implementationType);
			return services;
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0001D9D4 File Offset: 0x0001BBD4
		public static IGenericServiceContainer AddScoped(this IGenericServiceContainer services, Type serviceType, Func<object> factory)
		{
			services.RegisterScoped(serviceType, factory);
			return services;
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0001D9F0 File Offset: 0x0001BBF0
		public static IGenericServiceContainer AddTransient<TService>(this IGenericServiceContainer services) where TService : class
		{
			services.RegisterTransient<TService>();
			return services;
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0001DA0C File Offset: 0x0001BC0C
		public static IGenericServiceContainer AddTransient<TService, TImplementation>(this IGenericServiceContainer services) where TService : class where TImplementation : class, TService
		{
			services.RegisterTransient<TService, TImplementation>();
			return services;
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0001DA28 File Offset: 0x0001BC28
		public static IGenericServiceContainer AddTransient<TService>(this IGenericServiceContainer services, Func<IGenericServiceFactory, TService> factory) where TService : class
		{
			services.RegisterTransient<TService>(factory);
			return services;
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0001DA44 File Offset: 0x0001BC44
		public static IGenericServiceContainer AddTransient<TService, [Nullable(2)] TImplementation>(this IGenericServiceContainer services, Func<IGenericServiceFactory, TService> factory) where TService : class
		{
			services.RegisterTransient<TService>(factory);
			return services;
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0001DA60 File Offset: 0x0001BC60
		public static IGenericServiceContainer AddTransient(this IGenericServiceContainer services, Type serviceType, Type implementationType)
		{
			services.RegisterTransient(serviceType, implementationType);
			return services;
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0001DA7C File Offset: 0x0001BC7C
		public static IGenericServiceContainer AddTransient(this IGenericServiceContainer services, Type serviceType, Func<object> factory)
		{
			services.RegisterTransient(serviceType, factory);
			return services;
		}

		// Token: 0x0400028E RID: 654
		internal static WithHistoryGenericServiceContainer ServiceContainer;
	}
}
