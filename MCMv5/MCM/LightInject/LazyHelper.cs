using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace MCM.LightInject
{
	// Token: 0x02000125 RID: 293
	[ExcludeFromCodeCoverage]
	internal static class LazyHelper
	{
		// Token: 0x060006EA RID: 1770 RVA: 0x00016C14 File Offset: 0x00014E14
		public static Lazy<T> CreateScopedLazy<T>(ServiceContainer serviceContainer, Scope scope)
		{
			return new Lazy<T>(() => (T)((object)serviceContainer.GetInstance(typeof(T), scope)));
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00016C48 File Offset: 0x00014E48
		public static Lazy<T> CreateScopedLazyFromDelegate<T>(GetInstanceDelegate getInstanceDelegate, object[] constants, Scope scope)
		{
			return new Lazy<T>(() => (T)((object)getInstanceDelegate(constants, scope)));
		}

		// Token: 0x0400021D RID: 541
		public static readonly MethodInfo CreateScopedLazyMethod = typeof(LazyHelper).GetTypeInfo().GetDeclaredMethod("CreateScopedLazy");

		// Token: 0x0400021E RID: 542
		public static readonly MethodInfo CreateScopedLazyFromDelegateMethod = typeof(LazyHelper).GetTypeInfo().GetDeclaredMethod("CreateScopedLazyFromDelegate");
	}
}
