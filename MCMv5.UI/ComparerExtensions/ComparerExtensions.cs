using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ComparerExtensions
{
	// Token: 0x02000006 RID: 6
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ComparerExtensions
	{
		// Token: 0x06000006 RID: 6 RVA: 0x0000209C File Offset: 0x0000029C
		public static IComparer<T> ThenBy<[Nullable(2)] T>(this IComparer<T> baseComparer, IComparer<T> comparer)
		{
			bool flag = baseComparer == null;
			if (flag)
			{
				throw new ArgumentNullException("baseComparer");
			}
			bool flag2 = comparer == null;
			if (flag2)
			{
				throw new ArgumentNullException("comparer");
			}
			return CompoundComparer<T>.GetComparer(baseComparer, comparer);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020E0 File Offset: 0x000002E0
		public static IComparer<T> ThenBy<[Nullable(2)] T>(this IComparer<T> baseComparer, Func<T, T, int> comparison)
		{
			bool flag = baseComparer == null;
			if (flag)
			{
				throw new ArgumentNullException("baseComparer");
			}
			bool flag2 = comparison == null;
			if (flag2)
			{
				throw new ArgumentNullException("comparison");
			}
			IComparer<T> wrapper = ComparisonWrapper<T>.GetComparer(comparison);
			return CompoundComparer<T>.GetComparer(baseComparer, wrapper);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000212C File Offset: 0x0000032C
		public static IComparer<T> ThenBy<[Nullable(2)] T, [Nullable(2)] TKey>(this IComparer<T> baseComparer, Func<T, TKey> keySelector)
		{
			bool flag = baseComparer == null;
			if (flag)
			{
				throw new ArgumentNullException("baseComparer");
			}
			KeyComparer<T> comparer = KeyComparer<T>.OrderBy<TKey>(keySelector);
			return CompoundComparer<T>.GetComparer(baseComparer, comparer);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002164 File Offset: 0x00000364
		public static IComparer<T> ThenBy<[Nullable(2)] T, [Nullable(2)] TKey>(this IComparer<T> baseComparer, Func<T, TKey> keySelector, IComparer<TKey> keyComparer)
		{
			bool flag = baseComparer == null;
			if (flag)
			{
				throw new ArgumentNullException("baseComparer");
			}
			KeyComparer<T> comparer = KeyComparer<T>.OrderBy<TKey>(keySelector, keyComparer);
			return CompoundComparer<T>.GetComparer(baseComparer, comparer);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000219C File Offset: 0x0000039C
		public static IComparer<T> ThenBy<[Nullable(2)] T, [Nullable(2)] TKey>(this IComparer<T> baseComparer, Func<T, TKey> keySelector, Func<TKey, TKey, int> keyComparison)
		{
			bool flag = baseComparer == null;
			if (flag)
			{
				throw new ArgumentNullException("baseComparer");
			}
			KeyComparer<T> comparer = KeyComparer<T>.OrderBy<TKey>(keySelector, keyComparison);
			return CompoundComparer<T>.GetComparer(baseComparer, comparer);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021D4 File Offset: 0x000003D4
		public static IComparer<T> ThenByDescending<[Nullable(2)] T, [Nullable(2)] TKey>(this IComparer<T> baseComparer, Func<T, TKey> keySelector)
		{
			bool flag = baseComparer == null;
			if (flag)
			{
				throw new ArgumentNullException("baseComparer");
			}
			KeyComparer<T> comparer = KeyComparer<T>.OrderByDescending<TKey>(keySelector);
			return CompoundComparer<T>.GetComparer(baseComparer, comparer);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000220C File Offset: 0x0000040C
		public static IComparer<T> ThenByDescending<[Nullable(2)] T, [Nullable(2)] TKey>(this IComparer<T> baseComparer, Func<T, TKey> keySelector, IComparer<TKey> keyComparer)
		{
			bool flag = baseComparer == null;
			if (flag)
			{
				throw new ArgumentNullException("baseComparer");
			}
			KeyComparer<T> comparer = KeyComparer<T>.OrderByDescending<TKey>(keySelector, keyComparer);
			return CompoundComparer<T>.GetComparer(baseComparer, comparer);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002244 File Offset: 0x00000444
		public static IComparer<T> ThenByDescending<[Nullable(2)] T, [Nullable(2)] TKey>(this IComparer<T> baseComparer, Func<T, TKey> keySelector, Func<TKey, TKey, int> keyComparison)
		{
			bool flag = baseComparer == null;
			if (flag)
			{
				throw new ArgumentNullException("baseComparer");
			}
			KeyComparer<T> comparer = KeyComparer<T>.OrderByDescending<TKey>(keySelector, keyComparison);
			return CompoundComparer<T>.GetComparer(baseComparer, comparer);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000227C File Offset: 0x0000047C
		public static IComparer<T> ToComparer<[Nullable(2)] T>(this Func<T, T, int> comparison)
		{
			bool flag = comparison == null;
			if (flag)
			{
				throw new ArgumentNullException("comparison");
			}
			return ComparisonWrapper<T>.GetComparer(comparison);
		}
	}
}
