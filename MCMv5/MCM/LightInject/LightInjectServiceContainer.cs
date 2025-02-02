using System;
using System.Linq;
using System.Runtime.CompilerServices;
using BUTR.DependencyInjection;
using BUTR.DependencyInjection.Logger;

namespace MCM.LightInject
{
	// Token: 0x020000C7 RID: 199
	[NullableContext(1)]
	[Nullable(0)]
	internal class LightInjectServiceContainer : IGenericServiceContainer
	{
		// Token: 0x06000428 RID: 1064 RVA: 0x0000C6DF File Offset: 0x0000A8DF
		public LightInjectServiceContainer(IServiceContainer serviceContainer)
		{
			this._serviceContainer = serviceContainer;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000C6F0 File Offset: 0x0000A8F0
		public IGenericServiceContainer RegisterSingleton<TService>() where TService : class
		{
			this._serviceContainer.RegisterSingleton<TService>();
			return this;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000C710 File Offset: 0x0000A910
		public IGenericServiceContainer RegisterSingleton<TService>(Func<IGenericServiceFactory, TService> factory) where TService : class
		{
			this._serviceContainer.RegisterSingleton((IServiceFactory lightInjectFactory) => factory(new LightInjectGenericServiceFactory(lightInjectFactory)));
			return this;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000C748 File Offset: 0x0000A948
		public IGenericServiceContainer RegisterSingleton<TService, TImplementation>() where TService : class where TImplementation : class, TService
		{
			this._serviceContainer.RegisterSingleton<TService, TImplementation>();
			return this;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000C768 File Offset: 0x0000A968
		public IGenericServiceContainer RegisterSingleton<TService, TImplementation>(Func<IGenericServiceFactory, TImplementation> factory) where TService : class where TImplementation : class, TService
		{
			this._serviceContainer.RegisterSingleton((IServiceFactory lightInjectFactory) => (TService)((object)factory(new LightInjectGenericServiceFactory(lightInjectFactory))));
			return this;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000C7A0 File Offset: 0x0000A9A0
		public IGenericServiceContainer RegisterSingleton(Type serviceType, Type implementationType)
		{
			this._serviceContainer.RegisterSingleton(serviceType, implementationType);
			return this;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000C7C4 File Offset: 0x0000A9C4
		public IGenericServiceContainer RegisterSingleton(Type serviceType, Func<object> factory)
		{
			this._serviceContainer.RegisterSingleton(serviceType, (IServiceFactory _) => factory());
			return this;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000C800 File Offset: 0x0000AA00
		public IGenericServiceContainer RegisterScoped<TService>() where TService : class
		{
			this._serviceContainer.RegisterScoped<TService>();
			return this;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000C820 File Offset: 0x0000AA20
		public IGenericServiceContainer RegisterScoped<TService>(Func<IGenericServiceFactory, TService> factory) where TService : class
		{
			this._serviceContainer.RegisterScoped((IServiceFactory lightInjectFactory) => factory(new LightInjectGenericServiceFactory(lightInjectFactory)));
			return this;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000C858 File Offset: 0x0000AA58
		public IGenericServiceContainer RegisterScoped<TService, TImplementation>() where TService : class where TImplementation : class, TService
		{
			this._serviceContainer.RegisterScoped<TService, TImplementation>();
			return this;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000C878 File Offset: 0x0000AA78
		public IGenericServiceContainer RegisterScoped<TService, TImplementation>(Func<IGenericServiceFactory, TImplementation> factory) where TService : class where TImplementation : class, TService
		{
			this._serviceContainer.RegisterScoped((IServiceFactory lightInjectFactory) => (TService)((object)factory(new LightInjectGenericServiceFactory(lightInjectFactory))));
			return this;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000C8B0 File Offset: 0x0000AAB0
		public IGenericServiceContainer RegisterScoped(Type serviceType, Type implementationType)
		{
			this._serviceContainer.RegisterScoped(serviceType, implementationType);
			return this;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000C8D4 File Offset: 0x0000AAD4
		public IGenericServiceContainer RegisterScoped(Type serviceType, Func<object> factory)
		{
			this._serviceContainer.RegisterScoped(serviceType, (IServiceFactory _) => factory());
			return this;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000C910 File Offset: 0x0000AB10
		public IGenericServiceContainer RegisterTransient<TService>() where TService : class
		{
			this._serviceContainer.RegisterTransient<TService>();
			return this;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000C930 File Offset: 0x0000AB30
		public IGenericServiceContainer RegisterTransient<TService>(Func<IGenericServiceFactory, TService> factory) where TService : class
		{
			this._serviceContainer.RegisterTransient((IServiceFactory lightInjectFactory) => factory(new LightInjectGenericServiceFactory(lightInjectFactory)));
			return this;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000C968 File Offset: 0x0000AB68
		public IGenericServiceContainer RegisterTransient<TService, TImplementation>() where TService : class where TImplementation : class, TService
		{
			this._serviceContainer.RegisterTransient<TService, TImplementation>();
			return this;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000C988 File Offset: 0x0000AB88
		public IGenericServiceContainer RegisterTransient<TService, TImplementation>(Func<IGenericServiceFactory, TImplementation> factory) where TService : class where TImplementation : class, TService
		{
			this._serviceContainer.RegisterTransient((IServiceFactory lightInjectFactory) => (TService)((object)factory(new LightInjectGenericServiceFactory(lightInjectFactory))));
			return this;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000C9C0 File Offset: 0x0000ABC0
		public IGenericServiceContainer RegisterTransient(Type serviceType, Type implementationType)
		{
			this._serviceContainer.RegisterTransient(serviceType, implementationType);
			return this;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000C9E4 File Offset: 0x0000ABE4
		public IGenericServiceContainer RegisterTransient(Type serviceType, Func<object> factory)
		{
			this._serviceContainer.RegisterTransient(serviceType, (IServiceFactory _) => factory());
			return this;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000CA20 File Offset: 0x0000AC20
		public IGenericServiceProvider Build()
		{
			bool flag = this._serviceContainer.AvailableServices.All((ServiceRegistration s) => s.ServiceType != typeof(IBUTRLogger));
			if (flag)
			{
				this._serviceContainer.RegisterTransient<IBUTRLogger, DefaultBUTRLogger>();
			}
			bool flag2 = this._serviceContainer.AvailableServices.All((ServiceRegistration s) => s.ServiceType != typeof(IBUTRLogger<>));
			if (flag2)
			{
				this._serviceContainer.RegisterTransient(typeof(IBUTRLogger<>), typeof(DefaultBUTRLogger<>));
			}
			this._serviceContainer.Compile();
			return new LightInjectGenericServiceProvider(this._serviceContainer);
		}

		// Token: 0x0400016D RID: 365
		private readonly IServiceContainer _serviceContainer;
	}
}
