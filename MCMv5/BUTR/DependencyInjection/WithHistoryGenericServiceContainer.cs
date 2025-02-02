using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BUTR.DependencyInjection
{
	// Token: 0x02000149 RID: 329
	[NullableContext(1)]
	[Nullable(0)]
	public class WithHistoryGenericServiceContainer : IGenericServiceContainer
	{
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x0001D2E0 File Offset: 0x0001B4E0
		public List<Action<IGenericServiceContainer>> History { get; } = new List<Action<IGenericServiceContainer>>();

		// Token: 0x060008C5 RID: 2245 RVA: 0x0001D2E8 File Offset: 0x0001B4E8
		public WithHistoryGenericServiceContainer(IGenericServiceContainer serviceContainer)
		{
			this._serviceContainer = serviceContainer;
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0001D304 File Offset: 0x0001B504
		public IGenericServiceContainer RegisterSingleton<TService>() where TService : class
		{
			this.History.Add(delegate(IGenericServiceContainer serviceContainer)
			{
				serviceContainer.RegisterSingleton<TService>();
			});
			return this._serviceContainer.RegisterSingleton<TService>();
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0001D34C File Offset: 0x0001B54C
		public IGenericServiceContainer RegisterSingleton<TService>(Func<IGenericServiceFactory, TService> factory) where TService : class
		{
			this.History.Add(delegate(IGenericServiceContainer serviceContainer)
			{
				serviceContainer.RegisterSingleton<TService>(factory);
			});
			return this._serviceContainer.RegisterSingleton<TService>(factory);
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0001D394 File Offset: 0x0001B594
		public IGenericServiceContainer RegisterSingleton<TService, TImplementation>() where TService : class where TImplementation : class, TService
		{
			this.History.Add(delegate(IGenericServiceContainer serviceContainer)
			{
				serviceContainer.RegisterSingleton<TService, TImplementation>();
			});
			return this._serviceContainer.RegisterSingleton<TService, TImplementation>();
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0001D3DC File Offset: 0x0001B5DC
		public IGenericServiceContainer RegisterSingleton<TService, TImplementation>(Func<IGenericServiceFactory, TImplementation> factory) where TService : class where TImplementation : class, TService
		{
			this.History.Add(delegate(IGenericServiceContainer serviceContainer)
			{
				serviceContainer.RegisterSingleton<TService, TImplementation>(factory);
			});
			return this._serviceContainer.RegisterSingleton<TService, TImplementation>(factory);
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0001D424 File Offset: 0x0001B624
		public IGenericServiceContainer RegisterSingleton(Type serviceType, Type implementationType)
		{
			this.History.Add(delegate(IGenericServiceContainer serviceContainer)
			{
				serviceContainer.RegisterSingleton(serviceType, implementationType);
			});
			return this._serviceContainer.RegisterSingleton(serviceType, implementationType);
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0001D47C File Offset: 0x0001B67C
		public IGenericServiceContainer RegisterSingleton(Type serviceType, Func<object> factory)
		{
			this.History.Add(delegate(IGenericServiceContainer serviceContainer)
			{
				serviceContainer.RegisterSingleton(serviceType, factory);
			});
			return this._serviceContainer.RegisterSingleton(serviceType, factory);
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0001D4D4 File Offset: 0x0001B6D4
		public IGenericServiceContainer RegisterScoped<TService>() where TService : class
		{
			this.History.Add(delegate(IGenericServiceContainer serviceContainer)
			{
				serviceContainer.RegisterScoped<TService>();
			});
			return this._serviceContainer.RegisterScoped<TService>();
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0001D51C File Offset: 0x0001B71C
		public IGenericServiceContainer RegisterScoped<TService>(Func<IGenericServiceFactory, TService> factory) where TService : class
		{
			this.History.Add(delegate(IGenericServiceContainer serviceContainer)
			{
				serviceContainer.RegisterScoped<TService>(factory);
			});
			return this._serviceContainer.RegisterScoped<TService>(factory);
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0001D564 File Offset: 0x0001B764
		public IGenericServiceContainer RegisterScoped<TService, TImplementation>() where TService : class where TImplementation : class, TService
		{
			this.History.Add(delegate(IGenericServiceContainer serviceContainer)
			{
				serviceContainer.RegisterScoped<TService, TImplementation>();
			});
			return this._serviceContainer.RegisterScoped<TService, TImplementation>();
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0001D5AC File Offset: 0x0001B7AC
		public IGenericServiceContainer RegisterScoped<TService, TImplementation>(Func<IGenericServiceFactory, TImplementation> factory) where TService : class where TImplementation : class, TService
		{
			this.History.Add(delegate(IGenericServiceContainer serviceContainer)
			{
				serviceContainer.RegisterScoped<TService, TImplementation>(factory);
			});
			return this._serviceContainer.RegisterScoped<TService, TImplementation>(factory);
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0001D5F4 File Offset: 0x0001B7F4
		public IGenericServiceContainer RegisterScoped(Type serviceType, Type implementationType)
		{
			this.History.Add(delegate(IGenericServiceContainer serviceContainer)
			{
				serviceContainer.RegisterScoped(serviceType, implementationType);
			});
			return this._serviceContainer.RegisterScoped(serviceType, implementationType);
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0001D64C File Offset: 0x0001B84C
		public IGenericServiceContainer RegisterScoped(Type serviceType, Func<object> factory)
		{
			this.History.Add(delegate(IGenericServiceContainer serviceContainer)
			{
				serviceContainer.RegisterScoped(serviceType, factory);
			});
			return this._serviceContainer.RegisterScoped(serviceType, factory);
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0001D6A4 File Offset: 0x0001B8A4
		public IGenericServiceContainer RegisterTransient<TService>() where TService : class
		{
			this.History.Add(delegate(IGenericServiceContainer serviceContainer)
			{
				serviceContainer.RegisterTransient<TService>();
			});
			return this._serviceContainer.RegisterTransient<TService>();
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0001D6EC File Offset: 0x0001B8EC
		public IGenericServiceContainer RegisterTransient<TService>(Func<IGenericServiceFactory, TService> factory) where TService : class
		{
			this.History.Add(delegate(IGenericServiceContainer serviceContainer)
			{
				serviceContainer.RegisterTransient<TService>(factory);
			});
			return this._serviceContainer.RegisterTransient<TService>(factory);
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0001D734 File Offset: 0x0001B934
		public IGenericServiceContainer RegisterTransient<TService, TImplementation>() where TService : class where TImplementation : class, TService
		{
			this.History.Add(delegate(IGenericServiceContainer serviceContainer)
			{
				serviceContainer.RegisterTransient<TService, TImplementation>();
			});
			return this._serviceContainer.RegisterTransient<TService, TImplementation>();
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0001D77C File Offset: 0x0001B97C
		public IGenericServiceContainer RegisterTransient<TService, TImplementation>(Func<IGenericServiceFactory, TImplementation> factory) where TService : class where TImplementation : class, TService
		{
			this.History.Add(delegate(IGenericServiceContainer serviceContainer)
			{
				serviceContainer.RegisterTransient<TService, TImplementation>(factory);
			});
			return this._serviceContainer.RegisterTransient<TService, TImplementation>(factory);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0001D7C4 File Offset: 0x0001B9C4
		public IGenericServiceContainer RegisterTransient(Type serviceType, Type implementationType)
		{
			this.History.Add(delegate(IGenericServiceContainer serviceContainer)
			{
				serviceContainer.RegisterTransient(serviceType, implementationType);
			});
			return this._serviceContainer.RegisterTransient(serviceType, implementationType);
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0001D81C File Offset: 0x0001BA1C
		public IGenericServiceContainer RegisterTransient(Type serviceType, Func<object> factory)
		{
			this.History.Add(delegate(IGenericServiceContainer serviceContainer)
			{
				serviceContainer.RegisterTransient(serviceType, factory);
			});
			return this._serviceContainer.RegisterTransient(serviceType, factory);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0001D874 File Offset: 0x0001BA74
		public IGenericServiceProvider Build()
		{
			this.History.Clear();
			return this._serviceContainer.Build();
		}

		// Token: 0x0400028D RID: 653
		private readonly IGenericServiceContainer _serviceContainer;
	}
}
