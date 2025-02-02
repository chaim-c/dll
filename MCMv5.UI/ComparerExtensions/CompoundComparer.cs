using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ComparerExtensions
{
	// Token: 0x02000008 RID: 8
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal sealed class CompoundComparer<[Nullable(2)] T> : Comparer<T>
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000022F4 File Offset: 0x000004F4
		public static IComparer<T> GetComparer(IComparer<T> baseComparer, IComparer<T> nextComparer)
		{
			CompoundComparer<T> comparer = new CompoundComparer<T>();
			comparer.AppendComparison(baseComparer);
			comparer.AppendComparison(nextComparer);
			return comparer.Normalize();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002322 File Offset: 0x00000522
		public CompoundComparer()
		{
			this._comparers = new List<IComparer<T>>();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002338 File Offset: 0x00000538
		public void AppendComparison(IComparer<T> comparer)
		{
			bool flag = comparer is NullComparer<T>;
			if (!flag)
			{
				CompoundComparer<T> other = comparer as CompoundComparer<T>;
				bool flag2 = other != null;
				if (flag2)
				{
					this._comparers.AddRange(other._comparers);
				}
				else
				{
					this._comparers.Add(comparer);
				}
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002388 File Offset: 0x00000588
		public override int Compare(T x, T y)
		{
			foreach (IComparer<T> comparer in this._comparers)
			{
				int result = comparer.Compare(x, y);
				bool flag = result != 0;
				if (flag)
				{
					return result;
				}
			}
			return 0;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023F8 File Offset: 0x000005F8
		public IComparer<T> Normalize()
		{
			bool flag = this._comparers.Count == 0;
			IComparer<T> result;
			if (flag)
			{
				result = NullComparer<T>.Default;
			}
			else
			{
				bool flag2 = this._comparers.Count == 1;
				if (flag2)
				{
					result = this._comparers[0];
				}
				else
				{
					result = this;
				}
			}
			return result;
		}

		// Token: 0x04000005 RID: 5
		private readonly List<IComparer<T>> _comparers;
	}
}
