using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MCM.LightInject
{
	// Token: 0x02000120 RID: 288
	[ExcludeFromCodeCoverage]
	internal static class LifetimeHelper
	{
		// Token: 0x060006DA RID: 1754 RVA: 0x00016830 File Offset: 0x00014A30
		static LifetimeHelper()
		{
			LifetimeHelper.GetInstanceMethod = typeof(ILifetime).GetTypeInfo().GetDeclaredMethod("GetInstance");
			LifetimeHelper.GetCurrentScopeMethod = typeof(IScopeManager).GetTypeInfo().GetDeclaredProperty("CurrentScope").GetMethod;
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00016889 File Offset: 0x00014A89
		public static MethodInfo GetNonClosingGetInstanceMethod(Type lifetimeType)
		{
			ConcurrentDictionary<Type, MethodInfo> nonClosingGetInstanceMethods = LifetimeHelper.NonClosingGetInstanceMethods;
			Func<Type, MethodInfo> valueFactory;
			if ((valueFactory = LifetimeHelper.<>O.<0>__ResolveNonClosingGetInstanceMethod) == null)
			{
				valueFactory = (LifetimeHelper.<>O.<0>__ResolveNonClosingGetInstanceMethod = new Func<Type, MethodInfo>(LifetimeHelper.ResolveNonClosingGetInstanceMethod));
			}
			return nonClosingGetInstanceMethods.GetOrAdd(lifetimeType, valueFactory);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x000168B4 File Offset: 0x00014AB4
		private static MethodInfo ResolveNonClosingGetInstanceMethod(Type lifetimeType)
		{
			Type[] parameterTypes = new Type[]
			{
				typeof(GetInstanceDelegate),
				typeof(Scope),
				typeof(object[])
			};
			return lifetimeType.GetTypeInfo().DeclaredMethods.SingleOrDefault((MethodInfo m) => (from p in m.GetParameters()
			select p.ParameterType).SequenceEqual(parameterTypes));
		}

		// Token: 0x04000211 RID: 529
		public static readonly MethodInfo GetInstanceMethod;

		// Token: 0x04000212 RID: 530
		public static readonly MethodInfo GetCurrentScopeMethod;

		// Token: 0x04000213 RID: 531
		private static readonly ThreadSafeDictionary<Type, MethodInfo> NonClosingGetInstanceMethods = new ThreadSafeDictionary<Type, MethodInfo>();

		// Token: 0x02000236 RID: 566
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040004F8 RID: 1272
			public static Func<Type, MethodInfo> <0>__ResolveNonClosingGetInstanceMethod;
		}
	}
}
