using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Bannerlord.ModuleManager
{
	// Token: 0x0200013F RID: 319
	[NullableContext(1)]
	[Nullable(0)]
	internal static class CollectionsExtensions
	{
		// Token: 0x06000878 RID: 2168 RVA: 0x0001C310 File Offset: 0x0001A510
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

		// Token: 0x06000879 RID: 2169 RVA: 0x0001C380 File Offset: 0x0001A580
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

		// Token: 0x0600087A RID: 2170 RVA: 0x0001C3E4 File Offset: 0x0001A5E4
		public static IEnumerable<TSource> DistinctBy<[Nullable(2)] TSource, [Nullable(2)] TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.DistinctBy(keySelector, null);
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0001C3EE File Offset: 0x0001A5EE
		public static IEnumerable<TSource> DistinctBy<[Nullable(2)] TSource, [Nullable(2)] TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TKey> comparer)
		{
			return CollectionsExtensions.DistinctByIterator<TSource, TKey>(source, keySelector, comparer);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0001C3F8 File Offset: 0x0001A5F8
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
