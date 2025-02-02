using System;
using System.Runtime.CompilerServices;
using Bannerlord.ButterLib.Common.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace BUTR.DependencyInjection.ButterLib
{
	// Token: 0x0200005A RID: 90
	[NullableContext(1)]
	[Nullable(0)]
	internal class ButterLibServiceContainer : IGenericServiceContainer
	{
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000F672 File Offset: 0x0000D872
		[Nullable(2)]
		private static IServiceCollection ServiceContainer
		{
			[NullableContext(2)]
			get
			{
				return DependencyInjectionExtensions.GetServices(null);
			}
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000F67C File Offset: 0x0000D87C
		public IGenericServiceContainer RegisterSingleton<TService>() where TService : class
		{
			ServiceCollectionServiceExtensions.AddSingleton<TService>(ButterLibServiceContainer.ServiceContainer);
			return this;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000F69C File Offset: 0x0000D89C
		public IGenericServiceContainer RegisterSingleton<TService>(Func<IGenericServiceFactory, TService> factory) where TService : class
		{
			ServiceCollectionServiceExtensions.AddSingleton<TService>(ButterLibServiceContainer.ServiceContainer, (IServiceProvider sp) => factory(new ButterLibGenericServiceFactory(sp)));
			return this;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000F6D4 File Offset: 0x0000D8D4
		public IGenericServiceContainer RegisterSingleton<TService, TImplementation>() where TService : class where TImplementation : class, TService
		{
			ServiceCollectionServiceExtensions.AddSingleton<TService, TImplementation>(ButterLibServiceContainer.ServiceContainer);
			return this;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000F6F4 File Offset: 0x0000D8F4
		public IGenericServiceContainer RegisterSingleton<TService, TImplementation>(Func<IGenericServiceFactory, TImplementation> factory) where TService : class where TImplementation : class, TService
		{
			ServiceCollectionServiceExtensions.AddSingleton<TService, TImplementation>(ButterLibServiceContainer.ServiceContainer, (IServiceProvider sp) => factory(new ButterLibGenericServiceFactory(sp)));
			return this;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000F72C File Offset: 0x0000D92C
		public IGenericServiceContainer RegisterSingleton(Type serviceType, Type implementationType)
		{
			ServiceCollectionServiceExtensions.AddSingleton(ButterLibServiceContainer.ServiceContainer, serviceType, implementationType);
			return this;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000F74C File Offset: 0x0000D94C
		public IGenericServiceContainer RegisterSingleton(Type serviceType, Func<object> factory)
		{
			ServiceCollectionServiceExtensions.AddSingleton<object>(ButterLibServiceContainer.ServiceContainer, (IServiceProvider _) => factory());
			return this;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000F784 File Offset: 0x0000D984
		public IGenericServiceContainer RegisterScoped<TService>() where TService : class
		{
			ServiceCollectionServiceExtensions.AddScoped<TService>(ButterLibServiceContainer.ServiceContainer);
			return this;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000F7A4 File Offset: 0x0000D9A4
		public IGenericServiceContainer RegisterScoped<TService>(Func<IGenericServiceFactory, TService> factory) where TService : class
		{
			ServiceCollectionServiceExtensions.AddScoped<TService>(ButterLibServiceContainer.ServiceContainer, (IServiceProvider sp) => factory(new ButterLibGenericServiceFactory(sp)));
			return this;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000F7DC File Offset: 0x0000D9DC
		public IGenericServiceContainer RegisterScoped<TService, TImplementation>() where TService : class where TImplementation : class, TService
		{
			ServiceCollectionServiceExtensions.AddScoped<TService, TImplementation>(ButterLibServiceContainer.ServiceContainer);
			return this;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000F7FC File Offset: 0x0000D9FC
		public IGenericServiceContainer RegisterScoped<TService, TImplementation>(Func<IGenericServiceFactory, TImplementation> factory) where TService : class where TImplementation : class, TService
		{
			ServiceCollectionServiceExtensions.AddScoped<TService, TImplementation>(ButterLibServiceContainer.ServiceContainer, (IServiceProvider sp) => factory(new ButterLibGenericServiceFactory(sp)));
			return this;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000F834 File Offset: 0x0000DA34
		public IGenericServiceContainer RegisterScoped(Type serviceType, Type implementationType)
		{
			ServiceCollectionServiceExtensions.AddScoped(ButterLibServiceContainer.ServiceContainer, serviceType, implementationType);
			return this;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000F854 File Offset: 0x0000DA54
		public IGenericServiceContainer RegisterScoped(Type serviceType, Func<object> factory)
		{
			ServiceCollectionServiceExtensions.AddScoped<object>(ButterLibServiceContainer.ServiceContainer, (IServiceProvider _) => factory());
			return this;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000F88C File Offset: 0x0000DA8C
		public IGenericServiceContainer RegisterTransient<TService>() where TService : class
		{
			ServiceCollectionServiceExtensions.AddTransient<TService>(ButterLibServiceContainer.ServiceContainer);
			return this;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000F8AC File Offset: 0x0000DAAC
		public IGenericServiceContainer RegisterTransient<TService>(Func<IGenericServiceFactory, TService> factory) where TService : class
		{
			ServiceCollectionServiceExtensions.AddTransient<TService>(ButterLibServiceContainer.ServiceContainer, (IServiceProvider sp) => factory(new ButterLibGenericServiceFactory(sp)));
			return this;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000F8E4 File Offset: 0x0000DAE4
		public IGenericServiceContainer RegisterTransient<TService, TImplementation>() where TService : class where TImplementation : class, TService
		{
			ServiceCollectionServiceExtensions.AddTransient<TService, TImplementation>(ButterLibServiceContainer.ServiceContainer);
			return this;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000F904 File Offset: 0x0000DB04
		public IGenericServiceContainer RegisterTransient<TService, TImplementation>(Func<IGenericServiceFactory, TImplementation> factory) where TService : class where TImplementation : class, TService
		{
			ServiceCollectionServiceExtensions.AddTransient<TService, TImplementation>(ButterLibServiceContainer.ServiceContainer, (IServiceProvider sp) => factory(new ButterLibGenericServiceFactory(sp)));
			return this;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000F93C File Offset: 0x0000DB3C
		public IGenericServiceContainer RegisterTransient(Type serviceType, Type implementationType)
		{
			ServiceCollectionServiceExtensions.AddTransient(ButterLibServiceContainer.ServiceContainer, serviceType, implementationType);
			return this;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000F95C File Offset: 0x0000DB5C
		public IGenericServiceContainer RegisterTransient(Type serviceType, Func<object> factory)
		{
			ServiceCollectionServiceExtensions.AddTransient(ButterLibServiceContainer.ServiceContainer, serviceType, (IServiceProvider _) => factory());
			return this;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000F994 File Offset: 0x0000DB94
		public IGenericServiceProvider Build()
		{
			return new ButterLibGenericServiceProvider();
		}
	}
}
