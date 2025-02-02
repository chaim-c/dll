using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Bannerlord.ModuleManager
{
	// Token: 0x02000052 RID: 82
	[NullableContext(1)]
	[Nullable(0)]
	internal static class CollectionsExtensions
	{
		// Token: 0x0600036A RID: 874 RVA: 0x0000E64C File Offset: 0x0000C84C
		public static int IndexOf<[Nullable(2)] T>(this IReadOnlyList<T> self, T elementToFind)
		{
			int i = 0;
			foreach (T element in self)
			{
				bool flag = object.Equals(element, elementToFind);
				if (flag)
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000E6BC File Offset: 0x0000C8BC
		public static int IndexOf<[Nullable(2)] T>(this IReadOnlyList<T> self, Func<T, bool> preficate)
		{
			int i = 0;
			foreach (T element in self)
			{
				bool flag = preficate(element);
				if (flag)
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000E720 File Offset: 0x0000C920
		public static IEnumerable<TSource> DistinctBy<[Nullable(2)] TSource, [Nullable(2)] TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.DistinctBy(keySelector, null);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000E72A File Offset: 0x0000C92A
		public static IEnumerable<TSource> DistinctBy<[Nullable(2)] TSource, [Nullable(2)] TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TKey> comparer)
		{
			return CollectionsExtensions.DistinctByIterator<TSource, TKey>(source, keySelector, comparer);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000E734 File Offset: 0x0000C934
		private static IEnumerable<TSource> DistinctByIterator<[Nullable(2)] TSource, [Nullable(2)] TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TKey> comparer)
		{
			CollectionsExtensions.<DistinctByIterator>d__4<TSource, TKey> <DistinctByIterator>d__ = new CollectionsExtensions.<DistinctByIterator>d__4<TSource, TKey>(-2);
			<DistinctByIterator>d__.<>3__source = source;
			<DistinctByIterator>d__.<>3__keySelector = keySelector;
			<DistinctByIterator>d__.<>3__comparer = comparer;
			return <DistinctByIterator>d__;
		}
	}
}
