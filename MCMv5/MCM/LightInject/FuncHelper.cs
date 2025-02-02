using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace MCM.LightInject
{
	// Token: 0x02000122 RID: 290
	[ExcludeFromCodeCoverage]
	internal static class FuncHelper
	{
		// Token: 0x060006E2 RID: 1762 RVA: 0x00016AB8 File Offset: 0x00014CB8
		public static Func<object> CreateScopedFunc(GetInstanceDelegate getInstanceDelegate, object[] constants, Scope scope)
		{
			return () => getInstanceDelegate(constants, scope);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00016AEC File Offset: 0x00014CEC
		public static Func<T> CreateScopedGenericFunc<T>(ServiceContainer serviceContainer, Scope scope)
		{
			return () => (T)((object)serviceContainer.GetInstance(typeof(T), scope));
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00016B1C File Offset: 0x00014D1C
		public static Func<string, T> CreateScopedGenericNamedFunc<T>(ServiceContainer serviceContainer, Scope scope)
		{
			return (string serviceName) => (T)((object)serviceContainer.GetInstance(typeof(T), scope, serviceName));
		}

		// Token: 0x04000218 RID: 536
		public static readonly MethodInfo CreateScopedFuncMethod = typeof(FuncHelper).GetTypeInfo().GetDeclaredMethod("CreateScopedFunc");

		// Token: 0x04000219 RID: 537
		public static readonly MethodInfo CreateScopedGenericFuncMethod = typeof(FuncHelper).GetTypeInfo().GetDeclaredMethod("CreateScopedGenericFunc");

		// Token: 0x0400021A RID: 538
		public static readonly MethodInfo CreateScopedGenericNamedFuncMethod = typeof(FuncHelper).GetTypeInfo().GetDeclaredMethod("CreateScopedGenericNamedFunc");
	}
}
