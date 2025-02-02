using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace MCM.LightInject
{
	// Token: 0x02000123 RID: 291
	[ExcludeFromCodeCoverage]
	internal static class ServiceFactoryLoader
	{
		// Token: 0x060006E6 RID: 1766 RVA: 0x00016B6C File Offset: 0x00014D6C
		public static IServiceFactory LoadServiceFactory(IServiceFactory serviceFactory, IScopeManager scopeManager, Scope scope)
		{
			bool flag = scope != null;
			IServiceFactory result;
			if (flag)
			{
				result = scope;
			}
			else
			{
				Scope currentScope = scopeManager.CurrentScope;
				bool flag2 = currentScope != null;
				if (flag2)
				{
					result = currentScope;
				}
				else
				{
					result = serviceFactory;
				}
			}
			return result;
		}

		// Token: 0x0400021B RID: 539
		public static readonly MethodInfo LoadServiceFactoryMethod = typeof(ServiceFactoryLoader).GetTypeInfo().GetDeclaredMethod("LoadServiceFactory");
	}
}
