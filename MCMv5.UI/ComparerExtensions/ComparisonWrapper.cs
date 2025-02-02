using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ComparerExtensions
{
	// Token: 0x02000007 RID: 7
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal sealed class ComparisonWrapper<[Nullable(2)] T> : Comparer<T>
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000022A8 File Offset: 0x000004A8
		public static IComparer<T> GetComparer(Func<T, T, int> comparison)
		{
			IComparer<T> source = comparison.Target as IComparer<T>;
			return source ?? new ComparisonWrapper<T>(comparison);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022D1 File Offset: 0x000004D1
		private ComparisonWrapper(Func<T, T, int> comparison)
		{
			this._comparison = comparison;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022E2 File Offset: 0x000004E2
		public override int Compare(T x, T y)
		{
			return this._comparison(x, y);
		}

		// Token: 0x04000004 RID: 4
		private readonly Func<T, T, int> _comparison;
	}
}
