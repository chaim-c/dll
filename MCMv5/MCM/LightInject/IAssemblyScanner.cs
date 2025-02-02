using System;
using System.Reflection;

namespace MCM.LightInject
{
	// Token: 0x020000DC RID: 220
	internal interface IAssemblyScanner
	{
		// Token: 0x06000499 RID: 1177
		void Scan(Assembly assembly, IServiceRegistry serviceRegistry, Func<ILifetime> lifetime, Func<Type, Type, bool> shouldRegister, Func<Type, Type, string> serviceNameProvider);

		// Token: 0x0600049A RID: 1178
		void Scan(Assembly assembly, IServiceRegistry serviceRegistry);
	}
}
