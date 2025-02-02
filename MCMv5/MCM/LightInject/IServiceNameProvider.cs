using System;

namespace MCM.LightInject
{
	// Token: 0x020000DD RID: 221
	internal interface IServiceNameProvider
	{
		// Token: 0x0600049B RID: 1179
		string GetServiceName(Type serviceType, Type implementingType);
	}
}
