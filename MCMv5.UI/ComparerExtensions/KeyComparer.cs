using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ComparerExtensions
{
	// Token: 0x02000009 RID: 9
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class KeyComparer<[Nullable(2)] T> : IComparer<T>, IComparer
	{
		// Token: 0x06000017 RID: 23
		public abstract int Compare(T x, T y);

		// Token: 0x06000018 RID: 24 RVA: 0x00002448 File Offset: 0x00000648
		[NullableContext(2)]
		int IComparer.Compare(object x, object y)
		{
			if (x is T)
			{
				T x2 = (T)((object)x);
				if (y is T)
				{
					T y2 = (T)((object)y);
					return this.Compare(x2, y2);
				}
			}
			return 0;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002480 File Offset: 0x00000680
		public static KeyComparer<T> OrderBy<[Nullable(2)] TKey>(Func<T, TKey> keySelector)
		{
			bool flag = keySelector == null;
			if (flag)
			{
				throw new ArgumentNullException("keySelector");
			}
			return new TypedKeyComparer<T, TKey>(keySelector, Comparer<TKey>.Default);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000024B4 File Offset: 0x000006B4
		public static KeyComparer<T> OrderBy<[Nullable(2)] TKey>(Func<T, TKey> keySelector, IComparer<TKey> keyComparer)
		{
			bool flag = keySelector == null;
			if (flag)
			{
				throw new ArgumentNullException("keySelector");
			}
			bool flag2 = keyComparer == null;
			if (flag2)
			{
				throw new ArgumentNullException("keyComparer");
			}
			return new TypedKeyComparer<T, TKey>(keySelector, keyComparer);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024F8 File Offset: 0x000006F8
		public static KeyComparer<T> OrderBy<[Nullable(2)] TKey>(Func<T, TKey> keySelector, Func<TKey, TKey, int> keyComparison)
		{
			bool flag = keySelector == null;
			if (flag)
			{
				throw new ArgumentNullException("keySelector");
			}
			bool flag2 = keyComparison == null;
			if (flag2)
			{
				throw new ArgumentNullException("keyComparison");
			}
			IComparer<TKey> keyComparer = ComparisonWrapper<TKey>.GetComparer(keyComparison);
			return new TypedKeyComparer<T, TKey>(keySelector, keyComparer);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002540 File Offset: 0x00000740
		public static KeyComparer<T> OrderByDescending<[Nullable(2)] TKey>(Func<T, TKey> keySelector)
		{
			bool flag = keySelector == null;
			if (flag)
			{
				throw new ArgumentNullException("keySelector");
			}
			return new TypedKeyComparer<T, TKey>(keySelector, Comparer<TKey>.Default)
			{
				Descending = true
			};
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000257C File Offset: 0x0000077C
		public static KeyComparer<T> OrderByDescending<[Nullable(2)] TKey>(Func<T, TKey> keySelector, IComparer<TKey> keyComparer)
		{
			bool flag = keySelector == null;
			if (flag)
			{
				throw new ArgumentNullException("keySelector");
			}
			bool flag2 = keyComparer == null;
			if (flag2)
			{
				throw new ArgumentNullException("keyComparer");
			}
			return new TypedKeyComparer<T, TKey>(keySelector, keyComparer)
			{
				Descending = true
			};
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000025C8 File Offset: 0x000007C8
		public static KeyComparer<T> OrderByDescending<[Nullable(2)] TKey>(Func<T, TKey> keySelector, Func<TKey, TKey, int> keyComparison)
		{
			bool flag = keySelector == null;
			if (flag)
			{
				throw new ArgumentNullException("keySelector");
			}
			bool flag2 = keyComparison == null;
			if (flag2)
			{
				throw new ArgumentNullException("keyComparison");
			}
			IComparer<TKey> keyComparer = ComparisonWrapper<TKey>.GetComparer(keyComparison);
			return new TypedKeyComparer<T, TKey>(keySelector, keyComparer)
			{
				Descending = true
			};
		}
	}
}
