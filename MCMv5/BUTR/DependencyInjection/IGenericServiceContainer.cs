using System;
using System.Runtime.CompilerServices;

namespace BUTR.DependencyInjection
{
	// Token: 0x02000145 RID: 325
	[NullableContext(1)]
	public interface IGenericServiceContainer
	{
		// Token: 0x060008AD RID: 2221
		IGenericServiceContainer RegisterSingleton<TService>() where TService : class;

		// Token: 0x060008AE RID: 2222
		IGenericServiceContainer RegisterSingleton<TService>(Func<IGenericServiceFactory, TService> factory) where TService : class;

		// Token: 0x060008AF RID: 2223
		IGenericServiceContainer RegisterSingleton<TService, TImplementation>() where TService : class where TImplementation : class, TService;

		// Token: 0x060008B0 RID: 2224
		IGenericServiceContainer RegisterSingleton<TService, TImplementation>(Func<IGenericServiceFactory, TImplementation> factory) where TService : class where TImplementation : class, TService;

		// Token: 0x060008B1 RID: 2225
		IGenericServiceContainer RegisterSingleton(Type serviceType, Type implementationType);

		// Token: 0x060008B2 RID: 2226
		IGenericServiceContainer RegisterSingleton(Type serviceType, Func<object> factory);

		// Token: 0x060008B3 RID: 2227
		IGenericServiceContainer RegisterScoped<TService>() where TService : class;

		// Token: 0x060008B4 RID: 2228
		IGenericServiceContainer RegisterScoped<TService>(Func<IGenericServiceFactory, TService> factory) where TService : class;

		// Token: 0x060008B5 RID: 2229
		IGenericServiceContainer RegisterScoped<TService, TImplementation>() where TService : class where TImplementation : class, TService;

		// Token: 0x060008B6 RID: 2230
		IGenericServiceContainer RegisterScoped<TService, TImplementation>(Func<IGenericServiceFactory, TImplementation> factory) where TService : class where TImplementation : class, TService;

		// Token: 0x060008B7 RID: 2231
		IGenericServiceContainer RegisterScoped(Type serviceType, Type implementationType);

		// Token: 0x060008B8 RID: 2232
		IGenericServiceContainer RegisterScoped(Type serviceType, Func<object> factory);

		// Token: 0x060008B9 RID: 2233
		IGenericServiceContainer RegisterTransient<TService>() where TService : class;

		// Token: 0x060008BA RID: 2234
		IGenericServiceContainer RegisterTransient<TService>(Func<IGenericServiceFactory, TService> factory) where TService : class;

		// Token: 0x060008BB RID: 2235
		IGenericServiceContainer RegisterTransient<TService, TImplementation>() where TService : class where TImplementation : class, TService;

		// Token: 0x060008BC RID: 2236
		IGenericServiceContainer RegisterTransient<TService, TImplementation>(Func<IGenericServiceFactory, TImplementation> factory) where TService : class where TImplementation : class, TService;

		// Token: 0x060008BD RID: 2237
		IGenericServiceContainer RegisterTransient(Type serviceType, Type implementationType);

		// Token: 0x060008BE RID: 2238
		IGenericServiceContainer RegisterTransient(Type serviceType, Func<object> factory);

		// Token: 0x060008BF RID: 2239
		IGenericServiceProvider Build();
	}
}
