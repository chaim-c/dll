using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace MCM.LightInject
{
	// Token: 0x02000128 RID: 296
	[ExcludeFromCodeCoverage]
	internal static class EnumerableTypeExtensions
	{
		// Token: 0x06000703 RID: 1795 RVA: 0x00017213 File Offset: 0x00015413
		public static Type GetEnumerableType(this Type returnType)
		{
			ConcurrentDictionary<Type, Type> enumerableTypes = EnumerableTypeExtensions.EnumerableTypes;
			Func<Type, Type> valueFactory;
			if ((valueFactory = EnumerableTypeExtensions.<>O.<0>__CreateEnumerableType) == null)
			{
				valueFactory = (EnumerableTypeExtensions.<>O.<0>__CreateEnumerableType = new Func<Type, Type>(EnumerableTypeExtensions.CreateEnumerableType));
			}
			return enumerableTypes.GetOrAdd(returnType, valueFactory);
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0001723B File Offset: 0x0001543B
		private static Type CreateEnumerableType(Type type)
		{
			return typeof(IEnumerable<>).MakeGenericType(new Type[]
			{
				type
			});
		}

		// Token: 0x04000220 RID: 544
		private static readonly ThreadSafeDictionary<Type, Type> EnumerableTypes = new ThreadSafeDictionary<Type, Type>();

		// Token: 0x02000246 RID: 582
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000516 RID: 1302
			public static Func<Type, Type> <0>__CreateEnumerableType;
		}
	}
}
