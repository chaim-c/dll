using System;
using System.Diagnostics.CodeAnalysis;

namespace MCM.LightInject
{
	// Token: 0x020000E4 RID: 228
	[ExcludeFromCodeCoverage]
	internal static class ServiceRegistryExtensions
	{
		// Token: 0x060004B1 RID: 1201 RVA: 0x0000CB3B File Offset: 0x0000AD3B
		public static IServiceRegistry Register(this IServiceRegistry serviceRegistry, Type serviceType, Func<IServiceFactory, object> factory)
		{
			return serviceRegistry.Register(serviceType, factory, string.Empty, null);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0000CB4B File Offset: 0x0000AD4B
		public static IServiceRegistry Register(this IServiceRegistry serviceRegistry, Type serviceType, Func<IServiceFactory, object> factory, ILifetime lifetime)
		{
			return serviceRegistry.Register(serviceType, factory, string.Empty, lifetime);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0000CB5B File Offset: 0x0000AD5B
		public static IServiceRegistry Register(this IServiceRegistry serviceRegistry, Type serviceType, Func<IServiceFactory, object> factory, string serviceName)
		{
			return serviceRegistry.Register(serviceType, factory, serviceName, null);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0000CB68 File Offset: 0x0000AD68
		public static IServiceRegistry Register(this IServiceRegistry serviceRegistry, Type serviceType, Func<IServiceFactory, object> factory, string serviceName, ILifetime lifetime)
		{
			ServiceRegistration serviceRegistration = new ServiceRegistration
			{
				FactoryExpression = factory,
				ServiceType = serviceType,
				ServiceName = serviceName,
				Lifetime = lifetime
			};
			return serviceRegistry.Register(serviceRegistration);
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0000CBA8 File Offset: 0x0000ADA8
		public static IServiceRegistry RegisterSingleton(this IServiceRegistry serviceRegistry, Type serviceType, Func<IServiceFactory, object> factory)
		{
			return serviceRegistry.RegisterSingleton(serviceType, factory, string.Empty);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0000CBB7 File Offset: 0x0000ADB7
		public static IServiceRegistry RegisterSingleton(this IServiceRegistry serviceRegistry, Type serviceType, Func<IServiceFactory, object> factory, string serviceName)
		{
			return serviceRegistry.Register(serviceType, factory, serviceName, new PerContainerLifetime());
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0000CBC7 File Offset: 0x0000ADC7
		public static IServiceRegistry RegisterScoped(this IServiceRegistry serviceRegistry, Type serviceType, Func<IServiceFactory, object> factory)
		{
			return serviceRegistry.RegisterScoped(serviceType, factory, string.Empty);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0000CBD6 File Offset: 0x0000ADD6
		public static IServiceRegistry RegisterScoped(this IServiceRegistry serviceRegistry, Type serviceType, Func<IServiceFactory, object> factory, string serviceName)
		{
			return serviceRegistry.Register(serviceType, factory, serviceName, new PerScopeLifetime());
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0000CBE6 File Offset: 0x0000ADE6
		public static IServiceRegistry RegisterTransient(this IServiceRegistry serviceRegistry, Type serviceType, Func<IServiceFactory, object> factory)
		{
			return serviceRegistry.RegisterTransient(serviceType, factory, string.Empty);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0000CBF5 File Offset: 0x0000ADF5
		public static IServiceRegistry RegisterTransient(this IServiceRegistry serviceRegistry, Type serviceType, Func<IServiceFactory, object> factory, string serviceName)
		{
			return serviceRegistry.Register(serviceType, factory, serviceName);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0000CC00 File Offset: 0x0000AE00
		public static IServiceRegistry RegisterSingleton<TService, TImplementation>(this IServiceRegistry serviceRegistry) where TImplementation : TService
		{
			return serviceRegistry.Register<TService, TImplementation>(new PerContainerLifetime());
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0000CC0D File Offset: 0x0000AE0D
		public static IServiceRegistry RegisterSingleton<TService>(this IServiceRegistry serviceRegistry)
		{
			return serviceRegistry.Register<TService>(new PerContainerLifetime());
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0000CC1A File Offset: 0x0000AE1A
		public static IServiceRegistry RegisterSingleton<TService, TImplementation>(this IServiceRegistry serviceRegistry, string serviceName) where TImplementation : TService
		{
			return serviceRegistry.Register<TService, TImplementation>(serviceName, new PerContainerLifetime());
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0000CC28 File Offset: 0x0000AE28
		public static IServiceRegistry RegisterSingleton(this IServiceRegistry serviceRegistry, Type serviceType, Type implementingType)
		{
			return serviceRegistry.Register(serviceType, implementingType, new PerContainerLifetime());
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0000CC37 File Offset: 0x0000AE37
		public static IServiceRegistry RegisterSingleton(this IServiceRegistry serviceRegistry, Type serviceType)
		{
			return serviceRegistry.Register(serviceType, new PerContainerLifetime());
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0000CC45 File Offset: 0x0000AE45
		public static IServiceRegistry RegisterSingleton(this IServiceRegistry serviceRegistry, Type serviceType, Type implementingType, string serviceName)
		{
			return serviceRegistry.Register(serviceType, implementingType, serviceName, new PerContainerLifetime());
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0000CC55 File Offset: 0x0000AE55
		public static IServiceRegistry RegisterSingleton<TService>(this IServiceRegistry serviceRegistry, Func<IServiceFactory, TService> factory)
		{
			return serviceRegistry.Register<TService>(factory, new PerContainerLifetime());
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0000CC63 File Offset: 0x0000AE63
		public static IServiceRegistry RegisterSingleton<TService>(this IServiceRegistry serviceRegistry, Func<IServiceFactory, TService> factory, string serviceName)
		{
			return serviceRegistry.Register<TService>(factory, serviceName, new PerContainerLifetime());
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0000CC72 File Offset: 0x0000AE72
		public static IServiceRegistry RegisterScoped<TService, TImplementation>(this IServiceRegistry serviceRegistry) where TImplementation : TService
		{
			return serviceRegistry.Register<TService, TImplementation>(new PerScopeLifetime());
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0000CC7F File Offset: 0x0000AE7F
		public static IServiceRegistry RegisterScoped<TService>(this IServiceRegistry serviceRegistry)
		{
			return serviceRegistry.Register<TService>(new PerScopeLifetime());
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0000CC8C File Offset: 0x0000AE8C
		public static IServiceRegistry RegisterScoped<TService, TImplementation>(this IServiceRegistry serviceRegistry, string serviceName) where TImplementation : TService
		{
			return serviceRegistry.Register<TService, TImplementation>(serviceName, new PerScopeLifetime());
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0000CC9A File Offset: 0x0000AE9A
		public static IServiceRegistry RegisterScoped(this IServiceRegistry serviceRegistry, Type serviceType, Type implementingType)
		{
			return serviceRegistry.Register(serviceType, implementingType, new PerScopeLifetime());
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0000CCA9 File Offset: 0x0000AEA9
		public static IServiceRegistry RegisterScoped(this IServiceRegistry serviceRegistry, Type serviceType)
		{
			return serviceRegistry.Register(serviceType, new PerScopeLifetime());
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0000CCB7 File Offset: 0x0000AEB7
		public static IServiceRegistry RegisterScoped(this IServiceRegistry serviceRegistry, Type serviceType, Type implementingType, string serviceName)
		{
			return serviceRegistry.Register(serviceType, implementingType, serviceName, new PerScopeLifetime());
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0000CCC7 File Offset: 0x0000AEC7
		public static IServiceRegistry RegisterScoped<TService>(this IServiceRegistry serviceRegistry, Func<IServiceFactory, TService> factory)
		{
			return serviceRegistry.Register<TService>(factory, new PerScopeLifetime());
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0000CCD5 File Offset: 0x0000AED5
		public static IServiceRegistry RegisterScoped<TService>(this IServiceRegistry serviceRegistry, Func<IServiceFactory, TService> factory, string serviceName)
		{
			return serviceRegistry.Register<TService>(factory, serviceName, new PerScopeLifetime());
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0000CCE4 File Offset: 0x0000AEE4
		public static IServiceRegistry RegisterTransient<TService, TImplementation>(this IServiceRegistry serviceRegistry) where TImplementation : TService
		{
			return serviceRegistry.Register<TService, TImplementation>();
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0000CCEC File Offset: 0x0000AEEC
		public static IServiceRegistry RegisterTransient<TService>(this IServiceRegistry serviceRegistry)
		{
			return serviceRegistry.Register<TService>();
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0000CCF4 File Offset: 0x0000AEF4
		public static IServiceRegistry RegisterTransient<TService, TImplementation>(this IServiceRegistry serviceRegistry, string serviceName) where TImplementation : TService
		{
			return serviceRegistry.Register<TService, TImplementation>(serviceName);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0000CCFD File Offset: 0x0000AEFD
		public static IServiceRegistry RegisterTransient(this IServiceRegistry serviceRegistry, Type serviceType, Type implementingType)
		{
			return serviceRegistry.Register(serviceType, implementingType);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0000CD07 File Offset: 0x0000AF07
		public static IServiceRegistry RegisterTransient(this IServiceRegistry serviceRegistry, Type serviceType)
		{
			return serviceRegistry.Register(serviceType);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0000CD10 File Offset: 0x0000AF10
		public static IServiceRegistry RegisterTransient(this IServiceRegistry serviceRegistry, Type serviceType, Type implementingType, string serviceName)
		{
			return serviceRegistry.Register(serviceType, implementingType, serviceName);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0000CD1B File Offset: 0x0000AF1B
		public static IServiceRegistry RegisterTransient<TService>(this IServiceRegistry serviceRegistry, Func<IServiceFactory, TService> factory)
		{
			return serviceRegistry.Register<TService>(factory);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0000CD24 File Offset: 0x0000AF24
		public static IServiceRegistry RegisterTransient<TService>(this IServiceRegistry serviceRegistry, Func<IServiceFactory, TService> factory, string serviceName)
		{
			return serviceRegistry.Register<TService>(factory, serviceName);
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0000CD30 File Offset: 0x0000AF30
		public static IServiceRegistry Override<TService, TImplementation>(this IServiceRegistry serviceRegistry) where TImplementation : TService
		{
			return serviceRegistry.Override((ServiceRegistration sr) => sr.ServiceType == typeof(TService), delegate(IServiceFactory serviceFactory, ServiceRegistration registration)
			{
				registration.FactoryExpression = null;
				registration.ImplementingType = typeof(TImplementation);
				return registration;
			});
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0000CD88 File Offset: 0x0000AF88
		public static IServiceRegistry Override<TService, TImplementation>(this IServiceRegistry serviceRegistry, ILifetime lifetime) where TImplementation : TService
		{
			return serviceRegistry.Override((ServiceRegistration sr) => sr.ServiceType == typeof(TService), delegate(IServiceFactory serviceFactory, ServiceRegistration registration)
			{
				registration.FactoryExpression = null;
				registration.ImplementingType = typeof(TImplementation);
				registration.Lifetime = lifetime;
				return registration;
			});
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0000CDD8 File Offset: 0x0000AFD8
		public static IServiceRegistry Initialize<TService>(this IServiceRegistry serviceRegistry, Action<IServiceFactory, TService> processor)
		{
			return serviceRegistry.Initialize((ServiceRegistration sr) => sr.ServiceType == typeof(TService), delegate(IServiceFactory factory, object instance)
			{
				processor(factory, (TService)((object)instance));
			});
		}
	}
}
