using System;

namespace MCM.LightInject
{
	// Token: 0x020000CC RID: 204
	internal interface IServiceContainer : IServiceRegistry, IServiceFactory, IDisposable
	{
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600047E RID: 1150
		// (set) Token: 0x0600047F RID: 1151
		IScopeManagerProvider ScopeManagerProvider { get; set; }

		// Token: 0x06000480 RID: 1152
		bool CanGetInstance(Type serviceType, string serviceName);

		// Token: 0x06000481 RID: 1153
		object InjectProperties(object instance);

		// Token: 0x06000482 RID: 1154
		void Compile();

		// Token: 0x06000483 RID: 1155
		void Compile(Func<ServiceRegistration, bool> predicate);

		// Token: 0x06000484 RID: 1156
		void Compile<TService>(string serviceName = null);
	}
}
