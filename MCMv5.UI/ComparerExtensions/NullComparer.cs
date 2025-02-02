using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ComparerExtensions
{
	// Token: 0x0200000A RID: 10
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class NullComparer<[Nullable(2)] T> : IComparer<T>, IComparer
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002621 File Offset: 0x00000821
		private NullComparer()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000262B File Offset: 0x0000082B
		public static NullComparer<T> Default { get; } = new NullComparer<T>();

		// Token: 0x06000022 RID: 34 RVA: 0x00002632 File Offset: 0x00000832
		public int Compare(T x, T y)
		{
			return 0;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002638 File Offset: 0x00000838
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
	}
}
