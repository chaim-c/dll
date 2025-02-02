using System;
using System.Collections.Generic;

namespace MCM.LightInject
{
	// Token: 0x020000CB RID: 203
	internal interface IServiceFactory
	{
		// Token: 0x06000475 RID: 1141
		Scope BeginScope();

		// Token: 0x06000476 RID: 1142
		object GetInstance(Type serviceType);

		// Token: 0x06000477 RID: 1143
		object GetInstance(Type serviceType, object[] arguments);

		// Token: 0x06000478 RID: 1144
		object GetInstance(Type serviceType, string serviceName, object[] arguments);

		// Token: 0x06000479 RID: 1145
		object GetInstance(Type serviceType, string serviceName);

		// Token: 0x0600047A RID: 1146
		object TryGetInstance(Type serviceType);

		// Token: 0x0600047B RID: 1147
		object TryGetInstance(Type serviceType, string serviceName);

		// Token: 0x0600047C RID: 1148
		IEnumerable<object> GetAllInstances(Type serviceType);

		// Token: 0x0600047D RID: 1149
		object Create(Type serviceType);
	}
}
